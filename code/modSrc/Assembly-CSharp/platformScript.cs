using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000060 RID: 96
public class platformScript : MonoBehaviour
{
	// Token: 0x06000343 RID: 835 RVA: 0x00003C92 File Offset: 0x00001E92
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000344 RID: 836 RVA: 0x00046CD8 File Offset: 0x00044ED8
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
		if (!this.settings_)
		{
			this.settings_ = this.main_.GetComponent<settingsScript>();
		}
		if (!this.platforms_)
		{
			this.platforms_ = this.main_.GetComponent<platforms>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.gF_)
		{
			this.gF_ = this.main_.GetComponent<gameplayFeatures>();
		}
		if (!this.hardware_)
		{
			this.hardware_ = this.main_.GetComponent<hardware>();
		}
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
		if (!this.hardwareFeatures_)
		{
			this.hardwareFeatures_ = this.main_.GetComponent<hardwareFeatures>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
	}

	// Token: 0x06000345 RID: 837 RVA: 0x00003C9A File Offset: 0x00001E9A
	public void Init()
	{
		base.name = "PLATFORM_" + this.myID.ToString();
	}

	// Token: 0x06000346 RID: 838 RVA: 0x00003CB7 File Offset: 0x00001EB7
	public float GetHype()
	{
		return this.hype;
	}

