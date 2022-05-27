using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Engine_Abrechnung : MonoBehaviour
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
		this.gS_.FindMyEngineNew();
		Debug.Log("ENGINE ABRECHNUNG: " + game_.myName + " / " + game_.myID.ToString());
		if (this.gS_.engineS_ && (!this.gS_.engineS_.sellEngine || this.gS_.engineS_.gewinnbeteiligung <= 0))
		{
			this.BUTTON_Close();
			return;
		}
		if (!this.gS_.devS_ && this.gS_.developerID != -1)
		{
			this.gS_.FindMyDeveloper();
		}
		if (!this.gS_.pS_)
		{
			this.gS_.FindMyPublisher();
		}
		if (!this.gS_ || !this.gS_.engineS_ || !this.gS_.pS_)
		{
			Debug.Log("MENU_ENGINE_ABRECHNUNG(): Abbruch");
			this.BUTTON_Close();
			return;
		}
		if (this.gS_.devS_ && this.gS_.devS_.IsMyTochterfirma())
		{
			Debug.Log("MENU_ENGINE_ABRECHNUNG(): Abbruch -> Ist meine Tochterfirma");
			this.BUTTON_Close();
			return;
		}
		string newValue = "";
		if (this.gS_.devS_)
		{
			newValue = this.gS_.devS_.GetName();
		}
		if (this.gS_.developerID == -1)
		{
			newValue = this.mS_.companyName;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.gS_.engineS_.GetName();
		string text = this.tS_.GetText(502);
		text = text.Replace("<NAME1>", newValue);
		text = text.Replace("<NAME2>", this.gS_.GetNameWithTag());
		this.uiObjects[2].GetComponent<Text>().text = text;
		if (this.gS_.IsMyAuftragsspiel())
		{
			text = this.tS_.GetText(1815);
			text = text.Replace("<NAME1>", this.gS_.GetNameWithTag());
			this.uiObjects[2].GetComponent<Text>().text = text;
		}
		text = "";
		text = text + this.tS_.GetText(491) + "\n";
		text = text + this.tS_.GetText(275) + "\n";
		text += "\n";
		text = text + this.tS_.GetText(489) + "\n";
		text = text + this.tS_.GetText(503) + "\n";
		text = text + this.tS_.GetText(504) + "\n";
		this.uiObjects[3].GetComponent<Text>().text = text;
		text = "";
		text = text + this.gS_.weeksOnMarket.ToString() + "\n";
		text = text + this.mS_.GetMoney(this.gS_.sellsTotal, false) + "\n";
		text += "\n";
		text = text + this.mS_.GetMoney(this.gS_.GetGesamtGewinn(), true) + "\n";
		text = text + this.gS_.engineS_.gewinnbeteiligung + "%\n";
		float num = (float)this.gS_.GetGesamtGewinn();
		num = num / 100f * (float)this.gS_.engineS_.gewinnbeteiligung / (float)this.gS_.sellsTotal;
		text = text + this.mS_.Round(num, 2).ToString() + "$\n";
		this.uiObjects[4].GetComponent<Text>().text = text;
		if (num <= 0f)
		{
			Debug.Log("ENGINE ABRECHNUNG: ABBRUCH -> " + this.gS_.myID);
			this.BUTTON_Close();
			return;
		}
		num = (float)this.gS_.GetGesamtGewinn();
		num = num / 100f * (float)this.gS_.engineS_.gewinnbeteiligung;
		this.uiObjects[5].GetComponent<Text>().text = "<color=green>" + this.mS_.GetMoney((long)Mathf.RoundToInt(num), true) + "</color>";
		this.gS_.engineS_.umsatz += Mathf.RoundToInt(num);
		this.mS_.Earn((long)Mathf.RoundToInt(num), 4);
		if (this.mS_.settings_.disableEngineAbrechnung)
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
