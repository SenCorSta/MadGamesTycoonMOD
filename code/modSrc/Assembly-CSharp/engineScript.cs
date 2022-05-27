using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000050 RID: 80
public class engineScript : MonoBehaviour
{
	// Token: 0x060001D9 RID: 473 RVA: 0x0001ACA4 File Offset: 0x00018EA4
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060001DA RID: 474 RVA: 0x0001ACAC File Offset: 0x00018EAC
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

	// Token: 0x060001DB RID: 475 RVA: 0x0001ADCE File Offset: 0x00018FCE
	public void Init()
	{
		this.FindScripts();
		base.name = "ENGINE_" + this.myID.ToString();
	}

	// Token: 0x060001DC RID: 476 RVA: 0x0001ADF4 File Offset: 0x00018FF4
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

	// Token: 0x060001DD RID: 477 RVA: 0x0001AE8E File Offset: 0x0001908E
	public bool Complete()
	{
		return this.devPoints <= 0f;
	}

	// Token: 0x060001DE RID: 478 RVA: 0x0001AEA0 File Offset: 0x000190A0
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

	// Token: 0x060001DF RID: 479 RVA: 0x0001AF10 File Offset: 0x00019110
	public void CopyFeatures()
	{
		this.features = new bool[this.eF_.engineFeatures_PRICE.Length];
		for (int i = 0; i < this.features.Length; i++)
		{
			this.features[i] = this.featuresInDev[i];
		}
	}

	// Token: 0x060001E0 RID: 480 RVA: 0x0001AF58 File Offset: 0x00019158
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

	// Token: 0x060001E1 RID: 481 RVA: 0x0001AFA4 File Offset: 0x000191A4
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

	// Token: 0x060001E2 RID: 482 RVA: 0x0001AFD8 File Offset: 0x000191D8
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

	// Token: 0x060001E3 RID: 483 RVA: 0x0001B038 File Offset: 0x00019238
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

	// Token: 0x060001E4 RID: 484 RVA: 0x0001B093 File Offset: 0x00019293
	public string GetReleaseDateString()
	{
		return this.tS_.GetText(this.date_month + 220) + " " + this.date_year.ToString();
	}

	// Token: 0x060001E5 RID: 485 RVA: 0x0001B0C4 File Offset: 0x000192C4
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
		if (this.mS_.multiplayer && this.ownerID != -1)
		{
			text = text + "<color=blue>" + this.mpCalls_.GetCompanyName(this.ownerID) + "</color>\n";
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
		if (this.ownerID == this.mS_.myID)
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

	// Token: 0x060001E6 RID: 486 RVA: 0x0001B494 File Offset: 0x00019694
	public string GetName()
	{
		if (this.mS_.multiplayer && this.EngineFromMitspieler())
		{
			return this.myName;
		}
		if (!this.OwnerIsNPC())
		{
			return this.myName;
		}
		string text;
		switch (this.settings_.language)
		{
		case 0:
			text = this.name_EN;
			goto IL_F8;
		case 1:
			text = this.name_GE;
			goto IL_F8;
		case 2:
			text = this.name_TU;
			goto IL_F8;
		case 3:
			text = this.name_CH;
			goto IL_F8;
		case 4:
			text = this.name_FR;
			goto IL_F8;
		case 7:
			text = this.name_PB;
			goto IL_F8;
		case 8:
			text = this.name_HU;
			goto IL_F8;
		case 10:
			text = this.name_CT;
			goto IL_F8;
		case 11:
			text = this.name_PL;
			goto IL_F8;
		case 12:
			text = this.name_CZ;
			goto IL_F8;
		case 14:
			text = this.name_IT;
			goto IL_F8;
		case 16:
			text = this.name_JA;
			goto IL_F8;
		}
		text = this.name_EN;
		IL_F8:
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

	// Token: 0x060001E7 RID: 487 RVA: 0x0001B5BB File Offset: 0x000197BB
	public platformScript GetSpezialPlatformScript()
	{
		this.FindSpecialPlatformScript();
		return this.specialPlatformS_;
	}

	// Token: 0x060001E8 RID: 488 RVA: 0x0001B5CC File Offset: 0x000197CC
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

	// Token: 0x060001E9 RID: 489 RVA: 0x0001B62D File Offset: 0x0001982D
	public float GetProzent()
	{
		return 100f / this.devPointsStart * (this.devPointsStart - this.devPoints);
	}

	// Token: 0x060001EA RID: 490 RVA: 0x0001B64C File Offset: 0x0001984C
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

	// Token: 0x060001EB RID: 491 RVA: 0x0001B710 File Offset: 0x00019910
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

	// Token: 0x060001EC RID: 492 RVA: 0x0001B758 File Offset: 0x00019958
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
			bool flag = false;
			if (pS_.IsTochterfirma() && pS_.tf_engine == this.myID)
			{
				flag = true;
			}
			if (!flag)
			{
				if (this.mS_.myID == this.ownerID)
				{
					this.mS_.Earn((long)this.preis, 4);
					string text = this.tS_.GetText(500);
					text = text.Replace("<NAME1>", pS_.GetName());
					text = text.Replace("<NAME2>", this.GetName());
					text = text + "\n<color=green><b>+" + this.mS_.GetMoney((long)this.preis, true) + "</b></color>";
					this.guiMain_.CreateTopNewsInfo(text);
				}
				if (this.mS_.multiplayer && this.EngineFromMitspieler())
				{
					this.mS_.mpCalls_.SERVER_Send_Payment(this.mS_.myID, this.ownerID, 3, this.preis);
				}
			}
		}
	}

