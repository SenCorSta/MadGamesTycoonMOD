using System;
using System.IO;
using System.Text;
using UnityEngine;


public class textScript : MonoBehaviour
{
	
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

	
	public string GetRandomCharName(bool male)
	{
		if (male)
		{
			return (this.namesMale[UnityEngine.Random.Range(0, this.namesMale.Length)] + " " + this.surname[UnityEngine.Random.Range(0, this.surname.Length)]).Replace("\n", string.Empty).Replace("\r", string.Empty).Replace("\t", string.Empty);
		}
		return (this.namesFemale[UnityEngine.Random.Range(0, this.namesFemale.Length)] + " " + this.surname[UnityEngine.Random.Range(0, this.surname.Length)]).Replace("\n", string.Empty).Replace("\r", string.Empty).Replace("\t", string.Empty);
	}

	
	public string GetRandomNPCAddonName()
	{
		return this.npcAddons[UnityEngine.Random.Range(0, this.npcAddons.Length)];
	}

	
	public string GetRandomEngineName()
	{
		return this.randomEngineNames[UnityEngine.Random.Range(0, this.randomEngineNames.Length)];
	}

	
	public string GetRandomGameName()
	{
		return this.randomGameNames[UnityEngine.Random.Range(0, this.randomGameNames.Length)].Replace("\n", string.Empty).Replace("\r", string.Empty).Replace("\t", string.Empty);
	}

	
	public string GetPlatformName()
	{
		return this.randomPlatformNames[UnityEngine.Random.Range(0, this.randomPlatformNames.Length)].Replace("\n", string.Empty).Replace("\r", string.Empty).Replace("\t", string.Empty);
	}

	
	public string GetRandomCompanyName()
	{
		return this.randomCompanyNames[UnityEngine.Random.Range(0, this.randomCompanyNames.Length)].Replace("\n", string.Empty).Replace("\r", string.Empty).Replace("\t", string.Empty);
	}

	
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

	
	private string OpenFile(string filename)
	{
		StreamReader streamReader = new StreamReader(Application.dataPath + "/Extern/Text/" + filename, Encoding.Unicode);
		string result = streamReader.ReadToEnd();
		streamReader.Close();
		return result;
	}

	
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

	
	public void LoadContent_NPCGameNames()
	{
		this.npcGames = this.OpenFile("DATA/npcGames.txt").Split(new char[]
		{
			"\n"[0]
		});
		this.npcGameNameInUse = new bool[this.npcGames.Length];
		this.Reshuffle(this.npcGames);
	}

	
	public void LoadContent_NpcIPs()
	{
		this.npcIPs = this.OpenFile("DATA/npcIPs.txt").Split(new char[]
		{
			"\n"[0]
		});
		this.npcIPsInUse = new bool[this.npcIPs.Length];
	}

	
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

	
	private void LoadCountry_EN(string filename)
	{
		this.country_EN = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	
	private void LoadCountry_TU(string filename)
	{
		this.country_TU = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	
	private void LoadCountry_CH(string filename)
	{
		this.country_CH = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	
	private void LoadCountry_FR(string filename)
	{
		this.country_FR = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	
	private void LoadCountry_ES(string filename)
	{
		this.country_ES = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	
	private void LoadCountry_KO(string filename)
	{
		this.country_KO = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	
	private void LoadCountry_PB(string filename)
	{
		this.country_PB = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	
	private void LoadCountry_HU(string filename)
	{
		this.country_HU = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	
	private void LoadCountry_RU(string filename)
	{
		this.country_RU = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	
	private void LoadCountry_CT(string filename)
	{
		this.country_CT = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	
	private void LoadCountry_PL(string filename)
	{
		this.country_PL = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	
	private void LoadCountry_CZ(string filename)
	{
		this.country_CZ = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	
	private void LoadCountry_AR(string filename)
	{
		this.country_AR = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	
	private void LoadCountry_IT(string filename)
	{
		this.country_IT = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	
	private void LoadCountry_RO(string filename)
	{
		this.country_RO = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	
	private void LoadCountry_JA(string filename)
	{
		this.country_JA = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	
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

	
	private void LoadThemes_EN(string filename)
	{
		this.themes_EN = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	
	private void LoadThemes_TU(string filename)
	{
		this.themes_TU = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	
	private void LoadThemes_CH(string filename)
	{
		this.themes_CH = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	
	private void LoadThemes_FR(string filename)
	{
		this.themes_FR = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	
	private void LoadThemes_ES(string filename)
	{
		this.themes_ES = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	
	private void LoadThemes_KO(string filename)
	{
		this.themes_KO = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	
	private void LoadThemes_PB(string filename)
	{
		this.themes_PB = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	
	private void LoadThemes_HU(string filename)
	{
		this.themes_HU = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	
	private void LoadThemes_RU(string filename)
	{
		this.themes_RU = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	
	private void LoadThemes_CT(string filename)
	{
		this.themes_CT = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	
	private void LoadThemes_PL(string filename)
	{
		this.themes_PL = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	
	private void LoadThemes_CZ(string filename)
	{
		this.themes_CZ = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	
	private void LoadThemes_AR(string filename)
	{
		this.themes_AR = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	
	private void LoadThemes_IT(string filename)
	{
		this.themes_IT = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	
	private void LoadThemes_RO(string filename)
	{
		this.themes_RO = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	
	private void LoadThemes_JA(string filename)
	{
		this.themes_JA = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
	}

	
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

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private settingsScript settings_;

	
	private themes themes_;

	
	private genres genres_;

	
	public string[] namesFemale;

	
	public string[] namesMale;

	
	public string[] surname;

	
	public string[] devLegends;

	
	public string[] randomEngineNames;

	
	public string[] randomGameNames;

	
	public string[] randomPlatformNames;

	
	public string[] randomCompanyNames;

	
	public string credits;

	
	public string[] npcGames;

	
	public bool[] npcGameNameInUse;

	
	public string[] npcAddons;

	
	public string[] npcIPs;

	
	public bool[] npcIPsInUse;

	
	public string[] text_EN;

	
	public string[] text_GE;

	
	public string[] text_TU;

	
	public string[] text_CH;

	
	public string[] text_FR;

	
	public string[] text_ES;

	
	public string[] text_KO;

	
	public string[] text_PB;

	
	public string[] text_HU;

	
	public string[] text_RU;

	
	public string[] text_CT;

	
	public string[] text_PL;

	
	public string[] text_CZ;

	
	public string[] text_AR;

	
	public string[] text_IT;

	
	public string[] text_RO;

	
	public string[] text_JA;

	
	public string[] achivementsName_EN;

	
	public string[] achivementsName_GE;

	
	public string[] achivementsName_TU;

	
	public string[] achivementsName_CH;

	
	public string[] achivementsName_FR;

	
	public string[] achivementsName_ES;

	
	public string[] achivementsName_KO;

	
	public string[] achivementsName_PB;

	
	public string[] achivementsName_HU;

	
	public string[] achivementsName_RU;

	
	public string[] achivementsName_CT;

	
	public string[] achivementsName_PL;

	
	public string[] achivementsName_CZ;

	
	public string[] achivementsName_AR;

	
	public string[] achivementsName_IT;

	
	public string[] achivementsName_RO;

	
	public string[] achivementsName_JA;

	
	public string[] achivementsDesc_EN;

	
	public string[] achivementsDesc_GE;

	
	public string[] achivementsDesc_TU;

	
	public string[] achivementsDesc_CH;

	
	public string[] achivementsDesc_FR;

	
	public string[] achivementsDesc_ES;

	
	public string[] achivementsDesc_KO;

	
	public string[] achivementsDesc_PB;

	
	public string[] achivementsDesc_HU;

	
	public string[] achivementsDesc_RU;

	
	public string[] achivementsDesc_CT;

	
	public string[] achivementsDesc_PL;

	
	public string[] achivementsDesc_CZ;

	
	public string[] achivementsDesc_AR;

	
	public string[] achivementsDesc_IT;

	
	public string[] achivementsDesc_RO;

	
	public string[] achivementsDesc_JA;

	
	public string[] objects_EN;

	
	public string[] objects_GE;

	
	public string[] objects_TU;

	
	public string[] objects_CH;

	
	public string[] objects_FR;

	
	public string[] objects_ES;

	
	public string[] objects_KO;

	
	public string[] objects_PB;

	
	public string[] objects_HU;

	
	public string[] objects_RU;

	
	public string[] objects_CT;

	
	public string[] objects_PL;

	
	public string[] objects_CZ;

	
	public string[] objects_AR;

	
	public string[] objects_IT;

	
	public string[] objects_RO;

	
	public string[] objects_JA;

	
	public string[] objectsTooltip_EN;

	
	public string[] objectsTooltip_GE;

	
	public string[] objectsTooltip_TU;

	
	public string[] objectsTooltip_CH;

	
	public string[] objectsTooltip_FR;

	
	public string[] objectsTooltip_ES;

	
	public string[] objectsTooltip_KO;

	
	public string[] objectsTooltip_PB;

	
	public string[] objectsTooltip_HU;

	
	public string[] objectsTooltip_RU;

	
	public string[] objectsTooltip_CT;

	
	public string[] objectsTooltip_PL;

	
	public string[] objectsTooltip_CZ;

	
	public string[] objectsTooltip_AR;

	
	public string[] objectsTooltip_IT;

	
	public string[] objectsTooltip_RO;

	
	public string[] objectsTooltip_JA;

	
	public string[] country_EN;

	
	public string[] country_GE;

	
	public string[] country_TU;

	
	public string[] country_CH;

	
	public string[] country_FR;

	
	public string[] country_ES;

	
	public string[] country_KO;

	
	public string[] country_PB;

	
	public string[] country_HU;

	
	public string[] country_RU;

	
	public string[] country_CT;

	
	public string[] country_PL;

	
	public string[] country_CZ;

	
	public string[] country_AR;

	
	public string[] country_IT;

	
	public string[] country_RO;

	
	public string[] country_JA;

	
	public string[] quotes_EN;

	
	public string[] quotes_GE;

	
	public string[] quotes_TU;

	
	public string[] quotes_CH;

	
	public string[] quotes_FR;

	
	public string[] quotes_ES;

	
	public string[] quotes_KO;

	
	public string[] quotes_PB;

	
	public string[] quotes_HU;

	
	public string[] quotes_RU;

	
	public string[] quotes_CT;

	
	public string[] quotes_PL;

	
	public string[] quotes_CZ;

	
	public string[] quotes_AR;

	
	public string[] quotes_IT;

	
	public string[] quotes_RO;

	
	public string[] quotes_JA;

	
	public string[] themes_EN;

	
	public string[] themes_GE;

	
	public string[] themes_TU;

	
	public string[] themes_CH;

	
	public string[] themes_FR;

	
	public string[] themes_ES;

	
	public string[] themes_KO;

	
	public string[] themes_PB;

	
	public string[] themes_HU;

	
	public string[] themes_RU;

	
	public string[] themes_CT;

	
	public string[] themes_PL;

	
	public string[] themes_CZ;

	
	public string[] themes_AR;

	
	public string[] themes_IT;

	
	public string[] themes_RO;

	
	public string[] themes_JA;

	
	public string[] contractWork_EN;

	
	public string[] contractWork_GE;

	
	public string[] contractWork_TU;

	
	public string[] contractWork_CH;

	
	public string[] contractWork_FR;

	
	public string[] contractWork_ES;

	
	public string[] contractWork_KO;

	
	public string[] contractWork_PB;

	
	public string[] contractWork_HU;

	
	public string[] contractWork_RU;

	
	public string[] contractWork_CT;

	
	public string[] contractWork_PL;

	
	public string[] contractWork_CZ;

	
	public string[] contractWork_AR;

	
	public string[] contractWork_IT;

	
	public string[] contractWork_RO;

	
	public string[] contractWork_JA;

	
	public string[] fanLetter_EN;

	
	public string[] fanLetter_GE;

	
	public string[] fanLetter_TU;

	
	public string[] fanLetter_CH;

	
	public string[] fanLetter_FR;

	
	public string[] fanLetter_ES;

	
	public string[] fanLetter_KO;

	
	public string[] fanLetter_PB;

	
	public string[] fanLetter_HU;

	
	public string[] fanLetter_RU;

	
	public string[] fanLetter_CT;

	
	public string[] fanLetter_PL;

	
	public string[] fanLetter_CZ;

	
	public string[] fanLetter_AR;

	
	public string[] fanLetter_IT;

	
	public string[] fanLetter_RO;

	
	public string[] fanLetter_JA;

	
	public string[] tutorial_EN;

	
	public string[] tutorial_GE;

	
	public string[] tutorial_TU;

	
	public string[] tutorial_CH;

	
	public string[] tutorial_FR;

	
	public string[] tutorial_ES;

	
	public string[] tutorial_KO;

	
	public string[] tutorial_PB;

	
	public string[] tutorial_HU;

	
	public string[] tutorial_RU;

	
	public string[] tutorial_CT;

	
	public string[] tutorial_PL;

	
	public string[] tutorial_CZ;

	
	public string[] tutorial_AR;

	
	public string[] tutorial_IT;

	
	public string[] tutorial_RO;

	
	public string[] tutorial_JA;

	
	private bool textLoaded;
}