	// Token: 0x06000347 RID: 839 RVA: 0x00046E14 File Offset: 0x00045014
	public string GetName()
	{
		if (this.playerConsole || (this.multiplaySlot != -1 && this.mS_.multiplayer))
		{
			return this.myName;
		}
		int language = this.settings_.language;
		string text;
		switch (language)
		{
		case 0:
			text = this.name_EN;
			goto IL_A8;
		case 1:
			text = this.name_GE;
			goto IL_A8;
		case 2:
			text = this.name_TU;
			goto IL_A8;
		case 3:
			text = this.name_CH;
			goto IL_A8;
		case 4:
			text = this.name_FR;
			goto IL_A8;
		case 5:
		case 6:
		case 7:
			break;
		case 8:
			text = this.name_HU;
			goto IL_A8;
		default:
			if (language == 16)
			{
				text = this.name_JA;
				goto IL_A8;
			}
			break;
		}
		text = this.name_EN;
		IL_A8:
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

	// Token: 0x06000348 RID: 840 RVA: 0x00046EE4 File Offset: 0x000450E4
	public string GetManufacturer()
	{
		if (this.playerConsole)
		{
			return this.mS_.companyName;
		}
		if (this.mS_.multiplayer && this.multiplaySlot != -1)
		{
			if (this.mS_.mpCalls_)
			{
				return this.mS_.mpCalls_.GetCompanyName(this.multiplaySlot);
			}
			return "";
		}
		else
		{
			int language = this.settings_.language;
			string text;
			switch (language)
			{
			case 0:
				text = this.manufacturer_EN;
				goto IL_DC;
			case 1:
				text = this.manufacturer_GE;
				goto IL_DC;
			case 2:
				text = this.manufacturer_TU;
				goto IL_DC;
			case 3:
				text = this.manufacturer_CH;
				goto IL_DC;
			case 4:
				text = this.manufacturer_FR;
				goto IL_DC;
			case 5:
			case 6:
			case 7:
				break;
			case 8:
				text = this.manufacturer_HU;
				goto IL_DC;
			default:
				if (language == 16)
				{
					text = this.manufacturer_JA;
					goto IL_DC;
				}
				break;
			}
			text = this.manufacturer_EN;
			IL_DC:
			if (text == null)
			{
				return this.manufacturer_EN;
			}
			if (text.Length <= 0)
			{
				return this.manufacturer_EN;
			}
			return text;
		}
	}

	// Token: 0x06000349 RID: 841 RVA: 0x00003CBF File Offset: 0x00001EBF
	public void GetColor(GameObject go)
	{
		if (this.playerConsole)
		{
			Material material = go.GetComponent<Image>().material;
			material.SetFloat("_HsvShift", this.conHueShift);
			material.SetFloat("_HsvSaturation", this.conSaturation);
		}
	}

	// Token: 0x0600034A RID: 842 RVA: 0x00046FE8 File Offset: 0x000451E8
	public void SetPic(GameObject go)
	{
		if (this.playerConsole || (this.multiplaySlot != -1 && this.mS_.multiplayer))
		{
			if (this.component_case != -1)
			{
				go.GetComponent<Image>().material = this.mS_.specialMaterials[6];
				go.GetComponent<Image>().sprite = this.hardware_.GetTypPic(this.component_case);
				go.GetComponent<Image>().color = new Color(this.conHueShift / 255f, this.conSaturation / 255f, 0.5f, 1f);
				return;
			}
			return;
		}
		else
		{
			if (this.pic1_file.Length > 0 && !this.pic1)
			{
				this.pic1 = this.mS_.LoadPNG(Application.dataPath + "/Extern/Icons_Platforms/" + this.pic1_file);
			}
			if (this.pic2_file.Length > 0 && !this.pic2)
			{
				this.pic2 = this.mS_.LoadPNG(Application.dataPath + "/Extern/Icons_Platforms/" + this.pic2_file);
			}
			if (!this.pic2)
			{
				go.GetComponent<Image>().material = null;
				go.GetComponent<Image>().color = Color.white;
				go.GetComponent<Image>().sprite = this.pic1;
				return;
			}
			if (this.mS_.year >= this.pic2_year)
			{
				go.GetComponent<Image>().material = null;
				go.GetComponent<Image>().color = Color.white;
				go.GetComponent<Image>().sprite = this.pic2;
				return;
			}
			go.GetComponent<Image>().material = null;
			go.GetComponent<Image>().color = Color.white;
			go.GetComponent<Image>().sprite = this.pic1;
			return;
		}
	}

	// Token: 0x0600034B RID: 843 RVA: 0x00003CF5 File Offset: 0x00001EF5
	public int GetVerkaufspreis()
	{
		return this.verkaufspreis;
	}

	// Token: 0x0600034C RID: 844 RVA: 0x000471B0 File Offset: 0x000453B0
	public int GetAktuellProductionsCosts()
	{
		float num = (float)(this.startProduktionskosten - (50 + this.anzController * 5));
		num -= num * (this.kostenreduktion * 0.01f);
		num += (float)(50 + this.anzController * 5);
		if (num < 59f)
		{
			num = 59f;
		}
		if (num > 2000f)
		{
			num = 2000f;
		}
		if (this.mS_.globalEvent == 12)
		{
			num += 100f;
		}
		return Mathf.RoundToInt(num);
	}

	// Token: 0x0600034D RID: 845 RVA: 0x0004722C File Offset: 0x0004542C
	public int CalcStartProductionsCosts()
	{
		float num = 0f;
		float num2 = 0f;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Platform");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				platformScript component = array[i].GetComponent<platformScript>();
				if (component && component.myID != this.myID && component.isUnlocked && !component.vomMarktGenommen && (component.typ == 1 || component.typ == 2))
				{
					num += 1f;
					num2 += (float)component.tech;
				}
			}
		}
		float num3 = 0f;
		for (int j = 0; j < this.hwFeatures.Length; j++)
		{
			if (this.hwFeatures[j])
			{
				num3 += 5f;
			}
		}
		float num4 = (float)this.tech;
		if (num > 0f)
		{
			num4 = num2 / num;
		}
		float num5 = (float)this.tech - num4;
		num5 += 10f;
		num5 /= 20f;
		float num6 = this.platforms_.productionCostsCurve.Evaluate(num5);
		float num7;
		switch (this.mS_.difficulty)
		{
		case 0:
			num7 = (800f + num3) * num6;
			break;
		case 1:
			num7 = (900f + num3) * num6;
			break;
		case 2:
			num7 = (1000f + num3) * num6;
			break;
		case 3:
			num7 = (1100f + num3) * num6;
			break;
		case 4:
			num7 = (1150f + num3) * num6;
			break;
		case 5:
			num7 = (1200f + num3) * num6;
			break;
		default:
			num7 = (1200f + num3) * num6;
			break;
		}
		if (this.typ == 1)
		{
			num7 += (float)(this.anzController * 15);
		}
		if (num7 < 80f || num7 > 5000f)
		{
			num7 = 80f;
		}
		return Mathf.RoundToInt(num7);
	}

	// Token: 0x0600034E RID: 846 RVA: 0x00003CFD File Offset: 0x00001EFD
	public int GetPrice()
	{
		return this.price;
	}

	// Token: 0x0600034F RID: 847 RVA: 0x00003D05 File Offset: 0x00001F05
	public int GetDevCosts()
	{
		return this.dev_costs;
	}

	// Token: 0x06000350 RID: 848 RVA: 0x00003D0D File Offset: 0x00001F0D
	public long GetGesamtAusgaben()
	{
		return this.entwicklungsKosten + (long)this.costs_mitarbeiter + (long)this.costs_marketing + this.costs_production;
	}

	// Token: 0x06000351 RID: 849 RVA: 0x00003D2C File Offset: 0x00001F2C
	public long GetEntwicklungskosten()
	{
		return this.entwicklungsKosten + (long)this.costs_mitarbeiter;
	}

	// Token: 0x06000352 RID: 850 RVA: 0x00003D3C File Offset: 0x00001F3C
	public long GetGesamtGewinn()
	{
		return this.umsatzTotal - this.GetGesamtAusgaben();
	}

	// Token: 0x06000353 RID: 851 RVA: 0x00003D4B File Offset: 0x00001F4B
	public float GetMarktanteil()
	{
		if (this.typ == 4)
		{
			return 100f;
		}
		return this.marktanteil;
	}

	// Token: 0x06000354 RID: 852 RVA: 0x00003D62 File Offset: 0x00001F62
	public float GetProzent()
	{
		return 100f / this.devPointsStart * (this.devPointsStart - this.devPoints);
	}

	// Token: 0x06000355 RID: 853 RVA: 0x0004740C File Offset: 0x0004560C
	public string GetMarktanteilString()
	{
		if (this.typ == 4)
		{
			return "-";
		}
		return string.Concat(new object[]
		{
			this.mS_.Round(this.marktanteil, 1).ToString(),
			"%|",
			this.mS_.Round((float)this.units / 1000000f * this.powerFromMarket, 1),
			" ",
			this.tS_.GetText(1483)
		});
	}

	// Token: 0x06000356 RID: 854 RVA: 0x00003D7E File Offset: 0x00001F7E
	public int GetAktiveNutzer()
	{
		return Mathf.RoundToInt((float)this.units * this.powerFromMarket);
	}

	// Token: 0x06000357 RID: 855 RVA: 0x0004749C File Offset: 0x0004569C
	public void SetMarktanteil(long gesamtUnits)
	{
		this.marktanteil = 0f;
		if (this.IsVerfuegbar() || (this.vomMarktGenommen && this.isUnlocked && this.powerFromMarket > 0f))
		{
			if (!this.vomMarktGenommen)
			{
				float num = (float)gesamtUnits;
				this.marktanteil = (float)this.units / (num / 100f);
				return;
			}
			float num2 = (float)gesamtUnits;
			float num3 = (float)this.units;
			num3 *= this.powerFromMarket;
			this.marktanteil = num3 / (num2 / 100f);
		}
	}

	// Token: 0x06000358 RID: 856 RVA: 0x00047520 File Offset: 0x00045720
	public void ZaehleGames()
	{
		this.games = 0;
		this.exklusivGames = 0;
		for (int i = 0; i < this.games_.arrayGamesScripts.Length; i++)
		{
			if (this.games_.arrayGamesScripts[i] && !this.games_.arrayGamesScripts[i].inDevelopment && !this.games_.arrayGamesScripts[i].schublade)
			{
				for (int j = 0; j < this.games_.arrayGamesScripts[i].gamePlatform.Length; j++)
				{
					if (this.games_.arrayGamesScripts[i].gamePlatform[j] == this.myID)
					{
						this.games++;
						if (this.games_.arrayGamesScripts[i].exklusiv)
						{
							this.exklusivGames++;
						}
					}
				}
			}
		}
	}

	// Token: 0x06000359 RID: 857 RVA: 0x00003D93 File Offset: 0x00001F93
	public int GetGames()
	{
		return this.games;
	}

	// Token: 0x0600035A RID: 858 RVA: 0x00003D9B File Offset: 0x00001F9B
	public int GetExklusivGames()
	{
		return this.exklusivGames;
	}

	// Token: 0x0600035B RID: 859 RVA: 0x00047604 File Offset: 0x00045804
	public string GetDateString()
	{
		if (!this.isUnlocked)
		{
			return this.tS_.GetText(528);
		}
		return this.date_year.ToString() + " " + this.tS_.GetText(this.date_month + 220);
	}

	// Token: 0x0600035C RID: 860 RVA: 0x00047658 File Offset: 0x00045858
	public string GetDateStringEnd()
	{
		if (!this.isUnlocked)
		{
			return this.tS_.GetText(528);
		}
		return this.date_year_end.ToString() + " " + this.tS_.GetText(this.date_month_end + 220);
	}

	// Token: 0x0600035D RID: 861 RVA: 0x000476AC File Offset: 0x000458AC
	public string GetTooltip()
	{
		string text = "<b><size=18>" + this.GetName() + "</size></b>";
		if (this.isUnlocked && !this.vomMarktGenommen)
		{
			text = text + "\n<b>" + this.GetDateString() + "</b>";
		}
		if (this.isUnlocked && this.vomMarktGenommen)
		{
			string text2 = this.tS_.GetText(1673);
			text2 = text2.Replace("<DATE1>", this.GetDateString());
			text2 = text2.Replace("<DATE2>", this.GetDateStringEnd());
			text = text + "\n<b>" + text2 + "</b>";
		}
		text = text + "\n<b><color=blue>" + this.GetTypString() + "</color></b>";
		text = string.Concat(new string[]
		{
			text,
			"\n<b><color=magenta>",
			this.tS_.GetText(4),
			" ",
			this.tech.ToString(),
			"</color></b>\n"
		});
		if (this.playerConsole || (this.multiplaySlot != -1 && this.mS_.multiplayer))
		{
			text = string.Concat(new string[]
			{
				text,
				"<color=green>",
				this.tS_.GetText(1612),
				": ",
				this.performancePoints.ToString(),
				" </color>\n"
			});
		}
		text = string.Concat(new string[]
		{
			text,
			"\n",
			this.tS_.GetText(216),
			": <color=blue>",
			this.GetManufacturer(),
			"</color>"
		});
		if (this.typ != 4 && this.units > 0)
		{
			text = string.Concat(new string[]
			{
				text,
				"\n",
				this.tS_.GetText(275),
				": <color=blue>",
				this.mS_.GetMoney((long)this.units, false),
				"</color>"
			});
		}
		if (this.isUnlocked)
		{
			text = string.Concat(new string[]
			{
				text,
				"\n",
				this.tS_.GetText(219),
				": <color=blue>",
				this.GetMarktanteilString(),
				"</color>"
			});
		}
		if (this.isUnlocked)
		{
			text = string.Concat(new string[]
			{
				text,
				"\n",
				this.tS_.GetText(220),
				": <color=blue>",
				this.mS_.GetMoney((long)this.GetGames(), false),
				"</color>"
			});
		}
		if (this.isUnlocked)
		{
			text = string.Concat(new string[]
			{
				text,
				"\n",
				this.tS_.GetText(1863),
				": <color=blue>",
				this.mS_.GetMoney((long)this.GetExklusivGames(), false),
				"</color>"
			});
		}
		if (this.isUnlocked && (this.playerConsole || (this.multiplaySlot != -1 && this.mS_.multiplayer)))
		{
			text = string.Concat(new string[]
			{
				text,
				"\n",
				this.tS_.GetText(1653),
				": <color=blue>",
				this.mS_.Round(this.review, 1).ToString(),
				" / 10</color>"
			});
		}
		text += "\n";
		if (this.isUnlocked)
		{
			text = string.Concat(new string[]
			{
				text,
				"\n",
				this.tS_.GetText(218),
				": <color=blue>",
				this.mS_.GetMoney((long)this.price, true),
				"</color>"
			});
		}
		if (this.isUnlocked)
		{
			text = string.Concat(new string[]
			{
				text,
				"\n",
				this.tS_.GetText(6),
				": <color=blue>",
				this.mS_.GetMoney((long)this.dev_costs, true),
				"</color>"
			});
		}
		if (this.isUnlocked)
		{
			text = string.Concat(new object[]
			{
				text,
				"\n",
				this.tS_.GetText(1926),
				": <color=blue>-",
				Mathf.RoundToInt(this.GetExklusivBonus()),
				"%</color>"
			});
		}
		if (this.playerConsole && this.isUnlocked)
		{
			text += "\n";
			text = string.Concat(new string[]
			{
				text,
				this.tS_.GetText(276),
				": <color=blue>",
				this.mS_.GetMoney(this.umsatzTotal, true),
				"</color>\n"
			});
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
		}
		text += "\n";
		if (this.internet)
		{
			text = text + "\n<b><color=magenta>" + this.tS_.GetText(1261) + "</color></b>\n";
		}
		text = text + "\n<b>" + this.tS_.GetText(1019) + "<color=blue>";
		if (this.needFeatures[0] != -1)
		{
			text = text + "\n" + this.gF_.GetName(this.needFeatures[0]);
		}
		if (this.needFeatures[1] != -1)
		{
			text = text + "\n" + this.gF_.GetName(this.needFeatures[1]);
		}
		if (this.needFeatures[2] != -1)
		{
			text = text + "\n" + this.gF_.GetName(this.needFeatures[2]);
		}
		text += "</color></b>";
		if (this.vomMarktGenommen)
		{
			text = text + "<b>\n<color=red>" + this.tS_.GetText(233) + "</color></b>";
		}
		return text;
	}

	// Token: 0x0600035E RID: 862 RVA: 0x00003DA3 File Offset: 0x00001FA3
	public bool IsVerfuegbar()
	{
		return this.isUnlocked && !this.vomMarktGenommen;
	}

	// Token: 0x0600035F RID: 863 RVA: 0x00003DB8 File Offset: 0x00001FB8
	public void Sell()
	{
		if (this.IsVerfuegbar())
		{
			this.weeksOnMarket++;
		}
		if (this.npc)
		{
			this.SellNPC();
			return;
		}
		this.SellPlayer();
	}

	// Token: 0x06000360 RID: 864 RVA: 0x00047D54 File Offset: 0x00045F54
	public void BonusSellsExklusiv(int i)
	{
		if (!this.IsVerfuegbar())
		{
			return;
		}
		if (this.npc)
		{
			this.units += i;
			this.units_max += i;
			if (this.units > 400000000)
			{
				this.units = 400000000;
			}
			if (this.units_max > 400000000)
			{
				this.units_max = 400000000;
			}
		}
	}

	// Token: 0x06000361 RID: 865 RVA: 0x00047DC0 File Offset: 0x00045FC0
	private void SellNPC()
	{
		if (!this.IsVerfuegbar())
		{
			return;
		}
		int num = this.date_year_end - this.date_year;
		if (num > 10)
		{
			num = 10;
		}
		int num2 = this.units_max / num / 48;
		if (this.weeksOnMarket <= 10)
		{
			num2 *= 5;
		}
		if (this.weeksOnMarket > 10 && this.weeksOnMarket <= 20)
		{
			num2 *= 5;
		}
		if (this.weeksOnMarket > 20 && this.weeksOnMarket <= 30)
		{
			num2 *= 4;
		}
		if (this.weeksOnMarket > 30 && this.weeksOnMarket <= 40)
		{
			num2 *= 4;
		}
		if (this.weeksOnMarket > 40 && this.weeksOnMarket <= 50)
		{
			num2 *= 3;
		}
		if (this.weeksOnMarket > 50 && this.weeksOnMarket <= 60)
		{
			num2 *= 3;
		}
		if (this.weeksOnMarket > 60 && this.weeksOnMarket <= 70)
		{
			num2 *= 2;
		}
		if (this.weeksOnMarket > 70 && this.weeksOnMarket <= 80)
		{
			num2 *= 2;
		}
		if (this.weeksOnMarket > 80 && this.weeksOnMarket <= 90)
		{
			num2 *= 2;
		}
		this.units += num2;
		if (this.units > this.units_max)
		{
			this.units = this.units_max;
		}
		if (this.units > 400000000)
		{
			this.units = 400000000;
		}
		if (this.units_max > 400000000)
		{
			this.units_max = 400000000;
		}
	}

	// Token: 0x06000362 RID: 866 RVA: 0x00047F20 File Offset: 0x00046120
	private void SellPlayer()
	{
		if (!this.playerConsole)
		{
			return;
		}
		if (this.vomMarktGenommen)
		{
			return;
		}
		if (!this.isUnlocked)
		{
			return;
		}
		for (int i = this.sellsPerWeek.Length - 1; i >= 1; i--)
		{
			this.sellsPerWeek[i] = this.sellsPerWeek[i - 1];
		}
		if (this.autoPreis)
		{
			this.verkaufspreis = this.GetAktuellProductionsCosts() + this.autoPreisGewinn;
		}
		float num = (float)(400000 + this.performancePoints * 300);
		num -= (float)(this.weeksOnMarket * 400);
		num -= (float)(this.verkaufspreis * 50);
		num += (float)UnityEngine.Random.Range(1000, 2000);
		if (num < 10000f)
		{
			num = 10000f;
		}
		num *= this.platforms_.GetSellsCurve();
		switch (this.weeksOnMarket)
		{
		case 1:
			num *= 1.3f;
			break;
		case 2:
			num *= 1.4f;
			break;
		case 3:
			num *= 1.5f;
			break;
		case 4:
			num *= 1.4f;
			break;
		case 5:
			num *= 1.1f;
			break;
		}
		float num2 = (float)this.weeksOnMarket;
		num2 *= 0.001f;
		if (num2 > 0.99f)
		{
			num2 = 0.99f;
		}
		num -= num * num2;
		int amountOfKonsolesSameTyp = this.GetAmountOfKonsolesSameTyp();
		num -= num * 0.03f * (float)amountOfKonsolesSameTyp;
		int amountOfOwnKonsolesSameTyp = this.GetAmountOfOwnKonsolesSameTyp();
		if (amountOfOwnKonsolesSameTyp > 1)
		{
			num /= (float)(amountOfOwnKonsolesSameTyp * 2);
		}
		int amountOfOwnKonsolesSameTypRemoved = this.GetAmountOfOwnKonsolesSameTypRemoved();
		if (amountOfOwnKonsolesSameTypRemoved >= 1)
		{
			num /= (float)(amountOfOwnKonsolesSameTypRemoved * 2);
		}
		float techDifference = this.GetTechDifference();
		switch (Mathf.RoundToInt(techDifference))
		{
		case -10:
			num -= num * 0.99f;
			break;
		case -9:
			num -= num * 0.99f;
			break;
		case -8:
			num -= num * 0.99f;
			break;
		case -7:
			num -= num * 0.99f;
			break;
		case -6:
			num -= num * 0.99f;
			break;
		case -5:
			num -= num * 0.98f;
			break;
		case -4:
			num -= num * 0.95f;
			break;
		case -3:
			num -= num * 0.85f;
			break;
		case -2:
			num -= num * 0.75f;
			break;
		case -1:
			num -= num * 0.5f;
			break;
		case 0:
			num *= 1f;
			break;
		case 1:
			num *= 1.5f;
			break;
		case 2:
			num *= 2f;
			break;
		case 3:
			num *= 2.5f;
			break;
		case 4:
			num *= 3f;
			break;
		case 5:
			num *= 3.5f;
			break;
		case 6:
			num *= 4f;
			break;
		case 7:
			num *= 4.5f;
			break;
		case 8:
			num *= 5f;
			break;
		case 9:
			num *= 5.5f;
			break;
		case 10:
			num *= 5f;
			break;
		}
		if (techDifference >= 0f)
		{
			num += num * (techDifference * 0.09f);
		}
		else
		{
			num += num * (techDifference * 0.09f);
		}
		if (!this.internet && this.hardwareFeatures_.hardFeat_UNLOCK[0] && this.platforms_.ExistInternetReadyConsole())
		{
			num *= 0.5f;
		}
		for (int j = 1; j < this.hwFeatures.Length; j++)
		{
			if (this.hardwareFeatures_.hardFeat_UNLOCK[j] && !this.hwFeatures[j])
			{
				num *= 1f - this.hardwareFeatures_.hardFeat_QUALITY[j] * 0.01f;
			}
		}
		if (this.typ == 1)
		{
			switch (this.anzController)
			{
			case 1:
				num += num * 0.02f;
				break;
			case 2:
				num += num * 0.04f;
				break;
			case 3:
				num += num * 0.06f;
				break;
			case 4:
				num += num * 0.08f;
				break;
			}
		}
		int num3 = 0;
		for (int k = 0; k < this.hardware_.hardware_UNLOCK.Length; k++)
		{
			if (this.typ == 1)
			{
				if (this.hardware_.hardware_TYP[k] == 8 && this.hardware_.hardware_ONLYSTATIONARY[k])
				{
					num3++;
				}
			}
			else if (this.hardware_.hardware_TYP[k] == 8 && this.hardware_.hardware_ONLYHANDHELD[k])
			{
				num3++;
			}
		}
		float num4 = (float)(num3 - this.hardware_.hardware_TECH[this.component_case]) * 0.03f;
		num -= num * num4;
		if (this.gameID != -1)
		{
			if (!this.vorinstalledGame_)
			{
				this.FindMyGame();
			}
			if (this.vorinstalledGame_)
			{
				num += num * ((float)this.vorinstalledGame_.reviewTotal * 0.0005f);
			}
		}
		num *= this.GetPriceAbzug();
		float num5 = (float)(this.verkaufspreis - this.GetAktuellProductionsCosts());
		if (num5 > 30f)
		{
			if (num5 >= 11f && num5 <= 20f)
			{
				num *= 0.95f;
			}
			if (num5 >= 21f && num5 <= 30f)
			{
				num *= 0.9f;
			}
			if (num5 >= 31f && num5 <= 40f)
			{
				num *= 0.8f;
			}
			if (num5 >= 41f && num5 <= 50f)
			{
				num *= 0.7f;
			}
			if (num5 >= 51f && num5 <= 60f)
			{
				num *= 0.6f;
			}
			if (num5 >= 61f && num5 <= 70f)
			{
				num *= 0.5f;
			}
			if (num5 >= 71f && num5 <= 80f)
			{
				num *= 0.4f;
			}
			if (num5 >= 81f && num5 <= 90f)
			{
				num *= 0.3f;
			}
			if (num5 >= 91f && num5 <= 100f)
			{
				num *= 0.2f;
			}
			if (num5 >= 101f && num5 <= 110f)
			{
				num *= 0.15f;
			}
			if (num5 >= 111f && num5 <= 120f)
			{
				num *= 0.13f;
			}
			if (num5 >= 121f && num5 <= 130f)
			{
				num *= 0.11f;
			}
			if (num5 >= 131f && num5 <= 140f)
			{
				num *= 0.09f;
			}
			if (num5 >= 141f && num5 <= 150f)
			{
				num *= 0.08f;
			}
			if (num5 >= 151f && num5 <= 160f)
			{
				num *= 0.07f;
			}
			if (num5 >= 161f && num5 <= 170f)
			{
				num *= 0.06f;
			}
			if (num5 >= 171f)
			{
				num *= 0.05f;
			}
		}
		num += num * (this.GetHype() * 0.01f);
		switch (this.mS_.GetStudioLevel(this.mS_.studioPoints))
		{
		case 0:
			num *= 0.3f;
			break;
		case 1:
			num *= 0.45f;
			break;
		case 2:
			num *= 0.5f;
			break;
		case 3:
			num *= 0.55f;
			break;
		case 4:
			num *= 0.6f;
			break;
		case 5:
			num *= 0.7f;
			break;
		case 6:
			num *= 0.8f;
			break;
		case 7:
			num *= 0.85f;
			break;
		case 8:
			num *= 0.9f;
			break;
		case 9:
			num *= 0.95f;
			break;
		}
		if (this.mS_.month == 12 || this.mS_.month == 1)
		{
			num *= 1.5f;
		}
		if (this.mS_.month == 6 || this.mS_.month == 7)
		{
			num *= 0.7f;
		}
		if (this.mS_.globalEvent == 0)
		{
			num *= 0.5f;
		}
		if (this.mS_.globalEvent == 1)
		{
			num *= 1.5f;
		}
		float num6 = (float)this.mS_.GetAchivementBonus(6);
		num6 *= 0.01f;
		num += num * num6;
		switch (this.mS_.difficulty)
		{
		case 0:
			num *= 1.8f;
			break;
		case 1:
			num *= 1.3f;
			break;
		case 2:
			num *= 0.8f;
			break;
		case 3:
			num *= 0.4f;
			break;
		case 4:
			num *= 0.3f;
			break;
		case 5:
			num *= 0.2f;
			break;
		}
		if (this.units >= 1000000000)
		{
			num = 0f;
		}
		else
		{
			for (int l = 0; l < this.games_.arrayGamesScripts.Length; l++)
			{
				if (this.games_.arrayGamesScripts[l] && (this.games_.arrayGamesScripts[l].exklusiv || this.games_.arrayGamesScripts[l].herstellerExklusiv) && this.games_.arrayGamesScripts[l].isOnMarket)
				{
					for (int m = 0; m < this.games_.arrayGamesScripts[l].gamePlatform.Length; m++)
					{
						if (this.games_.arrayGamesScripts[l].gamePlatform[m] == this.myID)
						{
							float num7 = num * 0.3f;
							if (this.games_.arrayGamesScripts[l].herstellerExklusiv)
							{
								num7 = num * 0.15f;
							}
							num7 *= this.GetPriceAbzug();
							int reviewTotal = this.games_.arrayGamesScripts[l].reviewTotal;
							float num8 = (float)Mathf.RoundToInt(UnityEngine.Random.Range((float)this.games_.arrayGamesScripts[l].sellsPerWeek[0] * (float)reviewTotal / 300f, (float)this.games_.arrayGamesScripts[l].sellsPerWeek[0] * (float)reviewTotal / 150f));
							if (reviewTotal < 75)
							{
								num8 = 0f;
							}
							if (reviewTotal > 80)
							{
								num8 = num8 * 105f / 100f;
							}
							if (reviewTotal > 85)
							{
								num8 = num8 * 110f / 100f;
							}
							if (reviewTotal > 90)
							{
								num8 = num8 * 115f / 100f;
							}
							if (reviewTotal > 93)
							{
								num8 = num8 * 120f / 100f;
							}
							if (reviewTotal > 95)
							{
								num8 = num8 * 140f / 100f;
							}
							if (reviewTotal > 97)
							{
								num8 = num8 * 160f / 100f;
							}
							if (reviewTotal > 99)
							{
								num8 = num8 * 180f / 100f;
							}
							if (num8 > (float)this.sellsPerWeek[0])
							{
								num8 = (float)this.sellsPerWeek[0];
							}
							if (num7 * 5f <= num8)
							{
								num8 = num7 * 5f;
							}
							num7 += num8;
							this.games_.arrayGamesScripts[l].exklusivKonsolenSells += (long)Mathf.RoundToInt(num7);
							num += num7;
							if (this.mS_.multiplayer)
							{
								if (this.mS_.mpCalls_.isServer)
								{
									this.mS_.mpCalls_.SERVER_Send_ExklusivKonsolenSells(this.games_.arrayGamesScripts[l], (long)Mathf.RoundToInt(num7));
								}
								if (this.mS_.mpCalls_.isClient)
								{
									this.mS_.mpCalls_.CLIENT_Send_ExklusivKonsolenSells(this.games_.arrayGamesScripts[l], (long)Mathf.RoundToInt(num7));
								}
							}
						}
					}
				}
			}
		}
		if (num < 0f)
		{
			num = 0f;
		}
		this.sellsPerWeek[0] = Mathf.RoundToInt(num);
		this.units += Mathf.RoundToInt(num);
		this.mS_.AddVerkaufsverlaufKonsolen((long)Mathf.RoundToInt(num));
		long num9 = (long)Mathf.RoundToInt(num);
		num9 *= (long)this.GetAktuellProductionsCosts();
		this.mS_.Pay(num9, 23);
		this.costs_production += num9;
		long num10 = (long)Mathf.RoundToInt(num);
		num10 *= (long)this.verkaufspreis;
		this.mS_.Earn(num10, 9);
		this.umsatzTotal += num10;
		if (this.isUnlocked)
		{
			this.AddHype(-UnityEngine.Random.Range(0.1f, 1f));
		}
		if (!this.IsOutdatet())
		{
			if (this.kostenreduktion < 100f)
			{
				this.kostenreduktion += UnityEngine.Random.Range(0.1f, 0.3f);
				if (this.kostenreduktion > 100f)
				{
					this.kostenreduktion = 100f;
				}
			}
		}
		else
		{
			this.kostenreduktion -= UnityEngine.Random.Range(0.1f, 0.3f);
			if (this.kostenreduktion < 0f)
			{
				this.kostenreduktion = 0f;
			}
		}
		if (this.konsoleTab_)
		{
			this.konsoleTab_.UpdateData();
		}
		if (this.mS_.achScript_)
		{
			if (this.units >= 10000000)
			{
				this.mS_.achScript_.SetAchivement(66);
			}
			if (this.units >= 50000000 && this.mS_.difficulty >= 5)
			{
				this.mS_.achScript_.SetAchivement(67);
			}
		}
	}

	// Token: 0x06000363 RID: 867 RVA: 0x00003DE5 File Offset: 0x00001FE5
	public bool IsOutdatet()
	{
		return this.weeksOnMarket >= 500;
	}

	// Token: 0x06000364 RID: 868 RVA: 0x00048C2C File Offset: 0x00046E2C
	private float GetPriceAbzug()
	{
		if (this.verkaufspreis <= 60)
		{
			return 2f;
		}
		if (this.verkaufspreis >= 60 && this.verkaufspreis <= 100)
		{
			return 1.5f;
		}
		if (this.verkaufspreis >= 101 && this.verkaufspreis <= 150)
		{
			return 0.9f;
		}
		if (this.verkaufspreis >= 151 && this.verkaufspreis <= 200)
		{
			return 0.8f;
		}
		if (this.verkaufspreis >= 201 && this.verkaufspreis <= 250)
		{
			return 0.7f;
		}
		if (this.verkaufspreis >= 251 && this.verkaufspreis <= 300)
		{
			return 0.6f;
		}
		if (this.verkaufspreis >= 301 && this.verkaufspreis <= 350)
		{
			return 0.5f;
		}
		if (this.verkaufspreis >= 351 && this.verkaufspreis <= 400)
		{
			return 0.4f;
		}
		if (this.verkaufspreis >= 401 && this.verkaufspreis <= 450)
		{
			return 0.35f;
		}
		if (this.verkaufspreis >= 451 && this.verkaufspreis <= 500)
		{
			return 0.3f;
		}
		if (this.verkaufspreis >= 501 && this.verkaufspreis <= 550)
		{
			return 0.26f;
		}
		if (this.verkaufspreis >= 551 && this.verkaufspreis <= 600)
		{
			return 0.24f;
		}
		if (this.verkaufspreis >= 601 && this.verkaufspreis <= 650)
		{
			return 0.22f;
		}
		if (this.verkaufspreis >= 651 && this.verkaufspreis <= 700)
		{
			return 0.2f;
		}
		if (this.verkaufspreis >= 701 && this.verkaufspreis <= 750)
		{
			return 0.19f;
		}
		if (this.verkaufspreis >= 751 && this.verkaufspreis <= 800)
		{
			return 0.18f;
		}
		if (this.verkaufspreis >= 801 && this.verkaufspreis <= 850)
		{
			return 0.17f;
		}
		if (this.verkaufspreis >= 851 && this.verkaufspreis <= 900)
		{
			return 0.16f;
		}
		if (this.verkaufspreis >= 901 && this.verkaufspreis <= 950)
		{
			return 0.15f;
		}
		if (this.verkaufspreis >= 951 && this.verkaufspreis <= 1000)
		{
			return 0.14f;
		}
		if (this.verkaufspreis >= 1001 && this.verkaufspreis <= 1050)
		{
			return 0.13f;
		}
		if (this.verkaufspreis >= 1051 && this.verkaufspreis <= 1100)
		{
			return 0.12f;
		}
		if (this.verkaufspreis >= 1101 && this.verkaufspreis <= 1150)
		{
			return 0.11f;
		}
		if (this.verkaufspreis >= 1151 && this.verkaufspreis <= 1200)
		{
			return 0.1f;
		}
		if (this.verkaufspreis >= 1201 && this.verkaufspreis <= 1250)
		{
			return 0.09f;
		}
		if (this.verkaufspreis >= 1251 && this.verkaufspreis <= 1300)
		{
			return 0.08f;
		}
		if (this.verkaufspreis >= 1301 && this.verkaufspreis <= 1350)
		{
			return 0.07f;
		}
		if (this.verkaufspreis >= 1351 && this.verkaufspreis <= 1400)
		{
			return 0.06f;
		}
		if (this.verkaufspreis >= 1401 && this.verkaufspreis <= 1450)
		{
			return 0.04f;
		}
		if (this.verkaufspreis >= 1451 && this.verkaufspreis <= 1500)
		{
			return 0.03f;
		}
		if (this.verkaufspreis >= 1501)
		{
			return 0.01f;
		}
		return 1f;
	}

	// Token: 0x06000365 RID: 869 RVA: 0x00048FF8 File Offset: 0x000471F8
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

	// Token: 0x06000366 RID: 870 RVA: 0x00003DF7 File Offset: 0x00001FF7
	public Sprite GetComplexSprite()
	{
		return this.platforms_.complexSprites[this.complex];
	}

	// Token: 0x06000367 RID: 871 RVA: 0x00003E0B File Offset: 0x0000200B
	public Sprite GetTypSprite()
	{
		return this.platforms_.typSprites[this.typ];
	}

	// Token: 0x06000368 RID: 872 RVA: 0x00049074 File Offset: 0x00047274
	public string GetTypString()
	{
		switch (this.typ)
		{
		case 0:
			return this.tS_.GetText(1479);
		case 1:
			return this.tS_.GetText(1480);
		case 2:
			return this.tS_.GetText(1481);
		case 3:
			return this.tS_.GetText(1482);
		case 4:
			return this.tS_.GetText(1513);
		default:
			return this.tS_.GetText(1479);
		}
	}

	// Token: 0x06000369 RID: 873 RVA: 0x0004910C File Offset: 0x0004730C
	public void RemoveFromMarket()
	{
		this.vomMarktGenommen = true;
		this.date_year_end = this.mS_.year;
		this.date_month_end = this.mS_.month;
		if (this.guiMain_)
		{
			this.guiMain_.CreateTopNewsPlatformRemove(this);
		}
		if (this.konsoleTab_)
		{
			UnityEngine.Object.Destroy(this.konsoleTab_.gameObject);
		}
	}

	// Token: 0x0600036A RID: 874 RVA: 0x00049178 File Offset: 0x00047378
	public void SetOnMarket()
	{
		this.FindScripts();
		this.isUnlocked = true;
		this.date_year = this.mS_.year;
		this.date_month = this.mS_.month;
		this.InitUI();
		if (this.playerConsole)
		{
			this.mS_.AddStudioPoints(Mathf.RoundToInt(5f * this.review));
			if (this.mS_.achScript_)
			{
				if (this.typ == 1)
				{
					this.mS_.achScript_.SetAchivement(24);
				}
				if (this.typ == 2)
				{
					this.mS_.achScript_.SetAchivement(25);
				}
			}
		}
		if (this.guiMain_)
		{
			this.guiMain_.CreateTopNewsPlatform(this);
		}
		if (this.mS_.multiplayer)
		{
			if (this.mS_.mpCalls_.isServer)
			{
				this.mS_.mpCalls_.SERVER_Send_Platform(this);
			}
			if (this.mS_.mpCalls_.isClient)
			{
				this.mS_.mpCalls_.CLIENT_Send_Platform(this);
			}
		}
	}

	// Token: 0x0600036B RID: 875 RVA: 0x00049294 File Offset: 0x00047494
	public void InitUI()
	{
		if (!this.playerConsole)
		{
			return;
		}
		if (this.vomMarktGenommen)
		{
			return;
		}
		if (!this.isUnlocked)
		{
			return;
		}
		this.FindScripts();
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.guiMain_.uiPrefabs[20], this.guiMain_.uiObjects[81].transform);
		this.konsoleTab_ = gameObject.GetComponent<konsoleTab>();
		this.konsoleTab_.pS_ = this;
		this.konsoleTab_.mS_ = this.mS_;
		this.konsoleTab_.main_ = this.main_;
		this.konsoleTab_.guiMain_ = this.guiMain_;
		this.konsoleTab_.tS_ = this.tS_;
		this.konsoleTab_.Init(this.myID);
	}

	// Token: 0x0600036C RID: 876 RVA: 0x00049358 File Offset: 0x00047558
	public float GetTechDifference()
	{
		float num = 0f;
		float num2 = 0f;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Platform");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				platformScript component = array[i].GetComponent<platformScript>();
				if (component && component.myID != this.myID && component.isUnlocked && !component.vomMarktGenommen && component.typ == this.typ)
				{
					num += 1f;
					num2 += (float)component.tech;
				}
			}
		}
		if (num == 0f)
		{
			return 0f;
		}
		float num3 = num2 / num;
		return (float)this.tech - num3;
	}

	// Token: 0x0600036D RID: 877 RVA: 0x0004940C File Offset: 0x0004760C
	public int GetAmountOfOwnKonsolesSameTyp()
	{
		int num = 0;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Platform");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				platformScript component = array[i].GetComponent<platformScript>();
				if (component && component.playerConsole && component.isUnlocked && !component.vomMarktGenommen && component.typ == this.typ && component.tech >= this.tech)
				{
					num++;
				}
			}
		}
		return num;
	}

	// Token: 0x0600036E RID: 878 RVA: 0x00049488 File Offset: 0x00047688
	public int GetAmountOfOwnKonsolesSameTypRemoved()
	{
		int num = 0;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Platform");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				platformScript component = array[i].GetComponent<platformScript>();
				if (component && component.playerConsole && component.isUnlocked && component.typ == this.typ && component.vomMarktGenommen && component.tech > this.tech)
				{
					num++;
				}
			}
		}
		return num;
	}

	// Token: 0x0600036F RID: 879 RVA: 0x00049504 File Offset: 0x00047704
	public int GetAmountOfKonsolesSameTyp()
	{
		int num = 0;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Platform");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				platformScript component = array[i].GetComponent<platformScript>();
				if (component && component.isUnlocked && !component.vomMarktGenommen && component.typ == this.typ)
				{
					num++;
				}
			}
		}
		return num;
	}

	// Token: 0x06000370 RID: 880 RVA: 0x0004956C File Offset: 0x0004776C
	public void FindMyGame()
	{
		if (this.gameID == -1)
		{
			return;
		}
		if (this.vorinstalledGame_)
		{
			return;
		}
		if (!this.vorinstalledGame_)
		{
			GameObject gameObject = GameObject.Find("GAME_" + this.gameID.ToString());
			if (gameObject)
			{
				this.vorinstalledGame_ = gameObject.GetComponent<gameScript>();
				if (!this.vorinstalledGame_)
				{
					this.gameID = -1;
				}
			}
		}
	}

	// Token: 0x06000371 RID: 881 RVA: 0x000495E4 File Offset: 0x000477E4
	public void SellPlayerKonsoleToNPC(publisherScript pS_)
	{
		if (!pS_)
		{
			return;
		}
		if (this.publisherBuyed.Length == 0)
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag("Publisher");
			this.publisherBuyed = new bool[array.Length];
		}
		if (!this.publisherBuyed[pS_.myID])
		{
			this.publisherBuyed[pS_.myID] = true;
			if (this.playerConsole)
			{
				this.mS_.Earn((long)this.price, 10);
				string text = this.tS_.GetText(1659);
				text = text.Replace("<NAME1>", pS_.GetName());
				text = text.Replace("<NAME2>", this.GetName());
				text = text + "\n<color=green><b>+" + this.mS_.GetMoney((long)this.price, true) + "</b></color>";
				this.guiMain_.CreateTopNewsInfo(text);
			}
			if (this.mS_.multiplayer && !this.playerConsole && this.multiplaySlot != -1)
			{
				this.mS_.mpCalls_.SERVER_Send_Payment(this.mS_.mpCalls_.myID, this.multiplaySlot, 4, this.price);
			}
		}
	}

	// Token: 0x06000372 RID: 882 RVA: 0x00049708 File Offset: 0x00047908
	public bool GetPlattformEnd()
	{
		int num = Mathf.RoundToInt(this.GetMarktanteil());
		int num2 = num / 12;
		num -= num2 * 12;
		int num3 = this.date_year_end + num2;
		int num4 = this.date_month_end + num;
		if (num4 > 12)
		{
			num4 = 1;
			num3++;
		}
		if (this.mS_.year >= num3)
		{
			if (this.mS_.month >= num4)
			{
				return true;
			}
			if (this.mS_.year > num3)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000373 RID: 883 RVA: 0x0004977C File Offset: 0x0004797C
	public float GetExklusivBonus()
	{
		float result = 0f;
		int num = Mathf.RoundToInt(this.GetMarktanteil());
		if (num <= 3)
		{
			result = 75f;
		}
		if (num > 3 && num <= 5)
		{
			result = 50f;
		}
		if (num > 5 && num <= 10)
		{
			result = 45f;
		}
		if (num > 10 && num <= 15)
		{
			result = 40f;
		}
		if (num > 15 && num <= 20)
		{
			result = 35f;
		}
		if (num > 20 && num <= 25)
		{
			result = 30f;
		}
		if (num > 25 && num <= 30)
		{
			result = 25f;
		}
		if (num > 30 && num <= 35)
		{
			result = 20f;
		}
		if (num > 35 && num <= 40)
		{
			result = 15f;
		}
		if (num > 40 && num <= 45)
		{
			result = 10f;
		}
		if (num > 45 && num <= 50)
		{
			result = 5f;
		}
		if (num > 50)
		{
			result = 0f;
		}
		if (this.playerConsole || (this.multiplaySlot != -1 && this.mS_.multiplayer))
		{
			result = 0f;
		}
		return result;
	}

	// Token: 0x04000671 RID: 1649
	public GameObject main_;

	// Token: 0x04000672 RID: 1650
	public mainScript mS_;

	// Token: 0x04000673 RID: 1651
	public settingsScript settings_;

	// Token: 0x04000674 RID: 1652
	public textScript tS_;

	// Token: 0x04000675 RID: 1653
	public gameplayFeatures gF_;

	// Token: 0x04000676 RID: 1654
	public platforms platforms_;

	// Token: 0x04000677 RID: 1655
	public hardware hardware_;

	// Token: 0x04000678 RID: 1656
	public hardwareFeatures hardwareFeatures_;

	// Token: 0x04000679 RID: 1657
	public GUI_Main guiMain_;

	// Token: 0x0400067A RID: 1658
	public games games_;

	// Token: 0x0400067B RID: 1659
	public konsoleTab konsoleTab_;

	// Token: 0x0400067C RID: 1660
	public int myID;

	// Token: 0x0400067D RID: 1661
	public int date_year;

	// Token: 0x0400067E RID: 1662
	public int date_month;

	// Token: 0x0400067F RID: 1663
	public int date_year_end;

	// Token: 0x04000680 RID: 1664
	public int date_month_end;

	// Token: 0x04000681 RID: 1665
	public bool npc;

	// Token: 0x04000682 RID: 1666
	public int price;

	// Token: 0x04000683 RID: 1667
	public int dev_costs;

	// Token: 0x04000684 RID: 1668
	public int tech;

	// Token: 0x04000685 RID: 1669
	public int typ;

	// Token: 0x04000686 RID: 1670
	public float marktanteil;

	// Token: 0x04000687 RID: 1671
	public int[] needFeatures;

	// Token: 0x04000688 RID: 1672
	public int units;

	// Token: 0x04000689 RID: 1673
	public int units_max;

	// Token: 0x0400068A RID: 1674
	public string name_EN = "";

	// Token: 0x0400068B RID: 1675
	public string name_GE = "";

	// Token: 0x0400068C RID: 1676
	public string name_TU = "";

	// Token: 0x0400068D RID: 1677
	public string name_CH = "";

	// Token: 0x0400068E RID: 1678
	public string name_FR = "";

	// Token: 0x0400068F RID: 1679
	public string name_HU = "";

	// Token: 0x04000690 RID: 1680
	public string name_JA = "";

	// Token: 0x04000691 RID: 1681
	public string manufacturer_EN = "";

	// Token: 0x04000692 RID: 1682
	public string manufacturer_GE = "";

	// Token: 0x04000693 RID: 1683
	public string manufacturer_TU = "";

	// Token: 0x04000694 RID: 1684
	public string manufacturer_CH = "";

	// Token: 0x04000695 RID: 1685
	public string manufacturer_FR = "";

	// Token: 0x04000696 RID: 1686
	public string manufacturer_HU = "";

	// Token: 0x04000697 RID: 1687
	public string manufacturer_JA = "";

	// Token: 0x04000698 RID: 1688
	private Sprite pic1;

	// Token: 0x04000699 RID: 1689
	private Sprite pic2;

	// Token: 0x0400069A RID: 1690
	public string pic1_file = "";

	// Token: 0x0400069B RID: 1691
	public string pic2_file = "";

	// Token: 0x0400069C RID: 1692
	public int pic2_year;

	// Token: 0x0400069D RID: 1693
	public int games;

	// Token: 0x0400069E RID: 1694
	public int exklusivGames;

	// Token: 0x0400069F RID: 1695
	public int erfahrung;

	// Token: 0x040006A0 RID: 1696
	public bool isUnlocked;

	// Token: 0x040006A1 RID: 1697
	public bool inBesitz;

	// Token: 0x040006A2 RID: 1698
	public bool vomMarktGenommen;

	// Token: 0x040006A3 RID: 1699
	public int complex;

	// Token: 0x040006A4 RID: 1700
	public bool internet;

	// Token: 0x040006A5 RID: 1701
	public float powerFromMarket = 1f;

	// Token: 0x040006A6 RID: 1702
	public string myName;

	// Token: 0x040006A7 RID: 1703
	public bool playerConsole;

	// Token: 0x040006A8 RID: 1704
	public int multiplaySlot = -1;

	// Token: 0x040006A9 RID: 1705
	public int gameID = -1;

	// Token: 0x040006AA RID: 1706
	public gameScript vorinstalledGame_;

	// Token: 0x040006AB RID: 1707
	public int anzController;

	// Token: 0x040006AC RID: 1708
	public float conHueShift;

	// Token: 0x040006AD RID: 1709
	public float conSaturation;

	// Token: 0x040006AE RID: 1710
	public int component_cpu = -1;

	// Token: 0x040006AF RID: 1711
	public int component_gfx = -1;

	// Token: 0x040006B0 RID: 1712
	public int component_ram = -1;

	// Token: 0x040006B1 RID: 1713
	public int component_hdd = -1;

	// Token: 0x040006B2 RID: 1714
	public int component_sfx = -1;

	// Token: 0x040006B3 RID: 1715
	public int component_cooling = -1;

	// Token: 0x040006B4 RID: 1716
	public int component_disc = -1;

	// Token: 0x040006B5 RID: 1717
	public int component_controller = -1;

	// Token: 0x040006B6 RID: 1718
	public int component_case = -1;

	// Token: 0x040006B7 RID: 1719
	public int component_monitor = -1;

	// Token: 0x040006B8 RID: 1720
	public bool[] hwFeatures;

	// Token: 0x040006B9 RID: 1721
	public float devPoints;

	// Token: 0x040006BA RID: 1722
	public float devPointsStart;

	// Token: 0x040006BB RID: 1723
	public long entwicklungsKosten;

	// Token: 0x040006BC RID: 1724
	public long einnahmen;

	// Token: 0x040006BD RID: 1725
	public float hype;

	// Token: 0x040006BE RID: 1726
	public int costs_marketing;

	// Token: 0x040006BF RID: 1727
	public int costs_mitarbeiter;

	// Token: 0x040006C0 RID: 1728
	public int startProduktionskosten;

	// Token: 0x040006C1 RID: 1729
	public int verkaufspreis = 399;

	// Token: 0x040006C2 RID: 1730
	public float kostenreduktion;

	// Token: 0x040006C3 RID: 1731
	public bool autoPreis;

	// Token: 0x040006C4 RID: 1732
	public bool thridPartyGames;

	// Token: 0x040006C5 RID: 1733
	public long umsatzTotal;

	// Token: 0x040006C6 RID: 1734
	public long costs_production;

	// Token: 0x040006C7 RID: 1735
	public int[] sellsPerWeek;

	// Token: 0x040006C8 RID: 1736
	public int weeksOnMarket;

	// Token: 0x040006C9 RID: 1737
	public float review;

	// Token: 0x040006CA RID: 1738
	public bool[] publisherBuyed;

	// Token: 0x040006CB RID: 1739
	public int performancePoints;

	// Token: 0x040006CC RID: 1740
	public int autoPreisGewinn;

	// Token: 0x040006CD RID: 1741
	public int weeksInDevelopment;
}
