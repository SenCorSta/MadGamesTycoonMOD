using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Dev_TochterfirmaGameComplete : MonoBehaviour
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
		if (!this.forschungSonstiges_)
		{
			this.forschungSonstiges_ = this.main_.GetComponent<forschungSonstiges>();
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

	
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	
	public void Init(gameScript s1_, publisherScript s2_)
	{
		this.FindScripts();
		this.sfx_.PlaySound(40, true);
		this.gS_ = s1_;
		this.pS_ = s2_;
		if (this.gS_.GetNameWithTag().Replace(" <color=green>[★]</color>", "").Contains("<"))
		{
			this.uiObjects[0].GetComponent<InputField>().text = "";
			this.uiObjects[0].GetComponent<InputField>().interactable = false;
			this.uiObjects[7].GetComponent<Text>().text = this.gS_.GetNameWithTag();
		}
		else
		{
			this.uiObjects[0].GetComponent<InputField>().text = this.gS_.GetNameSimple();
			this.uiObjects[0].GetComponent<InputField>().interactable = true;
			this.uiObjects[7].GetComponent<Text>().text = "";
		}
		this.uiObjects[1].GetComponent<Image>().sprite = this.gS_.GetScreenshot();
		string text = this.tS_.GetText(1976);
		text = text.Replace("<NAME>", "<color=blue><b>" + this.pS_.GetName() + "</b></color>");
		this.uiObjects[2].GetComponent<Text>().text = text;
		text = "";
		if (!this.gS_.typ_bundle && !this.gS_.typ_bundleAddon && this.gS_.subgenre == -1)
		{
			text += this.gS_.GetGenreString();
		}
		if (!this.gS_.typ_bundle && !this.gS_.typ_bundleAddon && this.gS_.subgenre != -1)
		{
			text = text + this.gS_.GetGenreString() + " / " + this.gS_.GetSubGenreString();
		}
		this.uiObjects[3].GetComponent<Text>().text = text;
		this.uiObjects[4].GetComponent<Image>().sprite = this.gS_.GetTypSprite();
		this.uiObjects[4].GetComponent<tooltip>().c = this.gS_.GetTypString();
		this.uiObjects[5].GetComponent<Image>().sprite = this.gS_.GetPlatformTypSprite();
		this.uiObjects[5].GetComponent<tooltip>().c = this.gS_.GetPlatformTypString();
		this.uiObjects[6].GetComponent<Image>().sprite = this.gS_.GetSizeSprite();
		if (this.gS_.reviewTotal <= 0)
		{
			this.gS_.CalcReview(true);
			int num = this.gS_.reviewTotal - 10;
			int num2 = this.gS_.reviewTotal + 10;
			num = num / 10 * 10;
			num2 = num2 / 10 * 10;
			if (num < 1)
			{
				num = 1;
			}
			if (num2 > 100)
			{
				num2 = 100;
			}
			string str = string.Concat(new string[]
			{
				" ",
				num.ToString(),
				"% - ",
				num2.ToString(),
				"%"
			});
			this.uiObjects[8].GetComponent<Text>().text = this.tS_.GetText(1980) + "<color=blue>" + str + "</color>";
			this.gS_.ClearReview();
			this.guiMain_.DrawStars(this.uiObjects[9], Mathf.RoundToInt((float)(num2 / 20)));
		}
		else
		{
			this.uiObjects[8].GetComponent<Text>().text = this.tS_.GetText(277) + ": <color=blue>" + this.gS_.reviewTotal.ToString() + "%</color>";
			this.guiMain_.DrawStars(this.uiObjects[9], Mathf.RoundToInt((float)(this.gS_.reviewTotal / 20)));
		}
		this.uiObjects[10].GetComponent<Image>().sprite = this.pS_.GetLogo();
		this.uiObjects[18].GetComponent<Text>().text = this.mS_.Round(this.gS_.GetIpBekanntheit(), 1).ToString();
		this.gS_.FindMyPlatforms();
		for (int i = 0; i < this.gS_.gamePlatform.Length; i++)
		{
			platformScript platformScript = this.gS_.gamePlatformScript[i];
			if (platformScript)
			{
				if (!this.uiObjects[11 + i].activeSelf)
				{
					this.uiObjects[11 + i].SetActive(true);
				}
				platformScript.SetPic(this.uiObjects[11 + i]);
				this.uiObjects[11 + i].GetComponent<tooltip>().c = platformScript.GetTooltip();
			}
			else if (this.uiObjects[11 + i].activeSelf)
			{
				this.uiObjects[11 + i].SetActive(false);
			}
		}
		this.forschungSonstiges_.Unlock(33, this.uiObjects[15], this.uiObjects[16]);
		text = this.tS_.GetText(1981);
		text = text.Replace("<NUM>", Mathf.RoundToInt(this.games_.tf_gewinnbeteiligungTochterfirma).ToString());
		this.uiObjects[17].GetComponent<tooltip>().c = text + "\n\n";
		text = this.tS_.GetText(1982);
		text = text.Replace("<NUM>", Mathf.RoundToInt(100f - this.games_.tf_gewinnbeteiligungSelfPublish).ToString());
		tooltip component = this.uiObjects[17].GetComponent<tooltip>();
		component.c += text;
	}

	
	public void BUTTON_TochterfirmaUeberlassen()
	{
		this.sfx_.PlaySound(3, true);
		this.ReplaceName();
		this.pS_.ReleaseTheGame(this.gS_, false);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_SelfPublish()
	{
		this.sfx_.PlaySound(3, true);
		this.ReplaceName();
		this.gS_.CalcReview(false);
		this.SelfpublishGame(this.gS_);
	}

	
	private void ReplaceName()
	{
		if (this.uiObjects[0].GetComponent<InputField>().interactable && this.uiObjects[0].GetComponent<InputField>().text.Length > 0)
		{
			this.gS_.myName = this.uiObjects[0].GetComponent<InputField>().text;
		}
	}

	
	public void BUTTON_Verwerfen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[93]);
		this.guiMain_.uiObjects[93].GetComponent<Menu_W_GameVerwerfen>().Init(this.gS_, null);
	}

	
	private void SelfpublishGame(gameScript game_)
	{
		game_.pubOffer = true;
		game_.costs_marketing = 0L;
		game_.costs_mitarbeiter = 0L;
		game_.costs_server = 0L;
		game_.hype = (float)UnityEngine.Random.Range(0, 15);
		game_.pubAngebot_Retail = true;
		game_.pubAngebot_Digital = true;
		game_.pubAngebot_Garantiesumme = 0;
		game_.pubAngebot_Gewinnbeteiligung = this.games_.tf_gewinnbeteiligungSelfPublish;
		game_.date_start_month = UnityEngine.Random.Range(1, 10);
		game_.date_start_year = this.mS_.year - UnityEngine.Random.Range(2, 4);
		if (game_.date_start_year < 1976)
		{
			game_.date_start_year = 1976;
			game_.date_start_month = 1;
		}
		if (!this.mS_.settings_RandomEventsOff)
		{
			if (game_.reviewTotal >= 70 && UnityEngine.Random.Range(0, 100) < this.mS_.difficulty)
			{
				game_.commercialFlop = true;
			}
			if (!game_.commercialFlop && !game_.handy && !game_.typ_addon && !game_.typ_budget && !game_.typ_bundleAddon && !game_.typ_contractGame && !game_.typ_goty && !game_.typ_mmoaddon && UnityEngine.Random.Range(0, 100) == 1)
			{
				game_.commercialHit = true;
			}
		}
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[218]);
		this.guiMain_.uiObjects[218].GetComponent<Menu_Packung>().Init(game_, null, true, true);
		if (this.mS_.multiplayer)
		{
			if (this.mS_.mpCalls_.isServer)
			{
				this.mS_.mpCalls_.SERVER_Send_GameData(game_);
			}
			if (this.mS_.mpCalls_.isClient)
			{
				this.mS_.mpCalls_.CLIENT_Send_GameData(game_);
			}
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

	
	private forschungSonstiges forschungSonstiges_;

	
	private gameScript gS_;

	
	private publisherScript pS_;
}
