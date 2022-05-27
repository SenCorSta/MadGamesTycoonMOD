using System;
using System.IO;
using System.Text;
using UnityEngine;


public class npcEngines : MonoBehaviour
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
		if (!this.eF_)
		{
			this.eF_ = base.GetComponent<engineFeatures>();
		}
	}

	
	public void LoadNpcEngines(string filename)
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
		Debug.Log("NpcEngines Amount: " + num.ToString());
		engineScript engineScript = null;
		for (int j = 0; j < this.data.Length; j++)
		{
			if (this.ParseData("[ID]", j))
			{
				engineScript = this.eF_.CreateEngine();
				engineScript.myID = int.Parse(this.data[j]);
				engineScript.playerEngine = false;
				engineScript.multiplayerSlot = -1;
				engineScript.sellEngine = true;
				engineScript.devPoints = 0f;
				engineScript.Init();
				if (engineScript.myID == 0)
				{
					engineScript.gekauft = true;
					engineScript.isUnlocked = true;
					engineScript.InitNpcEngine();
				}
			}
			if (engineScript)
			{
				if (this.ParseData("[PRICE]", j))
				{
					engineScript.preis = int.Parse(this.data[j]);
				}
				if (this.ParseData("[GENRE]", j))
				{
					engineScript.spezialgenre = int.Parse(this.data[j]);
				}
				if (this.ParseData("[PLATFORM]", j))
				{
					engineScript.spezialplatform = int.Parse(this.data[j]);
				}
				if (this.ParseData("[SHARE]", j))
				{
					engineScript.gewinnbeteiligung = int.Parse(this.data[j]);
				}
				if (this.ParseData("[DATE]", j))
				{
					if (this.ParseDataDontCutLastChar("JAN", j))
					{
						engineScript.date_month = 1;
					}
					if (this.ParseDataDontCutLastChar("FEB", j))
					{
						engineScript.date_month = 2;
					}
					if (this.ParseDataDontCutLastChar("MAR", j))
					{
						engineScript.date_month = 3;
					}
					if (this.ParseDataDontCutLastChar("APR", j))
					{
						engineScript.date_month = 4;
					}
					if (this.ParseDataDontCutLastChar("MAY", j))
					{
						engineScript.date_month = 5;
					}
					if (this.ParseDataDontCutLastChar("JUN", j))
					{
						engineScript.date_month = 6;
					}
					if (this.ParseDataDontCutLastChar("JUL", j))
					{
						engineScript.date_month = 7;
					}
					if (this.ParseDataDontCutLastChar("AUG", j))
					{
						engineScript.date_month = 8;
					}
					if (this.ParseDataDontCutLastChar("SEP", j))
					{
						engineScript.date_month = 9;
					}
					if (this.ParseDataDontCutLastChar("OCT", j))
					{
						engineScript.date_month = 10;
					}
					if (this.ParseDataDontCutLastChar("NOV", j))
					{
						engineScript.date_month = 11;
					}
					if (this.ParseDataDontCutLastChar("DEC", j))
					{
						engineScript.date_month = 12;
					}
					if (engineScript.date_month <= 0)
					{
						Debug.Log("ERROR: NpcEngines.txt -> Incorrect Month: " + engineScript.myID.ToString());
					}
					engineScript.date_year = int.Parse(this.data[j]);
				}
				if (this.ParseData("[NAME GE]", j))
				{
					engineScript.name_GE = this.data[j];
				}
				if (this.ParseData("[NAME EN]", j))
				{
					engineScript.name_EN = this.data[j];
				}
				if (this.ParseData("[NAME TU]", j))
				{
					engineScript.name_TU = this.data[j];
				}
				if (this.ParseData("[NAME CH]", j))
				{
					engineScript.name_CH = this.data[j];
				}
				if (this.ParseData("[NAME FR]", j))
				{
					engineScript.name_FR = this.data[j];
				}
				if (this.ParseData("[NAME HU]", j))
				{
					engineScript.name_HU = this.data[j];
				}
				if (this.ParseData("[NAME CT]", j))
				{
					engineScript.name_CT = this.data[j];
				}
				if (this.ParseData("[NAME CZ]", j))
				{
					engineScript.name_CZ = this.data[j];
				}
				if (this.ParseData("[NAME PB]", j))
				{
					engineScript.name_PB = this.data[j];
				}
				if (this.ParseData("[NAME IT]", j))
				{
					engineScript.name_IT = this.data[j];
				}
				if (this.ParseData("[NAME JA]", j))
				{
					engineScript.name_JA = this.data[j];
				}
				if (this.ParseData("[NAME PL]", j))
				{
					engineScript.name_PL = this.data[j];
				}
				if (this.ParseData("[EOF]", j))
				{
					Debug.Log("NpcEngines.txt -> EOF");
					return;
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

	
	public GameObject prefabEngine;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private settingsScript settings_;

	
	private engineFeatures eF_;

	
	private string[] data;
}
