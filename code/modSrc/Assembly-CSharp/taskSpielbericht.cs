using System;
using UnityEngine;

// Token: 0x0200031B RID: 795
public class taskSpielbericht : MonoBehaviour
{
	// Token: 0x06001C22 RID: 7202 RVA: 0x00116E04 File Offset: 0x00115004
	private void Awake()
	{
		base.transform.position = new Vector3(100f, 0f, 0f);
	}

	// Token: 0x06001C23 RID: 7203 RVA: 0x00116E25 File Offset: 0x00115025
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001C24 RID: 7204 RVA: 0x00116E30 File Offset: 0x00115030
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
	}

	// Token: 0x06001C25 RID: 7205 RVA: 0x00116ED6 File Offset: 0x001150D6
	private void Update()
	{
		this.FindMyObject();
	}

	// Token: 0x06001C26 RID: 7206 RVA: 0x00116EDE File Offset: 0x001150DE
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001C27 RID: 7207 RVA: 0x00116F10 File Offset: 0x00115110
	private void FindMyObject()
	{
		if (this.gS_)
		{
			return;
		}
		if (!this.gS_)
		{
			GameObject gameObject = GameObject.Find("GAME_" + this.targetID.ToString());
			if (gameObject)
			{
				this.gS_ = gameObject.GetComponent<gameScript>();
			}
		}
		if (!this.gS_)
		{
			this.Abbrechen();
		}
	}

	// Token: 0x06001C28 RID: 7208 RVA: 0x00116F7A File Offset: 0x0011517A
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	// Token: 0x06001C29 RID: 7209 RVA: 0x00116F96 File Offset: 0x00115196
	public Sprite GetPic()
	{
		return this.guiMain_.uiSprites[18];
	}

	// Token: 0x06001C2A RID: 7210 RVA: 0x00116FA6 File Offset: 0x001151A6
	public void Work(float f)
	{
		if (this.pointsLeft > 0f)
		{
			this.pointsLeft -= f;
			if (this.pointsLeft <= 0f)
			{
				this.pointsLeft = 0f;
				this.Complete();
			}
		}
	}

	// Token: 0x06001C2B RID: 7211 RVA: 0x00116FE4 File Offset: 0x001151E4
	private void Complete()
	{
		this.FindMyObject();
		int roomID_ = -1;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Room");
		roomScript roomScript = null;
		for (int i = 0; i < array.Length; i++)
		{
			roomScript = array[i].GetComponent<roomScript>();
			if (roomScript && roomScript.taskID == this.myID)
			{
				roomID_ = roomScript.myID;
				break;
			}
		}
		string text = this.tS_.GetText(929);
		text = text.Replace("<NAME1>", "<b><color=blue>" + this.gS_.GetNameWithTag() + "</color></b>");
		this.guiMain_.CreateLeftNews(roomID_, this.GetPic(), text, this.rdS_.roomData_SPRITE[3]);
		this.gS_.SetSpielbericht();
		if (!this.DoAutomatic())
		{
			if (this.automatic && this.automaticWait)
			{
				taskWait taskWait = this.guiMain_.AddTask_Wait();
				taskWait.Init(false);
				taskWait.art = 0;
				roomScript.taskID = taskWait.myID;
				Debug.Log("SpielberichtWorkWait");
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06001C2C RID: 7212 RVA: 0x00117100 File Offset: 0x00115300
	private bool DoAutomatic()
	{
		this.FindMyObject();
		if (!this.automatic)
		{
			return false;
		}
		Menu_QA_NewSpielberichtSelectGame component = this.guiMain_.uiObjects[181].GetComponent<Menu_QA_NewSpielberichtSelectGame>();
		if (component)
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i])
				{
					gameScript component2 = array[i].GetComponent<gameScript>();
					if (component2 && component.CheckGameData(component2))
					{
						this.targetID = component2.myID;
						this.gS_ = component2;
						this.points = (float)component.GetWorkPoints(component2);
						this.pointsLeft = this.points;
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06001C2D RID: 7213 RVA: 0x001171AC File Offset: 0x001153AC
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

	// Token: 0x06001C2E RID: 7214 RVA: 0x0001A799 File Offset: 0x00018999
	public int GetRueckgeld()
	{
		return 0;
	}

	// Token: 0x06001C2F RID: 7215 RVA: 0x0011720C File Offset: 0x0011540C
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

	// Token: 0x0400232C RID: 9004
	public int myID = -1;

	// Token: 0x0400232D RID: 9005
	public int targetID = -1;

	// Token: 0x0400232E RID: 9006
	public bool automatic;

	// Token: 0x0400232F RID: 9007
	public float points;

	// Token: 0x04002330 RID: 9008
	public float pointsLeft;

	// Token: 0x04002331 RID: 9009
	public bool automaticWait;

	// Token: 0x04002332 RID: 9010
	private GameObject main_;

	// Token: 0x04002333 RID: 9011
	public mainScript mS_;

	// Token: 0x04002334 RID: 9012
	private GUI_Main guiMain_;

	// Token: 0x04002335 RID: 9013
	private textScript tS_;

	// Token: 0x04002336 RID: 9014
	private roomDataScript rdS_;

	// Token: 0x04002337 RID: 9015
	public gameScript gS_;
}
