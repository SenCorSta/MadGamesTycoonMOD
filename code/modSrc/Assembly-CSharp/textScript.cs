using System;
using System.IO;
using System.Text;
using UnityEngine;

// Token: 0x02000347 RID: 839
public class textScript : MonoBehaviour
{
	// Token: 0x06001ED7 RID: 7895 RVA: 0x00141084 File Offset: 0x0013F284
	private void Awake()
	{
		this.FindScripts();
		if (!this.textLoaded)
		{
			this.textLoaded = true;
			this.LoadGlobalText();
			this.LoadTexts_EN("EN/Text_EN.txt");
			this.LoadTexts_GE("GE/Text_GE.txt");
			this.LoadTexts_TU("TU/Text_TU.txt");
			this.LoadTexts_CH("CH/Text_CH.txt");
			this.LoadTexts_FR("FR/Text_FR.txt");
			this.LoadTexts_ES("ES/Text_ES.txt");
			this.LoadTexts_KO("KO/Text_KO.txt");
			this.LoadTexts_PB("PB/Text_PB.txt");
			this.LoadTexts_HU("HU/Text_HU.txt");
			this.LoadTexts_RU("RU/Text_RU.txt");
			this.LoadTexts_CT("CT/Text_CT.txt");
			this.LoadTexts_PL("PL/Text_PL.txt");
			this.LoadTexts_CZ("CZ/Text_CZ.txt");
			this.LoadTexts_AR("AR/Text_AR.txt");
			this.LoadTexts_IT("IT/Text_IT.txt");
			this.LoadTexts_RO("RO/Text_RO.txt");
			this.LoadTexts_JA("JA/Text_JA.txt");
			this.LoadAchivements_EN("EN/Achivements_EN.txt");
			this.LoadAchivements_GE("GE/Achivements_GE.txt");
			this.LoadAchivements_TU("TU/Achivements_TU.txt");
			this.LoadAchivements_CH("CH/Achivements_CH.txt");
			this.LoadAchivements_FR("FR/Achivements_FR.txt");
			this.LoadAchivements_ES("ES/Achivements_ES.txt");
			this.LoadAchivements_KO("KO/Achivements_KO.txt");
			this.LoadAchivements_PB("PB/Achivements_PB.txt");
			this.LoadAchivements_HU("HU/Achivements_HU.txt");
			this.LoadAchivements_RU("RU/Achivements_RU.txt");
			this.LoadAchivements_CT("CT/Achivements_CT.txt");
			this.LoadAchivements_PL("PL/Achivements_PL.txt");
			this.LoadAchivements_CZ("CZ/Achivements_CZ.txt");
			this.LoadAchivements_AR("AR/Achivements_AR.txt");
			this.LoadAchivements_IT("IT/Achivements_IT.txt");
			this.LoadAchivements_RO("RO/Achivements_RO.txt");
			this.LoadAchivements_JA("JA/Achivements_JA.txt");
			this.LoadObjects_EN("EN/Objects_EN.txt");
			this.LoadObjects_GE("GE/Objects_GE.txt");
			this.LoadObjects_TU("TU/Objects_TU.txt");
			this.LoadObjects_CH("CH/Objects_CH.txt");
			this.LoadObjects_FR("FR/Objects_FR.txt");
			this.LoadObjects_ES("ES/Objects_ES.txt");
			this.LoadObjects_KO("KO/Objects_KO.txt");
			this.LoadObjects_PB("PB/Objects_PB.txt");
			this.LoadObjects_HU("HU/Objects_HU.txt");
			this.LoadObjects_RU("RU/Objects_RU.txt");
			this.LoadObjects_CT("CT/Objects_CT.txt");
			this.LoadObjects_PL("PL/Objects_PL.txt");
			this.LoadObjects_CZ("CZ/Objects_CZ.txt");
			this.LoadObjects_AR("AR/Objects_AR.txt");
			this.LoadObjects_IT("IT/Objects_IT.txt");
			this.LoadObjects_RO("RO/Objects_RO.txt");
			this.LoadObjects_JA("JA/Objects_JA.txt");
			this.LoadCountry_EN("EN/Country_EN.txt");
			this.LoadCountry_GE("GE/Country_GE.txt");
			this.LoadCountry_TU("TU/Country_TU.txt");
			this.LoadCountry_CH("CH/Country_CH.txt");
			this.LoadCountry_FR("FR/Country_FR.txt");
			this.LoadCountry_ES("ES/Country_ES.txt");
			this.LoadCountry_KO("KO/Country_KO.txt");
			this.LoadCountry_PB("PB/Country_PB.txt");
			this.LoadCountry_HU("HU/Country_HU.txt");
			this.LoadCountry_RU("RU/Country_RU.txt");
			this.LoadCountry_CT("CT/Country_CT.txt");
			this.LoadCountry_PL("PL/Country_PL.txt");
			this.LoadCountry_CZ("CZ/Country_CZ.txt");
			this.LoadCountry_AR("AR/Country_AR.txt");
			this.LoadCountry_IT("IT/Country_IT.txt");
			this.LoadCountry_RO("RO/Country_RO.txt");
			this.LoadCountry_JA("JA/Country_JA.txt");
			this.LoadQuotes_EN("EN/Quotes_EN.txt");
			this.LoadQuotes_GE("GE/Quotes_GE.txt");
			this.LoadQuotes_TU("TU/Quotes_TU.txt");
			this.LoadQuotes_CH("CH/Quotes_CH.txt");
			this.LoadQuotes_FR("FR/Quotes_FR.txt");
			this.LoadQuotes_ES("ES/Quotes_ES.txt");
			this.LoadQuotes_KO("KO/Quotes_KO.txt");
			this.LoadQuotes_PB("PB/Quotes_PB.txt");
			this.LoadQuotes_HU("HU/Quotes_HU.txt");
			this.LoadQuotes_RU("RU/Quotes_RU.txt");
			this.LoadQuotes_CT("CT/Quotes_CT.txt");
			this.LoadQuotes_PL("PL/Quotes_PL.txt");
			this.LoadQuotes_CZ("CZ/Quotes_CZ.txt");
			this.LoadQuotes_AR("AR/Quotes_AR.txt");
			this.LoadQuotes_IT("IT/Quotes_IT.txt");
			this.LoadQuotes_RO("RO/Quotes_RO.txt");
			this.LoadQuotes_JA("JA/Quotes_JA.txt");
			this.LoadContractWork_EN("EN/ContractWork_EN.txt");
			this.LoadContractWork_GE("GE/ContractWork_GE.txt");
			this.LoadContractWork_TU("TU/ContractWork_TU.txt");
			this.LoadContractWork_CH("CH/ContractWork_CH.txt");
			this.LoadContractWork_FR("FR/ContractWork_FR.txt");
			this.LoadContractWork_ES("ES/ContractWork_ES.txt");
			this.LoadContractWork_KO("KO/ContractWork_KO.txt");
			this.LoadContractWork_PB("PB/ContractWork_PB.txt");
			this.LoadContractWork_HU("HU/ContractWork_HU.txt");
			this.LoadContractWork_RU("RU/ContractWork_RU.txt");
			this.LoadContractWork_CT("CT/ContractWork_CT.txt");
			this.LoadContractWork_PL("PL/ContractWork_PL.txt");
			this.LoadContractWork_CZ("CZ/ContractWork_CZ.txt");
			this.LoadContractWork_AR("AR/ContractWork_AR.txt");
			this.LoadContractWork_IT("IT/ContractWork_IT.txt");
			this.LoadContractWork_RO("RO/ContractWork_RO.txt");
			this.LoadContractWork_JA("JA/ContractWork_JA.txt");
			this.LoadFanLetter_EN("EN/FanLetter_EN.txt");
			this.LoadFanLetter_GE("GE/FanLetter_GE.txt");
			this.LoadFanLetter_TU("TU/FanLetter_TU.txt");
			this.LoadFanLetter_CH("CH/FanLetter_CH.txt");
			this.LoadFanLetter_FR("FR/FanLetter_FR.txt");
			this.LoadFanLetter_ES("ES/FanLetter_ES.txt");
			this.LoadFanLetter_KO("KO/FanLetter_KO.txt");
			this.LoadFanLetter_PB("PB/FanLetter_PB.txt");
			this.LoadFanLetter_HU("HU/FanLetter_HU.txt");
			this.LoadFanLetter_RU("RU/FanLetter_RU.txt");
			this.LoadFanLetter_CT("CT/FanLetter_CT.txt");
			this.LoadFanLetter_PL("PL/FanLetter_PL.txt");
			this.LoadFanLetter_CZ("CZ/FanLetter_CZ.txt");
			this.LoadFanLetter_AR("AR/FanLetter_AR.txt");
			this.LoadFanLetter_IT("IT/FanLetter_IT.txt");
			this.LoadFanLetter_RO("RO/FanLetter_RO.txt");
			this.LoadFanLetter_JA("JA/FanLetter_JA.txt");
			this.LoadTutorial_EN("EN/Tutorial_EN.txt");
			this.LoadTutorial_GE("GE/Tutorial_GE.txt");
			this.LoadTutorial_TU("TU/Tutorial_TU.txt");
			this.LoadTutorial_CH("CH/Tutorial_CH.txt");
			this.LoadTutorial_FR("FR/Tutorial_FR.txt");
			this.LoadTutorial_ES("ES/Tutorial_ES.txt");
			this.LoadTutorial_KO("KO/Tutorial_KO.txt");
			this.LoadTutorial_PB("PB/Tutorial_PB.txt");
			this.LoadTutorial_HU("HU/Tutorial_HU.txt");
			this.LoadTutorial_RU("RU/Tutorial_RU.txt");
			this.LoadTutorial_CT("CT/Tutorial_CT.txt");
			this.LoadTutorial_PL("PL/Tutorial_PL.txt");
			this.LoadTutorial_CZ("CZ/Tutorial_CZ.txt");
			this.LoadTutorial_AR("AR/Tutorial_AR.txt");
			this.LoadTutorial_IT("IT/Tutorial_IT.txt");
			this.LoadTutorial_RO("RO/Tutorial_RO.txt");
			this.LoadTutorial_JA("JA/Tutorial_JA.txt");
		}
	}

