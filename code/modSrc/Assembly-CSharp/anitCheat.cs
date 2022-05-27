using System;
using System.IO;
using System.Text;
using UnityEngine;

// Token: 0x02000049 RID: 73
public class anitCheat : MonoBehaviour
{
	// Token: 0x0600017C RID: 380 RVA: 0x00018230 File Offset: 0x00016430
	private void Awake()
	{
		this.FindScripts();
	}

	// Token: 0x0600017D RID: 381 RVA: 0x00018238 File Offset: 0x00016438
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
	}

	// Token: 0x0600017E RID: 382 RVA: 0x00018290 File Offset: 0x00016490
	public antiCheatScript CreateAntiCheat()
	{
		antiCheatScript component = UnityEngine.Object.Instantiate<GameObject>(this.prefabAntiCheat).GetComponent<antiCheatScript>();
		component.main_ = base.gameObject;
		component.mS_ = this.mS_;
		component.tS_ = this.tS_;
		component.settings_ = this.settings_;
		return component;
	}

	// Token: 0x0600017F RID: 383 RVA: 0x000182E0 File Offset: 0x000164E0
	public void LoadAnitCheat(string filename)
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
		antiCheatScript antiCheatScript = null;
		for (int j = 0; j < this.data.Length; j++)
		{
			if (this.ParseData("[ID]", j))
			{
				antiCheatScript = this.CreateAntiCheat();
				antiCheatScript.myID = int.Parse(this.data[j]);
				antiCheatScript.Init();
			}
			if (antiCheatScript)
			{
				if (this.ParseData("[PRICE]", j))
				{
					antiCheatScript.price = int.Parse(this.data[j]);
				}
				if (this.ParseData("[DEV COSTS]", j))
				{
					antiCheatScript.dev_costs = int.Parse(this.data[j]);
				}
				if (this.ParseData("[ENDLESS]", j))
				{
					antiCheatScript.neverLooseEffect = true;
				}
				if (this.ParseData("[DATE]", j))
				{
					if (this.ParseDataDontCutLastChar("JAN", j))
					{
						antiCheatScript.date_month = 1;
					}
					if (this.ParseDataDontCutLastChar("FEB", j))
					{
						antiCheatScript.date_month = 2;
					}
					if (this.ParseDataDontCutLastChar("MAR", j))
					{
						antiCheatScript.date_month = 3;
					}
					if (this.ParseDataDontCutLastChar("APR", j))
					{
						antiCheatScript.date_month = 4;
					}
					if (this.ParseDataDontCutLastChar("MAY", j))
					{
						antiCheatScript.date_month = 5;
					}
					if (this.ParseDataDontCutLastChar("JUN", j))
					{
						antiCheatScript.date_month = 6;
					}
					if (this.ParseDataDontCutLastChar("JUL", j))
					{
						antiCheatScript.date_month = 7;
					}
					if (this.ParseDataDontCutLastChar("AUG", j))
					{
						antiCheatScript.date_month = 8;
					}
					if (this.ParseDataDontCutLastChar("SEP", j))
					{
						antiCheatScript.date_month = 9;
					}
					if (this.ParseDataDontCutLastChar("OCT", j))
					{
						antiCheatScript.date_month = 10;
					}
					if (this.ParseDataDontCutLastChar("NOV", j))
					{
						antiCheatScript.date_month = 11;
					}
					if (this.ParseDataDontCutLastChar("DEC", j))
					{
						antiCheatScript.date_month = 12;
					}
					if (antiCheatScript.date_month <= 0)
					{
						Debug.Log("ERROR: AntiCheat.txt -> Incorrect Month: " + antiCheatScript.myID.ToString());
					}
					antiCheatScript.date_year = int.Parse(this.data[j]);
				}
				if (this.ParseData("[NAME GE]", j))
				{
					antiCheatScript.name_GE = this.data[j];
				}
				if (this.ParseData("[NAME EN]", j))
				{
					antiCheatScript.name_EN = this.data[j];
				}
				if (this.ParseData("[NAME TU]", j))
				{
					antiCheatScript.name_TU = this.data[j];
				}
				if (this.ParseData("[NAME CH]", j))
				{
					antiCheatScript.name_CH = this.data[j];
				}
				if (this.ParseData("[NAME FR]", j))
				{
					antiCheatScript.name_FR = this.data[j];
				}
				if (this.ParseData("[NAME CT]", j))
				{
					antiCheatScript.name_CT = this.data[j];
				}
				if (this.ParseData("[NAME RU]", j))
				{
					antiCheatScript.name_RU = this.data[j];
				}
				if (this.ParseData("[NAME IT]", j))
				{
					antiCheatScript.name_IT = this.data[j];
				}
				if (this.ParseData("[NAME JA]", j))
				{
					antiCheatScript.name_JA = this.data[j];
				}
				if (this.ParseData("[EOF]", j))
				{
					break;
				}
			}
		}
	}

	// Token: 0x06000180 RID: 384 RVA: 0x00018688 File Offset: 0x00016888
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

	// Token: 0x06000181 RID: 385 RVA: 0x000186E8 File Offset: 0x000168E8
	private bool ParseDataDontCutLastChar(string c, int i)
	{
		if (this.data[i].Contains(c))
		{
			this.data[i] = this.data[i].Replace(c, "");
			return true;
		}
		return false;
	}

	// Token: 0x06000182 RID: 386 RVA: 0x00018718 File Offset: 0x00016918
	public void UpdateEffekt()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("AntiCheat");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				array[i].GetComponent<antiCheatScript>().EffektVerschlechtern();
			}
		}
	}

	// Token: 0x0400036B RID: 875
	public GameObject prefabAntiCheat;

	// Token: 0x0400036C RID: 876
	private mainScript mS_;

	// Token: 0x0400036D RID: 877
	private textScript tS_;

	// Token: 0x0400036E RID: 878
	private settingsScript settings_;

	// Token: 0x0400036F RID: 879
	private string[] data;
}
