using System;
using UnityEngine;

// Token: 0x0200030F RID: 783
public class taskGrafikVerbessern : MonoBehaviour
{
	// Token: 0x06001B51 RID: 6993 RVA: 0x00012873 File Offset: 0x00010A73
	private void Awake()
	{
		base.transform.position = new Vector3(130f, 0f, 0f);
	}

	// Token: 0x06001B52 RID: 6994 RVA: 0x00012894 File Offset: 0x00010A94
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001B53 RID: 6995 RVA: 0x001167B4 File Offset: 0x001149B4
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
		if (!this.menuGFX_)
		{
			this.menuGFX_ = this.guiMain_.uiObjects[174].GetComponent<Menu_GFX_GrafikVerbessern>();
		}
	}

	// Token: 0x06001B54 RID: 6996 RVA: 0x0001289C File Offset: 0x00010A9C
	private void Update()
	{
		this.FindMyObject();
		this.FindMyRoom();
		this.GamePublished();
	}

	// Token: 0x06001B55 RID: 6997 RVA: 0x000128B0 File Offset: 0x00010AB0
	private void GamePublished()
	{
		if (this.gS_ && !this.gS_.inDevelopment)
		{
			this.Abbrechen();
		}
	}

	// Token: 0x06001B56 RID: 6998 RVA: 0x000128D2 File Offset: 0x00010AD2
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001B57 RID: 6999 RVA: 0x001168A4 File Offset: 0x00114AA4
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

	// Token: 0x06001B58 RID: 7000 RVA: 0x00116904 File Offset: 0x00114B04
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

	// Token: 0x06001B59 RID: 7001 RVA: 0x00012903 File Offset: 0x00010B03
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	// Token: 0x06001B5A RID: 7002 RVA: 0x0001291F File Offset: 0x00010B1F
	public Sprite GetPic()
	{
		return this.games_.gameAdds[this.aktuellerAdd + 6];
	}

	// Token: 0x06001B5B RID: 7003 RVA: 0x00116A0C File Offset: 0x00114C0C
	public void Work(float f)
	{
		if (this.pointsLeft > 0f)
		{
			this.pointsLeft -= 1f;
			if (this.gS_)
			{
				this.gS_.points_grafik += f;
			}
			if (this.pointsLeft <= 0f)
			{
				this.pointsLeft = 0f;
				this.Complete();
			}
		}
	}

	// Token: 0x06001B5C RID: 7004 RVA: 0x00116A78 File Offset: 0x00114C78
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
			this.points = num * this.menuGFX_.pointsInPercent[this.aktuellerAdd];
			this.pointsLeft = this.points;
			return;
		}
		this.guiMain_.uiObjects[279].GetComponent<Menu_ROOM_Polishing>().StartPolishingAutomatic(this.gS_, this.myID);
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06001B5D RID: 7005 RVA: 0x00116B30 File Offset: 0x00114D30
	private void Complete()
	{
		this.FindMyObject();
		this.adds[this.aktuellerAdd] = false;
		this.gS_.grafikStudio[this.aktuellerAdd] = true;
		this.gS_.costs_entwicklung += (long)this.menuGFX_.GetCosts(this.aktuellerAdd);
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
		string text = this.tS_.GetText(918);
		text = text.Replace("<NAME1>", "<b><color=blue>" + this.gS_.GetNameWithTag() + "</color></b>");
		this.guiMain_.CreateLeftNews(roomID_, this.GetPic(), text, this.rdS_.roomData_SPRITE[4]);
		this.FindNewAdd();
	}

	// Token: 0x06001B5E RID: 7006 RVA: 0x00116C2C File Offset: 0x00114E2C
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

	// Token: 0x06001B5F RID: 7007 RVA: 0x00116C8C File Offset: 0x00114E8C
	public int GetRueckgeld()
	{
		float num = 0f;
		for (int i = 0; i < this.adds.Length; i++)
		{
			if (this.adds[i])
			{
				num += (float)this.menuGFX_.GetCosts(i);
			}
		}
		return Mathf.RoundToInt(num);
	}

	// Token: 0x06001B60 RID: 7008 RVA: 0x00116CD4 File Offset: 0x00114ED4
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

	// Token: 0x04002297 RID: 8855
	public int myID = -1;

	// Token: 0x04002298 RID: 8856
	public int targetID = -1;

	// Token: 0x04002299 RID: 8857
	public float points;

	// Token: 0x0400229A RID: 8858
	public float pointsLeft;

	// Token: 0x0400229B RID: 8859
	public bool[] adds = new bool[6];

	// Token: 0x0400229C RID: 8860
	public int aktuellerAdd = -1;

	// Token: 0x0400229D RID: 8861
	private GameObject main_;

	// Token: 0x0400229E RID: 8862
	public mainScript mS_;

	// Token: 0x0400229F RID: 8863
	private GUI_Main guiMain_;

	// Token: 0x040022A0 RID: 8864
	private textScript tS_;

	// Token: 0x040022A1 RID: 8865
	private roomDataScript rdS_;

	// Token: 0x040022A2 RID: 8866
	public gameScript gS_;

	// Token: 0x040022A3 RID: 8867
	private Menu_GFX_GrafikVerbessern menuGFX_;

	// Token: 0x040022A4 RID: 8868
	private games games_;

	// Token: 0x040022A5 RID: 8869
	public roomScript rS_;

	// Token: 0x040022A6 RID: 8870
	private float findMyRoomTimer;
}
