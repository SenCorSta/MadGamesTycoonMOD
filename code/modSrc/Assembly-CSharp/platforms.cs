using System;
using System.IO;
using System.Text;
using UnityEngine;


public class platforms : MonoBehaviour
{
	
	private void Awake()
	{
		this.FindScripts();
	}

	
	private void FindScripts()
	{
		if (!this.mS_)
		{
			this.mS_ = base.GetComponent<mainScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = base.GetComponent<textScript>();
		}
		if (!this.gF_)
		{
			this.gF_ = base.GetComponent<gameplayFeatures>();
		}
		if (!this.hardware_)
		{
			this.hardware_ = base.GetComponent<hardware>();
		}
		if (!this.hardwareFeatures_)
		{
			this.hardwareFeatures_ = base.GetComponent<hardwareFeatures>();
		}
		if (!this.games_)
		{
			this.games_ = base.GetComponent<games>();
		}
		if (!this.settings_)
		{
			this.settings_ = base.GetComponent<settingsScript>();
		}
		if (!this.mpCalls_)
		{
			this.mpCalls_ = GameObject.Find("NetworkManager").GetComponent<mpCalls>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
	}

	
	public platformScript CreatePlatform()
	{
		platformScript component = UnityEngine.Object.Instantiate<GameObject>(this.prefabPlatform).GetComponent<platformScript>();
		component.main_ = base.gameObject;
		component.mS_ = this.mS_;
		component.tS_ = this.tS_;
		component.settings_ = this.settings_;
		component.gF_ = this.gF_;
		component.platforms_ = this;
		component.hardware_ = this.hardware_;
		component.hardwareFeatures_ = this.hardwareFeatures_;
		component.guiMain_ = this.guiMain_;
		component.games_ = this.games_;
		component.hwFeatures = new bool[this.hardwareFeatures_.hardFeat_UNLOCK.Length];
		return component;
	}

	
	public void LoadPlatformDataForOldSavegames(string filename)
	{
		StreamReader streamReader = new StreamReader(Application.dataPath + "/Extern/Text/" + filename, Encoding.Unicode);
		string text = streamReader.ReadToEnd();
		streamReader.Close();
		this.data = text.Split(new char[]
		{
			"\n"[0]
		});
		platformScript platformScript = null;
		for (int i = 0; i < this.data.Length; i++)
		{
			if (this.ParseData("[ID]", i))
			{
				GameObject gameObject = GameObject.Find("PLATFORM_" + int.Parse(this.data[i]));
				if (gameObject)
				{
					platformScript = gameObject.GetComponent<platformScript>();
				}
			}
			if (platformScript)
			{
				if (this.ParseData("[TYP]", i))
				{
					platformScript.typ = int.Parse(this.data[i]);
				}
				this.ParseData("[END]", i);
				if (this.ParseData("[EOF]", i))
				{
					Debug.Log("Platforms.txt -> EOF");
					return;
				}
			}
		}
	}

	
	public void LoadPlatforms(string filename)
	{
		StreamReader streamReader = new StreamReader(Application.dataPath + "/Extern/Text/" + filename, Encoding.Unicode);
		string text = streamReader.ReadToEnd();
		streamReader.Close();
		this.data = text.Split(new char[]
		{
			"\n"[0]
		});
		int num = 0;
		for (int i = 0; i < this.data.Length; i++)
		{
			if (this.data[i].Contains("[ID]"))
			{
				num++;
			}
		}
		platformScript platformScript = null;
		for (int j = 0; j < this.data.Length; j++)
		{
			if (this.ParseData("[ID]", j))
			{
				platformScript = this.CreatePlatform();
				platformScript.myID = int.Parse(this.data[j]);
				platformScript.ownerID = -1;
				platformScript.date_year_end = 999999;
				platformScript.date_month_end = 1;
				platformScript.pic2_year = 999999;
				platformScript.Init();
			}
			if (platformScript)
			{
				if (this.ParseData("[PRICE]", j))
				{
					platformScript.price = int.Parse(this.data[j]);
				}
				if (this.ParseData("[DEV COSTS]", j))
				{
					platformScript.dev_costs = int.Parse(this.data[j]);
				}
				if (this.ParseData("[TECHLEVEL]", j))
				{
					platformScript.tech = int.Parse(this.data[j]);
				}
				if (this.ParseData("[TYP]", j))
				{
					platformScript.typ = int.Parse(this.data[j]);
				}
				if (this.ParseData("[UNITS]", j))
				{
					platformScript.units_max = int.Parse(this.data[j]);
					if (platformScript.units_max > 500000000)
					{
						platformScript.units_max = 500000000;
					}
					platformScript.units_max += UnityEngine.Random.Range(0, platformScript.units_max / 20);
				}
				if (this.ParseData("[NEED-1]", j))
				{
					platformScript.needFeatures[0] = int.Parse(this.data[j]);
				}
				if (this.ParseData("[NEED-2]", j))
				{
					platformScript.needFeatures[1] = int.Parse(this.data[j]);
				}
				if (this.ParseData("[NEED-3]", j))
				{
					platformScript.needFeatures[2] = int.Parse(this.data[j]);
				}
				if (this.ParseData("[DATE]", j))
				{
					if (this.ParseDataDontCutLastChar("JAN", j))
					{
						platformScript.date_month = 1;
					}
					if (this.ParseDataDontCutLastChar("FEB", j))
					{
						platformScript.date_month = 2;
					}
					if (this.ParseDataDontCutLastChar("MAR", j))
					{
						platformScript.date_month = 3;
					}
					if (this.ParseDataDontCutLastChar("APR", j))
					{
						platformScript.date_month = 4;
					}
					if (this.ParseDataDontCutLastChar("MAY", j))
					{
						platformScript.date_month = 5;
					}
					if (this.ParseDataDontCutLastChar("JUN", j))
					{
						platformScript.date_month = 6;
					}
					if (this.ParseDataDontCutLastChar("JUL", j))
					{
						platformScript.date_month = 7;
					}
					if (this.ParseDataDontCutLastChar("AUG", j))
					{
						platformScript.date_month = 8;
					}
					if (this.ParseDataDontCutLastChar("SEP", j))
					{
						platformScript.date_month = 9;
					}
					if (this.ParseDataDontCutLastChar("OCT", j))
					{
						platformScript.date_month = 10;
					}
					if (this.ParseDataDontCutLastChar("NOV", j))
					{
						platformScript.date_month = 11;
					}
					if (this.ParseDataDontCutLastChar("DEC", j))
					{
						platformScript.date_month = 12;
					}
					if (platformScript.date_month <= 0)
					{
						platformScript.date_month = 1;
						Debug.Log("ERROR: Platform.txt -> Incorrect Month: " + platformScript.myID.ToString());
					}
					platformScript.date_year = int.Parse(this.data[j]);
				}
				if (this.ParseData("[DATE END]", j))
				{
					if (this.ParseDataDontCutLastChar("JAN", j))
					{
						platformScript.date_month_end = 1;
					}
					if (this.ParseDataDontCutLastChar("FEB", j))
					{
						platformScript.date_month_end = 2;
					}
					if (this.ParseDataDontCutLastChar("MAR", j))
					{
						platformScript.date_month_end = 3;
					}
					if (this.ParseDataDontCutLastChar("APR", j))
					{
						platformScript.date_month_end = 4;
					}
					if (this.ParseDataDontCutLastChar("MAY", j))
					{
						platformScript.date_month_end = 5;
					}
					if (this.ParseDataDontCutLastChar("JUN", j))
					{
						platformScript.date_month_end = 6;
					}
					if (this.ParseDataDontCutLastChar("JUL", j))
					{
						platformScript.date_month_end = 7;
					}
					if (this.ParseDataDontCutLastChar("AUG", j))
					{
						platformScript.date_month_end = 8;
					}
					if (this.ParseDataDontCutLastChar("SEP", j))
					{
						platformScript.date_month_end = 9;
					}
					if (this.ParseDataDontCutLastChar("OCT", j))
					{
						platformScript.date_month_end = 10;
					}
					if (this.ParseDataDontCutLastChar("NOV", j))
					{
						platformScript.date_month_end = 11;
					}
					if (this.ParseDataDontCutLastChar("DEC", j))
					{
						platformScript.date_month_end = 12;
					}
					if (platformScript.date_month_end <= 0)
					{
						platformScript.date_month_end = 1;
						Debug.Log("ERROR: Platform.txt -> Incorrect Month (END): " + platformScript.myID.ToString());
					}
					platformScript.date_year_end = int.Parse(this.data[j]);
				}
				if (this.ParseData("[PIC-1]", j))
				{
					platformScript.pic1_file = this.data[j];
				}
				if (this.ParseData("[PIC-2]", j))
				{
					platformScript.pic2_file = this.data[j];
				}
				if (this.ParseData("[PIC-2 YEAR]", j))
				{
					platformScript.pic2_year = int.Parse(this.data[j]);
				}
				if (this.ParseData("[NAME GE]", j))
				{
					platformScript.name_GE = this.data[j];
				}
				if (this.ParseData("[NAME EN]", j))
				{
					platformScript.name_EN = this.data[j];
				}
				if (this.ParseData("[NAME TU]", j))
				{
					platformScript.name_TU = this.data[j];
				}
				if (this.ParseData("[NAME CH]", j))
				{
					platformScript.name_CH = this.data[j];
				}
				if (this.ParseData("[NAME FR]", j))
				{
					platformScript.name_FR = this.data[j];
				}
				if (this.ParseData("[NAME HU]", j))
				{
					platformScript.name_HU = this.data[j];
				}
				if (this.ParseData("[NAME JA]", j))
				{
					platformScript.name_JA = this.data[j];
				}
				if (this.ParseData("[MANUFACTURER GE]", j))
				{
					platformScript.manufacturer_GE = this.data[j];
				}
				if (this.ParseData("[MANUFACTURER EN]", j))
				{
					platformScript.manufacturer_EN = this.data[j];
				}
				if (this.ParseData("[MANUFACTURER TU]", j))
				{
					platformScript.manufacturer_TU = this.data[j];
				}
				if (this.ParseData("[MANUFACTURER CH]", j))
				{
					platformScript.manufacturer_CH = this.data[j];
				}
				if (this.ParseData("[MANUFACTURER FR]", j))
				{
					platformScript.manufacturer_FR = this.data[j];
				}
				if (this.ParseData("[MANUFACTURER HU]", j))
				{
					platformScript.manufacturer_HU = this.data[j];
				}
				if (this.ParseData("[MANUFACTURER JA]", j))
				{
					platformScript.manufacturer_JA = this.data[j];
				}
				if (this.ParseData("[COMPLEX]", j))
				{
					platformScript.complex = int.Parse(this.data[j]);
					if (platformScript.complex > 2)
					{
						platformScript.complex = 2;
					}
					if (platformScript.complex < 0)
					{
						platformScript.complex = 0;
					}
				}
				if (this.ParseData("[INTERNET]", j))
				{
					if (int.Parse(this.data[j]) == 1)
					{
						platformScript.internet = true;
					}
					else
					{
						platformScript.internet = false;
					}
				}
				if (this.ParseData("[STARTPLATFORM]", j))
				{
					platformScript.inBesitz = true;
				}
				if (this.ParseData("[END]", j) && platformScript.date_year == 1976 && platformScript.date_month == 1)
				{
					platformScript.isUnlocked = true;
					platformScript.units = platformScript.units_max / 2;
				}
				if (this.ParseData("[EOF]", j))
				{
					break;
				}
			}
		}
	}

	
	private bool ParseData(string c, int i)
	{
		if (this.data[i].Contains(c))
		{
			this.data[i] = this.data[i].Remove(this.data[i].Length - 1, 1);
			this.data[i] = this.data[i].Replace(c, "");
			return true;
		}
		return false;
	}

	
	private bool ParseDataDontCutLastChar(string c, int i)
	{
		if (this.data[i].Contains(c))
		{
			this.data[i] = this.data[i].Replace(c, "");
			return true;
		}
		return false;
	}

	
	public int GetDurchschnittsDevKitPreis()
	{
		int num = 1;
		int num2 = 1000;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Platform");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				platformScript component = array[i].GetComponent<platformScript>();
				if (!component.vomMarktGenommen && component.isUnlocked)
				{
					num++;
					num2 += component.price;
				}
			}
		}
		return num2 / num;
	}

	
	public void UpdateGamesForPlatforms()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Platform");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				platformScript component = array[i].GetComponent<platformScript>();
				if (component)
				{
					component.ZaehleGames();
				}
			}
		}
	}

	
	public void UpdatePlatformSells(bool sendDataToClient, bool forceSendAll)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Platform");
		if (this.mS_.multiplayer && !this.mpCalls_.isServer)
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i])
				{
					platformScript component = array[i].GetComponent<platformScript>();
					if (component.ownerID == this.mS_.myID && !component.isUnlocked)
					{
						component.weeksInDevelopment++;
					}
					if (component.ownerID == this.mS_.myID && component.isUnlocked && !component.vomMarktGenommen)
					{
						component.Sell();
						this.mpCalls_.CLIENT_Send_Platform(component);
					}
				}
			}
			return;
		}
		long num = 0L;
		long num2 = 0L;
		long num3 = 0L;
		long num4 = 0L;
		long num5 = 0L;
		for (int j = 0; j < array.Length; j++)
		{
			if (array[j])
			{
				platformScript component2 = array[j].GetComponent<platformScript>();
				if (component2.ownerID == this.mS_.myID && !component2.isUnlocked)
				{
					component2.weeksInDevelopment++;
				}
				component2.Sell();
				if (component2.isUnlocked && component2.vomMarktGenommen && component2.powerFromMarket > 0f)
				{
					component2.powerFromMarket -= 0.05f;
					if (component2.powerFromMarket < 0f)
					{
						component2.powerFromMarket = 0f;
					}
				}
				if (component2.IsVerfuegbar() || (component2.isUnlocked && component2.vomMarktGenommen && component2.powerFromMarket > 0f))
				{
					if (!component2.vomMarktGenommen)
					{
						switch (component2.typ)
						{
						case 0:
							num += (long)component2.units;
							break;
						case 1:
							num2 += (long)component2.units;
							break;
						case 2:
							num3 += (long)component2.units;
							break;
						case 3:
							num4 += (long)component2.units;
							break;
						case 4:
							num5 += (long)component2.units;
							break;
						default:
							num += (long)component2.units;
							break;
						}
					}
					else
					{
						float num6 = (float)component2.units;
						num6 *= component2.powerFromMarket;
						switch (component2.typ)
						{
						case 0:
							num += (long)Mathf.RoundToInt(num6);
							break;
						case 1:
							num2 += (long)Mathf.RoundToInt(num6);
							break;
						case 2:
							num3 += (long)Mathf.RoundToInt(num6);
							break;
						case 3:
							num4 += (long)Mathf.RoundToInt(num6);
							break;
						case 4:
							num5 += (long)Mathf.RoundToInt(num6);
							break;
						default:
							num += (long)Mathf.RoundToInt(num6);
							break;
						}
					}
				}
			}
		}
		for (int k = 0; k < array.Length; k++)
		{
			if (array[k])
			{
				platformScript component3 = array[k].GetComponent<platformScript>();
				switch (component3.typ)
				{
				case 0:
					component3.SetMarktanteil(num);
					break;
				case 1:
					component3.SetMarktanteil(num2);
					break;
				case 2:
					component3.SetMarktanteil(num3);
					break;
				case 3:
					component3.SetMarktanteil(num4);
					break;
				case 4:
					component3.SetMarktanteil(num5);
					break;
				default:
					component3.SetMarktanteil(num);
					break;
				}
				if (this.mS_.multiplayer && this.mpCalls_.isServer && sendDataToClient && (component3.IsVerfuegbar() || (component3.isUnlocked && component3.vomMarktGenommen && component3.powerFromMarket > 0f) || forceSendAll))
				{
					this.mpCalls_.SERVER_Send_PlatformData(component3);
				}
			}
		}
	}

	
	public Sprite GetPlayerPlatformSprite(int id_, int platTyp_)
	{
		if (platTyp_ == 1)
		{
			return this.playerConsoleSprites[id_];
		}
		if (platTyp_ == 2)
		{
			return this.playerConsoleSprites[id_];
		}
		return null;
	}

	
	public int GetPerformance(platformScript script_)
	{
		int num = 0;
		if (script_.component_cpu != -1)
		{
			num += this.hardware_.GetPerformance(script_.component_cpu);
		}
		if (script_.component_gfx != -1)
		{
			num += this.hardware_.GetPerformance(script_.component_gfx);
		}
		if (script_.component_ram != -1)
		{
			num += this.hardware_.GetPerformance(script_.component_ram);
		}
		if (script_.component_hdd != -1)
		{
			num += this.hardware_.GetPerformance(script_.component_hdd);
		}
		if (script_.component_sfx != -1)
		{
			num += this.hardware_.GetPerformance(script_.component_sfx);
		}
		if (script_.component_cooling != -1)
		{
			num += this.hardware_.GetPerformance(script_.component_cooling);
		}
		if (script_.component_disc != -1)
		{
			num += this.hardware_.GetPerformance(script_.component_disc);
		}
		int component_controller = script_.component_controller;
		int component_case = script_.component_case;
		if (script_.component_monitor != -1)
		{
			num += this.hardware_.GetPerformance(script_.component_monitor);
		}
		return num;
	}

	
	public float GetSellsCurve()
	{
		float num = (float)this.mS_.PassedMonth();
		if (num > 600f)
		{
			num = 600f;
		}
		return this.sellsCurve.Evaluate(num / 600f) * 1f;
	}

	
	public bool ExistInternetReadyConsole()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Platform");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				platformScript component = array[i].GetComponent<platformScript>();
				if (component.isUnlocked && component.internet && (component.typ == 1 || component.typ == 2))
				{
					Debug.Log("AA: " + component.GetName());
					return true;
				}
			}
		}
		return false;
	}

	
	public bool IsPlatformRemovedFromMarket(int platID)
	{
		GameObject gameObject = GameObject.Find("PLATFORM_" + platID.ToString());
		if (gameObject)
		{
			platformScript component = gameObject.GetComponent<platformScript>();
			if (component)
			{
				return component.vomMarktGenommen;
			}
		}
		return true;
	}

	
	public GameObject prefabPlatform;

	
	public mainScript mS_;

	
	private textScript tS_;

	
	private mpCalls mpCalls_;

	
	private settingsScript settings_;

	
	private gameplayFeatures gF_;

	
	private hardware hardware_;

	
	private hardwareFeatures hardwareFeatures_;

	
	private GUI_Main guiMain_;

	
	private games games_;

	
	private string[] data;

	
	public Sprite[] complexSprites;

	
	public Sprite[] typSprites;

	
	public Sprite[] playerConsoleSprites;

	
	public Sprite[] playerHandheldSprites;

	
	public Color[] playerPlatformColors;

	
	public AnimationCurve productionCostsCurve;

	
	public AnimationCurve sellsCurve;
}
