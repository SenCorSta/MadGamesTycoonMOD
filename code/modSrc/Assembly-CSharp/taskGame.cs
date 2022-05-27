using System;
using UnityEngine;


public class taskGame : MonoBehaviour
{
	
	private void Awake()
	{
		base.transform.position = new Vector3(40f, 0f, 0f);
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
		if (!this.gF_)
		{
			this.gF_ = this.main_.GetComponent<gameplayFeatures>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.rdS_)
		{
			this.rdS_ = this.main_.GetComponent<roomDataScript>();
		}
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
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
		this.FindMyGame();
		this.FindMyLeitenderDesigner();
		this.FindMyMainMMO();
	}

	
	private void FindMyMainMMO()
	{
		if (this.gS_ && this.gS_.typ_mmoaddon)
		{
			gameScript gameScript = this.gS_.FindVorgaengerScript();
			if (gameScript && !gameScript.isOnMarket)
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
				string text = this.tS_.GetText(1259);
				text = text.Replace("<NAME>", "<b><color=blue>" + this.gS_.GetNameWithTag() + "</color></b>");
				this.guiMain_.CreateLeftNews(roomID_, this.guiMain_.uiSprites[3], text, this.rdS_.roomData_SPRITE[1]);
				this.Abbrechen();
			}
		}
	}

	
	private void FindMyGame()
	{
		if (!this.gS_)
		{
			GameObject gameObject = GameObject.Find("GAME_" + this.gameID.ToString());
			if (gameObject)
			{
				this.gS_ = gameObject.GetComponent<gameScript>();
				return;
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	
	private void FindMyLeitenderDesigner()
	{
		if (this.leitenderDesignerID == -1)
		{
			return;
		}
		if (this.designer_)
		{
			if (this.designer_.roomS_)
			{
				if (this.designer_.roomS_.taskID != this.myID)
				{
					this.leitenderDesignerID = -1;
					this.designer_ = null;
					return;
				}
			}
			else
			{
				this.leitenderDesignerID = -1;
				this.designer_ = null;
			}
			return;
		}
		GameObject gameObject = GameObject.Find("CHAR_" + this.leitenderDesignerID.ToString());
		if (gameObject)
		{
			this.designer_ = gameObject.GetComponent<characterScript>();
			return;
		}
		this.leitenderDesignerID = -1;
		this.designer_ = null;
	}

	
	public float GetProzent()
	{
		this.FindScripts();
		if (!this.gS_)
		{
			return -1f;
		}
		return this.gS_.GetProzentGesamt();
	}

	
	public void Work(float f, int what)
	{
		this.FindScripts();
		if (!this.gS_)
		{
			this.FindMyGame();
		}
		if (!this.gS_)
		{
			return;
		}
		if (this.gS_.devPoints > 0f)
		{
			if (!this.mS_.settings_RandomEventsOff && !this.randomEvent && UnityEngine.Random.Range(0, 1000) == 1 && !this.guiMain_.menuOpen && !this.guiMain_.uiObjects[215].activeSelf)
			{
				this.randomEvent = true;
				this.guiMain_.uiObjects[215].SetActive(true);
				this.guiMain_.uiObjects[215].GetComponent<Menu_RandomEventDev>().Init(this.gS_);
			}
			switch (what)
			{
			case 0:
				this.gS_.points_gameplay += f;
				break;
			case 1:
				this.gS_.points_grafik += f;
				break;
			case 2:
				this.gS_.points_sound += f;
				break;
			case 3:
				this.gS_.points_technik += f;
				break;
			case 4:
				this.gS_.points_bugs += f;
				if (f > 0f && UnityEngine.Random.Range(0, 100) < 30)
				{
					this.gS_.points_bugsInvis += f;
				}
				break;
			case 5:
				if (this.gS_.GetHype() < 50f)
				{
					this.gS_.AddHype(f);
				}
				break;
			}
			this.gS_.devPoints -= 1f;
			this.gS_.devPoints_Gesamt -= 1f;
			if (this.gS_.devPoints <= 0f)
			{
				this.CompleteFeature();
				this.gS_.devPoints_Gesamt += Mathf.Abs(this.gS_.devPoints);
				this.gS_.devPoints = 0f;
				this.gS_.FindNextFeatureForDevelopment();
				if (this.gS_.devPointsStart <= 0f)
				{
					this.gS_.devPoints_Gesamt = 0f;
					this.Complete();
					return;
				}
			}
		}
		else
		{
			switch (what)
			{
			case 0:
				this.gS_.points_gameplay += f;
				this.RemoveInvisBug();
				return;
			case 1:
				this.gS_.points_grafik += f;
				this.RemoveInvisBug();
				return;
			case 2:
				this.gS_.points_sound += f;
				this.RemoveInvisBug();
				return;
			case 3:
				this.gS_.points_technik += f;
				this.RemoveInvisBug();
				return;
			case 4:
				break;
			case 5:
				if (this.gS_.GetHype() < 50f)
				{
					this.gS_.AddHype(f);
					return;
				}
				break;
			case 6:
				this.gS_.points_bugs -= 1f;
				if (this.gS_.points_bugs < 0f)
				{
					this.gS_.points_bugs = 0f;
				}
				this.RemoveInvisBug();
				break;
			default:
				return;
			}
		}
	}

	
	private void RemoveInvisBug()
	{
		if (UnityEngine.Random.Range(0, 100) < 90)
		{
			return;
		}
		this.gS_.points_bugsInvis -= 1f;
		if (this.gS_.points_bugsInvis < 0f)
		{
			this.gS_.points_bugsInvis = 0f;
		}
	}

	
	private void CompleteFeature()
	{
		if (this.gS_.devAktFeature == -5)
		{
			return;
		}
		if (this.gS_.devAktFeature < 0)
		{
			if (this.gS_.finanzierung_Technology >= 100)
			{
				this.gS_.points_gameplay += (float)this.eF_.GetGameplay(this.gS_.gameEngineFeature[this.gS_.devAktFeature + 4]);
				this.gS_.points_grafik += (float)this.eF_.GetGraphic(this.gS_.gameEngineFeature[this.gS_.devAktFeature + 4]);
				this.gS_.points_sound += (float)this.eF_.GetSound(this.gS_.gameEngineFeature[this.gS_.devAktFeature + 4]);
				this.gS_.points_technik += (float)this.eF_.GetTechnik(this.gS_.gameEngineFeature[this.gS_.devAktFeature + 4]);
				return;
			}
			float num = (float)this.gS_.finanzierung_Technology;
			num *= 0.01f;
			float num2 = (float)this.eF_.GetGameplay(this.gS_.gameEngineFeature[this.gS_.devAktFeature + 4]);
			num2 *= UnityEngine.Random.Range(num, 1f);
			this.gS_.points_gameplay += num2;
			num2 = (float)this.eF_.GetGraphic(this.gS_.gameEngineFeature[this.gS_.devAktFeature + 4]);
			num2 *= UnityEngine.Random.Range(num, 1f);
			this.gS_.points_grafik += num2;
			num2 = (float)this.eF_.GetSound(this.gS_.gameEngineFeature[this.gS_.devAktFeature + 4]);
			num2 *= UnityEngine.Random.Range(num, 1f);
			this.gS_.points_sound += num2;
			num2 = (float)this.eF_.GetTechnik(this.gS_.gameEngineFeature[this.gS_.devAktFeature + 4]);
			num2 *= UnityEngine.Random.Range(num, 1f);
			this.gS_.points_technik += num2;
			return;
		}
		else
		{
			if (this.gS_.finanzierung_Kontent >= 100)
			{
				this.gS_.points_gameplay += (float)this.gF_.GetGameplay(this.gS_.devAktFeature, this.gS_.maingenre);
				this.gS_.points_grafik += (float)this.gF_.GetGraphic(this.gS_.devAktFeature, this.gS_.maingenre);
				this.gS_.points_sound += (float)this.gF_.GetSound(this.gS_.devAktFeature, this.gS_.maingenre);
				this.gS_.points_technik += (float)this.gF_.GetTechnik(this.gS_.devAktFeature, this.gS_.maingenre);
				return;
			}
			float num3 = (float)this.gS_.finanzierung_Kontent;
			num3 *= 0.01f;
			float num4 = (float)this.gF_.GetGameplay(this.gS_.devAktFeature, this.gS_.maingenre);
			num4 *= UnityEngine.Random.Range(num3, 1f);
			this.gS_.points_gameplay += num4;
			num4 = (float)this.gF_.GetGraphic(this.gS_.devAktFeature, this.gS_.maingenre);
			num4 *= UnityEngine.Random.Range(num3, 1f);
			this.gS_.points_grafik += num4;
			num4 = (float)this.gF_.GetSound(this.gS_.devAktFeature, this.gS_.maingenre);
			num4 *= UnityEngine.Random.Range(num3, 1f);
			this.gS_.points_sound += num4;
			num4 = (float)this.gF_.GetTechnik(this.gS_.devAktFeature, this.gS_.maingenre);
			num4 *= UnityEngine.Random.Range(num3, 1f);
			this.gS_.points_technik += num4;
			return;
		}
	}

	
	public void Complete()
	{
		this.FindScripts();
		if (!this.gS_)
		{
			this.FindMyGame();
		}
		if (!this.gS_)
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
		string tooltip_ = this.tS_.GetText(1128) + "\n<b>" + this.gS_.GetNameWithTag() + "</b>";
		this.guiMain_.CreateLeftNews(roomID_, this.games_.gameTypSprites[0], tooltip_, this.rdS_.roomData_SPRITE[1]);
		if (!this.mS_.multiplayer)
		{
			this.CompleteOpenMenue();
		}
	}

	
	public void CompleteOpenMenue()
	{
		this.FindScripts();
		if (!this.gS_)
		{
			this.FindMyGame();
		}
		if (!this.gS_)
		{
			return;
		}
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[69]);
		this.guiMain_.uiObjects[69].GetComponent<Menu_DevGame_Complete>().Init(this.gS_, this);
		this.guiMain_.OpenMenu(false);
		this.sfx_.PlaySound(37, false);
	}

	
	public int GetRueckgeld()
	{
		return this.gS_.GetRueckggeld();
	}

	
	public void Abbrechen()
	{
		this.FindScripts();
		if (!this.gS_)
		{
			this.FindMyGame();
		}
		if (!this.gS_)
		{
			return;
		}
		int rueckgeld = this.GetRueckgeld();
		if (rueckgeld >= 0)
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
		if (this.gS_.gameLicence != -1 && this.gS_.portID == -1 && !this.gS_.typ_addon && !this.gS_.typ_mmoaddon)
		{
			this.mS_.licences_.licence_GEKAUFT[this.gS_.gameLicence]++;
		}
		if (this.gS_.typ_contractGame)
		{
			this.mS_.Pay((long)Mathf.Abs(rueckgeld), 14);
			GameObject[] array2 = GameObject.FindGameObjectsWithTag("Room");
			for (int j = 0; j < array2.Length; j++)
			{
				roomScript component2 = array2[j].GetComponent<roomScript>();
				if (component2 && component2.taskID == this.myID)
				{
					this.guiMain_.MoneyPop(Mathf.Abs(rueckgeld), new Vector3(component2.uiPos.x, component2.uiPos.y + 3f, component2.uiPos.z), false);
					break;
				}
			}
			this.guiMain_.UpdateAuftragsansehen(-5f);
			this.gS_.FreeGameContract();
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		UnityEngine.Object.Destroy(this.gS_.gameObject);
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public int myID = -1;

	
	public int gameID = -1;

	
	public int leitenderDesignerID = -1;

	
	public bool randomEvent;

	
	public gameScript gS_;

	
	public characterScript designer_;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private engineFeatures eF_;

	
	private gameplayFeatures gF_;

	
	private GUI_Main guiMain_;

	
	private textScript tS_;

	
	private roomDataScript rdS_;

	
	private sfxScript sfx_;

	
	private games games_;
}
