using System;
using UnityEngine;


public class taskUpdate : MonoBehaviour
{
	
	private void Awake()
	{
		base.transform.position = new Vector3(30f, 0f, 0f);
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

	
	private void Update()
	{
		this.FindMyObject();
		this.CheckAbbruch();
	}

	
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	
	private void CheckAbbruch()
	{
		if (!this.gS_)
		{
			return;
		}
		if (!this.gS_.isOnMarket)
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
			string text = this.tS_.GetText(842);
			text = text.Replace("<NAME>", "<b><color=blue>" + this.gS_.GetNameWithTag() + "</color></b>");
			this.guiMain_.CreateLeftNews(roomID_, this.guiMain_.uiSprites[3], text, this.rdS_.roomData_SPRITE[1]);
			this.Abbrechen();
		}
	}

	
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
	}

	
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	
	public Sprite GetPic()
	{
		return this.guiMain_.uiSprites[15];
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
		this.gS_.costs_updates += (long)this.devCosts;
		this.gS_.amountUpdates++;
		this.gS_.bonusSellsUpdates += this.quality / (float)this.gS_.amountUpdates;
		if ((double)this.gS_.bonusSellsUpdates > 1.0)
		{
			this.gS_.bonusSellsUpdates = 1f;
		}
		this.gS_.points_gameplay += (float)this.pointsGameplay;
		this.gS_.points_grafik += (float)this.pointsGrafik;
		this.gS_.points_sound += (float)this.pointsSound;
		this.gS_.points_technik += (float)this.pointsTechnik;
		this.gS_.points_bugs -= (float)this.pointsBugs;
		if (this.gS_.points_bugs < 0f)
		{
			this.gS_.points_bugs = 0f;
		}
		for (int i = 0; i < this.sprachen.Length; i++)
		{
			if (this.sprachen[i])
			{
				this.gS_.gameLanguage[i] = true;
			}
		}
		int roomID_ = -1;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Room");
		for (int j = 0; j < array.Length; j++)
		{
			roomScript component = array[j].GetComponent<roomScript>();
			if (component && component.taskID == this.myID)
			{
				roomID_ = component.myID;
				break;
			}
		}
		string text = this.tS_.GetText(663);
		text = text.Replace("<NAME>", "<b><color=blue>" + this.gS_.GetNameWithTag() + "</color></b>");
		this.guiMain_.CreateLeftNews(roomID_, this.GetPic(), text, this.rdS_.roomData_SPRITE[1]);
		if (!this.DoAutomatic())
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	
	private bool DoAutomatic()
	{
		if (!this.automatic)
		{
			return false;
		}
		for (int i = 0; i < this.sprachen.Length; i++)
		{
			if (this.sprachen[i])
			{
				this.points -= 10f;
				if (!this.mS_.Muttersprache(i))
				{
					this.devCosts -= this.gS_.GetGesamtDevPoints() * 5;
				}
				if (this.devCosts < 1)
				{
					this.devCosts = 1;
				}
				this.sprachen[i] = false;
			}
		}
		this.pointsLeft = this.points;
		if (this.mS_.money < (long)this.devCosts)
		{
			this.LeftNews(this.tS_.GetText(728), this.guiMain_.uiSprites[16], this.rdS_.roomData_SPRITE[1]);
			return false;
		}
		this.mS_.Pay((long)this.devCosts, 15);
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
		return Mathf.RoundToInt((float)this.devCosts * ((100f - this.GetProzent()) * 0.01f));
	}

	
	public void Abbrechen()
	{
		this.FindMyObject();
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

	
	public int targetID = -1;

	
	public float points;

	
	public float pointsLeft;

	
	public float quality;

	
	public bool[] sprachen;

	
	public int devCosts;

	
	public int pointsGameplay;

	
	public int pointsSound;

	
	public int pointsGrafik;

	
	public int pointsTechnik;

	
	public int pointsBugs;

	
	public bool automatic;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private GUI_Main guiMain_;

	
	private textScript tS_;

	
	private roomDataScript rdS_;

	
	public gameScript gS_;
}
