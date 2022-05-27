using System;
using System.IO;
using System.Text;
using UnityEngine;


public class genres : MonoBehaviour
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

	
	public void Init()
	{
		this.genres_PIC = new Sprite[this.genres_LEVEL.Length];
		this.genres_SCREENSHOTS = new Sprite[this.genres_LEVEL.Length, 99];
		this.genres_SCREENSHOTS_TEXTURE = new Texture2D[this.genres_LEVEL.Length, 99];
	}

	
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
				break;
			}
			num++;
		}
	}

	
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

	
	public int GetDevCosts(int i)
	{
		float num = (float)this.genres_LEVEL[i] * 0.1f;
		num = (float)this.genres_DEV_COSTS[i] * (1f - num);
		return Mathf.RoundToInt(num);
	}

	
	public int GetPrice(int i)
	{
		return this.genres_PRICE[i];
	}

	
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

	
	public bool IsErforscht(int i)
	{
		return this.genres_RES_POINTS_LEFT[i] <= 0f;
	}

	
	public float GetProzent(int i)
	{
		return 100f / (float)this.genres_RES_POINTS[i] * ((float)this.genres_RES_POINTS[i] - this.genres_RES_POINTS_LEFT[i]);
	}

	
	public void UnlockAll()
	{
		for (int i = 0; i < this.genres_UNLOCK.Length; i++)
		{
			this.genres_UNLOCK[i] = true;
		}
	}

	
	public bool ForschungGestartet(int i)
	{
		return this.genres_RES_POINTS_LEFT[i] != (float)this.genres_RES_POINTS[i];
	}

	
	public bool IsTargetGroup(int genre_, int group_)
	{
		return genre_ >= 0 && group_ >= 0 && this.genres_TARGETGROUP[genre_, group_];
	}

	
	public bool IsGenreCombination(int genre_, int subgenre_)
	{
		return genre_ >= 0 && subgenre_ >= 0 && this.genres_COMBINATION[genre_, subgenre_];
	}

	
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

	
	public string GetTooltip(int i)
	{
		return "<b>" + this.GetName(i) + "</b>" + "\n" + this.GetDesc(i);
	}

	
	public int GetAmountFans()
	{
		int num = 0;
		for (int i = 0; i < this.genres_FANS.Length; i++)
		{
			num += this.genres_FANS[i];
		}
		return num;
	}

	
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

	
	public bool GetFocusKnown(int slot, int maingenre, int subgenre)
	{
		for (int i = 0; i < this.games_.arrayGamesScripts.Length; i++)
		{
			gameScript gameScript = this.games_.arrayGamesScripts[i];
			if ((gameScript.ownerID == this.mS_.myID || gameScript.developerID == this.mS_.myID) && gameScript.spielbericht && gameScript.maingenre == maingenre && gameScript.subgenre == subgenre && gameScript.Designschwerpunkt[slot] == this.GetFocus(slot, maingenre, subgenre))
			{
				return true;
			}
		}
		return false;
	}

	
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

	
	public bool GetAlignKnown(int slot, int maingenre, int subgenre)
	{
		for (int i = 0; i < this.games_.arrayGamesScripts.Length; i++)
		{
			gameScript gameScript = this.games_.arrayGamesScripts[i];
			if ((gameScript.ownerID == this.mS_.myID || gameScript.developerID == this.mS_.myID) && gameScript.spielbericht && gameScript.maingenre == maingenre && gameScript.subgenre == subgenre && gameScript.Designausrichtung[slot] == this.GetAlign(slot, maingenre, subgenre))
			{
				return true;
			}
		}
		return false;
	}

	
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

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private settingsScript settings_;

	
	private GUI_Main guiMain_;

	
	private games games_;

	
	private themes themes_;

	
	private Sprite[] genres_PIC;

	
	public float[] genres_BELIEBTHEIT;

	
	public bool[] genres_BELIEBTHEIT_SOLL;

	
	public int[] genres_RES_POINTS;

	
	public float[] genres_RES_POINTS_LEFT;

	
	public int[] genres_PRICE;

	
	public int[] genres_DEV_COSTS;

	
	public int[] genres_DATE_YEAR;

	
	public int[] genres_DATE_MONTH;

	
	public int[] genres_LEVEL;

	
	public bool[] genres_UNLOCK;

	
	private Sprite[,] genres_SCREENSHOTS;

	
	private Texture2D[,] genres_SCREENSHOTS_TEXTURE;

	
	public bool[,] genres_TARGETGROUP;

	
	public float[] genres_GAMEPLAY;

	
	public float[] genres_GRAPHIC;

	
	public float[] genres_SOUND;

	
	public float[] genres_CONTROL;

	
	public bool[,] genres_COMBINATION;

	
	public int[,] genres_FOCUS;

	
	public int[,] genres_ALIGN;

	
	public string[] genres_ICONFILE;

	
	public string[] genres_NAME_EN;

	
	public string[] genres_NAME_GE;

	
	public string[] genres_NAME_TU;

	
	public string[] genres_NAME_CH;

	
	public string[] genres_NAME_FR;

	
	public string[] genres_NAME_PB;

	
	public string[] genres_NAME_HU;

	
	public string[] genres_NAME_CT;

	
	public string[] genres_NAME_ES;

	
	public string[] genres_NAME_PL;

	
	public string[] genres_NAME_CZ;

	
	public string[] genres_NAME_KO;

	
	public string[] genres_NAME_IT;

	
	public string[] genres_NAME_AR;

	
	public string[] genres_NAME_JA;

	
	public string[] genres_DESC_EN;

	
	public string[] genres_DESC_GE;

	
	public string[] genres_DESC_TU;

	
	public string[] genres_DESC_CH;

	
	public string[] genres_DESC_FR;

	
	public string[] genres_DESC_PB;

	
	public string[] genres_DESC_HU;

	
	public string[] genres_DESC_CT;

	
	public string[] genres_DESC_ES;

	
	public string[] genres_DESC_PL;

	
	public string[] genres_DESC_CZ;

	
	public string[] genres_DESC_KO;

	
	public string[] genres_DESC_IT;

	
	public string[] genres_DESC_AR;

	
	public string[] genres_DESC_JA;

	
	public int[] genres_FANS;

	
	public int[] genres_MARKT;

	
	private string[] data;
}
