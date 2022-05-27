using System;
using UnityEngine;

// Token: 0x0200030E RID: 782
public class taskGameplayVerbessern : MonoBehaviour
{
	// Token: 0x06001B40 RID: 6976 RVA: 0x0001278A File Offset: 0x0001098A
	private void Awake()
	{
		base.transform.position = new Vector3(110f, 0f, 0f);
	}

	// Token: 0x06001B41 RID: 6977 RVA: 0x000127AB File Offset: 0x000109AB
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001B42 RID: 6978 RVA: 0x001161B0 File Offset: 0x001143B0
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

	// Token: 0x06001B43 RID: 6979 RVA: 0x000127B3 File Offset: 0x000109B3
	private void Update()
	{
		this.FindMyObject();
		this.FindMyRoom();
		this.GamePublished();
	}

	// Token: 0x06001B44 RID: 6980 RVA: 0x000127C7 File Offset: 0x000109C7
	private void GamePublished()
	{
		if (this.gS_ && !this.gS_.inDevelopment)
		{
			this.Abbrechen();
		}
	}

	// Token: 0x06001B45 RID: 6981 RVA: 0x000127E9 File Offset: 0x000109E9
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001B46 RID: 6982 RVA: 0x001162A0 File Offset: 0x001144A0
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

	// Token: 0x06001B47 RID: 6983 RVA: 0x00116300 File Offset: 0x00114500
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

	// Token: 0x06001B48 RID: 6984 RVA: 0x0001281A File Offset: 0x00010A1A
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	// Token: 0x06001B49 RID: 6985 RVA: 0x00012836 File Offset: 0x00010A36
	public Sprite GetPic()
	{
		return this.games_.gameAdds[this.aktuellerAdd];
	}

	// Token: 0x06001B4A RID: 6986 RVA: 0x00116408 File Offset: 0x00114608
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

	// Token: 0x06001B4B RID: 6987 RVA: 0x00116474 File Offset: 0x00114674
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

	// Token: 0x06001B4C RID: 6988 RVA: 0x0011655C File Offset: 0x0011475C
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

	// Token: 0x06001B4D RID: 6989 RVA: 0x00116658 File Offset: 0x00114858
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

	// Token: 0x06001B4E RID: 6990 RVA: 0x001166B8 File Offset: 0x001148B8
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

	// Token: 0x06001B4F RID: 6991 RVA: 0x00116700 File Offset: 0x00114900
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

	// Token: 0x04002286 RID: 8838
	public int myID = -1;

	// Token: 0x04002287 RID: 8839
	public int targetID = -1;

	// Token: 0x04002288 RID: 8840
	public float points;

	// Token: 0x04002289 RID: 8841
	public float pointsLeft;

	// Token: 0x0400228A RID: 8842
	public bool[] adds = new bool[6];

	// Token: 0x0400228B RID: 8843
	public int aktuellerAdd = -1;

	// Token: 0x0400228C RID: 8844
	public bool autoBugfix;

	// Token: 0x0400228D RID: 8845
	private GameObject main_;

	// Token: 0x0400228E RID: 8846
	public mainScript mS_;

	// Token: 0x0400228F RID: 8847
	private GUI_Main guiMain_;

	// Token: 0x04002290 RID: 8848
	private textScript tS_;

	// Token: 0x04002291 RID: 8849
	private roomDataScript rdS_;

	// Token: 0x04002292 RID: 8850
	public gameScript gS_;

	// Token: 0x04002293 RID: 8851
	private Menu_QA_GameplayVerbessern menuQA_;

	// Token: 0x04002294 RID: 8852
	private games games_;

	// Token: 0x04002295 RID: 8853
	public roomScript rS_;

	// Token: 0x04002296 RID: 8854
	private float findMyRoomTimer;
}
