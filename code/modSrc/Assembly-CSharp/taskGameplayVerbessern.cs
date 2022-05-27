using System;
using UnityEngine;

// Token: 0x02000311 RID: 785
public class taskGameplayVerbessern : MonoBehaviour
{
	// Token: 0x06001B8A RID: 7050 RVA: 0x00112F6B File Offset: 0x0011116B
	private void Awake()
	{
		base.transform.position = new Vector3(110f, 0f, 0f);
	}

	// Token: 0x06001B8B RID: 7051 RVA: 0x00112F8C File Offset: 0x0011118C
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001B8C RID: 7052 RVA: 0x00112F94 File Offset: 0x00111194
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
		if (!this.menuQA_)
		{
			this.menuQA_ = this.guiMain_.uiObjects[172].GetComponent<Menu_QA_GameplayVerbessern>();
		}
	}

	// Token: 0x06001B8D RID: 7053 RVA: 0x00113081 File Offset: 0x00111281
	private void Update()
	{
		this.FindMyObject();
		this.FindMyRoom();
		this.GamePublished();
	}

	// Token: 0x06001B8E RID: 7054 RVA: 0x00113095 File Offset: 0x00111295
	private void GamePublished()
	{
		if (this.gS_ && !this.gS_.inDevelopment)
		{
			this.Abbrechen();
		}
	}

	// Token: 0x06001B8F RID: 7055 RVA: 0x001130B7 File Offset: 0x001112B7
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001B90 RID: 7056 RVA: 0x001130E8 File Offset: 0x001112E8
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

	// Token: 0x06001B91 RID: 7057 RVA: 0x00113148 File Offset: 0x00111348
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

	// Token: 0x06001B92 RID: 7058 RVA: 0x0011324D File Offset: 0x0011144D
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	// Token: 0x06001B93 RID: 7059 RVA: 0x00113269 File Offset: 0x00111469
	public Sprite GetPic()
	{
		return this.games_.gameAdds[this.aktuellerAdd];
	}

	// Token: 0x06001B94 RID: 7060 RVA: 0x00113280 File Offset: 0x00111480
	public void Work(float f)
	{
		if (this.pointsLeft > 0f)
		{
			this.pointsLeft -= 1f;
			if (this.gS_)
			{
				this.gS_.points_gameplay += f;
			}
			if (this.pointsLeft <= 0f)
			{
				this.pointsLeft = 0f;
				this.Complete();
			}
		}
	}

	// Token: 0x06001B95 RID: 7061 RVA: 0x001132EC File Offset: 0x001114EC
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
			this.points = num * this.menuQA_.pointsInPercent[this.aktuellerAdd];
			this.pointsLeft = this.points;
			return;
		}
		if (!this.autoBugfix)
		{
			this.guiMain_.uiObjects[279].GetComponent<Menu_ROOM_Polishing>().StartPolishingAutomatic(this.gS_, this.myID);
		}
		else
		{
			this.guiMain_.uiObjects[171].GetComponent<Menu_QA_BugfixingSelectGame>().StartBugfixingAutomatic(this.gS_, this.myID);
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06001B96 RID: 7062 RVA: 0x001133D4 File Offset: 0x001115D4
	private void Complete()
	{
		this.FindMyObject();
		this.adds[this.aktuellerAdd] = false;
		this.gS_.gameplayStudio[this.aktuellerAdd] = true;
		this.gS_.costs_entwicklung += (long)this.menuQA_.GetCosts(this.aktuellerAdd);
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
		string text = this.tS_.GetText(915);
		text = text.Replace("<NAME1>", "<b><color=blue>" + this.gS_.GetNameWithTag() + "</color></b>");
		this.guiMain_.CreateLeftNews(roomID_, this.GetPic(), text, this.rdS_.roomData_SPRITE[3]);
		this.FindNewAdd();
	}

	// Token: 0x06001B97 RID: 7063 RVA: 0x001134D0 File Offset: 0x001116D0
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

	// Token: 0x06001B98 RID: 7064 RVA: 0x00113530 File Offset: 0x00111730
	public int GetRueckgeld()
	{
		float num = 0f;
		for (int i = 0; i < this.adds.Length; i++)
		{
			if (this.adds[i])
			{
				num += (float)this.menuQA_.GetCosts(i);
			}
		}
		return Mathf.RoundToInt(num);
	}

	// Token: 0x06001B99 RID: 7065 RVA: 0x00113578 File Offset: 0x00111778
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

	// Token: 0x040022A0 RID: 8864
	public int myID = -1;

	// Token: 0x040022A1 RID: 8865
	public int targetID = -1;

	// Token: 0x040022A2 RID: 8866
	public float points;

	// Token: 0x040022A3 RID: 8867
	public float pointsLeft;

	// Token: 0x040022A4 RID: 8868
	public bool[] adds = new bool[6];

	// Token: 0x040022A5 RID: 8869
	public int aktuellerAdd = -1;

	// Token: 0x040022A6 RID: 8870
	public bool autoBugfix;

	// Token: 0x040022A7 RID: 8871
	private GameObject main_;

	// Token: 0x040022A8 RID: 8872
	public mainScript mS_;

	// Token: 0x040022A9 RID: 8873
	private GUI_Main guiMain_;

	// Token: 0x040022AA RID: 8874
	private textScript tS_;

	// Token: 0x040022AB RID: 8875
	private roomDataScript rdS_;

	// Token: 0x040022AC RID: 8876
	public gameScript gS_;

	// Token: 0x040022AD RID: 8877
	private Menu_QA_GameplayVerbessern menuQA_;

	// Token: 0x040022AE RID: 8878
	private games games_;

	// Token: 0x040022AF RID: 8879
	public roomScript rS_;

	// Token: 0x040022B0 RID: 8880
	private float findMyRoomTimer;
}
