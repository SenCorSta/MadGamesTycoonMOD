using System;
using System.IO;
using System.Text;
using UnityEngine;

// Token: 0x02000344 RID: 836
public class textScript : MonoBehaviour
{
	// Token: 0x06001E84 RID: 7812 RVA: 0x001423C4 File Offset: 0x001405C4
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

	// Token: 0x06001E85 RID: 7813 RVA: 0x001429C8 File Offset: 0x00140BC8
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

	// Token: 0x06001E86 RID: 7814 RVA: 0x00142A68 File Offset: 0x00140C68
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

	// Token: 0x06001E87 RID: 7815 RVA: 0x00142B68 File Offset: 0x00140D68
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

	// Token: 0x06001E88 RID: 7816 RVA: 0x000145D1 File Offset: 0x000127D1
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

	// Token: 0x06001E89 RID: 7817 RVA: 0x00142C3C File Offset: 0x00140E3C
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

	// Token: 0x06001E8A RID: 7818 RVA: 0x00142CA8 File Offset: 0x00140EA8
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

	// Token: 0x06001E8B RID: 7819 RVA: 0x00142D14 File Offset: 0x00140F14
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

	// Token: 0x06001E8C RID: 7820 RVA: 0x00142F50 File Offset: 0x00141150
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

	// Token: 0x06001E8D RID: 7821 RVA: 0x0014318C File Offset: 0x0014138C
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

	// Token: 0x06001E8E RID: 7822 RVA: 0x001433C8 File Offset: 0x001415C8
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

	// Token: 0x06001E8F RID: 7823 RVA: 0x001435F0 File Offset: 0x001417F0
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

	// Token: 0x06001E90 RID: 7824 RVA: 0x00143818 File Offset: 0x00141A18
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

	// Token: 0x06001E91 RID: 7825 RVA: 0x00143A88 File Offset: 0x00141C88
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

	// Token: 0x06001E92 RID: 7826 RVA: 0x00143C6C File Offset: 0x00141E6C
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

	// Token: 0x06001E93 RID: 7827 RVA: 0x00143E94 File Offset: 0x00142094
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

	// Token: 0x06001E94 RID: 7828 RVA: 0x001440BC File Offset: 0x001422BC
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

	// Token: 0x06001E95 RID: 7829 RVA: 0x001442F4 File Offset: 0x001424F4
	public string GetRandomCharName(bool male)
	{
		if (male)
		{
			return (this.namesMale[UnityEngine.Random.Range(0, this.namesMale.Length)] + " " + this.surname[UnityEngine.Random.Range(0, this.surname.Length)]).Replace("\n", string.Empty).Replace("\r", string.Empty).Replace("\t", string.Empty);
		}
		return (this.namesFemale[UnityEngine.Random.Range(0, this.namesFemale.Length)] + " " + this.surname[UnityEngine.Random.Range(0, this.surname.Length)]).Replace("\n", string.Empty).Replace("\r", string.Empty).Replace("\t", string.Empty);
	}

	// Token: 0x06001E96 RID: 7830 RVA: 0x00014610 File Offset: 0x00012810
	public string GetRandomNPCAddonName()
	{
		return this.npcAddons[UnityEngine.Random.Range(0, this.npcAddons.Length)];
	}

	// Token: 0x06001E97 RID: 7831 RVA: 0x00014627 File Offset: 0x00012827
	public string GetRandomEngineName()
	{
		return this.randomEngineNames[UnityEngine.Random.Range(0, this.randomEngineNames.Length)];
	}

	// Token: 0x06001E98 RID: 7832 RVA: 0x001443C8 File Offset: 0x001425C8
	public string GetRandomGameName()
	{
		return this.randomGameNames[UnityEngine.Random.Range(0, this.randomGameNames.Length)].Replace("\n", string.Empty).Replace("\r", string.Empty).Replace("\t", string.Empty);
	}

	// Token: 0x06001E99 RID: 7833 RVA: 0x00144418 File Offset: 0x00142618
	public string GetPlatformName()
	{
		return this.randomPlatformNames[UnityEngine.Random.Range(0, this.randomPlatformNames.Length)].Replace("\n", string.Empty).Replace("\r", string.Empty).Replace("\t", string.Empty);
	}

	// Token: 0x06001E9A RID: 7834 RVA: 0x00144468 File Offset: 0x00142668
	public string GetRandomCompanyName()
	{
		return this.randomCompanyNames[UnityEngine.Random.Range(0, this.randomCompanyNames.Length)].Replace("\n", string.Empty).Replace("\r", string.Empty).Replace("\t", string.Empty);
	}

	// Token: 0x06001E9B RID: 7835 RVA: 0x001444B8 File Offset: 0x001426B8
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

	// Token: 0x06001E9C RID: 7836 RVA: 0x00144754 File Offset: 0x00142954
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

	// Token: 0x06001E9D RID: 7837 RVA: 0x00144A58 File Offset: 0x00142C58
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

	// Token: 0x06001E9E RID: 7838 RVA: 0x00144B74 File Offset: 0x00142D74
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

	// Token: 0x06001E9F RID: 7839 RVA: 0x00144BC4 File Offset: 0x00142DC4
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

	// Token: 0x06001EA0 RID: 7840 RVA: 0x00144C14 File Offset: 0x00142E14
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

	// Token: 0x06001EA1 RID: 7841 RVA: 0x00144C64 File Offset: 0x00142E64
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

	// Token: 0x06001EA2 RID: 7842 RVA: 0x00144CB4 File Offset: 0x00142EB4
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