	// Token: 0x060001ED RID: 493 RVA: 0x0001B898 File Offset: 0x00019A98
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

	// Token: 0x060001EE RID: 494 RVA: 0x0001B8D2 File Offset: 0x00019AD2
	public bool OwnerIsNPC()
	{
		return this.ownerID < 100000;
	}

	// Token: 0x060001EF RID: 495 RVA: 0x0001B8E4 File Offset: 0x00019AE4
	public bool EngineFromMitspieler()
	{
		return this.mS_.multiplayer && this.ownerID >= 100000 && this.ownerID != this.mS_.myID && this.ownerID >= 100000;
	}

	// Token: 0x040003F8 RID: 1016
	public mainScript mS_;

	// Token: 0x040003F9 RID: 1017
	public GameObject main_;

	// Token: 0x040003FA RID: 1018
	public GUI_Main guiMain_;

	// Token: 0x040003FB RID: 1019
	public textScript tS_;

	// Token: 0x040003FC RID: 1020
	public settingsScript settings_;

	// Token: 0x040003FD RID: 1021
	public engineFeatures eF_;

	// Token: 0x040003FE RID: 1022
	public genres genres_;

	// Token: 0x040003FF RID: 1023
	public games games_;

	// Token: 0x04000400 RID: 1024
	public platformScript specialPlatformS_;

	// Token: 0x04000401 RID: 1025
	public mpCalls mpCalls_;

	// Token: 0x04000402 RID: 1026
	public int myID;

	// Token: 0x04000403 RID: 1027
	public int ownerID;

	// Token: 0x04000404 RID: 1028
	public bool isUnlocked;

	// Token: 0x04000405 RID: 1029
	public bool gekauft;

	// Token: 0x04000406 RID: 1030
	public string myName;

	// Token: 0x04000407 RID: 1031
	public int umsatz;

	// Token: 0x04000408 RID: 1032
	public string name_EN;

	// Token: 0x04000409 RID: 1033
	public string name_GE;

	// Token: 0x0400040A RID: 1034
	public string name_TU;

	// Token: 0x0400040B RID: 1035
	public string name_CH;

	// Token: 0x0400040C RID: 1036
	public string name_FR;

	// Token: 0x0400040D RID: 1037
	public string name_HU;

	// Token: 0x0400040E RID: 1038
	public string name_CT;

	// Token: 0x0400040F RID: 1039
	public string name_CZ;

	// Token: 0x04000410 RID: 1040
	public string name_PB;

	// Token: 0x04000411 RID: 1041
	public string name_IT;

	// Token: 0x04000412 RID: 1042
	public string name_JA;

	// Token: 0x04000413 RID: 1043
	public string name_PL;

	// Token: 0x04000414 RID: 1044
	public bool[] features;

	// Token: 0x04000415 RID: 1045
	public bool[] featuresInDev;

	// Token: 0x04000416 RID: 1046
	public int spezialgenre;

	// Token: 0x04000417 RID: 1047
	public int spezialplatform;

	// Token: 0x04000418 RID: 1048
	public int spezialplatformUpdate;

	// Token: 0x04000419 RID: 1049
	public bool sellEngine;

	// Token: 0x0400041A RID: 1050
	public int preis;

	// Token: 0x0400041B RID: 1051
	public int gewinnbeteiligung;

	// Token: 0x0400041C RID: 1052
	public bool updating;

	// Token: 0x0400041D RID: 1053
	public float devPoints;

	// Token: 0x0400041E RID: 1054
	public float devPointsStart;

	// Token: 0x0400041F RID: 1055
	public int date_year;

	// Token: 0x04000420 RID: 1056
	public int date_month;

	// Token: 0x04000421 RID: 1057
	public bool[] publisherBuyed;

	// Token: 0x04000422 RID: 1058
	public bool archiv_engine;
}
