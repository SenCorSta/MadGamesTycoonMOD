using System;
using System.IO;
using System.Text;
using UnityEngine;


public class engineFeatures : MonoBehaviour
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

	
	public void Init()
	{
		this.engineFeatures_PIC = new Sprite[this.engineFeatures_UNLOCK.Length];
	}

	
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

	
	public int GetGameplay(int i)
	{
		float num = (float)this.engineFeatures_LEVEL[i] * 0.1f;
		num = (float)this.engineFeatures_GAMEPLAY[i] * (1f + num);
		return Mathf.RoundToInt(num);
	}

	
	public int GetGraphic(int i)
	{
		float num = (float)this.engineFeatures_LEVEL[i] * 0.1f;
		num = (float)this.engineFeatures_GRAPHIC[i] * (1f + num);
		return Mathf.RoundToInt(num);
	}

	
	public int GetSound(int i)
	{
		float num = (float)this.engineFeatures_LEVEL[i] * 0.1f;
		num = (float)this.engineFeatures_SOUND[i] * (1f + num);
		return Mathf.RoundToInt(num);
	}

	
	public int GetTechnik(int i)
	{
		float num = (float)this.engineFeatures_LEVEL[i] * 0.1f;
		num = (float)this.engineFeatures_TECHNIK[i] * (1f + num);
		return Mathf.RoundToInt(num);
	}

	
	public int GetDevCosts(int i)
	{
		float num = (float)this.engineFeatures_LEVEL[i] * 0.1f;
		num = (float)this.engineFeatures_DEV_COSTS[i] * (1f - num);
		return Mathf.RoundToInt(num);
	}

	
	public int GetDevCostsForEngine(int i)
	{
		float num = (float)this.mS_.difficulty;
		return Mathf.RoundToInt((float)(Mathf.RoundToInt((float)this.engineFeatures_DEV_COSTS[i] * (1.25f + num * 0.2f)) / 200 * 200));
	}

	
	public int GetPrice(int i)
	{
		return this.engineFeatures_PRICE[i];
	}

	
	public int GetDevPointsForEngine(int i)
	{
		if (i == -1)
		{
			return 0;
		}
		return 10 + this.engineFeatures_RES_POINTS[i] / 5;
	}

	
	public int GetDevPointsForGame(int i)
	{
		if (i == -1)
		{
			return 0;
		}
		return 10 + this.engineFeatures_RES_POINTS[i] / 10;
	}

	
	public int GetTypGrafik()
	{
		return 0;
	}

	
	public int GetTypSound()
	{
		return 1;
	}

	
	public int GetTypKI()
	{
		return 2;
	}

	
	public int GetTypPhysik()
	{
		return 3;
	}

	
	public bool IsErforscht(int i)
	{
		return this.engineFeatures_RES_POINTS_LEFT[i] <= 0f;
	}

	
	public float GetProzent(int i)
	{
		return 100f / (float)this.engineFeatures_RES_POINTS[i] * ((float)this.engineFeatures_RES_POINTS[i] - this.engineFeatures_RES_POINTS_LEFT[i]);
	}

	
	public void UnlockAll()
	{
		for (int i = 0; i < this.engineFeatures_UNLOCK.Length; i++)
		{
			this.engineFeatures_UNLOCK[i] = true;
			this.engineFeatures_RES_POINTS_LEFT[i] = 0f;
		}
	}

	
	public bool ForschungGestartet(int i)
	{
		return this.engineFeatures_RES_POINTS_LEFT[i] != (float)this.engineFeatures_RES_POINTS[i];
	}

	
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

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private settingsScript settings_;

	
	private GUI_Main guiMain_;

	
	private genres genres_;

	
	private games games_;

	
	private mpCalls mpCalls_;

	
	public GameObject prefabEngine;

	
	private Sprite[] engineFeatures_PIC;

	
	public Sprite[] engineFeatures_PICTYP;

	
	public int[] engineFeatures_TYP;

	
	public int[] engineFeatures_RES_POINTS;

	
	public float[] engineFeatures_RES_POINTS_LEFT;

	
	public int[] engineFeatures_PRICE;

	
	public int[] engineFeatures_DEV_COSTS;

	
	public int[] engineFeatures_TECH;

	
	public int[] engineFeatures_DATE_YEAR;

	
	public int[] engineFeatures_DATE_MONTH;

	
	public int[] engineFeatures_GAMEPLAY;

	
	public int[] engineFeatures_GRAPHIC;

	
	public int[] engineFeatures_SOUND;

	
	public int[] engineFeatures_TECHNIK;

	
	public int[] engineFeatures_LEVEL;

	
	public bool[] engineFeatures_UNLOCK;

	
	public string[] engineFeatures_ICONFILE;

	
	public string[] engineFeatures_NAME_EN;

	
	public string[] engineFeatures_NAME_GE;

	
	public string[] engineFeatures_NAME_TU;

	
	public string[] engineFeatures_NAME_CH;

	
	public string[] engineFeatures_NAME_FR;

	
	public string[] engineFeatures_NAME_PB;

	
	public string[] engineFeatures_NAME_CT;

	
	public string[] engineFeatures_NAME_HU;

	
	public string[] engineFeatures_NAME_ES;

	
	public string[] engineFeatures_NAME_CZ;

	
	public string[] engineFeatures_NAME_KO;

	
	public string[] engineFeatures_NAME_AR;

	
	public string[] engineFeatures_NAME_RU;

	
	public string[] engineFeatures_NAME_IT;

	
	public string[] engineFeatures_NAME_JA;

	
	public string[] engineFeatures_NAME_PL;

	
	public string[] engineFeatures_DESC_EN;

	
	public string[] engineFeatures_DESC_GE;

	
	public string[] engineFeatures_DESC_TU;

	
	public string[] engineFeatures_DESC_CH;

	
	public string[] engineFeatures_DESC_FR;

	
	public string[] engineFeatures_DESC_PB;

	
	public string[] engineFeatures_DESC_CT;

	
	public string[] engineFeatures_DESC_HU;

	
	public string[] engineFeatures_DESC_ES;

	
	public string[] engineFeatures_DESC_CZ;

	
	public string[] engineFeatures_DESC_KO;

	
	public string[] engineFeatures_DESC_AR;

	
	public string[] engineFeatures_DESC_RU;

	
	public string[] engineFeatures_DESC_IT;

	
	public string[] engineFeatures_DESC_JA;

	
	public string[] engineFeatures_DESC_PL;

	
	private string[] data;
}
