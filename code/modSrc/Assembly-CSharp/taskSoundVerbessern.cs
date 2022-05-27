using System;
using UnityEngine;

// Token: 0x0200031A RID: 794
public class taskSoundVerbessern : MonoBehaviour
{
	// Token: 0x06001C11 RID: 7185 RVA: 0x0011672C File Offset: 0x0011492C
	private void Awake()
	{
		base.transform.position = new Vector3(140f, 0f, 0f);
	}

	// Token: 0x06001C12 RID: 7186 RVA: 0x0011674D File Offset: 0x0011494D
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001C13 RID: 7187 RVA: 0x00116758 File Offset: 0x00114958
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindGameObjectWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.rdS_)
		{
			this.rdS_ = this.main_.GetComponent<roomDataScript>();
		}
		if (!this.menuSFX_)
		{
			this.menuSFX_ = this.guiMain_.uiObjects[176].GetComponent<Menu_SFX_SoundVerbessern>();
		}
	}

	// Token: 0x06001C14 RID: 7188 RVA: 0x00116845 File Offset: 0x00114A45
	private void Update()
	{
		this.FindMyObject();
		this.FindMyRoom();
		this.GamePublished();
	}

	// Token: 0x06001C15 RID: 7189 RVA: 0x00116859 File Offset: 0x00114A59
	private void GamePublished()
	{
		if (this.gS_ && !this.gS_.inDevelopment)
		{
			this.Abbrechen();
		}
	}

	// Token: 0x06001C16 RID: 7190 RVA: 0x0011687B File Offset: 0x00114A7B
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001C17 RID: 7191 RVA: 0x001168AC File Offset: 0x00114AAC
	private void FindMyObject()
	{
		if (this.gS_)
		{
			return;
		}
		GameObject gameObject = GameObject.Find("GAME_" + this.targetID.ToString());
		if (gameObject)
		{
			this.gS_ = gameObject.GetComponent<gameScript>();
		}
		if (!this.gS_)
		{
			this.Abbrechen();
		}
	}

	// Token: 0x06001C18 RID: 7192 RVA: 0x0011690C File Offset: 0x00114B0C
	private void FindMyRoom()
	{
		if (!this.gS_)
		{
			return;
		}
		this.findMyRoomTimer += Time.deltaTime;
		if (this.findMyRoomTimer < 0.2f)
		{
			return;
		}
		this.findMyRoomTimer = 0f;
		if (this.rS_ && this.rS_.taskID != -1)
		{
			GameObject taskGameObject = this.rS_.taskGameObject;
			if (taskGameObject)
			{
				taskGame component = taskGameObject.GetComponent<taskGame>();
				if (component && component.gameID == this.targetID)
				{
					return;
				}
			}
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("Room");
		for (int i = 0; i < array.Length; i++)
		{
			roomScript component2 = array[i].GetComponent<roomScript>();
			if (component2 && component2.taskID != -1)
			{
				GameObject taskGameObject2 = component2.taskGameObject;
				if (taskGameObject2)
				{
					taskGame component3 = taskGameObject2.GetComponent<taskGame>();
					if (component3 && component3.gameID == this.targetID)
					{
						this.rS_ = component2;
						return;
					}
				}
			}
		}
	}

	// Token: 0x06001C19 RID: 7193 RVA: 0x00116A11 File Offset: 0x00114C11
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	// Token: 0x06001C1A RID: 7194 RVA: 0x00116A2D File Offset: 0x00114C2D
	public Sprite GetPic()
	{
		return this.games_.gameAdds[this.aktuellerAdd + 12];
	}

	// Token: 0x06001C1B RID: 7195 RVA: 0x00116A44 File Offset: 0x00114C44
	public void Work(float f)
	{
		if (this.pointsLeft > 0f)
		{
			this.pointsLeft -= 1f;
			if (this.gS_)
			{
				this.gS_.points_sound += f;
			}
			if (this.pointsLeft <= 0f)
			{
				this.pointsLeft = 0f;
				this.Complete();
			}
		}
	}

	// Token: 0x06001C1C RID: 7196 RVA: 0x00116AB0 File Offset: 0x00114CB0
	public void FindNewAdd()
	{
		this.FindScripts();
		this.FindMyObject();
		this.aktuellerAdd = -1;
		for (int i = 0; i < this.adds.Length; i++)
		{
			if (this.adds[i])
			{
				this.aktuellerAdd = i;
				break;
			}
		}
		if (this.aktuellerAdd != -1)
		{
			float num = (float)this.gS_.GetGesamtDevPoints();
			this.points = num * this.menuSFX_.pointsInPercent[this.aktuellerAdd];
			this.pointsLeft = this.points;
			return;
		}
		this.guiMain_.uiObjects[279].GetComponent<Menu_ROOM_Polishing>().StartPolishingAutomatic(this.gS_, this.myID);
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06001C1D RID: 7197 RVA: 0x00116B68 File Offset: 0x00114D68
	private void Complete()
	{
		this.FindMyObject();
		this.adds[this.aktuellerAdd] = false;
		this.gS_.soundStudio[this.aktuellerAdd] = true;
		this.gS_.costs_entwicklung += (long)this.menuSFX_.GetCosts(this.aktuellerAdd);
		int roomID_ = -1;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Room");
		for (int i = 0; i < array.Length; i++)
		{
			roomScript component = array[i].GetComponent<roomScript>();
			if (component && component.taskID == this.myID)
			{
				roomID_ = component.myID;
				break;
			}
		}
		string text = this.tS_.GetText(921);
		text = text.Replace("<NAME1>", "<b><color=blue>" + this.gS_.GetNameWithTag() + "</color></b>");
		this.guiMain_.CreateLeftNews(roomID_, this.GetPic(), text, this.rdS_.roomData_SPRITE[5]);
		this.FindNewAdd();
	}

	// Token: 0x06001C1E RID: 7198 RVA: 0x00116C64 File Offset: 0x00114E64
	private void LeftNews(string c, Sprite icon, Sprite iconRoom)
	{
		int roomID_ = -1;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Room");
		for (int i = 0; i < array.Length; i++)
		{
			roomScript component = array[i].GetComponent<roomScript>();
			if (component && component.taskID == this.myID)
			{
				roomID_ = component.myID;
				break;
			}
		}
		this.guiMain_.CreateLeftNews(roomID_, icon, c, iconRoom);
	}

	// Token: 0x06001C1F RID: 7199 RVA: 0x00116CC4 File Offset: 0x00114EC4
	public int GetRueckgeld()
	{
		float num = 0f;
		for (int i = 0; i < this.adds.Length; i++)
		{
			if (this.adds[i])
			{
				num += (float)this.menuSFX_.GetCosts(i);
			}
		}
		return Mathf.RoundToInt(num);
	}

	// Token: 0x06001C20 RID: 7200 RVA: 0x00116D0C File Offset: 0x00114F0C
	public void Abbrechen()
	{
		int rueckgeld = this.GetRueckgeld();
		if (rueckgeld > 0)
		{
			this.mS_.Earn((long)Mathf.RoundToInt((float)rueckgeld), 1);
			GameObject[] array = GameObject.FindGameObjectsWithTag("Room");
			for (int i = 0; i < array.Length; i++)
			{
				roomScript component = array[i].GetComponent<roomScript>();
				if (component && component.taskID == this.myID)
				{
					this.guiMain_.MoneyPop(Mathf.RoundToInt((float)rueckgeld), new Vector3(component.uiPos.x, component.uiPos.y + 3f, component.uiPos.z), true);
					break;
				}
			}
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0400231C RID: 8988
	public int myID = -1;

	// Token: 0x0400231D RID: 8989
	public int targetID = -1;

	// Token: 0x0400231E RID: 8990
	public float points;

	// Token: 0x0400231F RID: 8991
	public float pointsLeft;

	// Token: 0x04002320 RID: 8992
	public bool[] adds = new bool[6];

	// Token: 0x04002321 RID: 8993
	public int aktuellerAdd = -1;

	// Token: 0x04002322 RID: 8994
	private GameObject main_;

	// Token: 0x04002323 RID: 8995
	public mainScript mS_;

	// Token: 0x04002324 RID: 8996
	private GUI_Main guiMain_;

	// Token: 0x04002325 RID: 8997
	private textScript tS_;

	// Token: 0x04002326 RID: 8998
	private roomDataScript rdS_;

	// Token: 0x04002327 RID: 8999
	public gameScript gS_;

	// Token: 0x04002328 RID: 9000
	private Menu_SFX_SoundVerbessern menuSFX_;

	// Token: 0x04002329 RID: 9001
	private games games_;

	// Token: 0x0400232A RID: 9002
	public roomScript rS_;

	// Token: 0x0400232B RID: 9003
	private float findMyRoomTimer;
}
