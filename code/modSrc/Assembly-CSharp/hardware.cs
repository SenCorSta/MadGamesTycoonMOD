using System;
using System.IO;
using System.Text;
using UnityEngine;

// Token: 0x0200005C RID: 92
public class hardware : MonoBehaviour
{
	// Token: 0x06000303 RID: 771 RVA: 0x000039BE File Offset: 0x00001BBE
	private void Awake()
	{
		this.FindScripts();
	}

	// Token: 0x06000304 RID: 772 RVA: 0x0004381C File Offset: 0x00041A1C
	private void FindScripts()
	{
		GameObject gameObject = GameObject.Find("Main");
		this.mS_ = gameObject.GetComponent<mainScript>();
		this.tS_ = gameObject.GetComponent<textScript>();
		this.settings_ = gameObject.GetComponent<settingsScript>();
	}

	// Token: 0x06000305 RID: 773 RVA: 0x000039C6 File Offset: 0x00001BC6
	public void Init()
	{
		this.hardware_PIC = new Sprite[this.hardware_UNLOCK.Length];
	}

	// Token: 0x06000306 RID: 774 RVA: 0x00043858 File Offset: 0x00041A58
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

	// Token: 0x06000307 RID: 775 RVA: 0x00044288 File Offset: 0x00042488
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

	// Token: 0x06000308 RID: 776 RVA: 0x000039DB File Offset: 0x00001BDB
	private bool ParseDataDontCutLastChar(string c, int i)
	{
		if (this.data[i].Contains(c))
		{
			this.data[i] = this.data[i].Replace(c, "");
			return true;
		}
		return false;
	}

	// Token: 0x06000309 RID: 777 RVA: 0x000442E8 File Offset: 0x000424E8
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

	// Token: 0x0600030A RID: 778 RVA: 0x000444E4 File Offset: 0x000426E4
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

	// Token: 0x0600030B RID: 779 RVA: 0x00003A0B File Offset: 0x00001C0B
	public int GetDevCosts(int i)
	{
		return this.hardware_DEV_COSTS[i];
	}

	// Token: 0x0600030C RID: 780 RVA: 0x00003A15 File Offset: 0x00001C15
	public int GetWorkPoints(int i)
	{
		return 100 + this.hardware_RES_POINTS[i];
	}

	// Token: 0x0600030D RID: 781 RVA: 0x00003A22 File Offset: 0x00001C22
	public int GetPrice(int i)
	{
		return this.hardware_PRICE[i];
	}

	// Token: 0x0600030E RID: 782 RVA: 0x00003A2C File Offset: 0x00001C2C
	public int GetPerformance(int i)
	{
		return Mathf.RoundToInt((float)(this.hardware_TECH[i] * (this.hardware_RES_POINTS[i] + 500) / 100));
	}

	// Token: 0x0600030F RID: 783 RVA: 0x00003A4E File Offset: 0x00001C4E
	public bool IsErforscht(int i)
	{
		return this.hardware_RES_POINTS_LEFT[i] <= 0f;
	}

	// Token: 0x06000310 RID: 784 RVA: 0x00003A62 File Offset: 0x00001C62
	public float GetProzent(int i)
	{
		return 100f / (float)this.hardware_RES_POINTS[i] * ((float)this.hardware_RES_POINTS[i] - this.hardware_RES_POINTS_LEFT[i]);
	}

	// Token: 0x06000311 RID: 785 RVA: 0x000446DC File Offset: 0x000428DC
	public void UnlockAll()
	{
		for (int i = 0; i < this.hardware_UNLOCK.Length; i++)
		{
			this.hardware_UNLOCK[i] = true;
		}
	}

	// Token: 0x06000312 RID: 786 RVA: 0x00003A86 File Offset: 0x00001C86
	public bool ForschungGestartet(int i)
	{
		return this.hardware_RES_POINTS_LEFT[i] != (float)this.hardware_RES_POINTS[i];
	}

	// Token: 0x06000313 RID: 787 RVA: 0x00003A9E File Offset: 0x00001C9E
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

	// Token: 0x06000314 RID: 788 RVA: 0x00044708 File Offset: 0x00042908
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

	// Token: 0x06000315 RID: 789 RVA: 0x00003AD5 File Offset: 0x00001CD5
	public string GetDateString(int i)
	{
		return this.hardware_DATE_YEAR[i].ToString() + " " + this.tS_.GetText(this.hardware_DATE_MONTH[i] + 220);
	}

	// Token: 0x06000316 RID: 790 RVA: 0x00044794 File Offset: 0x00042994
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

	// Token: 0x06000317 RID: 791 RVA: 0x0004497C File Offset: 0x00042B7C
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

	// Token: 0x06000318 RID: 792 RVA: 0x00044A24 File Offset: 0x00042C24
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

	// Token: 0x06000319 RID: 793 RVA: 0x00044B4C File Offset: 0x00042D4C
	public bool IsTechComponent(int i)
	{
		return this.hardware_TYP[i] == 0 || this.hardware_TYP[i] == 1 || this.hardware_TYP[i] == 2 || this.hardware_TYP[i] == 3 || this.hardware_TYP[i] == 4 || this.hardware_TYP[i] == 5 || this.hardware_TYP[i] == 6 || this.hardware_TYP[i] == 9;
	}

	// Token: 0x040005F1 RID: 1521
	private mainScript mS_;

