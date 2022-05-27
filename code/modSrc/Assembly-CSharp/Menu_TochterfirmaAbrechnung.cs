using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_TochterfirmaAbrechnung : MonoBehaviour
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
		Debug.Log("MENU_TOCHTERFIRMA_ABRECHNUNG");
		this.FindScripts();
		this.gS_ = game_;
		if (!this.gS_.devS_ && this.gS_.developerID != -1)
		{
			this.gS_.FindMyDeveloper();
		}
		if (!this.gS_.pS_)
		{
			this.gS_.FindMyPublisher();
		}
		if (!this.gS_ || !this.gS_.pS_ || !this.gS_.devS_)
		{
			Debug.Log("MENU_TOCHTERFIRMA_ABRECHNUNG(): Abbruch -> Script fehlt");
			this.BUTTON_Close();
			return;
		}
		if (!this.gS_.GetPublisherOrDeveloperIsTochterfirma())
		{
			Debug.Log("MENU_TOCHTERFIRMA_ABRECHNUNG(): Abbruch -> Ist nicht meine Tochterfirma");
			this.BUTTON_Close();
			return;
		}
		publisherScript publisherScript = null;
		if (this.gS_.GetPublisherIsTochtefirma())
		{
			publisherScript = this.gS_.pS_;
		}
		if (this.gS_.GetDeveloperIsTochtefirma())
		{
			publisherScript = this.gS_.devS_;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.gS_.GetNameWithTag();
		this.uiObjects[6].GetComponent<Image>().sprite = publisherScript.GetLogo();
		string text = this.tS_.GetText(1988);
		if (this.gS_.GetPublisherIsTochtefirma())
		{
			text = text.Replace("<NAME1>", "<color=blue>" + this.gS_.pS_.GetName() + "</color>");
		}
		if (this.gS_.GetDeveloperIsTochtefirma())
		{
			text = text.Replace("<NAME1>", "<color=blue>" + this.gS_.devS_.GetName() + "</color>");
		}
		text = text.Replace("<NAME2>", "<color=blue>" + this.gS_.GetNameWithTag() + "</color>");
		this.uiObjects[2].GetComponent<Text>().text = text;
		text = "";
		text = text + this.tS_.GetText(491) + "\n";
		text = text + this.tS_.GetText(275) + "\n";
		text += "\n";
		text = text + this.tS_.GetText(276) + "\n";
		text = text + this.tS_.GetText(1990) + "\n";
		text = text + this.tS_.GetText(503) + "\n";
		this.uiObjects[3].GetComponent<Text>().text = text;
		text = "";
		text = text + this.gS_.weeksOnMarket.ToString() + "\n";
		text = text + this.mS_.GetMoney(this.gS_.sellsTotal, false) + "\n";
		text += "\n";
		text = text + this.mS_.GetMoney(this.gS_.umsatzTotal, true) + "\n";
		text = text + "<color=red>" + this.mS_.GetMoney(this.gS_.umsatzTotal - this.gS_.tw_gewinnanteil * 2L, true) + "</color>\n";
		text = text + Mathf.RoundToInt(this.games_.tf_gewinnbeteiligungTochterfirma).ToString() + "%\n";
		this.uiObjects[4].GetComponent<Text>().text = text;
		this.uiObjects[5].GetComponent<Text>().text = "<color=green>" + this.mS_.GetMoney(this.gS_.tw_gewinnanteil, true) + "</color>";
		if (this.mS_.settings_.disableTochterfirmaAbrechnung)
		{
			this.BUTTON_Close();
		}
		this.sfx_.PlaySound(40, false);
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
