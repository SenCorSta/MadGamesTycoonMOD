using System;
using System.IO;
using System.Text;
using UnityEngine;


public class hardwareFeatures : MonoBehaviour
{
	
	private void Awake()
	{
		this.FindScripts();
	}

	
	private void FindScripts()
	{
		GameObject gameObject = GameObject.Find("Main");
		this.mS_ = gameObject.GetComponent<mainScript>();
		this.tS_ = gameObject.GetComponent<textScript>();
		this.settings_ = gameObject.GetComponent<settingsScript>();
	}

	
	public void Init()
	{
		this.hardFeat_PIC = new Sprite[this.hardFeat_UNLOCK.Length];
	}

	
	public void LoadHardwareFeatures(string filename)
	{
		int num = 0;
		StreamReader streamReader = new StreamReader(Application.dataPath + "/Extern/Text/" + filename, Encoding.Unicode);
		string text = streamReader.ReadToEnd();
		streamReader.Close();
		this.data = text.Split(new char[]
		{
			"\n"[0]
		});
		int num2 = 0;
		for (int i = 0; i < this.data.Length; i++)
		{
			if (this.data[i].Contains("[ID]"))
			{
				num2++;
			}
		}
		this.hardFeat_ICONFILE = new string[num2];
		this.hardFeat_PIC = new Sprite[num2];
		this.hardFeat_RES_POINTS = new int[num2];
		this.hardFeat_RES_POINTS_LEFT = new float[num2];
		this.hardFeat_PRICE = new int[num2];
		this.hardFeat_DEV_COSTS = new int[num2];
		this.hardFeat_DATE_YEAR = new int[num2];
		this.hardFeat_DATE_MONTH = new int[num2];
		this.hardFeat_UNLOCK = new bool[num2];
		this.hardFeat_ONLYSTATIONARY = new bool[num2];
		this.hardFeat_ONLYHANDHELD = new bool[num2];
		this.hardFeat_NEEDINTERNET = new bool[num2];
		this.hardFeat_QUALITY = new float[num2];
		this.hardFeat_NAME_EN = new string[num2];
		this.hardFeat_NAME_GE = new string[num2];
		this.hardFeat_NAME_TU = new string[num2];
		this.hardFeat_NAME_CH = new string[num2];
		this.hardFeat_NAME_FR = new string[num2];
		this.hardFeat_NAME_PB = new string[num2];
		this.hardFeat_NAME_CT = new string[num2];
		this.hardFeat_NAME_HU = new string[num2];
		this.hardFeat_NAME_ES = new string[num2];
		this.hardFeat_NAME_CZ = new string[num2];
		this.hardFeat_NAME_KO = new string[num2];
		this.hardFeat_NAME_AR = new string[num2];
		this.hardFeat_NAME_RU = new string[num2];
		this.hardFeat_NAME_IT = new string[num2];
		this.hardFeat_NAME_JA = new string[num2];
		this.hardFeat_NAME_PL = new string[num2];
		this.hardFeat_DESC_EN = new string[num2];
		this.hardFeat_DESC_GE = new string[num2];
		this.hardFeat_DESC_TU = new string[num2];
		this.hardFeat_DESC_CH = new string[num2];
		this.hardFeat_DESC_FR = new string[num2];
		this.hardFeat_DESC_PB = new string[num2];
		this.hardFeat_DESC_CT = new string[num2];
		this.hardFeat_DESC_HU = new string[num2];
		this.hardFeat_DESC_ES = new string[num2];
		this.hardFeat_DESC_CZ = new string[num2];
		this.hardFeat_DESC_KO = new string[num2];
		this.hardFeat_DESC_AR = new string[num2];
		this.hardFeat_DESC_RU = new string[num2];
		this.hardFeat_DESC_IT = new string[num2];
		this.hardFeat_DESC_JA = new string[num2];
		this.hardFeat_DESC_PL = new string[num2];
		int num3 = -1;
		for (int j = 0; j < this.data.Length; j++)
		{
			if (this.ParseData("[ID]", j))
			{
				num3 = int.Parse(this.data[j]);
			}
			if (this.ParseData("[RES POINTS]", j))
			{
				this.hardFeat_RES_POINTS[num3] = int.Parse(this.data[j]);
				this.hardFeat_RES_POINTS_LEFT[num3] = (float)this.hardFeat_RES_POINTS[num3];
			}
			if (this.ParseData("[PRICE]", j))
			{
				this.hardFeat_PRICE[num3] = int.Parse(this.data[j]);
			}
			if (this.ParseData("[DEV COSTS]", j))
			{
				this.hardFeat_DEV_COSTS[num3] = int.Parse(this.data[j]);
			}
			if (this.ParseData("[NEEDINTERNET]", j))
			{
				this.hardFeat_NEEDINTERNET[num3] = true;
			}
			if (this.ParseData("[QUALITY]", j))
			{
				this.hardFeat_QUALITY[num3] = (float)int.Parse(this.data[j]);
			}
			if (this.ParseData("[ONLY_STATIONARY]", j))
			{
				this.hardFeat_ONLYSTATIONARY[num3] = true;
			}
			if (this.ParseData("[ONLY_HANDHELD]", j))
			{
				this.hardFeat_ONLYHANDHELD[num3] = true;
			}
			if (this.ParseData("[DATE]", j))
			{
				if (this.ParseDataDontCutLastChar("JAN", j))
				{
					this.hardFeat_DATE_MONTH[num3] = 1;
				}
				if (this.ParseDataDontCutLastChar("FEB", j))
				{
					this.hardFeat_DATE_MONTH[num3] = 2;
				}
				if (this.ParseDataDontCutLastChar("MAR", j))
				{
					this.hardFeat_DATE_MONTH[num3] = 3;
				}
				if (this.ParseDataDontCutLastChar("APR", j))
				{
					this.hardFeat_DATE_MONTH[num3] = 4;
				}
				if (this.ParseDataDontCutLastChar("MAY", j))
				{
					this.hardFeat_DATE_MONTH[num3] = 5;
				}
				if (this.ParseDataDontCutLastChar("JUN", j))
				{
					this.hardFeat_DATE_MONTH[num3] = 6;
				}
				if (this.ParseDataDontCutLastChar("JUL", j))
				{
					this.hardFeat_DATE_MONTH[num3] = 7;
				}
				if (this.ParseDataDontCutLastChar("AUG", j))
				{
					this.hardFeat_DATE_MONTH[num3] = 8;
				}
				if (this.ParseDataDontCutLastChar("SEP", j))
				{
					this.hardFeat_DATE_MONTH[num3] = 9;
				}
				if (this.ParseDataDontCutLastChar("OCT", j))
				{
					this.hardFeat_DATE_MONTH[num3] = 10;
				}
				if (this.ParseDataDontCutLastChar("NOV", j))
				{
					this.hardFeat_DATE_MONTH[num3] = 11;
				}
				if (this.ParseDataDontCutLastChar("DEC", j))
				{
					this.hardFeat_DATE_MONTH[num3] = 12;
				}
				if (this.hardFeat_DATE_MONTH[num3] <= 0)
				{
					Debug.Log("ERROR: HardwareFeatures.txt -> Incorrect Month: " + this.hardFeat_NAME_EN[num3]);
				}
				this.hardFeat_DATE_YEAR[num3] = int.Parse(this.data[j]);
				if (this.hardFeat_DATE_YEAR[num3] == 1976 && this.hardFeat_DATE_MONTH[num3] == 1)
				{
					this.hardFeat_UNLOCK[num3] = true;
				}
			}
			if (this.ParseData("[PIC]", j))
			{
				this.hardFeat_ICONFILE[num3] = this.data[j];
			}
			if (this.ParseData("[NAME GE]", j))
			{
				this.hardFeat_NAME_GE[num3] = this.data[j];
			}
			if (this.ParseData("[NAME EN]", j))
			{
				this.hardFeat_NAME_EN[num3] = this.data[j];
			}
			if (this.ParseData("[NAME TU]", j))
			{
				this.hardFeat_NAME_TU[num3] = this.data[j];
			}
			if (this.ParseData("[NAME CH]", j))
			{
				this.hardFeat_NAME_CH[num3] = this.data[j];
			}
			if (this.ParseData("[NAME FR]", j))
			{
				this.hardFeat_NAME_FR[num3] = this.data[j];
			}
			if (this.ParseData("[NAME PB]", j))
			{
				this.hardFeat_NAME_PB[num3] = this.data[j];
			}
			if (this.ParseData("[NAME CT]", j))
			{
				this.hardFeat_NAME_CT[num3] = this.data[j];
			}
			if (this.ParseData("[NAME HU]", j))
			{
				this.hardFeat_NAME_HU[num3] = this.data[j];
			}
			if (this.ParseData("[NAME ES]", j))
			{
				this.hardFeat_NAME_ES[num3] = this.data[j];
			}
			if (this.ParseData("[NAME CZ]", j))
			{
				this.hardFeat_NAME_CZ[num3] = this.data[j];
			}
			if (this.ParseData("[NAME KO]", j))
			{
				this.hardFeat_NAME_KO[num3] = this.data[j];
			}
			if (this.ParseData("[NAME AR]", j))
			{
				this.hardFeat_NAME_AR[num3] = this.data[j];
			}
			if (this.ParseData("[NAME RU]", j))
			{
				this.hardFeat_NAME_RU[num3] = this.data[j];
			}
			if (this.ParseData("[NAME IT]", j))
			{
				this.hardFeat_NAME_IT[num3] = this.data[j];
			}
			if (this.ParseData("[NAME JA]", j))
			{
				this.hardFeat_NAME_JA[num3] = this.data[j];
			}
			if (this.ParseData("[NAME PL]", j))
			{
				this.hardFeat_NAME_PL[num3] = this.data[j];
			}
			if (this.ParseData("[DESC GE]", j))
			{
				this.hardFeat_DESC_GE[num3] = this.data[j];
			}
			if (this.ParseData("[DESC EN]", j))
			{
				this.hardFeat_DESC_EN[num3] = this.data[j];
			}
			if (this.ParseData("[DESC TU]", j))
			{
				this.hardFeat_DESC_TU[num3] = this.data[j];
			}
			if (this.ParseData("[DESC CH]", j))
			{
				this.hardFeat_DESC_CH[num3] = this.data[j];
			}
			if (this.ParseData("[DESC FR]", j))
			{
				this.hardFeat_DESC_FR[num3] = this.data[j];
			}
			if (this.ParseData("[DESC PB]", j))
			{
				this.hardFeat_DESC_PB[num3] = this.data[j];
			}
			if (this.ParseData("[DESC CT]", j))
			{
				this.hardFeat_DESC_CT[num3] = this.data[j];
			}
			if (this.ParseData("[DESC HU]", j))
			{
				this.hardFeat_DESC_HU[num3] = this.data[j];
			}
			if (this.ParseData("[DESC ES]", j))
			{
				this.hardFeat_DESC_ES[num3] = this.data[j];
			}
			if (this.ParseData("[DESC CZ]", j))
			{
				this.hardFeat_DESC_CZ[num3] = this.data[j];
			}
			if (this.ParseData("[DESC KO]", j))
			{
				this.hardFeat_DESC_KO[num3] = this.data[j];
			}
			if (this.ParseData("[DESC AR]", j))
			{
				this.hardFeat_DESC_AR[num3] = this.data[j];
			}
			if (this.ParseData("[DESC RU]", j))
			{
				this.hardFeat_DESC_RU[num3] = this.data[j];
			}
			if (this.ParseData("[DESC IT]", j))
			{
				this.hardFeat_DESC_IT[num3] = this.data[j];
			}
			if (this.ParseData("[DESC JA]", j))
			{
				this.hardFeat_DESC_JA[num3] = this.data[j];
			}
			if (this.ParseData("[DESC PL]", j))
			{
				this.hardFeat_DESC_PL[num3] = this.data[j];
			}
			this.ParseData("//", j);
			if (this.ParseData("[EOF]", j))
			{
				break;
			}
			num++;
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

	
	public string GetName(int i)
	{
		string text = "";
		switch (this.settings_.language)
		{
		case 0:
			text = this.hardFeat_NAME_EN[i];
			goto IL_1CE;
		case 1:
			text = this.hardFeat_NAME_GE[i];
			goto IL_1CE;
		case 2:
			if (this.hardFeat_NAME_TU.Length != 0)
			{
				text = this.hardFeat_NAME_TU[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 3:
			if (this.hardFeat_NAME_CH.Length != 0)
			{
				text = this.hardFeat_NAME_CH[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 4:
			if (this.hardFeat_NAME_FR.Length != 0)
			{
				text = this.hardFeat_NAME_FR[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 5:
			if (this.hardFeat_NAME_ES.Length != 0)
			{
				text = this.hardFeat_NAME_ES[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 6:
			if (this.hardFeat_NAME_KO.Length != 0)
			{
				text = this.hardFeat_NAME_KO[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 7:
			if (this.hardFeat_NAME_PB.Length != 0)
			{
				text = this.hardFeat_NAME_PB[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 8:
			if (this.hardFeat_NAME_HU.Length != 0)
			{
				text = this.hardFeat_NAME_HU[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 9:
			if (this.hardFeat_NAME_RU.Length != 0)
			{
				text = this.hardFeat_NAME_RU[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 10:
			if (this.hardFeat_NAME_CT.Length != 0)
			{
				text = this.hardFeat_NAME_CT[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 11:
			if (this.hardFeat_NAME_PL.Length != 0)
			{
				text = this.hardFeat_NAME_PL[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 12:
			if (this.hardFeat_NAME_CZ.Length != 0)
			{
				text = this.hardFeat_NAME_CZ[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 13:
			if (this.hardFeat_NAME_AR.Length != 0)
			{
				text = this.hardFeat_NAME_AR[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 14:
			if (this.hardFeat_NAME_IT.Length != 0)
			{
				text = this.hardFeat_NAME_IT[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 16:
			if (this.hardFeat_NAME_JA.Length != 0)
			{
				text = this.hardFeat_NAME_JA[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		}
		text = this.hardFeat_NAME_EN[i];
		IL_1CE:
		if (text == null)
		{
			return this.hardFeat_NAME_EN[i];
		}
		if (text.Length <= 0)
		{
			return this.hardFeat_NAME_EN[i];
		}
		return text;
	}

	
	public string GetDesc(int i)
	{
		string text = "";
		switch (this.settings_.language)
		{
		case 0:
			text = this.hardFeat_DESC_EN[i];
			goto IL_1CE;
		case 1:
			text = this.hardFeat_DESC_GE[i];
			goto IL_1CE;
		case 2:
			if (this.hardFeat_DESC_TU.Length != 0)
			{
				text = this.hardFeat_DESC_TU[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 3:
			if (this.hardFeat_DESC_CH.Length != 0)
			{
				text = this.hardFeat_DESC_CH[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 4:
			if (this.hardFeat_DESC_FR.Length != 0)
			{
				text = this.hardFeat_DESC_FR[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 5:
			if (this.hardFeat_DESC_ES.Length != 0)
			{
				text = this.hardFeat_DESC_ES[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 6:
			if (this.hardFeat_DESC_KO.Length != 0)
			{
				text = this.hardFeat_DESC_KO[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 7:
			if (this.hardFeat_DESC_PB.Length != 0)
			{
				text = this.hardFeat_DESC_PB[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 8:
			if (this.hardFeat_DESC_HU.Length != 0)
			{
				text = this.hardFeat_DESC_HU[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 9:
			if (this.hardFeat_DESC_RU.Length != 0)
			{
				text = this.hardFeat_DESC_RU[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 10:
			if (this.hardFeat_DESC_CT.Length != 0)
			{
				text = this.hardFeat_DESC_CT[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 11:
			if (this.hardFeat_DESC_PL.Length != 0)
			{
				text = this.hardFeat_DESC_PL[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 12:
			if (this.hardFeat_DESC_CZ.Length != 0)
			{
				text = this.hardFeat_DESC_CZ[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 13:
			if (this.hardFeat_DESC_AR.Length != 0)
			{
				text = this.hardFeat_DESC_AR[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 14:
			if (this.hardFeat_DESC_IT.Length != 0)
			{
				text = this.hardFeat_DESC_IT[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 16:
			if (this.hardFeat_DESC_JA.Length != 0)
			{
				text = this.hardFeat_DESC_JA[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		}
		text = this.hardFeat_DESC_EN[i];
		IL_1CE:
		if (text == null)
		{
			return "";
		}
		if (text.Length <= 0)
		{
			return this.hardFeat_DESC_EN[i];
		}
		return text;
	}

	
	public int GetDevCosts(int i)
	{
		return this.hardFeat_DEV_COSTS[i];
	}

	
	public int GetWorkPoints(int i)
	{
		float num = (float)this.hardFeat_RES_POINTS[i];
		num *= 0.3f;
		return 100 + Mathf.RoundToInt(num);
	}

	
	public int GetPrice(int i)
	{
		return this.hardFeat_PRICE[i];
	}

	
	public bool IsErforscht(int i)
	{
		return this.hardFeat_RES_POINTS_LEFT[i] <= 0f;
	}

	
	public float GetProzent(int i)
	{
		return 100f / (float)this.hardFeat_RES_POINTS[i] * ((float)this.hardFeat_RES_POINTS[i] - this.hardFeat_RES_POINTS_LEFT[i]);
	}

	
	public void UnlockAll()
	{
		for (int i = 0; i < this.hardFeat_UNLOCK.Length; i++)
		{
			this.hardFeat_UNLOCK[i] = true;
		}
	}

	
	public bool ForschungGestartet(int i)
	{
		return this.hardFeat_RES_POINTS_LEFT[i] != (float)this.hardFeat_RES_POINTS[i];
	}

	
	public bool Pay(int i)
	{
		if (!this.ForschungGestartet(i))
		{
			if (this.mS_.NotEnoughMoney(this.hardFeat_PRICE[i]))
			{
				return false;
			}
			this.mS_.Pay((long)this.GetPrice(i), 2);
		}
		return true;
	}

	
	public bool BereitsInAnderenRaumAktiv(int s)
	{
		for (int i = 0; i < this.mS_.arrayRooms.Length; i++)
		{
			if (this.mS_.arrayRooms[i])
			{
				roomScript component = this.mS_.arrayRooms[i].GetComponent<roomScript>();
				if (component.typ == 2 && component.taskGameObject)
				{
					taskForschung component2 = component.taskGameObject.GetComponent<taskForschung>();
					if (component2 && component2.slot == s && component2.typ == 6)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	
	public string GetDateString(int i)
	{
		return this.hardFeat_DATE_YEAR[i].ToString() + " " + this.tS_.GetText(this.hardFeat_DATE_MONTH[i] + 220);
	}

	
	public string GetTooltip(int i)
	{
		string text = "<b>" + this.GetName(i) + "</b>\n";
		text = text + "<color=magenta>" + this.tS_.GetText(1599) + "</color>";
		text = string.Concat(new string[]
		{
			text,
			"\n",
			this.tS_.GetText(217),
			": ",
			this.GetDateString(i)
		});
		string desc = this.GetDesc(i);
		if (desc.Length > 0)
		{
			text = text + "\n\n" + desc;
		}
		if (this.hardFeat_NEEDINTERNET[i])
		{
			text = text + "\n\n<b><color=blue>" + this.tS_.GetText(1618) + "</color></b>";
		}
		if (this.hardFeat_ONLYSTATIONARY[i])
		{
			text = text + "\n\n<b><color=red>" + this.tS_.GetText(1603) + "</color></b>";
		}
		if (this.hardFeat_ONLYHANDHELD[i])
		{
			text = text + "\n\n<b><color=red>" + this.tS_.GetText(1602) + "</color></b>";
		}
		return text;
	}

	
	public Sprite GetSprite(int i)
	{
		if (this.hardFeat_ICONFILE[i] == null)
		{
			return this.hardFeatureSprite;
		}
		if (string.IsNullOrEmpty(this.hardFeat_ICONFILE[i]))
		{
			return this.hardFeatureSprite;
		}
		if (this.hardFeat_ICONFILE[i].Length > 0)
		{
			if (this.hardFeat_PIC[i] == null)
			{
				this.hardFeat_PIC[i] = this.mS_.LoadPNG(Application.dataPath + "/Extern/Icons_Hardware/" + this.hardFeat_ICONFILE[i]);
			}
			return this.hardFeat_PIC[i];
		}
		return this.hardFeatureSprite;
	}

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private settingsScript settings_;

	
	public Sprite hardFeatureSprite;

	
	private Sprite[] hardFeat_PIC;

	
	public string[] hardFeat_ICONFILE;

	
	public int[] hardFeat_RES_POINTS;

	
	public float[] hardFeat_RES_POINTS_LEFT;

	
	public int[] hardFeat_PRICE;

	
	public int[] hardFeat_DEV_COSTS;

	
	public int[] hardFeat_DATE_YEAR;

	
	public int[] hardFeat_DATE_MONTH;

	
	public bool[] hardFeat_UNLOCK;

	
	public bool[] hardFeat_ONLYSTATIONARY;

	
	public bool[] hardFeat_ONLYHANDHELD;

	
	public bool[] hardFeat_NEEDINTERNET;

	
	public float[] hardFeat_QUALITY;

	
	public string[] hardFeat_NAME_EN;

	
	public string[] hardFeat_NAME_GE;

	
	public string[] hardFeat_NAME_TU;

	
	public string[] hardFeat_NAME_CH;

	
	public string[] hardFeat_NAME_FR;

	
	public string[] hardFeat_NAME_PB;

	
	public string[] hardFeat_NAME_CT;

	
	public string[] hardFeat_NAME_HU;

	
	public string[] hardFeat_NAME_ES;

	
	public string[] hardFeat_NAME_CZ;

	
	public string[] hardFeat_NAME_KO;

	
	public string[] hardFeat_NAME_AR;

	
	public string[] hardFeat_NAME_RU;

	
	public string[] hardFeat_NAME_IT;

	
	public string[] hardFeat_NAME_JA;

	
	public string[] hardFeat_NAME_PL;

	
	public string[] hardFeat_DESC_EN;

	
	public string[] hardFeat_DESC_GE;

	
	public string[] hardFeat_DESC_TU;

	
	public string[] hardFeat_DESC_CH;

	
	public string[] hardFeat_DESC_FR;

	
	public string[] hardFeat_DESC_PB;

	
	public string[] hardFeat_DESC_CT;

	
	public string[] hardFeat_DESC_HU;

	
	public string[] hardFeat_DESC_ES;

	
	public string[] hardFeat_DESC_CZ;

	
	public string[] hardFeat_DESC_KO;

	
	public string[] hardFeat_DESC_AR;

	
	public string[] hardFeat_DESC_RU;

	
	public string[] hardFeat_DESC_IT;

	
	public string[] hardFeat_DESC_JA;

	
	public string[] hardFeat_DESC_PL;

	
	private string[] data;
}
