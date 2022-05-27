using System;
using UnityEngine;
using UnityEngine.UI;


public class engineScript : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.settings_)
		{
			this.settings_ = this.main_.GetComponent<settingsScript>();
		}
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
		if (!this.mpCalls_)
		{
			this.mpCalls_ = GameObject.Find("NetworkManager").GetComponent<mpCalls>();
		}
	}

	
	public void Init()
	{
		this.FindScripts();
		base.name = "ENGINE_" + this.myID.ToString();
	}

	
	public void InitNpcEngine()
	{
		this.FindScripts();
		this.features = new bool[this.eF_.engineFeatures_UNLOCK.Length];
		this.featuresInDev = new bool[this.eF_.engineFeatures_UNLOCK.Length];
		this.features[0] = true;
		this.features[20] = true;
		this.features[34] = true;
		this.features[46] = true;
		for (int i = 0; i < this.eF_.engineFeatures_UNLOCK.Length; i++)
		{
			if (this.eF_.engineFeatures_UNLOCK[i])
			{
				this.features[i] = true;
			}
		}
	}

	
	public bool Complete()
	{
		return this.devPoints <= 0f;
	}

	
	public int GetGamesAmount()
	{
		int num = 0;
		for (int i = 0; i < this.games_.arrayGamesScripts.Length; i++)
		{
			if (this.games_.arrayGamesScripts[i] && this.games_.arrayGamesScripts[i].engineID == this.myID && !this.games_.arrayGamesScripts[i].inDevelopment)
			{
				num++;
			}
		}
		return num;
	}

	
	public void CopyFeatures()
	{
		this.features = new bool[this.eF_.engineFeatures_PRICE.Length];
		for (int i = 0; i < this.features.Length; i++)
		{
			this.features[i] = this.featuresInDev[i];
		}
	}

	
	public int GetTechLevel()
	{
		int num = 0;
		for (int i = 0; i < this.features.Length; i++)
		{
			if (this.features[i] && this.eF_.engineFeatures_TECH[i] > num)
			{
				num = this.eF_.engineFeatures_TECH[i];
			}
		}
		return num;
	}

	
	public int GetFeaturesAmount()
	{
		int num = 0;
		for (int i = 0; i < this.features.Length; i++)
		{
			if (this.features[i])
			{
				num++;
			}
		}
		return num;
	}

	
	public int GetBestFeature(int art)
	{
		int num = -1;
		int result = 0;
		for (int i = 0; i < this.features.Length; i++)
		{
			if (this.features[i] && this.eF_.engineFeatures_TYP[i] == art && this.eF_.engineFeatures_RES_POINTS[i] > num)
			{
				num = this.eF_.engineFeatures_RES_POINTS[i];
				result = i;
			}
		}
		return result;
	}

	
	private void FindSpecialPlatformScript()
	{
		if (this.spezialplatform == -1)
		{
			return;
		}
		if (!this.specialPlatformS_)
		{
			GameObject gameObject = GameObject.Find("PLATFORM_" + this.spezialplatform.ToString());
			if (gameObject)
			{
				this.specialPlatformS_ = gameObject.GetComponent<platformScript>();
				return;
			}
			this.spezialplatform = -1;
		}
	}

	
	public string GetReleaseDateString()
	{
		return this.tS_.GetText(this.date_month + 220) + " " + this.date_year.ToString();
	}

	
	public string GetTooltip()
	{
		this.FindSpecialPlatformScript();
		string text = "<b>" + this.GetName() + "</b>";
		text = string.Concat(new string[]
		{
			text,
			"\n<color=magenta>",
			this.tS_.GetText(4),
			" ",
			this.GetTechLevel().ToString(),
			"</color>\n"
		});
		if (this.date_year > 0)
		{
			text = text + "<color=blue>" + this.GetReleaseDateString() + "</color>\n";
		}
		if (this.mS_.multiplayer && this.multiplayerSlot != -1)
		{
			text = text + "<color=blue>" + this.mpCalls_.GetCompanyName(this.multiplayerSlot) + "</color>\n";
		}
		if (this.specialPlatformS_)
		{
			text = string.Concat(new string[]
			{
				text,
				"\n",
				this.tS_.GetText(1218),
				": <color=blue>",
				this.specialPlatformS_.GetName(),
				"</color>"
			});
		}
		else
		{
			text = string.Concat(new string[]
			{
				text,
				"\n",
				this.tS_.GetText(1218),
				": <color=blue>",
				this.tS_.GetText(1221),
				"</color>"
			});
		}
		text = string.Concat(new string[]
		{
			text,
			"\n",
			this.tS_.GetText(245),
			": <color=blue>",
			this.genres_.GetName(this.spezialgenre),
			"</color>"
		});
		text = string.Concat(new string[]
		{
			text,
			"\n",
			this.tS_.GetText(160),
			": <color=blue>",
			this.GetFeaturesAmount().ToString(),
			"</color>"
		});
		text = string.Concat(new string[]
		{
			text,
			"\n",
			this.tS_.GetText(261),
			": <color=blue>",
			this.GetGamesAmount().ToString(),
			"</color>\n"
		});
		if (this.sellEngine)
		{
			text = string.Concat(new string[]
			{
				text,
				"\n",
				this.tS_.GetText(88),
				": <color=blue>",
				this.mS_.GetMoney((long)this.preis, true),
				"</color>"
			});
			text = string.Concat(new string[]
			{
				text,
				"\n",
				this.tS_.GetText(260),
				": <color=blue>",
				this.gewinnbeteiligung.ToString(),
				"%</color>"
			});
		}
		if (this.playerEngine)
		{
			text = string.Concat(new string[]
			{
				text,
				"\n",
				this.tS_.GetText(268),
				": <color=blue>",
				this.GetVerkaufteLizenzen().ToString(),
				"</color>"
			});
			text = string.Concat(new string[]
			{
				text,
				"\n",
				this.tS_.GetText(276),
				": <color=blue>",
				this.mS_.GetMoney((long)this.umsatz, true),
				"</color>"
			});
			text = text + "\n\n<color=blue><b>" + this.tS_.GetText(262) + "</b></color>";
		}
		return text;
	}

	
	public string GetName()
	{
		if (this.mS_.multiplayer && !this.playerEngine && this.multiplayerSlot != -1)
		{
			return this.myName;
		}
		if (this.playerEngine)
		{
			return this.myName;
		}
		string text;
		switch (this.settings_.language)
		{
		case 0:
			text = this.name_EN;
			goto IL_101;
		case 1:
			text = this.name_GE;
			goto IL_101;
		case 2:
			text = this.name_TU;
			goto IL_101;
		case 3:
			text = this.name_CH;
			goto IL_101;
		case 4:
			text = this.name_FR;
			goto IL_101;
		case 7:
			text = this.name_PB;
			goto IL_101;
		case 8:
			text = this.name_HU;
			goto IL_101;
		case 10:
			text = this.name_CT;
			goto IL_101;
		case 11:
			text = this.name_PL;
			goto IL_101;
		case 12:
			text = this.name_CZ;
			goto IL_101;
		case 14:
			text = this.name_IT;
			goto IL_101;
		case 16:
			text = this.name_JA;
			goto IL_101;
		}
		text = this.name_EN;
		IL_101:
		if (text == null)
		{
			return this.name_EN;
		}
		if (text.Length <= 0)
		{
			return this.name_EN;
		}
		return text;
	}

	
	public platformScript GetSpezialPlatformScript()
	{
		this.FindSpecialPlatformScript();
		return this.specialPlatformS_;
	}

	
	public void SetSpezialPlatformSprite(GameObject go)
	{
		this.FindSpecialPlatformScript();
		if (this.specialPlatformS_)
		{
			this.specialPlatformS_.SetPic(go);
			return;
		}
		go.GetComponent<Image>().material = null;
		go.GetComponent<Image>().sprite = this.guiMain_.uiSprites[3];
		go.GetComponent<Image>().color = Color.white;
	}

	
	public float GetProzent()
	{
		return 100f / this.devPointsStart * (this.devPointsStart - this.devPoints);
	}

	
	public void SetComplete()
	{
		this.date_month = this.mS_.month;
		this.date_year = this.mS_.year;
		this.specialPlatformS_ = null;
		this.spezialplatform = this.spezialplatformUpdate;
		for (int i = 0; i < this.featuresInDev.Length; i++)
		{
			if (this.featuresInDev[i])
			{
				this.features[i] = true;
			}
		}
		this.EntwicklungBeenden();
		if (this.mS_.multiplayer)
		{
			if (this.mS_.mpCalls_.isServer)
			{
				this.mS_.mpCalls_.SERVER_Send_Engine(this);
			}
			if (this.mS_.mpCalls_.isClient)
			{
				this.mS_.mpCalls_.CLIENT_Send_Engine(this);
			}
		}
	}

	
	public void EntwicklungBeenden()
	{
		this.devPoints = 0f;
		this.devPointsStart = 0f;
		this.updating = false;
		for (int i = 0; i < this.featuresInDev.Length; i++)
		{
			this.featuresInDev[i] = false;
		}
	}

	
	public void SellPlayerEngine(publisherScript pS_)
	{
		if (!pS_)
		{
			return;
		}
		if (this.publisherBuyed.Length == 0)
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag("Publisher");
			this.publisherBuyed = new bool[array.Length];
		}
		if (!this.publisherBuyed[pS_.myID])
		{
			this.publisherBuyed[pS_.myID] = true;
			if (this.playerEngine)
			{
				this.mS_.Earn((long)this.preis, 4);
				string text = this.tS_.GetText(500);
				text = text.Replace("<NAME1>", pS_.GetName());
				text = text.Replace("<NAME2>", this.GetName());
				text = text + "\n<color=green><b>+" + this.mS_.GetMoney((long)this.preis, true) + "</b></color>";
				this.guiMain_.CreateTopNewsInfo(text);
			}
			if (this.mS_.multiplayer && !this.playerEngine && this.multiplayerSlot != -1)
			{
				this.mS_.mpCalls_.SERVER_Send_Payment(this.mS_.mpCalls_.myID, this.multiplayerSlot, 3, this.preis);
			}
		}
	}

	
	public int GetVerkaufteLizenzen()
	{
		int num = 0;
		if (this.publisherBuyed.Length != 0)
		{
			for (int i = 0; i < this.publisherBuyed.Length; i++)
			{
				if (this.publisherBuyed[i])
				{
					num++;
				}
			}
		}
		return num;
	}

	
	public mainScript mS_;

	
	public GameObject main_;

	
	public GUI_Main guiMain_;

	
	public textScript tS_;

	
	public settingsScript settings_;

	
	public engineFeatures eF_;

	
	public genres genres_;

	
	public games games_;

	
	public platformScript specialPlatformS_;

	
	public mpCalls mpCalls_;

	
	public int myID;

	
	public bool playerEngine;

	
	public int multiplayerSlot;

	
	public bool isUnlocked;

	
	public bool gekauft;

	
	public string myName;

	
	public int umsatz;

	
	public string name_EN;

	
	public string name_GE;

	
	public string name_TU;

	
	public string name_CH;

	
	public string name_FR;

	
	public string name_HU;

	
	public string name_CT;

	
	public string name_CZ;

	
	public string name_PB;

	
	public string name_IT;

	
	public string name_JA;

	
	public string name_PL;

	
	public bool[] features;

	
	public bool[] featuresInDev;

	
	public int spezialgenre;

	
	public int spezialplatform;

	
	public int spezialplatformUpdate;

	
	public bool sellEngine;

	
	public int preis;

	
	public int gewinnbeteiligung;

	
	public bool updating;

	
	public float devPoints;

	
	public float devPointsStart;

	
	public int date_year;

	
	public int date_month;

	
	public bool[] publisherBuyed;

	
	public bool archiv_engine;
}
