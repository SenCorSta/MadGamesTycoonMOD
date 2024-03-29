﻿using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Dev_Engine : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.Find("Main");
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
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
		if (!this.cmS_)
		{
			this.cmS_ = GameObject.Find("CamMovement").GetComponent<cameraMovementScript>();
		}
	}

	
	private void Update()
	{
		this.UpdateFeatureData();
		this.uiObjects[9].GetComponent<Text>().text = this.tS_.GetText(6) + ": <b>" + this.mS_.GetMoney((long)this.preis, true) + "</b>";
		this.uiObjects[10].GetComponent<Text>().text = this.featureAnzahl.ToString() + " " + this.tS_.GetText(160);
		this.uiObjects[11].GetComponent<Text>().text = this.tS_.GetText(4) + " " + this.techLevel.ToString();
	}

	
	private void UpdateLockedFeatures()
	{
		this.featuresLock[0] = true;
		this.featuresLock[20] = true;
		this.featuresLock[34] = true;
		this.featuresLock[46] = true;
	}

	
	private void UpdateFeatureData()
	{
		this.featureAnzahl = 0;
		this.preis = 0;
		this.techLevel = 0;
		for (int i = 0; i < this.features.Length; i++)
		{
			if (this.features[i] || this.featuresLock[i])
			{
				this.featureAnzahl++;
				if (this.features[i])
				{
					this.preis += this.eF_.GetDevCostsForEngine(i);
				}
				if (this.techLevel < this.eF_.engineFeatures_TECH[i])
				{
					this.techLevel = this.eF_.engineFeatures_TECH[i];
				}
			}
		}
	}

	
	public void Init(roomScript script_)
	{
		this.FindScripts();
		this.rS_ = script_;
		this.eSUpdate_ = null;
		this.uiObjects[14].GetComponent<Text>().text = this.tS_.GetText(241);
		this.uiObjects[15].GetComponent<Button>().interactable = true;
		this.InitArray();
		this.SLIDER_Preis();
		this.SLIDER_Gewinnbeteiligung();
		this.SetSpezialgenre(this.spezialgenre);
		this.SetSpezialplatform(this.spezialplatform);
		this.UpdateLockedFeatures();
	}

	
	public void InitUpdateEngine(roomScript scriptRoom_, engineScript scriptEngine_)
	{
		this.FindScripts();
		this.rS_ = scriptRoom_;
		this.eSUpdate_ = scriptEngine_;
		this.uiObjects[14].GetComponent<Text>().text = this.tS_.GetText(242);
		this.uiObjects[15].GetComponent<Button>().interactable = false;
		this.InitArray();
		this.uiObjects[4].GetComponent<InputField>().text = this.eSUpdate_.GetName() + "+";
		this.spezialgenre = this.eSUpdate_.spezialgenre;
		this.spezialplatform = this.eSUpdate_.spezialplatform;
		this.uiObjects[2].GetComponent<Slider>().value = (float)(this.eSUpdate_.preis / 1000);
		this.uiObjects[3].GetComponent<Slider>().value = (float)this.eSUpdate_.gewinnbeteiligung;
		this.uiObjects[13].GetComponent<Toggle>().isOn = this.eSUpdate_.sellEngine;
		for (int i = 0; i < this.featuresLock.Length; i++)
		{
			if (this.eSUpdate_.features[i])
			{
				this.featuresLock[i] = true;
				this.features[i] = false;
			}
		}
		this.SLIDER_Preis();
		this.SLIDER_Gewinnbeteiligung();
		this.SetSpezialgenre(this.spezialgenre);
		this.SetSpezialplatform(this.spezialplatform);
		this.UpdateLockedFeatures();
	}

	
	private void InitArray()
	{
		if (this.features.Length != this.eF_.engineFeatures_TYP.Length)
		{
			this.features = new bool[this.eF_.engineFeatures_TYP.Length];
		}
		if (this.featuresLock.Length != this.eF_.engineFeatures_TYP.Length)
		{
			this.featuresLock = new bool[this.eF_.engineFeatures_TYP.Length];
		}
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		if (this.eSUpdate_)
		{
			this.DeleteAllDatas();
			this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[41]);
			this.guiMain_.uiObjects[41].GetComponent<Menu_Dev_Engine_SelectOld>().Init(this.rS_);
		}
		else
		{
			this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[36]);
		}
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_RandomEngineName()
	{
		this.sfx_.PlaySound(3, true);
		this.uiObjects[4].GetComponent<InputField>().text = this.tS_.GetRandomEngineName();
	}

	
	public void BUTTON_Preis(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.uiObjects[2].GetComponent<Slider>().value += (float)i;
	}

	
	public void BUTTON_Gewinnbeteiligung(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.uiObjects[3].GetComponent<Slider>().value += (float)i;
	}

	
	public void BUTTON_Spezialgenre()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[38]);
		this.guiMain_.uiObjects[38].GetComponent<Menu_Dev_Engine_Genre>().Init();
	}

	
	public void BUTTON_Spezialplatform()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[237]);
		this.guiMain_.uiObjects[237].GetComponent<Menu_Dev_EnginePlatform>().Init();
	}

	
	public void BUTTON_Features()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[39]);
		this.guiMain_.uiObjects[39].GetComponent<Menu_Dev_EngineFeatures>().Init();
	}

	
	public void BUTTON_OK()
	{
		this.sfx_.PlaySound(3, true);
		if (this.spezialgenre == -1)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(251), false);
			return;
		}
		if (this.uiObjects[4].GetComponent<InputField>().text.Length <= 0)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(252), false);
			return;
		}
		if (this.spezialplatform == -1)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1219), false);
			return;
		}
		if (this.spezialplatform != -1)
		{
			GameObject gameObject = GameObject.Find("PLATFORM_" + this.spezialplatform);
			if (gameObject)
			{
				platformScript component = gameObject.GetComponent<platformScript>();
				if (this.techLevel > component.tech)
				{
					this.guiMain_.MessageBox(this.tS_.GetText(1220), false);
					return;
				}
			}
		}
		if (this.preis <= 0)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(253), false);
			return;
		}
		if (this.uiObjects[13].GetComponent<Toggle>().isOn && Mathf.RoundToInt(this.uiObjects[2].GetComponent<Slider>().value) <= 0 && Mathf.RoundToInt(this.uiObjects[3].GetComponent<Slider>().value) <= 0)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1174), false);
			return;
		}
		this.UpdateFeatureData();
		this.mS_.Pay((long)this.preis, 4);
		if (!this.eSUpdate_)
		{
			engineScript engineScript = this.eF_.CreateEngine();
			engineScript.myID = this.mS_.GetNewID();
			engineScript.myName = this.uiObjects[4].GetComponent<InputField>().text;
			engineScript.ownerID = this.mS_.myID;
			engineScript.spezialgenre = this.spezialgenre;
			engineScript.spezialplatform = this.spezialplatform;
			engineScript.spezialplatformUpdate = this.spezialplatform;
			engineScript.sellEngine = this.uiObjects[13].GetComponent<Toggle>().isOn;
			engineScript.preis = Mathf.RoundToInt(this.uiObjects[2].GetComponent<Slider>().value * 1000f);
			engineScript.gewinnbeteiligung = Mathf.RoundToInt(this.uiObjects[3].GetComponent<Slider>().value);
			engineScript.features = new bool[this.features.Length];
			engineScript.featuresInDev = new bool[this.features.Length];
			for (int i = 0; i < this.features.Length; i++)
			{
				if (this.features[i])
				{
					engineScript.featuresInDev[i] = true;
					engineScript.devPoints += (float)this.eF_.GetDevPointsForEngine(i);
				}
				if (this.featuresLock[i])
				{
					engineScript.features[i] = true;
				}
			}
			if (engineScript.devPoints <= 0f)
			{
				engineScript.devPoints = 20f;
			}
			engineScript.devPointsStart = engineScript.devPoints;
			engineScript.Init();
			taskEngine taskEngine = this.guiMain_.AddTask_Engine();
			taskEngine.Init(false);
			taskEngine.engineID = engineScript.myID;
			GameObject gameObject2 = GameObject.Find("Room_" + this.rS_.myID.ToString());
			if (gameObject2)
			{
				gameObject2.GetComponent<roomScript>().taskID = taskEngine.myID;
			}
		}
		else
		{
			this.eSUpdate_.myName = this.uiObjects[4].GetComponent<InputField>().text;
			this.eSUpdate_.sellEngine = this.uiObjects[13].GetComponent<Toggle>().isOn;
			this.eSUpdate_.preis = Mathf.RoundToInt(this.uiObjects[2].GetComponent<Slider>().value * 1000f);
			this.eSUpdate_.gewinnbeteiligung = Mathf.RoundToInt(this.uiObjects[3].GetComponent<Slider>().value);
			this.eSUpdate_.featuresInDev = new bool[this.features.Length];
			this.eSUpdate_.spezialplatformUpdate = this.spezialplatform;
			this.eSUpdate_.updating = true;
			for (int j = 0; j < this.features.Length; j++)
			{
				if (this.features[j])
				{
					this.eSUpdate_.featuresInDev[j] = true;
					this.eSUpdate_.devPoints += (float)this.eF_.GetDevPointsForEngine(j);
				}
			}
			this.eSUpdate_.devPointsStart = this.eSUpdate_.devPoints;
			taskEngine taskEngine2 = this.guiMain_.AddTask_Engine();
			taskEngine2.Init(false);
			taskEngine2.engineID = this.eSUpdate_.myID;
			GameObject gameObject3 = GameObject.Find("Room_" + this.rS_.myID.ToString());
			if (gameObject3)
			{
				gameObject3.GetComponent<roomScript>().taskID = taskEngine2.myID;
			}
		}
		this.DeleteAllDatas();
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	
	public void SLIDER_Preis()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt(this.uiObjects[2].GetComponent<Slider>().value * 1000f), true);
	}

	
	public void SLIDER_Gewinnbeteiligung()
	{
		this.uiObjects[1].GetComponent<Text>().text = Mathf.RoundToInt(this.uiObjects[3].GetComponent<Slider>().value).ToString() + "%";
	}

	
	public void TOGGLE_EngineVerkaufen()
	{
		this.sfx_.PlaySound(12, true);
	}

	
	public void SetSpezialgenre(int i)
	{
		if (i >= 0)
		{
			this.spezialgenre = i;
			this.uiObjects[5].GetComponent<Image>().sprite = this.genres_.GetPic(this.spezialgenre);
			this.uiObjects[6].GetComponent<Text>().text = this.genres_.GetName(this.spezialgenre);
			this.uiObjects[7].GetComponent<Image>().sprite = this.guiMain_.uiSprites[1];
			this.guiMain_.DrawStars(this.uiObjects[8], this.genres_.genres_LEVEL[this.spezialgenre]);
			return;
		}
		this.uiObjects[5].GetComponent<Image>().sprite = this.guiMain_.uiSprites[3];
		this.uiObjects[6].GetComponent<Text>().text = this.tS_.GetText(248);
		this.uiObjects[7].GetComponent<Image>().sprite = this.guiMain_.uiSprites[0];
		this.guiMain_.DrawStars(this.uiObjects[8], 0);
	}

	
	public void SetSpezialplatform(int i)
	{
		if (i < 0)
		{
			this.uiObjects[16].GetComponent<Image>().material = null;
			this.uiObjects[16].GetComponent<Image>().color = Color.white;
			this.uiObjects[16].GetComponent<Image>().sprite = this.guiMain_.uiSprites[3];
			this.uiObjects[17].GetComponent<Text>().text = this.tS_.GetText(248);
			this.uiObjects[19].GetComponent<Image>().sprite = this.guiMain_.uiSprites[0];
			this.guiMain_.DrawStars(this.uiObjects[18], 0);
			this.uiObjects[20].GetComponent<Text>().text = "-";
			return;
		}
		GameObject gameObject = GameObject.Find("PLATFORM_" + i);
		if (gameObject)
		{
			platformScript component = gameObject.GetComponent<platformScript>();
			this.spezialplatform = i;
			component.SetPic(this.uiObjects[16]);
			this.uiObjects[17].GetComponent<Text>().text = component.GetName();
			this.uiObjects[19].GetComponent<Image>().sprite = this.guiMain_.uiSprites[1];
			this.guiMain_.DrawStars(this.uiObjects[18], component.erfahrung);
			this.uiObjects[20].GetComponent<Text>().text = component.tech.ToString();
			return;
		}
		this.SetSpezialplatform(-1);
	}

	
	public void SetFeature(int i, bool b)
	{
		this.features[i] = b;
	}

	
	private void OnEnable()
	{
		this.FindScripts();
		this.cmS_.disableMovement = true;
	}

	
	private void OnDisable()
	{
		this.FindScripts();
		this.cmS_.disableMovement = false;
	}

	
	private void DeleteAllDatas()
	{
		this.uiObjects[4].GetComponent<InputField>().text = "";
		this.spezialgenre = -1;
		this.spezialplatform = -1;
		this.eSUpdate_ = null;
		for (int i = 0; i < this.features.Length; i++)
		{
			this.features[i] = false;
			this.featuresLock[i] = false;
		}
	}

	
	public void BUTTON_PlatformKaufen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[33]);
	}

	
	public GameObject[] uiObjects;

	
	private roomScript rS_;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private genres genres_;

	
	private engineFeatures eF_;

	
	private cameraMovementScript cmS_;

	
	public engineScript eSUpdate_;

	
	public int spezialgenre = -1;

	
	public int spezialplatform = -1;

	
	public int featureAnzahl;

	
	public int preis;

	
	public int techLevel;

	
	public bool[] features;

	
	public bool[] featuresLock;
}
