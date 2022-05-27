using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Stats_Engine_View : MonoBehaviour
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
	}

	
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	
	private void MultiplayerUpdate()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.updateTimer += Time.deltaTime;
		if (this.updateTimer < 5f)
		{
			return;
		}
		this.updateTimer = 0f;
		this.SetData();
	}

	
	public void Init(engineScript s)
	{
		this.FindScripts();
		this.eS_ = s;
		this.SetData();
		if (this.eS_.playerEngine)
		{
			this.uiObjects[8].SetActive(true);
			this.uiObjects[16].SetActive(false);
			this.uiObjects[9].GetComponent<Slider>().value = (float)(this.eS_.preis / 1000);
			this.uiObjects[10].GetComponent<Slider>().value = (float)this.eS_.gewinnbeteiligung;
			this.uiObjects[11].GetComponent<Toggle>().isOn = this.eS_.sellEngine;
			this.uiObjects[17].GetComponent<Text>().text = this.mS_.GetMoney((long)this.eS_.umsatz, true);
			this.uiObjects[18].GetComponent<Text>().text = this.eS_.GetVerkaufteLizenzen().ToString();
			return;
		}
		this.uiObjects[8].SetActive(false);
		this.uiObjects[16].SetActive(true);
	}

	
	private void SetData()
	{
		if (!this.eS_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.eS_.GetName();
		this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(4) + " " + this.eS_.GetTechLevel();
		this.uiObjects[2].GetComponent<Text>().text = this.genres_.GetName(this.eS_.spezialgenre);
		this.uiObjects[4].GetComponent<Text>().text = this.eS_.GetGamesAmount().ToString() + " " + this.tS_.GetText(271);
		this.uiObjects[5].GetComponent<Text>().text = this.eS_.GetFeaturesAmount().ToString() + " " + this.tS_.GetText(272);
		this.uiObjects[6].GetComponent<Text>().text = this.mS_.GetMoney((long)this.eS_.preis, true);
		this.uiObjects[7].GetComponent<Text>().text = this.eS_.gewinnbeteiligung.ToString() + "%";
		this.uiObjects[12].GetComponent<Image>().sprite = this.genres_.GetPic(this.eS_.spezialgenre);
		if (this.eS_.date_year > 0)
		{
			this.uiObjects[19].GetComponent<Text>().text = this.eS_.GetReleaseDateString();
		}
		else
		{
			this.uiObjects[19].GetComponent<Text>().text = "";
		}
		this.guiMain_.DrawStars(this.uiObjects[3], this.genres_.genres_LEVEL[this.eS_.spezialgenre]);
		platformScript spezialPlatformScript = this.eS_.GetSpezialPlatformScript();
		if (spezialPlatformScript)
		{
			this.uiObjects[15].GetComponent<Text>().text = spezialPlatformScript.GetName();
			spezialPlatformScript.SetPic(this.uiObjects[13]);
			this.guiMain_.DrawStars(this.uiObjects[14], spezialPlatformScript.erfahrung);
		}
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Kaufen()
	{
		this.sfx_.PlaySound(3, true);
		this.mS_.Pay((long)this.eS_.preis, 5);
		this.eS_.gekauft = true;
		this.guiMain_.uiObjects[42].GetComponent<Menu_BuyEngine>().OnEnable();
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_ShowFeatures()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[44]);
		this.guiMain_.uiObjects[44].GetComponent<Menu_Engine_ShowFeatures>().Init(this.eS_);
	}

	
	public void BUTTON_ShowGames()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[45]);
		this.guiMain_.uiObjects[45].GetComponent<Menu_Engine_ShowGames>().Init(this.eS_);
	}

	
	public void SLIDER_Preis()
	{
		this.uiObjects[6].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt(this.uiObjects[9].GetComponent<Slider>().value * 1000f), true);
	}

	
	public void SLIDER_Gewinnbeteiligung()
	{
		this.uiObjects[7].GetComponent<Text>().text = Mathf.RoundToInt(this.uiObjects[10].GetComponent<Slider>().value).ToString() + "%";
	}

	
	public void TOGGLE_EngineVerkaufen()
	{
		this.sfx_.PlaySound(12, true);
	}

	
	public void BUTTON_Preis(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.uiObjects[9].GetComponent<Slider>().value += (float)i;
	}

	
	public void BUTTON_Gewinnbeteiligung(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.uiObjects[10].GetComponent<Slider>().value += (float)i;
	}

	
	public void BUTTON_OK()
	{
		this.sfx_.PlaySound(3, true);
		if (this.eS_.playerEngine && this.uiObjects[11].GetComponent<Toggle>().isOn && Mathf.RoundToInt(this.uiObjects[9].GetComponent<Slider>().value) <= 0 && Mathf.RoundToInt(this.uiObjects[10].GetComponent<Slider>().value) <= 0)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1174), false);
			return;
		}
		if (this.eS_.playerEngine)
		{
			this.eS_.preis = Mathf.RoundToInt(this.uiObjects[9].GetComponent<Slider>().value * 1000f);
			this.eS_.gewinnbeteiligung = Mathf.RoundToInt(this.uiObjects[10].GetComponent<Slider>().value);
			this.eS_.sellEngine = this.uiObjects[11].GetComponent<Toggle>().isOn;
			if (this.mS_.multiplayer)
			{
				if (this.mS_.mpCalls_.isServer)
				{
					this.mS_.mpCalls_.SERVER_Send_Engine(this.eS_);
				}
				if (this.mS_.mpCalls_.isClient)
				{
					this.mS_.mpCalls_.CLIENT_Send_Engine(this.eS_);
				}
			}
		}
		base.gameObject.SetActive(false);
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

	
	private engineScript eS_;

	
	private float updateTimer;
}