	// Token: 0x06001ED8 RID: 7896 RVA: 0x00141688 File Offset: 0x0013F888
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = base.gameObject;
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.settings_)
		{
			this.settings_ = this.main_.GetComponent<settingsScript>();
		}
		if (!this.themes_)
		{
			this.themes_ = this.main_.GetComponent<themes>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
	}

	// Token: 0x06001ED9 RID: 7897 RVA: 0x00141728 File Offset: 0x0013F928
	public void LoadContent_Themes()
	{
		this.LoadDevLegends("DATA/DevLegends.txt");
		this.LoadThemes_EN("EN/Themes_EN.txt");
		this.LoadThemes_GE("GE/Themes_GE.txt");
		this.LoadThemes_TU("TU/Themes_TU.txt");
		this.LoadThemes_CH("CH/Themes_CH.txt");
		this.LoadThemes_FR("FR/Themes_FR.txt");
		this.LoadThemes_ES("ES/Themes_ES.txt");
		this.LoadThemes_KO("KO/Themes_KO.txt");
		this.LoadThemes_PB("PB/Themes_PB.txt");
		this.LoadThemes_HU("HU/Themes_HU.txt");
		this.LoadThemes_RU("RU/Themes_RU.txt");
		this.LoadThemes_CT("CT/Themes_CT.txt");
		this.LoadThemes_PL("PL/Themes_PL.txt");
		this.LoadThemes_CZ("CZ/Themes_CZ.txt");
		this.LoadThemes_AR("AR/Themes_AR.txt");
		this.LoadThemes_IT("IT/Themes_IT.txt");
		this.LoadThemes_RO("RO/Themes_RO.txt");
		this.LoadThemes_JA("JA/Themes_JA.txt");
		this.themes_.Init();
		this.themes_.Load_FITGENRE("GE/Themes_GE.txt");
		this.themes_.Load_THEMES_MGSR("GE/Themes_GE.txt");
	}

	// Token: 0x06001EDA RID: 7898 RVA: 0x00141828 File Offset: 0x0013FA28
	public string GetStudioBewertung(int sterne)
	{
		switch (sterne)
		{
		case 0:
			return this.GetText(1467);
		case 1:
			return this.GetText(1468);
		case 2:
			return this.GetText(1469);
		case 3:
			return this.GetText(1470);
		case 4:
			return this.GetText(1471);
		case 5:
			return this.GetText(1472);
		case 6:
			return this.GetText(1473);
		case 7:
			return this.GetText(1474);
		case 8:
			return this.GetText(1475);
		case 9:
			return this.GetText(1476);
		case 10:
			return this.GetText(1477);
		default:
			return this.GetText(1467);
		}
	}

	// Token: 0x06001EDB RID: 7899 RVA: 0x001418FB File Offset: 0x0013FAFB
	public string GetGameTyp(int i)
	{
		switch (i)
		{
		case 0:
			return this.GetText(322);
		case 1:
			return this.GetText(323);
		case 2:
			return this.GetText(324);
		default:
			return "<Missing Text>";
		}
	}

	// Token: 0x06001EDC RID: 7900 RVA: 0x0014193C File Offset: 0x0013FB3C
	public string GetGameSize(int i)
	{
		switch (i)
		{
		case 0:
			return this.GetText(329);
		case 1:
			return this.GetText(330);
		case 2:
			return this.GetText(331);
		case 3:
			return this.GetText(332);
		case 4:
			return this.GetText(333);
		default:
			return "<Missing Text>";
		}
	}

	// Token: 0x06001EDD RID: 7901 RVA: 0x001419A8 File Offset: 0x0013FBA8
	public string GetGameZielgruppe(int i)
	{
		switch (i)
		{
		case 0:
			return this.GetText(337);
		case 1:
			return this.GetText(338);
		case 2:
			return this.GetText(339);
		case 3:
			return this.GetText(340);
		case 4:
			return this.GetText(341);
		default:
			return "<Missing Text>";
		}
	}

	// Token: 0x06001EDE RID: 7902 RVA: 0x00141A14 File Offset: 0x0013FC14
	public string GetText(int i)
	{
		this.FindScripts();
		if (!this.textLoaded)
		{
			this.Awake();
		}
		switch (this.settings_.language)
		{
		case 0:
			if (this.text_EN.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.text_EN[i];
		case 1:
			if (this.text_GE.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.text_GE[i];
		case 2:
			if (this.text_TU.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.text_TU[i];
		case 3:
			if (this.text_CH.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.text_CH[i];
		case 4:
			if (this.text_FR.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.text_FR[i];
		case 5:
			if (this.text_ES.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.text_ES[i];
		case 6:
			if (this.text_KO.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.text_KO[i];
		case 7:
			if (this.text_PB.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.text_PB[i];
		case 8:
			if (this.text_HU.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.text_HU[i];
		case 9:
			if (this.text_RU.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.text_RU[i];
		case 10:
			if (this.text_CT.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.text_CT[i];
		case 11:
			if (this.text_PL.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.text_PL[i];
		case 12:
			if (this.text_CZ.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.text_CZ[i];
		case 13:
			if (this.text_AR.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.text_AR[i];
		case 14:
			if (this.text_IT.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.text_IT[i];
		case 15:
			if (this.text_RO.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.text_RO[i];
		case 16:
			if (this.text_JA.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.text_JA[i];
		default:
			return "<Missing Text>";
		}
	}

	// Token: 0x06001EDF RID: 7903 RVA: 0x00141C50 File Offset: 0x0013FE50
	public string GetAchivementName(int i)
	{
		this.FindScripts();
		if (!this.textLoaded)
		{
			this.Awake();
		}
		switch (this.settings_.language)
		{
		case 0:
			if (this.achivementsName_EN.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.achivementsName_EN[i];
		case 1:
			if (this.achivementsName_GE.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.achivementsName_GE[i];
		case 2:
			if (this.achivementsName_TU.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.achivementsName_TU[i];
		case 3:
			if (this.achivementsName_CH.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.achivementsName_CH[i];
		case 4:
			if (this.achivementsName_FR.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.achivementsName_FR[i];
		case 5:
			if (this.achivementsName_ES.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.achivementsName_ES[i];
		case 6:
			if (this.achivementsName_KO.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.achivementsName_KO[i];
		case 7:
			if (this.achivementsName_PB.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.achivementsName_PB[i];
		case 8:
			if (this.achivementsName_HU.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.achivementsName_HU[i];
		case 9:
			if (this.achivementsName_RU.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.achivementsName_RU[i];
		case 10:
			if (this.achivementsName_CT.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.achivementsName_CT[i];
		case 11:
			if (this.achivementsName_PL.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.achivementsName_PL[i];
		case 12:
			if (this.achivementsName_CZ.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.achivementsName_CZ[i];
		case 13:
			if (this.achivementsName_AR.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.achivementsName_AR[i];
		case 14:
			if (this.achivementsName_IT.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.achivementsName_IT[i];
		case 15:
			if (this.achivementsName_RO.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.achivementsName_RO[i];
		case 16:
			if (this.achivementsName_JA.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.achivementsName_JA[i];
		default:
			return "<Missing Text>";
		}
	}

	// Token: 0x06001EE0 RID: 7904 RVA: 0x00141E8C File Offset: 0x0014008C
	public string GetAchivementDesc(int i)
	{
		this.FindScripts();
		if (!this.textLoaded)
		{
			this.Awake();
		}
		switch (this.settings_.language)
		{
		case 0:
			if (this.achivementsDesc_EN.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.achivementsDesc_EN[i];
		case 1:
			if (this.achivementsDesc_GE.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.achivementsDesc_GE[i];
		case 2:
			if (this.achivementsDesc_TU.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.achivementsDesc_TU[i];
		case 3:
			if (this.achivementsDesc_CH.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.achivementsDesc_CH[i];
		case 4:
			if (this.achivementsDesc_FR.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.achivementsDesc_FR[i];
		case 5:
			if (this.achivementsDesc_ES.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.achivementsDesc_ES[i];
		case 6:
			if (this.achivementsDesc_KO.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.achivementsDesc_KO[i];
		case 7:
			if (this.achivementsDesc_PB.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.achivementsDesc_PB[i];
		case 8:
			if (this.achivementsDesc_HU.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.achivementsDesc_HU[i];
		case 9:
			if (this.achivementsDesc_RU.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.achivementsDesc_RU[i];
		case 10:
			if (this.achivementsDesc_CT.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.achivementsDesc_CT[i];
		case 11:
			if (this.achivementsDesc_PL.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.achivementsDesc_PL[i];
		case 12:
			if (this.achivementsDesc_CZ.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.achivementsDesc_CZ[i];
		case 13:
			if (this.achivementsDesc_AR.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.achivementsDesc_AR[i];
		case 14:
			if (this.achivementsDesc_IT.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.achivementsDesc_IT[i];
		case 15:
			if (this.achivementsDesc_RO.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.achivementsDesc_RO[i];
		case 16:
			if (this.achivementsDesc_JA.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.achivementsDesc_JA[i];
		default:
			return "<Missing Text>";
		}
	}

	// Token: 0x06001EE1 RID: 7905 RVA: 0x001420C8 File Offset: 0x001402C8
	public string GetObjects(int i)
	{
		switch (this.settings_.language)
		{
		case 0:
			if (this.objects_EN.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.objects_EN[i];
		case 1:
			if (this.objects_GE.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.objects_GE[i];
		case 2:
			if (this.objects_TU.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.objects_TU[i];
		case 3:
			if (this.objects_CH.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.objects_CH[i];
		case 4:
			if (this.objects_FR.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.objects_FR[i];
		case 5:
			if (this.objects_ES.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.objects_ES[i];
		case 6:
			if (this.objects_KO.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.objects_KO[i];
		case 7:
			if (this.objects_PB.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.objects_PB[i];
		case 8:
			if (this.objects_HU.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.objects_HU[i];
		case 9:
			if (this.objects_RU.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.objects_RU[i];
		case 10:
			if (this.objects_CT.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.objects_CT[i];
		case 11:
			if (this.objects_PL.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.objects_PL[i];
		case 12:
			if (this.objects_CZ.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.objects_CZ[i];
		case 13:
			if (this.objects_AR.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.objects_AR[i];
		case 14:
			if (this.objects_IT.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.objects_IT[i];
		case 15:
			if (this.objects_RO.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.objects_RO[i];
		case 16:
			if (this.objects_JA.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.objects_JA[i];
		default:
			return "<Missing Text>";
		}
	}

	// Token: 0x06001EE2 RID: 7906 RVA: 0x001422F0 File Offset: 0x001404F0
	public string GetObjectsTooltip(int i)
	{
		switch (this.settings_.language)
		{
		case 0:
			if (this.objectsTooltip_EN.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.objectsTooltip_EN[i];
		case 1:
			if (this.objectsTooltip_GE.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.objectsTooltip_GE[i];
		case 2:
			if (this.objectsTooltip_TU.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.objectsTooltip_TU[i];
		case 3:
			if (this.objectsTooltip_CH.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.objectsTooltip_CH[i];
		case 4:
			if (this.objectsTooltip_FR.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.objectsTooltip_FR[i];
		case 5:
			if (this.objectsTooltip_ES.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.objectsTooltip_ES[i];
		case 6:
			if (this.objectsTooltip_KO.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.objectsTooltip_KO[i];
		case 7:
			if (this.objectsTooltip_PB.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.objectsTooltip_PB[i];
		case 8:
			if (this.objectsTooltip_HU.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.objectsTooltip_HU[i];
		case 9:
			if (this.objectsTooltip_RU.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.objectsTooltip_RU[i];
		case 10:
			if (this.objectsTooltip_CT.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.objectsTooltip_CT[i];
		case 11:
			if (this.objectsTooltip_PL.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.objectsTooltip_PL[i];
		case 12:
			if (this.objectsTooltip_CZ.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.objectsTooltip_CZ[i];
		case 13:
			if (this.objectsTooltip_AR.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.objectsTooltip_AR[i];
		case 14:
			if (this.objectsTooltip_IT.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.objectsTooltip_IT[i];
		case 15:
			if (this.objectsTooltip_RO.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.objectsTooltip_RO[i];
		case 16:
			if (this.objectsTooltip_JA.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.objectsTooltip_JA[i];
		default:
			return "<Missing Text>";
		}
	}

	// Token: 0x06001EE3 RID: 7907 RVA: 0x00142518 File Offset: 0x00140718
	public string GetCountry(int i)
	{
		string result = "";
		switch (this.settings_.language)
		{
		case 0:
			if (this.country_EN.Length <= i)
			{
				return "<Missing Text>";
			}
			result = this.country_EN[i];
			break;
		case 1:
			if (this.country_GE.Length <= i)
			{
				return "<Missing Text>";
			}
			result = this.country_GE[i];
			break;
		case 2:
			if (this.country_TU.Length <= i)
			{
				return "<Missing Text>";
			}
			result = this.country_TU[i];
			break;
		case 3:
			if (this.country_CH.Length <= i)
			{
				return "<Missing Text>";
			}
			result = this.country_CH[i];
			break;
		case 4:
			if (this.country_FR.Length <= i)
			{
				return "<Missing Text>";
			}
			result = this.country_FR[i];
			break;
		case 5:
			if (this.country_ES.Length <= i)
			{
				return "<Missing Text>";
			}
			result = this.country_ES[i];
			break;
		case 6:
			if (this.country_KO.Length <= i)
			{
				return "<Missing Text>";
			}
			result = this.country_KO[i];
			break;
		case 7:
			if (this.country_PB.Length <= i)
			{
				return "<Missing Text>";
			}
			result = this.country_PB[i];
			break;
		case 8:
			if (this.country_HU.Length <= i)
			{
				return "<Missing Text>";
			}
			result = this.country_HU[i];
			break;
		case 9:
			if (this.country_RU.Length <= i)
			{
				return "<Missing Text>";
			}
			result = this.country_RU[i];
			break;
		case 10:
			if (this.country_CT.Length <= i)
			{
				return "<Missing Text>";
			}
			result = this.country_CT[i];
			break;
		case 11:
			if (this.country_PL.Length <= i)
			{
				return "<Missing Text>";
			}
			result = this.country_PL[i];
			break;
		case 12:
			if (this.country_CZ.Length <= i)
			{
				return "<Missing Text>";
			}
			result = this.country_CZ[i];
			break;
		case 13:
			if (this.country_AR.Length <= i)
			{
				return "<Missing Text>";
			}
			result = this.country_AR[i];
			break;
		case 14:
			if (this.country_IT.Length <= i)
			{
				return "<Missing Text>";
			}
			result = this.country_IT[i];
			break;
		case 15:
			if (this.country_RO.Length <= i)
			{
				return "<Missing Text>";
			}
			result = this.country_RO[i];
			break;
		case 16:
			if (this.country_JA.Length <= i)
			{
				return "<Missing Text>";
			}
			result = this.country_JA[i];
			break;
		}
		return result;
	}

	// Token: 0x06001EE4 RID: 7908 RVA: 0x00142788 File Offset: 0x00140988
	public string GetQuotes()
	{
		switch (this.settings_.language)
		{
		case 0:
			return this.quotes_EN[UnityEngine.Random.Range(0, this.quotes_EN.Length)];
		case 1:
			return this.quotes_GE[UnityEngine.Random.Range(0, this.quotes_GE.Length)];
		case 2:
			return this.quotes_TU[UnityEngine.Random.Range(0, this.quotes_TU.Length)];
		case 3:
			return this.quotes_CH[UnityEngine.Random.Range(0, this.quotes_CH.Length)];
		case 4:
			return this.quotes_FR[UnityEngine.Random.Range(0, this.quotes_FR.Length)];
		case 5:
			return this.quotes_ES[UnityEngine.Random.Range(0, this.quotes_ES.Length)];
		case 6:
			return this.quotes_KO[UnityEngine.Random.Range(0, this.quotes_KO.Length)];
		case 7:
			return this.quotes_PB[UnityEngine.Random.Range(0, this.quotes_PB.Length)];
		case 8:
			return this.quotes_HU[UnityEngine.Random.Range(0, this.quotes_HU.Length)];
		case 9:
			return this.quotes_RU[UnityEngine.Random.Range(0, this.quotes_RU.Length)];
		case 10:
			return this.quotes_CT[UnityEngine.Random.Range(0, this.quotes_CT.Length)];
		case 11:
			return this.quotes_PL[UnityEngine.Random.Range(0, this.quotes_PL.Length)];
		case 12:
			return this.quotes_CZ[UnityEngine.Random.Range(0, this.quotes_CZ.Length)];
		case 13:
			return this.quotes_AR[UnityEngine.Random.Range(0, this.quotes_AR.Length)];
		case 14:
			return this.quotes_IT[UnityEngine.Random.Range(0, this.quotes_IT.Length)];
		case 15:
			return this.quotes_RO[UnityEngine.Random.Range(0, this.quotes_RO.Length)];
		case 16:
			return this.quotes_JA[UnityEngine.Random.Range(0, this.quotes_JA.Length)];
		default:
			return "<Missing Text>";
		}
	}

	// Token: 0x06001EE5 RID: 7909 RVA: 0x0014296C File Offset: 0x00140B6C
	public string GetFanLetter(int i)
	{
		switch (this.settings_.language)
		{
		case 0:
			if (this.fanLetter_EN.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.fanLetter_EN[i];
		case 1:
			if (this.fanLetter_GE.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.fanLetter_GE[i];
		case 2:
			if (this.fanLetter_TU.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.fanLetter_TU[i];
		case 3:
			if (this.fanLetter_CH.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.fanLetter_CH[i];
		case 4:
			if (this.fanLetter_FR.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.fanLetter_FR[i];
		case 5:
			if (this.fanLetter_ES.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.fanLetter_ES[i];
		case 6:
			if (this.fanLetter_KO.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.fanLetter_KO[i];
		case 7:
			if (this.fanLetter_PB.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.fanLetter_PB[i];
		case 8:
			if (this.fanLetter_HU.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.fanLetter_HU[i];
		case 9:
			if (this.fanLetter_RU.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.fanLetter_RU[i];
		case 10:
			if (this.fanLetter_CT.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.fanLetter_CT[i];
		case 11:
			if (this.fanLetter_PL.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.fanLetter_PL[i];
		case 12:
			if (this.fanLetter_CZ.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.fanLetter_CZ[i];
		case 13:
			if (this.fanLetter_AR.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.fanLetter_AR[i];
		case 14:
			if (this.fanLetter_IT.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.fanLetter_IT[i];
		case 15:
			if (this.fanLetter_RO.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.fanLetter_RO[i];
		case 16:
			if (this.fanLetter_JA.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.fanLetter_JA[i];
		default:
			return "<Missing Text>";
		}
	}

	// Token: 0x06001EE6 RID: 7910 RVA: 0x00142B94 File Offset: 0x00140D94
	public string GetTutorial(int i)
	{
		switch (this.settings_.language)
		{
		case 0:
			if (this.tutorial_EN.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.tutorial_EN[i];
		case 1:
			if (this.tutorial_GE.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.tutorial_GE[i];
		case 2:
			if (this.tutorial_TU.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.tutorial_TU[i];
		case 3:
			if (this.tutorial_CH.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.tutorial_CH[i];
		case 4:
			if (this.tutorial_FR.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.tutorial_FR[i];
		case 5:
			if (this.tutorial_ES.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.tutorial_ES[i];
		case 6:
			if (this.tutorial_KO.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.tutorial_KO[i];
		case 7:
			if (this.tutorial_PB.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.tutorial_PB[i];
		case 8:
			if (this.tutorial_HU.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.tutorial_HU[i];
		case 9:
			if (this.tutorial_RU.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.tutorial_RU[i];
		case 10:
			if (this.tutorial_CT.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.tutorial_CT[i];
		case 11:
			if (this.tutorial_PL.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.tutorial_PL[i];
		case 12:
			if (this.tutorial_CZ.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.tutorial_CZ[i];
		case 13:
			if (this.tutorial_AR.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.tutorial_AR[i];
		case 14:
			if (this.tutorial_IT.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.tutorial_IT[i];
		case 15:
			if (this.tutorial_RO.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.tutorial_RO[i];
		case 16:
			if (this.tutorial_JA.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.tutorial_JA[i];
		default:
			return "<Missing Text>";
		}
	}

	// Token: 0x06001EE7 RID: 7911 RVA: 0x00142DBC File Offset: 0x00140FBC
	public string GetThemes(int i)
	{
		if (this.themes_EN.Length == 0)
		{
			return "<Not initalized>";
		}
		switch (this.settings_.language)
		{
		case 0:
			if (this.themes_EN.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.themes_EN[i];
		case 1:
			if (this.themes_GE.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.themes_GE[i];
		case 2:
			if (this.themes_TU.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.themes_TU[i];
		case 3:
			if (this.themes_CH.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.themes_CH[i];
		case 4:
			if (this.themes_FR.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.themes_FR[i];
		case 5:
			if (this.themes_ES.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.themes_ES[i];
		case 6:
			if (this.themes_KO.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.themes_KO[i];
		case 7:
			if (this.themes_PB.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.themes_PB[i];
		case 8:
			if (this.themes_HU.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.themes_HU[i];
		case 9:
			if (this.themes_RU.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.themes_RU[i];
		case 10:
			if (this.themes_CT.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.themes_CT[i];
		case 11:
			if (this.themes_PL.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.themes_PL[i];
		case 12:
			if (this.themes_CZ.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.themes_CZ[i];
		case 13:
			if (this.themes_AR.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.themes_AR[i];
		case 14:
			if (this.themes_IT.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.themes_IT[i];
		case 15:
			if (this.themes_RO.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.themes_RO[i];
		case 16:
			if (this.themes_JA.Length <= i)
			{
				return "<Missing Text>";
			}
			return this.themes_JA[i];
		default:
			return "<Missing Text>";
		}
	}

	// Token: 0x06001EE8 RID: 7912 RVA: 0x00142FF4 File Offset: 0x001411F4
	public string GetRandomCharName(bool male)
	{
		if (male)
		{
			return (this.namesMale[UnityEngine.Random.Range(0, this.namesMale.Length)] + " " + this.surname[UnityEngine.Random.Range(0, this.surname.Length)]).Replace("\n", string.Empty).Replace("\r", string.Empty).Replace("\t", string.Empty);
		}
		return (this.namesFemale[UnityEngine.Random.Range(0, this.namesFemale.Length)] + " " + this.surname[UnityEngine.Random.Range(0, this.surname.Length)]).Replace("\n", string.Empty).Replace("\r", string.Empty).Replace("\t", string.Empty);
	}

	// Token: 0x06001EE9 RID: 7913 RVA: 0x001430C7 File Offset: 0x001412C7
	public string GetRandomNPCAddonName()
	{
		return this.npcAddons[UnityEngine.Random.Range(0, this.npcAddons.Length)];
	}

	// Token: 0x06001EEA RID: 7914 RVA: 0x001430DE File Offset: 0x001412DE
	public string GetRandomEngineName()
	{
		return this.randomEngineNames[UnityEngine.Random.Range(0, this.randomEngineNames.Length)];
	}

	// Token: 0x06001EEB RID: 7915 RVA: 0x001430F8 File Offset: 0x001412F8
	public string GetRandomGameName()
	{
		return this.randomGameNames[UnityEngine.Random.Range(0, this.randomGameNames.Length)].Replace("\n", string.Empty).Replace("\r", string.Empty).Replace("\t", string.Empty);
	}

	// Token: 0x06001EEC RID: 7916 RVA: 0x00143148 File Offset: 0x00141348
	public string GetPlatformName()
	{
		return this.randomPlatformNames[UnityEngine.Random.Range(0, this.randomPlatformNames.Length)].Replace("\n", string.Empty).Replace("\r", string.Empty).Replace("\t", string.Empty);
	}

	// Token: 0x06001EED RID: 7917 RVA: 0x00143198 File Offset: 0x00141398
	public string GetRandomCompanyName()
	{
		return this.randomCompanyNames[UnityEngine.Random.Range(0, this.randomCompanyNames.Length)].Replace("\n", string.Empty).Replace("\r", string.Empty).Replace("\t", string.Empty);
	}

	// Token: 0x06001EEE RID: 7918 RVA: 0x001431E8 File Offset: 0x001413E8
	public string GetContractWork(int nr)
	{
		string text = "";
		switch (this.settings_.language)
		{
		case 0:
			if (this.contractWork_EN.Length < nr)
			{
				return "<Missing Text>";
			}
			text = this.contractWork_EN[nr];
			break;
		case 1:
			if (this.contractWork_GE.Length < nr)
			{
				return "<Missing Text>";
			}
			text = this.contractWork_GE[nr];
			break;
		case 2:
			if (this.contractWork_TU.Length < nr)
			{
				return "<Missing Text>";
			}
			text = this.contractWork_TU[nr];
			break;
		case 3:
			if (this.contractWork_CH.Length < nr)
			{
				return "<Missing Text>";
			}
			text = this.contractWork_CH[nr];
			break;
		case 4:
			if (this.contractWork_FR.Length < nr)
			{
				return "<Missing Text>";
			}
			text = this.contractWork_FR[nr];
			break;
		case 5:
			if (this.contractWork_ES.Length < nr)
			{
				return "<Missing Text>";
			}
			text = this.contractWork_ES[nr];
			break;
		case 6:
			if (this.contractWork_KO.Length < nr)
			{
				return "<Missing Text>";
			}
			text = this.contractWork_KO[nr];
			break;
		case 7:
			if (this.contractWork_PB.Length < nr)
			{
				return "<Missing Text>";
			}
			text = this.contractWork_PB[nr];
			break;
		case 8:
			if (this.contractWork_HU.Length < nr)
			{
				return "<Missing Text>";
			}
			text = this.contractWork_HU[nr];
			break;
		case 9:
			if (this.contractWork_RU.Length < nr)
			{
				return "<Missing Text>";
			}
			text = this.contractWork_RU[nr];
			break;
		case 10:
			if (this.contractWork_CT.Length < nr)
			{
				return "<Missing Text>";
			}
			text = this.contractWork_CT[nr];
			break;
		case 11:
			if (this.contractWork_PL.Length < nr)
			{
				return "<Missing Text>";
			}
			text = this.contractWork_PL[nr];
			break;
		case 12:
			if (this.contractWork_CZ.Length < nr)
			{
				return "<Missing Text>";
			}
			text = this.contractWork_CZ[nr];
			break;
		case 13:
			if (this.contractWork_AR.Length < nr)
			{
				return "<Missing Text>";
			}
			text = this.contractWork_AR[nr];
			break;
		case 14:
			if (this.contractWork_IT.Length < nr)
			{
				return "<Missing Text>";
			}
			text = this.contractWork_IT[nr];
			break;
		case 15:
			if (this.contractWork_RO.Length < nr)
			{
				return "<Missing Text>";
			}
			text = this.contractWork_RO[nr];
			break;
		case 16:
			if (this.contractWork_JA.Length < nr)
			{
				return "<Missing Text>";
			}
			text = this.contractWork_JA[nr];
			break;
		}
		for (int i = 0; i < 10; i++)
		{
			text = text.Replace("<" + i.ToString() + ">", "");
		}
		return text;
	}

	// Token: 0x06001EEF RID: 7919 RVA: 0x00143484 File Offset: 0x00141684
	public int GetRandomContractNumber(int art_)
	{
		int num = 0;
		int i = 0;
		string text = "";
		while (i < 10000)
		{
			switch (this.settings_.language)
			{
			case 0:
				num = UnityEngine.Random.Range(0, this.contractWork_EN.Length);
				break;
			case 1:
				num = UnityEngine.Random.Range(0, this.contractWork_GE.Length);
				break;
			case 2:
				num = UnityEngine.Random.Range(0, this.contractWork_TU.Length);
				break;
			case 3:
				num = UnityEngine.Random.Range(0, this.contractWork_CH.Length);
				break;
			case 4:
				num = UnityEngine.Random.Range(0, this.contractWork_FR.Length);
				break;
			case 5:
				num = UnityEngine.Random.Range(0, this.contractWork_ES.Length);
				break;
			case 6:
				num = UnityEngine.Random.Range(0, this.contractWork_KO.Length);
				break;
			case 7:
				num = UnityEngine.Random.Range(0, this.contractWork_PB.Length);
				break;
			case 8:
				num = UnityEngine.Random.Range(0, this.contractWork_HU.Length);
				break;
			case 9:
				num = UnityEngine.Random.Range(0, this.contractWork_RU.Length);
				break;
			case 10:
				num = UnityEngine.Random.Range(0, this.contractWork_CT.Length);
				break;
			case 11:
				num = UnityEngine.Random.Range(0, this.contractWork_PL.Length);
				break;
			case 12:
				num = UnityEngine.Random.Range(0, this.contractWork_CZ.Length);
				break;
			case 13:
				num = UnityEngine.Random.Range(0, this.contractWork_AR.Length);
				break;
			case 14:
				num = UnityEngine.Random.Range(0, this.contractWork_IT.Length);
				break;
			case 15:
				num = UnityEngine.Random.Range(0, this.contractWork_RO.Length);
				break;
			case 16:
				num = UnityEngine.Random.Range(0, this.contractWork_JA.Length);
				break;
			}
			switch (this.settings_.language)
			{
			case 0:
				text = this.contractWork_EN[num];
				break;
			case 1:
				text = this.contractWork_GE[num];
				break;
			case 2:
				text = this.contractWork_TU[num];
				break;
			case 3:
				text = this.contractWork_CH[num];
				break;
			case 4:
				text = this.contractWork_FR[num];
				break;
			case 5:
				text = this.contractWork_ES[num];
				break;
			case 6:
				text = this.contractWork_KO[num];
				break;
			case 7:
				text = this.contractWork_PB[num];
				break;
			case 8:
				text = this.contractWork_HU[num];
				break;
			case 9:
				text = this.contractWork_RU[num];
				break;
			case 10:
				text = this.contractWork_CT[num];
				break;
			case 11:
				text = this.contractWork_PL[num];
				break;
			case 12:
				text = this.contractWork_CZ[num];
				break;
			case 13:
				text = this.contractWork_AR[num];
				break;
			case 14:
				text = this.contractWork_IT[num];
				break;
			case 15:
				text = this.contractWork_RO[num];
				break;
			case 16:
				text = this.contractWork_JA[num];
				break;
			}
			if (text.Contains("<" + art_.ToString() + ">"))
			{
				break;
			}
			i++;
		}
		return num;
	}

	// Token: 0x06001EF0 RID: 7920 RVA: 0x00143788 File Offset: 0x00141988
	public string GetRandomNpcGame(int genre_)
	{
		string text = "";
		if (this.npcGameNameInUse.Length == 0)
		{
			this.npcGameNameInUse = new bool[this.npcGames.Length];
		}
		for (int i = 0; i < this.npcGames.Length; i++)
		{
			if (!this.npcGameNameInUse[i] && this.npcGames[i].Contains("<" + genre_.ToString() + ">"))
			{
				text = this.npcGames[i];
				this.npcGameNameInUse[i] = true;
				break;
			}
		}
		if (text.Length <= 0)
		{
			return "";
		}
		if (text == null)
		{
			return "";
		}
		for (int j = 0; j < this.genres_.genres_LEVEL.Length; j++)
		{
			text = text.Replace("<" + j.ToString() + ">", "");
		}
		text = text.Replace("\n", string.Empty);
		text = text.Replace("\r", string.Empty);
		text = text.Replace("\t", string.Empty);
		return text.Substring(0, text.Length - 1);
	}

	// Token: 0x06001EF1 RID: 7921 RVA: 0x001438A4 File Offset: 0x00141AA4
	private int GetGenreFromSonderIP(int i)
	{
		for (int j = 0; j < this.genres_.genres_LEVEL.Length; j++)
		{
			if (this.npcIPs[i].Contains("<G" + j.ToString() + ">"))
			{
				return j;
			}
		}
		return 0;
	}

	// Token: 0x06001EF2 RID: 7922 RVA: 0x001438F4 File Offset: 0x00141AF4
	private int GetSubGenreFromSonderIP(int i)
	{
		for (int j = 0; j < this.genres_.genres_LEVEL.Length; j++)
		{
			if (this.npcIPs[i].Contains("<SG" + j.ToString() + ">"))
			{
				return j;
			}
		}
		return -1;
	}

	// Token: 0x06001EF3 RID: 7923 RVA: 0x00143944 File Offset: 0x00141B44
	private int GetTopicFromSonderIP(int i)
	{
		for (int j = 0; j < this.themes_.themes_LEVEL.Length; j++)
		{
			if (this.npcIPs[i].Contains("<T" + j.ToString() + ">"))
			{
				return j;
			}
		}
		return 0;
	}

	// Token: 0x06001EF4 RID: 7924 RVA: 0x00143994 File Offset: 0x00141B94
	private int GetSubTopicFromSonderIP(int i)
	{
		for (int j = 0; j < this.themes_.themes_LEVEL.Length; j++)
		{
			if (this.npcIPs[i].Contains("<ST" + j.ToString() + ">"))
			{
				return j;
			}
		}
		return -1;
	}

	// Token: 0x06001EF5 RID: 7925 RVA: 0x001439E4 File Offset: 0x00141BE4
	private int GetReviewFromSonderIP(int i)
	{
		for (int j = 100; j > 0; j--)
		{
			if (this.npcIPs[i].Contains("<%" + j.ToString() + ">"))
			{
				return j;
			}
		}
		return 0;
	}

	// Token: 0x06001EF6 RID: 7926 RVA: 0x00143A28 File Offset: 0x00141C28
	private int GetYearFromSonderIP(int i)
	{
		for (int j = 1976; j < 2099; j++)
		{
			if (this.npcIPs[i].Contains("<Y" + j.ToString() + ">"))
			{
				return j;
			}
		}
		return 0;
	}

	// Token: 0x06001EF7 RID: 7927 RVA: 0x00143A74 File Offset: 0x00141C74
	private int GetTargetGroupFromSonderIP(int i)
	{
		for (int j = 0; j < 5; j++)
		{
			if (this.npcIPs[i].Contains("<TG" + j.ToString() + ">"))
			{
				return j;
			}
		}
		return 0;
	}

	// Token: 0x06001EF8 RID: 7928 RVA: 0x00143AB8 File Offset: 0x00141CB8
	public string GetRandomNpcIP(int publisher_, gameScript game_)
	{
		string text = "";
		int num = 0;
		int num2 = -1;
		int gameMainTheme = 0;
		int gameSubTheme = -1;
		int sonderIPMindestreview = 0;
		int num3 = 0;
		int gameZielgruppe = 0;
		if (this.npcIPsInUse.Length == 0)
		{
			this.npcIPsInUse = new bool[this.npcIPs.Length];
		}
		for (int i = 0; i < this.npcIPs.Length; i++)
		{
			if (!this.npcIPsInUse[i] && this.npcIPs[i].Contains("<P" + publisher_.ToString() + ">"))
			{
				num = this.GetGenreFromSonderIP(i);
				num2 = this.GetSubGenreFromSonderIP(i);
				bool flag = false;
				if (num2 == -1)
				{
					flag = true;
				}
				else if (this.genres_.genres_UNLOCK[num2])
				{
					flag = true;
				}
				if (this.genres_.genres_UNLOCK[num] && flag)
				{
					num3 = this.GetYearFromSonderIP(i);
					if (this.mS_.year >= num3)
					{
						text = this.npcIPs[i];
						this.npcIPsInUse[i] = true;
						gameMainTheme = this.GetTopicFromSonderIP(i);
						gameSubTheme = this.GetSubTopicFromSonderIP(i);
						sonderIPMindestreview = this.GetReviewFromSonderIP(i);
						gameZielgruppe = this.GetTargetGroupFromSonderIP(i);
						break;
					}
				}
			}
		}
		if (text.Length <= 0)
		{
			return "";
		}
		if (text == null)
		{
			return "";
		}
		text = text.Replace("<G" + num.ToString() + ">", "");
		text = text.Replace("<SG" + num2.ToString() + ">", "");
		text = text.Replace("<P" + publisher_.ToString() + ">", "");
		text = text.Replace("<T" + gameMainTheme.ToString() + ">", "");
		text = text.Replace("<ST" + gameSubTheme.ToString() + ">", "");
		text = text.Replace("<%" + sonderIPMindestreview.ToString() + ">", "");
		text = text.Replace("<Y" + num3.ToString() + ">", "");
		text = text.Replace("<TG" + gameZielgruppe.ToString() + ">", "");
		text = text.Replace("\n", string.Empty);
		text = text.Replace("\r", string.Empty);
		text = text.Replace("\t", string.Empty);
		text = text.Substring(0, text.Length - 1);
		game_.SetMyName(text);
		game_.maingenre = num;
		game_.subgenre = num2;
		game_.gameMainTheme = gameMainTheme;
		game_.gameSubTheme = gameSubTheme;
		game_.sonderIPMindestreview = sonderIPMindestreview;
		game_.gameZielgruppe = gameZielgruppe;
		return text;
	}

	// Token: 0x06001EF9 RID: 7929 RVA: 0x00143D88 File Offset: 0x00141F88
	private string OpenFile(string filename)
	{
		StreamReader streamReader = new StreamReader(Application.dataPath + "/Extern/Text/" + filename, Encoding.Unicode);
		string result = streamReader.ReadToEnd();
		streamReader.Close();
		return result;
	}

	// Token: 0x06001EFA RID: 7930 RVA: 0x00143DBC File Offset: 0x00141FBC
	private void LoadGlobalText()
	{
		this.namesMale = this.OpenFile("DATA/NamesMale.txt").Split(new char[]
		{
			"\n"[0]
		});
		this.namesFemale = this.OpenFile("DATA/NamesFemale.txt").Split(new char[]
		{
			"\n"[0]
		});
		this.surname = this.OpenFile("DATA/Surname.txt").Split(new char[]
		{
			"\n"[0]
		});
		this.randomEngineNames = this.OpenFile("DATA/RandomEngineNames.txt").Split(new char[]
		{
			"\n"[0]
		});
		this.randomGameNames = this.OpenFile("DATA/RandomGameNames.txt").Split(new char[]
		{
			"\n"[0]
		});
		this.randomPlatformNames = this.OpenFile("DATA/RandomPlatformNames.txt").Split(new char[]
		{
			"\n"[0]
		});
		this.randomCompanyNames = this.OpenFile("DATA/RandomCompanyNames.txt").Split(new char[]
		{
			"\n"[0]
		});
		this.npcAddons = this.OpenFile("DATA/npcAddons.txt").Split(new char[]
		{
			"\n"[0]
		});
		this.credits = this.OpenFile("DATA/Credits.txt");
	}

	// Token: 0x06001EFB RID: 7931 RVA: 0x00143F2C File Offset: 0x0014212C
	public void LoadContent_NPCGameNames()
	{
		this.npcGames = this.OpenFile("DATA/npcGames.txt").Split(new char[]
		{
			"\n"[0]
		});
		this.npcGameNameInUse = new bool[this.npcGames.Length];
		this.Reshuffle(this.npcGames);
	}

	// Token: 0x06001EFC RID: 7932 RVA: 0x00143F82 File Offset: 0x00142182
	public void LoadContent_NpcIPs()
	{
		this.npcIPs = this.OpenFile("DATA/npcIPs.txt").Split(new char[]
		{
			"\n"[0]
		});
		this.npcIPsInUse = new bool[this.npcIPs.Length];
	}

	// Token: 0x06001EFD RID: 7933 RVA: 0x00143FC4 File Offset: 0x001421C4
	private void Reshuffle(string[] texts)
	{
		for (int i = 0; i < texts.Length; i++)
		{
			string text = texts[i];
			int num = UnityEngine.Random.Range(i, texts.Length);
			texts[i] = texts[num];
			texts[num] = text;
		}
	}

	// Token: 0x06001EFE RID: 7934 RVA: 0x00143FF8 File Offset: 0x001421F8
	private void LoadTexts_GE(string filename)
	{
		int num = 0;
		this.text_GE = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.text_GE.Length; i++)
		{
			this.text_GE[i] = this.text_GE[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001EFF RID: 7935 RVA: 0x00144064 File Offset: 0x00142264
	private void LoadTexts_EN(string filename)
	{
		int num = 0;
		this.text_EN = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.text_EN.Length; i++)
		{
			this.text_EN[i] = this.text_EN[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F00 RID: 7936 RVA: 0x001440D0 File Offset: 0x001422D0
	private void LoadTexts_TU(string filename)
	{
		int num = 0;
		this.text_TU = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.text_TU.Length; i++)
		{
			this.text_TU[i] = this.text_TU[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F01 RID: 7937 RVA: 0x0014413C File Offset: 0x0014233C
	private void LoadTexts_CH(string filename)
	{
		int num = 0;
		this.text_CH = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.text_CH.Length; i++)
		{
			this.text_CH[i] = this.text_CH[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F02 RID: 7938 RVA: 0x001441A8 File Offset: 0x001423A8
	private void LoadTexts_FR(string filename)
	{
		int num = 0;
		this.text_FR = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.text_FR.Length; i++)
		{
			this.text_FR[i] = this.text_FR[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F03 RID: 7939 RVA: 0x00144214 File Offset: 0x00142414
	private void LoadTexts_ES(string filename)
	{
		int num = 0;
		this.text_ES = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.text_ES.Length; i++)
		{
			this.text_ES[i] = this.text_ES[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F04 RID: 7940 RVA: 0x00144280 File Offset: 0x00142480
	private void LoadTexts_KO(string filename)
	{
		int num = 0;
		this.text_KO = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.text_KO.Length; i++)
		{
			this.text_KO[i] = this.text_KO[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F05 RID: 7941 RVA: 0x001442EC File Offset: 0x001424EC
	private void LoadTexts_PB(string filename)
	{
		int num = 0;
		this.text_PB = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.text_PB.Length; i++)
		{
			this.text_PB[i] = this.text_PB[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F06 RID: 7942 RVA: 0x00144358 File Offset: 0x00142558
	private void LoadTexts_HU(string filename)
	{
		int num = 0;
		this.text_HU = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.text_HU.Length; i++)
		{
			this.text_HU[i] = this.text_HU[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F07 RID: 7943 RVA: 0x001443C4 File Offset: 0x001425C4
	private void LoadTexts_RU(string filename)
	{
		int num = 0;
		this.text_RU = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.text_RU.Length; i++)
		{
			this.text_RU[i] = this.text_RU[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F08 RID: 7944 RVA: 0x00144430 File Offset: 0x00142630
	private void LoadTexts_CT(string filename)
	{
		int num = 0;
		this.text_CT = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.text_CT.Length; i++)
		{
			this.text_CT[i] = this.text_CT[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F09 RID: 7945 RVA: 0x0014449C File Offset: 0x0014269C
	private void LoadTexts_PL(string filename)
	{
		int num = 0;
		this.text_PL = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.text_PL.Length; i++)
		{
			this.text_PL[i] = this.text_PL[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F0A RID: 7946 RVA: 0x00144508 File Offset: 0x00142708
	private void LoadTexts_CZ(string filename)
	{
		int num = 0;
		this.text_CZ = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.text_CZ.Length; i++)
		{
			this.text_CZ[i] = this.text_CZ[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F0B RID: 7947 RVA: 0x00144574 File Offset: 0x00142774
	private void LoadTexts_AR(string filename)
	{
		int num = 0;
		this.text_AR = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.text_AR.Length; i++)
		{
			this.text_AR[i] = this.text_AR[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F0C RID: 7948 RVA: 0x001445E0 File Offset: 0x001427E0
	private void LoadTexts_IT(string filename)
	{
		int num = 0;
		this.text_IT = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.text_IT.Length; i++)
		{
			this.text_IT[i] = this.text_IT[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F0D RID: 7949 RVA: 0x0014464C File Offset: 0x0014284C
	private void LoadTexts_RO(string filename)
	{
		int num = 0;
		this.text_RO = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.text_RO.Length; i++)
		{
			this.text_RO[i] = this.text_RO[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F0E RID: 7950 RVA: 0x001446B8 File Offset: 0x001428B8
	private void LoadTexts_JA(string filename)
	{
		int num = 0;
		this.text_JA = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.text_JA.Length; i++)
		{
			this.text_JA[i] = this.text_JA[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F0F RID: 7951 RVA: 0x00144724 File Offset: 0x00142924
	private void LoadAchivements_GE(string filename)
	{
		int num = 0;
		string[] array = this.OpenFile(filename).Split(new char[]
		{
			'\n',
			';'
		});
		this.achivementsName_GE = new string[array.Length / 2];
		this.achivementsDesc_GE = new string[array.Length / 2];
		for (int i = 0; i < array.Length; i++)
		{
			this.achivementsName_GE[num] = array[i].Replace("<br>", "\n");
			i++;
			this.achivementsDesc_GE[num] = array[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F10 RID: 7952 RVA: 0x001447BC File Offset: 0x001429BC
	private void LoadAchivements_EN(string filename)
	{
		int num = 0;
		string[] array = this.OpenFile(filename).Split(new char[]
		{
			'\n',
			';'
		});
		this.achivementsName_EN = new string[array.Length / 2];
		this.achivementsDesc_EN = new string[array.Length / 2];
		for (int i = 0; i < array.Length; i++)
		{
			this.achivementsName_EN[num] = array[i].Replace("<br>", "\n");
			i++;
			this.achivementsDesc_EN[num] = array[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F11 RID: 7953 RVA: 0x00144854 File Offset: 0x00142A54
	private void LoadAchivements_TU(string filename)
	{
		int num = 0;
		string[] array = this.OpenFile(filename).Split(new char[]
		{
			'\n',
			';'
		});
		this.achivementsName_TU = new string[array.Length / 2];
		this.achivementsDesc_TU = new string[array.Length / 2];
		for (int i = 0; i < array.Length; i++)
		{
			this.achivementsName_TU[num] = array[i].Replace("<br>", "\n");
			i++;
			this.achivementsDesc_TU[num] = array[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F12 RID: 7954 RVA: 0x001448EC File Offset: 0x00142AEC
	private void LoadAchivements_CH(string filename)
	{
		int num = 0;
		string[] array = this.OpenFile(filename).Split(new char[]
		{
			'\n',
			';'
		});
		this.achivementsName_CH = new string[array.Length / 2];
		this.achivementsDesc_CH = new string[array.Length / 2];
		for (int i = 0; i < array.Length; i++)
		{
			this.achivementsName_CH[num] = array[i].Replace("<br>", "\n");
			i++;
			this.achivementsDesc_CH[num] = array[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F13 RID: 7955 RVA: 0x00144984 File Offset: 0x00142B84
	private void LoadAchivements_FR(string filename)
	{
		int num = 0;
		string[] array = this.OpenFile(filename).Split(new char[]
		{
			'\n',
			';'
		});
		this.achivementsName_FR = new string[array.Length / 2];
		this.achivementsDesc_FR = new string[array.Length / 2];
		for (int i = 0; i < array.Length; i++)
		{
			this.achivementsName_FR[num] = array[i].Replace("<br>", "\n");
			i++;
			this.achivementsDesc_FR[num] = array[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F14 RID: 7956 RVA: 0x00144A1C File Offset: 0x00142C1C
	private void LoadAchivements_ES(string filename)
	{
		int num = 0;
		string[] array = this.OpenFile(filename).Split(new char[]
		{
			'\n',
			';'
		});
		this.achivementsName_ES = new string[array.Length / 2];
		this.achivementsDesc_ES = new string[array.Length / 2];
		for (int i = 0; i < array.Length; i++)
		{
			this.achivementsName_ES[num] = array[i].Replace("<br>", "\n");
			i++;
			this.achivementsDesc_ES[num] = array[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F15 RID: 7957 RVA: 0x00144AB4 File Offset: 0x00142CB4
	private void LoadAchivements_KO(string filename)
	{
		int num = 0;
		string[] array = this.OpenFile(filename).Split(new char[]
		{
			'\n',
			';'
		});
		this.achivementsName_KO = new string[array.Length / 2];
		this.achivementsDesc_KO = new string[array.Length / 2];
		for (int i = 0; i < array.Length; i++)
		{
			this.achivementsName_KO[num] = array[i].Replace("<br>", "\n");
			i++;
			this.achivementsDesc_KO[num] = array[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F16 RID: 7958 RVA: 0x00144B4C File Offset: 0x00142D4C
	private void LoadAchivements_PB(string filename)
	{
		int num = 0;
		string[] array = this.OpenFile(filename).Split(new char[]
		{
			'\n',
			';'
		});
		this.achivementsName_PB = new string[array.Length / 2];
		this.achivementsDesc_PB = new string[array.Length / 2];
		for (int i = 0; i < array.Length; i++)
		{
			this.achivementsName_PB[num] = array[i].Replace("<br>", "\n");
			i++;
			this.achivementsDesc_PB[num] = array[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F17 RID: 7959 RVA: 0x00144BE4 File Offset: 0x00142DE4
	private void LoadAchivements_HU(string filename)
	{
		int num = 0;
		string[] array = this.OpenFile(filename).Split(new char[]
		{
			'\n',
			';'
		});
		this.achivementsName_HU = new string[array.Length / 2];
		this.achivementsDesc_HU = new string[array.Length / 2];
		for (int i = 0; i < array.Length; i++)
		{
			this.achivementsName_HU[num] = array[i].Replace("<br>", "\n");
			i++;
			this.achivementsDesc_HU[num] = array[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F18 RID: 7960 RVA: 0x00144C7C File Offset: 0x00142E7C
	private void LoadAchivements_RU(string filename)
	{
		int num = 0;
		string[] array = this.OpenFile(filename).Split(new char[]
		{
			'\n',
			';'
		});
		this.achivementsName_RU = new string[array.Length / 2];
		this.achivementsDesc_RU = new string[array.Length / 2];
		for (int i = 0; i < array.Length; i++)
		{
			this.achivementsName_RU[num] = array[i].Replace("<br>", "\n");
			i++;
			this.achivementsDesc_RU[num] = array[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F19 RID: 7961 RVA: 0x00144D14 File Offset: 0x00142F14
	private void LoadAchivements_CT(string filename)
	{
		int num = 0;
		string[] array = this.OpenFile(filename).Split(new char[]
		{
			'\n',
			';'
		});
		this.achivementsName_CT = new string[array.Length / 2];
		this.achivementsDesc_CT = new string[array.Length / 2];
		for (int i = 0; i < array.Length; i++)
		{
			this.achivementsName_CT[num] = array[i].Replace("<br>", "\n");
			i++;
			this.achivementsDesc_CT[num] = array[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F1A RID: 7962 RVA: 0x00144DAC File Offset: 0x00142FAC
	private void LoadAchivements_PL(string filename)
	{
		int num = 0;
		string[] array = this.OpenFile(filename).Split(new char[]
		{
			'\n',
			';'
		});
		this.achivementsName_PL = new string[array.Length / 2];
		this.achivementsDesc_PL = new string[array.Length / 2];
		for (int i = 0; i < array.Length; i++)
		{
			this.achivementsName_PL[num] = array[i].Replace("<br>", "\n");
			i++;
			this.achivementsDesc_PL[num] = array[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F1B RID: 7963 RVA: 0x00144E44 File Offset: 0x00143044
	private void LoadAchivements_CZ(string filename)
	{
		int num = 0;
		string[] array = this.OpenFile(filename).Split(new char[]
		{
			'\n',
			';'
		});
		this.achivementsName_CZ = new string[array.Length / 2];
		this.achivementsDesc_CZ = new string[array.Length / 2];
		for (int i = 0; i < array.Length; i++)
		{
			this.achivementsName_CZ[num] = array[i].Replace("<br>", "\n");
			i++;
			this.achivementsDesc_CZ[num] = array[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F1C RID: 7964 RVA: 0x00144EDC File Offset: 0x001430DC
	private void LoadAchivements_AR(string filename)
	{
		int num = 0;
		string[] array = this.OpenFile(filename).Split(new char[]
		{
			'\n',
			';'
		});
		this.achivementsName_AR = new string[array.Length / 2];
		this.achivementsDesc_AR = new string[array.Length / 2];
		for (int i = 0; i < array.Length; i++)
		{
			this.achivementsName_AR[num] = array[i].Replace("<br>", "\n");
			i++;
			this.achivementsDesc_AR[num] = array[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F1D RID: 7965 RVA: 0x00144F74 File Offset: 0x00143174
	private void LoadAchivements_IT(string filename)
	{
		int num = 0;
		string[] array = this.OpenFile(filename).Split(new char[]
		{
			'\n',
			';'
		});
		this.achivementsName_IT = new string[array.Length / 2];
		this.achivementsDesc_IT = new string[array.Length / 2];
		for (int i = 0; i < array.Length; i++)
		{
			this.achivementsName_IT[num] = array[i].Replace("<br>", "\n");
			i++;
			this.achivementsDesc_IT[num] = array[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F1E RID: 7966 RVA: 0x0014500C File Offset: 0x0014320C
	private void LoadAchivements_RO(string filename)
	{
		int num = 0;
		string[] array = this.OpenFile(filename).Split(new char[]
		{
			'\n',
			';'
		});
		this.achivementsName_RO = new string[array.Length / 2];
		this.achivementsDesc_RO = new string[array.Length / 2];
		for (int i = 0; i < array.Length; i++)
		{
			this.achivementsName_RO[num] = array[i].Replace("<br>", "\n");
			i++;
			this.achivementsDesc_RO[num] = array[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F1F RID: 7967 RVA: 0x001450A4 File Offset: 0x001432A4
	private void LoadAchivements_JA(string filename)
	{
		int num = 0;
		string[] array = this.OpenFile(filename).Split(new char[]
		{
			'\n',
			';'
		});
		this.achivementsName_JA = new string[array.Length / 2];
		this.achivementsDesc_JA = new string[array.Length / 2];
		for (int i = 0; i < array.Length; i++)
		{
			this.achivementsName_JA[num] = array[i].Replace("<br>", "\n");
			i++;
			this.achivementsDesc_JA[num] = array[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F20 RID: 7968 RVA: 0x0014513C File Offset: 0x0014333C
	private void LoadObjects_GE(string filename)
	{
		int num = 0;
		string[] array = this.OpenFile(filename).Split(new char[]
		{
			'\n',
			';'
		});
		this.objects_GE = new string[array.Length / 2];
		this.objectsTooltip_GE = new string[array.Length / 2];
		for (int i = 0; i < array.Length; i++)
		{
			this.objects_GE[num] = array[i].Replace("<br>", "\n");
			i++;
			this.objectsTooltip_GE[num] = array[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F21 RID: 7969 RVA: 0x001451D4 File Offset: 0x001433D4
	private void LoadObjects_EN(string filename)
	{
		int num = 0;
		string[] array = this.OpenFile(filename).Split(new char[]
		{
			'\n',
			';'
		});
		this.objects_EN = new string[array.Length / 2];
		this.objectsTooltip_EN = new string[array.Length / 2];
		for (int i = 0; i < array.Length; i++)
		{
			this.objects_EN[num] = array[i].Replace("<br>", "\n");
			i++;
			this.objectsTooltip_EN[num] = array[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F22 RID: 7970 RVA: 0x0014526C File Offset: 0x0014346C
	private void LoadObjects_TU(string filename)
	{
		int num = 0;
		string[] array = this.OpenFile(filename).Split(new char[]
		{
			'\n',
			';'
		});
		this.objects_TU = new string[array.Length / 2];
		this.objectsTooltip_TU = new string[array.Length / 2];
		for (int i = 0; i < array.Length; i++)
		{
			this.objects_TU[num] = array[i].Replace("<br>", "\n");
			i++;
			this.objectsTooltip_TU[num] = array[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F23 RID: 7971 RVA: 0x00145304 File Offset: 0x00143504
	private void LoadObjects_CH(string filename)
	{
		int num = 0;
		string[] array = this.OpenFile(filename).Split(new char[]
		{
			'\n',
			';'
		});
		this.objects_CH = new string[array.Length / 2];
		this.objectsTooltip_CH = new string[array.Length / 2];
		for (int i = 0; i < array.Length; i++)
		{
			this.objects_CH[num] = array[i].Replace("<br>", "\n");
			i++;
			this.objectsTooltip_CH[num] = array[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F24 RID: 7972 RVA: 0x0014539C File Offset: 0x0014359C
	private void LoadObjects_FR(string filename)
	{
		int num = 0;
		string[] array = this.OpenFile(filename).Split(new char[]
		{
			'\n',
			';'
		});
		this.objects_FR = new string[array.Length / 2];
		this.objectsTooltip_FR = new string[array.Length / 2];
		for (int i = 0; i < array.Length; i++)
		{
			this.objects_FR[num] = array[i].Replace("<br>", "\n");
			i++;
			this.objectsTooltip_FR[num] = array[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F25 RID: 7973 RVA: 0x00145434 File Offset: 0x00143634
	private void LoadObjects_ES(string filename)
	{
		int num = 0;
		string[] array = this.OpenFile(filename).Split(new char[]
		{
			'\n',
			';'
		});
		this.objects_ES = new string[array.Length / 2];
		this.objectsTooltip_ES = new string[array.Length / 2];
		for (int i = 0; i < array.Length; i++)
		{
			this.objects_ES[num] = array[i].Replace("<br>", "\n");
			i++;
			this.objectsTooltip_ES[num] = array[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F26 RID: 7974 RVA: 0x001454CC File Offset: 0x001436CC
	private void LoadObjects_KO(string filename)
	{
		int num = 0;
		string[] array = this.OpenFile(filename).Split(new char[]
		{
			'\n',
			';'
		});
		this.objects_KO = new string[array.Length / 2];
		this.objectsTooltip_KO = new string[array.Length / 2];
		for (int i = 0; i < array.Length; i++)
		{
			this.objects_KO[num] = array[i].Replace("<br>", "\n");
			i++;
			this.objectsTooltip_KO[num] = array[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F27 RID: 7975 RVA: 0x00145564 File Offset: 0x00143764
	private void LoadObjects_PB(string filename)
	{
		int num = 0;
		string[] array = this.OpenFile(filename).Split(new char[]
		{
			'\n',
			';'
		});
		this.objects_PB = new string[array.Length / 2];
		this.objectsTooltip_PB = new string[array.Length / 2];
		for (int i = 0; i < array.Length; i++)
		{
			this.objects_PB[num] = array[i].Replace("<br>", "\n");
			i++;
			this.objectsTooltip_PB[num] = array[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F28 RID: 7976 RVA: 0x001455FC File Offset: 0x001437FC
	private void LoadObjects_HU(string filename)
	{
		int num = 0;
		string[] array = this.OpenFile(filename).Split(new char[]
		{
			'\n',
			';'
		});
		this.objects_HU = new string[array.Length / 2];
		this.objectsTooltip_HU = new string[array.Length / 2];
		for (int i = 0; i < array.Length; i++)
		{
			this.objects_HU[num] = array[i].Replace("<br>", "\n");
			i++;
			this.objectsTooltip_HU[num] = array[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F29 RID: 7977 RVA: 0x00145694 File Offset: 0x00143894
	private void LoadObjects_RU(string filename)
	{
		int num = 0;
		string[] array = this.OpenFile(filename).Split(new char[]
		{
			'\n',
			';'
		});
		this.objects_RU = new string[array.Length / 2];
		this.objectsTooltip_RU = new string[array.Length / 2];
		for (int i = 0; i < array.Length; i++)
		{
			this.objects_RU[num] = array[i].Replace("<br>", "\n");
			i++;
			this.objectsTooltip_RU[num] = array[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F2A RID: 7978 RVA: 0x0014572C File Offset: 0x0014392C
	private void LoadObjects_CT(string filename)
	{
		int num = 0;
		string[] array = this.OpenFile(filename).Split(new char[]
		{
			'\n',
			';'
		});
		this.objects_CT = new string[array.Length / 2];
		this.objectsTooltip_CT = new string[array.Length / 2];
		for (int i = 0; i < array.Length; i++)
		{
			this.objects_CT[num] = array[i].Replace("<br>", "\n");
			i++;
			this.objectsTooltip_CT[num] = array[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F2B RID: 7979 RVA: 0x001457C4 File Offset: 0x001439C4
	private void LoadObjects_PL(string filename)
	{
		int num = 0;
		string[] array = this.OpenFile(filename).Split(new char[]
		{
			'\n',
			';'
		});
		this.objects_PL = new string[array.Length / 2];
		this.objectsTooltip_PL = new string[array.Length / 2];
		for (int i = 0; i < array.Length; i++)
		{
			this.objects_PL[num] = array[i].Replace("<br>", "\n");
			i++;
			this.objectsTooltip_PL[num] = array[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F2C RID: 7980 RVA: 0x0014585C File Offset: 0x00143A5C
	private void LoadObjects_CZ(string filename)
	{
		int num = 0;
		string[] array = this.OpenFile(filename).Split(new char[]
		{
			'\n',
			';'
		});
		this.objects_CZ = new string[array.Length / 2];
		this.objectsTooltip_CZ = new string[array.Length / 2];
		for (int i = 0; i < array.Length; i++)
		{
			this.objects_CZ[num] = array[i].Replace("<br>", "\n");
			i++;
			this.objectsTooltip_CZ[num] = array[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F2D RID: 7981 RVA: 0x001458F4 File Offset: 0x00143AF4
	private void LoadObjects_AR(string filename)
	{
		int num = 0;
		string[] array = this.OpenFile(filename).Split(new char[]
		{
			'\n',
			';'
		});
		this.objects_AR = new string[array.Length / 2];
		this.objectsTooltip_AR = new string[array.Length / 2];
		for (int i = 0; i < array.Length; i++)
		{
			this.objects_AR[num] = array[i].Replace("<br>", "\n");
			i++;
			this.objectsTooltip_AR[num] = array[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F2E RID: 7982 RVA: 0x0014598C File Offset: 0x00143B8C
	private void LoadObjects_IT(string filename)
	{
		int num = 0;
		string[] array = this.OpenFile(filename).Split(new char[]
		{
			'\n',
			';'
		});
		this.objects_IT = new string[array.Length / 2];
		this.objectsTooltip_IT = new string[array.Length / 2];
		for (int i = 0; i < array.Length; i++)
		{
			this.objects_IT[num] = array[i].Replace("<br>", "\n");
			i++;
			this.objectsTooltip_IT[num] = array[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F2F RID: 7983 RVA: 0x00145A24 File Offset: 0x00143C24
	private void LoadObjects_RO(string filename)
	{
		int num = 0;
		string[] array = this.OpenFile(filename).Split(new char[]
		{
			'\n',
			';'
		});
		this.objects_RO = new string[array.Length / 2];
		this.objectsTooltip_RO = new string[array.Length / 2];
		for (int i = 0; i < array.Length; i++)
		{
			this.objects_RO[num] = array[i].Replace("<br>", "\n");
			i++;
			this.objectsTooltip_RO[num] = array[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F30 RID: 7984 RVA: 0x00145ABC File Offset: 0x00143CBC
	private void LoadObjects_JA(string filename)
	{
		int num = 0;
		string[] array = this.OpenFile(filename).Split(new char[]
		{
			'\n',
			';'
		});
		this.objects_JA = new string[array.Length / 2];
		this.objectsTooltip_JA = new string[array.Length / 2];
		for (int i = 0; i < array.Length; i++)
		{
			this.objects_JA[num] = array[i].Replace("<br>", "\n");
			i++;
			this.objectsTooltip_JA[num] = array[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F31 RID: 7985 RVA: 0x00145B54 File Offset: 0x00143D54
	private void LoadCountry_GE(string filename)
	{
		this.country_GE = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		int num = this.genres_.LoadAmountOfGenres("DATA/Genres.txt");
		for (int i = 0; i < this.country_GE.Length; i++)
		{
			for (int j = 0; j < num; j++)
			{
				if (this.country_GE[i].Contains("<" + j.ToString() + ">"))
				{
					this.country_GE[i] = this.country_GE[i].Replace("<" + j.ToString() + ">", "");
					break;
				}
			}
		}
	}

	// Token: 0x06001F32 RID: 7986 RVA: 0x00145C0E File Offset: 0x00143E0E
	private void LoadCountry_EN(string filename)
	{
		this.country_EN = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F33 RID: 7987 RVA: 0x00145C36 File Offset: 0x00143E36
	private void LoadCountry_TU(string filename)
	{
		this.country_TU = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F34 RID: 7988 RVA: 0x00145C5E File Offset: 0x00143E5E
	private void LoadCountry_CH(string filename)
	{
		this.country_CH = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F35 RID: 7989 RVA: 0x00145C86 File Offset: 0x00143E86
	private void LoadCountry_FR(string filename)
	{
		this.country_FR = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F36 RID: 7990 RVA: 0x00145CAE File Offset: 0x00143EAE
	private void LoadCountry_ES(string filename)
	{
		this.country_ES = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F37 RID: 7991 RVA: 0x00145CD6 File Offset: 0x00143ED6
	private void LoadCountry_KO(string filename)
	{
		this.country_KO = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F38 RID: 7992 RVA: 0x00145CFE File Offset: 0x00143EFE
	private void LoadCountry_PB(string filename)
	{
		this.country_PB = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F39 RID: 7993 RVA: 0x00145D26 File Offset: 0x00143F26
	private void LoadCountry_HU(string filename)
	{
		this.country_HU = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F3A RID: 7994 RVA: 0x00145D4E File Offset: 0x00143F4E
	private void LoadCountry_RU(string filename)
	{
		this.country_RU = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F3B RID: 7995 RVA: 0x00145D76 File Offset: 0x00143F76
	private void LoadCountry_CT(string filename)
	{
		this.country_CT = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F3C RID: 7996 RVA: 0x00145D9E File Offset: 0x00143F9E
	private void LoadCountry_PL(string filename)
	{
		this.country_PL = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F3D RID: 7997 RVA: 0x00145DC6 File Offset: 0x00143FC6
	private void LoadCountry_CZ(string filename)
	{
		this.country_CZ = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F3E RID: 7998 RVA: 0x00145DEE File Offset: 0x00143FEE
	private void LoadCountry_AR(string filename)
	{
		this.country_AR = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F3F RID: 7999 RVA: 0x00145E16 File Offset: 0x00144016
	private void LoadCountry_IT(string filename)
	{
		this.country_IT = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F40 RID: 8000 RVA: 0x00145E3E File Offset: 0x0014403E
	private void LoadCountry_RO(string filename)
	{
		this.country_RO = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F41 RID: 8001 RVA: 0x00145E66 File Offset: 0x00144066
	private void LoadCountry_JA(string filename)
	{
		this.country_JA = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F42 RID: 8002 RVA: 0x00145E90 File Offset: 0x00144090
	private void LoadQuotes_GE(string filename)
	{
		int num = 0;
		this.quotes_GE = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.quotes_GE.Length; i++)
		{
			this.quotes_GE[i] = this.quotes_GE[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F43 RID: 8003 RVA: 0x00145EFC File Offset: 0x001440FC
	private void LoadQuotes_EN(string filename)
	{
		int num = 0;
		this.quotes_EN = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.quotes_EN.Length; i++)
		{
			this.quotes_EN[i] = this.quotes_EN[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F44 RID: 8004 RVA: 0x00145F68 File Offset: 0x00144168
	private void LoadQuotes_TU(string filename)
	{
		int num = 0;
		this.quotes_TU = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.quotes_TU.Length; i++)
		{
			this.quotes_TU[i] = this.quotes_TU[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F45 RID: 8005 RVA: 0x00145FD4 File Offset: 0x001441D4
	private void LoadQuotes_CH(string filename)
	{
		int num = 0;
		this.quotes_CH = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.quotes_CH.Length; i++)
		{
			this.quotes_CH[i] = this.quotes_CH[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F46 RID: 8006 RVA: 0x00146040 File Offset: 0x00144240
	private void LoadQuotes_FR(string filename)
	{
		int num = 0;
		this.quotes_FR = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.quotes_FR.Length; i++)
		{
			this.quotes_FR[i] = this.quotes_FR[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F47 RID: 8007 RVA: 0x001460AC File Offset: 0x001442AC
	private void LoadQuotes_ES(string filename)
	{
		int num = 0;
		this.quotes_ES = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.quotes_ES.Length; i++)
		{
			this.quotes_ES[i] = this.quotes_ES[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F48 RID: 8008 RVA: 0x00146118 File Offset: 0x00144318
	private void LoadQuotes_KO(string filename)
	{
		int num = 0;
		this.quotes_KO = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.quotes_KO.Length; i++)
		{
			this.quotes_KO[i] = this.quotes_KO[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F49 RID: 8009 RVA: 0x00146184 File Offset: 0x00144384
	private void LoadQuotes_PB(string filename)
	{
		int num = 0;
		this.quotes_PB = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.quotes_PB.Length; i++)
		{
			this.quotes_PB[i] = this.quotes_PB[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F4A RID: 8010 RVA: 0x001461F0 File Offset: 0x001443F0
	private void LoadQuotes_HU(string filename)
	{
		int num = 0;
		this.quotes_HU = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.quotes_HU.Length; i++)
		{
			this.quotes_HU[i] = this.quotes_HU[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F4B RID: 8011 RVA: 0x0014625C File Offset: 0x0014445C
	private void LoadQuotes_RU(string filename)
	{
		int num = 0;
		this.quotes_RU = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.quotes_RU.Length; i++)
		{
			this.quotes_RU[i] = this.quotes_RU[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F4C RID: 8012 RVA: 0x001462C8 File Offset: 0x001444C8
	private void LoadQuotes_CT(string filename)
	{
		int num = 0;
		this.quotes_CT = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.quotes_CT.Length; i++)
		{
			this.quotes_CT[i] = this.quotes_CT[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F4D RID: 8013 RVA: 0x00146334 File Offset: 0x00144534
	private void LoadQuotes_PL(string filename)
	{
		int num = 0;
		this.quotes_PL = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.quotes_PL.Length; i++)
		{
			this.quotes_PL[i] = this.quotes_PL[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F4E RID: 8014 RVA: 0x001463A0 File Offset: 0x001445A0
	private void LoadQuotes_CZ(string filename)
	{
		int num = 0;
		this.quotes_CZ = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.quotes_CZ.Length; i++)
		{
			this.quotes_CZ[i] = this.quotes_CZ[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F4F RID: 8015 RVA: 0x0014640C File Offset: 0x0014460C
	private void LoadQuotes_AR(string filename)
	{
		int num = 0;
		this.quotes_AR = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.quotes_AR.Length; i++)
		{
			this.quotes_AR[i] = this.quotes_AR[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F50 RID: 8016 RVA: 0x00146478 File Offset: 0x00144678
	private void LoadQuotes_IT(string filename)
	{
		int num = 0;
		this.quotes_IT = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.quotes_IT.Length; i++)
		{
			this.quotes_IT[i] = this.quotes_IT[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F51 RID: 8017 RVA: 0x001464E4 File Offset: 0x001446E4
	private void LoadQuotes_RO(string filename)
	{
		int num = 0;
		this.quotes_RO = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.quotes_RO.Length; i++)
		{
			this.quotes_RO[i] = this.quotes_RO[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F52 RID: 8018 RVA: 0x00146550 File Offset: 0x00144750
	private void LoadQuotes_JA(string filename)
	{
		int num = 0;
		this.quotes_JA = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.quotes_JA.Length; i++)
		{
			this.quotes_JA[i] = this.quotes_JA[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F53 RID: 8019 RVA: 0x001465BC File Offset: 0x001447BC
	private void LoadContractWork_GE(string filename)
	{
		int num = 0;
		this.contractWork_GE = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.contractWork_GE.Length; i++)
		{
			this.contractWork_GE[i] = this.contractWork_GE[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F54 RID: 8020 RVA: 0x00146628 File Offset: 0x00144828
	private void LoadContractWork_EN(string filename)
	{
		int num = 0;
		this.contractWork_EN = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.contractWork_EN.Length; i++)
		{
			this.contractWork_EN[i] = this.contractWork_EN[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F55 RID: 8021 RVA: 0x00146694 File Offset: 0x00144894
	private void LoadContractWork_TU(string filename)
	{
		int num = 0;
		this.contractWork_TU = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.contractWork_TU.Length; i++)
		{
			this.contractWork_TU[i] = this.contractWork_TU[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F56 RID: 8022 RVA: 0x00146700 File Offset: 0x00144900
	private void LoadContractWork_CH(string filename)
	{
		int num = 0;
		this.contractWork_CH = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.contractWork_CH.Length; i++)
		{
			this.contractWork_CH[i] = this.contractWork_CH[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F57 RID: 8023 RVA: 0x0014676C File Offset: 0x0014496C
	private void LoadContractWork_FR(string filename)
	{
		int num = 0;
		this.contractWork_FR = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.contractWork_FR.Length; i++)
		{
			this.contractWork_FR[i] = this.contractWork_FR[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F58 RID: 8024 RVA: 0x001467D8 File Offset: 0x001449D8
	private void LoadContractWork_ES(string filename)
	{
		int num = 0;
		this.contractWork_ES = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.contractWork_ES.Length; i++)
		{
			this.contractWork_ES[i] = this.contractWork_ES[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F59 RID: 8025 RVA: 0x00146844 File Offset: 0x00144A44
	private void LoadContractWork_KO(string filename)
	{
		int num = 0;
		this.contractWork_KO = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.contractWork_KO.Length; i++)
		{
			this.contractWork_KO[i] = this.contractWork_KO[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F5A RID: 8026 RVA: 0x001468B0 File Offset: 0x00144AB0
	private void LoadContractWork_PB(string filename)
	{
		int num = 0;
		this.contractWork_PB = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.contractWork_PB.Length; i++)
		{
			this.contractWork_PB[i] = this.contractWork_PB[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F5B RID: 8027 RVA: 0x0014691C File Offset: 0x00144B1C
	private void LoadContractWork_HU(string filename)
	{
		int num = 0;
		this.contractWork_HU = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.contractWork_HU.Length; i++)
		{
			this.contractWork_HU[i] = this.contractWork_HU[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F5C RID: 8028 RVA: 0x00146988 File Offset: 0x00144B88
	private void LoadContractWork_RU(string filename)
	{
		int num = 0;
		this.contractWork_RU = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.contractWork_RU.Length; i++)
		{
			this.contractWork_RU[i] = this.contractWork_RU[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F5D RID: 8029 RVA: 0x001469F4 File Offset: 0x00144BF4
	private void LoadContractWork_CT(string filename)
	{
		int num = 0;
		this.contractWork_CT = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.contractWork_CT.Length; i++)
		{
			this.contractWork_CT[i] = this.contractWork_CT[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F5E RID: 8030 RVA: 0x00146A60 File Offset: 0x00144C60
	private void LoadContractWork_PL(string filename)
	{
		int num = 0;
		this.contractWork_PL = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.contractWork_PL.Length; i++)
		{
			this.contractWork_PL[i] = this.contractWork_PL[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F5F RID: 8031 RVA: 0x00146ACC File Offset: 0x00144CCC
	private void LoadContractWork_CZ(string filename)
	{
		int num = 0;
		this.contractWork_CZ = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.contractWork_CZ.Length; i++)
		{
			this.contractWork_CZ[i] = this.contractWork_CZ[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F60 RID: 8032 RVA: 0x00146B38 File Offset: 0x00144D38
	private void LoadContractWork_AR(string filename)
	{
		int num = 0;
		this.contractWork_AR = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.contractWork_AR.Length; i++)
		{
			this.contractWork_AR[i] = this.contractWork_AR[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F61 RID: 8033 RVA: 0x00146BA4 File Offset: 0x00144DA4
	private void LoadContractWork_IT(string filename)
	{
		int num = 0;
		this.contractWork_IT = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.contractWork_IT.Length; i++)
		{
			this.contractWork_IT[i] = this.contractWork_IT[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F62 RID: 8034 RVA: 0x00146C10 File Offset: 0x00144E10
	private void LoadContractWork_RO(string filename)
	{
		int num = 0;
		this.contractWork_RO = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.contractWork_RO.Length; i++)
		{
			this.contractWork_RO[i] = this.contractWork_RO[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F63 RID: 8035 RVA: 0x00146C7C File Offset: 0x00144E7C
	private void LoadContractWork_JA(string filename)
	{
		int num = 0;
		this.contractWork_JA = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.contractWork_JA.Length; i++)
		{
			this.contractWork_JA[i] = this.contractWork_JA[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F64 RID: 8036 RVA: 0x00146CE8 File Offset: 0x00144EE8
	private void LoadFanLetter_GE(string filename)
	{
		int num = 0;
		this.fanLetter_GE = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.fanLetter_GE.Length; i++)
		{
			this.fanLetter_GE[i] = this.fanLetter_GE[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F65 RID: 8037 RVA: 0x00146D54 File Offset: 0x00144F54
	private void LoadFanLetter_EN(string filename)
	{
		int num = 0;
		this.fanLetter_EN = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.fanLetter_EN.Length; i++)
		{
			this.fanLetter_EN[i] = this.fanLetter_EN[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F66 RID: 8038 RVA: 0x00146DC0 File Offset: 0x00144FC0
	private void LoadFanLetter_TU(string filename)
	{
		int num = 0;
		this.fanLetter_TU = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.fanLetter_TU.Length; i++)
		{
			this.fanLetter_TU[i] = this.fanLetter_TU[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F67 RID: 8039 RVA: 0x00146E2C File Offset: 0x0014502C
	private void LoadFanLetter_CH(string filename)
	{
		int num = 0;
		this.fanLetter_CH = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.fanLetter_CH.Length; i++)
		{
			this.fanLetter_CH[i] = this.fanLetter_CH[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F68 RID: 8040 RVA: 0x00146E98 File Offset: 0x00145098
	private void LoadFanLetter_FR(string filename)
	{
		int num = 0;
		this.fanLetter_FR = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.fanLetter_FR.Length; i++)
		{
			this.fanLetter_FR[i] = this.fanLetter_FR[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F69 RID: 8041 RVA: 0x00146F04 File Offset: 0x00145104
	private void LoadFanLetter_ES(string filename)
	{
		int num = 0;
		this.fanLetter_ES = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.fanLetter_ES.Length; i++)
		{
			this.fanLetter_ES[i] = this.fanLetter_ES[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F6A RID: 8042 RVA: 0x00146F70 File Offset: 0x00145170
	private void LoadFanLetter_KO(string filename)
	{
		int num = 0;
		this.fanLetter_KO = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.fanLetter_KO.Length; i++)
		{
			this.fanLetter_KO[i] = this.fanLetter_KO[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F6B RID: 8043 RVA: 0x00146FDC File Offset: 0x001451DC
	private void LoadFanLetter_PB(string filename)
	{
		int num = 0;
		this.fanLetter_PB = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.fanLetter_PB.Length; i++)
		{
			this.fanLetter_PB[i] = this.fanLetter_PB[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F6C RID: 8044 RVA: 0x00147048 File Offset: 0x00145248
	private void LoadFanLetter_HU(string filename)
	{
		int num = 0;
		this.fanLetter_HU = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.fanLetter_HU.Length; i++)
		{
			this.fanLetter_HU[i] = this.fanLetter_HU[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F6D RID: 8045 RVA: 0x001470B4 File Offset: 0x001452B4
	private void LoadFanLetter_RU(string filename)
	{
		int num = 0;
		this.fanLetter_RU = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.fanLetter_RU.Length; i++)
		{
			this.fanLetter_RU[i] = this.fanLetter_RU[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F6E RID: 8046 RVA: 0x00147120 File Offset: 0x00145320
	private void LoadFanLetter_CT(string filename)
	{
		int num = 0;
		this.fanLetter_CT = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.fanLetter_CT.Length; i++)
		{
			this.fanLetter_CT[i] = this.fanLetter_CT[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F6F RID: 8047 RVA: 0x0014718C File Offset: 0x0014538C
	private void LoadFanLetter_PL(string filename)
	{
		int num = 0;
		this.fanLetter_PL = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.fanLetter_PL.Length; i++)
		{
			this.fanLetter_PL[i] = this.fanLetter_PL[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F70 RID: 8048 RVA: 0x001471F8 File Offset: 0x001453F8
	private void LoadFanLetter_CZ(string filename)
	{
		int num = 0;
		this.fanLetter_CZ = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.fanLetter_CZ.Length; i++)
		{
			this.fanLetter_CZ[i] = this.fanLetter_CZ[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F71 RID: 8049 RVA: 0x00147264 File Offset: 0x00145464
	private void LoadFanLetter_AR(string filename)
	{
		int num = 0;
		this.fanLetter_AR = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.fanLetter_AR.Length; i++)
		{
			this.fanLetter_AR[i] = this.fanLetter_AR[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F72 RID: 8050 RVA: 0x001472D0 File Offset: 0x001454D0
	private void LoadFanLetter_IT(string filename)
	{
		int num = 0;
		this.fanLetter_IT = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.fanLetter_IT.Length; i++)
		{
			this.fanLetter_IT[i] = this.fanLetter_IT[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F73 RID: 8051 RVA: 0x0014733C File Offset: 0x0014553C
	private void LoadFanLetter_RO(string filename)
	{
		int num = 0;
		this.fanLetter_RO = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.fanLetter_RO.Length; i++)
		{
			this.fanLetter_RO[i] = this.fanLetter_RO[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F74 RID: 8052 RVA: 0x001473A8 File Offset: 0x001455A8
	private void LoadFanLetter_JA(string filename)
	{
		int num = 0;
		this.fanLetter_JA = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.fanLetter_JA.Length; i++)
		{
			this.fanLetter_JA[i] = this.fanLetter_JA[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F75 RID: 8053 RVA: 0x00147414 File Offset: 0x00145614
	private void LoadTutorial_GE(string filename)
	{
		int num = 0;
		this.tutorial_GE = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.tutorial_GE.Length; i++)
		{
			this.tutorial_GE[i] = this.tutorial_GE[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F76 RID: 8054 RVA: 0x00147480 File Offset: 0x00145680
	private void LoadTutorial_EN(string filename)
	{
		int num = 0;
		this.tutorial_EN = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.tutorial_EN.Length; i++)
		{
			this.tutorial_EN[i] = this.tutorial_EN[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F77 RID: 8055 RVA: 0x001474EC File Offset: 0x001456EC
	private void LoadTutorial_TU(string filename)
	{
		int num = 0;
		this.tutorial_TU = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.tutorial_TU.Length; i++)
		{
			this.tutorial_TU[i] = this.tutorial_TU[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F78 RID: 8056 RVA: 0x00147558 File Offset: 0x00145758
	private void LoadTutorial_CH(string filename)
	{
		int num = 0;
		this.tutorial_CH = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.tutorial_CH.Length; i++)
		{
			this.tutorial_CH[i] = this.tutorial_CH[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F79 RID: 8057 RVA: 0x001475C4 File Offset: 0x001457C4
	private void LoadTutorial_FR(string filename)
	{
		int num = 0;
		this.tutorial_FR = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.tutorial_FR.Length; i++)
		{
			this.tutorial_FR[i] = this.tutorial_FR[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F7A RID: 8058 RVA: 0x00147630 File Offset: 0x00145830
	private void LoadTutorial_ES(string filename)
	{
		int num = 0;
		this.tutorial_ES = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.tutorial_ES.Length; i++)
		{
			this.tutorial_ES[i] = this.tutorial_ES[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F7B RID: 8059 RVA: 0x0014769C File Offset: 0x0014589C
	private void LoadTutorial_KO(string filename)
	{
		int num = 0;
		this.tutorial_KO = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.tutorial_KO.Length; i++)
		{
			this.tutorial_KO[i] = this.tutorial_KO[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F7C RID: 8060 RVA: 0x00147708 File Offset: 0x00145908
	private void LoadTutorial_PB(string filename)
	{
		int num = 0;
		this.tutorial_PB = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.tutorial_PB.Length; i++)
		{
			this.tutorial_PB[i] = this.tutorial_PB[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F7D RID: 8061 RVA: 0x00147774 File Offset: 0x00145974
	private void LoadTutorial_HU(string filename)
	{
		int num = 0;
		this.tutorial_HU = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.tutorial_HU.Length; i++)
		{
			this.tutorial_HU[i] = this.tutorial_HU[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F7E RID: 8062 RVA: 0x001477E0 File Offset: 0x001459E0
	private void LoadTutorial_RU(string filename)
	{
		int num = 0;
		this.tutorial_RU = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.tutorial_RU.Length; i++)
		{
			this.tutorial_RU[i] = this.tutorial_RU[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F7F RID: 8063 RVA: 0x0014784C File Offset: 0x00145A4C
	private void LoadTutorial_CT(string filename)
	{
		int num = 0;
		this.tutorial_CT = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.tutorial_CT.Length; i++)
		{
			this.tutorial_CT[i] = this.tutorial_CT[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F80 RID: 8064 RVA: 0x001478B8 File Offset: 0x00145AB8
	private void LoadTutorial_PL(string filename)
	{
		int num = 0;
		this.tutorial_PL = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.tutorial_PL.Length; i++)
		{
			this.tutorial_PL[i] = this.tutorial_PL[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F81 RID: 8065 RVA: 0x00147924 File Offset: 0x00145B24
	private void LoadTutorial_CZ(string filename)
	{
		int num = 0;
		this.tutorial_CZ = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.tutorial_CZ.Length; i++)
		{
			this.tutorial_CZ[i] = this.tutorial_CZ[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F82 RID: 8066 RVA: 0x00147990 File Offset: 0x00145B90
	private void LoadTutorial_AR(string filename)
	{
		int num = 0;
		this.tutorial_AR = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.tutorial_AR.Length; i++)
		{
			this.tutorial_AR[i] = this.tutorial_AR[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F83 RID: 8067 RVA: 0x001479FC File Offset: 0x00145BFC
	private void LoadTutorial_IT(string filename)
	{
		int num = 0;
		this.tutorial_IT = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.tutorial_IT.Length; i++)
		{
			this.tutorial_IT[i] = this.tutorial_IT[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F84 RID: 8068 RVA: 0x00147A68 File Offset: 0x00145C68
	private void LoadTutorial_RO(string filename)
	{
		int num = 0;
		this.tutorial_RO = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.tutorial_RO.Length; i++)
		{
			this.tutorial_RO[i] = this.tutorial_RO[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F85 RID: 8069 RVA: 0x00147AD4 File Offset: 0x00145CD4
	private void LoadTutorial_JA(string filename)
	{
		int num = 0;
		this.tutorial_JA = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.tutorial_JA.Length; i++)
		{
			this.tutorial_JA[i] = this.tutorial_JA[i].Replace("<br>", "\n");
			num++;
		}
	}

	// Token: 0x06001F86 RID: 8070 RVA: 0x00147B40 File Offset: 0x00145D40
	private string RemoveThemesFit(string c)
	{
		int num = this.genres_.LoadAmountOfGenres("DATA/Genres.txt");
		for (int i = 0; i < num; i++)
		{
			c = c.Replace("<" + i.ToString() + ">", "");
		}
		for (int j = 0; j < 6; j++)
		{
			c = c.Replace("<M" + j.ToString() + ">", "");
		}
		c = c.Replace("\n", string.Empty);
		c = c.Replace("\r", string.Empty);
		c = c.Replace("\t", string.Empty);
		if (c[c.Length - 1] == ' ')
		{
			c = c.Remove(c.Length - 1);
		}
		return c;
	}

	// Token: 0x06001F87 RID: 8071 RVA: 0x00147C18 File Offset: 0x00145E18
	private void LoadThemes_GE(string filename)
	{
		int num = 0;
		this.themes_GE = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		for (int i = 0; i < this.themes_GE.Length; i++)
		{
			this.themes_GE[i] = this.RemoveThemesFit(this.themes_GE[i]);
			num++;
		}
	}

	// Token: 0x06001F88 RID: 8072 RVA: 0x00147C7A File Offset: 0x00145E7A
	private void LoadThemes_EN(string filename)
	{
		this.themes_EN = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F89 RID: 8073 RVA: 0x00147CA2 File Offset: 0x00145EA2
	private void LoadThemes_TU(string filename)
	{
		this.themes_TU = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F8A RID: 8074 RVA: 0x00147CCA File Offset: 0x00145ECA
	private void LoadThemes_CH(string filename)
	{
		this.themes_CH = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F8B RID: 8075 RVA: 0x00147CF2 File Offset: 0x00145EF2
	private void LoadThemes_FR(string filename)
	{
		this.themes_FR = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F8C RID: 8076 RVA: 0x00147D1A File Offset: 0x00145F1A
	private void LoadThemes_ES(string filename)
	{
		this.themes_ES = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F8D RID: 8077 RVA: 0x00147D42 File Offset: 0x00145F42
	private void LoadThemes_KO(string filename)
	{
		this.themes_KO = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F8E RID: 8078 RVA: 0x00147D6A File Offset: 0x00145F6A
	private void LoadThemes_PB(string filename)
	{
		this.themes_PB = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F8F RID: 8079 RVA: 0x00147D92 File Offset: 0x00145F92
	private void LoadThemes_HU(string filename)
	{
		this.themes_HU = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F90 RID: 8080 RVA: 0x00147DBA File Offset: 0x00145FBA
	private void LoadThemes_RU(string filename)
	{
		this.themes_RU = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F91 RID: 8081 RVA: 0x00147DE2 File Offset: 0x00145FE2
	private void LoadThemes_CT(string filename)
	{
		this.themes_CT = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F92 RID: 8082 RVA: 0x00147E0A File Offset: 0x0014600A
	private void LoadThemes_PL(string filename)
	{
		this.themes_PL = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F93 RID: 8083 RVA: 0x00147E32 File Offset: 0x00146032
	private void LoadThemes_CZ(string filename)
	{
		this.themes_CZ = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F94 RID: 8084 RVA: 0x00147E5A File Offset: 0x0014605A
	private void LoadThemes_AR(string filename)
	{
		this.themes_AR = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F95 RID: 8085 RVA: 0x00147E82 File Offset: 0x00146082
	private void LoadThemes_IT(string filename)
	{
		this.themes_IT = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F96 RID: 8086 RVA: 0x00147EAA File Offset: 0x001460AA
	private void LoadThemes_RO(string filename)
	{
		this.themes_RO = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F97 RID: 8087 RVA: 0x00147ED2 File Offset: 0x001460D2
	private void LoadThemes_JA(string filename)
	{
		this.themes_JA = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F98 RID: 8088 RVA: 0x00147EFC File Offset: 0x001460FC
	private void LoadDevLegends(string filename)
	{
		int num = 0;
		this.devLegends = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		if (this.mS_.devLegendsInUse.Length == 0)
		{
			this.mS_.devLegendsInUse = new bool[this.devLegends.Length];
			this.mS_.devLegendsFemale = new bool[this.devLegends.Length];
			this.mS_.devLegendsDesigner = new bool[this.devLegends.Length];
			this.mS_.devLegendsProgrammierer = new bool[this.devLegends.Length];
			this.mS_.devLegendsGrafiker = new bool[this.devLegends.Length];
			this.mS_.devLegendsMusiker = new bool[this.devLegends.Length];
			this.mS_.devLegendsForscher = new bool[this.devLegends.Length];
			this.mS_.devLegendsHardware = new bool[this.devLegends.Length];
		}
		if (this.mS_.devLegendsInUse.Length != this.devLegends.Length)
		{
			this.mS_.devLegendsInUse = new bool[this.devLegends.Length];
			this.mS_.devLegendsFemale = new bool[this.devLegends.Length];
			this.mS_.devLegendsDesigner = new bool[this.devLegends.Length];
			this.mS_.devLegendsProgrammierer = new bool[this.devLegends.Length];
			this.mS_.devLegendsGrafiker = new bool[this.devLegends.Length];
			this.mS_.devLegendsMusiker = new bool[this.devLegends.Length];
			this.mS_.devLegendsForscher = new bool[this.devLegends.Length];
			this.mS_.devLegendsHardware = new bool[this.devLegends.Length];
		}
		for (int i = 0; i < this.devLegends.Length; i++)
		{
			if (this.devLegends[i].Contains("<f>"))
			{
				if (this.devLegends.Length != this.mS_.devLegendsFemale.Length)
				{
					this.mS_.devLegendsFemale = new bool[this.devLegends.Length];
				}
				this.mS_.devLegendsFemale[i] = true;
			}
			if (this.devLegends[i].Contains("<D>"))
			{
				if (this.devLegends.Length != this.mS_.devLegendsDesigner.Length)
				{
					this.mS_.devLegendsDesigner = new bool[this.devLegends.Length];
				}
				this.mS_.devLegendsDesigner[i] = true;
			}
			if (this.devLegends[i].Contains("<P>"))
			{
				if (this.devLegends.Length != this.mS_.devLegendsProgrammierer.Length)
				{
					this.mS_.devLegendsProgrammierer = new bool[this.devLegends.Length];
				}
				this.mS_.devLegendsProgrammierer[i] = true;
			}
			if (this.devLegends[i].Contains("<G>"))
			{
				if (this.devLegends.Length != this.mS_.devLegendsGrafiker.Length)
				{
					this.mS_.devLegendsGrafiker = new bool[this.devLegends.Length];
				}
				this.mS_.devLegendsGrafiker[i] = true;
			}
			if (this.devLegends[i].Contains("<S>"))
			{
				if (this.devLegends.Length != this.mS_.devLegendsMusiker.Length)
				{
					this.mS_.devLegendsMusiker = new bool[this.devLegends.Length];
				}
				this.mS_.devLegendsMusiker[i] = true;
			}
			if (this.devLegends[i].Contains("<R>"))
			{
				if (this.devLegends.Length != this.mS_.devLegendsForscher.Length)
				{
					this.mS_.devLegendsForscher = new bool[this.devLegends.Length];
				}
				this.mS_.devLegendsForscher[i] = true;
			}
			if (this.devLegends[i].Contains("<T>"))
			{
				if (this.devLegends.Length != this.mS_.devLegendsHardware.Length)
				{
					this.mS_.devLegendsHardware = new bool[this.devLegends.Length];
				}
				this.mS_.devLegendsHardware[i] = true;
			}
			this.devLegends[i] = this.RemoveDevLegendsTags(this.devLegends[i]);
			num++;
		}
	}

	// Token: 0x06001F99 RID: 8089 RVA: 0x00148348 File Offset: 0x00146548
	private string RemoveDevLegendsTags(string c)
	{
		c = c.Replace("<D>", string.Empty);
		c = c.Replace("<P>", string.Empty);
		c = c.Replace("<G>", string.Empty);
		c = c.Replace("<S>", string.Empty);
		c = c.Replace("<R>", string.Empty);
		c = c.Replace("<T>", string.Empty);
		c = c.Replace("<f>", string.Empty);
		c = c.Replace("\n", string.Empty);
		c = c.Replace("\r", string.Empty);
		c = c.Replace("\t", string.Empty);
		if (c[c.Length - 1] == ' ')
		{
			c = c.Remove(c.Length - 1);
		}
		return c;
	}

	// Token: 0x06001F9A RID: 8090 RVA: 0x0014842C File Offset: 0x0014662C
	public int GetDevLegend()
	{
		int i = 0;
		while (i < 100000)
		{
			i++;
			if (this.devLegends.Length == 0)
			{
				return -1;
			}
			int num = UnityEngine.Random.Range(0, this.devLegends.Length);
			if (!this.mS_.devLegendsInUse[num])
			{
				return num;
			}
		}
		return -1;
	}

	// Token: 0x040026BB RID: 9915
	private GameObject main_;

	// Token: 0x040026BC RID: 9916
	private mainScript mS_;

	// Token: 0x040026BD RID: 9917
	private settingsScript settings_;

	// Token: 0x040026BE RID: 9918
	private themes themes_;

	// Token: 0x040026BF RID: 9919
	private genres genres_;

	// Token: 0x040026C0 RID: 9920
	public string[] namesFemale;

	// Token: 0x040026C1 RID: 9921
	public string[] namesMale;

	// Token: 0x040026C2 RID: 9922
	public string[] surname;

	// Token: 0x040026C3 RID: 9923
	public string[] devLegends;

	// Token: 0x040026C4 RID: 9924
	public string[] randomEngineNames;

	// Token: 0x040026C5 RID: 9925
	public string[] randomGameNames;

	// Token: 0x040026C6 RID: 9926
	public string[] randomPlatformNames;

	// Token: 0x040026C7 RID: 9927
	public string[] randomCompanyNames;

	// Token: 0x040026C8 RID: 9928
	public string credits;

	// Token: 0x040026C9 RID: 9929
	public string[] npcGames;

	// Token: 0x040026CA RID: 9930
	public bool[] npcGameNameInUse;

	// Token: 0x040026CB RID: 9931
	public string[] npcAddons;

	// Token: 0x040026CC RID: 9932
	public string[] npcIPs;

	// Token: 0x040026CD RID: 9933
	public bool[] npcIPsInUse;

	// Token: 0x040026CE RID: 9934
	public string[] text_EN;

	// Token: 0x040026CF RID: 9935
	public string[] text_GE;

	// Token: 0x040026D0 RID: 9936
	public string[] text_TU;

	// Token: 0x040026D1 RID: 9937
	public string[] text_CH;

	// Token: 0x040026D2 RID: 9938
	public string[] text_FR;

	// Token: 0x040026D3 RID: 9939
	public string[] text_ES;

	// Token: 0x040026D4 RID: 9940
	public string[] text_KO;

	// Token: 0x040026D5 RID: 9941
	public string[] text_PB;

	// Token: 0x040026D6 RID: 9942
	public string[] text_HU;

	// Token: 0x040026D7 RID: 9943
	public string[] text_RU;

	// Token: 0x040026D8 RID: 9944
	public string[] text_CT;

	// Token: 0x040026D9 RID: 9945
	public string[] text_PL;

	// Token: 0x040026DA RID: 9946
	public string[] text_CZ;

	// Token: 0x040026DB RID: 9947
	public string[] text_AR;

	// Token: 0x040026DC RID: 9948
	public string[] text_IT;

	// Token: 0x040026DD RID: 9949
	public string[] text_RO;

	// Token: 0x040026DE RID: 9950
	public string[] text_JA;

	// Token: 0x040026DF RID: 9951
	public string[] achivementsName_EN;

	// Token: 0x040026E0 RID: 9952
	public string[] achivementsName_GE;

	// Token: 0x040026E1 RID: 9953
	public string[] achivementsName_TU;

	// Token: 0x040026E2 RID: 9954
	public string[] achivementsName_CH;

	// Token: 0x040026E3 RID: 9955
	public string[] achivementsName_FR;

	// Token: 0x040026E4 RID: 9956
	public string[] achivementsName_ES;

	// Token: 0x040026E5 RID: 9957
	public string[] achivementsName_KO;

	// Token: 0x040026E6 RID: 9958
	public string[] achivementsName_PB;

	// Token: 0x040026E7 RID: 9959
	public string[] achivementsName_HU;

	// Token: 0x040026E8 RID: 9960
	public string[] achivementsName_RU;

	// Token: 0x040026E9 RID: 9961
	public string[] achivementsName_CT;

	// Token: 0x040026EA RID: 9962
	public string[] achivementsName_PL;

	// Token: 0x040026EB RID: 9963
	public string[] achivementsName_CZ;

	// Token: 0x040026EC RID: 9964
	public string[] achivementsName_AR;

	// Token: 0x040026ED RID: 9965
	public string[] achivementsName_IT;

	// Token: 0x040026EE RID: 9966
	public string[] achivementsName_RO;

	// Token: 0x040026EF RID: 9967
	public string[] achivementsName_JA;

	// Token: 0x040026F0 RID: 9968
	public string[] achivementsDesc_EN;

	// Token: 0x040026F1 RID: 9969
	public string[] achivementsDesc_GE;

	// Token: 0x040026F2 RID: 9970
	public string[] achivementsDesc_TU;

	// Token: 0x040026F3 RID: 9971
	public string[] achivementsDesc_CH;

	// Token: 0x040026F4 RID: 9972
	public string[] achivementsDesc_FR;

	// Token: 0x040026F5 RID: 9973
	public string[] achivementsDesc_ES;

	// Token: 0x040026F6 RID: 9974
	public string[] achivementsDesc_KO;

	// Token: 0x040026F7 RID: 9975
	public string[] achivementsDesc_PB;

	// Token: 0x040026F8 RID: 9976
	public string[] achivementsDesc_HU;

	// Token: 0x040026F9 RID: 9977
	public string[] achivementsDesc_RU;

	// Token: 0x040026FA RID: 9978
	public string[] achivementsDesc_CT;

	// Token: 0x040026FB RID: 9979
	public string[] achivementsDesc_PL;

	// Token: 0x040026FC RID: 9980
	public string[] achivementsDesc_CZ;

	// Token: 0x040026FD RID: 9981
	public string[] achivementsDesc_AR;

	// Token: 0x040026FE RID: 9982
	public string[] achivementsDesc_IT;

	// Token: 0x040026FF RID: 9983
	public string[] achivementsDesc_RO;

	// Token: 0x04002700 RID: 9984
	public string[] achivementsDesc_JA;

	// Token: 0x04002701 RID: 9985
	public string[] objects_EN;

	// Token: 0x04002702 RID: 9986
	public string[] objects_GE;

	// Token: 0x04002703 RID: 9987
	public string[] objects_TU;

	// Token: 0x04002704 RID: 9988
	public string[] objects_CH;

	// Token: 0x04002705 RID: 9989
	public string[] objects_FR;

	// Token: 0x04002706 RID: 9990
	public string[] objects_ES;

	// Token: 0x04002707 RID: 9991
	public string[] objects_KO;

	// Token: 0x04002708 RID: 9992
	public string[] objects_PB;

	// Token: 0x04002709 RID: 9993
	public string[] objects_HU;

	// Token: 0x0400270A RID: 9994
	public string[] objects_RU;

	// Token: 0x0400270B RID: 9995
	public string[] objects_CT;

	// Token: 0x0400270C RID: 9996
	public string[] objects_PL;

	// Token: 0x0400270D RID: 9997
	public string[] objects_CZ;

	// Token: 0x0400270E RID: 9998
	public string[] objects_AR;

	// Token: 0x0400270F RID: 9999
	public string[] objects_IT;

	// Token: 0x04002710 RID: 10000
	public string[] objects_RO;

	// Token: 0x04002711 RID: 10001
	public string[] objects_JA;

	// Token: 0x04002712 RID: 10002
	public string[] objectsTooltip_EN;

	// Token: 0x04002713 RID: 10003
	public string[] objectsTooltip_GE;

	// Token: 0x04002714 RID: 10004
	public string[] objectsTooltip_TU;

	// Token: 0x04002715 RID: 10005
	public string[] objectsTooltip_CH;

	// Token: 0x04002716 RID: 10006
	public string[] objectsTooltip_FR;

	// Token: 0x04002717 RID: 10007
	public string[] objectsTooltip_ES;

	// Token: 0x04002718 RID: 10008
	public string[] objectsTooltip_KO;

	// Token: 0x04002719 RID: 10009
	public string[] objectsTooltip_PB;

	// Token: 0x0400271A RID: 10010
	public string[] objectsTooltip_HU;

	// Token: 0x0400271B RID: 10011
	public string[] objectsTooltip_RU;

	// Token: 0x0400271C RID: 10012
	public string[] objectsTooltip_CT;

	// Token: 0x0400271D RID: 10013
	public string[] objectsTooltip_PL;

	// Token: 0x0400271E RID: 10014
	public string[] objectsTooltip_CZ;

	// Token: 0x0400271F RID: 10015
	public string[] objectsTooltip_AR;

	// Token: 0x04002720 RID: 10016
	public string[] objectsTooltip_IT;

	// Token: 0x04002721 RID: 10017
	public string[] objectsTooltip_RO;

	// Token: 0x04002722 RID: 10018
	public string[] objectsTooltip_JA;

	// Token: 0x04002723 RID: 10019
	public string[] country_EN;

	// Token: 0x04002724 RID: 10020
	public string[] country_GE;

	// Token: 0x04002725 RID: 10021
	public string[] country_TU;

	// Token: 0x04002726 RID: 10022
	public string[] country_CH;

	// Token: 0x04002727 RID: 10023
	public string[] country_FR;

	// Token: 0x04002728 RID: 10024
	public string[] country_ES;

	// Token: 0x04002729 RID: 10025
	public string[] country_KO;

	// Token: 0x0400272A RID: 10026
	public string[] country_PB;

	// Token: 0x0400272B RID: 10027
	public string[] country_HU;

	// Token: 0x0400272C RID: 10028
	public string[] country_RU;

	// Token: 0x0400272D RID: 10029
	public string[] country_CT;

	// Token: 0x0400272E RID: 10030
	public string[] country_PL;

	// Token: 0x0400272F RID: 10031
	public string[] country_CZ;

	// Token: 0x04002730 RID: 10032
	public string[] country_AR;

	// Token: 0x04002731 RID: 10033
	public string[] country_IT;

	// Token: 0x04002732 RID: 10034
	public string[] country_RO;

	// Token: 0x04002733 RID: 10035
	public string[] country_JA;

	// Token: 0x04002734 RID: 10036
	public string[] quotes_EN;

	// Token: 0x04002735 RID: 10037
	public string[] quotes_GE;

	// Token: 0x04002736 RID: 10038
	public string[] quotes_TU;

	// Token: 0x04002737 RID: 10039
	public string[] quotes_CH;

	// Token: 0x04002738 RID: 10040
	public string[] quotes_FR;

	// Token: 0x04002739 RID: 10041
	public string[] quotes_ES;

	// Token: 0x0400273A RID: 10042
	public string[] quotes_KO;

	// Token: 0x0400273B RID: 10043
	public string[] quotes_PB;

	// Token: 0x0400273C RID: 10044
	public string[] quotes_HU;

	// Token: 0x0400273D RID: 10045
	public string[] quotes_RU;

	// Token: 0x0400273E RID: 10046
	public string[] quotes_CT;

	// Token: 0x0400273F RID: 10047
	public string[] quotes_PL;

	// Token: 0x04002740 RID: 10048
	public string[] quotes_CZ;

	// Token: 0x04002741 RID: 10049
	public string[] quotes_AR;

	// Token: 0x04002742 RID: 10050
	public string[] quotes_IT;

	// Token: 0x04002743 RID: 10051
	public string[] quotes_RO;

	// Token: 0x04002744 RID: 10052
	public string[] quotes_JA;

	// Token: 0x04002745 RID: 10053
	public string[] themes_EN;

	// Token: 0x04002746 RID: 10054
	public string[] themes_GE;

	// Token: 0x04002747 RID: 10055
	public string[] themes_TU;

	// Token: 0x04002748 RID: 10056
	public string[] themes_CH;

	// Token: 0x04002749 RID: 10057
	public string[] themes_FR;

	// Token: 0x0400274A RID: 10058
	public string[] themes_ES;

	// Token: 0x0400274B RID: 10059
	public string[] themes_KO;

	// Token: 0x0400274C RID: 10060
	public string[] themes_PB;

	// Token: 0x0400274D RID: 10061
	public string[] themes_HU;

	// Token: 0x0400274E RID: 10062
	public string[] themes_RU;

	// Token: 0x0400274F RID: 10063
	public string[] themes_CT;

	// Token: 0x04002750 RID: 10064
	public string[] themes_PL;

	// Token: 0x04002751 RID: 10065
	public string[] themes_CZ;

	// Token: 0x04002752 RID: 10066
	public string[] themes_AR;

	// Token: 0x04002753 RID: 10067
	public string[] themes_IT;

	// Token: 0x04002754 RID: 10068
	public string[] themes_RO;

	// Token: 0x04002755 RID: 10069
	public string[] themes_JA;

	// Token: 0x04002756 RID: 10070
	public string[] contractWork_EN;

	// Token: 0x04002757 RID: 10071
	public string[] contractWork_GE;

	// Token: 0x04002758 RID: 10072
	public string[] contractWork_TU;

	// Token: 0x04002759 RID: 10073
	public string[] contractWork_CH;

	// Token: 0x0400275A RID: 10074
	public string[] contractWork_FR;

	// Token: 0x0400275B RID: 10075
	public string[] contractWork_ES;

	// Token: 0x0400275C RID: 10076
	public string[] contractWork_KO;

	// Token: 0x0400275D RID: 10077
	public string[] contractWork_PB;

	// Token: 0x0400275E RID: 10078
	public string[] contractWork_HU;

	// Token: 0x0400275F RID: 10079
	public string[] contractWork_RU;

	// Token: 0x04002760 RID: 10080
	public string[] contractWork_CT;

	// Token: 0x04002761 RID: 10081
	public string[] contractWork_PL;

	// Token: 0x04002762 RID: 10082
	public string[] contractWork_CZ;

	// Token: 0x04002763 RID: 10083
	public string[] contractWork_AR;

	// Token: 0x04002764 RID: 10084
	public string[] contractWork_IT;

	// Token: 0x04002765 RID: 10085
	public string[] contractWork_RO;

	// Token: 0x04002766 RID: 10086
	public string[] contractWork_JA;

	// Token: 0x04002767 RID: 10087
	public string[] fanLetter_EN;

	// Token: 0x04002768 RID: 10088
	public string[] fanLetter_GE;

	// Token: 0x04002769 RID: 10089
	public string[] fanLetter_TU;

	// Token: 0x0400276A RID: 10090
	public string[] fanLetter_CH;

	// Token: 0x0400276B RID: 10091
	public string[] fanLetter_FR;

	// Token: 0x0400276C RID: 10092
	public string[] fanLetter_ES;

	// Token: 0x0400276D RID: 10093
	public string[] fanLetter_KO;

	// Token: 0x0400276E RID: 10094
	public string[] fanLetter_PB;

	// Token: 0x0400276F RID: 10095
	public string[] fanLetter_HU;

	// Token: 0x04002770 RID: 10096
	public string[] fanLetter_RU;

	// Token: 0x04002771 RID: 10097
	public string[] fanLetter_CT;

	// Token: 0x04002772 RID: 10098
	public string[] fanLetter_PL;

	// Token: 0x04002773 RID: 10099
	public string[] fanLetter_CZ;

	// Token: 0x04002774 RID: 10100
	public string[] fanLetter_AR;

	// Token: 0x04002775 RID: 10101
	public string[] fanLetter_IT;

	// Token: 0x04002776 RID: 10102
	public string[] fanLetter_RO;

	// Token: 0x04002777 RID: 10103
	public string[] fanLetter_JA;

	// Token: 0x04002778 RID: 10104
	public string[] tutorial_EN;

	// Token: 0x04002779 RID: 10105
	public string[] tutorial_GE;

	// Token: 0x0400277A RID: 10106
	public string[] tutorial_TU;

	// Token: 0x0400277B RID: 10107
	public string[] tutorial_CH;

	// Token: 0x0400277C RID: 10108
	public string[] tutorial_FR;

	// Token: 0x0400277D RID: 10109
	public string[] tutorial_ES;

	// Token: 0x0400277E RID: 10110
	public string[] tutorial_KO;

	// Token: 0x0400277F RID: 10111
	public string[] tutorial_PB;

	// Token: 0x04002780 RID: 10112
	public string[] tutorial_HU;

	// Token: 0x04002781 RID: 10113
	public string[] tutorial_RU;

	// Token: 0x04002782 RID: 10114
	public string[] tutorial_CT;

	// Token: 0x04002783 RID: 10115
	public string[] tutorial_PL;

	// Token: 0x04002784 RID: 10116
	public string[] tutorial_CZ;

	// Token: 0x04002785 RID: 10117
	public string[] tutorial_AR;

	// Token: 0x04002786 RID: 10118
	public string[] tutorial_IT;

	// Token: 0x04002787 RID: 10119
	public string[] tutorial_RO;

	// Token: 0x04002788 RID: 10120
	public string[] tutorial_JA;

	// Token: 0x04002789 RID: 10121
	private bool textLoaded;
}
