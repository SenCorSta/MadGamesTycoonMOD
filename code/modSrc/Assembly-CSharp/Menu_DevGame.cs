using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Menu_DevGame : MonoBehaviour
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
		if (!this.main_)
		{
			return;
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
		if (!this.forschungSonstiges_)
		{
			this.forschungSonstiges_ = this.main_.GetComponent<forschungSonstiges>();
		}
	}

	
	private void OnEnable()
	{
		this.FindScripts();
		if (!this.mS_.settings_TutorialOff)
		{
			this.guiMain_.SetTutorialStep(11);
		}
		this.cmS_.disableMovement = true;
		this.uiObjects[0].GetComponent<InputField>().characterLimit = 50;
		this.uiObjects[204].GetComponent<InputField>().text = "";
		this.searchStringA = "";
		this.CalcDevCosts();
		this.InitDropdowns_GameplayFeatures();
		this.InitDropdown_PlattformTyp(-1);
		string c = string.Concat(new string[]
		{
			this.tS_.GetText(904),
			"\n\n",
			this.tS_.GetText(365),
			"\n\n",
			this.tS_.GetText(1695),
			"\n\n",
			this.tS_.GetText(905),
			"\n\n",
			this.tS_.GetText(1061),
			"\n\n",
			this.tS_.GetText(1062)
		});
		this.uiObjects[147].GetComponent<tooltip>().c = c;
		this.uiObjects[0].GetComponent<InputField>().interactable = true;
		this.uiObjects[124].SetActive(true);
		this.uiObjects[125].SetActive(false);
		this.uiObjects[15].GetComponent<Button>().interactable = true;
		this.uiObjects[16].GetComponent<Button>().interactable = true;
		this.uiObjects[17].GetComponent<Button>().interactable = true;
		this.uiObjects[126].GetComponent<Button>().interactable = true;
		this.uiObjects[124].GetComponent<Button>().interactable = true;
		this.uiObjects[127].GetComponent<Button>().interactable = true;
		this.uiObjects[128].GetComponent<Button>().interactable = true;
		this.uiObjects[129].GetComponent<Button>().interactable = true;
		this.LockDesign(true);
		this.uiObjects[139].GetComponent<Button>().interactable = true;
		this.uiObjects[140].GetComponent<Button>().interactable = true;
		this.uiObjects[33].GetComponent<Button>().interactable = true;
		this.uiObjects[34].GetComponent<Button>().interactable = true;
		this.uiObjects[35].GetComponent<Button>().interactable = true;
		this.uiObjects[146].GetComponent<Dropdown>().interactable = true;
		this.uiObjects[146].GetComponent<Dropdown>().value = 0;
		this.uiObjects[84].GetComponent<Button>().interactable = true;
		this.uiObjects[85].GetComponent<Button>().interactable = true;
		this.uiObjects[173].GetComponent<Button>().interactable = true;
		this.uiObjects[172].GetComponent<Button>().interactable = true;
		this.uiObjects[167].SetActive(false);
		this.uiObjects[214].SetActive(false);
	}

	
	private void OnDisable()
	{
		this.FindScripts();
		this.cmS_.disableMovement = false;
	}

	
	private void InitDropdown_PlattformTyp(int platTyp_)
	{
		List<string> list = new List<string>();
		list = new List<string>();
		list.Add(this.tS_.GetText(902));
		list.Add(this.tS_.GetText(364));
		list.Add(this.tS_.GetText(1694));
		list.Add(this.tS_.GetText(903));
		if (platTyp_ == -1 || platTyp_ == 4 || platTyp_ == 5)
		{
			list.Add(this.tS_.GetText(1059));
			list.Add(this.tS_.GetText(1060));
		}
		this.uiObjects[146].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[146].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[146].GetComponent<Dropdown>().value = 0;
	}

	
	private void LockDesign(bool b)
	{
		for (int i = 0; i < this.uiDesignschwerpunkte.Length; i++)
		{
			this.uiDesignschwerpunkte[i].transform.GetChild(1).GetComponent<Slider>().interactable = b;
		}
		for (int j = 0; j < this.uiDesignausrichtung.Length; j++)
		{
			this.uiDesignausrichtung[j].transform.GetChild(1).GetComponent<Slider>().interactable = b;
		}
		this.uiObjects[222].GetComponent<Button>().interactable = b;
	}

	
	public void SetAndLockPlatformTyp(int platTyp_)
	{
		if (platTyp_ != 5 && platTyp_ != 4)
		{
			this.InitDropdown_PlattformTyp(0);
			this.uiObjects[146].GetComponent<Dropdown>().interactable = true;
		}
		else
		{
			this.uiObjects[146].GetComponent<Dropdown>().interactable = false;
		}
		this.uiObjects[146].GetComponent<Dropdown>().value = platTyp_;
	}

	
	public void CopyAndLockPlatformTyp(gameScript script_)
	{
		if (!script_.handy && !script_.arcade)
		{
			this.InitDropdown_PlattformTyp(0);
			this.uiObjects[146].GetComponent<Dropdown>().interactable = true;
		}
		else
		{
			this.uiObjects[146].GetComponent<Dropdown>().interactable = false;
		}
		this.uiObjects[146].GetComponent<Dropdown>().value = 0;
		if (script_.exklusiv)
		{
			this.uiObjects[146].GetComponent<Dropdown>().value = 1;
		}
		if (script_.herstellerExklusiv)
		{
			this.uiObjects[146].GetComponent<Dropdown>().value = 2;
		}
		if (script_.retro)
		{
			this.uiObjects[146].GetComponent<Dropdown>().value = 3;
		}
		if (script_.arcade)
		{
			this.uiObjects[146].GetComponent<Dropdown>().value = 4;
		}
		if (script_.handy)
		{
			this.uiObjects[146].GetComponent<Dropdown>().value = 5;
		}
	}

	
	private void DrawIpStars()
	{
		if (this.g_mainIP != -1)
		{
			GameObject gameObject = GameObject.Find("GAME_" + this.g_mainIP.ToString());
			if (gameObject)
			{
				this.guiMain_.DrawIpBekanntheit(this.uiObjects[215], gameObject.GetComponent<gameScript>());
				return;
			}
		}
		else
		{
			this.guiMain_.DrawIpBekanntheit(this.uiObjects[215], null);
		}
	}

	
	public void InitSondereinstellungen(int platTyp_)
	{
		if (platTyp_ == 4)
		{
			this.SetGameTyp(0);
			this.uiObjects[126].GetComponent<Button>().interactable = false;
			this.uiObjects[185].SetActive(false);
			this.uiObjects[214].SetActive(true);
			this.uiObjects[84].GetComponent<Button>().interactable = false;
			this.uiObjects[85].GetComponent<Button>().interactable = false;
			this.uiObjects[173].GetComponent<Button>().interactable = false;
			this.uiObjects[172].GetComponent<Button>().interactable = false;
			this.g_InAppPurchase[0] = true;
			this.BUTTON_AlleInAppPurchase();
			this.SetCopyProtect(-1);
			this.SetAntiCheat(-1);
		}
	}

	
	public void InitNewGame(roomScript script_, int platTyp_)
	{
		this.Init(script_);
		this.InitSondereinstellungen(platTyp_);
		this.typ_standard = true;
		this.g_teil = 1;
		this.g_originalIP = -1;
		this.g_portIP = -1;
		this.allFeatures = true;
		this.g_mainIP = -1;
		this.DrawIpStars();
		this.InitDropdown_PlattformTyp(platTyp_);
		this.uiObjects[146].GetComponent<Dropdown>().value = platTyp_;
		switch (platTyp_)
		{
		case 0:
			this.uiObjects[146].GetComponent<Dropdown>().interactable = true;
			break;
		case 1:
			this.uiObjects[146].GetComponent<Dropdown>().interactable = true;
			break;
		case 2:
			this.uiObjects[146].GetComponent<Dropdown>().interactable = true;
			break;
		case 3:
			this.uiObjects[146].GetComponent<Dropdown>().interactable = true;
			break;
		case 4:
			this.uiObjects[146].GetComponent<Dropdown>().interactable = false;
			break;
		case 5:
			this.uiObjects[146].GetComponent<Dropdown>().interactable = false;
			break;
		}
		this.DROPDOWN_PlattformTyp();
		this.uiObjects[123].GetComponent<Image>().sprite = this.games_.gameTypSprites[0];
		this.Init_GameplayFeatures();
		this.CalcDevCosts();
	}

	
	public void InitPort(roomScript rscript_, int orgIP_, int platTyp_)
	{
		this.DeleteAllDatas();
		gameScript gameScript = null;
		GameObject gameObject = GameObject.Find("GAME_" + orgIP_.ToString());
		if (gameObject)
		{
			gameScript = gameObject.GetComponent<gameScript>();
			this.uiObjects[0].GetComponent<InputField>().text = gameScript.GetNameSimple();
			this.g_Beschreibung = gameScript.beschreibung;
			this.g_GameTyp = gameScript.gameTyp;
			this.g_GameSize = gameScript.gameSize;
			this.g_GameZielgruppe = gameScript.gameZielgruppe;
			this.g_GameMainGenre = gameScript.maingenre;
			this.g_GameSubGenre = gameScript.subgenre;
			this.g_GameMainTheme = gameScript.gameMainTheme;
			this.g_GameSubTheme = gameScript.gameSubTheme;
			this.CopyDesignSettings(gameScript);
		}
		this.Init(rscript_);
		this.g_mainIP = gameScript.mainIP;
		this.DrawIpStars();
		this.g_originalIP = gameScript.originalIP;
		this.typ_standard = gameScript.typ_standard;
		this.typ_nachfolger = gameScript.typ_nachfolger;
		this.typ_spinoff = gameScript.typ_spinoff;
		this.typ_remaster = gameScript.typ_remaster;
		this.typ_contractGame = gameScript.typ_contractGame;
		this.typ_addon = gameScript.typ_addon;
		this.typ_bundle = gameScript.typ_bundle;
		this.typ_budget = gameScript.typ_budget;
		this.typ_addonStandalone = gameScript.typ_addonStandalone;
		this.typ_mmoaddon = gameScript.typ_mmoaddon;
		this.g_portIP = orgIP_;
		this.SetLicence(gameScript.gameLicence);
		this.InitDropdown_PlattformTyp(platTyp_);
		this.uiObjects[146].GetComponent<Dropdown>().value = platTyp_;
		switch (platTyp_)
		{
		case 0:
			this.uiObjects[146].GetComponent<Dropdown>().interactable = true;
			break;
		case 1:
			this.uiObjects[146].GetComponent<Dropdown>().interactable = true;
			break;
		case 2:
			this.uiObjects[146].GetComponent<Dropdown>().interactable = true;
			break;
		case 3:
			this.uiObjects[146].GetComponent<Dropdown>().interactable = true;
			break;
		case 4:
			this.uiObjects[146].GetComponent<Dropdown>().interactable = false;
			break;
		case 5:
			this.uiObjects[146].GetComponent<Dropdown>().interactable = false;
			break;
		}
		this.DROPDOWN_PlattformTyp();
		this.uiObjects[123].GetComponent<Image>().sprite = this.games_.gameTypSprites[14];
		this.uiObjects[15].GetComponent<Button>().interactable = false;
		this.uiObjects[16].GetComponent<Button>().interactable = false;
		this.uiObjects[17].GetComponent<Button>().interactable = false;
		this.uiObjects[126].GetComponent<Button>().interactable = false;
		this.uiObjects[127].GetComponent<Button>().interactable = false;
		this.uiObjects[128].GetComponent<Button>().interactable = false;
		this.uiObjects[129].GetComponent<Button>().interactable = false;
		this.LockDesign(false);
		this.InitSondereinstellungen(this.uiObjects[146].GetComponent<Dropdown>().value);
		this.Init_GameplayFeatures();
		this.CalcDevCosts();
	}

	
	public void InitNachfolger(roomScript rscript_, int orgIP_, int platTyp_)
	{
		this.DeleteAllDatas();
		gameScript gameScript = null;
		GameObject gameObject = GameObject.Find("GAME_" + orgIP_.ToString());
		if (gameObject)
		{
			gameScript = gameObject.GetComponent<gameScript>();
			gameScript.nachfolger_created = true;
			this.g_teil = gameScript.teile + 1;
			this.uiObjects[0].GetComponent<InputField>().text = gameScript.GetNameSimple();
			this.g_Beschreibung = gameScript.beschreibung;
			this.g_GameTyp = gameScript.gameTyp;
			this.g_GameSize = gameScript.gameSize;
			this.g_GameZielgruppe = gameScript.gameZielgruppe;
			this.g_GameMainGenre = gameScript.maingenre;
			this.g_GameSubGenre = gameScript.subgenre;
			this.g_GameMainTheme = gameScript.gameMainTheme;
			this.g_GameSubTheme = gameScript.gameSubTheme;
			this.CopyDesignSettings(gameScript);
		}
		this.Init(rscript_);
		this.typ_nachfolger = true;
		this.g_mainIP = gameScript.mainIP;
		this.DrawIpStars();
		this.g_originalIP = orgIP_;
		this.uiObjects[123].GetComponent<Image>().sprite = this.games_.gameTypSprites[1];
		this.uiObjects[128].GetComponent<Button>().interactable = false;
		this.SetAndLockPlatformTyp(platTyp_);
		this.InitSondereinstellungen(this.uiObjects[146].GetComponent<Dropdown>().value);
		this.Init_GameplayFeatures();
		this.CalcDevCosts();
	}

	
	public void InitSpinoff(roomScript rscript_, int orgIP_, int platTyp_)
	{
		this.DeleteAllDatas();
		gameScript gameScript = null;
		GameObject gameObject = GameObject.Find("GAME_" + orgIP_.ToString());
		if (gameObject)
		{
			gameScript = gameObject.GetComponent<gameScript>();
			this.uiObjects[0].GetComponent<InputField>().text = gameScript.GetNameSimple();
			this.g_Beschreibung = gameScript.beschreibung;
			this.g_GameTyp = gameScript.gameTyp;
			this.g_GameSize = gameScript.gameSize;
			this.g_GameZielgruppe = gameScript.gameZielgruppe;
			this.g_GameMainGenre = gameScript.maingenre;
			this.g_GameSubGenre = gameScript.subgenre;
			this.g_GameMainTheme = gameScript.gameMainTheme;
			this.g_GameSubTheme = gameScript.gameSubTheme;
			this.CopyDesignSettings(gameScript);
		}
		this.Init(rscript_);
		this.typ_spinoff = true;
		this.g_mainIP = gameScript.mainIP;
		this.DrawIpStars();
		this.g_originalIP = orgIP_;
		this.uiObjects[123].GetComponent<Image>().sprite = this.games_.gameTypSprites[13];
		this.SetAndLockPlatformTyp(platTyp_);
		this.InitSondereinstellungen(this.uiObjects[146].GetComponent<Dropdown>().value);
		this.Init_GameplayFeatures();
		this.CalcDevCosts();
	}

	
	public void InitRemaster(roomScript rscript_, int orgIP_)
	{
		this.DeleteAllDatas();
		gameScript gameScript = null;
		GameObject gameObject = GameObject.Find("GAME_" + orgIP_.ToString());
		if (gameObject)
		{
			gameScript = gameObject.GetComponent<gameScript>();
			gameScript.remaster_created = true;
			this.orignal_name = gameScript.GetNameSimple();
			this.BUTTON_RemasterName(0);
			this.g_Beschreibung = gameScript.beschreibung;
			this.g_GameTyp = gameScript.gameTyp;
			this.g_GameSize = gameScript.gameSize;
			this.g_GameZielgruppe = gameScript.gameZielgruppe;
			this.g_GameMainGenre = gameScript.maingenre;
			this.g_GameSubGenre = gameScript.subgenre;
			this.g_GameMainTheme = gameScript.gameMainTheme;
			this.g_GameSubTheme = gameScript.gameSubTheme;
			this.CopyDesignSettings(gameScript);
		}
		this.Init(rscript_);
		this.typ_remaster = true;
		this.g_mainIP = gameScript.mainIP;
		this.DrawIpStars();
		this.g_originalIP = orgIP_;
		this.uiObjects[123].GetComponent<Image>().sprite = this.games_.gameTypSprites[2];
		this.uiObjects[15].GetComponent<Button>().interactable = false;
		this.uiObjects[16].GetComponent<Button>().interactable = false;
		this.uiObjects[17].GetComponent<Button>().interactable = false;
		this.uiObjects[126].GetComponent<Button>().interactable = false;
		this.uiObjects[127].GetComponent<Button>().interactable = false;
		this.uiObjects[128].GetComponent<Button>().interactable = false;
		this.uiObjects[129].GetComponent<Button>().interactable = false;
		this.LockDesign(false);
		this.CopyAndLockPlatformTyp(gameScript);
		this.InitSondereinstellungen(this.uiObjects[146].GetComponent<Dropdown>().value);
		this.Init_GameplayFeatures();
		this.CalcDevCosts();
	}

	
	public void InitContractGame(roomScript rscript_, gameScript contractGame_)
	{
		this.DeleteAllDatas();
		this.uiObjects[14].GetComponent<Text>().text = this.tS_.GetText(598);
		if (contractGame_)
		{
			this.contractAuftragsspiel_ = contractGame_;
			this.uiObjects[0].GetComponent<InputField>().text = contractGame_.GetNameSimple();
			this.g_GameTyp = 0;
			this.g_GameSize = contractGame_.gameSize;
			this.g_GameMainGenre = contractGame_.maingenre;
			this.g_GamePlatform[0] = contractGame_.gamePlatform[0];
		}
		this.Init(rscript_);
		this.g_mainIP = -1;
		this.DrawIpStars();
		this.typ_contractGame = true;
		this.typ_standard = true;
		this.uiObjects[123].GetComponent<Image>().sprite = this.games_.gameTypSprites[3];
		this.uiObjects[0].GetComponent<InputField>().interactable = false;
		this.uiObjects[17].GetComponent<Button>().interactable = false;
		this.uiObjects[126].GetComponent<Button>().interactable = false;
		this.uiObjects[128].GetComponent<Button>().interactable = false;
		this.uiObjects[124].GetComponent<Button>().interactable = false;
		this.uiObjects[139].GetComponent<Button>().interactable = false;
		this.uiObjects[140].GetComponent<Button>().interactable = false;
		this.uiObjects[33].GetComponent<Button>().interactable = false;
		this.uiObjects[34].GetComponent<Button>().interactable = false;
		this.uiObjects[35].GetComponent<Button>().interactable = false;
		this.uiObjects[146].GetComponent<Dropdown>().interactable = false;
		this.uiObjects[146].GetComponent<Dropdown>().value = 1;
		this.ResetDesignSettings();
		this.uiObjects[162].SetActive(true);
		this.uiObjects[185].SetActive(false);
		this.uiObjects[200].SetActive(true);
		this.g_InAppPurchase[0] = true;
		this.BUTTON_AlleInAppPurchase();
		this.uiObjects[167].SetActive(true);
		string text = this.tS_.GetText(605);
		text = text.Replace("<NUM>", contractGame_.auftragsspiel_zeitInWochen.ToString());
		this.uiObjects[163].GetComponent<Text>().text = text;
		text = this.tS_.GetText(626);
		text = text.Replace("<NUM>", contractGame_.auftragsspiel_mindestbewertung.ToString());
		this.uiObjects[164].GetComponent<Text>().text = text;
		this.uiObjects[165].GetComponent<Text>().text = this.tS_.GetText(600) + ": " + this.mS_.GetMoney((long)contractGame_.auftragsspiel_gehalt, true);
		this.uiObjects[166].GetComponent<Text>().text = this.tS_.GetText(627) + ": " + this.mS_.GetMoney((long)contractGame_.auftragsspiel_bonus, true);
		this.InitSondereinstellungen(this.uiObjects[146].GetComponent<Dropdown>().value);
		this.Init_GameplayFeatures();
		this.CalcDevCosts();
	}

	
	public void Init(roomScript script_)
	{
		this.FindScripts();
		this.rS_ = script_;
		this.typ_standard = false;
		this.typ_nachfolger = false;
		this.typ_remaster = false;
		this.typ_addon = false;
		this.typ_bundle = false;
		this.typ_budget = false;
		this.typ_addonStandalone = false;
		this.typ_mmoaddon = false;
		this.typ_spinoff = false;
		this.Unlock(31, this.uiObjects[86], this.uiObjects[84]);
		this.Unlock(31, null, this.uiObjects[85]);
		this.forschungSonstiges_.Unlock(35, this.uiObjects[18], this.uiObjects[15]);
		this.forschungSonstiges_.Unlock(36, this.uiObjects[19], this.uiObjects[16]);
		this.Unlock(64, this.uiObjects[174], this.uiObjects[173]);
		this.Unlock(64, null, this.uiObjects[172]);
		this.Unlock(25, this.uiObjects[20], this.uiObjects[17]);
		this.Unlock(25, null, this.uiObjects[62]);
		this.Unlock(28, this.uiObjects[36], this.uiObjects[33]);
		this.Unlock(29, this.uiObjects[37], this.uiObjects[34]);
		this.Unlock(30, this.uiObjects[38], this.uiObjects[35]);
		this.uiObjects[200].SetActive(false);
		if (this.gF_.IsErforscht(57))
		{
			if (this.uiObjects[185].activeSelf)
			{
				this.uiObjects[185].SetActive(false);
			}
		}
		else if (!this.uiObjects[185].activeSelf)
		{
			this.uiObjects[185].SetActive(true);
		}
		this.uiObjects[186].GetComponent<Text>().text = "$" + this.mS_.Round(this.games_.inAppPurchasePrice[0], 1).ToString() + "0";
		this.uiObjects[187].GetComponent<Text>().text = "$" + this.mS_.Round(this.games_.inAppPurchasePrice[1], 1).ToString() + "0";
		this.uiObjects[188].GetComponent<Text>().text = "$" + this.mS_.Round(this.games_.inAppPurchasePrice[2], 1).ToString() + "0";
		this.uiObjects[189].GetComponent<Text>().text = "$" + this.mS_.Round(this.games_.inAppPurchasePrice[3], 1).ToString() + "0";
		this.uiObjects[190].GetComponent<Text>().text = "$" + this.mS_.Round(this.games_.inAppPurchasePrice[4], 1).ToString() + "0";
		this.uiObjects[191].GetComponent<Text>().text = "$" + this.mS_.Round(this.games_.inAppPurchasePrice[5], 1).ToString() + "0";
		this.SetLeitenderDesigner(null, false);
		this.SetGameTyp(this.g_GameTyp);
		this.SetGameSize(this.g_GameSize);
		this.SetZielgruppe(this.g_GameZielgruppe);
		this.SetMainGenre(this.g_GameMainGenre);
		this.SetSubGenre(this.g_GameSubGenre);
		this.SetMainTheme(this.g_GameMainTheme);
		this.SetSubTheme(this.g_GameSubTheme);
		this.SetLicence(this.g_GameLicence);
		this.SetPlatform(0, this.g_GamePlatform[0]);
		this.SetPlatform(1, this.g_GamePlatform[1]);
		this.SetPlatform(2, this.g_GamePlatform[2]);
		this.SetPlatform(3, this.g_GamePlatform[3]);
		this.SetEngine(this.mS_.lastUsedEngine);
		this.SetCopyProtect(this.g_GameCopyProtect);
		this.SetAutomaticBestCopyProtect();
		this.SetAntiCheat(this.g_GameAntiCheat);
		this.SetAutomaticBestAntiCheat();
		this.UpdateDesignSettings();
		this.UpdateDesignSlider();
		this.SetAP_Gameplay();
		this.SetAP_Grafik();
		this.SetAP_Sound();
		this.SetAP_Technik();
		this.SetMaxVerdienstInApp();
		for (int i = 0; i < this.g_GameLanguage.Length; i++)
		{
			this.SetLanguage(i);
		}
		this.uiObjects[159].GetComponent<Slider>().value = 100f;
		this.uiObjects[160].GetComponent<Slider>().value = 100f;
		this.uiObjects[161].GetComponent<Slider>().value = 100f;
		this.g_finanzierung_Grundkosten = 100;
		this.g_finanzierung_Kontent = 100;
		this.g_finanzierung_Technology = 100;
		this.uiObjects[162].SetActive(false);
		this.SLIDER_Finanzierung(0);
		this.SLIDER_Finanzierung(1);
		this.SLIDER_Finanzierung(2);
		this.OpenSide(0);
	}

	
	private void Unlock(int id_, GameObject lock_, GameObject button_)
	{
		if (this.unlock_.unlock[id_])
		{
			button_.GetComponent<Button>().interactable = true;
			if (lock_)
			{
				lock_.SetActive(false);
				return;
			}
		}
		else
		{
			button_.GetComponent<Button>().interactable = false;
			if (lock_)
			{
				lock_.SetActive(true);
			}
		}
	}

	
	public void OpenSide(int i)
	{
		this.sfx_.PlaySound(3, false);
		this.UpdateDesignSettings();
		if (this.uiObjects[12].activeSelf && i != 0)
		{
			this.uiObjects[12].SetActive(false);
		}
		if (this.uiObjects[13].activeSelf && i != 1)
		{
			this.uiObjects[13].SetActive(false);
		}
		if (this.uiObjects[67].activeSelf && i != 2)
		{
			this.uiObjects[67].SetActive(false);
		}
		if (this.uiObjects[91].activeSelf && i != 3)
		{
			this.uiObjects[91].SetActive(false);
		}
		if (this.uiObjects[118].activeSelf && i != 4)
		{
			this.uiObjects[118].SetActive(false);
		}
		if (this.uiObjects[151].activeSelf && i != 5)
		{
			this.uiObjects[151].SetActive(false);
		}
		this.seite = i;
		for (int j = 0; j < this.uiObjects[32].transform.childCount; j++)
		{
			this.uiObjects[32].transform.GetChild(j).GetComponent<Image>().color = Color.white;
		}
		this.uiObjects[32].transform.GetChild(i).GetComponent<Image>().color = Color.grey;
		switch (i)
		{
		case 0:
			if (!this.uiObjects[12].activeSelf)
			{
				this.uiObjects[12].SetActive(true);
				return;
			}
			break;
		case 1:
			if (!this.uiObjects[13].activeSelf)
			{
				this.uiObjects[13].SetActive(true);
				return;
			}
			break;
		case 2:
			if (!this.uiObjects[67].activeSelf)
			{
				this.uiObjects[67].SetActive(true);
				return;
			}
			break;
		case 3:
			if (!this.uiObjects[91].activeSelf)
			{
				this.uiObjects[91].SetActive(true);
				return;
			}
			break;
		case 4:
			if (!this.uiObjects[118].activeSelf)
			{
				this.uiObjects[118].SetActive(true);
				base.StartCoroutine(this.iDROPDOWN_SortGameplayFeatures());
				return;
			}
			break;
		case 5:
			if (!this.uiObjects[151].activeSelf)
			{
				this.uiObjects[151].SetActive(true);
			}
			break;
		default:
			return;
		}
	}

	
	private IEnumerator iDROPDOWN_SortGameplayFeatures()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		this.DROPDOWN_SortGameplayFeatures();
		yield break;
	}

	
	public void NextSide(int i)
	{
		this.seite += i;
		if (this.seite < 0)
		{
			this.seite = 0;
		}
		if (this.seite > 5)
		{
			this.seite = 5;
		}
		this.OpenSide(this.seite);
		this.sfx_.PlaySound(3, true);
	}

	
	public void BUTTON_Beschreibung()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[198]);
	}

	
	public void BUTTON_Spielkonzepte()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[109]);
	}

	
	public void BUTTON_Spielberichte()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[182]);
		this.guiMain_.uiObjects[182].GetComponent<Menu_QA_ShowSpielberichtSelectGame>().Init();
	}

	
	public void BUTTON_Fanbriefe()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[111]);
	}

	
	public void BUTTON_Marktumfrage()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[230]);
		this.guiMain_.uiObjects[230].GetComponent<Menu_Marktforschung>().Init(null);
	}

	
	public void BUTTON_Start()
	{
		this.sfx_.PlaySound(3, true);
		if (this.mS_.multiplayer && this.typ_contractGame && this.contractAuftragsspiel_ && !this.contractAuftragsspiel_.auftragsspiel)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1812), false);
			return;
		}
		if (this.uiObjects[0].GetComponent<InputField>().text.Length <= 0)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(412), false);
			this.OpenSide(0);
			return;
		}
		if (!this.typ_contractGame && this.g_portIP == -1)
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
			if (array.Length != 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					gameScript component = array[i].GetComponent<gameScript>();
					if (component && component.GetNameSimple() == this.uiObjects[0].GetComponent<InputField>().text)
					{
						this.guiMain_.MessageBox(this.tS_.GetText(618), false);
						this.OpenSide(0);
						return;
					}
				}
			}
		}
		if (this.g_GameMainGenre < 0)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(401), false);
			this.OpenSide(0);
			return;
		}
		if (this.g_GameMainTheme < 0)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(402), false);
			this.OpenSide(0);
			return;
		}
		if (this.g_GamePlatform[0] < 0)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(403), false);
			this.OpenSide(1);
			return;
		}
		if (this.EngineFeatureToHighTechLevel())
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1691), false);
			this.OpenSide(2);
			return;
		}
		if (this.UpdateGesamtArbeitsprioritaet() > 100)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(400), false);
			this.OpenSide(3);
			return;
		}
		if (this.UpdateGesamtArbeitsprioritaet() < 100)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(416), false);
			this.OpenSide(3);
			return;
		}
		if (this.AnzahlLanguages() <= 0)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(404), false);
			this.OpenSide(2);
			return;
		}
		if (this.GetAmountDesignschwerpunkte() > 0)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1464), false);
			this.OpenSide(3);
			return;
		}
		if (this.GetAmountDesignschwerpunkte() < 0)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1465), false);
			this.OpenSide(3);
			return;
		}
		if (this.UpdateGesamtGameplayFeatures() <= 0)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(846), false);
			this.OpenSide(4);
			return;
		}
		if (this.UpdateGesamtGameplayFeatures() > this.maxFeatures_gameSize[this.g_GameSize])
		{
			this.guiMain_.MessageBox(this.tS_.GetText(411), false);
			this.OpenSide(4);
			return;
		}
		if ((this.g_GameTyp == 1 || this.g_GameTyp == 2) && !this.g_GameGameplayFeatures[23])
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1235), false);
			this.OpenSide(4);
			return;
		}
		if (this.g_GameTyp == 2 && this.SetMaxVerdienstInApp() <= 0f)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1388), false);
			this.OpenSide(5);
			return;
		}
		if (this.SetMaxVerdienstInApp() > 0f && (!this.g_GameGameplayFeatures[57] || !this.g_GameGameplayFeatures[23]))
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1369), false);
			this.OpenSide(5);
			return;
		}
		string text = "";
		for (int j = 0; j < this.g_GamePlatform.Length; j++)
		{
			if (this.g_GamePlatform[j] != -1)
			{
				GameObject gameObject = GameObject.Find("PLATFORM_" + this.g_GamePlatform[j].ToString());
				if (gameObject)
				{
					platformScript component2 = gameObject.GetComponent<platformScript>();
					if (component2)
					{
						if (component2.needFeatures[0] != -1 && !this.g_GameGameplayFeatures[component2.needFeatures[0]])
						{
							text = string.Concat(new string[]
							{
								text,
								component2.GetName(),
								": ",
								this.gF_.GetName(component2.needFeatures[0]),
								"\n"
							});
						}
						if (component2.needFeatures[1] != -1 && !this.g_GameGameplayFeatures[component2.needFeatures[1]])
						{
							text = string.Concat(new string[]
							{
								text,
								component2.GetName(),
								": ",
								this.gF_.GetName(component2.needFeatures[1]),
								"\n"
							});
						}
						if (component2.needFeatures[2] != -1 && !this.g_GameGameplayFeatures[component2.needFeatures[2]])
						{
							text = string.Concat(new string[]
							{
								text,
								component2.GetName(),
								": ",
								this.gF_.GetName(component2.needFeatures[2]),
								"\n"
							});
						}
						if ((this.g_GameTyp == 1 || this.g_GameTyp == 2) && !component2.internet)
						{
							this.guiMain_.MessageBox(this.tS_.GetText(1262), false);
							this.OpenSide(1);
							return;
						}
					}
				}
			}
		}
		if (text.Length > 0)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1020) + "\n<color=blue>" + text + "</color>", false);
			this.OpenSide(4);
			return;
		}
		if (this.uiObjects[146].GetComponent<Dropdown>().value == 3)
		{
			for (int k = 0; k < this.g_GamePlatform.Length; k++)
			{
				if (this.g_GamePlatform[k] != -1)
				{
					GameObject gameObject2 = GameObject.Find("PLATFORM_" + this.g_GamePlatform[k].ToString());
					if (gameObject2)
					{
						platformScript component3 = gameObject2.GetComponent<platformScript>();
						if (component3 && !component3.vomMarktGenommen)
						{
							this.guiMain_.MessageBox(this.tS_.GetText(906), false);
							this.OpenSide(1);
							return;
						}
					}
				}
			}
		}
		if (this.uiObjects[146].GetComponent<Dropdown>().value == 2)
		{
			for (int l = 0; l < this.g_GamePlatform.Length; l++)
			{
				if (this.g_GamePlatform[l] != -1)
				{
					GameObject gameObject3 = GameObject.Find("PLATFORM_" + this.g_GamePlatform[l].ToString());
					if (gameObject3)
					{
						platformScript component4 = gameObject3.GetComponent<platformScript>();
						if (component4 && component4.ownerID != this.mS_.myID)
						{
							this.guiMain_.MessageBox(this.tS_.GetText(1696), false);
							this.OpenSide(1);
							return;
						}
					}
				}
			}
		}
		int num = this.CalcDevCosts();
		this.mS_.Pay((long)num, 10);
		gameScript gameScript;
		if (!this.typ_contractGame)
		{
			gameScript = this.games_.CreateNewGame(false, true);
			gameScript.ownerID = this.mS_.myID;
		}
		else
		{
			gameScript = this.contractAuftragsspiel_;
		}
		gameScript.costs_entwicklung = (long)num;
		gameScript.inDevelopment = true;
		gameScript.developerID = this.mS_.myID;
		gameScript.devS_ = this.mS_.myPubS_;
		gameScript.typ_contractGame = this.typ_contractGame;
		if (!this.typ_contractGame)
		{
			gameScript.SetMyName(this.uiObjects[0].GetComponent<InputField>().text);
			gameScript.beschreibung = this.g_Beschreibung;
			gameScript.typ_standard = this.typ_standard;
			gameScript.typ_nachfolger = this.typ_nachfolger;
			gameScript.typ_spinoff = this.typ_spinoff;
			gameScript.typ_remaster = this.typ_remaster;
			gameScript.typ_addon = this.typ_addon;
			gameScript.typ_bundle = this.typ_bundle;
			gameScript.typ_budget = this.typ_budget;
			gameScript.typ_addonStandalone = false;
			gameScript.typ_mmoaddon = false;
			if (this.uiObjects[146].GetComponent<Dropdown>().value == 1)
			{
				gameScript.exklusiv = true;
			}
			if (this.uiObjects[146].GetComponent<Dropdown>().value == 2)
			{
				gameScript.herstellerExklusiv = true;
			}
			if (this.uiObjects[146].GetComponent<Dropdown>().value == 3)
			{
				gameScript.retro = true;
			}
			if (this.uiObjects[146].GetComponent<Dropdown>().value == 4)
			{
				gameScript.arcade = true;
			}
			if (this.uiObjects[146].GetComponent<Dropdown>().value == 5)
			{
				gameScript.handy = true;
			}
			gameScript.mainIP = this.g_mainIP;
			if (gameScript.mainIP == -1)
			{
				gameScript.mainIP = gameScript.myID;
				if (!this.mS_.settings_RandomEventsOff)
				{
					if (!gameScript.arcade && UnityEngine.Random.Range(0, 100) < this.mS_.difficulty && this.mS_.difficulty >= 3)
					{
						if (!this.mS_.lastGameCommercialFlop)
						{
							gameScript.commercialFlop = true;
							this.mS_.lastGameCommercialFlop = true;
						}
						else
						{
							this.mS_.lastGameCommercialFlop = false;
						}
					}
					if (!gameScript.commercialFlop && !gameScript.handy && !gameScript.typ_addon && !gameScript.typ_budget && !gameScript.typ_bundleAddon && !gameScript.typ_contractGame && !gameScript.typ_goty && !gameScript.typ_mmoaddon && UnityEngine.Random.Range(0, 100) == 1)
					{
						gameScript.commercialHit = true;
					}
				}
			}
		}
		if (!gameScript.typ_contractGame)
		{
			if (this.games_.IsNewGenreCombination(this.g_GameMainGenre, this.g_GameSubGenre))
			{
				gameScript.newGenreCombination = true;
			}
			if (this.games_.IsNewTopicCombination(this.g_GameMainTheme, this.g_GameSubTheme))
			{
				gameScript.newTopicCombination = true;
			}
		}
		if (!this.typ_contractGame)
		{
			gameScript.originalIP = this.g_originalIP;
			gameScript.portID = this.g_portIP;
			this.games_.SetPortFlags(gameScript);
			gameScript.teile = this.g_teil;
		}
		gameScript.gameTyp = this.g_GameTyp;
		gameScript.gameSize = this.g_GameSize;
		gameScript.gameZielgruppe = this.g_GameZielgruppe;
		gameScript.maingenre = this.g_GameMainGenre;
		gameScript.subgenre = this.g_GameSubGenre;
		gameScript.gameMainTheme = this.g_GameMainTheme;
		gameScript.gameSubTheme = this.g_GameSubTheme;
		gameScript.gameLicence = this.g_GameLicence;
		gameScript.engineID = this.g_GameEngine;
		gameScript.gameCopyProtect = this.g_GameCopyProtect;
		gameScript.gameAntiCheat = this.g_GameAntiCheat;
		if (this.g_GameLicence != -1 && gameScript.portID == -1 && !gameScript.typ_addon && !gameScript.typ_mmoaddon)
		{
			if (this.licences_.licence_GEKAUFT[this.g_GameLicence] > 0)
			{
				this.licences_.licence_GEKAUFT[this.g_GameLicence]--;
			}
			else
			{
				gameScript.gameLicence = -1;
			}
		}
		for (int m = 0; m < this.g_Designschwerpunkt.Length; m++)
		{
			gameScript.Designschwerpunkt[m] = this.g_Designschwerpunkt[m];
		}
		for (int n = 0; n < this.g_Designausrichtung.Length; n++)
		{
			gameScript.Designausrichtung[n] = this.g_Designausrichtung[n];
		}
		gameScript.gameAP_Gameplay = this.g_GameAP_Gameplay;
		gameScript.gameAP_Grafik = this.g_GameAP_Grafik;
		gameScript.gameAP_Sound = this.g_GameAP_Sound;
		gameScript.gameAP_Technik = this.g_GameAP_Technik;
		gameScript.finanzierung_Grundkosten = this.g_finanzierung_Grundkosten;
		gameScript.finanzierung_Technology = this.g_finanzierung_Technology;
		gameScript.finanzierung_Kontent = this.g_finanzierung_Kontent;
		for (int num2 = 0; num2 < this.g_InAppPurchase.Length; num2++)
		{
			gameScript.inAppPurchase[num2] = this.g_InAppPurchase[num2];
		}
		this.SetMGSR_Result(gameScript, this.g_Designausrichtung[1]);
		for (int num3 = 0; num3 < this.g_GameLanguage.Length; num3++)
		{
			gameScript.gameLanguage[num3] = this.g_GameLanguage[num3];
		}
		for (int num4 = 0; num4 < this.g_GameGameplayFeatures.Length; num4++)
		{
			gameScript.gameGameplayFeatures[num4] = this.g_GameGameplayFeatures[num4];
		}
		for (int num5 = 0; num5 < this.g_GamePlatform.Length; num5++)
		{
			gameScript.gamePlatform[num5] = this.g_GamePlatform[num5];
		}
		for (int num6 = 0; num6 < this.g_GameEngineFeature.Length; num6++)
		{
			gameScript.gameEngineFeature[num6] = this.g_GameEngineFeature[num6];
		}
		if (this.typ_contractGame)
		{
			gameScript.auftragsspiel = false;
			gameScript.points_gameplay = 0f;
			gameScript.points_grafik = 0f;
			gameScript.points_sound = 0f;
			gameScript.points_technik = 0f;
			gameScript.points_bugs = 0f;
			gameScript.points_bugsInvis = 0f;
			gameScript.hype = 0f;
			gameScript.costs_mitarbeiter = 0L;
			gameScript.costs_marketing = 0L;
			gameScript.costs_enginegebuehren = 0L;
			gameScript.costs_server = 0L;
			gameScript.costs_production = 0L;
			gameScript.costs_updates = 0L;
			for (int num7 = 0; num7 < gameScript.gameplayStudio.Length; num7++)
			{
				gameScript.gameplayStudio[num7] = false;
			}
			for (int num8 = 0; num8 < gameScript.grafikStudio.Length; num8++)
			{
				gameScript.grafikStudio[num8] = false;
			}
			for (int num9 = 0; num9 < gameScript.soundStudio.Length; num9++)
			{
				gameScript.soundStudio[num9] = false;
			}
			for (int num10 = 0; num10 < gameScript.motionCaptureStudio.Length; num10++)
			{
				gameScript.motionCaptureStudio[num10] = false;
			}
			for (int num11 = 0; num11 < gameScript.gameplayFeatures_DevDone.Length; num11++)
			{
				gameScript.gameplayFeatures_DevDone[num11] = false;
			}
			for (int num12 = 0; num12 < gameScript.engineFeature_DevDone.Length; num12++)
			{
				gameScript.engineFeature_DevDone[num12] = false;
			}
		}
		gameScript.devPointsStart_Gesamt = (float)gameScript.GetGesamtDevPoints();
		gameScript.devPoints_Gesamt = gameScript.devPointsStart_Gesamt;
		gameScript.FindNextFeatureForDevelopment();
		taskGame taskGame = this.guiMain_.AddTask_Game();
		taskGame.Init(false);
		taskGame.gameID = gameScript.myID;
		if (this.g_leitenderDesigner)
		{
			taskGame.leitenderDesignerID = this.g_leitenderDesigner.myID;
			taskGame.designer_ = this.g_leitenderDesigner;
		}
		this.rS_.taskID = taskGame.myID;
		if (this.typ_contractGame && this.contractAuftragsspiel_)
		{
			this.guiMain_.uiObjects[100].SetActive(true);
			this.guiMain_.uiObjects[100].GetComponent<Menu_Dev_AuftragsSpielGehalt>().Init(this.contractAuftragsspiel_);
		}
		if (!gameScript.typ_contractGame)
		{
			if (gameScript.portID == -1)
			{
				if (this.typ_nachfolger)
				{
					this.guiMain_.OpenMenu(false);
					this.guiMain_.uiObjects[233].SetActive(true);
					this.guiMain_.uiObjects[233].GetComponent<Menu_Dev_NachfolgerHype>().Init(gameScript);
				}
				if (this.typ_spinoff)
				{
					this.guiMain_.OpenMenu(false);
					this.guiMain_.uiObjects[311].SetActive(true);
					this.guiMain_.uiObjects[311].GetComponent<Menu_Dev_SpinoffHype>().Init(gameScript);
				}
			}
			else
			{
				this.guiMain_.OpenMenu(false);
				this.guiMain_.uiObjects[314].SetActive(true);
				this.guiMain_.uiObjects[314].GetComponent<Menu_Dev_PortHype>().Init(gameScript);
			}
		}
		this.DeleteAllDatas();
		this.ResetDesignSettings();
		if (!this.mS_.settings_TutorialOff)
		{
			this.guiMain_.SetTutorialStep(12);
		}
		if (this.guiMain_.uiObjects[312].activeSelf)
		{
			this.guiMain_.uiObjects[312].SetActive(false);
		}
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	
	public void DeleteAllDatas()
	{
		Debug.Log("GameConcept: DeleteAllDatas()");
		this.typ_standard = false;
		this.typ_nachfolger = false;
		this.typ_remaster = false;
		this.typ_contractGame = false;
		this.typ_addon = false;
		this.typ_bundle = false;
		this.typ_budget = false;
		this.typ_addonStandalone = false;
		this.typ_mmoaddon = false;
		this.typ_spinoff = false;
		this.g_Beschreibung = "";
		this.g_leitenderDesigner = null;
		this.g_GameTyp = 0;
		this.g_GameSize = 0;
		this.g_GameZielgruppe = 4;
		this.g_GameMainGenre = -1;
		this.g_GameSubGenre = -1;
		this.g_GameMainTheme = -1;
		this.g_GameSubTheme = -1;
		this.g_GameLicence = -1;
		this.g_GameEngine = 0;
		this.g_mainIP = -1;
		this.g_originalIP = -1;
		this.g_portIP = -1;
		this.g_teil = 1;
		this.g_GameCopyProtect = -1;
		this.g_GameAntiCheat = -1;
		this.g_GameAP_Gameplay = 0;
		this.g_GameAP_Grafik = 0;
		this.g_GameAP_Sound = 0;
		this.g_GameAP_Technik = 0;
		for (int i = 0; i < this.g_Designschwerpunkt.Length; i++)
		{
			this.g_Designschwerpunkt[i] = 5;
		}
		for (int j = 0; j < this.g_Designausrichtung.Length; j++)
		{
			this.g_Designausrichtung[j] = 5;
		}
		for (int k = 0; k < this.g_GameLanguage.Length; k++)
		{
			this.g_GameLanguage[k] = false;
		}
		for (int l = 0; l < this.g_GameGameplayFeatures.Length; l++)
		{
			this.g_GameGameplayFeatures[l] = false;
		}
		for (int m = 0; m < this.g_GamePlatform.Length; m++)
		{
			this.g_GamePlatform[m] = -1;
		}
		for (int n = 0; n < this.g_GameEngineFeature.Length; n++)
		{
			this.g_GameEngineFeature[n] = -1;
		}
		for (int num = 0; num < this.g_InAppPurchase.Length; num++)
		{
			this.g_InAppPurchase[num] = false;
		}
		this.uiObjects[0].GetComponent<InputField>().text = "";
	}

	
	public void BUTTON_Abbrechen()
	{
		if (this.typ_spinoff)
		{
			this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[57]);
			this.DeleteAllDatas();
		}
		if (this.typ_nachfolger)
		{
			if (this.g_originalIP != -1)
			{
				GameObject gameObject = GameObject.Find("GAME_" + this.g_originalIP.ToString());
				if (gameObject)
				{
					gameObject.GetComponent<gameScript>().nachfolger_created = false;
				}
			}
			this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[57]);
			this.DeleteAllDatas();
		}
		if (this.typ_remaster)
		{
			if (this.g_originalIP != -1)
			{
				GameObject gameObject2 = GameObject.Find("GAME_" + this.g_originalIP.ToString());
				if (gameObject2)
				{
					gameObject2.GetComponent<gameScript>().remaster_created = false;
				}
			}
			this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[57]);
			this.DeleteAllDatas();
		}
		if (this.typ_contractGame)
		{
			this.contractAuftragsspiel_;
			this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[99]);
			this.guiMain_.uiObjects[99].GetComponent<Menu_Dev_Auftragsspiel>().Init(this.rS_);
			this.DeleteAllDatas();
		}
		if (this.typ_standard)
		{
			this.guiMain_.uiObjects[57].SetActive(true);
		}
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_AllGameplayFeatures()
	{
		this.allFeatures = !this.allFeatures;
		if (!this.allFeatures)
		{
			this.DisableAllGameplayFeatures();
			return;
		}
		for (int i = 0; i < this.uiObjects[120].transform.childCount; i++)
		{
			GameObject gameObject = this.uiObjects[120].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				gameObject.GetComponent<Item_DevGame_GameplayFeature>().BUTTON_Click();
			}
		}
	}

	
	public void BUTTON_AllPassendenGameplayFeatures()
	{
		if (this.g_GameMainGenre < 0)
		{
			return;
		}
		this.allFeatures = !this.allFeatures;
		if (!this.allFeatures)
		{
			this.DisableAllGameplayFeatures();
			return;
		}
		for (int i = 0; i < this.uiObjects[120].transform.childCount; i++)
		{
			GameObject gameObject = this.uiObjects[120].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				Item_DevGame_GameplayFeature component = gameObject.GetComponent<Item_DevGame_GameplayFeature>();
				if (this.gF_.gameplayFeatures_GOOD[component.myID, this.g_GameMainGenre] || !this.gF_.gameplayFeatures_BAD[component.myID, this.g_GameMainGenre])
				{
					component.BUTTON_Click();
				}
			}
		}
	}

	
	public void DisableAllGameplayFeatures()
	{
		for (int i = 0; i < this.g_GameGameplayFeatures.Length; i++)
		{
			this.g_GameGameplayFeatures[i] = false;
		}
		this.CalcDevCosts();
		this.GetGesamtDevPoints();
		this.UpdateGesamtGameplayFeatures();
		this.sfx_.PlaySound(3, true);
	}

	
	public void BUTTON_RandomGameName()
	{
		this.sfx_.PlaySound(3, true);
		this.uiObjects[0].GetComponent<InputField>().text = this.tS_.GetRandomGameName();
	}

	
	public void BUTTON_LeitenderEntwickler()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[196]);
		this.guiMain_.uiObjects[196].GetComponent<Menu_Dev_LeitenderDesigner>().Init(this.rS_);
	}

	
	public void BUTTON_GameTyp()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[58]);
	}

	
	public void BUTTON_GameSize()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[59]);
	}

	
	public void BUTTON_Zielgruppe()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[60]);
	}

	
	public void BUTTON_Genre(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[61]);
		this.guiMain_.uiObjects[61].GetComponent<Menu_DevGame_Genre>().Init(i);
	}

	
	public void BUTTON_Thema(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[62]);
		this.guiMain_.uiObjects[62].GetComponent<Menu_DevGame_Theme>().Init(i);
	}

	
	public void BUTTON_Platform(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[66]);
		this.guiMain_.uiObjects[66].GetComponent<Menu_DevGame_Platform>().Init(i);
	}

	
	public void BUTTON_EngineFeature(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[67]);
		this.guiMain_.uiObjects[67].GetComponent<Menu_DevGame_EngineFeature>().Init(i);
	}

	
	public void BUTTON_Lizenz()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[63]);
	}

	
	public void BUTTON_Engine()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[65]);
	}

	
	public void BUTTON_EngineKaufen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[42]);
	}

	
	public void BUTTON_PlatformKaufen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[33]);
	}

	
	public void BUTTON_LizenzKaufen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[51]);
	}

	
	public void BUTTON_CopyProtect()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[68]);
	}

	
	public void BUTTON_CopyProtectKaufen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[49]);
	}

	
	public void BUTTON_AntiCheat()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[236]);
	}

	
	public void BUTTON_AntiCheatKaufen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[234]);
	}

	
	public void BUTTON_AlleSprachen()
	{
		this.sfx_.PlaySound(3, true);
		bool flag = this.g_GameLanguage[0];
		for (int i = 0; i < this.g_GameLanguage.Length; i++)
		{
			this.g_GameLanguage[i] = flag;
			this.SetLanguage(i);
		}
	}

	
	public void DROPDOWN_PlattformTyp()
	{
		if (this.uiObjects[146].GetComponent<Dropdown>().value == 1 || this.uiObjects[146].GetComponent<Dropdown>().value == 4)
		{
			this.SetPlatform(1, -1);
			this.SetPlatform(2, -1);
			this.SetPlatform(3, -1);
			this.uiObjects[33].GetComponent<Button>().interactable = false;
			this.uiObjects[34].GetComponent<Button>().interactable = false;
			this.uiObjects[35].GetComponent<Button>().interactable = false;
		}
		else
		{
			this.uiObjects[33].GetComponent<Button>().interactable = true;
			this.uiObjects[34].GetComponent<Button>().interactable = true;
			this.uiObjects[35].GetComponent<Button>().interactable = true;
			this.Unlock(28, this.uiObjects[36], this.uiObjects[33]);
			this.Unlock(29, this.uiObjects[37], this.uiObjects[34]);
			this.Unlock(30, this.uiObjects[38], this.uiObjects[35]);
		}
		if (this.uiObjects[146].GetComponent<Dropdown>().value == 0 || this.uiObjects[146].GetComponent<Dropdown>().value == 1 || this.uiObjects[146].GetComponent<Dropdown>().value == 2)
		{
			for (int i = 0; i < this.g_GamePlatform.Length; i++)
			{
				if (this.g_GamePlatform[i] != -1)
				{
					GameObject gameObject = GameObject.Find("PLATFORM_" + this.g_GamePlatform[i].ToString());
					if (gameObject && (gameObject.GetComponent<platformScript>().typ == 3 || gameObject.GetComponent<platformScript>().typ == 4))
					{
						this.SetPlatform(i, -1);
					}
				}
			}
			this.CalcDevCosts();
			return;
		}
		if (this.uiObjects[146].GetComponent<Dropdown>().value == 3)
		{
			for (int j = 0; j < this.g_GamePlatform.Length; j++)
			{
				if (this.g_GamePlatform[j] != -1)
				{
					GameObject gameObject2 = GameObject.Find("PLATFORM_" + this.g_GamePlatform[j].ToString());
					if (gameObject2)
					{
						if (!gameObject2.GetComponent<platformScript>().vomMarktGenommen)
						{
							this.SetPlatform(j, -1);
						}
						if (gameObject2.GetComponent<platformScript>().typ == 3 || gameObject2.GetComponent<platformScript>().typ == 4)
						{
							this.SetPlatform(j, -1);
						}
					}
				}
			}
			this.CalcDevCosts();
			return;
		}
		if (this.uiObjects[146].GetComponent<Dropdown>().value == 5)
		{
			for (int k = 0; k < this.g_GamePlatform.Length; k++)
			{
				if (this.g_GamePlatform[k] != -1)
				{
					GameObject gameObject3 = GameObject.Find("PLATFORM_" + this.g_GamePlatform[k].ToString());
					if (gameObject3 && gameObject3.GetComponent<platformScript>().typ != 3)
					{
						this.SetPlatform(k, -1);
					}
				}
			}
			this.CalcDevCosts();
			return;
		}
		if (this.uiObjects[146].GetComponent<Dropdown>().value == 4)
		{
			for (int l = 0; l < this.g_GamePlatform.Length; l++)
			{
				if (this.g_GamePlatform[l] != -1)
				{
					GameObject gameObject4 = GameObject.Find("PLATFORM_" + this.g_GamePlatform[l].ToString());
					if (gameObject4 && gameObject4.GetComponent<platformScript>().typ != 4)
					{
						this.SetPlatform(l, -1);
					}
				}
			}
			this.CalcDevCosts();
			return;
		}
	}

	
	public int GetEngineTechLevel()
	{
		if (this.g_GameEngineScript_)
		{
			return this.g_GameEngineScript_.GetTechLevel();
		}
		return 0;
	}

	
	public void SetEngine(int i)
	{
		this.g_GameEngine = i;
		GameObject gameObject = GameObject.Find("ENGINE_" + i.ToString());
		if (gameObject)
		{
			this.mS_.lastUsedEngine = i;
			engineScript component = gameObject.GetComponent<engineScript>();
			this.g_GameEngineScript_ = component;
			this.uiObjects[63].GetComponent<Text>().text = component.GetName();
			this.uiObjects[64].GetComponent<Text>().text = component.GetTechLevel().ToString();
			this.uiObjects[216].GetComponent<Text>().text = component.GetTechLevel().ToString();
			if (component.ownerID != this.mS_.myID)
			{
				this.uiObjects[65].GetComponent<Text>().text = this.tS_.GetText(260) + ": " + component.gewinnbeteiligung.ToString() + "%";
			}
			else
			{
				this.uiObjects[65].GetComponent<Text>().text = this.tS_.GetText(262);
			}
			this.uiObjects[66].GetComponent<Image>().sprite = this.genres_.GetPic(component.spezialgenre);
			component.SetSpezialPlatformSprite(this.uiObjects[179]);
			this.SetEngineFeature(this.eF_.GetTypGrafik(), this.g_GameEngineScript_.GetBestFeature(this.eF_.GetTypGrafik()));
			this.SetEngineFeature(this.eF_.GetTypSound(), this.g_GameEngineScript_.GetBestFeature(this.eF_.GetTypSound()));
			this.SetEngineFeature(this.eF_.GetTypKI(), this.g_GameEngineScript_.GetBestFeature(this.eF_.GetTypKI()));
			this.SetEngineFeature(this.eF_.GetTypPhysik(), this.g_GameEngineScript_.GetBestFeature(this.eF_.GetTypPhysik()));
		}
		else
		{
			this.g_GameEngineScript_ = null;
		}
		this.CalcDevCosts();
	}

	
	public void SetEngineFeature(int featureArt_, int featureNr_)
	{
		if (featureNr_ >= 0)
		{
			this.uiObjects[68 + featureArt_].GetComponent<Text>().text = this.eF_.GetName(featureNr_);
			this.guiMain_.DrawStars(this.uiObjects[72 + featureArt_], this.eF_.engineFeatures_LEVEL[featureNr_]);
			this.uiObjects[76 + featureArt_].GetComponent<Text>().text = this.eF_.engineFeatures_TECH[featureNr_].ToString();
			this.uiObjects[80 + featureArt_].GetComponent<tooltip>().c = this.eF_.GetTooltip(featureNr_);
			this.g_GameEngineFeature[featureArt_] = featureNr_;
		}
		this.GetGesamtDevPoints();
		this.CalcDevCosts();
	}

	
	public void SetBeschreibung(string c)
	{
		this.g_Beschreibung = c;
	}

	
	public void SetGameTyp(int i)
	{
		this.g_GameTyp = i;
		this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetGameTyp(i);
		this.uiObjects[149].GetComponent<Image>().sprite = this.GetTypSprite();
		this.CalcDevCosts();
	}

	
	public void SetGameSize(int i)
	{
		this.g_GameSize = i;
		this.uiObjects[3].GetComponent<Text>().text = this.tS_.GetGameSize(i);
		this.CalcDevCosts();
		this.UpdateGesamtGameplayFeatures();
	}

	
	public void SetZielgruppe(int i)
	{
		this.g_GameZielgruppe = i;
		this.uiObjects[4].GetComponent<Text>().text = this.tS_.GetGameZielgruppe(i);
	}

	
	private void SetAutomaticBestAntiCheat()
	{
		if (this.uiObjects[146].GetComponent<Dropdown>().value == 4)
		{
			this.SetAntiCheat(-1);
			return;
		}
		if (this.g_GameAntiCheat == -1)
		{
			float num = 0f;
			int num2 = -1;
			GameObject[] array = GameObject.FindGameObjectsWithTag("AntiCheat");
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i])
				{
					antiCheatScript component = array[i].GetComponent<antiCheatScript>();
					if (component && component.inBesitz && component.effekt > num)
					{
						num2 = component.myID;
						num = component.effekt;
					}
				}
			}
			if (num2 != -1)
			{
				this.SetAntiCheat(num2);
			}
		}
	}

	
	public void SetAntiCheat(int i)
	{
		this.g_GameAntiCheat = i;
		if (i >= 0)
		{
			GameObject gameObject = GameObject.Find("ANTICHEAT_" + i.ToString());
			if (gameObject)
			{
				antiCheatScript component = gameObject.GetComponent<antiCheatScript>();
				this.g_GameAntiCheatScript_ = component;
				this.uiObjects[175].GetComponent<Text>().text = component.GetName();
				this.uiObjects[176].GetComponent<Text>().text = this.mS_.GetMoney((long)component.GetDevCosts(), true);
				this.uiObjects[177].GetComponent<Image>().fillAmount = component.effekt * 0.01f;
				this.uiObjects[178].GetComponent<Text>().text = this.mS_.Round(component.effekt, 2) + "%";
				this.uiObjects[177].GetComponent<Image>().color = this.GetValColor(component.effekt);
			}
		}
		else
		{
			this.g_GameAntiCheatScript_ = null;
			this.uiObjects[175].GetComponent<Text>().text = this.tS_.GetText(1213);
			this.uiObjects[176].GetComponent<Text>().text = "";
			this.uiObjects[177].GetComponent<Image>().fillAmount = 0f;
			this.uiObjects[178].GetComponent<Text>().text = "0.0%";
			this.uiObjects[177].GetComponent<Image>().color = this.GetValColor(0f);
		}
		this.CalcDevCosts();
	}

	
	private void SetAutomaticBestCopyProtect()
	{
		if (this.uiObjects[146].GetComponent<Dropdown>().value == 4)
		{
			this.SetCopyProtect(-1);
			return;
		}
		if (this.g_GameCopyProtect == -1)
		{
			float num = 0f;
			int num2 = -1;
			GameObject[] array = GameObject.FindGameObjectsWithTag("CopyProtect");
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i])
				{
					copyProtectScript component = array[i].GetComponent<copyProtectScript>();
					if (component && component.inBesitz && component.effekt > num)
					{
						num2 = component.myID;
						num = component.effekt;
					}
				}
			}
			if (num2 != -1)
			{
				this.SetCopyProtect(num2);
			}
		}
	}

	
	public void SetCopyProtect(int i)
	{
		this.g_GameCopyProtect = i;
		if (i >= 0)
		{
			GameObject gameObject = GameObject.Find("COPYPROTECT_" + i.ToString());
			if (gameObject)
			{
				copyProtectScript component = gameObject.GetComponent<copyProtectScript>();
				this.g_GameCopyProtectScript_ = component;
				this.uiObjects[87].GetComponent<Text>().text = component.GetName();
				this.uiObjects[88].GetComponent<Text>().text = this.mS_.GetMoney((long)component.GetDevCosts(), true);
				this.uiObjects[89].GetComponent<Image>().fillAmount = component.effekt * 0.01f;
				this.uiObjects[90].GetComponent<Text>().text = this.mS_.Round(component.effekt, 2) + "%";
				this.uiObjects[89].GetComponent<Image>().color = this.GetValColor(component.effekt);
			}
		}
		else
		{
			this.g_GameCopyProtectScript_ = null;
			this.uiObjects[87].GetComponent<Text>().text = this.tS_.GetText(383);
			this.uiObjects[88].GetComponent<Text>().text = "";
			this.uiObjects[89].GetComponent<Image>().fillAmount = 0f;
			this.uiObjects[90].GetComponent<Text>().text = "0.0%";
			this.uiObjects[89].GetComponent<Image>().color = this.GetValColor(0f);
		}
		this.CalcDevCosts();
	}

	
	public void SetMainGenre(int i)
	{
		this.g_GameMainGenre = i;
		if (i >= 0)
		{
			this.uiObjects[5].GetComponent<Text>().text = this.genres_.GetName(i);
			this.guiMain_.DrawStars(this.uiObjects[7], this.genres_.genres_LEVEL[i]);
			this.uiObjects[9].GetComponent<Image>().sprite = this.genres_.GetPic(i);
			this.uiObjects[11].GetComponent<Image>().sprite = this.guiMain_.uiSprites[1];
			this.uiObjects[9].GetComponent<tooltip>().c = this.genres_.GetTooltip(i);
			this.uiObjects[218].GetComponent<Image>().sprite = this.genres_.GetPic(i);
			this.uiObjects[218].GetComponent<tooltip>().c = this.genres_.GetTooltip(i);
		}
		else
		{
			this.uiObjects[5].GetComponent<Text>().text = "---";
			this.guiMain_.DrawStars(this.uiObjects[7], 0);
			this.uiObjects[9].GetComponent<Image>().sprite = this.guiMain_.uiSprites[3];
			this.uiObjects[11].GetComponent<Image>().sprite = this.guiMain_.uiSprites[0];
			this.uiObjects[9].GetComponent<tooltip>().c = "";
			this.uiObjects[218].GetComponent<Image>().sprite = this.guiMain_.uiSprites[3];
			this.uiObjects[218].GetComponent<tooltip>().c = "";
		}
		if (this.games_.IsNewGenreCombination(this.g_GameMainGenre, this.g_GameSubGenre) && !this.typ_contractGame)
		{
			if (!this.uiObjects[220].activeSelf)
			{
				this.uiObjects[220].SetActive(true);
				return;
			}
		}
		else if (this.uiObjects[220].activeSelf)
		{
			this.uiObjects[220].SetActive(false);
		}
	}

	
	public void SetSubGenre(int i)
	{
		if (this.typ_contractGame && i >= 0 && this.g_GameMainGenre == i)
		{
			return;
		}
		this.g_GameSubGenre = i;
		if (i >= 0)
		{
			this.uiObjects[6].GetComponent<Text>().text = this.genres_.GetName(i);
			this.guiMain_.DrawStars(this.uiObjects[8], this.genres_.genres_LEVEL[i]);
			this.uiObjects[10].GetComponent<Image>().sprite = this.genres_.GetPic(i);
			this.uiObjects[10].GetComponent<tooltip>().c = this.genres_.GetTooltip(i);
			this.uiObjects[219].GetComponent<Image>().sprite = this.genres_.GetPic(i);
			this.uiObjects[219].GetComponent<tooltip>().c = this.genres_.GetTooltip(i);
		}
		else
		{
			this.uiObjects[6].GetComponent<Text>().text = "---";
			this.guiMain_.DrawStars(this.uiObjects[8], 0);
			this.uiObjects[10].GetComponent<Image>().sprite = this.guiMain_.uiSprites[3];
			this.uiObjects[10].GetComponent<tooltip>().c = "";
			this.uiObjects[219].GetComponent<Image>().sprite = this.guiMain_.uiSprites[3];
			this.uiObjects[219].GetComponent<tooltip>().c = "";
		}
		if (this.games_.IsNewGenreCombination(this.g_GameMainGenre, this.g_GameSubGenre) && !this.typ_contractGame)
		{
			if (!this.uiObjects[220].activeSelf)
			{
				this.uiObjects[220].SetActive(true);
				return;
			}
		}
		else if (this.uiObjects[220].activeSelf)
		{
			this.uiObjects[220].SetActive(false);
		}
	}

	
	public void SetMainTheme(int i)
	{
		this.g_GameMainTheme = i;
		if (i >= 0)
		{
			this.uiObjects[21].GetComponent<Text>().text = this.tS_.GetThemes(i);
			this.guiMain_.DrawStars(this.uiObjects[22], this.themes_.themes_LEVEL[i]);
			this.uiObjects[25].GetComponent<Image>().sprite = this.guiMain_.uiSprites[6];
			this.uiObjects[27].GetComponent<Image>().sprite = this.guiMain_.uiSprites[1];
		}
		else
		{
			this.uiObjects[21].GetComponent<Text>().text = "---";
			this.guiMain_.DrawStars(this.uiObjects[22], 0);
			this.uiObjects[25].GetComponent<Image>().sprite = this.guiMain_.uiSprites[3];
			this.uiObjects[27].GetComponent<Image>().sprite = this.guiMain_.uiSprites[0];
		}
		if (this.games_.IsNewTopicCombination(this.g_GameMainTheme, this.g_GameSubTheme) && !this.typ_contractGame)
		{
			if (!this.uiObjects[221].activeSelf)
			{
				this.uiObjects[221].SetActive(true);
				return;
			}
		}
		else if (this.uiObjects[221].activeSelf)
		{
			this.uiObjects[221].SetActive(false);
		}
	}

	
	public void SetSubTheme(int i)
	{
		this.g_GameSubTheme = i;
		if (i >= 0)
		{
			this.uiObjects[23].GetComponent<Text>().text = this.tS_.GetThemes(i);
			this.guiMain_.DrawStars(this.uiObjects[24], this.themes_.themes_LEVEL[i]);
			this.uiObjects[26].GetComponent<Image>().sprite = this.guiMain_.uiSprites[6];
		}
		else
		{
			this.uiObjects[23].GetComponent<Text>().text = "---";
			this.guiMain_.DrawStars(this.uiObjects[24], 0);
			this.uiObjects[26].GetComponent<Image>().sprite = this.guiMain_.uiSprites[3];
		}
		if (this.games_.IsNewTopicCombination(this.g_GameMainTheme, this.g_GameSubTheme) && !this.typ_contractGame)
		{
			if (!this.uiObjects[221].activeSelf)
			{
				this.uiObjects[221].SetActive(true);
				return;
			}
		}
		else if (this.uiObjects[221].activeSelf)
		{
			this.uiObjects[221].SetActive(false);
		}
	}

	
	public void SetLicence(int i)
	{
		this.g_GameLicence = i;
		if (i >= 0)
		{
			this.uiObjects[28].GetComponent<Text>().text = this.licences_.GetName(i);
			this.guiMain_.DrawStars(this.uiObjects[29], Mathf.RoundToInt(this.licences_.licence_QUALITY[i] / 20f));
			this.uiObjects[30].GetComponent<Image>().sprite = this.licences_.licenceSprites[this.licences_.licence_TYP[i]];
			this.uiObjects[31].GetComponent<Text>().text = this.licences_.GetTypString(i);
			return;
		}
		this.uiObjects[28].GetComponent<Text>().text = this.tS_.GetText(358);
		this.guiMain_.DrawStars(this.uiObjects[29], 0);
		this.uiObjects[30].GetComponent<Image>().sprite = this.guiMain_.uiSprites[3];
		this.uiObjects[31].GetComponent<Text>().text = "";
	}

	
	public void SetLicenceName()
	{
		if (this.g_GameLicence != -1)
		{
			this.uiObjects[0].GetComponent<InputField>().text = this.licences_.GetName(this.g_GameLicence);
		}
	}

	
	public void SetMGSR_Result(gameScript script_, int value)
	{
		this.FindScripts();
		switch (value)
		{
		case 0:
			script_.usk = 0;
			break;
		case 1:
			script_.usk = 0;
			if (UnityEngine.Random.Range(0, 100) > 70)
			{
				script_.usk = 1;
			}
			break;
		case 2:
			script_.usk = 1;
			break;
		case 3:
			script_.usk = 1;
			if (UnityEngine.Random.Range(0, 100) > 70)
			{
				script_.usk = 2;
			}
			break;
		case 4:
			script_.usk = 2;
			break;
		case 5:
			script_.usk = 2;
			if (UnityEngine.Random.Range(0, 100) > 70)
			{
				script_.usk = 3;
			}
			break;
		case 6:
			script_.usk = 3;
			break;
		case 7:
			script_.usk = 3;
			if (UnityEngine.Random.Range(0, 100) > 70)
			{
				script_.usk = 4;
			}
			break;
		case 8:
			script_.usk = 4;
			break;
		case 9:
			script_.usk = 4;
			if (UnityEngine.Random.Range(0, 100) > 80)
			{
				script_.usk = 5;
			}
			break;
		case 10:
			script_.usk = 5;
			if (UnityEngine.Random.Range(0, 100) > 80)
			{
				script_.usk = 4;
			}
			break;
		}
		if (script_.gameMainTheme != -1 && script_.usk < this.themes_.themes_MGSR[script_.gameMainTheme])
		{
			script_.usk = this.themes_.themes_MGSR[script_.gameMainTheme];
		}
		if (script_.gameSubTheme != -1 && script_.usk < this.themes_.themes_MGSR[script_.gameSubTheme])
		{
			script_.usk = this.themes_.themes_MGSR[script_.gameSubTheme];
		}
	}

	
	private void SetMGSR()
	{
		switch (this.g_Designausrichtung[1])
		{
		case 0:
			this.uiObjects[203].GetComponent<Image>().sprite = this.games_.gamePEGI[0];
			return;
		case 1:
			this.uiObjects[203].GetComponent<Image>().sprite = this.games_.gamePEGI[0];
			return;
		case 2:
			this.uiObjects[203].GetComponent<Image>().sprite = this.games_.gamePEGI[1];
			return;
		case 3:
			this.uiObjects[203].GetComponent<Image>().sprite = this.games_.gamePEGI[1];
			return;
		case 4:
			this.uiObjects[203].GetComponent<Image>().sprite = this.games_.gamePEGI[2];
			return;
		case 5:
			this.uiObjects[203].GetComponent<Image>().sprite = this.games_.gamePEGI[2];
			return;
		case 6:
			this.uiObjects[203].GetComponent<Image>().sprite = this.games_.gamePEGI[3];
			return;
		case 7:
			this.uiObjects[203].GetComponent<Image>().sprite = this.games_.gamePEGI[3];
			return;
		case 8:
			this.uiObjects[203].GetComponent<Image>().sprite = this.games_.gamePEGI[4];
			return;
		case 9:
			this.uiObjects[203].GetComponent<Image>().sprite = this.games_.gamePEGI[4];
			return;
		case 10:
			this.uiObjects[203].GetComponent<Image>().sprite = this.games_.gamePEGI[5];
			return;
		default:
			return;
		}
	}

	
	private void ResetDesignSettings()
	{
		for (int i = 0; i < this.g_Designschwerpunkt.Length; i++)
		{
			this.g_Designschwerpunkt[i] = 5;
			this.uiDesignschwerpunkte[i].transform.GetChild(1).GetComponent<Slider>().value = (float)this.g_Designschwerpunkt[i];
		}
		for (int j = 0; j < this.g_Designausrichtung.Length; j++)
		{
			this.g_Designausrichtung[j] = 5;
			this.uiDesignausrichtung[j].transform.GetChild(1).GetComponent<Slider>().value = (float)this.g_Designausrichtung[j];
		}
		this.g_GameAP_Gameplay = 5;
		this.g_GameAP_Grafik = 5;
		this.g_GameAP_Sound = 5;
		this.g_GameAP_Technik = 5;
		this.uiObjects[97].GetComponent<Slider>().value = (float)this.g_GameAP_Gameplay;
		this.uiObjects[98].GetComponent<Slider>().value = (float)this.g_GameAP_Grafik;
		this.uiObjects[99].GetComponent<Slider>().value = (float)this.g_GameAP_Sound;
		this.uiObjects[100].GetComponent<Slider>().value = (float)this.g_GameAP_Technik;
		this.SetMGSR();
		this.UpdateDesignSettings();
	}

	
	public void CopyDesignSettings(gameScript script_)
	{
		for (int i = 0; i < this.g_Designschwerpunkt.Length; i++)
		{
			this.g_Designschwerpunkt[i] = script_.Designschwerpunkt[i];
			this.uiDesignschwerpunkte[i].transform.GetChild(1).GetComponent<Slider>().value = (float)this.g_Designschwerpunkt[i];
		}
		for (int j = 0; j < this.g_Designausrichtung.Length; j++)
		{
			this.g_Designausrichtung[j] = script_.Designausrichtung[j];
			this.uiDesignausrichtung[j].transform.GetChild(1).GetComponent<Slider>().value = (float)this.g_Designausrichtung[j];
		}
		this.g_GameAP_Gameplay = script_.gameAP_Gameplay;
		this.g_GameAP_Grafik = script_.gameAP_Grafik;
		this.g_GameAP_Sound = script_.gameAP_Sound;
		this.g_GameAP_Technik = script_.gameAP_Technik;
		this.uiObjects[97].GetComponent<Slider>().value = (float)this.g_GameAP_Gameplay;
		this.uiObjects[98].GetComponent<Slider>().value = (float)this.g_GameAP_Grafik;
		this.uiObjects[99].GetComponent<Slider>().value = (float)this.g_GameAP_Sound;
		this.uiObjects[100].GetComponent<Slider>().value = (float)this.g_GameAP_Technik;
		this.SetMGSR();
		this.UpdateDesignSettings();
	}

	
	public int GetAmountDesignschwerpunkte()
	{
		int num = 40;
		for (int i = 0; i < this.g_Designschwerpunkt.Length; i++)
		{
			num -= this.g_Designschwerpunkt[i];
		}
		return num;
	}

	
	public void UpdateDesignSlider()
	{
		for (int i = 0; i < this.g_Designschwerpunkt.Length; i++)
		{
			this.uiDesignschwerpunkte[i].transform.GetChild(1).GetComponent<Slider>().value = (float)this.g_Designschwerpunkt[i];
		}
		for (int j = 0; j < this.g_Designausrichtung.Length; j++)
		{
			this.uiDesignausrichtung[j].transform.GetChild(1).GetComponent<Slider>().value = (float)this.g_Designausrichtung[j];
		}
	}

	
	public void BUTTON_AutoDesignSettings()
	{
		this.sfx_.PlaySound(3, true);
		for (int i = 0; i < this.g_Designschwerpunkt.Length; i++)
		{
			this.g_Designschwerpunkt[i] = this.genres_.GetFocus(i, this.g_GameMainGenre, this.g_GameSubGenre);
		}
		for (int j = 0; j < this.g_Designausrichtung.Length; j++)
		{
			this.uiDesignausrichtung[j].transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = this.g_Designausrichtung[j].ToString();
			this.g_Designausrichtung[j] = this.genres_.GetAlign(j, this.g_GameMainGenre, this.g_GameSubGenre);
		}
		this.UpdateDesignSettings();
		this.UpdateDesignSlider();
		genres component = this.main_.GetComponent<genres>();
		this.uiObjects[97].GetComponent<Slider>().value = component.genres_GAMEPLAY[this.g_GameMainGenre] / 5f;
		this.uiObjects[98].GetComponent<Slider>().value = component.genres_GRAPHIC[this.g_GameMainGenre] / 5f;
		this.uiObjects[99].GetComponent<Slider>().value = component.genres_SOUND[this.g_GameMainGenre] / 5f;
		this.uiObjects[100].GetComponent<Slider>().value = component.genres_CONTROL[this.g_GameMainGenre] / 5f;
		this.SetAP_Gameplay();
		this.SetAP_Grafik();
		this.SetAP_Sound();
		this.SetAP_Technik();
	}

	
	public void UpdateDesignSettings()
	{
		for (int i = 0; i < this.g_Designschwerpunkt.Length; i++)
		{
			this.uiDesignschwerpunkte[i].transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = this.g_Designschwerpunkt[i].ToString();
			if (this.g_GameMainGenre != -1 && this.genres_.GetFocusKnown(i, this.g_GameMainGenre, this.g_GameSubGenre))
			{
				Text component = this.uiDesignschwerpunkte[i].transform.GetChild(2).transform.GetChild(0).GetComponent<Text>();
				component.text = component.text + " <color=green>[" + this.genres_.GetFocus(i, this.g_GameMainGenre, this.g_GameSubGenre).ToString() + "]</color>";
			}
		}
		for (int j = 0; j < this.g_Designausrichtung.Length; j++)
		{
			this.uiDesignausrichtung[j].transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = this.g_Designausrichtung[j].ToString();
			if (this.g_GameMainGenre != -1 && this.genres_.GetAlignKnown(j, this.g_GameMainGenre, this.g_GameSubGenre))
			{
				Text component2 = this.uiDesignausrichtung[j].transform.GetChild(2).transform.GetChild(0).GetComponent<Text>();
				component2.text = component2.text + " <color=green>[" + this.genres_.GetAlign(j, this.g_GameMainGenre, this.g_GameSubGenre).ToString() + "]</color>";
			}
		}
		int amountDesignschwerpunkte = this.GetAmountDesignschwerpunkte();
		this.uiObjects[202].GetComponent<Text>().text = amountDesignschwerpunkte.ToString();
		if (amountDesignschwerpunkte < 0)
		{
			this.uiObjects[202].GetComponent<Text>().color = this.guiMain_.colors[18];
		}
		else
		{
			this.uiObjects[202].GetComponent<Text>().color = this.guiMain_.colors[6];
		}
		float num = (float)amountDesignschwerpunkte;
		this.uiObjects[201].GetComponent<Image>().fillAmount = num / 40f;
		for (int k = 0; k < this.g_Designschwerpunkt.Length; k++)
		{
			if (amountDesignschwerpunkte < 0)
			{
				this.uiDesignschwerpunkte[k].transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().color = this.guiMain_.colors[5];
			}
			else
			{
				this.uiDesignschwerpunkte[k].transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().color = this.guiMain_.colors[20];
			}
		}
		this.SetMGSR();
	}

	
	public void SLIDER_Designausrichtung(int i)
	{
		this.g_Designausrichtung[i] = Mathf.RoundToInt(this.uiDesignausrichtung[i].transform.GetChild(1).GetComponent<Slider>().value);
		this.UpdateDesignSettings();
		if (i == 1)
		{
			this.uiObjects[203].GetComponent<Animation>().Play();
		}
	}

	
	public void SLIDER_Designschwerpunkt(int i)
	{
		this.g_Designschwerpunkt[i] = Mathf.RoundToInt(this.uiDesignschwerpunkte[i].transform.GetChild(1).GetComponent<Slider>().value);
		this.UpdateDesignSettings();
	}

	
	public void SetAP_Gameplay()
	{
		this.g_GameAP_Gameplay = Mathf.RoundToInt(this.uiObjects[97].GetComponent<Slider>().value);
		this.uiObjects[101].GetComponent<Text>().text = (this.g_GameAP_Gameplay * 5).ToString() + "%";
		this.UpdateGesamtArbeitsprioritaet();
	}

	
	public void SetAP_Grafik()
	{
		this.g_GameAP_Grafik = Mathf.RoundToInt(this.uiObjects[98].GetComponent<Slider>().value);
		this.uiObjects[102].GetComponent<Text>().text = (this.g_GameAP_Grafik * 5).ToString() + "%";
		this.UpdateGesamtArbeitsprioritaet();
	}

	
	public void SetAP_Sound()
	{
		this.g_GameAP_Sound = Mathf.RoundToInt(this.uiObjects[99].GetComponent<Slider>().value);
		this.uiObjects[103].GetComponent<Text>().text = (this.g_GameAP_Sound * 5).ToString() + "%";
		this.UpdateGesamtArbeitsprioritaet();
	}

	
	public void SetAP_Technik()
	{
		this.g_GameAP_Technik = Mathf.RoundToInt(this.uiObjects[100].GetComponent<Slider>().value);
		this.uiObjects[104].GetComponent<Text>().text = (this.g_GameAP_Technik * 5).ToString() + "%";
		this.UpdateGesamtArbeitsprioritaet();
	}

	
	public void SetLanguage(int i)
	{
		this.sfx_.PlaySound(3, false);
		this.g_GameLanguage[i] = !this.g_GameLanguage[i];
		if (this.g_GameLanguage[i])
		{
			this.uiObjects[107 + i].GetComponent<Image>().color = Color.white;
		}
		else
		{
			this.uiObjects[107 + i].GetComponent<Image>().color = this.languageColor;
		}
		this.CalcDevCosts();
	}

	
	public int AnzahlLanguages()
	{
		int num = 0;
		for (int i = 0; i < this.g_GameLanguage.Length; i++)
		{
			if (this.g_GameLanguage[i])
			{
				num++;
			}
		}
		return num;
	}

	
	private int GetPlatformTechLevel(int platform_)
	{
		GameObject gameObject = GameObject.Find("PLATFORM_" + platform_.ToString());
		if (gameObject)
		{
			platformScript component = gameObject.GetComponent<platformScript>();
			if (component)
			{
				return component.tech;
			}
		}
		return 0;
	}

	
	private float GetExklusivGameBonus()
	{
		if (this.typ_contractGame)
		{
			return 0f;
		}
		if (this.uiObjects[146].GetComponent<Dropdown>().value == 1)
		{
			GameObject gameObject = GameObject.Find("PLATFORM_" + this.g_GamePlatform[0].ToString());
			if (gameObject)
			{
				platformScript component = gameObject.GetComponent<platformScript>();
				if (component)
				{
					return component.GetExklusivBonus();
				}
			}
		}
		return 0f;
	}

	
	public void SetPlatform(int slot, int platform_)
	{
		this.g_GamePlatform[slot] = platform_;
		if (platform_ >= 0)
		{
			GameObject gameObject = GameObject.Find("PLATFORM_" + platform_.ToString());
			if (gameObject)
			{
				platformScript component = gameObject.GetComponent<platformScript>();
				this.uiObjects[39 + slot].GetComponent<Text>().text = component.GetName();
				component.SetPic(this.uiObjects[43 + slot]);
				this.uiObjects[43 + slot].SetActive(true);
				this.guiMain_.DrawStars(this.uiObjects[47 + slot], component.erfahrung);
				this.uiObjects[51 + slot].GetComponent<Text>().text = component.tech.ToString();
				this.uiObjects[55 + slot].GetComponent<Text>().text = component.GetMarktanteilString();
				this.uiObjects[168 + slot].GetComponent<Image>().sprite = component.GetComplexSprite();
				this.uiObjects[205 + slot].GetComponent<Image>().sprite = component.GetTypSprite();
				this.uiObjects[205 + slot].GetComponent<tooltip>().c = component.GetTypString();
				if (component.internet)
				{
					this.uiObjects[180 + slot].SetActive(true);
				}
				else
				{
					this.uiObjects[180 + slot].SetActive(false);
				}
				this.uiObjects[43 + slot].GetComponent<tooltip>().c = component.GetTooltip();
				if (slot == 0)
				{
					this.uiObjects[59].GetComponent<Image>().sprite = this.guiMain_.uiSprites[1];
				}
			}
		}
		else
		{
			this.uiObjects[39 + slot].GetComponent<Text>().text = this.tS_.GetText(360 + slot);
			this.uiObjects[43 + slot].GetComponent<Image>().sprite = null;
			this.uiObjects[43 + slot].SetActive(false);
			this.guiMain_.DrawStars(this.uiObjects[47 + slot], 0);
			this.uiObjects[51 + slot].GetComponent<Text>().text = "-";
			this.uiObjects[55 + slot].GetComponent<Text>().text = "";
			this.uiObjects[168 + slot].GetComponent<Image>().sprite = this.platforms_.complexSprites[0];
			this.uiObjects[205 + slot].GetComponent<Image>().sprite = this.guiMain_.uiSprites[3];
			this.uiObjects[205 + slot].GetComponent<tooltip>().c = "";
			this.uiObjects[180 + slot].SetActive(false);
			if (slot == 0)
			{
				this.uiObjects[59].GetComponent<Image>().sprite = this.guiMain_.uiSprites[0];
			}
		}
		this.uiObjects[209].GetComponent<Text>().text = this.mS_.Round(this.GetGesamtMarktanteil(0), 1).ToString() + "%";
		this.uiObjects[210].GetComponent<Text>().text = this.mS_.Round(this.GetGesamtMarktanteil(1), 1).ToString() + "%";
		this.uiObjects[211].GetComponent<Text>().text = this.mS_.Round(this.GetGesamtMarktanteil(2), 1).ToString() + "%";
		this.uiObjects[212].GetComponent<Text>().text = this.mS_.Round(this.GetGesamtMarktanteil(3), 1).ToString() + "%";
		long num = 0L;
		for (int i = 0; i < this.g_GamePlatform.Length; i++)
		{
			if (this.g_GamePlatform[i] >= 0)
			{
				GameObject gameObject2 = GameObject.Find("PLATFORM_" + this.g_GamePlatform[i].ToString());
				if (gameObject2)
				{
					platformScript component2 = gameObject2.GetComponent<platformScript>();
					num += (long)component2.GetAktiveNutzer();
				}
			}
		}
		this.uiObjects[213].GetComponent<Text>().text = this.mS_.Round((float)num / 1000000f, 1).ToString() + " " + this.tS_.GetText(1483);
		if (this.uiObjects[146].GetComponent<Dropdown>().value == 4)
		{
			this.uiObjects[213].GetComponent<Text>().text = "---";
		}
		this.uiObjects[217].GetComponent<Text>().text = this.GetLowestPlatformTechLevel().ToString();
		this.CalcDevCosts();
		this.GetGesamtDevPoints();
	}

	
	public bool SetGameplayFeature(int i)
	{
		this.g_GameGameplayFeatures[i] = !this.g_GameGameplayFeatures[i];
		if (this.uiObjects[146].GetComponent<Dropdown>().value == 4 && this.gF_.gameplayFeatures_LOCKPLATFORM[i, 4])
		{
			this.g_GameGameplayFeatures[i] = false;
		}
		if (this.uiObjects[146].GetComponent<Dropdown>().value == 5 && this.gF_.gameplayFeatures_LOCKPLATFORM[i, 3])
		{
			this.g_GameGameplayFeatures[i] = false;
		}
		this.CalcDevCosts();
		this.GetGesamtDevPoints();
		this.UpdateGesamtGameplayFeatures();
		return this.g_GameGameplayFeatures[i];
	}

	
	public bool DisableGameplayFeature(int i)
	{
		this.g_GameGameplayFeatures[i] = false;
		this.CalcDevCosts();
		this.GetGesamtDevPoints();
		this.UpdateGesamtGameplayFeatures();
		return this.g_GameGameplayFeatures[i];
	}

	
	private int UpdateGesamtGameplayFeatures()
	{
		int num = 0;
		for (int i = 0; i < this.g_GameGameplayFeatures.Length; i++)
		{
			if (this.g_GameGameplayFeatures[i])
			{
				num++;
			}
		}
		if (this.g_GameSize < 4)
		{
			this.uiObjects[122].GetComponent<Text>().text = string.Concat(new string[]
			{
				this.tS_.GetText(410),
				": ",
				num.ToString(),
				" / ",
				this.maxFeatures_gameSize[this.g_GameSize].ToString()
			});
		}
		else
		{
			this.uiObjects[122].GetComponent<Text>().text = this.tS_.GetText(410) + ": " + num.ToString();
		}
		if (num > this.maxFeatures_gameSize[this.g_GameSize])
		{
			this.uiObjects[122].GetComponent<Text>().color = Color.red;
		}
		else
		{
			this.uiObjects[122].GetComponent<Text>().color = Color.black;
		}
		return num;
	}

	
	private int UpdateGesamtArbeitsprioritaet()
	{
		float num = this.uiObjects[97].GetComponent<Slider>().value;
		num += this.uiObjects[98].GetComponent<Slider>().value;
		num += this.uiObjects[99].GetComponent<Slider>().value;
		num += this.uiObjects[100].GetComponent<Slider>().value;
		num *= 5f;
		this.uiObjects[106].GetComponent<Text>().text = Mathf.RoundToInt(num).ToString() + "%";
		if (Mathf.RoundToInt(num) > 100)
		{
			this.uiObjects[106].GetComponent<Text>().color = Color.red;
		}
		else
		{
			this.uiObjects[106].GetComponent<Text>().color = this.guiMain_.colors[6];
		}
		float num2 = num;
		num2 *= 0.01f;
		if (num2 > 1f)
		{
			num2 = 1f;
		}
		this.uiObjects[105].GetComponent<Image>().fillAmount = num2;
		return Mathf.RoundToInt(num);
	}

	
	public float GetGesamtMarktanteil(int platformTyp)
	{
		this.FindScripts();
		float num = 0f;
		for (int i = 0; i < this.g_GamePlatform.Length; i++)
		{
			if (this.g_GamePlatform[i] != -1)
			{
				GameObject gameObject = GameObject.Find("PLATFORM_" + this.g_GamePlatform[i].ToString());
				if (gameObject)
				{
					platformScript component = gameObject.GetComponent<platformScript>();
					if (component.typ == platformTyp)
					{
						num += component.GetMarktanteil();
					}
				}
			}
		}
		return num;
	}

	
	private float GetPreisnachlass()
	{
		float num = 0f;
		if (this.g_portIP != -1)
		{
			num += 0.15f;
		}
		if (this.typ_remaster)
		{
			num += 0.2f;
		}
		if (this.uiObjects[146].GetComponent<Dropdown>().value == 5)
		{
			num += 0.25f;
		}
		num += this.GetExklusivGameBonus() * 0.01f;
		if (num > 0.8f)
		{
			num = 0.8f;
		}
		return num;
	}

	
	private int CalcDevCosts_Grundkosten()
	{
		int num = 0;
		if (this.g_GameTyp != 0)
		{
			num += this.costs_gameTyp[this.g_GameTyp] * (this.mS_.difficulty + 1);
		}
		else
		{
			num += this.costs_gameTyp[this.g_GameTyp];
		}
		if (this.g_GameSize != 0)
		{
			num += this.costs_gameSize[this.g_GameSize] * (this.mS_.difficulty + 1);
		}
		else
		{
			num += this.costs_gameSize[this.g_GameSize];
		}
		return Mathf.RoundToInt((float)num * (1f - this.GetPreisnachlass()) * (this.uiObjects[159].GetComponent<Slider>().value * 0.01f));
	}

	
	private int CalcDevCosts_Technology()
	{
		int num = 0;
		if (this.g_GameEngineScript_)
		{
			if (this.g_GameEngineFeature[0] >= 0)
			{
				num += this.eF_.GetDevCosts(this.g_GameEngineFeature[0]);
			}
			if (this.g_GameEngineFeature[1] >= 0)
			{
				num += this.eF_.GetDevCosts(this.g_GameEngineFeature[1]);
			}
			if (this.g_GameEngineFeature[2] >= 0)
			{
				num += this.eF_.GetDevCosts(this.g_GameEngineFeature[2]);
			}
			if (this.g_GameEngineFeature[3] >= 0)
			{
				num += this.eF_.GetDevCosts(this.g_GameEngineFeature[3]);
			}
		}
		return Mathf.RoundToInt((float)num * (1f - this.GetPreisnachlass()) * (this.uiObjects[160].GetComponent<Slider>().value * 0.01f));
	}

	
	private int CalcDevCosts_Kontent()
	{
		int num = 0;
		for (int i = 0; i < this.g_GameGameplayFeatures.Length; i++)
		{
			if (this.g_GameGameplayFeatures[i])
			{
				num += this.gF_.GetDevCosts(i);
			}
		}
		return Mathf.RoundToInt((float)num * (1f - this.GetPreisnachlass()) * (this.uiObjects[161].GetComponent<Slider>().value * 0.01f));
	}

	
	private int CalcDevCosts_Sonstiges()
	{
		int num = 0;
		for (int i = 0; i < this.g_GamePlatform.Length; i++)
		{
			if (this.g_GamePlatform[i] != -1)
			{
				GameObject gameObject = GameObject.Find("PLATFORM_" + this.g_GamePlatform[i].ToString());
				if (gameObject)
				{
					num += gameObject.GetComponent<platformScript>().GetDevCosts();
				}
			}
		}
		if (this.g_GameCopyProtectScript_)
		{
			num += this.g_GameCopyProtectScript_.GetDevCosts();
		}
		if (this.g_GameAntiCheatScript_)
		{
			num += this.g_GameAntiCheatScript_.GetDevCosts();
		}
		int num2 = 0;
		for (int j = 0; j < this.g_GameLanguage.Length; j++)
		{
			if (this.g_GameLanguage[j] && !this.mS_.Muttersprache(j))
			{
				num2 += this.GetGesamtDevPoints() * 5;
				num += this.GetGesamtDevPoints() * 5;
			}
		}
		this.uiObjects[184].GetComponent<Text>().text = this.mS_.GetMoney((long)num2, true);
		if (this.typ_remaster)
		{
			num /= 2;
		}
		if (this.uiObjects[146].GetComponent<Dropdown>().value == 5)
		{
			num /= 4;
		}
		return Mathf.RoundToInt((float)num * (1f - this.GetPreisnachlass()));
	}

	
	private int CalcDevCosts()
	{
		int num = 0;
		num += this.CalcDevCosts_Grundkosten();
		num += this.CalcDevCosts_Technology();
		num += this.CalcDevCosts_Kontent();
		num += this.CalcDevCosts_Sonstiges();
		this.uiObjects[152].GetComponent<Text>().text = this.mS_.GetMoney((long)this.CalcDevCosts_Grundkosten(), true);
		this.uiObjects[153].GetComponent<Text>().text = this.mS_.GetMoney((long)this.CalcDevCosts_Technology(), true);
		this.uiObjects[154].GetComponent<Text>().text = this.mS_.GetMoney((long)this.CalcDevCosts_Kontent(), true);
		this.uiObjects[155].GetComponent<Text>().text = this.mS_.GetMoney((long)this.CalcDevCosts_Sonstiges(), true);
		float num2 = this.GetPreisnachlass() * 100f;
		if (num2 > 0f)
		{
			this.tS_.GetText(624).Replace("<NUM>", this.mS_.GetMoney((long)num, true));
			this.uiObjects[2].GetComponent<Text>().text = string.Concat(new object[]
			{
				this.mS_.GetMoney((long)num, true),
				" (",
				Mathf.RoundToInt(100f - num2),
				"%)"
			});
			return num;
		}
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)num, true);
		return num;
	}

	
	private Color GetValColor(float val)
	{
		if (val < 30f)
		{
			return this.guiMain_.colorsBalken[0];
		}
		if (val >= 30f && val < 70f)
		{
			return this.guiMain_.colorsBalken[1];
		}
		if (val >= 70f)
		{
			return this.guiMain_.colorsBalken[2];
		}
		return this.guiMain_.colorsBalken[0];
	}

	
	public void InitDropdowns_GameplayFeatures()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[119].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(6));
		list.Add(this.tS_.GetText(413));
		list.Add(this.tS_.GetText(1438));
		this.uiObjects[119].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[119].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[119].GetComponent<Dropdown>().value = @int;
	}

	
	private void Init_GameplayFeatures()
	{
		this.FindScripts();
		if (this.g_GameGameplayFeatures.Length == 0)
		{
			this.g_GameGameplayFeatures = new bool[this.gF_.gameplayFeatures_LEVEL.Length];
		}
		for (int i = 0; i < this.uiObjects[120].transform.childCount; i++)
		{
			UnityEngine.Object.Destroy(this.uiObjects[120].transform.GetChild(i).gameObject);
		}
		for (int j = 0; j < this.gF_.gameplayFeatures_LEVEL.Length; j++)
		{
			if (this.gF_.IsErforscht(j))
			{
				string text = this.gF_.GetName(j);
				this.searchStringA = this.searchStringA.ToLower();
				text = text.ToLower();
				if (this.uiObjects[204].GetComponent<InputField>().text.Length <= 0 || text.Contains(this.searchStringA))
				{
					Item_DevGame_GameplayFeature component = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[120].transform).GetComponent<Item_DevGame_GameplayFeature>();
					component.myID = j;
					component.mS_ = this.mS_;
					component.tS_ = this.tS_;
					component.sfx_ = this.sfx_;
					component.guiMain_ = this.guiMain_;
					component.gF_ = this.gF_;
					component.menuDevGame_ = this;
					component.BUTTON_Click();
					component.BUTTON_Click();
				}
			}
		}
		this.DROPDOWN_SortGameplayFeatures();
		this.guiMain_.KeinEintrag(this.uiObjects[120], this.uiObjects[121]);
	}

	
	public void DROPDOWN_SortGameplayFeatures()
	{
		int value = this.uiObjects[119].GetComponent<Dropdown>().value;
		PlayerPrefs.SetInt(this.uiObjects[119].name, value);
		int childCount = this.uiObjects[120].transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = this.uiObjects[120].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				Item_DevGame_GameplayFeature component = gameObject.GetComponent<Item_DevGame_GameplayFeature>();
				switch (value)
				{
				case 0:
					gameObject.name = this.gF_.GetName(component.myID);
					break;
				case 1:
					gameObject.name = this.gF_.GetDevCosts(component.myID).ToString();
					break;
				case 2:
					gameObject.name = this.gF_.gameplayFeatures_TYP[component.myID].ToString();
					break;
				case 3:
					gameObject.name = component.goodBad.ToString();
					break;
				}
			}
		}
		if (value == 0)
		{
			this.mS_.SortChildrenByName(this.uiObjects[120]);
			return;
		}
		this.mS_.SortChildrenByFloat(this.uiObjects[120]);
	}

	
	public void BUTTON_RemasterName(int i)
	{
		this.uiObjects[0].GetComponent<InputField>().characterLimit = 99;
		this.uiObjects[0].GetComponent<InputField>().text = this.orignal_name + " " + this.tS_.GetText(620 + i);
		this.uiObjects[135].GetComponent<Button>().interactable = true;
		this.uiObjects[136].GetComponent<Button>().interactable = true;
		this.uiObjects[137].GetComponent<Button>().interactable = true;
		this.uiObjects[138].GetComponent<Button>().interactable = true;
		this.uiObjects[135 + i].GetComponent<Button>().interactable = false;
	}

	
	public int GetGesamtDevPoints()
	{
		int num = 0;
		for (int i = 0; i < this.g_GameEngineFeature.Length; i++)
		{
			num += this.eF_.GetDevPointsForGame(this.g_GameEngineFeature[i]);
		}
		for (int j = 0; j < this.g_GameGameplayFeatures.Length; j++)
		{
			if (this.g_GameGameplayFeatures[j])
			{
				num += this.gF_.GetDevPoints(j);
			}
		}
		float num2 = 1f;
		for (int k = 0; k < this.g_GamePlatform.Length; k++)
		{
			if (this.g_GamePlatform[k] != -1)
			{
				GameObject gameObject = GameObject.Find("PLATFORM_" + this.g_GamePlatform[k].ToString());
				if (gameObject)
				{
					platformScript component = gameObject.GetComponent<platformScript>();
					if (component)
					{
						switch (component.complex)
						{
						case 0:
							num2 += 0.1f;
							break;
						case 1:
							num2 += 0.3f;
							break;
						case 2:
							num2 += 0.6f;
							break;
						}
					}
				}
			}
		}
		num2 *= (float)num;
		num = Mathf.RoundToInt(num2);
		this.uiObjects[148].GetComponent<Text>().text = this.mS_.GetMoney((long)num, false);
		return num;
	}

	
	public Sprite GetTypSprite()
	{
		if (!this.games_)
		{
			return null;
		}
		switch (this.g_GameTyp)
		{
		case 0:
			return this.games_.gameTypSprites[0];
		case 1:
			return this.games_.gameTypSprites[6];
		case 2:
			return this.games_.gameTypSprites[7];
		default:
			return null;
		}
	}

	
	public void SetLeitenderDesigner(characterScript charS_, bool manuellSelectet)
	{
		if (charS_ && charS_.roomID != this.rS_.myID)
		{
			charS_ = null;
		}
		if (!charS_)
		{
			float num = 0f;
			GameObject[] array = GameObject.FindGameObjectsWithTag("Character");
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i])
				{
					characterScript component = array[i].GetComponent<characterScript>();
					if (component && component.roomID == this.rS_.myID)
					{
						if (component.s_gamedesign > num)
						{
							num = component.s_gamedesign;
							charS_ = component;
						}
						if (this.rS_.leitenderGamedesigner == component.myID)
						{
							charS_ = component;
							break;
						}
					}
				}
			}
		}
		if (!charS_)
		{
			this.uiObjects[150].GetComponent<Text>().text = "---";
			this.g_leitenderDesigner = null;
			this.rS_.leitenderGamedesigner = -1;
			return;
		}
		this.g_leitenderDesigner = charS_;
		if (this.rS_.leitenderGamedesigner != charS_.myID)
		{
			this.rS_.leitenderGamedesigner = -1;
		}
		if (manuellSelectet)
		{
			this.rS_.leitenderGamedesigner = charS_.myID;
		}
		this.uiObjects[150].GetComponent<Text>().text = charS_.myName;
	}

	
	public void SLIDER_Finanzierung(int i)
	{
		switch (i)
		{
		case 0:
			this.uiObjects[156].GetComponent<Text>().text = Mathf.RoundToInt(this.uiObjects[159].GetComponent<Slider>().value).ToString() + "%";
			this.g_finanzierung_Grundkosten = Mathf.RoundToInt(this.uiObjects[159].GetComponent<Slider>().value);
			break;
		case 1:
			this.uiObjects[157].GetComponent<Text>().text = Mathf.RoundToInt(this.uiObjects[160].GetComponent<Slider>().value).ToString() + "%";
			this.g_finanzierung_Technology = Mathf.RoundToInt(this.uiObjects[160].GetComponent<Slider>().value);
			break;
		case 2:
			this.uiObjects[158].GetComponent<Text>().text = Mathf.RoundToInt(this.uiObjects[161].GetComponent<Slider>().value).ToString() + "%";
			this.g_finanzierung_Kontent = Mathf.RoundToInt(this.uiObjects[161].GetComponent<Slider>().value);
			break;
		}
		this.CalcDevCosts();
	}

	
	public void BUTTON_Finanzierung_Minus(int i)
	{
		if (this.typ_contractGame)
		{
			return;
		}
		this.sfx_.PlaySound(3, true);
		switch (i)
		{
		case 0:
		{
			Slider component = this.uiObjects[159].GetComponent<Slider>();
			float value = component.value;
			component.value = value - 1f;
			return;
		}
		case 1:
		{
			Slider component2 = this.uiObjects[160].GetComponent<Slider>();
			float value = component2.value;
			component2.value = value - 1f;
			return;
		}
		case 2:
		{
			Slider component3 = this.uiObjects[161].GetComponent<Slider>();
			float value = component3.value;
			component3.value = value - 1f;
			return;
		}
		default:
			return;
		}
	}

	
	public void BUTTON_Finanzierung_Plus(int i)
	{
		if (this.typ_contractGame)
		{
			return;
		}
		this.sfx_.PlaySound(3, true);
		switch (i)
		{
		case 0:
		{
			Slider component = this.uiObjects[159].GetComponent<Slider>();
			float value = component.value;
			component.value = value + 1f;
			return;
		}
		case 1:
		{
			Slider component2 = this.uiObjects[160].GetComponent<Slider>();
			float value = component2.value;
			component2.value = value + 1f;
			return;
		}
		case 2:
		{
			Slider component3 = this.uiObjects[161].GetComponent<Slider>();
			float value = component3.value;
			component3.value = value + 1f;
			return;
		}
		default:
			return;
		}
	}

	
	private float SetMaxVerdienstInApp()
	{
		float num = 0f;
		float num2 = 0f;
		for (int i = 0; i < 6; i++)
		{
			if (this.g_InAppPurchase[i])
			{
				num += this.games_.inAppPurchasePrice[i];
				num2 += this.games_.inAppPurchaseHate[i];
				this.uiObjects[193 + i].GetComponent<Image>().color = this.guiMain_.colors[20];
			}
			else
			{
				this.uiObjects[193 + i].GetComponent<Image>().color = Color.white;
			}
		}
		this.uiObjects[192].GetComponent<Text>().text = "$" + this.mS_.Round(num, 2).ToString();
		this.uiObjects[199].GetComponent<Image>().fillAmount = num2 * 0.01f;
		this.uiObjects[199].GetComponent<Image>().color = this.GetValColor(100f - num2);
		return num;
	}

	
	public void BUTTON_InAppPurchase(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.g_InAppPurchase[i] = !this.g_InAppPurchase[i];
		this.SetMaxVerdienstInApp();
	}

	
	public void BUTTON_AlleInAppPurchase()
	{
		this.sfx_.PlaySound(3, true);
		bool flag = this.g_InAppPurchase[0];
		for (int i = 0; i < 6; i++)
		{
			this.g_InAppPurchase[i] = !flag;
		}
		this.SetMaxVerdienstInApp();
	}

	
	public void BUTTON_Search()
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		for (int i = 0; i < this.uiObjects[120].transform.childCount; i++)
		{
			this.uiObjects[120].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.searchStringA = this.uiObjects[204].GetComponent<InputField>().text;
		this.Init_GameplayFeatures();
	}

	
	public bool EngineFeatureToHighTechLevel()
	{
		int num = 99;
		for (int i = 0; i < this.g_GamePlatform.Length; i++)
		{
			if (this.g_GamePlatform[i] != -1)
			{
				int platformTechLevel = this.GetPlatformTechLevel(this.g_GamePlatform[i]);
				if (platformTechLevel < num)
				{
					num = platformTechLevel;
				}
			}
		}
		Debug.Log("TECH Level: " + num);
		for (int j = 0; j < this.g_GameEngineFeature.Length; j++)
		{
			if (this.eF_.engineFeatures_TECH[this.g_GameEngineFeature[j]] > num)
			{
				return true;
			}
		}
		return false;
	}

	
	public int GetLowestPlatformTechLevel()
	{
		int num = 99;
		for (int i = 0; i < this.g_GamePlatform.Length; i++)
		{
			if (this.g_GamePlatform[i] != -1)
			{
				int platformTechLevel = this.GetPlatformTechLevel(this.g_GamePlatform[i]);
				if (platformTechLevel < num)
				{
					num = platformTechLevel;
				}
			}
		}
		if (num >= 99)
		{
			num = 1;
		}
		return num;
	}

	
	public int[] costs_gameTyp;

	
	public int[] costs_gameSize;

	
	public int[] maxFeatures_gameSize;

	
	public bool typ_standard;

	
	public bool typ_nachfolger;

	
	public bool typ_spinoff;

	
	public bool typ_remaster;

	
	public bool typ_contractGame;

	
	public bool typ_addon;

	
	public bool typ_bundle;

	
	public bool typ_budget;

	
	public bool typ_addonStandalone;

	
	public bool typ_mmoaddon;

	
	public characterScript g_leitenderDesigner;

	
	public string g_Beschreibung = "";

	
	public int g_GameTyp;

	
	public int g_GameSize;

	
	public int g_GameZielgruppe = 4;

	
	public int g_GameMainGenre;

	
	public int g_GameSubGenre;

	
	public int g_GameMainTheme;

	
	public int g_GameSubTheme;

	
	public int g_GameLicence = -1;

	
	public int g_GameEngine = -1;

	
	public engineScript g_GameEngineScript_;

	
	public int g_mainIP = -1;

	
	public int g_originalIP = -1;

	
	public int g_portIP = -1;

	
	public int g_teil = 1;

	
	public int[] g_GamePlatform;

	
	public int[] g_GameEngineFeature;

	
	public int g_GameCopyProtect = -1;

	
	public copyProtectScript g_GameCopyProtectScript_;

	
	public int g_GameAntiCheat = -1;

	
	public antiCheatScript g_GameAntiCheatScript_;

	
	public int g_GameAP_Gameplay;

	
	public int g_GameAP_Grafik;

	
	public int g_GameAP_Sound;

	
	public int g_GameAP_Technik;

	
	public bool[] g_GameLanguage;

	
	public bool[] g_GameGameplayFeatures;

	
	public int g_finanzierung_Grundkosten;

	
	public int g_finanzierung_Technology;

	
	public int g_finanzierung_Kontent;

	
	public bool[] g_InAppPurchase;

	
	public GameObject[] uiDesignschwerpunkte;

	
	public int[] g_Designschwerpunkt;

	
	public GameObject[] uiDesignausrichtung;

	
	public int[] g_Designausrichtung;

	
	private int seite;

	
	public Color languageColor;

	
	public GameObject[] uiObjects;

	
	public GameObject[] uiPrefabs;

	
	private roomScript rS_;

	
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

	
	private platforms platforms_;

	
	public const int dropdown_multiplatform = 0;

	
	public const int dropdown_exklusiv = 1;

	
	public const int dropdown_herstellerExklusiv = 2;

	
	public const int dropdown_retro = 3;

	
	public const int dropdown_arcade = 4;

	
	public const int dropdown_handy = 5;

	
	private string orignal_name = "";

	
	private gameScript contractAuftragsspiel_;

	
	private bool allFeatures;

	
	private string searchStringA = "";
}
