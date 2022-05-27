using System;
using UnityEngine;

// Token: 0x02000318 RID: 792
public class taskSpielbericht : MonoBehaviour
{
	// Token: 0x06001BD8 RID: 7128 RVA: 0x0001311C File Offset: 0x0001131C
	private void Awake()
	{
		base.transform.position = new Vector3(100f, 0f, 0f);
	}

	// Token: 0x06001BD9 RID: 7129 RVA: 0x0001313D File Offset: 0x0001133D
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001BDA RID: 7130 RVA: 0x001196A8 File Offset: 0x001178A8
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

	// Token: 0x06001BDB RID: 7131 RVA: 0x00013145 File Offset: 0x00011345
	private void Update()
	{
		this.FindMyObject();
	}

	// Token: 0x06001BDC RID: 7132 RVA: 0x0001314D File Offset: 0x0001134D
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001BDD RID: 7133 RVA: 0x00119750 File Offset: 0x00117950
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

	// Token: 0x06001BDE RID: 7134 RVA: 0x0001317E File Offset: 0x0001137E
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	// Token: 0x06001BDF RID: 7135 RVA: 0x0001319A File Offset: 0x0001139A
	public Sprite GetPic()
	{
		return this.guiMain_.uiSprites[18];
	}

	// Token: 0x06001BE0 RID: 7136 RVA: 0x000131AA File Offset: 0x000113AA
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

	// Token: 0x06001BE1 RID: 7137 RVA: 0x001197BC File Offset: 0x001179BC
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

	// Token: 0x06001BE2 RID: 7138 RVA: 0x001198D8 File Offset: 0x00117AD8
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

	// Token: 0x06001BE3 RID: 7139 RVA: 0x00119984 File Offset: 0x00117B84
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

	// Token: 0x06001BE4 RID: 7140 RVA: 0x000030EA File Offset: 0x000012EA
	public int GetRueckgeld()
	{
		return 0;
	}

	// Token: 0x06001BE5 RID: 7141 RVA: 0x001199E4 File Offset: 0x00117BE4
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

	// Token: 0x04002312 RID: 8978
	public int myID = -1;

	// Token: 0x04002313 RID: 8979
	public int targetID = -1;

	// Token: 0x04002314 RID: 8980
	public bool automatic;

	// Token: 0x04002315 RID: 8981
	public float points;

	// Token: 0x04002316 RID: 8982
	public float pointsLeft;

	// Token: 0x04002317 RID: 8983
	public bool automaticWait;

	// Token: 0x04002318 RID: 8984
	private GameObject main_;

	// Token: 0x04002319 RID: 8985
	public mainScript mS_;

	// Token: 0x0400231A RID: 8986
	private GUI_Main guiMain_;

	// Token: 0x0400231B RID: 8987
	private textScript tS_;

	// Token: 0x0400231C RID: 8988
	private roomDataScript rdS_;

	// Token: 0x0400231D RID: 8989
	public gameScript gS_;
}