	// Token: 0x040005F2 RID: 1522
	private textScript tS_;

	// Token: 0x040005F3 RID: 1523
	private settingsScript settings_;

	// Token: 0x040005F4 RID: 1524
	public const int component_cpu = 0;

	// Token: 0x040005F5 RID: 1525
	public const int component_gfx = 1;

	// Token: 0x040005F6 RID: 1526
	public const int component_ram = 2;

	// Token: 0x040005F7 RID: 1527
	public const int component_hdd = 3;

	// Token: 0x040005F8 RID: 1528
	public const int component_sfx = 4;

	// Token: 0x040005F9 RID: 1529
	public const int component_cooling = 5;

	// Token: 0x040005FA RID: 1530
	public const int component_disc = 6;

	// Token: 0x040005FB RID: 1531
	public const int component_controller = 7;

	// Token: 0x040005FC RID: 1532
	public const int component_case = 8;

	// Token: 0x040005FD RID: 1533
	public const int component_monitor = 9;

	// Token: 0x040005FE RID: 1534
	private Sprite[] hardware_PIC;

	// Token: 0x040005FF RID: 1535
	public Sprite[] hardware_PICTYP;

	// Token: 0x04000600 RID: 1536
	public string[] hardware_ICONFILE;

	// Token: 0x04000601 RID: 1537
	public int[] hardware_TYP;

	// Token: 0x04000602 RID: 1538
	public int[] hardware_RES_POINTS;

	// Token: 0x04000603 RID: 1539
	public float[] hardware_RES_POINTS_LEFT;

	// Token: 0x04000604 RID: 1540
	public int[] hardware_PRICE;

	// Token: 0x04000605 RID: 1541
	public int[] hardware_DEV_COSTS;

	// Token: 0x04000606 RID: 1542
	public int[] hardware_TECH;

	// Token: 0x04000607 RID: 1543
	public int[] hardware_DATE_YEAR;

	// Token: 0x04000608 RID: 1544
	public int[] hardware_DATE_MONTH;

	// Token: 0x04000609 RID: 1545
	public bool[] hardware_UNLOCK;

	// Token: 0x0400060A RID: 1546
	public bool[] hardware_ONLYSTATIONARY;

	// Token: 0x0400060B RID: 1547
	public bool[] hardware_ONLYHANDHELD;

	// Token: 0x0400060C RID: 1548
	public int[] hardware_NEED1;

	// Token: 0x0400060D RID: 1549
	public int[] hardware_NEED2;

	// Token: 0x0400060E RID: 1550
	public string[] hardware_NAME_EN;

	// Token: 0x0400060F RID: 1551
	public string[] hardware_NAME_GE;

	// Token: 0x04000610 RID: 1552
	public string[] hardware_NAME_TU;

	// Token: 0x04000611 RID: 1553
	public string[] hardware_NAME_CH;

	// Token: 0x04000612 RID: 1554
	public string[] hardware_NAME_FR;

	// Token: 0x04000613 RID: 1555
	public string[] hardware_NAME_PB;

	// Token: 0x04000614 RID: 1556
	public string[] hardware_NAME_CT;

	// Token: 0x04000615 RID: 1557
	public string[] hardware_NAME_HU;

	// Token: 0x04000616 RID: 1558
	public string[] hardware_NAME_ES;

	// Token: 0x04000617 RID: 1559
	public string[] hardware_NAME_CZ;

	// Token: 0x04000618 RID: 1560
	public string[] hardware_NAME_KO;

	// Token: 0x04000619 RID: 1561
	public string[] hardware_NAME_AR;

	// Token: 0x0400061A RID: 1562
	public string[] hardware_NAME_RU;

	// Token: 0x0400061B RID: 1563
	public string[] hardware_NAME_IT;

	// Token: 0x0400061C RID: 1564
	public string[] hardware_NAME_JA;

	// Token: 0x0400061D RID: 1565
	public string[] hardware_NAME_PL;

	// Token: 0x0400061E RID: 1566
	public string[] hardware_DESC_EN;

	// Token: 0x0400061F RID: 1567
	public string[] hardware_DESC_GE;

	// Token: 0x04000620 RID: 1568
	public string[] hardware_DESC_TU;

	// Token: 0x04000621 RID: 1569
	public string[] hardware_DESC_CH;

	// Token: 0x04000622 RID: 1570
	public string[] hardware_DESC_FR;

	// Token: 0x04000623 RID: 1571
	public string[] hardware_DESC_PB;

	// Token: 0x04000624 RID: 1572
	public string[] hardware_DESC_CT;

	// Token: 0x04000625 RID: 1573
	public string[] hardware_DESC_HU;

	// Token: 0x04000626 RID: 1574
	public string[] hardware_DESC_ES;

	// Token: 0x04000627 RID: 1575
	public string[] hardware_DESC_CZ;

	// Token: 0x04000628 RID: 1576
	public string[] hardware_DESC_KO;

	// Token: 0x04000629 RID: 1577
	public string[] hardware_DESC_AR;

	// Token: 0x0400062A RID: 1578
	public string[] hardware_DESC_RU;

	// Token: 0x0400062B RID: 1579
	public string[] hardware_DESC_IT;

	// Token: 0x0400062C RID: 1580
	public string[] hardware_DESC_JA;

	// Token: 0x0400062D RID: 1581
	public string[] hardware_DESC_PL;

	// Token: 0x0400062E RID: 1582
	private string[] data;
}
