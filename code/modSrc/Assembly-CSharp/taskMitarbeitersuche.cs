using System;
using UnityEngine;


public class taskMitarbeitersuche : MonoBehaviour
{
	
	private void Awake()
	{
		base.transform.position = new Vector3(80f, 0f, 0f);
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
		if (!this.arbeitsmarkt_)
		{
			this.arbeitsmarkt_ = this.main_.GetComponent<arbeitsmarkt>();
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

	
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	
	public Sprite GetPic()
	{
		this.FindScripts();
		return this.guiMain_.uiSprites[44];
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
		if (this.mS_.multiplayer && this.guiMain_.menuOpen)
		{
			this.pointsLeft = 0.1f;
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
		float chance = this.guiMain_.uiObjects[344].GetComponent<Menu_Mitarbeitersuche>().GetChance(this.berufserfahrung);
		float num = UnityEngine.Random.Range(0f, 100f);
		Debug.Log(string.Concat(new object[]
		{
			"AA: ",
			num,
			" / ",
			chance
		}));
		if (num < chance)
		{
			this.guiMain_.CreateLeftNews(roomID_, this.GetPic(), this.tS_.GetText(1719), this.rdS_.roomData_SPRITE[6]);
			charArbeitsmarkt charArbeitsmarkt = this.arbeitsmarkt_.CreateArbeitsmarktItem();
			if (charArbeitsmarkt)
			{
				charArbeitsmarkt.Create(this);
			}
			this.guiMain_.uiObjects[345].SetActive(true);
			this.guiMain_.uiObjects[345].GetComponent<Menu_MitarbeitersucheResult>().Init(charArbeitsmarkt);
			this.guiMain_.OpenMenu(false);
		}
		else
		{
			this.guiMain_.CreateLeftNews(roomID_, this.guiMain_.uiSprites[48], this.tS_.GetText(1718), this.rdS_.roomData_SPRITE[6]);
		}
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
		Menu_Mitarbeitersuche component = this.guiMain_.uiObjects[344].GetComponent<Menu_Mitarbeitersuche>();
		if (this.mS_.money < (long)component.price[this.berufserfahrung])
		{
			this.LeftNews(this.tS_.GetText(728), this.guiMain_.uiSprites[16], this.rdS_.roomData_SPRITE[6]);
			return false;
		}
		this.mS_.Pay((long)component.price[this.berufserfahrung], 24);
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
		return Mathf.RoundToInt((float)this.guiMain_.uiObjects[344].GetComponent<Menu_Mitarbeitersuche>().price[this.berufserfahrung] * ((100f - this.GetProzent()) * 0.01f));
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

	
	public int beruf = -1;

	
	public int berufserfahrung;

	
	public bool automatic;

	
	public float points;

	
	public float pointsLeft;

	
	private GameObject main_;

	
	public mainScript mS_;

	
	private GUI_Main guiMain_;

	
	private textScript tS_;

	
	private roomDataScript rdS_;

	
	public gameScript gS_;

	
	public platformScript pS_;

	
	private arbeitsmarkt arbeitsmarkt_;
}
