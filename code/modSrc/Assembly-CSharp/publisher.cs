﻿using System;
using System.IO;
using System.Text;
using UnityEngine;


public class publisher : MonoBehaviour
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
		if (!this.gF_)
		{
			this.gF_ = base.GetComponent<gameplayFeatures>();
		}
		if (!this.eF_)
		{
			this.eF_ = base.GetComponent<engineFeatures>();
		}
		if (!this.games_)
		{
			this.games_ = base.GetComponent<games>();
		}
		if (!this.unlock_)
		{
			this.unlock_ = base.GetComponent<unlockScript>();
		}
		if (!this.genres_)
		{
			this.genres_ = base.GetComponent<genres>();
		}
		if (!this.platforms_)
		{
			this.platforms_ = base.GetComponent<platforms>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.reviewText_)
		{
			this.reviewText_ = base.GetComponent<reviewText>();
		}
	}

	
	public publisherScript CreatePublisher()
	{
		this.FindScripts();
		publisherScript component = UnityEngine.Object.Instantiate<GameObject>(this.prefabPublisher).GetComponent<publisherScript>();
		component.main_ = base.gameObject;
		component.mS_ = this.mS_;
		component.tS_ = this.tS_;
		component.eF_ = this.eF_;
		component.guiMain_ = this.guiMain_;
		component.settings_ = this.settings_;
		component.genres_ = this.genres_;
		component.games_ = this.games_;
		component.gF_ = this.gF_;
		component.unlock_ = this.unlock_;
		component.platforms_ = this.platforms_;
		component.reviewText_ = this.reviewText_;
		return component;
	}

	
	public void LoadPublisher(string filename)
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
		publisherScript publisherScript = null;
		for (int j = 0; j < this.data.Length; j++)
		{
			if (this.ParseData("[ID]", j))
			{
				publisherScript = this.CreatePublisher();
				publisherScript.myID = int.Parse(this.data[j]);
				publisherScript.SetNewGameInWeeks(0);
				publisherScript.Init();
			}
			if (publisherScript)
			{
				if (this.ParseData("[MARKET]", j))
				{
					publisherScript.stars = (float)int.Parse(this.data[j]);
				}
				if (this.ParseData("[PIC]", j))
				{
					publisherScript.logoID = int.Parse(this.data[j]);
				}
				if (this.ParseData("[SHARE]", j))
				{
					publisherScript.share = (float)int.Parse(this.data[j]);
				}
				if (this.ParseData("[GENRE]", j))
				{
					publisherScript.fanGenre = int.Parse(this.data[j]);
				}
				if (this.ParseData("[SPEED]", j))
				{
					publisherScript.developmentSpeed = int.Parse(this.data[j]);
				}
				if (this.ParseData("[COMVAL]", j))
				{
					publisherScript.firmenwert = (long)int.Parse(this.data[j]);
				}
				if (this.ParseData("[COUNTRY]", j))
				{
					publisherScript.country = int.Parse(this.data[j]);
				}
				if (this.ParseData("[DEVELOPER]", j))
				{
					if (this.ParseDataDontCutLastChar("true", j))
					{
						publisherScript.developer = true;
					}
					else
					{
						publisherScript.developer = false;
					}
				}
				if (this.ParseData("[PUBLISHER]", j))
				{
					if (this.ParseDataDontCutLastChar("true", j))
					{
						publisherScript.publisher = true;
					}
					else
					{
						publisherScript.publisher = false;
					}
				}
				if (this.ParseData("[ONLYMOBILE]", j))
				{
					if (this.ParseDataDontCutLastChar("true", j))
					{
						publisherScript.onlyMobile = true;
						publisherScript.publisher = true;
					}
					else
					{
						publisherScript.onlyMobile = false;
					}
				}
				if (this.ParseData("[PLATFORM]", j))
				{
					if (this.ParseDataDontCutLastChar("true", j))
					{
						publisherScript.ownPlatform = true;
					}
					else
					{
						publisherScript.ownPlatform = false;
					}
				}
				if (this.ParseData("[EXCLUSIVE]", j))
				{
					if (this.ParseDataDontCutLastChar("true", j))
					{
						publisherScript.exklusive = true;
					}
					else
					{
						publisherScript.exklusive = false;
					}
				}
				if (this.ParseData("[NOTFORSALE]", j))
				{
					if (this.ParseDataDontCutLastChar("true", j))
					{
						publisherScript.notForSale = true;
					}
					else
					{
						publisherScript.notForSale = false;
					}
				}
				if (this.ParseData("[DATE]", j))
				{
					if (this.ParseDataDontCutLastChar("JAN", j))
					{
						publisherScript.date_month = 1;
					}
					if (this.ParseDataDontCutLastChar("FEB", j))
					{
						publisherScript.date_month = 2;
					}
					if (this.ParseDataDontCutLastChar("MAR", j))
					{
						publisherScript.date_month = 3;
					}
					if (this.ParseDataDontCutLastChar("APR", j))
					{
						publisherScript.date_month = 4;
					}
					if (this.ParseDataDontCutLastChar("MAY", j))
					{
						publisherScript.date_month = 5;
					}
					if (this.ParseDataDontCutLastChar("JUN", j))
					{
						publisherScript.date_month = 6;
					}
					if (this.ParseDataDontCutLastChar("JUL", j))
					{
						publisherScript.date_month = 7;
					}
					if (this.ParseDataDontCutLastChar("AUG", j))
					{
						publisherScript.date_month = 8;
					}
					if (this.ParseDataDontCutLastChar("SEP", j))
					{
						publisherScript.date_month = 9;
					}
					if (this.ParseDataDontCutLastChar("OCT", j))
					{
						publisherScript.date_month = 10;
					}
					if (this.ParseDataDontCutLastChar("NOV", j))
					{
						publisherScript.date_month = 11;
					}
					if (this.ParseDataDontCutLastChar("DEC", j))
					{
						publisherScript.date_month = 12;
					}
					if (publisherScript.date_month <= 0)
					{
						Debug.Log("ERROR: Publisher.txt -> Incorrect Month: " + publisherScript.myID.ToString());
					}
					publisherScript.date_year = int.Parse(this.data[j]);
					if (publisherScript.date_year == 1976 && publisherScript.date_month == 1)
					{
						publisherScript.isUnlocked = true;
					}
				}
				if (this.ParseData("[NAME GE]", j))
				{
					publisherScript.name_GE = this.data[j];
				}
				if (this.ParseData("[NAME EN]", j))
				{
					publisherScript.name_EN = this.data[j];
				}
				if (this.ParseData("[NAME TU]", j))
				{
					publisherScript.name_TU = this.data[j];
				}
				if (this.ParseData("[NAME CH]", j))
				{
					publisherScript.name_CH = this.data[j];
				}
				if (this.ParseData("[NAME FR]", j))
				{
					publisherScript.name_FR = this.data[j];
				}
				if (this.ParseData("[NAME JA]", j))
				{
					publisherScript.name_JA = this.data[j];
				}
				if (this.ParseData("[EOF]", j))
				{
					break;
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

	
	public GameObject prefabPublisher;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private settingsScript settings_;

	
	private gameplayFeatures gF_;

	
	private engineFeatures eF_;

	
	private games games_;

	
	private unlockScript unlock_;

	
	private genres genres_;

	
	public GUI_Main guiMain_;

	
	public platforms platforms_;

	
	public reviewText reviewText_;

	
	private string[] data;
}
