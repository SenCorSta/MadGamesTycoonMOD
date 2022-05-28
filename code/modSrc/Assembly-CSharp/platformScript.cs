using System;
using UnityEngine;
using UnityEngine.UI;


public class platformScript : MonoBehaviour
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

	
	public void Init()
	{
		base.name = "PLATFORM_" + this.myID.ToString();
	}

	
	public float GetHype()
	{
		return this.hype;
	}

	
	public string GetName()
	{
		if (!this.OwnerIsNPC())
		{
			return this.myName;
		}
		int language = this.settings_.language;
		string text;
		switch (language)
		{
		case 0:
			text = this.name_EN;
			goto IL_98;
		case 1:
			text = this.name_GE;
			goto IL_98;
		case 2:
			text = this.name_TU;
			goto IL_98;
		case 3:
			text = this.name_CH;
			goto IL_98;
		case 4:
			text = this.name_FR;
			goto IL_98;
		case 5:
		case 6:
		case 7:
			break;
		case 8:
			text = this.name_HU;
			goto IL_98;
		default:
			if (language == 16)
			{
				text = this.name_JA;
				goto IL_98;
			}
			break;
		}
		text = this.name_EN;
		IL_98:
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

	
	public string GetManufacturer()
	{
		if (this.ownerID == this.mS_.myID)
		{
			return this.mS_.GetCompanyName();
		}
		if (this.mS_.multiplayer)
		{
			if (!this.OwnerIsNPC() && this.mS_.mpCalls_)
			{
				return this.mS_.mpCalls_.GetCompanyName(this.ownerID);
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
				goto IL_EC;
			case 1:
				text = this.manufacturer_GE;
				goto IL_EC;
			case 2:
				text = this.manufacturer_TU;
				goto IL_EC;
			case 3:
				text = this.manufacturer_CH;
				goto IL_EC;
			case 4:
				text = this.manufacturer_FR;
				goto IL_EC;
			case 5:
			case 6:
			case 7:
				break;
			case 8:
				text = this.manufacturer_HU;
				goto IL_EC;
			default:
				if (language == 16)
				{
					text = this.manufacturer_JA;
					goto IL_EC;
				}
				break;
			}
			text = this.manufacturer_EN;
			IL_EC:
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

	
	public void GetColor(GameObject go)
	{
		if (this.ownerID == this.mS_.myID)
		{
			Material material = go.GetComponent<Image>().material;
			material.SetFloat("_HsvShift", this.conHueShift);
			material.SetFloat("_HsvSaturation", this.conSaturation);
		}
	}

	
	public void SetPic(GameObject go)
	{
		if (!this.OwnerIsNPC())
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

	
	public int GetVerkaufspreis()
	{
		return this.verkaufspreis;
	}

	
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

	
	public int GetPrice()
	{
		return this.price;
	}

	
	public int GetDevCosts()
	{
		return this.dev_costs;
	}

	
	public long GetGesamtAusgaben()
	{
		return this.entwicklungsKosten + (long)this.costs_mitarbeiter + (long)this.costs_marketing + this.costs_production;
	}

	
	public long GetEntwicklungskosten()
	{
		return this.entwicklungsKosten + (long)this.costs_mitarbeiter;
	}

	
	public long GetGesamtGewinn()
	{
		return this.umsatzTotal - this.GetGesamtAusgaben();
	}

	
	public float GetMarktanteil()
	{
		if (this.typ == 4)
		{
			return 100f;
		}
		return this.marktanteil;
	}

	
	public float GetProzent()
	{
		return 100f / this.devPointsStart * (this.devPointsStart - this.devPoints);
	}

	
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

	
	public int GetAktiveNutzer()
	{
		return Mathf.RoundToInt((float)this.units * this.powerFromMarket);
	}

	
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

	
	public int GetGames()
	{
		return this.games;
	}

	
	public int GetExklusivGames()
	{
		return this.exklusivGames;
	}

	
	public string GetDateString()
	{
		if (!this.isUnlocked)
		{
			return this.tS_.GetText(528);
		}
		return this.date_year.ToString() + " " + this.tS_.GetText(this.date_month + 220);
	}

	
	public string GetDateStringEnd()
	{
		if (!this.isUnlocked)
		{
			return this.tS_.GetText(528);
		}
		return this.date_year_end.ToString() + " " + this.tS_.GetText(this.date_month_end + 220);
	}

	
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
		if (!this.OwnerIsNPC())
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
		if (this.isUnlocked && !this.OwnerIsNPC())
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
		if (this.ownerID == this.mS_.myID && this.isUnlocked)
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

	
	public bool IsVerfuegbar()
	{
		return this.isUnlocked && !this.vomMarktGenommen;
	}

	
	public void Sell()
	{
		if (this.IsVerfuegbar())
		{
			this.weeksOnMarket++;
		}
		if (this.OwnerIsNPC())
		{
			this.SellNPC();
			return;
		}
		this.SellPlayer();
	}

	
	public void BonusSellsExklusiv(int i)
	{
		if (!this.IsVerfuegbar())
		{
			return;
		}
		if (this.OwnerIsNPC())
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

	
	private void SellPlayer()
	{
		if (this.ownerID != this.mS_.myID)
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
							float num8 = (float)Mathf.RoundToInt(UnityEngine.Random.Range((float)this.games_.arrayGamesScripts[l].sellsPerWeek[0] * 0.2f, (float)this.games_.arrayGamesScripts[l].sellsPerWeek[0] * 0.3f));
							num8 = num8 / 100f * (130f - this.GetMarktanteil());
							if (num8 > (float)this.sellsPerWeek[0])
							{
								num8 = (float)this.sellsPerWeek[0];
							}
							if (num7 >= num8)
							{
								this.games_.arrayGamesScripts[l].exklusivKonsolenSells += (long)Mathf.RoundToInt(num8);
								num += num8;
								if (this.mS_.multiplayer)
								{
									if (this.mS_.mpCalls_.isServer)
									{
										this.mS_.mpCalls_.SERVER_Send_ExklusivKonsolenSells(this.games_.arrayGamesScripts[l], (long)Mathf.RoundToInt(num8));
									}
									if (this.mS_.mpCalls_.isClient)
									{
										this.mS_.mpCalls_.CLIENT_Send_ExklusivKonsolenSells(this.games_.arrayGamesScripts[l], (long)Mathf.RoundToInt(num8));
									}
								}
							}
							else
							{
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

	
	public bool IsOutdatet()
	{
		return this.weeksOnMarket >= 500;
	}

	
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

	
	public Sprite GetComplexSprite()
	{
		return this.platforms_.complexSprites[this.complex];
	}

	
	public Sprite GetTypSprite()
	{
		return this.platforms_.typSprites[this.typ];
	}

	
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

	
	public void SetOnMarket()
	{
		this.FindScripts();
		this.isUnlocked = true;
		this.date_year = this.mS_.year;
		this.date_month = this.mS_.month;
		this.InitUI();
		if (this.ownerID == this.mS_.myID)
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

	
	public void InitUI()
	{
		if (this.ownerID != this.mS_.myID)
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

	
	public int GetAmountOfOwnKonsolesSameTyp()
	{
		int num = 0;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Platform");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				platformScript component = array[i].GetComponent<platformScript>();
				if (component && component.ownerID == this.mS_.myID && component.isUnlocked && !component.vomMarktGenommen && component.typ == this.typ && component.tech >= this.tech)
				{
					num++;
				}
			}
		}
		return num;
	}

	
	public int GetAmountOfOwnKonsolesSameTypRemoved()
	{
		int num = 0;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Platform");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				platformScript component = array[i].GetComponent<platformScript>();
				if (component && component.ownerID == this.mS_.myID && component.isUnlocked && component.typ == this.typ && component.vomMarktGenommen && component.tech > this.tech)
				{
					num++;
				}
			}
		}
		return num;
	}

	
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
			if (this.ownerID == this.mS_.myID)
			{
				this.mS_.Earn((long)this.price, 10);
				string text = this.tS_.GetText(1659);
				text = text.Replace("<NAME1>", pS_.GetName());
				text = text.Replace("<NAME2>", this.GetName());
				text = text + "\n<color=green><b>+" + this.mS_.GetMoney((long)this.price, true) + "</b></color>";
				this.guiMain_.CreateTopNewsInfo(text);
			}
			if (this.mS_.multiplayer && this.PlatformFromMitspieler())
			{
				this.mS_.mpCalls_.SERVER_Send_Payment(this.mS_.myID, this.ownerID, 4, this.price);
			}
		}
	}

	
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
		if (!this.OwnerIsNPC())
		{
			result = 0f;
		}
		return result;
	}

	
	public bool OwnerIsNPC()
	{
		return this.ownerID < 100000;
	}

	
	public bool PlatformFromMitspieler()
	{
		return this.mS_.multiplayer && this.ownerID >= 100000 && this.ownerID != this.mS_.myID && this.ownerID >= 100000;
	}

	
	public GameObject main_;

	
	public mainScript mS_;

	
	public settingsScript settings_;

	
	public textScript tS_;

	
	public gameplayFeatures gF_;

	
	public platforms platforms_;

	
	public hardware hardware_;

	
	public hardwareFeatures hardwareFeatures_;

	
	public GUI_Main guiMain_;

	
	public games games_;

	
	public konsoleTab konsoleTab_;

	
	public int myID;

	
	public int ownerID = -1;

	
	public int date_year;

	
	public int date_month;

	
	public int date_year_end;

	
	public int date_month_end;

	
	public int price;

	
	public int dev_costs;

	
	public int tech;

	
	public int typ;

	
	public float marktanteil;

	
	public int[] needFeatures;

	
	public int units;

	
	public int units_max;

	
	public string name_EN = "";

	
	public string name_GE = "";

	
	public string name_TU = "";

	
	public string name_CH = "";

	
	public string name_FR = "";

	
	public string name_HU = "";

	
	public string name_JA = "";

	
	public string manufacturer_EN = "";

	
	public string manufacturer_GE = "";

	
	public string manufacturer_TU = "";

	
	public string manufacturer_CH = "";

	
	public string manufacturer_FR = "";

	
	public string manufacturer_HU = "";

	
	public string manufacturer_JA = "";

	
	private Sprite pic1;

	
	private Sprite pic2;

	
	public string pic1_file = "";

	
	public string pic2_file = "";

	
	public int pic2_year;

	
	public int games;

	
	public int exklusivGames;

	
	public int erfahrung;

	
	public bool isUnlocked;

	
	public bool inBesitz;

	
	public bool vomMarktGenommen;

	
	public int complex;

	
	public bool internet;

	
	public float powerFromMarket = 1f;

	
	public string myName;

	
	public int gameID = -1;

	
	public gameScript vorinstalledGame_;

	
	public int anzController;

	
	public float conHueShift;

	
	public float conSaturation;

	
	public int component_cpu = -1;

	
	public int component_gfx = -1;

	
	public int component_ram = -1;

	
	public int component_hdd = -1;

	
	public int component_sfx = -1;

	
	public int component_cooling = -1;

	
	public int component_disc = -1;

	
	public int component_controller = -1;

	
	public int component_case = -1;

	
	public int component_monitor = -1;

	
	public bool[] hwFeatures;

	
	public float devPoints;

	
	public float devPointsStart;

	
	public long entwicklungsKosten;

	
	public long einnahmen;

	
	public float hype;

	
	public int costs_marketing;

	
	public int costs_mitarbeiter;

	
	public int startProduktionskosten;

	
	public int verkaufspreis = 399;

	
	public float kostenreduktion;

	
	public bool autoPreis;

	
	public bool thridPartyGames;

	
	public long umsatzTotal;

	
	public long costs_production;

	
	public int[] sellsPerWeek;

	
	public int weeksOnMarket;

	
	public float review;

	
	public bool[] publisherBuyed;

	
	public int performancePoints;

	
	public int autoPreisGewinn;

	
	public int weeksInDevelopment;
}
