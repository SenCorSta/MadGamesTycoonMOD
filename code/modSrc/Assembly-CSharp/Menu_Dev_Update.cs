using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Dev_Update : MonoBehaviour
{
	
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
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.themes_)
		{
			this.themes_ = this.main_.GetComponent<themes>();
		}
		if (!this.licences_)
		{
			this.licences_ = this.main_.GetComponent<licences>();
		}
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
		if (!this.cmS_)
		{
			this.cmS_ = GameObject.Find("CamMovement").GetComponent<cameraMovementScript>();
		}
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
		}
		if (!this.gF_)
		{
			this.gF_ = this.main_.GetComponent<gameplayFeatures>();
		}
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
	}

	
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	
	public void Init(roomScript roomScript_, gameScript gameScript_)
	{
		this.FindScripts();
		this.rS_ = roomScript_;
		this.gS_ = gameScript_;
		this.allAdds = false;
		for (int i = 0; i < this.buttonAdds.Length; i++)
		{
			this.buttonAdds[i] = false;
		}
		for (int j = 0; j < this.sprachen.Length; j++)
		{
			this.sprachen[j] = false;
			if (this.gS_.gameLanguage[j])
			{
				this.uiObjects[26 + j].GetComponent<Button>().interactable = false;
			}
			else
			{
				this.uiObjects[26 + j].GetComponent<Button>().interactable = true;
			}
		}
		this.uiObjects[0].GetComponent<Text>().text = this.gS_.GetNameWithTag();
		this.uiObjects[9].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt((float)this.gS_.costs_entwicklung * this.devCostsPercent[0]), true);
		this.uiObjects[10].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt((float)this.gS_.costs_entwicklung * this.devCostsPercent[1]), true);
		this.uiObjects[11].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt((float)this.gS_.costs_entwicklung * this.devCostsPercent[2]), true);
		this.uiObjects[12].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt((float)this.gS_.costs_entwicklung * this.devCostsPercent[3]), true);
		this.uiObjects[13].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt((float)this.gS_.costs_entwicklung * this.devCostsPercent[4]), true);
		this.uiObjects[14].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt((float)this.gS_.costs_entwicklung * this.devCostsPercent[5]), true);
		this.uiObjects[15].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt((float)this.gS_.costs_entwicklung * this.devCostsPercent[6]), true);
		this.uiObjects[16].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt((float)this.gS_.costs_entwicklung * this.devCostsPercent[7]), true);
		this.UpdateGUI();
	}

	
	private void UpdateGUI()
	{
		this.uiObjects[1].GetComponent<Text>().text = Mathf.RoundToInt(this.gS_.points_gameplay).ToString() + "+" + this.GetP_Gameplay().ToString();
		this.uiObjects[2].GetComponent<Text>().text = Mathf.RoundToInt(this.gS_.points_grafik).ToString() + "+" + this.GetP_Grafik().ToString();
		this.uiObjects[3].GetComponent<Text>().text = Mathf.RoundToInt(this.gS_.points_sound).ToString() + "+" + this.GetP_Sound().ToString();
		this.uiObjects[4].GetComponent<Text>().text = Mathf.RoundToInt(this.gS_.points_technik).ToString() + "+" + this.GetP_Technik().ToString();
		this.uiObjects[5].GetComponent<Text>().text = Mathf.RoundToInt(this.gS_.points_bugs).ToString() + "-" + this.GetP_Bugs().ToString();
		this.uiObjects[7].GetComponent<Text>().text = (this.uiObjects[6].GetComponent<Slider>().value * 10f).ToString() + "%";
		this.uiObjects[8].GetComponent<Text>().text = this.mS_.GetMoney(this.GetCosts_Bugs(), true);
		for (int i = 0; i < this.buttonAdds.Length; i++)
		{
			if (this.buttonAdds[i])
			{
				this.uiObjects[17 + i].GetComponent<Image>().color = this.guiMain_.colors[7];
			}
			else
			{
				this.uiObjects[17 + i].GetComponent<Image>().color = Color.white;
			}
		}
		for (int j = 0; j < this.sprachen.Length; j++)
		{
			if (this.sprachen[j])
			{
				this.uiObjects[26 + j].GetComponent<Image>().color = this.guiMain_.colors[7];
			}
			else
			{
				this.uiObjects[26 + j].GetComponent<Image>().color = Color.white;
			}
		}
		this.uiObjects[25].GetComponent<Text>().text = this.mS_.GetMoney(this.GetDevCosts(), true);
	}

	
	private int GetP_Gameplay()
	{
		float num = 0f;
		if (this.buttonAdds[0])
		{
			num += 1f;
			num += this.gS_.points_gameplay * 0.02f;
		}
		if (this.buttonAdds[1])
		{
			num += 1f;
			num += this.gS_.points_gameplay * 0.02f;
		}
		return Mathf.RoundToInt(num);
	}

	
	private int GetP_Grafik()
	{
		float num = 0f;
		if (this.buttonAdds[2])
		{
			num += 1f;
			num += this.gS_.points_grafik * 0.02f;
		}
		if (this.buttonAdds[3])
		{
			num += 1f;
			num += this.gS_.points_grafik * 0.02f;
		}
		return Mathf.RoundToInt(num);
	}

	
	private int GetP_Sound()
	{
		float num = 0f;
		if (this.buttonAdds[4])
		{
			num += 1f;
			num += this.gS_.points_sound * 0.02f;
		}
		if (this.buttonAdds[5])
		{
			num += 1f;
			num += this.gS_.points_sound * 0.02f;
		}
		return Mathf.RoundToInt(num);
	}

	
	private int GetP_Technik()
	{
		float num = 0f;
		if (this.buttonAdds[6])
		{
			num += 1f;
			num += this.gS_.points_technik * 0.02f;
		}
		if (this.buttonAdds[7])
		{
			num += 1f;
			num += this.gS_.points_technik * 0.02f;
		}
		return Mathf.RoundToInt(num);
	}

	
	private int GetP_Bugs()
	{
		float points_bugs = this.gS_.points_bugs;
		float num = this.uiObjects[6].GetComponent<Slider>().value;
		num *= 0.1f;
		return Mathf.RoundToInt(points_bugs * num);
	}

	
	private long GetCosts_Bugs()
	{
		return (long)this.GetP_Bugs() * (this.gS_.costs_entwicklung / 7500L);
	}

	
	private long GetDevCosts()
	{
		long num = this.GetCosts_Bugs();
		for (int i = 0; i < this.buttonAdds.Length; i++)
		{
			if (this.buttonAdds[i])
			{
				num += (long)Mathf.RoundToInt((float)this.gS_.costs_entwicklung * this.devCostsPercent[i]);
				num += (long)Mathf.RoundToInt((float)this.gS_.costs_updates * this.devCostsPercent[i]);
			}
		}
		for (int j = 0; j < this.sprachen.Length; j++)
		{
			if (this.sprachen[j] && !this.mS_.Muttersprache(j))
			{
				num += (long)(this.gS_.GetGesamtDevPoints() * 5);
			}
		}
		return num;
	}

	
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[105]);
		this.guiMain_.uiObjects[105].GetComponent<Menu_Dev_UpdateSelectGame>().Init(this.rS_, 0);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Adds(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.buttonAdds[i] = !this.buttonAdds[i];
		this.UpdateGUI();
	}

	
	public void BUTTON_Sprache(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.sprachen[i] = !this.sprachen[i];
		this.UpdateGUI();
	}

	
	public void BUTTON_AlleSprache()
	{
		this.sfx_.PlaySound(3, true);
		this.allSprachen = !this.allSprachen;
		for (int i = 0; i < this.sprachen.Length; i++)
		{
			if (this.uiObjects[26 + i].GetComponent<Button>().interactable)
			{
				this.sprachen[i] = this.allSprachen;
			}
		}
		this.UpdateGUI();
	}

	
	public void BUTTON_AlleAdds()
	{
		this.sfx_.PlaySound(3, true);
		this.allAdds = !this.allAdds;
		for (int i = 0; i < this.buttonAdds.Length; i++)
		{
			this.buttonAdds[i] = this.allAdds;
		}
		this.UpdateGUI();
	}

	
	public void BUTTON_AllBugs()
	{
		this.sfx_.PlaySound(3, true);
		this.allBugs = !this.allBugs;
		if (this.allBugs)
		{
			this.uiObjects[6].GetComponent<Slider>().value = 10f;
			return;
		}
		this.uiObjects[6].GetComponent<Slider>().value = 0f;
	}

	
	public void BUTTON_Start()
	{
		int num = Mathf.RoundToInt((float)this.GetDevCosts());
		if (!this.gS_)
		{
			return;
		}
		if (!this.rS_)
		{
			return;
		}
		if (num <= 0)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(662), false);
			return;
		}
		if (this.uiObjects[37].GetComponent<Toggle>().isOn)
		{
			bool flag = false;
			for (int i = 0; i < this.buttonAdds.Length; i++)
			{
				if (this.buttonAdds[i])
				{
					flag = true;
				}
			}
			if (!flag)
			{
				this.guiMain_.MessageBox(this.tS_.GetText(727), false);
				return;
			}
		}
		this.sfx_.PlaySound(3, true);
		this.mS_.Pay((long)num, 15);
		taskUpdate taskUpdate = this.guiMain_.AddTask_Update();
		taskUpdate.Init(false);
		taskUpdate.targetID = this.gS_.myID;
		taskUpdate.devCosts = Mathf.RoundToInt((float)this.GetDevCosts());
		taskUpdate.pointsGameplay = this.GetP_Gameplay();
		taskUpdate.pointsGrafik = this.GetP_Grafik();
		taskUpdate.pointsSound = this.GetP_Sound();
		taskUpdate.pointsTechnik = this.GetP_Technik();
		taskUpdate.pointsBugs = this.GetP_Bugs();
		taskUpdate.automatic = this.uiObjects[37].GetComponent<Toggle>().isOn;
		float num2 = (float)this.gS_.GetGesamtDevPoints();
		num2 *= 0.1f;
		taskUpdate.points = (float)Mathf.RoundToInt(num2);
		for (int j = 0; j < this.buttonAdds.Length; j++)
		{
			if (this.buttonAdds[j])
			{
				taskUpdate.quality += 0.1f;
				taskUpdate.points += (float)Mathf.RoundToInt((float)this.gS_.GetGesamtDevPoints() * 0.02f);
			}
		}
		for (int k = 0; k < this.sprachen.Length; k++)
		{
			if (this.sprachen[k])
			{
				taskUpdate.quality += 0.02f;
				taskUpdate.sprachen[k] = this.sprachen[k];
				taskUpdate.points += 10f;
			}
		}
		taskUpdate.points += (float)this.GetP_Bugs() * 0.3f;
		taskUpdate.pointsLeft = taskUpdate.points;
		GameObject gameObject = GameObject.Find("Room_" + this.rS_.myID.ToString());
		if (gameObject)
		{
			gameObject.GetComponent<roomScript>().taskID = taskUpdate.myID;
		}
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	
	public void SLIDER_Bugs()
	{
		this.UpdateGUI();
	}

	
	public void TOGGLE_Auto()
	{
	}

	
	public GameObject[] uiObjects;

	
	public float[] devCostsPercent;

	
	private bool[] buttonAdds = new bool[8];

	
	private bool[] sprachen = new bool[11];

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private genres genres_;

	
	private themes themes_;

	
	private licences licences_;

	
	private engineFeatures eF_;

	
	private cameraMovementScript cmS_;

	
	private unlockScript unlock_;

	
	private gameplayFeatures gF_;

	
	private games games_;

	
	private gameScript gS_;

	
	private roomScript rS_;

	
	private bool allSprachen;

	
	private bool allAdds;

	
	private bool allBugs;
}
