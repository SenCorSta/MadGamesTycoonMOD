using System;
using System.IO;
using System.Text;
using UnityEngine;


public class hardware : MonoBehaviour
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
		this.hardware_PIC = new Sprite[this.hardware_UNLOCK.Length];
	}

	
	public void LoadHardwareKomponenten(string filename)
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
		Debug.Log("Hardware Features Amount: " + num2.ToString());
		this.hardware_ICONFILE = new string[num2];
		this.hardware_PIC = new Sprite[num2];
		this.hardware_TYP = new int[num2];
		this.hardware_RES_POINTS = new int[num2];
		this.hardware_RES_POINTS_LEFT = new float[num2];
		this.hardware_PRICE = new int[num2];
		this.hardware_DEV_COSTS = new int[num2];
		this.hardware_TECH = new int[num2];
		this.hardware_DATE_YEAR = new int[num2];
		this.hardware_DATE_MONTH = new int[num2];
		this.hardware_UNLOCK = new bool[num2];
		this.hardware_ONLYSTATIONARY = new bool[num2];
		this.hardware_ONLYHANDHELD = new bool[num2];
		this.hardware_NEED1 = new int[num2];
		this.hardware_NEED2 = new int[num2];
		this.hardware_NAME_EN = new string[num2];
		this.hardware_NAME_GE = new string[num2];
		this.hardware_NAME_TU = new string[num2];
		this.hardware_NAME_CH = new string[num2];
		this.hardware_NAME_FR = new string[num2];
		this.hardware_NAME_PB = new string[num2];
		this.hardware_NAME_CT = new string[num2];
		this.hardware_NAME_HU = new string[num2];
		this.hardware_NAME_ES = new string[num2];
		this.hardware_NAME_CZ = new string[num2];
		this.hardware_NAME_KO = new string[num2];
		this.hardware_NAME_AR = new string[num2];
		this.hardware_NAME_RU = new string[num2];
		this.hardware_NAME_IT = new string[num2];
		this.hardware_NAME_JA = new string[num2];
		this.hardware_NAME_PL = new string[num2];
		this.hardware_DESC_EN = new string[num2];
		this.hardware_DESC_GE = new string[num2];
		this.hardware_DESC_TU = new string[num2];
		this.hardware_DESC_CH = new string[num2];
		this.hardware_DESC_FR = new string[num2];
		this.hardware_DESC_PB = new string[num2];
		this.hardware_DESC_CT = new string[num2];
		this.hardware_DESC_HU = new string[num2];
		this.hardware_DESC_ES = new string[num2];
		this.hardware_DESC_CZ = new string[num2];
		this.hardware_DESC_KO = new string[num2];
		this.hardware_DESC_AR = new string[num2];
		this.hardware_DESC_RU = new string[num2];
		this.hardware_DESC_IT = new string[num2];
		this.hardware_DESC_JA = new string[num2];
		this.hardware_DESC_PL = new string[num2];
		int num3 = -1;
		for (int j = 0; j < this.data.Length; j++)
		{
			if (this.ParseData("[ID]", j))
			{
				num3 = int.Parse(this.data[j]);
			}
			if (this.ParseData("[TYP]", j))
			{
				this.hardware_TYP[num3] = int.Parse(this.data[j]);
			}
			if (this.ParseData("[RES POINTS]", j))
			{
				this.hardware_RES_POINTS[num3] = int.Parse(this.data[j]);
				this.hardware_RES_POINTS_LEFT[num3] = (float)this.hardware_RES_POINTS[num3];
			}
			if (this.ParseData("[PRICE]", j))
			{
				this.hardware_PRICE[num3] = int.Parse(this.data[j]);
			}
			if (this.ParseData("[DEV COSTS]", j))
			{
				this.hardware_DEV_COSTS[num3] = int.Parse(this.data[j]);
			}
			if (this.ParseData("[TECHLEVEL]", j))
			{
				this.hardware_TECH[num3] = int.Parse(this.data[j]);
			}
			if (this.ParseData("[ONLY_STATIONARY]", j))
			{
				this.hardware_ONLYSTATIONARY[num3] = true;
			}
			if (this.ParseData("[ONLY_HANDHELD]", j))
			{
				this.hardware_ONLYHANDHELD[num3] = true;
			}
			if (this.ParseData("[NEED-1]", j))
			{
				this.hardware_NEED1[num3] = int.Parse(this.data[j]);
			}
			if (this.ParseData("[NEED-2]", j))
			{
				this.hardware_NEED2[num3] = int.Parse(this.data[j]);
			}
			if (this.ParseData("[DATE]", j))
			{
				if (this.ParseDataDontCutLastChar("JAN", j))
				{
					this.hardware_DATE_MONTH[num3] = 1;
				}
				if (this.ParseDataDontCutLastChar("FEB", j))
				{
					this.hardware_DATE_MONTH[num3] = 2;
				}
				if (this.ParseDataDontCutLastChar("MAR", j))
				{
					this.hardware_DATE_MONTH[num3] = 3;
				}
				if (this.ParseDataDontCutLastChar("APR", j))
				{
					this.hardware_DATE_MONTH[num3] = 4;
				}
				if (this.ParseDataDontCutLastChar("MAY", j))
				{
					this.hardware_DATE_MONTH[num3] = 5;
				}
				if (this.ParseDataDontCutLastChar("JUN", j))
				{
					this.hardware_DATE_MONTH[num3] = 6;
				}
				if (this.ParseDataDontCutLastChar("JUL", j))
				{
					this.hardware_DATE_MONTH[num3] = 7;
				}
				if (this.ParseDataDontCutLastChar("AUG", j))
				{
					this.hardware_DATE_MONTH[num3] = 8;
				}
				if (this.ParseDataDontCutLastChar("SEP", j))
				{
					this.hardware_DATE_MONTH[num3] = 9;
				}
				if (this.ParseDataDontCutLastChar("OCT", j))
				{
					this.hardware_DATE_MONTH[num3] = 10;
				}
				if (this.ParseDataDontCutLastChar("NOV", j))
				{
					this.hardware_DATE_MONTH[num3] = 11;
				}
				if (this.ParseDataDontCutLastChar("DEC", j))
				{
					this.hardware_DATE_MONTH[num3] = 12;
				}
				if (this.hardware_DATE_MONTH[num3] <= 0)
				{
					Debug.Log("ERROR: Hardware.txt -> Incorrect Month: " + this.hardware_NAME_EN[num3]);
				}
				this.hardware_DATE_YEAR[num3] = int.Parse(this.data[j]);
				if (this.hardware_DATE_YEAR[num3] == 1976 && this.hardware_DATE_MONTH[num3] == 1)
				{
					this.hardware_UNLOCK[num3] = true;
				}
			}
			if (this.ParseData("[PIC]", j))
			{
				this.hardware_ICONFILE[num3] = this.data[j];
			}
			if (this.ParseData("[NAME GE]", j))
			{
				this.hardware_NAME_GE[num3] = this.data[j];
			}
			if (this.ParseData("[NAME EN]", j))
			{
				this.hardware_NAME_EN[num3] = this.data[j];
			}
			if (this.ParseData("[NAME TU]", j))
			{
				this.hardware_NAME_TU[num3] = this.data[j];
			}
			if (this.ParseData("[NAME CH]", j))
			{
				this.hardware_NAME_CH[num3] = this.data[j];
			}
			if (this.ParseData("[NAME FR]", j))
			{
				this.hardware_NAME_FR[num3] = this.data[j];
			}
			if (this.ParseData("[NAME PB]", j))
			{
				this.hardware_NAME_PB[num3] = this.data[j];
			}
			if (this.ParseData("[NAME CT]", j))
			{
				this.hardware_NAME_CT[num3] = this.data[j];
			}
			if (this.ParseData("[NAME HU]", j))
			{
				this.hardware_NAME_HU[num3] = this.data[j];
			}
			if (this.ParseData("[NAME ES]", j))
			{
				this.hardware_NAME_ES[num3] = this.data[j];
			}
			if (this.ParseData("[NAME CZ]", j))
			{
				this.hardware_NAME_CZ[num3] = this.data[j];
			}
			if (this.ParseData("[NAME KO]", j))
			{
				this.hardware_NAME_KO[num3] = this.data[j];
			}
			if (this.ParseData("[NAME AR]", j))
			{
				this.hardware_NAME_AR[num3] = this.data[j];
			}
			if (this.ParseData("[NAME RU]", j))
			{
				this.hardware_NAME_RU[num3] = this.data[j];
			}
			if (this.ParseData("[NAME IT]", j))
			{
				this.hardware_NAME_IT[num3] = this.data[j];
			}
			if (this.ParseData("[NAME JA]", j))
			{
				this.hardware_NAME_JA[num3] = this.data[j];
			}
			if (this.ParseData("[NAME PL]", j))
			{
				this.hardware_NAME_PL[num3] = this.data[j];
			}
			if (this.ParseData("[DESC GE]", j))
			{
				this.hardware_DESC_GE[num3] = this.data[j];
			}
			if (this.ParseData("[DESC EN]", j))
			{
				this.hardware_DESC_EN[num3] = this.data[j];
			}
			if (this.ParseData("[DESC TU]", j))
			{
				this.hardware_DESC_TU[num3] = this.data[j];
			}
			if (this.ParseData("[DESC CH]", j))
			{
				this.hardware_DESC_CH[num3] = this.data[j];
			}
			if (this.ParseData("[DESC FR]", j))
			{
				this.hardware_DESC_FR[num3] = this.data[j];
			}
			if (this.ParseData("[DESC PB]", j))
			{
				this.hardware_DESC_PB[num3] = this.data[j];
			}
			if (this.ParseData("[DESC CT]", j))
			{
				this.hardware_DESC_CT[num3] = this.data[j];
			}
			if (this.ParseData("[DESC HU]", j))
			{
				this.hardware_DESC_HU[num3] = this.data[j];
			}
			if (this.ParseData("[DESC ES]", j))
			{
				this.hardware_DESC_ES[num3] = this.data[j];
			}
			if (this.ParseData("[DESC CZ]", j))
			{
				this.hardware_DESC_CZ[num3] = this.data[j];
			}
			if (this.ParseData("[DESC KO]", j))
			{
				this.hardware_DESC_KO[num3] = this.data[j];
			}
			if (this.ParseData("[DESC AR]", j))
			{
				this.hardware_DESC_AR[num3] = this.data[j];
			}
			if (this.ParseData("[DESC RU]", j))
			{
				this.hardware_DESC_RU[num3] = this.data[j];
			}
			if (this.ParseData("[DESC IT]", j))
			{
				this.hardware_DESC_IT[num3] = this.data[j];
			}
			if (this.ParseData("[DESC JA]", j))
			{
				this.hardware_DESC_JA[num3] = this.data[j];
			}
			if (this.ParseData("[DESC PL]", j))
			{
				this.hardware_DESC_PL[num3] = this.data[j];
			}
			this.ParseData("//", j);
			if (this.ParseData("[EOF]", j))
			{
				Debug.Log("Hardware.txt -> EOF");
				return;
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
			text = this.hardware_NAME_EN[i];
			goto IL_1CE;
		case 1:
			text = this.hardware_NAME_GE[i];
			goto IL_1CE;
		case 2:
			if (this.hardware_NAME_TU.Length != 0)
			{
				text = this.hardware_NAME_TU[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 3:
			if (this.hardware_NAME_CH.Length != 0)
			{
				text = this.hardware_NAME_CH[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 4:
			if (this.hardware_NAME_FR.Length != 0)
			{
				text = this.hardware_NAME_FR[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 5:
			if (this.hardware_NAME_ES.Length != 0)
			{
				text = this.hardware_NAME_ES[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 6:
			if (this.hardware_NAME_KO.Length != 0)
			{
				text = this.hardware_NAME_KO[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 7:
			if (this.hardware_NAME_PB.Length != 0)
			{
				text = this.hardware_NAME_PB[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 8:
			if (this.hardware_NAME_HU.Length != 0)
			{
				text = this.hardware_NAME_HU[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 9:
			if (this.hardware_NAME_RU.Length != 0)
			{
				text = this.hardware_NAME_RU[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 10:
			if (this.hardware_NAME_CT.Length != 0)
			{
				text = this.hardware_NAME_CT[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 11:
			if (this.hardware_NAME_PL.Length != 0)
			{
				text = this.hardware_NAME_PL[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 12:
			if (this.hardware_NAME_CZ.Length != 0)
			{
				text = this.hardware_NAME_CZ[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 13:
			if (this.hardware_NAME_AR.Length != 0)
			{
				text = this.hardware_NAME_AR[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 14:
			if (this.hardware_NAME_IT.Length != 0)
			{
				text = this.hardware_NAME_IT[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 16:
			if (this.hardware_NAME_JA.Length != 0)
			{
				text = this.hardware_NAME_JA[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		}
		text = this.hardware_NAME_EN[i];
		IL_1CE:
		if (text == null)
		{
			return this.hardware_NAME_EN[i];
		}
		if (text.Length <= 0)
		{
			return this.hardware_NAME_EN[i];
		}
		return text;
	}

	
	public string GetDesc(int i)
	{
		string text = "";
		switch (this.settings_.language)
		{
		case 0:
			text = this.hardware_DESC_EN[i];
			goto IL_1CE;
		case 1:
			text = this.hardware_DESC_GE[i];
			goto IL_1CE;
		case 2:
			if (this.hardware_DESC_TU.Length != 0)
			{
				text = this.hardware_DESC_TU[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 3:
			if (this.hardware_DESC_CH.Length != 0)
			{
				text = this.hardware_DESC_CH[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 4:
			if (this.hardware_DESC_FR.Length != 0)
			{
				text = this.hardware_DESC_FR[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 5:
			if (this.hardware_DESC_ES.Length != 0)
			{
				text = this.hardware_DESC_ES[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 6:
			if (this.hardware_DESC_KO.Length != 0)
			{
				text = this.hardware_DESC_KO[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 7:
			if (this.hardware_DESC_PB.Length != 0)
			{
				text = this.hardware_DESC_PB[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 8:
			if (this.hardware_DESC_HU.Length != 0)
			{
				text = this.hardware_DESC_HU[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 9:
			if (this.hardware_DESC_RU.Length != 0)
			{
				text = this.hardware_DESC_RU[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 10:
			if (this.hardware_DESC_CT.Length != 0)
			{
				text = this.hardware_DESC_CT[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 11:
			if (this.hardware_DESC_PL.Length != 0)
			{
				text = this.hardware_DESC_PL[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 12:
			if (this.hardware_DESC_CZ.Length != 0)
			{
				text = this.hardware_DESC_CZ[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 13:
			if (this.hardware_DESC_AR.Length != 0)
			{
				text = this.hardware_DESC_AR[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 14:
			if (this.hardware_DESC_IT.Length != 0)
			{
				text = this.hardware_DESC_IT[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 16:
			if (this.hardware_DESC_JA.Length != 0)
			{
				text = this.hardware_DESC_JA[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		}
		text = this.hardware_DESC_EN[i];
		IL_1CE:
		if (text == null)
		{
			return "";
		}
		if (text.Length <= 0)
		{
			return this.hardware_DESC_EN[i];
		}
		return text;
	}

	
	public int GetDevCosts(int i)
	{
		return this.hardware_DEV_COSTS[i];
	}

	
	public int GetWorkPoints(int i)
	{
		return 100 + this.hardware_RES_POINTS[i];
	}

	
	public int GetPrice(int i)
	{
		return this.hardware_PRICE[i];
	}

	
	public int GetPerformance(int i)
	{
		return Mathf.RoundToInt((float)(this.hardware_TECH[i] * (this.hardware_RES_POINTS[i] + 500) / 100));
	}

	
	public bool IsErforscht(int i)
	{
		return this.hardware_RES_POINTS_LEFT[i] <= 0f;
	}

	
	public float GetProzent(int i)
	{
		return 100f / (float)this.hardware_RES_POINTS[i] * ((float)this.hardware_RES_POINTS[i] - this.hardware_RES_POINTS_LEFT[i]);
	}

	
	public void UnlockAll()
	{
		for (int i = 0; i < this.hardware_UNLOCK.Length; i++)
		{
			this.hardware_UNLOCK[i] = true;
		}
	}

	
	public bool ForschungGestartet(int i)
	{
		return this.hardware_RES_POINTS_LEFT[i] != (float)this.hardware_RES_POINTS[i];
	}

	
	public bool Pay(int i)
	{
		if (!this.ForschungGestartet(i))
		{
			if (this.mS_.NotEnoughMoney(this.hardware_PRICE[i]))
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
					if (component2 && component2.slot == s && component2.typ == 4)
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
		return this.hardware_DATE_YEAR[i].ToString() + " " + this.tS_.GetText(this.hardware_DATE_MONTH[i] + 220);
	}

	
	public string GetTooltip(int i)
	{
		string text = "<b>" + this.GetName(i) + "</b>\n";
		text = text + "<color=magenta>" + this.GetTypString(this.hardware_TYP[i]) + "</color>";
		text = string.Concat(new string[]
		{
			text,
			"\n",
			this.tS_.GetText(217),
			": ",
			this.GetDateString(i)
		});
		text = string.Concat(new string[]
		{
			text,
			"\n",
			this.tS_.GetText(1604),
			": ",
			this.GetPerformance(i).ToString()
		});
		if (this.IsTechComponent(i))
		{
			text = text + "\n\n<color=green>" + this.tS_.GetText(1610) + "</color>";
			text = string.Concat(new string[]
			{
				text,
				"\n<b><color=blue>",
				this.tS_.GetText(4),
				" ",
				this.hardware_TECH[i].ToString(),
				"</color></b>"
			});
		}
		if (this.hardware_TYP[i] == 8 || this.hardware_TYP[i] == 7)
		{
			text = text + "\n\n<color=green>" + this.tS_.GetText(1611) + "</color>";
		}
		string desc = this.GetDesc(i);
		if (desc.Length > 0)
		{
			text = text + "\n\n" + desc;
		}
		if (this.hardware_ONLYSTATIONARY[i])
		{
			text = text + "\n\n<b><color=red>" + this.tS_.GetText(1603) + "</color></b>";
		}
		if (this.hardware_ONLYHANDHELD[i])
		{
			text = text + "\n\n<b><color=red>" + this.tS_.GetText(1602) + "</color></b>";
		}
		return text;
	}

	
	public Sprite GetTypPic(int i)
	{
		if (this.hardware_ICONFILE[i] == null)
		{
			return this.hardware_PICTYP[this.hardware_TYP[i]];
		}
		if (string.IsNullOrEmpty(this.hardware_ICONFILE[i]))
		{
			return this.hardware_PICTYP[this.hardware_TYP[i]];
		}
		if (this.hardware_ICONFILE[i].Length > 3)
		{
			if (this.hardware_PIC[i] == null)
			{
				this.hardware_PIC[i] = this.mS_.LoadPNG(Application.dataPath + "/Extern/Icons_Hardware/" + this.hardware_ICONFILE[i]);
			}
			return this.hardware_PIC[i];
		}
		return this.hardware_PICTYP[this.hardware_TYP[i]];
	}

	
	public string GetTypString(int i)
	{
		string result = "";
		switch (i)
		{
		case 0:
			result = this.tS_.GetText(1588);
			break;
		case 1:
			result = this.tS_.GetText(1590);
			break;
		case 2:
			result = this.tS_.GetText(1589);
			break;
		case 3:
			result = this.tS_.GetText(1592);
			break;
		case 4:
			result = this.tS_.GetText(1591);
			break;
		case 5:
			result = this.tS_.GetText(1593);
			break;
		case 6:
			result = this.tS_.GetText(1594);
			break;
		case 7:
			result = this.tS_.GetText(1597);
			break;
		case 8:
			result = this.tS_.GetText(1598);
			break;
		case 9:
			result = this.tS_.GetText(1595);
			break;
		case 10:
			result = this.tS_.GetText(1599);
			break;
		}
		return result;
	}

	
	public bool IsTechComponent(int i)
	{
		return this.hardware_TYP[i] == 0 || this.hardware_TYP[i] == 1 || this.hardware_TYP[i] == 2 || this.hardware_TYP[i] == 3 || this.hardware_TYP[i] == 4 || this.hardware_TYP[i] == 5 || this.hardware_TYP[i] == 6 || this.hardware_TYP[i] == 9;
	}

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private settingsScript settings_;

	
	public const int component_cpu = 0;

	
	public const int component_gfx = 1;

	
	public const int component_ram = 2;

	
	public const int component_hdd = 3;

	
	public const int component_sfx = 4;

	
	public const int component_cooling = 5;

	
	public const int component_disc = 6;

	
	public const int component_controller = 7;

	
	public const int component_case = 8;

	
	public const int component_monitor = 9;

	
	private Sprite[] hardware_PIC;

	
	public Sprite[] hardware_PICTYP;

	
	public string[] hardware_ICONFILE;

	
	public int[] hardware_TYP;

	
	public int[] hardware_RES_POINTS;

	
	public float[] hardware_RES_POINTS_LEFT;

	
	public int[] hardware_PRICE;

	
	public int[] hardware_DEV_COSTS;

	
	public int[] hardware_TECH;

	
	public int[] hardware_DATE_YEAR;

	
	public int[] hardware_DATE_MONTH;

	
	public bool[] hardware_UNLOCK;

	
	public bool[] hardware_ONLYSTATIONARY;

	
	public bool[] hardware_ONLYHANDHELD;

	
	public int[] hardware_NEED1;

	
	public int[] hardware_NEED2;

	
	public string[] hardware_NAME_EN;

	
	public string[] hardware_NAME_GE;

	
	public string[] hardware_NAME_TU;

	
	public string[] hardware_NAME_CH;

	
	public string[] hardware_NAME_FR;

	
	public string[] hardware_NAME_PB;

	
	public string[] hardware_NAME_CT;

	
	public string[] hardware_NAME_HU;

	
	public string[] hardware_NAME_ES;

	
	public string[] hardware_NAME_CZ;

	
	public string[] hardware_NAME_KO;

	
	public string[] hardware_NAME_AR;

	
	public string[] hardware_NAME_RU;

	
	public string[] hardware_NAME_IT;

	
	public string[] hardware_NAME_JA;

	
	public string[] hardware_NAME_PL;

	
	public string[] hardware_DESC_EN;

	
	public string[] hardware_DESC_GE;

	
	public string[] hardware_DESC_TU;

	
	public string[] hardware_DESC_CH;

	
	public string[] hardware_DESC_FR;

	
	public string[] hardware_DESC_PB;

	
	public string[] hardware_DESC_CT;

	
	public string[] hardware_DESC_HU;

	
	public string[] hardware_DESC_ES;

	
	public string[] hardware_DESC_CZ;

	
	public string[] hardware_DESC_KO;

	
	public string[] hardware_DESC_AR;

	
	public string[] hardware_DESC_RU;

	
	public string[] hardware_DESC_IT;

	
	public string[] hardware_DESC_JA;

	
	public string[] hardware_DESC_PL;

	
	private string[] data;
}