	// Token: 0x06001EA3 RID: 7843 RVA: 0x00144CF8 File Offset: 0x00142EF8
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

	// Token: 0x06001EA4 RID: 7844 RVA: 0x00144D44 File Offset: 0x00142F44
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

	// Token: 0x06001EA5 RID: 7845 RVA: 0x00144D88 File Offset: 0x00142F88
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

	// Token: 0x06001EA6 RID: 7846 RVA: 0x000528AC File Offset: 0x00050AAC
	private string OpenFile(string filename)
	{
		StreamReader streamReader = new StreamReader(Application.dataPath + "/Extern/Text/" + filename, Encoding.Unicode);
		string result = streamReader.ReadToEnd();
		streamReader.Close();
		return result;
	}

	// Token: 0x06001EA7 RID: 7847 RVA: 0x00145058 File Offset: 0x00143258
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

	// Token: 0x06001EA8 RID: 7848 RVA: 0x001451C8 File Offset: 0x001433C8
	public void LoadContent_NPCGameNames()
	{
		this.npcGames = this.OpenFile("DATA/npcGames.txt").Split(new char[]
		{
			"\n"[0]
		});
		this.npcGameNameInUse = new bool[this.npcGames.Length];
		this.Reshuffle(this.npcGames);
	}

	// Token: 0x06001EA9 RID: 7849 RVA: 0x0001463E File Offset: 0x0001283E
	public void LoadContent_NpcIPs()
	{
		this.npcIPs = this.OpenFile("DATA/npcIPs.txt").Split(new char[]
		{
			"\n"[0]
		});
		this.npcIPsInUse = new bool[this.npcIPs.Length];
	}

	// Token: 0x06001EAA RID: 7850 RVA: 0x00145220 File Offset: 0x00143420
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

	// Token: 0x06001EAB RID: 7851 RVA: 0x00145254 File Offset: 0x00143454
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

	// Token: 0x06001EAC RID: 7852 RVA: 0x001452C0 File Offset: 0x001434C0
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

	// Token: 0x06001EAD RID: 7853 RVA: 0x0014532C File Offset: 0x0014352C
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

	// Token: 0x06001EAE RID: 7854 RVA: 0x00145398 File Offset: 0x00143598
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

	// Token: 0x06001EAF RID: 7855 RVA: 0x00145404 File Offset: 0x00143604
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

	// Token: 0x06001EB0 RID: 7856 RVA: 0x00145470 File Offset: 0x00143670
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

	// Token: 0x06001EB1 RID: 7857 RVA: 0x001454DC File Offset: 0x001436DC
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

	// Token: 0x06001EB2 RID: 7858 RVA: 0x00145548 File Offset: 0x00143748
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

	// Token: 0x06001EB3 RID: 7859 RVA: 0x001455B4 File Offset: 0x001437B4
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

	// Token: 0x06001EB4 RID: 7860 RVA: 0x00145620 File Offset: 0x00143820
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

	// Token: 0x06001EB5 RID: 7861 RVA: 0x0014568C File Offset: 0x0014388C
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

	// Token: 0x06001EB6 RID: 7862 RVA: 0x001456F8 File Offset: 0x001438F8
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

	// Token: 0x06001EB7 RID: 7863 RVA: 0x00145764 File Offset: 0x00143964
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

	// Token: 0x06001EB8 RID: 7864 RVA: 0x001457D0 File Offset: 0x001439D0
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

	// Token: 0x06001EB9 RID: 7865 RVA: 0x0014583C File Offset: 0x00143A3C
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

	// Token: 0x06001EBA RID: 7866 RVA: 0x001458A8 File Offset: 0x00143AA8
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

	// Token: 0x06001EBB RID: 7867 RVA: 0x00145914 File Offset: 0x00143B14
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

	// Token: 0x06001EBC RID: 7868 RVA: 0x00145980 File Offset: 0x00143B80
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

	// Token: 0x06001EBD RID: 7869 RVA: 0x00145A18 File Offset: 0x00143C18
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

	// Token: 0x06001EBE RID: 7870 RVA: 0x00145AB0 File Offset: 0x00143CB0
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

	// Token: 0x06001EBF RID: 7871 RVA: 0x00145B48 File Offset: 0x00143D48
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

	// Token: 0x06001EC0 RID: 7872 RVA: 0x00145BE0 File Offset: 0x00143DE0
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

	// Token: 0x06001EC1 RID: 7873 RVA: 0x00145C78 File Offset: 0x00143E78
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

	// Token: 0x06001EC2 RID: 7874 RVA: 0x00145D10 File Offset: 0x00143F10
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

	// Token: 0x06001EC3 RID: 7875 RVA: 0x00145DA8 File Offset: 0x00143FA8
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

	// Token: 0x06001EC4 RID: 7876 RVA: 0x00145E40 File Offset: 0x00144040
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

	// Token: 0x06001EC5 RID: 7877 RVA: 0x00145ED8 File Offset: 0x001440D8
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

	// Token: 0x06001EC6 RID: 7878 RVA: 0x00145F70 File Offset: 0x00144170
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

	// Token: 0x06001EC7 RID: 7879 RVA: 0x00146008 File Offset: 0x00144208
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

	// Token: 0x06001EC8 RID: 7880 RVA: 0x001460A0 File Offset: 0x001442A0
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

	// Token: 0x06001EC9 RID: 7881 RVA: 0x00146138 File Offset: 0x00144338
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

	// Token: 0x06001ECA RID: 7882 RVA: 0x001461D0 File Offset: 0x001443D0
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

