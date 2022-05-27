using System;
using UnityEngine;


public class taskMarketing : MonoBehaviour
{
	
	private void Awake()
	{
		base.transform.position = new Vector3(60f, 0f, 0f);
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
		if (!this.scriptMarketing_)
		{
			this.scriptMarketing_ = this.guiMain_.uiObjects[89].GetComponent<Menu_Marketing_GameKampagne>();
		}
	}

	
	private void Update()
	{
		this.FindMyObject();
		if (this.gS_ && !this.gS_.isOnMarket && this.gS_.sellsTotal > 0L)
		{
			this.Abbrechen();
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

	
	private void FindMyObject()
	{
		if (this.gS_ || this.pS_)
		{
			return;
		}
		int num = this.typ;
		if (num != 0)
		{
			if (num == 1)
			{
				if (!this.pS_)
				{
					GameObject gameObject = GameObject.Find("PLATFORM_" + this.targetID.ToString());
					if (gameObject)
					{
						this.pS_ = gameObject.GetComponent<platformScript>();
					}
				}
			}
		}
		else if (!this.gS_)
		{
			GameObject gameObject2 = GameObject.Find("GAME_" + this.targetID.ToString());
			if (gameObject2)
			{
				this.gS_ = gameObject2.GetComponent<gameScript>();
			}
		}
		if (!this.gS_ && !this.pS_)
		{
			this.Abbrechen();
		}
	}

	
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	
	public Sprite GetPic()
	{
		return this.guiMain_.uiObjects[89].GetComponent<Menu_Marketing_GameKampagne>().sprites[this.kampagne];
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
		this.FindMyObject();
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
		int num = this.typ;
		if (num != 0)
		{
			if (num == 1)
			{
				string text = this.tS_.GetText(529);
				text = text.Replace("<NAME1>", "<b><color=blue>" + this.pS_.GetName() + "</color></b>");
				this.guiMain_.CreateLeftNews(roomID_, this.GetPic(), text, this.rdS_.roomData_SPRITE[6]);
				if (this.pS_.hype < (float)this.scriptMarketing_.maxHype[this.kampagne])
				{
					this.pS_.AddHype((float)this.scriptMarketing_.hypeProKampagne[this.kampagne]);
					if (this.pS_.hype > (float)this.scriptMarketing_.maxHype[this.kampagne])
					{
						this.pS_.hype = (float)this.scriptMarketing_.maxHype[this.kampagne];
					}
				}
				this.pS_.costs_marketing += this.scriptMarketing_.preise[this.kampagne];
			}
		}
		else
		{
			string text = this.tS_.GetText(529);
			text = text.Replace("<NAME1>", "<b><color=blue>" + this.gS_.GetNameWithTag() + "</color></b>");
			this.guiMain_.CreateLeftNews(roomID_, this.GetPic(), text, this.rdS_.roomData_SPRITE[6]);
			if (this.gS_.hype < (float)this.scriptMarketing_.maxHype[this.kampagne])
			{
				this.gS_.AddHype((float)this.scriptMarketing_.hypeProKampagne[this.kampagne]);
				if (this.gS_.hype > (float)this.scriptMarketing_.maxHype[this.kampagne])
				{
					this.gS_.hype = (float)this.scriptMarketing_.maxHype[this.kampagne];
				}
			}
			this.gS_.costs_marketing += (long)this.scriptMarketing_.preise[this.kampagne];
		}
		if (!this.DoAutomatic())
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	
	public bool WaitForMinimumHype()
	{
		if (this.automatic)
		{
			this.FindScripts();
			this.FindMyObject();
			if (this.disableWarten)
			{
				return false;
			}
			if (this.typ == 0)
			{
				if (this.gS_)
				{
					return this.gS_.hype + (float)this.scriptMarketing_.hypeProKampagne[this.kampagne] >= (float)this.scriptMarketing_.maxHype[this.kampagne];
				}
			}
			else if (this.pS_)
			{
				return this.pS_.hype + (float)this.scriptMarketing_.hypeProKampagne[this.kampagne] >= (float)this.scriptMarketing_.maxHype[this.kampagne];
			}
		}
		return false;
	}

	
	private bool DoAutomatic()
	{
		this.FindMyObject();
		if (!this.automatic)
		{
			return false;
		}
		if (this.stopAutomatic)
		{
			if (this.gS_ && this.gS_.hype >= (float)this.scriptMarketing_.maxHype[this.kampagne])
			{
				this.LeftNews(this.tS_.GetText(730), this.guiMain_.uiSprites[16], this.rdS_.roomData_SPRITE[6]);
				return false;
			}
			if (this.pS_ && this.pS_.hype >= (float)this.scriptMarketing_.maxHype[this.kampagne])
			{
				this.LeftNews(this.tS_.GetText(730), this.guiMain_.uiSprites[16], this.rdS_.roomData_SPRITE[6]);
				return false;
			}
		}
		if (this.mS_.money < (long)this.scriptMarketing_.preise[this.kampagne])
		{
			this.LeftNews(this.tS_.GetText(728), this.guiMain_.uiSprites[16], this.rdS_.roomData_SPRITE[6]);
			return false;
		}
		this.mS_.Pay((long)this.scriptMarketing_.preise[this.kampagne], 12);
		this.pointsLeft = this.points;
		return true;
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

	
	public int GetRueckgeld()
	{
		return Mathf.RoundToInt((float)this.scriptMarketing_.preise[this.kampagne] * ((100f - this.GetProzent()) * 0.01f));
	}

	
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

	
	public int myID = -1;

	
	public int typ = -1;

	
	public int targetID = -1;

	
	public int kampagne = -1;

	
	public bool automatic;

	
	public bool stopAutomatic;

	
	public bool disableWarten;

	
	public float points;

	
	public float pointsLeft;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private GUI_Main guiMain_;

	
	private textScript tS_;

	
	private roomDataScript rdS_;

	
	public Menu_Marketing_GameKampagne scriptMarketing_;

	
	public gameScript gS_;

	
	public platformScript pS_;
}
