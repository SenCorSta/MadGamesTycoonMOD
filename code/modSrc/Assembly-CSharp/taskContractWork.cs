using System;
using UnityEngine;

// Token: 0x0200030A RID: 778
public class taskContractWork : MonoBehaviour
{
	// Token: 0x06001B2B RID: 6955 RVA: 0x0010F640 File Offset: 0x0010D840
	private void Awake()
	{
		base.transform.position = new Vector3(210f, 0f, 0f);
	}

	// Token: 0x06001B2C RID: 6956 RVA: 0x0010F661 File Offset: 0x0010D861
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001B2D RID: 6957 RVA: 0x0010F66C File Offset: 0x0010D86C
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

	// Token: 0x06001B2E RID: 6958 RVA: 0x0010F712 File Offset: 0x0010D912
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001B2F RID: 6959 RVA: 0x0010F743 File Offset: 0x0010D943
	private void Update()
	{
		this.FindMyContractWork();
	}

	// Token: 0x06001B30 RID: 6960 RVA: 0x0010F74C File Offset: 0x0010D94C
	private void FindMyContractWork()
	{
		if (!this.contract_)
		{
			GameObject gameObject = GameObject.Find("CONTRACTWORK_" + this.contractID.ToString());
			if (gameObject)
			{
				this.contract_ = gameObject.GetComponent<contractWork>();
				return;
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06001B31 RID: 6961 RVA: 0x0010F7A1 File Offset: 0x0010D9A1
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	// Token: 0x06001B32 RID: 6962 RVA: 0x0010F7BD File Offset: 0x0010D9BD
	public Sprite GetPic()
	{
		return this.guiMain_.uiSprites[10];
	}

	// Token: 0x06001B33 RID: 6963 RVA: 0x0010F7CD File Offset: 0x0010D9CD
	public string GetName()
	{
		this.FindMyContractWork();
		if (this.contract_)
		{
			return this.contract_.GetName();
		}
		return "";
	}

	// Token: 0x06001B34 RID: 6964 RVA: 0x0010F7F3 File Offset: 0x0010D9F3
	public int GetStrafe()
	{
		this.FindMyContractWork();
		if (this.contract_)
		{
			return this.contract_.GetStrafe();
		}
		return 0;
	}

	// Token: 0x06001B35 RID: 6965 RVA: 0x0010F815 File Offset: 0x0010DA15
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

	// Token: 0x06001B36 RID: 6966 RVA: 0x0010F850 File Offset: 0x0010DA50
	private void Complete()
	{
		this.FindMyContractWork();
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
		this.mS_.Earn((long)this.contract_.GetGehalt(), 5);
		this.guiMain_.UpdateAuftragsansehen(this.contract_.GetAuftragsansehen());
		this.mS_.AddStudioPoints(1);
		string text = this.tS_.GetText(607);
		text = text.Replace("<NAME>", "<b><color=blue>" + this.contract_.GetName() + "</color></b>");
		text = text.Replace("<NUM>", "<b><color=green>" + this.mS_.GetMoney((long)this.contract_.GetGehalt(), true) + "</color></b>");
		switch (this.contract_.art)
		{
		case 0:
			this.guiMain_.CreateLeftNews(roomID_, this.GetPic(), text, this.rdS_.roomData_SPRITE[1]);
			break;
		case 1:
			this.guiMain_.CreateLeftNews(roomID_, this.GetPic(), text, this.rdS_.roomData_SPRITE[3]);
			break;
		case 2:
			this.guiMain_.CreateLeftNews(roomID_, this.GetPic(), text, this.rdS_.roomData_SPRITE[4]);
			break;
		case 3:
			this.guiMain_.CreateLeftNews(roomID_, this.GetPic(), text, this.rdS_.roomData_SPRITE[5]);
			break;
		case 4:
			this.guiMain_.CreateLeftNews(roomID_, this.GetPic(), text, this.rdS_.roomData_SPRITE[10]);
			break;
		case 5:
			this.guiMain_.CreateLeftNews(roomID_, this.GetPic(), text, this.rdS_.roomData_SPRITE[14]);
			break;
		case 6:
			this.guiMain_.CreateLeftNews(roomID_, this.GetPic(), text, this.rdS_.roomData_SPRITE[17]);
			break;
		case 7:
			this.guiMain_.CreateLeftNews(roomID_, this.GetPic(), text, this.rdS_.roomData_SPRITE[8]);
			break;
		}
		int art = this.contract_.art;
		if (this.contract_)
		{
			UnityEngine.Object.Destroy(this.contract_.gameObject);
		}
		if (!this.DoAutomatic(art))
		{
			if (this.automatic && this.automaticWait)
			{
				taskContractWait taskContractWait = this.guiMain_.AddTask_ContractWait();
				taskContractWait.Init(false);
				taskContractWait.art = art;
				roomScript.taskID = taskContractWait.myID;
				Debug.Log("ContractWorkWait");
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06001B37 RID: 6967 RVA: 0x0010FB2C File Offset: 0x0010DD2C
	private bool DoAutomatic(int art_)
	{
		if (!this.automatic)
		{
			return false;
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("ContractWork");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				contractWork component = array[i].GetComponent<contractWork>();
				if (component && component.art == art_ && !component.IsAngenommen())
				{
					component.angenommen = true;
					this.contract_ = null;
					this.contractID = component.myID;
					this.points = component.GetArbeitsaufwand();
					this.pointsLeft = component.GetArbeitsaufwand();
					return true;
				}
			}
		}
		if (this.automaticWait)
		{
			return false;
		}
		switch (art_)
		{
		case 0:
			this.LeftNews(this.tS_.GetText(729), this.guiMain_.uiSprites[16], this.rdS_.roomData_SPRITE[1]);
			break;
		case 1:
			this.LeftNews(this.tS_.GetText(729), this.guiMain_.uiSprites[16], this.rdS_.roomData_SPRITE[3]);
			break;
		case 2:
			this.LeftNews(this.tS_.GetText(729), this.guiMain_.uiSprites[16], this.rdS_.roomData_SPRITE[4]);
			break;
		case 3:
			this.LeftNews(this.tS_.GetText(729), this.guiMain_.uiSprites[16], this.rdS_.roomData_SPRITE[5]);
			break;
		case 4:
			this.LeftNews(this.tS_.GetText(729), this.guiMain_.uiSprites[16], this.rdS_.roomData_SPRITE[10]);
			break;
		case 5:
			this.LeftNews(this.tS_.GetText(729), this.guiMain_.uiSprites[16], this.rdS_.roomData_SPRITE[14]);
			break;
		case 6:
			this.LeftNews(this.tS_.GetText(729), this.guiMain_.uiSprites[16], this.rdS_.roomData_SPRITE[17]);
			break;
		case 7:
			this.LeftNews(this.tS_.GetText(729), this.guiMain_.uiSprites[16], this.rdS_.roomData_SPRITE[8]);
			break;
		}
		return false;
	}

	// Token: 0x06001B38 RID: 6968 RVA: 0x0010FD9C File Offset: 0x0010DF9C
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

	// Token: 0x06001B39 RID: 6969 RVA: 0x0010FDFC File Offset: 0x0010DFFC
	public void Abbrechen()
	{
		this.FindMyContractWork();
		int strafe = this.GetStrafe();
		if (strafe > 0)
		{
			this.mS_.Pay((long)Mathf.RoundToInt((float)strafe), 14);
			GameObject[] array = GameObject.FindGameObjectsWithTag("Room");
			for (int i = 0; i < array.Length; i++)
			{
				roomScript component = array[i].GetComponent<roomScript>();
				if (component && component.taskID == this.myID)
				{
					this.guiMain_.MoneyPop(Mathf.RoundToInt((float)strafe), new Vector3(component.uiPos.x, component.uiPos.y + 3f, component.uiPos.z), false);
					break;
				}
			}
		}
		if (this.contract_)
		{
			this.contract_.angenommen = false;
			this.guiMain_.UpdateAuftragsansehen(-this.contract_.GetAuftragsansehen());
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x04002248 RID: 8776
	public int myID = -1;

	// Token: 0x04002249 RID: 8777
	public int contractID = -1;

	// Token: 0x0400224A RID: 8778
	public bool automatic;

	// Token: 0x0400224B RID: 8779
	public float points;

	// Token: 0x0400224C RID: 8780
	public float pointsLeft;

	// Token: 0x0400224D RID: 8781
	public contractWork contract_;

	// Token: 0x0400224E RID: 8782
	public bool automaticWait;

	// Token: 0x0400224F RID: 8783
	private GameObject main_;

	// Token: 0x04002250 RID: 8784
	public mainScript mS_;

	// Token: 0x04002251 RID: 8785
	private GUI_Main guiMain_;

	// Token: 0x04002252 RID: 8786
	private textScript tS_;

	// Token: 0x04002253 RID: 8787
	private roomDataScript rdS_;
}
