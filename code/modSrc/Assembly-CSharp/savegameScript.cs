using System;
using System.Collections;
using UnityEngine;


public class savegameScript : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = base.gameObject;
		}
		if (!this.mS_)
		{
			this.mS_ = base.GetComponent<mainScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = base.GetComponent<textScript>();
		}
		if (!this.mapS_)
		{
			this.mapS_ = base.GetComponent<mapScript>();
		}
		if (!this.settings_)
		{
			this.settings_ = base.GetComponent<settingsScript>();
		}
		if (!this.brS_)
		{
			this.brS_ = base.GetComponent<buildRoomScript>();
		}
		if (!this.games_)
		{
			this.games_ = base.GetComponent<games>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.genres_)
		{
			this.genres_ = base.GetComponent<genres>();
		}
		if (!this.themes_)
		{
			this.themes_ = base.GetComponent<themes>();
		}
		if (!this.licences_)
		{
			this.licences_ = base.GetComponent<licences>();
		}
		if (!this.eF_)
		{
			this.eF_ = base.GetComponent<engineFeatures>();
		}
		if (!this.cmS_)
		{
			this.cmS_ = GameObject.Find("CamMovement").GetComponent<cameraMovementScript>();
		}
		if (!this.unlock_)
		{
			this.unlock_ = base.GetComponent<unlockScript>();
		}
		if (!this.gF_)
		{
			this.gF_ = base.GetComponent<gameplayFeatures>();
		}
		if (!this.publisher_)
		{
			this.publisher_ = base.GetComponent<publisher>();
		}
		if (!this.hardware_)
		{
			this.hardware_ = base.GetComponent<hardware>();
		}
		if (!this.hardwareFeatures_)
		{
			this.hardwareFeatures_ = base.GetComponent<hardwareFeatures>();
		}
		if (!this.platforms_)
		{
			this.platforms_ = base.GetComponent<platforms>();
		}
		if (!this.copyProtect_)
		{
			this.copyProtect_ = base.GetComponent<copyProtect>();
		}
		if (!this.antiCheat_)
		{
			this.antiCheat_ = base.GetComponent<anitCheat>();
		}
		if (!this.arbeitsmarkt_)
		{
			this.arbeitsmarkt_ = base.GetComponent<arbeitsmarkt>();
		}
		if (!this.cCS_)
		{
			this.cCS_ = base.GetComponent<createCharScript>();
		}
		if (!this.fS_)
		{
			this.fS_ = base.GetComponent<forschungSonstiges>();
		}
		if (!this.menuPackung_)
		{
			this.menuPackung_ = this.guiMain_.uiObjects[218].GetComponent<Menu_Packung>();
		}
		if (!this.menuArcadePreis_)
		{
			this.menuArcadePreis_ = this.guiMain_.uiObjects[307].GetComponent<Menu_ArcadePreis>();
		}
		if (!this.menu_BuyInventar_)
		{
			this.menu_BuyInventar_ = this.guiMain_.uiObjects[20].GetComponent<Menu_BuyInventar>();
		}
		if (!this.mpMain_)
		{
			this.mpMain_ = base.GetComponent<mpMain>();
		}
		if (!this.contractWorkMain_)
		{
			this.contractWorkMain_ = base.GetComponent<contractWorkMain>();
		}
		if (!this.publishingOfferMain_)
		{
			this.publishingOfferMain_ = base.GetComponent<publishingOfferMain>();
		}
		if (!this.mpCalls_)
		{
			this.mpCalls_ = GameObject.Find("NetworkManager").GetComponent<mpCalls>();
		}
	}

	
	public void SaveMultiplayer(int i)
	{
		base.StartCoroutine(this.SaveMultiplayerDelay(i));
	}

	
	public IEnumerator SaveMultiplayerDelay(int i)
	{
		if (this.mS_.mpCalls_.isServer)
		{
			this.mS_.SetGameSpeed(0f);
			this.mS_.mpCalls_.SERVER_Send_GameSpeed(Mathf.RoundToInt(0f));
			this.mS_.mpCalls_.SERVER_Send_Command(5);
			this.mS_.mpCalls_.SetPlayersUnready();
			this.guiMain_.uiObjects[202].SetActive(true);
			yield return new WaitForSeconds(10f);
		}
		else
		{
			yield return new WaitForEndOfFrame();
			yield return new WaitForEndOfFrame();
			yield return new WaitForEndOfFrame();
		}
		this.Save(i);
		yield break;
	}

	
	public void Save(int i)
	{
		if (this.mS_.multiplayer)
		{
			if (this.mS_.mpCalls_.isServer)
			{
				this.mS_.SetRandomMultiplayerSaveID();
				this.mS_.mpCalls_.SERVER_Send_Save(this.mS_.multiplayerSaveID);
			}
		}
		else
		{
			PlayerPrefs.SetInt("ResumeGame", i);
		}
		string filePath = this.mS_.GetSavegameTitle() + i.ToString() + ".txt";
		ES3Settings settings = new ES3Settings();
		ES3.DeleteFile(filePath);
		this.writer = ES3Writer.Create(filePath, settings);
		this.SaveGlobals(this.writer);
		this.SaveNPCGameNames(this.writer);
		this.SaveLicences(this.writer);
		this.SaveGenres(this.writer);
		this.SaveThemes(this.writer);
		this.SaveGameplayFeatures(this.writer);
		this.SaveEngineFeatures(this.writer);
		this.SaveHardware(this.writer);
		this.SaveHardwareFeatures(this.writer);
		this.SaveRooms(this.writer);
		this.SaveArbeitsmarkt(this.writer);
		this.SaveObjects(this.writer);
		this.SaveMuell(this.writer);
		this.SaveMitarbeiter(this.writer);
		this.SavePublisher(this.writer);
		this.SaveCopyProtect(this.writer);
		this.SaveAntiCheat(this.writer);
		this.SavePlatforms(this.writer);
		this.SaveEngines(this.writer);
		this.SaveGames(this.writer);
		this.SaveContractWork(this.writer);
		this.SaveTasks(this.writer);
		this.writer.Save();
		this.writer.Dispose();
		Debug.Log("Save Complete: " + i.ToString());
		if (this.mS_.multiplayer && this.mS_.mpCalls_.isClient)
		{
			this.mS_.mpCalls_.CLIENT_Send_Command(1);
		}
	}

	
	public int GetOfficeFromSavegame(int i)
	{
		string filePath = this.mS_.GetSavegameTitle() + i.ToString() + ".txt";
		this.reader = ES3Reader.Create(filePath);
		if (this.reader == null)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1226), false);
			Debug.Log("MISSING SAVE FILE!");
			return -1;
		}
		new int[100];
		int[] array = this.reader.Read<int[]>("globals_int");
		this.reader.Dispose();
		return array[21];
	}

	
	public void Load(int i)
	{
		this.loadingSavegame = true;
		string text = this.mS_.GetSavegameTitle() + i.ToString() + ".txt";
		this.KeysAbfragen(text);
		this.es3file = new ES3File(text);
		this.reader = ES3Reader.Create(text);
		Debug.Log("Load: Globals " + Time.realtimeSinceStartup);
		this.LoadGlobals(this.reader, text);
		Debug.Log("Load: LoadNPCGameNames " + Time.realtimeSinceStartup);
		this.LoadNPCGameNames(this.reader, text);
		Debug.Log("Load: LoadLicences " + Time.realtimeSinceStartup);
		this.LoadLicences(this.reader, text);
		Debug.Log("Load: LoadGenres " + Time.realtimeSinceStartup);
		this.LoadGenres(this.reader, text);
		Debug.Log("Load: LoadThemes " + Time.realtimeSinceStartup);
		this.LoadThemes(this.reader, text);
		Debug.Log("Load: LoadGameplayFeatures " + Time.realtimeSinceStartup);
		this.LoadGameplayFeatures(this.reader, text);
		Debug.Log("Load: LoadEngineFeatures " + Time.realtimeSinceStartup);
		this.LoadEngineFeatures(this.reader, text);
		Debug.Log("Load: Hardware " + Time.realtimeSinceStartup);
		this.LoadHardware(this.reader, text);
		Debug.Log("Load: HardwareFeatures " + Time.realtimeSinceStartup);
		this.LoadHardwareFeatures(this.reader, text);
		Debug.Log("Load: LoadRooms " + Time.realtimeSinceStartup);
		this.LoadRooms(this.reader, text);
		Debug.Log("Load: LoadArbeitsmarkt " + Time.realtimeSinceStartup);
		this.LoadArbeitsmarkt(this.reader, text);
		Debug.Log("Load: LoadObjects " + Time.realtimeSinceStartup);
		this.LoadObjects(this.reader, text);
		Debug.Log("Load: LoadMuell " + Time.realtimeSinceStartup);
		this.LoadMuell(this.reader, text);
		Debug.Log("Load: LoadMitarbeiter " + Time.realtimeSinceStartup);
		this.LoadMitarbeiter(this.reader, text);
		Debug.Log("Load: LoadPublisher " + Time.realtimeSinceStartup);
		this.LoadPublisher(this.reader, text);
		Debug.Log("Load: LoadCopyProtect " + Time.realtimeSinceStartup);
		this.LoadCopyProtect(this.reader, text);
		Debug.Log("Load: LoadAntiCheat " + Time.realtimeSinceStartup);
		this.LoadAntiCheat(this.reader, text);
		Debug.Log("Load: LoadPlatforms " + Time.realtimeSinceStartup);
		this.LoadPlatforms(this.reader, text);
		Debug.Log("Load: LoadEngines " + Time.realtimeSinceStartup);
		this.LoadEngines(this.reader, text);
		Debug.Log("Load: LoadGames " + Time.realtimeSinceStartup);
		this.LoadGames(this.reader, text);
		Debug.Log("Load: LoadContractWork " + Time.realtimeSinceStartup);
		this.LoadContractWork(this.reader, text);
		Debug.Log("Load: LoadTasks " + Time.realtimeSinceStartup);
		this.LoadTasks(this.reader, text);
		Debug.Log("Load: UpdateListInventar " + Time.realtimeSinceStartup);
		this.mS_.FindRooms();
		GameObject[] array = GameObject.FindGameObjectsWithTag("Room");
		for (int j = 0; j < array.Length; j++)
		{
			if (array[j])
			{
				array[j].GetComponent<roomScript>().UpdateListInventar();
			}
		}
		Debug.Log("Load: LagerplatzVerteilen " + Time.realtimeSinceStartup);
		this.games_.LagerplatzVerteilen();
		this.games_.UpdateChartsWeek();
		this.platforms_.UpdateGamesForPlatforms();
		Debug.Log("Load: UpdatePathfindingNextFrameExtern " + Time.realtimeSinceStartup);
		this.mS_.UpdatePathfindingNextFrameExtern();
		this.reader.Dispose();
		this.es3file.Clear();
		Debug.Log("Load: UpdateOnce");
		this.guiMain_.UpdateOnce();
		if (!this.mS_.multiplayer)
		{
			base.StartCoroutine(this.IENUM_UpdateCharacters());
		}
		if (this.mS_.savegameVersion == 15)
		{
			this.mS_.savegameVersion = 16;
		}
		this.mS_.settings_TutorialOff = true;
		this.loadingSavegame = false;
	}

	
	private IEnumerator IENUM_UpdateCharacters()
	{
		yield return new WaitForEndOfFrame();
		GameObject.FindGameObjectsWithTag("Character");
		for (int i = 0; i < this.mS_.arrayCharacters.Length; i++)
		{
			if (this.mS_.arrayCharacters[i])
			{
				movementScript component = this.mS_.arrayCharacters[i].GetComponent<movementScript>();
				if (component)
				{
					component.InitUpdate();
				}
			}
		}
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		this.mS_.SetGameSpeed(0f);
		yield break;
	}

	
	public string LoadSaveGameName(int i)
	{
		this.FindScripts();
		string filePath = this.mS_.GetSavegameTitle() + i.ToString() + ".txt";
		this.reader = ES3Reader.Create(filePath);
		if (this.reader == null)
		{
			return "<Missing File>";
		}
		new long[100];
		int[] array = new int[100];
		new float[100];
		string[] array2 = new string[100];
		array = this.reader.Read<int[]>("globals_int");
		this.reader.Read<float[]>("globals_float");
		this.reader.Read<long[]>("globals_long");
		array2 = this.reader.Read<string[]>("globals_string");
		this.reader.Dispose();
		string text = this.tS_.GetText(102) + array[3].ToString();
		text = text + " " + this.tS_.GetText(103) + array[4].ToString();
		text = text + " " + this.tS_.GetText(104) + array[5].ToString();
		return string.Concat(new string[]
		{
			array2[2],
			"\n<b>",
			text,
			" ▪ ",
			array2[0],
			"</b>"
		});
	}

	
	public bool IsSaveGameOutdatet(int i)
	{
		this.FindScripts();
		string filePath = this.mS_.GetSavegameTitle() + i.ToString() + ".txt";
		this.reader = ES3Reader.Create(filePath);
		if (this.reader == null)
		{
			return false;
		}
		new int[100];
		int[] array = this.reader.Read<int[]>("globals_int");
		this.reader.Dispose();
		return array[45] != this.saveGameVersion;
	}

	
	private void SaveGlobals(ES3Writer writer)
	{
		long[] array = new long[100];
		int[] array2 = new int[100];
		float[] array3 = new float[100];
		string[] array4 = new string[100];
		bool[] array5 = new bool[100];
		array2[0] = this.mS_.logo;
		array2[1] = this.mS_.country;
		array2[2] = 0;
		array2[3] = this.mS_.year;
		array2[4] = this.mS_.month;
		array2[5] = this.mS_.week;
		array2[6] = this.mS_.trendGenre;
		array2[7] = this.mS_.trendAntiGenre;
		array2[8] = this.mS_.trendTheme;
		array2[9] = this.mS_.trendAntiTheme;
		array2[10] = this.mS_.trendWeeks;
		array2[11] = this.mS_.anrufe;
		array2[12] = this.mS_.goldeneSchallplatten;
		array2[13] = this.mS_.difficulty;
		array2[14] = this.mS_.anzKonkurrenten;
		array2[15] = this.mS_.personal_druck;
		array2[16] = this.mS_.personal_pausen;
		array2[17] = this.mS_.personal_motivation;
		array2[18] = this.mS_.personal_crunch;
		array2[19] = this.mS_.exklusivVertrag_ID;
		array2[20] = this.mS_.exklusivVertrag_laufzeit;
		array2[21] = this.mS_.office;
		array2[22] = this.mS_.globalEvent;
		array2[23] = this.mS_.globalEventWeeks;
		array2[24] = this.mS_.marktforschung_bestPlattform;
		array2[25] = this.mS_.marktforschung_badPlattform;
		array2[26] = this.mS_.marktforschung_nextGenre;
		array2[27] = this.mS_.marktforschung_nextTopic;
		array2[28] = this.mS_.marktforschung_nextBadGenre;
		array2[29] = this.mS_.marktforschung_nextBadTopic;
		array2[30] = this.mS_.trendNextGenre;
		array2[31] = this.mS_.trendNextAntiGenre;
		array2[32] = this.mS_.trendNextTheme;
		array2[33] = this.mS_.trendNextAntiTheme;
		array2[34] = this.mS_.multiplayerSaveID;
		array2[35] = this.mS_.bankWarning;
		array2[36] = this.mS_.lastGamesGenre[0];
		array2[37] = this.mS_.lastGamesGenre[1];
		array2[38] = this.mS_.lastGamesGenre[2];
		array2[39] = this.mS_.lastGamesGenre[3];
		array2[40] = this.mS_.lastGamesGenre[4];
		array2[41] = this.mS_.gelangweiltGenre;
		array2[42] = this.mS_.platinSchallplatten;
		array2[43] = this.mS_.diamantSchallplatten;
		array2[44] = this.mS_.sauerBugs;
		if (this.mS_.savegameVersion <= 0)
		{
			array2[45] = this.saveGameVersion;
		}
		else
		{
			array2[45] = this.mS_.savegameVersion;
		}
		array2[46] = this.mS_.award_GOTY;
		array2[47] = this.mS_.award_Studio;
		array2[48] = this.mS_.companySpecialGenre;
		array2[49] = this.mS_.mpCalls_.myID;
		array2[50] = this.mS_.award_Grafik;
		array2[51] = this.mS_.award_Sound;
		array2[52] = this.mS_.award_Trendsetter;
		array2[53] = this.mS_.studioPoints;
		array2[54] = this.mS_.award_Publisher;
		array2[55] = this.mS_.marktforschung_bestPlattformKonsole;
		array2[56] = this.mS_.marktforschung_badPlattformKonsole;
		array2[57] = this.mS_.marktforschung_bestPlattformHandheld;
		array2[58] = this.mS_.marktforschung_badPlattformHandheld;
		array2[59] = this.mS_.marktforschung_bestPlattformHandy;
		array2[60] = this.mS_.marktforschung_badPlattformHandy;
		array2[61] = this.mS_.pubOffersAmount;
		array2[62] = this.publishingOfferMain_.amountPublishingOffers;
		array2[63] = this.menuArcadePreis_.setCase;
		array2[64] = this.menuArcadePreis_.setMonitor;
		array2[65] = this.menuArcadePreis_.setJoystick;
		array2[66] = this.menuArcadePreis_.setSound;
		array3[0] = this.mS_.auftragsAnsehen;
		if (!this.mS_.multiplayer)
		{
			array3[1] = this.mS_.dayTimer;
		}
		array3[2] = this.mS_.record_Gameplay;
		array3[3] = this.mS_.record_Grafik;
		array3[4] = this.mS_.record_Sound;
		array3[5] = this.mS_.record_Technik;
		array3[6] = this.mS_.speedSetting;
		array3[7] = this.mS_.marktforschung_digtal;
		array3[8] = this.mS_.marktforschung_retail;
		array3[9] = this.mS_.marktforschung_deluxe;
		array3[10] = this.mS_.marktforschung_collectors;
		array3[11] = this.mS_.marktforschung_arcade;
		array[0] = this.mS_.money;
		array[1] = this.mS_.kredit;
		array4[0] = this.mS_.companyName;
		array4[1] = this.mS_.playerName;
		array4[2] = DateTime.Now.ToShortDateString() + " ▪ " + DateTime.Now.ToShortTimeString();
		array4[3] = this.mS_.marktforschung_datum;
		array5[0] = this.mS_.settings_TutorialOff;
		array5[1] = this.mS_.settings_RandomEventsOff;
		array5[2] = this.mS_.automatic_RemoveGameFormMarket;
		array5[3] = this.mS_.multiplayer;
		array5[4] = this.mS_.settings_autoPauseForMultiplayer;
		array5[5] = this.mS_.settings_RandomReviews;
		array5[6] = this.mS_.settings_arbeitsgeschwindigkeitAnpassen;
		array5[7] = this.mS_.personal_dontLeaveBuilding;
		array5[8] = this.mS_.personal_RobotDontLeaveBuilding;
		array5[9] = this.mS_.personal_ki;
		array5[10] = this.mS_.badGameThisYear;
		array5[11] = this.mS_.sellLagerbestandAutomatic;
		array5[12] = this.mS_.lastGameCommercialFlop;
		array5[13] = this.mS_.settings_history;
		array5[14] = this.mS_.settings_plattformEnd;
		writer.Write<int[]>("globals_int", array2);
		writer.Write<float[]>("globals_float", array3);
		writer.Write<long[]>("globals_long", array);
		writer.Write<string[]>("globals_string", array4);
		writer.Write<bool[]>("globals_bool", array5);
		writer.Write<bool[]>("unlock->unlock", this.unlock_.unlock);
		writer.Write<bool[]>("devLegendsInUse", this.mS_.devLegendsInUse);
		writer.Write<bool[]>("devLegendsFemale", this.mS_.devLegendsFemale);
		writer.Write<bool[]>("devLegendsDesigner", this.mS_.devLegendsDesigner);
		writer.Write<bool[]>("devLegendsProgrammierer", this.mS_.devLegendsProgrammierer);
		writer.Write<bool[]>("devLegendsGrafiker", this.mS_.devLegendsGrafiker);
		writer.Write<bool[]>("devLegendsMusiker", this.mS_.devLegendsMusiker);
		writer.Write<bool[]>("devLegendsForscher", this.mS_.devLegendsForscher);
		writer.Write<bool[]>("devLegendsHardware", this.mS_.devLegendsHardware);
		writer.Write<string[]>("tS->devLegends", this.tS_.devLegends);
		writer.Write<float[]>("fS->RES_POINTS_LEFT", this.fS_.RES_POINTS_LEFT);
		writer.Write<long[]>("fanshopverlauf", this.mS_.fanshopverlauf);
		writer.Write<long[]>("finanzVerlauf", this.mS_.finanzVerlauf);
		writer.Write<long[]>("verkaufsverlauf", this.mS_.verkaufsverlauf);
		writer.Write<long[]>("verkaufsverlaufKonsolen", this.mS_.verkaufsverlaufKonsolen);
		writer.Write<long[]>("aboverlauf", this.mS_.aboverlauf);
		writer.Write<long[]>("downloadverlauf", this.mS_.downloadverlauf);
		writer.Write<long[]>("fansverlauf", this.mS_.fansverlauf);
		writer.Write<long[]>("finanzenMonat", this.mS_.finanzenMonat);
		writer.Write<long[]>("finanzenMonatLast", this.mS_.finanzenMonatLast);
		writer.Write<long[]>("finanzenJahr", this.mS_.finanzenJahr);
		writer.Write<long[]>("finanzenJahrLast", this.mS_.finanzenJahrLast);
		writer.Write<int[]>("awards", this.mS_.awards);
		writer.Write<bool[]>("newsSetting", this.mS_.newsSetting);
		writer.Write<bool[]>("gameTabFilter", this.mS_.gameTabFilter);
		writer.Write<bool[]>("buildings", this.mS_.buildings);
		writer.Write<string[]>("personal_group_names", this.mS_.personal_group_names);
		writer.Write<int[]>("verkaufspreis_default_addon", this.menuPackung_.verkaufspreis_default_addon);
		writer.Write<int[]>("verkaufspreis_default_budget", this.menuPackung_.verkaufspreis_default_budget);
		writer.Write<int[]>("verkaufspreis_default_goty", this.menuPackung_.verkaufspreis_default_goty);
		writer.Write<int[]>("verkaufspreis_default_standard", this.menuPackung_.verkaufspreis_default_standard);
		writer.Write<bool[]>("standard_default_addon", this.menuPackung_.standard_default_addon);
		writer.Write<bool[]>("deluxe_default_addon", this.menuPackung_.deluxe_default_addon);
		writer.Write<bool[]>("collectors_default_addon", this.menuPackung_.collectors_default_addon);
		writer.Write<bool[]>("standard_default_budget", this.menuPackung_.standard_default_budget);
		writer.Write<bool[]>("deluxe_default_budget", this.menuPackung_.deluxe_default_budget);
		writer.Write<bool[]>("collectors_default_budget", this.menuPackung_.collectors_default_budget);
		writer.Write<bool[]>("standard_default_goty", this.menuPackung_.standard_default_goty);
		writer.Write<bool[]>("deluxe_default_goty", this.menuPackung_.deluxe_default_goty);
		writer.Write<bool[]>("collectors_default_goty", this.menuPackung_.collectors_default_goty);
		writer.Write<bool[]>("standard_default_standard", this.menuPackung_.standard_default_standard);
		writer.Write<bool[]>("deluxe_default_standard", this.menuPackung_.deluxe_default_standard);
		writer.Write<bool[]>("collectors_default_standard", this.menuPackung_.collectors_default_standard);
		writer.Write<int[]>("verkaufspreis_default_bundle", this.menuPackung_.verkaufspreis_default_bundle);
		writer.Write<int[]>("verkaufspreis_default_bundleAddon", this.menuPackung_.verkaufspreis_default_bundleAddon);
		writer.Write<bool[]>("standard_default_bundleAddon", this.menuPackung_.standard_default_bundleAddon);
		writer.Write<bool[]>("deluxe_default_bundleAddon", this.menuPackung_.deluxe_default_bundleAddon);
		writer.Write<bool[]>("collectors_default_bundleAddon", this.menuPackung_.collectors_default_bundleAddon);
		writer.Write<bool[]>("standard_default_bundle", this.menuPackung_.standard_default_bundle);
		writer.Write<bool[]>("deluxe_default_bundle", this.menuPackung_.deluxe_default_bundle);
		writer.Write<bool[]>("collectors_default_bundle", this.menuPackung_.collectors_default_bundle);
		writer.Write<bool[]>("achivements", this.mS_.achivements);
		writer.Write<bool[]>("inventarFilter", this.menu_BuyInventar_.filter);
		writer.Write<string[]>("npcIPs", this.tS_.npcIPs);
		writer.Write<bool[]>("npcIPsInUse", this.tS_.npcIPsInUse);
		long[] value = this.mS_.finanzVerlaufEinnahmen.ToArray();
		writer.Write<long[]>("finanzVerlaufEinnahmen", value);
		long[] value2 = this.mS_.finanzVerlaufAusgaben.ToArray();
		writer.Write<long[]>("finanzVerlaufAusgaben", value2);
		string[] value3 = this.mS_.history.ToArray();
		writer.Write<string[]>("history", value3);
		int[] value4 = this.mS_.madGamesCon_Jahr.ToArray();
		writer.Write<int[]>("madGamesCon_Jahr", value4);
		int[] value5 = this.mS_.madGamesCon_BestGrafik.ToArray();
		writer.Write<int[]>("madGamesCon_BestGrafik", value5);
		int[] value6 = this.mS_.madGamesCon_BestSound.ToArray();
		writer.Write<int[]>("madGamesCon_BestSound", value6);
		int[] value7 = this.mS_.madGamesCon_BestStudio.ToArray();
		writer.Write<int[]>("madGamesCon_BestStudio", value7);
		int[] value8 = this.mS_.madGamesCon_BestStudioPlayer.ToArray();
		writer.Write<int[]>("madGamesCon_BestStudioPlayer", value8);
		int[] value9 = this.mS_.madGamesCon_BestPublisher.ToArray();
		writer.Write<int[]>("madGamesCon_BestPublisher", value9);
		int[] value10 = this.mS_.madGamesCon_BestPublisherPlayer.ToArray();
		writer.Write<int[]>("madGamesCon_BestPublisherPlayer", value10);
		int[] value11 = this.mS_.madGamesCon_BestGame.ToArray();
		writer.Write<int[]>("madGamesCon_BestGame", value11);
		int[] value12 = this.mS_.madGamesCon_BadGame.ToArray();
		writer.Write<int[]>("madGamesCon_BadGame", value12);
		if (this.mS_.multiplayer)
		{
			for (int i = 0; i < 4; i++)
			{
				if (this.mpCalls_.playersMP.Count > i)
				{
					player_mp player_mp = this.mpCalls_.playersMP[i];
					writer.Write<int>("MP_playerID" + i.ToString(), player_mp.playerID);
					writer.Write<string>("MP_playerName" + i.ToString(), player_mp.playerName);
					writer.Write<string>("MP_companyName" + i.ToString(), player_mp.companyName);
					writer.Write<int>("MP_companyLogo" + i.ToString(), player_mp.companyLogo);
					writer.Write<int>("MP_companyCountry" + i.ToString(), player_mp.companyCountry);
					writer.Write<int[,]>("MP_mapRoomID" + i.ToString(), player_mp.mapRoomID);
					writer.Write<int[,]>("MP_mapRoomTyp" + i.ToString(), player_mp.mapRoomTyp);
					writer.Write<int[,]>("MP_mapDoors" + i.ToString(), player_mp.mapDoors);
					writer.Write<int[,]>("MP_mapWindows" + i.ToString(), player_mp.mapWindows);
					int count = player_mp.objects.Count;
					int[] array6 = new int[count];
					int[] array7 = new int[count];
					Vector3[] array8 = new Vector3[count];
					for (int j = 0; j < count; j++)
					{
						array6[j] = player_mp.objects[j].id;
						array7[j] = player_mp.objects[j].typ;
						array8[j] = new Vector3(player_mp.objects[j].posX, player_mp.objects[j].posY, player_mp.objects[j].rotation);
					}
					writer.Write<int[]>("MP_object_id" + i.ToString(), array6);
					writer.Write<int[]>("MP_object_typ" + i.ToString(), array7);
					writer.Write<Vector3[]>("MP_object_pos" + i.ToString(), array8);
				}
				else
				{
					writer.Write<int>("MP_playerID" + i.ToString(), -1);
				}
			}
		}
	}

	
	private void LoadGlobals(ES3Reader reader, string filename)
	{
		long[] array = new long[100];
		int[] array2 = new int[100];
		float[] array3 = new float[100];
		string[] array4 = new string[100];
		bool[] array5 = new bool[100];
		array2 = reader.Read<int[]>("globals_int");
		array3 = reader.Read<float[]>("globals_float");
		array = reader.Read<long[]>("globals_long");
		array4 = reader.Read<string[]>("globals_string");
		array5 = reader.Read<bool[]>("globals_bool");
		this.mS_.logo = array2[0];
		this.mS_.country = array2[1];
		this.mS_.year = array2[3];
		this.mS_.month = array2[4];
		this.mS_.week = array2[5];
		this.mS_.trendGenre = array2[6];
		this.mS_.trendAntiGenre = array2[7];
		this.mS_.trendTheme = array2[8];
		this.mS_.trendAntiTheme = array2[9];
		this.mS_.trendWeeks = array2[10];
		this.mS_.anrufe = array2[11];
		this.mS_.goldeneSchallplatten = array2[12];
		this.mS_.difficulty = array2[13];
		this.mS_.anzKonkurrenten = array2[14];
		this.mS_.personal_druck = array2[15];
		this.mS_.personal_pausen = array2[16];
		this.mS_.personal_motivation = array2[17];
		this.mS_.personal_crunch = array2[18];
		this.mS_.exklusivVertrag_ID = array2[19];
		this.mS_.exklusivVertrag_laufzeit = array2[20];
		this.mS_.office = array2[21];
		this.mS_.globalEvent = array2[22];
		this.mS_.globalEventWeeks = array2[23];
		this.mS_.marktforschung_bestPlattform = array2[24];
		this.mS_.marktforschung_badPlattform = array2[25];
		this.mS_.marktforschung_nextGenre = array2[26];
		this.mS_.marktforschung_nextTopic = array2[27];
		this.mS_.marktforschung_nextBadGenre = array2[28];
		this.mS_.marktforschung_nextBadTopic = array2[29];
		this.mS_.trendNextGenre = array2[30];
		this.mS_.trendNextAntiGenre = array2[31];
		this.mS_.trendNextTheme = array2[32];
		this.mS_.trendNextAntiTheme = array2[33];
		this.mS_.multiplayerSaveID = array2[34];
		this.mS_.bankWarning = array2[35];
		this.mS_.lastGamesGenre[0] = array2[36];
		this.mS_.lastGamesGenre[1] = array2[37];
		this.mS_.lastGamesGenre[2] = array2[38];
		this.mS_.lastGamesGenre[3] = array2[39];
		this.mS_.lastGamesGenre[4] = array2[40];
		this.mS_.gelangweiltGenre = array2[41];
		this.mS_.platinSchallplatten = array2[42];
		this.mS_.diamantSchallplatten = array2[43];
		this.mS_.sauerBugs = array2[44];
		this.mS_.savegameVersion = array2[45];
		this.mS_.award_GOTY = array2[46];
		this.mS_.award_Studio = array2[47];
		this.mS_.companySpecialGenre = array2[48];
		this.savegamePlayerID = array2[49];
		this.mS_.award_Grafik = array2[50];
		this.mS_.award_Sound = array2[51];
		this.mS_.award_Trendsetter = array2[52];
		this.mS_.studioPoints = array2[53];
		this.mS_.award_Publisher = array2[54];
		this.mS_.marktforschung_bestPlattformKonsole = array2[55];
		this.mS_.marktforschung_badPlattformKonsole = array2[56];
		this.mS_.marktforschung_bestPlattformHandheld = array2[57];
		this.mS_.marktforschung_badPlattformHandheld = array2[58];
		this.mS_.marktforschung_bestPlattformHandy = array2[59];
		this.mS_.marktforschung_badPlattformHandy = array2[60];
		this.mS_.pubOffersAmount = array2[61];
		this.publishingOfferMain_.amountPublishingOffers = array2[62];
		this.menuArcadePreis_.setCase = array2[63];
		this.menuArcadePreis_.setMonitor = array2[64];
		this.menuArcadePreis_.setJoystick = array2[65];
		this.menuArcadePreis_.setSound = array2[66];
		this.mS_.auftragsAnsehen = array3[0];
		if (!this.mS_.multiplayer)
		{
			this.mS_.dayTimer = array3[1];
		}
		this.mS_.record_Gameplay = array3[2];
		this.mS_.record_Grafik = array3[3];
		this.mS_.record_Sound = array3[4];
		this.mS_.record_Technik = array3[5];
		this.mS_.speedSetting = array3[6];
		this.mS_.marktforschung_digtal = array3[7];
		this.mS_.marktforschung_retail = array3[8];
		this.mS_.marktforschung_deluxe = array3[9];
		this.mS_.marktforschung_collectors = array3[10];
		this.mS_.marktforschung_arcade = array3[11];
		this.mS_.money = array[0];
		this.mS_.kredit = array[1];
		this.mS_.companyName = array4[0];
		this.mS_.playerName = array4[1];
		this.mS_.marktforschung_datum = array4[3];
		this.mS_.settings_TutorialOff = array5[0];
		this.mS_.settings_RandomEventsOff = array5[1];
		this.mS_.automatic_RemoveGameFormMarket = array5[2];
		this.mS_.multiplayer = array5[3];
		this.mS_.settings_autoPauseForMultiplayer = array5[4];
		this.mS_.settings_RandomReviews = array5[5];
		this.mS_.settings_arbeitsgeschwindigkeitAnpassen = array5[6];
		this.mS_.personal_dontLeaveBuilding = array5[7];
		this.mS_.personal_RobotDontLeaveBuilding = array5[8];
		this.mS_.personal_ki = array5[9];
		this.mS_.badGameThisYear = array5[10];
		this.mS_.sellLagerbestandAutomatic = array5[11];
		this.mS_.lastGameCommercialFlop = array5[12];
		this.mS_.settings_history = array5[13];
		this.mS_.settings_plattformEnd = array5[14];
		this.unlock_.unlock = reader.Read<bool[]>("unlock->unlock");
		this.mS_.devLegendsInUse = reader.Read<bool[]>("devLegendsInUse");
		this.mS_.devLegendsFemale = reader.Read<bool[]>("devLegendsFemale");
		if (this.key_legends)
		{
			this.mS_.devLegendsDesigner = reader.Read<bool[]>("devLegendsDesigner");
			this.mS_.devLegendsProgrammierer = reader.Read<bool[]>("devLegendsProgrammierer");
			this.mS_.devLegendsGrafiker = reader.Read<bool[]>("devLegendsGrafiker");
			this.mS_.devLegendsMusiker = reader.Read<bool[]>("devLegendsMusiker");
			this.mS_.devLegendsForscher = reader.Read<bool[]>("devLegendsForscher");
			if (this.mS_.savegameVersion >= 15)
			{
				this.mS_.devLegendsHardware = reader.Read<bool[]>("devLegendsHardware");
			}
		}
		this.tS_.devLegends = reader.Read<string[]>("tS->devLegends");
		this.fS_.RES_POINTS_LEFT = reader.Read<float[]>("fS->RES_POINTS_LEFT");
		if (this.fS_.RES_POINTS_LEFT.Length < this.fS_.RES_POINTS.Length)
		{
			this.fS_.RES_POINTS_LEFT = new float[this.fS_.RES_POINTS.Length];
			float[] array6 = reader.Read<float[]>("fS->RES_POINTS_LEFT");
			for (int i = 0; i < array6.Length; i++)
			{
				this.fS_.RES_POINTS_LEFT[i] = array6[i];
			}
		}
		if (this.key_fanshopverlauf)
		{
			this.mS_.fanshopverlauf = reader.Read<long[]>("fanshopverlauf");
		}
		this.mS_.finanzVerlauf = reader.Read<long[]>("finanzVerlauf");
		this.mS_.verkaufsverlauf = reader.Read<long[]>("verkaufsverlauf");
		if (this.mS_.savegameVersion >= 14)
		{
			this.mS_.verkaufsverlaufKonsolen = reader.Read<long[]>("verkaufsverlaufKonsolen");
		}
		this.mS_.aboverlauf = reader.Read<long[]>("aboverlauf");
		this.mS_.downloadverlauf = reader.Read<long[]>("downloadverlauf");
		this.mS_.fansverlauf = reader.Read<long[]>("fansverlauf");
		this.mS_.finanzenMonat = reader.Read<long[]>("finanzenMonat");
		this.mS_.finanzenMonatLast = reader.Read<long[]>("finanzenMonatLast");
		this.mS_.finanzenJahr = reader.Read<long[]>("finanzenJahr");
		this.mS_.finanzenJahrLast = reader.Read<long[]>("finanzenJahrLast");
		this.mS_.awards = reader.Read<int[]>("awards");
		this.mS_.newsSetting = reader.Read<bool[]>("newsSetting");
		if (this.key_gameTabFilter)
		{
			this.mS_.gameTabFilter = reader.Read<bool[]>("gameTabFilter");
		}
		this.mS_.buildings = reader.Read<bool[]>("buildings");
		if (this.mS_.savegameVersion >= 3)
		{
			this.mS_.personal_group_names = reader.Read<string[]>("personal_group_names");
		}
		if (this.key_default_verkaufpreis)
		{
			this.menuPackung_.verkaufspreis_default_addon = reader.Read<int[]>("verkaufspreis_default_addon");
			this.menuPackung_.verkaufspreis_default_budget = reader.Read<int[]>("verkaufspreis_default_budget");
			this.menuPackung_.verkaufspreis_default_goty = reader.Read<int[]>("verkaufspreis_default_goty");
			this.menuPackung_.verkaufspreis_default_standard = reader.Read<int[]>("verkaufspreis_default_standard");
			this.menuPackung_.standard_default_addon = reader.Read<bool[]>("standard_default_addon");
			this.menuPackung_.deluxe_default_addon = reader.Read<bool[]>("deluxe_default_addon");
			this.menuPackung_.collectors_default_addon = reader.Read<bool[]>("collectors_default_addon");
			this.menuPackung_.standard_default_budget = reader.Read<bool[]>("standard_default_budget");
			this.menuPackung_.deluxe_default_budget = reader.Read<bool[]>("deluxe_default_budget");
			this.menuPackung_.collectors_default_budget = reader.Read<bool[]>("collectors_default_budget");
			this.menuPackung_.standard_default_goty = reader.Read<bool[]>("standard_default_goty");
			this.menuPackung_.deluxe_default_goty = reader.Read<bool[]>("deluxe_default_goty");
			this.menuPackung_.collectors_default_goty = reader.Read<bool[]>("collectors_default_goty");
			this.menuPackung_.standard_default_standard = reader.Read<bool[]>("standard_default_standard");
			this.menuPackung_.deluxe_default_standard = reader.Read<bool[]>("deluxe_default_standard");
			this.menuPackung_.collectors_default_standard = reader.Read<bool[]>("collectors_default_standard");
		}
		if (this.key_default_verkaufpreisBundle)
		{
			this.menuPackung_.verkaufspreis_default_bundle = reader.Read<int[]>("verkaufspreis_default_bundle");
			this.menuPackung_.verkaufspreis_default_bundleAddon = reader.Read<int[]>("verkaufspreis_default_bundleAddon");
			this.menuPackung_.standard_default_bundleAddon = reader.Read<bool[]>("standard_default_bundleAddon");
			this.menuPackung_.deluxe_default_bundleAddon = reader.Read<bool[]>("deluxe_default_bundleAddon");
			this.menuPackung_.collectors_default_bundleAddon = reader.Read<bool[]>("collectors_default_bundleAddon");
			this.menuPackung_.standard_default_bundle = reader.Read<bool[]>("standard_default_bundle");
			this.menuPackung_.deluxe_default_bundle = reader.Read<bool[]>("deluxe_default_bundle");
			this.menuPackung_.collectors_default_bundle = reader.Read<bool[]>("collectors_default_bundle");
		}
		if (this.key_achivements)
		{
			this.mS_.achivements = reader.Read<bool[]>("achivements");
		}
		if (this.key_inventarFilter)
		{
			this.menu_BuyInventar_.filter = reader.Read<bool[]>("inventarFilter");
		}
		if (this.key_NpcIPs)
		{
			this.tS_.npcIPs = reader.Read<string[]>("npcIPs");
			this.tS_.npcIPsInUse = reader.Read<bool[]>("npcIPsInUse");
		}
		long[] array7 = reader.Read<long[]>("finanzVerlaufEinnahmen");
		for (int j = 0; j < array7.Length; j++)
		{
			this.mS_.finanzVerlaufEinnahmen.Add(array7[j]);
		}
		long[] array8 = reader.Read<long[]>("finanzVerlaufAusgaben");
		for (int k = 0; k < array8.Length; k++)
		{
			this.mS_.finanzVerlaufAusgaben.Add(array8[k]);
		}
		string[] array9 = reader.Read<string[]>("history");
		for (int l = 0; l < array9.Length; l++)
		{
			this.mS_.history.Add(array9[l]);
		}
		if (this.mS_.savegameVersion >= 4)
		{
			int[] array10 = reader.Read<int[]>("madGamesCon_Jahr");
			for (int m = 0; m < array10.Length; m++)
			{
				this.mS_.madGamesCon_Jahr.Add(array10[m]);
			}
			int[] array11 = reader.Read<int[]>("madGamesCon_BestGrafik");
			for (int n = 0; n < array11.Length; n++)
			{
				this.mS_.madGamesCon_BestGrafik.Add(array11[n]);
			}
			int[] array12 = reader.Read<int[]>("madGamesCon_BestSound");
			for (int num = 0; num < array12.Length; num++)
			{
				this.mS_.madGamesCon_BestSound.Add(array12[num]);
			}
			int[] array13 = reader.Read<int[]>("madGamesCon_BestStudio");
			for (int num2 = 0; num2 < array13.Length; num2++)
			{
				this.mS_.madGamesCon_BestStudio.Add(array13[num2]);
			}
			int[] array14 = reader.Read<int[]>("madGamesCon_BestStudioPlayer");
			for (int num3 = 0; num3 < array14.Length; num3++)
			{
				this.mS_.madGamesCon_BestStudioPlayer.Add(array14[num3]);
			}
			int[] array15 = reader.Read<int[]>("madGamesCon_BestPublisher");
			for (int num4 = 0; num4 < array15.Length; num4++)
			{
				this.mS_.madGamesCon_BestPublisher.Add(array15[num4]);
			}
			int[] array16 = reader.Read<int[]>("madGamesCon_BestPublisherPlayer");
			for (int num5 = 0; num5 < array16.Length; num5++)
			{
				this.mS_.madGamesCon_BestPublisherPlayer.Add(array16[num5]);
			}
			int[] array17 = reader.Read<int[]>("madGamesCon_BestGame");
			for (int num6 = 0; num6 < array17.Length; num6++)
			{
				this.mS_.madGamesCon_BestGame.Add(array17[num6]);
			}
			int[] array18 = reader.Read<int[]>("madGamesCon_BadGame");
			for (int num7 = 0; num7 < array18.Length; num7++)
			{
				this.mS_.madGamesCon_BadGame.Add(array18[num7]);
			}
		}
		if (this.mS_.multiplayer)
		{
			this.mpCalls_.playersMP.Clear();
			for (int num8 = 0; num8 < 4; num8++)
			{
				int num9 = reader.Read<int>("MP_playerID" + num8.ToString());
				if (num9 != -1)
				{
					this.mpCalls_.playersMP.Add(new player_mp(num9));
					player_mp player_mp = this.mpCalls_.FindPlayer(num9);
					player_mp.playerName = reader.Read<string>("MP_playerName" + num8.ToString());
					player_mp.companyName = reader.Read<string>("MP_companyName" + num8.ToString());
					player_mp.companyLogo = reader.Read<int>("MP_companyLogo" + num8.ToString());
					player_mp.companyCountry = reader.Read<int>("MP_companyCountry" + num8.ToString());
					player_mp.mapRoomID = reader.Read<int[,]>("MP_mapRoomID" + num8.ToString());
					player_mp.mapRoomTyp = reader.Read<int[,]>("MP_mapRoomTyp" + num8.ToString());
					player_mp.mapDoors = reader.Read<int[,]>("MP_mapDoors" + num8.ToString());
					player_mp.mapWindows = reader.Read<int[,]>("MP_mapWindows" + num8.ToString());
					int[] array19 = reader.Read<int[]>("MP_object_id" + num8.ToString());
					int[] array20 = reader.Read<int[]>("MP_object_typ" + num8.ToString());
					Vector3[] array21 = reader.Read<Vector3[]>("MP_object_pos" + num8.ToString());
					Debug.Log("Amount of MP Objects: " + array19.Length.ToString());
					for (int num10 = 0; num10 < array19.Length; num10++)
					{
						player_mp.objects.Add(new object_mp(array19[num10], array20[num10], array21[num10].x, array21[num10].y, array21[num10].z));
					}
				}
			}
		}
	}

	
	private void SaveMitarbeiter(ES3Writer writer)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Character");
		int num = array.Length;
		writer.Write<int>("anzCharacter", num);
		int[] array2 = new int[30 * num];
		float[] array3 = new float[30 * num];
		string[] array4 = new string[5 * num];
		bool[] array5 = new bool[30 * num];
		int num2 = 0;
		if (array.Length != 0 && array[0])
		{
			characterScript component = array[0].GetComponent<characterScript>();
			if (component)
			{
				num2 = component.perks.Length;
			}
		}
		bool[] array6 = new bool[num2 * num];
		for (int i = 0; i < array.Length; i++)
		{
			characterScript component2 = array[i].GetComponent<characterScript>();
			int num3 = i * 30;
			array2[num3] = component2.myID;
			array2[1 + num3] = component2.group;
			array2[2 + num3] = component2.roomID;
			array2[3 + num3] = component2.objectUsingID;
			array2[4 + num3] = component2.objectBelegtID;
			array2[5 + num3] = component2.legend;
			array2[6 + num3] = component2.model_body;
			array2[7 + num3] = component2.model_eyes;
			array2[8 + num3] = component2.model_hair;
			array2[9 + num3] = component2.model_beard;
			array2[10 + num3] = component2.model_skinColor;
			array2[11 + num3] = component2.model_hairColor;
			array2[12 + num3] = component2.model_beardColor;
			array2[13 + num3] = component2.model_HoseColor;
			array2[14 + num3] = component2.model_ShirtColor;
			array2[15 + num3] = component2.model_Add1Color;
			array2[16 + num3] = component2.krank;
			array2[17 + num3] = component2.beruf;
			int num4 = i * 30;
			array3[num4] = array[i].transform.position.x;
			array3[1 + num4] = array[i].transform.position.y;
			array3[2 + num4] = array[i].transform.position.z;
			array3[3 + num4] = component2.s_motivation;
			array3[4 + num4] = component2.s_gamedesign;
			array3[5 + num4] = component2.s_programmieren;
			array3[6 + num4] = component2.s_grafik;
			array3[7 + num4] = component2.s_sound;
			array3[8 + num4] = component2.s_pr;
			array3[9 + num4] = component2.s_gametests;
			array3[10 + num4] = component2.s_technik;
			array3[11 + num4] = component2.s_forschen;
			array3[12 + num4] = component2.workProgress;
			array3[13 + num4] = component2.durst;
			array3[14 + num4] = component2.hunger;
			array3[15 + num4] = component2.klo;
			array3[16 + num4] = component2.waschbecken;
			array3[17 + num4] = component2.muell;
			array3[18 + num4] = component2.giessen;
			array3[19 + num4] = component2.pause;
			array3[20 + num4] = array[i].transform.eulerAngles.x;
			array3[21 + num4] = array[i].transform.eulerAngles.y;
			array3[22 + num4] = array[i].transform.eulerAngles.z;
			array3[23 + num4] = component2.freezer;
			int num5 = i * 5;
			array4[num5] = component2.myName;
			int num6 = i * 30;
			array5[num6] = component2.male;
			int num7 = i * num2;
			for (int j = 0; j < component2.perks.Length; j++)
			{
				array6[j + num7] = component2.perks[j];
			}
		}
		writer.Write<int[]>("characters_I", array2);
		writer.Write<float[]>("characters_F", array3);
		writer.Write<string[]>("characters_S", array4);
		writer.Write<bool[]>("characters_B", array5);
		writer.Write<bool[]>("characters_perks", array6);
	}

	
	private void LoadMitarbeiter(ES3Reader reader, string filename)
	{
		int num = reader.Read<int>("anzCharacter", -1);
		if (num <= 0)
		{
			return;
		}
		bool[] array = new bool[0];
		int[] array2 = reader.Read<int[]>("characters_I");
		float[] array3 = reader.Read<float[]>("characters_F");
		string[] array4 = reader.Read<string[]>("characters_S");
		bool[] array5 = reader.Read<bool[]>("characters_B");
		if (this.mS_.savegameVersion >= 16)
		{
			array = reader.Read<bool[]>("characters_perks");
		}
		for (int i = 0; i < num; i++)
		{
			int num2 = i * 30;
			int num3 = i * 30;
			int num4 = i * 30;
			int num5 = i * 5;
			characterScript characterScript = this.cCS_.CreateCharacter(array2[num2], array5[num4], array2[6 + num2]);
			characterScript.myID = array2[num2];
			characterScript.group = array2[1 + num2];
			characterScript.roomID = array2[2 + num2];
			characterScript.objectUsingID = array2[3 + num2];
			characterScript.objectBelegtID = array2[4 + num2];
			characterScript.legend = array2[5 + num2];
			characterScript.model_body = array2[6 + num2];
			characterScript.model_eyes = array2[7 + num2];
			characterScript.model_hair = array2[8 + num2];
			characterScript.model_beard = array2[9 + num2];
			characterScript.model_skinColor = array2[10 + num2];
			characterScript.model_hairColor = array2[11 + num2];
			characterScript.model_beardColor = array2[12 + num2];
			characterScript.model_HoseColor = array2[13 + num2];
			characterScript.model_ShirtColor = array2[14 + num2];
			characterScript.model_Add1Color = array2[15 + num2];
			characterScript.krank = array2[16 + num2];
			characterScript.beruf = array2[17 + num2];
			characterScript.s_motivation = array3[3 + num3];
			characterScript.s_gamedesign = array3[4 + num3];
			characterScript.s_programmieren = array3[5 + num3];
			characterScript.s_grafik = array3[6 + num3];
			characterScript.s_sound = array3[7 + num3];
			characterScript.s_pr = array3[8 + num3];
			characterScript.s_gametests = array3[9 + num3];
			characterScript.s_technik = array3[10 + num3];
			characterScript.s_forschen = array3[11 + num3];
			characterScript.workProgress = array3[12 + num3];
			characterScript.durst = array3[13 + num3];
			characterScript.hunger = array3[14 + num3];
			characterScript.klo = array3[15 + num3];
			characterScript.waschbecken = array3[16 + num3];
			characterScript.muell = array3[17 + num3];
			characterScript.giessen = array3[18 + num3];
			characterScript.pause = array3[19 + num3];
			characterScript.freezer = array3[23 + num3];
			characterScript.myName = array4[num5];
			characterScript.male = array5[num4];
			if (this.mS_.savegameVersion < 16)
			{
				if (this.es3file.KeyExists("charactersA1_"))
				{
					characterScript.perks = reader.Read<bool[]>("charactersA1_" + characterScript.myID.ToString());
				}
				if (characterScript.perks.Length < 40)
				{
					bool[] array6 = (bool[])characterScript.perks.Clone();
					characterScript.perks = new bool[40];
					for (int j = 0; j < array6.Length; j++)
					{
						characterScript.perks[j] = array6[j];
					}
				}
			}
			else
			{
				characterScript.perks = new bool[array.Length / num];
				int num6 = i * (array.Length / num);
				for (int k = 0; k < characterScript.perks.Length; k++)
				{
					characterScript.perks[k] = array[k + num6];
				}
			}
			characterScript.gameObject.transform.GetChild(0).GetComponent<characterGFXScript>().Init(true);
			characterScript.gameObject.transform.position = new Vector3(array3[num3], array3[1 + num3], array3[2 + num3]);
			characterScript.gameObject.transform.eulerAngles = new Vector3(array3[20 + num3], array3[21 + num3], array3[22 + num3]);
			if (characterScript.objectBelegtID != -1)
			{
				characterScript.gameObject.GetComponent<movementScript>().FindObjectInRoom(-1, GameObject.Find("O_" + characterScript.objectBelegtID.ToString()), false);
			}
		}
	}

	
	private void SaveObjects(ES3Writer writer)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Object");
		int num = array.Length;
		writer.Write<int>("anzObjects", num);
		int[] array2 = new int[10 * num];
		float[] array3 = new float[10 * num];
		bool[] array4 = new bool[10 * num];
		for (int i = 0; i < array.Length; i++)
		{
			objectScript component = array[i].GetComponent<objectScript>();
			if (component.gekauft)
			{
				int num2 = i * 10;
				array2[num2] = component.myID;
				array2[1 + num2] = component.typ;
				array2[2 + num2] = component.typGhost;
				array2[3 + num2] = component.besetztCharID;
				array2[4 + num2] = component.aufladungenAkt;
				int num3 = i * 10;
				array3[num3] = array[i].transform.position.x;
				array3[1 + num3] = 0f;
				array3[2 + num3] = array[i].transform.position.z;
				array3[3 + num3] = array[i].transform.eulerAngles.y;
				array3[4 + num3] = component.maschieneTimer;
				int num4 = i * 10;
				array4[num4] = component.inUse;
			}
		}
		writer.Write<int[]>("objects_I", array2);
		writer.Write<float[]>("objects_F", array3);
		writer.Write<bool[]>("objects_B", array4);
	}

	
	private void LoadObjects(ES3Reader reader, string filename)
	{
		int num = reader.Read<int>("anzObjects", -1);
		Debug.Log("anzObjects: " + num.ToString());
		if (num <= 0)
		{
			return;
		}
		int[] array = new int[10 * num];
		float[] array2 = new float[10 * num];
		bool[] array3 = new bool[10 * num];
		array = reader.Read<int[]>("objects_I");
		array2 = reader.Read<float[]>("objects_F");
		for (int i = 0; i < num; i++)
		{
			int num2 = i * 10;
			int num3 = i * 10;
			int num4 = i * 10;
			if (array[num2] != 0 && !float.IsNaN(array2[num3]))
			{
				GameObject gameObject = null;
				if (array[1 + num2] != -1)
				{
					gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mapS_.prefabsInventar[array[1 + num2]]);
				}
				if (array[2 + num2] != -1)
				{
					gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mS_.miscGamePrefabs[array[2 + num2]]);
				}
				gameObject.transform.position = new Vector3(array2[num3], 0f, array2[2 + num3]);
				objectScript component = gameObject.GetComponent<objectScript>();
				component.mS_ = this.mS_;
				component.sfx_ = this.sfx_;
				component.tS_ = this.tS_;
				component.mapS_ = this.mapS_;
				component.myID = array[num2];
				component.typ = array[1 + num2];
				component.typGhost = array[2 + num2];
				component.besetztCharID = array[3 + num2];
				component.aufladungenAkt = array[4 + num2];
				component.maschieneTimer = array2[4 + num3];
				component.inUse = array3[num4];
				component.InitObjectFromSavegame();
				this.mS_.objectRotation = array2[3 + num3];
				component.PlatziereObject(new Vector3(array2[num3], 0f, array2[2 + num3]), true, false);
			}
		}
	}

	
	private void SaveRooms(ES3Writer writer)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Room");
		int num = array.Length;
		writer.Write<int>("anzRooms", num);
		new long[20 * num];
		int[] array2 = new int[20 * num];
		float[] array3 = new float[20 * num];
		string[] array4 = new string[10 * num];
		bool[] array5 = new bool[20 * num];
		for (int i = 0; i < array.Length; i++)
		{
			roomScript component = array[i].GetComponent<roomScript>();
			int num2 = i * 20;
			array2[num2] = component.myID;
			array2[1 + num2] = component.typ;
			array2[2 + num2] = component.taskID;
			array2[3 + num2] = component.serverplatzUsed;
			array2[4 + num2] = component.leitenderGamedesigner;
			array2[5 + num2] = component.leitenderTechniker;
			int num3 = i * 20;
			array3[num3] = array[i].transform.position.x;
			array3[1 + num3] = array[i].transform.position.y;
			array3[2 + num3] = array[i].transform.position.z;
			array3[3 + num3] = array[i].transform.eulerAngles.y;
			int num4 = i * 10;
			array4[num4] = component.myName;
			int num5 = i * 20;
			array5[num5] = component.pause;
			array5[1 + num5] = component.lockKI;
			array5[2 + num5] = component.serverDown;
		}
		writer.Write<int[]>("rooms_I", array2);
		writer.Write<float[]>("rooms_F", array3);
		writer.Write<string[]>("rooms_S", array4);
		writer.Write<bool[]>("rooms_B", array5);
		writer.Write<int[,]>("mapRoomID", this.mapS_.mapRoomID);
		writer.Write<int[,]>("mapDoors", this.mapS_.mapDoors);
		writer.Write<int[,]>("mapWindows", this.mapS_.mapWindows);
		bool multiplayer = this.mS_.multiplayer;
	}

	
	private void LoadRooms(ES3Reader reader, string filename)
	{
		int num = reader.Read<int>("anzRooms", -1);
		if (num <= 0)
		{
			return;
		}
		new long[20 * num];
		int[] array = new int[20 * num];
		new float[20 * num];
		string[] array2 = new string[10 * num];
		bool[] array3 = new bool[20 * num];
		array = reader.Read<int[]>("rooms_I");
		reader.Read<float[]>("rooms_F");
		array2 = reader.Read<string[]>("rooms_S");
		array3 = reader.Read<bool[]>("rooms_B");
		this.mapS_.mapRoomID = reader.Read<int[,]>("mapRoomID");
		this.mapS_.mapDoors = reader.Read<int[,]>("mapDoors");
		this.mapS_.mapWindows = reader.Read<int[,]>("mapWindows");
		bool multiplayer = this.mS_.multiplayer;
		for (int i = 0; i < num; i++)
		{
			int num2 = i * 20;
			int num3 = i * 20;
			int num4 = i * 10;
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.brS_.roomMainObject, new Vector3(0f, 0f, 0f), Quaternion.identity);
			roomScript component = gameObject.GetComponent<roomScript>();
			gameObject.name = "Room_" + array[num2];
			component.myID = array[num2];
			component.typ = array[1 + num2];
			component.taskID = array[2 + num2];
			component.serverplatzUsed = array[3 + num2];
			component.leitenderGamedesigner = array[4 + num2];
			component.leitenderTechniker = array[5 + num2];
			component.myName = array2[num4];
			component.pause = array3[num3];
			component.lockKI = array3[1 + num3];
			component.serverDown = array3[2 + num3];
			component.uiPos = this.brS_.FindUiPositionExtern(component.myID);
			gameObject.transform.position = new Vector3(component.uiPos.x, 0f, component.uiPos.z);
			this.SetRoomScripts(array[num2]);
		}
		this.mapS_.CreateWalls(-1);
	}

	
	private void SaveMuell(ES3Writer writer)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Muell");
		int num = array.Length;
		writer.Write<int>("anzMuell", num);
		int[] array2 = new int[5 * num];
		float[] array3 = new float[5 * num];
		for (int i = 0; i < array.Length; i++)
		{
			muellScript component = array[i].GetComponent<muellScript>();
			int num2 = i * 5;
			array2[num2] = component.myGFXSlot;
			int num3 = i * 5;
			array3[num3] = array[i].transform.position.x;
			array3[1 + num3] = array[i].transform.position.y;
			array3[2 + num3] = array[i].transform.position.z;
			array3[3 + num3] = array[i].transform.eulerAngles.y;
		}
		writer.Write<int[]>("muell_I", array2);
		writer.Write<float[]>("muell_F", array3);
	}

	
	private void LoadMuell(ES3Reader reader, string filename)
	{
		int num = reader.Read<int>("anzMuell", -1);
		Debug.Log("Load: (anzMuell) " + num.ToString());
		if (num <= 0)
		{
			return;
		}
		int[] array = new int[5 * num];
		float[] array2 = new float[5 * num];
		array = reader.Read<int[]>("muell_I");
		array2 = reader.Read<float[]>("muell_F");
		for (int i = 0; i < num; i++)
		{
			int num2 = i * 5;
			int num3 = i * 5;
			GameObject gameObject = this.mS_.CreateMuell(array[num2], array[num2]);
			gameObject.transform.position = new Vector3(array2[num3], array2[1 + num3], array2[2 + num3]);
			gameObject.transform.eulerAngles = new Vector3(0f, array2[3 + num3], 0f);
		}
	}

	
	private void SaveTasks(ES3Writer writer)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Task");
		int num = array.Length;
		writer.Write<int>("anzTasks", num);
		int[] array2 = new int[20 * num];
		bool[] array3 = new bool[20 * num];
		float[] array4 = new float[20 * num];
		for (int i = 0; i < array.Length; i++)
		{
			int num2 = i * 20;
			int num3 = i * 20;
			int num4 = i * 20;
			taskForschung component = array[i].GetComponent<taskForschung>();
			if (component)
			{
				array2[num2] = 0;
				array2[1 + num2] = component.myID;
				array2[2 + num2] = component.typ;
				array2[3 + num2] = component.slot;
				array3[num3] = component.automatic;
			}
			taskEngine component2 = array[i].GetComponent<taskEngine>();
			if (component2)
			{
				array2[num2] = 1;
				array2[1 + num2] = component2.myID;
				array2[2 + num2] = component2.engineID;
			}
			taskGame component3 = array[i].GetComponent<taskGame>();
			if (component3)
			{
				array2[num2] = 2;
				array2[1 + num2] = component3.myID;
				array2[2 + num2] = component3.gameID;
				array2[3 + num2] = component3.leitenderDesignerID;
				array3[num3] = component3.randomEvent;
			}
			taskUnterstuetzen component4 = array[i].GetComponent<taskUnterstuetzen>();
			if (component4)
			{
				array2[num2] = 3;
				array2[1 + num2] = component4.myID;
				array2[2 + num2] = component4.roomID;
			}
			taskMarketing component5 = array[i].GetComponent<taskMarketing>();
			if (component5)
			{
				array2[num2] = 4;
				array2[1 + num2] = component5.myID;
				array2[2 + num2] = component5.typ;
				array2[3 + num2] = component5.targetID;
				array2[4 + num2] = component5.kampagne;
				array3[num3] = component5.automatic;
				array3[1 + num3] = component5.stopAutomatic;
				array3[2 + num3] = component5.disableWarten;
				array4[num4] = component5.points;
				array4[1 + num4] = component5.pointsLeft;
			}
			taskTraining component6 = array[i].GetComponent<taskTraining>();
			if (component6)
			{
				array2[num2] = 5;
				array2[1 + num2] = component6.myID;
				array2[2 + num2] = component6.slot;
				array3[num3] = component6.automatic;
				array4[num4] = component6.points;
				array4[1 + num4] = component6.pointsLeft;
			}
			taskContractWork component7 = array[i].GetComponent<taskContractWork>();
			if (component7)
			{
				array2[num2] = 6;
				array2[1 + num2] = component7.myID;
				array2[2 + num2] = component7.contractID;
				array3[num3] = component7.automatic;
				array3[1 + num3] = component7.automaticWait;
				array4[num4] = component7.points;
				array4[1 + num4] = component7.pointsLeft;
			}
			taskUpdate component8 = array[i].GetComponent<taskUpdate>();
			if (component8)
			{
				array2[num2] = 7;
				array2[1 + num2] = component8.myID;
				array2[2 + num2] = component8.targetID;
				array2[3 + num2] = component8.devCosts;
				array2[4 + num2] = component8.pointsGameplay;
				array2[5 + num2] = component8.pointsSound;
				array2[6 + num2] = component8.pointsGrafik;
				array2[7 + num2] = component8.pointsTechnik;
				array2[8 + num2] = component8.pointsBugs;
				array3[num3] = component8.sprachen[0];
				array3[1 + num3] = component8.sprachen[1];
				array3[2 + num3] = component8.sprachen[2];
				array3[3 + num3] = component8.sprachen[3];
				array3[4 + num3] = component8.sprachen[4];
				array3[5 + num3] = component8.sprachen[5];
				array3[6 + num3] = component8.sprachen[6];
				array3[7 + num3] = component8.sprachen[7];
				array3[8 + num3] = component8.sprachen[8];
				array3[9 + num3] = component8.sprachen[9];
				array3[10 + num3] = component8.sprachen[10];
				array3[11 + num3] = component8.automatic;
				array4[num4] = component8.points;
				array4[1 + num4] = component8.pointsLeft;
				array4[2 + num4] = component8.quality;
			}
			taskFankampagne component9 = array[i].GetComponent<taskFankampagne>();
			if (component9)
			{
				array2[num2] = 8;
				array2[1 + num2] = component9.myID;
				array2[2 + num2] = component9.kampagne;
				array3[num3] = component9.automatic;
				array4[num4] = component9.points;
				array4[1 + num4] = component9.pointsLeft;
			}
			taskSupport component10 = array[i].GetComponent<taskSupport>();
			if (component10)
			{
				array2[num2] = 9;
				array2[1 + num2] = component10.myID;
			}
			taskBugfixing component11 = array[i].GetComponent<taskBugfixing>();
			if (component11)
			{
				array2[num2] = 10;
				array2[1 + num2] = component11.myID;
				array2[2 + num2] = component11.targetID;
				array4[num4] = component11.points;
				array4[1 + num4] = component11.pointsLeft;
			}
			taskGameplayVerbessern component12 = array[i].GetComponent<taskGameplayVerbessern>();
			if (component12)
			{
				array2[num2] = 11;
				array2[1 + num2] = component12.myID;
				array2[2 + num2] = component12.targetID;
				array2[3 + num2] = component12.aktuellerAdd;
				array4[num4] = component12.points;
				array4[1 + num4] = component12.pointsLeft;
				array3[num3] = component12.adds[0];
				array3[1 + num3] = component12.adds[1];
				array3[2 + num3] = component12.adds[2];
				array3[3 + num3] = component12.adds[3];
				array3[4 + num3] = component12.adds[4];
				array3[5 + num3] = component12.adds[5];
				array3[6 + num3] = component12.autoBugfix;
			}
			taskGrafikVerbessern component13 = array[i].GetComponent<taskGrafikVerbessern>();
			if (component13)
			{
				array2[num2] = 12;
				array2[1 + num2] = component13.myID;
				array2[2 + num2] = component13.targetID;
				array2[3 + num2] = component13.aktuellerAdd;
				array4[num4] = component13.points;
				array4[1 + num4] = component13.pointsLeft;
				array3[num3] = component13.adds[0];
				array3[1 + num3] = component13.adds[1];
				array3[2 + num3] = component13.adds[2];
				array3[3 + num3] = component13.adds[3];
				array3[4 + num3] = component13.adds[4];
				array3[5 + num3] = component13.adds[5];
			}
			taskSoundVerbessern component14 = array[i].GetComponent<taskSoundVerbessern>();
			if (component14)
			{
				array2[num2] = 13;
				array2[1 + num2] = component14.myID;
				array2[2 + num2] = component14.targetID;
				array2[3 + num2] = component14.aktuellerAdd;
				array4[num4] = component14.points;
				array4[1 + num4] = component14.pointsLeft;
				array3[num3] = component14.adds[0];
				array3[1 + num3] = component14.adds[1];
				array3[2 + num3] = component14.adds[2];
				array3[3 + num3] = component14.adds[3];
				array3[4 + num3] = component14.adds[4];
				array3[5 + num3] = component14.adds[5];
			}
			taskAnimationVerbessern component15 = array[i].GetComponent<taskAnimationVerbessern>();
			if (component15)
			{
				array2[num2] = 14;
				array2[1 + num2] = component15.myID;
				array2[2 + num2] = component15.targetID;
				array2[3 + num2] = component15.aktuellerAdd;
				array4[num4] = component15.points;
				array4[1 + num4] = component15.pointsLeft;
				array3[num3] = component15.adds[0];
				array3[1 + num3] = component15.adds[1];
				array3[2 + num3] = component15.adds[2];
				array3[3 + num3] = component15.adds[3];
				array3[4 + num3] = component15.adds[4];
				array3[5 + num3] = component15.adds[5];
			}
			taskSpielbericht component16 = array[i].GetComponent<taskSpielbericht>();
			if (component16)
			{
				array2[num2] = 15;
				array2[1 + num2] = component16.myID;
				array2[2 + num2] = component16.targetID;
				array4[num4] = component16.points;
				array4[1 + num4] = component16.pointsLeft;
				array3[num3] = component16.automatic;
				array3[1 + num3] = component16.automaticWait;
			}
			taskProduction component17 = array[i].GetComponent<taskProduction>();
			if (component17)
			{
				array2[num2] = 16;
				array2[1 + num2] = component17.myID;
				array2[2 + num2] = component17.targetID;
				array2[3 + num2] = component17.amountStandard;
				array2[4 + num2] = component17.amountDeluxe;
				array2[5 + num2] = component17.amountCollectors;
				array2[6 + num2] = component17.gesamtProduktion;
				array3[num3] = component17.automatic;
			}
			taskMarktforschung component18 = array[i].GetComponent<taskMarktforschung>();
			if (component18)
			{
				array2[num2] = 17;
				array2[1 + num2] = component18.myID;
				array4[num4] = component18.points;
				array4[1 + num4] = component18.pointsLeft;
			}
			taskPolishing component19 = array[i].GetComponent<taskPolishing>();
			if (component19)
			{
				array2[num2] = 18;
				array2[1 + num2] = component19.myID;
				array2[2 + num2] = component19.targetID;
				array4[num4] = component19.points;
				array4[1 + num4] = component19.pointsLeft;
			}
			taskMarketingSpezial component20 = array[i].GetComponent<taskMarketingSpezial>();
			if (component20)
			{
				array2[num2] = 19;
				array2[1 + num2] = component20.myID;
				array2[2 + num2] = component20.targetID;
				array2[3 + num2] = component20.kampagne;
				array4[num4] = component20.points;
				array4[1 + num4] = component20.pointsLeft;
			}
			taskF2PUpdate component21 = array[i].GetComponent<taskF2PUpdate>();
			if (component21)
			{
				array2[num2] = 20;
				array2[1 + num2] = component21.myID;
				array2[2 + num2] = component21.targetID;
				array2[3 + num2] = component21.devCosts;
				array3[num3] = component21.automatic;
				array4[num4] = component21.points;
				array4[1 + num4] = component21.pointsLeft;
				array4[2 + num4] = component21.quality;
			}
			taskArcadeProduction component22 = array[i].GetComponent<taskArcadeProduction>();
			if (component22)
			{
				array2[num2] = 21;
				array2[1 + num2] = component22.myID;
				array2[2 + num2] = component22.targetID;
				array4[num4] = component22.points;
				array4[1 + num4] = component22.pointsLeft;
			}
			taskKonsole component23 = array[i].GetComponent<taskKonsole>();
			if (component23)
			{
				array2[num2] = 22;
				array2[1 + num2] = component23.myID;
				array2[2 + num2] = component23.konsoleID;
				array2[3 + num2] = component23.leitenderTechnikerID;
			}
			taskContractWait component24 = array[i].GetComponent<taskContractWait>();
			if (component24)
			{
				array2[num2] = 23;
				array2[1 + num2] = component24.myID;
				array2[2 + num2] = component24.art;
			}
			taskWait component25 = array[i].GetComponent<taskWait>();
			if (component25)
			{
				array2[num2] = 24;
				array2[1 + num2] = component25.myID;
				array2[2 + num2] = component25.art;
			}
			taskMitarbeitersuche component26 = array[i].GetComponent<taskMitarbeitersuche>();
			if (component26)
			{
				array2[num2] = 25;
				array2[1 + num2] = component26.myID;
				array2[2 + num2] = component26.beruf;
				array2[3 + num2] = component26.berufserfahrung;
				array3[num3] = component26.automatic;
				array4[num4] = component26.points;
				array4[1 + num4] = component26.pointsLeft;
			}
			taskFanshop component27 = array[i].GetComponent<taskFanshop>();
			if (component27)
			{
				array2[num2] = 26;
				array2[1 + num2] = component27.myID;
				array2[2 + num2] = component27.verdienst;
				array2[3 + num2] = component27.bestellungen[0];
				array2[4 + num2] = component27.bestellungen[1];
				array2[5 + num2] = component27.bestellungen[2];
				array2[6 + num2] = component27.bestellungen[3];
				array2[7 + num2] = component27.bestellungen[4];
				array2[8 + num2] = component27.bestellungen[5];
				array2[9 + num2] = component27.bestellungen[6];
				array2[10 + num2] = component27.bestellungen[7];
			}
		}
		writer.Write<int[]>("task_I", array2);
		writer.Write<bool[]>("task_B", array3);
		writer.Write<float[]>("task_F", array4);
	}

	
	private void LoadTasks(ES3Reader reader, string filename)
	{
		int num = reader.Read<int>("anzTasks", -1);
		Debug.Log("Load: (anzTasks) " + num.ToString());
		if (num <= 0)
		{
			return;
		}
		int[] array = new int[20 * num];
		bool[] array2 = new bool[20 * num];
		float[] array3 = new float[20 * num];
		array = reader.Read<int[]>("task_I");
		array2 = reader.Read<bool[]>("task_B");
		array3 = reader.Read<float[]>("task_F");
		for (int i = 0; i < num; i++)
		{
			int num2 = i * 20;
			int num3 = i * 20;
			int num4 = i * 20;
			switch (array[num2])
			{
			case 0:
			{
				taskForschung component = UnityEngine.Object.Instantiate<GameObject>(this.guiMain_.uiTaskPrefabs[0]).GetComponent<taskForschung>();
				component.myID = array[1 + num2];
				component.typ = array[2 + num2];
				component.slot = array[3 + num2];
				component.automatic = array2[num3];
				component.Init(true);
				if (component.myID <= 0)
				{
					UnityEngine.Object.Destroy(component.gameObject);
				}
				break;
			}
			case 1:
			{
				taskEngine component2 = UnityEngine.Object.Instantiate<GameObject>(this.guiMain_.uiTaskPrefabs[1]).GetComponent<taskEngine>();
				component2.myID = array[1 + num2];
				component2.engineID = array[2 + num2];
				component2.Init(true);
				if (component2.myID <= 0)
				{
					UnityEngine.Object.Destroy(component2.gameObject);
				}
				break;
			}
			case 2:
			{
				taskGame component3 = UnityEngine.Object.Instantiate<GameObject>(this.guiMain_.uiTaskPrefabs[2]).GetComponent<taskGame>();
				component3.myID = array[1 + num2];
				component3.gameID = array[2 + num2];
				component3.leitenderDesignerID = array[3 + num2];
				component3.randomEvent = array2[num3];
				component3.Init(true);
				if (component3.myID <= 0)
				{
					UnityEngine.Object.Destroy(component3.gameObject);
				}
				break;
			}
			case 3:
			{
				taskUnterstuetzen component4 = UnityEngine.Object.Instantiate<GameObject>(this.guiMain_.uiTaskPrefabs[3]).GetComponent<taskUnterstuetzen>();
				component4.myID = array[1 + num2];
				component4.roomID = array[2 + num2];
				component4.Init(true);
				if (component4.myID <= 0)
				{
					UnityEngine.Object.Destroy(component4.gameObject);
				}
				break;
			}
			case 4:
			{
				taskMarketing component5 = UnityEngine.Object.Instantiate<GameObject>(this.guiMain_.uiTaskPrefabs[4]).GetComponent<taskMarketing>();
				component5.myID = array[1 + num2];
				component5.typ = array[2 + num2];
				component5.targetID = array[3 + num2];
				component5.kampagne = array[4 + num2];
				component5.automatic = array2[num3];
				component5.stopAutomatic = array2[1 + num3];
				component5.disableWarten = array2[2 + num3];
				component5.points = array3[num4];
				component5.pointsLeft = array3[1 + num4];
				component5.Init(true);
				if (component5.myID <= 0)
				{
					UnityEngine.Object.Destroy(component5.gameObject);
				}
				break;
			}
			case 5:
			{
				taskTraining component6 = UnityEngine.Object.Instantiate<GameObject>(this.guiMain_.uiTaskPrefabs[5]).GetComponent<taskTraining>();
				component6.myID = array[1 + num2];
				component6.slot = array[2 + num2];
				component6.automatic = array2[num3];
				component6.points = array3[num4];
				component6.pointsLeft = array3[1 + num4];
				component6.Init(true);
				if (component6.myID <= 0)
				{
					UnityEngine.Object.Destroy(component6.gameObject);
				}
				break;
			}
			case 6:
			{
				taskContractWork component7 = UnityEngine.Object.Instantiate<GameObject>(this.guiMain_.uiTaskPrefabs[6]).GetComponent<taskContractWork>();
				component7.myID = array[1 + num2];
				component7.contractID = array[2 + num2];
				component7.automatic = array2[num3];
				component7.automaticWait = array2[1 + num3];
				component7.points = array3[num4];
				component7.pointsLeft = array3[1 + num4];
				component7.Init(true);
				if (component7.myID <= 0)
				{
					UnityEngine.Object.Destroy(component7.gameObject);
				}
				break;
			}
			case 7:
			{
				taskUpdate component8 = UnityEngine.Object.Instantiate<GameObject>(this.guiMain_.uiTaskPrefabs[7]).GetComponent<taskUpdate>();
				component8.myID = array[1 + num2];
				component8.targetID = array[2 + num2];
				component8.devCosts = array[3 + num2];
				component8.pointsGameplay = array[4 + num2];
				component8.pointsSound = array[5 + num2];
				component8.pointsGrafik = array[6 + num2];
				component8.pointsTechnik = array[7 + num2];
				component8.pointsBugs = array[8 + num2];
				component8.sprachen[0] = array2[num3];
				component8.sprachen[1] = array2[1 + num3];
				component8.sprachen[2] = array2[2 + num3];
				component8.sprachen[3] = array2[3 + num3];
				component8.sprachen[4] = array2[4 + num3];
				component8.sprachen[5] = array2[5 + num3];
				component8.sprachen[6] = array2[6 + num3];
				component8.sprachen[7] = array2[7 + num3];
				component8.sprachen[8] = array2[8 + num3];
				component8.sprachen[9] = array2[9 + num3];
				component8.sprachen[10] = array2[10 + num3];
				component8.automatic = array2[11 + num3];
				component8.points = array3[num4];
				component8.pointsLeft = array3[1 + num4];
				component8.quality = array3[2 + num4];
				component8.Init(true);
				if (component8.myID <= 0)
				{
					UnityEngine.Object.Destroy(component8.gameObject);
				}
				break;
			}
			case 8:
			{
				taskFankampagne component9 = UnityEngine.Object.Instantiate<GameObject>(this.guiMain_.uiTaskPrefabs[8]).GetComponent<taskFankampagne>();
				component9.myID = array[1 + num2];
				component9.kampagne = array[2 + num2];
				component9.automatic = array2[num3];
				component9.points = array3[num4];
				component9.pointsLeft = array3[1 + num4];
				component9.Init(true);
				if (component9.myID <= 0)
				{
					UnityEngine.Object.Destroy(component9.gameObject);
				}
				break;
			}
			case 9:
			{
				taskSupport component10 = UnityEngine.Object.Instantiate<GameObject>(this.guiMain_.uiTaskPrefabs[9]).GetComponent<taskSupport>();
				component10.myID = array[1 + num2];
				component10.Init(true);
				if (component10.myID <= 0)
				{
					UnityEngine.Object.Destroy(component10.gameObject);
				}
				break;
			}
			case 10:
			{
				taskBugfixing component11 = UnityEngine.Object.Instantiate<GameObject>(this.guiMain_.uiTaskPrefabs[10]).GetComponent<taskBugfixing>();
				component11.myID = array[1 + num2];
				component11.targetID = array[2 + num2];
				component11.points = array3[num4];
				component11.pointsLeft = array3[1 + num4];
				component11.Init(true);
				if (component11.myID <= 0)
				{
					UnityEngine.Object.Destroy(component11.gameObject);
				}
				break;
			}
			case 11:
			{
				taskGameplayVerbessern component12 = UnityEngine.Object.Instantiate<GameObject>(this.guiMain_.uiTaskPrefabs[11]).GetComponent<taskGameplayVerbessern>();
				component12.myID = array[1 + num2];
				component12.targetID = array[2 + num2];
				component12.aktuellerAdd = array[3 + num2];
				component12.points = array3[num4];
				component12.pointsLeft = array3[1 + num4];
				component12.adds[0] = array2[num3];
				component12.adds[1] = array2[1 + num3];
				component12.adds[2] = array2[2 + num3];
				component12.adds[3] = array2[3 + num3];
				component12.adds[4] = array2[4 + num3];
				component12.adds[5] = array2[5 + num3];
				component12.autoBugfix = array2[6 + num3];
				component12.Init(true);
				if (component12.myID <= 0)
				{
					UnityEngine.Object.Destroy(component12.gameObject);
				}
				break;
			}
			case 12:
			{
				taskGrafikVerbessern component13 = UnityEngine.Object.Instantiate<GameObject>(this.guiMain_.uiTaskPrefabs[12]).GetComponent<taskGrafikVerbessern>();
				component13.myID = array[1 + num2];
				component13.targetID = array[2 + num2];
				component13.aktuellerAdd = array[3 + num2];
				component13.points = array3[num4];
				component13.pointsLeft = array3[1 + num4];
				component13.adds[0] = array2[num3];
				component13.adds[1] = array2[1 + num3];
				component13.adds[2] = array2[2 + num3];
				component13.adds[3] = array2[3 + num3];
				component13.adds[4] = array2[4 + num3];
				component13.adds[5] = array2[5 + num3];
				component13.Init(true);
				if (component13.myID <= 0)
				{
					UnityEngine.Object.Destroy(component13.gameObject);
				}
				break;
			}
			case 13:
			{
				taskSoundVerbessern component14 = UnityEngine.Object.Instantiate<GameObject>(this.guiMain_.uiTaskPrefabs[13]).GetComponent<taskSoundVerbessern>();
				component14.myID = array[1 + num2];
				component14.targetID = array[2 + num2];
				component14.aktuellerAdd = array[3 + num2];
				component14.points = array3[num4];
				component14.pointsLeft = array3[1 + num4];
				component14.adds[0] = array2[num3];
				component14.adds[1] = array2[1 + num3];
				component14.adds[2] = array2[2 + num3];
				component14.adds[3] = array2[3 + num3];
				component14.adds[4] = array2[4 + num3];
				component14.adds[5] = array2[5 + num3];
				component14.Init(true);
				if (component14.myID <= 0)
				{
					UnityEngine.Object.Destroy(component14.gameObject);
				}
				break;
			}
			case 14:
			{
				taskAnimationVerbessern component15 = UnityEngine.Object.Instantiate<GameObject>(this.guiMain_.uiTaskPrefabs[14]).GetComponent<taskAnimationVerbessern>();
				component15.myID = array[1 + num2];
				component15.targetID = array[2 + num2];
				component15.aktuellerAdd = array[3 + num2];
				component15.points = array3[num4];
				component15.pointsLeft = array3[1 + num4];
				component15.adds[0] = array2[num3];
				component15.adds[1] = array2[1 + num3];
				component15.adds[2] = array2[2 + num3];
				component15.adds[3] = array2[3 + num3];
				component15.adds[4] = array2[4 + num3];
				component15.adds[5] = array2[5 + num3];
				component15.Init(true);
				if (component15.myID <= 0)
				{
					UnityEngine.Object.Destroy(component15.gameObject);
				}
				break;
			}
			case 15:
			{
				taskSpielbericht component16 = UnityEngine.Object.Instantiate<GameObject>(this.guiMain_.uiTaskPrefabs[15]).GetComponent<taskSpielbericht>();
				component16.myID = array[1 + num2];
				component16.targetID = array[2 + num2];
				component16.points = array3[num4];
				component16.pointsLeft = array3[1 + num4];
				component16.automatic = array2[num3];
				component16.automaticWait = array2[1 + num3];
				component16.Init(true);
				if (component16.myID <= 0)
				{
					UnityEngine.Object.Destroy(component16.gameObject);
				}
				break;
			}
			case 16:
			{
				taskProduction component17 = UnityEngine.Object.Instantiate<GameObject>(this.guiMain_.uiTaskPrefabs[16]).GetComponent<taskProduction>();
				component17.myID = array[1 + num2];
				component17.targetID = array[2 + num2];
				component17.amountStandard = array[3 + num2];
				component17.amountDeluxe = array[4 + num2];
				component17.amountCollectors = array[5 + num2];
				component17.gesamtProduktion = array[6 + num2];
				component17.automatic = array2[num3];
				component17.Init(true);
				if (component17.myID <= 0)
				{
					UnityEngine.Object.Destroy(component17.gameObject);
				}
				break;
			}
			case 17:
			{
				taskMarktforschung component18 = UnityEngine.Object.Instantiate<GameObject>(this.guiMain_.uiTaskPrefabs[17]).GetComponent<taskMarktforschung>();
				component18.myID = array[1 + num2];
				component18.points = array3[num4];
				component18.pointsLeft = array3[1 + num4];
				component18.Init(true);
				if (component18.myID <= 0)
				{
					UnityEngine.Object.Destroy(component18.gameObject);
				}
				break;
			}
			case 18:
			{
				taskPolishing component19 = UnityEngine.Object.Instantiate<GameObject>(this.guiMain_.uiTaskPrefabs[18]).GetComponent<taskPolishing>();
				component19.myID = array[1 + num2];
				component19.targetID = array[2 + num2];
				component19.points = array3[num4];
				component19.pointsLeft = array3[1 + num4];
				component19.Init(true);
				if (component19.myID <= 0)
				{
					UnityEngine.Object.Destroy(component19.gameObject);
				}
				break;
			}
			case 19:
			{
				taskMarketingSpezial component20 = UnityEngine.Object.Instantiate<GameObject>(this.guiMain_.uiTaskPrefabs[19]).GetComponent<taskMarketingSpezial>();
				component20.myID = array[1 + num2];
				component20.targetID = array[2 + num2];
				component20.kampagne = array[3 + num2];
				component20.points = array3[num4];
				component20.pointsLeft = array3[1 + num4];
				component20.Init(true);
				if (component20.myID <= 0)
				{
					UnityEngine.Object.Destroy(component20.gameObject);
				}
				break;
			}
			case 20:
			{
				taskF2PUpdate component21 = UnityEngine.Object.Instantiate<GameObject>(this.guiMain_.uiTaskPrefabs[20]).GetComponent<taskF2PUpdate>();
				component21.myID = array[1 + num2];
				component21.targetID = array[2 + num2];
				component21.devCosts = array[3 + num2];
				component21.automatic = array2[num3];
				component21.points = array3[num4];
				component21.pointsLeft = array3[1 + num4];
				component21.quality = array3[2 + num4];
				component21.Init(true);
				if (component21.myID <= 0)
				{
					UnityEngine.Object.Destroy(component21.gameObject);
				}
				break;
			}
			case 21:
			{
				taskArcadeProduction component22 = UnityEngine.Object.Instantiate<GameObject>(this.guiMain_.uiTaskPrefabs[21]).GetComponent<taskArcadeProduction>();
				component22.myID = array[1 + num2];
				component22.targetID = array[2 + num2];
				component22.points = array3[num4];
				component22.pointsLeft = array3[1 + num4];
				component22.Init(true);
				if (component22.myID <= 0)
				{
					UnityEngine.Object.Destroy(component22.gameObject);
				}
				break;
			}
			case 22:
			{
				taskKonsole component23 = UnityEngine.Object.Instantiate<GameObject>(this.guiMain_.uiTaskPrefabs[22]).GetComponent<taskKonsole>();
				component23.myID = array[1 + num2];
				component23.konsoleID = array[2 + num2];
				component23.leitenderTechnikerID = array[3 + num2];
				component23.Init(true);
				if (component23.myID <= 0)
				{
					UnityEngine.Object.Destroy(component23.gameObject);
				}
				break;
			}
			case 23:
			{
				taskContractWait component24 = UnityEngine.Object.Instantiate<GameObject>(this.guiMain_.uiTaskPrefabs[23]).GetComponent<taskContractWait>();
				component24.myID = array[1 + num2];
				component24.art = array[2 + num2];
				component24.Init(true);
				if (component24.myID <= 0)
				{
					UnityEngine.Object.Destroy(component24.gameObject);
				}
				break;
			}
			case 24:
			{
				taskWait component25 = UnityEngine.Object.Instantiate<GameObject>(this.guiMain_.uiTaskPrefabs[24]).GetComponent<taskWait>();
				component25.myID = array[1 + num2];
				component25.art = array[2 + num2];
				component25.Init(true);
				if (component25.myID <= 0)
				{
					UnityEngine.Object.Destroy(component25.gameObject);
				}
				break;
			}
			case 25:
			{
				taskMitarbeitersuche component26 = UnityEngine.Object.Instantiate<GameObject>(this.guiMain_.uiTaskPrefabs[25]).GetComponent<taskMitarbeitersuche>();
				component26.myID = array[1 + num2];
				component26.beruf = array[2 + num2];
				component26.berufserfahrung = array[3 + num2];
				component26.automatic = array2[num3];
				component26.points = array3[num4];
				component26.pointsLeft = array3[1 + num4];
				component26.Init(true);
				if (component26.myID <= 0)
				{
					UnityEngine.Object.Destroy(component26.gameObject);
				}
				break;
			}
			case 26:
			{
				taskFanshop component27 = UnityEngine.Object.Instantiate<GameObject>(this.guiMain_.uiTaskPrefabs[26]).GetComponent<taskFanshop>();
				component27.myID = array[1 + num2];
				component27.verdienst = array[2 + num2];
				component27.bestellungen[0] = array[3 + num2];
				component27.bestellungen[1] = array[4 + num2];
				component27.bestellungen[2] = array[5 + num2];
				component27.bestellungen[3] = array[6 + num2];
				component27.bestellungen[4] = array[7 + num2];
				component27.bestellungen[5] = array[8 + num2];
				component27.bestellungen[6] = array[9 + num2];
				component27.bestellungen[7] = array[10 + num2];
				component27.Init(true);
				if (component27.myID <= 0)
				{
					UnityEngine.Object.Destroy(component27.gameObject);
				}
				break;
			}
			}
		}
	}

	
	private void SaveHardware(ES3Writer writer)
	{
		writer.Write<int[]>("hardware_TYP", this.hardware_.hardware_TYP);
		writer.Write<int[]>("hardware_RES_POINTS", this.hardware_.hardware_RES_POINTS);
		writer.Write<float[]>("hardware_RES_POINTS_LEFT", this.hardware_.hardware_RES_POINTS_LEFT);
		writer.Write<int[]>("hardware_PRICE", this.hardware_.hardware_PRICE);
		writer.Write<int[]>("hardware_DEV_COSTS", this.hardware_.hardware_DEV_COSTS);
		writer.Write<int[]>("hardware_TECH", this.hardware_.hardware_TECH);
		writer.Write<int[]>("hardware_DATE_YEAR", this.hardware_.hardware_DATE_YEAR);
		writer.Write<int[]>("hardware_DATE_MONTH", this.hardware_.hardware_DATE_MONTH);
		writer.Write<bool[]>("hardware_UNLOCK", this.hardware_.hardware_UNLOCK);
		writer.Write<string[]>("hardware_ICONFILE", this.hardware_.hardware_ICONFILE);
		writer.Write<int[]>("hardware_NEED1", this.hardware_.hardware_NEED1);
		writer.Write<int[]>("hardware_NEED2", this.hardware_.hardware_NEED2);
		writer.Write<bool[]>("hardware_ONLYSTATIONARY", this.hardware_.hardware_ONLYSTATIONARY);
		writer.Write<bool[]>("hardware_ONLYHANDHELD", this.hardware_.hardware_ONLYHANDHELD);
		writer.Write<string[]>("hardware_NAME_EN", this.hardware_.hardware_NAME_EN);
		writer.Write<string[]>("hardware_NAME_GE", this.hardware_.hardware_NAME_GE);
		writer.Write<string[]>("hardware_NAME_TU", this.hardware_.hardware_NAME_TU);
		writer.Write<string[]>("hardware_NAME_CH", this.hardware_.hardware_NAME_CH);
		writer.Write<string[]>("hardware_NAME_FR", this.hardware_.hardware_NAME_FR);
		writer.Write<string[]>("hardware_NAME_PB", this.hardware_.hardware_NAME_PB);
		writer.Write<string[]>("hardware_NAME_CT", this.hardware_.hardware_NAME_CT);
		writer.Write<string[]>("hardware_NAME_HU", this.hardware_.hardware_NAME_HU);
		writer.Write<string[]>("hardware_NAME_ES", this.hardware_.hardware_NAME_ES);
		writer.Write<string[]>("hardware_NAME_CZ", this.hardware_.hardware_NAME_CZ);
		writer.Write<string[]>("hardware_NAME_KO", this.hardware_.hardware_NAME_KO);
		writer.Write<string[]>("hardware_NAME_AR", this.hardware_.hardware_NAME_AR);
		writer.Write<string[]>("hardware_NAME_RU", this.hardware_.hardware_NAME_RU);
		writer.Write<string[]>("hardware_NAME_IT", this.hardware_.hardware_NAME_IT);
		writer.Write<string[]>("hardware_NAME_JA", this.hardware_.hardware_NAME_JA);
		writer.Write<string[]>("hardware_NAME_PL", this.hardware_.hardware_NAME_PL);
		writer.Write<string[]>("hardware_DESC_EN", this.hardware_.hardware_DESC_EN);
		writer.Write<string[]>("hardware_DESC_GE", this.hardware_.hardware_DESC_GE);
		writer.Write<string[]>("hardware_DESC_TU", this.hardware_.hardware_DESC_TU);
		writer.Write<string[]>("hardware_DESC_CH", this.hardware_.hardware_DESC_CH);
		writer.Write<string[]>("hardware_DESC_FR", this.hardware_.hardware_DESC_FR);
		writer.Write<string[]>("hardware_DESC_PB", this.hardware_.hardware_DESC_PB);
		writer.Write<string[]>("hardware_DESC_CT", this.hardware_.hardware_DESC_CT);
		writer.Write<string[]>("hardware_DESC_HU", this.hardware_.hardware_DESC_HU);
		writer.Write<string[]>("hardware_DESC_ES", this.hardware_.hardware_DESC_ES);
		writer.Write<string[]>("hardware_DESC_CZ", this.hardware_.hardware_DESC_CZ);
		writer.Write<string[]>("hardware_DESC_KO", this.hardware_.hardware_DESC_KO);
		writer.Write<string[]>("hardware_DESC_AR", this.hardware_.hardware_DESC_AR);
		writer.Write<string[]>("hardware_DESC_RU", this.hardware_.hardware_DESC_RU);
		writer.Write<string[]>("hardware_DESC_IT", this.hardware_.hardware_DESC_IT);
		writer.Write<string[]>("hardware_DESC_JA", this.hardware_.hardware_DESC_JA);
		writer.Write<string[]>("hardware_DESC_PL", this.hardware_.hardware_DESC_PL);
	}

	
	private void LoadHardware(ES3Reader reader, string filename)
	{
		if (this.mS_.savegameVersion >= 10)
		{
			this.hardware_.hardware_TYP = reader.Read<int[]>("hardware_TYP");
			this.hardware_.hardware_RES_POINTS = reader.Read<int[]>("hardware_RES_POINTS");
			this.hardware_.hardware_RES_POINTS_LEFT = reader.Read<float[]>("hardware_RES_POINTS_LEFT");
			this.hardware_.hardware_PRICE = reader.Read<int[]>("hardware_PRICE");
			this.hardware_.hardware_DEV_COSTS = reader.Read<int[]>("hardware_DEV_COSTS");
			this.hardware_.hardware_TECH = reader.Read<int[]>("hardware_TECH");
			this.hardware_.hardware_DATE_YEAR = reader.Read<int[]>("hardware_DATE_YEAR");
			this.hardware_.hardware_DATE_MONTH = reader.Read<int[]>("hardware_DATE_MONTH");
			this.hardware_.hardware_UNLOCK = reader.Read<bool[]>("hardware_UNLOCK");
			this.hardware_.hardware_ICONFILE = reader.Read<string[]>("hardware_ICONFILE");
			this.hardware_.hardware_NEED1 = reader.Read<int[]>("hardware_NEED1");
			this.hardware_.hardware_NEED2 = reader.Read<int[]>("hardware_NEED2");
			this.hardware_.hardware_ONLYSTATIONARY = reader.Read<bool[]>("hardware_ONLYSTATIONARY");
			this.hardware_.hardware_ONLYHANDHELD = reader.Read<bool[]>("hardware_ONLYHANDHELD");
			if (this.key_EN)
			{
				this.hardware_.hardware_NAME_EN = reader.Read<string[]>("hardware_NAME_EN");
			}
			if (this.key_GE)
			{
				this.hardware_.hardware_NAME_GE = reader.Read<string[]>("hardware_NAME_GE");
			}
			if (this.key_TU)
			{
				this.hardware_.hardware_NAME_TU = reader.Read<string[]>("hardware_NAME_TU");
			}
			if (this.key_CH)
			{
				this.hardware_.hardware_NAME_CH = reader.Read<string[]>("hardware_NAME_CH");
			}
			if (this.key_FR)
			{
				this.hardware_.hardware_NAME_FR = reader.Read<string[]>("hardware_NAME_FR");
			}
			if (this.key_PB)
			{
				this.hardware_.hardware_NAME_PB = reader.Read<string[]>("hardware_NAME_PB");
			}
			if (this.key_CT)
			{
				this.hardware_.hardware_NAME_CT = reader.Read<string[]>("hardware_NAME_CT");
			}
			if (this.key_HU)
			{
				this.hardware_.hardware_NAME_HU = reader.Read<string[]>("hardware_NAME_HU");
			}
			if (this.key_ES)
			{
				this.hardware_.hardware_NAME_ES = reader.Read<string[]>("hardware_NAME_ES");
			}
			if (this.key_CZ)
			{
				this.hardware_.hardware_NAME_CZ = reader.Read<string[]>("hardware_NAME_CZ");
			}
			if (this.key_KO)
			{
				this.hardware_.hardware_NAME_KO = reader.Read<string[]>("hardware_NAME_KO");
			}
			if (this.key_AR)
			{
				this.hardware_.hardware_NAME_AR = reader.Read<string[]>("hardware_NAME_AR");
			}
			if (this.key_RU)
			{
				this.hardware_.hardware_NAME_RU = reader.Read<string[]>("hardware_NAME_RU");
			}
			if (this.key_IT)
			{
				this.hardware_.hardware_NAME_IT = reader.Read<string[]>("hardware_NAME_IT");
			}
			if (this.key_JA)
			{
				this.hardware_.hardware_NAME_JA = reader.Read<string[]>("hardware_NAME_JA");
			}
			if (this.key_PL)
			{
				this.hardware_.hardware_NAME_PL = reader.Read<string[]>("hardware_NAME_PL");
			}
			if (this.key_EN)
			{
				this.hardware_.hardware_DESC_EN = reader.Read<string[]>("hardware_DESC_EN");
			}
			if (this.key_GE)
			{
				this.hardware_.hardware_DESC_GE = reader.Read<string[]>("hardware_DESC_GE");
			}
			if (this.key_TU)
			{
				this.hardware_.hardware_DESC_TU = reader.Read<string[]>("hardware_DESC_TU");
			}
			if (this.key_CH)
			{
				this.hardware_.hardware_DESC_CH = reader.Read<string[]>("hardware_DESC_CH");
			}
			if (this.key_FR)
			{
				this.hardware_.hardware_DESC_FR = reader.Read<string[]>("hardware_DESC_FR");
			}
			if (this.key_PB)
			{
				this.hardware_.hardware_DESC_PB = reader.Read<string[]>("hardware_DESC_PB");
			}
			if (this.key_CT)
			{
				this.hardware_.hardware_DESC_CT = reader.Read<string[]>("hardware_DESC_CT");
			}
			if (this.key_HU)
			{
				this.hardware_.hardware_DESC_HU = reader.Read<string[]>("hardware_DESC_HU");
			}
			if (this.key_ES)
			{
				this.hardware_.hardware_DESC_ES = reader.Read<string[]>("hardware_DESC_ES");
			}
			if (this.key_CZ)
			{
				this.hardware_.hardware_DESC_CZ = reader.Read<string[]>("hardware_DESC_CZ");
			}
			if (this.key_KO)
			{
				this.hardware_.hardware_DESC_KO = reader.Read<string[]>("hardware_DESC_KO");
			}
			if (this.key_AR)
			{
				this.hardware_.hardware_DESC_AR = reader.Read<string[]>("hardware_DESC_AR");
			}
			if (this.key_RU)
			{
				this.hardware_.hardware_DESC_RU = reader.Read<string[]>("hardware_DESC_RU");
			}
			if (this.key_IT)
			{
				this.hardware_.hardware_DESC_IT = reader.Read<string[]>("hardware_DESC_IT");
			}
			if (this.key_JA)
			{
				this.hardware_.hardware_DESC_JA = reader.Read<string[]>("hardware_DESC_JA");
			}
			if (this.key_PL)
			{
				this.hardware_.hardware_DESC_PL = reader.Read<string[]>("hardware_DESC_PL");
			}
			this.hardware_.Init();
		}
	}

	
	private void SaveHardwareFeatures(ES3Writer writer)
	{
		writer.Write<int[]>("hardFeat_RES_POINTS", this.hardwareFeatures_.hardFeat_RES_POINTS);
		writer.Write<float[]>("hardFeat_RES_POINTS_LEFT", this.hardwareFeatures_.hardFeat_RES_POINTS_LEFT);
		writer.Write<int[]>("hardFeat_PRICE", this.hardwareFeatures_.hardFeat_PRICE);
		writer.Write<int[]>("hardFeat_DEV_COSTS", this.hardwareFeatures_.hardFeat_DEV_COSTS);
		writer.Write<int[]>("hardFeat_DATE_YEAR", this.hardwareFeatures_.hardFeat_DATE_YEAR);
		writer.Write<int[]>("hardFeat_DATE_MONTH", this.hardwareFeatures_.hardFeat_DATE_MONTH);
		writer.Write<bool[]>("hardFeat_UNLOCK", this.hardwareFeatures_.hardFeat_UNLOCK);
		writer.Write<string[]>("hardFeat_ICONFILE", this.hardwareFeatures_.hardFeat_ICONFILE);
		writer.Write<bool[]>("hardFeat_ONLYSTATIONARY", this.hardwareFeatures_.hardFeat_ONLYSTATIONARY);
		writer.Write<bool[]>("hardFeat_ONLYHANDHELD", this.hardwareFeatures_.hardFeat_ONLYHANDHELD);
		writer.Write<bool[]>("hardFeat_NEEDINTERNET", this.hardwareFeatures_.hardFeat_NEEDINTERNET);
		writer.Write<float[]>("hardFeat_QUALITY", this.hardwareFeatures_.hardFeat_QUALITY);
		writer.Write<string[]>("hardFeat_NAME_EN", this.hardwareFeatures_.hardFeat_NAME_EN);
		writer.Write<string[]>("hardFeat_NAME_GE", this.hardwareFeatures_.hardFeat_NAME_GE);
		writer.Write<string[]>("hardFeat_NAME_TU", this.hardwareFeatures_.hardFeat_NAME_TU);
		writer.Write<string[]>("hardFeat_NAME_CH", this.hardwareFeatures_.hardFeat_NAME_CH);
		writer.Write<string[]>("hardFeat_NAME_FR", this.hardwareFeatures_.hardFeat_NAME_FR);
		writer.Write<string[]>("hardFeat_NAME_PB", this.hardwareFeatures_.hardFeat_NAME_PB);
		writer.Write<string[]>("hardFeat_NAME_CT", this.hardwareFeatures_.hardFeat_NAME_CT);
		writer.Write<string[]>("hardFeat_NAME_HU", this.hardwareFeatures_.hardFeat_NAME_HU);
		writer.Write<string[]>("hardFeat_NAME_ES", this.hardwareFeatures_.hardFeat_NAME_ES);
		writer.Write<string[]>("hardFeat_NAME_CZ", this.hardwareFeatures_.hardFeat_NAME_CZ);
		writer.Write<string[]>("hardFeat_NAME_KO", this.hardwareFeatures_.hardFeat_NAME_KO);
		writer.Write<string[]>("hardFeat_NAME_AR", this.hardwareFeatures_.hardFeat_NAME_AR);
		writer.Write<string[]>("hardFeat_NAME_RU", this.hardwareFeatures_.hardFeat_NAME_RU);
		writer.Write<string[]>("hardFeat_NAME_IT", this.hardwareFeatures_.hardFeat_NAME_IT);
		writer.Write<string[]>("hardFeat_NAME_JA", this.hardwareFeatures_.hardFeat_NAME_JA);
		writer.Write<string[]>("hardFeat_NAME_PL", this.hardwareFeatures_.hardFeat_NAME_PL);
		writer.Write<string[]>("hardFeat_DESC_EN", this.hardwareFeatures_.hardFeat_DESC_EN);
		writer.Write<string[]>("hardFeat_DESC_GE", this.hardwareFeatures_.hardFeat_DESC_GE);
		writer.Write<string[]>("hardFeat_DESC_TU", this.hardwareFeatures_.hardFeat_DESC_TU);
		writer.Write<string[]>("hardFeat_DESC_CH", this.hardwareFeatures_.hardFeat_DESC_CH);
		writer.Write<string[]>("hardFeat_DESC_FR", this.hardwareFeatures_.hardFeat_DESC_FR);
		writer.Write<string[]>("hardFeat_DESC_PB", this.hardwareFeatures_.hardFeat_DESC_PB);
		writer.Write<string[]>("hardFeat_DESC_CT", this.hardwareFeatures_.hardFeat_DESC_CT);
		writer.Write<string[]>("hardFeat_DESC_HU", this.hardwareFeatures_.hardFeat_DESC_HU);
		writer.Write<string[]>("hardFeat_DESC_ES", this.hardwareFeatures_.hardFeat_DESC_ES);
		writer.Write<string[]>("hardFeat_DESC_CZ", this.hardwareFeatures_.hardFeat_DESC_CZ);
		writer.Write<string[]>("hardFeat_DESC_KO", this.hardwareFeatures_.hardFeat_DESC_KO);
		writer.Write<string[]>("hardFeat_DESC_AR", this.hardwareFeatures_.hardFeat_DESC_AR);
		writer.Write<string[]>("hardFeat_DESC_RU", this.hardwareFeatures_.hardFeat_DESC_RU);
		writer.Write<string[]>("hardFeat_DESC_IT", this.hardwareFeatures_.hardFeat_DESC_IT);
		writer.Write<string[]>("hardFeat_DESC_JA", this.hardwareFeatures_.hardFeat_DESC_JA);
		writer.Write<string[]>("hardFeat_DESC_PL", this.hardwareFeatures_.hardFeat_DESC_PL);
	}

	
	private void LoadHardwareFeatures(ES3Reader reader, string filename)
	{
		if (this.mS_.savegameVersion >= 11)
		{
			this.hardwareFeatures_.hardFeat_RES_POINTS = reader.Read<int[]>("hardFeat_RES_POINTS");
			this.hardwareFeatures_.hardFeat_RES_POINTS_LEFT = reader.Read<float[]>("hardFeat_RES_POINTS_LEFT");
			this.hardwareFeatures_.hardFeat_PRICE = reader.Read<int[]>("hardFeat_PRICE");
			this.hardwareFeatures_.hardFeat_DEV_COSTS = reader.Read<int[]>("hardFeat_DEV_COSTS");
			this.hardwareFeatures_.hardFeat_DATE_YEAR = reader.Read<int[]>("hardFeat_DATE_YEAR");
			this.hardwareFeatures_.hardFeat_DATE_MONTH = reader.Read<int[]>("hardFeat_DATE_MONTH");
			this.hardwareFeatures_.hardFeat_UNLOCK = reader.Read<bool[]>("hardFeat_UNLOCK");
			this.hardwareFeatures_.hardFeat_ICONFILE = reader.Read<string[]>("hardFeat_ICONFILE");
			this.hardwareFeatures_.hardFeat_ONLYSTATIONARY = reader.Read<bool[]>("hardFeat_ONLYSTATIONARY");
			this.hardwareFeatures_.hardFeat_ONLYHANDHELD = reader.Read<bool[]>("hardFeat_ONLYHANDHELD");
			this.hardwareFeatures_.hardFeat_NEEDINTERNET = reader.Read<bool[]>("hardFeat_NEEDINTERNET");
			this.hardwareFeatures_.hardFeat_QUALITY = reader.Read<float[]>("hardFeat_QUALITY");
			if (this.key_EN)
			{
				this.hardwareFeatures_.hardFeat_NAME_EN = reader.Read<string[]>("hardFeat_NAME_EN");
			}
			if (this.key_GE)
			{
				this.hardwareFeatures_.hardFeat_NAME_GE = reader.Read<string[]>("hardFeat_NAME_GE");
			}
			if (this.key_TU)
			{
				this.hardwareFeatures_.hardFeat_NAME_TU = reader.Read<string[]>("hardFeat_NAME_TU");
			}
			if (this.key_CH)
			{
				this.hardwareFeatures_.hardFeat_NAME_CH = reader.Read<string[]>("hardFeat_NAME_CH");
			}
			if (this.key_FR)
			{
				this.hardwareFeatures_.hardFeat_NAME_FR = reader.Read<string[]>("hardFeat_NAME_FR");
			}
			if (this.key_PB)
			{
				this.hardwareFeatures_.hardFeat_NAME_PB = reader.Read<string[]>("hardFeat_NAME_PB");
			}
			if (this.key_CT)
			{
				this.hardwareFeatures_.hardFeat_NAME_CT = reader.Read<string[]>("hardFeat_NAME_CT");
			}
			if (this.key_HU)
			{
				this.hardwareFeatures_.hardFeat_NAME_HU = reader.Read<string[]>("hardFeat_NAME_HU");
			}
			if (this.key_ES)
			{
				this.hardwareFeatures_.hardFeat_NAME_ES = reader.Read<string[]>("hardFeat_NAME_ES");
			}
			if (this.key_CZ)
			{
				this.hardwareFeatures_.hardFeat_NAME_CZ = reader.Read<string[]>("hardFeat_NAME_CZ");
			}
			if (this.key_KO)
			{
				this.hardwareFeatures_.hardFeat_NAME_KO = reader.Read<string[]>("hardFeat_NAME_KO");
			}
			if (this.key_AR)
			{
				this.hardwareFeatures_.hardFeat_NAME_AR = reader.Read<string[]>("hardFeat_NAME_AR");
			}
			if (this.key_RU)
			{
				this.hardwareFeatures_.hardFeat_NAME_RU = reader.Read<string[]>("hardFeat_NAME_RU");
			}
			if (this.key_IT)
			{
				this.hardwareFeatures_.hardFeat_NAME_IT = reader.Read<string[]>("hardFeat_NAME_IT");
			}
			if (this.key_JA)
			{
				this.hardwareFeatures_.hardFeat_NAME_JA = reader.Read<string[]>("hardFeat_NAME_JA");
			}
			if (this.key_PL)
			{
				this.hardwareFeatures_.hardFeat_NAME_PL = reader.Read<string[]>("hardFeat_NAME_PL");
			}
			if (this.key_EN)
			{
				this.hardwareFeatures_.hardFeat_DESC_EN = reader.Read<string[]>("hardFeat_DESC_EN");
			}
			if (this.key_GE)
			{
				this.hardwareFeatures_.hardFeat_DESC_GE = reader.Read<string[]>("hardFeat_DESC_GE");
			}
			if (this.key_TU)
			{
				this.hardwareFeatures_.hardFeat_DESC_TU = reader.Read<string[]>("hardFeat_DESC_TU");
			}
			if (this.key_CH)
			{
				this.hardwareFeatures_.hardFeat_DESC_CH = reader.Read<string[]>("hardFeat_DESC_CH");
			}
			if (this.key_FR)
			{
				this.hardwareFeatures_.hardFeat_DESC_FR = reader.Read<string[]>("hardFeat_DESC_FR");
			}
			if (this.key_PB)
			{
				this.hardwareFeatures_.hardFeat_DESC_PB = reader.Read<string[]>("hardFeat_DESC_PB");
			}
			if (this.key_CT)
			{
				this.hardwareFeatures_.hardFeat_DESC_CT = reader.Read<string[]>("hardFeat_DESC_CT");
			}
			if (this.key_HU)
			{
				this.hardwareFeatures_.hardFeat_DESC_HU = reader.Read<string[]>("hardFeat_DESC_HU");
			}
			if (this.key_ES)
			{
				this.hardwareFeatures_.hardFeat_DESC_ES = reader.Read<string[]>("hardFeat_DESC_ES");
			}
			if (this.key_CZ)
			{
				this.hardwareFeatures_.hardFeat_DESC_CZ = reader.Read<string[]>("hardFeat_DESC_CZ");
			}
			if (this.key_KO)
			{
				this.hardwareFeatures_.hardFeat_DESC_KO = reader.Read<string[]>("hardFeat_DESC_KO");
			}
			if (this.key_AR)
			{
				this.hardwareFeatures_.hardFeat_DESC_AR = reader.Read<string[]>("hardFeat_DESC_AR");
			}
			if (this.key_RU)
			{
				this.hardwareFeatures_.hardFeat_DESC_RU = reader.Read<string[]>("hardFeat_DESC_RU");
			}
			if (this.key_IT)
			{
				this.hardwareFeatures_.hardFeat_DESC_IT = reader.Read<string[]>("hardFeat_DESC_IT");
			}
			if (this.key_JA)
			{
				this.hardwareFeatures_.hardFeat_DESC_JA = reader.Read<string[]>("hardFeat_DESC_JA");
			}
			if (this.key_PL)
			{
				this.hardwareFeatures_.hardFeat_DESC_PL = reader.Read<string[]>("hardFeat_DESC_PL");
			}
			this.hardwareFeatures_.Init();
		}
	}

	
	private void SaveEngineFeatures(ES3Writer writer)
	{
		writer.Write<int[]>("engineFeatures_TYP", this.eF_.engineFeatures_TYP);
		writer.Write<int[]>("engineFeatures_RES_POINTS", this.eF_.engineFeatures_RES_POINTS);
		writer.Write<float[]>("engineFeatures_RES_POINTS_LEFT", this.eF_.engineFeatures_RES_POINTS_LEFT);
		writer.Write<int[]>("engineFeatures_PRICE", this.eF_.engineFeatures_PRICE);
		writer.Write<int[]>("engineFeatures_DEV_COSTS", this.eF_.engineFeatures_DEV_COSTS);
		writer.Write<int[]>("engineFeatures_TECH", this.eF_.engineFeatures_TECH);
		writer.Write<int[]>("engineFeatures_DATE_YEAR", this.eF_.engineFeatures_DATE_YEAR);
		writer.Write<int[]>("engineFeatures_DATE_MONTH", this.eF_.engineFeatures_DATE_MONTH);
		writer.Write<int[]>("engineFeatures_GAMEPLAY", this.eF_.engineFeatures_GAMEPLAY);
		writer.Write<int[]>("engineFeatures_GRAPHIC", this.eF_.engineFeatures_GRAPHIC);
		writer.Write<int[]>("engineFeatures_SOUND", this.eF_.engineFeatures_SOUND);
		writer.Write<int[]>("engineFeatures_TECHNIK", this.eF_.engineFeatures_TECHNIK);
		writer.Write<int[]>("engineFeatures_LEVEL", this.eF_.engineFeatures_LEVEL);
		writer.Write<bool[]>("engineFeatures_UNLOCK", this.eF_.engineFeatures_UNLOCK);
		writer.Write<string[]>("engineFeatures_ICONFILE", this.eF_.engineFeatures_ICONFILE);
		writer.Write<string[]>("engineFeatures_NAME_EN", this.eF_.engineFeatures_NAME_EN);
		writer.Write<string[]>("engineFeatures_NAME_GE", this.eF_.engineFeatures_NAME_GE);
		writer.Write<string[]>("engineFeatures_NAME_TU", this.eF_.engineFeatures_NAME_TU);
		writer.Write<string[]>("engineFeatures_NAME_CH", this.eF_.engineFeatures_NAME_CH);
		writer.Write<string[]>("engineFeatures_NAME_FR", this.eF_.engineFeatures_NAME_FR);
		writer.Write<string[]>("engineFeatures_NAME_PB", this.eF_.engineFeatures_NAME_PB);
		writer.Write<string[]>("engineFeatures_NAME_CT", this.eF_.engineFeatures_NAME_CT);
		writer.Write<string[]>("engineFeatures_NAME_HU", this.eF_.engineFeatures_NAME_HU);
		writer.Write<string[]>("engineFeatures_NAME_ES", this.eF_.engineFeatures_NAME_ES);
		writer.Write<string[]>("engineFeatures_NAME_CZ", this.eF_.engineFeatures_NAME_CZ);
		writer.Write<string[]>("engineFeatures_NAME_KO", this.eF_.engineFeatures_NAME_KO);
		writer.Write<string[]>("engineFeatures_NAME_AR", this.eF_.engineFeatures_NAME_AR);
		writer.Write<string[]>("engineFeatures_NAME_RU", this.eF_.engineFeatures_NAME_RU);
		writer.Write<string[]>("engineFeatures_NAME_IT", this.eF_.engineFeatures_NAME_IT);
		writer.Write<string[]>("engineFeatures_NAME_JA", this.eF_.engineFeatures_NAME_JA);
		writer.Write<string[]>("engineFeatures_NAME_PL", this.eF_.engineFeatures_NAME_PL);
		writer.Write<string[]>("engineFeatures_DESC_EN", this.eF_.engineFeatures_DESC_EN);
		writer.Write<string[]>("engineFeatures_DESC_GE", this.eF_.engineFeatures_DESC_GE);
		writer.Write<string[]>("engineFeatures_DESC_TU", this.eF_.engineFeatures_DESC_TU);
		writer.Write<string[]>("engineFeatures_DESC_CH", this.eF_.engineFeatures_DESC_CH);
		writer.Write<string[]>("engineFeatures_DESC_FR", this.eF_.engineFeatures_DESC_FR);
		writer.Write<string[]>("engineFeatures_DESC_PB", this.eF_.engineFeatures_DESC_PB);
		writer.Write<string[]>("engineFeatures_DESC_CT", this.eF_.engineFeatures_DESC_CT);
		writer.Write<string[]>("engineFeatures_DESC_HU", this.eF_.engineFeatures_DESC_HU);
		writer.Write<string[]>("engineFeatures_DESC_ES", this.eF_.engineFeatures_DESC_ES);
		writer.Write<string[]>("engineFeatures_DESC_CZ", this.eF_.engineFeatures_DESC_CZ);
		writer.Write<string[]>("engineFeatures_DESC_KO", this.eF_.engineFeatures_DESC_KO);
		writer.Write<string[]>("engineFeatures_DESC_AR", this.eF_.engineFeatures_DESC_AR);
		writer.Write<string[]>("engineFeatures_DESC_RU", this.eF_.engineFeatures_DESC_RU);
		writer.Write<string[]>("engineFeatures_DESC_IT", this.eF_.engineFeatures_DESC_IT);
		writer.Write<string[]>("engineFeatures_DESC_JA", this.eF_.engineFeatures_DESC_JA);
		writer.Write<string[]>("engineFeatures_DESC_PL", this.eF_.engineFeatures_DESC_PL);
	}

	
	private void LoadEngineFeatures(ES3Reader reader, string filename)
	{
		this.eF_.engineFeatures_TYP = reader.Read<int[]>("engineFeatures_TYP");
		this.eF_.engineFeatures_RES_POINTS = reader.Read<int[]>("engineFeatures_RES_POINTS");
		this.eF_.engineFeatures_RES_POINTS_LEFT = reader.Read<float[]>("engineFeatures_RES_POINTS_LEFT");
		this.eF_.engineFeatures_PRICE = reader.Read<int[]>("engineFeatures_PRICE");
		this.eF_.engineFeatures_DEV_COSTS = reader.Read<int[]>("engineFeatures_DEV_COSTS");
		this.eF_.engineFeatures_TECH = reader.Read<int[]>("engineFeatures_TECH");
		this.eF_.engineFeatures_DATE_YEAR = reader.Read<int[]>("engineFeatures_DATE_YEAR");
		this.eF_.engineFeatures_DATE_MONTH = reader.Read<int[]>("engineFeatures_DATE_MONTH");
		this.eF_.engineFeatures_GAMEPLAY = reader.Read<int[]>("engineFeatures_GAMEPLAY");
		this.eF_.engineFeatures_GRAPHIC = reader.Read<int[]>("engineFeatures_GRAPHIC");
		this.eF_.engineFeatures_SOUND = reader.Read<int[]>("engineFeatures_SOUND");
		this.eF_.engineFeatures_TECHNIK = reader.Read<int[]>("engineFeatures_TECHNIK");
		this.eF_.engineFeatures_LEVEL = reader.Read<int[]>("engineFeatures_LEVEL");
		this.eF_.engineFeatures_UNLOCK = reader.Read<bool[]>("engineFeatures_UNLOCK");
		this.eF_.engineFeatures_ICONFILE = reader.Read<string[]>("engineFeatures_ICONFILE");
		if (this.key_EN)
		{
			this.eF_.engineFeatures_NAME_EN = reader.Read<string[]>("engineFeatures_NAME_EN");
		}
		if (this.key_GE)
		{
			this.eF_.engineFeatures_NAME_GE = reader.Read<string[]>("engineFeatures_NAME_GE");
		}
		if (this.key_TU)
		{
			this.eF_.engineFeatures_NAME_TU = reader.Read<string[]>("engineFeatures_NAME_TU");
		}
		if (this.key_CH)
		{
			this.eF_.engineFeatures_NAME_CH = reader.Read<string[]>("engineFeatures_NAME_CH");
		}
		if (this.key_FR)
		{
			this.eF_.engineFeatures_NAME_FR = reader.Read<string[]>("engineFeatures_NAME_FR");
		}
		if (this.key_PB)
		{
			this.eF_.engineFeatures_NAME_PB = reader.Read<string[]>("engineFeatures_NAME_PB");
		}
		if (this.key_CT)
		{
			this.eF_.engineFeatures_NAME_CT = reader.Read<string[]>("engineFeatures_NAME_CT");
		}
		if (this.key_HU)
		{
			this.eF_.engineFeatures_NAME_HU = reader.Read<string[]>("engineFeatures_NAME_HU");
		}
		if (this.key_ES)
		{
			this.eF_.engineFeatures_NAME_ES = reader.Read<string[]>("engineFeatures_NAME_ES");
		}
		if (this.key_CZ)
		{
			this.eF_.engineFeatures_NAME_CZ = reader.Read<string[]>("engineFeatures_NAME_CZ");
		}
		if (this.key_KO)
		{
			this.eF_.engineFeatures_NAME_KO = reader.Read<string[]>("engineFeatures_NAME_KO");
		}
		if (this.key_AR)
		{
			this.eF_.engineFeatures_NAME_AR = reader.Read<string[]>("engineFeatures_NAME_AR");
		}
		if (this.key_RU)
		{
			this.eF_.engineFeatures_NAME_RU = reader.Read<string[]>("engineFeatures_NAME_RU");
		}
		if (this.key_IT)
		{
			this.eF_.engineFeatures_NAME_IT = reader.Read<string[]>("engineFeatures_NAME_IT");
		}
		if (this.key_JA)
		{
			this.eF_.engineFeatures_NAME_JA = reader.Read<string[]>("engineFeatures_NAME_JA");
		}
		if (this.key_PL)
		{
			this.eF_.engineFeatures_NAME_PL = reader.Read<string[]>("engineFeatures_NAME_PL");
		}
		if (this.key_EN)
		{
			this.eF_.engineFeatures_DESC_EN = reader.Read<string[]>("engineFeatures_DESC_EN");
		}
		if (this.key_GE)
		{
			this.eF_.engineFeatures_DESC_GE = reader.Read<string[]>("engineFeatures_DESC_GE");
		}
		if (this.key_TU)
		{
			this.eF_.engineFeatures_DESC_TU = reader.Read<string[]>("engineFeatures_DESC_TU");
		}
		if (this.key_CH)
		{
			this.eF_.engineFeatures_DESC_CH = reader.Read<string[]>("engineFeatures_DESC_CH");
		}
		if (this.key_FR)
		{
			this.eF_.engineFeatures_DESC_FR = reader.Read<string[]>("engineFeatures_DESC_FR");
		}
		if (this.key_PB)
		{
			this.eF_.engineFeatures_DESC_PB = reader.Read<string[]>("engineFeatures_DESC_PB");
		}
		if (this.key_CT)
		{
			this.eF_.engineFeatures_DESC_CT = reader.Read<string[]>("engineFeatures_DESC_CT");
		}
		if (this.key_HU)
		{
			this.eF_.engineFeatures_DESC_HU = reader.Read<string[]>("engineFeatures_DESC_HU");
		}
		if (this.key_ES)
		{
			this.eF_.engineFeatures_DESC_ES = reader.Read<string[]>("engineFeatures_DESC_ES");
		}
		if (this.key_CZ)
		{
			this.eF_.engineFeatures_DESC_CZ = reader.Read<string[]>("engineFeatures_DESC_CZ");
		}
		if (this.key_KO)
		{
			this.eF_.engineFeatures_DESC_KO = reader.Read<string[]>("engineFeatures_DESC_KO");
		}
		if (this.key_AR)
		{
			this.eF_.engineFeatures_DESC_AR = reader.Read<string[]>("engineFeatures_DESC_AR");
		}
		if (this.key_RU)
		{
			this.eF_.engineFeatures_DESC_RU = reader.Read<string[]>("engineFeatures_DESC_RU");
		}
		if (this.key_IT)
		{
			this.eF_.engineFeatures_DESC_IT = reader.Read<string[]>("engineFeatures_DESC_IT");
		}
		if (this.key_JA)
		{
			this.eF_.engineFeatures_DESC_JA = reader.Read<string[]>("engineFeatures_DESC_JA");
		}
		if (this.key_PL)
		{
			this.eF_.engineFeatures_DESC_PL = reader.Read<string[]>("engineFeatures_DESC_PL");
		}
		this.eF_.Init();
	}

	
	private void SaveGameplayFeatures(ES3Writer writer)
	{
		writer.Write<int[]>("gameplayFeatures_TYP", this.gF_.gameplayFeatures_TYP);
		writer.Write<int[]>("gameplayFeatures_RES_POINTS", this.gF_.gameplayFeatures_RES_POINTS);
		writer.Write<float[]>("gameplayFeatures_RES_POINTS_LEFT", this.gF_.gameplayFeatures_RES_POINTS_LEFT);
		writer.Write<int[]>("gameplayFeatures_PRICE", this.gF_.gameplayFeatures_PRICE);
		writer.Write<int[]>("gameplayFeatures_DEV_COSTS", this.gF_.gameplayFeatures_DEV_COSTS);
		writer.Write<int[]>("gameplayFeatures_DATE_YEAR", this.gF_.gameplayFeatures_DATE_YEAR);
		writer.Write<int[]>("gameplayFeatures_DATE_MONTH", this.gF_.gameplayFeatures_DATE_MONTH);
		writer.Write<int[]>("gameplayFeatures_GAMEPLAY", this.gF_.gameplayFeatures_GAMEPLAY);
		writer.Write<int[]>("gameplayFeatures_GRAPHIC", this.gF_.gameplayFeatures_GRAPHIC);
		writer.Write<int[]>("gameplayFeatures_SOUND", this.gF_.gameplayFeatures_SOUND);
		writer.Write<int[]>("gameplayFeatures_TECHNIK", this.gF_.gameplayFeatures_TECHNIK);
		writer.Write<int[]>("gameplayFeatures_LEVEL", this.gF_.gameplayFeatures_LEVEL);
		writer.Write<bool[]>("gameplayFeatures_UNLOCK", this.gF_.gameplayFeatures_UNLOCK);
		writer.Write<bool[,]>("gameplayFeatures_GOOD", this.gF_.gameplayFeatures_GOOD);
		writer.Write<bool[,]>("gameplayFeatures_BAD", this.gF_.gameplayFeatures_BAD);
		writer.Write<bool[,]>("gameplayFeatures_LOCKPLATFORM", this.gF_.gameplayFeatures_LOCKPLATFORM);
		writer.Write<string[]>("gameplayFeatures_NAME_EN", this.gF_.gameplayFeatures_NAME_EN);
		writer.Write<string[]>("gameplayFeatures_NAME_GE", this.gF_.gameplayFeatures_NAME_GE);
		writer.Write<string[]>("gameplayFeatures_NAME_TU", this.gF_.gameplayFeatures_NAME_TU);
		writer.Write<string[]>("gameplayFeatures_NAME_CH", this.gF_.gameplayFeatures_NAME_CH);
		writer.Write<string[]>("gameplayFeatures_NAME_FR", this.gF_.gameplayFeatures_NAME_FR);
		writer.Write<string[]>("gameplayFeatures_NAME_PB", this.gF_.gameplayFeatures_NAME_PB);
		writer.Write<string[]>("gameplayFeatures_NAME_CT", this.gF_.gameplayFeatures_NAME_CT);
		writer.Write<string[]>("gameplayFeatures_NAME_HU", this.gF_.gameplayFeatures_NAME_HU);
		writer.Write<string[]>("gameplayFeatures_NAME_ES", this.gF_.gameplayFeatures_NAME_ES);
		writer.Write<string[]>("gameplayFeatures_NAME_CZ", this.gF_.gameplayFeatures_NAME_CZ);
		writer.Write<string[]>("gameplayFeatures_NAME_KO", this.gF_.gameplayFeatures_NAME_KO);
		writer.Write<string[]>("gameplayFeatures_NAME_RU", this.gF_.gameplayFeatures_NAME_RU);
		writer.Write<string[]>("gameplayFeatures_NAME_IT", this.gF_.gameplayFeatures_NAME_IT);
		writer.Write<string[]>("gameplayFeatures_NAME_AR", this.gF_.gameplayFeatures_NAME_AR);
		writer.Write<string[]>("gameplayFeatures_NAME_JA", this.gF_.gameplayFeatures_NAME_JA);
		writer.Write<string[]>("gameplayFeatures_NAME_PL", this.gF_.gameplayFeatures_NAME_PL);
		writer.Write<string[]>("gameplayFeatures_DESC_EN", this.gF_.gameplayFeatures_DESC_EN);
		writer.Write<string[]>("gameplayFeatures_DESC_GE", this.gF_.gameplayFeatures_DESC_GE);
		writer.Write<string[]>("gameplayFeatures_DESC_TU", this.gF_.gameplayFeatures_DESC_TU);
		writer.Write<string[]>("gameplayFeatures_DESC_CH", this.gF_.gameplayFeatures_DESC_CH);
		writer.Write<string[]>("gameplayFeatures_DESC_FR", this.gF_.gameplayFeatures_DESC_FR);
		writer.Write<string[]>("gameplayFeatures_DESC_PB", this.gF_.gameplayFeatures_DESC_PB);
		writer.Write<string[]>("gameplayFeatures_DESC_CT", this.gF_.gameplayFeatures_DESC_CT);
		writer.Write<string[]>("gameplayFeatures_DESC_HU", this.gF_.gameplayFeatures_DESC_HU);
		writer.Write<string[]>("gameplayFeatures_DESC_ES", this.gF_.gameplayFeatures_DESC_ES);
		writer.Write<string[]>("gameplayFeatures_DESC_CZ", this.gF_.gameplayFeatures_DESC_CZ);
		writer.Write<string[]>("gameplayFeatures_DESC_KO", this.gF_.gameplayFeatures_DESC_KO);
		writer.Write<string[]>("gameplayFeatures_DESC_RU", this.gF_.gameplayFeatures_DESC_RU);
		writer.Write<string[]>("gameplayFeatures_DESC_IT", this.gF_.gameplayFeatures_DESC_IT);
		writer.Write<string[]>("gameplayFeatures_DESC_AR", this.gF_.gameplayFeatures_DESC_AR);
		writer.Write<string[]>("gameplayFeatures_DESC_JA", this.gF_.gameplayFeatures_DESC_JA);
		writer.Write<string[]>("gameplayFeatures_DESC_PL", this.gF_.gameplayFeatures_DESC_PL);
		writer.Write<string[]>("gameplayFeatures_ICONFILE", this.gF_.gameplayFeatures_ICONFILE);
	}

	
	private void LoadGameplayFeatures(ES3Reader reader, string filename)
	{
		this.gF_.gameplayFeatures_TYP = reader.Read<int[]>("gameplayFeatures_TYP");
		this.gF_.gameplayFeatures_RES_POINTS = reader.Read<int[]>("gameplayFeatures_RES_POINTS");
		this.gF_.gameplayFeatures_RES_POINTS_LEFT = reader.Read<float[]>("gameplayFeatures_RES_POINTS_LEFT");
		this.gF_.gameplayFeatures_PRICE = reader.Read<int[]>("gameplayFeatures_PRICE");
		this.gF_.gameplayFeatures_DEV_COSTS = reader.Read<int[]>("gameplayFeatures_DEV_COSTS");
		this.gF_.gameplayFeatures_DATE_YEAR = reader.Read<int[]>("gameplayFeatures_DATE_YEAR");
		this.gF_.gameplayFeatures_DATE_MONTH = reader.Read<int[]>("gameplayFeatures_DATE_MONTH");
		this.gF_.gameplayFeatures_GAMEPLAY = reader.Read<int[]>("gameplayFeatures_GAMEPLAY");
		this.gF_.gameplayFeatures_GRAPHIC = reader.Read<int[]>("gameplayFeatures_GRAPHIC");
		this.gF_.gameplayFeatures_SOUND = reader.Read<int[]>("gameplayFeatures_SOUND");
		this.gF_.gameplayFeatures_TECHNIK = reader.Read<int[]>("gameplayFeatures_TECHNIK");
		this.gF_.gameplayFeatures_LEVEL = reader.Read<int[]>("gameplayFeatures_LEVEL");
		this.gF_.gameplayFeatures_UNLOCK = reader.Read<bool[]>("gameplayFeatures_UNLOCK");
		if (this.mS_.savegameVersion >= 5)
		{
			this.gF_.gameplayFeatures_GOOD = reader.Read<bool[,]>("gameplayFeatures_GOOD");
			this.gF_.gameplayFeatures_BAD = reader.Read<bool[,]>("gameplayFeatures_BAD");
		}
		else
		{
			this.gF_.gameplayFeatures_GOOD = new bool[this.gF_.gameplayFeatures_UNLOCK.Length, this.genres_.genres_UNLOCK.Length];
			this.gF_.gameplayFeatures_BAD = new bool[this.gF_.gameplayFeatures_UNLOCK.Length, this.genres_.genres_UNLOCK.Length];
		}
		if (this.mS_.savegameVersion >= 8)
		{
			this.gF_.gameplayFeatures_LOCKPLATFORM = reader.Read<bool[,]>("gameplayFeatures_LOCKPLATFORM");
		}
		else
		{
			this.gF_.gameplayFeatures_LOCKPLATFORM = new bool[this.gF_.gameplayFeatures_UNLOCK.Length, 5];
		}
		if (this.key_EN)
		{
			this.gF_.gameplayFeatures_NAME_EN = reader.Read<string[]>("gameplayFeatures_NAME_EN");
		}
		if (this.key_GE)
		{
			this.gF_.gameplayFeatures_NAME_GE = reader.Read<string[]>("gameplayFeatures_NAME_GE");
		}
		if (this.key_TU)
		{
			this.gF_.gameplayFeatures_NAME_TU = reader.Read<string[]>("gameplayFeatures_NAME_TU");
		}
		if (this.key_CH)
		{
			this.gF_.gameplayFeatures_NAME_CH = reader.Read<string[]>("gameplayFeatures_NAME_CH");
		}
		if (this.key_FR)
		{
			this.gF_.gameplayFeatures_NAME_FR = reader.Read<string[]>("gameplayFeatures_NAME_FR");
		}
		if (this.key_PB)
		{
			this.gF_.gameplayFeatures_NAME_PB = reader.Read<string[]>("gameplayFeatures_NAME_PB");
		}
		if (this.key_CT)
		{
			this.gF_.gameplayFeatures_NAME_CT = reader.Read<string[]>("gameplayFeatures_NAME_CT");
		}
		if (this.key_HU)
		{
			this.gF_.gameplayFeatures_NAME_HU = reader.Read<string[]>("gameplayFeatures_NAME_HU");
		}
		if (this.key_ES)
		{
			this.gF_.gameplayFeatures_NAME_ES = reader.Read<string[]>("gameplayFeatures_NAME_ES");
		}
		if (this.key_CZ)
		{
			this.gF_.gameplayFeatures_NAME_CZ = reader.Read<string[]>("gameplayFeatures_NAME_CZ");
		}
		if (this.key_KO)
		{
			this.gF_.gameplayFeatures_NAME_KO = reader.Read<string[]>("gameplayFeatures_NAME_KO");
		}
		if (this.key_RU)
		{
			this.gF_.gameplayFeatures_NAME_RU = reader.Read<string[]>("gameplayFeatures_NAME_RU");
		}
		if (this.key_IT)
		{
			this.gF_.gameplayFeatures_NAME_IT = reader.Read<string[]>("gameplayFeatures_NAME_IT");
		}
		if (this.key_AR)
		{
			this.gF_.gameplayFeatures_NAME_AR = reader.Read<string[]>("gameplayFeatures_NAME_AR");
		}
		if (this.key_JA)
		{
			this.gF_.gameplayFeatures_NAME_JA = reader.Read<string[]>("gameplayFeatures_NAME_JA");
		}
		if (this.key_PL)
		{
			this.gF_.gameplayFeatures_NAME_PL = reader.Read<string[]>("gameplayFeatures_NAME_PL");
		}
		if (this.key_EN)
		{
			this.gF_.gameplayFeatures_DESC_EN = reader.Read<string[]>("gameplayFeatures_DESC_EN");
		}
		if (this.key_GE)
		{
			this.gF_.gameplayFeatures_DESC_GE = reader.Read<string[]>("gameplayFeatures_DESC_GE");
		}
		if (this.key_TU)
		{
			this.gF_.gameplayFeatures_DESC_TU = reader.Read<string[]>("gameplayFeatures_DESC_TU");
		}
		if (this.key_CH)
		{
			this.gF_.gameplayFeatures_DESC_CH = reader.Read<string[]>("gameplayFeatures_DESC_CH");
		}
		if (this.key_FR)
		{
			this.gF_.gameplayFeatures_DESC_FR = reader.Read<string[]>("gameplayFeatures_DESC_FR");
		}
		if (this.key_PB)
		{
			this.gF_.gameplayFeatures_DESC_PB = reader.Read<string[]>("gameplayFeatures_DESC_PB");
		}
		if (this.key_CT)
		{
			this.gF_.gameplayFeatures_DESC_CT = reader.Read<string[]>("gameplayFeatures_DESC_CT");
		}
		if (this.key_HU)
		{
			this.gF_.gameplayFeatures_DESC_HU = reader.Read<string[]>("gameplayFeatures_DESC_HU");
		}
		if (this.key_ES)
		{
			this.gF_.gameplayFeatures_DESC_ES = reader.Read<string[]>("gameplayFeatures_DESC_ES");
		}
		if (this.key_CZ)
		{
			this.gF_.gameplayFeatures_DESC_CZ = reader.Read<string[]>("gameplayFeatures_DESC_CZ");
		}
		if (this.key_KO)
		{
			this.gF_.gameplayFeatures_DESC_KO = reader.Read<string[]>("gameplayFeatures_DESC_KO");
		}
		if (this.key_RU)
		{
			this.gF_.gameplayFeatures_DESC_RU = reader.Read<string[]>("gameplayFeatures_DESC_RU");
		}
		if (this.key_IT)
		{
			this.gF_.gameplayFeatures_DESC_IT = reader.Read<string[]>("gameplayFeatures_DESC_IT");
		}
		if (this.key_AR)
		{
			this.gF_.gameplayFeatures_DESC_AR = reader.Read<string[]>("gameplayFeatures_DESC_AR");
		}
		if (this.key_JA)
		{
			this.gF_.gameplayFeatures_DESC_JA = reader.Read<string[]>("gameplayFeatures_DESC_JA");
		}
		if (this.key_PL)
		{
			this.gF_.gameplayFeatures_DESC_PL = reader.Read<string[]>("gameplayFeatures_DESC_PL");
		}
		this.gF_.gameplayFeatures_ICONFILE = reader.Read<string[]>("gameplayFeatures_ICONFILE");
		this.gF_.Init();
	}

	
	private void SaveThemes(ES3Writer writer)
	{
		writer.Write<float[]>("themes_RES_POINTS_LEFT", this.themes_.themes_RES_POINTS_LEFT);
		writer.Write<int[]>("themes_LEVEL", this.themes_.themes_LEVEL);
		writer.Write<int[]>("themes_MARKT", this.themes_.themes_MARKT);
	}

	
	private void LoadThemes(ES3Reader reader, string filename)
	{
		this.tS_.LoadContent_Themes();
		this.themes_.themes_RES_POINTS_LEFT = reader.Read<float[]>("themes_RES_POINTS_LEFT");
		this.themes_.themes_LEVEL = reader.Read<int[]>("themes_LEVEL");
		if (this.mS_.savegameVersion >= 9)
		{
			this.themes_.themes_MARKT = reader.Read<int[]>("themes_MARKT");
		}
		if (this.tS_.themes_GE.Length != this.themes_.themes_RES_POINTS_LEFT.Length)
		{
			this.themes_.themes_MGSR = new int[this.themes_.themes_LEVEL.Length];
		}
	}

	
	private void SaveNPCGameNames(ES3Writer writer)
	{
		writer.Write<string[]>("npcGames", this.tS_.npcGames);
		writer.Write<bool[]>("npcGameNameInUse", this.tS_.npcGameNameInUse);
	}

	
	private void LoadNPCGameNames(ES3Reader reader, string filename)
	{
		this.tS_.npcGames = reader.Read<string[]>("npcGames");
		if (this.key_npcGameNameInUse)
		{
			this.tS_.npcGameNameInUse = reader.Read<bool[]>("npcGameNameInUse");
		}
	}

	
	private void SaveGenres(ES3Writer writer)
	{
		writer.Write<int[]>("genres_RES_POINTS", this.genres_.genres_RES_POINTS);
		writer.Write<float[]>("genres_RES_POINTS_LEFT", this.genres_.genres_RES_POINTS_LEFT);
		writer.Write<int[]>("genres_PRICE", this.genres_.genres_PRICE);
		writer.Write<int[]>("genres_DEV_COSTS", this.genres_.genres_DEV_COSTS);
		writer.Write<int[]>("genres_DATE_YEAR", this.genres_.genres_DATE_YEAR);
		writer.Write<int[]>("genres_DATE_MONTH", this.genres_.genres_DATE_MONTH);
		writer.Write<int[]>("genres_LEVEL", this.genres_.genres_LEVEL);
		writer.Write<bool[]>("genres_UNLOCK", this.genres_.genres_UNLOCK);
		writer.Write<bool[,]>("genres_TARGETGROUP", this.genres_.genres_TARGETGROUP);
		writer.Write<float[]>("genres_GAMEPLAY", this.genres_.genres_GAMEPLAY);
		writer.Write<float[]>("genres_GRAPHIC", this.genres_.genres_GRAPHIC);
		writer.Write<float[]>("genres_SOUND", this.genres_.genres_SOUND);
		writer.Write<float[]>("genres_CONTROL", this.genres_.genres_CONTROL);
		writer.Write<bool[,]>("genres_COMBINATION", this.genres_.genres_COMBINATION);
		writer.Write<float[]>("genres_BELIEBTHEIT", this.genres_.genres_BELIEBTHEIT);
		writer.Write<bool[]>("genres_BELIEBTHEIT_SOLL", this.genres_.genres_BELIEBTHEIT_SOLL);
		writer.Write<int[,]>("genres_FOCUS", this.genres_.genres_FOCUS);
		writer.Write<int[,]>("genres_ALIGN", this.genres_.genres_ALIGN);
		writer.Write<string[]>("genres_NAME_EN", this.genres_.genres_NAME_EN);
		writer.Write<string[]>("genres_NAME_GE", this.genres_.genres_NAME_GE);
		writer.Write<string[]>("genres_NAME_TU", this.genres_.genres_NAME_TU);
		writer.Write<string[]>("genres_NAME_CH", this.genres_.genres_NAME_CH);
		writer.Write<string[]>("genres_NAME_FR", this.genres_.genres_NAME_FR);
		writer.Write<string[]>("genres_NAME_PB", this.genres_.genres_NAME_PB);
		writer.Write<string[]>("genres_NAME_HU", this.genres_.genres_NAME_HU);
		writer.Write<string[]>("genres_NAME_CT", this.genres_.genres_NAME_CT);
		writer.Write<string[]>("genres_NAME_ES", this.genres_.genres_NAME_ES);
		writer.Write<string[]>("genres_NAME_PL", this.genres_.genres_NAME_PL);
		writer.Write<string[]>("genres_NAME_CZ", this.genres_.genres_NAME_CZ);
		writer.Write<string[]>("genres_NAME_KO", this.genres_.genres_NAME_KO);
		writer.Write<string[]>("genres_NAME_IT", this.genres_.genres_NAME_IT);
		writer.Write<string[]>("genres_NAME_AR", this.genres_.genres_NAME_AR);
		writer.Write<string[]>("genres_NAME_JA", this.genres_.genres_NAME_JA);
		writer.Write<string[]>("genres_DESC_EN", this.genres_.genres_DESC_EN);
		writer.Write<string[]>("genres_DESC_GE", this.genres_.genres_DESC_GE);
		writer.Write<string[]>("genres_DESC_TU", this.genres_.genres_DESC_TU);
		writer.Write<string[]>("genres_DESC_CH", this.genres_.genres_DESC_CH);
		writer.Write<string[]>("genres_DESC_FR", this.genres_.genres_DESC_FR);
		writer.Write<string[]>("genres_DESC_PB", this.genres_.genres_DESC_PB);
		writer.Write<string[]>("genres_DESC_HU", this.genres_.genres_DESC_HU);
		writer.Write<string[]>("genres_DESC_CT", this.genres_.genres_DESC_CT);
		writer.Write<string[]>("genres_DESC_ES", this.genres_.genres_DESC_ES);
		writer.Write<string[]>("genres_DESC_PL", this.genres_.genres_DESC_PL);
		writer.Write<string[]>("genres_DESC_CZ", this.genres_.genres_DESC_CZ);
		writer.Write<string[]>("genres_DESC_KO", this.genres_.genres_DESC_KO);
		writer.Write<string[]>("genres_DESC_IT", this.genres_.genres_DESC_IT);
		writer.Write<string[]>("genres_DESC_AR", this.genres_.genres_DESC_AR);
		writer.Write<string[]>("genres_DESC_JA", this.genres_.genres_DESC_JA);
		writer.Write<string[]>("genres_ICONFILE", this.genres_.genres_ICONFILE);
		writer.Write<int[]>("genres_FANS", this.genres_.genres_FANS);
		writer.Write<int[]>("genres_MARKT", this.genres_.genres_MARKT);
	}

	
	private void LoadGenres(ES3Reader reader, string filename)
	{
		this.genres_.genres_RES_POINTS = reader.Read<int[]>("genres_RES_POINTS");
		this.genres_.genres_RES_POINTS_LEFT = reader.Read<float[]>("genres_RES_POINTS_LEFT");
		this.genres_.genres_PRICE = reader.Read<int[]>("genres_PRICE");
		this.genres_.genres_DEV_COSTS = reader.Read<int[]>("genres_DEV_COSTS");
		this.genres_.genres_DATE_YEAR = reader.Read<int[]>("genres_DATE_YEAR");
		this.genres_.genres_DATE_MONTH = reader.Read<int[]>("genres_DATE_MONTH");
		this.genres_.genres_LEVEL = reader.Read<int[]>("genres_LEVEL");
		this.genres_.genres_UNLOCK = reader.Read<bool[]>("genres_UNLOCK");
		this.genres_.genres_TARGETGROUP = reader.Read<bool[,]>("genres_TARGETGROUP");
		this.genres_.genres_GAMEPLAY = reader.Read<float[]>("genres_GAMEPLAY");
		this.genres_.genres_GRAPHIC = reader.Read<float[]>("genres_GRAPHIC");
		this.genres_.genres_SOUND = reader.Read<float[]>("genres_SOUND");
		this.genres_.genres_CONTROL = reader.Read<float[]>("genres_CONTROL");
		this.genres_.genres_COMBINATION = reader.Read<bool[,]>("genres_COMBINATION");
		if (this.mS_.savegameVersion >= 6)
		{
			this.genres_.genres_FOCUS = reader.Read<int[,]>("genres_FOCUS");
			this.genres_.genres_ALIGN = reader.Read<int[,]>("genres_ALIGN");
		}
		else
		{
			this.genres_.LoadGenresettingsForOldSavegeames("DATA/Genres.txt");
		}
		if (this.mS_.savegameVersion >= 2)
		{
			this.genres_.genres_BELIEBTHEIT = reader.Read<float[]>("genres_BELIEBTHEIT");
			this.genres_.genres_BELIEBTHEIT_SOLL = reader.Read<bool[]>("genres_BELIEBTHEIT_SOLL");
		}
		else
		{
			this.genres_.genres_BELIEBTHEIT = new float[this.genres_.genres_RES_POINTS.Length];
			this.genres_.genres_BELIEBTHEIT_SOLL = new bool[this.genres_.genres_RES_POINTS.Length];
			for (int i = 0; i < this.genres_.genres_BELIEBTHEIT.Length; i++)
			{
				this.genres_.genres_BELIEBTHEIT[i] = (float)UnityEngine.Random.Range(40, 80);
				if (UnityEngine.Random.Range(0, 100) > 50)
				{
					this.genres_.genres_BELIEBTHEIT_SOLL[i] = true;
				}
				else
				{
					this.genres_.genres_BELIEBTHEIT_SOLL[i] = false;
				}
			}
		}
		if (this.key_EN)
		{
			this.genres_.genres_NAME_EN = reader.Read<string[]>("genres_NAME_EN");
		}
		if (this.key_GE)
		{
			this.genres_.genres_NAME_GE = reader.Read<string[]>("genres_NAME_GE");
		}
		if (this.key_TU)
		{
			this.genres_.genres_NAME_TU = reader.Read<string[]>("genres_NAME_TU");
		}
		if (this.key_CH)
		{
			this.genres_.genres_NAME_CH = reader.Read<string[]>("genres_NAME_CH");
		}
		if (this.key_FR)
		{
			this.genres_.genres_NAME_FR = reader.Read<string[]>("genres_NAME_FR");
		}
		if (this.key_PB)
		{
			this.genres_.genres_NAME_PB = reader.Read<string[]>("genres_NAME_PB");
		}
		if (this.key_HU)
		{
			this.genres_.genres_NAME_HU = reader.Read<string[]>("genres_NAME_HU");
		}
		if (this.key_CT)
		{
			this.genres_.genres_NAME_CT = reader.Read<string[]>("genres_NAME_CT");
		}
		if (this.key_ES)
		{
			this.genres_.genres_NAME_ES = reader.Read<string[]>("genres_NAME_ES");
		}
		if (this.key_PL)
		{
			this.genres_.genres_NAME_PL = reader.Read<string[]>("genres_NAME_PL");
		}
		if (this.key_CZ)
		{
			this.genres_.genres_NAME_CZ = reader.Read<string[]>("genres_NAME_CZ");
		}
		if (this.key_KO)
		{
			this.genres_.genres_NAME_KO = reader.Read<string[]>("genres_NAME_KO");
		}
		if (this.key_IT)
		{
			this.genres_.genres_NAME_IT = reader.Read<string[]>("genres_NAME_IT");
		}
		if (this.key_AR)
		{
			this.genres_.genres_NAME_AR = reader.Read<string[]>("genres_NAME_AR");
		}
		if (this.key_JA)
		{
			this.genres_.genres_NAME_JA = reader.Read<string[]>("genres_NAME_JA");
		}
		if (this.key_EN)
		{
			this.genres_.genres_DESC_EN = reader.Read<string[]>("genres_DESC_EN");
		}
		if (this.key_GE)
		{
			this.genres_.genres_DESC_GE = reader.Read<string[]>("genres_DESC_GE");
		}
		if (this.key_TU)
		{
			this.genres_.genres_DESC_TU = reader.Read<string[]>("genres_DESC_TU");
		}
		if (this.key_CH)
		{
			this.genres_.genres_DESC_CH = reader.Read<string[]>("genres_DESC_CH");
		}
		if (this.key_FR)
		{
			this.genres_.genres_DESC_FR = reader.Read<string[]>("genres_DESC_FR");
		}
		if (this.key_PB)
		{
			this.genres_.genres_DESC_PB = reader.Read<string[]>("genres_DESC_PB");
		}
		if (this.key_HU)
		{
			this.genres_.genres_DESC_HU = reader.Read<string[]>("genres_DESC_HU");
		}
		if (this.key_CT)
		{
			this.genres_.genres_DESC_CT = reader.Read<string[]>("genres_DESC_CT");
		}
		if (this.key_ES)
		{
			this.genres_.genres_DESC_ES = reader.Read<string[]>("genres_DESC_ES");
		}
		if (this.key_PL)
		{
			this.genres_.genres_DESC_PL = reader.Read<string[]>("genres_DESC_PL");
		}
		if (this.key_CZ)
		{
			this.genres_.genres_DESC_CZ = reader.Read<string[]>("genres_DESC_CZ");
		}
		if (this.key_KO)
		{
			this.genres_.genres_DESC_KO = reader.Read<string[]>("genres_DESC_KO");
		}
		if (this.key_IT)
		{
			this.genres_.genres_DESC_IT = reader.Read<string[]>("genres_DESC_IT");
		}
		if (this.key_AR)
		{
			this.genres_.genres_DESC_AR = reader.Read<string[]>("genres_DESC_AR");
		}
		if (this.key_JA)
		{
			this.genres_.genres_DESC_JA = reader.Read<string[]>("genres_DESC_JA");
		}
		this.genres_.genres_ICONFILE = reader.Read<string[]>("genres_ICONFILE");
		this.genres_.genres_FANS = reader.Read<int[]>("genres_FANS");
		if (this.es3file.KeyExists("genres_MARKT"))
		{
			this.genres_.genres_MARKT = reader.Read<int[]>("genres_MARKT");
		}
		else
		{
			this.genres_.genres_MARKT = new int[this.genres_.genres_LEVEL.Length];
		}
		this.genres_.Init();
	}

	
	private void SaveLicences(ES3Writer writer)
	{
		writer.Write<string[]>("licence_EN", this.licences_.licence_EN);
		writer.Write<int[]>("licence_TYP", this.licences_.licence_TYP);
		writer.Write<float[]>("licence_QUALITY", this.licences_.licence_QUALITY);
		writer.Write<int[]>("licence_ANGEBOT", this.licences_.licence_ANGEBOT);
		writer.Write<int[]>("licence_GEKAUFT", this.licences_.licence_GEKAUFT);
	}

	
	private void LoadLicences(ES3Reader reader, string filename)
	{
		this.licences_.licence_EN = reader.Read<string[]>("licence_EN");
		this.licences_.licence_TYP = reader.Read<int[]>("licence_TYP");
		this.licences_.licence_QUALITY = reader.Read<float[]>("licence_QUALITY");
		this.licences_.licence_ANGEBOT = reader.Read<int[]>("licence_ANGEBOT");
		this.licences_.licence_GEKAUFT = reader.Read<int[]>("licence_GEKAUFT");
	}

	
	private void SaveCopyProtect(ES3Writer writer)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("CopyProtect");
		int num = array.Length;
		writer.Write<int>("anzCopyProtect", num);
		int[] array2 = new int[10 * num];
		float[] array3 = new float[10 * num];
		string[] array4 = new string[20 * num];
		bool[] array5 = new bool[10 * num];
		for (int i = 0; i < array.Length; i++)
		{
			copyProtectScript component = array[i].GetComponent<copyProtectScript>();
			int num2 = i * 20;
			array4[num2] = component.name_EN;
			array4[1 + num2] = component.name_GE;
			array4[2 + num2] = component.name_TU;
			array4[3 + num2] = component.name_CH;
			array4[4 + num2] = component.name_FR;
			array4[5 + num2] = component.name_CT;
			array4[6 + num2] = component.name_RU;
			array4[7 + num2] = component.name_IT;
			array4[8 + num2] = component.name_JA;
			int num3 = i * 10;
			array2[num3] = component.myID;
			array2[1 + num3] = component.date_year;
			array2[2 + num3] = component.date_month;
			array2[3 + num3] = component.price;
			array2[4 + num3] = component.dev_costs;
			int num4 = i * 10;
			array3[num4] = component.effekt;
			int num5 = i * 10;
			array5[num5] = component.isUnlocked;
			array5[1 + num5] = component.inBesitz;
			array5[2 + num5] = component.neverLooseEffect;
		}
		writer.Write<int[]>("copyProtect_I", array2);
		writer.Write<float[]>("copyProtect_F", array3);
		writer.Write<string[]>("copyProtect_S", array4);
		writer.Write<bool[]>("copyProtect_B", array5);
	}

	
	private void LoadCopyProtect(ES3Reader reader, string filename)
	{
		int num = reader.Read<int>("anzCopyProtect", -1);
		Debug.Log("Load: (anzCopyProtect) " + num.ToString());
		if (num <= 0)
		{
			return;
		}
		int[] array = new int[10 * num];
		float[] array2 = new float[10 * num];
		string[] array3 = new string[20 * num];
		bool[] array4 = new bool[10 * num];
		array = reader.Read<int[]>("copyProtect_I");
		array2 = reader.Read<float[]>("copyProtect_F");
		array3 = reader.Read<string[]>("copyProtect_S");
		array4 = reader.Read<bool[]>("copyProtect_B");
		for (int i = 0; i < num; i++)
		{
			int num2 = i * 10;
			int num3 = i * 10;
			int num4 = i * 10;
			int num5 = i * 20;
			copyProtectScript copyProtectScript = this.copyProtect_.CreateCopyProtect();
			copyProtectScript.name_EN = array3[num5];
			copyProtectScript.name_GE = array3[1 + num5];
			copyProtectScript.name_TU = array3[2 + num5];
			copyProtectScript.name_CH = array3[3 + num5];
			copyProtectScript.name_FR = array3[4 + num5];
			copyProtectScript.name_CT = array3[5 + num5];
			copyProtectScript.name_RU = array3[6 + num5];
			copyProtectScript.name_IT = array3[7 + num5];
			copyProtectScript.name_JA = array3[8 + num5];
			copyProtectScript.myID = array[num2];
			copyProtectScript.date_year = array[1 + num2];
			copyProtectScript.date_month = array[2 + num2];
			copyProtectScript.price = array[3 + num2];
			copyProtectScript.dev_costs = array[4 + num2];
			copyProtectScript.effekt = array2[num3];
			copyProtectScript.isUnlocked = array4[num4];
			copyProtectScript.inBesitz = array4[1 + num4];
			copyProtectScript.neverLooseEffect = array4[2 + num4];
			copyProtectScript.Init();
		}
	}

	
	private void SaveAntiCheat(ES3Writer writer)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("AntiCheat");
		int num = array.Length;
		writer.Write<int>("anzAntiCheat", num);
		int[] array2 = new int[10 * num];
		float[] array3 = new float[10 * num];
		string[] array4 = new string[20 * num];
		bool[] array5 = new bool[10 * num];
		for (int i = 0; i < array.Length; i++)
		{
			antiCheatScript component = array[i].GetComponent<antiCheatScript>();
			int num2 = i * 20;
			array4[num2] = component.name_EN;
			array4[1 + num2] = component.name_GE;
			array4[2 + num2] = component.name_TU;
			array4[3 + num2] = component.name_CH;
			array4[4 + num2] = component.name_FR;
			array4[5 + num2] = component.name_CT;
			array4[6 + num2] = component.name_RU;
			array4[7 + num2] = component.name_IT;
			array4[8 + num2] = component.name_JA;
			int num3 = i * 10;
			array2[num3] = component.myID;
			array2[1 + num3] = component.date_year;
			array2[2 + num3] = component.date_month;
			array2[3 + num3] = component.price;
			array2[4 + num3] = component.dev_costs;
			int num4 = i * 10;
			array3[num4] = component.effekt;
			int num5 = i * 10;
			array5[num5] = component.isUnlocked;
			array5[1 + num5] = component.inBesitz;
			array5[2 + num5] = component.neverLooseEffect;
		}
		writer.Write<int[]>("antiCheat_I", array2);
		writer.Write<float[]>("antiCheat_F", array3);
		writer.Write<string[]>("antiCheat_S", array4);
		writer.Write<bool[]>("antiCheat_B", array5);
	}

	
	private void LoadAntiCheat(ES3Reader reader, string filename)
	{
		int num = reader.Read<int>("anzAntiCheat", -1);
		Debug.Log("Load: (anzAntiCheat) " + num.ToString());
		if (num <= 0)
		{
			return;
		}
		int[] array = new int[10 * num];
		float[] array2 = new float[10 * num];
		string[] array3 = new string[20 * num];
		bool[] array4 = new bool[10 * num];
		array = reader.Read<int[]>("antiCheat_I");
		array2 = reader.Read<float[]>("antiCheat_F");
		array3 = reader.Read<string[]>("antiCheat_S");
		array4 = reader.Read<bool[]>("antiCheat_B");
		for (int i = 0; i < num; i++)
		{
			int num2 = i * 10;
			int num3 = i * 10;
			int num4 = i * 10;
			int num5 = i * 20;
			antiCheatScript antiCheatScript = this.antiCheat_.CreateAntiCheat();
			antiCheatScript.name_EN = array3[num5];
			antiCheatScript.name_GE = array3[1 + num5];
			antiCheatScript.name_TU = array3[2 + num5];
			antiCheatScript.name_CH = array3[3 + num5];
			antiCheatScript.name_FR = array3[4 + num5];
			antiCheatScript.name_CT = array3[5 + num5];
			antiCheatScript.name_RU = array3[6 + num5];
			antiCheatScript.name_IT = array3[7 + num5];
			antiCheatScript.name_JA = array3[8 + num5];
			antiCheatScript.myID = array[num2];
			antiCheatScript.date_year = array[1 + num2];
			antiCheatScript.date_month = array[2 + num2];
			antiCheatScript.price = array[3 + num2];
			antiCheatScript.dev_costs = array[4 + num2];
			antiCheatScript.effekt = array2[num3];
			antiCheatScript.isUnlocked = array4[num4];
			antiCheatScript.inBesitz = array4[1 + num4];
			antiCheatScript.neverLooseEffect = array4[2 + num4];
			antiCheatScript.Init();
		}
	}

	
	private void SaveContractWork(ES3Writer writer)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("ContractWork");
		int num = array.Length;
		writer.Write<int>("anzContractWork", num);
		int[] array2 = new int[10 * num];
		float[] array3 = new float[5 * num];
		bool[] array4 = new bool[5 * num];
		for (int i = 0; i < array.Length; i++)
		{
			contractWork component = array[i].GetComponent<contractWork>();
			int num2 = i * 10;
			array2[num2] = component.myID;
			array2[1 + num2] = component.typ;
			array2[2 + num2] = component.gehalt;
			array2[3 + num2] = component.strafe;
			array2[4 + num2] = component.auftraggeberID;
			array2[5 + num2] = component.zeitInWochen;
			array2[6 + num2] = component.wochenAlsAngebot;
			array2[7 + num2] = component.art;
			int num3 = i * 5;
			array3[num3] = component.points;
			int num4 = i * 5;
			array4[num4] = component.angenommen;
		}
		writer.Write<int[]>("contractWork_I", array2);
		writer.Write<float[]>("contractWork_F", array3);
		writer.Write<bool[]>("contractWork_B", array4);
	}

	
	private void LoadContractWork(ES3Reader reader, string filename)
	{
		int num = reader.Read<int>("anzContractWork", -1);
		Debug.Log("Load: (anzContractWork) " + num.ToString());
		if (num <= 0)
		{
			return;
		}
		int[] array = new int[10 * num];
		float[] array2 = new float[5 * num];
		bool[] array3 = new bool[5 * num];
		array = reader.Read<int[]>("contractWork_I");
		array2 = reader.Read<float[]>("contractWork_F");
		array3 = reader.Read<bool[]>("contractWork_B");
		for (int i = 0; i < num; i++)
		{
			int num2 = i * 10;
			int num3 = i * 5;
			int num4 = i * 5;
			contractWork contractWork = this.contractWorkMain_.CreateContractWork();
			contractWork.myID = array[num2];
			contractWork.typ = array[1 + num2];
			contractWork.gehalt = array[2 + num2];
			contractWork.strafe = array[3 + num2];
			contractWork.auftraggeberID = array[4 + num2];
			contractWork.zeitInWochen = array[5 + num2];
			contractWork.wochenAlsAngebot = array[6 + num2];
			contractWork.art = array[7 + num2];
			contractWork.points = array2[num3];
			contractWork.angenommen = array3[num4];
			contractWork.Init();
		}
	}

	
	private void SaveContractGame(ES3Writer writer)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("ContractGame");
		int num = array.Length;
		writer.Write<int>("anzContractGame", num);
		int[] array2 = new int[15 * num];
		string[] array3 = new string[2 * num];
		bool[] array4 = new bool[5 * num];
		for (int i = 0; i < array.Length; i++)
		{
			contractAuftragsspiel component = array[i].GetComponent<contractAuftragsspiel>();
			int num2 = i * 15;
			array2[num2] = component.myID;
			array2[1 + num2] = component.gehalt;
			array2[2 + num2] = component.bonus;
			array2[3 + num2] = component.auftraggeberID;
			array2[4 + num2] = component.zeitInWochen;
			array2[5 + num2] = component.wochenAlsAngebot;
			array2[6 + num2] = component.mindestbewertung;
			array2[7 + num2] = component.genre;
			array2[8 + num2] = component.gameSize;
			array2[9 + num2] = component.platform;
			int num3 = i * 2;
			array3[num3] = component.gameName;
			int num4 = i * 5;
			array4[num4] = component.angenommen;
			array4[1 + num4] = component.zeitAbgelaufen;
		}
		writer.Write<int[]>("contractGame_I", array2);
		writer.Write<string[]>("contractGame_S", array3);
		writer.Write<bool[]>("contractGame_B", array4);
	}

	
	private void LoadContractGame(ES3Reader reader, string filename)
	{
		int num = reader.Read<int>("anzContractGame", -1);
		Debug.Log("Load: (anzContractGame) " + num.ToString());
		if (num <= 0)
		{
			return;
		}
		int[] array = new int[15 * num];
		string[] array2 = new string[2 * num];
		bool[] array3 = new bool[5 * num];
		array = reader.Read<int[]>("contractGame_I");
		array2 = reader.Read<string[]>("contractGame_S");
		array3 = reader.Read<bool[]>("contractGame_B");
		for (int i = 0; i < num; i++)
		{
			int num2 = i * 15;
			int num3 = i * 2;
			int num4 = i * 5;
			contractAuftragsspiel contractAuftragsspiel = this.contractWorkMain_.CreateContractGame();
			contractAuftragsspiel.myID = array[num2];
			contractAuftragsspiel.gehalt = array[1 + num2];
			contractAuftragsspiel.bonus = array[2 + num2];
			contractAuftragsspiel.auftraggeberID = array[3 + num2];
			contractAuftragsspiel.zeitInWochen = array[4 + num2];
			contractAuftragsspiel.wochenAlsAngebot = array[5 + num2];
			contractAuftragsspiel.mindestbewertung = array[6 + num2];
			contractAuftragsspiel.genre = array[7 + num2];
			contractAuftragsspiel.gameSize = array[8 + num2];
			contractAuftragsspiel.platform = array[9 + num2];
			contractAuftragsspiel.gameName = array2[num3];
			contractAuftragsspiel.angenommen = array3[num4];
			contractAuftragsspiel.zeitAbgelaufen = array3[1 + num4];
			contractAuftragsspiel.Init();
		}
	}

	
	private void SavePublishingOffer(ES3Writer writer)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("PubOffer");
		int num = array.Length;
		writer.Write<int>("anzPubOffer", num);
		int[] array2 = new int[15 * num];
		string[] array3 = new string[2 * num];
		float[] array4 = new float[5 * num];
		bool[] array5 = new bool[5 * num];
		for (int i = 0; i < array.Length; i++)
		{
			publishingOffer component = array[i].GetComponent<publishingOffer>();
			int num2 = i * 15;
			array2[num2] = component.myID;
			array2[1 + num2] = component.garantiesumme;
			array2[2 + num2] = component.developer;
			array2[3 + num2] = component.wochenAlsAngebot;
			array2[4 + num2] = component.genre;
			array2[5 + num2] = component.gameSize;
			array2[6 + num2] = component.gamePlatform[0];
			array2[7 + num2] = Mathf.RoundToInt(component.points_grafik);
			array2[8 + num2] = component.gameVorbild;
			array2[9 + num2] = component.gamePlatform[1];
			array2[10 + num2] = component.gamePlatform[2];
			array2[11 + num2] = component.gamePlatform[3];
			int num3 = i * 2;
			array3[num3] = component.gameName;
			int num4 = i * 5;
			array4[num4] = component.gewinnbeteiligung;
			array4[1 + num4] = component.stimmung;
			array4[2 + num4] = component.review;
			array4[3 + num4] = component.verhandlung;
			array4[4 + num4] = component.verhandlungProzent;
			int num5 = i * 5;
			array5[num5] = component.retail;
			array5[1 + num5] = component.digital;
			array5[2 + num5] = component.angebotWoche;
			array5[3 + num5] = component.nachfolger;
		}
		writer.Write<int[]>("pubOffer_I", array2);
		writer.Write<string[]>("pubOffer_S", array3);
		writer.Write<float[]>("pubOffer_F", array4);
		writer.Write<bool[]>("pubOffer_B", array5);
	}

	
	private void LoadPublishingOffer(ES3Reader reader, string filename)
	{
		int num = reader.Read<int>("anzPubOffer", -1);
		Debug.Log("Load: (anzPubOffer) " + num.ToString());
		if (num <= 0)
		{
			return;
		}
		int[] array = new int[15 * num];
		string[] array2 = new string[2 * num];
		float[] array3 = new float[5 * num];
		bool[] array4 = new bool[5 * num];
		array = reader.Read<int[]>("pubOffer_I");
		array2 = reader.Read<string[]>("pubOffer_S");
		array3 = reader.Read<float[]>("pubOffer_F");
		array4 = reader.Read<bool[]>("pubOffer_B");
		for (int i = 0; i < num; i++)
		{
			int num2 = i * 15;
			int num3 = i * 2;
			int num4 = i * 5;
			int num5 = i * 5;
			publishingOffer publishingOffer = this.publishingOfferMain_.CreatePublishingOffer();
			publishingOffer.myID = array[num2];
			publishingOffer.garantiesumme = array[1 + num2];
			publishingOffer.developer = array[2 + num2];
			publishingOffer.wochenAlsAngebot = array[3 + num2];
			publishingOffer.genre = array[4 + num2];
			publishingOffer.gameSize = array[5 + num2];
			publishingOffer.gamePlatform[0] = array[6 + num2];
			publishingOffer.points_grafik = (float)array[7 + num2];
			publishingOffer.gameVorbild = array[8 + num2];
			publishingOffer.gamePlatform[1] = array[9 + num2];
			publishingOffer.gamePlatform[2] = array[10 + num2];
			publishingOffer.gamePlatform[3] = array[11 + num2];
			publishingOffer.gameName = array2[num3];
			publishingOffer.gewinnbeteiligung = array3[num4];
			publishingOffer.stimmung = array3[1 + num4];
			publishingOffer.review = array3[2 + num4];
			publishingOffer.verhandlung = array3[3 + num4];
			publishingOffer.verhandlungProzent = array3[4 + num4];
			publishingOffer.retail = array4[num5];
			publishingOffer.digital = array4[1 + num5];
			publishingOffer.angebotWoche = array4[2 + num5];
			publishingOffer.nachfolger = array4[3 + num5];
			publishingOffer.Init();
		}
	}

	
	private void SavePlatforms(ES3Writer writer)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Platform");
		int num = array.Length;
		writer.Write<int>("anzPlatforms", num);
		int[] array2 = new int[20 * num];
		int[] array3 = new int[40 * num];
		float[] array4 = new float[10 * num];
		string[] array5 = new string[40 * num];
		bool[] array6 = new bool[10 * num];
		long[] array7 = new long[20 * num];
		for (int i = 0; i < array.Length; i++)
		{
			platformScript component = array[i].GetComponent<platformScript>();
			int num2 = i * 40;
			array5[num2] = component.name_EN;
			array5[1 + num2] = component.name_GE;
			array5[2 + num2] = component.manufacturer_EN;
			array5[3 + num2] = component.manufacturer_GE;
			array5[4 + num2] = component.pic1_file;
			array5[5 + num2] = component.pic2_file;
			array5[6 + num2] = component.name_TU;
			array5[7 + num2] = component.manufacturer_TU;
			array5[8 + num2] = component.name_CH;
			array5[9 + num2] = component.manufacturer_CH;
			array5[10 + num2] = component.name_FR;
			array5[11 + num2] = component.manufacturer_FR;
			array5[12 + num2] = component.name_HU;
			array5[13 + num2] = component.manufacturer_HU;
			array5[14 + num2] = component.name_JA;
			array5[15 + num2] = component.manufacturer_JA;
			array5[16 + num2] = component.myName;
			int num3 = i * 20;
			array2[num3] = component.myID;
			array2[1 + num3] = component.date_year;
			array2[2 + num3] = component.date_month;
			array2[3 + num3] = component.date_year_end;
			array2[4 + num3] = component.date_month_end;
			array2[5 + num3] = component.price;
			array2[6 + num3] = component.dev_costs;
			array2[7 + num3] = component.tech;
			array2[8 + num3] = component.units;
			array2[9 + num3] = component.units_max;
			array2[10 + num3] = component.erfahrung;
			array2[11 + num3] = component.pic2_year;
			array2[12 + num3] = component.games;
			array2[13 + num3] = component.needFeatures[0];
			array2[14 + num3] = component.needFeatures[1];
			array2[15 + num3] = component.needFeatures[2];
			array2[16 + num3] = component.complex;
			array2[17 + num3] = component.typ;
			array2[18 + num3] = component.multiplaySlot;
			array2[19 + num3] = component.anzController;
			int num4 = i * 40;
			array3[num4] = component.component_cpu;
			array3[1 + num4] = component.component_gfx;
			array3[2 + num4] = component.component_ram;
			array3[3 + num4] = component.component_hdd;
			array3[4 + num4] = component.component_sfx;
			array3[5 + num4] = component.component_cooling;
			array3[6 + num4] = component.component_disc;
			array3[7 + num4] = component.component_controller;
			array3[8 + num4] = component.component_case;
			array3[9 + num4] = component.component_monitor;
			array3[10 + num4] = component.gameID;
			array3[11 + num4] = component.costs_marketing;
			array3[12 + num4] = component.costs_mitarbeiter;
			array3[13 + num4] = component.startProduktionskosten;
			array3[14 + num4] = component.verkaufspreis;
			array3[15 + num4] = component.weeksOnMarket;
			array3[16 + num4] = component.performancePoints;
			array3[17 + num4] = component.autoPreisGewinn;
			array3[18 + num4] = component.weeksInDevelopment;
			array3[19 + num4] = component.exklusivGames;
			int num5 = i * 10;
			array4[num5] = component.marktanteil;
			array4[1 + num5] = component.powerFromMarket;
			array4[2 + num5] = component.conHueShift;
			array4[3 + num5] = component.conSaturation;
			array4[4 + num5] = component.devPointsStart;
			array4[5 + num5] = component.devPoints;
			array4[6 + num5] = component.hype;
			array4[7 + num5] = component.kostenreduktion;
			array4[8 + num5] = component.review;
			int num6 = i * 10;
			array6[num6] = component.npc;
			array6[1 + num6] = component.isUnlocked;
			array6[2 + num6] = component.inBesitz;
			array6[4 + num6] = component.vomMarktGenommen;
			array6[5 + num6] = component.internet;
			array6[6 + num6] = component.playerConsole;
			array6[7 + num6] = component.autoPreis;
			array6[8 + num6] = component.thridPartyGames;
			int num7 = i * 20;
			array7[num7] = component.einnahmen;
			array7[1 + num7] = component.entwicklungsKosten;
			array7[2 + num7] = component.umsatzTotal;
			array7[3 + num7] = component.costs_production;
			if (component.playerConsole || component.multiplaySlot != -1)
			{
				writer.Write<bool[]>("platformA1_" + component.myID.ToString(), component.hwFeatures);
				writer.Write<int[]>("platformA2_" + component.myID.ToString(), component.sellsPerWeek);
				writer.Write<bool[]>("platformA3_" + component.myID.ToString(), component.publisherBuyed);
			}
		}
		writer.Write<int[]>("platform_I", array2);
		writer.Write<int[]>("platform_I2", array3);
		writer.Write<float[]>("platform_F", array4);
		writer.Write<string[]>("platform_S", array5);
		writer.Write<bool[]>("platform_B", array6);
		writer.Write<long[]>("platform_L", array7);
	}

	
	private void LoadPlatforms(ES3Reader reader, string filename)
	{
		int num = reader.Read<int>("anzPlatforms", -1);
		Debug.Log("Load: (anzPlatforms) " + num.ToString());
		if (num <= 0)
		{
			return;
		}
		int[] array = new int[20 * num];
		int[] array2 = new int[40 * num];
		float[] array3 = new float[10 * num];
		string[] array4 = new string[40 * num];
		bool[] array5 = new bool[10 * num];
		long[] array6 = new long[20 * num];
		array = reader.Read<int[]>("platform_I");
		if (this.mS_.savegameVersion >= 11)
		{
			array2 = reader.Read<int[]>("platform_I2");
		}
		array3 = reader.Read<float[]>("platform_F");
		if (this.mS_.savegameVersion >= 11)
		{
			array6 = reader.Read<long[]>("platform_L");
		}
		array4 = reader.Read<string[]>("platform_S");
		array5 = reader.Read<bool[]>("platform_B");
		for (int i = 0; i < num; i++)
		{
			int num2 = i * 20;
			int num3 = i * 40;
			int num4 = i * 10;
			int num5 = i * 10;
			int num6 = i * 40;
			int num7 = i * 20;
			platformScript platformScript = this.platforms_.CreatePlatform();
			platformScript.name_EN = array4[num6];
			platformScript.name_GE = array4[1 + num6];
			platformScript.manufacturer_EN = array4[2 + num6];
			platformScript.manufacturer_GE = array4[3 + num6];
			platformScript.pic1_file = array4[4 + num6];
			platformScript.pic2_file = array4[5 + num6];
			platformScript.name_TU = array4[6 + num6];
			platformScript.manufacturer_TU = array4[7 + num6];
			platformScript.name_CH = array4[8 + num6];
			platformScript.manufacturer_CH = array4[9 + num6];
			platformScript.name_FR = array4[10 + num6];
			platformScript.manufacturer_FR = array4[11 + num6];
			platformScript.name_HU = array4[12 + num6];
			platformScript.manufacturer_HU = array4[13 + num6];
			platformScript.name_JA = array4[14 + num6];
			platformScript.manufacturer_JA = array4[15 + num6];
			platformScript.myName = array4[16 + num6];
			platformScript.myID = array[num2];
			platformScript.date_year = array[1 + num2];
			platformScript.date_month = array[2 + num2];
			platformScript.date_year_end = array[3 + num2];
			platformScript.date_month_end = array[4 + num2];
			platformScript.price = array[5 + num2];
			platformScript.dev_costs = array[6 + num2];
			platformScript.tech = array[7 + num2];
			platformScript.units = array[8 + num2];
			platformScript.units_max = array[9 + num2];
			platformScript.erfahrung = array[10 + num2];
			platformScript.pic2_year = array[11 + num2];
			platformScript.games = array[12 + num2];
			platformScript.needFeatures[0] = array[13 + num2];
			platformScript.needFeatures[1] = array[14 + num2];
			platformScript.needFeatures[2] = array[15 + num2];
			platformScript.complex = array[16 + num2];
			platformScript.typ = array[17 + num2];
			platformScript.multiplaySlot = array[18 + num2];
			platformScript.anzController = array[19 + num2];
			platformScript.component_cpu = array2[num3];
			platformScript.component_gfx = array2[1 + num3];
			platformScript.component_ram = array2[2 + num3];
			platformScript.component_hdd = array2[3 + num3];
			platformScript.component_sfx = array2[4 + num3];
			platformScript.component_cooling = array2[5 + num3];
			platformScript.component_disc = array2[6 + num3];
			platformScript.component_controller = array2[7 + num3];
			platformScript.component_case = array2[8 + num3];
			platformScript.component_monitor = array2[9 + num3];
			platformScript.gameID = array2[10 + num3];
			platformScript.costs_marketing = array2[11 + num3];
			platformScript.costs_mitarbeiter = array2[12 + num3];
			platformScript.startProduktionskosten = array2[13 + num3];
			platformScript.verkaufspreis = array2[14 + num3];
			platformScript.weeksOnMarket = array2[15 + num3];
			platformScript.performancePoints = array2[16 + num3];
			platformScript.autoPreisGewinn = array2[17 + num3];
			platformScript.weeksInDevelopment = array2[18 + num3];
			platformScript.exklusivGames = array2[19 + num3];
			platformScript.marktanteil = array3[num4];
			platformScript.powerFromMarket = array3[1 + num4];
			platformScript.conHueShift = array3[2 + num4];
			platformScript.conSaturation = array3[3 + num4];
			platformScript.devPointsStart = array3[4 + num4];
			platformScript.devPoints = array3[5 + num4];
			platformScript.hype = array3[6 + num4];
			platformScript.kostenreduktion = array3[7 + num4];
			platformScript.review = array3[8 + num4];
			platformScript.npc = array5[num5];
			platformScript.isUnlocked = array5[1 + num5];
			platformScript.inBesitz = array5[2 + num5];
			platformScript.vomMarktGenommen = array5[4 + num5];
			platformScript.internet = array5[5 + num5];
			platformScript.playerConsole = array5[6 + num5];
			platformScript.autoPreis = array5[7 + num5];
			platformScript.thridPartyGames = array5[8 + num5];
			if (this.mS_.savegameVersion >= 11)
			{
				platformScript.einnahmen = array6[num7];
				platformScript.entwicklungsKosten = array6[1 + num7];
				platformScript.umsatzTotal = array6[2 + num7];
				platformScript.costs_production = array6[3 + num7];
			}
			if (this.mS_.savegameVersion >= 13 && (platformScript.playerConsole || platformScript.multiplaySlot != -1))
			{
				platformScript.hwFeatures = reader.Read<bool[]>("platformA1_" + platformScript.myID.ToString());
				platformScript.sellsPerWeek = reader.Read<int[]>("platformA2_" + platformScript.myID.ToString());
				platformScript.publisherBuyed = reader.Read<bool[]>("platformA3_" + platformScript.myID.ToString());
			}
			platformScript.Init();
			platformScript.InitUI();
		}
		if (this.mS_.savegameVersion < 7)
		{
			this.platforms_.LoadPlatformDataForOldSavegames("DATA/Platforms.txt");
		}
	}

	
	private void SaveArbeitsmarkt(ES3Writer writer)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Arbeitsmarkt");
		int num = array.Length;
		writer.Write<int>("anzArbeitsmarkt", num);
		int[] array2 = new int[20 * num];
		float[] array3 = new float[10 * num];
		string[] array4 = new string[5 * num];
		bool[] array5 = new bool[20 * num];
		int num2 = 0;
		if (array.Length != 0 && array[0])
		{
			charArbeitsmarkt component = array[0].GetComponent<charArbeitsmarkt>();
			if (component)
			{
				num2 = component.perks.Length;
			}
		}
		bool[] array6 = new bool[num2 * num];
		for (int i = 0; i < array.Length; i++)
		{
			charArbeitsmarkt component2 = array[i].GetComponent<charArbeitsmarkt>();
			int num3 = i * 5;
			array4[num3] = component2.myName;
			int num4 = i * 20;
			array2[num4] = component2.myID;
			array2[1 + num4] = component2.wochenAmArbeitsmarkt;
			array2[2 + num4] = component2.legend;
			array2[3 + num4] = component2.model_body;
			array2[4 + num4] = component2.model_eyes;
			array2[5 + num4] = component2.model_hair;
			array2[6 + num4] = component2.model_beard;
			array2[7 + num4] = component2.model_skinColor;
			array2[8 + num4] = component2.model_hairColor;
			array2[9 + num4] = component2.model_beardColor;
			array2[10 + num4] = component2.model_HoseColor;
			array2[11 + num4] = component2.model_ShirtColor;
			array2[12 + num4] = component2.model_Add1Color;
			array2[13 + num4] = component2.beruf;
			int num5 = i * 10;
			array3[num5] = component2.s_gamedesign;
			array3[1 + num5] = component2.s_programmieren;
			array3[2 + num5] = component2.s_grafik;
			array3[3 + num5] = component2.s_sound;
			array3[4 + num5] = component2.s_pr;
			array3[5 + num5] = component2.s_gametests;
			array3[6 + num5] = component2.s_technik;
			array3[7 + num5] = component2.s_forschen;
			int num6 = i * 20;
			array5[num6] = component2.male;
			int num7 = i * num2;
			for (int j = 0; j < component2.perks.Length; j++)
			{
				array6[j + num7] = component2.perks[j];
			}
		}
		writer.Write<int[]>("arbeitsmarkt_I", array2);
		writer.Write<float[]>("arbeitsmarkt_F", array3);
		writer.Write<string[]>("arbeitsmarkt_S", array4);
		writer.Write<bool[]>("arbeitsmarkt_B", array5);
		writer.Write<bool[]>("arbeitsmarkt_perks", array6);
	}

	
	private void LoadArbeitsmarkt(ES3Reader reader, string filename)
	{
		int num = reader.Read<int>("anzArbeitsmarkt", -1);
		Debug.Log("Load: (anzArbeitsmarkt) " + num.ToString());
		if (num <= 0)
		{
			return;
		}
		int[] array = new int[20 * num];
		float[] array2 = new float[10 * num];
		string[] array3 = new string[5 * num];
		bool[] array4 = new bool[20 * num];
		bool[] array5 = new bool[0];
		array = reader.Read<int[]>("arbeitsmarkt_I");
		array2 = reader.Read<float[]>("arbeitsmarkt_F");
		array3 = reader.Read<string[]>("arbeitsmarkt_S");
		array4 = reader.Read<bool[]>("arbeitsmarkt_B");
		if (this.mS_.savegameVersion >= 16)
		{
			array5 = reader.Read<bool[]>("arbeitsmarkt_perks");
		}
		for (int i = 0; i < num; i++)
		{
			int num2 = i * 20;
			int num3 = i * 10;
			int num4 = i * 20;
			int num5 = i * 5;
			charArbeitsmarkt charArbeitsmarkt = this.arbeitsmarkt_.CreateArbeitsmarktItem();
			charArbeitsmarkt.myName = array3[num5];
			charArbeitsmarkt.myID = array[num2];
			charArbeitsmarkt.wochenAmArbeitsmarkt = array[1 + num2];
			charArbeitsmarkt.legend = array[2 + num2];
			charArbeitsmarkt.model_body = array[3 + num2];
			charArbeitsmarkt.model_eyes = array[4 + num2];
			charArbeitsmarkt.model_hair = array[5 + num2];
			charArbeitsmarkt.model_beard = array[6 + num2];
			charArbeitsmarkt.model_skinColor = array[7 + num2];
			charArbeitsmarkt.model_hairColor = array[8 + num2];
			charArbeitsmarkt.model_beardColor = array[9 + num2];
			charArbeitsmarkt.model_HoseColor = array[10 + num2];
			charArbeitsmarkt.model_ShirtColor = array[11 + num2];
			charArbeitsmarkt.model_Add1Color = array[12 + num2];
			charArbeitsmarkt.beruf = array[13 + num2];
			charArbeitsmarkt.s_gamedesign = array2[num3];
			charArbeitsmarkt.s_programmieren = array2[1 + num3];
			charArbeitsmarkt.s_grafik = array2[2 + num3];
			charArbeitsmarkt.s_sound = array2[3 + num3];
			charArbeitsmarkt.s_pr = array2[4 + num3];
			charArbeitsmarkt.s_gametests = array2[5 + num3];
			charArbeitsmarkt.s_technik = array2[6 + num3];
			charArbeitsmarkt.s_forschen = array2[7 + num3];
			charArbeitsmarkt.male = array4[num4];
			if (this.mS_.savegameVersion < 16)
			{
				if (this.es3file.KeyExists("arbeitsmarktA1_"))
				{
					charArbeitsmarkt.perks = reader.Read<bool[]>("arbeitsmarktA1_" + charArbeitsmarkt.myID.ToString());
				}
				if (charArbeitsmarkt.perks.Length < 40)
				{
					bool[] array6 = (bool[])charArbeitsmarkt.perks.Clone();
					charArbeitsmarkt.perks = new bool[40];
					for (int j = 0; j < array6.Length; j++)
					{
						charArbeitsmarkt.perks[j] = array6[j];
					}
				}
			}
			else
			{
				charArbeitsmarkt.perks = new bool[array5.Length / num];
				int num6 = i * (array5.Length / num);
				for (int k = 0; k < charArbeitsmarkt.perks.Length; k++)
				{
					charArbeitsmarkt.perks[k] = array5[k + num6];
				}
			}
			charArbeitsmarkt.gameObject.name = "AA_" + charArbeitsmarkt.myID.ToString();
		}
	}

	
	private void SavePublisher(ES3Writer writer)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Publisher");
		int num = array.Length;
		writer.Write<int>("anzPublisher", num);
		long[] array2 = new long[25 * num];
		int[] array3 = new int[24 * num];
		float[] array4 = new float[3 * num];
		string[] array5 = new string[6 * num];
		bool[] array6 = new bool[24 * num];
		for (int i = 0; i < array.Length; i++)
		{
			publisherScript component = array[i].GetComponent<publisherScript>();
			int num2 = i * (array5.Length / num);
			int num3 = i * (array3.Length / num);
			int num4 = i * (array4.Length / num);
			int num5 = i * (array6.Length / num);
			int num6 = i * (array2.Length / num);
			array5[num2] = component.name_EN;
			array5[1 + num2] = component.name_GE;
			array5[2 + num2] = component.name_TU;
			array5[3 + num2] = component.name_CH;
			array5[4 + num2] = component.name_FR;
			array5[5 + num2] = component.name_JA;
			array3[num3] = component.myID;
			array3[1 + num3] = component.date_year;
			array3[2 + num3] = component.date_month;
			array3[3 + num3] = component.logoID;
			array3[4 + num3] = component.fanGenre;
			array3[5 + num3] = component.newGameInWeeks;
			array3[6 + num3] = component.exklusivLaufzeit;
			array3[7 + num3] = component.developmentSpeed;
			array3[8 + num3] = component.multiplayerID;
			array3[9 + num3] = component.newGameInWeeksORG;
			array3[10 + num3] = component.tf_entwicklungsdauer;
			array3[11 + num3] = component.tf_gameSize;
			array3[12 + num3] = component.tf_gameGenre;
			array3[13 + num3] = component.tf_gameTopic;
			array3[14 + num3] = component.tf_ipFocus[0];
			array3[15 + num3] = component.lockToBuy;
			array3[16 + num3] = component.tf_ipFocus[1];
			array3[17 + num3] = component.tf_ipFocus[2];
			array3[18 + num3] = component.tf_engine;
			array3[19 + num3] = component.tf_autoReleaseVal;
			array3[20 + num3] = component.tf_platformFocus[0];
			array3[21 + num3] = component.tf_platformFocus[1];
			array3[22 + num3] = component.tf_platformFocus[2];
			array3[23 + num3] = component.tf_platformFocus[3];
			array4[num4] = component.stars;
			array4[1 + num4] = component.relation;
			array4[2 + num4] = component.share;
			array6[num5] = component.isUnlocked;
			array6[1 + num5] = component.developer;
			array6[2 + num5] = component.publisher;
			array6[3 + num5] = component.ownPlatform;
			array6[4 + num5] = component.exklusive;
			array6[5 + num5] = component.notForSale;
			array6[6 + num5] = component.tochterfirma;
			array6[7 + num5] = component.tf_geschlossen;
			array6[8 + num5] = component.tf_autoRelease;
			array6[9 + num5] = component.tf_onlyPlayerConsole;
			array6[10 + num5] = component.tf_allowMMO;
			array6[11 + num5] = component.tf_allowF2P;
			array6[12 + num5] = component.tf_allowAddon;
			array6[13 + num5] = component.tf_noArcade;
			array6[14 + num5] = component.tf_noHandy;
			array6[15 + num5] = component.tf_noRetro;
			array6[16 + num5] = component.tf_noPorts;
			array6[17 + num5] = component.tf_noBudget;
			array6[18 + num5] = component.tf_noGOTY;
			array6[19 + num5] = component.tf_publisher;
			array6[20 + num5] = component.tf_developer;
			array6[21 + num5] = component.tf_noRemaster;
			array6[22 + num5] = component.tf_noSpinoffs;
			array6[23 + num5] = component.tf_ownPublisher;
			array2[num6] = component.firmenwert;
			array2[1 + num6] = component.tf_umsatz[0];
			array2[2 + num6] = component.tf_umsatz[1];
			array2[3 + num6] = component.tf_umsatz[2];
			array2[4 + num6] = component.tf_umsatz[3];
			array2[5 + num6] = component.tf_umsatz[4];
			array2[6 + num6] = component.tf_umsatz[5];
			array2[7 + num6] = component.tf_umsatz[6];
			array2[8 + num6] = component.tf_umsatz[7];
			array2[9 + num6] = component.tf_umsatz[8];
			array2[10 + num6] = component.tf_umsatz[9];
			array2[11 + num6] = component.tf_umsatz[10];
			array2[12 + num6] = component.tf_umsatz[11];
			array2[13 + num6] = component.tf_umsatz[12];
			array2[14 + num6] = component.tf_umsatz[13];
			array2[15 + num6] = component.tf_umsatz[14];
			array2[16 + num6] = component.tf_umsatz[15];
			array2[17 + num6] = component.tf_umsatz[16];
			array2[18 + num6] = component.tf_umsatz[17];
			array2[19 + num6] = component.tf_umsatz[18];
			array2[20 + num6] = component.tf_umsatz[19];
			array2[21 + num6] = component.tf_umsatz[20];
			array2[22 + num6] = component.tf_umsatz[21];
			array2[23 + num6] = component.tf_umsatz[22];
			array2[24 + num6] = component.tf_umsatz[23];
		}
		writer.Write<int[]>("publisher_I", array3);
		writer.Write<float[]>("publisher_F", array4);
		writer.Write<string[]>("publisher_S", array5);
		writer.Write<bool[]>("publisher_B", array6);
		writer.Write<long[]>("publisher_L", array2);
	}

	
	private void LoadPublisher(ES3Reader reader, string filename)
	{
		int num = reader.Read<int>("anzPublisher", -1);
		Debug.Log("Load: (anzPublisher) " + num.ToString());
		if (num <= 0)
		{
			return;
		}
		long[] array = new long[0];
		int[] array2 = new int[0];
		float[] array3 = new float[0];
		string[] array4 = new string[0];
		bool[] array5 = new bool[0];
		array2 = reader.Read<int[]>("publisher_I");
		array3 = reader.Read<float[]>("publisher_F");
		array4 = reader.Read<string[]>("publisher_S");
		array5 = reader.Read<bool[]>("publisher_B");
		if (this.es3file.KeyExists("publisher_L"))
		{
			array = reader.Read<long[]>("publisher_L");
		}
		int num2 = array2.Length / num;
		int num3 = array3.Length / num;
		int num4 = array5.Length / num;
		int num5 = array4.Length / num;
		int num6 = array.Length / num;
		for (int i = 0; i < num; i++)
		{
			int num7 = i * num2;
			int num8 = i * num3;
			int num9 = i * num4;
			int num10 = i * num5;
			int num11 = i * num6;
			publisherScript publisherScript = this.publisher_.CreatePublisher();
			publisherScript.name_EN = array4[num10];
			publisherScript.name_GE = array4[1 + num10];
			publisherScript.name_TU = array4[2 + num10];
			publisherScript.name_CH = array4[3 + num10];
			publisherScript.name_FR = array4[4 + num10];
			publisherScript.name_JA = array4[5 + num10];
			publisherScript.myID = array2[num7];
			publisherScript.date_year = array2[1 + num7];
			publisherScript.date_month = array2[2 + num7];
			publisherScript.logoID = array2[3 + num7];
			publisherScript.fanGenre = array2[4 + num7];
			publisherScript.newGameInWeeks = array2[5 + num7];
			publisherScript.exklusivLaufzeit = array2[6 + num7];
			publisherScript.developmentSpeed = array2[7 + num7];
			publisherScript.multiplayerID = array2[8 + num7];
			publisherScript.newGameInWeeksORG = array2[9 + num7];
			if (num2 > 10)
			{
				publisherScript.tf_entwicklungsdauer = array2[10 + num7];
			}
			if (num2 > 11)
			{
				publisherScript.tf_gameSize = array2[11 + num7];
			}
			if (num2 > 12)
			{
				publisherScript.tf_gameGenre = array2[12 + num7];
			}
			if (num2 > 13)
			{
				publisherScript.tf_gameTopic = array2[13 + num7];
			}
			else
			{
				publisherScript.tf_gameTopic = -1;
			}
			if (num2 > 14)
			{
				publisherScript.tf_ipFocus[0] = array2[14 + num7];
			}
			else
			{
				publisherScript.tf_ipFocus[0] = -1;
			}
			if (num2 > 15)
			{
				publisherScript.lockToBuy = array2[15 + num7];
			}
			if (num2 > 16)
			{
				publisherScript.tf_ipFocus[1] = array2[16 + num7];
			}
			else
			{
				publisherScript.tf_ipFocus[1] = -1;
			}
			if (num2 > 17)
			{
				publisherScript.tf_ipFocus[2] = array2[17 + num7];
			}
			else
			{
				publisherScript.tf_ipFocus[2] = -1;
			}
			if (num2 > 18)
			{
				publisherScript.tf_engine = array2[18 + num7];
			}
			else
			{
				publisherScript.tf_engine = -1;
			}
			if (num2 > 19)
			{
				publisherScript.tf_autoReleaseVal = array2[19 + num7];
			}
			if (num2 > 20)
			{
				publisherScript.tf_platformFocus[0] = array2[20 + num7];
			}
			else
			{
				publisherScript.tf_platformFocus[0] = -1;
			}
			if (num2 > 21)
			{
				publisherScript.tf_platformFocus[1] = array2[21 + num7];
			}
			else
			{
				publisherScript.tf_platformFocus[1] = -1;
			}
			if (num2 > 22)
			{
				publisherScript.tf_platformFocus[2] = array2[22 + num7];
			}
			else
			{
				publisherScript.tf_platformFocus[2] = -1;
			}
			if (num2 > 23)
			{
				publisherScript.tf_platformFocus[3] = array2[23 + num7];
			}
			else
			{
				publisherScript.tf_platformFocus[3] = -1;
			}
			publisherScript.stars = array3[num8];
			publisherScript.relation = array3[1 + num8];
			publisherScript.share = array3[2 + num8];
			publisherScript.isUnlocked = array5[num9];
			publisherScript.developer = array5[1 + num9];
			publisherScript.publisher = array5[2 + num9];
			publisherScript.ownPlatform = array5[3 + num9];
			publisherScript.exklusive = array5[4 + num9];
			publisherScript.notForSale = array5[5 + num9];
			publisherScript.tochterfirma = array5[6 + num9];
			publisherScript.tf_geschlossen = array5[7 + num9];
			publisherScript.tf_autoRelease = array5[8 + num9];
			publisherScript.tf_onlyPlayerConsole = array5[9 + num9];
			if (num4 > 10)
			{
				publisherScript.tf_allowMMO = array5[10 + num9];
			}
			if (num4 > 11)
			{
				publisherScript.tf_allowF2P = array5[11 + num9];
			}
			if (num4 > 12)
			{
				publisherScript.tf_allowAddon = array5[12 + num9];
			}
			if (num4 > 13)
			{
				publisherScript.tf_noArcade = array5[13 + num9];
			}
			if (num4 > 14)
			{
				publisherScript.tf_noHandy = array5[14 + num9];
			}
			if (num4 > 15)
			{
				publisherScript.tf_noRetro = array5[15 + num9];
			}
			if (num4 > 16)
			{
				publisherScript.tf_noPorts = array5[16 + num9];
			}
			if (num4 > 17)
			{
				publisherScript.tf_noBudget = array5[17 + num9];
			}
			if (num4 > 18)
			{
				publisherScript.tf_noGOTY = array5[18 + num9];
			}
			if (num4 > 19)
			{
				publisherScript.tf_publisher = array5[19 + num9];
			}
			if (num4 > 20)
			{
				publisherScript.tf_developer = array5[20 + num9];
			}
			if (num4 > 21)
			{
				publisherScript.tf_noRemaster = array5[21 + num9];
			}
			if (num4 > 22)
			{
				publisherScript.tf_noSpinoffs = array5[22 + num9];
			}
			if (num4 > 23)
			{
				publisherScript.tf_ownPublisher = array5[23 + num9];
			}
			if (num6 > 0)
			{
				publisherScript.firmenwert = array[num11];
			}
			if (num6 > 1)
			{
				publisherScript.tf_umsatz[0] = array[1 + num11];
			}
			if (num6 > 2)
			{
				publisherScript.tf_umsatz[1] = array[2 + num11];
			}
			if (num6 > 3)
			{
				publisherScript.tf_umsatz[2] = array[3 + num11];
			}
			if (num6 > 4)
			{
				publisherScript.tf_umsatz[3] = array[4 + num11];
			}
			if (num6 > 5)
			{
				publisherScript.tf_umsatz[4] = array[5 + num11];
			}
			if (num6 > 6)
			{
				publisherScript.tf_umsatz[5] = array[6 + num11];
			}
			if (num6 > 7)
			{
				publisherScript.tf_umsatz[6] = array[7 + num11];
			}
			if (num6 > 8)
			{
				publisherScript.tf_umsatz[7] = array[8 + num11];
			}
			if (num6 > 9)
			{
				publisherScript.tf_umsatz[8] = array[9 + num11];
			}
			if (num6 > 10)
			{
				publisherScript.tf_umsatz[9] = array[10 + num11];
			}
			if (num6 > 11)
			{
				publisherScript.tf_umsatz[10] = array[11 + num11];
			}
			if (num6 > 12)
			{
				publisherScript.tf_umsatz[11] = array[12 + num11];
			}
			if (num6 > 13)
			{
				publisherScript.tf_umsatz[12] = array[13 + num11];
			}
			if (num6 > 14)
			{
				publisherScript.tf_umsatz[13] = array[14 + num11];
			}
			if (num6 > 15)
			{
				publisherScript.tf_umsatz[14] = array[15 + num11];
			}
			if (num6 > 16)
			{
				publisherScript.tf_umsatz[15] = array[16 + num11];
			}
			if (num6 > 17)
			{
				publisherScript.tf_umsatz[16] = array[17 + num11];
			}
			if (num6 > 18)
			{
				publisherScript.tf_umsatz[17] = array[18 + num11];
			}
			if (num6 > 19)
			{
				publisherScript.tf_umsatz[18] = array[19 + num11];
			}
			if (num6 > 20)
			{
				publisherScript.tf_umsatz[19] = array[20 + num11];
			}
			if (num6 > 21)
			{
				publisherScript.tf_umsatz[20] = array[21 + num11];
			}
			if (num6 > 22)
			{
				publisherScript.tf_umsatz[21] = array[22 + num11];
			}
			if (num6 > 23)
			{
				publisherScript.tf_umsatz[22] = array[23 + num11];
			}
			if (num6 > 24)
			{
				publisherScript.tf_umsatz[23] = array[24 + num11];
			}
			publisherScript.Init();
		}
	}

	
	private void SaveEngines(ES3Writer writer)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Engine");
		int num = array.Length;
		writer.Write<int>("anzEngines", num);
		int[] array2 = new int[10 * num];
		float[] array3 = new float[10 * num];
		string[] array4 = new string[20 * num];
		bool[] array5 = new bool[10 * num];
		int num2 = this.eF_.engineFeatures_UNLOCK.Length;
		int num3 = this.eF_.engineFeatures_UNLOCK.Length;
		int num4 = GameObject.FindGameObjectsWithTag("Publisher").Length;
		bool[] array6 = new bool[num2 * num];
		bool[] array7 = new bool[num3 * num];
		bool[] array8 = new bool[num4 * num];
		for (int i = 0; i < array.Length; i++)
		{
			engineScript component = array[i].GetComponent<engineScript>();
			int num5 = i * 20;
			array4[num5] = component.myName;
			array4[1 + num5] = component.name_EN;
			array4[2 + num5] = component.name_GE;
			array4[3 + num5] = component.name_TU;
			array4[4 + num5] = component.name_CH;
			array4[5 + num5] = component.name_FR;
			array4[6 + num5] = component.name_HU;
			array4[7 + num5] = component.name_CT;
			array4[8 + num5] = component.name_CZ;
			array4[9 + num5] = component.name_PB;
			array4[10 + num5] = component.name_IT;
			array4[11 + num5] = component.name_JA;
			array4[12 + num5] = component.name_PL;
			int num6 = i * 10;
			array2[num6] = component.myID;
			array2[1 + num6] = component.spezialgenre;
			array2[2 + num6] = component.preis;
			array2[3 + num6] = component.gewinnbeteiligung;
			array2[4 + num6] = component.date_year;
			array2[5 + num6] = component.date_month;
			array2[6 + num6] = component.multiplayerSlot;
			array2[7 + num6] = component.spezialplatform;
			array2[8 + num6] = component.umsatz;
			array2[9 + num6] = component.spezialplatformUpdate;
			int num7 = i * 10;
			array3[num7] = component.devPoints;
			array3[1 + num7] = component.devPointsStart;
			int num8 = i * 10;
			array5[num8] = component.playerEngine;
			array5[1 + num8] = component.isUnlocked;
			array5[2 + num8] = component.gekauft;
			array5[3 + num8] = component.sellEngine;
			array5[4 + num8] = component.updating;
			array5[5 + num8] = component.archiv_engine;
			if (this.mS_.savegameVersion != 0 && this.mS_.savegameVersion < 16)
			{
				writer.Write<bool[]>("EngineA1_" + component.myID.ToString(), component.features);
				writer.Write<bool[]>("EngineA2_" + component.myID.ToString(), component.featuresInDev);
				writer.Write<bool[]>("EngineA3_" + component.myID.ToString(), component.publisherBuyed);
			}
			else
			{
				if (component.features == null || component.features.Length == 0)
				{
					component.features = new bool[num2];
				}
				int num9 = i * num2;
				for (int j = 0; j < num2; j++)
				{
					array6[j + num9] = component.features[j];
				}
				if (component.featuresInDev == null || component.featuresInDev.Length == 0)
				{
					component.featuresInDev = new bool[num3];
				}
				num9 = i * num3;
				for (int k = 0; k < num3; k++)
				{
					array7[k + num9] = component.featuresInDev[k];
				}
				if (component.publisherBuyed == null || component.publisherBuyed.Length == 0)
				{
					component.publisherBuyed = new bool[num4];
				}
				num9 = i * num4;
				for (int l = 0; l < num4; l++)
				{
					array8[l + num9] = component.publisherBuyed[l];
				}
			}
		}
		writer.Write<int[]>("engines_I", array2);
		writer.Write<float[]>("engines_F", array3);
		writer.Write<string[]>("engines_S", array4);
		writer.Write<bool[]>("engines_B", array5);
		writer.Write<bool[]>("engines_features", array6);
		writer.Write<bool[]>("engines_featuresInDev", array7);
		writer.Write<bool[]>("engines_publisherBuyed", array8);
	}

	
	private void LoadEngines(ES3Reader reader, string filename)
	{
		int num = reader.Read<int>("anzEngines", -1);
		Debug.Log("Load: (anzEngines) " + num.ToString());
		if (num <= 0)
		{
			return;
		}
		bool[] array = new bool[0];
		bool[] array2 = new bool[0];
		bool[] array3 = new bool[0];
		int[] array4 = reader.Read<int[]>("engines_I");
		float[] array5 = reader.Read<float[]>("engines_F");
		string[] array6 = reader.Read<string[]>("engines_S");
		bool[] array7 = reader.Read<bool[]>("engines_B");
		if (this.mS_.savegameVersion >= 16)
		{
			array = reader.Read<bool[]>("engines_features");
			array2 = reader.Read<bool[]>("engines_featuresInDev");
			array3 = reader.Read<bool[]>("engines_publisherBuyed");
		}
		for (int i = 0; i < num; i++)
		{
			int num2 = i * 10;
			int num3 = i * 10;
			int num4 = i * 10;
			int num5 = i * 20;
			engineScript engineScript = this.eF_.CreateEngine();
			engineScript.myName = array6[num5];
			engineScript.name_EN = array6[1 + num5];
			engineScript.name_GE = array6[2 + num5];
			engineScript.name_TU = array6[3 + num5];
			engineScript.name_CH = array6[4 + num5];
			engineScript.name_FR = array6[5 + num5];
			engineScript.name_HU = array6[6 + num5];
			engineScript.name_CT = array6[7 + num5];
			engineScript.name_CZ = array6[8 + num5];
			engineScript.name_PB = array6[9 + num5];
			engineScript.name_IT = array6[10 + num5];
			engineScript.name_JA = array6[11 + num5];
			engineScript.name_PL = array6[12 + num5];
			engineScript.myID = array4[num2];
			engineScript.spezialgenre = array4[1 + num2];
			engineScript.preis = array4[2 + num2];
			engineScript.gewinnbeteiligung = array4[3 + num2];
			engineScript.date_year = array4[4 + num2];
			engineScript.date_month = array4[5 + num2];
			engineScript.multiplayerSlot = array4[6 + num2];
			engineScript.spezialplatform = array4[7 + num2];
			engineScript.umsatz = array4[8 + num2];
			engineScript.spezialplatformUpdate = array4[9 + num2];
			engineScript.devPoints = array5[num3];
			engineScript.devPointsStart = array5[1 + num3];
			engineScript.playerEngine = array7[num4];
			engineScript.isUnlocked = array7[1 + num4];
			engineScript.gekauft = array7[2 + num4];
			engineScript.sellEngine = array7[3 + num4];
			engineScript.updating = array7[4 + num4];
			engineScript.archiv_engine = array7[5 + num4];
			if (this.mS_.savegameVersion < 16)
			{
				engineScript.features = reader.Read<bool[]>("EngineA1_" + engineScript.myID.ToString());
				engineScript.featuresInDev = reader.Read<bool[]>("EngineA2_" + engineScript.myID.ToString());
				engineScript.publisherBuyed = reader.Read<bool[]>("EngineA3_" + engineScript.myID.ToString());
			}
			else
			{
				engineScript.features = new bool[array.Length / num];
				engineScript.featuresInDev = new bool[array2.Length / num];
				engineScript.publisherBuyed = new bool[array3.Length / num];
				int num6 = i * (array.Length / num);
				for (int j = 0; j < engineScript.features.Length; j++)
				{
					engineScript.features[j] = array[j + num6];
				}
				num6 = i * (array2.Length / num);
				for (int k = 0; k < engineScript.featuresInDev.Length; k++)
				{
					engineScript.featuresInDev[k] = array2[k + num6];
				}
				num6 = i * (array3.Length / num);
				for (int l = 0; l < engineScript.publisherBuyed.Length; l++)
				{
					engineScript.publisherBuyed[l] = array3[l + num6];
				}
			}
			engineScript.Init();
		}
	}

	
	private void SaveGames(ES3Writer writer)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		int num = array.Length;
		writer.Write<int>("anzGames", num);
		long[] array2 = new long[55 * num];
		int[] array3 = new int[135 * num];
		float[] array4 = new float[31 * num];
		string[] array5 = new string[3 * num];
		bool[] array6 = new bool[135 * num];
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		if (array.Length != 0 && array[0])
		{
			gameScript component = array[0].GetComponent<gameScript>();
			if (component)
			{
				num2 = component.gameGameplayFeatures.Length;
				num3 = component.gameplayFeatures_DevDone.Length;
				num4 = component.fanbrief.Length;
			}
		}
		bool[] array7 = new bool[num2 * num];
		bool[] array8 = new bool[num3 * num];
		bool[] array9 = new bool[num4 * num];
		for (int i = 0; i < array.Length; i++)
		{
			gameScript component2 = array[i].GetComponent<gameScript>();
			int num5 = i * (array5.Length / num);
			array5[num5] = component2.GetNameSimple();
			array5[1 + num5] = component2.beschreibung;
			array5[2 + num5] = component2.ipName;
			int num6 = i * (array3.Length / num);
			array3[num6] = component2.myID;
			array3[1 + num6] = component2.developerID;
			array3[2 + num6] = component2.engineID;
			array3[3 + num6] = component2.reviewGameplay;
			array3[4 + num6] = component2.reviewGrafik;
			array3[5 + num6] = component2.reviewSound;
			array3[6 + num6] = component2.reviewSteuerung;
			array3[7 + num6] = component2.reviewTotal;
			array3[8 + num6] = component2.reviewGameplayText;
			array3[9 + num6] = component2.reviewGrafikText;
			array3[10 + num6] = component2.reviewSoundText;
			array3[11 + num6] = component2.reviewSteuerungText;
			array3[12 + num6] = component2.reviewTotalText;
			array3[13 + num6] = component2.date_year;
			array3[14 + num6] = component2.date_month;
			array3[16 + num6] = component2.devAktFeature;
			array3[17 + num6] = component2.gameTyp;
			array3[18 + num6] = component2.gameSize;
			array3[19 + num6] = component2.gameZielgruppe;
			array3[20 + num6] = component2.maingenre;
			array3[21 + num6] = component2.subgenre;
			array3[22 + num6] = component2.gameMainTheme;
			array3[23 + num6] = component2.gameSubTheme;
			array3[24 + num6] = component2.gameLicence;
			array3[25 + num6] = component2.gameCopyProtect;
			array3[26 + num6] = component2.auftragsspiel_gehalt;
			array3[27 + num6] = component2.auftragsspiel_bonus;
			array3[28 + num6] = component2.auftragsspiel_zeitInWochen;
			array3[29 + num6] = component2.auftragsspiel_wochenAlsAngebot;
			array3[30 + num6] = component2.auftragsspiel_mindestbewertung;
			array3[31 + num6] = component2.gameAP_Gameplay;
			array3[32 + num6] = component2.gameAP_Grafik;
			array3[33 + num6] = component2.gameAP_Sound;
			array3[34 + num6] = component2.gameAP_Technik;
			array3[35 + num6] = component2.gameEngineFeature[0];
			array3[36 + num6] = component2.gameEngineFeature[1];
			array3[37 + num6] = component2.gameEngineFeature[2];
			array3[38 + num6] = component2.gameEngineFeature[3];
			array3[39 + num6] = component2.gamePlatform[0];
			array3[40 + num6] = component2.gamePlatform[1];
			array3[41 + num6] = component2.gamePlatform[2];
			array3[42 + num6] = component2.gamePlatform[3];
			array3[43 + num6] = component2.publisherID;
			array3[44 + num6] = component2.weeksOnMarket;
			array3[45 + num6] = component2.originalIP;
			array3[46 + num6] = component2.teile;
			array3[48 + num6] = component2.amountUpdates;
			array3[49 + num6] = component2.amountAddons;
			array3[50 + num6] = component2.amountMMOAddons;
			array3[51 + num6] = component2.usk;
			array3[52 + num6] = component2.multiplayerSlot;
			array3[53 + num6] = component2.verkaufspreis[0];
			array3[54 + num6] = component2.verkaufspreis[1];
			array3[55 + num6] = component2.verkaufspreis[2];
			array3[56 + num6] = component2.verkaufspreis[3];
			array3[57 + num6] = component2.releaseDate;
			array3[58 + num6] = component2.vorbestellungen;
			array3[59 + num6] = component2.lagerbestand[0];
			array3[60 + num6] = component2.lagerbestand[1];
			array3[61 + num6] = component2.lagerbestand[2];
			array3[66 + num6] = component2.freigabeBudget;
			array3[67 + num6] = component2.sellsPerWeek[0];
			array3[68 + num6] = component2.sellsPerWeek[1];
			array3[69 + num6] = component2.sellsPerWeek[2];
			array3[70 + num6] = component2.sellsPerWeek[3];
			array3[71 + num6] = component2.sellsPerWeek[4];
			array3[72 + num6] = component2.sellsPerWeek[5];
			array3[73 + num6] = component2.sellsPerWeek[6];
			array3[74 + num6] = component2.sellsPerWeek[7];
			array3[75 + num6] = component2.sellsPerWeek[8];
			array3[76 + num6] = component2.sellsPerWeek[9];
			array3[77 + num6] = component2.sellsPerWeek[10];
			array3[78 + num6] = component2.sellsPerWeek[11];
			array3[79 + num6] = component2.sellsPerWeek[12];
			array3[80 + num6] = component2.sellsPerWeek[13];
			array3[81 + num6] = component2.sellsPerWeek[14];
			array3[82 + num6] = component2.sellsPerWeek[15];
			array3[83 + num6] = component2.sellsPerWeek[16];
			array3[84 + num6] = component2.sellsPerWeek[17];
			array3[85 + num6] = component2.sellsPerWeek[18];
			array3[86 + num6] = component2.sellsPerWeek[19];
			array3[87 + num6] = component2.finanzierung_Grundkosten;
			array3[88 + num6] = component2.finanzierung_Technology;
			array3[89 + num6] = component2.finanzierung_Kontent;
			array3[90 + num6] = component2.gameAntiCheat;
			array3[91 + num6] = component2.abonnements;
			array3[92 + num6] = component2.abonnementsWoche;
			array3[93 + num6] = component2.aboPreis;
			array3[94 + num6] = component2.abosAddons;
			array3[95 + num6] = component2.lastChartPosition;
			array3[96 + num6] = component2.date_start_year;
			array3[97 + num6] = component2.date_start_month;
			array3[98 + num6] = component2.userPositiv;
			array3[99 + num6] = component2.userNegativ;
			array3[100 + num6] = component2.merchBestellungen[0];
			array3[101 + num6] = component2.merchBestellungen[1];
			array3[102 + num6] = component2.merchBestellungen[2];
			array3[103 + num6] = component2.merchBestellungen[3];
			array3[104 + num6] = component2.merchBestellungen[4];
			array3[105 + num6] = component2.merchBestellungen[5];
			array3[106 + num6] = component2.merchBestellungen[6];
			array3[107 + num6] = component2.merchBestellungen[7];
			array3[108 + num6] = component2.merchGesamtSells[0];
			array3[109 + num6] = component2.merchGesamtSells[1];
			array3[110 + num6] = component2.merchGesamtSells[2];
			array3[111 + num6] = component2.merchGesamtSells[3];
			array3[112 + num6] = component2.merchGesamtSells[4];
			array3[113 + num6] = component2.merchGesamtSells[5];
			array3[114 + num6] = component2.merchGesamtSells[6];
			array3[115 + num6] = component2.merchGesamtSells[7];
			array3[116 + num6] = component2.merchDiesenMonat[0];
			array3[117 + num6] = component2.merchDiesenMonat[1];
			array3[118 + num6] = component2.merchDiesenMonat[2];
			array3[119 + num6] = component2.merchDiesenMonat[3];
			array3[120 + num6] = component2.merchDiesenMonat[4];
			array3[121 + num6] = component2.merchDiesenMonat[5];
			array3[122 + num6] = component2.merchDiesenMonat[6];
			array3[123 + num6] = component2.merchDiesenMonat[7];
			array3[124 + num6] = component2.merchLetzterMonat[0];
			array3[125 + num6] = component2.merchLetzterMonat[1];
			array3[126 + num6] = component2.merchLetzterMonat[2];
			array3[127 + num6] = component2.merchLetzterMonat[3];
			array3[128 + num6] = component2.merchLetzterMonat[4];
			array3[129 + num6] = component2.merchLetzterMonat[5];
			array3[130 + num6] = component2.merchLetzterMonat[6];
			array3[131 + num6] = component2.merchLetzterMonat[7];
			array3[132 + num6] = component2.sonderIPMindestreview;
			array3[133 + num6] = component2.bestAbonnements;
			array3[134 + num6] = component2.ownerID;
			int num7 = i * (array4.Length / num);
			array4[num7] = component2.hype;
			array4[1 + num7] = component2.devPoints;
			array4[2 + num7] = component2.devPointsStart;
			array4[3 + num7] = component2.devPoints_Gesamt;
			array4[4 + num7] = component2.devPointsStart_Gesamt;
			array4[5 + num7] = component2.points_gameplay;
			array4[6 + num7] = component2.points_grafik;
			array4[7 + num7] = component2.points_sound;
			array4[8 + num7] = component2.points_technik;
			array4[9 + num7] = component2.points_bugs;
			array4[10 + num7] = component2.bonusSellsUpdates;
			array4[11 + num7] = component2.bonusSellsAddons;
			array4[12 + num7] = component2.bonusSellsMMOAddons;
			array4[13 + num7] = component2.addonQuality;
			array4[14 + num7] = component2.f2pInteresse;
			array4[15 + num7] = component2.mmoInteresse;
			array4[16 + num7] = component2.ipPunkte;
			array4[17 + num7] = component2.merchGesamtReviewPoints;
			array4[18 + num7] = component2.points_bugsInvis;
			array4[19 + num7] = component2.pubAngebot_Verhandlung;
			array4[20 + num7] = component2.pubAngebot_VerhandlungProzent;
			array4[21 + num7] = component2.pubAngebot_Stimmung;
			array4[22 + num7] = component2.pubAngebot_Gewinnbeteiligung;
			array4[23 + num7] = component2.merchVerkaufspreis[0];
			array4[24 + num7] = component2.merchVerkaufspreis[1];
			array4[25 + num7] = component2.merchVerkaufspreis[2];
			array4[26 + num7] = component2.merchVerkaufspreis[3];
			array4[27 + num7] = component2.merchVerkaufspreis[4];
			array4[28 + num7] = component2.merchVerkaufspreis[5];
			array4[29 + num7] = component2.merchVerkaufspreis[6];
			array4[30 + num7] = component2.merchVerkaufspreis[7];
			int num8 = i * (array6.Length / num);
			array6[num8] = component2.playerGame;
			array6[1 + num8] = component2.inDevelopment;
			array6[2 + num8] = component2.typ_standard;
			array6[3 + num8] = component2.typ_nachfolger;
			array6[4 + num8] = component2.typ_remaster;
			array6[5 + num8] = component2.typ_addon;
			array6[6 + num8] = component2.typ_bundle;
			array6[7 + num8] = component2.typ_budget;
			array6[8 + num8] = component2.engineFeature_DevDone[0];
			array6[9 + num8] = component2.engineFeature_DevDone[1];
			array6[10 + num8] = component2.engineFeature_DevDone[2];
			array6[11 + num8] = component2.engineFeature_DevDone[3];
			array6[12 + num8] = component2.exklusiv;
			array6[13 + num8] = component2.isOnMarket;
			array6[14 + num8] = component2.nachfolger_created;
			array6[15 + num8] = component2.remaster_created;
			array6[16 + num8] = component2.typ_contractGame;
			array6[17 + num8] = component2.trendsetter;
			array6[18 + num8] = component2.warBeiAwards;
			array6[19 + num8] = component2.retro;
			array6[20 + num8] = component2.spielbericht;
			array6[21 + num8] = component2.typ_addonStandalone;
			array6[22 + num8] = component2.digitalVersion;
			array6[23 + num8] = component2.retailVersion;
			array6[24 + num8] = component2.budget_created;
			array6[25 + num8] = component2.gameplayStudio[0];
			array6[26 + num8] = component2.gameplayStudio[1];
			array6[27 + num8] = component2.gameplayStudio[2];
			array6[28 + num8] = component2.gameplayStudio[3];
			array6[29 + num8] = component2.gameplayStudio[4];
			array6[30 + num8] = component2.gameplayStudio[5];
			array6[31 + num8] = component2.grafikStudio[0];
			array6[32 + num8] = component2.grafikStudio[1];
			array6[33 + num8] = component2.grafikStudio[2];
			array6[34 + num8] = component2.grafikStudio[3];
			array6[35 + num8] = component2.grafikStudio[4];
			array6[36 + num8] = component2.grafikStudio[5];
			array6[37 + num8] = component2.soundStudio[0];
			array6[38 + num8] = component2.soundStudio[1];
			array6[39 + num8] = component2.soundStudio[2];
			array6[40 + num8] = component2.soundStudio[3];
			array6[41 + num8] = component2.soundStudio[4];
			array6[42 + num8] = component2.soundStudio[5];
			array6[43 + num8] = component2.motionCaptureStudio[0];
			array6[44 + num8] = component2.motionCaptureStudio[1];
			array6[45 + num8] = component2.motionCaptureStudio[2];
			array6[46 + num8] = component2.motionCaptureStudio[3];
			array6[47 + num8] = component2.motionCaptureStudio[4];
			array6[48 + num8] = component2.motionCaptureStudio[5];
			array6[49 + num8] = component2.standard_edition[0];
			array6[50 + num8] = component2.standard_edition[1];
			array6[51 + num8] = component2.standard_edition[2];
			array6[52 + num8] = component2.standard_edition[3];
			array6[53 + num8] = component2.standard_edition[4];
			array6[54 + num8] = component2.standard_edition[5];
			array6[55 + num8] = component2.standard_edition[6];
			array6[56 + num8] = component2.standard_edition[7];
			array6[57 + num8] = component2.standard_edition[8];
			array6[58 + num8] = component2.standard_edition[9];
			array6[59 + num8] = component2.deluxe_edition[0];
			array6[60 + num8] = component2.deluxe_edition[1];
			array6[61 + num8] = component2.deluxe_edition[2];
			array6[62 + num8] = component2.deluxe_edition[3];
			array6[63 + num8] = component2.deluxe_edition[4];
			array6[64 + num8] = component2.deluxe_edition[5];
			array6[65 + num8] = component2.deluxe_edition[6];
			array6[66 + num8] = component2.deluxe_edition[7];
			array6[67 + num8] = component2.deluxe_edition[8];
			array6[68 + num8] = component2.deluxe_edition[9];
			array6[69 + num8] = component2.collectors_edition[0];
			array6[70 + num8] = component2.collectors_edition[1];
			array6[71 + num8] = component2.collectors_edition[2];
			array6[72 + num8] = component2.collectors_edition[3];
			array6[73 + num8] = component2.collectors_edition[4];
			array6[74 + num8] = component2.collectors_edition[5];
			array6[75 + num8] = component2.collectors_edition[6];
			array6[76 + num8] = component2.collectors_edition[7];
			array6[77 + num8] = component2.collectors_edition[8];
			array6[78 + num8] = component2.collectors_edition[9];
			array6[79 + num8] = component2.gameLanguage[0];
			array6[80 + num8] = component2.gameLanguage[1];
			array6[81 + num8] = component2.gameLanguage[2];
			array6[82 + num8] = component2.gameLanguage[3];
			array6[83 + num8] = component2.gameLanguage[4];
			array6[84 + num8] = component2.gameLanguage[5];
			array6[85 + num8] = component2.gameLanguage[6];
			array6[86 + num8] = component2.gameLanguage[7];
			array6[87 + num8] = component2.gameLanguage[8];
			array6[88 + num8] = component2.gameLanguage[9];
			array6[89 + num8] = component2.gameLanguage[10];
			array6[90 + num8] = component2.typ_mmoaddon;
			array6[91 + num8] = component2.spielbericht_favorit;
			array6[92 + num8] = component2.bundle_created;
			array6[93 + num8] = component2.typ_bundleAddon;
			array6[94 + num8] = component2.typ_goty;
			array6[95 + num8] = component2.goty;
			array6[96 + num8] = component2.goty_created;
			array6[97 + num8] = component2.inAppPurchase[0];
			array6[98 + num8] = component2.inAppPurchase[1];
			array6[99 + num8] = component2.inAppPurchase[2];
			array6[100 + num8] = component2.inAppPurchase[3];
			array6[101 + num8] = component2.inAppPurchase[4];
			array6[102 + num8] = component2.inAppPurchase[5];
			array6[103 + num8] = component2.mmoTOf2p_created;
			array6[104 + num8] = component2.archiv_spielkonzept;
			array6[105 + num8] = component2.archiv_spielbericht;
			array6[106 + num8] = component2.archiv_fanbriefe;
			array6[107 + num8] = component2.handy;
			array6[108 + num8] = component2.arcade;
			array6[109 + num8] = component2.typ_spinoff;
			array6[110 + num8] = component2.merchKeinVerkauf;
			array6[111 + num8] = component2.portExist[0];
			array6[112 + num8] = component2.portExist[1];
			array6[113 + num8] = component2.portExist[2];
			array6[114 + num8] = component2.herstellerExklusiv;
			array6[115 + num8] = component2.schublade;
			array6[116 + num8] = component2.autoPreis;
			array6[117 + num8] = component2.pubOffer;
			array6[120 + num8] = component2.commercialFlop;
			array6[121 + num8] = component2.commercialHit;
			array6[122 + num8] = component2.newGenreCombination;
			array6[123 + num8] = component2.newTopicCombination;
			array6[124 + num8] = component2.npcLateinNumbers;
			array6[125 + num8] = component2.pubAngebot;
			array6[126 + num8] = component2.pubAngebot_Retail;
			array6[127 + num8] = component2.pubAngebot_Digital;
			array6[128 + num8] = component2.pubAnbgebot_Inivs;
			array6[129 + num8] = component2.pubAngebot_AngebotWoche;
			array6[130 + num8] = component2.auftragsspiel;
			array6[131 + num8] = component2.auftragsspiel_zeitAbgelaufen;
			array6[132 + num8] = component2.auftragsspiel_Inivs;
			array6[133 + num8] = component2.sonderIP;
			array6[134 + num8] = component2.f2pConverted;
			int num9 = i * (array2.Length / num);
			array2[num9] = component2.umsatzTotal;
			array2[1 + num9] = component2.costs_entwicklung;
			array2[2 + num9] = component2.costs_mitarbeiter;
			array2[3 + num9] = component2.costs_marketing;
			array2[4 + num9] = component2.costs_enginegebuehren;
			array2[5 + num9] = component2.costs_server;
			array2[6 + num9] = component2.costs_production;
			array2[7 + num9] = component2.umsatzAbos;
			array2[8 + num9] = component2.umsatzInApp;
			array2[9 + num9] = component2.exklusivKonsolenSells;
			array2[10 + num9] = (long)component2.bundleID[0];
			array2[11 + num9] = (long)component2.bundleID[1];
			array2[12 + num9] = (long)component2.bundleID[2];
			array2[13 + num9] = (long)component2.bundleID[3];
			array2[14 + num9] = (long)component2.bundleID[4];
			array2[15 + num9] = (long)component2.inAppPurchaseWeek;
			array2[16 + num9] = (long)component2.originalGameID;
			array2[17 + num9] = component2.costs_updates;
			array2[18 + num9] = (long)component2.specialMarketing[0];
			array2[19 + num9] = (long)component2.specialMarketing[1];
			array2[20 + num9] = (long)component2.specialMarketing[2];
			array2[21 + num9] = (long)component2.specialMarketing[3];
			array2[22 + num9] = (long)component2.specialMarketing[4];
			array2[23 + num9] = (long)component2.Designschwerpunkt[0];
			array2[24 + num9] = (long)component2.Designschwerpunkt[1];
			array2[25 + num9] = (long)component2.Designschwerpunkt[2];
			array2[26 + num9] = (long)component2.Designschwerpunkt[3];
			array2[27 + num9] = (long)component2.Designschwerpunkt[4];
			array2[28 + num9] = (long)component2.Designschwerpunkt[5];
			array2[29 + num9] = (long)component2.Designschwerpunkt[6];
			array2[30 + num9] = (long)component2.Designschwerpunkt[7];
			array2[31 + num9] = (long)component2.Designausrichtung[0];
			array2[32 + num9] = (long)component2.Designausrichtung[1];
			array2[33 + num9] = (long)component2.Designausrichtung[2];
			array2[34 + num9] = (long)component2.arcadeCase;
			array2[35 + num9] = (long)component2.arcadeMonitor;
			array2[36 + num9] = (long)component2.arcadeJoystick;
			array2[37 + num9] = (long)component2.arcadeSound;
			array2[38 + num9] = (long)component2.arcadeProdCosts;
			array2[39 + num9] = (long)component2.portID;
			array2[40 + num9] = (long)component2.mainIP;
			array2[41 + num9] = (long)component2.ipTime;
			array2[42 + num9] = (long)component2.bestChartPosition;
			array2[43 + num9] = (long)component2.stornierungen;
			array2[44 + num9] = (long)component2.schubladeTaskID;
			array2[45 + num9] = component2.merchGesamtGewinn;
			array2[46 + num9] = (long)component2.weeksInDevelopment;
			array2[47 + num9] = (long)component2.pubAngebot_Weeks;
			array2[48 + num9] = (long)component2.pubAngebot_Garantiesumme;
			array2[49 + num9] = component2.sellsTotal;
			array2[50 + num9] = component2.sellsTotalStandard;
			array2[51 + num9] = component2.sellsTotalDeluxe;
			array2[52 + num9] = component2.sellsTotalCollectors;
			array2[53 + num9] = component2.sellsTotalOnline;
			array2[54 + num9] = component2.tw_gewinnanteil;
			int num10 = i * num2;
			for (int j = 0; j < component2.gameGameplayFeatures.Length; j++)
			{
				array7[j + num10] = component2.gameGameplayFeatures[j];
			}
			num10 = i * num3;
			for (int k = 0; k < component2.gameplayFeatures_DevDone.Length; k++)
			{
				array8[k + num10] = component2.gameplayFeatures_DevDone[k];
			}
			num10 = i * num4;
			for (int l = 0; l < component2.fanbrief.Length; l++)
			{
				array9[l + num10] = component2.fanbrief[l];
			}
		}
		writer.Write<int[]>("games_I", array3);
		writer.Write<float[]>("games_F", array4);
		writer.Write<long[]>("games_L", array2);
		writer.Write<string[]>("games_S", array5);
		writer.Write<bool[]>("games_B", array6);
		writer.Write<bool[]>("games_gameplayFeatures", array7);
		writer.Write<bool[]>("games_gameplayFeaturesDevDone", array8);
		writer.Write<bool[]>("games_fanbrief", array9);
	}

	
	private void LoadGames(ES3Reader reader, string filename)
	{
		int num = reader.Read<int>("anzGames", -1);
		Debug.Log("Load: (anzGames) " + num.ToString());
		if (num <= 0)
		{
			return;
		}
		int[] array = reader.Read<int[]>("games_I");
		float[] array2 = reader.Read<float[]>("games_F");
		long[] array3 = reader.Read<long[]>("games_L");
		string[] array4 = reader.Read<string[]>("games_S");
		bool[] array5 = reader.Read<bool[]>("games_B");
		bool[] array6 = reader.Read<bool[]>("games_gameplayFeatures");
		bool[] array7 = reader.Read<bool[]>("games_gameplayFeaturesDevDone");
		bool[] array8 = reader.Read<bool[]>("games_fanbrief");
		int num2 = array.Length / num;
		int num3 = array2.Length / num;
		int num4 = array5.Length / num;
		int num5 = array4.Length / num;
		int num6 = array3.Length / num;
		for (int i = 0; i < num; i++)
		{
			int num7 = i * (array.Length / num);
			int num8 = i * (array2.Length / num);
			int num9 = i * (array5.Length / num);
			int num10 = i * (array4.Length / num);
			int num11 = i * (array3.Length / num);
			if (array4[num10].Length > 0)
			{
				gameScript gameScript = this.games_.CreateNewGame(true, false);
				array4[num10] = array4[num10].Replace(" <color=green>" + this.tS_.GetText(1549) + "</color>", string.Empty);
				array4[num10] = array4[num10].Replace(" <color=green>" + this.tS_.GetText(1896) + "</color>", string.Empty);
				gameScript.SetMyName(array4[num10]);
				gameScript.beschreibung = array4[1 + num10];
				gameScript.ipName = array4[2 + num10];
				gameScript.myID = array[num7];
				gameScript.developerID = array[1 + num7];
				gameScript.engineID = array[2 + num7];
				gameScript.reviewGameplay = array[3 + num7];
				gameScript.reviewGrafik = array[4 + num7];
				gameScript.reviewSound = array[5 + num7];
				gameScript.reviewSteuerung = array[6 + num7];
				gameScript.reviewTotal = array[7 + num7];
				gameScript.reviewGameplayText = array[8 + num7];
				gameScript.reviewGrafikText = array[9 + num7];
				gameScript.reviewSoundText = array[10 + num7];
				gameScript.reviewSteuerungText = array[11 + num7];
				gameScript.reviewTotalText = array[12 + num7];
				gameScript.date_year = array[13 + num7];
				gameScript.date_month = array[14 + num7];
				gameScript.sellsTotal = (long)array[15 + num7];
				gameScript.devAktFeature = array[16 + num7];
				gameScript.gameTyp = array[17 + num7];
				gameScript.gameSize = array[18 + num7];
				gameScript.gameZielgruppe = array[19 + num7];
				gameScript.maingenre = array[20 + num7];
				gameScript.subgenre = array[21 + num7];
				gameScript.gameMainTheme = array[22 + num7];
				gameScript.gameSubTheme = array[23 + num7];
				gameScript.gameLicence = array[24 + num7];
				gameScript.gameCopyProtect = array[25 + num7];
				gameScript.auftragsspiel_gehalt = array[26 + num7];
				gameScript.auftragsspiel_bonus = array[27 + num7];
				gameScript.auftragsspiel_zeitInWochen = array[28 + num7];
				gameScript.auftragsspiel_wochenAlsAngebot = array[29 + num7];
				gameScript.auftragsspiel_mindestbewertung = array[30 + num7];
				gameScript.gameAP_Gameplay = array[31 + num7];
				gameScript.gameAP_Grafik = array[32 + num7];
				gameScript.gameAP_Sound = array[33 + num7];
				gameScript.gameAP_Technik = array[34 + num7];
				gameScript.gameEngineFeature[0] = array[35 + num7];
				gameScript.gameEngineFeature[1] = array[36 + num7];
				gameScript.gameEngineFeature[2] = array[37 + num7];
				gameScript.gameEngineFeature[3] = array[38 + num7];
				gameScript.gamePlatform[0] = array[39 + num7];
				gameScript.gamePlatform[1] = array[40 + num7];
				gameScript.gamePlatform[2] = array[41 + num7];
				gameScript.gamePlatform[3] = array[42 + num7];
				gameScript.publisherID = array[43 + num7];
				gameScript.weeksOnMarket = array[44 + num7];
				gameScript.originalIP = array[45 + num7];
				gameScript.teile = array[46 + num7];
				gameScript.amountUpdates = array[48 + num7];
				gameScript.amountAddons = array[49 + num7];
				gameScript.amountMMOAddons = array[50 + num7];
				gameScript.usk = array[51 + num7];
				gameScript.multiplayerSlot = array[52 + num7];
				gameScript.verkaufspreis[0] = array[53 + num7];
				gameScript.verkaufspreis[1] = array[54 + num7];
				gameScript.verkaufspreis[2] = array[55 + num7];
				gameScript.verkaufspreis[3] = array[56 + num7];
				gameScript.releaseDate = array[57 + num7];
				gameScript.vorbestellungen = array[58 + num7];
				gameScript.lagerbestand[0] = array[59 + num7];
				gameScript.lagerbestand[1] = array[60 + num7];
				gameScript.lagerbestand[2] = array[61 + num7];
				gameScript.sellsTotalStandard = (long)array[62 + num7];
				gameScript.sellsTotalDeluxe = (long)array[63 + num7];
				gameScript.sellsTotalCollectors = (long)array[64 + num7];
				gameScript.sellsTotalOnline = (long)array[65 + num7];
				gameScript.freigabeBudget = array[66 + num7];
				gameScript.sellsPerWeek[0] = array[67 + num7];
				gameScript.sellsPerWeek[1] = array[68 + num7];
				gameScript.sellsPerWeek[2] = array[69 + num7];
				gameScript.sellsPerWeek[3] = array[70 + num7];
				gameScript.sellsPerWeek[4] = array[71 + num7];
				gameScript.sellsPerWeek[5] = array[72 + num7];
				gameScript.sellsPerWeek[6] = array[73 + num7];
				gameScript.sellsPerWeek[7] = array[74 + num7];
				gameScript.sellsPerWeek[8] = array[75 + num7];
				gameScript.sellsPerWeek[9] = array[76 + num7];
				gameScript.sellsPerWeek[10] = array[77 + num7];
				gameScript.sellsPerWeek[11] = array[78 + num7];
				gameScript.sellsPerWeek[12] = array[79 + num7];
				gameScript.sellsPerWeek[13] = array[80 + num7];
				gameScript.sellsPerWeek[14] = array[81 + num7];
				gameScript.sellsPerWeek[15] = array[82 + num7];
				gameScript.sellsPerWeek[16] = array[83 + num7];
				gameScript.sellsPerWeek[17] = array[84 + num7];
				gameScript.sellsPerWeek[18] = array[85 + num7];
				gameScript.sellsPerWeek[19] = array[86 + num7];
				gameScript.finanzierung_Grundkosten = array[87 + num7];
				gameScript.finanzierung_Technology = array[88 + num7];
				gameScript.finanzierung_Kontent = array[89 + num7];
				gameScript.gameAntiCheat = array[90 + num7];
				gameScript.abonnements = array[91 + num7];
				gameScript.abonnementsWoche = array[92 + num7];
				gameScript.aboPreis = array[93 + num7];
				gameScript.abosAddons = array[94 + num7];
				gameScript.lastChartPosition = array[95 + num7];
				gameScript.date_start_year = array[96 + num7];
				gameScript.date_start_month = array[97 + num7];
				gameScript.userPositiv = array[98 + num7];
				gameScript.userNegativ = array[99 + num7];
				if (num2 > 100)
				{
					gameScript.merchBestellungen[0] = array[100 + num7];
				}
				if (num2 > 101)
				{
					gameScript.merchBestellungen[1] = array[101 + num7];
				}
				if (num2 > 102)
				{
					gameScript.merchBestellungen[2] = array[102 + num7];
				}
				if (num2 > 103)
				{
					gameScript.merchBestellungen[3] = array[103 + num7];
				}
				if (num2 > 104)
				{
					gameScript.merchBestellungen[4] = array[104 + num7];
				}
				if (num2 > 105)
				{
					gameScript.merchBestellungen[5] = array[105 + num7];
				}
				if (num2 > 106)
				{
					gameScript.merchBestellungen[6] = array[106 + num7];
				}
				if (num2 > 107)
				{
					gameScript.merchBestellungen[7] = array[107 + num7];
				}
				if (num2 > 108)
				{
					gameScript.merchGesamtSells[0] = array[108 + num7];
				}
				if (num2 > 109)
				{
					gameScript.merchGesamtSells[1] = array[109 + num7];
				}
				if (num2 > 110)
				{
					gameScript.merchGesamtSells[2] = array[110 + num7];
				}
				if (num2 > 111)
				{
					gameScript.merchGesamtSells[3] = array[111 + num7];
				}
				if (num2 > 112)
				{
					gameScript.merchGesamtSells[4] = array[112 + num7];
				}
				if (num2 > 113)
				{
					gameScript.merchGesamtSells[5] = array[113 + num7];
				}
				if (num2 > 114)
				{
					gameScript.merchGesamtSells[6] = array[114 + num7];
				}
				if (num2 > 115)
				{
					gameScript.merchGesamtSells[7] = array[115 + num7];
				}
				if (num2 > 116)
				{
					gameScript.merchDiesenMonat[0] = array[116 + num7];
				}
				if (num2 > 117)
				{
					gameScript.merchDiesenMonat[1] = array[117 + num7];
				}
				if (num2 > 118)
				{
					gameScript.merchDiesenMonat[2] = array[118 + num7];
				}
				if (num2 > 119)
				{
					gameScript.merchDiesenMonat[3] = array[119 + num7];
				}
				if (num2 > 120)
				{
					gameScript.merchDiesenMonat[4] = array[120 + num7];
				}
				if (num2 > 121)
				{
					gameScript.merchDiesenMonat[5] = array[121 + num7];
				}
				if (num2 > 122)
				{
					gameScript.merchDiesenMonat[6] = array[122 + num7];
				}
				if (num2 > 123)
				{
					gameScript.merchDiesenMonat[7] = array[123 + num7];
				}
				if (num2 > 124)
				{
					gameScript.merchLetzterMonat[0] = array[124 + num7];
				}
				if (num2 > 125)
				{
					gameScript.merchLetzterMonat[1] = array[125 + num7];
				}
				if (num2 > 126)
				{
					gameScript.merchLetzterMonat[2] = array[126 + num7];
				}
				if (num2 > 127)
				{
					gameScript.merchLetzterMonat[3] = array[127 + num7];
				}
				if (num2 > 128)
				{
					gameScript.merchLetzterMonat[4] = array[128 + num7];
				}
				if (num2 > 129)
				{
					gameScript.merchLetzterMonat[5] = array[129 + num7];
				}
				if (num2 > 130)
				{
					gameScript.merchLetzterMonat[6] = array[130 + num7];
				}
				if (num2 > 131)
				{
					gameScript.merchLetzterMonat[7] = array[131 + num7];
				}
				if (num2 > 132)
				{
					gameScript.sonderIPMindestreview = array[132 + num7];
				}
				if (num2 > 133)
				{
					gameScript.bestAbonnements = array[133 + num7];
				}
				if (num2 > 134)
				{
					gameScript.ownerID = array[134 + num7];
				}
				gameScript.hype = array2[num8];
				gameScript.devPoints = array2[1 + num8];
				gameScript.devPointsStart = array2[2 + num8];
				gameScript.devPoints_Gesamt = array2[3 + num8];
				gameScript.devPointsStart_Gesamt = array2[4 + num8];
				gameScript.points_gameplay = array2[5 + num8];
				gameScript.points_grafik = array2[6 + num8];
				gameScript.points_sound = array2[7 + num8];
				gameScript.points_technik = array2[8 + num8];
				gameScript.points_bugs = array2[9 + num8];
				gameScript.bonusSellsUpdates = array2[10 + num8];
				gameScript.bonusSellsAddons = array2[11 + num8];
				gameScript.bonusSellsMMOAddons = array2[12 + num8];
				gameScript.addonQuality = array2[13 + num8];
				gameScript.f2pInteresse = array2[14 + num8];
				gameScript.mmoInteresse = array2[15 + num8];
				gameScript.ipPunkte = array2[16 + num8];
				gameScript.merchGesamtReviewPoints = array2[17 + num8];
				gameScript.points_bugsInvis = array2[18 + num8];
				gameScript.pubAngebot_Verhandlung = array2[19 + num8];
				gameScript.pubAngebot_VerhandlungProzent = array2[20 + num8];
				gameScript.pubAngebot_Stimmung = array2[21 + num8];
				gameScript.pubAngebot_Gewinnbeteiligung = array2[22 + num8];
				if (num3 > 23)
				{
					gameScript.merchVerkaufspreis[0] = array2[23 + num8];
				}
				if (num3 > 24)
				{
					gameScript.merchVerkaufspreis[1] = array2[24 + num8];
				}
				if (num3 > 25)
				{
					gameScript.merchVerkaufspreis[2] = array2[25 + num8];
				}
				if (num3 > 26)
				{
					gameScript.merchVerkaufspreis[3] = array2[26 + num8];
				}
				if (num3 > 27)
				{
					gameScript.merchVerkaufspreis[4] = array2[27 + num8];
				}
				if (num3 > 28)
				{
					gameScript.merchVerkaufspreis[5] = array2[28 + num8];
				}
				if (num3 > 29)
				{
					gameScript.merchVerkaufspreis[6] = array2[29 + num8];
				}
				if (num3 > 30)
				{
					gameScript.merchVerkaufspreis[7] = array2[30 + num8];
				}
				gameScript.playerGame = array5[num9];
				gameScript.inDevelopment = array5[1 + num9];
				gameScript.typ_standard = array5[2 + num9];
				gameScript.typ_nachfolger = array5[3 + num9];
				gameScript.typ_remaster = array5[4 + num9];
				gameScript.typ_addon = array5[5 + num9];
				gameScript.typ_bundle = array5[6 + num9];
				gameScript.typ_budget = array5[7 + num9];
				gameScript.engineFeature_DevDone[0] = array5[8 + num9];
				gameScript.engineFeature_DevDone[1] = array5[9 + num9];
				gameScript.engineFeature_DevDone[2] = array5[10 + num9];
				gameScript.engineFeature_DevDone[3] = array5[11 + num9];
				gameScript.exklusiv = array5[12 + num9];
				gameScript.isOnMarket = array5[13 + num9];
				gameScript.nachfolger_created = array5[14 + num9];
				gameScript.remaster_created = array5[15 + num9];
				gameScript.typ_contractGame = array5[16 + num9];
				gameScript.trendsetter = array5[17 + num9];
				gameScript.warBeiAwards = array5[18 + num9];
				gameScript.retro = array5[19 + num9];
				gameScript.spielbericht = array5[20 + num9];
				gameScript.typ_addonStandalone = array5[21 + num9];
				gameScript.digitalVersion = array5[22 + num9];
				gameScript.retailVersion = array5[23 + num9];
				gameScript.budget_created = array5[24 + num9];
				gameScript.gameplayStudio[0] = array5[25 + num9];
				gameScript.gameplayStudio[1] = array5[26 + num9];
				gameScript.gameplayStudio[2] = array5[27 + num9];
				gameScript.gameplayStudio[3] = array5[28 + num9];
				gameScript.gameplayStudio[4] = array5[29 + num9];
				gameScript.gameplayStudio[5] = array5[30 + num9];
				gameScript.grafikStudio[0] = array5[31 + num9];
				gameScript.grafikStudio[1] = array5[32 + num9];
				gameScript.grafikStudio[2] = array5[33 + num9];
				gameScript.grafikStudio[3] = array5[34 + num9];
				gameScript.grafikStudio[4] = array5[35 + num9];
				gameScript.grafikStudio[5] = array5[36 + num9];
				gameScript.soundStudio[0] = array5[37 + num9];
				gameScript.soundStudio[1] = array5[38 + num9];
				gameScript.soundStudio[2] = array5[39 + num9];
				gameScript.soundStudio[3] = array5[40 + num9];
				gameScript.soundStudio[4] = array5[41 + num9];
				gameScript.soundStudio[5] = array5[42 + num9];
				gameScript.motionCaptureStudio[0] = array5[43 + num9];
				gameScript.motionCaptureStudio[1] = array5[44 + num9];
				gameScript.motionCaptureStudio[2] = array5[45 + num9];
				gameScript.motionCaptureStudio[3] = array5[46 + num9];
				gameScript.motionCaptureStudio[4] = array5[47 + num9];
				gameScript.motionCaptureStudio[5] = array5[48 + num9];
				gameScript.standard_edition[0] = array5[49 + num9];
				gameScript.standard_edition[1] = array5[50 + num9];
				gameScript.standard_edition[2] = array5[51 + num9];
				gameScript.standard_edition[3] = array5[52 + num9];
				gameScript.standard_edition[4] = array5[53 + num9];
				gameScript.standard_edition[5] = array5[54 + num9];
				gameScript.standard_edition[6] = array5[55 + num9];
				gameScript.standard_edition[7] = array5[56 + num9];
				gameScript.standard_edition[8] = array5[57 + num9];
				gameScript.standard_edition[9] = array5[58 + num9];
				gameScript.deluxe_edition[0] = array5[59 + num9];
				gameScript.deluxe_edition[1] = array5[60 + num9];
				gameScript.deluxe_edition[2] = array5[61 + num9];
				gameScript.deluxe_edition[3] = array5[62 + num9];
				gameScript.deluxe_edition[4] = array5[63 + num9];
				gameScript.deluxe_edition[5] = array5[64 + num9];
				gameScript.deluxe_edition[6] = array5[65 + num9];
				gameScript.deluxe_edition[7] = array5[66 + num9];
				gameScript.deluxe_edition[8] = array5[67 + num9];
				gameScript.deluxe_edition[9] = array5[68 + num9];
				gameScript.collectors_edition[0] = array5[69 + num9];
				gameScript.collectors_edition[1] = array5[70 + num9];
				gameScript.collectors_edition[2] = array5[71 + num9];
				gameScript.collectors_edition[3] = array5[72 + num9];
				gameScript.collectors_edition[4] = array5[73 + num9];
				gameScript.collectors_edition[5] = array5[74 + num9];
				gameScript.collectors_edition[6] = array5[75 + num9];
				gameScript.collectors_edition[7] = array5[76 + num9];
				gameScript.collectors_edition[8] = array5[77 + num9];
				gameScript.collectors_edition[9] = array5[78 + num9];
				gameScript.gameLanguage[0] = array5[79 + num9];
				gameScript.gameLanguage[1] = array5[80 + num9];
				gameScript.gameLanguage[2] = array5[81 + num9];
				gameScript.gameLanguage[3] = array5[82 + num9];
				gameScript.gameLanguage[4] = array5[83 + num9];
				gameScript.gameLanguage[5] = array5[84 + num9];
				gameScript.gameLanguage[6] = array5[85 + num9];
				gameScript.gameLanguage[7] = array5[86 + num9];
				gameScript.gameLanguage[8] = array5[87 + num9];
				gameScript.gameLanguage[9] = array5[88 + num9];
				gameScript.gameLanguage[10] = array5[89 + num9];
				gameScript.typ_mmoaddon = array5[90 + num9];
				gameScript.spielbericht_favorit = array5[91 + num9];
				gameScript.bundle_created = array5[92 + num9];
				gameScript.typ_bundleAddon = array5[93 + num9];
				gameScript.typ_goty = array5[94 + num9];
				gameScript.goty = array5[95 + num9];
				gameScript.goty_created = array5[96 + num9];
				gameScript.inAppPurchase[0] = array5[97 + num9];
				gameScript.inAppPurchase[1] = array5[98 + num9];
				gameScript.inAppPurchase[2] = array5[99 + num9];
				gameScript.inAppPurchase[3] = array5[100 + num9];
				gameScript.inAppPurchase[4] = array5[101 + num9];
				gameScript.inAppPurchase[5] = array5[102 + num9];
				gameScript.mmoTOf2p_created = array5[103 + num9];
				gameScript.archiv_spielkonzept = array5[104 + num9];
				gameScript.archiv_spielbericht = array5[105 + num9];
				gameScript.archiv_fanbriefe = array5[106 + num9];
				gameScript.handy = array5[107 + num9];
				gameScript.arcade = array5[108 + num9];
				gameScript.typ_spinoff = array5[109 + num9];
				gameScript.merchKeinVerkauf = array5[110 + num9];
				gameScript.portExist[0] = array5[111 + num9];
				gameScript.portExist[1] = array5[112 + num9];
				gameScript.portExist[2] = array5[113 + num9];
				gameScript.herstellerExklusiv = array5[114 + num9];
				gameScript.schublade = array5[115 + num9];
				gameScript.autoPreis = array5[116 + num9];
				gameScript.pubOffer = array5[117 + num9];
				gameScript.commercialFlop = array5[120 + num9];
				gameScript.commercialHit = array5[121 + num9];
				gameScript.newGenreCombination = array5[122 + num9];
				gameScript.newTopicCombination = array5[123 + num9];
				gameScript.npcLateinNumbers = array5[124 + num9];
				gameScript.pubAngebot = array5[125 + num9];
				gameScript.pubAngebot_Retail = array5[126 + num9];
				gameScript.pubAngebot_Digital = array5[127 + num9];
				gameScript.pubAnbgebot_Inivs = array5[128 + num9];
				gameScript.pubAngebot_AngebotWoche = array5[129 + num9];
				gameScript.auftragsspiel = array5[130 + num9];
				gameScript.auftragsspiel_zeitAbgelaufen = array5[131 + num9];
				gameScript.auftragsspiel_Inivs = array5[132 + num9];
				if (num4 > 133)
				{
					gameScript.sonderIP = array5[133 + num9];
				}
				if (num4 > 134)
				{
					gameScript.f2pConverted = array5[134 + num9];
				}
				gameScript.umsatzTotal = array3[num11];
				gameScript.costs_entwicklung = array3[1 + num11];
				gameScript.costs_mitarbeiter = array3[2 + num11];
				gameScript.costs_marketing = array3[3 + num11];
				gameScript.costs_enginegebuehren = array3[4 + num11];
				gameScript.costs_server = array3[5 + num11];
				gameScript.costs_production = array3[6 + num11];
				gameScript.umsatzAbos = array3[7 + num11];
				gameScript.umsatzInApp = array3[8 + num11];
				gameScript.exklusivKonsolenSells = array3[9 + num11];
				gameScript.bundleID[0] = (int)array3[10 + num11];
				gameScript.bundleID[1] = (int)array3[11 + num11];
				gameScript.bundleID[2] = (int)array3[12 + num11];
				gameScript.bundleID[3] = (int)array3[13 + num11];
				gameScript.bundleID[4] = (int)array3[14 + num11];
				gameScript.inAppPurchaseWeek = (int)array3[15 + num11];
				gameScript.originalGameID = (int)array3[16 + num11];
				gameScript.costs_updates = array3[17 + num11];
				gameScript.specialMarketing[0] = (int)array3[18 + num11];
				gameScript.specialMarketing[1] = (int)array3[19 + num11];
				gameScript.specialMarketing[2] = (int)array3[20 + num11];
				gameScript.specialMarketing[3] = (int)array3[21 + num11];
				gameScript.specialMarketing[4] = (int)array3[22 + num11];
				gameScript.Designschwerpunkt[0] = (int)array3[23 + num11];
				gameScript.Designschwerpunkt[1] = (int)array3[24 + num11];
				gameScript.Designschwerpunkt[2] = (int)array3[25 + num11];
				gameScript.Designschwerpunkt[3] = (int)array3[26 + num11];
				gameScript.Designschwerpunkt[4] = (int)array3[27 + num11];
				gameScript.Designschwerpunkt[5] = (int)array3[28 + num11];
				gameScript.Designschwerpunkt[6] = (int)array3[29 + num11];
				gameScript.Designschwerpunkt[7] = (int)array3[30 + num11];
				gameScript.Designausrichtung[0] = (int)array3[31 + num11];
				gameScript.Designausrichtung[1] = (int)array3[32 + num11];
				gameScript.Designausrichtung[2] = (int)array3[33 + num11];
				gameScript.arcadeCase = (int)array3[34 + num11];
				gameScript.arcadeMonitor = (int)array3[35 + num11];
				gameScript.arcadeJoystick = (int)array3[36 + num11];
				gameScript.arcadeSound = (int)array3[37 + num11];
				gameScript.arcadeProdCosts = (int)array3[38 + num11];
				gameScript.portID = (int)array3[39 + num11];
				if (gameScript.portID == 0)
				{
					gameScript.portID = -1;
				}
				gameScript.mainIP = (int)array3[40 + num11];
				gameScript.ipTime = (int)array3[41 + num11];
				gameScript.bestChartPosition = (int)array3[42 + num11];
				gameScript.stornierungen = (int)array3[43 + num11];
				gameScript.schubladeTaskID = (int)array3[44 + num11];
				gameScript.merchGesamtGewinn = (long)((int)array3[45 + num11]);
				gameScript.weeksInDevelopment = (int)array3[46 + num11];
				gameScript.pubAngebot_Weeks = (int)array3[47 + num11];
				gameScript.pubAngebot_Garantiesumme = (int)array3[48 + num11];
				if (num6 > 50)
				{
					gameScript.sellsTotal = array3[49 + num11];
				}
				if (num6 > 50)
				{
					gameScript.sellsTotalStandard = array3[50 + num11];
				}
				if (num6 > 50)
				{
					gameScript.sellsTotalDeluxe = array3[51 + num11];
				}
				if (num6 > 50)
				{
					gameScript.sellsTotalCollectors = array3[52 + num11];
				}
				if (num6 > 50)
				{
					gameScript.sellsTotalOnline = array3[53 + num11];
				}
				if (num6 > 54)
				{
					gameScript.tw_gewinnanteil = array3[54 + num11];
				}
				if (gameScript.mainIP == 0)
				{
					if (gameScript.typ_bundle || gameScript.typ_budget || gameScript.typ_bundleAddon || gameScript.typ_goty || gameScript.typ_contractGame || gameScript.typ_addon || gameScript.typ_addonStandalone || gameScript.typ_mmoaddon || gameScript.typ_remaster || gameScript.typ_spinoff || gameScript.typ_nachfolger)
					{
						gameScript.mainIP = -1;
					}
					else
					{
						gameScript.mainIP = gameScript.myID;
					}
				}
				if (!gameScript.playerGame && gameScript.ownerID == -1)
				{
					gameScript.ownerID = gameScript.developerID;
				}
				if (gameScript.Designausrichtung[0] == 0 && gameScript.Designausrichtung[1] == 0 && gameScript.Designausrichtung[2] == 0)
				{
					for (int j = 0; j < gameScript.Designschwerpunkt.Length; j++)
					{
						gameScript.Designschwerpunkt[j] = 5;
					}
					for (int k = 0; k < gameScript.Designausrichtung.Length; k++)
					{
						gameScript.Designausrichtung[k] = 5;
					}
				}
				if (this.mS_.savegameVersion == 0)
				{
					gameScript.gameGameplayFeatures = reader.Read<bool[]>("GameA2_" + gameScript.myID.ToString());
					if (gameScript.playerGame)
					{
						gameScript.gameplayFeatures_DevDone = reader.Read<bool[]>("GameA3_" + gameScript.myID.ToString());
					}
					else
					{
						gameScript.gameplayFeatures_DevDone = (bool[])gameScript.gameGameplayFeatures.Clone();
					}
					if (gameScript.playerGame)
					{
						gameScript.fanbrief = reader.Read<bool[]>("GameA9_" + gameScript.myID.ToString());
					}
				}
				else
				{
					int num12 = i * (array6.Length / num);
					for (int l = 0; l < gameScript.gameGameplayFeatures.Length; l++)
					{
						gameScript.gameGameplayFeatures[l] = array6[l + num12];
					}
					num12 = i * (array7.Length / num);
					for (int m = 0; m < gameScript.gameplayFeatures_DevDone.Length; m++)
					{
						gameScript.gameplayFeatures_DevDone[m] = array7[m + num12];
					}
					num12 = i * (array8.Length / num);
					for (int n = 0; n < gameScript.fanbrief.Length; n++)
					{
						gameScript.fanbrief[n] = array8[n + num12];
					}
				}
				gameScript.SetGameObjectName();
				gameScript.InitUI();
			}
		}
		this.games_.FindGames();
	}

	
	private void SetRoomScripts(int id)
	{
		GameObject gameObject = GameObject.Find("Room_" + id.ToString());
		if (gameObject)
		{
			roomScript component = gameObject.GetComponent<roomScript>();
			if (component)
			{
				for (int i = 0; i < mapScript.mapSizeX; i++)
				{
					for (int j = 0; j < mapScript.mapSizeY; j++)
					{
						if (this.mapS_.mapRoomID[i, j] == component.myID)
						{
							this.mapS_.mapRoomScript[i, j] = component;
						}
					}
				}
			}
		}
	}

	
	private void KeysAbfragen(string filename)
	{
		this.key_NpcIPs = false;
		this.key_inventarFilter = false;
		this.key_fanshopverlauf = false;
		this.key_achivements = false;
		this.key_npcGameNameInUse = false;
		this.key_default_verkaufpreis = false;
		this.key_default_verkaufpreisBundle = false;
		this.key_legends = false;
		this.key_EN = false;
		this.key_GE = false;
		this.key_TU = false;
		this.key_CH = false;
		this.key_FR = false;
		this.key_PB = false;
		this.key_HU = false;
		this.key_CT = false;
		this.key_PL = false;
		this.key_CZ = false;
		this.key_KO = false;
		this.key_AR = false;
		this.key_RU = false;
		this.key_IT = false;
		this.key_JA = false;
		if (ES3.KeyExists("npcIPs", filename))
		{
			this.key_NpcIPs = true;
		}
		if (ES3.KeyExists("inventarFilter", filename))
		{
			this.key_inventarFilter = true;
		}
		if (ES3.KeyExists("fanshopverlauf", filename))
		{
			this.key_fanshopverlauf = true;
		}
		if (ES3.KeyExists("gameTabFilter", filename))
		{
			this.key_gameTabFilter = true;
		}
		if (ES3.KeyExists("devLegendsDesigner", filename))
		{
			this.key_legends = true;
		}
		if (ES3.KeyExists("engineFeatures_NAME_EN", filename))
		{
			this.key_EN = true;
		}
		if (ES3.KeyExists("engineFeatures_NAME_GE", filename))
		{
			this.key_GE = true;
		}
		if (ES3.KeyExists("engineFeatures_NAME_TU", filename))
		{
			this.key_TU = true;
		}
		if (ES3.KeyExists("engineFeatures_NAME_CH", filename))
		{
			this.key_CH = true;
		}
		if (ES3.KeyExists("engineFeatures_NAME_FR", filename))
		{
			this.key_FR = true;
		}
		if (ES3.KeyExists("gameplayFeatures_NAME_PB", filename))
		{
			this.key_PB = true;
		}
		if (ES3.KeyExists("gameplayFeatures_NAME_HU", filename))
		{
			this.key_HU = true;
		}
		if (ES3.KeyExists("genres_NAME_CT", filename))
		{
			this.key_CT = true;
		}
		if (ES3.KeyExists("engineFeatures_NAME_ES", filename))
		{
			this.key_ES = true;
		}
		if (ES3.KeyExists("hardware_NAME_PL", filename))
		{
			this.key_PL = true;
		}
		if (ES3.KeyExists("engineFeatures_NAME_CZ", filename))
		{
			this.key_CZ = true;
		}
		if (ES3.KeyExists("genres_NAME_KO", filename))
		{
			this.key_KO = true;
		}
		if (ES3.KeyExists("gameplayFeatures_NAME_AR", filename))
		{
			this.key_AR = true;
		}
		if (ES3.KeyExists("engineFeatures_NAME_RU", filename))
		{
			this.key_RU = true;
		}
		if (ES3.KeyExists("genres_NAME_IT", filename))
		{
			this.key_IT = true;
		}
		if (ES3.KeyExists("engineFeatures_NAME_JA", filename))
		{
			this.key_JA = true;
		}
		if (ES3.KeyExists("verkaufspreis_default_addon", filename))
		{
			this.key_default_verkaufpreis = true;
		}
		if (ES3.KeyExists("verkaufspreis_default_bundle", filename))
		{
			this.key_default_verkaufpreisBundle = true;
		}
		if (ES3.KeyExists("npcGameNameInUse", filename))
		{
			this.key_npcGameNameInUse = true;
		}
		if (ES3.KeyExists("achivements", filename))
		{
			this.key_achivements = true;
		}
	}

	
	public bool loadingSavegame;

	
	private ES3Writer writer;

	
	private ES3Reader reader;

	
	private GameObject main_;

	
	private settingsScript settings_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private sfxScript sfx_;

	
	private mapScript mapS_;

	
	private buildRoomScript brS_;

	
	private games games_;

	
	private GUI_Main guiMain_;

	
	private genres genres_;

	
	private themes themes_;

	
	private licences licences_;

	
	private engineFeatures eF_;

	
	private cameraMovementScript cmS_;

	
	private unlockScript unlock_;

	
	private gameplayFeatures gF_;

	
	private publisher publisher_;

	
	private platforms platforms_;

	
	private hardware hardware_;

	
	private hardwareFeatures hardwareFeatures_;

	
	private copyProtect copyProtect_;

	
	private anitCheat antiCheat_;

	
	private arbeitsmarkt arbeitsmarkt_;

	
	private createCharScript cCS_;

	
	private forschungSonstiges fS_;

	
	private contractWorkMain contractWorkMain_;

	
	private publishingOfferMain publishingOfferMain_;

	
	private Menu_Packung menuPackung_;

	
	private Menu_ArcadePreis menuArcadePreis_;

	
	private Menu_BuyInventar menu_BuyInventar_;

	
	private mpMain mpMain_;

	
	private mpCalls mpCalls_;

	
	public int savegamePlayerID;

	
	public int saveGameVersion = 17;

	
	private ES3File es3file;

	
	private bool key_NpcIPs;

	
	private bool key_inventarFilter;

	
	private bool key_fanshopverlauf;

	
	private bool key_achivements;

	
	private bool key_gameTabFilter;

	
	private bool key_npcGameNameInUse;

	
	private bool key_default_verkaufpreis;

	
	private bool key_default_verkaufpreisBundle;

	
	private bool key_legends;

	
	private bool key_EN;

	
	private bool key_GE;

	
	private bool key_TU;

	
	private bool key_CH;

	
	private bool key_FR;

	
	private bool key_PB;

	
	private bool key_HU;

	
	private bool key_CT;

	
	private bool key_ES;

	
	private bool key_PL;

	
	private bool key_CZ;

	
	private bool key_KO;

	
	private bool key_AR;

	
	private bool key_RU;

	
	private bool key_IT;

	
	private bool key_JA;
}
