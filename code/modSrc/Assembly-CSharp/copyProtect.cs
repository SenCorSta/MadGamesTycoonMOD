using System;
using System.IO;
using System.Text;
using UnityEngine;


public class copyProtect : MonoBehaviour
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
	}

	
	public copyProtectScript CreateCopyProtect()
	{
		copyProtectScript component = UnityEngine.Object.Instantiate<GameObject>(this.prefabCopyProtect).GetComponent<copyProtectScript>();
		component.main_ = base.gameObject;
		component.mS_ = this.mS_;
		component.tS_ = this.tS_;
		component.settings_ = this.settings_;
		return component;
	}

	
	public void LoadCopyProtect(string filename)
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
		Debug.Log("CopyProtect Amount: " + num.ToString());
		copyProtectScript copyProtectScript = null;
		for (int j = 0; j < this.data.Length; j++)
		{
			if (this.ParseData("[ID]", j))
			{
				copyProtectScript = this.CreateCopyProtect();
				copyProtectScript.myID = int.Parse(this.data[j]);
				copyProtectScript.Init();
			}
			if (copyProtectScript)
			{
				if (this.ParseData("[PRICE]", j))
				{
					copyProtectScript.price = int.Parse(this.data[j]);
				}
				if (this.ParseData("[DEV COSTS]", j))
				{
					copyProtectScript.dev_costs = int.Parse(this.data[j]);
				}
				if (this.ParseData("[ENDLESS]", j))
				{
					copyProtectScript.neverLooseEffect = true;
				}
				if (this.ParseData("[DATE]", j))
				{
					if (this.ParseDataDontCutLastChar("JAN", j))
					{
						copyProtectScript.date_month = 1;
					}
					if (this.ParseDataDontCutLastChar("FEB", j))
					{
						copyProtectScript.date_month = 2;
					}
					if (this.ParseDataDontCutLastChar("MAR", j))
					{
						copyProtectScript.date_month = 3;
					}
					if (this.ParseDataDontCutLastChar("APR", j))
					{
						copyProtectScript.date_month = 4;
					}
					if (this.ParseDataDontCutLastChar("MAY", j))
					{
						copyProtectScript.date_month = 5;
					}
					if (this.ParseDataDontCutLastChar("JUN", j))
					{
						copyProtectScript.date_month = 6;
					}
					if (this.ParseDataDontCutLastChar("JUL", j))
					{
						copyProtectScript.date_month = 7;
					}
					if (this.ParseDataDontCutLastChar("AUG", j))
					{
						copyProtectScript.date_month = 8;
					}
					if (this.ParseDataDontCutLastChar("SEP", j))
					{
						copyProtectScript.date_month = 9;
					}
					if (this.ParseDataDontCutLastChar("OCT", j))
					{
						copyProtectScript.date_month = 10;
					}
					if (this.ParseDataDontCutLastChar("NOV", j))
					{
						copyProtectScript.date_month = 11;
					}
					if (this.ParseDataDontCutLastChar("DEC", j))
					{
						copyProtectScript.date_month = 12;
					}
					if (copyProtectScript.date_month <= 0)
					{
						Debug.Log("ERROR: CopyProtect.txt -> Incorrect Month: " + copyProtectScript.myID.ToString());
					}
					copyProtectScript.date_year = int.Parse(this.data[j]);
				}
				if (this.ParseData("[NAME GE]", j))
				{
					copyProtectScript.name_GE = this.data[j];
				}
				if (this.ParseData("[NAME EN]", j))
				{
					copyProtectScript.name_EN = this.data[j];
				}
				if (this.ParseData("[NAME TU]", j))
				{
					copyProtectScript.name_TU = this.data[j];
				}
				if (this.ParseData("[NAME CH]", j))
				{
					copyProtectScript.name_CH = this.data[j];
				}
				if (this.ParseData("[NAME FR]", j))
				{
					copyProtectScript.name_FR = this.data[j];
				}
				if (this.ParseData("[NAME CT]", j))
				{
					copyProtectScript.name_CT = this.data[j];
				}
				if (this.ParseData("[NAME RU]", j))
				{
					copyProtectScript.name_RU = this.data[j];
				}
				if (this.ParseData("[NAME IT]", j))
				{
					copyProtectScript.name_IT = this.data[j];
				}
				if (this.ParseData("[NAME JA]", j))
				{
					copyProtectScript.name_JA = this.data[j];
				}
				if (this.ParseData("[EOF]", j))
				{
					Debug.Log("CopyProtect.txt -> EOF");
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

	
	public void UpdateEffekt()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("CopyProtect");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				array[i].GetComponent<copyProtectScript>().EffektVerschlechtern();
			}
		}
	}

	
	public GameObject prefabCopyProtect;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private settingsScript settings_;

	
	private string[] data;
}
