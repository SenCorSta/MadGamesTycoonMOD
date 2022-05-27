using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Umsatz_Konsole : MonoBehaviour
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
		if (!this.platforms_)
		{
			this.platforms_ = this.main_.GetComponent<platforms>();
		}
	}

	
	public void Init(platformScript plat_)
	{
		this.FindScripts();
		this.pS_ = plat_;
		if (!this.pS_)
		{
			this.BUTTON_Close();
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
		this.uiObjects[1].GetComponent<Text>().text = this.pS_.GetManufacturer();
		this.pS_.SetPic(this.uiObjects[7]);
		string text = "";
		text = text + this.tS_.GetText(491) + "\n";
		text = text + this.tS_.GetText(696) + "\n";
		text += "\n";
		text = text + this.tS_.GetText(6) + "\n";
		text = text + this.tS_.GetText(492) + "\n";
		text = text + this.tS_.GetText(530) + "\n";
		text += "\n";
		text += this.tS_.GetText(1239);
		this.uiObjects[3].GetComponent<Text>().text = text;
		text = "";
		text = text + this.pS_.weeksOnMarket.ToString() + "\n";
		text = text + this.mS_.GetMoney((long)this.pS_.units, false) + "\n";
		text += "\n";
		text += "<color=red>";
		text = text + this.mS_.GetMoney(this.pS_.entwicklungsKosten, true) + "\n";
		text = text + this.mS_.GetMoney((long)this.pS_.costs_marketing, true) + "\n";
		text = text + this.mS_.GetMoney(this.pS_.costs_production, true) + "\n";
		text += "</color>";
		text += "\n";
		text += "<color=green>";
		text += this.mS_.GetMoney(this.pS_.umsatzTotal, true);
		text += "</color>";
		this.uiObjects[4].GetComponent<Text>().text = text;
		if (this.pS_.GetGesamtGewinn() < 0L)
		{
			this.uiObjects[5].GetComponent<Text>().text = "<color=red>" + this.mS_.GetMoney(this.pS_.GetGesamtGewinn(), true) + "</color>";
			return;
		}
		this.uiObjects[5].GetComponent<Text>().text = "<color=green>" + this.mS_.GetMoney(this.pS_.GetGesamtGewinn(), true) + "</color>";
	}

	
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		if (!this.guiMain_.uiObjects[335].activeSelf && !this.guiMain_.uiObjects[336].activeSelf && !this.guiMain_.uiObjects[343].activeSelf)
		{
			this.guiMain_.CloseMenu();
		}
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

	
	private platforms platforms_;

	
	private platformScript pS_;
}
