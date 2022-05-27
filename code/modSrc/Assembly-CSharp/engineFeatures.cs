using System;
using System.IO;
using System.Text;
using UnityEngine;

// Token: 0x0200004F RID: 79
public class engineFeatures : MonoBehaviour
{
	// Token: 0x060001B9 RID: 441 RVA: 0x0001960D File Offset: 0x0001780D
	private void Awake()
	{
		this.FindScripts();
	}

	// Token: 0x060001BA RID: 442 RVA: 0x00019618 File Offset: 0x00017818
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
		if (!this.settings_)
		{
			this.settings_ = base.GetComponent<settingsScript>();
		}
		if (!this.genres_)
		{
			this.genres_ = base.GetComponent<genres>();
		}
		if (!this.games_)
		{
			this.games_ = base.GetComponent<games>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.mpCalls_)
		{
			this.mpCalls_ = GameObject.Find("NetworkManager").GetComponent<mpCalls>();
		}
	}

	// Token: 0x060001BB RID: 443 RVA: 0x000196E6 File Offset: 0x000178E6
	public void Init()
	{
		this.engineFeatures_PIC = new Sprite[this.engineFeatures_UNLOCK.Length];
	}

	// Token: 0x060001BC RID: 444 RVA: 0x000196FC File Offset: 0x000178FC
	public int GetOutdatetAmount(int usedFeature_)
	{
		int num = 0;
		for (int i = 0; i < this.engineFeatures_TYP.Length; i++)
		{
			if (this.engineFeatures_UNLOCK[i] && this.engineFeatures_TYP[i] == this.engineFeatures_TYP[usedFeature_] && this.engineFeatures_DATE_YEAR[usedFeature_] < this.engineFeatures_DATE_YEAR[i])
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x060001BD RID: 445 RVA: 0x00019754 File Offset: 0x00017954
	public void LoadEngineFeatures(string filename)
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
		this.engineFeatures_PIC = new Sprite[num2];
		this.engineFeatures_TYP = new int[num2];
		this.engineFeatures_RES_POINTS = new int[num2];
		this.engineFeatures_RES_POINTS_LEFT = new float[num2];
		this.engineFeatures_PRICE = new int[num2];
		this.engineFeatures_DEV_COSTS = new int[num2];
		this.engineFeatures_TECH = new int[num2];
		this.engineFeatures_GAMEPLAY = new int[num2];
		this.engineFeatures_GRAPHIC = new int[num2];
		this.engineFeatures_SOUND = new int[num2];
		this.engineFeatures_TECHNIK = new int[num2];
		this.engineFeatures_DATE_YEAR = new int[num2];
		this.engineFeatures_DATE_MONTH = new int[num2];
		this.engineFeatures_LEVEL = new int[num2];
		this.engineFeatures_UNLOCK = new bool[num2];
		this.engineFeatures_ICONFILE = new string[num2];
		this.engineFeatures_NAME_EN = new string[num2];
		this.engineFeatures_NAME_GE = new string[num2];
		this.engineFeatures_NAME_TU = new string[num2];
		this.engineFeatures_NAME_CH = new string[num2];
		this.engineFeatures_NAME_FR = new string[num2];
		this.engineFeatures_NAME_PB = new string[num2];
		this.engineFeatures_NAME_CT = new string[num2];
		this.engineFeatures_NAME_HU = new string[num2];
		this.engineFeatures_NAME_ES = new string[num2];
		this.engineFeatures_NAME_CZ = new string[num2];
		this.engineFeatures_NAME_KO = new string[num2];
		this.engineFeatures_NAME_AR = new string[num2];
		this.engineFeatures_NAME_RU = new string[num2];
		this.engineFeatures_NAME_IT = new string[num2];
		this.engineFeatures_NAME_JA = new string[num2];
		this.engineFeatures_NAME_PL = new string[num2];
		this.engineFeatures_DESC_EN = new string[num2];
		this.engineFeatures_DESC_GE = new string[num2];
		this.engineFeatures_DESC_TU = new string[num2];
		this.engineFeatures_DESC_CH = new string[num2];
		this.engineFeatures_DESC_FR = new string[num2];
		this.engineFeatures_DESC_PB = new string[num2];
		this.engineFeatures_DESC_CT = new string[num2];
		this.engineFeatures_DESC_HU = new string[num2];
		this.engineFeatures_DESC_ES = new string[num2];
		this.engineFeatures_DESC_CZ = new string[num2];
		this.engineFeatures_DESC_KO = new string[num2];
		this.engineFeatures_DESC_AR = new string[num2];
		this.engineFeatures_DESC_RU = new string[num2];
		this.engineFeatures_DESC_IT = new string[num2];
		this.engineFeatures_DESC_JA = new string[num2];
		this.engineFeatures_DESC_PL = new string[num2];
		int num3 = -1;
		for (int j = 0; j < this.data.Length; j++)
		{
			if (this.ParseData("[ID]", j))
			{
				num3 = int.Parse(this.data[j]);
			}
			if (this.ParseData("[TYP]", j))
			{
				this.engineFeatures_TYP[num3] = int.Parse(this.data[j]);
			}
			if (this.ParseData("[RES POINTS]", j))
			{
				this.engineFeatures_RES_POINTS[num3] = int.Parse(this.data[j]);
				this.engineFeatures_RES_POINTS_LEFT[num3] = (float)this.engineFeatures_RES_POINTS[num3];
			}
			if (this.ParseData("[PRICE]", j))
			{
				this.engineFeatures_PRICE[num3] = int.Parse(this.data[j]);
			}
			if (this.ParseData("[DEV COSTS]", j))
			{
				this.engineFeatures_DEV_COSTS[num3] = int.Parse(this.data[j]);
			}
			if (this.ParseData("[TECHLEVEL]", j))
			{
				this.engineFeatures_TECH[num3] = int.Parse(this.data[j]);
			}
			if (this.ParseData("[GAMEPLAY]", j))
			{
				this.engineFeatures_GAMEPLAY[num3] = int.Parse(this.data[j]);
			}
			if (this.ParseData("[GRAPHIC]", j))
			{
				this.engineFeatures_GRAPHIC[num3] = int.Parse(this.data[j]);
			}
			if (this.ParseData("[SOUND]", j))
			{
				this.engineFeatures_SOUND[num3] = int.Parse(this.data[j]);
			}
			if (this.ParseData("[TECH]", j))
			{
				this.engineFeatures_TECHNIK[num3] = int.Parse(this.data[j]);
			}
			if (this.ParseData("[DATE]", j))
			{
				if (this.ParseDataDontCutLastChar("JAN", j))
				{
					this.engineFeatures_DATE_MONTH[num3] = 1;
				}
				if (this.ParseDataDontCutLastChar("FEB", j))
				{
					this.engineFeatures_DATE_MONTH[num3] = 2;
				}
				if (this.ParseDataDontCutLastChar("MAR", j))
				{
					this.engineFeatures_DATE_MONTH[num3] = 3;
				}
				if (this.ParseDataDontCutLastChar("APR", j))
				{
					this.engineFeatures_DATE_MONTH[num3] = 4;
				}
				if (this.ParseDataDontCutLastChar("MAY", j))
				{
					this.engineFeatures_DATE_MONTH[num3] = 5;
				}
				if (this.ParseDataDontCutLastChar("JUN", j))
				{
					this.engineFeatures_DATE_MONTH[num3] = 6;
				}
				if (this.ParseDataDontCutLastChar("JUL", j))
				{
					this.engineFeatures_DATE_MONTH[num3] = 7;
				}
				if (this.ParseDataDontCutLastChar("AUG", j))
				{
					this.engineFeatures_DATE_MONTH[num3] = 8;
				}
				if (this.ParseDataDontCutLastChar("SEP", j))
				{
					this.engineFeatures_DATE_MONTH[num3] = 9;
				}
				if (this.ParseDataDontCutLastChar("OCT", j))
				{
					this.engineFeatures_DATE_MONTH[num3] = 10;
				}
				if (this.ParseDataDontCutLastChar("NOV", j))
				{
					this.engineFeatures_DATE_MONTH[num3] = 11;
				}
				if (this.ParseDataDontCutLastChar("DEC", j))
				{
					this.engineFeatures_DATE_MONTH[num3] = 12;
				}
				if (this.engineFeatures_DATE_MONTH[num3] <= 0)
				{
					Debug.Log("ERROR: EngineFeatures.txt -> Incorrect Month: " + this.engineFeatures_NAME_EN[num3]);
				}
				this.engineFeatures_DATE_YEAR[num3] = int.Parse(this.data[j]);
				if (this.engineFeatures_DATE_YEAR[num3] == 1976 && this.engineFeatures_DATE_MONTH[num3] == 1)
				{
					this.engineFeatures_UNLOCK[num3] = true;
				}
			}
			if (this.ParseData("[PIC]", j))
			{
				this.engineFeatures_ICONFILE[num3] = this.data[j];
			}
			if (this.ParseData("[NAME GE]", j))
			{
				this.engineFeatures_NAME_GE[num3] = this.data[j];
			}
			if (this.ParseData("[NAME EN]", j))
			{
				this.engineFeatures_NAME_EN[num3] = this.data[j];
			}
			if (this.ParseData("[NAME TU]", j))
			{
				this.engineFeatures_NAME_TU[num3] = this.data[j];
			}
			if (this.ParseData("[NAME CH]", j))
			{
				this.engineFeatures_NAME_CH[num3] = this.data[j];
			}
			if (this.ParseData("[NAME FR]", j))
			{
				this.engineFeatures_NAME_FR[num3] = this.data[j];
			}
			if (this.ParseData("[NAME PB]", j))
			{
				this.engineFeatures_NAME_PB[num3] = this.data[j];
			}
			if (this.ParseData("[NAME CT]", j))
			{
				this.engineFeatures_NAME_CT[num3] = this.data[j];
			}
			if (this.ParseData("[NAME HU]", j))
			{
				this.engineFeatures_NAME_HU[num3] = this.data[j];
			}
			if (this.ParseData("[NAME ES]", j))
			{
				this.engineFeatures_NAME_ES[num3] = this.data[j];
			}
			if (this.ParseData("[NAME CZ]", j))
			{
				this.engineFeatures_NAME_CZ[num3] = this.data[j];
			}
			if (this.ParseData("[NAME KO]", j))
			{
				this.engineFeatures_NAME_KO[num3] = this.data[j];
			}
			if (this.ParseData("[NAME AR]", j))
			{
				this.engineFeatures_NAME_AR[num3] = this.data[j];
			}
			if (this.ParseData("[NAME RU]", j))
			{
				this.engineFeatures_NAME_RU[num3] = this.data[j];
			}
			if (this.ParseData("[NAME IT]", j))
			{
				this.engineFeatures_NAME_IT[num3] = this.data[j];
			}
			if (this.ParseData("[NAME JA]", j))
			{
				this.engineFeatures_NAME_JA[num3] = this.data[j];
			}
			if (this.ParseData("[NAME PL]", j))
			{
				this.engineFeatures_NAME_PL[num3] = this.data[j];
			}
			if (this.ParseData("[DESC GE]", j))
			{
				this.engineFeatures_DESC_GE[num3] = this.data[j];
			}
			if (this.ParseData("[DESC EN]", j))
			{
				this.engineFeatures_DESC_EN[num3] = this.data[j];
			}
			if (this.ParseData("[DESC TU]", j))
			{
				this.engineFeatures_DESC_TU[num3] = this.data[j];
			}
			if (this.ParseData("[DESC CH]", j))
			{
				this.engineFeatures_DESC_CH[num3] = this.data[j];
			}
			if (this.ParseData("[DESC FR]", j))
			{
				this.engineFeatures_DESC_FR[num3] = this.data[j];
			}
			if (this.ParseData("[DESC PB]", j))
			{
				this.engineFeatures_DESC_PB[num3] = this.data[j];
			}
			if (this.ParseData("[DESC CT]", j))
			{
				this.engineFeatures_DESC_CT[num3] = this.data[j];
			}
			if (this.ParseData("[DESC HU]", j))
			{
				this.engineFeatures_DESC_HU[num3] = this.data[j];
			}
			if (this.ParseData("[DESC ES]", j))
			{
				this.engineFeatures_DESC_ES[num3] = this.data[j];
			}
			if (this.ParseData("[DESC CZ]", j))
			{
				this.engineFeatures_DESC_CZ[num3] = this.data[j];
			}
			if (this.ParseData("[DESC KO]", j))
			{
				this.engineFeatures_DESC_KO[num3] = this.data[j];
			}
			if (this.ParseData("[DESC AR]", j))
			{
				this.engineFeatures_DESC_AR[num3] = this.data[j];
			}
			if (this.ParseData("[DESC RU]", j))
			{
				this.engineFeatures_DESC_RU[num3] = this.data[j];
			}
			if (this.ParseData("[DESC IT]", j))
			{
				this.engineFeatures_DESC_IT[num3] = this.data[j];
			}
			if (this.ParseData("[DESC JA]", j))
			{
				this.engineFeatures_DESC_JA[num3] = this.data[j];
			}
			if (this.ParseData("[DESC PL]", j))
			{
				this.engineFeatures_DESC_PL[num3] = this.data[j];
			}
			if (this.ParseData("[EOF]", j))
			{
				break;
			}
			num++;
		}
	}

	// Token: 0x060001BE RID: 446 RVA: 0x0001A17C File Offset: 0x0001837C
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

	// Token: 0x060001BF RID: 447 RVA: 0x0001A1DC File Offset: 0x000183DC
	private bool ParseDataDontCutLastChar(string c, int i)
	{
		if (this.data[i].Contains(c))
		{
			this.data[i] = this.data[i].Replace(c, "");
			return true;
		}
		return false;
	}

	// Token: 0x060001C0 RID: 448 RVA: 0x0001A20C File Offset: 0x0001840C
	public string GetName(int i)
	{
		string text = "";
		switch (this.settings_.language)
		{
		case 0:
			text = this.engineFeatures_NAME_EN[i];
			goto IL_1CE;
		case 1:
			text = this.engineFeatures_NAME_GE[i];
			goto IL_1CE;
		case 2:
			if (this.engineFeatures_NAME_TU.Length != 0)
			{
				text = this.engineFeatures_NAME_TU[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 3:
			if (this.engineFeatures_NAME_CH.Length != 0)
			{
				text = this.engineFeatures_NAME_CH[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 4:
			if (this.engineFeatures_NAME_FR.Length != 0)
			{
				text = this.engineFeatures_NAME_FR[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 5:
			if (this.engineFeatures_NAME_ES.Length != 0)
			{
				text = this.engineFeatures_NAME_ES[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 6:
			if (this.engineFeatures_NAME_KO.Length != 0)
			{
				text = this.engineFeatures_NAME_KO[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 7:
			if (this.engineFeatures_NAME_PB.Length != 0)
			{
				text = this.engineFeatures_NAME_PB[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 8:
			if (this.engineFeatures_NAME_HU.Length != 0)
			{
				text = this.engineFeatures_NAME_HU[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 9:
			if (this.engineFeatures_NAME_RU.Length != 0)
			{
				text = this.engineFeatures_NAME_RU[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 10:
			if (this.engineFeatures_NAME_CT.Length != 0)
			{
				text = this.engineFeatures_NAME_CT[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 11:
			if (this.engineFeatures_NAME_PL.Length != 0)
			{
				text = this.engineFeatures_NAME_PL[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 12:
			if (this.engineFeatures_NAME_CZ.Length != 0)
			{
				text = this.engineFeatures_NAME_CZ[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 13:
			if (this.engineFeatures_NAME_AR.Length != 0)
			{
				text = this.engineFeatures_NAME_AR[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 14:
			if (this.engineFeatures_NAME_IT.Length != 0)
			{
				text = this.engineFeatures_NAME_IT[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 16:
			if (this.engineFeatures_NAME_JA.Length != 0)
			{
				text = this.engineFeatures_NAME_JA[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		}
		text = this.engineFeatures_NAME_EN[i];
		IL_1CE:
		if (text == null)
		{
			return this.engineFeatures_NAME_EN[i];
		}
		if (text.Length <= 0)
		{
			return this.engineFeatures_NAME_EN[i];
		}
		return text;
	}

	// Token: 0x060001C1 RID: 449 RVA: 0x0001A408 File Offset: 0x00018608
	public string GetDesc(int i)
	{
		string text = "";
		switch (this.settings_.language)
		{
		case 0:
			text = this.engineFeatures_DESC_EN[i];
			goto IL_1CE;
		case 1:
			text = this.engineFeatures_DESC_GE[i];
			goto IL_1CE;
		case 2:
			if (this.engineFeatures_DESC_TU.Length != 0)
			{
				text = this.engineFeatures_DESC_TU[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 3:
			if (this.engineFeatures_DESC_CH.Length != 0)
			{
				text = this.engineFeatures_DESC_CH[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 4:
			if (this.engineFeatures_DESC_FR.Length != 0)
			{
				text = this.engineFeatures_DESC_FR[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 5:
			if (this.engineFeatures_DESC_ES.Length != 0)
			{
				text = this.engineFeatures_DESC_ES[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 6:
			if (this.engineFeatures_DESC_KO.Length != 0)
			{
				text = this.engineFeatures_DESC_KO[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 7:
			if (this.engineFeatures_DESC_PB.Length != 0)
			{
				text = this.engineFeatures_DESC_PB[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 8:
			if (this.engineFeatures_DESC_HU.Length != 0)
			{
				text = this.engineFeatures_DESC_HU[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 9:
			if (this.engineFeatures_DESC_RU.Length != 0)
			{
				text = this.engineFeatures_DESC_RU[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 10:
			if (this.engineFeatures_DESC_CT.Length != 0)
			{
				text = this.engineFeatures_DESC_CT[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 11:
			if (this.engineFeatures_DESC_PL.Length != 0)
			{
				text = this.engineFeatures_DESC_PL[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 12:
			if (this.engineFeatures_DESC_CZ.Length != 0)
			{
				text = this.engineFeatures_DESC_CZ[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 13:
			if (this.engineFeatures_DESC_AR.Length != 0)
			{
				text = this.engineFeatures_DESC_AR[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 14:
			if (this.engineFeatures_DESC_IT.Length != 0)
			{
				text = this.engineFeatures_DESC_IT[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 16:
			if (this.engineFeatures_DESC_JA.Length != 0)
			{
				text = this.engineFeatures_DESC_JA[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		}
		text = this.engineFeatures_DESC_EN[i];
		IL_1CE:
		if (text == null)
		{
			return this.engineFeatures_DESC_EN[i];
		}
		if (text.Length <= 0)
		{
			return this.engineFeatures_DESC_EN[i];
		}
		return text;
	}

	// Token: 0x060001C2 RID: 450 RVA: 0x0001A604 File Offset: 0x00018804
	public int GetGameplay(int i)
	{
		float num = (float)this.engineFeatures_LEVEL[i] * 0.1f;
		num = (float)this.engineFeatures_GAMEPLAY[i] * (1f + num);
		return Mathf.RoundToInt(num);
	}

	// Token: 0x060001C3 RID: 451 RVA: 0x0001A63C File Offset: 0x0001883C
	public int GetGraphic(int i)
	{
		float num = (float)this.engineFeatures_LEVEL[i] * 0.1f;
		num = (float)this.engineFeatures_GRAPHIC[i] * (1f + num);
		return Mathf.RoundToInt(num);
	}

	// Token: 0x060001C4 RID: 452 RVA: 0x0001A674 File Offset: 0x00018874
	public int GetSound(int i)
	{
		float num = (float)this.engineFeatures_LEVEL[i] * 0.1f;
		num = (float)this.engineFeatures_SOUND[i] * (1f + num);
		return Mathf.RoundToInt(num);
	}

	// Token: 0x060001C5 RID: 453 RVA: 0x0001A6AC File Offset: 0x000188AC
	public int GetTechnik(int i)
	{
		float num = (float)this.engineFeatures_LEVEL[i] * 0.1f;
		num = (float)this.engineFeatures_TECHNIK[i] * (1f + num);
		return Mathf.RoundToInt(num);
	}

	// Token: 0x060001C6 RID: 454 RVA: 0x0001A6E4 File Offset: 0x000188E4
	public int GetDevCosts(int i)
	{
		float num = (float)this.engineFeatures_LEVEL[i] * 0.1f;
		num = (float)this.engineFeatures_DEV_COSTS[i] * (1f - num);
		return Mathf.RoundToInt(num);
	}

	// Token: 0x060001C7 RID: 455 RVA: 0x0001A71C File Offset: 0x0001891C
	public int GetDevCostsForEngine(int i)
	{
		float num = (float)this.mS_.difficulty;
		return Mathf.RoundToInt((float)(Mathf.RoundToInt((float)this.engineFeatures_DEV_COSTS[i] * (1.25f + num * 0.2f)) / 200 * 200));
	}

	// Token: 0x060001C8 RID: 456 RVA: 0x0001A764 File Offset: 0x00018964
	public int GetPrice(int i)
	{
		return this.engineFeatures_PRICE[i];
	}

	// Token: 0x060001C9 RID: 457 RVA: 0x0001A76E File Offset: 0x0001896E
	public int GetDevPointsForEngine(int i)
	{
		if (i == -1)
		{
			return 0;
		}
		return 10 + this.engineFeatures_RES_POINTS[i] / 5;
	}

	// Token: 0x060001CA RID: 458 RVA: 0x0001A783 File Offset: 0x00018983
	public int GetDevPointsForGame(int i)
	{
		if (i == -1)
		{
			return 0;
		}
		return 10 + this.engineFeatures_RES_POINTS[i] / 10;
	}

	// Token: 0x060001CB RID: 459 RVA: 0x0001A799 File Offset: 0x00018999
	public int GetTypGrafik()
	{
		return 0;
	}

	// Token: 0x060001CC RID: 460 RVA: 0x0001A79C File Offset: 0x0001899C
	public int GetTypSound()
	{
		return 1;
	}

	// Token: 0x060001CD RID: 461 RVA: 0x0001A79F File Offset: 0x0001899F
	public int GetTypKI()
	{
		return 2;
	}

	// Token: 0x060001CE RID: 462 RVA: 0x0001A7A2 File Offset: 0x000189A2
	public int GetTypPhysik()
	{
		return 3;
	}

	// Token: 0x060001CF RID: 463 RVA: 0x0001A7A5 File Offset: 0x000189A5
	public bool IsErforscht(int i)
	{
		return this.engineFeatures_RES_POINTS_LEFT[i] <= 0f;
	}

	// Token: 0x060001D0 RID: 464 RVA: 0x0001A7B9 File Offset: 0x000189B9
	public float GetProzent(int i)
	{
		return 100f / (float)this.engineFeatures_RES_POINTS[i] * ((float)this.engineFeatures_RES_POINTS[i] - this.engineFeatures_RES_POINTS_LEFT[i]);
	}

	// Token: 0x060001D1 RID: 465 RVA: 0x0001A7E0 File Offset: 0x000189E0
	public void UnlockAll()
	{
		for (int i = 0; i < this.engineFeatures_UNLOCK.Length; i++)
		{
			this.engineFeatures_UNLOCK[i] = true;
			this.engineFeatures_RES_POINTS_LEFT[i] = 0f;
		}
	}

	// Token: 0x060001D2 RID: 466 RVA: 0x0001A816 File Offset: 0x00018A16
	public bool ForschungGestartet(int i)
	{
		return this.engineFeatures_RES_POINTS_LEFT[i] != (float)this.engineFeatures_RES_POINTS[i];
	}

	// Token: 0x060001D3 RID: 467 RVA: 0x0001A82E File Offset: 0x00018A2E
	public bool Pay(int i)
	{
		if (!this.ForschungGestartet(i))
		{
			if (this.mS_.NotEnoughMoney(this.engineFeatures_PRICE[i]))
			{
				return false;
			}
			this.mS_.Pay((long)this.GetPrice(i), 2);
		}
		return true;
	}

	// Token: 0x060001D4 RID: 468 RVA: 0x0001A868 File Offset: 0x00018A68
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
					if (component2 && component2.slot == s && component2.typ == 2)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x060001D5 RID: 469 RVA: 0x0001A8F4 File Offset: 0x00018AF4
	public string GetTooltip(int i)
	{
		string text = "<b>" + this.GetName(i) + "</b>\n";
		switch (this.engineFeatures_TYP[i])
		{
		case 0:
			text = text + "<color=black>" + this.tS_.GetText(9) + "</color>";
			break;
		case 1:
			text = text + "<color=black>" + this.tS_.GetText(10) + "</color>";
			break;
		case 2:
			text = text + "<color=black>" + this.tS_.GetText(11) + "</color>";
			break;
		case 3:
			text = text + "<color=black>" + this.tS_.GetText(12) + "</color>";
			break;
		}
		text = string.Concat(new string[]
		{
			text,
			"\n<b><color=magenta>",
			this.tS_.GetText(4),
			" ",
			this.engineFeatures_TECH[i].ToString(),
			"</color></b>"
		});
		text = text + "\n\n" + this.GetDesc(i) + "\n";
		string text2 = this.tS_.GetText(254);
		text2 = text2.Replace("<NUM>", this.GetGameplay(i).ToString());
		text = text + "\n<b>" + text2 + "</b>";
		text2 = this.tS_.GetText(255);
		text2 = text2.Replace("<NUM>", this.GetGraphic(i).ToString());
		text = text + "\n<b>" + text2 + "</b>";
		text2 = this.tS_.GetText(256);
		text2 = text2.Replace("<NUM>", this.GetSound(i).ToString());
		text = text + "\n<b>" + text2 + "</b>";
		text2 = this.tS_.GetText(257);
		text2 = text2.Replace("<NUM>", this.GetTechnik(i).ToString());
		text = text + "\n<b>" + text2 + "</b>";
		return string.Concat(new string[]
		{
			text,
			"\n\n<b><color=red>",
			this.tS_.GetText(6),
			": ",
			this.mS_.GetMoney((long)this.GetDevCosts(i), true),
			"</color></b>"
		});
	}

	// Token: 0x060001D6 RID: 470 RVA: 0x0001AB64 File Offset: 0x00018D64
	public engineScript CreateEngine()
	{
		if (!this.mS_)
		{
			this.FindScripts();
		}
		engineScript component = UnityEngine.Object.Instantiate<GameObject>(this.prefabEngine).GetComponent<engineScript>();
		component.main_ = base.gameObject;
		component.mS_ = this.mS_;
		component.tS_ = this.tS_;
		component.eF_ = this;
		component.guiMain_ = this.guiMain_;
		component.settings_ = this.settings_;
		component.genres_ = this.genres_;
		component.games_ = this.games_;
		component.mpCalls_ = this.mpCalls_;
		return component;
	}

	// Token: 0x060001D7 RID: 471 RVA: 0x0001ABFC File Offset: 0x00018DFC
	public Sprite GetTypPic(int i)
	{
		if (this.engineFeatures_ICONFILE[i] == null)
		{
			return this.engineFeatures_PICTYP[this.engineFeatures_TYP[i]];
		}
		if (string.IsNullOrEmpty(this.engineFeatures_ICONFILE[i]))
		{
			return this.engineFeatures_PICTYP[this.engineFeatures_TYP[i]];
		}
		if (this.engineFeatures_ICONFILE[i].Length > 0)
		{
			if (this.engineFeatures_PIC[i] == null)
			{
				this.engineFeatures_PIC[i] = this.mS_.LoadPNG(Application.dataPath + "/Extern/Icons_EngineFeatures/" + this.engineFeatures_ICONFILE[i]);
			}
			return this.engineFeatures_PIC[i];
		}
		return this.engineFeatures_PICTYP[this.engineFeatures_TYP[i]];
	}

	// Token: 0x040003BE RID: 958
	private mainScript mS_;

	// Token: 0x040003BF RID: 959
	private textScript tS_;

	// Token: 0x040003C0 RID: 960
	private settingsScript settings_;

	// Token: 0x040003C1 RID: 961
	private GUI_Main guiMain_;

	// Token: 0x040003C2 RID: 962
	private genres genres_;

	// Token: 0x040003C3 RID: 963
	private games games_;

	// Token: 0x040003C4 RID: 964
	private mpCalls mpCalls_;

	// Token: 0x040003C5 RID: 965
	public GameObject prefabEngine;

	// Token: 0x040003C6 RID: 966
	private Sprite[] engineFeatures_PIC;

	// Token: 0x040003C7 RID: 967
	public Sprite[] engineFeatures_PICTYP;

	// Token: 0x040003C8 RID: 968
	public int[] engineFeatures_TYP;

	// Token: 0x040003C9 RID: 969
	public int[] engineFeatures_RES_POINTS;

	// Token: 0x040003CA RID: 970
	public float[] engineFeatures_RES_POINTS_LEFT;

	// Token: 0x040003CB RID: 971
	public int[] engineFeatures_PRICE;

	// Token: 0x040003CC RID: 972
	public int[] engineFeatures_DEV_COSTS;

	// Token: 0x040003CD RID: 973
	public int[] engineFeatures_TECH;

	// Token: 0x040003CE RID: 974
	public int[] engineFeatures_DATE_YEAR;

	// Token: 0x040003CF RID: 975
	public int[] engineFeatures_DATE_MONTH;

	// Token: 0x040003D0 RID: 976
	public int[] engineFeatures_GAMEPLAY;

	// Token: 0x040003D1 RID: 977
	public int[] engineFeatures_GRAPHIC;

	// Token: 0x040003D2 RID: 978
	public int[] engineFeatures_SOUND;

	// Token: 0x040003D3 RID: 979
	public int[] engineFeatures_TECHNIK;

	// Token: 0x040003D4 RID: 980
	public int[] engineFeatures_LEVEL;

	// Token: 0x040003D5 RID: 981
	public bool[] engineFeatures_UNLOCK;

	// Token: 0x040003D6 RID: 982
	public string[] engineFeatures_ICONFILE;

	// Token: 0x040003D7 RID: 983
	public string[] engineFeatures_NAME_EN;

	// Token: 0x040003D8 RID: 984
	public string[] engineFeatures_NAME_GE;

	// Token: 0x040003D9 RID: 985
	public string[] engineFeatures_NAME_TU;

	// Token: 0x040003DA RID: 986
	public string[] engineFeatures_NAME_CH;

	// Token: 0x040003DB RID: 987
	public string[] engineFeatures_NAME_FR;

	// Token: 0x040003DC RID: 988
	public string[] engineFeatures_NAME_PB;

	// Token: 0x040003DD RID: 989
	public string[] engineFeatures_NAME_CT;

	// Token: 0x040003DE RID: 990
	public string[] engineFeatures_NAME_HU;

	// Token: 0x040003DF RID: 991
	public string[] engineFeatures_NAME_ES;

	// Token: 0x040003E0 RID: 992
	public string[] engineFeatures_NAME_CZ;

	// Token: 0x040003E1 RID: 993
	public string[] engineFeatures_NAME_KO;

	// Token: 0x040003E2 RID: 994
	public string[] engineFeatures_NAME_AR;

	// Token: 0x040003E3 RID: 995
	public string[] engineFeatures_NAME_RU;

	// Token: 0x040003E4 RID: 996
	public string[] engineFeatures_NAME_IT;

	// Token: 0x040003E5 RID: 997
	public string[] engineFeatures_NAME_JA;

	// Token: 0x040003E6 RID: 998
	public string[] engineFeatures_NAME_PL;

	// Token: 0x040003E7 RID: 999
	public string[] engineFeatures_DESC_EN;

	// Token: 0x040003E8 RID: 1000
	public string[] engineFeatures_DESC_GE;

	// Token: 0x040003E9 RID: 1001
	public string[] engineFeatures_DESC_TU;

	// Token: 0x040003EA RID: 1002
	public string[] engineFeatures_DESC_CH;

	// Token: 0x040003EB RID: 1003
	public string[] engineFeatures_DESC_FR;

	// Token: 0x040003EC RID: 1004
	public string[] engineFeatures_DESC_PB;

	// Token: 0x040003ED RID: 1005
	public string[] engineFeatures_DESC_CT;

	// Token: 0x040003EE RID: 1006
	public string[] engineFeatures_DESC_HU;

	// Token: 0x040003EF RID: 1007
	public string[] engineFeatures_DESC_ES;

	// Token: 0x040003F0 RID: 1008
	public string[] engineFeatures_DESC_CZ;

	// Token: 0x040003F1 RID: 1009
	public string[] engineFeatures_DESC_KO;

	// Token: 0x040003F2 RID: 1010
	public string[] engineFeatures_DESC_AR;

	// Token: 0x040003F3 RID: 1011
	public string[] engineFeatures_DESC_RU;

	// Token: 0x040003F4 RID: 1012
	public string[] engineFeatures_DESC_IT;

	// Token: 0x040003F5 RID: 1013
	public string[] engineFeatures_DESC_JA;

	// Token: 0x040003F6 RID: 1014
	public string[] engineFeatures_DESC_PL;

	// Token: 0x040003F7 RID: 1015
	private string[] data;
}
