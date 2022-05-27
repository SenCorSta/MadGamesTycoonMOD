using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200014B RID: 331
public class Menu_Dev_TochterfirmaGameComplete : MonoBehaviour
{
	// Token: 0x06000C22 RID: 3106 RVA: 0x000827C9 File Offset: 0x000809C9
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000C23 RID: 3107 RVA: 0x000827D4 File Offset: 0x000809D4
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

	// Token: 0x06000C24 RID: 3108 RVA: 0x00082990 File Offset: 0x00080B90
	private void OnEnable()
	{
		this.FindScripts();
		this.cmS_.disableMovement = true;
	}

	// Token: 0x06000C25 RID: 3109 RVA: 0x000829A4 File Offset: 0x00080BA4
	private void OnDisable()
	{
		this.FindScripts();
		this.cmS_.disableMovement = false;
	}

	// Token: 0x06000C26 RID: 3110 RVA: 0x000829B8 File Offset: 0x00080BB8
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x06000C27 RID: 3111 RVA: 0x000829D4 File Offset: 0x00080BD4
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

	// Token: 0x06000C28 RID: 3112 RVA: 0x00082F92 File Offset: 0x00081192
	public void BUTTON_TochterfirmaUeberlassen()
	{
		this.sfx_.PlaySound(3, true);
		this.ReplaceName();
		this.pS_.ReleaseTheGame(this.gS_, false);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000C29 RID: 3113 RVA: 0x00082FD0 File Offset: 0x000811D0
	public void BUTTON_SelfPublish()
	{
		this.sfx_.PlaySound(3, true);
		this.ReplaceName();
		this.gS_.CalcReview(false);
		this.SelfpublishGame(this.gS_);
	}

	// Token: 0x06000C2A RID: 3114 RVA: 0x00083000 File Offset: 0x00081200
	private void ReplaceName()
	{
		if (this.uiObjects[0].GetComponent<InputField>().interactable && this.uiObjects[0].GetComponent<InputField>().text.Length > 0)
		{
			this.gS_.myName = this.uiObjects[0].GetComponent<InputField>().text;
		}
	}

	// Token: 0x06000C2B RID: 3115 RVA: 0x00083058 File Offset: 0x00081258
	public void BUTTON_Verwerfen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[93]);
		this.guiMain_.uiObjects[93].GetComponent<Menu_W_GameVerwerfen>().Init(this.gS_, null);
	}

	// Token: 0x06000C2C RID: 3116 RVA: 0x000830AC File Offset: 0x000812AC
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

	// Token: 0x04001063 RID: 4195
	public GameObject[] uiObjects;

	// Token: 0x04001064 RID: 4196
	private GameObject main_;

	// Token: 0x04001065 RID: 4197
	private mainScript mS_;

	// Token: 0x04001066 RID: 4198
	private textScript tS_;

	// Token: 0x04001067 RID: 4199
	private GUI_Main guiMain_;

	// Token: 0x04001068 RID: 4200
	private sfxScript sfx_;

	// Token: 0x04001069 RID: 4201
	private genres genres_;

	// Token: 0x0400106A RID: 4202
	private themes themes_;

	// Token: 0x0400106B RID: 4203
	private licences licences_;

	// Token: 0x0400106C RID: 4204
	private engineFeatures eF_;

	// Token: 0x0400106D RID: 4205
	private cameraMovementScript cmS_;

	// Token: 0x0400106E RID: 4206
	private unlockScript unlock_;

	// Token: 0x0400106F RID: 4207
	private gameplayFeatures gF_;

	// Token: 0x04001070 RID: 4208
	private games games_;

	// Token: 0x04001071 RID: 4209
	private forschungSonstiges forschungSonstiges_;

	// Token: 0x04001072 RID: 4210
	private gameScript gS_;

	// Token: 0x04001073 RID: 4211
	private publisherScript pS_;
}
