using System;
using UnityEngine;

// Token: 0x02000307 RID: 775
public class taskContractWork : MonoBehaviour
{
	// Token: 0x06001AE1 RID: 6881 RVA: 0x000121BD File Offset: 0x000103BD
	private void Awake()
	{
		base.transform.position = new Vector3(210f, 0f, 0f);
	}

	// Token: 0x06001AE2 RID: 6882 RVA: 0x000121DE File Offset: 0x000103DE
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001AE3 RID: 6883 RVA: 0x00112E48 File Offset: 0x00111048
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

	// Token: 0x06001AE4 RID: 6884 RVA: 0x000121E6 File Offset: 0x000103E6
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001AE5 RID: 6885 RVA: 0x00012217 File Offset: 0x00010417
	private void Update()
	{
		this.FindMyContractWork();
	}

	// Token: 0x06001AE6 RID: 6886 RVA: 0x00112EF0 File Offset: 0x001110F0
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

	// Token: 0x06001AE7 RID: 6887 RVA: 0x0001221F File Offset: 0x0001041F
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	// Token: 0x06001AE8 RID: 6888 RVA: 0x0001223B File Offset: 0x0001043B
	public Sprite GetPic()
	{
		return this.guiMain_.uiSprites[10];
	}

	// Token: 0x06001AE9 RID: 6889 RVA: 0x0001224B File Offset: 0x0001044B
	public string GetName()
	{
		this.FindMyContractWork();
		if (this.contract_)
		{
			return this.contract_.GetName();
		}
		return "";
	}

	// Token: 0x06001AEA RID: 6890 RVA: 0x00012271 File Offset: 0x00010471
	public int GetStrafe()
	{
		this.FindMyContractWork();
		if (this.contract_)
		{
			return this.contract_.GetStrafe();
		}
		return 0;
	}

	// Token: 0x06001AEB RID: 6891 RVA: 0x00012293 File Offset: 0x00010493
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

	// Token: 0x06001AEC RID: 6892 RVA: 0x00112F48 File Offset: 0x00111148
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

	// Token: 0x06001AED RID: 6893 RVA: 0x00113224 File Offset: 0x00111424
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

	// Token: 0x06001AEE RID: 6894 RVA: 0x00113494 File Offset: 0x00111694
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

	// Token: 0x06001AEF RID: 6895 RVA: 0x001134F4 File Offset: 0x001116F4
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

	// Token: 0x0400222E RID: 8750
	public int myID = -1;

	// Token: 0x0400222F RID: 8751
	public int contractID = -1;

	// Token: 0x04002230 RID: 8752
	public bool automatic;

	// Token: 0x04002231 RID: 8753
	public float points;

	// Token: 0x04002232 RID: 8754
	public float pointsLeft;

	// Token: 0x04002233 RID: 8755
	public contractWork contract_;

	// Token: 0x04002234 RID: 8756
	public bool automaticWait;

	// Token: 0x04002235 RID: 8757
	private GameObject main_;

	// Token: 0x04002236 RID: 8758
	public mainScript mS_;

	// Token: 0x04002237 RID: 8759
	private GUI_Main guiMain_;

	// Token: 0x04002238 RID: 8760
	private textScript tS_;

	// Token: 0x04002239 RID: 8761
	private roomDataScript rdS_;
}
