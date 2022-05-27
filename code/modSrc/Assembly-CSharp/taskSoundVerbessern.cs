using System;
using UnityEngine;

// Token: 0x02000317 RID: 791
public class taskSoundVerbessern : MonoBehaviour
{
	// Token: 0x06001BC7 RID: 7111 RVA: 0x00013030 File Offset: 0x00011230
	private void Awake()
	{
		base.transform.position = new Vector3(140f, 0f, 0f);
	}

	// Token: 0x06001BC8 RID: 7112 RVA: 0x00013051 File Offset: 0x00011251
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001BC9 RID: 7113 RVA: 0x001190D4 File Offset: 0x001172D4
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

	// Token: 0x06001BCA RID: 7114 RVA: 0x00013059 File Offset: 0x00011259
	private void Update()
	{
		this.FindMyObject();
		this.FindMyRoom();
		this.GamePublished();
	}

	// Token: 0x06001BCB RID: 7115 RVA: 0x0001306D File Offset: 0x0001126D
	private void GamePublished()
	{
		if (this.gS_ && !this.gS_.inDevelopment)
		{
			this.Abbrechen();
		}
	}

	// Token: 0x06001BCC RID: 7116 RVA: 0x0001308F File Offset: 0x0001128F
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001BCD RID: 7117 RVA: 0x001191C4 File Offset: 0x001173C4
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

	// Token: 0x06001BCE RID: 7118 RVA: 0x00119224 File Offset: 0x00117424
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

	// Token: 0x06001BCF RID: 7119 RVA: 0x000130C0 File Offset: 0x000112C0
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	// Token: 0x06001BD0 RID: 7120 RVA: 0x000130DC File Offset: 0x000112DC
	public Sprite GetPic()
	{
		return this.games_.gameAdds[this.aktuellerAdd + 12];
	}

	// Token: 0x06001BD1 RID: 7121 RVA: 0x0011932C File Offset: 0x0011752C
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

	// Token: 0x06001BD2 RID: 7122 RVA: 0x00119398 File Offset: 0x00117598
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

	// Token: 0x06001BD3 RID: 7123 RVA: 0x00119450 File Offset: 0x00117650
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

	// Token: 0x06001BD4 RID: 7124 RVA: 0x0011954C File Offset: 0x0011774C
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

	// Token: 0x06001BD5 RID: 7125 RVA: 0x001195AC File Offset: 0x001177AC
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

	// Token: 0x06001BD6 RID: 7126 RVA: 0x001195F4 File Offset: 0x001177F4
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

	// Token: 0x04002302 RID: 8962
	public int myID = -1;

	// Token: 0x04002303 RID: 8963
	public int targetID = -1;

	// Token: 0x04002304 RID: 8964
	public float points;

	// Token: 0x04002305 RID: 8965
	public float pointsLeft;

	// Token: 0x04002306 RID: 8966
	public bool[] adds = new bool[6];

	// Token: 0x04002307 RID: 8967
	public int aktuellerAdd = -1;

	// Token: 0x04002308 RID: 8968
	private GameObject main_;

	// Token: 0x04002309 RID: 8969
	public mainScript mS_;

	// Token: 0x0400230A RID: 8970
	private GUI_Main guiMain_;

	// Token: 0x0400230B RID: 8971
	private textScript tS_;

	// Token: 0x0400230C RID: 8972
	private roomDataScript rdS_;

	// Token: 0x0400230D RID: 8973
	public gameScript gS_;

	// Token: 0x0400230E RID: 8974
	private Menu_SFX_SoundVerbessern menuSFX_;

	// Token: 0x0400230F RID: 8975
	private games games_;

	// Token: 0x04002310 RID: 8976
	public roomScript rS_;

	// Token: 0x04002311 RID: 8977
	private float findMyRoomTimer;
}