	// Token: 0x06001ECB RID: 7883 RVA: 0x00146268 File Offset: 0x00144468
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

	// Token: 0x06001ECC RID: 7884 RVA: 0x00146300 File Offset: 0x00144500
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

	// Token: 0x06001ECD RID: 7885 RVA: 0x00146398 File Offset: 0x00144598
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

	// Token: 0x06001ECE RID: 7886 RVA: 0x00146430 File Offset: 0x00144630
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

	// Token: 0x06001ECF RID: 7887 RVA: 0x001464C8 File Offset: 0x001446C8
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

	// Token: 0x06001ED0 RID: 7888 RVA: 0x00146560 File Offset: 0x00144760
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

	// Token: 0x06001ED1 RID: 7889 RVA: 0x001465F8 File Offset: 0x001447F8
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

	// Token: 0x06001ED2 RID: 7890 RVA: 0x00146690 File Offset: 0x00144890
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

	// Token: 0x06001ED3 RID: 7891 RVA: 0x00146728 File Offset: 0x00144928
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

	// Token: 0x06001ED4 RID: 7892 RVA: 0x001467C0 File Offset: 0x001449C0
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

	// Token: 0x06001ED5 RID: 7893 RVA: 0x00146858 File Offset: 0x00144A58
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

	// Token: 0x06001ED6 RID: 7894 RVA: 0x001468F0 File Offset: 0x00144AF0
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

	// Token: 0x06001ED7 RID: 7895 RVA: 0x00146988 File Offset: 0x00144B88
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

	// Token: 0x06001ED8 RID: 7896 RVA: 0x00146A20 File Offset: 0x00144C20
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

	// Token: 0x06001ED9 RID: 7897 RVA: 0x00146AB8 File Offset: 0x00144CB8
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

	// Token: 0x06001EDA RID: 7898 RVA: 0x00146B50 File Offset: 0x00144D50
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

	// Token: 0x06001EDB RID: 7899 RVA: 0x00146BE8 File Offset: 0x00144DE8
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

	// Token: 0x06001EDC RID: 7900 RVA: 0x00146C80 File Offset: 0x00144E80
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

	// Token: 0x06001EDD RID: 7901 RVA: 0x00146D18 File Offset: 0x00144F18
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

	// Token: 0x06001EDE RID: 7902 RVA: 0x00146DB0 File Offset: 0x00144FB0
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

