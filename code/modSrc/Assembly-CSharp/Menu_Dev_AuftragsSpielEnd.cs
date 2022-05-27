using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Dev_AuftragsSpielEnd : MonoBehaviour
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
		if (this.mS_.achScript_)
		{
			this.mS_.achScript_.SetAchivement(26);
		}
		this.uiObjects[0].GetComponent<Text>().text = this.gS_.GetNameWithTag();
		float num = (float)this.gS_.reviewTotal / 20f;
		if (!this.gS_.auftragsspiel_zeitAbgelaufen)
		{
			this.uiObjects[2].GetComponent<Image>().sprite = this.guiMain_.uiSprites[14];
			this.uiObjects[3].GetComponent<Text>().text = "<color=green>+" + this.mS_.Round(num, 2) + "</color>";
			this.guiMain_.UpdateAuftragsansehen(num);
		}
		else
		{
			this.uiObjects[2].GetComponent<Image>().sprite = this.guiMain_.uiSprites[13];
			this.uiObjects[3].GetComponent<Text>().text = "<color=red>-5.0%</color>";
			this.guiMain_.UpdateAuftragsansehen(-5f);
		}
		string text = this.tS_.GetText(626);
		text = text.Replace("<NUM>", this.gS_.auftragsspiel_mindestbewertung.ToString());
		this.uiObjects[4].GetComponent<Text>().text = text;
		long num2;
		if (this.gS_.reviewTotal >= this.gS_.auftragsspiel_mindestbewertung)
		{
			this.uiObjects[5].GetComponent<Image>().sprite = this.guiMain_.uiSprites[14];
			this.uiObjects[6].GetComponent<Text>().text = "<color=green>" + this.mS_.GetMoney((long)this.gS_.auftragsspiel_bonus, true) + "</color>";
			this.mS_.Earn((long)this.gS_.auftragsspiel_bonus, 6);
			this.uiObjects[7].GetComponent<Text>().text = this.mS_.GetMoney((long)(this.gS_.auftragsspiel_bonus + this.gS_.auftragsspiel_gehalt), true);
			num2 = (long)(this.gS_.auftragsspiel_bonus + this.gS_.auftragsspiel_gehalt) - this.gS_.GetEntwicklungskosten();
		}
		else
		{
			this.uiObjects[5].GetComponent<Image>().sprite = this.guiMain_.uiSprites[13];
			this.uiObjects[6].GetComponent<Text>().text = "<color=black>$0</color>";
			this.uiObjects[7].GetComponent<Text>().text = this.mS_.GetMoney((long)this.gS_.auftragsspiel_gehalt, true);
			num2 = (long)this.gS_.auftragsspiel_gehalt - this.gS_.GetEntwicklungskosten();
		}
		this.uiObjects[8].GetComponent<Text>().text = this.mS_.GetMoney(this.gS_.GetEntwicklungskosten(), true);
		if (num2 > 0L)
		{
			this.uiObjects[9].GetComponent<Text>().text = "<color=green>" + this.mS_.GetMoney(num2, true) + "</color>";
			return;
		}
		this.uiObjects[9].GetComponent<Text>().text = "<color=red>" + this.mS_.GetMoney(num2, true) + "</color>";
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
		this.guiMain_.CloseMenu();
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
