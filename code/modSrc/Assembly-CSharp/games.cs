using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000056 RID: 86
public class games : MonoBehaviour
{
	// Token: 0x060002A2 RID: 674 RVA: 0x0002A20B File Offset: 0x0002840B
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060002A3 RID: 675 RVA: 0x0002A214 File Offset: 0x00028414
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
		if (!this.poM_)
		{
			this.poM_ = this.main_.GetComponent<publishingOfferMain>();
		}
	}

	// Token: 0x060002A4 RID: 676 RVA: 0x0002A3B4 File Offset: 0x000285B4
	public float GetGrundkosten()
	{
		float num = (float)this.mS_.difficulty;
		num *= 0.5f;
		return 2.5f + num;
	}

	// Token: 0x060002A5 RID: 677 RVA: 0x0002A3E0 File Offset: 0x000285E0
	public float GetDigitalSells()
	{
		if (this.unlock_.Get(59))
		{
			float num = (float)this.mS_.PassedMonth();
			if (num > 600f)
			{
				num = 600f;
			}
			return this.curveSellsDigital.Evaluate(num / 600f);
		}
		return 0f;
	}

	// Token: 0x060002A6 RID: 678 RVA: 0x0002A430 File Offset: 0x00028630
	public float GetSells()
	{
		float num = (float)this.mS_.PassedMonth();
		if (num > 600f)
		{
			num = 600f;
		}
		return this.curveSells.Evaluate(num / 600f) * 30f;
	}

	// Token: 0x060002A7 RID: 679 RVA: 0x0002A470 File Offset: 0x00028670
	public float GetSellsArcade()
	{
		float num = (float)this.mS_.PassedMonth();
		if (num > 600f)
		{
			num = 600f;
		}
		return this.curveSellsArcade.Evaluate(num / 600f) * 30f;
	}

	// Token: 0x060002A8 RID: 680 RVA: 0x0002A4B0 File Offset: 0x000286B0
	public float GetReviewCurve()
	{
		float num = (float)this.mS_.PassedMonth();
		if (num > 600f)
		{
			num = 600f;
		}
		return this.curveReview.Evaluate(num / 600f);
	}

	// Token: 0x060002A9 RID: 681 RVA: 0x0002A4EC File Offset: 0x000286EC
	public float GetArcadeCurve()
	{
		float num = (float)this.mS_.PassedMonth();
		num = Mathf.PingPong(num, 600f);
		return this.curveSellsArcade.Evaluate(num / 600f);
	}

	// Token: 0x060002AA RID: 682 RVA: 0x0002A524 File Offset: 0x00028724
	public float GetDeluxeCurve()
	{
		float num = (float)this.mS_.PassedMonth();
		num = Mathf.PingPong(num, 600f);
		return this.curveSellsDeluxe.Evaluate(num / 600f);
	}

	// Token: 0x060002AB RID: 683 RVA: 0x0002A55C File Offset: 0x0002875C
	public float GetCollectorsCurve()
	{
		float num = (float)this.mS_.PassedMonth();
		num = Mathf.PingPong(num, 600f);
		return this.curveSellsCollectors.Evaluate(num / 600f);
	}

	// Token: 0x060002AC RID: 684 RVA: 0x0002A594 File Offset: 0x00028794
	public gameScript CreateNewGame(bool fromSavegame, bool setDate)
	{
		gameScript component = UnityEngine.Object.Instantiate<GameObject>(this.prefabGame).GetComponent<gameScript>();
		if (!fromSavegame)
		{
			component.myID = this.GetNewID();
			component.SetGameObjectName();
		}
		component.main_ = this.main_;
		component.mS_ = this.mS_;
		component.tS_ = this.tS_;
		component.eF_ = this.eF_;
		component.genres_ = this.genres_;
		component.guiMain_ = this.guiMain_;
		component.sfx_ = this.sfx_;
		component.gF_ = this.gF_;
		component.games_ = this;
		component.themes_ = this.themes_;
		component.unlock_ = this.unlock_;
		component.licences_ = this.licences_;
		component.InitArrays();
		if (setDate)
		{
			component.date_start_year = this.mS_.year;
			component.date_start_month = this.mS_.month;
		}
		if (!fromSavegame)
		{
			this.FindGames();
		}
		return component;
	}

	// Token: 0x060002AD RID: 685 RVA: 0x0002A688 File Offset: 0x00028888
	public void InitMMOtoF2PGame(gameScript script_)
	{
		script_.myID = this.GetNewID();
		script_.SetGameObjectName();
		script_.goty = false;
		script_.exklusivKonsolenSells = 0L;
		script_.originalGameID = -1;
		script_.costs_updates = 0L;
		for (int i = 0; i < script_.specialMarketing.Length; i++)
		{
			script_.specialMarketing[i] = 0;
		}
		this.FindGames();
	}

	// Token: 0x060002AE RID: 686 RVA: 0x0002A6E8 File Offset: 0x000288E8
	public void InitBudgetGame(gameScript script_)
	{
		script_.myID = this.GetNewID();
		script_.SetGameObjectName();
		script_.goty = false;
		script_.exklusivKonsolenSells = 0L;
		script_.originalGameID = -1;
		script_.costs_updates = 0L;
		script_.ipPunkte = 0f;
		script_.ipTime = 0;
		script_.script_mainIP = null;
		for (int i = 0; i < script_.specialMarketing.Length; i++)
		{
			script_.specialMarketing[i] = 0;
		}
		this.FindGames();
	}

	// Token: 0x060002AF RID: 687 RVA: 0x0002A760 File Offset: 0x00028960
	public void InitAddonBundle(gameScript script_)
	{
		script_.myID = this.GetNewID();
		script_.SetGameObjectName();
		script_.goty = false;
		script_.exklusivKonsolenSells = 0L;
		script_.originalGameID = -1;
		script_.costs_updates = 0L;
		script_.ipPunkte = 0f;
		script_.ipTime = 0;
		script_.script_mainIP = null;
		for (int i = 0; i < script_.inAppPurchase.Length; i++)
		{
			script_.inAppPurchase[i] = false;
		}
		for (int j = 0; j < script_.specialMarketing.Length; j++)
		{
			script_.specialMarketing[j] = 0;
		}
		this.FindGames();
	}

	// Token: 0x060002B0 RID: 688 RVA: 0x0002A7F4 File Offset: 0x000289F4
	public void InitGotyGame(gameScript script_)
	{
		script_.myID = this.GetNewID();
		script_.SetGameObjectName();
		script_.goty = false;
		script_.exklusivKonsolenSells = 0L;
		script_.originalGameID = -1;
		script_.costs_updates = 0L;
		script_.ipPunkte = 0f;
		script_.ipTime = 0;
		script_.script_mainIP = null;
		for (int i = 0; i < script_.specialMarketing.Length; i++)
		{
			script_.specialMarketing[i] = 0;
		}
		this.FindGames();
	}

	// Token: 0x060002B1 RID: 689 RVA: 0x0002A86C File Offset: 0x00028A6C
	private int GetNewID()
	{
		return UnityEngine.Random.Range(1, 2000000000);
	}

	// Token: 0x060002B2 RID: 690 RVA: 0x0002A87C File Offset: 0x00028A7C
	public void RemovePortFlags(gameScript scriptPort_)
	{
		if (scriptPort_.portID != -1)
		{
			GameObject gameObject = GameObject.Find("GAME_" + scriptPort_.portID.ToString());
			if (gameObject)
			{
				gameScript component = gameObject.GetComponent<gameScript>();
				if (component)
				{
					if (scriptPort_.handy)
					{
						component.portExist[1] = false;
						return;
					}
					if (scriptPort_.arcade)
					{
						component.portExist[2] = false;
						return;
					}
					component.portExist[0] = false;
				}
			}
		}
	}

	// Token: 0x060002B3 RID: 691 RVA: 0x0002A8F4 File Offset: 0x00028AF4
	public void SetPortFlags(gameScript scriptPort_)
	{
		if (scriptPort_.portID != -1)
		{
			GameObject gameObject = GameObject.Find("GAME_" + scriptPort_.portID.ToString());
			if (gameObject)
			{
				gameScript component = gameObject.GetComponent<gameScript>();
				if (component)
				{
					if (scriptPort_.handy)
					{
						component.portExist[1] = true;
						return;
					}
					if (scriptPort_.arcade)
					{
						component.portExist[2] = true;
						return;
					}
					component.portExist[0] = true;
				}
			}
		}
	}

	// Token: 0x060002B4 RID: 692 RVA: 0x0002A96C File Offset: 0x00028B6C
	public void FindGames()
	{
		this.arrayGames = GameObject.FindGameObjectsWithTag("Game");
		this.arrayGamesScripts = new gameScript[this.arrayGames.Length];
		this.arrayMyIpScripts.Clear();
		for (int i = 0; i < this.arrayGames.Length; i++)
		{
			if (this.arrayGames[i])
			{
				this.arrayGamesScripts[i] = this.arrayGames[i].GetComponent<gameScript>();
				if (this.arrayGamesScripts[i] && this.arrayGamesScripts[i].ownerID == this.mS_.myID && this.arrayGamesScripts[i].myID == this.arrayGamesScripts[i].mainIP)
				{
					this.arrayMyIpScripts.Add(this.arrayGamesScripts[i]);
				}
			}
		}
	}

	// Token: 0x060002B5 RID: 693 RVA: 0x0002AA3C File Offset: 0x00028C3C
	public void SellAllGames()
	{
		this.poM_.amountPublishingOffers = 0;
		for (int i = 0; i < this.themes_.themes_MARKT.Length; i++)
		{
			this.themes_.themes_MARKT[i] = 0;
		}
		for (int j = 0; j < this.genres_.genres_MARKT.Length; j++)
		{
			this.genres_.genres_MARKT[j] = 0;
		}
		this.freeServerPlatz = 0;
		for (int k = 0; k < this.mS_.arrayRooms.Length; k++)
		{
			if (this.mS_.arrayRooms[k])
			{
				roomScript component = this.mS_.arrayRooms[k].GetComponent<roomScript>();
				if (component && component.typ == 15)
				{
					this.freeServerPlatz += component.GetFreeServerplatz();
					component.serverplatzUsed = 0;
				}
			}
		}
		for (int l = 0; l < this.arrayGamesScripts.Length; l++)
		{
			if (this.arrayGamesScripts[l])
			{
				if (this.arrayGamesScripts[l].pubAngebot)
				{
					this.arrayGamesScripts[l].pubAngebot_AngebotWoche = false;
					this.arrayGamesScripts[l].pubAngebot_Weeks++;
					if (this.arrayGamesScripts[l].pubAngebot_Weeks > 25 && UnityEngine.Random.Range(0, 100) > 90)
					{
						this.arrayGamesScripts[l].FindPublisherForGame();
						this.arrayGamesScripts[l].SetOnMarket();
						if (this.mS_.newsSetting[0])
						{
							string text = this.tS_.GetText(494);
							text = text.Replace("<NAME1>", this.arrayGamesScripts[l].GetPublisherName());
							text = text.Replace("<NAME2>", this.arrayGamesScripts[l].GetNameWithTag());
							this.guiMain_.CreateTopNewsInfo(text);
						}
					}
					if (this.arrayGamesScripts[l].pubAngebot && !this.arrayGamesScripts[l].pubAnbgebot_Inivs)
					{
						this.poM_.amountPublishingOffers++;
					}
				}
				if (this.arrayGamesScripts[l].developerID == this.mS_.myID && this.arrayGamesScripts[l].inDevelopment && this.arrayGamesScripts[l].typ_contractGame && !this.arrayGamesScripts[l].auftragsspiel_zeitAbgelaufen)
				{
					this.arrayGamesScripts[l].auftragsspiel_zeitInWochen--;
					if (this.arrayGamesScripts[l].auftragsspiel_zeitInWochen < 0)
					{
						this.arrayGamesScripts[l].auftragsspiel_zeitInWochen = 0;
						this.arrayGamesScripts[l].auftragsspiel_zeitAbgelaufen = true;
					}
				}
				if (this.arrayGamesScripts[l].auftragsspiel)
				{
					bool flag = false;
					if (!this.arrayGamesScripts[l].retro)
					{
						if (!this.arrayGamesScripts[l].gamePlatformScript[0])
						{
							this.arrayGamesScripts[l].FindMyPlatforms();
						}
						if (this.arrayGamesScripts[l].gamePlatformScript[0].vomMarktGenommen)
						{
							flag = true;
						}
					}
					this.arrayGamesScripts[l].auftragsspiel_wochenAlsAngebot++;
					if ((this.arrayGamesScripts[l].auftragsspiel_wochenAlsAngebot > 25 || flag) && (UnityEngine.Random.Range(0, 100) > 90 || flag))
					{
						this.arrayGamesScripts[l].developerID = this.arrayGamesScripts[l].publisherID;
						this.arrayGamesScripts[l].FindMyPublisher();
						if (this.arrayGamesScripts[l].pS_)
						{
							this.arrayGamesScripts[l].pS_.ResetDataForAuftragsspiel(this.arrayGamesScripts[l]);
						}
						this.arrayGamesScripts[l].CalcReview(false);
						if (this.arrayGamesScripts[l].reviewGameplay > 99)
						{
							this.arrayGamesScripts[l].reviewGameplay = 99;
						}
						if (this.arrayGamesScripts[l].reviewTotal > 99)
						{
							this.arrayGamesScripts[l].reviewGameplay = 99;
						}
						this.mS_.reviewText_.GetReviewText(this.arrayGamesScripts[l]);
						this.arrayGamesScripts[l].SetOnMarket();
						if (this.mS_.newsSetting[0])
						{
							string text2 = this.tS_.GetText(494);
							text2 = text2.Replace("<NAME1>", this.arrayGamesScripts[l].GetPublisherName());
							text2 = text2.Replace("<NAME2>", this.arrayGamesScripts[l].GetNameWithTag());
							this.guiMain_.CreateTopNewsInfo(text2);
						}
					}
				}
				if (this.arrayGamesScripts[l].ownerID == this.mS_.myID && !this.arrayGamesScripts[l].typ_contractGame && !this.arrayGamesScripts[l].auftragsspiel && this.arrayGamesScripts[l].mainIP == this.arrayGamesScripts[l].myID)
				{
					this.arrayGamesScripts[l].ipTime++;
					if (this.arrayGamesScripts[l].ipTime > 250)
					{
						switch (this.mS_.difficulty)
						{
						case 0:
							this.arrayGamesScripts[l].AddIpPoints(-0.1f);
							break;
						case 1:
							this.arrayGamesScripts[l].AddIpPoints(-0.2f);
							break;
						case 2:
							this.arrayGamesScripts[l].AddIpPoints(-0.3f);
							break;
						case 3:
							this.arrayGamesScripts[l].AddIpPoints(-0.35f);
							break;
						case 4:
							this.arrayGamesScripts[l].AddIpPoints(-0.4f);
							break;
						case 5:
							this.arrayGamesScripts[l].AddIpPoints(-0.5f);
							break;
						default:
							this.arrayGamesScripts[l].AddIpPoints(-0.3f);
							break;
						}
					}
					if (this.mS_.week == 5)
					{
						for (int m = 0; m < this.arrayGamesScripts[l].merchLetzterMonat.Length; m++)
						{
							this.arrayGamesScripts[l].merchLetzterMonat[m] = this.arrayGamesScripts[l].merchDiesenMonat[m];
							this.arrayGamesScripts[l].merchDiesenMonat[m] = 0;
						}
					}
					if (this.arrayGamesScripts[l].merchGesamtReviewPoints > 0f)
					{
						this.arrayGamesScripts[l].merchGesamtReviewPoints -= 10f;
						if (this.arrayGamesScripts[l].merchGesamtReviewPoints < 0f)
						{
							this.arrayGamesScripts[l].merchGesamtReviewPoints = 0f;
						}
					}
				}
				if (this.arrayGamesScripts[l].ownerID == this.mS_.myID || this.arrayGamesScripts[l].publisherID == this.mS_.myID)
				{
					if (this.arrayGamesScripts[l].inDevelopment)
					{
						this.arrayGamesScripts[l].weeksInDevelopment++;
					}
					if (this.arrayGamesScripts[l].isOnMarket || this.arrayGamesScripts[l].inDevelopment || this.arrayGamesScripts[l].schublade)
					{
						if (this.arrayGamesScripts[l].specialMarketing[0] == 1)
						{
							base.StartCoroutine(this.iWaitForSpecialMarketing(this.arrayGamesScripts[l], 0));
						}
						if (this.arrayGamesScripts[l].specialMarketing[1] == 1)
						{
							base.StartCoroutine(this.iWaitForSpecialMarketing(this.arrayGamesScripts[l], 1));
						}
						if (this.arrayGamesScripts[l].specialMarketing[2] == 1)
						{
							base.StartCoroutine(this.iWaitForSpecialMarketing(this.arrayGamesScripts[l], 2));
						}
						if (this.arrayGamesScripts[l].specialMarketing[3] == 1)
						{
							base.StartCoroutine(this.iWaitForSpecialMarketing(this.arrayGamesScripts[l], 3));
						}
						if (this.arrayGamesScripts[l].specialMarketing[4] == 1)
						{
							base.StartCoroutine(this.iWaitForSpecialMarketing(this.arrayGamesScripts[l], 4));
						}
						if (this.arrayGamesScripts[l].hype > 100f && this.arrayGamesScripts[l].isOnMarket && this.arrayGamesScripts[l].reviewTotal > 0 && this.arrayGamesScripts[l].reviewTotal < 90 && this.arrayGamesScripts[l].weeksOnMarket > 0 && !this.guiMain_.uiObjects[296].activeSelf)
						{
							base.StartCoroutine(this.iWaitForSpecialMarketing(this.arrayGamesScripts[l], 100));
						}
					}
				}
				if (this.arrayGamesScripts[l].isOnMarket && !this.arrayGamesScripts[l].inDevelopment && !this.arrayGamesScripts[l].typ_addon && !this.arrayGamesScripts[l].typ_mmoaddon && !this.arrayGamesScripts[l].typ_bundle)
				{
					this.themes_.themes_MARKT[this.arrayGamesScripts[l].gameMainTheme]++;
					if (this.arrayGamesScripts[l].gameSubTheme != -1)
					{
						this.themes_.themes_MARKT[this.arrayGamesScripts[l].gameSubTheme]++;
					}
					this.genres_.genres_MARKT[this.arrayGamesScripts[l].maingenre]++;
				}
				if (this.mS_.multiplayer)
				{
					if (this.mS_.mpCalls_.isServer && (this.arrayGamesScripts[l].IsMyGame() || this.arrayGamesScripts[l].typ_contractGame || (this.arrayGamesScripts[l].DeveloperIsNPC() && this.arrayGamesScripts[l].PublisherIsNPC() && this.arrayGamesScripts[l].OwnerIsNPC())))
					{
						this.arrayGamesScripts[l].SellGame();
					}
					if (this.mS_.mpCalls_.isClient && this.arrayGamesScripts[l].IsMyGame())
					{
						this.arrayGamesScripts[l].SellGame();
					}
				}
				else
				{
					this.arrayGamesScripts[l].SellGame();
				}
				if (this.arrayGamesScripts[l].ownerID == this.mS_.myID)
				{
					this.arrayGamesScripts[l].SellMerchandise();
				}
			}
		}
		this.LagerplatzVerteilen();
	}

	// Token: 0x060002B6 RID: 694 RVA: 0x0002B483 File Offset: 0x00029683
	private IEnumerator iWaitForSpecialMarketing(gameScript gS_, int kampagne)
	{
		bool done = false;
		while (!done)
		{
			if (gS_ && !this.guiMain_.uiObjects[296].activeSelf)
			{
				done = true;
				this.guiMain_.OpenMenu(false);
				this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[296]);
				this.guiMain_.uiObjects[296].GetComponent<Menu_Result_MarketingSpezial>().Init(gS_, kampagne);
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x060002B7 RID: 695 RVA: 0x0002B4A0 File Offset: 0x000296A0
	public int GetAmountOfMMOs()
	{
		int num = 0;
		for (int i = 0; i < this.arrayGamesScripts.Length; i++)
		{
			if (this.arrayGamesScripts[i])
			{
				gameScript gameScript = this.arrayGamesScripts[i];
				if (gameScript && gameScript.gameTyp == 1 && gameScript.isOnMarket && gameScript.releaseDate <= 0)
				{
					num++;
				}
			}
		}
		return num;
	}

	// Token: 0x060002B8 RID: 696 RVA: 0x0002B504 File Offset: 0x00029704
	public int GetAmountOfF2Ps()
	{
		int num = 0;
		for (int i = 0; i < this.arrayGamesScripts.Length; i++)
		{
			if (this.arrayGamesScripts[i])
			{
				gameScript gameScript = this.arrayGamesScripts[i];
				if (gameScript && gameScript.gameTyp == 2 && gameScript.isOnMarket && gameScript.releaseDate <= 0)
				{
					num++;
				}
			}
		}
		return num;
	}

	// Token: 0x060002B9 RID: 697 RVA: 0x0002B568 File Offset: 0x00029768
	public Vector4 GetAmountGamesWithGenreAndTopic(gameScript gS_)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		for (int i = 0; i < this.arrayGamesScripts.Length; i++)
		{
			if (this.arrayGamesScripts[i])
			{
				gameScript gameScript = this.arrayGamesScripts[i];
				if (gameScript && gS_.myID != gameScript.myID && gameScript.isOnMarket && !gameScript.typ_addon && !gameScript.typ_bundle && !gameScript.typ_mmoaddon)
				{
					if (gameScript.maingenre == gS_.maingenre)
					{
						num++;
						if (this.SamePlatform(gameScript.gamePlatform[0], gS_))
						{
							num3++;
						}
						if (this.SamePlatform(gameScript.gamePlatform[1], gS_))
						{
							num3++;
						}
						if (this.SamePlatform(gameScript.gamePlatform[2], gS_))
						{
							num3++;
						}
						if (this.SamePlatform(gameScript.gamePlatform[3], gS_))
						{
							num3++;
						}
					}
					if (gameScript.gameMainTheme == gS_.gameMainTheme)
					{
						num2++;
						if (this.SamePlatform(gameScript.gamePlatform[0], gS_))
						{
							num4++;
						}
						if (this.SamePlatform(gameScript.gamePlatform[1], gS_))
						{
							num4++;
						}
						if (this.SamePlatform(gameScript.gamePlatform[2], gS_))
						{
							num4++;
						}
						if (this.SamePlatform(gameScript.gamePlatform[3], gS_))
						{
							num4++;
						}
					}
				}
			}
		}
		return new Vector4((float)num, (float)num2, (float)num3, (float)num4);
	}

	// Token: 0x060002BA RID: 698 RVA: 0x0002B6EC File Offset: 0x000298EC
	public int GetAmountGamesWithGenre_OnMarket(int genreID)
	{
		int num = 0;
		for (int i = 0; i < this.arrayGamesScripts.Length; i++)
		{
			if (this.arrayGamesScripts[i])
			{
				gameScript gameScript = this.arrayGamesScripts[i];
				if (gameScript && gameScript.isOnMarket && !gameScript.typ_addon && !gameScript.typ_bundle && !gameScript.typ_mmoaddon && gameScript.maingenre == genreID)
				{
					num++;
				}
			}
		}
		return num;
	}

	// Token: 0x060002BB RID: 699 RVA: 0x0002B75C File Offset: 0x0002995C
	private bool SamePlatform(int platformID, gameScript gS_)
	{
		if (platformID != -1)
		{
			for (int i = 0; i < gS_.gamePlatform.Length; i++)
			{
				if (platformID == gS_.gamePlatform[i])
				{
					return true;
				}
			}
			return false;
		}
		return false;
	}

	// Token: 0x060002BC RID: 700 RVA: 0x0002B794 File Offset: 0x00029994
	public int GetFreeLagerplatz()
	{
		int num = 0;
		foreach (GameObject gameObject in this.mS_.arrayRooms)
		{
			if (gameObject)
			{
				roomScript component = gameObject.GetComponent<roomScript>();
				if (component.typ == 9)
				{
					num += component.GetFreeLagerplatz();
				}
			}
		}
		return num;
	}

	// Token: 0x060002BD RID: 701 RVA: 0x0002B7E7 File Offset: 0x000299E7
	public void LagerplatzVerteilenEinGame(int menge)
	{
		if (menge > 0)
		{
			this.SearchLager(menge);
		}
	}

	// Token: 0x060002BE RID: 702 RVA: 0x0002B7F4 File Offset: 0x000299F4
	public void LagerplatzVerteilen()
	{
		foreach (GameObject gameObject in this.mS_.arrayRooms)
		{
			if (gameObject)
			{
				roomScript component = gameObject.GetComponent<roomScript>();
				if (component.typ == 9)
				{
					component.lagerplatzUsed = 0;
				}
			}
		}
		for (int j = 0; j < this.arrayGamesScripts.Length; j++)
		{
			if (this.arrayGamesScripts[j] && this.arrayGamesScripts[j].publisherID == this.mS_.myID)
			{
				int lagerbestand = this.arrayGamesScripts[j].GetLagerbestand();
				if (lagerbestand > 0)
				{
					this.SearchLager(lagerbestand);
				}
			}
		}
	}

	// Token: 0x060002BF RID: 703 RVA: 0x0002B8A0 File Offset: 0x00029AA0
	private void SearchLager(int bestand)
	{
		foreach (GameObject gameObject in this.mS_.arrayRooms)
		{
			if (gameObject)
			{
				roomScript component = gameObject.GetComponent<roomScript>();
				if (component.typ == 9)
				{
					int freeLagerplatz = component.GetFreeLagerplatz();
					if (freeLagerplatz > 0)
					{
						if (freeLagerplatz >= bestand)
						{
							component.lagerplatzUsed += bestand;
							return;
						}
						component.lagerplatzUsed += freeLagerplatz;
						bestand -= freeLagerplatz;
					}
				}
			}
		}
	}

	// Token: 0x060002C0 RID: 704 RVA: 0x0002B91C File Offset: 0x00029B1C
	public void SaveLastChartPosition()
	{
		if (!this.mS_ || !this.mS_.mpCalls_)
		{
			this.mS_.FindScripts();
		}
		if (!this.mS_.multiplayer || this.mS_.mpCalls_.isServer)
		{
			for (int i = 0; i < this.chartsWeekList.Count; i++)
			{
				gameScript script_ = this.chartsWeekList[i].script_;
				if (script_)
				{
					script_.lastChartPosition = i;
				}
			}
			for (int j = 0; j < this.chartsWeekList_Handy.Count; j++)
			{
				gameScript script_2 = this.chartsWeekList_Handy[j].script_;
				if (script_2)
				{
					script_2.lastChartPosition = j;
				}
			}
			for (int k = 0; k < this.chartsWeekList_Arcade.Count; k++)
			{
				gameScript script_3 = this.chartsWeekList_Arcade[k].script_;
				if (script_3)
				{
					script_3.lastChartPosition = k;
				}
			}
			for (int l = 0; l < this.chartsWeekList_F2P.Count; l++)
			{
				gameScript script_4 = this.chartsWeekList_F2P[l].script_;
				if (script_4)
				{
					script_4.lastChartPosition = l;
				}
			}
		}
	}

	// Token: 0x060002C1 RID: 705 RVA: 0x0002BA64 File Offset: 0x00029C64
	public void UpdateChartsWeek()
	{
		if (!this.mS_ || !this.mS_.mpCalls_)
		{
			this.mS_.FindScripts();
		}
		this.chartsWeekList.Clear();
		this.chartsWeekList_Handy.Clear();
		this.chartsWeekList_Arcade.Clear();
		this.chartsWeekList_F2P.Clear();
		for (int n = 0; n < this.arrayGamesScripts.Length; n++)
		{
			if (this.arrayGamesScripts[n] && this.arrayGamesScripts[n].isOnMarket && this.arrayGamesScripts[n].sellsPerWeek[0] > 0 && !this.arrayGamesScripts[n].inDevelopment)
			{
				if (this.arrayGamesScripts[n].gameTyp != 2 && !this.arrayGamesScripts[n].handy && !this.arrayGamesScripts[n].arcade)
				{
					this.chartsWeekList.Add(new ChartsWeek(this.arrayGamesScripts[n].myID, this.arrayGamesScripts[n].sellsPerWeek[0], this.arrayGamesScripts[n]));
				}
				if (this.arrayGamesScripts[n].gameTyp != 2 && this.arrayGamesScripts[n].handy)
				{
					this.chartsWeekList_Handy.Add(new ChartsWeek(this.arrayGamesScripts[n].myID, this.arrayGamesScripts[n].sellsPerWeek[0], this.arrayGamesScripts[n]));
				}
				if (this.arrayGamesScripts[n].gameTyp != 2 && this.arrayGamesScripts[n].arcade)
				{
					this.chartsWeekList_Arcade.Add(new ChartsWeek(this.arrayGamesScripts[n].myID, this.arrayGamesScripts[n].sellsPerWeek[0], this.arrayGamesScripts[n]));
				}
				if (this.arrayGamesScripts[n].gameTyp == 2)
				{
					this.chartsWeekList_F2P.Add(new ChartsWeek(this.arrayGamesScripts[n].myID, this.arrayGamesScripts[n].sellsPerWeek[0], this.arrayGamesScripts[n]));
				}
			}
		}
		this.chartsWeekList = (from i in this.chartsWeekList
		orderby i.sells descending
		select i).ToList<ChartsWeek>();
		this.chartsWeekList_Handy = (from i in this.chartsWeekList_Handy
		orderby i.sells descending
		select i).ToList<ChartsWeek>();
		this.chartsWeekList_Arcade = (from i in this.chartsWeekList_Arcade
		orderby i.sells descending
		select i).ToList<ChartsWeek>();
		this.chartsWeekList_F2P = (from i in this.chartsWeekList_F2P
		orderby i.sells descending
		select i).ToList<ChartsWeek>();
		if (!this.mS_.multiplayer || this.mS_.mpCalls_.isServer)
		{
			for (int j = 0; j < this.chartsWeekList.Count; j++)
			{
				gameScript script_ = this.chartsWeekList[j].script_;
				if (script_ && (script_.bestChartPosition > j + 1 || script_.bestChartPosition <= 0))
				{
					script_.bestChartPosition = j + 1;
				}
			}
			for (int k = 0; k < this.chartsWeekList_Handy.Count; k++)
			{
				gameScript script_2 = this.chartsWeekList_Handy[k].script_;
				if (script_2 && (script_2.bestChartPosition > k + 1 || script_2.bestChartPosition <= 0))
				{
					script_2.bestChartPosition = k + 1;
				}
			}
			for (int l = 0; l < this.chartsWeekList_Arcade.Count; l++)
			{
				gameScript script_3 = this.chartsWeekList_Arcade[l].script_;
				if (script_3 && (script_3.bestChartPosition > l + 1 || script_3.bestChartPosition <= 0))
				{
					script_3.bestChartPosition = l + 1;
				}
			}
			for (int m = 0; m < this.chartsWeekList_F2P.Count; m++)
			{
				gameScript script_4 = this.chartsWeekList_F2P[m].script_;
				if (script_4 && (script_4.bestChartPosition > m + 1 || script_4.bestChartPosition <= 0))
				{
					script_4.bestChartPosition = m + 1;
				}
			}
		}
	}

	// Token: 0x060002C2 RID: 706 RVA: 0x0002BEC0 File Offset: 0x0002A0C0
	public int GetChartsWeekPosition(int gameID_)
	{
		for (int i = 0; i < this.chartsWeekList.Count; i++)
		{
			if (this.chartsWeekList[i].gameID == gameID_)
			{
				return i + 1;
			}
		}
		for (int j = 0; j < this.chartsWeekList_Handy.Count; j++)
		{
			if (this.chartsWeekList_Handy[j].gameID == gameID_)
			{
				return j + 1;
			}
		}
		for (int k = 0; k < this.chartsWeekList_Arcade.Count; k++)
		{
			if (this.chartsWeekList_Arcade[k].gameID == gameID_)
			{
				return k + 1;
			}
		}
		for (int l = 0; l < this.chartsWeekList_F2P.Count; l++)
		{
			if (this.chartsWeekList_F2P[l].gameID == gameID_)
			{
				return l + 1;
			}
		}
		return -1;
	}

	// Token: 0x060002C3 RID: 707 RVA: 0x0002BF88 File Offset: 0x0002A188
	public void CreateAllTimeCharts(int max)
	{
		this.chartsList.Clear();
		for (int j = 0; j < this.arrayGamesScripts.Length; j++)
		{
			if (this.arrayGamesScripts[j] && this.arrayGamesScripts[j].sellsTotal > 0L && (this.arrayGamesScripts[j].typ_nachfolger || this.arrayGamesScripts[j].typ_remaster || this.arrayGamesScripts[j].typ_standard || this.arrayGamesScripts[j].typ_spinoff) && !this.arrayGamesScripts[j].inDevelopment && !this.arrayGamesScripts[j].typ_addon && !this.arrayGamesScripts[j].typ_mmoaddon && !this.arrayGamesScripts[j].typ_bundle && !this.arrayGamesScripts[j].typ_budget && !this.arrayGamesScripts[j].typ_addonStandalone && this.arrayGamesScripts[j].gameTyp != 2 && !this.arrayGamesScripts[j].handy && !this.arrayGamesScripts[j].arcade)
			{
				this.chartsList.Add(new ChartsList(this.arrayGamesScripts[j].myID, this.arrayGamesScripts[j].sellsTotal, this.arrayGamesScripts[j]));
			}
		}
		this.chartsList = (from i in this.chartsList
		orderby i.wert descending
		select i).ToList<ChartsList>();
		while (this.chartsList.Count > max)
		{
			this.chartsList.RemoveAt(this.chartsList.Count - 1);
		}
	}

	// Token: 0x060002C4 RID: 708 RVA: 0x0002C140 File Offset: 0x0002A340
	public void CreateBestGamesCharts(int max, int genre)
	{
		this.chartsList.Clear();
		for (int j = 0; j < this.arrayGamesScripts.Length; j++)
		{
			if (this.arrayGamesScripts[j] && this.arrayGamesScripts[j].reviewTotal > 0 && (this.arrayGamesScripts[j].maingenre == genre || genre < 0) && (this.arrayGamesScripts[j].typ_nachfolger || this.arrayGamesScripts[j].typ_remaster || this.arrayGamesScripts[j].typ_standard || this.arrayGamesScripts[j].typ_spinoff) && !this.arrayGamesScripts[j].inDevelopment && !this.arrayGamesScripts[j].pubAngebot && !this.arrayGamesScripts[j].auftragsspiel && !this.arrayGamesScripts[j].typ_addon && !this.arrayGamesScripts[j].typ_mmoaddon && !this.arrayGamesScripts[j].typ_bundle && !this.arrayGamesScripts[j].typ_budget && !this.arrayGamesScripts[j].typ_addonStandalone)
			{
				this.chartsList.Add(new ChartsList(this.arrayGamesScripts[j].myID, (long)this.arrayGamesScripts[j].reviewTotal, this.arrayGamesScripts[j]));
			}
		}
		this.chartsList = (from i in this.chartsList
		orderby i.wert descending
		select i).ToList<ChartsList>();
		while (this.chartsList.Count > max)
		{
			this.chartsList.RemoveAt(this.chartsList.Count - 1);
		}
	}

	// Token: 0x060002C5 RID: 709 RVA: 0x0002C2FC File Offset: 0x0002A4FC
	public void CreateAllTimeChartsUmsatz(int max)
	{
		this.chartsList.Clear();
		for (int j = 0; j < this.arrayGamesScripts.Length; j++)
		{
			if (this.arrayGamesScripts[j] && this.arrayGamesScripts[j].umsatzTotal > 0L && (this.arrayGamesScripts[j].typ_nachfolger || this.arrayGamesScripts[j].typ_remaster || this.arrayGamesScripts[j].typ_standard || this.arrayGamesScripts[j].typ_spinoff) && !this.arrayGamesScripts[j].inDevelopment && !this.arrayGamesScripts[j].pubAngebot && !this.arrayGamesScripts[j].auftragsspiel && !this.arrayGamesScripts[j].typ_addon && !this.arrayGamesScripts[j].typ_mmoaddon && !this.arrayGamesScripts[j].typ_bundle && !this.arrayGamesScripts[j].typ_budget && !this.arrayGamesScripts[j].typ_addonStandalone)
			{
				this.chartsList.Add(new ChartsList(this.arrayGamesScripts[j].myID, this.arrayGamesScripts[j].umsatzTotal, this.arrayGamesScripts[j]));
			}
		}
		this.chartsList = (from i in this.chartsList
		orderby i.wert descending
		select i).ToList<ChartsList>();
		while (this.chartsList.Count > max)
		{
			this.chartsList.RemoveAt(this.chartsList.Count - 1);
		}
	}

	// Token: 0x060002C6 RID: 710 RVA: 0x0002C4A0 File Offset: 0x0002A6A0
	public void CreateAllTimeChartsHandy(int max)
	{
		this.chartsList.Clear();
		for (int j = 0; j < this.arrayGamesScripts.Length; j++)
		{
			if (this.arrayGamesScripts[j] && this.arrayGamesScripts[j].sellsTotal > 0L && (this.arrayGamesScripts[j].typ_nachfolger || this.arrayGamesScripts[j].typ_remaster || this.arrayGamesScripts[j].typ_standard || this.arrayGamesScripts[j].typ_spinoff) && !this.arrayGamesScripts[j].inDevelopment && !this.arrayGamesScripts[j].pubAngebot && !this.arrayGamesScripts[j].auftragsspiel && !this.arrayGamesScripts[j].typ_addon && !this.arrayGamesScripts[j].typ_mmoaddon && !this.arrayGamesScripts[j].typ_bundle && !this.arrayGamesScripts[j].typ_budget && !this.arrayGamesScripts[j].typ_addonStandalone && this.arrayGamesScripts[j].gameTyp != 2 && this.arrayGamesScripts[j].handy)
			{
				this.chartsList.Add(new ChartsList(this.arrayGamesScripts[j].myID, this.arrayGamesScripts[j].sellsTotal, this.arrayGamesScripts[j]));
			}
		}
		this.chartsList = (from i in this.chartsList
		orderby i.wert descending
		select i).ToList<ChartsList>();
		while (this.chartsList.Count > max)
		{
			this.chartsList.RemoveAt(this.chartsList.Count - 1);
		}
	}

	// Token: 0x060002C7 RID: 711 RVA: 0x0002C668 File Offset: 0x0002A868
	public void CreateAllTimeChartsArcade(int max)
	{
		this.chartsList.Clear();
		for (int j = 0; j < this.arrayGamesScripts.Length; j++)
		{
			if (this.arrayGamesScripts[j] && this.arrayGamesScripts[j].sellsTotal > 0L && (this.arrayGamesScripts[j].typ_nachfolger || this.arrayGamesScripts[j].typ_remaster || this.arrayGamesScripts[j].typ_standard || this.arrayGamesScripts[j].typ_spinoff) && !this.arrayGamesScripts[j].inDevelopment && !this.arrayGamesScripts[j].pubAngebot && !this.arrayGamesScripts[j].auftragsspiel && !this.arrayGamesScripts[j].typ_addon && !this.arrayGamesScripts[j].typ_mmoaddon && !this.arrayGamesScripts[j].typ_bundle && !this.arrayGamesScripts[j].typ_budget && !this.arrayGamesScripts[j].typ_addonStandalone && this.arrayGamesScripts[j].gameTyp != 2 && this.arrayGamesScripts[j].arcade)
			{
				this.chartsList.Add(new ChartsList(this.arrayGamesScripts[j].myID, this.arrayGamesScripts[j].sellsTotal, this.arrayGamesScripts[j]));
			}
		}
		this.chartsList = (from i in this.chartsList
		orderby i.wert descending
		select i).ToList<ChartsList>();
		while (this.chartsList.Count > max)
		{
			this.chartsList.RemoveAt(this.chartsList.Count - 1);
		}
	}

	// Token: 0x060002C8 RID: 712 RVA: 0x0002C830 File Offset: 0x0002AA30
	public bool IsNewGenreCombination(int maingenre, int subgenre)
	{
		if (maingenre <= -1)
		{
			return false;
		}
		if (subgenre <= -1)
		{
			return false;
		}
		for (int i = 0; i < this.arrayGamesScripts.Length; i++)
		{
			if (this.arrayGamesScripts[i] && this.arrayGamesScripts[i].ownerID == this.mS_.myID && !this.arrayGamesScripts[i].pubOffer && this.arrayGamesScripts[i].maingenre == maingenre && this.arrayGamesScripts[i].subgenre == subgenre)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060002C9 RID: 713 RVA: 0x0002C8B8 File Offset: 0x0002AAB8
	public bool IsNewTopicCombination(int maintopic, int subtopic)
	{
		if (maintopic <= -1)
		{
			return false;
		}
		if (subtopic <= -1)
		{
			return false;
		}
		for (int i = 0; i < this.arrayGamesScripts.Length; i++)
		{
			if (this.arrayGamesScripts[i] && this.arrayGamesScripts[i].ownerID == this.mS_.myID && !this.arrayGamesScripts[i].pubOffer && this.arrayGamesScripts[i].gameMainTheme == maintopic && this.arrayGamesScripts[i].gameSubTheme == subtopic)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x04000573 RID: 1395
	public GameObject prefabGame;

	// Token: 0x04000574 RID: 1396
	public Sprite[] gameTypSprites;

	// Token: 0x04000575 RID: 1397
	public Sprite[] gamePlatformTypSprites;

	// Token: 0x04000576 RID: 1398
	public Sprite[] gameSizeSprites;

	// Token: 0x04000577 RID: 1399
	public Sprite[] gameAdds;

	// Token: 0x04000578 RID: 1400
	public Sprite[] gamePEGI;

	// Token: 0x04000579 RID: 1401
	public float[] inAppPurchasePrice;

	// Token: 0x0400057A RID: 1402
	public float[] inAppPurchaseHate;

	// Token: 0x0400057B RID: 1403
	public GameObject[] arrayGames;

	// Token: 0x0400057C RID: 1404
	public gameScript[] arrayGamesScripts;

	// Token: 0x0400057D RID: 1405
	public List<gameScript> arrayMyIpScripts = new List<gameScript>();

	// Token: 0x0400057E RID: 1406
	public float[] preise_inhalt;

	// Token: 0x0400057F RID: 1407
	public float tf_gewinnbeteiligungSelfPublish;

	// Token: 0x04000580 RID: 1408
	public float tf_gewinnbeteiligungTochterfirma;

	// Token: 0x04000581 RID: 1409
	public AnimationCurve curveSellsBewertung;

	// Token: 0x04000582 RID: 1410
	public AnimationCurve curveReview;

	// Token: 0x04000583 RID: 1411
	public AnimationCurve curveSells;

	// Token: 0x04000584 RID: 1412
	public AnimationCurve curveSellsDigital;

	// Token: 0x04000585 RID: 1413
	public AnimationCurve curveSellsDeluxe;

	// Token: 0x04000586 RID: 1414
	public AnimationCurve curveSellsCollectors;

	// Token: 0x04000587 RID: 1415
	public AnimationCurve curveSellsArcade;

	// Token: 0x04000588 RID: 1416
	private GameObject main_;

	// Token: 0x04000589 RID: 1417
	private mainScript mS_;

	// Token: 0x0400058A RID: 1418
	private textScript tS_;

	// Token: 0x0400058B RID: 1419
	private GUI_Main guiMain_;

	// Token: 0x0400058C RID: 1420
	private sfxScript sfx_;

	// Token: 0x0400058D RID: 1421
	private genres genres_;

	// Token: 0x0400058E RID: 1422
	private themes themes_;

	// Token: 0x0400058F RID: 1423
	private licences licences_;

	// Token: 0x04000590 RID: 1424
	private engineFeatures eF_;

	// Token: 0x04000591 RID: 1425
	private cameraMovementScript cmS_;

	// Token: 0x04000592 RID: 1426
	private unlockScript unlock_;

	// Token: 0x04000593 RID: 1427
	private gameplayFeatures gF_;

	// Token: 0x04000594 RID: 1428
	private publishingOfferMain poM_;

	// Token: 0x04000595 RID: 1429
	public int freeServerPlatz;

	// Token: 0x04000596 RID: 1430
	public List<ChartsWeek> chartsWeekList = new List<ChartsWeek>();

	// Token: 0x04000597 RID: 1431
	public List<ChartsWeek> chartsWeekList_Handy = new List<ChartsWeek>();

	// Token: 0x04000598 RID: 1432
	public List<ChartsWeek> chartsWeekList_Arcade = new List<ChartsWeek>();

	// Token: 0x04000599 RID: 1433
	public List<ChartsWeek> chartsWeekList_F2P = new List<ChartsWeek>();

	// Token: 0x0400059A RID: 1434
	public List<ChartsList> chartsList = new List<ChartsList>();
}
