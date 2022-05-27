using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class gameScript : MonoBehaviour
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
		if (this.main_)
		{
			if (!this.mS_)
			{
				this.mS_ = this.main_.GetComponent<mainScript>();
			}
			if (!this.tS_)
			{
				this.tS_ = this.main_.GetComponent<textScript>();
			}
			if (!this.eF_)
			{
				this.eF_ = this.main_.GetComponent<engineFeatures>();
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
			if (!this.themes_)
			{
				this.themes_ = this.main_.GetComponent<themes>();
			}
			if (!this.unlock_)
			{
				this.unlock_ = this.main_.GetComponent<unlockScript>();
			}
			if (!this.licences_)
			{
				this.licences_ = this.main_.GetComponent<licences>();
			}
		}
		if (!this.guiMain_)
		{
			GameObject gameObject = GameObject.Find("CanvasInGameMenu");
			if (gameObject)
			{
				this.guiMain_ = gameObject.GetComponent<GUI_Main>();
			}
		}
		if (!this.sfx_)
		{
			GameObject gameObject2 = GameObject.Find("SFX");
			if (gameObject2)
			{
				this.sfx_ = gameObject2.GetComponent<sfxScript>();
			}
		}
	}

	
	private void OnDestroy()
	{
		if (!this.games_)
		{
			return;
		}
		if (this.portID != -1)
		{
			this.games_.RemovePortFlags(this);
			return;
		}
		if (this.typ_nachfolger)
		{
			GameObject gameObject = GameObject.Find("GAME_" + this.originalIP.ToString());
			if (gameObject)
			{
				gameScript component = gameObject.GetComponent<gameScript>();
				if (component)
				{
					component.nachfolger_created = false;
				}
			}
		}
		if (this.typ_remaster)
		{
			GameObject gameObject2 = GameObject.Find("GAME_" + this.originalIP.ToString());
			if (gameObject2)
			{
				gameScript component2 = gameObject2.GetComponent<gameScript>();
				if (component2)
				{
					component2.remaster_created = false;
				}
			}
		}
		if (this.typ_budget)
		{
			GameObject gameObject3 = GameObject.Find("GAME_" + this.originalIP.ToString());
			if (gameObject3)
			{
				gameScript component3 = gameObject3.GetComponent<gameScript>();
				if (component3)
				{
					component3.budget_created = false;
				}
			}
		}
		if (this.typ_goty)
		{
			GameObject gameObject4 = GameObject.Find("GAME_" + this.originalIP.ToString());
			if (gameObject4)
			{
				gameScript component4 = gameObject4.GetComponent<gameScript>();
				if (component4)
				{
					component4.goty_created = false;
				}
			}
		}
	}

	
	public void SetGameObjectName()
	{
		base.name = "GAME_" + this.myID.ToString();
	}

	
	public void InitArrays()
	{
		this.gameGameplayFeatures = new bool[this.gF_.gameplayFeatures_UNLOCK.Length];
		this.gameplayFeatures_DevDone = new bool[this.gF_.gameplayFeatures_UNLOCK.Length];
		this.fanbrief = new bool[this.tS_.fanLetter_GE.Length];
	}

	
	public void InitUI()
	{
		if (!this.IsMyGame())
		{
			return;
		}
		if (this.gameTab_)
		{
			return;
		}
		if (this.isOnMarket || this.schublade)
		{
			this.FindScripts();
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.guiMain_.uiPrefabs[11], this.guiMain_.uiObjects[81].transform);
			this.gameTab_ = gameObject.GetComponent<gameTab>();
			this.gameTab_.gS_ = this;
			this.gameTab_.mS_ = this.mS_;
			this.gameTab_.main_ = this.main_;
			this.gameTab_.guiMain_ = this.guiMain_;
			this.gameTab_.sfx_ = this.sfx_;
			this.gameTab_.tS_ = this.tS_;
			this.gameTab_.themes_ = this.themes_;
			this.gameTab_.genres_ = this.genres_;
			this.gameTab_.Init(this.myID);
		}
	}

	
	public float GetUserReviewPercent()
	{
		if (this.userPositiv <= 0 && this.userNegativ <= 0)
		{
			return 0f;
		}
		if (this.userPositiv > 1 && this.userNegativ <= 0)
		{
			return 100f;
		}
		if (this.userPositiv <= 0 && this.userNegativ > 0)
		{
			return 0f;
		}
		float num = (float)(this.userPositiv + this.userNegativ);
		num = 100f / num * (float)this.userPositiv;
		return this.mS_.Round(num, 1);
	}

	
	public string GetTooltipIP()
	{
		this.FindScripts();
		int num = 0;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		for (int i = 0; i < this.games_.arrayGamesScripts.Length; i++)
		{
			if (this.games_.arrayGamesScripts[i] && this.games_.arrayGamesScripts[i].mainIP == this.mainIP)
			{
				num++;
				if (this.gameTyp != 2 && this.games_.arrayGamesScripts[i].sellsTotal > 0L)
				{
					float num5 = (float)this.games_.arrayGamesScripts[i].sellsTotal;
					num2 += num5 / 1000000f;
				}
				if (this.gameTyp == 2 && this.games_.arrayGamesScripts[i].sellsTotal > 0L)
				{
					float num6 = (float)this.games_.arrayGamesScripts[i].sellsTotal;
					num3 += num6 / 1000000f;
				}
				if (this.games_.arrayGamesScripts[i].umsatzTotal > 0L)
				{
					float num7 = (float)this.games_.arrayGamesScripts[i].umsatzTotal;
					num4 += num7 / 1000000f;
				}
			}
		}
		if (this.mainIP != -1)
		{
			string text = "";
			text = text + "<b><size=18>" + this.GetIpName() + "</size></b>\n";
			text = text + "<b>" + this.GetDeveloperName() + "</b>\n";
			text = string.Concat(new string[]
			{
				text,
				this.tS_.GetText(1555),
				": <color=blue>",
				this.mS_.Round(this.GetIpBekanntheit(), 1).ToString(),
				"</color>\n"
			});
			text = string.Concat(new string[]
			{
				text,
				this.tS_.GetText(1559),
				" <color=blue>",
				num.ToString(),
				"</color>\n"
			});
			text += "\n";
			text = string.Concat(new string[]
			{
				text,
				this.tS_.GetText(275),
				": <color=blue>",
				this.mS_.Round(num2, 2).ToString(),
				" ",
				this.tS_.GetText(1558),
				"</color>\n"
			});
			text = string.Concat(new string[]
			{
				text,
				this.tS_.GetText(697),
				": <color=blue>",
				this.mS_.Round(num3, 2).ToString(),
				" ",
				this.tS_.GetText(1558),
				"</color>\n"
			});
			if (this.merchGesamtGewinn > 0L)
			{
				float value = (float)this.merchGesamtGewinn / 1000000f;
				text = string.Concat(new string[]
				{
					text,
					this.tS_.GetText(1842),
					": <color=blue>$",
					this.mS_.Round(value, 2).ToString(),
					" ",
					this.tS_.GetText(1558),
					"</color>\n"
				});
			}
			text = string.Concat(new string[]
			{
				text,
				this.tS_.GetText(276),
				": <color=blue>$",
				this.mS_.Round(num4, 2).ToString(),
				" ",
				this.tS_.GetText(1558),
				"</color>\n"
			});
			if (this.ipTime > 0)
			{
				string text2 = "\n" + this.tS_.GetText(1899);
				text2 = text2.Replace("<NUM>", "<color=red>" + this.mS_.GetMoney((long)this.ipTime, false) + "</color>");
				text += text2;
			}
			return text;
		}
		return "";
	}

	
	public string GetTooltip()
	{
		if (this.pubAngebot)
		{
			return this.PUBOFFER_GetTooltip();
		}
		this.FindMyEngineNew();
		string text = "";
		text = text + "<b><size=18>" + this.GetNameWithTag() + "</size></b>\n";
		if (this.IsMyAuftragsspiel())
		{
			text = text + "<color=blue><b>" + this.tS_.GetText(598) + "</b></color>\n";
		}
		if (this.pubOffer)
		{
			text = text + "<color=blue><b>" + this.tS_.GetText(1744) + "</b></color>\n";
			text = text + "<color=blue>" + this.PUBOFFER_GetRetailDigitalString() + "</color>\n";
			if (this.publisherID == this.mS_.myID)
			{
				text = string.Concat(new string[]
				{
					text,
					"<color=red>",
					this.tS_.GetText(1983),
					": ",
					Mathf.RoundToInt((float)this.PUBOFFER_GetGewinnbeteiligung()).ToString(),
					"%</color>\n"
				});
			}
		}
		text += "<b>";
		if (!this.pS_)
		{
			this.FindMyPublisher();
		}
		if (!this.devS_)
		{
			this.FindMyDeveloper();
		}
		if (this.devS_)
		{
			if (this.devS_.IsMyTochterfirma())
			{
				text = text + "<color=green>" + this.GetDeveloperName() + "</color>";
			}
			else
			{
				text += this.GetDeveloperName();
			}
		}
		else
		{
			text += this.GetDeveloperName();
		}
		if (this.pS_)
		{
			if (this.pS_.IsMyTochterfirma())
			{
				text = text + " | <color=green>" + this.GetPublisherName() + "</color>";
			}
			else
			{
				text = text + " | " + this.GetPublisherName();
			}
		}
		else
		{
			text = text + " | " + this.GetPublisherName();
		}
		text += "\n";
		if (!this.inDevelopment)
		{
			text = text + this.GetReleaseDateString() + "\n";
		}
		else
		{
			text = text + this.tS_.GetText(528) + "\n";
		}
		if (!this.typ_bundle && !this.typ_bundleAddon)
		{
			text = string.Concat(new string[]
			{
				text,
				this.GetTypString(),
				" | ",
				this.GetPlatformTypString(),
				"\n"
			});
		}
		if (this.typ_bundle)
		{
			text = text + this.GetTypString() + "\n\n";
			for (int i = 0; i < this.bundleID.Length; i++)
			{
				gameScript bundleGame = this.GetBundleGame(i);
				if (bundleGame)
				{
					text = text + bundleGame.GetNameWithTag() + "\n";
				}
			}
		}
		if (this.typ_bundleAddon)
		{
			text = text + this.GetTypString() + "\n";
			text = text + this.GetGenreString() + "\n\n";
			for (int j = 0; j < this.bundleID.Length; j++)
			{
				gameScript bundleGame2 = this.GetBundleGame(j);
				if (bundleGame2)
				{
					text = text + bundleGame2.GetNameWithTag() + "\n";
				}
			}
		}
		text += "</b><size=12>";
		if (!this.typ_bundle && !this.typ_bundleAddon && this.subgenre == -1)
		{
			text = text + this.GetGenreString() + "\n";
		}
		if (!this.typ_bundle && !this.typ_bundleAddon && this.subgenre != -1)
		{
			text = string.Concat(new string[]
			{
				text,
				this.GetGenreString(),
				" / ",
				this.GetSubGenreString(),
				"\n"
			});
		}
		if (!this.typ_bundle && !this.typ_bundleAddon && this.gameSubTheme == -1)
		{
			text = text + this.tS_.GetThemes(this.gameMainTheme) + "\n";
		}
		if (!this.typ_bundle && !this.typ_bundleAddon && this.gameSubTheme != -1)
		{
			text = string.Concat(new string[]
			{
				text,
				this.tS_.GetThemes(this.gameMainTheme),
				" / ",
				this.tS_.GetThemes(this.gameSubTheme),
				"\n"
			});
		}
		text += "</size>\n";
		bool flag = false;
		if (this.goty || this.typ_goty)
		{
			flag = true;
			text = text + "<color=green><b>★ " + this.tS_.GetText(770) + " ★</b></color>\n";
		}
		if (this.commercialFlop && this.weeksOnMarket >= 4)
		{
			flag = true;
			text = text + "<color=red><b>" + this.tS_.GetText(1757) + "</b></color>\n";
		}
		if (this.commercialHit && this.weeksOnMarket >= 4)
		{
			flag = true;
			text = text + "<color=green><b>★ " + this.tS_.GetText(1762) + " ★</b></color>\n";
		}
		if (this.newGenreCombination)
		{
			flag = true;
			text = text + "<color=green><b>★ " + this.tS_.GetText(1768) + " ★</b></color>\n";
		}
		if (this.newTopicCombination)
		{
			flag = true;
			text = text + "<color=green><b>★ " + this.tS_.GetText(1769) + " ★</b></color>\n";
		}
		if (flag)
		{
			text += "\n";
		}
		text += "<color=grey><size=12>";
		if (!this.gamePlatformScript[0])
		{
			this.FindMyPlatforms();
		}
		for (int k = 0; k < this.gamePlatformScript.Length; k++)
		{
			if (this.gamePlatformScript[k])
			{
				text = text + this.gamePlatformScript[k].GetName() + "\n";
			}
		}
		text += "</size></color>\n";
		if (this.gameLicence != -1)
		{
			text = string.Concat(new string[]
			{
				text,
				"<b>",
				this.tS_.GetText(356),
				"\n</b>",
				this.licences_.GetName(this.gameLicence),
				"\n\n"
			});
		}
		if (!this.typ_bundle)
		{
			text = string.Concat(new string[]
			{
				text,
				this.tS_.GetText(1555),
				": <color=blue>",
				this.mS_.Round(this.GetIpBekanntheit(), 1).ToString(),
				"</color>\n"
			});
		}
		if ((this.developerID == this.mS_.myID || this.publisherID == this.mS_.myID) && !this.typ_bundle && !this.typ_bundleAddon)
		{
			text = string.Concat(new string[]
			{
				text,
				this.tS_.GetText(1293),
				": <color=blue>",
				this.GetEntwicklungsbeginnDateString(),
				"</color>\n"
			});
		}
		text = string.Concat(new string[]
		{
			text,
			this.tS_.GetText(327),
			": <color=blue>",
			this.tS_.GetText(330 + this.gameSize - 1),
			"</color>\n"
		});
		text = string.Concat(new string[]
		{
			text,
			this.tS_.GetText(336),
			": <color=blue>",
			this.GetZielgruppeString(),
			"</color>\n"
		});
		if (!this.typ_bundle)
		{
			if (this.DeveloperIsNPC() && this.engineID == 0)
			{
				text = string.Concat(new string[]
				{
					text,
					this.tS_.GetText(369),
					": <color=blue>",
					this.tS_.GetText(1561),
					"</color>\n"
				});
			}
			else
			{
				text = string.Concat(new string[]
				{
					text,
					this.tS_.GetText(369),
					": <color=blue>",
					this.engineS_.GetName(),
					"</color>\n"
				});
			}
		}
		if (!this.schublade && !this.inDevelopment)
		{
			text = string.Concat(new string[]
			{
				text,
				this.tS_.GetText(990),
				": <color=blue>",
				this.GetUskString(),
				"</color>\n"
			});
			text = string.Concat(new string[]
			{
				text,
				this.tS_.GetText(277),
				": <color=blue>",
				Mathf.RoundToInt((float)this.reviewTotal).ToString(),
				"%</color>"
			});
		}
		if (this.IsMyGame())
		{
			if (!this.schublade && !this.inDevelopment)
			{
				text = string.Concat(new string[]
				{
					text,
					" | ",
					this.tS_.GetText(433),
					": <color=blue>",
					Mathf.RoundToInt(this.GetHype()).ToString(),
					"</color>"
				});
			}
			else
			{
				text = string.Concat(new string[]
				{
					text,
					this.tS_.GetText(433),
					": <color=blue>",
					Mathf.RoundToInt(this.GetHype()).ToString(),
					"</color>"
				});
			}
		}
		if (this.isOnMarket && this.gameTyp != 2 && !this.handy)
		{
			text = string.Concat(new string[]
			{
				text,
				"\n",
				this.tS_.GetText(491),
				": <color=blue>",
				this.weeksOnMarket.ToString(),
				"</color>"
			});
		}
		if (this.isOnMarket && (this.gameTyp == 2 || this.handy))
		{
			text = string.Concat(new string[]
			{
				text,
				"\n",
				this.tS_.GetText(1391),
				": <color=blue>",
				this.weeksOnMarket.ToString(),
				"</color>"
			});
		}
		if (this.bestChartPosition != 0)
		{
			text = string.Concat(new string[]
			{
				text,
				"\n",
				this.tS_.GetText(1669),
				": <color=blue>",
				this.bestChartPosition.ToString(),
				"</color>"
			});
		}
		if (this.isOnMarket || this.sellsTotal > 0L)
		{
			text = string.Concat(new string[]
			{
				text,
				"\n",
				this.tS_.GetText(1289),
				": (",
				this.GetUserReviewPercent().ToString(),
				"%)<color=green> ",
				this.mS_.GetMoney((long)this.userPositiv, false),
				"</color> | <color=red>",
				this.mS_.GetMoney((long)this.userNegativ, false),
				"</color>"
			});
		}
		if (this.amountUpdates > 0)
		{
			text = string.Concat(new string[]
			{
				text,
				"\n",
				this.tS_.GetText(649),
				": <color=blue>",
				this.amountUpdates.ToString(),
				"</color>"
			});
		}
		if (this.amountAddons > 0)
		{
			text = string.Concat(new string[]
			{
				text,
				"\n",
				this.tS_.GetText(956),
				": <color=blue>",
				this.amountAddons.ToString(),
				"</color>"
			});
		}
		if (this.amountMMOAddons > 0)
		{
			text = string.Concat(new string[]
			{
				text,
				"\n",
				this.tS_.GetText(956),
				": <color=blue>",
				this.amountMMOAddons.ToString(),
				"</color>"
			});
		}
		if (this.exklusiv && this.exklusivKonsolenSells > 0L)
		{
			if (!this.gamePlatformScript[0])
			{
				this.FindMyPlatforms();
			}
			if (this.gamePlatformScript[0])
			{
				string text2 = "\n" + this.tS_.GetText(1313);
				text2 = text2.Replace("<NUM>", this.mS_.GetMoney(this.exklusivKonsolenSells, false));
				text2 = text2.Replace("<NAME>", this.gamePlatformScript[0].GetName());
				text += text2;
			}
		}
		if (this.herstellerExklusiv && this.exklusivKonsolenSells > 0L)
		{
			if (!this.gamePlatformScript[0])
			{
				this.FindMyPlatforms();
			}
			if (this.gamePlatformScript[0])
			{
				string text3 = "\n" + this.tS_.GetText(1313);
				text3 = text3.Replace("<NUM>", this.mS_.GetMoney(this.exklusivKonsolenSells, false));
				text3 = text3.Replace("<NAME>", this.tS_.GetText(675));
				text += text3;
			}
		}
		text += "\n\n";
		if (!this.inDevelopment && !this.schublade)
		{
			if (this.gameTyp == 2 && (this.abonnements > 0 || this.bestAbonnements > 0))
			{
				text = string.Concat(new string[]
				{
					text,
					this.tS_.GetText(1392),
					": <color=blue>",
					this.mS_.GetMoney((long)this.abonnements, false),
					"</color> | ",
					this.tS_.GetText(1905),
					": <color=blue>",
					this.mS_.GetMoney((long)this.bestAbonnements, false),
					"</color>\n"
				});
			}
			if (this.gameTyp == 1 && (this.abonnements > 0 || this.bestAbonnements > 0))
			{
				text = string.Concat(new string[]
				{
					text,
					this.tS_.GetText(1236),
					": <color=blue>",
					this.mS_.GetMoney((long)this.abonnements, false),
					"</color> | ",
					this.tS_.GetText(1905),
					": <color=blue>",
					this.mS_.GetMoney((long)this.bestAbonnements, false),
					"</color>\n"
				});
			}
			if (this.gameTyp != 2 && !this.handy)
			{
				text = string.Concat(new string[]
				{
					text,
					this.tS_.GetText(275),
					": <color=blue>",
					this.mS_.GetMoney(this.sellsTotal, false),
					"</color>\n"
				});
			}
			if (this.gameTyp == 2 || this.handy)
			{
				text = string.Concat(new string[]
				{
					text,
					this.tS_.GetText(697),
					": <color=blue>",
					this.mS_.GetMoney(this.sellsTotal, false),
					"</color>\n"
				});
			}
			text = string.Concat(new string[]
			{
				text,
				this.tS_.GetText(276),
				": <color=blue>",
				this.mS_.GetMoney(this.umsatzTotal, true),
				"</color>\n"
			});
			if (!this.IsMyGame() && this.GetPublisherOrDeveloperIsTochterfirma())
			{
				text = text + "\n<color=green><b>" + this.tS_.GetText(1987) + "</b></color>\n";
				text = string.Concat(new string[]
				{
					text,
					this.tS_.GetText(1986),
					": <color=blue>",
					this.mS_.GetMoney(this.tw_gewinnanteil, true),
					"</color>\n"
				});
			}
		}
		if (this.IsMyGame())
		{
			if (this.GetGesamtGewinn() >= 0L)
			{
				text = string.Concat(new string[]
				{
					text,
					this.tS_.GetText(724),
					": <color=blue>",
					this.mS_.GetMoney(this.GetGesamtGewinn(), true),
					"</color>\n"
				});
			}
			else
			{
				text = string.Concat(new string[]
				{
					text,
					this.tS_.GetText(724),
					": <color=red>",
					this.mS_.GetMoney(this.GetGesamtGewinn(), true),
					"</color>\n"
				});
			}
			if (!this.schublade && this.publisherID == this.mS_.myID && this.gameTyp != 2 && !this.handy && !this.arcade)
			{
				text += "\n";
				text = string.Concat(new string[]
				{
					text,
					this.tS_.GetText(1103),
					": <color=blue>",
					this.mS_.GetMoney(this.sellsTotalStandard, false),
					"</color>\n"
				});
				if (this.sellsTotalDeluxe > 0L)
				{
					text = string.Concat(new string[]
					{
						text,
						this.tS_.GetText(1104),
						": <color=blue>",
						this.mS_.GetMoney(this.sellsTotalDeluxe, false),
						"</color>\n"
					});
				}
				if (this.sellsTotalCollectors > 0L)
				{
					text = string.Concat(new string[]
					{
						text,
						this.tS_.GetText(1105),
						": <color=blue>",
						this.mS_.GetMoney(this.sellsTotalCollectors, false),
						"</color>\n"
					});
				}
				if (this.sellsTotalOnline > 0L)
				{
					text = string.Concat(new string[]
					{
						text,
						this.tS_.GetText(1126),
						": <color=blue>",
						this.mS_.GetMoney(this.sellsTotalOnline, false),
						"</color>\n"
					});
				}
			}
		}
		if (this.IsMyGame() && this.points_bugs > 0f)
		{
			text = text + "\n<color=red>" + this.tS_.GetText(1761) + "</color>";
		}
		if (!this.isOnMarket && !this.inDevelopment && !this.schublade)
		{
			text = text + "\n<color=red>" + this.tS_.GetText(488) + "</color>";
		}
		return text;
	}

	
	public string GetDeveloperName()
	{
		if (!this.devS_)
		{
			this.FindMyDeveloper();
		}
		if (this.devS_)
		{
			return this.devS_.GetName();
		}
		return "";
	}

	
	public string GetPublisherName()
	{
		if (!this.pS_)
		{
			this.FindMyPublisher();
		}
		if (this.pS_)
		{
			return this.pS_.GetName();
		}
		return "";
	}

	
	public string GetGenreString()
	{
		return this.genres_.GetName(this.maingenre);
	}

	
	public string GetSubGenreString()
	{
		if (this.subgenre != -1)
		{
			return this.genres_.GetName(this.subgenre);
		}
		return "";
	}

	
	public string GetReleaseDateString()
	{
		if (this.inDevelopment)
		{
			return this.tS_.GetText(528);
		}
		if (this.schublade)
		{
			return this.tS_.GetText(1704);
		}
		return this.tS_.GetText(this.date_month + 220) + " " + this.date_year.ToString();
	}

	
	public string GetEntwicklungsbeginnDateString()
	{
		if (this.date_start_year == 0)
		{
			return "-";
		}
		return this.tS_.GetText(this.date_start_month + 220) + " " + this.date_start_year.ToString();
	}

	
	public Sprite GetScreenshot()
	{
		return this.genres_.GetScreenshot(this.maingenre, Mathf.RoundToInt(this.points_grafik));
	}

	
	public Texture2D GetScreenshotTexture2D()
	{
		return this.genres_.GetScreenshotTexture2D(this.maingenre, Mathf.RoundToInt(this.points_grafik));
	}

	
	public void FindNextFeatureForDevelopment()
	{
		Debug.Log("FindNextFeatureForDevelopment");
		this.devPointsStart = 0f;
		for (int i = 0; i < this.gameEngineFeature.Length; i++)
		{
			if (!this.engineFeature_DevDone[i])
			{
				this.engineFeature_DevDone[i] = true;
				this.devAktFeature = i - 4;
				this.devPointsStart = (float)this.eF_.GetDevPointsForGame(this.gameEngineFeature[i]);
				this.devPointsStart = this.CalcPlatformComplex(this.devPointsStart);
				this.devPoints = this.devPointsStart;
				return;
			}
		}
		for (int j = 0; j < this.gameGameplayFeatures.Length; j++)
		{
			if (!this.gameplayFeatures_DevDone[j] && this.gameGameplayFeatures[j])
			{
				this.gameplayFeatures_DevDone[j] = true;
				this.devAktFeature = j;
				this.devPointsStart = (float)this.gF_.GetDevPoints(j);
				this.devPointsStart = this.CalcPlatformComplex(this.devPointsStart);
				this.devPoints = this.devPointsStart;
				return;
			}
		}
	}

	
	public int GetGesamtDevPoints()
	{
		int num = 0;
		for (int i = 0; i < this.gameEngineFeature.Length; i++)
		{
			num += this.eF_.GetDevPointsForGame(this.gameEngineFeature[i]);
		}
		for (int j = 0; j < this.gameGameplayFeatures.Length; j++)
		{
			if (this.gameGameplayFeatures[j])
			{
				num += this.gF_.GetDevPoints(j);
			}
		}
		return Mathf.RoundToInt(this.CalcPlatformComplex((float)num));
	}

	
	public int GetGesamtDevPointsAddon()
	{
		int num = 0;
		for (int i = 0; i < this.gameEngineFeature.Length; i++)
		{
			if (!this.engineFeature_DevDone[i])
			{
				num += this.eF_.GetDevPointsForGame(this.gameEngineFeature[i]);
			}
		}
		for (int j = 0; j < this.gameGameplayFeatures.Length; j++)
		{
			if (this.gameGameplayFeatures[j] && !this.gameplayFeatures_DevDone[j])
			{
				num += this.gF_.GetDevPoints(j);
			}
		}
		return num;
	}

	
	public int GetRueckggeld()
	{
		int result;
		if (!this.typ_contractGame)
		{
			result = Mathf.RoundToInt((float)((int)this.costs_entwicklung / 100) * (100f - this.GetProzentGesamt()));
		}
		else
		{
			result = Mathf.RoundToInt((float)(-(float)this.auftragsspiel_gehalt) * 1.5f);
		}
		return result;
	}

	
	public long GetBisherigeEntwicklungskosten()
	{
		long num = (long)this.GetRueckggeld();
		num = this.costs_entwicklung - num;
		return num + this.costs_mitarbeiter;
	}

	
	public float GetProzentGesamt()
	{
		float num = 100f / this.devPointsStart_Gesamt * (this.devPointsStart_Gesamt - this.devPoints_Gesamt);
		if (num > 100f)
		{
			num = 100f;
		}
		return num;
	}

	
	public float GetProzentFeature()
	{
		float num = 100f / this.devPointsStart * (this.devPointsStart - this.devPoints);
		if (num > 100f)
		{
			num = 100f;
		}
		return num;
	}

	
	public long GetEntwicklungskosten()
	{
		return this.costs_entwicklung + this.costs_mitarbeiter;
	}

	
	public long GetGesamteEntwicklungskosten()
	{
		return this.costs_entwicklung + this.costs_mitarbeiter + this.costs_updates;
	}

	
	public long GetMarketingkosten()
	{
		return this.costs_marketing;
	}

	
	public long GetEnginegebuehren()
	{
		return this.costs_enginegebuehren;
	}

	
	public long GetUmsatzVerkauf()
	{
		return this.umsatzTotal - this.umsatzAbos - this.umsatzInApp;
	}

	
	public long GetUmsatzbeteiligung()
	{
		return this.umsatzTotal / 100L * (long)Mathf.RoundToInt((float)this.PUBOFFER_GetGewinnbeteiligung());
	}

	
	public int SubGewinnbeteiligung(int i)
	{
		float num = (float)i / 100f;
		num *= (float)this.PUBOFFER_GetGewinnbeteiligung();
		return Mathf.RoundToInt((float)(i - Mathf.RoundToInt(num)));
	}

	
	public void PayGewinnbeteiligung(long i)
	{
		if (this.PUBOFFER_GetGewinnbeteiligung() <= 0)
		{
			return;
		}
		float num = (float)i / 100f;
		num *= (float)this.PUBOFFER_GetGewinnbeteiligung();
		this.mS_.Pay((long)Mathf.RoundToInt(num), 26);
	}

	
	public long GetGesamtAusgaben()
	{
		return this.costs_entwicklung + this.costs_mitarbeiter + this.costs_marketing + this.costs_enginegebuehren + this.costs_server + this.costs_production + this.costs_updates;
	}

	
	public long GetGesamtGewinn()
	{
		long num;
		if (!this.pubOffer)
		{
			num = this.costs_entwicklung;
			num += this.costs_mitarbeiter;
			num += this.costs_marketing;
			num += this.costs_enginegebuehren;
			num += this.costs_server;
			num += this.costs_production;
			num += this.costs_updates;
		}
		else
		{
			num = (long)this.PUBOFFER_GetGarantiesumme();
			num += this.costs_marketing;
			num += this.costs_enginegebuehren;
			num += this.costs_server;
			num += this.costs_production;
			num += this.costs_updates;
			if (this.PUBOFFER_GetGewinnbeteiligung() > 0)
			{
				num += this.GetUmsatzbeteiligung();
			}
		}
		return this.umsatzTotal - num;
	}

	
	public string GetTypString()
	{
		if (!this.tS_)
		{
			return "";
		}
		string str = "";
		if (this.typ_contractGame)
		{
			str = " [" + this.tS_.GetText(598) + "]";
		}
		if (this.typ_bundle)
		{
			return this.tS_.GetText(977) + str;
		}
		if (this.typ_bundleAddon)
		{
			return this.tS_.GetText(1354) + str;
		}
		if (this.typ_goty)
		{
			return this.tS_.GetText(1359) + str;
		}
		if (this.typ_budget)
		{
			return this.tS_.GetText(978) + str;
		}
		if (this.typ_mmoaddon)
		{
			return this.tS_.GetText(646) + str;
		}
		if (this.typ_addon)
		{
			return this.tS_.GetText(645) + str;
		}
		if (this.typ_addonStandalone)
		{
			return this.tS_.GetText(979) + str;
		}
		if (this.typ_standard || this.gameTyp == 1 || this.gameTyp == 2)
		{
			switch (this.gameTyp)
			{
			case 0:
				return this.tS_.GetText(486) + str;
			case 1:
				return this.tS_.GetText(1244) + str;
			case 2:
				return this.tS_.GetText(1245) + str;
			}
		}
		if (this.typ_nachfolger)
		{
			return this.tS_.GetText(319) + str;
		}
		if (this.typ_remaster)
		{
			return this.tS_.GetText(320) + str;
		}
		if (this.typ_spinoff)
		{
			return this.tS_.GetText(1535) + str;
		}
		return "";
	}

	
	public string GetPlatformTypString()
	{
		if (!this.tS_)
		{
			return "";
		}
		if (this.herstellerExklusiv)
		{
			return this.tS_.GetText(1694);
		}
		if (this.exklusiv)
		{
			return this.tS_.GetText(364);
		}
		if (this.retro)
		{
			return this.tS_.GetText(903);
		}
		if (this.handy)
		{
			return this.tS_.GetText(1060);
		}
		if (this.arcade)
		{
			return this.tS_.GetText(1059);
		}
		if (!this.exklusiv && !this.retro && !this.herstellerExklusiv)
		{
			return this.tS_.GetText(902);
		}
		return "";
	}

	
	public Sprite GetSizeSprite()
	{
		if (!this.games_)
		{
			return null;
		}
		return this.games_.gameSizeSprites[this.gameSize];
	}

	
	public Sprite GetTypSprite()
	{
		if (!this.games_)
		{
			return null;
		}
		if (this.typ_contractGame)
		{
			return this.games_.gameTypSprites[3];
		}
		if (this.typ_bundle)
		{
			return this.games_.gameTypSprites[8];
		}
		if (this.typ_bundleAddon)
		{
			return this.games_.gameTypSprites[11];
		}
		if (this.typ_goty)
		{
			return this.games_.gameTypSprites[12];
		}
		if (this.typ_budget)
		{
			return this.games_.gameTypSprites[9];
		}
		if (this.typ_addon)
		{
			return this.games_.gameTypSprites[4];
		}
		if (this.typ_mmoaddon)
		{
			return this.games_.gameTypSprites[5];
		}
		if (this.typ_addonStandalone)
		{
			return this.games_.gameTypSprites[10];
		}
		if (this.typ_standard || this.gameTyp == 1 || this.gameTyp == 2)
		{
			switch (this.gameTyp)
			{
			case 0:
				return this.games_.gameTypSprites[0];
			case 1:
				return this.games_.gameTypSprites[6];
			case 2:
				return this.games_.gameTypSprites[7];
			}
		}
		if (this.gameTyp == 0)
		{
			if (this.typ_nachfolger)
			{
				return this.games_.gameTypSprites[1];
			}
			if (this.typ_remaster)
			{
				return this.games_.gameTypSprites[2];
			}
			if (this.typ_spinoff)
			{
				return this.games_.gameTypSprites[13];
			}
		}
		return null;
	}

	
	public Sprite GetPlatformTypSprite()
	{
		if (!this.games_)
		{
			return null;
		}
		if (this.herstellerExklusiv)
		{
			return this.games_.gamePlatformTypSprites[5];
		}
		if (this.exklusiv)
		{
			return this.games_.gamePlatformTypSprites[1];
		}
		if (this.retro)
		{
			return this.games_.gamePlatformTypSprites[2];
		}
		if (this.handy)
		{
			return this.games_.gamePlatformTypSprites[3];
		}
		if (this.arcade)
		{
			return this.games_.gamePlatformTypSprites[4];
		}
		return this.games_.gamePlatformTypSprites[0];
	}

	
	public void ClearReview()
	{
		this.date_month = 0;
		this.date_year = 0;
		this.reviewGameplay = 0;
		this.reviewGrafik = 0;
		this.reviewSound = 0;
		this.reviewSteuerung = 0;
		this.reviewTotal = 0;
	}

	
	public void CalcReview(bool entwicklungsbericht)
	{
		if (this.reviewTotal > 0)
		{
			return;
		}
		this.date_month = this.mS_.month;
		this.date_year = this.mS_.year;
		float num = 0f;
		if (this.developerID == this.mS_.myID)
		{
			if (!this.retro)
			{
				switch (this.mS_.difficulty)
				{
				case 0:
					num = 6000f * this.games_.GetReviewCurve();
					break;
				case 1:
					num = 7000f * this.games_.GetReviewCurve();
					break;
				case 2:
					num = 11000f * this.games_.GetReviewCurve();
					break;
				case 3:
					num = 14000f * this.games_.GetReviewCurve();
					break;
				case 4:
					num = 17000f * this.games_.GetReviewCurve();
					break;
				case 5:
					num = 20000f * this.games_.GetReviewCurve();
					break;
				}
			}
			else
			{
				switch (this.mS_.difficulty)
				{
				case 0:
					num = 2500f * this.games_.GetReviewCurve();
					break;
				case 1:
					num = 3000f * this.games_.GetReviewCurve();
					break;
				case 2:
					num = 3500f * this.games_.GetReviewCurve();
					break;
				case 3:
					num = 4000f * this.games_.GetReviewCurve();
					break;
				case 4:
					num = 4200f * this.games_.GetReviewCurve();
					break;
				case 5:
					num = 4500f * this.games_.GetReviewCurve();
					break;
				}
			}
		}
		else if (!this.retro)
		{
			num = 14000f * this.games_.GetReviewCurve();
		}
		else
		{
			num = 4000f * this.games_.GetReviewCurve();
		}
		float num2 = this.points_gameplay / (num / 100f);
		float num3 = this.points_grafik / (num / 100f);
		float num4 = this.points_sound / (num / 100f);
		float num5 = this.points_technik / (num / 100f);
		float num6 = 0f;
		if (num2 > 99f)
		{
			num2 = 99f;
		}
		if (num3 > 99f)
		{
			num3 = 99f;
		}
		if (num4 > 99f)
		{
			num4 = 99f;
		}
		if (num5 > 99f)
		{
			num5 = 99f;
		}
		if (this.developerID != this.mS_.myID && this.retro)
		{
			num3 *= 0.7f;
			num4 *= 0.7f;
			num5 *= 0.7f;
		}
		if (this.developerID == this.mS_.myID)
		{
			float num7 = 0f;
			int num8 = 0;
			for (int i = 0; i < this.gamePlatform.Length; i++)
			{
				if (this.gamePlatform[i] != -1)
				{
					if (!this.gamePlatformScript[i])
					{
						this.FindMyPlatforms();
					}
					if (this.gamePlatformScript[i])
					{
						num8++;
						num7 += (float)this.gamePlatformScript[i].erfahrung;
					}
				}
			}
			num7 /= (float)num8;
			num3 -= (5f - num7) * 2f;
			num4 -= (5f - num7) * 2f;
			num5 -= (5f - num7) * 2f;
		}
		else
		{
			num3 -= UnityEngine.Random.Range(0f, 5f);
			num4 -= UnityEngine.Random.Range(0f, 5f);
			num5 -= UnityEngine.Random.Range(0f, 5f);
		}
		if (this.developerID == this.mS_.myID && this.mS_.year >= 1979)
		{
			if (!this.gameplayStudio[0])
			{
				num2 -= 1f;
			}
			if (!this.gameplayStudio[1])
			{
				num2 -= 1f;
			}
			if (!this.gameplayStudio[2])
			{
				num2 -= 1f;
			}
			if (!this.gameplayStudio[3])
			{
				num2 -= 1f;
			}
			if (!this.gameplayStudio[4])
			{
				num2 -= 1f;
			}
			if (!this.gameplayStudio[5])
			{
				num2 -= 1f;
			}
		}
		if (this.mS_.year >= 1982)
		{
			if (!this.grafikStudio[0])
			{
				num3 -= 1f;
			}
			if (!this.grafikStudio[1])
			{
				num3 -= 1f;
			}
			if (!this.grafikStudio[2])
			{
				num3 -= 1f;
			}
			if (!this.grafikStudio[3])
			{
				num3 -= 1f;
			}
			if (!this.grafikStudio[4])
			{
				num3 -= 1f;
			}
			if (!this.grafikStudio[5])
			{
				num3 -= 1f;
			}
		}
		if (this.mS_.year >= 1985)
		{
			if (!this.soundStudio[0])
			{
				num4 -= 2f;
			}
			if (!this.soundStudio[1])
			{
				num4 -= 2f;
			}
			if (!this.soundStudio[2])
			{
				num4 -= 2f;
			}
			if (!this.soundStudio[3])
			{
				num4 -= 2f;
			}
			if (!this.soundStudio[4])
			{
				num4 -= 2f;
			}
			if (!this.soundStudio[5])
			{
				num4 -= 2f;
			}
		}
		if (this.unlock_.Get(8))
		{
			if (!this.motionCaptureStudio[0])
			{
				num3 -= 1f;
			}
			if (!this.motionCaptureStudio[1])
			{
				num3 -= 1f;
			}
			if (!this.motionCaptureStudio[2])
			{
				num3 -= 1f;
			}
			if (!this.motionCaptureStudio[3])
			{
				num3 -= 1f;
			}
			if (!this.motionCaptureStudio[4])
			{
				num3 -= 1f;
			}
			if (!this.motionCaptureStudio[5])
			{
				num3 -= 1f;
			}
		}
		if (this.developerID == this.mS_.myID)
		{
			int outdatetAmount = this.eF_.GetOutdatetAmount(this.gameEngineFeature[0]);
			int outdatetAmount2 = this.eF_.GetOutdatetAmount(this.gameEngineFeature[1]);
			int outdatetAmount3 = this.eF_.GetOutdatetAmount(this.gameEngineFeature[2]);
			int outdatetAmount4 = this.eF_.GetOutdatetAmount(this.gameEngineFeature[3]);
			if (outdatetAmount > 0)
			{
				num3 -= (float)(outdatetAmount * 2);
			}
			if (outdatetAmount2 > 0)
			{
				num4 -= (float)(outdatetAmount2 * 2);
			}
			if (outdatetAmount3 > 0)
			{
				num2 -= (float)outdatetAmount3;
			}
			if (outdatetAmount4 > 0)
			{
				num2 -= (float)outdatetAmount3;
			}
		}
		if (this.developerID == this.mS_.myID)
		{
			if (this.mS_.year >= 1983 && this.mS_.year < 1988 && this.gameSize == 0)
			{
				num2 -= 1f;
				num3 -= 1f;
				num4 -= 1f;
				num5 -= 1f;
			}
			if (this.mS_.year >= 1988 && this.mS_.year < 1995)
			{
				if (this.gameSize == 0)
				{
					num2 -= 2f;
					num3 -= 2f;
					num4 -= 2f;
					num5 -= 2f;
				}
				if (this.gameSize == 1)
				{
					num2 -= 1f;
					num3 -= 1f;
					num4 -= 1f;
					num5 -= 1f;
				}
			}
			if (this.mS_.year >= 1995 && this.mS_.year < 2004)
			{
				if (this.gameSize == 0)
				{
					num2 -= 3f;
					num3 -= 3f;
					num4 -= 3f;
					num5 -= 3f;
				}
				if (this.gameSize == 1)
				{
					num2 -= 2f;
					num3 -= 2f;
					num4 -= 2f;
					num5 -= 2f;
				}
				if (this.gameSize == 2)
				{
					num2 -= 1f;
					num3 -= 1f;
					num4 -= 1f;
					num5 -= 1f;
				}
			}
			if (this.mS_.year >= 2004)
			{
				if (this.gameSize == 0)
				{
					num2 -= 4f;
					num3 -= 4f;
					num4 -= 4f;
					num5 -= 4f;
				}
				if (this.gameSize == 1)
				{
					num2 -= 3f;
					num3 -= 3f;
					num4 -= 3f;
					num5 -= 3f;
				}
				if (this.gameSize == 2)
				{
					num2 -= 2f;
					num3 -= 2f;
					num4 -= 2f;
					num5 -= 2f;
				}
				if (this.gameSize == 3)
				{
					num2 -= 1f;
					num3 -= 1f;
					num4 -= 1f;
					num5 -= 1f;
				}
			}
		}
		if (this.developerID != this.mS_.myID)
		{
			num2 -= UnityEngine.Random.Range(0f, 2f);
			num3 -= UnityEngine.Random.Range(0f, 2f);
			num4 -= UnityEngine.Random.Range(0f, 2f);
			num5 -= UnityEngine.Random.Range(0f, 2f);
		}
		num6 += num2 * 0.01f * this.genres_.genres_GAMEPLAY[this.maingenre];
		num6 += num3 * 0.01f * this.genres_.genres_GRAPHIC[this.maingenre];
		num6 += num4 * 0.01f * this.genres_.genres_SOUND[this.maingenre];
		num6 += num5 * 0.01f * this.genres_.genres_CONTROL[this.maingenre];
		num3 -= this.points_bugs * 0.1f;
		num4 -= this.points_bugs * 0.1f;
		num5 -= this.points_bugs * 0.1f;
		num2 -= this.points_bugs * 0.2f;
		num6 -= this.points_bugs * 0.1f;
		if (!this.genres_.IsTargetGroup(this.maingenre, this.gameZielgruppe))
		{
			num2 -= 4f;
			num6 -= 3f;
		}
		if (!this.genres_.IsGenreCombination(this.maingenre, this.subgenre))
		{
			num2 -= 4f;
			num6 -= 3f;
		}
		if (!this.themes_.IsThemesFitWithGenre(this.gameMainTheme, this.maingenre))
		{
			num2 -= 4f;
			num6 -= 3f;
		}
		if (!this.themes_.IsThemesFitWithGenre(this.gameSubTheme, this.maingenre))
		{
			num2 -= 2f;
			num6 -= 1.5f;
		}
		for (int j = 0; j < this.Designschwerpunkt.Length; j++)
		{
			if (this.Designschwerpunkt[j] < this.genres_.GetFocus(j, this.maingenre, this.subgenre))
			{
				float num9 = (float)(this.genres_.GetFocus(j, this.maingenre, this.subgenre) - this.Designschwerpunkt[j]);
				switch (this.mS_.difficulty)
				{
				case 0:
					num2 -= num9 * 0.3f;
					num6 -= num9 * 0.2f;
					break;
				case 1:
					num2 -= num9 * 0.4f;
					num6 -= num9 * 0.3f;
					break;
				case 2:
					num2 -= num9 * 0.5f;
					num6 -= num9 * 0.4f;
					break;
				case 3:
					num2 -= num9 * 0.8f;
					num6 -= num9 * 0.5f;
					break;
				case 4:
					num2 -= num9 * 0.9f;
					num6 -= num9 * 0.6f;
					break;
				case 5:
					num2 -= num9 * 1f;
					num6 -= num9 * 0.7f;
					break;
				}
			}
		}
		for (int k = 0; k < this.Designausrichtung.Length; k++)
		{
			float num10 = (float)Mathf.Abs(this.Designausrichtung[k] - this.genres_.GetAlign(k, this.maingenre, this.subgenre));
			switch (this.mS_.difficulty)
			{
			case 0:
				num2 -= num10 * 0.3f;
				num6 -= num10 * 0.2f;
				break;
			case 1:
				num2 -= num10 * 0.4f;
				num6 -= num10 * 0.3f;
				break;
			case 2:
				num2 -= num10 * 0.5f;
				num6 -= num10 * 0.4f;
				break;
			case 3:
				num2 -= num10 * 0.8f;
				num6 -= num10 * 0.5f;
				break;
			case 4:
				num2 -= num10 * 0.9f;
				num6 -= num10 * 0.6f;
				break;
			case 5:
				num2 -= num10 * 1f;
				num6 -= num10 * 0.7f;
				break;
			}
		}
		if (this.developerID == this.mS_.myID)
		{
			float num10 = (5f - (float)this.genres_.genres_LEVEL[this.maingenre]) * 0.4f;
			num2 -= num10 * 2f;
			num6 -= num10 * 1.5f;
		}
		else if (this.developerID == this.mS_.myID)
		{
			if (this.subgenre >= 0)
			{
				float num10 = (5f - (float)this.genres_.genres_LEVEL[this.subgenre]) * 0.2f;
				num2 -= num10 * 2f;
				num6 -= num10 * 1.5f;
			}
			else
			{
				num2 -= 2f;
				num6 -= 1.5f;
			}
		}
		if (this.developerID == this.mS_.myID)
		{
			float num10 = (5f - (float)this.themes_.themes_LEVEL[this.gameMainTheme]) * 0.4f;
			num2 -= num10 * 2f;
			num6 -= num10 * 1.5f;
		}
		if (this.developerID == this.mS_.myID)
		{
			if (this.gameSubTheme >= 0)
			{
				float num10 = (5f - (float)this.themes_.themes_LEVEL[this.gameSubTheme]) * 0.2f;
				num2 -= num10 * 2f;
				num6 -= num10 * 1.5f;
			}
			else
			{
				num2 -= 2f;
				num6 -= 1.5f;
			}
		}
		if (this.developerID == this.mS_.myID)
		{
			if (this.typ_addon || this.typ_addonStandalone)
			{
				float num11 = 0.4f;
				num11 -= this.addonQuality;
				num2 -= num2 * num11;
				num6 -= num6 * num11;
			}
			if (this.typ_mmoaddon)
			{
				float num12 = 0.4f;
				num12 -= this.addonQuality;
				num2 -= num2 * num12;
				num6 -= num6 * num12;
			}
		}
		if (this.developerID == this.mS_.myID && this.finanzierung_Grundkosten < 100)
		{
			float num13 = (float)this.finanzierung_Grundkosten;
			num13 *= 0.01f;
			float num14 = num6 - num6 * num13;
			num6 -= num14 * 0.5f;
			num14 = num6 - num2 * num13;
			num2 -= num14 * 0.5f;
			num14 = num3 - num3 * num13;
			num3 -= num14 * 0.2f;
			num14 = num4 - num4 * num13;
			num4 -= num14 * 0.3f;
			num14 = num5 - num5 * num13;
			num5 -= num14 * 0.2f;
		}
		if (!entwicklungsbericht && this.specialMarketing[1] != 0)
		{
			num2 += (float)this.specialMarketing[1];
			num3 += (float)this.specialMarketing[1];
			num4 += (float)this.specialMarketing[1];
			num5 += (float)this.specialMarketing[1];
			num6 += (float)this.specialMarketing[1];
		}
		if (this.developerID == this.mS_.myID && this.mS_.GetFanGenreID() == this.maingenre)
		{
			num6 += 3f;
		}
		if (this.developerID == this.mS_.myID)
		{
			switch (this.mS_.difficulty)
			{
			case 0:
				num2 += 4f;
				num3 += 4f;
				num4 += 4f;
				num5 += 4f;
				num6 += 4f;
				break;
			case 1:
				num2 += 3f;
				num3 += 3f;
				num4 += 3f;
				num5 += 3f;
				num6 += 3f;
				break;
			case 2:
				num2 += 2f;
				num3 += 2f;
				num4 += 2f;
				num5 += 2f;
				num6 += 2f;
				break;
			case 3:
				num2 += 1f;
				num3 += 1f;
				num4 += 1f;
				num5 += 1f;
				num6 += 1f;
				break;
			case 4:
				num2 += 0.5f;
				num3 += 0.5f;
				num4 += 0.5f;
				num5 += 0.5f;
				num6 += 0.5f;
				break;
			case 5:
				num2 += 0f;
				num3 += 0f;
				num4 += 0f;
				num5 += 0f;
				num6 += 0f;
				break;
			}
		}
		if (!entwicklungsbericht && this.mS_.settings_RandomReviews)
		{
			num2 += (float)UnityEngine.Random.Range(-10, 10);
			num3 += (float)UnityEngine.Random.Range(-10, 10);
			num4 += (float)UnityEngine.Random.Range(-10, 10);
			num5 += (float)UnityEngine.Random.Range(-10, 10);
			num6 += (float)UnityEngine.Random.Range(-10, 10);
		}
		if (this.developerID != this.mS_.myID)
		{
			if (!this.devS_)
			{
				this.FindMyDeveloper();
			}
			if (this.devS_ && !this.devS_.IsTochterfirma())
			{
				switch (this.mS_.difficulty)
				{
				case 0:
					num3 -= (float)UnityEngine.Random.Range(10, 20);
					num4 -= (float)UnityEngine.Random.Range(10, 20);
					num5 -= (float)UnityEngine.Random.Range(10, 20);
					num6 -= (float)UnityEngine.Random.Range(10, 20);
					break;
				case 1:
					num3 -= (float)UnityEngine.Random.Range(5, 10);
					num4 -= (float)UnityEngine.Random.Range(5, 10);
					num5 -= (float)UnityEngine.Random.Range(5, 10);
					num6 -= (float)UnityEngine.Random.Range(5, 10);
					break;
				case 2:
					num3 -= (float)UnityEngine.Random.Range(0, 5);
					num4 -= (float)UnityEngine.Random.Range(0, 5);
					num5 -= (float)UnityEngine.Random.Range(0, 5);
					num6 -= (float)UnityEngine.Random.Range(0, 5);
					break;
				case 3:
					num2 += (float)UnityEngine.Random.Range(0, 8);
					num3 += (float)UnityEngine.Random.Range(0, 2);
					num4 += (float)UnityEngine.Random.Range(0, 2);
					num5 += (float)UnityEngine.Random.Range(0, 2);
					num6 += (float)UnityEngine.Random.Range(0, 4);
					break;
				case 4:
					num2 += (float)UnityEngine.Random.Range(0, 12);
					num3 += (float)UnityEngine.Random.Range(0, 4);
					num4 += (float)UnityEngine.Random.Range(0, 4);
					num5 += (float)UnityEngine.Random.Range(0, 4);
					num6 += (float)UnityEngine.Random.Range(0, 8);
					break;
				case 5:
					num2 += (float)UnityEngine.Random.Range(0, 16);
					num3 += (float)UnityEngine.Random.Range(0, 6);
					num4 += (float)UnityEngine.Random.Range(0, 6);
					num5 += (float)UnityEngine.Random.Range(0, 6);
					num6 += (float)UnityEngine.Random.Range(0, 12);
					break;
				}
			}
			if (this.sonderIP)
			{
				num2 = (float)UnityEngine.Random.Range(this.sonderIPMindestreview - 5, this.sonderIPMindestreview + 5);
				num3 = (float)UnityEngine.Random.Range(this.sonderIPMindestreview - 5, this.sonderIPMindestreview + 5);
				num4 = (float)UnityEngine.Random.Range(this.sonderIPMindestreview - 5, this.sonderIPMindestreview + 5);
				num5 = (float)UnityEngine.Random.Range(this.sonderIPMindestreview - 5, this.sonderIPMindestreview + 5);
				num6 = (float)UnityEngine.Random.Range(this.sonderIPMindestreview - 5, this.sonderIPMindestreview + 5);
				if (!this.devS_)
				{
					this.FindMyDeveloper();
				}
				if (this.devS_ && this.devS_.IsTochterfirma())
				{
					switch (this.devS_.tf_entwicklungsdauer)
					{
					case 0:
						num2 -= 10f;
						num3 -= 10f;
						num4 -= 10f;
						num5 -= 10f;
						num6 -= 10f;
						break;
					case 1:
						num2 -= 5f;
						num3 -= 5f;
						num4 -= 5f;
						num5 -= 5f;
						num6 -= 5f;
						break;
					}
				}
			}
		}
		if (num6 >= 98f)
		{
			num6 = 98f;
			if (UnityEngine.Random.Range(0, 25) == 1)
			{
				num6 = 99f;
			}
			if (UnityEngine.Random.Range(0, 50) == 1)
			{
				num6 = 100f;
			}
		}
		if (num2 >= 98f)
		{
			num2 = 98f;
			if (UnityEngine.Random.Range(0, 10) == 1)
			{
				num2 = 99f;
			}
			if (UnityEngine.Random.Range(0, 25) == 1)
			{
				num2 = 100f;
			}
		}
		if (num3 >= 98f)
		{
			num3 = 98f;
			if (UnityEngine.Random.Range(0, 10) == 1)
			{
				num3 = 99f;
			}
			if (UnityEngine.Random.Range(0, 25) == 1)
			{
				num3 = 100f;
			}
		}
		if (num4 >= 98f)
		{
			num4 = 98f;
			if (UnityEngine.Random.Range(0, 10) == 1)
			{
				num4 = 99f;
			}
			if (UnityEngine.Random.Range(0, 25) == 1)
			{
				num4 = 100f;
			}
		}
		if (num5 >= 98f)
		{
			num5 = 98f;
			if (UnityEngine.Random.Range(0, 10) == 1)
			{
				num5 = 99f;
			}
			if (UnityEngine.Random.Range(0, 25) == 1)
			{
				num5 = 100f;
			}
		}
		this.reviewGameplay = Mathf.RoundToInt(num2);
		this.reviewGrafik = Mathf.RoundToInt(num3);
		this.reviewSound = Mathf.RoundToInt(num4);
		this.reviewSteuerung = Mathf.RoundToInt(num5);
		this.reviewTotal = Mathf.RoundToInt(num6);
		if (this.reviewGameplay < 1)
		{
			this.reviewGameplay = 1;
		}
		if (this.reviewGrafik < 1)
		{
			this.reviewGrafik = 1;
		}
		if (this.reviewSound < 1)
		{
			this.reviewSound = 1;
		}
		if (this.reviewSteuerung < 1)
		{
			this.reviewSteuerung = 1;
		}
		if (this.reviewTotal < 1)
		{
			this.reviewTotal = 1;
		}
		if (this.reviewGameplay > 100)
		{
			this.reviewGameplay = 100;
		}
		if (this.reviewGrafik > 100)
		{
			this.reviewGrafik = 100;
		}
		if (this.reviewSound > 100)
		{
			this.reviewSound = 100;
		}
		if (this.reviewSteuerung > 100)
		{
			this.reviewSteuerung = 100;
		}
		if (this.reviewTotal > 100)
		{
			this.reviewTotal = 100;
		}
		if (this.developerID != this.mS_.myID && this.reviewTotal <= 1)
		{
			if (this.reviewGameplay <= 1)
			{
				this.reviewGameplay = UnityEngine.Random.Range(2, 10);
			}
			if (this.reviewGrafik <= 1)
			{
				this.reviewGrafik = UnityEngine.Random.Range(2, 10);
			}
			if (this.reviewSound <= 1)
			{
				this.reviewSound = UnityEngine.Random.Range(2, 10);
			}
			if (this.reviewSteuerung <= 1)
			{
				this.reviewSteuerung = UnityEngine.Random.Range(2, 10);
			}
			if (this.reviewTotal <= 1)
			{
				this.reviewTotal = UnityEngine.Random.Range(2, 10);
			}
		}
		if (!entwicklungsbericht && !this.typ_addon && !this.typ_addonStandalone && !this.typ_budget && !this.typ_bundle && !this.typ_mmoaddon && !this.typ_goty && !this.typ_bundleAddon)
		{
			if (!this.devS_)
			{
				this.FindMyDeveloper();
			}
			if (this.reviewTotal >= 80)
			{
				this.devS_.awards[8]++;
			}
			if (this.reviewTotal < 30)
			{
				this.devS_.awards[9]++;
			}
		}
	}

	
	public void SetPublisher(int id_)
	{
		this.pS_ = null;
		this.publisherID = id_;
	}

	
	public void AddIpPoints(float p)
	{
		if (this.mainIP != -1)
		{
			this.ipTime = 0;
			if (!this.script_mainIP)
			{
				this.FindMainIpScript();
			}
			if (this.script_mainIP && !this.typ_bundle && !this.typ_budget && !this.typ_bundleAddon && !this.typ_goty && !this.typ_contractGame)
			{
				this.script_mainIP.ipPunkte += p;
				this.script_mainIP.ipTime = 0;
				if (this.script_mainIP.ipPunkte < 0f)
				{
					this.script_mainIP.ipPunkte = 0f;
				}
				if (this.developerID == this.mS_.myID && this.mS_.achScript_ && this.script_mainIP.ipPunkte >= 990f)
				{
					this.mS_.achScript_.SetAchivement(57);
				}
				if (this.mS_.multiplayer)
				{
					if (this.mS_.mpCalls_.isServer)
					{
						this.mS_.mpCalls_.SERVER_Send_GameData(this);
					}
					if (this.mS_.mpCalls_.isClient && this.developerID == this.mS_.myID)
					{
						this.mS_.mpCalls_.CLIENT_Send_GameData(this);
					}
				}
			}
		}
		if (this.script_mainIP.ipPunkte < 0f)
		{
			this.script_mainIP.ipPunkte = 0f;
		}
	}

	
	public void AddIpPointsRelease()
	{
		if (this.mainIP != -1)
		{
			if (!this.script_mainIP)
			{
				this.FindMainIpScript();
			}
			if (this.script_mainIP && !this.typ_bundle && !this.typ_budget && !this.typ_bundleAddon && !this.typ_goty && !this.typ_contractGame)
			{
				if (this.reviewTotal < 40)
				{
					float num = (float)this.reviewTotal;
					if (this.portID != -1)
					{
						this.AddIpPoints(-(num * 0.4f));
						return;
					}
					if (this.typ_nachfolger)
					{
						this.AddIpPoints(-num);
						return;
					}
					if (this.typ_addon || this.typ_addonStandalone)
					{
						this.AddIpPoints(-(num * 0.25f));
						return;
					}
					if (this.typ_mmoaddon)
					{
						this.AddIpPoints(-(num * 0.05f));
						return;
					}
					if (this.typ_remaster)
					{
						this.AddIpPoints(-(num * 0.5f));
						return;
					}
					if (this.typ_spinoff)
					{
						this.AddIpPoints(-(num * 0.9f));
						return;
					}
					this.AddIpPoints(-num);
					return;
				}
				else
				{
					float num;
					if (this.developerID == this.mS_.myID)
					{
						num = (float)(this.reviewTotal * 2 / (this.mS_.difficulty + 1));
					}
					else
					{
						num = (float)(this.reviewTotal * 2 / 2);
					}
					if (this.script_mainIP.developerID == this.mS_.myID)
					{
						if (this.script_mainIP.ipTime < 8)
						{
							num *= 0.2f;
						}
						else if ((this.typ_nachfolger || this.typ_remaster || this.typ_spinoff) && this.script_mainIP.ipTime < 30)
						{
							num *= 0.5f;
						}
					}
					if (this.portID != -1)
					{
						this.AddIpPoints(num * 0.4f);
						return;
					}
					if (this.typ_nachfolger)
					{
						this.AddIpPoints(num);
						return;
					}
					if (this.typ_addon || this.typ_addonStandalone)
					{
						this.AddIpPoints(num * 0.25f);
						return;
					}
					if (this.typ_mmoaddon)
					{
						this.AddIpPoints(num * 0.05f);
						return;
					}
					if (this.typ_remaster)
					{
						this.AddIpPoints(num * 0.5f);
						return;
					}
					if (this.typ_spinoff)
					{
						this.AddIpPoints(num * 0.9f);
						return;
					}
					this.AddIpPoints(num);
					return;
				}
			}
		}
	}

	
	public void SetAsContractGameAngebot()
	{
		this.auftragsspiel = true;
		this.auftragsspiel_wochenAlsAngebot = 0;
		this.auftragsspiel_zeitAbgelaufen = false;
		this.auftragsspiel_Inivs = false;
		switch (this.gameSize)
		{
		case 0:
			this.auftragsspiel_mindestbewertung = UnityEngine.Random.Range(1, 5) * 5;
			break;
		case 1:
			this.auftragsspiel_mindestbewertung = UnityEngine.Random.Range(2, 7) * 5;
			break;
		case 2:
			this.auftragsspiel_mindestbewertung = UnityEngine.Random.Range(6, 11) * 5;
			break;
		case 3:
			this.auftragsspiel_mindestbewertung = UnityEngine.Random.Range(10, 15) * 5;
			break;
		case 4:
			this.auftragsspiel_mindestbewertung = UnityEngine.Random.Range(13, 18) * 5;
			break;
		}
		this.auftragsspiel_zeitInWochen = 20 + this.gameSize * 20 + UnityEngine.Random.Range(5, 20);
		Menu_DevGame component = this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>();
		float num = 0f;
		num += (float)component.costs_gameSize[this.gameSize];
		num += (float)component.costs_gameTyp[0];
		switch (this.mS_.difficulty)
		{
		case 0:
			num *= 1.6f;
			break;
		case 1:
			num *= 1.4f;
			break;
		case 2:
			num *= 1.2f;
			break;
		case 3:
			num *= 1f;
			break;
		case 4:
			num *= 0.9f;
			break;
		case 5:
			num *= 0.8f;
			break;
		}
		this.auftragsspiel_gehalt = 10000 + Mathf.RoundToInt(num) / 100 * this.auftragsspiel_mindestbewertung;
		this.auftragsspiel_bonus = 100 * UnityEngine.Random.Range(0, 100) + Mathf.RoundToInt(num * (2.5f + this.mS_.auftragsAnsehen * 0.01f));
		this.auftragsspiel_gehalt += UnityEngine.Random.Range(0, 20) * 100;
		this.auftragsspiel_bonus += UnityEngine.Random.Range(0, 20) * 100;
		if (this.gameSize != 0)
		{
			this.auftragsspiel_gehalt += component.costs_gameSize[this.gameSize] * (this.mS_.difficulty + 1) / 2;
			this.auftragsspiel_bonus += component.costs_gameSize[this.gameSize] * (this.mS_.difficulty + 1) / 2;
		}
		else
		{
			this.auftragsspiel_gehalt += component.costs_gameSize[this.gameSize] / 2;
			this.auftragsspiel_bonus += component.costs_gameSize[this.gameSize] / 2;
		}
		if (this.mS_.multiplayer && this.mS_.mpCalls_.isServer)
		{
			this.mS_.mpCalls_.SERVER_Send_Game(this);
		}
	}

	
	public void SetAsPublishingAngebot()
	{
		this.pubAngebot = true;
		this.pubAngebot_Weeks = 0;
		this.pubAngebot_Stimmung = 100f;
		this.pubAngebot_VerhandlungProzent = 100f;
		this.pubAngebot_Verhandlung = (float)UnityEngine.Random.Range(2, 15);
		if (!this.unlock_.Get(59))
		{
			this.pubAngebot_Retail = true;
			this.pubAngebot_Digital = false;
		}
		else
		{
			this.pubAngebot_Retail = true;
			this.pubAngebot_Digital = true;
			if (UnityEngine.Random.Range(0, 100) > 50)
			{
				this.pubAngebot_Retail = false;
				this.pubAngebot_Digital = true;
			}
			if (UnityEngine.Random.Range(0, 100) > 50)
			{
				this.pubAngebot_Retail = true;
				this.pubAngebot_Digital = false;
			}
		}
		switch (this.gameSize)
		{
		case 0:
			this.pubAngebot_Garantiesumme = Mathf.RoundToInt((float)this.reviewTotal) * 10000 + UnityEngine.Random.Range(1, 100) * 1000;
			break;
		case 1:
			this.pubAngebot_Garantiesumme = Mathf.RoundToInt((float)this.reviewTotal) * 20000 + UnityEngine.Random.Range(1, 100) * 2000;
			break;
		case 2:
			this.pubAngebot_Garantiesumme = Mathf.RoundToInt((float)this.reviewTotal) * 40000 + UnityEngine.Random.Range(1, 100) * 4000;
			break;
		case 3:
			this.pubAngebot_Garantiesumme = Mathf.RoundToInt((float)this.reviewTotal) * 80000 + UnityEngine.Random.Range(1, 100) * 8000;
			break;
		case 4:
			this.pubAngebot_Garantiesumme = Mathf.RoundToInt((float)this.reviewTotal) * 160000 + UnityEngine.Random.Range(1, 100) * 16000;
			break;
		}
		this.pubAngebot_Gewinnbeteiligung = (float)(this.reviewTotal / 2 + UnityEngine.Random.Range(1, 15));
		if (this.pubAngebot_Gewinnbeteiligung > 80f)
		{
			this.pubAngebot_Gewinnbeteiligung = 80f;
		}
		if (this.mS_.multiplayer && this.mS_.mpCalls_.isServer)
		{
			this.mS_.mpCalls_.SERVER_Send_Game(this);
		}
	}

	
	public void SetOnMarket()
	{
		bool flag = false;
		this.inDevelopment = false;
		this.isOnMarket = true;
		this.schublade = false;
		this.pubAngebot = false;
		this.auftragsspiel = false;
		if (this.typ_contractGame && this.developerID == this.mS_.myID)
		{
			Debug.Log("SetOnMarktet() -> Contract Game");
			flag = true;
		}
		if (this.gameTab_ && this.gameTab_.gameObject)
		{
			UnityEngine.Object.Destroy(this.gameTab_.gameObject);
			this.gameTab_ = null;
		}
		this.InitUI();
		this.AddIpPointsRelease();
		if (this.points_bugsInvis > 0f)
		{
			if (this.typ_bundle)
			{
				this.points_bugsInvis = 0f;
			}
			if (this.typ_bundleAddon)
			{
				this.points_bugsInvis = 0f;
			}
			if (this.typ_contractGame)
			{
				this.points_bugsInvis = 0f;
			}
			if (this.typ_budget)
			{
				this.points_bugsInvis = 0f;
			}
			if (this.typ_goty)
			{
				this.points_bugsInvis = 0f;
			}
		}
		if (this.gameTyp == 2)
		{
			this.retailVersion = false;
			this.digitalVersion = true;
			for (int i = 0; i < this.verkaufspreis.Length; i++)
			{
				this.verkaufspreis[i] = 0;
			}
		}
		if (this.handy)
		{
			this.retailVersion = false;
			this.digitalVersion = true;
		}
		if (this.arcade)
		{
			this.retailVersion = true;
			this.digitalVersion = false;
			for (int j = 1; j < this.verkaufspreis.Length; j++)
			{
				this.verkaufspreis[j] = 0;
			}
		}
		if (this.developerID == this.mS_.myID && this.points_bugs >= 50f)
		{
			this.mS_.sauerBugs = UnityEngine.Random.Range(4, 16);
		}
		for (int k = 0; k < this.gamePlatform.Length; k++)
		{
			if (this.gamePlatform[k] != -1)
			{
				if (!this.gamePlatformScript[k])
				{
					this.FindMyPlatforms();
				}
				if (this.gamePlatformScript[k])
				{
					this.gamePlatformScript[k].games++;
					if (this.exklusiv)
					{
						this.gamePlatformScript[k].exklusivGames++;
					}
				}
			}
		}
		if (this.typ_addon)
		{
			if (!this.script_vorgaenger)
			{
				this.FindVorgaengerScript();
			}
			if (this.script_vorgaenger)
			{
				this.script_vorgaenger.amountAddons++;
				float num = (float)this.reviewTotal;
				this.script_vorgaenger.bonusSellsAddons += num * 0.01f / (float)this.script_vorgaenger.amountAddons;
			}
		}
		if (this.typ_addonStandalone)
		{
			if (!this.script_vorgaenger)
			{
				this.FindVorgaengerScript();
			}
			if (this.script_vorgaenger)
			{
				this.script_vorgaenger.amountAddons++;
			}
		}
		if (this.typ_mmoaddon)
		{
			if (!this.script_vorgaenger)
			{
				this.FindVorgaengerScript();
			}
			if (this.script_vorgaenger)
			{
				this.script_vorgaenger.amountMMOAddons++;
				this.script_vorgaenger.AddMMOInteresse((float)this.reviewTotal);
			}
		}
		if (this.IsMyGame())
		{
			if ((this.typ_standard || this.typ_nachfolger || this.typ_remaster || this.typ_spinoff) && !this.typ_contractGame && !this.typ_addon && !this.typ_addonStandalone && !this.typ_budget && !this.typ_bundle && !this.typ_bundleAddon && !this.typ_mmoaddon && !this.typ_goty && !this.arcade && this.portID == -1)
			{
				this.mS_.lastGamesGenre[0] = this.mS_.lastGamesGenre[1];
				this.mS_.lastGamesGenre[1] = this.mS_.lastGamesGenre[2];
				this.mS_.lastGamesGenre[2] = this.mS_.lastGamesGenre[3];
				this.mS_.lastGamesGenre[3] = this.mS_.lastGamesGenre[4];
				this.mS_.lastGamesGenre[4] = this.maingenre;
			}
			float num2 = 1f;
			if (this.typ_remaster)
			{
				num2 = 0.5f;
			}
			if (this.portID != -1)
			{
				num2 = 0.2f;
			}
			if (this.typ_addon || this.typ_addonStandalone || this.typ_mmoaddon)
			{
				num2 = 0.1f;
			}
			if (this.typ_budget || this.typ_bundle || this.typ_bundleAddon || this.typ_goty)
			{
				num2 = 0.05f;
			}
			if (this.pubOffer)
			{
				num2 = 0.2f;
			}
			if (this.reviewTotal < 10)
			{
				this.mS_.AddStudioPoints(Mathf.RoundToInt(1f * num2));
			}
			if (this.reviewTotal >= 10 && this.reviewTotal < 20)
			{
				this.mS_.AddStudioPoints(Mathf.RoundToInt(1f * num2));
			}
			if (this.reviewTotal >= 20 && this.reviewTotal < 30)
			{
				this.mS_.AddStudioPoints(Mathf.RoundToInt(2f * num2));
			}
			if (this.reviewTotal >= 30 && this.reviewTotal < 40)
			{
				this.mS_.AddStudioPoints(Mathf.RoundToInt(4f * num2));
			}
			if (this.reviewTotal >= 40 && this.reviewTotal < 50)
			{
				this.mS_.AddStudioPoints(Mathf.RoundToInt(6f * num2));
			}
			if (this.reviewTotal >= 50 && this.reviewTotal < 60)
			{
				this.mS_.AddStudioPoints(Mathf.RoundToInt(8f * num2));
			}
			if (this.reviewTotal >= 60 && this.reviewTotal < 70)
			{
				this.mS_.AddStudioPoints(Mathf.RoundToInt(10f * num2));
			}
			if (this.reviewTotal >= 70 && this.reviewTotal < 80)
			{
				this.mS_.AddStudioPoints(Mathf.RoundToInt(12f * num2));
			}
			if (this.reviewTotal >= 80 && this.reviewTotal < 90)
			{
				this.mS_.AddStudioPoints(Mathf.RoundToInt(15f * num2));
			}
			if (this.reviewTotal >= 90 && this.reviewTotal < 95)
			{
				this.mS_.AddStudioPoints(Mathf.RoundToInt(18f * num2));
			}
			if (this.reviewTotal >= 95 && this.reviewTotal < 100)
			{
				this.mS_.AddStudioPoints(Mathf.RoundToInt(25f * num2));
			}
			if (this.reviewTotal >= 100)
			{
				this.mS_.AddStudioPoints(Mathf.RoundToInt(50f * num2));
			}
		}
		if (this.IsMyGame() && this.mS_.achScript_)
		{
			if (this.developerID == this.mS_.myID)
			{
				if (this.reviewTotal >= 70)
				{
					if (this.maingenre == 0)
					{
						this.mS_.achScript_.SetAchivement(1);
					}
					if (this.maingenre == 1)
					{
						this.mS_.achScript_.SetAchivement(2);
					}
					if (this.maingenre == 2)
					{
						this.mS_.achScript_.SetAchivement(3);
					}
					if (this.maingenre == 3)
					{
						this.mS_.achScript_.SetAchivement(4);
					}
					if (this.maingenre == 4)
					{
						this.mS_.achScript_.SetAchivement(5);
					}
					if (this.maingenre == 5)
					{
						this.mS_.achScript_.SetAchivement(0);
					}
					if (this.maingenre == 6)
					{
						this.mS_.achScript_.SetAchivement(6);
					}
					if (this.maingenre == 7)
					{
						this.mS_.achScript_.SetAchivement(7);
					}
					if (this.maingenre == 8)
					{
						this.mS_.achScript_.SetAchivement(8);
					}
					if (this.maingenre == 9)
					{
						this.mS_.achScript_.SetAchivement(9);
					}
					if (this.maingenre == 10)
					{
						this.mS_.achScript_.SetAchivement(10);
					}
					if (this.maingenre == 11)
					{
						this.mS_.achScript_.SetAchivement(11);
					}
					if (this.maingenre == 12)
					{
						this.mS_.achScript_.SetAchivement(12);
					}
					if (this.maingenre == 13)
					{
						this.mS_.achScript_.SetAchivement(13);
					}
					if (this.maingenre == 14)
					{
						this.mS_.achScript_.SetAchivement(14);
					}
					if (this.maingenre == 15)
					{
						this.mS_.achScript_.SetAchivement(15);
					}
					if (this.maingenre == 16)
					{
						this.mS_.achScript_.SetAchivement(16);
					}
					if (this.maingenre == 17)
					{
						this.mS_.achScript_.SetAchivement(17);
					}
				}
				if (this.retro)
				{
					this.mS_.achScript_.SetAchivement(18);
				}
				if (this.gameTyp == 1)
				{
					this.mS_.achScript_.SetAchivement(19);
				}
				if (this.gameTyp == 2)
				{
					this.mS_.achScript_.SetAchivement(20);
				}
				if (this.arcade)
				{
					this.mS_.achScript_.SetAchivement(21);
				}
				if (this.handy)
				{
					this.mS_.achScript_.SetAchivement(22);
				}
				if (this.typ_spinoff)
				{
					this.mS_.achScript_.SetAchivement(30);
				}
				if (this.typ_nachfolger)
				{
					this.mS_.achScript_.SetAchivement(31);
				}
				if (this.portID != -1)
				{
					this.mS_.achScript_.SetAchivement(32);
				}
				if (this.typ_remaster)
				{
					this.mS_.achScript_.SetAchivement(33);
				}
				if (this.reviewTotal >= 80)
				{
					this.mS_.achScript_.SetAchivement(51);
				}
				if (this.reviewTotal >= 90)
				{
					this.mS_.achScript_.SetAchivement(52);
				}
				if (this.reviewTotal >= 100 && this.mS_.difficulty >= 5)
				{
					this.mS_.achScript_.SetAchivement(53);
				}
				if (this.publisherID == this.mS_.myID)
				{
					this.mS_.achScript_.SetAchivement(44);
				}
			}
			if (this.typ_bundle)
			{
				this.mS_.achScript_.SetAchivement(27);
			}
			if (this.typ_budget)
			{
				this.mS_.achScript_.SetAchivement(28);
			}
			if (this.typ_bundleAddon)
			{
				this.mS_.achScript_.SetAchivement(29);
			}
			if (this.pubOffer)
			{
				this.mS_.achScript_.SetAchivement(46);
			}
		}
		if (this.mS_.multiplayer)
		{
			if (this.mS_.mpCalls_.isServer)
			{
				this.mS_.mpCalls_.SERVER_Send_Game(this);
			}
			if (this.mS_.mpCalls_.isClient && (this.IsMyGame() || flag))
			{
				this.mS_.mpCalls_.CLIENT_Send_Game(this);
			}
		}
	}

	
	public void RemoveFromMarket()
	{
		this.FindScripts();
		this.isOnMarket = false;
		this.freigabeBudget = 48;
		if (this.gameTab_)
		{
			UnityEngine.Object.Destroy(this.gameTab_.gameObject);
		}
		if (this.publisherID == this.mS_.myID && this.mS_.sellLagerbestandAutomatic)
		{
			this.guiMain_.uiObjects[226].SetActive(true);
			this.guiMain_.uiObjects[226].GetComponent<Menu_W_Restbestand>().Init(this);
			this.guiMain_.uiObjects[226].GetComponent<Menu_W_Restbestand>().BUTTON_Yes();
			this.guiMain_.uiObjects[226].SetActive(false);
		}
	}

	
	public gameScript FindMainIpScript()
	{
		if (this.script_mainIP)
		{
			return this.script_mainIP;
		}
		if (this.mainIP == -1)
		{
			return null;
		}
		GameObject gameObject = GameObject.Find("GAME_" + this.mainIP.ToString());
		if (gameObject)
		{
			this.script_mainIP = gameObject.GetComponent<gameScript>();
			return this.script_mainIP;
		}
		this.mainIP = -1;
		return null;
	}

	
	public gameScript FindPortOriginalScript()
	{
		if (this.originalIP == -1)
		{
			return null;
		}
		if (this.script_portOriginal)
		{
			return this.script_portOriginal;
		}
		if (this.portID == -1)
		{
			return null;
		}
		GameObject gameObject = GameObject.Find("GAME_" + this.portID.ToString());
		if (gameObject)
		{
			this.script_portOriginal = gameObject.GetComponent<gameScript>();
			return this.script_portOriginal;
		}
		return null;
	}

	
	public gameScript FindVorgaengerScript()
	{
		if (this.originalIP == -1)
		{
			return null;
		}
		if (this.script_vorgaenger)
		{
			return this.script_vorgaenger;
		}
		GameObject gameObject = GameObject.Find("GAME_" + this.originalIP.ToString());
		if (gameObject)
		{
			this.script_vorgaenger = gameObject.GetComponent<gameScript>();
			return this.script_vorgaenger;
		}
		return null;
	}

	
	public gameScript FindNachfolgerScript()
	{
		if (this.originalIP == -1)
		{
			return null;
		}
		if (this.script_nachfolger)
		{
			return this.script_nachfolger;
		}
		for (int i = 0; i < this.games_.arrayGamesScripts.Length; i++)
		{
			if (this.games_.arrayGamesScripts[i] && this.games_.arrayGamesScripts[i].originalIP == this.myID)
			{
				this.script_nachfolger = this.games_.arrayGamesScripts[i];
				return this.script_nachfolger;
			}
		}
		return null;
	}

	
	public int GetContractGameAbgabetermin()
	{
		if (!this.typ_contractGame)
		{
			return 0;
		}
		return this.auftragsspiel_zeitInWochen;
	}

	
	public void FreeGameContract()
	{
		this.auftragsspiel = true;
		this.developerID = this.publisherID;
		this.typ_contractGame = false;
		this.auftragsspiel_Inivs = true;
		if (this.mS_.multiplayer)
		{
			if (this.mS_.mpCalls_.isServer)
			{
				this.mS_.mpCalls_.SERVER_Send_Game(this);
			}
			if (this.mS_.mpCalls_.isClient)
			{
				this.mS_.mpCalls_.CLIENT_Send_Game(this);
			}
		}
	}

	
	public void SellMerchandise()
	{
		if (this.ownerID != this.mS_.myID)
		{
			return;
		}
		if (this.mainIP != this.myID)
		{
			return;
		}
		if (this.schublade)
		{
			return;
		}
		if (this.inDevelopment)
		{
			return;
		}
		if (!this.guiMain_)
		{
			this.FindScripts();
		}
		float num = (float)Mathf.RoundToInt(this.GetIpBekanntheit());
		if (!this.script_mainIP)
		{
			this.FindMainIpScript();
		}
		if (!this.script_mainIP)
		{
			return;
		}
		if (!this.menuFanshop_)
		{
			this.menuFanshop_ = this.guiMain_.uiObjects[367].GetComponent<Menu_Fanshop>();
		}
		float num2 = this.ipPunkte;
		if (num2 > 1000f)
		{
			num2 = 1000f;
		}
		if (num2 < 0f)
		{
			num2 = 0f;
		}
		float num3 = this.script_mainIP.merchGesamtReviewPoints + 30f;
		if ((double)num3 > 100.0)
		{
			num3 = 100f;
		}
		num2 = num2 * num3 * 0.01f;
		switch (this.mS_.difficulty)
		{
		case 0:
			num2 *= 1.2f;
			break;
		case 1:
			num2 *= 1f;
			break;
		case 2:
			num2 *= 0.7f;
			break;
		case 3:
			num2 *= 0.5f;
			break;
		case 4:
			num2 *= 0.4f;
			break;
		case 5:
			num2 *= 0.2f;
			break;
		default:
			num2 *= 1f;
			break;
		}
		float num4 = (float)this.mS_.genres_.GetAmountFans();
		for (int i = 0; i < this.merchBestellungen.Length; i++)
		{
			this.merchBestellungen[i] = 0;
			if (num >= (float)this.menuFanshop_.needStars[i])
			{
				float num5 = num4 * 0.5f - (float)this.merchGesamtSells[i];
				if (num5 < 0f)
				{
					num5 = 0f;
				}
				if (num5 > 1000000f)
				{
					num5 = 1000000f;
				}
				num5 /= 10000f;
				float num6 = num2 * num5 * 0.01f;
				num6 *= this.menuFanshop_.beliebtheit[i];
				float num7 = this.menuFanshop_.optimalerPreis[i] - this.merchVerkaufspreis[i];
				if (num7 < 0f)
				{
					num7 = Mathf.Abs(num7);
					num7 = 0.07f * num7;
					if (num7 > 1f)
					{
						num7 = 0.99f;
					}
					num6 -= num6 * num7;
				}
				else
				{
					num7 = 0.03f * num7;
					num6 += num6 * num7;
				}
				num6 = UnityEngine.Random.Range(num6, num6 * 1.3f);
				num6 *= 0.4f;
				if (this.mS_.globalEvent == 13)
				{
					num6 *= 0.5f;
				}
				if ((float)this.merchGesamtSells[i] > num4)
				{
					num6 = 0f;
				}
				int num8 = Mathf.RoundToInt(num6);
				this.merchBestellungen[i] = num8;
			}
		}
	}

	
	public void SellGame()
	{
		if (!this.isOnMarket)
		{
			if (!this.inDevelopment && this.freigabeBudget > 0)
			{
				this.freigabeBudget--;
			}
			return;
		}
		if (this.publisherID != -1 && !this.pS_)
		{
			this.FindMyPublisher();
		}
		if (this.releaseDate <= 0)
		{
			this.weeksOnMarket++;
		}
		else
		{
			this.releaseDate--;
		}
		float num = 0.07f;
		float num2 = 0f;
		float num3 = 0f;
		long num4 = this.sellsTotal;
		int num5 = this.reviewTotal;
		float num6 = 1f + this.GetUserReviewPercent() * 0.01f;
		if (num5 < 3)
		{
			num2 = 5f * num6;
		}
		if (num5 >= 3 && num5 < 5)
		{
			num2 = 30f * num6;
		}
		if (num5 >= 5 && num5 < 10)
		{
			num2 = 200f * num6;
		}
		if (num5 >= 10 && num5 < 20)
		{
			num2 = 300f * num6;
		}
		if (num5 >= 20 && num5 < 30)
		{
			num2 = 500f * num6;
		}
		if (num5 >= 30 && num5 < 40)
		{
			num2 = 1000f * num6;
		}
		if (num5 >= 40 && num5 < 50)
		{
			num2 = 2000f * num6;
		}
		if (num5 >= 50 && num5 < 60)
		{
			num2 = 5000f * num6;
		}
		if (num5 >= 60 && num5 < 70)
		{
			num2 = 8000f * num6;
		}
		if (num5 >= 70 && num5 < 80)
		{
			num2 = 13000f * num6;
		}
		if (num5 >= 80 && num5 < 90)
		{
			num2 = 19000f * num6;
		}
		if (num5 >= 90 && num5 < 95)
		{
			num2 = 25000f * num6;
		}
		if (num5 >= 95 && num5 < 100)
		{
			num2 = 40000f * num6;
		}
		if (num5 >= 100)
		{
			num2 = 100000f * num6;
		}
		num2 *= 0.5f;
		if (this.debug)
		{
			Debug.Log(string.Concat(new object[]
			{
				"GAME ",
				this.myName,
				" A ",
				num2
			}));
		}
		float num7 = (float)this.reviewTotal;
		num2 += this.points_gameplay * (num7 * 0.01f);
		num2 += this.points_grafik * (num7 * 0.01f);
		num2 += this.points_sound * (num7 * 0.01f);
		num2 += this.points_technik * (num7 * 0.01f);
		if (this.mainIP != -1)
		{
			if (!this.script_mainIP)
			{
				this.FindMainIpScript();
			}
			if (this.script_mainIP)
			{
				switch (this.mS_.difficulty)
				{
				case 0:
					num2 += num2 * this.GetIpBekanntheit() * 0.2f;
					break;
				case 1:
					num2 += num2 * this.GetIpBekanntheit() * 0.2f * 0.8f;
					break;
				case 2:
					num2 += num2 * this.GetIpBekanntheit() * 0.2f * 0.5f;
					break;
				case 3:
					num2 += num2 * this.GetIpBekanntheit() * 0.2f * 0.4f;
					break;
				case 4:
					num2 += num2 * this.GetIpBekanntheit() * 0.2f * 0.37f;
					break;
				case 5:
					num2 += num2 * this.GetIpBekanntheit() * 0.2f * 0.35f;
					break;
				default:
					num2 += num2 * this.GetIpBekanntheit() * 0.2f * 0.5f;
					break;
				}
				if (this.script_mainIP.merchGesamtReviewPoints < (float)this.reviewTotal)
				{
					this.script_mainIP.merchGesamtReviewPoints = (float)this.reviewTotal;
				}
			}
		}
		if (this.ExistAutomatenspiel())
		{
			num2 += num2 * 0.2f;
		}
		if (this.nachfolger_created)
		{
			if (!this.script_nachfolger)
			{
				this.FindNachfolgerScript();
			}
			if (this.script_nachfolger && this.script_nachfolger.isOnMarket)
			{
				num2 *= 0.6f;
			}
		}
		if (this.debug)
		{
			Debug.Log(string.Concat(new object[]
			{
				"GAME ",
				this.myName,
				" B ",
				num2
			}));
		}
		if (this.typ_nachfolger)
		{
			if (!this.script_vorgaenger)
			{
				this.FindVorgaengerScript();
			}
			if (this.script_vorgaenger)
			{
				if (this.weeksOnMarket > 2)
				{
					if (this.script_vorgaenger.reviewTotal < 5)
					{
						num2 -= 1000f;
					}
					if (this.script_vorgaenger.reviewTotal >= 5 && this.script_vorgaenger.reviewTotal < 10)
					{
						num2 -= 500f;
					}
					if (this.script_vorgaenger.reviewTotal >= 10 && this.script_vorgaenger.reviewTotal < 20)
					{
						num2 -= 300f;
					}
					if (this.script_vorgaenger.reviewTotal >= 20 && this.script_vorgaenger.reviewTotal < 30)
					{
						num2 -= 100f;
					}
					if (this.script_vorgaenger.reviewTotal >= 30 && this.script_vorgaenger.reviewTotal < 40)
					{
						num2 -= 50f;
					}
				}
				if (this.script_vorgaenger.reviewTotal >= 40 && this.script_vorgaenger.reviewTotal < 50)
				{
					num2 += 50f;
				}
				if (this.script_vorgaenger.reviewTotal >= 50 && this.script_vorgaenger.reviewTotal < 60)
				{
					num2 += 1000f;
				}
				if (this.script_vorgaenger.reviewTotal >= 60 && this.script_vorgaenger.reviewTotal < 70)
				{
					num2 += 2000f;
				}
				if (this.script_vorgaenger.reviewTotal >= 70 && this.script_vorgaenger.reviewTotal < 80)
				{
					num2 += 3000f;
				}
				if (this.script_vorgaenger.reviewTotal >= 80 && this.script_vorgaenger.reviewTotal < 90)
				{
					num2 += 4000f;
				}
				if (this.script_vorgaenger.reviewTotal >= 90 && this.script_vorgaenger.reviewTotal < 95)
				{
					num2 += 5000f;
				}
				if (this.script_vorgaenger.reviewTotal >= 95 && this.script_vorgaenger.reviewTotal < 100)
				{
					num2 += 10000f;
				}
				if (this.script_vorgaenger.reviewTotal >= 100)
				{
					num2 += 15000f;
				}
			}
		}
		if (this.IsMyGame())
		{
			float num8 = (float)this.reviewTotal * 0.01f;
			float num9 = num8 * (float)this.genres_.GetAmountFans() * 0.005f;
			float num10 = num8 * (float)this.genres_.genres_FANS[this.maingenre] * 0.05f;
			float num11 = 0f;
			if (this.subgenre != -1)
			{
				num11 = num8 * (float)this.genres_.genres_FANS[this.subgenre] * 0.01f;
			}
			num2 += num9 + num10 + num11;
		}
		else
		{
			int num12 = this.mS_.year - 1975;
			float num13 = (float)this.reviewTotal * 0.01f;
			float num14 = num13 * (float)(50000 * num12) * 0.001f;
			float num15 = num13 * (float)(5000 * num12) * 0.01f;
			num2 += num14 + num15;
		}
		switch (this.gameTyp)
		{
		case 0:
			if (!this.arcade)
			{
				float num16 = 1f - (float)this.weeksOnMarket * 0.01f;
				if (num16 >= 0f)
				{
					num2 *= num16;
				}
				else
				{
					num2 *= 0f;
				}
				num2 -= (float)(this.weeksOnMarket * this.weeksOnMarket * 3);
			}
			else
			{
				float num17 = 1f - (float)this.weeksOnMarket * 0.003f;
				if (num17 >= 0f)
				{
					num2 *= num17;
				}
				else
				{
					num2 *= 0f;
				}
				num2 -= (float)(this.weeksOnMarket * this.weeksOnMarket);
			}
			break;
		case 1:
			num2 *= this.mmoInteresse * 0.01f;
			if (this.IsMyGame())
			{
				this.AddMMOInteresse(-UnityEngine.Random.Range(0.3f, 0.5f));
			}
			else
			{
				this.AddMMOInteresse(-UnityEngine.Random.Range(0.1f, 0.3f));
			}
			break;
		case 2:
			num2 *= this.f2pInteresse * 0.01f;
			if (this.IsMyGame())
			{
				this.AddF2PInteresse(-UnityEngine.Random.Range(0.3f, 0.5f));
			}
			else
			{
				this.AddF2PInteresse(-UnityEngine.Random.Range(0.1f, 0.3f));
			}
			break;
		}
		if (num2 < 0f)
		{
			num2 = 0f;
		}
		if (!this.arcade)
		{
			num2 *= this.games_.GetSells();
		}
		else
		{
			num2 *= this.games_.GetSellsArcade();
		}
		switch (this.weeksOnMarket)
		{
		case 1:
			num2 *= 1.3f;
			break;
		case 2:
			num2 *= 1.4f;
			break;
		case 3:
			num2 *= 1.5f;
			break;
		case 4:
			num2 *= 1.4f;
			break;
		case 5:
			num2 *= 1.1f;
			break;
		}
		if (this.debug)
		{
			Debug.Log(string.Concat(new object[]
			{
				"GAME ",
				this.myName,
				" D ",
				num2
			}));
		}
		if (this.IsMyGame())
		{
			if (this.typ_nachfolger || this.typ_remaster || this.typ_spinoff || this.typ_standard)
			{
				if (!this.handy && !this.arcade)
				{
					float num18 = (float)this.mS_.GetAchivementBonus(0);
					num18 *= 0.01f;
					num3 += num18;
				}
				if (this.arcade)
				{
					float num19 = (float)this.mS_.GetAchivementBonus(1);
					num19 *= 0.01f;
					num3 += num19;
				}
				if (this.handy)
				{
					float num20 = (float)this.mS_.GetAchivementBonus(2);
					num20 *= 0.01f;
					num3 += num20;
				}
			}
			if (this.typ_addon || this.typ_addonStandalone || this.typ_mmoaddon)
			{
				float num21 = (float)this.mS_.GetAchivementBonus(3);
				num21 *= 0.01f;
				num3 += num21;
			}
			if (this.typ_budget || this.typ_bundle || this.typ_bundleAddon || this.typ_goty)
			{
				float num22 = (float)this.mS_.GetAchivementBonus(4);
				num22 *= 0.01f;
				num3 += num22;
			}
		}
		switch (this.usk)
		{
		case 0:
			num3 += 0.1f;
			break;
		case 1:
			num3 += 0.1f;
			break;
		case 2:
			num3 += 0.05f;
			break;
		case 3:
			num3 += 0f;
			break;
		case 4:
			num3 -= 0.05f;
			break;
		case 5:
			num3 -= 0.2f;
			break;
		}
		if (!this.gameLanguage[0])
		{
			num3 -= 0.05f;
		}
		if (!this.gameLanguage[1])
		{
			num3 -= 0.03f;
		}
		if (!this.gameLanguage[2])
		{
			num3 -= 0.03f;
		}
		if (!this.gameLanguage[3])
		{
			num3 -= 0.02f;
		}
		if (!this.gameLanguage[4])
		{
			num3 -= 0.02f;
		}
		if (!this.gameLanguage[5])
		{
			num3 -= 0.02f;
		}
		if (!this.gameLanguage[6])
		{
			num3 -= 0.01f;
		}
		if (!this.gameLanguage[7])
		{
			num3 -= 0.02f;
		}
		if (!this.gameLanguage[8])
		{
			num3 -= 0.02f;
		}
		if (!this.gameLanguage[9])
		{
			num3 -= 0.03f;
		}
		if (!this.gameLanguage[10])
		{
			num3 -= 0.04f;
		}
		if (!this.typ_bundle)
		{
			if (this.mS_.trendGenre == this.maingenre)
			{
				num3 += 0.33f;
			}
			if (this.mS_.trendTheme == this.gameMainTheme)
			{
				num3 += 0.15f;
			}
			if (this.mS_.trendAntiGenre == this.maingenre)
			{
				num3 -= 0.33f;
			}
			if (this.mS_.trendAntiTheme == this.gameMainTheme)
			{
				num3 -= 0.15f;
			}
			if (this.mS_.trendGenre == this.subgenre)
			{
				num3 += 0.1f;
			}
			if (this.mS_.trendTheme == this.gameSubTheme)
			{
				num3 += 0.05f;
			}
			if (this.mS_.trendAntiGenre == this.subgenre)
			{
				num3 -= 0.1f;
			}
			if (this.mS_.trendAntiTheme == this.gameSubTheme)
			{
				num3 -= 0.05f;
			}
		}
		num3 += this.GetHype() * 0.01f;
		if (!this.typ_bundle && !this.arcade)
		{
			if (this.gameCopyProtect != -1)
			{
				if (!this.gameCopyProtectScript_)
				{
					GameObject gameObject = GameObject.Find("COPYPROTECT_" + this.gameCopyProtect.ToString());
					if (gameObject)
					{
						this.gameCopyProtectScript_ = gameObject.GetComponent<copyProtectScript>();
					}
					else
					{
						this.gameCopyProtect = -1;
					}
				}
				if (this.gameCopyProtectScript_)
				{
					num3 += this.gameCopyProtectScript_.effekt * 0.002f;
				}
			}
			if (this.gameAntiCheat != -1 && (this.gameplayFeatures_DevDone[21] || this.gameplayFeatures_DevDone[23]))
			{
				if (!this.gameAntiCheatScript_)
				{
					GameObject gameObject2 = GameObject.Find("ANTICHEAT_" + this.gameAntiCheat.ToString());
					if (gameObject2)
					{
						this.gameAntiCheatScript_ = gameObject2.GetComponent<antiCheatScript>();
					}
					else
					{
						this.gameAntiCheat = -1;
					}
				}
				if (this.gameAntiCheatScript_)
				{
					num3 += this.gameAntiCheatScript_.effekt * 0.003f;
				}
			}
		}
		if (this.publisherID != this.mS_.myID && this.pS_)
		{
			if (this.maingenre == this.pS_.fanGenre)
			{
				num3 += 0.2f;
			}
			num3 += this.pS_.stars * 0.01f;
		}
		if (!this.arcade)
		{
			if (this.mS_.month == 12 || this.mS_.month == 1)
			{
				num3 += 0.5f;
			}
			if (this.mS_.month == 6 || this.mS_.month == 7)
			{
				num3 -= 0.3f;
			}
		}
		if (this.IsMyGame() && this.mS_.awardBonus > 0 && this.mS_.awardBonusAmount > 0f)
		{
			num3 += this.mS_.awardBonusAmount;
		}
		if (!this.arcade)
		{
			num3 += this.bonusSellsUpdates;
		}
		else
		{
			num3 += this.bonusSellsUpdates * 0.2f;
		}
		num3 += this.bonusSellsAddons;
		num3 += this.addonQuality;
		if (num3 < -0.5f)
		{
			num3 = -0.5f;
		}
		num2 *= 1f + num3;
		if (!this.arcade)
		{
			if (this.debug)
			{
				Debug.Log("Platform Start: " + num2);
			}
			float num23 = 10f;
			for (int i = 0; i < this.gamePlatform.Length; i++)
			{
				if (this.gamePlatform[i] != -1)
				{
					if (!this.gamePlatformScript[i])
					{
						this.FindMyPlatforms();
					}
					if (this.gamePlatformScript[i])
					{
						num23 += this.gamePlatformScript[i].GetMarktanteil();
					}
				}
			}
			num23 *= 0.007f;
			num2 *= num23;
			if (this.debug)
			{
				Debug.Log("Platform End: " + num2);
			}
		}
		if (this.debug)
		{
			Debug.Log(string.Concat(new object[]
			{
				"GAME ",
				this.myName,
				" G ",
				num2
			}));
		}
		if (this.IsMyGame())
		{
			if (!this.typ_bundle && !this.arcade && this.mS_.gelangweiltGenre != -1)
			{
				if (this.maingenre == this.mS_.gelangweiltGenre)
				{
					num2 *= 0.5f;
				}
				else if (this.subgenre == this.mS_.gelangweiltGenre)
				{
					num2 *= 0.85f;
				}
			}
			if (this.mS_.sauerBugs > 0)
			{
				num2 *= 0.7f;
			}
			if (!this.typ_bundle && !this.typ_addon && !this.typ_mmoaddon)
			{
				Vector4 amountGamesWithGenreAndTopic = this.games_.GetAmountGamesWithGenreAndTopic(this);
				float num24 = 0.00055555557f * (float)this.genres_.genres_LEVEL.Length;
				float num25 = 5E-05f * (float)this.themes_.themes_LEVEL.Length;
				float num26 = amountGamesWithGenreAndTopic.x * num24 + amountGamesWithGenreAndTopic.y * num25 + amountGamesWithGenreAndTopic.z * num24 * 2.5f + amountGamesWithGenreAndTopic.w * num25 * 2.5f;
				if (num26 > 0.5f)
				{
					num26 = 0.5f;
				}
				num2 -= num2 * num26;
			}
		}
		if (this.gameLicence != -1)
		{
			num2 += num2 * (this.licences_.licence_QUALITY[this.gameLicence] * 0.01f);
		}
		float num27 = this.genres_.genres_BELIEBTHEIT[this.maingenre];
		if (this.maingenre == this.mS_.trendGenre)
		{
			num27 = 100f;
		}
		if (this.maingenre == this.mS_.trendAntiGenre)
		{
			num27 = 20f;
		}
		float num28 = num2 * 0.5f * (num27 * 0.01f);
		num2 *= 0.8f;
		num2 += num28;
		if (this.mS_.globalEvent == 0)
		{
			num2 *= 0.5f;
		}
		if (this.mS_.globalEvent == 1)
		{
			num2 *= 1.5f;
		}
		if (this.newGenreCombination)
		{
			num2 *= 1.1f;
		}
		if (this.newTopicCombination)
		{
			num2 *= 1.03f;
		}
		if (this.commercialFlop)
		{
			if (this.reviewTotal >= 70 && !this.typ_bundle && this.mS_.trendGenre != this.maingenre)
			{
				num2 *= 0.1f;
			}
			else
			{
				this.commercialFlop = false;
			}
		}
		if (this.commercialHit)
		{
			if (this.reviewTotal >= 70 && this.reviewTotal < 90)
			{
				num2 *= 2f;
			}
			else
			{
				this.commercialHit = false;
			}
		}
		switch (this.mS_.difficulty)
		{
		case 0:
			num2 *= 2f;
			break;
		case 1:
			num2 *= 1.5f;
			break;
		case 2:
			num2 *= 1f;
			break;
		case 3:
			num2 *= 0.5f;
			break;
		case 4:
			num2 *= 0.35f;
			break;
		case 5:
			num2 *= 0.25f;
			break;
		}
		if (this.IsMyGame() && this.publisherID == this.mS_.myID)
		{
			switch (this.mS_.GetStudioLevel(this.mS_.studioPoints))
			{
			case 0:
				num2 *= 0.5f;
				break;
			case 1:
				num2 *= 0.55f;
				break;
			case 2:
				num2 *= 0.6f;
				break;
			case 3:
				num2 *= 0.65f;
				break;
			case 4:
				num2 *= 0.7f;
				break;
			case 5:
				num2 *= 0.75f;
				break;
			case 6:
				num2 *= 0.8f;
				break;
			case 7:
				num2 *= 0.85f;
				break;
			case 8:
				num2 *= 0.9f;
				break;
			case 9:
				num2 *= 0.95f;
				break;
			}
		}
		num2 *= num;
		if (this.typ_addon)
		{
			num2 *= 0.4f;
			if (!this.script_vorgaenger)
			{
				this.FindVorgaengerScript();
			}
			if (this.script_vorgaenger && num2 > 0f)
			{
				if (this.script_vorgaenger.amountAddons > 0)
				{
					num2 /= (float)this.script_vorgaenger.amountAddons;
				}
				if (!this.script_vorgaenger.isOnMarket && this.script_vorgaenger.publisherID != this.mS_.myID)
				{
					num2 *= 0.8f;
				}
				if ((float)this.sellsTotal + num2 > (float)this.script_vorgaenger.sellsTotal)
				{
					num2 = (float)(this.script_vorgaenger.sellsTotal - this.sellsTotal);
				}
				if (num2 <= 0f)
				{
					num2 = 1f;
				}
				if (!this.script_vorgaenger.isOnMarket && (float)this.sellsTotal + num2 > (float)this.script_vorgaenger.sellsTotal)
				{
					num2 = 0f;
				}
			}
		}
		if (this.typ_addonStandalone)
		{
			num2 *= 0.5f;
			if (!this.script_vorgaenger)
			{
				this.FindVorgaengerScript();
			}
			if (this.script_vorgaenger && this.script_vorgaenger.amountAddons > 0)
			{
				num2 /= (float)this.script_vorgaenger.amountAddons;
			}
		}
		if (this.typ_mmoaddon)
		{
			num2 *= 0.65f;
			if (!this.script_vorgaenger)
			{
				this.FindVorgaengerScript();
			}
			if (this.script_vorgaenger && num2 > 0f)
			{
				this.script_vorgaenger.abosAddons = Mathf.RoundToInt(num2);
				if ((float)this.sellsTotal + num2 > (float)this.script_vorgaenger.sellsTotal)
				{
					num2 = (float)(this.script_vorgaenger.sellsTotal - this.sellsTotal);
				}
				if (num2 <= 0f)
				{
					num2 = 1f;
				}
			}
		}
		if (this.gameTyp == 1)
		{
			if (this.IsMyGame() && this.games_.freeServerPlatz <= 0)
			{
				num2 *= 0.05f;
			}
			int amountOfMMOs = this.games_.GetAmountOfMMOs();
			float num29 = 1f + (float)amountOfMMOs * 0.1f;
			if (amountOfMMOs > 0)
			{
				num2 /= num29;
			}
			float num30 = UnityEngine.Random.Range((num2 + (float)this.abosAddons) * 0.5f, (num2 + (float)this.abosAddons) * 0.7f);
			this.abosAddons = 0;
			float num31 = (float)this.abonnements - (float)this.abonnements / 102f * (float)this.reviewTotal;
			num31 *= 0.25f;
			num31 += (float)this.weeksOnMarket;
			if (this.IsMyGame())
			{
				num31 += num31 * ((100f - this.hype) * 0.01f);
			}
			switch (this.aboPreis)
			{
			case 1:
				num30 *= 1f;
				break;
			case 2:
				num30 *= 0.95f;
				break;
			case 3:
				num30 *= 0.9f;
				break;
			case 4:
				num30 *= 0.8f;
				break;
			case 5:
				num30 *= 0.7f;
				break;
			case 6:
				num30 *= 0.65f;
				break;
			case 7:
				num30 *= 0.6f;
				break;
			case 8:
				num30 *= 0.5f;
				break;
			case 9:
				num30 *= 0.4f;
				break;
			case 10:
				num30 *= 0.2f;
				break;
			}
			if (amountOfMMOs > 0)
			{
				num30 /= num29;
			}
			num30 *= 0.7f;
			this.abonnementsWoche = this.abonnements;
			this.abonnements -= Mathf.RoundToInt(num31);
			this.abonnements += Mathf.RoundToInt(num30);
			if ((long)this.abonnements > this.sellsTotal)
			{
				this.abonnements = (int)this.sellsTotal;
			}
			if (this.IsMyGame())
			{
				int num32 = this.abonnements;
				for (int j = 0; j < this.mS_.arrayRooms.Length; j++)
				{
					if (this.mS_.arrayRooms[j])
					{
						roomScript component = this.mS_.arrayRooms[j].GetComponent<roomScript>();
						if (component && component.typ == 15)
						{
							num32 = component.SetAbos(num32);
							if (num32 <= 0)
							{
								break;
							}
						}
					}
				}
				this.abonnements -= num32;
				this.mS_.AddAboverlauf((long)this.abonnements);
			}
			this.abonnementsWoche = this.abonnements - this.abonnementsWoche;
			if (this.abonnements < 0)
			{
				this.abonnements = 0;
			}
			if (this.bestAbonnements < this.abonnements)
			{
				this.bestAbonnements = this.abonnements;
			}
		}
		if (this.handy && this.gameTyp == 0)
		{
			num2 *= 2.5f;
		}
		if (this.arcade)
		{
			if (this.IsMyGame())
			{
				float num33 = (float)(this.arcadeCase + this.arcadeMonitor + this.arcadeJoystick + this.arcadeSound);
				num33 = 1f + num33 * 0.05f;
				num2 *= num33;
			}
			else
			{
				num2 *= 0.1f;
			}
			num2 *= 0.005f;
			if (num2 < 1f && !this.IsMyGame() && this.weeksOnMarket < 2)
			{
				num2 = (float)UnityEngine.Random.Range(1, 4);
			}
		}
		if (this.gameTyp == 2)
		{
			int amountOfF2Ps = this.games_.GetAmountOfF2Ps();
			float num34 = 1f + (float)amountOfF2Ps * 0.1f;
			num2 *= 4f;
			if (amountOfF2Ps > 0)
			{
				num2 /= num34;
			}
			float num35 = UnityEngine.Random.Range((num2 + (float)this.abosAddons) * 0.3f, (num2 + (float)this.abosAddons) * 0.5f);
			this.abosAddons = 0;
			float num36 = (float)this.abonnements - (float)this.abonnements / 102f * (float)this.reviewTotal;
			num36 *= 0.25f;
			num36 += (float)this.weeksOnMarket;
			if (amountOfF2Ps > 0)
			{
				num35 /= (float)amountOfF2Ps;
			}
			this.abonnementsWoche = this.abonnements;
			this.abonnements -= Mathf.RoundToInt(num36);
			this.abonnements += Mathf.RoundToInt(num35);
			if ((long)this.abonnements > this.sellsTotal)
			{
				this.abonnements = (int)this.sellsTotal;
			}
			if (this.IsMyGame())
			{
				int num37 = this.abonnements;
				for (int k = 0; k < this.mS_.arrayRooms.Length; k++)
				{
					if (this.mS_.arrayRooms[k])
					{
						roomScript component2 = this.mS_.arrayRooms[k].GetComponent<roomScript>();
						if (component2 && component2.typ == 15)
						{
							num37 = component2.SetAbos(num37);
							if (num37 <= 0)
							{
								break;
							}
						}
					}
				}
				this.abonnements -= num37;
			}
			this.abonnementsWoche = this.abonnements - this.abonnementsWoche;
			if (this.abonnements < 0)
			{
				this.abonnements = 0;
				this.abonnementsWoche = 0;
			}
			if (this.bestAbonnements < this.abonnements)
			{
				this.bestAbonnements = this.abonnements;
			}
		}
		if (this.typ_budget)
		{
			num2 *= 0.5f;
			float num38 = (float)(this.mS_.year * (this.date_start_year - 1));
			num38 *= 0.05f;
			if (num38 > 0.7f)
			{
				num38 = 0.7f;
			}
			num38 = 1f - num38;
			num2 *= num38;
		}
		if (this.typ_remaster)
		{
			num2 *= 0.7f;
		}
		if (this.typ_goty)
		{
			num2 *= 0.5f;
			float num39 = (float)(this.mS_.year * (this.date_start_year - 1));
			num39 *= 0.03f;
			if (num39 > 0.7f)
			{
				num39 = 0.7f;
			}
			num39 = 1f - num39;
			num2 *= num39;
		}
		if (this.typ_bundle)
		{
			float num40 = 0.4f;
			if (this.bundleID[0] != -1)
			{
				num40 += 0.1f;
			}
			if (this.bundleID[1] != -1)
			{
				num40 += 0.1f;
			}
			if (this.bundleID[2] != -1)
			{
				num40 += 0.1f;
			}
			if (this.bundleID[3] != -1)
			{
				num40 += 0.1f;
			}
			if (this.bundleID[4] != -1)
			{
				num40 += 0.1f;
			}
			num2 *= num40;
		}
		if (this.typ_bundleAddon)
		{
			float num41 = (float)(this.mS_.year * (this.date_start_year - 1));
			num41 *= 0.05f;
			if (num41 > 0.7f)
			{
				num41 = 0.7f;
			}
			num41 = 1f - num41;
			num2 *= num41;
			float num42 = 0.5f;
			if (this.bundleID[0] != -1)
			{
				num42 += 0.05f;
			}
			if (this.bundleID[1] != -1)
			{
				num42 += 0.05f;
			}
			if (this.bundleID[2] != -1)
			{
				num42 += 0.05f;
			}
			if (this.bundleID[3] != -1)
			{
				num42 += 0.05f;
			}
			if (this.bundleID[4] != -1)
			{
				num42 += 0.05f;
			}
			num2 *= num42;
		}
		if (!this.arcade)
		{
			long num43 = 0L;
			if (num2 > 0f)
			{
				for (int l = 0; l < this.gamePlatform.Length; l++)
				{
					if (this.gamePlatform[l] != -1)
					{
						if (!this.gamePlatformScript[l])
						{
							this.FindMyPlatforms();
						}
						if (this.gamePlatformScript[l])
						{
							if (this.exklusiv && this.gamePlatformScript[l].OwnerIsNPC() && !this.gamePlatformScript[l].vomMarktGenommen)
							{
								if (this.gameTyp != 2)
								{
									int num44 = Mathf.RoundToInt(UnityEngine.Random.Range((float)this.sellsPerWeek[0] * 0.2f, (float)this.sellsPerWeek[0] * 0.3f));
									num44 = num44 / 100 * (130 - (int)this.gamePlatformScript[l].GetMarktanteil());
									if (num44 > this.sellsPerWeek[0])
									{
										num44 = this.sellsPerWeek[0];
									}
									this.exklusivKonsolenSells += (long)num44;
									this.gamePlatformScript[l].BonusSellsExklusiv(num44);
								}
								else
								{
									int num45 = Mathf.RoundToInt(UnityEngine.Random.Range((float)this.sellsPerWeek[0] * 0.2f, (float)this.sellsPerWeek[0] * 0.3f));
									num45 = num45 / 100 * (130 - (int)this.gamePlatformScript[l].GetMarktanteil());
									if (num45 > this.sellsPerWeek[0])
									{
										num45 = this.sellsPerWeek[0];
									}
									this.exklusivKonsolenSells += (long)(num45 / 5);
									this.gamePlatformScript[l].BonusSellsExklusiv(num45);
								}
							}
							num43 += (long)this.gamePlatformScript[l].units;
						}
					}
				}
				if ((float)this.sellsTotal + num2 > (float)num43)
				{
					num2 = (float)(num43 - this.sellsTotal);
					if (num2 <= 0f)
					{
						num2 = 1f;
					}
				}
			}
		}
		if (this.HasInAppPurchases())
		{
			if (this.gameTyp == 0 && this.releaseDate <= 0)
			{
				float num46 = num2 * (this.GetInAppPurchaseHate() * 0.01f) * 0.3f;
				num2 -= num46;
				float num47 = this.GetInAppPurchaseMoneyPerWeek();
				float num48 = UnityEngine.Random.Range(((float)this.sellsTotal + num2) / 100f * 2f, ((float)this.sellsTotal + num2) / 100f * 3f);
				if (this.IsMyGame())
				{
					float num49 = (float)this.mS_.GetAchivementBonus(5);
					num49 *= 0.01f;
					num48 += num48 * num49;
				}
				if (num2 <= 0f)
				{
					num48 *= 0.6f;
				}
				float num50 = 1f - (float)this.weeksOnMarket * 0.01f;
				if (num50 < 0.1f)
				{
					num50 = 0.1f;
				}
				num48 *= num50;
				if (this.weeksOnMarket > 5)
				{
					num48 -= (float)(this.weeksOnMarket * 30);
				}
				if (num48 < 0f)
				{
					num48 = 0f;
				}
				this.inAppPurchaseWeek = Mathf.RoundToInt(num48);
				num47 *= (float)Mathf.RoundToInt(num48);
				this.umsatzTotal += (long)Mathf.RoundToInt(num47);
				this.umsatzInApp += (long)Mathf.RoundToInt(num47);
				if (this.IsMyGame())
				{
					this.mS_.Earn((long)Mathf.RoundToInt(num47), 8);
				}
				if (this.IsMyGame())
				{
					this.PayGewinnbeteiligung((long)Mathf.RoundToInt(num47));
				}
				if (!this.IsMyGame())
				{
					this.AddFirmenwert((long)num47);
					this.AddTochterfirmaUmsatz((long)num47);
				}
			}
			if (this.gameTyp == 1 && this.releaseDate <= 0)
			{
				float num51 = num2 * (this.GetInAppPurchaseHate() * 0.01f) * 0.3f;
				num2 -= num51;
				float num47 = this.GetInAppPurchaseMoneyPerWeek();
				float num52 = UnityEngine.Random.Range(((float)this.abonnements + num2) / 100f * 4f, ((float)this.abonnements + num2) / 100f * 5f);
				if (this.IsMyGame())
				{
					float num53 = (float)this.mS_.GetAchivementBonus(5);
					num53 *= 0.01f;
					num52 += num52 * num53;
				}
				if (num2 <= 0f)
				{
					num52 *= 0.8f;
				}
				this.inAppPurchaseWeek = Mathf.RoundToInt(num52);
				num47 *= (float)Mathf.RoundToInt(num52);
				this.umsatzTotal += (long)Mathf.RoundToInt(num47);
				this.umsatzInApp += (long)Mathf.RoundToInt(num47);
				if (this.IsMyGame())
				{
					this.mS_.Earn((long)Mathf.RoundToInt(num47), 8);
				}
				if (this.IsMyGame())
				{
					this.PayGewinnbeteiligung((long)Mathf.RoundToInt(num47));
				}
				if (!this.IsMyGame())
				{
					this.AddFirmenwert((long)num47);
					this.AddTochterfirmaUmsatz((long)num47);
				}
			}
			if (this.gameTyp == 2 && this.releaseDate <= 0)
			{
				float num54 = num2 * (this.GetInAppPurchaseHate() * 0.01f) * 0.3f;
				num2 -= num54;
				float num47 = this.GetInAppPurchaseMoneyPerWeek();
				float num55 = UnityEngine.Random.Range(((float)this.abonnements + num2) / 100f * 150f, ((float)this.abonnements + num2) / 100f * 200f);
				if (this.IsMyGame())
				{
					float num56 = (float)this.mS_.GetAchivementBonus(5);
					num56 *= 0.01f;
					num55 += num55 * num56;
				}
				if (num2 <= 0f)
				{
					num55 *= 0.8f;
				}
				this.inAppPurchaseWeek = Mathf.RoundToInt(num55);
				num47 *= (float)Mathf.RoundToInt(num55);
				this.umsatzTotal += (long)Mathf.RoundToInt(num47);
				this.umsatzInApp += (long)Mathf.RoundToInt(num47);
				if (this.IsMyGame())
				{
					this.mS_.Earn((long)Mathf.RoundToInt(num47), 8);
				}
				if (this.IsMyGame())
				{
					this.PayGewinnbeteiligung((long)Mathf.RoundToInt(num47));
				}
				if (!this.IsMyGame())
				{
					this.AddFirmenwert((long)num47);
					this.AddTochterfirmaUmsatz((long)num47);
				}
			}
		}
		int num57 = Mathf.RoundToInt(num2);
		if (num57 < 0)
		{
			num57 = 0;
		}
		if (this.IsMyGame() && this.releaseDate <= 0 && !this.typ_bundle)
		{
			int genre_ = this.maingenre;
			if (UnityEngine.Random.Range(0, 100) > 70)
			{
				genre_ = this.subgenre;
			}
			float num58 = (float)this.mS_.GetAchivementBonus(7);
			num58 *= 0.01f;
			if (!this.retro)
			{
				float num59 = num2;
				if (this.gameTyp == 2)
				{
					num59 *= 0.1f;
				}
				num59 += num59 * num58;
				if (this.reviewTotal < 10)
				{
					this.mS_.AddFans(-Mathf.RoundToInt(UnityEngine.Random.Range(0f, num59 * 0.1f)), genre_);
				}
				if (this.reviewTotal >= 10 && this.reviewTotal < 20)
				{
					this.mS_.AddFans(-Mathf.RoundToInt(UnityEngine.Random.Range(0f, num59 * 0.05f)), genre_);
				}
				if (this.reviewTotal >= 20 && this.reviewTotal < 30)
				{
					this.mS_.AddFans(-Mathf.RoundToInt(UnityEngine.Random.Range(0f, num59 * 0.03f)), genre_);
				}
				if (this.reviewTotal >= 30 && this.reviewTotal < 40)
				{
					this.mS_.AddFans(-Mathf.RoundToInt(UnityEngine.Random.Range(0f, num59 * 0.02f)), genre_);
				}
				if (this.reviewTotal >= 40 && this.reviewTotal < 50)
				{
					this.mS_.AddFans(-Mathf.RoundToInt(UnityEngine.Random.Range(0f, num59 * 0.01f)), genre_);
				}
				if (this.reviewTotal >= 50 && this.reviewTotal < 60)
				{
					this.mS_.AddFans(Mathf.RoundToInt(UnityEngine.Random.Range(0f, num59 * 0.01f)), genre_);
				}
				if (this.reviewTotal >= 60 && this.reviewTotal < 70)
				{
					this.mS_.AddFans(Mathf.RoundToInt(UnityEngine.Random.Range(0f, num59 * 0.01f)), genre_);
				}
				if (this.reviewTotal >= 70 && this.reviewTotal < 80)
				{
					this.mS_.AddFans(Mathf.RoundToInt(UnityEngine.Random.Range(0f, num59 * 0.01f)), genre_);
				}
				if (this.reviewTotal >= 80 && this.reviewTotal < 90)
				{
					this.mS_.AddFans(Mathf.RoundToInt(UnityEngine.Random.Range(0f, num59 * 0.01f)), genre_);
				}
				if (this.reviewTotal >= 90 && this.reviewTotal < 95)
				{
					this.mS_.AddFans(Mathf.RoundToInt(UnityEngine.Random.Range(0f, num59 * 0.01f)), genre_);
				}
				if (this.reviewTotal >= 95 && this.reviewTotal < 100)
				{
					this.mS_.AddFans(Mathf.RoundToInt(UnityEngine.Random.Range(0f, num59 * 0.01f)), genre_);
				}
				if (this.reviewTotal >= 100)
				{
					this.mS_.AddFans(Mathf.RoundToInt(UnityEngine.Random.Range(0f, num59 * 0.01f)), genre_);
				}
			}
			else
			{
				float num60 = num2;
				num60 += num60 * num58;
				if (this.reviewTotal < 40)
				{
					this.mS_.AddFans(Mathf.RoundToInt(UnityEngine.Random.Range(0f, num60 * 0.01f)), genre_);
				}
				if (this.reviewTotal >= 40 && this.reviewTotal < 50)
				{
					this.mS_.AddFans(Mathf.RoundToInt(UnityEngine.Random.Range(0f, num60 * 0.05f)), genre_);
				}
				if (this.reviewTotal >= 50 && this.reviewTotal < 60)
				{
					this.mS_.AddFans(Mathf.RoundToInt(UnityEngine.Random.Range(0f, num60 * 0.05f)), genre_);
				}
				if (this.reviewTotal >= 60 && this.reviewTotal < 70)
				{
					this.mS_.AddFans(Mathf.RoundToInt(UnityEngine.Random.Range(0f, num60 * 0.05f)), genre_);
				}
				if (this.reviewTotal >= 70 && this.reviewTotal < 80)
				{
					this.mS_.AddFans(Mathf.RoundToInt(UnityEngine.Random.Range(0f, num60 * 0.05f)), genre_);
				}
				if (this.reviewTotal >= 80 && this.reviewTotal < 90)
				{
					this.mS_.AddFans(Mathf.RoundToInt(UnityEngine.Random.Range(0f, num60 * 0.05f)), genre_);
				}
				if (this.reviewTotal >= 90 && this.reviewTotal < 95)
				{
					this.mS_.AddFans(Mathf.RoundToInt(UnityEngine.Random.Range(0f, num60 * 0.05f)), genre_);
				}
				if (this.reviewTotal >= 95 && this.reviewTotal < 100)
				{
					this.mS_.AddFans(Mathf.RoundToInt(UnityEngine.Random.Range(0f, num60 * 0.05f)), genre_);
				}
				if (this.reviewTotal >= 100)
				{
					this.mS_.AddFans(Mathf.RoundToInt(UnityEngine.Random.Range(0f, num60 * 0.05f)), genre_);
				}
			}
		}
		float num61 = 0f;
		float num62 = 0f;
		float num63 = 0f;
		float num64 = 0f;
		if (this.releaseDate <= 0)
		{
			for (int m = this.sellsPerWeek.Length - 1; m >= 1; m--)
			{
				this.sellsPerWeek[m] = this.sellsPerWeek[m - 1];
			}
			if (this.publisherID != this.mS_.myID)
			{
				this.sellsPerWeek[0] = num57;
				this.sellsTotal += (long)num57;
			}
			else if (this.IsMyGame())
			{
				if (this.gameTyp != 2)
				{
					if (!this.arcade)
					{
						float digitalSells = this.games_.GetDigitalSells();
						if (this.digitalVersion)
						{
							num64 = (float)num57 * digitalSells * this.GetPreisAbzug(3);
							if (!this.retailVersion)
							{
								num64 += (float)num57 * 0.2f * this.GetPreisAbzug(3);
							}
						}
						if (this.retailVersion)
						{
							num61 = (float)num57 * (1f - digitalSells) * this.GetPreisAbzug(0);
							num61 += num61 * this.GetEditionQualiaet(0);
							num62 = (float)num57 * this.games_.GetDeluxeCurve() * this.GetPreisAbzug(1) * this.GetEditionQualiaet(1);
							num61 -= num62;
							num63 = (float)num57 * this.games_.GetCollectorsCurve() * this.GetPreisAbzug(2) * this.GetEditionQualiaet(2);
							num61 -= num63;
							if (!this.digitalVersion)
							{
								num61 += (float)num57 * 0.2f * this.GetPreisAbzug(0);
							}
							if (this.lagerbestand[1] <= 0)
							{
								num61 += num62;
								num62 = 0f;
							}
							if (this.lagerbestand[2] <= 0)
							{
								num61 += num63;
								num63 = 0f;
							}
							if (num61 < 0f)
							{
								num61 = 0f;
							}
						}
						num61 += (float)this.vorbestellungen;
						this.vorbestellungen = 0;
						if (this.retailVersion)
						{
							num64 = (float)Mathf.RoundToInt(num64);
							num61 = (float)Mathf.RoundToInt(num61);
							num62 = (float)Mathf.RoundToInt(num62);
							num63 = (float)Mathf.RoundToInt(num63);
							if ((float)this.lagerbestand[0] < num61)
							{
								this.vorbestellungen += Mathf.RoundToInt(num61 - (float)this.lagerbestand[0]);
								num61 = (float)this.lagerbestand[0];
							}
							this.lagerbestand[0] -= Mathf.RoundToInt(num61);
							if ((float)this.lagerbestand[1] < num62)
							{
								num62 = (float)this.lagerbestand[1];
							}
							this.lagerbestand[1] -= Mathf.RoundToInt(num62);
							if ((float)this.lagerbestand[2] < num63)
							{
								num63 = (float)this.lagerbestand[2];
							}
							this.lagerbestand[2] -= Mathf.RoundToInt(num63);
						}
						this.sellsPerWeek[0] = Mathf.RoundToInt(num64 + num61 + num62 + num63);
						this.sellsTotal += (long)Mathf.RoundToInt(num64 + num61 + num62 + num63);
						this.sellsTotalStandard += (long)Mathf.RoundToInt(num61);
						this.sellsTotalDeluxe += (long)Mathf.RoundToInt(num62);
						this.sellsTotalCollectors += (long)Mathf.RoundToInt(num63);
						this.sellsTotalOnline += (long)Mathf.RoundToInt(num64);
					}
					else
					{
						num61 = (float)num57 * this.GetPreisAbzug(0);
						this.sellsPerWeek[0] = Mathf.RoundToInt(num61);
						this.vorbestellungen += Mathf.RoundToInt(num61);
						if (this.vorbestellungen > 50)
						{
							this.stornierungen = UnityEngine.Random.Range(0, this.vorbestellungen / 50 + 3);
							this.vorbestellungen -= this.stornierungen;
						}
						else
						{
							this.stornierungen = 0;
							if (this.weeksOnMarket > 20 && this.vorbestellungen > 0)
							{
								this.stornierungen = 1;
								this.vorbestellungen--;
							}
						}
					}
				}
				else
				{
					num64 = (float)num57;
					this.sellsPerWeek[0] = Mathf.RoundToInt(num64);
					this.sellsTotal += (long)Mathf.RoundToInt(num64);
					this.sellsTotalOnline += (long)Mathf.RoundToInt(num64);
				}
			}
		}
		else if (this.retailVersion)
		{
			this.vorbestellungen += Mathf.RoundToInt((float)num57 * this.GetPreisAbzug(0) / (float)(this.releaseDate + 1));
		}
		if (this.IsMyGame())
		{
			if (this.hype > 0f && this.releaseDate <= 0)
			{
				this.AddHype(-UnityEngine.Random.Range(0.1f, 1f));
			}
		}
		else
		{
			this.hype = 100f;
		}
		if (this.releaseDate <= 0 && ((this.sellsPerWeek[0] > 100 && !this.arcade) || this.sellsTotal > 100L || (this.arcade && this.sellsTotal > 0L)) && !this.typ_budget && !this.typ_goty)
		{
			float num65;
			if (!this.arcade)
			{
				num65 = (float)this.sellsPerWeek[0];
				num65 = UnityEngine.Random.Range(num65 * 0.01f, num65 * 0.02f);
			}
			else
			{
				num65 = (float)this.sellsTotal;
				num65 = UnityEngine.Random.Range(num65 * 0.01f, num65 * 0.02f) + (float)UnityEngine.Random.Range(0, 5);
			}
			float num66 = 0f;
			switch (UnityEngine.Random.Range(0, 5))
			{
			case 0:
				num66 = num65 * (float)this.reviewGameplay / 100f;
				break;
			case 1:
				num66 = num65 * (float)this.reviewGrafik / 100f;
				break;
			case 2:
				num66 = num65 * (float)this.reviewSound / 100f;
				break;
			case 3:
				num66 = num65 * (float)this.reviewSteuerung / 100f;
				break;
			case 4:
				num66 = num65 * (float)this.reviewTotal / 100f;
				break;
			}
			num66 -= UnityEngine.Random.Range(0f, this.points_bugs);
			if (num66 < 0f)
			{
				num66 = 0f;
			}
			this.userPositiv += Mathf.RoundToInt(num66);
			this.userNegativ += Mathf.RoundToInt(num65 - num66);
		}
		if (this.gameTyp != 2 && !this.arcade && !this.typ_addon && !this.typ_addonStandalone && !this.typ_mmoaddon && this.releaseDate <= 0)
		{
			if (!this.devS_)
			{
				this.FindMyDeveloper();
			}
			if (!this.pS_)
			{
				this.FindMyPublisher();
			}
			if (num4 < 1000000L && this.sellsTotal >= 1000000L)
			{
				this.mS_.AddAwards(7, this.devS_);
				if (this.publisherID != this.developerID)
				{
					this.mS_.AddAwards(7, this.pS_);
				}
				if (this.IsMyGame() || this.developerID == this.mS_.myID)
				{
					this.guiMain_.CreateTopNewsGoldeneSchallplatte(this.GetNameWithTag());
					this.mS_.goldeneSchallplatten++;
				}
			}
			if (num4 < 5000000L && this.sellsTotal >= 5000000L)
			{
				this.mS_.AddAwards(10, this.devS_);
				if (this.publisherID != this.developerID)
				{
					this.mS_.AddAwards(10, this.pS_);
				}
				if (this.IsMyGame() || this.developerID == this.mS_.myID)
				{
					this.guiMain_.CreateTopNewsPlatinSchallplatte(this.GetNameWithTag());
					this.mS_.platinSchallplatten++;
				}
			}
			if (num4 < 10000000L && this.sellsTotal >= 10000000L)
			{
				this.mS_.AddAwards(11, this.devS_);
				if (this.publisherID != this.developerID)
				{
					this.mS_.AddAwards(11, this.pS_);
				}
				if (this.IsMyGame() || this.developerID == this.mS_.myID)
				{
					this.guiMain_.CreateTopNewsDiamantSchallplatte(this.GetNameWithTag());
					this.mS_.diamantSchallplatten++;
				}
			}
		}
		if (this.IsMyGame())
		{
			if (this.releaseDate <= 0 && this.mS_.achScript_ && this.gameTyp != 2)
			{
				if (this.sellsTotal >= 1000000L)
				{
					this.mS_.achScript_.SetAchivement(48);
				}
				if (this.sellsTotal >= 10000000L)
				{
					this.mS_.achScript_.SetAchivement(49);
				}
				if (this.sellsTotal >= 50000000L && this.mS_.difficulty >= 5)
				{
					this.mS_.achScript_.SetAchivement(50);
				}
			}
			this.UpdateFanletter();
			if (!this.typ_addon && !this.typ_mmoaddon && !this.arcade)
			{
				float num67 = (float)num57 * 0.001f + this.points_bugs;
				num67 = UnityEngine.Random.Range(0f, num67);
				this.mS_.AddAnrufe(Mathf.RoundToInt(num67));
			}
			if (this.publisherID != this.mS_.myID)
			{
				if (this.pS_)
				{
					this.mS_.AddVerkaufsverlauf((long)num57);
					float f;
					if (this.mS_.exklusivVertrag_ID == this.publisherID)
					{
						f = (float)(num57 * this.pS_.GetShareExklusiv());
					}
					else
					{
						f = (float)(num57 * this.pS_.GetShare());
					}
					int num68 = Mathf.RoundToInt(f);
					this.umsatzTotal += (long)num68;
					this.mS_.Earn((long)num68, 3);
					this.PayGewinnbeteiligung((long)num68);
					long num69 = 0L;
					if (this.gameTyp == 1 && this.mS_.week == 5)
					{
						num69 = (long)this.abonnements * (long)this.aboPreis;
						this.umsatzTotal += num69;
						this.umsatzAbos += num69;
						this.mS_.Earn(num69, 7);
						this.PayGewinnbeteiligung(num69);
						this.costs_server += (long)(this.abonnements / 10);
					}
					this.PlayerPayEngineLicence((long)num68 + num69);
					if (this.hype < 10f && (UnityEngine.Random.Range(0f, 100f + this.pS_.stars) > 90f || this.weeksOnMarket <= 1))
					{
						this.AddHype(UnityEngine.Random.Range(0f, this.pS_.stars));
						string text = this.tS_.GetText(495);
						text = text.Replace("<NAME1>", this.GetNameWithTag());
						this.guiMain_.CreateTopNewsInfo(text);
					}
				}
			}
			else if (!this.arcade)
			{
				this.mS_.AddVerkaufsverlauf((long)Mathf.RoundToInt(num64 + num61 + num62 + num63));
				if (num64 > 0f)
				{
					this.mS_.AddDownloadverlauf((long)Mathf.RoundToInt(num64));
				}
				long num70 = 0L;
				if (this.gameTyp != 2)
				{
					num70 = Convert.ToInt64(num64 * (float)this.verkaufspreis[3]) + (long)Mathf.RoundToInt(num61 * (float)this.verkaufspreis[0]) + (long)Mathf.RoundToInt(num62 * (float)this.verkaufspreis[1]) + (long)Mathf.RoundToInt(num63 * (float)this.verkaufspreis[2]);
					this.umsatzTotal += num70;
					this.mS_.Earn(num70, 3);
					this.PayGewinnbeteiligung(num70);
				}
				long num71;
				if (this.gameTyp == 1 && this.mS_.week == 5)
				{
					num71 = (long)this.abonnements * (long)this.aboPreis;
					this.umsatzTotal += num71;
					this.umsatzAbos += num71;
					this.mS_.Earn(num71, 7);
					this.PayGewinnbeteiligung(num71);
					this.costs_server += (long)(this.abonnements / 10);
				}
				num71 = 0L;
				if (this.gameTyp == 2 && this.mS_.week == 5)
				{
					this.costs_server += (long)(this.abonnements / 10);
				}
				this.PlayerPayEngineLicence(num70 + num71);
				if (this.autoPreis && !this.arcade && !this.handy)
				{
					this.UpdateAutoPreis();
				}
			}
			if (this.gameTab_)
			{
				this.gameTab_.UpdateData();
			}
			if (num57 <= 0 && this.abonnements <= 0)
			{
				if ((this.publisherID != this.mS_.myID || this.mS_.automatic_RemoveGameFormMarket) && (!this.arcade || (this.arcade && this.vorbestellungen <= 0)))
				{
					this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[82]);
					this.guiMain_.uiObjects[82].GetComponent<Menu_GameFromMarket>().Init(this, false);
					this.guiMain_.OpenMenu(false);
					this.RemoveFromMarket();
				}
			}
			else
			{
				if (this.sellsTotal > 0L && this.weeksOnMarket < 24 && !this.guiMain_.menuOpen && this.reviewTotal > 90 && !this.trendsetter && this.releaseDate <= 0 && this.mS_.trendGenre != this.maingenre && !this.typ_mmoaddon && !this.typ_addon && !this.typ_budget && !this.typ_bundle && !this.typ_addonStandalone && !this.typ_goty && !this.typ_bundleAddon && UnityEngine.Random.Range(0, 200) == 1)
				{
					this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[142]);
					this.guiMain_.uiObjects[142].GetComponent<Menu_Trendsetter>().Init(this);
					this.guiMain_.OpenMenu(false);
					this.mS_.award_Trendsetter++;
					this.AddHype(30f);
					this.AddIpPoints(70f);
					if (this.mS_.achScript_)
					{
						this.mS_.achScript_.SetAchivement(34);
					}
				}
				if (this.sellsTotal > 0L && this.commercialFlop && this.weeksOnMarket == 4 && !this.guiMain_.menuOpen)
				{
					this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[352]);
					this.guiMain_.uiObjects[352].GetComponent<Menu_RandomEventCommercialFlop>().Init(this);
					this.guiMain_.OpenMenu(false);
				}
				if (this.sellsTotal > 0L && this.commercialHit && this.weeksOnMarket == 4 && !this.guiMain_.menuOpen)
				{
					this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[354]);
					this.guiMain_.uiObjects[354].GetComponent<Menu_RandomEventCommercialHit>().Init(this);
					this.guiMain_.OpenMenu(false);
				}
				if (this.sellsTotal > 0L && this.points_bugsInvis > 0f && this.mS_.difficulty >= 2 && this.weeksOnMarket >= 4 && this.weeksOnMarket <= 20 && UnityEngine.Random.Range(0, 200) <= this.mS_.difficulty && !this.guiMain_.menuOpen)
				{
					this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[353]);
					this.guiMain_.uiObjects[353].GetComponent<Menu_RandomEventBugs>().Init(this);
					this.guiMain_.OpenMenu(false);
				}
			}
		}
		else
		{
			if (this.gameTyp != 2)
			{
				float f2 = 0f;
				if (!this.handy && !this.arcade)
				{
					if (this.publisherID != this.developerID)
					{
						if (this.pS_)
						{
							f2 = (float)num57 * this.pS_.share;
						}
					}
					else
					{
						f2 = (float)(num57 * this.verkaufspreis[0]);
					}
				}
				if (this.handy)
				{
					f2 = (float)(num57 * 3);
				}
				if (this.arcade)
				{
					f2 = (float)(num57 * this.verkaufspreis[0]);
				}
				int num72 = Mathf.RoundToInt(f2);
				this.umsatzTotal += (long)num72;
				this.AddFirmenwert((long)num72);
				this.AddTochterfirmaUmsatz((long)num72);
			}
			if (this.gameTyp == 1 && this.mS_.week == 5)
			{
				int num73 = this.abonnements * this.aboPreis;
				this.umsatzTotal += (long)num73;
				this.umsatzAbos += (long)num73;
				this.AddFirmenwert((long)num73);
				this.AddTochterfirmaUmsatz((long)num73);
				this.costs_server += (long)(this.abonnements / 10);
			}
			if (this.gameTyp == 2 && this.mS_.week == 5)
			{
				this.costs_server += (long)(this.abonnements / 10);
			}
			if ((num57 <= 0 && this.abonnements < 10) || (this.gameTyp == 2 && this.abonnements < 10 && this.weeksOnMarket > 5))
			{
				if (!this.typ_bundle)
				{
					this.FindMyEngineNew();
					if (this.engineS_)
					{
						if (this.engineS_.ownerID == this.mS_.myID)
						{
							if (this.guiMain_)
							{
								this.guiMain_.OpenEngineAbrechnung(this);
							}
						}
						else if (this.mS_.multiplayer && this.engineS_.EngineFromMitspieler())
						{
							this.mS_.mpCalls_.SERVER_Send_EngineAbrechnung(this.engineS_.ownerID, this.myID);
						}
						if (this.GetPublisherOrDeveloperIsTochterfirma() && this.guiMain_)
						{
							this.guiMain_.OpenTochterfirmaAbrechnung(this);
						}
					}
				}
				this.RemoveFromMarket();
			}
		}
		if (this.typ_mmoaddon)
		{
			gameScript gameScript = this.FindVorgaengerScript();
			if (gameScript && !gameScript.isOnMarket)
			{
				this.RemoveFromMarket();
			}
		}
		if (this.mS_.multiplayer)
		{
			if (this.mS_.mpCalls_.isServer && (this.IsMyGame() || this.typ_contractGame || (this.DeveloperIsNPC() && this.PublisherIsNPC() && this.OwnerIsNPC())))
			{
				this.mS_.mpCalls_.SERVER_Send_GameData(this);
			}
			if (this.mS_.mpCalls_.isClient && this.IsMyGame())
			{
				this.mS_.mpCalls_.CLIENT_Send_GameData(this);
			}
		}
	}

	
	public void PlayerPayEngineLicence(long e)
	{
		if (!this.typ_bundle)
		{
			this.FindMyEngineNew();
			if (this.engineS_ && this.engineS_.ownerID != this.mS_.myID && this.engineS_.gewinnbeteiligung > 0)
			{
				long num = e * (long)this.engineS_.gewinnbeteiligung / 100L;
				if (num > 0L)
				{
					this.costs_enginegebuehren += num;
					this.mS_.Pay(num, 11);
					if (this.engineS_.ownerID != -1)
					{
						if (this.mS_.mpCalls_.isServer)
						{
							this.mS_.mpCalls_.SERVER_Send_Payment(this.mS_.myID, this.engineS_.ownerID, 0, (int)num);
						}
						if (this.mS_.mpCalls_.isClient)
						{
							this.mS_.mpCalls_.CLIENT_Send_Payment(this.engineS_.ownerID, 0, (int)num);
						}
					}
				}
			}
		}
	}

	
	public void FindMyPlatforms()
	{
		for (int i = 0; i < this.gamePlatform.Length; i++)
		{
			if (this.gamePlatform[i] != -1)
			{
				GameObject gameObject = GameObject.Find("PLATFORM_" + this.gamePlatform[i].ToString());
				if (gameObject)
				{
					this.gamePlatformScript[i] = gameObject.GetComponent<platformScript>();
				}
				else
				{
					this.gamePlatform[i] = -1;
				}
			}
		}
	}

	
	public void FindMyPublisher()
	{
		if (this.publisherID == -1)
		{
			return;
		}
		GameObject gameObject = GameObject.Find("PUB_" + this.publisherID.ToString());
		if (gameObject)
		{
			this.pS_ = gameObject.GetComponent<publisherScript>();
		}
	}

	
	public void FindMyDeveloper()
	{
		if (this.developerID == -1)
		{
			return;
		}
		GameObject gameObject = GameObject.Find("PUB_" + this.developerID.ToString());
		if (gameObject)
		{
			this.devS_ = gameObject.GetComponent<publisherScript>();
		}
	}

	
	public void FindMyEngineNew()
	{
		if (this.engineID == -1)
		{
			this.engineS_ = null;
			return;
		}
		if (this.engineS_)
		{
			if (this.engineID == this.engineS_.myID)
			{
				return;
			}
			this.engineS_ = null;
		}
		GameObject gameObject = GameObject.Find("ENGINE_" + this.engineID.ToString());
		if (gameObject)
		{
			this.engineS_ = gameObject.GetComponent<engineScript>();
			return;
		}
		this.engineID = 0;
	}

	
	public void AddF2PInteresse(float f)
	{
		this.f2pInteresse += f;
		if (this.f2pInteresse > 100f)
		{
			this.f2pInteresse = 100f;
		}
		if (this.weeksOnMarket > 52)
		{
			int num = this.weeksOnMarket;
			if (this.f2pInteresse > 100f - this.GetF2PAbnutzung())
			{
				this.f2pInteresse = 100f - this.GetF2PAbnutzung();
			}
		}
		if (this.f2pInteresse < 0f)
		{
			this.f2pInteresse = 0f;
		}
	}

	
	public void AddMMOInteresse(float f)
	{
		this.mmoInteresse += f;
		if (this.mmoInteresse > 100f)
		{
			this.mmoInteresse = 100f;
		}
		if (this.weeksOnMarket > 52)
		{
			int num = this.weeksOnMarket;
			if (this.mmoInteresse > 100f - this.GetMMOAbnutzung())
			{
				this.mmoInteresse = 100f - this.GetMMOAbnutzung();
			}
		}
		if (this.mmoInteresse < 0f)
		{
			this.mmoInteresse = 0f;
		}
	}

	
	public float GetMMOAbnutzung()
	{
		if (this.weeksOnMarket > 52)
		{
			return (float)(this.weeksOnMarket - 51) / 15f;
		}
		return 0f;
	}

	
	public float GetF2PAbnutzung()
	{
		if (this.weeksOnMarket > 52)
		{
			return (float)(this.weeksOnMarket - 51) / 10f;
		}
		return 0f;
	}

	
	public void AddHype(float f)
	{
		if (this.hype <= 100f)
		{
			this.hype += f;
			if (this.hype > 100f)
			{
				this.hype = 100f;
			}
		}
		if (this.hype > 100f && f < 0f)
		{
			this.hype += f;
		}
		if (this.hype < 0f)
		{
			this.hype = 0f;
		}
	}

	
	public float GetHype()
	{
		return this.hype;
	}

	
	public float GetIpBekanntheit()
	{
		if (this.mainIP == -1)
		{
			return 0f;
		}
		if (!this.script_mainIP)
		{
			this.FindMainIpScript();
		}
		if (!this.script_mainIP)
		{
			this.mainIP = -1;
			return 0f;
		}
		float num = this.script_mainIP.ipPunkte;
		if (num > 1000f)
		{
			num = 1000f;
		}
		if (num < 0f)
		{
			num = 0f;
		}
		return num / 200f;
	}

	
	public long GetIpWert()
	{
		float num = this.script_mainIP.ipPunkte;
		if (num > 1000f)
		{
			num = 1000f;
		}
		if (num < 0f)
		{
			num = 0f;
		}
		long result = 0L;
		if (num > 0f && num <= 100f)
		{
			result = (long)((int)num * 5000);
		}
		if (num > 100f && num <= 200f)
		{
			result = (long)((int)num * 10000);
		}
		if (num > 200f && num <= 300f)
		{
			result = (long)((int)num * 15000);
		}
		if (num > 300f && num <= 400f)
		{
			result = (long)((int)num * 20000);
		}
		if (num > 400f && num <= 500f)
		{
			result = (long)((int)num * 30000);
		}
		if (num > 500f && num <= 600f)
		{
			result = (long)((int)num * 40000);
		}
		if (num > 600f && num <= 700f)
		{
			result = (long)((int)num * 50000);
		}
		if (num > 700f && num <= 800f)
		{
			result = (long)((int)num * 60000);
		}
		if (num > 800f && num <= 900f)
		{
			result = (long)((int)num * 70000);
		}
		if (num > 900f)
		{
			result = (long)((int)num * 100000);
		}
		return result;
	}

	
	public int GetHypeNachfolger()
	{
		if (this.reviewTotal < 30)
		{
			return 1;
		}
		if (this.reviewTotal >= 30 && this.reviewTotal < 40)
		{
			return 5;
		}
		if (this.reviewTotal >= 40 && this.reviewTotal < 50)
		{
			return 10;
		}
		if (this.reviewTotal >= 50 && this.reviewTotal < 60)
		{
			return 15;
		}
		if (this.reviewTotal >= 60 && this.reviewTotal < 70)
		{
			return 25;
		}
		if (this.reviewTotal >= 70 && this.reviewTotal < 80)
		{
			return 35;
		}
		if (this.reviewTotal >= 80 && this.reviewTotal < 90)
		{
			return 40;
		}
		if (this.reviewTotal >= 90 && this.reviewTotal < 98)
		{
			return 50;
		}
		if (this.reviewTotal >= 98)
		{
			return 60;
		}
		return 1;
	}

	
	public int GetHypeSpinoff()
	{
		int num = -1;
		float num2 = 0f;
		for (int i = 0; i < this.games_.arrayGamesScripts.Length; i++)
		{
			gameScript gameScript = this.games_.arrayGamesScripts[i];
			if (gameScript && gameScript.mainIP == this.mainIP && !gameScript.typ_budget && !gameScript.typ_goty && !gameScript.typ_bundle && !gameScript.typ_bundleAddon && !gameScript.typ_addon)
			{
				float num3 = (float)gameScript.date_month;
				num3 /= 13f;
				num3 = (float)gameScript.date_year + num3;
				if (num2 < num3)
				{
					num2 = num3;
					num = i;
				}
			}
		}
		gameScript gameScript2;
		if (num == -1)
		{
			gameScript2 = this;
		}
		else
		{
			gameScript2 = this.games_.arrayGamesScripts[num];
		}
		if (gameScript2.reviewTotal < 30)
		{
			return 1;
		}
		if (gameScript2.reviewTotal >= 30 && gameScript2.reviewTotal < 40)
		{
			return 3;
		}
		if (gameScript2.reviewTotal >= 40 && gameScript2.reviewTotal < 50)
		{
			return 5;
		}
		if (gameScript2.reviewTotal >= 50 && gameScript2.reviewTotal < 60)
		{
			return 8;
		}
		if (gameScript2.reviewTotal >= 60 && gameScript2.reviewTotal < 70)
		{
			return 10;
		}
		if (gameScript2.reviewTotal >= 70 && gameScript2.reviewTotal < 80)
		{
			return 15;
		}
		if (gameScript2.reviewTotal >= 80 && gameScript2.reviewTotal < 90)
		{
			return 20;
		}
		if (gameScript2.reviewTotal >= 90 && gameScript2.reviewTotal < 98)
		{
			return 25;
		}
		if (gameScript2.reviewTotal >= 98)
		{
			return 30;
		}
		return 1;
	}

	
	public int GetAmountFanbriefe()
	{
		int num = 0;
		for (int i = 0; i < this.fanbrief.Length; i++)
		{
			if (this.fanbrief[i])
			{
				num++;
			}
		}
		return num;
	}

	
	public string GetZielgruppeString()
	{
		string result = "";
		switch (this.gameZielgruppe)
		{
		case 0:
			result = this.tS_.GetText(337);
			break;
		case 1:
			result = this.tS_.GetText(338);
			break;
		case 2:
			result = this.tS_.GetText(339);
			break;
		case 3:
			result = this.tS_.GetText(340);
			break;
		case 4:
			result = this.tS_.GetText(341);
			break;
		}
		return result;
	}

	
	private void UpdateFanletter()
	{
		if (this.IsMyGame() || this.IsMyAuftragsspiel())
		{
			if (this.pubOffer)
			{
				return;
			}
			if (this.sellsTotal < 100L)
			{
				return;
			}
			if (this.typ_addon || this.typ_mmoaddon || this.typ_bundle || this.typ_budget || this.typ_addonStandalone || this.typ_bundleAddon || this.typ_goty)
			{
				return;
			}
			if (this.fanbrief.Length == 0)
			{
				this.fanbrief = new bool[this.tS_.fanLetter_GE.Length];
			}
			if (this.fanbrief.Length < this.tS_.fanLetter_GE.Length)
			{
				this.fanbrief = new bool[this.tS_.fanLetter_GE.Length];
			}
			if (!this.fanbrief[0] && this.reviewTotal >= 80 && UnityEngine.Random.Range(0, 10) == 1)
			{
				this.guiMain_.ShowFanLetter(0, this.GetNameWithTag());
				this.fanbrief[0] = true;
				return;
			}
			if (!this.fanbrief[1] && this.reviewTotal <= 40 && UnityEngine.Random.Range(0, 10) == 1)
			{
				this.guiMain_.ShowFanLetter(1, this.GetNameWithTag());
				this.fanbrief[1] = true;
				return;
			}
			if (!this.fanbrief[2] && this.reviewGrafik >= 80 && UnityEngine.Random.Range(0, 10) == 1)
			{
				this.guiMain_.ShowFanLetter(2, this.GetNameWithTag());
				this.fanbrief[2] = true;
				return;
			}
			if (!this.fanbrief[3] && this.reviewGrafik <= 40 && UnityEngine.Random.Range(0, 10) == 1)
			{
				this.guiMain_.ShowFanLetter(3, this.GetNameWithTag());
				this.fanbrief[3] = true;
				return;
			}
			if (!this.fanbrief[4] && this.reviewSound >= 80 && UnityEngine.Random.Range(0, 10) == 1)
			{
				this.guiMain_.ShowFanLetter(4, this.GetNameWithTag());
				this.fanbrief[4] = true;
				return;
			}
			if (!this.fanbrief[5] && this.reviewSound <= 40 && UnityEngine.Random.Range(0, 10) == 1)
			{
				this.guiMain_.ShowFanLetter(5, this.GetNameWithTag());
				this.fanbrief[5] = true;
				return;
			}
			if (!this.fanbrief[6] && this.reviewSteuerung >= 80 && UnityEngine.Random.Range(0, 10) == 1)
			{
				this.guiMain_.ShowFanLetter(6, this.GetNameWithTag());
				this.fanbrief[6] = true;
				return;
			}
			if (!this.fanbrief[7] && this.reviewSteuerung <= 40 && UnityEngine.Random.Range(0, 10) == 1)
			{
				this.guiMain_.ShowFanLetter(7, this.GetNameWithTag());
				this.fanbrief[7] = true;
				return;
			}
			if (!this.fanbrief[8])
			{
				int num = 0;
				for (int i = 0; i < this.gameLanguage.Length; i++)
				{
					if (this.gameLanguage[i])
					{
						num++;
					}
				}
				if (num <= 3 && UnityEngine.Random.Range(0, 10) == 1)
				{
					this.guiMain_.ShowFanLetter(8, this.GetNameWithTag());
					this.fanbrief[8] = true;
					return;
				}
			}
			if (!this.fanbrief[12] && this.Designschwerpunkt[0] < this.genres_.GetFocus(0, this.maingenre, this.subgenre) && UnityEngine.Random.Range(0, 10) == 1)
			{
				this.guiMain_.ShowFanLetter(12, this.GetNameWithTag());
				this.fanbrief[12] = true;
				return;
			}
			if (!this.fanbrief[15] && this.Designschwerpunkt[1] < this.genres_.GetFocus(1, this.maingenre, this.subgenre) && UnityEngine.Random.Range(0, 10) == 1)
			{
				this.guiMain_.ShowFanLetter(15, this.GetNameWithTag());
				this.fanbrief[15] = true;
				return;
			}
			if (!this.fanbrief[16] && this.Designschwerpunkt[2] < this.genres_.GetFocus(2, this.maingenre, this.subgenre) && UnityEngine.Random.Range(0, 10) == 1)
			{
				this.guiMain_.ShowFanLetter(16, this.GetNameWithTag());
				this.fanbrief[16] = true;
				return;
			}
			if (!this.fanbrief[19] && this.Designschwerpunkt[3] < this.genres_.GetFocus(3, this.maingenre, this.subgenre) && UnityEngine.Random.Range(0, 10) == 1)
			{
				this.guiMain_.ShowFanLetter(19, this.GetNameWithTag());
				this.fanbrief[19] = true;
				return;
			}
			if (!this.fanbrief[11] && this.Designschwerpunkt[4] < this.genres_.GetFocus(4, this.maingenre, this.subgenre) && UnityEngine.Random.Range(0, 10) == 1)
			{
				this.guiMain_.ShowFanLetter(11, this.GetNameWithTag());
				this.fanbrief[11] = true;
				return;
			}
			if (!this.fanbrief[20] && this.Designschwerpunkt[5] < this.genres_.GetFocus(5, this.maingenre, this.subgenre) && UnityEngine.Random.Range(0, 10) == 1)
			{
				this.guiMain_.ShowFanLetter(20, this.GetNameWithTag());
				this.fanbrief[20] = true;
				return;
			}
			if (!this.fanbrief[21] && this.Designschwerpunkt[6] < this.genres_.GetFocus(6, this.maingenre, this.subgenre) && UnityEngine.Random.Range(0, 10) == 1)
			{
				this.guiMain_.ShowFanLetter(21, this.GetNameWithTag());
				this.fanbrief[21] = true;
				return;
			}
			if (!this.fanbrief[22] && this.Designschwerpunkt[7] < this.genres_.GetFocus(7, this.maingenre, this.subgenre) && UnityEngine.Random.Range(0, 10) == 1)
			{
				this.guiMain_.ShowFanLetter(22, this.GetNameWithTag());
				this.fanbrief[22] = true;
				return;
			}
			if (!this.fanbrief[17] && this.Designausrichtung[0] > this.genres_.GetAlign(0, this.maingenre, this.subgenre) && UnityEngine.Random.Range(0, 10) == 1)
			{
				this.guiMain_.ShowFanLetter(17, this.GetNameWithTag());
				this.fanbrief[17] = true;
				return;
			}
			if (!this.fanbrief[18] && this.Designausrichtung[0] < this.genres_.GetAlign(0, this.maingenre, this.subgenre) && UnityEngine.Random.Range(0, 10) == 1)
			{
				this.guiMain_.ShowFanLetter(18, this.GetNameWithTag());
				this.fanbrief[18] = true;
				return;
			}
			if (!this.fanbrief[23] && this.Designausrichtung[1] < this.genres_.GetAlign(1, this.maingenre, this.subgenre) && UnityEngine.Random.Range(0, 10) == 1)
			{
				this.guiMain_.ShowFanLetter(23, this.GetNameWithTag());
				this.fanbrief[23] = true;
				return;
			}
			if (!this.fanbrief[24] && this.Designausrichtung[1] > this.genres_.GetAlign(1, this.maingenre, this.subgenre) && UnityEngine.Random.Range(0, 10) == 1)
			{
				this.guiMain_.ShowFanLetter(24, this.GetNameWithTag());
				this.fanbrief[24] = true;
				return;
			}
			if (!this.fanbrief[25] && this.Designausrichtung[2] < this.genres_.GetAlign(2, this.maingenre, this.subgenre) && UnityEngine.Random.Range(0, 10) == 1)
			{
				this.guiMain_.ShowFanLetter(25, this.GetNameWithTag());
				this.fanbrief[25] = true;
				return;
			}
			if (!this.fanbrief[26] && this.Designausrichtung[2] > this.genres_.GetAlign(2, this.maingenre, this.subgenre) && UnityEngine.Random.Range(0, 10) == 1)
			{
				this.guiMain_.ShowFanLetter(26, this.GetNameWithTag());
				this.fanbrief[26] = true;
				return;
			}
		}
	}

	
	public void SetSpielbericht()
	{
		this.spielbericht = true;
	}

	
	public void SetMyName(string c)
	{
		this.myName = c;
	}

	
	public string GetNameSimple()
	{
		return this.myName;
	}

	
	public string GetNameWithTag()
	{
		string text = "";
		if (this.mS_.settings_)
		{
			if (this.GetPublisherOrDeveloperIsTochterfirma() && this.mS_.settings_.tochtefirmaTAG)
			{
				text = this.myName + " <color=green>[★]</color>";
			}
			else
			{
				text = this.myName;
			}
		}
		if (this.tS_)
		{
			if (this.portID != -1)
			{
				text = text + " <color=green>" + this.tS_.GetText(1549) + "</color>";
			}
			if (this.typ_addon || this.typ_addonStandalone || this.typ_mmoaddon)
			{
				text = text + " <color=green>" + this.tS_.GetText(1896) + "</color>";
			}
			if (this.f2pConverted)
			{
				text = text + " <color=grey><i>" + this.tS_.GetText(1401) + "</i></color>";
			}
		}
		return text;
	}

	
	public string GetIpName()
	{
		if (this.ipName == null)
		{
			return this.myName;
		}
		if (this.ipName.Length <= 0)
		{
			return this.myName;
		}
		return this.ipName;
	}

	
	public int GetLagerbestand()
	{
		int num = 0;
		for (int i = 0; i < this.lagerbestand.Length; i++)
		{
			num += this.lagerbestand[i];
		}
		return num;
	}

	
	public string GetLagerbestandString()
	{
		if (this.lagerbestand[1] > 0 || this.lagerbestand[2] > 0)
		{
			return this.mS_.GetMoney((long)this.lagerbestand[0], false) + " [+" + this.mS_.GetMoney((long)(this.lagerbestand[1] + this.lagerbestand[2]), false) + "]";
		}
		return this.mS_.GetMoney((long)this.lagerbestand[0], false);
	}

	
	public int GetVorbestellungen()
	{
		return this.vorbestellungen;
	}

	
	public float GetEditionQualiaet(int i)
	{
		float num = 0f;
		switch (i)
		{
		case 0:
			if (this.standard_edition[0])
			{
				num += 0.01f;
			}
			if (this.standard_edition[1])
			{
				num += 0.05f;
			}
			if (this.standard_edition[2])
			{
				num += 0.02f;
			}
			if (this.standard_edition[3])
			{
				num += 0.02f;
			}
			if (this.standard_edition[4])
			{
				num += 0.03f;
			}
			if (this.standard_edition[5])
			{
				num += 0.03f;
			}
			if (this.standard_edition[6])
			{
				num += 0.04f;
			}
			if (this.standard_edition[7])
			{
				num += 0.04f;
			}
			break;
		case 1:
			num = 0f;
			if (this.deluxe_edition[0])
			{
				num += 0.01f;
			}
			if (this.deluxe_edition[1])
			{
				num += 0.05f;
			}
			if (this.deluxe_edition[2])
			{
				num += 0.05f;
			}
			if (this.deluxe_edition[3])
			{
				num += 0.05f;
			}
			if (this.deluxe_edition[4])
			{
				num += 0.06f;
			}
			if (this.deluxe_edition[5])
			{
				num += 0.08f;
			}
			if (this.deluxe_edition[6])
			{
				num += 0.1f;
			}
			if (this.deluxe_edition[7])
			{
				num += 0.15f;
			}
			if (this.deluxe_edition[8])
			{
				num += 0.2f;
			}
			break;
		case 2:
			num = 0f;
			if (this.collectors_edition[0])
			{
				num += 0.01f;
			}
			if (this.collectors_edition[1])
			{
				num += 0.05f;
			}
			if (this.collectors_edition[2])
			{
				num += 0.05f;
			}
			if (this.collectors_edition[3])
			{
				num += 0.05f;
			}
			if (this.collectors_edition[4])
			{
				num += 0.06f;
			}
			if (this.collectors_edition[5])
			{
				num += 0.08f;
			}
			if (this.collectors_edition[6])
			{
				num += 0.1f;
			}
			if (this.collectors_edition[7])
			{
				num += 0.15f;
			}
			if (this.collectors_edition[8])
			{
				num += 0.2f;
			}
			if (this.collectors_edition[9])
			{
				num += 0.25f;
			}
			break;
		}
		return num;
	}

	
	public float GetPreisAbzug(int i)
	{
		if (this.arcade)
		{
			if (this.verkaufspreis[i] <= 550)
			{
				return 1f;
			}
			if (this.verkaufspreis[i] >= 551 && this.verkaufspreis[i] <= 600)
			{
				return 0.98f;
			}
			if (this.verkaufspreis[i] >= 601 && this.verkaufspreis[i] <= 650)
			{
				return 0.96f;
			}
			if (this.verkaufspreis[i] >= 651 && this.verkaufspreis[i] <= 700)
			{
				return 0.94f;
			}
			if (this.verkaufspreis[i] >= 701 && this.verkaufspreis[i] <= 750)
			{
				return 0.92f;
			}
			if (this.verkaufspreis[i] >= 751 && this.verkaufspreis[i] <= 800)
			{
				return 0.9f;
			}
			if (this.verkaufspreis[i] >= 801 && this.verkaufspreis[i] <= 850)
			{
				return 0.88f;
			}
			if (this.verkaufspreis[i] >= 851 && this.verkaufspreis[i] <= 900)
			{
				return 0.86f;
			}
			if (this.verkaufspreis[i] >= 901 && this.verkaufspreis[i] <= 950)
			{
				return 0.84f;
			}
			if (this.verkaufspreis[i] >= 951 && this.verkaufspreis[i] <= 1000)
			{
				return 0.82f;
			}
			if (this.verkaufspreis[i] >= 1001 && this.verkaufspreis[i] <= 1050)
			{
				return 0.8f;
			}
			if (this.verkaufspreis[i] >= 1051 && this.verkaufspreis[i] <= 1100)
			{
				return 0.75f;
			}
			if (this.verkaufspreis[i] >= 1101 && this.verkaufspreis[i] <= 1150)
			{
				return 0.7f;
			}
			if (this.verkaufspreis[i] >= 1151 && this.verkaufspreis[i] <= 1200)
			{
				return 0.65f;
			}
			if (this.verkaufspreis[i] >= 1201 && this.verkaufspreis[i] <= 1250)
			{
				return 0.6f;
			}
			if (this.verkaufspreis[i] >= 1251 && this.verkaufspreis[i] <= 1300)
			{
				return 0.55f;
			}
			if (this.verkaufspreis[i] >= 1301 && this.verkaufspreis[i] <= 1350)
			{
				return 0.5f;
			}
			if (this.verkaufspreis[i] >= 1351 && this.verkaufspreis[i] <= 1400)
			{
				return 0.45f;
			}
			if (this.verkaufspreis[i] >= 1401 && this.verkaufspreis[i] <= 1450)
			{
				return 0.4f;
			}
			if (this.verkaufspreis[i] > 1450)
			{
				return 0.3f;
			}
		}
		if (this.handy)
		{
			if (this.verkaufspreis[i] == 1)
			{
				return 1f;
			}
			if (this.verkaufspreis[i] == 2)
			{
				return 0.8f;
			}
			if (this.verkaufspreis[i] == 3)
			{
				return 0.6f;
			}
			if (this.verkaufspreis[i] == 4)
			{
				return 0.4f;
			}
			if (this.verkaufspreis[i] >= 5)
			{
				return 0.25f;
			}
			return 0.5f;
		}
		else
		{
			if (this.verkaufspreis[i] <= 4)
			{
				return 1f;
			}
			if (this.verkaufspreis[i] >= 5 && this.verkaufspreis[i] <= 9)
			{
				return 0.95f;
			}
			if (this.verkaufspreis[i] >= 10 && this.verkaufspreis[i] <= 14)
			{
				return 0.9f;
			}
			if (this.verkaufspreis[i] >= 15 && this.verkaufspreis[i] <= 19)
			{
				return 0.8f;
			}
			if (this.verkaufspreis[i] >= 20 && this.verkaufspreis[i] <= 24)
			{
				return 0.7f;
			}
			if (this.verkaufspreis[i] >= 25 && this.verkaufspreis[i] <= 29)
			{
				return 0.6f;
			}
			if (this.verkaufspreis[i] >= 30 && this.verkaufspreis[i] <= 34)
			{
				return 0.5f;
			}
			if (this.verkaufspreis[i] >= 35 && this.verkaufspreis[i] <= 39)
			{
				return 0.45f;
			}
			if (this.verkaufspreis[i] >= 40 && this.verkaufspreis[i] <= 44)
			{
				return 0.3f;
			}
			if (this.verkaufspreis[i] >= 45 && this.verkaufspreis[i] <= 49)
			{
				return 0.25f;
			}
			if (this.verkaufspreis[i] >= 50 && this.verkaufspreis[i] <= 54)
			{
				return 0.22f;
			}
			if (this.verkaufspreis[i] >= 55 && this.verkaufspreis[i] <= 59)
			{
				return 0.2f;
			}
			if (this.verkaufspreis[i] >= 60 && this.verkaufspreis[i] <= 64)
			{
				return 0.18f;
			}
			if (this.verkaufspreis[i] >= 65 && this.verkaufspreis[i] <= 69)
			{
				return 0.16f;
			}
			if (this.verkaufspreis[i] >= 70 && this.verkaufspreis[i] <= 74)
			{
				return 0.15f;
			}
			if (this.verkaufspreis[i] >= 75 && this.verkaufspreis[i] <= 79)
			{
				return 0.14f;
			}
			if (this.verkaufspreis[i] >= 80 && this.verkaufspreis[i] <= 84)
			{
				return 0.12f;
			}
			if (this.verkaufspreis[i] >= 85 && this.verkaufspreis[i] <= 89)
			{
				return 0.1f;
			}
			if (this.verkaufspreis[i] >= 90 && this.verkaufspreis[i] <= 94)
			{
				return 0.08f;
			}
			if (this.verkaufspreis[i] >= 95)
			{
				return 0.05f;
			}
			return 0.5f;
		}
	}

	
	public float GetProduktionskosten(int edition)
	{
		float num = 0f;
		switch (edition)
		{
		case 0:
			for (int i = 0; i < this.standard_edition.Length; i++)
			{
				if (this.standard_edition[i])
				{
					num += this.games_.preise_inhalt[i];
				}
			}
			break;
		case 1:
			for (int j = 0; j < this.deluxe_edition.Length; j++)
			{
				if (this.deluxe_edition[j])
				{
					num += this.games_.preise_inhalt[j];
				}
			}
			break;
		case 2:
			for (int k = 0; k < this.collectors_edition.Length; k++)
			{
				if (this.collectors_edition[k])
				{
					num += this.games_.preise_inhalt[k];
				}
			}
			break;
		case 3:
			return 0f;
		}
		return num + this.games_.GetGrundkosten();
	}

	
	public float CalcPlatformComplex(float points)
	{
		float num = 1f;
		for (int i = 0; i < this.gamePlatform.Length; i++)
		{
			if (this.gamePlatform[i] != -1)
			{
				if (!this.gamePlatformScript[i])
				{
					this.FindMyPlatforms();
				}
				if (this.gamePlatformScript[i])
				{
					switch (this.gamePlatformScript[i].complex)
					{
					case 0:
						num += 0.1f;
						break;
					case 1:
						num += 0.3f;
						break;
					case 2:
						num += 0.6f;
						break;
					}
				}
			}
		}
		return num * points;
	}

	
	public engineScript GetEngineScript()
	{
		this.FindMyEngineNew();
		return this.engineS_;
	}

	
	private string GetUskString()
	{
		if (!this.tS_)
		{
			return "";
		}
		string result = "";
		switch (this.usk)
		{
		case 0:
			result = "0";
			break;
		case 1:
			result = "6";
			break;
		case 2:
			result = "12";
			break;
		case 3:
			result = "16";
			break;
		case 4:
			result = "18";
			break;
		case 5:
			result = this.tS_.GetText(1306);
			break;
		}
		return result;
	}

	
	public gameScript GetBundleGame(int slot_)
	{
		if (this.bundleID[slot_] == -1)
		{
			return null;
		}
		for (int i = 0; i < this.games_.arrayGamesScripts.Length; i++)
		{
			if (this.games_.arrayGamesScripts[i] && this.games_.arrayGamesScripts[i].myID == this.bundleID[slot_])
			{
				return this.games_.arrayGamesScripts[i];
			}
		}
		return null;
	}

	
	public bool HasInAppPurchases()
	{
		for (int i = 0; i < this.inAppPurchase.Length; i++)
		{
			if (this.inAppPurchase[i])
			{
				return true;
			}
		}
		return false;
	}

	
	public float GetInAppPurchaseHate()
	{
		float num = 0f;
		for (int i = 0; i < this.inAppPurchase.Length; i++)
		{
			if (this.inAppPurchase[i])
			{
				num += this.games_.inAppPurchaseHate[i];
			}
		}
		return num;
	}

	
	public float GetInAppPurchaseMoneyPerWeek()
	{
		float num = 0f;
		for (int i = 0; i < this.inAppPurchase.Length; i++)
		{
			if (this.inAppPurchase[i])
			{
				num += this.games_.inAppPurchasePrice[i];
			}
		}
		return num * 0.25f;
	}

	
	public bool ExistAutomatenspiel()
	{
		if (this.gameTyp == 0 && !this.typ_addon && !this.typ_addonStandalone && !this.typ_bundle && !this.typ_bundleAddon && !this.typ_contractGame && !this.typ_mmoaddon && !this.arcade)
		{
			if (this.portExist[2])
			{
				return true;
			}
			if (this.portID != -1)
			{
				this.FindPortOriginalScript();
				if (this.script_portOriginal && this.script_portOriginal.arcade)
				{
					return true;
				}
			}
		}
		return false;
	}

	
	public bool IsHit()
	{
		return !this.typ_addon && !this.typ_addonStandalone && !this.typ_budget && !this.typ_bundle && !this.typ_mmoaddon && !this.typ_bundleAddon && this.reviewTotal >= 80;
	}

	
	public bool HasGold()
	{
		return this.gameTyp != 2 && !this.arcade && !this.typ_addon && !this.typ_addonStandalone && !this.typ_mmoaddon && this.sellsTotal >= 1000000L;
	}

	
	public bool HasPlatin()
	{
		return this.gameTyp != 2 && !this.arcade && !this.typ_addon && !this.typ_addonStandalone && !this.typ_mmoaddon && this.sellsTotal >= 5000000L;
	}

	
	public bool HasDiamant()
	{
		return this.gameTyp != 2 && !this.arcade && !this.typ_addon && !this.typ_addonStandalone && !this.typ_mmoaddon && this.sellsTotal >= 10000000L;
	}

	
	public bool AllePlattformenReleased()
	{
		this.FindMyPlatforms();
		for (int i = 0; i < this.gamePlatformScript.Length; i++)
		{
			if (this.gamePlatformScript[i] && !this.gamePlatformScript[i].isUnlocked)
			{
				return false;
			}
		}
		return true;
	}

	
	public string GetUnreleasedPlatformsString()
	{
		this.FindMyPlatforms();
		string text = "";
		for (int i = 0; i < this.gamePlatformScript.Length; i++)
		{
			if (this.gamePlatformScript[i] && !this.gamePlatformScript[i].isUnlocked)
			{
				text = text + this.gamePlatformScript[i].GetName() + "\n";
			}
		}
		return text;
	}

	
	public void UpdateAutoPreis()
	{
		if (this.typ_budget)
		{
			this.verkaufspreis[0] = 10;
			this.verkaufspreis[3] = 10;
			if (this.weeksOnMarket > 30)
			{
				this.verkaufspreis[0] -= 3;
				this.verkaufspreis[3] -= 3;
			}
			if (this.weeksOnMarket > 60)
			{
				this.verkaufspreis[0] -= 2;
				this.verkaufspreis[3] -= 2;
			}
			if (this.verkaufspreis[0] > 10)
			{
				this.verkaufspreis[0] = 10;
			}
			if (this.verkaufspreis[3] > 10)
			{
				this.verkaufspreis[3] = 10;
			}
			if (this.verkaufspreis[0] < 5)
			{
				this.verkaufspreis[0] = 5;
			}
			if (this.verkaufspreis[3] < 5)
			{
				this.verkaufspreis[3] = 5;
			}
			return;
		}
		if (this.typ_goty)
		{
			this.verkaufspreis[0] = 19;
			this.verkaufspreis[1] = 29;
			this.verkaufspreis[2] = 39;
			this.verkaufspreis[3] = 19;
			if (this.weeksOnMarket > 30)
			{
				this.verkaufspreis[0] -= 4;
				this.verkaufspreis[1] -= 4;
				this.verkaufspreis[2] -= 4;
				this.verkaufspreis[3] -= 4;
			}
			if (this.weeksOnMarket > 60)
			{
				this.verkaufspreis[0] -= 6;
				this.verkaufspreis[1] -= 6;
				this.verkaufspreis[2] -= 6;
				this.verkaufspreis[3] -= 6;
			}
			if (this.verkaufspreis[0] > 19)
			{
				this.verkaufspreis[0] = 19;
			}
			if (this.verkaufspreis[1] > 29)
			{
				this.verkaufspreis[1] = 29;
			}
			if (this.verkaufspreis[2] > 39)
			{
				this.verkaufspreis[2] = 39;
			}
			if (this.verkaufspreis[3] > 19)
			{
				this.verkaufspreis[3] = 19;
			}
			if (this.verkaufspreis[0] < 5)
			{
				this.verkaufspreis[0] = 5;
			}
			if (this.verkaufspreis[1] < 6)
			{
				this.verkaufspreis[1] = 6;
			}
			if (this.verkaufspreis[2] < 7)
			{
				this.verkaufspreis[2] = 7;
			}
			if (this.verkaufspreis[3] < 5)
			{
				this.verkaufspreis[3] = 5;
			}
			return;
		}
		if (this.typ_addon || this.typ_addonStandalone || this.typ_mmoaddon)
		{
			this.verkaufspreis[0] = 29;
			this.verkaufspreis[1] = 39;
			this.verkaufspreis[2] = 49;
			this.verkaufspreis[3] = 29;
			if (this.weeksOnMarket > 30)
			{
				this.verkaufspreis[0] -= 5;
				this.verkaufspreis[1] -= 5;
				this.verkaufspreis[2] -= 5;
				this.verkaufspreis[3] -= 5;
			}
			if (this.weeksOnMarket > 60)
			{
				this.verkaufspreis[0] -= 5;
				this.verkaufspreis[1] -= 5;
				this.verkaufspreis[2] -= 5;
				this.verkaufspreis[3] -= 5;
			}
			if (this.verkaufspreis[0] > 29)
			{
				this.verkaufspreis[0] = 29;
			}
			if (this.verkaufspreis[1] > 39)
			{
				this.verkaufspreis[1] = 39;
			}
			if (this.verkaufspreis[2] > 49)
			{
				this.verkaufspreis[2] = 49;
			}
			if (this.verkaufspreis[3] > 29)
			{
				this.verkaufspreis[3] = 29;
			}
			if (this.verkaufspreis[0] < 5)
			{
				this.verkaufspreis[0] = 5;
			}
			if (this.verkaufspreis[1] < 6)
			{
				this.verkaufspreis[1] = 6;
			}
			if (this.verkaufspreis[2] < 7)
			{
				this.verkaufspreis[2] = 7;
			}
			if (this.verkaufspreis[3] < 5)
			{
				this.verkaufspreis[3] = 5;
			}
			return;
		}
		if (this.reviewTotal <= 50)
		{
			this.verkaufspreis[0] = 35;
			this.verkaufspreis[1] = 45;
			this.verkaufspreis[2] = 55;
			this.verkaufspreis[3] = 35;
		}
		if (this.reviewTotal > 50 && this.reviewTotal <= 70)
		{
			this.verkaufspreis[0] = 39;
			this.verkaufspreis[1] = 49;
			this.verkaufspreis[2] = 59;
			this.verkaufspreis[3] = 39;
		}
		if (this.reviewTotal > 70 && this.reviewTotal <= 90)
		{
			this.verkaufspreis[0] = 45;
			this.verkaufspreis[1] = 55;
			this.verkaufspreis[2] = 65;
			this.verkaufspreis[3] = 55;
		}
		if (this.reviewTotal > 90)
		{
			this.verkaufspreis[0] = 49;
			this.verkaufspreis[1] = 59;
			this.verkaufspreis[2] = 69;
			this.verkaufspreis[3] = 49;
		}
		if (this.weeksOnMarket > 30)
		{
			this.verkaufspreis[0] -= 5;
			this.verkaufspreis[1] -= 5;
			this.verkaufspreis[2] -= 5;
			this.verkaufspreis[3] -= 5;
		}
		if (this.weeksOnMarket > 50)
		{
			this.verkaufspreis[0] -= 5;
			this.verkaufspreis[1] -= 5;
			this.verkaufspreis[2] -= 5;
			this.verkaufspreis[3] -= 5;
		}
		if (this.weeksOnMarket > 70)
		{
			this.verkaufspreis[0] -= 5;
			this.verkaufspreis[1] -= 5;
			this.verkaufspreis[2] -= 5;
			this.verkaufspreis[3] -= 5;
		}
		if (this.verkaufspreis[0] > 79)
		{
			this.verkaufspreis[0] = 79;
		}
		if (this.verkaufspreis[1] > 89)
		{
			this.verkaufspreis[1] = 89;
		}
		if (this.verkaufspreis[2] > 99)
		{
			this.verkaufspreis[2] = 99;
		}
		if (this.verkaufspreis[3] > 79)
		{
			this.verkaufspreis[3] = 79;
		}
		if (this.verkaufspreis[0] < 5)
		{
			this.verkaufspreis[0] = 5;
		}
		if (this.verkaufspreis[1] < 6)
		{
			this.verkaufspreis[1] = 6;
		}
		if (this.verkaufspreis[2] < 7)
		{
			this.verkaufspreis[2] = 7;
		}
		if (this.verkaufspreis[3] < 5)
		{
			this.verkaufspreis[3] = 5;
		}
	}

	
	public Sprite GetDeveloperLogo()
	{
		if (!this.guiMain_)
		{
			this.FindScripts();
		}
		GameObject gameObject = GameObject.Find("PUB_" + this.developerID.ToString());
		if (gameObject)
		{
			return gameObject.GetComponent<publisherScript>().GetLogo();
		}
		return this.guiMain_.uiSprites[19];
	}

	
	public void FindPublisherForGame()
	{
		int num = -1;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Publisher");
		if (!this.devS_)
		{
			this.FindMyDeveloper();
		}
		if (this.devS_ && this.devS_.IsMyTochterfirma() && this.devS_.tf_ownPublisher)
		{
			List<gameScript.PublisherList> list = new List<gameScript.PublisherList>();
			for (int n = 0; n < array.Length; n++)
			{
				if (array[n])
				{
					publisherScript component = array[n].GetComponent<publisherScript>();
					if (component && component.isUnlocked && !component.isPlayer && component.publisher && component.IsMyTochterfirma() && !component.TochterfirmaGeschlossen())
					{
						list.Add(new gameScript.PublisherList(0L, component));
					}
				}
			}
			if (list.Count > 0)
			{
				this.publisherID = list[UnityEngine.Random.Range(0, list.Count)].script_.myID;
				return;
			}
		}
		if (UnityEngine.Random.Range(0, 100) > 50)
		{
			List<gameScript.PublisherList> list2 = new List<gameScript.PublisherList>();
			for (int j = 0; j < array.Length; j++)
			{
				if (array[j])
				{
					publisherScript component2 = array[j].GetComponent<publisherScript>();
					if (component2 && component2.isUnlocked && !component2.isPlayer && component2.publisher && !component2.TochterfirmaGeschlossen())
					{
						float f = component2.share + component2.stars * 0.1f;
						list2.Add(new gameScript.PublisherList((long)Mathf.RoundToInt(f), component2));
					}
				}
			}
			list2 = (from i in list2
			orderby i.wert descending
			select i).ToList<gameScript.PublisherList>();
			for (int k = 0; k < list2.Count; k++)
			{
				if (UnityEngine.Random.Range(0, 100) > 80)
				{
					this.publisherID = list2[k].script_.myID;
					return;
				}
			}
		}
		for (int l = 0; l < 10; l++)
		{
			int num2 = UnityEngine.Random.Range(0, array.Length - 1);
			if (array[num2])
			{
				publisherScript component3 = array[num2].GetComponent<publisherScript>();
				if (component3 && component3.isUnlocked && !component3.isPlayer && component3.publisher && !component3.TochterfirmaGeschlossen())
				{
					this.publisherID = component3.myID;
					return;
				}
			}
		}
		for (int m = 0; m < array.Length; m++)
		{
			if (array[m])
			{
				publisherScript component4 = array[m].GetComponent<publisherScript>();
				if (component4 && component4.isUnlocked && !component4.isPlayer && component4.publisher && !component4.TochterfirmaGeschlossen() && (this.publisherID == -1 || UnityEngine.Random.Range(0, 100) > 70))
				{
					num = component4.myID;
				}
			}
		}
		this.publisherID = num;
	}

	
	public int PUBOFFER_GetGarantiesumme()
	{
		if (this.typ_budget)
		{
			return 0;
		}
		if (this.typ_bundle)
		{
			return 0;
		}
		if (this.typ_bundleAddon)
		{
			return 0;
		}
		if (this.typ_goty)
		{
			return 0;
		}
		float num = this.pubAngebot_VerhandlungProzent;
		num *= 0.01f;
		return Mathf.RoundToInt((float)this.pubAngebot_Garantiesumme * num);
	}

	
	public int PUBOFFER_GetGewinnbeteiligung()
	{
		float num = this.pubAngebot_VerhandlungProzent;
		num *= 0.01f;
		return Mathf.RoundToInt(this.pubAngebot_Gewinnbeteiligung * num);
	}

	
	public string PUBOFFER_GetRetailDigitalString()
	{
		if (this.tS_)
		{
			if (this.pubAngebot_Retail && this.pubAngebot_Digital)
			{
				return this.tS_.GetText(1746);
			}
			if (this.pubAngebot_Retail && !this.pubAngebot_Digital)
			{
				return this.tS_.GetText(1747);
			}
			if (!this.pubAngebot_Retail && this.pubAngebot_Digital)
			{
				return this.tS_.GetText(1748);
			}
		}
		return "<missing>";
	}

	
	public string PUBOFFER_GetTooltip()
	{
		string text = "<b>" + this.GetNameWithTag() + "</b>";
		text = text + "\n<color=black>" + this.GetDeveloperName() + "</color>";
		if (!this.typ_bundle && !this.typ_bundleAddon)
		{
			text = string.Concat(new string[]
			{
				text,
				"\n",
				this.GetTypString(),
				" | ",
				this.GetPlatformTypString(),
				"\n"
			});
		}
		text = text + "<color=magenta>" + this.PUBOFFER_GetRetailDigitalString() + "</color>\n";
		for (int i = 0; i < this.gamePlatform.Length; i++)
		{
			if (this.gamePlatform[i] > 0)
			{
				if (!this.gamePlatformScript[i])
				{
					this.FindMyPlatforms();
				}
				if (this.gamePlatformScript[i])
				{
					text = text + "\n<color=blue>" + this.gamePlatformScript[i].GetName() + "</color>";
				}
			}
		}
		text += "\n\n";
		text = text + this.genres_.GetName(this.maingenre) + "\n";
		text = string.Concat(new string[]
		{
			text,
			this.tS_.GetText(327),
			": <color=blue>",
			this.tS_.GetText(330 + this.gameSize - 1),
			"</color>\n"
		});
		text = string.Concat(new string[]
		{
			text,
			this.tS_.GetText(1730),
			": <color=blue>",
			this.mS_.GetMoney((long)this.PUBOFFER_GetGarantiesumme(), true),
			"</color>\n"
		});
		text = string.Concat(new string[]
		{
			text,
			this.tS_.GetText(1731),
			": <color=blue>",
			this.PUBOFFER_GetGewinnbeteiligung().ToString(),
			"%</color>\n"
		});
		text += "\n";
		int i2 = Mathf.RoundToInt((float)(this.reviewTotal / 20));
		text = string.Concat(new string[]
		{
			text,
			this.tS_.GetText(1732),
			"\n<size=21>",
			this.PUBOFFER_GetQualitatStars(i2),
			"</size>\n\n"
		});
		if (!this.typ_bundle)
		{
			text = string.Concat(new string[]
			{
				text,
				this.tS_.GetText(1555),
				": <color=blue>",
				this.mS_.Round(this.GetIpBekanntheit(), 1).ToString(),
				"</color>\n\n"
			});
		}
		text = text + this.tS_.GetText(1736) + "\n";
		if (this.pubAngebot_Stimmung < 33f)
		{
			text = text + "<color=red><b>" + this.tS_.GetText(1740) + "</b></color>";
		}
		if (this.pubAngebot_Stimmung > 33f && this.pubAngebot_Stimmung < 66f)
		{
			text = text + "<color=orange><b>" + this.tS_.GetText(1741) + "</b></color>";
		}
		if (this.pubAngebot_Stimmung > 66f)
		{
			text = text + "<color=green><b>" + this.tS_.GetText(1742) + "</b></color>";
		}
		return text;
	}

	
	private string PUBOFFER_GetQualitatStars(int i)
	{
		string result;
		switch (i)
		{
		case 0:
			result = "☆☆☆☆☆";
			break;
		case 1:
			result = "★☆☆☆☆";
			break;
		case 2:
			result = "★★☆☆☆";
			break;
		case 3:
			result = "★★★☆☆";
			break;
		case 4:
			result = "★★★★☆";
			break;
		case 5:
			result = "★★★★★";
			break;
		default:
			result = "☆☆☆☆☆";
			break;
		}
		return result;
	}

	
	public bool IsMyAuftragsspiel()
	{
		if (!this.mS_)
		{
			this.FindScripts();
		}
		return this.typ_contractGame && this.developerID == this.mS_.myID && this.ownerID != this.mS_.myID;
	}

	
	public string GetUrsprungsName()
	{
		gameScript gameScript = this.FindVorgaengerScript();
		if (gameScript)
		{
			for (int i = 0; i < 10000; i++)
			{
				gameScript gameScript2 = gameScript.FindVorgaengerScript();
				if (!gameScript2)
				{
					IL_3B:
					return gameScript.myName;
				}
				gameScript = gameScript2;
			}
			goto IL_3B;
		}
		return this.myName;
	}

	
	public bool GetDeveloperIsTochtefirma()
	{
		if (!this.devS_)
		{
			this.FindMyDeveloper();
		}
		return this.devS_ && this.devS_.IsMyTochterfirma();
	}

	
	public bool GetPublisherIsTochtefirma()
	{
		if (!this.pS_)
		{
			this.FindMyPublisher();
		}
		return this.pS_ && this.pS_.IsMyTochterfirma();
	}

	
	public bool GetPublisherOrDeveloperIsTochterfirma()
	{
		if (!this.pS_)
		{
			this.FindMyPublisher();
		}
		if (!this.devS_)
		{
			this.FindMyDeveloper();
		}
		return (this.pS_ && this.pS_.IsMyTochterfirma()) || (this.devS_ && this.devS_.IsMyTochterfirma());
	}

	
	public bool IsMyGame()
	{
		return !this.typ_contractGame && (this.developerID == this.mS_.myID || this.publisherID == this.mS_.myID) && ((this.developerID == this.mS_.myID && this.publisherID != this.mS_.myID) || (this.developerID == this.mS_.myID && this.publisherID == this.mS_.myID) || (this.developerID != this.mS_.myID && this.publisherID == this.mS_.myID));
	}

	
	public void AddTochterfirmaUmsatz(long i)
	{
		if (!this.IsMyGame())
		{
			if (!this.pS_)
			{
				this.FindMyPublisher();
			}
			if (!this.devS_)
			{
				this.FindMyDeveloper();
			}
			if (this.pS_ && this.devS_ && (this.pS_.IsMyTochterfirma() || this.devS_.IsMyTochterfirma()))
			{
				long num = i / 100L * (long)Mathf.RoundToInt(this.games_.tf_gewinnbeteiligungTochterfirma);
				this.mS_.Earn(num, 13);
				this.tw_gewinnanteil += num;
				if (this.pS_.IsMyTochterfirma() && this.devS_.IsMyTochterfirma())
				{
					this.devS_.AddTochterfirmaUmsatz(num);
					return;
				}
				if (this.pS_.IsMyTochterfirma() && !this.devS_.IsMyTochterfirma())
				{
					this.pS_.AddTochterfirmaUmsatz(num);
					return;
				}
				if (!this.pS_.IsMyTochterfirma() && this.devS_.IsMyTochterfirma())
				{
					this.devS_.AddTochterfirmaUmsatz(num);
					return;
				}
			}
		}
	}

	
	public void AddFirmenwert(long i)
	{
		if (!this.pS_)
		{
			this.FindMyPublisher();
		}
		if (this.pS_)
		{
			switch (Mathf.RoundToInt(this.pS_.stars / 20f))
			{
			case 0:
				this.pS_.firmenwert += i / 20L;
				break;
			case 1:
				this.pS_.firmenwert += i / 17L;
				break;
			case 2:
				this.pS_.firmenwert += i / 15L;
				break;
			case 3:
				this.pS_.firmenwert += i / 10L;
				break;
			case 4:
				this.pS_.firmenwert += i / 7L;
				break;
			case 5:
				this.pS_.firmenwert += i / 5L;
				break;
			default:
				this.pS_.firmenwert += i / 20L;
				break;
			}
		}
		if (!this.devS_)
		{
			this.FindMyDeveloper();
		}
		if (this.devS_)
		{
			switch (Mathf.RoundToInt(this.devS_.stars / 20f))
			{
			case 0:
				this.devS_.firmenwert += i / 20L;
				return;
			case 1:
				this.devS_.firmenwert += i / 17L;
				return;
			case 2:
				this.devS_.firmenwert += i / 15L;
				return;
			case 3:
				this.devS_.firmenwert += i / 10L;
				return;
			case 4:
				this.devS_.firmenwert += i / 7L;
				return;
			case 5:
				this.devS_.firmenwert += i / 5L;
				return;
			default:
				this.devS_.firmenwert += i / 20L;
				break;
			}
		}
	}

	
	public bool IsMyIP(publisherScript script_)
	{
		return script_.myID == this.ownerID;
	}

	
	public bool OwnerIsNPC()
	{
		return this.ownerID < 100000;
	}

	
	public bool DeveloperIsNPC()
	{
		return this.developerID < 100000;
	}

	
	public bool PublisherIsNPC()
	{
		return this.publisherID < 100000;
	}

	
	public bool GameFromMitspieler()
	{
		return this.mS_.multiplayer && (this.ownerID >= 100000 || this.publisherID >= 100000 || this.developerID >= 100000) && this.ownerID != this.mS_.myID && this.publisherID != this.mS_.myID && this.developerID != this.mS_.myID && (this.ownerID >= 100000 || this.publisherID >= 100000 || this.developerID >= 100000);
	}

	
	public int GetIdFromMitspieler()
	{
		if (!this.mS_.multiplayer)
		{
			return -1;
		}
		if (this.ownerID < 100000 && this.publisherID < 100000 && this.developerID < 100000)
		{
			return -1;
		}
		if (this.ownerID >= 100000)
		{
			return this.ownerID;
		}
		if (this.developerID >= 100000)
		{
			return this.developerID;
		}
		if (this.publisherID >= 100000)
		{
			return this.publisherID;
		}
		return -1;
	}

	
	public GameObject main_;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public GUI_Main guiMain_;

	
	public genres genres_;

	
	public engineFeatures eF_;

	
	public gameplayFeatures gF_;

	
	public games games_;

	
	public themes themes_;

	
	public unlockScript unlock_;

	
	public licences licences_;

	
	public gameTab gameTab_;

	
	public bool debug;

	
	public int myID;

	
	public int developerID = -1;

	
	public int publisherID = -1;

	
	public int ownerID = -1;

	
	public int mainIP = -1;

	
	public publisherScript devS_;

	
	public publisherScript pS_;

	
	public string myName = "";

	
	public string ipName = "";

	
	public bool inDevelopment = true;

	
	public int engineID;

	
	public engineScript engineS_;

	
	public float hype;

	
	public bool isOnMarket;

	
	public bool warBeiAwards;

	
	public int weeksOnMarket;

	
	public int weeksInDevelopment;

	
	public int usk;

	
	public int freigabeBudget;

	
	public int reviewGameplay;

	
	public int reviewGrafik;

	
	public int reviewSound;

	
	public int reviewSteuerung;

	
	public int reviewTotal;

	
	public int reviewGameplayText = -1;

	
	public int reviewGrafikText = -1;

	
	public int reviewSoundText = -1;

	
	public int reviewSteuerungText = -1;

	
	public int reviewTotalText = -1;

	
	public int date_year;

	
	public int date_month;

	
	public int date_start_year;

	
	public int date_start_month;

	
	public int userPositiv;

	
	public int userNegativ;

	
	public long sellsTotalStandard;

	
	public long sellsTotalDeluxe;

	
	public long sellsTotalCollectors;

	
	public long sellsTotalOnline;

	
	public long sellsTotal;

	
	public long umsatzTotal;

	
	public long umsatzAbos;

	
	public long umsatzInApp;

	
	public long exklusivKonsolenSells;

	
	public long costs_entwicklung;

	
	public long costs_mitarbeiter;

	
	public long costs_marketing;

	
	public long costs_enginegebuehren;

	
	public long costs_server;

	
	public long costs_production;

	
	public long costs_updates;

	
	public bool typ_standard;

	
	public bool typ_nachfolger;

	
	public int originalIP = -1;

	
	public int teile;

	
	public gameScript script_vorgaenger;

	
	public gameScript script_nachfolger;

	
	public gameScript script_mainIP;

	
	public gameScript script_portOriginal;

	
	public bool typ_contractGame;

	
	public bool typ_remaster;

	
	public bool typ_spinoff;

	
	public bool typ_addon;

	
	public bool typ_addonStandalone;

	
	public bool typ_mmoaddon;

	
	public bool typ_bundle;

	
	public bool typ_budget;

	
	public bool typ_bundleAddon;

	
	public bool typ_goty;

	
	public int originalGameID = -1;

	
	public int portID = -1;

	
	public float ipPunkte;

	
	public int ipTime;

	
	public int bestChartPosition;

	
	public int lastChartPosition;

	
	public bool pubOffer;

	
	public bool sonderIP;

	
	public int sonderIPMindestreview;

	
	public int[] specialMarketing;

	
	public bool exklusiv;

	
	public bool herstellerExklusiv;

	
	public bool retro;

	
	public bool handy;

	
	public bool arcade;

	
	public bool goty;

	
	public bool[] portExist;

	
	public bool nachfolger_created;

	
	public bool remaster_created;

	
	public bool budget_created;

	
	public bool goty_created;

	
	public bool mmoTOf2p_created;

	
	public bool f2pConverted;

	
	public bool trendsetter;

	
	public bool spielbericht;

	
	public bool spielbericht_favorit;

	
	public bool archiv_spielkonzept;

	
	public bool archiv_spielbericht;

	
	public bool archiv_fanbriefe;

	
	public bool bundle_created;

	
	public int[] bundleID;

	
	public int amountUpdates;

	
	public float bonusSellsUpdates;

	
	public int amountAddons;

	
	public float bonusSellsAddons;

	
	public int amountMMOAddons;

	
	public float bonusSellsMMOAddons;

	
	public float addonQuality;

	
	public float f2pInteresse;

	
	public float mmoInteresse;

	
	public int devAktFeature;

	
	public float devPoints;

	
	public float devPointsStart;

	
	public float devPoints_Gesamt;

	
	public float devPointsStart_Gesamt;

	
	public float points_gameplay;

	
	public float points_grafik;

	
	public float points_sound;

	
	public float points_technik;

	
	public float points_bugs;

	
	public float points_bugsInvis;

	
	public string beschreibung = "";

	
	public int gameTyp;

	
	public int gameSize;

	
	public int gameZielgruppe;

	
	public int maingenre;

	
	public int subgenre;

	
	public int gameMainTheme;

	
	public int gameSubTheme;

	
	public int gameLicence = -1;

	
	public int gameCopyProtect = -1;

	
	public copyProtectScript gameCopyProtectScript_;

	
	public int gameAntiCheat = -1;

	
	public antiCheatScript gameAntiCheatScript_;

	
	public int gameAP_Gameplay;

	
	public int gameAP_Grafik;

	
	public int gameAP_Sound;

	
	public int gameAP_Technik;

	
	public bool[] gameLanguage;

	
	public bool[] gameGameplayFeatures;

	
	public int[] gamePlatform;

	
	public platformScript[] gamePlatformScript;

	
	public int[] gameEngineFeature;

	
	public bool[] gameplayFeatures_DevDone;

	
	public bool[] engineFeature_DevDone;

	
	public bool[] gameplayStudio;

	
	public bool[] grafikStudio;

	
	public bool[] soundStudio;

	
	public bool[] motionCaptureStudio;

	
	public int[] sellsPerWeek;

	
	public bool[] fanbrief;

	
	public bool[] inAppPurchase;

	
	public int releaseDate;

	
	public bool[] standard_edition;

	
	public bool[] deluxe_edition;

	
	public bool[] collectors_edition;

	
	public int[] verkaufspreis;

	
	public int[] lagerbestand;

	
	public bool autoPreis;

	
	public int vorbestellungen;

	
	public int stornierungen;

	
	public bool retailVersion;

	
	public bool digitalVersion;

	
	public int abonnements;

	
	public int abonnementsWoche;

	
	public int abosAddons;

	
	public int aboPreis;

	
	public int inAppPurchaseWeek;

	
	public int bestAbonnements;

	
	public int finanzierung_Grundkosten;

	
	public int finanzierung_Technology;

	
	public int finanzierung_Kontent;

	
	public int[] Designschwerpunkt;

	
	public int[] Designausrichtung;

	
	public int arcadeCase;

	
	public int arcadeMonitor;

	
	public int arcadeJoystick;

	
	public int arcadeSound;

	
	public int arcadeProdCosts;

	
	public bool schublade;

	
	public int schubladeTaskID = -1;

	
	public bool commercialFlop;

	
	public bool commercialHit;

	
	public bool newGenreCombination;

	
	public bool newTopicCombination;

	
	public bool npcLateinNumbers;

	
	public bool pubAngebot;

	
	public int pubAngebot_Weeks;

	
	public float pubAngebot_Verhandlung;

	
	public float pubAngebot_VerhandlungProzent = 100f;

	
	public float pubAngebot_Stimmung = 100f;

	
	public bool pubAngebot_Retail;

	
	public bool pubAngebot_Digital;

	
	public int pubAngebot_Garantiesumme;

	
	public float pubAngebot_Gewinnbeteiligung;

	
	public bool pubAnbgebot_Inivs;

	
	public bool pubAngebot_AngebotWoche;

	
	public bool auftragsspiel;

	
	public int auftragsspiel_gehalt;

	
	public int auftragsspiel_bonus;

	
	public int auftragsspiel_zeitInWochen;

	
	public int auftragsspiel_wochenAlsAngebot;

	
	public bool auftragsspiel_zeitAbgelaufen;

	
	public int auftragsspiel_mindestbewertung;

	
	public bool auftragsspiel_Inivs;

	
	public float[] merchVerkaufspreis;

	
	public int[] merchGesamtSells;

	
	public int[] merchLetzterMonat;

	
	public int[] merchDiesenMonat;

	
	public long merchGesamtGewinn;

	
	public bool merchKeinVerkauf;

	
	public int[] merchBestellungen;

	
	public float merchGesamtReviewPoints;

	
	public long tw_gewinnanteil;

	
	private Menu_Fanshop menuFanshop_;

	
	public class PublisherList
	{
		
		public PublisherList(long wert_, publisherScript s_)
		{
			this.wert = wert_;
			this.script_ = s_;
		}

		
		public long wert;

		
		public publisherScript script_;
	}
}