	// Token: 0x06001EDF RID: 7903 RVA: 0x0001467D File Offset: 0x0001287D
	private void LoadCountry_EN(string filename)
	{
		this.country_EN = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001EE0 RID: 7904 RVA: 0x000146A5 File Offset: 0x000128A5
	private void LoadCountry_TU(string filename)
	{
		this.country_TU = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001EE1 RID: 7905 RVA: 0x000146CD File Offset: 0x000128CD
	private void LoadCountry_CH(string filename)
	{
		this.country_CH = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001EE2 RID: 7906 RVA: 0x000146F5 File Offset: 0x000128F5
	private void LoadCountry_FR(string filename)
	{
		this.country_FR = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001EE3 RID: 7907 RVA: 0x0001471D File Offset: 0x0001291D
	private void LoadCountry_ES(string filename)
	{
		this.country_ES = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001EE4 RID: 7908 RVA: 0x00014745 File Offset: 0x00012945
	private void LoadCountry_KO(string filename)
	{
		this.country_KO = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001EE5 RID: 7909 RVA: 0x0001476D File Offset: 0x0001296D
	private void LoadCountry_PB(string filename)
	{
		this.country_PB = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001EE6 RID: 7910 RVA: 0x00014795 File Offset: 0x00012995
	private void LoadCountry_HU(string filename)
	{
		this.country_HU = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001EE7 RID: 7911 RVA: 0x000147BD File Offset: 0x000129BD
	private void LoadCountry_RU(string filename)
	{
		this.country_RU = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001EE8 RID: 7912 RVA: 0x000147E5 File Offset: 0x000129E5
	private void LoadCountry_CT(string filename)
	{
		this.country_CT = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001EE9 RID: 7913 RVA: 0x0001480D File Offset: 0x00012A0D
	private void LoadCountry_PL(string filename)
	{
		this.country_PL = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001EEA RID: 7914 RVA: 0x00014835 File Offset: 0x00012A35
	private void LoadCountry_CZ(string filename)
	{
		this.country_CZ = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001EEB RID: 7915 RVA: 0x0001485D File Offset: 0x00012A5D
	private void LoadCountry_AR(string filename)
	{
		this.country_AR = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001EEC RID: 7916 RVA: 0x00014885 File Offset: 0x00012A85
	private void LoadCountry_IT(string filename)
	{
		this.country_IT = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001EED RID: 7917 RVA: 0x000148AD File Offset: 0x00012AAD
	private void LoadCountry_RO(string filename)
	{
		this.country_RO = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001EEE RID: 7918 RVA: 0x000148D5 File Offset: 0x00012AD5
	private void LoadCountry_JA(string filename)
	{
		this.country_JA = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001EEF RID: 7919 RVA: 0x00146E6C File Offset: 0x0014506C
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

	// Token: 0x06001EF0 RID: 7920 RVA: 0x00146ED8 File Offset: 0x001450D8
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

	// Token: 0x06001EF1 RID: 7921 RVA: 0x00146F44 File Offset: 0x00145144
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

	// Token: 0x06001EF2 RID: 7922 RVA: 0x00146FB0 File Offset: 0x001451B0
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

	// Token: 0x06001EF3 RID: 7923 RVA: 0x0014701C File Offset: 0x0014521C
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

	// Token: 0x06001EF4 RID: 7924 RVA: 0x00147088 File Offset: 0x00145288
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

	// Token: 0x06001EF5 RID: 7925 RVA: 0x001470F4 File Offset: 0x001452F4
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

	// Token: 0x06001EF6 RID: 7926 RVA: 0x00147160 File Offset: 0x00145360
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

	// Token: 0x06001EF7 RID: 7927 RVA: 0x001471CC File Offset: 0x001453CC
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

	// Token: 0x06001EF8 RID: 7928 RVA: 0x00147238 File Offset: 0x00145438
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

	// Token: 0x06001EF9 RID: 7929 RVA: 0x001472A4 File Offset: 0x001454A4
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

	// Token: 0x06001EFA RID: 7930 RVA: 0x00147310 File Offset: 0x00145510
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

	// Token: 0x06001EFB RID: 7931 RVA: 0x0014737C File Offset: 0x0014557C
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

	// Token: 0x06001EFC RID: 7932 RVA: 0x001473E8 File Offset: 0x001455E8
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

	// Token: 0x06001EFD RID: 7933 RVA: 0x00147454 File Offset: 0x00145654
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

	// Token: 0x06001EFE RID: 7934 RVA: 0x001474C0 File Offset: 0x001456C0
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

	// Token: 0x06001EFF RID: 7935 RVA: 0x0014752C File Offset: 0x0014572C
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

	// Token: 0x06001F00 RID: 7936 RVA: 0x00147598 File Offset: 0x00145798
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

	// Token: 0x06001F01 RID: 7937 RVA: 0x00147604 File Offset: 0x00145804
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

	// Token: 0x06001F02 RID: 7938 RVA: 0x00147670 File Offset: 0x00145870
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

	// Token: 0x06001F03 RID: 7939 RVA: 0x001476DC File Offset: 0x001458DC
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

	// Token: 0x06001F04 RID: 7940 RVA: 0x00147748 File Offset: 0x00145948
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

	// Token: 0x06001F05 RID: 7941 RVA: 0x001477B4 File Offset: 0x001459B4
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

	// Token: 0x06001F06 RID: 7942 RVA: 0x00147820 File Offset: 0x00145A20
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

	// Token: 0x06001F07 RID: 7943 RVA: 0x0014788C File Offset: 0x00145A8C
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

	// Token: 0x06001F08 RID: 7944 RVA: 0x001478F8 File Offset: 0x00145AF8
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

	// Token: 0x06001F09 RID: 7945 RVA: 0x00147964 File Offset: 0x00145B64
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

	// Token: 0x06001F0A RID: 7946 RVA: 0x001479D0 File Offset: 0x00145BD0
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

	// Token: 0x06001F0B RID: 7947 RVA: 0x00147A3C File Offset: 0x00145C3C
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

	// Token: 0x06001F0C RID: 7948 RVA: 0x00147AA8 File Offset: 0x00145CA8
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

	// Token: 0x06001F0D RID: 7949 RVA: 0x00147B14 File Offset: 0x00145D14
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

	// Token: 0x06001F0E RID: 7950 RVA: 0x00147B80 File Offset: 0x00145D80
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

	// Token: 0x06001F0F RID: 7951 RVA: 0x00147BEC File Offset: 0x00145DEC
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

	// Token: 0x06001F10 RID: 7952 RVA: 0x00147C58 File Offset: 0x00145E58
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

	// Token: 0x06001F11 RID: 7953 RVA: 0x00147CC4 File Offset: 0x00145EC4
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

	// Token: 0x06001F12 RID: 7954 RVA: 0x00147D30 File Offset: 0x00145F30
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

	// Token: 0x06001F13 RID: 7955 RVA: 0x00147D9C File Offset: 0x00145F9C
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

	// Token: 0x06001F14 RID: 7956 RVA: 0x00147E08 File Offset: 0x00146008
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

	// Token: 0x06001F15 RID: 7957 RVA: 0x00147E74 File Offset: 0x00146074
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

	// Token: 0x06001F16 RID: 7958 RVA: 0x00147EE0 File Offset: 0x001460E0
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

	// Token: 0x06001F17 RID: 7959 RVA: 0x00147F4C File Offset: 0x0014614C
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

	// Token: 0x06001F18 RID: 7960 RVA: 0x00147FB8 File Offset: 0x001461B8
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

	// Token: 0x06001F19 RID: 7961 RVA: 0x00148024 File Offset: 0x00146224
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

	// Token: 0x06001F1A RID: 7962 RVA: 0x00148090 File Offset: 0x00146290
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

	// Token: 0x06001F1B RID: 7963 RVA: 0x001480FC File Offset: 0x001462FC
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

	// Token: 0x06001F1C RID: 7964 RVA: 0x00148168 File Offset: 0x00146368
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

	// Token: 0x06001F1D RID: 7965 RVA: 0x001481D4 File Offset: 0x001463D4
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

	// Token: 0x06001F1E RID: 7966 RVA: 0x00148240 File Offset: 0x00146440
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

	// Token: 0x06001F1F RID: 7967 RVA: 0x001482AC File Offset: 0x001464AC
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

	// Token: 0x06001F20 RID: 7968 RVA: 0x00148318 File Offset: 0x00146518
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

	// Token: 0x06001F21 RID: 7969 RVA: 0x00148384 File Offset: 0x00146584
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

	// Token: 0x06001F22 RID: 7970 RVA: 0x001483F0 File Offset: 0x001465F0
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

	// Token: 0x06001F23 RID: 7971 RVA: 0x0014845C File Offset: 0x0014665C
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

	// Token: 0x06001F24 RID: 7972 RVA: 0x001484C8 File Offset: 0x001466C8
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

	// Token: 0x06001F25 RID: 7973 RVA: 0x00148534 File Offset: 0x00146734
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

	// Token: 0x06001F26 RID: 7974 RVA: 0x001485A0 File Offset: 0x001467A0
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

	// Token: 0x06001F27 RID: 7975 RVA: 0x0014860C File Offset: 0x0014680C
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

	// Token: 0x06001F28 RID: 7976 RVA: 0x00148678 File Offset: 0x00146878
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

	// Token: 0x06001F29 RID: 7977 RVA: 0x001486E4 File Offset: 0x001468E4
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

	// Token: 0x06001F2A RID: 7978 RVA: 0x00148750 File Offset: 0x00146950
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

	// Token: 0x06001F2B RID: 7979 RVA: 0x001487BC File Offset: 0x001469BC
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

	// Token: 0x06001F2C RID: 7980 RVA: 0x00148828 File Offset: 0x00146A28
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

	// Token: 0x06001F2D RID: 7981 RVA: 0x00148894 File Offset: 0x00146A94
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

	// Token: 0x06001F2E RID: 7982 RVA: 0x00148900 File Offset: 0x00146B00
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

	// Token: 0x06001F2F RID: 7983 RVA: 0x0014896C File Offset: 0x00146B6C
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

	// Token: 0x06001F30 RID: 7984 RVA: 0x001489D8 File Offset: 0x00146BD8
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

	// Token: 0x06001F31 RID: 7985 RVA: 0x00148A44 File Offset: 0x00146C44
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

	// Token: 0x06001F32 RID: 7986 RVA: 0x00148AB0 File Offset: 0x00146CB0
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

	// Token: 0x06001F33 RID: 7987 RVA: 0x00148B1C File Offset: 0x00146D1C
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

	// Token: 0x06001F34 RID: 7988 RVA: 0x00148BF4 File Offset: 0x00146DF4
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

	// Token: 0x06001F35 RID: 7989 RVA: 0x000148FD File Offset: 0x00012AFD
	private void LoadThemes_EN(string filename)
	{
		this.themes_EN = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F36 RID: 7990 RVA: 0x00014925 File Offset: 0x00012B25
	private void LoadThemes_TU(string filename)
	{
		this.themes_TU = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F37 RID: 7991 RVA: 0x0001494D File Offset: 0x00012B4D
	private void LoadThemes_CH(string filename)
	{
		this.themes_CH = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F38 RID: 7992 RVA: 0x00014975 File Offset: 0x00012B75
	private void LoadThemes_FR(string filename)
	{
		this.themes_FR = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F39 RID: 7993 RVA: 0x0001499D File Offset: 0x00012B9D
	private void LoadThemes_ES(string filename)
	{
		this.themes_ES = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F3A RID: 7994 RVA: 0x000149C5 File Offset: 0x00012BC5
	private void LoadThemes_KO(string filename)
	{
		this.themes_KO = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F3B RID: 7995 RVA: 0x000149ED File Offset: 0x00012BED
	private void LoadThemes_PB(string filename)
	{
		this.themes_PB = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F3C RID: 7996 RVA: 0x00014A15 File Offset: 0x00012C15
	private void LoadThemes_HU(string filename)
	{
		this.themes_HU = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F3D RID: 7997 RVA: 0x00014A3D File Offset: 0x00012C3D
	private void LoadThemes_RU(string filename)
	{
		this.themes_RU = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F3E RID: 7998 RVA: 0x00014A65 File Offset: 0x00012C65
	private void LoadThemes_CT(string filename)
	{
		this.themes_CT = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F3F RID: 7999 RVA: 0x00014A8D File Offset: 0x00012C8D
	private void LoadThemes_PL(string filename)
	{
		this.themes_PL = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F40 RID: 8000 RVA: 0x00014AB5 File Offset: 0x00012CB5
	private void LoadThemes_CZ(string filename)
	{
		this.themes_CZ = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F41 RID: 8001 RVA: 0x00014ADD File Offset: 0x00012CDD
	private void LoadThemes_AR(string filename)
	{
		this.themes_AR = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F42 RID: 8002 RVA: 0x00014B05 File Offset: 0x00012D05
	private void LoadThemes_IT(string filename)
	{
		this.themes_IT = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F43 RID: 8003 RVA: 0x00014B2D File Offset: 0x00012D2D
	private void LoadThemes_RO(string filename)
	{
		this.themes_RO = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F44 RID: 8004 RVA: 0x00014B55 File Offset: 0x00012D55
	private void LoadThemes_JA(string filename)
	{
		this.themes_JA = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	// Token: 0x06001F45 RID: 8005 RVA: 0x00148C58 File Offset: 0x00146E58
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

	// Token: 0x06001F46 RID: 8006 RVA: 0x001490A4 File Offset: 0x001472A4
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

	// Token: 0x06001F47 RID: 8007 RVA: 0x00149188 File Offset: 0x00147388
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

	// Token: 0x040026A5 RID: 9893
	private GameObject main_;

	// Token: 0x040026A6 RID: 9894
	private mainScript mS_;

	// Token: 0x040026A7 RID: 9895
	private settingsScript settings_;

	// Token: 0x040026A8 RID: 9896
	private themes themes_;

	// Token: 0x040026A9 RID: 9897
	private genres genres_;

	// Token: 0x040026AA RID: 9898
	public string[] namesFemale;

	// Token: 0x040026AB RID: 9899
	public string[] namesMale;

	// Token: 0x040026AC RID: 9900
	public string[] surname;

	// Token: 0x040026AD RID: 9901
	public string[] devLegends;

	// Token: 0x040026AE RID: 9902
	public string[] randomEngineNames;

	// Token: 0x040026AF RID: 9903
	public string[] randomGameNames;

	// Token: 0x040026B0 RID: 9904
	public string[] randomPlatformNames;

	// Token: 0x040026B1 RID: 9905
	public string[] randomCompanyNames;

	// Token: 0x040026B2 RID: 9906
	public string credits;

	// Token: 0x040026B3 RID: 9907
	public string[] npcGames;

	// Token: 0x040026B4 RID: 9908
	public bool[] npcGameNameInUse;

	// Token: 0x040026B5 RID: 9909
	public string[] npcAddons;

	// Token: 0x040026B6 RID: 9910
	public string[] npcIPs;

	// Token: 0x040026B7 RID: 9911
	public bool[] npcIPsInUse;

	// Token: 0x040026B8 RID: 9912
	public string[] text_EN;

	// Token: 0x040026B9 RID: 9913
	public string[] text_GE;

	// Token: 0x040026BA RID: 9914
	public string[] text_TU;

	// Token: 0x040026BB RID: 9915
	public string[] text_CH;

	// Token: 0x040026BC RID: 9916
	public string[] text_FR;

	// Token: 0x040026BD RID: 9917
	public string[] text_ES;

	// Token: 0x040026BE RID: 9918
	public string[] text_KO;

	// Token: 0x040026BF RID: 9919
	public string[] text_PB;

	// Token: 0x040026C0 RID: 9920
	public string[] text_HU;

	// Token: 0x040026C1 RID: 9921
	public string[] text_RU;

	// Token: 0x040026C2 RID: 9922
	public string[] text_CT;

	// Token: 0x040026C3 RID: 9923
	public string[] text_PL;

	// Token: 0x040026C4 RID: 9924
	public string[] text_CZ;

	// Token: 0x040026C5 RID: 9925
	public string[] text_AR;

	// Token: 0x040026C6 RID: 9926
	public string[] text_IT;

	// Token: 0x040026C7 RID: 9927
	public string[] text_RO;

	// Token: 0x040026C8 RID: 9928
	public string[] text_JA;

	// Token: 0x040026C9 RID: 9929
	public string[] achivementsName_EN;

	// Token: 0x040026CA RID: 9930
	public string[] achivementsName_GE;

	// Token: 0x040026CB RID: 9931
	public string[] achivementsName_TU;

	// Token: 0x040026CC RID: 9932
	public string[] achivementsName_CH;

	// Token: 0x040026CD RID: 9933
	public string[] achivementsName_FR;

	// Token: 0x040026CE RID: 9934
	public string[] achivementsName_ES;

	// Token: 0x040026CF RID: 9935
	public string[] achivementsName_KO;

	// Token: 0x040026D0 RID: 9936
	public string[] achivementsName_PB;

	// Token: 0x040026D1 RID: 9937
	public string[] achivementsName_HU;

	// Token: 0x040026D2 RID: 9938
	public string[] achivementsName_RU;

	// Token: 0x040026D3 RID: 9939
	public string[] achivementsName_CT;

	// Token: 0x040026D4 RID: 9940
	public string[] achivementsName_PL;

	// Token: 0x040026D5 RID: 9941
	public string[] achivementsName_CZ;

	// Token: 0x040026D6 RID: 9942
	public string[] achivementsName_AR;

	// Token: 0x040026D7 RID: 9943
	public string[] achivementsName_IT;

	// Token: 0x040026D8 RID: 9944
	public string[] achivementsName_RO;

	// Token: 0x040026D9 RID: 9945
	public string[] achivementsName_JA;

	// Token: 0x040026DA RID: 9946
	public string[] achivementsDesc_EN;

	// Token: 0x040026DB RID: 9947
	public string[] achivementsDesc_GE;

	// Token: 0x040026DC RID: 9948
	public string[] achivementsDesc_TU;

	// Token: 0x040026DD RID: 9949
	public string[] achivementsDesc_CH;

	// Token: 0x040026DE RID: 9950
	public string[] achivementsDesc_FR;

	// Token: 0x040026DF RID: 9951
	public string[] achivementsDesc_ES;

	// Token: 0x040026E0 RID: 9952
	public string[] achivementsDesc_KO;

	// Token: 0x040026E1 RID: 9953
	public string[] achivementsDesc_PB;

	// Token: 0x040026E2 RID: 9954
	public string[] achivementsDesc_HU;

	// Token: 0x040026E3 RID: 9955
	public string[] achivementsDesc_RU;

	// Token: 0x040026E4 RID: 9956
	public string[] achivementsDesc_CT;

	// Token: 0x040026E5 RID: 9957
	public string[] achivementsDesc_PL;

	// Token: 0x040026E6 RID: 9958
	public string[] achivementsDesc_CZ;

	// Token: 0x040026E7 RID: 9959
	public string[] achivementsDesc_AR;

	// Token: 0x040026E8 RID: 9960
	public string[] achivementsDesc_IT;

	// Token: 0x040026E9 RID: 9961
	public string[] achivementsDesc_RO;

	// Token: 0x040026EA RID: 9962
	public string[] achivementsDesc_JA;

	// Token: 0x040026EB RID: 9963
	public string[] objects_EN;

	// Token: 0x040026EC RID: 9964
	public string[] objects_GE;

	// Token: 0x040026ED RID: 9965
	public string[] objects_TU;

	// Token: 0x040026EE RID: 9966
	public string[] objects_CH;

	// Token: 0x040026EF RID: 9967
	public string[] objects_FR;

	// Token: 0x040026F0 RID: 9968
	public string[] objects_ES;

	// Token: 0x040026F1 RID: 9969
	public string[] objects_KO;

	// Token: 0x040026F2 RID: 9970
	public string[] objects_PB;

	// Token: 0x040026F3 RID: 9971
	public string[] objects_HU;

	// Token: 0x040026F4 RID: 9972
	public string[] objects_RU;

	// Token: 0x040026F5 RID: 9973
	public string[] objects_CT;

	// Token: 0x040026F6 RID: 9974
	public string[] objects_PL;

	// Token: 0x040026F7 RID: 9975
	public string[] objects_CZ;

	// Token: 0x040026F8 RID: 9976
	public string[] objects_AR;

	// Token: 0x040026F9 RID: 9977
	public string[] objects_IT;

	// Token: 0x040026FA RID: 9978
	public string[] objects_RO;

	// Token: 0x040026FB RID: 9979
	public string[] objects_JA;

	// Token: 0x040026FC RID: 9980
	public string[] objectsTooltip_EN;

	// Token: 0x040026FD RID: 9981
	public string[] objectsTooltip_GE;

	// Token: 0x040026FE RID: 9982
	public string[] objectsTooltip_TU;

	// Token: 0x040026FF RID: 9983
	public string[] objectsTooltip_CH;

	// Token: 0x04002700 RID: 9984
	public string[] objectsTooltip_FR;

	// Token: 0x04002701 RID: 9985
	public string[] objectsTooltip_ES;

	// Token: 0x04002702 RID: 9986
	public string[] objectsTooltip_KO;

	// Token: 0x04002703 RID: 9987
	public string[] objectsTooltip_PB;

	// Token: 0x04002704 RID: 9988
	public string[] objectsTooltip_HU;

	// Token: 0x04002705 RID: 9989
	public string[] objectsTooltip_RU;

	// Token: 0x04002706 RID: 9990
	public string[] objectsTooltip_CT;

	// Token: 0x04002707 RID: 9991
	public string[] objectsTooltip_PL;

	// Token: 0x04002708 RID: 9992
	public string[] objectsTooltip_CZ;

	// Token: 0x04002709 RID: 9993
	public string[] objectsTooltip_AR;

	// Token: 0x0400270A RID: 9994
	public string[] objectsTooltip_IT;

	// Token: 0x0400270B RID: 9995
	public string[] objectsTooltip_RO;

	// Token: 0x0400270C RID: 9996
	public string[] objectsTooltip_JA;

	// Token: 0x0400270D RID: 9997
	public string[] country_EN;

	// Token: 0x0400270E RID: 9998
	public string[] country_GE;

	// Token: 0x0400270F RID: 9999
	public string[] country_TU;

	// Token: 0x04002710 RID: 10000
	public string[] country_CH;

	// Token: 0x04002711 RID: 10001
	public string[] country_FR;

	// Token: 0x04002712 RID: 10002
	public string[] country_ES;

	// Token: 0x04002713 RID: 10003
	public string[] country_KO;

	// Token: 0x04002714 RID: 10004
	public string[] country_PB;

	// Token: 0x04002715 RID: 10005
	public string[] country_HU;

	// Token: 0x04002716 RID: 10006
	public string[] country_RU;

	// Token: 0x04002717 RID: 10007
	public string[] country_CT;

	// Token: 0x04002718 RID: 10008
	public string[] country_PL;

	// Token: 0x04002719 RID: 10009
	public string[] country_CZ;

	// Token: 0x0400271A RID: 10010
	public string[] country_AR;

	// Token: 0x0400271B RID: 10011
	public string[] country_IT;

	// Token: 0x0400271C RID: 10012
	public string[] country_RO;

	// Token: 0x0400271D RID: 10013
	public string[] country_JA;

	// Token: 0x0400271E RID: 10014
	public string[] quotes_EN;

	// Token: 0x0400271F RID: 10015
	public string[] quotes_GE;

	// Token: 0x04002720 RID: 10016
	public string[] quotes_TU;

	// Token: 0x04002721 RID: 10017
	public string[] quotes_CH;

	// Token: 0x04002722 RID: 10018
	public string[] quotes_FR;

	// Token: 0x04002723 RID: 10019
	public string[] quotes_ES;

	// Token: 0x04002724 RID: 10020
	public string[] quotes_KO;

	// Token: 0x04002725 RID: 10021
	public string[] quotes_PB;

	// Token: 0x04002726 RID: 10022
	public string[] quotes_HU;

	// Token: 0x04002727 RID: 10023
	public string[] quotes_RU;

	// Token: 0x04002728 RID: 10024
	public string[] quotes_CT;

	// Token: 0x04002729 RID: 10025
	public string[] quotes_PL;

	// Token: 0x0400272A RID: 10026
	public string[] quotes_CZ;

	// Token: 0x0400272B RID: 10027
	public string[] quotes_AR;

	// Token: 0x0400272C RID: 10028
	public string[] quotes_IT;

	// Token: 0x0400272D RID: 10029
	public string[] quotes_RO;

	// Token: 0x0400272E RID: 10030
	public string[] quotes_JA;

	// Token: 0x0400272F RID: 10031
	public string[] themes_EN;

	// Token: 0x04002730 RID: 10032
	public string[] themes_GE;

	// Token: 0x04002731 RID: 10033
	public string[] themes_TU;

	// Token: 0x04002732 RID: 10034
	public string[] themes_CH;

	// Token: 0x04002733 RID: 10035
	public string[] themes_FR;

	// Token: 0x04002734 RID: 10036
	public string[] themes_ES;

	// Token: 0x04002735 RID: 10037
	public string[] themes_KO;

	// Token: 0x04002736 RID: 10038
	public string[] themes_PB;

	// Token: 0x04002737 RID: 10039
	public string[] themes_HU;

	// Token: 0x04002738 RID: 10040
	public string[] themes_RU;

	// Token: 0x04002739 RID: 10041
	public string[] themes_CT;

	// Token: 0x0400273A RID: 10042
	public string[] themes_PL;

	// Token: 0x0400273B RID: 10043
	public string[] themes_CZ;

	// Token: 0x0400273C RID: 10044
	public string[] themes_AR;

	// Token: 0x0400273D RID: 10045
	public string[] themes_IT;

	// Token: 0x0400273E RID: 10046
	public string[] themes_RO;

	// Token: 0x0400273F RID: 10047
	public string[] themes_JA;

	// Token: 0x04002740 RID: 10048
	public string[] contractWork_EN;

	// Token: 0x04002741 RID: 10049
	public string[] contractWork_GE;

	// Token: 0x04002742 RID: 10050
	public string[] contractWork_TU;

	// Token: 0x04002743 RID: 10051
	public string[] contractWork_CH;

	// Token: 0x04002744 RID: 10052
	public string[] contractWork_FR;

	// Token: 0x04002745 RID: 10053
	public string[] contractWork_ES;

	// Token: 0x04002746 RID: 10054
	public string[] contractWork_KO;

	// Token: 0x04002747 RID: 10055
	public string[] contractWork_PB;

	// Token: 0x04002748 RID: 10056
	public string[] contractWork_HU;

	// Token: 0x04002749 RID: 10057
	public string[] contractWork_RU;

	// Token: 0x0400274A RID: 10058
	public string[] contractWork_CT;

	// Token: 0x0400274B RID: 10059
	public string[] contractWork_PL;

	// Token: 0x0400274C RID: 10060
	public string[] contractWork_CZ;

	// Token: 0x0400274D RID: 10061
	public string[] contractWork_AR;

	// Token: 0x0400274E RID: 10062
	public string[] contractWork_IT;

	// Token: 0x0400274F RID: 10063
	public string[] contractWork_RO;

	// Token: 0x04002750 RID: 10064
	public string[] contractWork_JA;

	// Token: 0x04002751 RID: 10065
	public string[] fanLetter_EN;

	// Token: 0x04002752 RID: 10066
	public string[] fanLetter_GE;

	// Token: 0x04002753 RID: 10067
	public string[] fanLetter_TU;

	// Token: 0x04002754 RID: 10068
	public string[] fanLetter_CH;

	// Token: 0x04002755 RID: 10069
	public string[] fanLetter_FR;

	// Token: 0x04002756 RID: 10070
	public string[] fanLetter_ES;

	// Token: 0x04002757 RID: 10071
	public string[] fanLetter_KO;

	// Token: 0x04002758 RID: 10072
	public string[] fanLetter_PB;

	// Token: 0x04002759 RID: 10073
	public string[] fanLetter_HU;

	// Token: 0x0400275A RID: 10074
	public string[] fanLetter_RU;

	// Token: 0x0400275B RID: 10075
	public string[] fanLetter_CT;

	// Token: 0x0400275C RID: 10076
	public string[] fanLetter_PL;

	// Token: 0x0400275D RID: 10077
	public string[] fanLetter_CZ;

	// Token: 0x0400275E RID: 10078
	public string[] fanLetter_AR;

	// Token: 0x0400275F RID: 10079
	public string[] fanLetter_IT;

	// Token: 0x04002760 RID: 10080
	public string[] fanLetter_RO;

	// Token: 0x04002761 RID: 10081
	public string[] fanLetter_JA;

	// Token: 0x04002762 RID: 10082
	public string[] tutorial_EN;

	// Token: 0x04002763 RID: 10083
	public string[] tutorial_GE;

	// Token: 0x04002764 RID: 10084
	public string[] tutorial_TU;

	// Token: 0x04002765 RID: 10085
	public string[] tutorial_CH;

	// Token: 0x04002766 RID: 10086
	public string[] tutorial_FR;

	// Token: 0x04002767 RID: 10087
	public string[] tutorial_ES;

	// Token: 0x04002768 RID: 10088
	public string[] tutorial_KO;

	// Token: 0x04002769 RID: 10089
	public string[] tutorial_PB;

	// Token: 0x0400276A RID: 10090
	public string[] tutorial_HU;

	// Token: 0x0400276B RID: 10091
	public string[] tutorial_RU;

	// Token: 0x0400276C RID: 10092
	public string[] tutorial_CT;

	// Token: 0x0400276D RID: 10093
	public string[] tutorial_PL;

	// Token: 0x0400276E RID: 10094
	public string[] tutorial_CZ;

	// Token: 0x0400276F RID: 10095
	public string[] tutorial_AR;

	// Token: 0x04002770 RID: 10096
	public string[] tutorial_IT;

	// Token: 0x04002771 RID: 10097
	public string[] tutorial_RO;

	// Token: 0x04002772 RID: 10098
	public string[] tutorial_JA;

	// Token: 0x04002773 RID: 10099
	private bool textLoaded;
}
