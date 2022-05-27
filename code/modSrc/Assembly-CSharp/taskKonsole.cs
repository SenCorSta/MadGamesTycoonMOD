using System;
using UnityEngine;


public class taskKonsole : MonoBehaviour
{
	
	private void Awake()
	{
		base.transform.position = new Vector3(180f, 0f, 0f);
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
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.rdS_)
		{
			this.rdS_ = this.main_.GetComponent<roomDataScript>();
		}
		if (!this.hardware_)
		{
			this.hardware_ = this.main_.GetComponent<hardware>();
		}
		if (!this.hardwareFeatures_)
		{
			this.hardwareFeatures_ = this.main_.GetComponent<hardwareFeatures>();
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
		this.FindMyKonsole();
		this.FindMyLeitenderTechniker();
	}

	
	private void FindMyKonsole()
	{
		if (!this.pS_)
		{
			GameObject gameObject = GameObject.Find("PLATFORM_" + this.konsoleID.ToString());
			if (gameObject)
			{
				this.pS_ = gameObject.GetComponent<platformScript>();
				return;
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	
	private void FindMyLeitenderTechniker()
	{
		if (this.leitenderTechnikerID == -1)
		{
			return;
		}
		if (this.techniker_)
		{
			if (this.techniker_.roomS_)
			{
				if (this.techniker_.roomS_.taskID != this.myID)
				{
					this.leitenderTechnikerID = -1;
					this.techniker_ = null;
					return;
				}
			}
			else
			{
				this.leitenderTechnikerID = -1;
				this.techniker_ = null;
			}
			return;
		}
		GameObject gameObject = GameObject.Find("CHAR_" + this.leitenderTechnikerID.ToString());
		if (gameObject)
		{
			this.techniker_ = gameObject.GetComponent<characterScript>();
			return;
		}
		this.leitenderTechnikerID = -1;
		this.techniker_ = null;
	}

	
	public float GetProzent()
	{
		this.FindScripts();
		if (!this.pS_)
		{
			return -1f;
		}
		return this.pS_.GetProzent();
	}

	
	public void Work(float f)
	{
		this.FindScripts();
		if (!this.pS_)
		{
			this.FindMyKonsole();
		}
		if (!this.pS_)
		{
			return;
		}
		if (this.pS_.devPoints > 0f)
		{
			this.pS_.devPoints -= f;
			if (this.pS_.devPoints <= 0f)
			{
				this.pS_.devPoints = 0f;
				this.Complete();
			}
		}
	}

	
	private void Complete()
	{
		this.FindScripts();
		if (!this.pS_)
		{
			this.FindMyKonsole();
		}
		if (!this.pS_)
		{
			return;
		}
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
		string tooltip_ = this.tS_.GetText(1635) + "\n<b>" + this.pS_.GetName() + "</b>";
		this.guiMain_.CreateLeftNews(roomID_, this.pS_.GetTypSprite(), tooltip_, this.rdS_.roomData_SPRITE[8]);
		if (!this.mS_.multiplayer)
		{
			this.CompleteOpenMenue();
		}
	}

	
	public void CompleteOpenMenue()
	{
		this.FindScripts();
		if (!this.pS_)
		{
			this.FindMyKonsole();
		}
		if (!this.pS_)
		{
			return;
		}
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[326]);
		this.guiMain_.uiObjects[326].GetComponent<Menu_Dev_KonsoleComplete>().Init(this.pS_, this);
		this.guiMain_.OpenMenu(false);
		if (this.mS_.sfx_)
		{
			this.mS_.sfx_.PlaySound(37, false);
		}
	}

	
	public int GetRueckgeld()
	{
		if (this.pS_)
		{
			long num = this.pS_.entwicklungsKosten / 100L * (long)(100 - Mathf.RoundToInt(this.GetProzent()));
			if (num > 2000000000L)
			{
				num = 2000000000L;
			}
			return Mathf.RoundToInt((float)num);
		}
		return 0;
	}

	
	public void Abbrechen()
	{
		this.FindScripts();
		if (!this.pS_)
		{
			this.FindMyKonsole();
		}
		if (!this.pS_)
		{
			return;
		}
		int rueckgeld = this.GetRueckgeld();
		if (rueckgeld > 0)
		{
			this.mS_.Earn((long)rueckgeld, 1);
			GameObject[] array = GameObject.FindGameObjectsWithTag("Room");
			for (int i = 0; i < array.Length; i++)
			{
				roomScript component = array[i].GetComponent<roomScript>();
				if (component && component.taskID == this.myID)
				{
					this.guiMain_.MoneyPop(rueckgeld, new Vector3(component.uiPos.x, component.uiPos.y + 3f, component.uiPos.z), true);
					break;
				}
			}
		}
		UnityEngine.Object.Destroy(this.pS_.gameObject);
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public int myID = -1;

	
	public int konsoleID = -1;

	
	public int leitenderTechnikerID = -1;

	
	public characterScript techniker_;

	
	public platformScript pS_;

	
	private GameObject main_;

	
	public mainScript mS_;

	
	private engineFeatures eF_;

	
	private GUI_Main guiMain_;

	
	private textScript tS_;

	
	private roomDataScript rdS_;

	
	private hardware hardware_;

	
	private hardwareFeatures hardwareFeatures_;
}
