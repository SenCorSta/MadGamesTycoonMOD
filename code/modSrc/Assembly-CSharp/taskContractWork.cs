using System;
using UnityEngine;


public class taskContractWork : MonoBehaviour
{
	
	private void Awake()
	{
		base.transform.position = new Vector3(210f, 0f, 0f);
	}

	
	private void Start()
	{
		this.FindScripts();
	}

	
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

	
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	
	private void Update()
	{
		this.FindMyContractWork();
	}

	
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

	
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	
	public Sprite GetPic()
	{
		return this.guiMain_.uiSprites[10];
	}

	
	public string GetName()
	{
		this.FindMyContractWork();
		if (this.contract_)
		{
			return this.contract_.GetName();
		}
		return "";
	}

	
	public int GetStrafe()
	{
		this.FindMyContractWork();
		if (this.contract_)
		{
			return this.contract_.GetStrafe();
		}
		return 0;
	}

	
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

	
	public int myID = -1;

	
	public int contractID = -1;

	
	public bool automatic;

	
	public float points;

	
	public float pointsLeft;

	
	public contractWork contract_;

	
	public bool automaticWait;

	
	private GameObject main_;

	
	public mainScript mS_;

	
	private GUI_Main guiMain_;

	
	private textScript tS_;

	
	private roomDataScript rdS_;
}
