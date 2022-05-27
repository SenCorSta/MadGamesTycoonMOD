using System;
using System.IO;
using System.Text;
using UnityEngine;

// Token: 0x0200005B RID: 91
public class genres : MonoBehaviour
{
	// Token: 0x060002D9 RID: 729 RVA: 0x00003829 File Offset: 0x00001A29
	private void Awake()
	{
		this.FindScripts();
	}

	// Token: 0x060002DA RID: 730 RVA: 0x00041B48 File Offset: 0x0003FD48
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
		if (!this.games_)
		{
			this.games_ = base.GetComponent<games>();
		}
		if (!this.themes_)
		{
			this.themes_ = base.GetComponent<themes>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
	}

	// Token: 0x060002DB RID: 731 RVA: 0x00003831 File Offset: 0x00001A31
	public void Init()
	{
		this.genres_PIC = new Sprite[this.genres_LEVEL.Length];
		this.genres_SCREENSHOTS = new Sprite[this.genres_LEVEL.Length, 99];
		this.genres_SCREENSHOTS_TEXTURE = new Texture2D[this.genres_LEVEL.Length, 99];
	}

	// Token: 0x060002DC RID: 732 RVA: 0x00041BF4 File Offset: 0x0003FDF4
	public bool[] Return1DimensionArray_TARGETGROUP()
	{
		int num = this.genres_UNLOCK.Length;
		int num2 = 5;
		bool[] array = new bool[num * num2];
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num2; j++)
			{
				array[i * num2 + j] = this.genres_TARGETGROUP[i, j];
			}
		}
		return array;
	}

	// Token: 0x060002DD RID: 733 RVA: 0x00041C48 File Offset: 0x0003FE48
	public void Copy2DimensionArray_TARGETGROUP(bool[] arr)
	{
		int num = this.genres_UNLOCK.Length;
		int num2 = arr.Length / num;
		this.genres_TARGETGROUP = new bool[num, num2];
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num2; j++)
			{
				this.genres_TARGETGROUP[i, j] = arr[i * num2 + j];
			}
		}
	}

	// Token: 0x060002DE RID: 734 RVA: 0x00041CA0 File Offset: 0x0003FEA0
	public bool[] Return1DimensionArray_COMBINATION()
	{
		int num = this.genres_UNLOCK.Length;
		int num2 = this.genres_UNLOCK.Length;
		bool[] array = new bool[num * num2];
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num2; j++)
			{
				array[i * num2 + j] = this.genres_COMBINATION[i, j];
			}
		}
		return array;
	}

	// Token: 0x060002DF RID: 735 RVA: 0x00041CFC File Offset: 0x0003FEFC
	public void Copy2DimensionArray_COMBINATION(bool[] arr)
	{
		int num = this.genres_UNLOCK.Length;
		int num2 = arr.Length / num;
		this.genres_COMBINATION = new bool[num, num2];
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num2; j++)
			{
				this.genres_COMBINATION[i, j] = arr[i * num2 + j];
			}
		}
	}

	// Token: 0x060002E0 RID: 736 RVA: 0x00041D54 File Offset: 0x0003FF54
	public int[] Return1DimensionArray_FOCUS()
	{
		int num = this.genres_UNLOCK.Length;
		int num2 = 8;
		int[] array = new int[num * num2];
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num2; j++)
			{
				array[i * num2 + j] = this.genres_FOCUS[i, j];
			}
		}
		return array;
	}

	// Token: 0x060002E1 RID: 737 RVA: 0x00041DA8 File Offset: 0x0003FFA8
	public void Copy2DimensionArray_FOCUS(int[] arr)
	{
		int num = this.genres_UNLOCK.Length;
		int num2 = arr.Length / num;
		this.genres_FOCUS = new int[num, num2];
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num2; j++)
			{
				this.genres_FOCUS[i, j] = arr[i * num2 + j];
			}
		}
	}

	// Token: 0x060002E2 RID: 738 RVA: 0x00041E00 File Offset: 0x00040000
	public int[] Return1DimensionArray_ALIGN()
	{
		int num = this.genres_UNLOCK.Length;
		int num2 = 3;
		int[] array = new int[num * num2];
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num2; j++)
			{
				array[i * num2 + j] = this.genres_ALIGN[i, j];
			}
		}
		return array;
	}

	// Token: 0x060002E3 RID: 739 RVA: 0x00041E54 File Offset: 0x00040054
	public void Copy2DimensionArray_ALIGN(int[] arr)
	{
		int num = this.genres_UNLOCK.Length;
		int num2 = arr.Length / num;
		this.genres_ALIGN = new int[num, num2];
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num2; j++)
			{
				this.genres_ALIGN[i, j] = arr[i * num2 + j];
			}
		}
	}

	// Token: 0x060002E4 RID: 740 RVA: 0x00041EAC File Offset: 0x000400AC
	public int LoadAmountOfGenres(string filename)
	{
		if (this.genres_UNLOCK.Length != 0)
		{
			return this.genres_UNLOCK.Length;
		}
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
		return num;
	}

	// Token: 0x060002E5 RID: 741 RVA: 0x00041F40 File Offset: 0x00040140
	public void LoadGenres(string filename)
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
		Debug.Log("Genres Amount: " + num2.ToString());
		this.genres_BELIEBTHEIT = new float[num2];
		this.genres_BELIEBTHEIT_SOLL = new bool[num2];
		this.genres_PIC = new Sprite[num2];
		this.genres_RES_POINTS = new int[num2];
		this.genres_RES_POINTS_LEFT = new float[num2];
		this.genres_PRICE = new int[num2];
		this.genres_DEV_COSTS = new int[num2];
		this.genres_DATE_YEAR = new int[num2];
		this.genres_DATE_MONTH = new int[num2];
		this.genres_LEVEL = new int[num2];
		this.genres_UNLOCK = new bool[num2];
		this.genres_GAMEPLAY = new float[num2];
		this.genres_GRAPHIC = new float[num2];
		this.genres_SOUND = new float[num2];
		this.genres_CONTROL = new float[num2];
		this.genres_FOCUS = new int[num2, 8];
		this.genres_ALIGN = new int[num2, 3];
		this.genres_ICONFILE = new string[num2];
		this.genres_NAME_EN = new string[num2];
		this.genres_NAME_GE = new string[num2];
		this.genres_NAME_TU = new string[num2];
		this.genres_NAME_CH = new string[num2];
		this.genres_NAME_FR = new string[num2];
		this.genres_NAME_PB = new string[num2];
		this.genres_NAME_HU = new string[num2];
		this.genres_NAME_CT = new string[num2];
		this.genres_NAME_ES = new string[num2];
		this.genres_NAME_PL = new string[num2];
		this.genres_NAME_CZ = new string[num2];
		this.genres_NAME_KO = new string[num2];
		this.genres_NAME_IT = new string[num2];
		this.genres_NAME_AR = new string[num2];
		this.genres_NAME_JA = new string[num2];
		this.genres_DESC_EN = new string[num2];
		this.genres_DESC_GE = new string[num2];
		this.genres_DESC_TU = new string[num2];
		this.genres_DESC_CH = new string[num2];
		this.genres_DESC_FR = new string[num2];
		this.genres_DESC_PB = new string[num2];
		this.genres_DESC_HU = new string[num2];
		this.genres_DESC_CT = new string[num2];
		this.genres_DESC_ES = new string[num2];
		this.genres_DESC_PL = new string[num2];
		this.genres_DESC_CZ = new string[num2];
		this.genres_DESC_KO = new string[num2];
		this.genres_DESC_IT = new string[num2];
		this.genres_DESC_AR = new string[num2];
		this.genres_DESC_JA = new string[num2];
		this.genres_SCREENSHOTS = new Sprite[num2, 99];
		this.genres_SCREENSHOTS_TEXTURE = new Texture2D[num2, 99];
		this.genres_TARGETGROUP = new bool[num2, 5];
		this.genres_COMBINATION = new bool[num2, num2];
		this.genres_FANS = new int[num2];
		this.genres_MARKT = new int[num2];
		int num3 = -1;
		for (int j = 0; j < this.data.Length; j++)
		{
			if (this.ParseData("[ID]", j))
			{
				num3 = int.Parse(this.data[j]);
			}
			if (this.ParseData("[TGROUP]", j))
			{
				if (this.ParseDataDontCutLastChar("<KID>", j))
				{
					this.genres_TARGETGROUP[num3, 0] = true;
				}
				if (this.ParseDataDontCutLastChar("<TEEN>", j))
				{
					this.genres_TARGETGROUP[num3, 1] = true;
				}
				if (this.ParseDataDontCutLastChar("<ADULT>", j))
				{
					this.genres_TARGETGROUP[num3, 2] = true;
				}
				if (this.ParseDataDontCutLastChar("<OLD>", j))
				{
					this.genres_TARGETGROUP[num3, 3] = true;
				}
				if (this.ParseDataDontCutLastChar("<ALL>", j))
				{
					this.genres_TARGETGROUP[num3, 4] = true;
				}
			}
			if (this.ParseData("[RES POINTS]", j))
			{
				this.genres_RES_POINTS[num3] = int.Parse(this.data[j]);
				this.genres_RES_POINTS_LEFT[num3] = (float)this.genres_RES_POINTS[num3];
			}
			if (this.ParseData("[PRICE]", j))
			{
				this.genres_PRICE[num3] = int.Parse(this.data[j]);
			}
			if (this.ParseData("[DEV COSTS]", j))
			{
				this.genres_DEV_COSTS[num3] = int.Parse(this.data[j]);
			}
			if (this.ParseData("[GAMEPLAY]", j))
			{
				this.genres_GAMEPLAY[num3] = (float)int.Parse(this.data[j]);
			}
			if (this.ParseData("[GRAPHIC]", j))
			{
				this.genres_GRAPHIC[num3] = (float)int.Parse(this.data[j]);
			}
			if (this.ParseData("[SOUND]", j))
			{
				this.genres_SOUND[num3] = (float)int.Parse(this.data[j]);
			}
			if (this.ParseData("[CONTROL]", j))
			{
				this.genres_CONTROL[num3] = (float)int.Parse(this.data[j]);
			}
			for (int k = 0; k < 8; k++)
			{
				if (this.ParseData("[FOCUS" + k + "]", j))
				{
					this.genres_FOCUS[num3, k] = int.Parse(this.data[j]);
				}
			}
			for (int l = 0; l < 3; l++)
			{
				if (this.ParseData("[ALIGN" + l + "]", j))
				{
					this.genres_ALIGN[num3, l] = int.Parse(this.data[j]);
				}
			}
			if (this.ParseData("[GENRE COMB]", j))
			{
				for (int m = 0; m < this.genres_LEVEL.Length; m++)
				{
					if (this.ParseDataDontCutLastChar("<" + m.ToString() + ">", j))
					{
						this.genres_COMBINATION[num3, m] = true;
					}
				}
			}
			if (this.ParseData("[DATE]", j))
			{
				if (this.ParseDataDontCutLastChar("JAN", j))
				{
					this.genres_DATE_MONTH[num3] = 1;
				}
				if (this.ParseDataDontCutLastChar("FEB", j))
				{
					this.genres_DATE_MONTH[num3] = 2;
				}
				if (this.ParseDataDontCutLastChar("MAR", j))
				{
					this.genres_DATE_MONTH[num3] = 3;
				}
				if (this.ParseDataDontCutLastChar("APR", j))
				{
					this.genres_DATE_MONTH[num3] = 4;
				}
				if (this.ParseDataDontCutLastChar("MAY", j))
				{
					this.genres_DATE_MONTH[num3] = 5;
				}
				if (this.ParseDataDontCutLastChar("JUN", j))
				{
					this.genres_DATE_MONTH[num3] = 6;
				}
				if (this.ParseDataDontCutLastChar("JUL", j))
				{
					this.genres_DATE_MONTH[num3] = 7;
				}
				if (this.ParseDataDontCutLastChar("AUG", j))
				{
					this.genres_DATE_MONTH[num3] = 8;
				}
				if (this.ParseDataDontCutLastChar("SEP", j))
				{
					this.genres_DATE_MONTH[num3] = 9;
				}
				if (this.ParseDataDontCutLastChar("OCT", j))
				{
					this.genres_DATE_MONTH[num3] = 10;
				}
				if (this.ParseDataDontCutLastChar("NOV", j))
				{
					this.genres_DATE_MONTH[num3] = 11;
				}
				if (this.ParseDataDontCutLastChar("DEC", j))
				{
					this.genres_DATE_MONTH[num3] = 12;
				}
				if (this.genres_DATE_MONTH[num3] <= 0)
				{
					Debug.Log("ERROR: Genres.txt -> Incorrect Month: " + this.genres_NAME_EN[num3]);
				}
				this.genres_DATE_YEAR[num3] = int.Parse(this.data[j]);
				if (this.genres_DATE_YEAR[num3] == 1976 && this.genres_DATE_MONTH[num3] == 1)
				{
					this.genres_UNLOCK[num3] = true;
				}
			}
			if (this.ParseData("[PIC]", j))
			{
				this.genres_ICONFILE[num3] = this.data[j];
			}
			if (this.ParseData("[NAME GE]", j))
			{
				this.genres_NAME_GE[num3] = this.data[j];
			}
			if (this.ParseData("[NAME EN]", j))
			{
				this.genres_NAME_EN[num3] = this.data[j];
			}
			if (this.ParseData("[NAME TU]", j))
			{
				this.genres_NAME_TU[num3] = this.data[j];
			}
			if (this.ParseData("[NAME CH]", j))
			{
				this.genres_NAME_CH[num3] = this.data[j];
			}
			if (this.ParseData("[NAME FR]", j))
			{
				this.genres_NAME_FR[num3] = this.data[j];
			}
			if (this.ParseData("[NAME PB]", j))
			{
				this.genres_NAME_PB[num3] = this.data[j];
			}
			if (this.ParseData("[NAME HU]", j))
			{
				this.genres_NAME_HU[num3] = this.data[j];
			}
			if (this.ParseData("[NAME CT]", j))
			{
				this.genres_NAME_CT[num3] = this.data[j];
			}
			if (this.ParseData("[NAME ES]", j))
			{
				this.genres_NAME_ES[num3] = this.data[j];
			}
			if (this.ParseData("[NAME PL]", j))
			{
				this.genres_NAME_PL[num3] = this.data[j];
			}
			if (this.ParseData("[NAME CZ]", j))
			{
				this.genres_NAME_CZ[num3] = this.data[j];
			}
			if (this.ParseData("[NAME KO]", j))
			{
				this.genres_NAME_KO[num3] = this.data[j];
			}
			if (this.ParseData("[NAME IT]", j))
			{
				this.genres_NAME_IT[num3] = this.data[j];
			}
			if (this.ParseData("[NAME AR]", j))
			{
				this.genres_NAME_AR[num3] = this.data[j];
			}
			if (this.ParseData("[NAME JA]", j))
			{
				this.genres_NAME_JA[num3] = this.data[j];
			}
			if (this.ParseData("[DESC GE]", j))
			{
				this.genres_DESC_GE[num3] = this.data[j];
			}
			if (this.ParseData("[DESC EN]", j))
			{
				this.genres_DESC_EN[num3] = this.data[j];
			}
			if (this.ParseData("[DESC TU]", j))
			{
				this.genres_DESC_TU[num3] = this.data[j];
			}
			if (this.ParseData("[DESC CH]", j))
			{
				this.genres_DESC_CH[num3] = this.data[j];
			}
			if (this.ParseData("[DESC FR]", j))
			{
				this.genres_DESC_FR[num3] = this.data[j];
			}
			if (this.ParseData("[DESC PB]", j))
			{
				this.genres_DESC_PB[num3] = this.data[j];
			}
			if (this.ParseData("[DESC HU]", j))
			{
				this.genres_DESC_HU[num3] = this.data[j];
			}
			if (this.ParseData("[DESC CT]", j))
			{
				this.genres_DESC_CT[num3] = this.data[j];
			}
			if (this.ParseData("[DESC ES]", j))
			{
				this.genres_DESC_ES[num3] = this.data[j];
			}
			if (this.ParseData("[DESC PL]", j))
			{
				this.genres_DESC_PL[num3] = this.data[j];
			}
			if (this.ParseData("[DESC CZ]", j))
			{
				this.genres_DESC_CZ[num3] = this.data[j];
			}
			if (this.ParseData("[DESC KO]", j))
			{
				this.genres_DESC_KO[num3] = this.data[j];
			}
			if (this.ParseData("[DESC IT]", j))
			{
				this.genres_DESC_IT[num3] = this.data[j];
			}
			if (this.ParseData("[DESC AR]", j))
			{
				this.genres_DESC_AR[num3] = this.data[j];
			}
			if (this.ParseData("[DESC JA]", j))
			{
				this.genres_DESC_JA[num3] = this.data[j];
			}
			if (this.ParseData("[EOF]", j))
			{
				Debug.Log("Genres.txt -> EOF");
				return;
			}
			num++;
		}
	}

	// Token: 0x060002E6 RID: 742 RVA: 0x00042AE4 File Offset: 0x00040CE4
	public void LoadGenresettingsForOldSavegeames(string filename)
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
		this.genres_FOCUS = new int[num2, 8];
		this.genres_ALIGN = new int[num2, 3];
		int num3 = -1;
		for (int j = 0; j < this.data.Length; j++)
		{
			if (this.ParseData("[ID]", j))
			{
				num3 = int.Parse(this.data[j]);
			}
			for (int k = 0; k < 8; k++)
			{
				if (this.ParseData("[FOCUS" + k + "]", j))
				{
					this.genres_FOCUS[num3, k] = int.Parse(this.data[j]);
				}
			}
			for (int l = 0; l < 3; l++)
			{
				if (this.ParseData("[ALIGN" + l + "]", j))
				{
					this.genres_ALIGN[num3, l] = int.Parse(this.data[j]);
				}
			}
			if (this.ParseData("[EOF]", j))
			{
				Debug.Log("Genres.txt -> EOF");
				return;
			}
			num++;
		}
	}

	// Token: 0x060002E7 RID: 743 RVA: 0x00042C78 File Offset: 0x00040E78
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

	// Token: 0x060002E8 RID: 744 RVA: 0x00003870 File Offset: 0x00001A70
	private bool ParseDataDontCutLastChar(string c, int i)
	{
		if (this.data[i].Contains(c))
		{
			this.data[i] = this.data[i].Replace(c, "");
			return true;
		}
		return false;
	}

	// Token: 0x060002E9 RID: 745 RVA: 0x00042CD8 File Offset: 0x00040ED8
	public Sprite GetScreenshot(int genre_, int grafikPoints)
	{
		int num = 0;
		int num2 = 0;
		while (num2 < 1000 && File.Exists(string.Concat(new string[]
		{
			Application.dataPath,
			"/Extern/Screenshots/",
			genre_.ToString(),
			"/",
			num2.ToString(),
			".png"
		})))
		{
			num = num2;
			num2++;
		}
		int num3 = 30000 / (num + 1);
		num3 = grafikPoints / num3;
		if (num3 < 0)
		{
			num3 = 0;
		}
		if (num3 > num)
		{
			num3 = num;
		}
		if (this.genres_SCREENSHOTS[genre_, num3])
		{
			return this.genres_SCREENSHOTS[genre_, num3];
		}
		this.genres_SCREENSHOTS[genre_, num3] = this.mS_.LoadPNG(string.Concat(new string[]
		{
			Application.dataPath,
			"/Extern/Screenshots/",
			genre_.ToString(),
			"/",
			num3.ToString(),
			".png"
		}));
		return this.genres_SCREENSHOTS[genre_, num3];
	}

	// Token: 0x060002EA RID: 746 RVA: 0x00042DE0 File Offset: 0x00040FE0
	public Texture2D GetScreenshotTexture2D(int genre_, int grafikPoints)
	{
		int num = 0;
		int num2 = 0;
		while (num2 < 1000 && File.Exists(string.Concat(new string[]
		{
			Application.dataPath,
			"/Extern/Screenshots/",
			genre_.ToString(),
			"/",
			num2.ToString(),
			".png"
		})))
		{
			num = num2;
			num2++;
		}
		int num3 = 30000 / (num + 1);
		num3 = grafikPoints / num3;
		if (num3 < 0)
		{
			num3 = 0;
		}
		if (num3 > num)
		{
			num3 = num;
		}
		if (this.genres_SCREENSHOTS_TEXTURE[genre_, num3])
		{
			return this.genres_SCREENSHOTS_TEXTURE[genre_, num3];
		}
		this.genres_SCREENSHOTS_TEXTURE[genre_, num3] = this.mS_.LoadTexture(string.Concat(new string[]
		{
			Application.dataPath,
			"/Extern/Screenshots/",
			genre_.ToString(),
			"/",
			num3.ToString(),
			".png"
		}));
		return this.genres_SCREENSHOTS_TEXTURE[genre_, num3];
	}

	// Token: 0x060002EB RID: 747 RVA: 0x00042EE8 File Offset: 0x000410E8
	public string GetName(int i)
	{
		if (this.genres_PIC == null)
		{
			return "<Not initialized>";
		}
		if (this.genres_NAME_EN.Length == 0)
		{
			return "<Not initialized>";
		}
		if (i < 0)
		{
			return this.tS_.GetText(688);
		}
		string text = "";
		switch (this.settings_.language)
		{
		case 0:
			text = this.genres_NAME_EN[i];
			goto IL_1E6;
		case 1:
			text = this.genres_NAME_GE[i];
			goto IL_1E6;
		case 2:
			if (this.genres_NAME_TU.Length != 0)
			{
				text = this.genres_NAME_TU[i];
				goto IL_1E6;
			}
			goto IL_1E6;
		case 3:
			if (this.genres_NAME_CH.Length != 0)
			{
				text = this.genres_NAME_CH[i];
				goto IL_1E6;
			}
			goto IL_1E6;
		case 4:
			if (this.genres_NAME_FR.Length != 0)
			{
				text = this.genres_NAME_FR[i];
				goto IL_1E6;
			}
			goto IL_1E6;
		case 5:
			if (this.genres_NAME_ES.Length != 0)
			{
				text = this.genres_NAME_ES[i];
				goto IL_1E6;
			}
			goto IL_1E6;
		case 6:
			if (this.genres_NAME_KO.Length != 0)
			{
				text = this.genres_NAME_KO[i];
				goto IL_1E6;
			}
			goto IL_1E6;
		case 7:
			if (this.genres_NAME_PB.Length != 0)
			{
				text = this.genres_NAME_PB[i];
				goto IL_1E6;
			}
			goto IL_1E6;
		case 8:
			if (this.genres_NAME_HU.Length != 0)
			{
				text = this.genres_NAME_HU[i];
				goto IL_1E6;
			}
			goto IL_1E6;
		case 10:
			if (this.genres_NAME_CT.Length != 0)
			{
				text = this.genres_NAME_CT[i];
				goto IL_1E6;
			}
			goto IL_1E6;
		case 11:
			if (this.genres_NAME_PL.Length != 0)
			{
				text = this.genres_NAME_PL[i];
				goto IL_1E6;
			}
			goto IL_1E6;
		case 12:
			if (this.genres_NAME_CZ.Length != 0)
			{
				text = this.genres_NAME_CZ[i];
				goto IL_1E6;
			}
			goto IL_1E6;
		case 13:
			if (this.genres_NAME_AR.Length != 0)
			{
				text = this.genres_NAME_AR[i];
				goto IL_1E6;
			}
			goto IL_1E6;
		case 14:
			if (this.genres_NAME_IT.Length != 0)
			{
				text = this.genres_NAME_IT[i];
				goto IL_1E6;
			}
			goto IL_1E6;
		case 16:
			if (this.genres_NAME_JA.Length != 0)
			{
				text = this.genres_NAME_JA[i];
				goto IL_1E6;
			}
			goto IL_1E6;
		}
		text = this.genres_NAME_EN[i];
		IL_1E6:
		if (text == null)
		{
			return this.genres_NAME_EN[i];
		}
		if (text.Length <= 0)
		{
			return this.genres_NAME_EN[i];
		}
		return text;
	}

	// Token: 0x060002EC RID: 748 RVA: 0x000430FC File Offset: 0x000412FC
	public string GetDesc(int i)
	{
		if (this.genres_PIC == null)
		{
			return "<Not initialized>";
		}
		if (i < 0)
		{
			return "";
		}
		string text = "";
		switch (this.settings_.language)
		{
		case 0:
			text = this.genres_DESC_EN[i];
			goto IL_1CC;
		case 1:
			text = this.genres_DESC_GE[i];
			goto IL_1CC;
		case 2:
			if (this.genres_DESC_TU.Length != 0)
			{
				text = this.genres_DESC_TU[i];
				goto IL_1CC;
			}
			goto IL_1CC;
		case 3:
			if (this.genres_DESC_CH.Length != 0)
			{
				text = this.genres_DESC_CH[i];
				goto IL_1CC;
			}
			goto IL_1CC;
		case 4:
			if (this.genres_DESC_FR.Length != 0)
			{
				text = this.genres_DESC_FR[i];
				goto IL_1CC;
			}
			goto IL_1CC;
		case 5:
			if (this.genres_DESC_ES.Length != 0)
			{
				text = this.genres_DESC_ES[i];
				goto IL_1CC;
			}
			goto IL_1CC;
		case 6:
			if (this.genres_DESC_KO.Length != 0)
			{
				text = this.genres_DESC_KO[i];
				goto IL_1CC;
			}
			goto IL_1CC;
		case 7:
			if (this.genres_DESC_PB.Length != 0)
			{
				text = this.genres_DESC_PB[i];
				goto IL_1CC;
			}
			goto IL_1CC;
		case 8:
			if (this.genres_DESC_HU.Length != 0)
			{
				text = this.genres_DESC_HU[i];
				goto IL_1CC;
			}
			goto IL_1CC;
		case 10:
			if (this.genres_DESC_CT.Length != 0)
			{
				text = this.genres_DESC_CT[i];
				goto IL_1CC;
			}
			goto IL_1CC;
		case 11:
			if (this.genres_DESC_PL.Length != 0)
			{
				text = this.genres_DESC_PL[i];
				goto IL_1CC;
			}
			goto IL_1CC;
		case 12:
			if (this.genres_DESC_CZ.Length != 0)
			{
				text = this.genres_DESC_CZ[i];
				goto IL_1CC;
			}
			goto IL_1CC;
		case 13:
			if (this.genres_DESC_AR.Length != 0)
			{
				text = this.genres_DESC_AR[i];
				goto IL_1CC;
			}
			goto IL_1CC;
		case 14:
			if (this.genres_DESC_IT.Length != 0)
			{
				text = this.genres_DESC_IT[i];
				goto IL_1CC;
			}
			goto IL_1CC;
		case 16:
			if (this.genres_DESC_JA.Length != 0)
			{
				text = this.genres_DESC_JA[i];
				goto IL_1CC;
			}
			goto IL_1CC;
		}
		text = this.genres_DESC_EN[i];
		IL_1CC:
		if (text == null)
		{
			return this.genres_DESC_EN[i];
		}
		if (text.Length <= 0)
		{
			return this.genres_DESC_EN[i];
		}
		return text;
	}

	// Token: 0x060002ED RID: 749 RVA: 0x000432F4 File Offset: 0x000414F4
	public int GetDevCosts(int i)
	{
		float num = (float)this.genres_LEVEL[i] * 0.1f;
		num = (float)this.genres_DEV_COSTS[i] * (1f - num);
		return Mathf.RoundToInt(num);
	}

	// Token: 0x060002EE RID: 750 RVA: 0x000038A0 File Offset: 0x00001AA0
	public int GetPrice(int i)
	{
		return this.genres_PRICE[i];
	}

	// Token: 0x060002EF RID: 751 RVA: 0x0004332C File Offset: 0x0004152C
	public Sprite GetPic(int i)
	{
		if (this.genres_PIC == null)
		{
			return this.guiMain_.uiSprites[3];
		}
		if (i > -1)
		{
			if (this.genres_ICONFILE[i].Length > 0 && !this.genres_PIC[i])
			{
				this.genres_PIC[i] = this.mS_.LoadPNG(Application.dataPath + "/Extern/Icons_Genres/" + this.genres_ICONFILE[i]);
			}
			return this.genres_PIC[i];
		}
		return this.guiMain_.uiSprites[3];
	}

	// Token: 0x060002F0 RID: 752 RVA: 0x000038AA File Offset: 0x00001AAA
	public bool IsErforscht(int i)
	{
		return this.genres_RES_POINTS_LEFT[i] <= 0f;
	}

	// Token: 0x060002F1 RID: 753 RVA: 0x000038BE File Offset: 0x00001ABE
	public float GetProzent(int i)
	{
		return 100f / (float)this.genres_RES_POINTS[i] * ((float)this.genres_RES_POINTS[i] - this.genres_RES_POINTS_LEFT[i]);
	}

	// Token: 0x060002F2 RID: 754 RVA: 0x000433B4 File Offset: 0x000415B4
	public void UnlockAll()
	{
		for (int i = 0; i < this.genres_UNLOCK.Length; i++)
		{
			this.genres_UNLOCK[i] = true;
		}
	}

	// Token: 0x060002F3 RID: 755 RVA: 0x000038E2 File Offset: 0x00001AE2
	public bool ForschungGestartet(int i)
	{
		return this.genres_RES_POINTS_LEFT[i] != (float)this.genres_RES_POINTS[i];
	}

	// Token: 0x060002F4 RID: 756 RVA: 0x000038FA File Offset: 0x00001AFA
	public bool IsTargetGroup(int genre_, int group_)
	{
		return genre_ >= 0 && group_ >= 0 && this.genres_TARGETGROUP[genre_, group_];
	}

	// Token: 0x060002F5 RID: 757 RVA: 0x00003913 File Offset: 0x00001B13
	public bool IsGenreCombination(int genre_, int subgenre_)
	{
		return genre_ >= 0 && subgenre_ >= 0 && this.genres_COMBINATION[genre_, subgenre_];
	}

	// Token: 0x060002F6 RID: 758 RVA: 0x0000392C File Offset: 0x00001B2C
	public bool Pay(int i)
	{
		if (!this.ForschungGestartet(i))
		{
			if (this.mS_.NotEnoughMoney(this.genres_PRICE[i]))
			{
				return false;
			}
			this.mS_.Pay((long)this.GetPrice(i), 2);
		}
		return true;
	}

	// Token: 0x060002F7 RID: 759 RVA: 0x000433E0 File Offset: 0x000415E0
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
					if (component2 && component2.slot == s && component2.typ == 0)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x060002F8 RID: 760 RVA: 0x00003963 File Offset: 0x00001B63
	public string GetTooltip(int i)
	{
		return "<b>" + this.GetName(i) + "</b>" + "\n" + this.GetDesc(i);
	}

	// Token: 0x060002F9 RID: 761 RVA: 0x0004346C File Offset: 0x0004166C
	public int GetAmountFans()
	{
		int num = 0;
		for (int i = 0; i < this.genres_FANS.Length; i++)
		{
			num += this.genres_FANS[i];
		}
		return num;
	}

	// Token: 0x060002FA RID: 762 RVA: 0x0004349C File Offset: 0x0004169C
	public string GetStringBeliebtheit(int i, bool kurz)
	{
		if (i == this.mS_.trendGenre)
		{
			if (!kurz)
			{
				return this.tS_.GetText(1381);
			}
			return "";
		}
		else
		{
			if (i != this.mS_.trendAntiGenre)
			{
				return Mathf.RoundToInt(this.genres_BELIEBTHEIT[i]).ToString() + "%";
			}
			if (!kurz)
			{
				return this.tS_.GetText(1382);
			}
			return "";
		}
	}

	// Token: 0x060002FB RID: 763 RVA: 0x0000398C File Offset: 0x00001B8C
	public float GetFloatBeliebtheit(int i)
	{
		if (i == this.mS_.trendGenre)
		{
			return 100f;
		}
		if (i == this.mS_.trendAntiGenre)
		{
			return 0f;
		}
		return this.genres_BELIEBTHEIT[i];
	}

	// Token: 0x060002FC RID: 764 RVA: 0x00043518 File Offset: 0x00041718
	public bool GetFocusKnown(int slot, int maingenre, int subgenre)
	{
		for (int i = 0; i < this.games_.arrayGamesScripts.Length; i++)
		{
			gameScript gameScript = this.games_.arrayGamesScripts[i];
			if ((gameScript.playerGame || gameScript.IsMyAuftragsspiel()) && gameScript.spielbericht && gameScript.maingenre == maingenre && gameScript.subgenre == subgenre && gameScript.Designschwerpunkt[slot] == this.GetFocus(slot, maingenre, subgenre))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060002FD RID: 765 RVA: 0x0004358C File Offset: 0x0004178C
	public int GetFocus(int slot, int maingenre, int subgenre)
	{
		int[] array = new int[8];
		for (int i = 0; i < array.Length; i++)
		{
			if (maingenre != -1)
			{
				array[i] += this.genres_FOCUS[maingenre, i];
			}
			if (subgenre != -1)
			{
				array[i] += this.genres_FOCUS[subgenre, i];
				array[i] /= 2;
			}
		}
		int num = 0;
		for (int j = 0; j < array.Length; j++)
		{
			num += array[j];
		}
		num = 40 - num;
		if (num > 0)
		{
			for (int k = 0; k < array.Length; k++)
			{
				if (num > 0 && array[k] < 10)
				{
					array[k]++;
					num--;
				}
			}
		}
		if (num < 0)
		{
			for (int l = 0; l < array.Length; l++)
			{
				if (num < 0 && array[l] > 0)
				{
					array[l]--;
					num++;
				}
			}
		}
		return array[slot];
	}

	// Token: 0x060002FE RID: 766 RVA: 0x00043674 File Offset: 0x00041874
	public bool GetAlignKnown(int slot, int maingenre, int subgenre)
	{
		for (int i = 0; i < this.games_.arrayGamesScripts.Length; i++)
		{
			gameScript gameScript = this.games_.arrayGamesScripts[i];
			if ((gameScript.playerGame || gameScript.IsMyAuftragsspiel()) && gameScript.spielbericht && gameScript.maingenre == maingenre && gameScript.subgenre == subgenre && gameScript.Designausrichtung[slot] == this.GetAlign(slot, maingenre, subgenre))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060002FF RID: 767 RVA: 0x000436E8 File Offset: 0x000418E8
	public int GetAlign(int slot, int maingenre, int subgenre)
	{
		int[] array = new int[3];
		for (int i = 0; i < array.Length; i++)
		{
			if (maingenre != -1)
			{
				array[i] += this.genres_ALIGN[maingenre, i];
			}
			if (subgenre != -1)
			{
				array[i] += this.genres_ALIGN[subgenre, i];
				array[i] /= 2;
			}
		}
		return array[slot];
	}

	// Token: 0x06000300 RID: 768 RVA: 0x00043750 File Offset: 0x00041950
	public Sprite GetSpriteMarkt(int i)
	{
		int num = this.genres_MARKT[i];
		if (num <= 5)
		{
			return this.themes_.spriteMarkt[0];
		}
		if (num > 5 && num < 10)
		{
			return this.themes_.spriteMarkt[1];
		}
		if (num >= 10)
		{
			return this.themes_.spriteMarkt[2];
		}
		return this.themes_.spriteMarkt[0];
	}

	// Token: 0x06000301 RID: 769 RVA: 0x000437B0 File Offset: 0x000419B0
	public string GetStringMarktsaettigung(int i)
	{
		int num = this.genres_MARKT[i];
		if (num < 5)
		{
			return this.tS_.GetText(1908);
		}
		if (num >= 5 && num < 10)
		{
			return this.tS_.GetText(1909);
		}
		if (num >= 10)
		{
			return this.tS_.GetText(1910);
		}
		return this.tS_.GetText(1908);
	}

	// Token: 0x040005B4 RID: 1460
	private mainScript mS_;

	// Token: 0x040005B5 RID: 1461
	private textScript tS_;

	// Token: 0x040005B6 RID: 1462
	private settingsScript settings_;

	// Token: 0x040005B7 RID: 1463
	private GUI_Main guiMain_;

	// Token: 0x040005B8 RID: 1464
	private games games_;

	// Token: 0x040005B9 RID: 1465
	private themes themes_;

	// Token: 0x040005BA RID: 1466
	private Sprite[] genres_PIC;

	// Token: 0x040005BB RID: 1467
	public float[] genres_BELIEBTHEIT;

	// Token: 0x040005BC RID: 1468
	public bool[] genres_BELIEBTHEIT_SOLL;

	// Token: 0x040005BD RID: 1469
	public int[] genres_RES_POINTS;

	// Token: 0x040005BE RID: 1470
	public float[] genres_RES_POINTS_LEFT;

	// Token: 0x040005BF RID: 1471
	public int[] genres_PRICE;

	// Token: 0x040005C0 RID: 1472
	public int[] genres_DEV_COSTS;

	// Token: 0x040005C1 RID: 1473
	public int[] genres_DATE_YEAR;

	// Token: 0x040005C2 RID: 1474
	public int[] genres_DATE_MONTH;

	// Token: 0x040005C3 RID: 1475
	public int[] genres_LEVEL;

	// Token: 0x040005C4 RID: 1476
	public bool[] genres_UNLOCK;

	// Token: 0x040005C5 RID: 1477
	private Sprite[,] genres_SCREENSHOTS;

	// Token: 0x040005C6 RID: 1478
	private Texture2D[,] genres_SCREENSHOTS_TEXTURE;

	// Token: 0x040005C7 RID: 1479
	public bool[,] genres_TARGETGROUP;

	// Token: 0x040005C8 RID: 1480
	public float[] genres_GAMEPLAY;

	// Token: 0x040005C9 RID: 1481
	public float[] genres_GRAPHIC;

	// Token: 0x040005CA RID: 1482
	public float[] genres_SOUND;

	// Token: 0x040005CB RID: 1483
	public float[] genres_CONTROL;

	// Token: 0x040005CC RID: 1484
	public bool[,] genres_COMBINATION;

	// Token: 0x040005CD RID: 1485
	public int[,] genres_FOCUS;

	// Token: 0x040005CE RID: 1486
	public int[,] genres_ALIGN;

	// Token: 0x040005CF RID: 1487
	public string[] genres_ICONFILE;

	// Token: 0x040005D0 RID: 1488
	public string[] genres_NAME_EN;

	// Token: 0x040005D1 RID: 1489
	public string[] genres_NAME_GE;

	// Token: 0x040005D2 RID: 1490
	public string[] genres_NAME_TU;

	// Token: 0x040005D3 RID: 1491
	public string[] genres_NAME_CH;

	// Token: 0x040005D4 RID: 1492
	public string[] genres_NAME_FR;

	// Token: 0x040005D5 RID: 1493
	public string[] genres_NAME_PB;

	// Token: 0x040005D6 RID: 1494
	public string[] genres_NAME_HU;

	// Token: 0x040005D7 RID: 1495
	public string[] genres_NAME_CT;

	// Token: 0x040005D8 RID: 1496
	public string[] genres_NAME_ES;

	// Token: 0x040005D9 RID: 1497
	public string[] genres_NAME_PL;

	// Token: 0x040005DA RID: 1498
	public string[] genres_NAME_CZ;

	// Token: 0x040005DB RID: 1499
	public string[] genres_NAME_KO;

	// Token: 0x040005DC RID: 1500
	public string[] genres_NAME_IT;

	// Token: 0x040005DD RID: 1501
	public string[] genres_NAME_AR;

	// Token: 0x040005DE RID: 1502
	public string[] genres_NAME_JA;

	// Token: 0x040005DF RID: 1503
	public string[] genres_DESC_EN;

	// Token: 0x040005E0 RID: 1504
	public string[] genres_DESC_GE;

	// Token: 0x040005E1 RID: 1505
	public string[] genres_DESC_TU;

	// Token: 0x040005E2 RID: 1506
	public string[] genres_DESC_CH;

	// Token: 0x040005E3 RID: 1507
	public string[] genres_DESC_FR;

	// Token: 0x040005E4 RID: 1508
	public string[] genres_DESC_PB;

	// Token: 0x040005E5 RID: 1509
	public string[] genres_DESC_HU;

	// Token: 0x040005E6 RID: 1510
	public string[] genres_DESC_CT;

	// Token: 0x040005E7 RID: 1511
	public string[] genres_DESC_ES;

	// Token: 0x040005E8 RID: 1512
	public string[] genres_DESC_PL;

	// Token: 0x040005E9 RID: 1513
	public string[] genres_DESC_CZ;

	// Token: 0x040005EA RID: 1514
	public string[] genres_DESC_KO;

	// Token: 0x040005EB RID: 1515
	public string[] genres_DESC_IT;

	// Token: 0x040005EC RID: 1516
	public string[] genres_DESC_AR;

	// Token: 0x040005ED RID: 1517
	public string[] genres_DESC_JA;

	// Token: 0x040005EE RID: 1518
	public int[] genres_FANS;

	// Token: 0x040005EF RID: 1519
	public int[] genres_MARKT;

	// Token: 0x040005F0 RID: 1520
	private string[] data;
}
