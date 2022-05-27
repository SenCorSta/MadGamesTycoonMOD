using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class publisherScript : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.settings_)
		{
			this.settings_ = this.main_.GetComponent<settingsScript>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
		if (!this.gF_)
		{
			this.gF_ = this.main_.GetComponent<gameplayFeatures>();
		}
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
		}
		if (!this.fS_)
		{
			this.fS_ = this.main_.GetComponent<forschungSonstiges>();
		}
		if (!this.reviewText_)
		{
			this.reviewText_ = this.main_.GetComponent<reviewText>();
		}
		if (!this.platforms_)
		{
			this.platforms_ = this.main_.GetComponent<platforms>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.menuDevGame_)
		{
			this.menuDevGame_ = this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>();
		}
		if (!this.licences_)
		{
			this.licences_ = this.main_.GetComponent<licences>();
		}
		if (!this.themes_)
		{
			this.themes_ = this.main_.GetComponent<themes>();
		}
	}

	
	public void Init()
	{
		this.FindScripts();
		base.name = "PUB_" + this.myID.ToString();
	}

	
	private bool PlayerHasNpcReplaced()
	{
		return false;
	}

	
	public void Unlock()
	{
		if (this.PlayerHasNpcReplaced())
		{
			return;
		}
		this.isUnlocked = true;
		this.lockToBuy = UnityEngine.Random.Range(12, 48);
		if (!this.genres_.genres_UNLOCK[this.fanGenre])
		{
			for (int i = 0; i < this.genres_.genres_UNLOCK.Length; i++)
			{
				if (this.genres_.genres_UNLOCK[i])
				{
					this.fanGenre = i;
					if (UnityEngine.Random.Range(0, 10) == 1)
					{
						break;
					}
				}
			}
		}
	}

	
	public string GetName()
	{
		int language = this.settings_.language;
		string text;
		switch (language)
		{
		case 0:
			text = this.name_EN;
			break;
		case 1:
			text = this.name_GE;
			break;
		case 2:
			text = this.name_TU;
			break;
		case 3:
			text = this.name_CH;
			break;
		case 4:
			text = this.name_FR;
			break;
		default:
			if (language != 16)
			{
				text = this.name_EN;
			}
			else
			{
				text = this.name_JA;
			}
			break;
		}
		if (text == null)
		{
			return this.name_EN;
		}
		if (text.Length <= 0)
		{
			return this.name_EN;
		}
		return text;
	}

	
	public void SetOwnName(string c)
	{
		if (c == null)
		{
			return;
		}
		if (c.Length <= 0)
		{
			return;
		}
		this.name_EN = c;
		this.name_GE = c;
		this.name_TU = c;
		this.name_CH = c;
		this.name_FR = c;
		this.name_JA = c;
	}

	
	public Sprite GetLogo()
	{
		if (this.logoID < 0)
		{
			return null;
		}
		return this.guiMain_.GetCompanyLogo(this.logoID);
	}

	
	public float GetRelation()
	{
		if (!this.IsMyTochterfirma())
		{
			return this.relation;
		}
		return 100f;
	}

	
	public float GetMinimalReviewPoints()
	{
		float num = this.stars * 0.9f - this.GetRelation() / 5f;
		if (num < 20f)
		{
			num = 0f;
		}
		return num;
	}

	
	public bool IsMyTochterfirma()
	{
		return !this.mS_.multiplayer && this.tochterfirma;
	}

	
	public void SetAsTochterfirma()
	{
		this.tochterfirma = true;
	}

	
	public void RemoveTochterfirma()
	{
		this.tochterfirma = false;
	}

	
	public void VerwaltungskostenBezahlen()
	{
		if (!this.IsMyTochterfirma())
		{
			return;
		}
		if (this.tf_geschlossen)
		{
			return;
		}
		this.mS_.Pay(this.GetVerwaltungskosten(), 30);
	}

	
	public long GetVerwaltungskosten()
	{
		if (!this.mS_)
		{
			this.FindScripts();
		}
		if (this.tf_geschlossen)
		{
			return 0L;
		}
		long num = this.GetFirmenwert();
		switch (this.mS_.difficulty)
		{
		case 0:
			num = num / 1000000L * 2000L;
			break;
		case 1:
			num = num / 1000000L * 3000L;
			break;
		case 2:
			num = num / 1000000L * 4000L;
			break;
		case 3:
			num = num / 1000000L * 5000L;
			break;
		case 4:
			num = num / 1000000L * 6000L;
			break;
		case 5:
			num = num / 1000000L * 7000L;
			break;
		}
		return num + 30000L;
	}

	
	public long GetFirmenwert()
	{
		if (!this.mS_)
		{
			this.FindScripts();
		}
		return this.firmenwert + (long)((this.mS_.year - 1976) * 1500000);
	}

	
	public string GetFirmenwertString()
	{
		if (!this.mS_)
		{
			this.FindScripts();
		}
		if (!this.tS_)
		{
			this.FindScripts();
		}
		if (!this.notForSale)
		{
			return this.mS_.GetMoney(this.GetFirmenwert(), true);
		}
		return this.tS_.GetText(1912);
	}

	
	public void SetNewGameInWeeks(int force)
	{
		if (force != 9999)
		{
			if (force == 0)
			{
				float num = (float)((this.mS_.year - 1976) * 2 + 10);
				if (num > 48f)
				{
					num = 48f;
				}
				num -= (float)(this.developmentSpeed * 2);
				if (this.tochterfirma)
				{
					switch (this.tf_entwicklungsdauer)
					{
					case 1:
						num *= 1.5f;
						break;
					case 2:
						num *= 2f;
						break;
					}
				}
				if (num < 10f)
				{
					num = 10f;
				}
				this.newGameInWeeks = UnityEngine.Random.Range(Mathf.RoundToInt(num), Mathf.RoundToInt(num * 2f));
			}
			else
			{
				this.newGameInWeeks = force;
				if (this.newGameInWeeks < 5)
				{
					this.newGameInWeeks = 5;
				}
			}
		}
		else
		{
			this.newGameInWeeks = 2;
		}
		this.newGameInWeeksORG = this.newGameInWeeks;
	}

	
	public gameScript CreateNewGame2(bool forceContractGame)
	{
		if (this.mS_.multiplayer && this.mS_.mpCalls_.isClient)
		{
			return null;
		}
		if (!this.isUnlocked)
		{
			return null;
		}
		if (this.tochterfirma && this.tf_geschlossen)
		{
			return null;
		}
		if (!this.developer)
		{
			return null;
		}
		if (!forceContractGame)
		{
			this.newGameInWeeks--;
			if (this.newGameInWeeks > 0)
			{
				return null;
			}
			this.SetNewGameInWeeks(0);
		}
		else
		{
			if (!this.publisher)
			{
				return null;
			}
			if (this.onlyMobile)
			{
				return null;
			}
		}
		if (this.onlyMobile && !this.unlock_.Get(65))
		{
			return null;
		}
		bool flag = false;
		gameScript gameScript = this.games_.CreateNewGame(false, true);
		gameScript.playerGame = false;
		gameScript.multiplayerSlot = -1;
		gameScript.inDevelopment = false;
		gameScript.developerID = this.myID;
		gameScript.ownerID = this.myID;
		gameScript.hype = UnityEngine.Random.Range(this.stars * 0.5f, this.stars);
		gameScript.costs_marketing = (long)Mathf.RoundToInt(gameScript.hype * 7500f);
		gameScript.mainIP = gameScript.myID;
		gameScript.finanzierung_Grundkosten = 100;
		gameScript.finanzierung_Technology = 100;
		gameScript.finanzierung_Kontent = 100;
		gameScript.gameAP_Gameplay = 5;
		gameScript.gameAP_Grafik = 5;
		gameScript.gameAP_Sound = 5;
		gameScript.gameAP_Technik = 5;
		int num = UnityEngine.Random.Range(0, 11);
		if (UnityEngine.Random.Range(0, 100) > 50)
		{
			num = 6;
		}
		if (UnityEngine.Random.Range(0, 100) > 50)
		{
			num = 0;
		}
		if (this.nextGameAddon)
		{
			num = 2;
			this.nextGameAddon = false;
		}
		if (this.nextGameMMOAddon)
		{
			num = 3;
			this.nextGameMMOAddon = false;
		}
		if (this.tochterfirma)
		{
			if (!this.tf_allowAddon && num == 2)
			{
				num = 6;
				if (UnityEngine.Random.Range(0, 100) > 50)
				{
					num = 0;
				}
			}
			if (!this.tf_allowAddon && num == 3)
			{
				num = 6;
				if (UnityEngine.Random.Range(0, 100) > 50)
				{
					num = 0;
				}
			}
			if (this.tf_noPorts && num == 9)
			{
				num = 6;
				if (UnityEngine.Random.Range(0, 100) > 50)
				{
					num = 0;
				}
			}
			if (this.tf_noPorts && num == 10)
			{
				num = 6;
				if (UnityEngine.Random.Range(0, 100) > 50)
				{
					num = 0;
				}
			}
			if (this.tf_noBudget && num == 4)
			{
				num = 6;
				if (UnityEngine.Random.Range(0, 100) > 50)
				{
					num = 0;
				}
			}
			if (this.tf_noGOTY && num == 8)
			{
				num = 6;
				if (UnityEngine.Random.Range(0, 100) > 50)
				{
					num = 0;
				}
			}
			if (this.tf_noRemaster && num == 1)
			{
				num = 6;
				if (UnityEngine.Random.Range(0, 100) > 50)
				{
					num = 0;
				}
			}
			if (this.tf_noSpinoffs && num == 7)
			{
				num = 6;
				if (UnityEngine.Random.Range(0, 100) > 50)
				{
					num = 0;
				}
			}
			if (num == 6)
			{
				for (int i = 0; i < this.tf_ipFocus.Length; i++)
				{
					if (this.tf_ipFocus[i] != -1)
					{
						num = 0;
						break;
					}
				}
			}
		}
		if (forceContractGame)
		{
			num = 6;
		}
		switch (num)
		{
		case 0:
		{
			gameScript gameForNachfolger = this.GetGameForNachfolger();
			if (gameForNachfolger)
			{
				flag = true;
				gameScript.teile = gameForNachfolger.teile + 1;
				gameForNachfolger.nachfolger_created = true;
				gameScript.mainIP = gameForNachfolger.mainIP;
				gameScript.maingenre = gameForNachfolger.maingenre;
				gameScript.subgenre = gameForNachfolger.subgenre;
				gameScript.gameZielgruppe = gameForNachfolger.gameZielgruppe;
				gameScript.originalIP = gameForNachfolger.myID;
				gameScript.script_vorgaenger = gameForNachfolger;
				gameScript.npcLateinNumbers = gameForNachfolger.npcLateinNumbers;
				gameScript.sonderIP = gameForNachfolger.sonderIP;
				gameScript.sonderIPMindestreview = gameForNachfolger.sonderIPMindestreview;
				gameForNachfolger.FindMainIpScript();
				if (gameForNachfolger.script_mainIP)
				{
					if (gameScript.maingenre != 8)
					{
						if (!gameForNachfolger.npcLateinNumbers)
						{
							gameScript.SetMyName(gameForNachfolger.GetUrsprungsName() + " " + gameScript.teile.ToString());
						}
						else
						{
							switch (gameScript.teile)
							{
							case 1:
								gameScript.SetMyName(gameForNachfolger.script_mainIP.GetNameSimple() + " I");
								break;
							case 2:
								gameScript.SetMyName(gameForNachfolger.script_mainIP.GetNameSimple() + " II");
								break;
							case 3:
								gameScript.SetMyName(gameForNachfolger.script_mainIP.GetNameSimple() + " III");
								break;
							case 4:
								gameScript.SetMyName(gameForNachfolger.script_mainIP.GetNameSimple() + " IV");
								break;
							case 5:
								gameScript.SetMyName(gameForNachfolger.script_mainIP.GetNameSimple() + " V");
								break;
							case 6:
								gameScript.SetMyName(gameForNachfolger.script_mainIP.GetNameSimple() + " VI");
								break;
							case 7:
								gameScript.SetMyName(gameForNachfolger.script_mainIP.GetNameSimple() + " VII");
								break;
							case 8:
								gameScript.SetMyName(gameForNachfolger.script_mainIP.GetNameSimple() + " VIII");
								break;
							case 9:
								gameScript.SetMyName(gameForNachfolger.script_mainIP.GetNameSimple() + " IX");
								break;
							case 10:
								gameScript.SetMyName(gameForNachfolger.script_mainIP.GetNameSimple() + " X");
								break;
							case 11:
								gameScript.SetMyName(gameForNachfolger.script_mainIP.GetNameSimple() + " XI");
								break;
							case 12:
								gameScript.SetMyName(gameForNachfolger.script_mainIP.GetNameSimple() + " XII");
								break;
							case 13:
								gameScript.SetMyName(gameForNachfolger.script_mainIP.GetNameSimple() + " XIII");
								break;
							case 14:
								gameScript.SetMyName(gameForNachfolger.script_mainIP.GetNameSimple() + " XIV");
								break;
							case 15:
								gameScript.SetMyName(gameForNachfolger.script_mainIP.GetNameSimple() + " XV");
								break;
							case 16:
								gameScript.SetMyName(gameForNachfolger.script_mainIP.GetNameSimple() + " XVI");
								break;
							case 17:
								gameScript.SetMyName(gameForNachfolger.script_mainIP.GetNameSimple() + " XVII");
								break;
							case 18:
								gameScript.SetMyName(gameForNachfolger.script_mainIP.GetNameSimple() + " XVIII");
								break;
							case 19:
								gameScript.SetMyName(gameForNachfolger.script_mainIP.GetNameSimple() + " XIX");
								break;
							case 20:
								gameScript.SetMyName(gameForNachfolger.script_mainIP.GetNameSimple() + " XX");
								break;
							default:
								gameScript.SetMyName(gameForNachfolger.script_mainIP.GetNameSimple() + " " + gameScript.teile.ToString());
								break;
							}
						}
					}
					else if (this.mS_.year < 2000)
					{
						gameScript.SetMyName(gameForNachfolger.script_mainIP.GetNameSimple() + " " + (this.mS_.year - 1900).ToString());
					}
					else
					{
						gameScript.SetMyName(gameForNachfolger.script_mainIP.GetNameSimple() + " " + this.mS_.year.ToString());
					}
					gameScript.typ_nachfolger = true;
					int platTyp = this.SetPlatformTyp(gameScript, forceContractGame);
					gameScript.gameTyp = 0;
					gameScript.gameLicence = -1;
					this.SetCopyProtect(gameScript);
					this.SetAntiCheat(gameScript);
					this.SetGameSize(gameScript);
					this.SetMMOorF2P(gameScript, platTyp);
					if (!gameScript.sonderIP)
					{
						this.SetLicence(gameScript);
					}
					if (!gameScript.sonderIP)
					{
						this.SetTheme(gameScript);
					}
					else
					{
						gameScript.gameMainTheme = gameForNachfolger.gameMainTheme;
						gameScript.gameSubTheme = gameForNachfolger.gameSubTheme;
					}
					this.SetDesignSlider(gameScript);
					this.SetLanguages(gameScript);
					this.SetStudioAufwertungen(gameScript);
					this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().SetMGSR_Result(gameScript, gameScript.Designausrichtung[1]);
					this.FindPlatformsForGameNew(gameScript);
					this.FindEngineForGameNew(gameScript);
					this.SetGameplayFeatures(gameScript);
					this.SetPoints(gameScript);
					if (gameScript.points_gameplay < gameForNachfolger.points_gameplay)
					{
						gameScript.points_gameplay = gameForNachfolger.points_gameplay;
					}
					if (gameScript.points_grafik < gameForNachfolger.points_grafik)
					{
						gameScript.points_grafik = gameForNachfolger.points_grafik;
					}
					if (gameScript.points_sound < gameForNachfolger.points_sound)
					{
						gameScript.points_sound = gameForNachfolger.points_sound;
					}
					if (gameScript.points_technik < gameForNachfolger.points_technik)
					{
						gameScript.points_gameplay = gameForNachfolger.points_technik;
					}
					if (gameScript.gameTyp == 0 && !this.nextGameAddon && UnityEngine.Random.Range(0, 100) < 30)
					{
						this.nextGameAddon = true;
						this.SetNewGameInWeeks(UnityEngine.Random.Range(10, 20) - this.developmentSpeed);
					}
					if (gameScript.gameTyp == 1 && !this.nextGameMMOAddon && UnityEngine.Random.Range(0, 100) < 80)
					{
						this.nextGameMMOAddon = true;
						this.SetNewGameInWeeks(UnityEngine.Random.Range(10, 20) - this.developmentSpeed);
					}
				}
				else
				{
					flag = false;
				}
			}
			break;
		}
		case 1:
			if (this.mS_.year >= 1987)
			{
				gameScript remaster = this.GetRemaster();
				if (remaster)
				{
					flag = true;
					remaster.remaster_created = true;
					gameScript.typ_standard = false;
					gameScript.typ_remaster = true;
					gameScript.SetMyName(remaster.GetNameSimple() + " " + this.tS_.GetText(620));
					gameScript.mainIP = remaster.mainIP;
					gameScript.maingenre = remaster.maingenre;
					gameScript.subgenre = remaster.subgenre;
					gameScript.gameMainTheme = remaster.gameMainTheme;
					gameScript.gameSubTheme = remaster.gameSubTheme;
					gameScript.gameZielgruppe = remaster.gameZielgruppe;
					gameScript.gameSize = remaster.gameSize;
					gameScript.npcLateinNumbers = remaster.npcLateinNumbers;
					gameScript.gameLicence = remaster.gameLicence;
					gameScript.sonderIP = remaster.sonderIP;
					gameScript.sonderIPMindestreview = remaster.sonderIPMindestreview;
					gameScript.exklusiv = remaster.exklusiv;
					gameScript.herstellerExklusiv = remaster.herstellerExklusiv;
					gameScript.handy = remaster.handy;
					gameScript.arcade = remaster.arcade;
					gameScript.retro = remaster.retro;
					gameScript.gameTyp = 0;
					gameScript.gameLicence = -1;
					this.SetCopyProtect(gameScript);
					this.SetAntiCheat(gameScript);
					this.SetDesignSlider(gameScript);
					this.SetLanguages(gameScript);
					this.SetStudioAufwertungen(gameScript);
					this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().SetMGSR_Result(gameScript, gameScript.Designausrichtung[1]);
					this.FindPlatformsForGameNew(gameScript);
					this.FindEngineForGameNew(gameScript);
					this.SetGameplayFeatures(gameScript);
					this.SetPoints(gameScript);
					if (gameScript.points_gameplay < remaster.points_gameplay)
					{
						gameScript.points_gameplay = remaster.points_gameplay;
					}
					if (gameScript.points_grafik < remaster.points_grafik)
					{
						gameScript.points_grafik = remaster.points_grafik;
					}
					if (gameScript.points_sound < remaster.points_sound)
					{
						gameScript.points_sound = remaster.points_sound;
					}
					if (gameScript.points_technik < remaster.points_technik)
					{
						gameScript.points_gameplay = remaster.points_technik;
					}
				}
			}
			break;
		case 2:
		{
			gameScript addon = this.GetAddon();
			if (addon)
			{
				addon.FindMyEngineNew();
				if (addon.engineS_ && !addon.engineS_.playerEngine && addon.engineS_.multiplayerSlot == -1)
				{
					flag = true;
					gameScript.SetMyName(addon.GetNameSimple() + " - " + this.tS_.GetRandomNPCAddonName());
					gameScript.mainIP = addon.mainIP;
					gameScript.typ_standard = false;
					gameScript.typ_addon = true;
					gameScript.maingenre = addon.maingenre;
					gameScript.subgenre = addon.subgenre;
					gameScript.gameMainTheme = addon.gameMainTheme;
					gameScript.gameSubTheme = addon.gameSubTheme;
					gameScript.gameZielgruppe = addon.gameZielgruppe;
					gameScript.gameSize = addon.gameSize;
					gameScript.addonQuality = 0.4f;
					gameScript.points_gameplay = addon.points_gameplay * 1.1f;
					gameScript.points_grafik = addon.points_grafik * 1.1f;
					gameScript.points_sound = addon.points_sound * 1.1f;
					gameScript.points_technik = addon.points_technik * 1.1f;
					gameScript.publisherID = addon.publisherID;
					gameScript.pS_ = addon.pS_;
					gameScript.originalIP = addon.myID;
					gameScript.usk = addon.usk;
					gameScript.npcLateinNumbers = addon.npcLateinNumbers;
					gameScript.sonderIP = addon.sonderIP;
					gameScript.sonderIPMindestreview = addon.sonderIPMindestreview;
					gameScript.exklusiv = addon.exklusiv;
					gameScript.herstellerExklusiv = addon.herstellerExklusiv;
					if (UnityEngine.Random.Range(0, 100) < 30)
					{
						gameScript.typ_addon = false;
						gameScript.typ_addonStandalone = true;
					}
					gameScript.engineID = addon.engineID;
					gameScript.engineS_ = addon.engineS_;
					gameScript.engineFeature_DevDone[0] = true;
					gameScript.engineFeature_DevDone[1] = true;
					gameScript.engineFeature_DevDone[2] = true;
					gameScript.engineFeature_DevDone[3] = true;
					gameScript.gameEngineFeature[0] = addon.engineS_.GetBestFeature(this.eF_.GetTypGrafik());
					gameScript.gameEngineFeature[1] = addon.engineS_.GetBestFeature(this.eF_.GetTypSound());
					gameScript.gameEngineFeature[2] = addon.engineS_.GetBestFeature(this.eF_.GetTypKI());
					gameScript.gameEngineFeature[3] = addon.engineS_.GetBestFeature(this.eF_.GetTypPhysik());
					for (int j = 0; j < addon.gamePlatform.Length; j++)
					{
						gameScript.gamePlatform[j] = addon.gamePlatform[j];
						gameScript.gamePlatformScript[j] = addon.gamePlatformScript[j];
					}
					for (int k = 0; k < gameScript.gameLanguage.Length; k++)
					{
						gameScript.gameLanguage[k] = addon.gameLanguage[k];
					}
					for (int l = 0; l < gameScript.gameplayStudio.Length; l++)
					{
						gameScript.gameplayStudio[l] = addon.gameplayStudio[l];
					}
					for (int m = 0; m < gameScript.grafikStudio.Length; m++)
					{
						gameScript.grafikStudio[m] = addon.grafikStudio[m];
					}
					for (int n = 0; n < gameScript.soundStudio.Length; n++)
					{
						gameScript.soundStudio[n] = addon.soundStudio[n];
					}
					for (int num2 = 0; num2 < gameScript.motionCaptureStudio.Length; num2++)
					{
						gameScript.motionCaptureStudio[num2] = addon.motionCaptureStudio[num2];
					}
					gameScript.gameTyp = 0;
					gameScript.gameLicence = -1;
					this.SetCopyProtect(gameScript);
					this.SetAntiCheat(gameScript);
					this.SetDesignSlider(gameScript);
					this.SetGameplayFeatures(gameScript);
				}
			}
			break;
		}
		case 3:
		{
			gameScript mmoaddon = this.GetMMOAddon();
			if (mmoaddon)
			{
				mmoaddon.FindMyEngineNew();
				if (mmoaddon.engineS_ && !mmoaddon.engineS_.playerEngine && mmoaddon.engineS_.multiplayerSlot == -1)
				{
					flag = true;
					gameScript.SetMyName(mmoaddon.GetNameSimple() + " - " + this.tS_.GetRandomNPCAddonName());
					gameScript.mainIP = mmoaddon.mainIP;
					gameScript.typ_standard = false;
					gameScript.typ_mmoaddon = true;
					gameScript.maingenre = mmoaddon.maingenre;
					gameScript.subgenre = mmoaddon.subgenre;
					gameScript.gameMainTheme = mmoaddon.gameMainTheme;
					gameScript.gameSubTheme = mmoaddon.gameSubTheme;
					gameScript.gameZielgruppe = mmoaddon.gameZielgruppe;
					gameScript.gameSize = mmoaddon.gameSize;
					gameScript.addonQuality = 0.4f;
					gameScript.points_gameplay = mmoaddon.points_gameplay * 1.1f;
					gameScript.points_grafik = mmoaddon.points_grafik * 1.1f;
					gameScript.points_sound = mmoaddon.points_sound * 1.1f;
					gameScript.points_technik = mmoaddon.points_technik * 1.1f;
					gameScript.publisherID = mmoaddon.publisherID;
					gameScript.pS_ = mmoaddon.pS_;
					gameScript.originalIP = mmoaddon.myID;
					gameScript.usk = mmoaddon.usk;
					gameScript.npcLateinNumbers = mmoaddon.npcLateinNumbers;
					gameScript.sonderIP = mmoaddon.sonderIP;
					gameScript.sonderIPMindestreview = mmoaddon.sonderIPMindestreview;
					gameScript.exklusiv = mmoaddon.exklusiv;
					gameScript.herstellerExklusiv = mmoaddon.herstellerExklusiv;
					gameScript.engineID = mmoaddon.engineID;
					gameScript.engineS_ = mmoaddon.engineS_;
					gameScript.engineFeature_DevDone[0] = true;
					gameScript.engineFeature_DevDone[1] = true;
					gameScript.engineFeature_DevDone[2] = true;
					gameScript.engineFeature_DevDone[3] = true;
					gameScript.gameEngineFeature[0] = mmoaddon.engineS_.GetBestFeature(this.eF_.GetTypGrafik());
					gameScript.gameEngineFeature[1] = mmoaddon.engineS_.GetBestFeature(this.eF_.GetTypSound());
					gameScript.gameEngineFeature[2] = mmoaddon.engineS_.GetBestFeature(this.eF_.GetTypKI());
					gameScript.gameEngineFeature[3] = mmoaddon.engineS_.GetBestFeature(this.eF_.GetTypPhysik());
					for (int num3 = 0; num3 < mmoaddon.gamePlatform.Length; num3++)
					{
						gameScript.gamePlatform[num3] = mmoaddon.gamePlatform[num3];
						gameScript.gamePlatformScript[num3] = mmoaddon.gamePlatformScript[num3];
					}
					for (int num4 = 0; num4 < gameScript.gameLanguage.Length; num4++)
					{
						gameScript.gameLanguage[num4] = mmoaddon.gameLanguage[num4];
					}
					for (int num5 = 0; num5 < gameScript.gameplayStudio.Length; num5++)
					{
						gameScript.gameplayStudio[num5] = mmoaddon.gameplayStudio[num5];
					}
					for (int num6 = 0; num6 < gameScript.grafikStudio.Length; num6++)
					{
						gameScript.grafikStudio[num6] = mmoaddon.grafikStudio[num6];
					}
					for (int num7 = 0; num7 < gameScript.soundStudio.Length; num7++)
					{
						gameScript.soundStudio[num7] = mmoaddon.soundStudio[num7];
					}
					for (int num8 = 0; num8 < gameScript.motionCaptureStudio.Length; num8++)
					{
						gameScript.motionCaptureStudio[num8] = mmoaddon.motionCaptureStudio[num8];
					}
					gameScript.gameTyp = 0;
					gameScript.gameLicence = -1;
					this.SetCopyProtect(gameScript);
					this.SetAntiCheat(gameScript);
					this.SetDesignSlider(gameScript);
					this.SetGameplayFeatures(gameScript);
				}
			}
			break;
		}
		case 4:
		{
			gameScript gameForBudget = this.GetGameForBudget();
			if (gameForBudget && this.publisher)
			{
				gameForBudget.FindMyEngineNew();
				if (gameForBudget.engineS_ && !gameForBudget.engineS_.playerEngine && gameForBudget.engineS_.multiplayerSlot == -1)
				{
					flag = true;
					gameForBudget.budget_created = true;
					gameScript.date_month = this.mS_.month;
					gameScript.date_year = this.mS_.year;
					gameScript.SetMyName(gameForBudget.GetNameSimple() + " <color=grey><i>" + this.tS_.GetText(1154 + UnityEngine.Random.Range(0, 4)) + "</i></color>");
					gameScript.mainIP = gameForBudget.mainIP;
					gameScript.typ_standard = false;
					gameScript.typ_budget = true;
					gameScript.maingenre = gameForBudget.maingenre;
					gameScript.subgenre = gameForBudget.subgenre;
					gameScript.gameMainTheme = gameForBudget.gameMainTheme;
					gameScript.gameSubTheme = gameForBudget.gameSubTheme;
					gameScript.gameZielgruppe = gameForBudget.gameZielgruppe;
					gameScript.gameSize = gameForBudget.gameSize;
					gameScript.points_gameplay = gameForBudget.points_gameplay;
					gameScript.points_grafik = gameForBudget.points_grafik;
					gameScript.points_sound = gameForBudget.points_sound;
					gameScript.points_technik = gameForBudget.points_technik;
					gameScript.publisherID = gameForBudget.publisherID;
					gameScript.pS_ = gameForBudget.pS_;
					gameScript.originalIP = gameForBudget.myID;
					gameScript.usk = gameForBudget.usk;
					gameScript.npcLateinNumbers = gameForBudget.npcLateinNumbers;
					gameScript.gameLicence = gameForBudget.gameLicence;
					gameScript.sonderIP = gameForBudget.sonderIP;
					gameScript.sonderIPMindestreview = gameForBudget.sonderIPMindestreview;
					gameScript.exklusiv = gameForBudget.exklusiv;
					gameScript.herstellerExklusiv = gameForBudget.herstellerExklusiv;
					gameScript.engineID = gameForBudget.engineID;
					gameScript.engineS_ = gameForBudget.engineS_;
					gameScript.engineFeature_DevDone[0] = true;
					gameScript.engineFeature_DevDone[1] = true;
					gameScript.engineFeature_DevDone[2] = true;
					gameScript.engineFeature_DevDone[3] = true;
					gameScript.gameEngineFeature[0] = gameForBudget.engineS_.GetBestFeature(this.eF_.GetTypGrafik());
					gameScript.gameEngineFeature[1] = gameForBudget.engineS_.GetBestFeature(this.eF_.GetTypSound());
					gameScript.gameEngineFeature[2] = gameForBudget.engineS_.GetBestFeature(this.eF_.GetTypKI());
					gameScript.gameEngineFeature[3] = gameForBudget.engineS_.GetBestFeature(this.eF_.GetTypPhysik());
					for (int num9 = 0; num9 < gameForBudget.gamePlatform.Length; num9++)
					{
						gameScript.gamePlatform[num9] = gameForBudget.gamePlatform[num9];
						gameScript.gamePlatformScript[num9] = gameForBudget.gamePlatformScript[num9];
					}
					for (int num10 = 0; num10 < gameScript.gameLanguage.Length; num10++)
					{
						gameScript.gameLanguage[num10] = gameForBudget.gameLanguage[num10];
					}
					for (int num11 = 0; num11 < gameScript.gameplayStudio.Length; num11++)
					{
						gameScript.gameplayStudio[num11] = gameForBudget.gameplayStudio[num11];
					}
					for (int num12 = 0; num12 < gameScript.grafikStudio.Length; num12++)
					{
						gameScript.grafikStudio[num12] = gameForBudget.grafikStudio[num12];
					}
					for (int num13 = 0; num13 < gameScript.soundStudio.Length; num13++)
					{
						gameScript.soundStudio[num13] = gameForBudget.soundStudio[num13];
					}
					for (int num14 = 0; num14 < gameScript.motionCaptureStudio.Length; num14++)
					{
						gameScript.motionCaptureStudio[num14] = gameForBudget.motionCaptureStudio[num14];
					}
					gameScript.gameTyp = 0;
					gameScript.gameLicence = -1;
					this.SetCopyProtect(gameScript);
					this.SetAntiCheat(gameScript);
					this.SetDesignSlider(gameScript);
					this.SetGameplayFeatures(gameScript);
					gameScript.reviewGameplay = gameForBudget.reviewGameplay;
					gameScript.reviewGrafik = gameForBudget.reviewGrafik;
					gameScript.reviewSound = gameForBudget.reviewSound;
					gameScript.reviewSteuerung = gameForBudget.reviewSteuerung;
					gameScript.reviewTotal = gameForBudget.reviewTotal;
					gameScript.reviewGameplayText = gameForBudget.reviewGameplayText;
					gameScript.reviewGrafikText = gameForBudget.reviewGrafikText;
					gameScript.reviewSoundText = gameForBudget.reviewSoundText;
					gameScript.reviewSteuerungText = gameForBudget.reviewSteuerungText;
					gameScript.reviewTotalText = gameForBudget.reviewTotalText;
				}
			}
			break;
		}
		case 5:
			gameScript.typ_bundle = true;
			break;
		case 6:
			if (!forceContractGame)
			{
				this.tS_.GetRandomNpcIP(this.myID, gameScript);
			}
			if (gameScript.GetNameSimple().Length <= 0)
			{
				this.SetGenre(gameScript);
				gameScript.SetMyName(this.tS_.GetRandomNpcGame(gameScript.maingenre));
			}
			else
			{
				gameScript.sonderIP = true;
			}
			if (gameScript.GetNameSimple().Length > 0)
			{
				flag = true;
				gameScript.teile = 1;
				if (UnityEngine.Random.Range(0, 100) > 60)
				{
					gameScript.npcLateinNumbers = true;
				}
				gameScript.typ_standard = true;
				int platTyp2 = this.SetPlatformTyp(gameScript, forceContractGame);
				gameScript.gameTyp = 0;
				if (!gameScript.sonderIP)
				{
					gameScript.gameZielgruppe = UnityEngine.Random.Range(0, 5);
				}
				gameScript.gameLicence = -1;
				this.SetCopyProtect(gameScript);
				this.SetAntiCheat(gameScript);
				this.SetGameSize(gameScript);
				this.SetMMOorF2P(gameScript, platTyp2);
				if (!gameScript.sonderIP)
				{
					this.SetLicence(gameScript);
				}
				if (!gameScript.sonderIP)
				{
					this.SetTheme(gameScript);
				}
				this.SetDesignSlider(gameScript);
				this.SetLanguages(gameScript);
				this.SetStudioAufwertungen(gameScript);
				this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().SetMGSR_Result(gameScript, gameScript.Designausrichtung[1]);
				this.FindPlatformsForGameNew(gameScript);
				this.FindEngineForGameNew(gameScript);
				this.SetGameplayFeatures(gameScript);
				this.SetPoints(gameScript);
				if (gameScript.gameTyp == 0 && !this.nextGameAddon && UnityEngine.Random.Range(0, 100) < 30)
				{
					this.nextGameAddon = true;
					this.SetNewGameInWeeks(UnityEngine.Random.Range(10, 20) - this.developmentSpeed);
				}
				if (gameScript.gameTyp == 1 && !this.nextGameMMOAddon && UnityEngine.Random.Range(0, 100) < 80)
				{
					this.nextGameMMOAddon = true;
					this.SetNewGameInWeeks(UnityEngine.Random.Range(10, 20) - this.developmentSpeed);
				}
			}
			break;
		case 7:
		{
			gameScript spinoff = this.GetSpinoff();
			if (spinoff)
			{
				this.SetGenre(gameScript);
				gameScript.SetMyName(this.tS_.GetRandomNpcGame(gameScript.maingenre));
				if (gameScript.GetNameSimple().Length > 0)
				{
					flag = true;
					gameScript.teile = 1;
					gameScript.mainIP = spinoff.mainIP;
					if (UnityEngine.Random.Range(0, 100) > 60)
					{
						gameScript.npcLateinNumbers = true;
					}
					gameScript.typ_standard = false;
					gameScript.typ_spinoff = true;
					int platTyp3 = this.SetPlatformTyp(gameScript, forceContractGame);
					gameScript.gameTyp = 0;
					gameScript.gameZielgruppe = UnityEngine.Random.Range(0, 5);
					gameScript.gameLicence = -1;
					this.SetCopyProtect(gameScript);
					this.SetAntiCheat(gameScript);
					this.SetGameSize(gameScript);
					this.SetMMOorF2P(gameScript, platTyp3);
					this.SetTheme(gameScript);
					this.SetDesignSlider(gameScript);
					this.SetLanguages(gameScript);
					this.SetStudioAufwertungen(gameScript);
					this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().SetMGSR_Result(gameScript, gameScript.Designausrichtung[1]);
					this.FindPlatformsForGameNew(gameScript);
					this.FindEngineForGameNew(gameScript);
					this.SetGameplayFeatures(gameScript);
					this.SetPoints(gameScript);
					Debug.Log("NPC Create Spinoff:" + gameScript.myID);
				}
			}
			break;
		}
		case 8:
		{
			gameScript goty = this.GetGOTY();
			if (goty && this.publisher)
			{
				goty.FindMyEngineNew();
				if (goty.engineS_ && !goty.engineS_.playerEngine && goty.engineS_.multiplayerSlot == -1)
				{
					flag = true;
					goty.goty_created = true;
					gameScript.date_month = this.mS_.month;
					gameScript.date_year = this.mS_.year;
					gameScript.SetMyName(goty.GetNameSimple() + " " + this.tS_.GetText(1361));
					gameScript.mainIP = goty.mainIP;
					gameScript.typ_standard = false;
					gameScript.typ_goty = true;
					gameScript.maingenre = goty.maingenre;
					gameScript.subgenre = goty.subgenre;
					gameScript.gameMainTheme = goty.gameMainTheme;
					gameScript.gameSubTheme = goty.gameSubTheme;
					gameScript.gameZielgruppe = goty.gameZielgruppe;
					gameScript.gameSize = goty.gameSize;
					gameScript.points_gameplay = goty.points_gameplay;
					gameScript.points_grafik = goty.points_grafik;
					gameScript.points_sound = goty.points_sound;
					gameScript.points_technik = goty.points_technik;
					gameScript.publisherID = goty.publisherID;
					gameScript.pS_ = goty.pS_;
					gameScript.originalIP = goty.myID;
					gameScript.usk = goty.usk;
					gameScript.npcLateinNumbers = goty.npcLateinNumbers;
					gameScript.gameLicence = goty.gameLicence;
					gameScript.sonderIP = goty.sonderIP;
					gameScript.sonderIPMindestreview = goty.sonderIPMindestreview;
					gameScript.exklusiv = goty.exklusiv;
					gameScript.herstellerExklusiv = goty.herstellerExklusiv;
					gameScript.engineID = goty.engineID;
					gameScript.engineS_ = goty.engineS_;
					gameScript.engineFeature_DevDone[0] = true;
					gameScript.engineFeature_DevDone[1] = true;
					gameScript.engineFeature_DevDone[2] = true;
					gameScript.engineFeature_DevDone[3] = true;
					gameScript.gameEngineFeature[0] = goty.engineS_.GetBestFeature(this.eF_.GetTypGrafik());
					gameScript.gameEngineFeature[1] = goty.engineS_.GetBestFeature(this.eF_.GetTypSound());
					gameScript.gameEngineFeature[2] = goty.engineS_.GetBestFeature(this.eF_.GetTypKI());
					gameScript.gameEngineFeature[3] = goty.engineS_.GetBestFeature(this.eF_.GetTypPhysik());
					for (int num15 = 0; num15 < goty.gamePlatform.Length; num15++)
					{
						gameScript.gamePlatform[num15] = goty.gamePlatform[num15];
						gameScript.gamePlatformScript[num15] = goty.gamePlatformScript[num15];
					}
					for (int num16 = 0; num16 < gameScript.gameLanguage.Length; num16++)
					{
						gameScript.gameLanguage[num16] = goty.gameLanguage[num16];
					}
					for (int num17 = 0; num17 < gameScript.gameplayStudio.Length; num17++)
					{
						gameScript.gameplayStudio[num17] = goty.gameplayStudio[num17];
					}
					for (int num18 = 0; num18 < gameScript.grafikStudio.Length; num18++)
					{
						gameScript.grafikStudio[num18] = goty.grafikStudio[num18];
					}
					for (int num19 = 0; num19 < gameScript.soundStudio.Length; num19++)
					{
						gameScript.soundStudio[num19] = goty.soundStudio[num19];
					}
					for (int num20 = 0; num20 < gameScript.motionCaptureStudio.Length; num20++)
					{
						gameScript.motionCaptureStudio[num20] = goty.motionCaptureStudio[num20];
					}
					gameScript.gameTyp = 0;
					gameScript.gameLicence = -1;
					this.SetCopyProtect(gameScript);
					this.SetAntiCheat(gameScript);
					this.SetDesignSlider(gameScript);
					this.SetGameplayFeatures(gameScript);
					gameScript.reviewGameplay = goty.reviewGameplay;
					gameScript.reviewGrafik = goty.reviewGrafik;
					gameScript.reviewSound = goty.reviewSound;
					gameScript.reviewSteuerung = goty.reviewSteuerung;
					gameScript.reviewTotal = goty.reviewTotal;
					gameScript.reviewGameplayText = goty.reviewGameplayText;
					gameScript.reviewGrafikText = goty.reviewGrafikText;
					gameScript.reviewSoundText = goty.reviewSoundText;
					gameScript.reviewSteuerungText = goty.reviewSteuerungText;
					gameScript.reviewTotalText = goty.reviewTotalText;
					Debug.Log("NPC Create GOTY Game:" + gameScript.myID);
				}
			}
			break;
		}
		case 9:
		{
			gameScript portForHandy = this.GetPortForHandy();
			if (portForHandy && this.unlock_.Get(65))
			{
				portForHandy.FindMyEngineNew();
				if (portForHandy.engineS_ && !portForHandy.engineS_.playerEngine && portForHandy.engineS_.multiplayerSlot == -1)
				{
					flag = true;
					portForHandy.portExist[1] = true;
					gameScript.date_month = this.mS_.month;
					gameScript.date_year = this.mS_.year;
					gameScript.SetMyName(portForHandy.GetNameSimple());
					gameScript.handy = true;
					gameScript.mainIP = portForHandy.mainIP;
					gameScript.typ_standard = portForHandy.typ_standard;
					gameScript.typ_nachfolger = portForHandy.typ_nachfolger;
					gameScript.typ_remaster = portForHandy.typ_remaster;
					gameScript.typ_spinoff = portForHandy.typ_spinoff;
					gameScript.typ_goty = portForHandy.typ_goty;
					gameScript.maingenre = portForHandy.maingenre;
					gameScript.subgenre = portForHandy.subgenre;
					gameScript.gameMainTheme = portForHandy.gameMainTheme;
					gameScript.gameSubTheme = portForHandy.gameSubTheme;
					gameScript.gameZielgruppe = portForHandy.gameZielgruppe;
					gameScript.gameSize = portForHandy.gameSize;
					gameScript.points_gameplay = portForHandy.points_gameplay;
					gameScript.points_grafik = portForHandy.points_grafik;
					gameScript.points_sound = portForHandy.points_sound;
					gameScript.points_technik = portForHandy.points_technik;
					gameScript.publisherID = portForHandy.publisherID;
					gameScript.pS_ = portForHandy.pS_;
					gameScript.portID = portForHandy.myID;
					gameScript.usk = portForHandy.usk;
					gameScript.npcLateinNumbers = portForHandy.npcLateinNumbers;
					gameScript.gameLicence = portForHandy.gameLicence;
					gameScript.sonderIP = portForHandy.sonderIP;
					gameScript.sonderIPMindestreview = portForHandy.sonderIPMindestreview;
					this.FindPlatformsForGameNew(gameScript);
					gameScript.engineID = portForHandy.engineID;
					gameScript.engineS_ = portForHandy.engineS_;
					gameScript.engineFeature_DevDone[0] = true;
					gameScript.engineFeature_DevDone[1] = true;
					gameScript.engineFeature_DevDone[2] = true;
					gameScript.engineFeature_DevDone[3] = true;
					gameScript.gameEngineFeature[0] = portForHandy.engineS_.GetBestFeature(this.eF_.GetTypGrafik());
					gameScript.gameEngineFeature[1] = portForHandy.engineS_.GetBestFeature(this.eF_.GetTypSound());
					gameScript.gameEngineFeature[2] = portForHandy.engineS_.GetBestFeature(this.eF_.GetTypKI());
					gameScript.gameEngineFeature[3] = portForHandy.engineS_.GetBestFeature(this.eF_.GetTypPhysik());
					for (int num21 = 0; num21 < gameScript.gameLanguage.Length; num21++)
					{
						gameScript.gameLanguage[num21] = portForHandy.gameLanguage[num21];
					}
					for (int num22 = 0; num22 < gameScript.gameplayStudio.Length; num22++)
					{
						gameScript.gameplayStudio[num22] = portForHandy.gameplayStudio[num22];
					}
					for (int num23 = 0; num23 < gameScript.grafikStudio.Length; num23++)
					{
						gameScript.grafikStudio[num23] = portForHandy.grafikStudio[num23];
					}
					for (int num24 = 0; num24 < gameScript.soundStudio.Length; num24++)
					{
						gameScript.soundStudio[num24] = portForHandy.soundStudio[num24];
					}
					for (int num25 = 0; num25 < gameScript.motionCaptureStudio.Length; num25++)
					{
						gameScript.motionCaptureStudio[num25] = portForHandy.motionCaptureStudio[num25];
					}
					gameScript.gameTyp = 0;
					gameScript.gameLicence = -1;
					this.SetCopyProtect(gameScript);
					this.SetAntiCheat(gameScript);
					this.SetDesignSlider(gameScript);
					this.SetGameplayFeatures(gameScript);
				}
			}
			break;
		}
		case 10:
			if (!this.onlyMobile)
			{
				gameScript portForArcade = this.GetPortForArcade();
				if (portForArcade && this.publisher)
				{
					portForArcade.FindMyEngineNew();
					if (portForArcade.engineS_ && !portForArcade.engineS_.playerEngine && portForArcade.engineS_.multiplayerSlot == -1)
					{
						flag = true;
						portForArcade.portExist[2] = true;
						gameScript.date_month = this.mS_.month;
						gameScript.date_year = this.mS_.year;
						gameScript.SetMyName(portForArcade.GetNameSimple());
						gameScript.arcade = true;
						gameScript.mainIP = portForArcade.mainIP;
						gameScript.typ_standard = portForArcade.typ_standard;
						gameScript.typ_nachfolger = portForArcade.typ_nachfolger;
						gameScript.typ_remaster = portForArcade.typ_remaster;
						gameScript.typ_spinoff = portForArcade.typ_spinoff;
						gameScript.typ_goty = portForArcade.typ_goty;
						gameScript.maingenre = portForArcade.maingenre;
						gameScript.subgenre = portForArcade.subgenre;
						gameScript.gameMainTheme = portForArcade.gameMainTheme;
						gameScript.gameSubTheme = portForArcade.gameSubTheme;
						gameScript.gameZielgruppe = portForArcade.gameZielgruppe;
						gameScript.gameSize = portForArcade.gameSize;
						gameScript.points_gameplay = portForArcade.points_gameplay;
						gameScript.points_grafik = portForArcade.points_grafik;
						gameScript.points_sound = portForArcade.points_sound;
						gameScript.points_technik = portForArcade.points_technik;
						gameScript.publisherID = portForArcade.publisherID;
						gameScript.pS_ = portForArcade.pS_;
						gameScript.portID = portForArcade.myID;
						gameScript.usk = portForArcade.usk;
						gameScript.npcLateinNumbers = portForArcade.npcLateinNumbers;
						gameScript.gameLicence = portForArcade.gameLicence;
						gameScript.sonderIP = portForArcade.sonderIP;
						gameScript.sonderIPMindestreview = portForArcade.sonderIPMindestreview;
						this.FindPlatformsForGameNew(gameScript);
						gameScript.engineID = portForArcade.engineID;
						gameScript.engineS_ = portForArcade.engineS_;
						gameScript.engineFeature_DevDone[0] = true;
						gameScript.engineFeature_DevDone[1] = true;
						gameScript.engineFeature_DevDone[2] = true;
						gameScript.engineFeature_DevDone[3] = true;
						gameScript.gameEngineFeature[0] = portForArcade.engineS_.GetBestFeature(this.eF_.GetTypGrafik());
						gameScript.gameEngineFeature[1] = portForArcade.engineS_.GetBestFeature(this.eF_.GetTypSound());
						gameScript.gameEngineFeature[2] = portForArcade.engineS_.GetBestFeature(this.eF_.GetTypKI());
						gameScript.gameEngineFeature[3] = portForArcade.engineS_.GetBestFeature(this.eF_.GetTypPhysik());
						for (int num26 = 0; num26 < gameScript.gameLanguage.Length; num26++)
						{
							gameScript.gameLanguage[num26] = portForArcade.gameLanguage[num26];
						}
						for (int num27 = 0; num27 < gameScript.gameplayStudio.Length; num27++)
						{
							gameScript.gameplayStudio[num27] = portForArcade.gameplayStudio[num27];
						}
						for (int num28 = 0; num28 < gameScript.grafikStudio.Length; num28++)
						{
							gameScript.grafikStudio[num28] = portForArcade.grafikStudio[num28];
						}
						for (int num29 = 0; num29 < gameScript.soundStudio.Length; num29++)
						{
							gameScript.soundStudio[num29] = portForArcade.soundStudio[num29];
						}
						for (int num30 = 0; num30 < gameScript.motionCaptureStudio.Length; num30++)
						{
							gameScript.motionCaptureStudio[num30] = portForArcade.motionCaptureStudio[num30];
						}
						gameScript.gameTyp = 0;
						gameScript.gameLicence = -1;
						this.SetCopyProtect(gameScript);
						this.SetAntiCheat(gameScript);
						this.SetDesignSlider(gameScript);
						this.SetGameplayFeatures(gameScript);
						Debug.Log("NPC Create Arcade Game:" + gameScript.myID);
					}
				}
			}
			break;
		}
		if (!flag)
		{
			this.SetNewGameInWeeks(9999);
			if (gameScript)
			{
				UnityEngine.Object.Destroy(gameScript.gameObject);
				this.games_.FindGames();
			}
			return null;
		}
		if (this.tochterfirma && this.IsMyTochterfirma() && (gameScript.gameTyp == 0 || gameScript.gameTyp == 1) && !gameScript.arcade && !gameScript.handy && (gameScript.typ_standard || gameScript.typ_remaster || gameScript.typ_nachfolger || gameScript.typ_spinoff || gameScript.typ_budget || gameScript.typ_goty || gameScript.typ_addon || gameScript.typ_addonStandalone))
		{
			int reviewTotal = gameScript.reviewTotal;
			if (gameScript.reviewTotal <= 0)
			{
				gameScript.CalcReview(true);
				reviewTotal = gameScript.reviewTotal;
				gameScript.ClearReview();
			}
			if (!this.tf_autoRelease || (this.tf_autoRelease && this.tf_autoReleaseVal != 0 && reviewTotal >= this.tf_autoReleaseVal * 10))
			{
				base.StartCoroutine(this.iWaitTochterfirmaReleaseGame(gameScript, this));
				return null;
			}
		}
		this.ReleaseTheGame(gameScript, forceContractGame);
		return gameScript;
	}

	
	public void ReleaseTheGame(gameScript script_, bool forceContractGame)
	{
		if (this.publisher)
		{
			script_.publisherID = this.myID;
			script_.pS_ = this;
			script_.verkaufspreis[0] = UnityEngine.Random.Range(19, 39);
			if (script_.arcade)
			{
				script_.verkaufspreis[0] = UnityEngine.Random.Range(400, 800);
			}
			if (((this.mS_.globalEvent != 2 && !script_.sonderIP && !this.tochterfirma) || forceContractGame) && script_.engineID == 0 && script_.gameTyp == 0 && !script_.arcade && !script_.handy && !script_.retro && !script_.typ_remaster && !script_.typ_budget && !script_.typ_goty && !script_.typ_bundle && !script_.typ_addon && !script_.typ_addonStandalone && !script_.typ_mmoaddon && (script_.typ_standard || script_.typ_nachfolger || script_.typ_spinoff) && this.mS_.contractWorkMain_ && (UnityEngine.Random.Range(0, 100) > 80 || (UnityEngine.Random.Range(0, 100) > 50 && this.mS_.contractWorkMain_.anzContractGames < 3) || forceContractGame))
			{
				script_.auftragsspiel = true;
				script_.exklusiv = true;
				script_.gameLicence = -1;
				for (int i = 1; i < script_.gamePlatform.Length; i++)
				{
					script_.gamePlatform[i] = -1;
					script_.gamePlatformScript[i] = null;
				}
				this.nextGameAddon = false;
				this.nextGameMMOAddon = false;
			}
		}
		else if (!this.tochterfirma && this.mS_.pubOffersAmount < this.mS_.GetStudioLevel(this.mS_.studioPoints) / 2 + 1 && UnityEngine.Random.Range(0, 100) > 50 && script_.gameTyp == 0 && !script_.arcade && !script_.handy && !script_.typ_budget && !script_.typ_goty && !script_.typ_bundle && !script_.typ_addon && !script_.typ_addonStandalone && !script_.typ_mmoaddon && (script_.typ_standard || script_.typ_remaster || script_.typ_nachfolger || script_.typ_spinoff))
		{
			this.mS_.pubOffersAmount++;
			script_.pubAngebot = true;
			this.nextGameAddon = false;
			this.nextGameMMOAddon = false;
		}
		else
		{
			script_.FindPublisherForGame();
		}
		if (!script_.typ_budget && !script_.typ_goty && !script_.auftragsspiel)
		{
			script_.CalcReview(false);
			if (script_.reviewGameplay > 99)
			{
				script_.reviewGameplay = 99;
			}
			if (script_.reviewTotal > 99)
			{
				script_.reviewGameplay = 99;
			}
			this.reviewText_.GetReviewText(script_);
		}
		if (!script_.pubAngebot && !script_.auftragsspiel)
		{
			script_.SetOnMarket();
			bool flag = false;
			script_.FindMyPlatforms();
			for (int j = 0; j < script_.gamePlatformScript.Length; j++)
			{
				if (script_.gamePlatformScript[j] && script_.gamePlatformScript[j].playerConsole)
				{
					flag = true;
					break;
				}
			}
			if (this.mS_.newsSetting[0] || (this.mS_.newsSetting[9] && flag))
			{
				string text = this.tS_.GetText(494);
				if (script_.GetPublisherIsTochtefirma())
				{
					text = text.Replace("<NAME1>", "<color=green><b>" + script_.GetPublisherName() + "</b></color>");
				}
				else
				{
					text = text.Replace("<NAME1>", script_.GetPublisherName());
				}
				if (script_.GetDeveloperIsTochtefirma())
				{
					text = text.Replace("<NAME2>", "<color=green><b>" + script_.GetNameWithTag() + "</b></color>");
				}
				else
				{
					text = text.Replace("<NAME2>", script_.GetNameWithTag());
				}
				this.guiMain_.CreateTopNewsInfo(text);
				return;
			}
		}
		else
		{
			if (script_.pubAngebot)
			{
				script_.SetAsPublishingAngebot();
			}
			if (script_.auftragsspiel)
			{
				script_.SetAsContractGameAngebot();
			}
		}
	}

	
	private void SetMMOorF2P(gameScript script_, int platTyp)
	{
		if (platTyp == 0 || platTyp == 1 || platTyp == 5 || platTyp == 2)
		{
			int num = UnityEngine.Random.Range(0, 20);
			if (this.onlyMobile && UnityEngine.Random.Range(0, 100) > 50)
			{
				num = 1;
			}
			if (this.tochterfirma)
			{
				if (!this.tf_allowMMO && num == 0)
				{
					return;
				}
				if (!this.tf_allowF2P && num == 1)
				{
					return;
				}
				if (this.tf_onlyPlayerConsole && num == 1)
				{
					return;
				}
			}
			if (num != 0)
			{
				if (num != 1)
				{
					return;
				}
				if (this.publisher && this.unlock_.Get(22))
				{
					script_.gameTyp = 2;
					script_.exklusiv = false;
					script_.herstellerExklusiv = false;
					script_.verkaufspreis[0] = 0;
					script_.inAppPurchase[0] = true;
					script_.inAppPurchase[1] = true;
					script_.inAppPurchase[2] = true;
					script_.inAppPurchase[3] = true;
					if (UnityEngine.Random.Range(0, 100) > 50)
					{
						script_.inAppPurchase[4] = true;
					}
					if (UnityEngine.Random.Range(0, 100) > 50)
					{
						script_.inAppPurchase[5] = true;
					}
				}
			}
			else if (this.unlock_.Get(21))
			{
				script_.gameTyp = 1;
				script_.aboPreis = 5;
				return;
			}
		}
	}

	
	private int SetPlatformTyp(gameScript script_, bool forceContractGame)
	{
		int num = 0;
		if (UnityEngine.Random.Range(0, 100) < 20)
		{
			num = 1;
		}
		if (UnityEngine.Random.Range(0, 100) < 20 && this.publisher && this.unlock_.Get(65))
		{
			num = 5;
		}
		if (UnityEngine.Random.Range(0, 100) < 20 && this.publisher)
		{
			num = 4;
		}
		if (UnityEngine.Random.Range(0, 100) < 20 && this.mS_.year > 1995)
		{
			num = 3;
		}
		if (this.tochterfirma)
		{
			if (this.tf_noArcade && num == 4)
			{
				num = 0;
			}
			if (this.tf_noHandy && num == 5)
			{
				num = 0;
			}
			if (this.tf_noRetro && num == 3)
			{
				num = 0;
			}
		}
		if (this.onlyMobile && this.unlock_.Get(65))
		{
			num = 5;
		}
		if (script_.sonderIP && num == 3)
		{
			num = 0;
		}
		if (this.tochterfirma && this.tf_onlyPlayerConsole)
		{
			num = 1;
			if (UnityEngine.Random.Range(0, 100) > 50)
			{
				num = 2;
			}
		}
		if (forceContractGame)
		{
			num = 1;
		}
		if (num == 1)
		{
			script_.exklusiv = true;
		}
		if (num == 5)
		{
			script_.handy = true;
		}
		if (num == 4)
		{
			script_.arcade = true;
		}
		if (num == 3)
		{
			script_.retro = true;
		}
		if (num == 2)
		{
			script_.herstellerExklusiv = true;
		}
		return num;
	}

	
	private void SetGameSize(gameScript script_)
	{
		if (!this.tochterfirma)
		{
			if (this.mS_.year >= 1978 && (UnityEngine.Random.Range(0, 100) > 50 || script_.sonderIP))
			{
				script_.gameSize = 1;
			}
			if (this.mS_.year >= 1982 && (UnityEngine.Random.Range(0, 100) > 50 || script_.sonderIP))
			{
				script_.gameSize = 2;
			}
			if (!script_.retro && this.mS_.year >= 1986 && (UnityEngine.Random.Range(0, 100) > 50 || script_.sonderIP))
			{
				script_.gameSize = 3;
			}
			if (!script_.retro && this.mS_.year >= 1990 && (UnityEngine.Random.Range(0, 100) > 50 || script_.sonderIP))
			{
				script_.gameSize = 4;
				return;
			}
		}
		else if (this.tf_gameSize == 0)
		{
			if (this.mS_.year >= 1978 && (UnityEngine.Random.Range(0, 100) > 50 || script_.sonderIP))
			{
				script_.gameSize = 1;
			}
			if (this.mS_.year >= 1982 && (UnityEngine.Random.Range(0, 100) > 50 || script_.sonderIP))
			{
				script_.gameSize = 2;
			}
			if (!script_.retro && this.mS_.year >= 1986 && (UnityEngine.Random.Range(0, 100) > 50 || script_.sonderIP))
			{
				script_.gameSize = 3;
			}
			if (!script_.retro && this.mS_.year >= 1990 && (UnityEngine.Random.Range(0, 100) > 50 || script_.sonderIP))
			{
				script_.gameSize = 4;
				return;
			}
		}
		else
		{
			if (this.mS_.year >= 1978 && (UnityEngine.Random.Range(0, 100) > 50 || script_.sonderIP))
			{
				script_.gameSize = 1;
			}
			if (this.mS_.year >= 1982 && (UnityEngine.Random.Range(0, 100) > 50 || script_.sonderIP))
			{
				script_.gameSize = 2;
			}
			if (!script_.retro && this.mS_.year >= 1986 && (UnityEngine.Random.Range(0, 100) > 50 || script_.sonderIP))
			{
				script_.gameSize = 3;
			}
			if (!script_.retro && this.mS_.year >= 1990 && (UnityEngine.Random.Range(0, 100) > 50 || script_.sonderIP))
			{
				script_.gameSize = 4;
			}
			if (!script_.retro && !script_.sonderIP)
			{
				script_.gameSize = this.tf_gameSize - 1;
			}
		}
	}

	
	private void SetGameplayFeatures(gameScript script_)
	{
		script_.gameGameplayFeatures = new bool[this.gF_.gameplayFeatures_LEVEL.Length];
		script_.gameplayFeatures_DevDone = new bool[this.gF_.gameplayFeatures_LEVEL.Length];
		script_.gameGameplayFeatures[23] = true;
		script_.gameplayFeatures_DevDone[23] = true;
		script_.costs_entwicklung += (long)this.gF_.GetDevCosts(0);
		if (script_.gameTyp == 1 || script_.gameTyp == 2)
		{
			script_.gameGameplayFeatures[0] = true;
			script_.gameplayFeatures_DevDone[0] = true;
			script_.costs_entwicklung += (long)this.gF_.GetDevCosts(0);
		}
		int num = 0;
		int num2 = this.menuDevGame_.maxFeatures_gameSize[script_.gameSize];
		for (int i = 0; i < script_.gameGameplayFeatures.Length; i++)
		{
			if (this.gF_.gameplayFeatures_UNLOCK[i] && (UnityEngine.Random.Range(0, 100) > 30 || script_.sonderIP) && num < num2)
			{
				num++;
				script_.gameGameplayFeatures[i] = true;
				script_.gameplayFeatures_DevDone[i] = true;
				script_.points_gameplay += (float)this.gF_.GetGameplay(i, script_.maingenre);
				script_.points_grafik += (float)this.gF_.GetGraphic(i, script_.maingenre);
				script_.points_sound += (float)this.gF_.GetSound(i, script_.maingenre);
				script_.points_technik += (float)this.gF_.GetTechnik(i, script_.maingenre);
				script_.costs_entwicklung += (long)this.gF_.GetDevCosts(i);
				script_.costs_mitarbeiter += (long)UnityEngine.Random.Range(1000, 10000);
			}
		}
		if (script_.gameTyp != 0)
		{
			script_.costs_entwicklung += (long)(this.menuDevGame_.costs_gameTyp[script_.gameTyp] * (this.mS_.difficulty + 1));
		}
		else
		{
			script_.costs_entwicklung += (long)this.menuDevGame_.costs_gameTyp[script_.gameTyp];
		}
		if (script_.gameSize != 0)
		{
			script_.costs_entwicklung += (long)(this.menuDevGame_.costs_gameSize[script_.gameSize] * (this.mS_.difficulty + 1));
			return;
		}
		script_.costs_entwicklung += (long)this.menuDevGame_.costs_gameSize[script_.gameSize];
	}

	
	private void FindEngineForGameNew(gameScript gS_)
	{
		engineScript engineScript = null;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Engine");
		int num = 0;
		float num2 = 0f;
		engineScript engineScript2 = null;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				engineScript component = array[i].GetComponent<engineScript>();
				if (component && component.isUnlocked && !component.playerEngine && component.multiplayerSlot == -1)
				{
					if (num < component.preis)
					{
						num = component.preis;
						engineScript2 = component;
					}
					if (num2 < (float)component.gewinnbeteiligung)
					{
						num2 = (float)component.gewinnbeteiligung;
					}
				}
			}
		}
		num += 20000;
		num2 += 1f;
		for (int j = 0; j < array.Length; j++)
		{
			if (array[j])
			{
				engineScript component2 = array[j].GetComponent<engineScript>();
				if (component2)
				{
					if (component2.myID == 0)
					{
						engineScript = component2;
						if (UnityEngine.Random.Range(0, 100) < 50 || gS_.gameTyp != 0)
						{
							break;
						}
					}
					if (UnityEngine.Random.Range(0, 100) < 50)
					{
						bool flag = false;
						if (component2.playerEngine || component2.multiplayerSlot != -1)
						{
							flag = true;
						}
						if (((component2.isUnlocked && !flag) || (flag && !component2.updating && component2.Complete() && UnityEngine.Random.Range(0, this.mS_.difficulty) == 0)) && component2.GetTechLevel() >= engineScript.GetTechLevel() && UnityEngine.Random.Range(0, component2.GetFeaturesAmount()) > UnityEngine.Random.Range(0, engineScript.GetFeaturesAmount()) && ((component2.preis < UnityEngine.Random.Range(0, num) && (float)component2.gewinnbeteiligung < UnityEngine.Random.Range(0f, num2)) || !flag))
						{
							if (!flag)
							{
								engineScript = component2;
							}
							else if (component2.sellEngine)
							{
								engineScript = component2;
							}
						}
					}
				}
			}
		}
		if (this.tochterfirma && this.tf_engine != -1 && this.tf_engine != 0)
		{
			GameObject gameObject = GameObject.Find("ENGINE_" + this.tf_engine.ToString());
			if (gameObject)
			{
				engineScript = gameObject.GetComponent<engineScript>();
			}
		}
		if (engineScript)
		{
			gS_.engineID = engineScript.myID;
			gS_.engineS_ = engineScript;
			gS_.engineFeature_DevDone[0] = true;
			gS_.engineFeature_DevDone[1] = true;
			gS_.engineFeature_DevDone[2] = true;
			gS_.engineFeature_DevDone[3] = true;
			gS_.gameEngineFeature[0] = engineScript.GetBestFeature(this.eF_.GetTypGrafik());
			gS_.gameEngineFeature[1] = engineScript.GetBestFeature(this.eF_.GetTypSound());
			gS_.gameEngineFeature[2] = engineScript.GetBestFeature(this.eF_.GetTypKI());
			gS_.gameEngineFeature[3] = engineScript.GetBestFeature(this.eF_.GetTypPhysik());
			if (engineScript.myID == 0 && engineScript2)
			{
				gS_.gameEngineFeature[0] = engineScript2.GetBestFeature(this.eF_.GetTypGrafik());
				gS_.gameEngineFeature[1] = engineScript2.GetBestFeature(this.eF_.GetTypSound());
				gS_.gameEngineFeature[2] = engineScript2.GetBestFeature(this.eF_.GetTypKI());
				gS_.gameEngineFeature[3] = engineScript2.GetBestFeature(this.eF_.GetTypPhysik());
			}
			gS_.costs_entwicklung += (long)this.eF_.GetDevCosts(gS_.gameEngineFeature[0]);
			gS_.costs_entwicklung += (long)this.eF_.GetDevCosts(gS_.gameEngineFeature[1]);
			gS_.costs_entwicklung += (long)this.eF_.GetDevCosts(gS_.gameEngineFeature[2]);
			gS_.costs_entwicklung += (long)this.eF_.GetDevCosts(gS_.gameEngineFeature[3]);
			if (engineScript.playerEngine || engineScript.multiplayerSlot != -1)
			{
				engineScript.SellPlayerEngine(this);
			}
		}
	}

	
	private void FindPlatformsForGameNew(gameScript gS_)
	{
		int num = 0;
		int num2 = 0;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Platform");
		if (this.tochterfirma)
		{
			for (int n = 0; n < array.Length; n++)
			{
				if (array[n])
				{
					platformScript component = array[n].GetComponent<platformScript>();
					if (component && ((gS_.handy && component.typ == 3) || (gS_.arcade && component.typ == 4) || (!gS_.handy && !gS_.arcade && component.typ == 0) || (!gS_.handy && !gS_.arcade && component.typ == 1) || (!gS_.handy && !gS_.arcade && component.typ == 2)) && component.isUnlocked && component.playerConsole && ((gS_.retro && component.vomMarktGenommen) || (!gS_.retro && !component.vomMarktGenommen)) && (gS_.gameTyp == 0 || (gS_.gameTyp == 1 && component.internet) || (gS_.gameTyp == 2 && component.internet)))
					{
						num = component.myID;
						gS_.gamePlatform[num2] = component.myID;
						gS_.gamePlatformScript[num2] = component;
						gS_.costs_entwicklung += (long)component.dev_costs;
						if (gS_.exklusiv)
						{
							this.ClearPlatforms(gS_);
							return;
						}
						num2++;
						if (num2 >= 4)
						{
							this.ClearPlatforms(gS_);
							return;
						}
					}
				}
			}
		}
		if (this.tochterfirma && this.tf_onlyPlayerConsole)
		{
			if (num2 != 0)
			{
				if (num2 == 1 && gS_.herstellerExklusiv)
				{
					gS_.herstellerExklusiv = false;
					gS_.exklusiv = true;
				}
				this.ClearPlatforms(gS_);
				return;
			}
			if (gS_.herstellerExklusiv)
			{
				gS_.herstellerExklusiv = false;
				gS_.exklusiv = true;
			}
		}
		if (this.tochterfirma && !this.tf_onlyPlayerConsole && (this.tf_platformFocus[0] != -1 || this.tf_platformFocus[1] != -1 || this.tf_platformFocus[2] != -1 || this.tf_platformFocus[3] != -1))
		{
			for (int j = 0; j < array.Length; j++)
			{
				if (array[j])
				{
					platformScript component2 = array[j].GetComponent<platformScript>();
					if (component2 && (this.tf_platformFocus[0] == component2.myID || this.tf_platformFocus[1] == component2.myID || this.tf_platformFocus[2] == component2.myID || this.tf_platformFocus[3] == component2.myID) && ((gS_.handy && component2.typ == 3) || (gS_.arcade && component2.typ == 4) || (!gS_.handy && !gS_.arcade && component2.typ == 0) || (!gS_.handy && !gS_.arcade && component2.typ == 1) || (!gS_.handy && !gS_.arcade && component2.typ == 2)) && component2.isUnlocked && ((gS_.retro && component2.vomMarktGenommen) || (!gS_.retro && !component2.vomMarktGenommen)) && (gS_.gameTyp == 0 || (gS_.gameTyp == 1 && component2.internet) || (gS_.gameTyp == 2 && component2.internet)))
					{
						gS_.gamePlatform[num2] = component2.myID;
						gS_.gamePlatformScript[num2] = component2;
						gS_.costs_entwicklung += (long)component2.dev_costs;
						if (gS_.exklusiv)
						{
							this.ClearPlatforms(gS_);
							return;
						}
						num2++;
						if (num2 >= 4)
						{
							this.ClearPlatforms(gS_);
							return;
						}
					}
				}
			}
		}
		if (this.ownPlatform)
		{
			for (int k = 0; k < array.Length; k++)
			{
				if (array[k])
				{
					platformScript component3 = array[k].GetComponent<platformScript>();
					if (component3 && ((gS_.handy && component3.typ == 3) || (gS_.arcade && component3.typ == 4) || (!gS_.handy && !gS_.arcade && component3.typ == 0) || (!gS_.handy && !gS_.arcade && component3.typ == 1) || (!gS_.handy && !gS_.arcade && component3.typ == 2)) && component3.isUnlocked && ((gS_.retro && component3.vomMarktGenommen) || (!gS_.retro && !component3.vomMarktGenommen)) && (gS_.gameTyp == 0 || (gS_.gameTyp == 1 && component3.internet) || (gS_.gameTyp == 2 && component3.internet)) && component3.npc)
					{
						num = component3.myID;
						bool flag = false;
						if (component3.manufacturer_GE.Contains(this.name_GE))
						{
							flag = true;
						}
						if (flag)
						{
							gS_.gamePlatform[num2] = component3.myID;
							gS_.gamePlatformScript[num2] = component3;
							gS_.costs_entwicklung += (long)component3.dev_costs;
							if (gS_.exklusiv)
							{
								this.ClearPlatforms(gS_);
								return;
							}
							num2++;
							if (num2 >= 4)
							{
								this.ClearPlatforms(gS_);
								return;
							}
						}
					}
				}
			}
		}
		if ((!this.exklusive && num2 <= 3) || num2 == 0)
		{
			this.platformList.Clear();
			for (int l = 0; l < array.Length; l++)
			{
				if (array[l])
				{
					platformScript component4 = array[l].GetComponent<platformScript>();
					if (component4 && component4.isUnlocked && gS_.gamePlatform[0] != component4.myID && gS_.gamePlatform[1] != component4.myID && gS_.gamePlatform[2] != component4.myID && gS_.gamePlatform[3] != component4.myID && ((gS_.retro && component4.vomMarktGenommen) || (!gS_.retro && !component4.vomMarktGenommen)))
					{
						this.platformList.Add(new publisherScript.PlatformList(component4, component4.GetMarktanteil()));
					}
				}
			}
			this.platformList = (from i in this.platformList
			orderby i.marktanteil descending
			select i).ToList<publisherScript.PlatformList>();
			for (int m = 0; m < this.platformList.Count; m++)
			{
				platformScript script_ = this.platformList[m].script_;
				if (script_ && gS_.gamePlatform[0] != script_.myID && gS_.gamePlatform[1] != script_.myID && gS_.gamePlatform[2] != script_.myID && gS_.gamePlatform[3] != script_.myID && ((gS_.handy && script_.typ == 3) || (gS_.arcade && script_.typ == 4) || (!gS_.handy && !gS_.arcade && script_.typ == 0) || (!gS_.handy && !gS_.arcade && script_.typ == 1) || (!gS_.handy && !gS_.arcade && script_.typ == 2)) && script_.isUnlocked && ((gS_.retro && script_.vomMarktGenommen) || (!gS_.retro && !script_.vomMarktGenommen)) && (gS_.gameTyp == 0 || (gS_.gameTyp == 1 && script_.internet) || (gS_.gameTyp == 2 && script_.internet)) && (script_.npc || (script_.thridPartyGames && UnityEngine.Random.Range(0, script_.price) < UnityEngine.Random.Range(0, this.platforms_.GetDurchschnittsDevKitPreis()) && (UnityEngine.Random.Range(0f, 100f + script_.GetMarktanteil()) > 60f || script_.GetMarktanteil() > 30f))))
				{
					num = script_.myID;
					bool flag2 = false;
					if (this.ownPlatform && script_.manufacturer_GE.Contains(this.name_GE))
					{
						flag2 = true;
					}
					if (UnityEngine.Random.Range(0, 100) > 50 || script_.playerConsole || script_.multiplaySlot != -1 || flag2)
					{
						gS_.gamePlatform[num2] = script_.myID;
						gS_.gamePlatformScript[num2] = script_;
						gS_.costs_entwicklung += (long)script_.dev_costs;
						if (script_.playerConsole || script_.multiplaySlot != -1)
						{
							script_.SellPlayerKonsoleToNPC(this);
						}
						if (gS_.exklusiv)
						{
							this.ClearPlatforms(gS_);
							return;
						}
						num2++;
						if (num2 >= 4)
						{
							this.ClearPlatforms(gS_);
							return;
						}
					}
				}
			}
		}
		if (num2 == 0)
		{
			gS_.gamePlatform[0] = num;
		}
		this.ClearPlatforms(gS_);
	}

	
	private void ClearPlatforms(gameScript gS_)
	{
		if (gS_.arcade)
		{
			for (int i = 1; i < gS_.gamePlatform.Length; i++)
			{
				gS_.gamePlatform[i] = -1;
				gS_.gamePlatformScript[i] = null;
			}
		}
	}

	
	private void SetStudioAufwertungen(gameScript script_)
	{
		if (this.mS_.year > 1977)
		{
			for (int i = 0; i < script_.gameplayStudio.Length; i++)
			{
				if (UnityEngine.Random.Range(this.stars * 0.3f, 100f) > 50f || script_.sonderIP)
				{
					script_.gameplayStudio[i] = true;
				}
			}
		}
		if (this.mS_.year > 1983)
		{
			for (int j = 0; j < script_.grafikStudio.Length; j++)
			{
				if (UnityEngine.Random.Range(this.stars * 0.3f, 100f) > 50f || script_.sonderIP)
				{
					script_.grafikStudio[j] = true;
				}
			}
		}
		if (this.mS_.year > 1980)
		{
			for (int k = 0; k < script_.soundStudio.Length; k++)
			{
				if (UnityEngine.Random.Range(this.stars * 0.3f, 100f) > 50f || script_.sonderIP)
				{
					script_.soundStudio[k] = true;
				}
			}
		}
		if (this.unlock_.Get(8) && (UnityEngine.Random.Range(this.stars * 0.3f, 100f) > 50f || script_.sonderIP))
		{
			for (int l = 0; l < script_.motionCaptureStudio.Length; l++)
			{
				script_.motionCaptureStudio[l] = true;
			}
		}
	}

	
	private void SetLanguages(gameScript script_)
	{
		script_.gameLanguage[0] = true;
		for (int i = 0; i < script_.gameLanguage.Length; i++)
		{
			if (UnityEngine.Random.Range(0, 100) > 70 || script_.sonderIP)
			{
				script_.gameLanguage[i] = true;
				script_.costs_entwicklung += 5000L;
			}
		}
	}

	
	private void SetPoints(gameScript script_)
	{
		float num = this.stars * 0.1f;
		float num2 = 0f;
		for (int i = 0; i < script_.gameGameplayFeatures.Length; i++)
		{
			if (script_.gameGameplayFeatures[i])
			{
				num2 += (float)this.gF_.GetDevPoints(i);
				script_.points_gameplay += (float)this.gF_.gameplayFeatures_GAMEPLAY[i];
				script_.points_grafik += (float)this.gF_.gameplayFeatures_GRAPHIC[i];
				script_.points_sound += (float)this.gF_.gameplayFeatures_SOUND[i];
				script_.points_technik += (float)this.gF_.gameplayFeatures_TECHNIK[i];
			}
		}
		for (int j = 0; j < script_.gameplayStudio.Length; j++)
		{
			if (script_.gameplayStudio[j])
			{
				script_.points_gameplay += num2 * 0.1f * (num * 2f);
			}
		}
		for (int k = 0; k < script_.grafikStudio.Length; k++)
		{
			if (script_.grafikStudio[k])
			{
				script_.points_grafik += num2 * 0.1f * (num * 2f);
			}
		}
		for (int l = 0; l < script_.soundStudio.Length; l++)
		{
			if (script_.soundStudio[l])
			{
				script_.points_sound += num2 * 0.1f * (num * 2f);
			}
		}
		for (int m = 0; m < script_.motionCaptureStudio.Length; m++)
		{
			if (script_.motionCaptureStudio[m])
			{
				script_.points_technik += num2 * 0.1f * (num * 2f);
			}
		}
		script_.points_gameplay += UnityEngine.Random.Range(num2, num2 * num);
		script_.points_grafik += UnityEngine.Random.Range(num2, num2 * num);
		script_.points_sound += UnityEngine.Random.Range(num2, num2 * num);
		script_.points_technik += UnityEngine.Random.Range(num2, num2 * num);
		if (this.tochterfirma)
		{
			switch (this.tf_entwicklungsdauer)
			{
			case 0:
				script_.points_gameplay *= 0.5f;
				script_.points_grafik *= 0.5f;
				script_.points_sound *= 0.5f;
				script_.points_technik *= 0.5f;
				break;
			case 1:
				script_.points_gameplay *= 0.7f;
				script_.points_grafik *= 0.7f;
				script_.points_sound *= 0.7f;
				script_.points_technik *= 0.7f;
				break;
			}
		}
		if (UnityEngine.Random.Range(0, 100) > 10)
		{
			script_.points_gameplay *= 0.3f;
			script_.points_grafik *= 0.3f;
			script_.points_sound *= 0.3f;
			script_.points_technik *= 0.3f;
		}
		if (UnityEngine.Random.Range(0, 100) > 70 && this.stars < 70f && !script_.sonderIP && !this.tochterfirma)
		{
			script_.points_gameplay *= 0.3f;
			script_.points_grafik *= 0.3f;
			script_.points_sound *= 0.3f;
			script_.points_technik *= 0.3f;
		}
		script_.points_bugs = 0f;
		if (this.publisher && !script_.sonderIP && !this.tochterfirma && !script_.typ_addon && !script_.typ_addonStandalone && !script_.typ_budget && !script_.typ_bundle && !script_.typ_bundleAddon && !script_.typ_goty && !script_.typ_mmoaddon && ((!this.mS_.badGameThisYear && UnityEngine.Random.Range(0, 100) > 70) || (!this.mS_.badGameThisYear && this.mS_.month > 3)))
		{
			this.mS_.badGameThisYear = true;
			script_.points_bugs = (float)UnityEngine.Random.Range(250, 500);
			script_.points_gameplay *= 0.1f;
			script_.points_grafik *= 0.1f;
			script_.points_sound *= 0.1f;
			script_.points_technik *= 0.1f;
		}
		if (script_.points_gameplay > 99999f)
		{
			script_.points_gameplay = 99999f;
		}
		if (script_.points_grafik > 99999f)
		{
			script_.points_grafik = 99999f;
		}
		if (script_.points_sound > 99999f)
		{
			script_.points_sound = 99999f;
		}
		if (script_.points_technik > 99999f)
		{
			script_.points_technik = 99999f;
		}
		if (script_.points_gameplay < 1f)
		{
			script_.points_gameplay = 1f;
		}
		if (script_.points_grafik < 1f)
		{
			script_.points_grafik = 1f;
		}
		if (script_.points_sound < 1f)
		{
			script_.points_sound = 1f;
		}
		if (script_.points_technik < 1f)
		{
			script_.points_technik = 1f;
		}
	}

	
	private void SetDesignSlider(gameScript script_)
	{
		if (!script_.sonderIP)
		{
			for (int i = 0; i < script_.Designschwerpunkt.Length; i++)
			{
				script_.Designschwerpunkt[i] = this.genres_.genres_FOCUS[UnityEngine.Random.Range(0, this.genres_.genres_UNLOCK.Length), i];
			}
			for (int j = 0; j < script_.Designausrichtung.Length; j++)
			{
				script_.Designausrichtung[j] = UnityEngine.Random.Range(0, 11);
			}
			return;
		}
		for (int k = 0; k < script_.Designschwerpunkt.Length; k++)
		{
			script_.Designschwerpunkt[k] = this.genres_.genres_FOCUS[script_.maingenre, k];
		}
		for (int l = 0; l < script_.Designausrichtung.Length; l++)
		{
			script_.Designausrichtung[l] = UnityEngine.Random.Range(0, 11);
		}
	}

	
	private void SetGenre(gameScript script_)
	{
		script_.maingenre = -1;
		script_.subgenre = -1;
		int num = 0;
		bool flag = false;
		while (!flag)
		{
			int num2 = UnityEngine.Random.Range(0, this.genres_.genres_UNLOCK.Length);
			if (this.genres_.genres_UNLOCK[num2])
			{
				if (UnityEngine.Random.Range(0, 100) > 70 && this.genres_.genres_UNLOCK[this.fanGenre])
				{
					num2 = this.fanGenre;
				}
				script_.maingenre = num2;
				break;
			}
			num++;
			if (num > 10000)
			{
				break;
			}
		}
		if (this.tochterfirma && this.tf_gameGenre != 0 && this.genres_.genres_UNLOCK[this.tf_gameGenre - 1])
		{
			script_.maingenre = this.tf_gameGenre - 1;
		}
		if (UnityEngine.Random.Range(0, 100) > 30)
		{
			num = 0;
			flag = false;
			while (!flag)
			{
				int num3 = UnityEngine.Random.Range(0, this.genres_.genres_UNLOCK.Length);
				if (num3 != script_.maingenre && this.genres_.genres_UNLOCK[num3])
				{
					script_.subgenre = num3;
					return;
				}
				num++;
				if (num > 10000)
				{
					return;
				}
			}
		}
	}

	
	private void SetCopyProtect(gameScript script_)
	{
		if (script_.arcade)
		{
			script_.gameCopyProtect = -1;
			return;
		}
		float num = 0f;
		int num2 = -1;
		GameObject[] array = GameObject.FindGameObjectsWithTag("CopyProtect");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				copyProtectScript component = array[i].GetComponent<copyProtectScript>();
				if (component && component.isUnlocked && component.effekt > num)
				{
					num2 = component.myID;
					num = component.effekt;
				}
			}
		}
		if (num2 != -1)
		{
			script_.gameCopyProtect = num2;
		}
	}

	
	private void SetAntiCheat(gameScript script_)
	{
		if (script_.arcade)
		{
			script_.gameAntiCheat = -1;
			return;
		}
		float num = 0f;
		int num2 = -1;
		GameObject[] array = GameObject.FindGameObjectsWithTag("AntiCheat");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				antiCheatScript component = array[i].GetComponent<antiCheatScript>();
				if (component && component.isUnlocked && component.effekt > num)
				{
					num2 = component.myID;
					num = component.effekt;
				}
			}
		}
		if (num2 != -1)
		{
			script_.gameAntiCheat = num2;
		}
	}

	
	private void SetTheme(gameScript script_)
	{
		for (int i = 0; i < 500; i++)
		{
			script_.gameMainTheme = UnityEngine.Random.Range(0, this.themes_.themes_LEVEL.Length);
			if (this.themes_.IsThemesFitWithGenre(script_.gameMainTheme, script_.maingenre))
			{
				break;
			}
		}
		if (this.tochterfirma && this.tf_gameTopic != -1)
		{
			script_.gameMainTheme = this.tf_gameTopic;
		}
		if (UnityEngine.Random.Range(0, 100) > 50)
		{
			for (int j = 0; j < 500; j++)
			{
				script_.gameSubTheme = UnityEngine.Random.Range(0, this.themes_.themes_LEVEL.Length);
				if (this.themes_.IsThemesFitWithGenre(script_.gameSubTheme, script_.maingenre))
				{
					break;
				}
			}
			if (script_.gameMainTheme == script_.gameSubTheme)
			{
				script_.gameSubTheme = -1;
				return;
			}
		}
		else
		{
			script_.gameSubTheme = -1;
		}
	}

	
	private void SetLicence(gameScript script_)
	{
		if (UnityEngine.Random.Range(0, 100) < 5)
		{
			script_.gameLicence = UnityEngine.Random.RandomRange(0, this.licences_.licence_QUALITY.Length);
		}
	}

	
	private bool IpFokusCheck(gameScript script_)
	{
		if (!this.tochterfirma)
		{
			return true;
		}
		if (this.tf_ipFocus[0] == -1 && this.tf_ipFocus[1] == -1 && this.tf_ipFocus[2] == -1)
		{
			return true;
		}
		for (int i = 0; i < this.tf_ipFocus.Length; i++)
		{
			if (script_.mainIP == this.tf_ipFocus[i])
			{
				return true;
			}
		}
		return false;
	}

	
	private gameScript GetSpinoff()
	{
		List<gameScript> list = new List<gameScript>();
		for (int i = 0; i < this.games_.arrayGamesScripts.Length; i++)
		{
			if (this.games_.arrayGamesScripts[i] && this.games_.arrayGamesScripts[i].IsMyIP(this) && this.IpFokusCheck(this.games_.arrayGamesScripts[i]) && !this.games_.arrayGamesScripts[i].inDevelopment && this.games_.arrayGamesScripts[i].portID == -1 && !this.games_.arrayGamesScripts[i].pubAngebot && !this.games_.arrayGamesScripts[i].auftragsspiel && !this.games_.arrayGamesScripts[i].isOnMarket && this.games_.arrayGamesScripts[i].sellsTotal > 1000L && !this.games_.arrayGamesScripts[i].nachfolger_created && !this.games_.arrayGamesScripts[i].sonderIP && this.games_.arrayGamesScripts[i].typ_standard && this.games_.arrayGamesScripts[i].mainIP == this.games_.arrayGamesScripts[i].myID)
			{
				list.Add(this.games_.arrayGamesScripts[i]);
				if (this.games_.arrayGamesScripts[i].reviewTotal > 70)
				{
					list.Add(this.games_.arrayGamesScripts[i]);
				}
				if (this.games_.arrayGamesScripts[i].reviewTotal > 80)
				{
					list.Add(this.games_.arrayGamesScripts[i]);
				}
				if (this.games_.arrayGamesScripts[i].reviewTotal > 85)
				{
					list.Add(this.games_.arrayGamesScripts[i]);
				}
				if (this.games_.arrayGamesScripts[i].reviewTotal > 90)
				{
					list.Add(this.games_.arrayGamesScripts[i]);
				}
			}
		}
		if (list.Count > 0)
		{
			return list[UnityEngine.Random.Range(0, list.Count)];
		}
		return null;
	}

	
	private gameScript GetGameForNachfolger()
	{
		List<gameScript> list = new List<gameScript>();
		for (int i = 0; i < this.games_.arrayGamesScripts.Length; i++)
		{
			if (this.games_.arrayGamesScripts[i] && this.games_.arrayGamesScripts[i].IsMyIP(this) && this.IpFokusCheck(this.games_.arrayGamesScripts[i]) && !this.games_.arrayGamesScripts[i].inDevelopment && !this.games_.arrayGamesScripts[i].nachfolger_created && this.games_.arrayGamesScripts[i].portID == -1 && !this.games_.arrayGamesScripts[i].pubAngebot && !this.games_.arrayGamesScripts[i].auftragsspiel && !this.games_.arrayGamesScripts[i].isOnMarket && this.games_.arrayGamesScripts[i].date_start_year != this.mS_.year && (this.games_.arrayGamesScripts[i].typ_standard || this.games_.arrayGamesScripts[i].typ_nachfolger || this.games_.arrayGamesScripts[i].typ_spinoff))
			{
				list.Add(this.games_.arrayGamesScripts[i]);
				if (this.games_.arrayGamesScripts[i].reviewTotal > 70)
				{
					list.Add(this.games_.arrayGamesScripts[i]);
				}
				if (this.games_.arrayGamesScripts[i].reviewTotal > 80)
				{
					list.Add(this.games_.arrayGamesScripts[i]);
				}
				if (this.games_.arrayGamesScripts[i].reviewTotal > 85)
				{
					list.Add(this.games_.arrayGamesScripts[i]);
				}
				if (this.games_.arrayGamesScripts[i].reviewTotal > 90)
				{
					list.Add(this.games_.arrayGamesScripts[i]);
				}
				if (this.games_.arrayGamesScripts[i].sonderIP)
				{
					list.Add(this.games_.arrayGamesScripts[i]);
					list.Add(this.games_.arrayGamesScripts[i]);
					list.Add(this.games_.arrayGamesScripts[i]);
					list.Add(this.games_.arrayGamesScripts[i]);
				}
			}
		}
		if (list.Count > 0)
		{
			return list[UnityEngine.Random.Range(0, list.Count)];
		}
		return null;
	}

	
	private gameScript GetRemaster()
	{
		for (int i = 0; i < this.games_.arrayGamesScripts.Length; i++)
		{
			if (this.games_.arrayGamesScripts[i] && this.games_.arrayGamesScripts[i].IsMyIP(this) && this.IpFokusCheck(this.games_.arrayGamesScripts[i]) && !this.games_.arrayGamesScripts[i].inDevelopment && this.games_.arrayGamesScripts[i].portID == -1 && !this.games_.arrayGamesScripts[i].pubAngebot && !this.games_.arrayGamesScripts[i].auftragsspiel && this.games_.arrayGamesScripts[i].gameTyp == 0 && !this.games_.arrayGamesScripts[i].isOnMarket && this.games_.arrayGamesScripts[i].sellsTotal > 1000L && !this.games_.arrayGamesScripts[i].remaster_created && this.mS_.year - this.games_.arrayGamesScripts[i].date_year >= 3 && (this.games_.arrayGamesScripts[i].typ_standard || this.games_.arrayGamesScripts[i].typ_nachfolger || this.games_.arrayGamesScripts[i].typ_spinoff) && !this.games_.arrayGamesScripts[i].handy && !this.games_.arrayGamesScripts[i].arcade)
			{
				return this.games_.arrayGamesScripts[i];
			}
		}
		return null;
	}

	
	private gameScript GetGOTY()
	{
		for (int i = 0; i < this.games_.arrayGamesScripts.Length; i++)
		{
			if (this.games_.arrayGamesScripts[i] && this.games_.arrayGamesScripts[i].IsMyIP(this) && this.IpFokusCheck(this.games_.arrayGamesScripts[i]) && !this.games_.arrayGamesScripts[i].inDevelopment && this.games_.arrayGamesScripts[i].portID == -1 && !this.games_.arrayGamesScripts[i].pubAngebot && !this.games_.arrayGamesScripts[i].auftragsspiel && this.games_.arrayGamesScripts[i].gameTyp == 0 && !this.games_.arrayGamesScripts[i].isOnMarket && this.games_.arrayGamesScripts[i].sellsTotal > 1000L && !this.games_.arrayGamesScripts[i].goty_created && this.games_.arrayGamesScripts[i].goty && (this.games_.arrayGamesScripts[i].typ_standard || this.games_.arrayGamesScripts[i].typ_nachfolger || this.games_.arrayGamesScripts[i].typ_spinoff || this.games_.arrayGamesScripts[i].typ_remaster) && !this.games_.arrayGamesScripts[i].handy && !this.games_.arrayGamesScripts[i].arcade)
			{
				return this.games_.arrayGamesScripts[i];
			}
		}
		return null;
	}

	
	private gameScript GetPortForHandy()
	{
		for (int i = 0; i < this.games_.arrayGamesScripts.Length; i++)
		{
			if (this.games_.arrayGamesScripts[i] && this.games_.arrayGamesScripts[i].IsMyIP(this) && this.IpFokusCheck(this.games_.arrayGamesScripts[i]) && !this.games_.arrayGamesScripts[i].inDevelopment && this.games_.arrayGamesScripts[i].portID == -1 && !this.games_.arrayGamesScripts[i].pubAngebot && !this.games_.arrayGamesScripts[i].auftragsspiel && this.games_.arrayGamesScripts[i].gameTyp == 0 && this.games_.arrayGamesScripts[i].sellsTotal > 1000L && !this.games_.arrayGamesScripts[i].portExist[1] && (this.games_.arrayGamesScripts[i].typ_standard || this.games_.arrayGamesScripts[i].typ_nachfolger || this.games_.arrayGamesScripts[i].typ_spinoff || this.games_.arrayGamesScripts[i].typ_remaster) && !this.games_.arrayGamesScripts[i].handy && !this.games_.arrayGamesScripts[i].arcade)
			{
				return this.games_.arrayGamesScripts[i];
			}
		}
		return null;
	}

	
	private gameScript GetPortForArcade()
	{
		for (int i = 0; i < this.games_.arrayGamesScripts.Length; i++)
		{
			if (this.games_.arrayGamesScripts[i] && this.games_.arrayGamesScripts[i].IsMyIP(this) && this.IpFokusCheck(this.games_.arrayGamesScripts[i]) && !this.games_.arrayGamesScripts[i].inDevelopment && this.games_.arrayGamesScripts[i].portID == -1 && !this.games_.arrayGamesScripts[i].pubAngebot && !this.games_.arrayGamesScripts[i].auftragsspiel && this.games_.arrayGamesScripts[i].gameTyp == 0 && this.games_.arrayGamesScripts[i].sellsTotal > 1000L && !this.games_.arrayGamesScripts[i].portExist[2] && (this.games_.arrayGamesScripts[i].typ_standard || this.games_.arrayGamesScripts[i].typ_nachfolger || this.games_.arrayGamesScripts[i].typ_spinoff || this.games_.arrayGamesScripts[i].typ_remaster) && !this.games_.arrayGamesScripts[i].handy && !this.games_.arrayGamesScripts[i].arcade)
			{
				return this.games_.arrayGamesScripts[i];
			}
		}
		return null;
	}

	
	private gameScript GetGameForBudget()
	{
		for (int i = 0; i < this.games_.arrayGamesScripts.Length; i++)
		{
			if (this.games_.arrayGamesScripts[i] && this.games_.arrayGamesScripts[i].IsMyIP(this) && this.IpFokusCheck(this.games_.arrayGamesScripts[i]) && !this.games_.arrayGamesScripts[i].inDevelopment && this.games_.arrayGamesScripts[i].portID == -1 && !this.games_.arrayGamesScripts[i].pubAngebot && !this.games_.arrayGamesScripts[i].auftragsspiel && this.games_.arrayGamesScripts[i].gameTyp == 0 && !this.games_.arrayGamesScripts[i].isOnMarket && this.games_.arrayGamesScripts[i].sellsTotal > 1000L && !this.games_.arrayGamesScripts[i].budget_created && this.mS_.year - this.games_.arrayGamesScripts[i].date_year >= 2 && (this.games_.arrayGamesScripts[i].typ_standard || this.games_.arrayGamesScripts[i].typ_nachfolger || this.games_.arrayGamesScripts[i].typ_spinoff || this.games_.arrayGamesScripts[i].typ_remaster) && !this.games_.arrayGamesScripts[i].handy && !this.games_.arrayGamesScripts[i].arcade)
			{
				return this.games_.arrayGamesScripts[i];
			}
		}
		return null;
	}

	
	private gameScript GetAddon()
	{
		for (int i = 0; i < this.games_.arrayGamesScripts.Length; i++)
		{
			if (this.games_.arrayGamesScripts[i] && this.games_.arrayGamesScripts[i].IsMyIP(this) && this.IpFokusCheck(this.games_.arrayGamesScripts[i]) && !this.games_.arrayGamesScripts[i].inDevelopment && this.games_.arrayGamesScripts[i].portID == -1 && !this.games_.arrayGamesScripts[i].pubAngebot && !this.games_.arrayGamesScripts[i].auftragsspiel && this.games_.arrayGamesScripts[i].gameTyp == 0 && this.games_.arrayGamesScripts[i].isOnMarket && this.games_.arrayGamesScripts[i].sellsTotal > 1000L && this.games_.arrayGamesScripts[i].amountAddons <= 0 && (this.games_.arrayGamesScripts[i].typ_standard || this.games_.arrayGamesScripts[i].typ_nachfolger || this.games_.arrayGamesScripts[i].typ_spinoff || this.games_.arrayGamesScripts[i].typ_remaster) && !this.games_.arrayGamesScripts[i].arcade)
			{
				return this.games_.arrayGamesScripts[i];
			}
		}
		return null;
	}

	
	private gameScript GetMMOAddon()
	{
		for (int i = 0; i < this.games_.arrayGamesScripts.Length; i++)
		{
			if (this.games_.arrayGamesScripts[i] && this.games_.arrayGamesScripts[i].IsMyIP(this) && this.IpFokusCheck(this.games_.arrayGamesScripts[i]) && !this.games_.arrayGamesScripts[i].inDevelopment && this.games_.arrayGamesScripts[i].portID == -1 && !this.games_.arrayGamesScripts[i].pubAngebot && !this.games_.arrayGamesScripts[i].auftragsspiel && this.games_.arrayGamesScripts[i].gameTyp == 1 && this.games_.arrayGamesScripts[i].isOnMarket && this.games_.arrayGamesScripts[i].sellsTotal > 1000L && this.games_.arrayGamesScripts[i].amountAddons <= 0 && (this.games_.arrayGamesScripts[i].typ_standard || this.games_.arrayGamesScripts[i].typ_nachfolger || this.games_.arrayGamesScripts[i].typ_spinoff || this.games_.arrayGamesScripts[i].typ_remaster) && !this.games_.arrayGamesScripts[i].handy && !this.games_.arrayGamesScripts[i].arcade)
			{
				return this.games_.arrayGamesScripts[i];
			}
		}
		return null;
	}

	
	public void UpdateRandomData()
	{
		if (!this.isUnlocked)
		{
			return;
		}
		if (this.lockToBuy > 0)
		{
			this.lockToBuy--;
			if (this.lockToBuy < 0)
			{
				this.lockToBuy = 0;
			}
		}
		if (UnityEngine.Random.Range(0, 20) == 1)
		{
			this.exklusivLaufzeit = 12 * UnityEngine.Random.Range(2, 11);
			if (!this.tochterfirma)
			{
				int num = UnityEngine.Random.Range(0, this.genres_.genres_LEVEL.Length);
				if (this.genres_.genres_UNLOCK[num])
				{
					this.fanGenre = num;
				}
			}
			else if (this.tf_gameGenre != 0)
			{
				this.fanGenre = this.tf_gameGenre - 1;
			}
			if (this.mS_.multiplayer && this.mS_.mpCalls_.isServer)
			{
				this.mS_.mpCalls_.SERVER_Send_Publisher(this);
			}
		}
	}

	
	public bool TochterfirmaGeschlossen()
	{
		return this.tochterfirma && (!this.tochterfirma || this.tf_geschlossen) && (this.tochterfirma && this.tf_geschlossen);
	}

	
	public int GetMoneyExklusiv()
	{
		return Mathf.RoundToInt((float)((this.mS_.year - 1975) * 250000) * (this.stars / 100f) / 120f * (float)this.exklusivLaufzeit);
	}

	
	public int GetShareExklusiv()
	{
		return Mathf.RoundToInt(this.share * 1.5f);
	}

	
	public int GetShare()
	{
		if (this.IsMyTochterfirma())
		{
			return 15;
		}
		return Mathf.RoundToInt(this.share);
	}

	
	public string GetDeveloperPublisherString()
	{
		if (this.developer && !this.publisher)
		{
			return this.tS_.GetText(274);
		}
		if (!this.developer && this.publisher)
		{
			return this.tS_.GetText(432);
		}
		if (this.developer && this.publisher)
		{
			return this.tS_.GetText(432) + " & " + this.tS_.GetText(274);
		}
		return "";
	}

	
	public int GetStarsAmount()
	{
		return Mathf.RoundToInt(this.stars / 20f);
	}

	
	public string GetTooltip()
	{
		string text = "<b><size=18>" + this.GetName() + "</size></b>";
		if (this.IsMyTochterfirma())
		{
			text = text + "\n<b><size=15><color=green>" + this.tS_.GetText(1924) + "</color></size></b>";
		}
		text = text + "\n<b>" + this.GetDeveloperPublisherString() + "</b>";
		text = text + "\n<b>" + this.GetDateString() + "</b>";
		if (this.publisher)
		{
			text = text + "\n\n" + this.tS_.GetText(434) + ": <color=blue><size=20>";
			for (int i = 0; i < Mathf.RoundToInt(this.stars / 20f); i++)
			{
				text += "★";
			}
			text += "</size></color>";
			text = string.Concat(new string[]
			{
				text,
				"\n",
				this.tS_.GetText(437),
				": <color=blue>",
				this.genres_.GetName(this.fanGenre),
				"</color>"
			});
			text = string.Concat(new string[]
			{
				text,
				"\n",
				this.tS_.GetText(1745),
				": <color=blue>",
				this.mS_.GetMoney((long)Mathf.RoundToInt((float)this.GetAmountVertriebeneSpiele()), false),
				"</color>"
			});
			text = string.Concat(new string[]
			{
				text,
				"\n",
				this.tS_.GetText(435),
				": <color=blue>",
				this.mS_.Round(this.GetRelation(), 2).ToString(),
				"</color>"
			});
			if (!this.IsMyTochterfirma())
			{
				text = string.Concat(new string[]
				{
					text,
					"\n",
					this.tS_.GetText(436),
					": <color=blue>",
					this.mS_.GetMoney((long)Mathf.RoundToInt(this.share), true),
					"</color>"
				});
			}
			else
			{
				text = string.Concat(new string[]
				{
					text,
					"\n",
					this.tS_.GetText(436),
					": <color=green><b>",
					this.mS_.GetMoney((long)this.GetShare(), true),
					"</b></color><color=blue> [",
					this.mS_.GetMoney((long)Mathf.RoundToInt(this.share), true),
					"]</color>"
				});
			}
		}
		else
		{
			text = text + "\n\n" + this.tS_.GetText(434) + ": <color=blue><size=20>";
			for (int j = 0; j < Mathf.RoundToInt(this.stars / 20f); j++)
			{
				text += "★";
			}
			text += "</size></color>";
			text = string.Concat(new string[]
			{
				text,
				"\n",
				this.tS_.GetText(271),
				": <color=blue>",
				this.mS_.GetMoney((long)Mathf.RoundToInt((float)this.GetAmountGames()), false),
				"</color>"
			});
		}
		if (this.tf_geschlossen)
		{
			text = text + "\n\n<b><color=red>" + this.tS_.GetText(1932) + "</color></b>";
		}
		return text;
	}

	
	public string GetDateString()
	{
		return this.tS_.GetText(this.date_month - 1 + 221) + " " + this.date_year.ToString();
	}

	
	public int GetAmountGames()
	{
		int num = 0;
		for (int i = 0; i < this.games_.arrayGamesScripts.Length; i++)
		{
			if (this.games_.arrayGamesScripts[i] && this.games_.arrayGamesScripts[i].developerID == this.myID && !this.games_.arrayGamesScripts[i].pubAngebot && !this.games_.arrayGamesScripts[i].auftragsspiel)
			{
				num++;
			}
		}
		return num;
	}

	
	public int GetAmountVertriebeneSpiele()
	{
		int num = 0;
		if (this.games_)
		{
			for (int i = 0; i < this.games_.arrayGamesScripts.Length; i++)
			{
				if (this.games_.arrayGamesScripts[i] && this.games_.arrayGamesScripts[i].publisherID == this.myID && !this.games_.arrayGamesScripts[i].pubAngebot && !this.games_.arrayGamesScripts[i].auftragsspiel)
				{
					num++;
				}
			}
		}
		return num;
	}

	
	public void ResetDataForAuftragsspiel(gameScript script_)
	{
		this.SetTheme(script_);
		this.SetDesignSlider(script_);
		this.SetLanguages(script_);
		this.SetStudioAufwertungen(script_);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().SetMGSR_Result(script_, script_.Designausrichtung[1]);
		this.FindPlatformsForGameNew(script_);
		this.FindEngineForGameNew(script_);
		this.SetGameplayFeatures(script_);
		this.SetPoints(script_);
	}

	
	private IEnumerator iWaitTochterfirmaReleaseGame(gameScript s1_, publisherScript s2_)
	{
		bool done = false;
		while (!done)
		{
			if (s1_ && !this.guiMain_.uiObjects[397].activeSelf)
			{
				done = true;
				this.guiMain_.OpenMenu(false);
				this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[397]);
				this.guiMain_.uiObjects[397].GetComponent<Menu_Dev_TochterfirmaGameComplete>().Init(s1_, s2_);
			}
			yield return null;
		}
		yield break;
	}

	
	public void AddTochterfirmaUmsatz(long l)
	{
		this.tf_umsatz[0] += l;
	}

	
	public void UpdateTochterfirmaUmsatzVerlauf()
	{
		for (int i = this.tf_umsatz.Length - 1; i > 0; i--)
		{
			this.tf_umsatz[i] = this.tf_umsatz[i - 1];
		}
		this.tf_umsatz[0] = 0L;
	}

	
	public mainScript mS_;

	
	public GameObject main_;

	
	public textScript tS_;

	
	public settingsScript settings_;

	
	public GUI_Main guiMain_;

	
	public genres genres_;

	
	public games games_;

	
	public gameplayFeatures gF_;

	
	public engineFeatures eF_;

	
	public unlockScript unlock_;

	
	public reviewText reviewText_;

	
	public platforms platforms_;

	
	public forschungSonstiges fS_;

	
	public Menu_DevGame menuDevGame_;

	
	public licences licences_;

	
	public themes themes_;

	
	public int myID;

	
	public bool isUnlocked;

	
	public string name_EN;

	
	public string name_GE;

	
	public string name_TU;

	
	public string name_CH;

	
	public string name_FR;

	
	public string name_JA;

	
	public int date_year;

	
	public int date_month;

	
	public float stars;

	
	public int logoID;

	
	public bool developer;

	
	public bool publisher;

	
	public float relation;

	
	public float share;

	
	public int fanGenre;

	
	public int newGameInWeeks;

	
	public int newGameInWeeksORG;

	
	public int exklusivLaufzeit;

	
	public bool onlyMobile;

	
	public bool ownPlatform;

	
	public bool exklusive;

	
	public int developmentSpeed;

	
	public long firmenwert;

	
	public bool notForSale;

	
	public bool tochterfirma;

	
	public int lockToBuy;

	
	public int multiplayerID;

	
	public bool tf_geschlossen;

	
	public bool tf_autoRelease;

	
	public bool tf_onlyPlayerConsole;

	
	public bool tf_allowMMO;

	
	public bool tf_allowF2P;

	
	public bool tf_allowAddon;

	
	public bool tf_noArcade;

	
	public bool tf_noHandy;

	
	public bool tf_noRetro;

	
	public bool tf_noPorts;

	
	public bool tf_noBudget;

	
	public bool tf_noGOTY;

	
	public bool tf_noRemaster;

	
	public bool tf_noSpinoffs;

	
	public bool tf_publisher;

	
	public bool tf_developer;

	
	public int tf_entwicklungsdauer;

	
	public int tf_gameSize;

	
	public int tf_gameGenre;

	
	public long[] tf_umsatz;

	
	public bool tf_ownPublisher;

	
	public int tf_gameTopic;

	
	public int[] tf_ipFocus;

	
	public int tf_engine;

	
	public int tf_autoReleaseVal;

	
	public int[] tf_platformFocus;

	
	private bool nextGameAddon;

	
	private bool nextGameMMOAddon;

	
	public List<publisherScript.PlatformList> platformList = new List<publisherScript.PlatformList>();

	
	public class PlatformList
	{
		
		public PlatformList(platformScript s_, float markt)
		{
			this.script_ = s_;
			this.marktanteil = markt;
		}

		
		public platformScript script_;

		
		public float marktanteil;
	}
}
