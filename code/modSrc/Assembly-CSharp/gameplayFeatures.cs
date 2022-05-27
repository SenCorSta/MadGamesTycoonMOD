using System;
using System.IO;
using System.Text;
using UnityEngine;

// Token: 0x02000055 RID: 85
public class gameplayFeatures : MonoBehaviour
{
	// Token: 0x0600027C RID: 636 RVA: 0x00028867 File Offset: 0x00026A67
	private void Awake()
	{
		this.FindScripts();
	}

	// Token: 0x0600027D RID: 637 RVA: 0x00028870 File Offset: 0x00026A70
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
	}

	// Token: 0x0600027E RID: 638 RVA: 0x000288E1 File Offset: 0x00026AE1
	public void Init()
	{
		this.gameplayFeatures_PIC = new Sprite[this.gameplayFeatures_UNLOCK.Length];
	}

	// Token: 0x0600027F RID: 639 RVA: 0x000288F8 File Offset: 0x00026AF8
	public bool[] Return1DimensionArray_GOOD()
	{
		int num = this.gameplayFeatures_UNLOCK.Length;
		int num2 = this.genres_.genres_UNLOCK.Length;
		bool[] array = new bool[num * num2];
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num2; j++)
			{
				array[i * num2 + j] = this.gameplayFeatures_GOOD[i, j];
			}
		}
		return array;
	}

	// Token: 0x06000280 RID: 640 RVA: 0x00028958 File Offset: 0x00026B58
	public bool[] Return1DimensionArray_BAD()
	{
		int num = this.gameplayFeatures_UNLOCK.Length;
		int num2 = this.genres_.genres_UNLOCK.Length;
		bool[] array = new bool[num * num2];
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num2; j++)
			{
				array[i * num2 + j] = this.gameplayFeatures_BAD[i, j];
			}
		}
		return array;
	}

	// Token: 0x06000281 RID: 641 RVA: 0x000289B8 File Offset: 0x00026BB8
	public bool[] Return1DimensionArray_LOCKPLATFORM()
	{
		int num = this.gameplayFeatures_UNLOCK.Length;
		int num2 = 5;
		bool[] array = new bool[num * num2];
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num2; j++)
			{
				array[i * num2 + j] = this.gameplayFeatures_LOCKPLATFORM[i, j];
			}
		}
		return array;
	}

	// Token: 0x06000282 RID: 642 RVA: 0x00028A0C File Offset: 0x00026C0C
	public void Copy2DimensionArray_GOOD(bool[] arr)
	{
		int num = this.gameplayFeatures_UNLOCK.Length;
		int num2 = arr.Length / num;
		this.gameplayFeatures_GOOD = new bool[num, num2];
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num2; j++)
			{
				this.gameplayFeatures_GOOD[i, j] = arr[i * num2 + j];
			}
		}
	}

	// Token: 0x06000283 RID: 643 RVA: 0x00028A64 File Offset: 0x00026C64
	public void Copy2DimensionArray_BAD(bool[] arr)
	{
		int num = this.gameplayFeatures_UNLOCK.Length;
		int num2 = arr.Length / num;
		this.gameplayFeatures_BAD = new bool[num, num2];
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num2; j++)
			{
				this.gameplayFeatures_BAD[i, j] = arr[i * num2 + j];
			}
		}
	}

	// Token: 0x06000284 RID: 644 RVA: 0x00028ABC File Offset: 0x00026CBC
	public void Copy2DimensionArray_LOCKPLATFORM(bool[] arr)
	{
		int num = this.gameplayFeatures_UNLOCK.Length;
		int num2 = arr.Length / num;
		this.gameplayFeatures_LOCKPLATFORM = new bool[num, num2];
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num2; j++)
			{
				this.gameplayFeatures_LOCKPLATFORM[i, j] = arr[i * num2 + j];
			}
		}
	}

	// Token: 0x06000285 RID: 645 RVA: 0x00028B14 File Offset: 0x00026D14
	public void LoadGameplayFeatures(string filename)
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
		this.gameplayFeatures_PIC = new Sprite[num2];
		this.gameplayFeatures_TYP = new int[num2];
		this.gameplayFeatures_RES_POINTS = new int[num2];
		this.gameplayFeatures_RES_POINTS_LEFT = new float[num2];
		this.gameplayFeatures_PRICE = new int[num2];
		this.gameplayFeatures_DEV_COSTS = new int[num2];
		this.gameplayFeatures_GAMEPLAY = new int[num2];
		this.gameplayFeatures_GRAPHIC = new int[num2];
		this.gameplayFeatures_SOUND = new int[num2];
		this.gameplayFeatures_TECHNIK = new int[num2];
		this.gameplayFeatures_DATE_YEAR = new int[num2];
		this.gameplayFeatures_DATE_MONTH = new int[num2];
		this.gameplayFeatures_LEVEL = new int[num2];
		this.gameplayFeatures_UNLOCK = new bool[num2];
		this.gameplayFeatures_ICONFILE = new string[num2];
		this.gameplayFeatures_GOOD = new bool[num2, this.genres_.genres_UNLOCK.Length];
		this.gameplayFeatures_BAD = new bool[num2, this.genres_.genres_UNLOCK.Length];
		this.gameplayFeatures_LOCKPLATFORM = new bool[num2, 5];
		this.gameplayFeatures_NAME_EN = new string[num2];
		this.gameplayFeatures_NAME_GE = new string[num2];
		this.gameplayFeatures_NAME_TU = new string[num2];
		this.gameplayFeatures_NAME_CH = new string[num2];
		this.gameplayFeatures_NAME_FR = new string[num2];
		this.gameplayFeatures_NAME_PB = new string[num2];
		this.gameplayFeatures_NAME_CT = new string[num2];
		this.gameplayFeatures_NAME_HU = new string[num2];
		this.gameplayFeatures_NAME_ES = new string[num2];
		this.gameplayFeatures_NAME_CZ = new string[num2];
		this.gameplayFeatures_NAME_KO = new string[num2];
		this.gameplayFeatures_NAME_RU = new string[num2];
		this.gameplayFeatures_NAME_IT = new string[num2];
		this.gameplayFeatures_NAME_AR = new string[num2];
		this.gameplayFeatures_NAME_JA = new string[num2];
		this.gameplayFeatures_NAME_PL = new string[num2];
		this.gameplayFeatures_DESC_EN = new string[num2];
		this.gameplayFeatures_DESC_GE = new string[num2];
		this.gameplayFeatures_DESC_TU = new string[num2];
		this.gameplayFeatures_DESC_CH = new string[num2];
		this.gameplayFeatures_DESC_FR = new string[num2];
		this.gameplayFeatures_DESC_PB = new string[num2];
		this.gameplayFeatures_DESC_CT = new string[num2];
		this.gameplayFeatures_DESC_HU = new string[num2];
		this.gameplayFeatures_DESC_ES = new string[num2];
		this.gameplayFeatures_DESC_CZ = new string[num2];
		this.gameplayFeatures_DESC_KO = new string[num2];
		this.gameplayFeatures_DESC_RU = new string[num2];
		this.gameplayFeatures_DESC_IT = new string[num2];
		this.gameplayFeatures_DESC_AR = new string[num2];
		this.gameplayFeatures_DESC_JA = new string[num2];
		this.gameplayFeatures_DESC_PL = new string[num2];
		int num3 = -1;
		for (int j = 0; j < this.data.Length; j++)
		{
			if (this.ParseData("[ID]", j))
			{
				num3 = int.Parse(this.data[j]);
			}
			if (this.ParseData("[TYP]", j))
			{
				this.gameplayFeatures_TYP[num3] = int.Parse(this.data[j]);
			}
			if (this.ParseData("[RES POINTS]", j))
			{
				this.gameplayFeatures_RES_POINTS[num3] = int.Parse(this.data[j]);
				this.gameplayFeatures_RES_POINTS_LEFT[num3] = (float)this.gameplayFeatures_RES_POINTS[num3];
			}
			if (this.ParseData("[PRICE]", j))
			{
				this.gameplayFeatures_PRICE[num3] = int.Parse(this.data[j]);
			}
			if (this.ParseData("[DEV COSTS]", j))
			{
				this.gameplayFeatures_DEV_COSTS[num3] = int.Parse(this.data[j]);
			}
			if (this.ParseData("[GAMEPLAY]", j))
			{
				this.gameplayFeatures_GAMEPLAY[num3] = int.Parse(this.data[j]);
			}
			if (this.ParseData("[GRAPHIC]", j))
			{
				this.gameplayFeatures_GRAPHIC[num3] = int.Parse(this.data[j]);
			}
			if (this.ParseData("[SOUND]", j))
			{
				this.gameplayFeatures_SOUND[num3] = int.Parse(this.data[j]);
			}
			if (this.ParseData("[TECH]", j))
			{
				this.gameplayFeatures_TECHNIK[num3] = int.Parse(this.data[j]);
			}
			if (this.ParseData("[NO_ARCADE]", j))
			{
				this.gameplayFeatures_LOCKPLATFORM[num3, 4] = true;
			}
			if (this.ParseData("[NO_MOBILE]", j))
			{
				this.gameplayFeatures_LOCKPLATFORM[num3, 3] = true;
			}
			if (this.ParseData("[DATE]", j))
			{
				if (this.ParseDataDontCutLastChar("JAN", j))
				{
					this.gameplayFeatures_DATE_MONTH[num3] = 1;
				}
				if (this.ParseDataDontCutLastChar("FEB", j))
				{
					this.gameplayFeatures_DATE_MONTH[num3] = 2;
				}
				if (this.ParseDataDontCutLastChar("MAR", j))
				{
					this.gameplayFeatures_DATE_MONTH[num3] = 3;
				}
				if (this.ParseDataDontCutLastChar("APR", j))
				{
					this.gameplayFeatures_DATE_MONTH[num3] = 4;
				}
				if (this.ParseDataDontCutLastChar("MAY", j))
				{
					this.gameplayFeatures_DATE_MONTH[num3] = 5;
				}
				if (this.ParseDataDontCutLastChar("JUN", j))
				{
					this.gameplayFeatures_DATE_MONTH[num3] = 6;
				}
				if (this.ParseDataDontCutLastChar("JUL", j))
				{
					this.gameplayFeatures_DATE_MONTH[num3] = 7;
				}
				if (this.ParseDataDontCutLastChar("AUG", j))
				{
					this.gameplayFeatures_DATE_MONTH[num3] = 8;
				}
				if (this.ParseDataDontCutLastChar("SEP", j))
				{
					this.gameplayFeatures_DATE_MONTH[num3] = 9;
				}
				if (this.ParseDataDontCutLastChar("OCT", j))
				{
					this.gameplayFeatures_DATE_MONTH[num3] = 10;
				}
				if (this.ParseDataDontCutLastChar("NOV", j))
				{
					this.gameplayFeatures_DATE_MONTH[num3] = 11;
				}
				if (this.ParseDataDontCutLastChar("DEC", j))
				{
					this.gameplayFeatures_DATE_MONTH[num3] = 12;
				}
				if (this.gameplayFeatures_DATE_MONTH[num3] <= 0)
				{
					Debug.Log("ERROR: GameplayFeatures.txt -> Incorrect Month: " + this.gameplayFeatures_NAME_EN[num3]);
				}
				this.gameplayFeatures_DATE_YEAR[num3] = int.Parse(this.data[j]);
				if (this.gameplayFeatures_DATE_YEAR[num3] == 1976 && this.gameplayFeatures_DATE_MONTH[num3] == 1)
				{
					this.gameplayFeatures_UNLOCK[num3] = true;
				}
			}
			if (this.ParseData("[PIC]", j))
			{
				this.gameplayFeatures_ICONFILE[num3] = this.data[j];
			}
			if (this.ParseData("[GOOD]", j))
			{
				for (int k = 0; k < this.genres_.genres_UNLOCK.Length; k++)
				{
					if (this.data[j].Contains("<" + k.ToString() + ">"))
					{
						this.gameplayFeatures_GOOD[num3, k] = true;
					}
				}
			}
			if (this.ParseData("[BAD]", j))
			{
				for (int l = 0; l < this.genres_.genres_UNLOCK.Length; l++)
				{
					if (this.data[j].Contains("<" + l.ToString() + ">"))
					{
						this.gameplayFeatures_BAD[num3, l] = true;
					}
				}
			}
			if (this.ParseData("[NAME GE]", j))
			{
				this.gameplayFeatures_NAME_GE[num3] = this.data[j];
			}
			if (this.ParseData("[NAME EN]", j))
			{
				this.gameplayFeatures_NAME_EN[num3] = this.data[j];
			}
			if (this.ParseData("[NAME TU]", j))
			{
				this.gameplayFeatures_NAME_TU[num3] = this.data[j];
			}
			if (this.ParseData("[NAME CH]", j))
			{
				this.gameplayFeatures_NAME_CH[num3] = this.data[j];
			}
			if (this.ParseData("[NAME FR]", j))
			{
				this.gameplayFeatures_NAME_FR[num3] = this.data[j];
			}
			if (this.ParseData("[NAME PB]", j))
			{
				this.gameplayFeatures_NAME_PB[num3] = this.data[j];
			}
			if (this.ParseData("[NAME CT]", j))
			{
				this.gameplayFeatures_NAME_CT[num3] = this.data[j];
			}
			if (this.ParseData("[NAME HU]", j))
			{
				this.gameplayFeatures_NAME_HU[num3] = this.data[j];
			}
			if (this.ParseData("[NAME ES]", j))
			{
				this.gameplayFeatures_NAME_ES[num3] = this.data[j];
			}
			if (this.ParseData("[NAME CZ]", j))
			{
				this.gameplayFeatures_NAME_CZ[num3] = this.data[j];
			}
			if (this.ParseData("[NAME KO]", j))
			{
				this.gameplayFeatures_NAME_KO[num3] = this.data[j];
			}
			if (this.ParseData("[NAME RU]", j))
			{
				this.gameplayFeatures_NAME_RU[num3] = this.data[j];
			}
			if (this.ParseData("[NAME IT]", j))
			{
				this.gameplayFeatures_NAME_IT[num3] = this.data[j];
			}
			if (this.ParseData("[NAME AR]", j))
			{
				this.gameplayFeatures_NAME_AR[num3] = this.data[j];
			}
			if (this.ParseData("[NAME JA]", j))
			{
				this.gameplayFeatures_NAME_JA[num3] = this.data[j];
			}
			if (this.ParseData("[NAME PL]", j))
			{
				this.gameplayFeatures_NAME_PL[num3] = this.data[j];
			}
			if (this.ParseData("[DESC GE]", j))
			{
				this.gameplayFeatures_DESC_GE[num3] = this.data[j];
			}
			if (this.ParseData("[DESC EN]", j))
			{
				this.gameplayFeatures_DESC_EN[num3] = this.data[j];
			}
			if (this.ParseData("[DESC TU]", j))
			{
				this.gameplayFeatures_DESC_TU[num3] = this.data[j];
			}
			if (this.ParseData("[DESC CH]", j))
			{
				this.gameplayFeatures_DESC_CH[num3] = this.data[j];
			}
			if (this.ParseData("[DESC FR]", j))
			{
				this.gameplayFeatures_DESC_FR[num3] = this.data[j];
			}
			if (this.ParseData("[DESC PB]", j))
			{
				this.gameplayFeatures_DESC_PB[num3] = this.data[j];
			}
			if (this.ParseData("[DESC CT]", j))
			{
				this.gameplayFeatures_DESC_CT[num3] = this.data[j];
			}
			if (this.ParseData("[DESC HU]", j))
			{
				this.gameplayFeatures_DESC_HU[num3] = this.data[j];
			}
			if (this.ParseData("[DESC ES]", j))
			{
				this.gameplayFeatures_DESC_ES[num3] = this.data[j];
			}
			if (this.ParseData("[DESC CZ]", j))
			{
				this.gameplayFeatures_DESC_CZ[num3] = this.data[j];
			}
			if (this.ParseData("[DESC KO]", j))
			{
				this.gameplayFeatures_DESC_KO[num3] = this.data[j];
			}
			if (this.ParseData("[DESC RU]", j))
			{
				this.gameplayFeatures_DESC_RU[num3] = this.data[j];
			}
			if (this.ParseData("[DESC IT]", j))
			{
				this.gameplayFeatures_DESC_IT[num3] = this.data[j];
			}
			if (this.ParseData("[DESC AR]", j))
			{
				this.gameplayFeatures_DESC_AR[num3] = this.data[j];
			}
			if (this.ParseData("[DESC JA]", j))
			{
				this.gameplayFeatures_DESC_JA[num3] = this.data[j];
			}
			if (this.ParseData("[DESC PL]", j))
			{
				this.gameplayFeatures_DESC_PL[num3] = this.data[j];
			}
			if (this.ParseData("[EOF]", j))
			{
				break;
			}
			num++;
		}
	}

	// Token: 0x06000286 RID: 646 RVA: 0x00029644 File Offset: 0x00027844
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

	// Token: 0x06000287 RID: 647 RVA: 0x000296A4 File Offset: 0x000278A4
	private bool ParseDataDontCutLastChar(string c, int i)
	{
		if (this.data[i].Contains(c))
		{
			this.data[i] = this.data[i].Replace(c, "");
			return true;
		}
		return false;
	}

	// Token: 0x06000288 RID: 648 RVA: 0x000296D4 File Offset: 0x000278D4
	public string GetName(int i)
	{
		string text = "";
		switch (this.settings_.language)
		{
		case 0:
			text = this.gameplayFeatures_NAME_EN[i];
			goto IL_1CE;
		case 1:
			text = this.gameplayFeatures_NAME_GE[i];
			goto IL_1CE;
		case 2:
			if (this.gameplayFeatures_NAME_TU.Length != 0)
			{
				text = this.gameplayFeatures_NAME_TU[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 3:
			if (this.gameplayFeatures_NAME_CH.Length != 0)
			{
				text = this.gameplayFeatures_NAME_CH[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 4:
			if (this.gameplayFeatures_NAME_FR.Length != 0)
			{
				text = this.gameplayFeatures_NAME_FR[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 5:
			if (this.gameplayFeatures_NAME_ES.Length != 0)
			{
				text = this.gameplayFeatures_NAME_ES[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 6:
			if (this.gameplayFeatures_NAME_KO.Length != 0)
			{
				text = this.gameplayFeatures_NAME_KO[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 7:
			if (this.gameplayFeatures_NAME_PB.Length != 0)
			{
				text = this.gameplayFeatures_NAME_PB[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 8:
			if (this.gameplayFeatures_NAME_HU.Length != 0)
			{
				text = this.gameplayFeatures_NAME_HU[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 9:
			if (this.gameplayFeatures_NAME_RU.Length != 0)
			{
				text = this.gameplayFeatures_NAME_RU[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 10:
			if (this.gameplayFeatures_NAME_CT.Length != 0)
			{
				text = this.gameplayFeatures_NAME_CT[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 11:
			if (this.gameplayFeatures_NAME_PL.Length != 0)
			{
				text = this.gameplayFeatures_NAME_PL[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 12:
			if (this.gameplayFeatures_NAME_CZ.Length != 0)
			{
				text = this.gameplayFeatures_NAME_CZ[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 13:
			if (this.gameplayFeatures_NAME_AR.Length != 0)
			{
				text = this.gameplayFeatures_NAME_AR[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 14:
			if (this.gameplayFeatures_NAME_IT.Length != 0)
			{
				text = this.gameplayFeatures_NAME_IT[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 16:
			if (this.gameplayFeatures_NAME_JA.Length != 0)
			{
				text = this.gameplayFeatures_NAME_JA[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		}
		text = this.gameplayFeatures_NAME_EN[i];
		IL_1CE:
		if (text == null)
		{
			return this.gameplayFeatures_NAME_EN[i];
		}
		if (text.Length <= 0)
		{
			return this.gameplayFeatures_NAME_EN[i];
		}
		return text;
	}

	// Token: 0x06000289 RID: 649 RVA: 0x000298D0 File Offset: 0x00027AD0
	public string GetDesc(int i)
	{
		string text = "";
		switch (this.settings_.language)
		{
		case 0:
			text = this.gameplayFeatures_DESC_EN[i];
			goto IL_1CE;
		case 1:
			text = this.gameplayFeatures_DESC_GE[i];
			goto IL_1CE;
		case 2:
			if (this.gameplayFeatures_DESC_TU.Length != 0)
			{
				text = this.gameplayFeatures_DESC_TU[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 3:
			if (this.gameplayFeatures_DESC_CH.Length != 0)
			{
				text = this.gameplayFeatures_DESC_CH[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 4:
			if (this.gameplayFeatures_DESC_FR.Length != 0)
			{
				text = this.gameplayFeatures_DESC_FR[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 5:
			if (this.gameplayFeatures_DESC_ES.Length != 0)
			{
				text = this.gameplayFeatures_DESC_ES[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 6:
			if (this.gameplayFeatures_DESC_KO.Length != 0)
			{
				text = this.gameplayFeatures_DESC_KO[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 7:
			if (this.gameplayFeatures_DESC_PB.Length != 0)
			{
				text = this.gameplayFeatures_DESC_PB[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 8:
			if (this.gameplayFeatures_DESC_HU.Length != 0)
			{
				text = this.gameplayFeatures_DESC_HU[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 9:
			if (this.gameplayFeatures_DESC_RU.Length != 0)
			{
				text = this.gameplayFeatures_DESC_RU[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 10:
			if (this.gameplayFeatures_DESC_CT.Length != 0)
			{
				text = this.gameplayFeatures_DESC_CT[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 11:
			if (this.gameplayFeatures_DESC_PL.Length != 0)
			{
				text = this.gameplayFeatures_DESC_PL[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 12:
			if (this.gameplayFeatures_DESC_CZ.Length != 0)
			{
				text = this.gameplayFeatures_DESC_CZ[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 13:
			if (this.gameplayFeatures_DESC_AR.Length != 0)
			{
				text = this.gameplayFeatures_DESC_AR[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 14:
			if (this.gameplayFeatures_DESC_IT.Length != 0)
			{
				text = this.gameplayFeatures_DESC_IT[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		case 16:
			if (this.gameplayFeatures_DESC_JA.Length != 0)
			{
				text = this.gameplayFeatures_DESC_JA[i];
				goto IL_1CE;
			}
			goto IL_1CE;
		}
		text = this.gameplayFeatures_DESC_EN[i];
		IL_1CE:
		if (text == null)
		{
			return this.gameplayFeatures_DESC_EN[i];
		}
		if (text.Length <= 0)
		{
			return this.gameplayFeatures_DESC_EN[i];
		}
		return text;
	}

	// Token: 0x0600028A RID: 650 RVA: 0x00029ACA File Offset: 0x00027CCA
	public float GetBonus(int i, int genre_)
	{
		if (genre_ != -1)
		{
			if (this.gameplayFeatures_GOOD[i, genre_])
			{
				return 1.5f;
			}
			if (this.gameplayFeatures_BAD[i, genre_])
			{
				return 0.1f;
			}
		}
		return 1f;
	}

	// Token: 0x0600028B RID: 651 RVA: 0x00029B00 File Offset: 0x00027D00
	public int GetGameplay(int i, int genre_)
	{
		if (this.gameplayFeatures_GAMEPLAY[i] <= 0)
		{
			return this.gameplayFeatures_GAMEPLAY[i];
		}
		float num = (float)this.gameplayFeatures_LEVEL[i] * 0.1f;
		num = (float)this.gameplayFeatures_GAMEPLAY[i] * (1f + num);
		num *= this.GetBonus(i, genre_);
		return Mathf.RoundToInt(num);
	}

	// Token: 0x0600028C RID: 652 RVA: 0x00029B54 File Offset: 0x00027D54
	public int GetGraphic(int i, int genre_)
	{
		if (this.gameplayFeatures_GRAPHIC[i] <= 0)
		{
			return this.gameplayFeatures_GRAPHIC[i];
		}
		float num = (float)this.gameplayFeatures_LEVEL[i] * 0.1f;
		num = (float)this.gameplayFeatures_GRAPHIC[i] * (1f + num);
		num *= this.GetBonus(i, genre_);
		return Mathf.RoundToInt(num);
	}

	// Token: 0x0600028D RID: 653 RVA: 0x00029BA8 File Offset: 0x00027DA8
	public int GetSound(int i, int genre_)
	{
		if (this.gameplayFeatures_SOUND[i] <= 0)
		{
			return this.gameplayFeatures_SOUND[i];
		}
		float num = (float)this.gameplayFeatures_LEVEL[i] * 0.1f;
		num = (float)this.gameplayFeatures_SOUND[i] * (1f + num);
		num *= this.GetBonus(i, genre_);
		return Mathf.RoundToInt(num);
	}

	// Token: 0x0600028E RID: 654 RVA: 0x00029BFC File Offset: 0x00027DFC
	public int GetTechnik(int i, int genre_)
	{
		if (this.gameplayFeatures_TECHNIK[i] <= 0)
		{
			return this.gameplayFeatures_TECHNIK[i];
		}
		float num = (float)this.gameplayFeatures_LEVEL[i] * 0.1f;
		num = (float)this.gameplayFeatures_TECHNIK[i] * (1f + num);
		num *= this.GetBonus(i, genre_);
		return Mathf.RoundToInt(num);
	}

	// Token: 0x0600028F RID: 655 RVA: 0x00029C50 File Offset: 0x00027E50
	public int GetDevCosts(int i)
	{
		float num = (float)this.gameplayFeatures_LEVEL[i] * 0.1f;
		num = (float)this.gameplayFeatures_DEV_COSTS[i] * (1f - num);
		return Mathf.RoundToInt(num);
	}

	// Token: 0x06000290 RID: 656 RVA: 0x0001A799 File Offset: 0x00018999
	public int GetTypGrafik()
	{
		return 0;
	}

	// Token: 0x06000291 RID: 657 RVA: 0x0001A79C File Offset: 0x0001899C
	public int GetTypSound()
	{
		return 1;
	}

	// Token: 0x06000292 RID: 658 RVA: 0x0001A79F File Offset: 0x0001899F
	public int GetTypKI()
	{
		return 2;
	}

	// Token: 0x06000293 RID: 659 RVA: 0x0001A7A2 File Offset: 0x000189A2
	public int GetTypPhysik()
	{
		return 3;
	}

	// Token: 0x06000294 RID: 660 RVA: 0x00029C85 File Offset: 0x00027E85
	public int GetTypGameplay()
	{
		return 4;
	}

	// Token: 0x06000295 RID: 661 RVA: 0x00029C88 File Offset: 0x00027E88
	public int GetTypSteuerung()
	{
		return 5;
	}

	// Token: 0x06000296 RID: 662 RVA: 0x00029C8B File Offset: 0x00027E8B
	public int GetTypMultiplayer()
	{
		return 6;
	}

	// Token: 0x06000297 RID: 663 RVA: 0x00029C90 File Offset: 0x00027E90
	public Sprite GetTypSprite(int i)
	{
		if (this.gameplayFeatures_ICONFILE[i] == null)
		{
			return this.gameplayfeatures_PICTYP[this.gameplayFeatures_TYP[i]];
		}
		if (string.IsNullOrEmpty(this.gameplayFeatures_ICONFILE[i]))
		{
			return this.gameplayfeatures_PICTYP[this.gameplayFeatures_TYP[i]];
		}
		if (this.gameplayFeatures_ICONFILE[i].Length > 0)
		{
			if (this.gameplayFeatures_PIC[i] == null)
			{
				this.gameplayFeatures_PIC[i] = this.mS_.LoadPNG(Application.dataPath + "/Extern/Icons_GameplayFeatures/" + this.gameplayFeatures_ICONFILE[i]);
			}
			return this.gameplayFeatures_PIC[i];
		}
		return this.gameplayfeatures_PICTYP[this.gameplayFeatures_TYP[i]];
	}

	// Token: 0x06000298 RID: 664 RVA: 0x00029D38 File Offset: 0x00027F38
	public int GetPrice(int i)
	{
		return this.gameplayFeatures_PRICE[i];
	}

	// Token: 0x06000299 RID: 665 RVA: 0x00029D42 File Offset: 0x00027F42
	public bool IsErforscht(int i)
	{
		return this.gameplayFeatures_RES_POINTS_LEFT.Length >= i + 1 && this.gameplayFeatures_RES_POINTS_LEFT[i] <= 0f;
	}

	// Token: 0x0600029A RID: 666 RVA: 0x00029D65 File Offset: 0x00027F65
	public float GetProzent(int i)
	{
		return 100f / (float)this.gameplayFeatures_RES_POINTS[i] * ((float)this.gameplayFeatures_RES_POINTS[i] - this.gameplayFeatures_RES_POINTS_LEFT[i]);
	}

	// Token: 0x0600029B RID: 667 RVA: 0x00029D89 File Offset: 0x00027F89
	public int GetDevPoints(int i)
	{
		return 10 + this.gameplayFeatures_RES_POINTS[i] / 10;
	}

	// Token: 0x0600029C RID: 668 RVA: 0x00029D9C File Offset: 0x00027F9C
	public void UnlockAll()
	{
		for (int i = 0; i < this.gameplayFeatures_UNLOCK.Length; i++)
		{
			this.gameplayFeatures_UNLOCK[i] = true;
			this.gameplayFeatures_RES_POINTS_LEFT[i] = 0f;
		}
	}

	// Token: 0x0600029D RID: 669 RVA: 0x00029DD2 File Offset: 0x00027FD2
	public bool ForschungGestartet(int i)
	{
		return this.gameplayFeatures_RES_POINTS_LEFT[i] != (float)this.gameplayFeatures_RES_POINTS[i];
	}

	// Token: 0x0600029E RID: 670 RVA: 0x00029DEA File Offset: 0x00027FEA
	public bool Pay(int i)
	{
		if (!this.ForschungGestartet(i))
		{
			if (this.mS_.NotEnoughMoney(this.gameplayFeatures_PRICE[i]))
			{
				return false;
			}
			this.mS_.Pay((long)this.GetPrice(i), 2);
		}
		return true;
	}

	// Token: 0x0600029F RID: 671 RVA: 0x00029E24 File Offset: 0x00028024
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
					if (component2 && component2.slot == s && component2.typ == 3)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x060002A0 RID: 672 RVA: 0x00029EB0 File Offset: 0x000280B0
	public string GetTooltip(int i, int genre_)
	{
		string text = "<b>" + this.GetName(i) + "</b>\n";
		switch (this.gameplayFeatures_TYP[i])
		{
		case 0:
			text = text + "<color=magenta>" + this.tS_.GetText(9) + "</color>";
			break;
		case 1:
			text = text + "<color=magenta>" + this.tS_.GetText(10) + "</color>";
			break;
		case 2:
			text = text + "<color=magenta>" + this.tS_.GetText(11) + "</color>";
			break;
		case 3:
			text = text + "<color=magenta>" + this.tS_.GetText(12) + "</color>";
			break;
		case 4:
			text = text + "<color=magenta>" + this.tS_.GetText(13) + "</color>";
			break;
		case 5:
			text = text + "<color=magenta>" + this.tS_.GetText(14) + "</color>";
			break;
		case 6:
			text = text + "<color=magenta>" + this.tS_.GetText(15) + "</color>";
			break;
		}
		text = text + "\n" + this.GetDesc(i) + "\n";
		string text2 = this.tS_.GetText(254);
		text2 = text2.Replace("<NUM>", this.GetGameplay(i, genre_).ToString());
		text = text + "\n<b>" + text2 + "</b>";
		text2 = this.tS_.GetText(255);
		text2 = text2.Replace("<NUM>", this.GetGraphic(i, genre_).ToString());
		text = text + "\n<b>" + text2 + "</b>";
		text2 = this.tS_.GetText(256);
		text2 = text2.Replace("<NUM>", this.GetSound(i, genre_).ToString());
		text = text + "\n<b>" + text2 + "</b>";
		text2 = this.tS_.GetText(257);
		text2 = text2.Replace("<NUM>", this.GetTechnik(i, genre_).ToString());
		text = text + "\n<b>" + text2 + "</b>";
		text += "\n";
		text += "\n<color=green>";
		for (int j = 0; j < this.genres_.genres_LEVEL.Length; j++)
		{
			if (this.gameplayFeatures_GOOD[i, j])
			{
				text = text + this.genres_.GetName(j) + "\n";
			}
		}
		text += "</color>";
		text += "\n<color=red>";
		for (int k = 0; k < this.genres_.genres_LEVEL.Length; k++)
		{
			if (this.gameplayFeatures_BAD[i, k])
			{
				text = text + this.genres_.GetName(k) + "\n";
			}
		}
		text += "</color>";
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

	// Token: 0x0400053B RID: 1339
	private mainScript mS_;

	// Token: 0x0400053C RID: 1340
	private textScript tS_;

	// Token: 0x0400053D RID: 1341
	private settingsScript settings_;

	// Token: 0x0400053E RID: 1342
	private genres genres_;

	// Token: 0x0400053F RID: 1343
	private Sprite[] gameplayFeatures_PIC;

	// Token: 0x04000540 RID: 1344
	public Sprite[] gameplayfeatures_PICTYP;

	// Token: 0x04000541 RID: 1345
	public int[] gameplayFeatures_TYP;

	// Token: 0x04000542 RID: 1346
	public int[] gameplayFeatures_RES_POINTS;

	// Token: 0x04000543 RID: 1347
	public float[] gameplayFeatures_RES_POINTS_LEFT;

	// Token: 0x04000544 RID: 1348
	public int[] gameplayFeatures_PRICE;

	// Token: 0x04000545 RID: 1349
	public int[] gameplayFeatures_DEV_COSTS;

	// Token: 0x04000546 RID: 1350
	public int[] gameplayFeatures_DATE_YEAR;

	// Token: 0x04000547 RID: 1351
	public int[] gameplayFeatures_DATE_MONTH;

	// Token: 0x04000548 RID: 1352
	public int[] gameplayFeatures_GAMEPLAY;

	// Token: 0x04000549 RID: 1353
	public int[] gameplayFeatures_GRAPHIC;

	// Token: 0x0400054A RID: 1354
	public int[] gameplayFeatures_SOUND;

	// Token: 0x0400054B RID: 1355
	public int[] gameplayFeatures_TECHNIK;

	// Token: 0x0400054C RID: 1356
	public int[] gameplayFeatures_LEVEL;

	// Token: 0x0400054D RID: 1357
	public bool[] gameplayFeatures_UNLOCK;

	// Token: 0x0400054E RID: 1358
	public string[] gameplayFeatures_ICONFILE;

	// Token: 0x0400054F RID: 1359
	public bool[,] gameplayFeatures_GOOD;

	// Token: 0x04000550 RID: 1360
	public bool[,] gameplayFeatures_BAD;

	// Token: 0x04000551 RID: 1361
	public bool[,] gameplayFeatures_LOCKPLATFORM;

	// Token: 0x04000552 RID: 1362
	public string[] gameplayFeatures_NAME_EN;

	// Token: 0x04000553 RID: 1363
	public string[] gameplayFeatures_NAME_GE;

	// Token: 0x04000554 RID: 1364
	public string[] gameplayFeatures_NAME_TU;

	// Token: 0x04000555 RID: 1365
	public string[] gameplayFeatures_NAME_CH;

	// Token: 0x04000556 RID: 1366
	public string[] gameplayFeatures_NAME_FR;

	// Token: 0x04000557 RID: 1367
	public string[] gameplayFeatures_NAME_PB;

	// Token: 0x04000558 RID: 1368
	public string[] gameplayFeatures_NAME_CT;

	// Token: 0x04000559 RID: 1369
	public string[] gameplayFeatures_NAME_HU;

	// Token: 0x0400055A RID: 1370
	public string[] gameplayFeatures_NAME_ES;

	// Token: 0x0400055B RID: 1371
	public string[] gameplayFeatures_NAME_CZ;

	// Token: 0x0400055C RID: 1372
	public string[] gameplayFeatures_NAME_KO;

	// Token: 0x0400055D RID: 1373
	public string[] gameplayFeatures_NAME_RU;

	// Token: 0x0400055E RID: 1374
	public string[] gameplayFeatures_NAME_IT;

	// Token: 0x0400055F RID: 1375
	public string[] gameplayFeatures_NAME_AR;

	// Token: 0x04000560 RID: 1376
	public string[] gameplayFeatures_NAME_JA;

	// Token: 0x04000561 RID: 1377
	public string[] gameplayFeatures_NAME_PL;

	// Token: 0x04000562 RID: 1378
	public string[] gameplayFeatures_DESC_EN;

	// Token: 0x04000563 RID: 1379
	public string[] gameplayFeatures_DESC_GE;

	// Token: 0x04000564 RID: 1380
	public string[] gameplayFeatures_DESC_TU;

	// Token: 0x04000565 RID: 1381
	public string[] gameplayFeatures_DESC_CH;

	// Token: 0x04000566 RID: 1382
	public string[] gameplayFeatures_DESC_FR;

	// Token: 0x04000567 RID: 1383
	public string[] gameplayFeatures_DESC_PB;

	// Token: 0x04000568 RID: 1384
	public string[] gameplayFeatures_DESC_CT;

	// Token: 0x04000569 RID: 1385
	public string[] gameplayFeatures_DESC_HU;

	// Token: 0x0400056A RID: 1386
	public string[] gameplayFeatures_DESC_ES;

	// Token: 0x0400056B RID: 1387
	public string[] gameplayFeatures_DESC_CZ;

	// Token: 0x0400056C RID: 1388
	public string[] gameplayFeatures_DESC_KO;

	// Token: 0x0400056D RID: 1389
	public string[] gameplayFeatures_DESC_RU;

	// Token: 0x0400056E RID: 1390
	public string[] gameplayFeatures_DESC_IT;

	// Token: 0x0400056F RID: 1391
	public string[] gameplayFeatures_DESC_AR;

	// Token: 0x04000570 RID: 1392
	public string[] gameplayFeatures_DESC_JA;

	// Token: 0x04000571 RID: 1393
	public string[] gameplayFeatures_DESC_PL;

	// Token: 0x04000572 RID: 1394
	private string[] data;
}
