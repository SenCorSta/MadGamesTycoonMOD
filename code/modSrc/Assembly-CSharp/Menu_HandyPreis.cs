using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_HandyPreis : MonoBehaviour
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

	
	public void Init(gameScript game_)
	{
		this.FindScripts();
		this.gS_ = game_;
		this.uiObjects[0].GetComponent<Text>().text = this.gS_.GetNameWithTag();
		if (this.gS_.verkaufspreis[3] <= 0)
		{
			this.uiObjects[2].GetComponent<Slider>().value = 2f;
		}
		else
		{
			this.uiObjects[2].GetComponent<Slider>().value = (float)this.gS_.verkaufspreis[3];
		}
		this.SLIDER_Preis();
		if (!this.guiMain_.uiObjects[308].activeSelf)
		{
			this.uiObjects[4].SetActive(false);
			return;
		}
		this.uiObjects[4].SetActive(true);
	}

	
	public void SLIDER_Preis()
	{
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt(this.uiObjects[2].GetComponent<Slider>().value), true);
		float value = this.uiObjects[2].GetComponent<Slider>().value;
		if (1f.Equals(value))
		{
			this.uiObjects[3].GetComponent<Text>().text = "$1.29";
			return;
		}
		if (2f.Equals(value))
		{
			this.uiObjects[3].GetComponent<Text>().text = "$2.59";
			return;
		}
		if (3f.Equals(value))
		{
			this.uiObjects[3].GetComponent<Text>().text = "$3.99";
			return;
		}
		if (4f.Equals(value))
		{
			this.uiObjects[3].GetComponent<Text>().text = "$5.19";
			return;
		}
		if (!5f.Equals(value))
		{
			return;
		}
		this.uiObjects[3].GetComponent<Text>().text = "$6.49";
	}

	
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Ok()
	{
		this.sfx_.PlaySound(3, true);
		this.gS_.verkaufspreis[3] = Mathf.RoundToInt(this.uiObjects[2].GetComponent<Slider>().value);
		if (!this.guiMain_.uiObjects[308].activeSelf)
		{
			this.gS_.SetPublisher(-1);
			this.gS_.SetOnMarket();
			this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[71]);
			this.guiMain_.uiObjects[71].GetComponent<Menu_Dev_XP>().Init(this.gS_);
		}
		base.gameObject.SetActive(false);
	}

	
	public GameObject[] uiObjects;

	
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
}
