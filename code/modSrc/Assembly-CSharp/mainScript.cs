using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vectrosity;


public class mainScript : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
	public void FindScripts()
	{
		if (!this.tS_)
		{
			this.tS_ = base.gameObject.GetComponent<textScript>();
		}
		if (!this.mapScript_)
		{
			this.mapScript_ = base.gameObject.GetComponent<mapScript>();
		}
		if (!this.settings_)
		{
			this.settings_ = base.gameObject.GetComponent<settingsScript>();
		}
		if (!this.pickChar_)
		{
			this.pickChar_ = base.gameObject.GetComponent<pickCharacterScript>();
		}
		if (!this.pickObject_)
		{
			this.pickObject_ = base.gameObject.GetComponent<pickObjectScript>();
		}
		if (!this.unlock_)
		{
			this.unlock_ = base.gameObject.GetComponent<unlockScript>();
		}
		if (!this.arbeitsmarkt_)
		{
			this.arbeitsmarkt_ = base.gameObject.GetComponent<arbeitsmarkt>();
		}
		if (!this.genres_)
		{
			this.genres_ = base.gameObject.GetComponent<genres>();
		}
		if (!this.themes_)
		{
			this.themes_ = base.gameObject.GetComponent<themes>();
		}
		if (!this.eF_)
		{
			this.eF_ = base.gameObject.GetComponent<engineFeatures>();
		}
		if (!this.gF_)
		{
			this.gF_ = base.gameObject.GetComponent<gameplayFeatures>();
		}
		if (!this.hardware_)
		{
			this.hardware_ = base.gameObject.GetComponent<hardware>();
		}
		if (!this.hardwareFeatures_)
		{
			this.hardwareFeatures_ = base.gameObject.GetComponent<hardwareFeatures>();
		}
		if (!this.platforms_)
		{
			this.platforms_ = base.gameObject.GetComponent<platforms>();
		}
		if (!this.copyProtect_)
		{
			this.copyProtect_ = base.gameObject.GetComponent<copyProtect>();
		}
		if (!this.antiCheat_)
		{
			this.antiCheat_ = base.gameObject.GetComponent<anitCheat>();
		}
		if (!this.licences_)
		{
			this.licences_ = base.gameObject.GetComponent<licences>();
		}
		if (!this.games_)
		{
			this.games_ = base.gameObject.GetComponent<games>();
		}
		if (!this.npcEngines_)
		{
			this.npcEngines_ = base.gameObject.GetComponent<npcEngines>();
		}
		if (!this.publisher_)
		{
			this.publisher_ = base.gameObject.GetComponent<publisher>();
		}
		if (!this.cCS_)
		{
			this.cCS_ = base.gameObject.GetComponent<createCharScript>();
		}
		if (!this.achScript_)
		{
			this.achScript_ = base.gameObject.GetComponent<achiementScript>();
		}
		if (!this.reviewText_)
		{
			this.reviewText_ = base.gameObject.GetComponent<reviewText>();
		}
		if (!this.forschungSonstiges_)
		{
			this.forschungSonstiges_ = base.gameObject.GetComponent<forschungSonstiges>();
		}
		if (!this.contractWorkMain_)
		{
			this.contractWorkMain_ = base.gameObject.GetComponent<contractWorkMain>();
		}
		if (!this.publishingOfferMain_)
		{
			this.publishingOfferMain_ = base.gameObject.GetComponent<publishingOfferMain>();
		}
		if (!this.rdS_)
		{
			this.rdS_ = base.gameObject.GetComponent<roomDataScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.save_)
		{
			this.save_ = base.gameObject.GetComponent<savegameScript>();
		}
		if (!this.cmS_)
		{
			this.cmS_ = GameObject.Find("CamMovement").GetComponent<cameraMovementScript>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.manager)
		{
			this.manager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
		}
		if (!this.mpCalls_)
		{
			this.mpCalls_ = GameObject.Find("NetworkManager").GetComponent<mpCalls>();
		}
	}

	
	private void Update()
	{
		if (this.guiMain_.IsStartMenuActive())
		{
			return;
		}
		if (this.findRobots)
		{
			this.FindRobots();
			this.findRobots = false;
		}
		if (this.findMuell)
		{
			this.FindMuell();
			this.findMuell = false;
		}
		if (this.findCharacters)
		{
			this.FindCharacters();
			this.findCharacters = false;
		}
		if (this.findObjects)
		{
			this.FindObjects();
			this.findObjects = false;
		}
		if (this.findRooms)
		{
			this.FindRooms();
			this.findRooms = false;
		}
		this.UpdateObjectsAndChars();
		this.UpdateTime();
		this.UpdateSpecialMaterials();
		this.UpdateCars();
		this.UpdateAchivementBonus();
	}

	
	public void LoadOffice(int i, bool fromSavegame)
	{
		this.FindScripts();
		if (i != -1)
		{
			this.officeLoaded = false;
			this.office = i;
			SceneManager.LoadScene("sceneOffice" + i.ToString(), LoadSceneMode.Additive);
			base.StartCoroutine(this.iInitScene(fromSavegame));
			return;
		}
		if (this.multiplayer)
		{
			this.guiMain_.uiObjects[201].SetActive(true);
			this.guiMain_.uiObjects[201].GetComponent<mpMain>().StopNetwork();
			this.guiMain_.uiObjects[201].SetActive(false);
		}
		this.guiMain_.uiObjects[152].SetActive(false);
		this.guiMain_.uiObjects[151].SetActive(true);
		this.guiMain_.uiObjects[155].SetActive(false);
	}

	
	public int GetMapIDfromDropdown(GameObject drop_)
	{
		int result = 0;
		switch (drop_.GetComponent<Dropdown>().value)
		{
		case 0:
			result = 3;
			break;
		case 1:
			result = 4;
			break;
		case 2:
			result = 6;
			break;
		case 3:
			result = 5;
			break;
		}
		return result;
	}

	
	public int GetDropdownSlotFromMapID(int id_)
	{
		int result = 0;
		switch (id_)
		{
		case 3:
			result = 0;
			break;
		case 4:
			result = 1;
			break;
		case 5:
			result = 3;
			break;
		case 6:
			result = 2;
			break;
		}
		return result;
	}

	
	public void CreateStartAuto(int officeID)
	{
		if (officeID == 3)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mapScript_.prefabsInventar[92]);
			gameObject.transform.position = new Vector3(3.5f, 0f, 4f);
			objectScript component = gameObject.GetComponent<objectScript>();
			component.myID = 1;
			component.typ = 92;
			component.InitObjectFromSavegame();
			this.objectRotation = 0f;
			component.PlatziereObject(new Vector3(3.5f, 0f, 5f), true, true);
		}
	}

	
	private IEnumerator iInitScene(bool fromSavegame)
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		this.mapScript_.InitBuilding(fromSavegame);
		GameObject.Find("CamMovement").GetComponent<cameraMovementScript>().FindCameraLimits();
		this.officeLoaded = true;
		yield break;
	}

	
	private void Cheat()
	{
		if (Input.GetKeyDown(KeyCode.C))
		{
			this.guiMain_.uiObjects[216].SetActive(true);
			this.guiMain_.uiObjects[216].GetComponent<Menu_RandomEventGlobal>().Init(13);
		}
	}

	
	public void LoadContent()
	{
		if (this.contendIsLoaded)
		{
			return;
		}
		this.contendIsLoaded = true;
		this.tS_.LoadContent_NPCGameNames();
		this.tS_.LoadContent_NpcIPs();
		this.genres_.LoadGenres("DATA/Genres.txt");
		this.tS_.LoadContent_Themes();
		this.gF_.LoadGameplayFeatures("DATA/GameplayFeatures.txt");
		this.eF_.LoadEngineFeatures("DATA/EngineFeatures.txt");
		this.npcEngines_.LoadNpcEngines("DATA/NpcEngines.txt");
		this.publisher_.LoadPublisher("DATA/Publisher.txt");
		this.hardware_.LoadHardwareKomponenten("DATA/Hardware.txt");
		this.hardwareFeatures_.LoadHardwareFeatures("DATA/HardwareFeatures.txt");
		this.platforms_.LoadPlatforms("DATA/Platforms.txt");
		this.copyProtect_.LoadCopyProtect("DATA/CopyProtect.txt");
		this.antiCheat_.LoadAnitCheat("DATA/AntiCheat.txt");
		this.licences_.LoadLicences("DATA/Licence.txt");
	}

	
	public void LoadContent_MultiplayerClient()
	{
		if (this.contendIsLoaded)
		{
			return;
		}
		this.contendIsLoaded = true;
		this.tS_.LoadContent_NPCGameNames();
		this.tS_.LoadContent_NpcIPs();
		this.licences_.LoadLicences("DATA/Licence.txt");
	}

	
	public void InitNewGame()
	{
		PlayerPrefs.SetInt("Toggle_Walls", 0);
		PlayerPrefs.SetInt("Toggle_PickChars", 0);
		PlayerPrefs.SetInt("Toggle_PickObjects", 0);
		PlayerPrefs.SetInt("Toggle_RoomUI", 0);
		PlayerPrefs.SetInt("Toggle_Ausstattung", 0);
		PlayerPrefs.SetInt("Toggle_Muell", 0);
		PlayerPrefs.SetInt("Toggle_Waerme", 0);
		this.arbeitsmarkt_.ArbeitsmarktUpdaten();
		this.arbeitsmarkt_.ArbeitsmarktUpdaten();
		this.arbeitsmarkt_.ArbeitsmarktUpdaten();
		this.licences_.LizenzenUpdaten();
		this.unlock_.NewGameUnlocks();
		this.unlock_.CheckUnlock(false);
		Menu_NewGameCEO component = this.guiMain_.uiObjects[162].GetComponent<Menu_NewGameCEO>();
		characterScript characterScript = this.CreatePlayer(component.male, component.body, component.eyes, component.hair, component.beard, component.colorSkin, component.colorHair, component.colorHair, component.colorHose, component.colorShirt, component.colorAdd1);
		characterScript.myName = component.uiObjects[12].GetComponent<InputField>().text;
		characterScript.male = component.male;
		characterScript.beruf = component.beruf;
		characterScript.perks = (bool[])component.perks.Clone();
		characterScript.s_gamedesign = component.s_gamedesign;
		characterScript.s_programmieren = component.s_programmieren;
		characterScript.s_grafik = component.s_grafik;
		characterScript.s_sound = component.s_sound;
		characterScript.s_pr = component.s_pr;
		characterScript.s_gametests = component.s_gametests;
		characterScript.s_technik = component.s_technik;
		characterScript.s_forschen = component.s_forschen;
		this.UnlockRandomThemeAndGenre();
		if (!this.multiplayer || (this.multiplayer && this.mpCalls_.isServer))
		{
			this.UpdateTrend(true);
		}
		this.contractWorkMain_.UpdateContractWork(true);
		this.platforms_.UpdatePlatformSells(true, false);
		for (int i = 0; i < this.newsSetting.Length; i++)
		{
			this.newsSetting[i] = true;
		}
	}

	
	public void CreateStartAuftragsspiele()
	{
		int num = 0;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Publisher");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				publisherScript component = array[i].GetComponent<publisherScript>();
				if (component)
				{
					gameScript gameScript = component.CreateNewGame2(true);
					if (gameScript)
					{
						if (gameScript.auftragsspiel)
						{
							num++;
						}
						if (num > 2)
						{
							return;
						}
					}
				}
			}
		}
	}

	
	public void UnlockRandomThemeAndGenre()
	{
		if (this.themes_.themes_RES_POINTS_LEFT == null)
		{
			return;
		}
		if (this.genres_.genres_UNLOCK == null)
		{
			return;
		}
		if (this.themes_.themes_RES_POINTS_LEFT.Length == 0)
		{
			return;
		}
		if (this.genres_.genres_UNLOCK.Length == 0)
		{
			return;
		}
		if (this.multiplayer)
		{
			for (int i = 0; i < this.themes_.themes_RES_POINTS_LEFT.Length; i++)
			{
				this.themes_.themes_RES_POINTS_LEFT[i] = (float)this.themes_.RES_POINTS;
			}
			for (int j = 0; j < this.genres_.genres_UNLOCK.Length; j++)
			{
				this.genres_.genres_RES_POINTS_LEFT[j] = (float)this.genres_.genres_RES_POINTS[j];
			}
		}
		bool flag = false;
		while (!flag)
		{
			this.themes_.themes_RES_POINTS_LEFT[UnityEngine.Random.Range(0, this.themes_.themes_RES_POINTS_LEFT.Length)] = 0f;
			int num = 0;
			for (int k = 0; k < this.themes_.themes_RES_POINTS_LEFT.Length; k++)
			{
				if (this.themes_.themes_RES_POINTS_LEFT[k] <= 0f)
				{
					num++;
				}
				if (num >= 3)
				{
					flag = true;
					break;
				}
			}
		}
		List<int> list = new List<int>();
		for (int l = 0; l < this.genres_.genres_UNLOCK.Length; l++)
		{
			if (this.genres_.genres_UNLOCK[l])
			{
				list.Add(l);
			}
		}
		int num2 = list[UnityEngine.Random.Range(0, list.Count)];
		this.genres_.genres_RES_POINTS_LEFT[num2] = 0f;
	}

	
	public characterScript CreatePlayer(bool male_, int body_, int eyes_, int hair_, int beard_, int skinC_, int hairC_, int beardC_, int hoseC_, int shirtC_, int add1C_)
	{
		characterScript characterScript = this.cCS_.CreateCharacter(1, male_, body_);
		characterScript.myID = 1;
		characterScript.model_body = body_;
		characterScript.model_eyes = eyes_;
		characterScript.model_hair = hair_;
		characterScript.model_beard = beard_;
		characterScript.model_skinColor = skinC_;
		characterScript.model_hairColor = hairC_;
		characterScript.model_beardColor = beardC_;
		characterScript.model_HoseColor = hoseC_;
		characterScript.model_ShirtColor = shirtC_;
		characterScript.model_Add1Color = add1C_;
		characterScript.gameObject.transform.GetChild(0).GetComponent<characterGFXScript>().Init(true);
		characterScript.gameObject.transform.position = new Vector3(10f, 0f, 3f);
		characterScript.gameObject.transform.eulerAngles = new Vector3(0f, 0f, -180f);
		return characterScript;
	}

	
	private void FindCharacters()
	{
		this.arrayCharacters = GameObject.FindGameObjectsWithTag("Character");
		this.arrayCharactersScripts = new characterScript[this.arrayCharacters.Length];
		for (int i = 0; i < this.arrayCharacters.Length; i++)
		{
			if (this.arrayCharacters[i])
			{
				this.arrayCharactersScripts[i] = this.arrayCharacters[i].GetComponent<characterScript>();
			}
		}
		if (this.achScript_)
		{
			if (this.arrayCharacters.Length >= 21)
			{
				this.achScript_.SetAchivement(60);
			}
			if (this.arrayCharacters.Length >= 51)
			{
				this.achScript_.SetAchivement(61);
			}
			if (this.arrayCharacters.Length >= 101)
			{
				this.achScript_.SetAchivement(62);
			}
		}
	}

	
	private void FindRobots()
	{
		this.arrayRobots = GameObject.FindGameObjectsWithTag("Robot");
	}

	
	private void FindMuell()
	{
		this.arrayMuell = GameObject.FindGameObjectsWithTag("Muell");
	}

	
	private void FindObjects()
	{
		this.arrayObjects = GameObject.FindGameObjectsWithTag("Object");
		this.arrayObjectScripts = new objectScript[this.arrayObjects.Length];
		for (int i = 0; i < this.arrayObjects.Length; i++)
		{
			if (this.arrayObjects[i])
			{
				this.arrayObjectScripts[i] = this.arrayObjects[i].GetComponent<objectScript>();
			}
		}
	}

	
	public void FindRooms()
	{
		this.arrayRooms = GameObject.FindGameObjectsWithTag("Room");
	}

	
	private void UpdateObjectsAndChars()
	{
		bool flag = false;
		this.updateUnkorrekterRoom += Time.deltaTime;
		if (this.updateUnkorrekterRoom > 0.3f)
		{
			this.updateUnkorrekterRoom = 0f;
			flag = true;
		}
		foreach (GameObject gameObject in this.arrayRooms)
		{
			if (gameObject)
			{
				gameObject.GetComponent<roomScript>().mitarbeiterZugeteilt = 0;
			}
		}
		for (int j = 0; j < this.arrayObjectScripts.Length; j++)
		{
			if (this.arrayObjectScripts[j])
			{
				this.arrayObjectScripts[j].inUse = false;
				this.arrayObjectScripts[j].MouseMovement();
				if (flag)
				{
					this.arrayObjectScripts[j].UpdateUnkorrekterRoom();
				}
			}
		}
		this.arrayCharactersForDoors.Clear();
		for (int k = 0; k < this.arrayCharactersScripts.Length; k++)
		{
			if (this.arrayCharactersScripts[k])
			{
				if (this.arrayCharactersScripts[k].objectUsingS_)
				{
					this.arrayCharactersScripts[k].objectUsingS_.inUse = true;
				}
				if (this.arrayCharactersScripts[k].roomS_)
				{
					this.arrayCharactersScripts[k].roomS_.mitarbeiterZugeteilt++;
				}
				if (this.arrayCharactersScripts[k].gameObject.transform.position.y < 0.001f)
				{
					this.arrayCharactersForDoors.Add(this.arrayCharactersScripts[k].gameObject);
				}
			}
		}
		this.updateKiTimer += Time.deltaTime;
		if (this.updateKiTimer < 1f)
		{
			return;
		}
		this.updateKiTimer = 0f;
		if (this.personal_ki)
		{
			for (int l = 0; l < this.arrayCharactersScripts.Length; l++)
			{
				if (this.arrayCharactersScripts[l])
				{
					this.arrayCharactersScripts[l].UpdateKI(true);
				}
			}
			for (int m = 0; m < this.arrayCharactersScripts.Length; m++)
			{
				if (this.arrayCharactersScripts[m])
				{
					this.arrayCharactersScripts[m].UpdateKI(false);
				}
			}
		}
	}

	
	public bool IsForcedPause()
	{
		return this.pauseGame;
	}

	
	public void PauseGame(bool p)
	{
		if (this.multiplayer)
		{
			return;
		}
		this.pauseGame = p;
		if (p)
		{
			if (this.gameSpeed > 0f)
			{
				this.gameSpeedOrg = this.gameSpeed;
				this.gameSpeed = 0f;
				return;
			}
		}
		else
		{
			if (this.gameSpeedOrg > 0f)
			{
				this.gameSpeed = this.gameSpeedOrg;
				this.gameSpeedOrg = 0f;
			}
			if (this.guiMain_.spacePause)
			{
				this.gameSpeedOrg = this.gameSpeed;
				this.gameSpeed = 0f;
				this.pauseGame = true;
			}
		}
	}

	
	private void UpdateTime()
	{
		this.dayTimer += Time.deltaTime * this.speedSetting * this.GetGameSpeed();
		this.UpdateWeatherEffects();
		if (this.multiplayer && this.mpCalls_.isClient)
		{
			if (this.dayTimer >= 1f)
			{
				this.dayTimer = 1f;
			}
			return;
		}
		if (this.dayTimer >= 1f)
		{
			this.Autosave();
			this.WochenUpdates();
			if (this.week >= 5)
			{
				this.MonatlicheUpdates();
			}
		}
	}

	
	public void WochenUpdates()
	{
		if (this.multiplayer && this.mpCalls_.isServer)
		{
			this.mpCalls_.SERVER_Send_Command(3);
		}
		this.dayTimer = 0f;
		this.week++;
		this.contractWorkMain_.UpdateContractWork(false);
		this.publishingOfferMain_.UpdatePublishingOffer(false);
		this.arbeitsmarkt_.ArbeitsmarktUpdaten();
		this.platforms_.UpdatePlatformSells(true, false);
		this.copyProtect_.UpdateEffekt();
		this.antiCheat_.UpdateEffekt();
		this.licences_.LizenzenUpdaten();
		this.UpdateTrend(false);
		this.games_.SaveLastChartPosition();
		this.games_.SellAllGames();
		this.games_.UpdateChartsWeek();
		this.UpdateAnrufeWeekly();
		this.UpdateGlobalEvent();
		this.UpdateGenreBeliebtheit();
		this.guiMain_.UpdateCharts();
		if (this.multiplayer && this.mpCalls_.isServer)
		{
			this.mpCalls_.SERVER_Send_Firmenwert();
		}
		if (this.sauerBugs > 0)
		{
			this.sauerBugs--;
		}
		if (this.sauerBugs < 0)
		{
			this.sauerBugs = 0;
		}
		if (this.awardBonus > 0)
		{
			this.awardBonus--;
		}
		if (this.awardBonus < 0)
		{
			this.awardBonus = 0;
		}
		for (int i = 0; i < this.arrayCharacters.Length; i++)
		{
			if (this.arrayCharacters[i])
			{
				characterScript component = this.arrayCharacters[i].GetComponent<characterScript>();
				if (component)
				{
					component.UpdateKuendigen(false);
					component.UpdateKrank();
				}
			}
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("Publisher");
		for (int j = 0; j < array.Length; j++)
		{
			if (array[j])
			{
				publisherScript component2 = array[j].GetComponent<publisherScript>();
				if (component2 && !component2.isPlayer)
				{
					component2.amountTrys = 0;
					component2.CreateNewGame2(false);
				}
			}
		}
		this.unlock_.CheckUnlock(true);
	}

	
	public void MonatlicheUpdates()
	{
		if (this.multiplayer && this.mpCalls_.isServer)
		{
			this.mpCalls_.SERVER_Send_Command(4);
		}
		this.week = 1;
		this.month++;
		this.autoSaveInterval--;
		for (int i = 0; i < this.arrayObjects.Length; i++)
		{
			if (this.arrayObjects[i])
			{
				objectScript component = this.arrayObjects[i].GetComponent<objectScript>();
				if (component)
				{
					component.Monatskosten();
					component.AddAufladungen();
				}
			}
		}
		for (int j = 0; j < this.arrayCharacters.Length; j++)
		{
			if (this.arrayCharacters[j])
			{
				characterScript component2 = this.arrayCharacters[j].GetComponent<characterScript>();
				if (component2)
				{
					component2.Monatskosten();
				}
			}
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("Publisher");
		for (int k = 0; k < array.Length; k++)
		{
			if (array[k])
			{
				publisherScript component3 = array[k].GetComponent<publisherScript>();
				if (component3 && !component3.isPlayer)
				{
					component3.UpdateRandomData();
					component3.VerwaltungskostenBezahlen();
					component3.UpdateTochterfirmaUmsatzVerlauf();
				}
			}
		}
		this.unlock_.CheckUnlock(true);
		this.PayBankZinsen();
		this.UpdateStatsVerlaeufe();
		this.AddFanverlauf((long)this.genres_.GetAmountFans());
		this.ResetMonatsbilanz();
		this.MadGamesAward(false);
		this.MadGamesConvention();
		this.UpdateExklusivPublisher();
		this.UpdateBankWarning();
		this.UpdateTasks();
		if (this.month >= 13)
		{
			this.month = 1;
			this.year++;
			this.badGameThisYear = false;
			this.pubOffersAmount = 0;
			if (this.year >= 2050)
			{
				this.achScript_.SetAchivement(45);
			}
		}
	}

	
	private void UpdateTasks()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Task");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				taskFanshop component = array[i].GetComponent<taskFanshop>();
				if (component)
				{
					component.ResetData();
				}
			}
		}
	}

	
	public void Autosave()
	{
		if (this.autoSaveInterval <= -1)
		{
			switch (this.settings_.saveInterval)
			{
			case 0:
				this.autoSaveInterval = 12;
				break;
			case 1:
				this.autoSaveInterval = 9;
				break;
			case 2:
				this.autoSaveInterval = 6;
				break;
			case 3:
				this.autoSaveInterval = 3;
				break;
			default:
				this.autoSaveInterval = 12;
				break;
			}
		}
		if (this.autoSaveInterval == 0)
		{
			switch (this.settings_.saveInterval)
			{
			case 0:
				this.autoSaveInterval = 12;
				break;
			case 1:
				this.autoSaveInterval = 9;
				break;
			case 2:
				this.autoSaveInterval = 6;
				break;
			case 3:
				this.autoSaveInterval = 3;
				break;
			default:
				this.autoSaveInterval = 12;
				break;
			}
			if (!this.multiplayer)
			{
				this.save_.Save(0);
				this.guiMain_.ShowGameHasSaved();
			}
		}
	}

	
	public void AutoSaveMultiplayer()
	{
		if (this.multiplayer)
		{
			this.save_.SaveMultiplayer(0);
			this.guiMain_.ShowGameHasSaved();
		}
	}

	
	public void MadGamesAward(bool force)
	{
		if ((!this.multiplayer || (this.multiplayer && this.mpCalls_.isServer) || force) && (this.month == 12 || force) && (this.week == 1 || force))
		{
			this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[143]);
			this.guiMain_.uiObjects[143].GetComponent<Menu_Awards>().Init();
			this.guiMain_.OpenMenu(false);
			this.sfx_.PlaySound(50, false);
		}
	}

	
	private void MadGamesConvention()
	{
		if (this.month == 7 && this.week == 1)
		{
			this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[185]);
			this.guiMain_.uiObjects[185].GetComponent<Menu_Messe>().Init();
		}
	}

	
	public string GetMoney(long i, bool showDollar)
	{
		if (!this.tS_)
		{
			this.FindScripts();
		}
		string str = ".";
		if (this.settings_.language == 0)
		{
			str = ",";
		}
		string text = i.ToString();
		text = text.Replace("-", "");
		string text2 = "";
		int num = 0;
		for (int j = text.Length - 1; j >= 0; j--)
		{
			text2 = text[j].ToString() + text2;
			if (j != 0)
			{
				num++;
				if (num >= 3)
				{
					text2 = str + text2;
					num = 0;
				}
			}
		}
		if (i < 0L)
		{
			text2 = "-" + text2;
		}
		if (showDollar)
		{
			return this.tS_.GetText(7) + text2;
		}
		return text2;
	}

	
	public Sprite LoadPNG(string filePath)
	{
		Texture2D texture2D;
		if (File.Exists(filePath))
		{
			byte[] data = File.ReadAllBytes(filePath);
			texture2D = new Texture2D(2, 2, TextureFormat.RGBA32, false);
			texture2D.LoadImage(data);
		}
		else
		{
			byte[] data = File.ReadAllBytes(Application.dataPath + "/Extern/Icons_Platforms/missing.png");
			texture2D = new Texture2D(2, 2, TextureFormat.RGBA32, false);
			texture2D.LoadImage(data);
			Debug.Log("LoadPNG() -> Missing File: " + filePath);
		}
		return Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), Vector2.zero);
	}

	
	public Texture2D LoadTexture(string filePath)
	{
		Texture2D texture2D = null;
		if (File.Exists(filePath))
		{
			byte[] data = File.ReadAllBytes(filePath);
			texture2D = new Texture2D(2, 2, TextureFormat.RGBA32, true);
			texture2D.LoadImage(data);
		}
		else
		{
			Debug.Log("LoadPNG() -> Missing File: " + filePath);
		}
		return texture2D;
	}

	
	public void CopyArbeitsmarktCharacterData(charArbeitsmarkt cA_, characterScript cS_)
	{
		cS_.myID = cA_.myID;
		cS_.male = cA_.male;
		cS_.myName = cA_.myName;
		cS_.legend = cA_.legend;
		cS_.beruf = cA_.beruf;
		cS_.s_motivation = 100f;
		cS_.s_gamedesign = cA_.s_gamedesign;
		cS_.s_programmieren = cA_.s_programmieren;
		cS_.s_grafik = cA_.s_grafik;
		cS_.s_sound = cA_.s_sound;
		cS_.s_pr = cA_.s_pr;
		cS_.s_gametests = cA_.s_gametests;
		cS_.s_technik = cA_.s_technik;
		cS_.s_forschen = cA_.s_forschen;
		cS_.perks = (bool[])cA_.perks.Clone();
		cS_.durst = UnityEngine.Random.Range(15f, 100f);
		cS_.hunger = UnityEngine.Random.Range(15f, 100f);
		cS_.klo = UnityEngine.Random.Range(15f, 100f);
		cS_.muell = UnityEngine.Random.Range(15f, 100f);
		cS_.giessen = UnityEngine.Random.Range(15f, 100f);
	}

	
	public long finanzenJahr_GetGewinn()
	{
		long num = 0L;
		for (int i = 0; i < 50; i++)
		{
			num += this.finanzenJahr[i];
		}
		long num2 = 0L;
		for (int j = 50; j < 100; j++)
		{
			num2 += this.finanzenJahr[j];
		}
		return num2 - num;
	}

	
	public long finanzenJahrLast_GetGewinn()
	{
		long num = 0L;
		for (int i = 0; i < 50; i++)
		{
			num += this.finanzenJahrLast[i];
		}
		long num2 = 0L;
		for (int j = 50; j < 100; j++)
		{
			num2 += this.finanzenJahrLast[j];
		}
		return num2 - num;
	}

	
	public long finanzenMonat_GetGewinn()
	{
		long num = 0L;
		for (int i = 0; i < 50; i++)
		{
			num += this.finanzenMonat[i];
		}
		long num2 = 0L;
		for (int j = 50; j < 100; j++)
		{
			num2 += this.finanzenMonat[j];
		}
		return num2 - num;
	}

	
	public long finanzenMonatLast_GetGewinn()
	{
		long num = 0L;
		for (int i = 0; i < 50; i++)
		{
			num += this.finanzenMonatLast[i];
		}
		long num2 = 0L;
		for (int j = 50; j < 100; j++)
		{
			num2 += this.finanzenMonatLast[j];
		}
		return num2 - num;
	}

	
	private void ResetMonatsbilanz()
	{
		this.finanzVerlauf[0] = this.finanzenMonat_GetGewinn();
		if (this.month >= 13)
		{
			this.ResetJahresbilanz();
		}
		for (int i = 0; i < this.finanzenMonat.Length; i++)
		{
			this.finanzenMonatLast[i] = this.finanzenMonat[i];
			this.finanzenMonat[i] = 0L;
		}
	}

	
	public void ResetJahresbilanz()
	{
		long num = 0L;
		for (int i = 0; i < 50; i++)
		{
			num += this.finanzenJahr[i];
		}
		long num2 = 0L;
		for (int j = 50; j < 100; j++)
		{
			num2 += this.finanzenJahr[j];
		}
		this.finanzVerlaufEinnahmen.Add(num2);
		this.finanzVerlaufAusgaben.Add(num);
		for (int k = 0; k < this.finanzenJahr.Length; k++)
		{
			this.finanzenJahrLast[k] = this.finanzenJahr[k];
			this.finanzenJahr[k] = 0L;
		}
	}

	
	public void AddMadGameConvetionVerlauf(int bestGrafik_, int bestSound_, int bestStudio_, int bestPublisher_, int bestGame_, int badBame_)
	{
		this.madGamesCon_Jahr.Add(this.year);
		this.madGamesCon_BestGrafik.Add(bestGrafik_);
		this.madGamesCon_BestSound.Add(bestSound_);
		this.madGamesCon_BestStudio.Add(bestStudio_);
		this.madGamesCon_BestPublisher.Add(bestPublisher_);
		this.madGamesCon_BestGame.Add(bestGame_);
		this.madGamesCon_BadGame.Add(badBame_);
	}

	
	public void Pay(long amount, int what)
	{
		this.money -= amount;
		switch (what)
		{
		case 0:
			this.finanzenMonat[0] += amount;
			this.finanzenJahr[0] += amount;
			return;
		case 1:
			this.finanzenMonat[1] += amount;
			this.finanzenJahr[1] += amount;
			return;
		case 2:
			this.finanzenMonat[3] += amount;
			this.finanzenJahr[3] += amount;
			return;
		case 3:
			this.finanzenMonat[6] += amount;
			this.finanzenJahr[6] += amount;
			return;
		case 4:
			this.finanzenMonat[2] += amount;
			this.finanzenJahr[2] += amount;
			return;
		case 5:
			this.finanzenMonat[6] += amount;
			this.finanzenJahr[6] += amount;
			return;
		case 6:
			this.finanzenMonat[6] += amount;
			this.finanzenJahr[6] += amount;
			return;
		case 7:
			this.finanzenMonat[7] += amount;
			this.finanzenJahr[7] += amount;
			return;
		case 8:
			this.finanzenMonat[9] += amount;
			this.finanzenJahr[9] += amount;
			return;
		case 9:
			this.finanzenMonat[4] += amount;
			this.finanzenJahr[4] += amount;
			return;
		case 10:
			this.finanzenMonat[2] += amount;
			this.finanzenJahr[2] += amount;
			return;
		case 11:
			this.finanzenMonat[7] += amount;
			this.finanzenJahr[7] += amount;
			return;
		case 12:
			this.finanzenMonat[5] += amount;
			this.finanzenJahr[5] += amount;
			return;
		case 13:
			this.finanzenMonat[9] += amount;
			this.finanzenJahr[9] += amount;
			return;
		case 14:
			this.finanzenMonat[8] += amount;
			this.finanzenJahr[8] += amount;
			return;
		case 15:
			this.finanzenMonat[2] += amount;
			this.finanzenJahr[2] += amount;
			return;
		case 16:
			this.finanzenMonat[5] += amount;
			this.finanzenJahr[5] += amount;
			return;
		case 17:
			this.finanzenMonat[5] += amount;
			this.finanzenJahr[5] += amount;
			return;
		case 18:
			this.finanzenMonat[2] += amount;
			this.finanzenJahr[2] += amount;
			return;
		case 19:
			this.finanzenMonat[10] += amount;
			this.finanzenJahr[10] += amount;
			return;
		case 20:
			this.finanzenMonat[11] += amount;
			this.finanzenJahr[11] += amount;
			return;
		case 21:
			this.finanzenMonat[12] += amount;
			this.finanzenJahr[12] += amount;
			return;
		case 22:
			this.finanzenMonat[13] += amount;
			this.finanzenJahr[13] += amount;
			return;
		case 23:
			this.finanzenMonat[14] += amount;
			this.finanzenJahr[14] += amount;
			return;
		case 24:
			this.finanzenMonat[4] += amount;
			this.finanzenJahr[4] += amount;
			return;
		case 25:
			this.finanzenMonat[9] += amount;
			this.finanzenJahr[9] += amount;
			return;
		case 26:
			this.finanzenMonat[9] += amount;
			this.finanzenJahr[9] += amount;
			return;
		case 27:
			this.finanzenMonat[15] += amount;
			this.finanzenJahr[15] += amount;
			return;
		case 28:
			this.finanzenMonat[16] += amount;
			this.finanzenJahr[16] += amount;
			return;
		case 29:
			this.finanzenMonat[16] += amount;
			this.finanzenJahr[16] += amount;
			return;
		case 30:
			this.finanzenMonat[16] += amount;
			this.finanzenJahr[16] += amount;
			return;
		default:
			return;
		}
	}

	
	public void Earn(long amount, int what)
	{
		this.money += amount;
		switch (what)
		{
		case 0:
			this.finanzenMonat[54] += amount;
			this.finanzenJahr[54] += amount;
			break;
		case 1:
			this.finanzenMonat[56] += amount;
			this.finanzenJahr[56] += amount;
			break;
		case 2:
			this.finanzenMonat[55] += amount;
			this.finanzenJahr[55] += amount;
			break;
		case 3:
			this.finanzenMonat[50] += amount;
			this.finanzenJahr[50] += amount;
			break;
		case 4:
			this.finanzenMonat[51] += amount;
			this.finanzenJahr[51] += amount;
			break;
		case 5:
			this.finanzenMonat[52] += amount;
			this.finanzenJahr[52] += amount;
			break;
		case 6:
			this.finanzenMonat[53] += amount;
			this.finanzenJahr[53] += amount;
			break;
		case 7:
			this.finanzenMonat[57] += amount;
			this.finanzenJahr[57] += amount;
			break;
		case 8:
			this.finanzenMonat[58] += amount;
			this.finanzenJahr[58] += amount;
			break;
		case 9:
			this.finanzenMonat[59] += amount;
			this.finanzenJahr[59] += amount;
			break;
		case 10:
			this.finanzenMonat[56] += amount;
			this.finanzenJahr[56] += amount;
			break;
		case 11:
			this.finanzenMonat[60] += amount;
			this.finanzenJahr[60] += amount;
			break;
		case 12:
			this.finanzenMonat[61] += amount;
			this.finanzenJahr[61] += amount;
			break;
		case 13:
			this.finanzenMonat[61] += amount;
			this.finanzenJahr[61] += amount;
			break;
		}
		if (this.achScript_)
		{
			if (this.money >= 50000000L)
			{
				this.achScript_.SetAchivement(63);
			}
			if (this.money >= 500000000L)
			{
				this.achScript_.SetAchivement(64);
			}
			if (this.money >= 1000000000L && this.difficulty >= 5)
			{
				this.achScript_.SetAchivement(65);
			}
		}
	}

	
	private void UpdateAnrufeWeekly()
	{
		if (this.genres_.GetAmountFans() < 5000)
		{
			return;
		}
		int amountFans = this.genres_.GetAmountFans();
		float num = (float)(amountFans / 3000);
		this.AddAnrufe(UnityEngine.Random.Range(0, (int)num));
		switch (this.GetAnrufeAmount())
		{
		case 0:
		case 1:
			break;
		case 2:
		{
			float f = UnityEngine.Random.Range(0f, (float)amountFans * 0.001f);
			this.AddFans(-UnityEngine.Random.Range(0, Mathf.RoundToInt(f)), -1);
			return;
		}
		case 3:
		{
			float f2 = UnityEngine.Random.Range(0f, (float)amountFans * 0.001f);
			float f3 = UnityEngine.Random.Range(0f, (float)amountFans * 0.002f);
			this.AddFans(-UnityEngine.Random.Range(Mathf.RoundToInt(f2), Mathf.RoundToInt(f3)), -1);
			break;
		}
		default:
			return;
		}
	}

	
	public float GetAnrufe100Prozent()
	{
		int num = this.genres_.GetAmountFans();
		if (num < 5000)
		{
			num = 5000;
		}
		float num2 = (float)num * 0.01f;
		if (num2 > 0f)
		{
			num2 = (float)this.anrufe / num2 * 5f;
		}
		return num2;
	}

	
	public int GetAnrufeAmount()
	{
		if (this.genres_.GetAmountFans() <= 0)
		{
			return 0;
		}
		if (this.anrufe <= 0)
		{
			return 0;
		}
		float anrufe100Prozent = this.GetAnrufe100Prozent();
		if (anrufe100Prozent >= 0f && anrufe100Prozent <= 25f)
		{
			return 0;
		}
		if (anrufe100Prozent >= 25f && anrufe100Prozent <= 50f)
		{
			return 1;
		}
		if (anrufe100Prozent >= 50f && anrufe100Prozent <= 75f)
		{
			return 2;
		}
		if (anrufe100Prozent >= 75f)
		{
			return 3;
		}
		return 0;
	}

	
	public void AddAnrufe(int i)
	{
		int amountFans = this.genres_.GetAmountFans();
		this.anrufe += i * (this.difficulty + 1);
		if (this.anrufe < 0)
		{
			this.anrufe = 0;
		}
		if (this.anrufe > amountFans)
		{
			this.anrufe = amountFans;
		}
	}

	
	public void AddFans(int i, int genre_)
	{
		if ((this.gelangweiltGenre != -1 || this.sauerBugs > 0) && genre_ != -1 && i >= 0)
		{
			return;
		}
		if (genre_ != -1)
		{
			this.genres_.genres_FANS[genre_] += i;
			if (this.genres_.genres_FANS[genre_] < 0)
			{
				this.genres_.genres_FANS[genre_] = 0;
			}
		}
		else if (i < 0)
		{
			int num = this.genres_.GetAmountFans();
			i = Mathf.Abs(i);
			while (i > 0)
			{
				for (int j = 0; j < this.genres_.genres_FANS.Length; j++)
				{
					if (this.genres_.genres_FANS[j] > 0 && i > 0)
					{
						this.genres_.genres_FANS[j]--;
						i--;
						num--;
					}
				}
				if (i <= 0 || num <= 0)
				{
					break;
				}
			}
			for (int k = 0; k < this.genres_.genres_FANS.Length; k++)
			{
				if (this.genres_.genres_FANS[k] < 0)
				{
					this.genres_.genres_FANS[k] = 0;
				}
			}
		}
		else
		{
			while (i > 0 && i > 0)
			{
				int num2 = UnityEngine.Random.Range(0, this.genres_.genres_FANS.Length);
				if (this.genres_.genres_UNLOCK[num2])
				{
					if (i >= 10)
					{
						this.genres_.genres_FANS[num2] += 10;
						i -= 10;
					}
					else
					{
						this.genres_.genres_FANS[num2]++;
						i--;
					}
				}
			}
		}
		for (int l = 0; l < this.genres_.genres_FANS.Length; l++)
		{
			if (this.genres_.genres_FANS[l] > 20000000)
			{
				this.genres_.genres_FANS[l] = 20000000;
			}
		}
		if (this.genres_.GetAmountFans() >= 1000000)
		{
			this.achScript_.SetAchivement(54);
		}
	}

	
	public void UpdatePathfindingForAll()
	{
		for (int i = 0; i < this.arrayCharacters.Length; i++)
		{
			if (this.arrayCharacters[i])
			{
				this.arrayCharacters[i].GetComponent<movementScript>().RecalculatePath();
			}
		}
		for (int j = 0; j < this.arrayRobots.Length; j++)
		{
			if (this.arrayRobots[j])
			{
				this.arrayRobots[j].GetComponent<robotScript>().RecalculatePath();
			}
		}
	}

	
	public void UpdatePathfindingNextFrameExtern()
	{
		base.StartCoroutine(this.UpdatePathfindingNextFrame());
	}

	
	public IEnumerator UpdatePathfindingNextFrame()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		if (this.mapScript_.aStar_)
		{
			this.mapScript_.aStar_.Scan(null);
		}
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		this.UpdatePathfindingForAll();
		yield break;
	}

	
	public void DisableSelectLayer(GameObject go)
	{
		base.StartCoroutine(this.iDisableSelectLayer(go));
	}

	
	private IEnumerator iDisableSelectLayer(GameObject go)
	{
		yield return new WaitForEndOfFrame();
		if (go && go.transform.GetChild(0))
		{
			this.SetLayer(0, go.transform.GetChild(0));
		}
		yield break;
	}

	
	public void SetLayer(int newLayer, Transform trans)
	{
		trans.gameObject.layer = newLayer;
		foreach (object obj in trans)
		{
			Transform transform = (Transform)obj;
			transform.gameObject.layer = newLayer;
			if (transform.childCount > 0)
			{
				this.SetLayer(newLayer, transform.transform);
			}
		}
	}

	
	public void RemovePickedChar(GameObject go)
	{
		if (!this.unlock_.Get(13))
		{
			this.unlock_.CheckUnlock(true);
		}
		for (int i = 0; i < this.pickedChars.Count; i++)
		{
			if (this.pickedChars[i] == go)
			{
				this.pickedChars.RemoveAt(i);
				return;
			}
		}
	}

	
	public void AddPickedChar(GameObject go)
	{
		this.pickedChars.Add(go);
	}

	
	public void SetGameSpeed(float f)
	{
		this.gameSpeed = f;
	}

	
	public float GetGameSpeed()
	{
		float result = this.gameSpeed;
		if (this.multiplayer && this.settings_autoPauseForMultiplayer && this.mpCalls_.AutoPause())
		{
			result = 0f;
		}
		return result;
	}

	
	public float GetDeltaTime()
	{
		return Time.deltaTime * this.GetGameSpeed();
	}

	
	public float Round(float value, int digits)
	{
		float num = Mathf.Pow(10f, (float)digits);
		return Mathf.Round(value * num) / num;
	}

	
	public void SortChildrenByName(GameObject obj)
	{
		List<Transform> list = new List<Transform>();
		for (int i = obj.transform.childCount - 1; i >= 0; i--)
		{
			Transform child = obj.transform.GetChild(i);
			list.Add(child);
			child.parent = null;
		}
		list.Sort((Transform t1, Transform t2) => t1.name.CompareTo(t2.name));
		foreach (Transform transform in list)
		{
			transform.parent = obj.transform;
		}
	}

	
	private float GetFloatMax(string stringValue)
	{
		float num = 1f;
		float.TryParse(stringValue, out num);
		return -num;
	}

	
	private float GetFloatMin(string stringValue)
	{
		float result = 1f;
		float.TryParse(stringValue, out result);
		return result;
	}

	
	public void SortChildrenByFloat(GameObject obj)
	{
		List<Transform> list = new List<Transform>();
		for (int i = obj.transform.childCount - 1; i >= 0; i--)
		{
			Transform child = obj.transform.GetChild(i);
			list.Add(child);
			child.parent = null;
		}
		list.Sort(new Comparison<Transform>(this.CompareFloat));
		foreach (Transform transform in list)
		{
			transform.parent = obj.transform;
		}
	}

	
	private int CompareFloat(Transform t1, Transform t2)
	{
		int num = this.GetFloatMax(t1.name).CompareTo(this.GetFloatMax(t2.name));
		if (num != 0)
		{
			return num;
		}
		if (t1.GetInstanceID() > t2.GetInstanceID())
		{
			return -1;
		}
		return 1;
	}

	
	public int GetNewID()
	{
		return UnityEngine.Random.Range(1, 2000000000);
	}

	
	private void UpdateSpecialMaterials()
	{
		float x = Mathf.Cos(Time.time * this.GetGameSpeed()) * 0.5f + 1f;
		float y = Mathf.Sin(Time.time * this.GetGameSpeed()) * 0.5f + 1f;
		this.specialMaterials[0].mainTextureScale = new Vector2(x, y);
		this.specialMaterials[1].mainTextureOffset = new Vector2(Time.time * (this.GetGameSpeed() * 0.3f), 0f);
		this.specialMaterials[2].mainTextureOffset = new Vector2(0f, -Time.time * (this.GetGameSpeed() * 0.26f));
		this.specialMaterials[3].mainTextureOffset = new Vector2(0f, -Time.time * (this.GetGameSpeed() * 1.5f));
	}

	
	private void ResetGenreBeliebtheit()
	{
		for (int i = 0; i < this.genres_.genres_BELIEBTHEIT.Length; i++)
		{
			if (this.genres_.genres_BELIEBTHEIT[i] <= 0f)
			{
				this.genres_.genres_BELIEBTHEIT[i] = (float)UnityEngine.Random.Range(40, 80);
			}
			if (UnityEngine.Random.Range(0, 100) > 50)
			{
				this.genres_.genres_BELIEBTHEIT_SOLL[i] = true;
			}
			else
			{
				this.genres_.genres_BELIEBTHEIT_SOLL[i] = false;
			}
		}
		int num = 0;
		int num2 = 0;
		for (int j = 0; j < this.genres_.genres_BELIEBTHEIT.Length; j++)
		{
			if (j != this.trendAntiGenre && j != this.trendGenre && this.genres_.genres_UNLOCK[j])
			{
				if (this.genres_.genres_BELIEBTHEIT_SOLL[j])
				{
					num++;
				}
				else
				{
					num2++;
				}
			}
		}
		if (num <= 0)
		{
			int num3;
			do
			{
				num3 = UnityEngine.Random.Range(0, this.genres_.genres_BELIEBTHEIT.Length);
			}
			while (num3 == this.trendAntiGenre || num3 == this.trendGenre || !this.genres_.genres_UNLOCK[num3]);
			this.genres_.genres_BELIEBTHEIT_SOLL[num3] = true;
		}
	}

	
	private void UpdateGenreBeliebtheit()
	{
		if (this.multiplayer && this.mpCalls_.isClient)
		{
			return;
		}
		for (int i = 0; i < this.genres_.genres_BELIEBTHEIT.Length; i++)
		{
			if (this.genres_.genres_BELIEBTHEIT_SOLL[i])
			{
				this.genres_.genres_BELIEBTHEIT[i] += 0.3f;
				if (this.genres_.genres_BELIEBTHEIT[i] > 80f)
				{
					this.genres_.genres_BELIEBTHEIT[i] = 80f;
				}
			}
			else
			{
				this.genres_.genres_BELIEBTHEIT[i] -= 0.3f;
				if (this.genres_.genres_BELIEBTHEIT[i] < 40f)
				{
					this.genres_.genres_BELIEBTHEIT[i] = 40f;
				}
			}
		}
		if (this.multiplayer && this.mpCalls_.isServer)
		{
			this.mpCalls_.SERVER_Send_GenreBeliebtheit();
		}
	}

	
	private void UpdateTrend(bool newGame)
	{
		Debug.Log("UpdateTrend()");
		this.trendWeeks--;
		if (this.multiplayer && this.mpCalls_.isClient)
		{
			return;
		}
		if (this.trendWeeks < 0)
		{
			this.trendWeeks = UnityEngine.Random.Range(50, 100);
			this.ResetGenreBeliebtheit();
			this.UpdateGenreBeliebtheit();
			if (UnityEngine.Random.Range(0, 100) < 20 || this.trendNextTheme == -1)
			{
				this.trendTheme = UnityEngine.Random.Range(0, this.themes_.themes_LEVEL.Length);
				this.trendAntiTheme = UnityEngine.Random.Range(0, this.themes_.themes_LEVEL.Length);
				this.trendNextTheme = UnityEngine.Random.Range(0, this.themes_.themes_LEVEL.Length);
				this.trendNextAntiTheme = UnityEngine.Random.Range(0, this.themes_.themes_LEVEL.Length);
			}
			else
			{
				this.trendTheme = this.trendNextTheme;
				this.trendAntiTheme = this.trendNextAntiTheme;
				this.trendNextTheme = UnityEngine.Random.Range(0, this.themes_.themes_LEVEL.Length);
				this.trendNextAntiTheme = UnityEngine.Random.Range(0, this.themes_.themes_LEVEL.Length);
			}
			if (this.trendAntiTheme == this.trendTheme)
			{
				if (this.trendAntiTheme > 0)
				{
					this.trendAntiTheme--;
				}
				else
				{
					this.trendAntiTheme++;
				}
			}
			if (this.trendNextAntiTheme == this.trendNextTheme)
			{
				if (this.trendNextAntiTheme > 0)
				{
					this.trendNextAntiTheme--;
				}
				else
				{
					this.trendNextAntiTheme++;
				}
			}
			int num = 0;
			bool flag = false;
			if (UnityEngine.Random.Range(0, 100) >= 20)
			{
				if (this.trendNextGenre != -1)
				{
					this.trendGenre = this.trendNextGenre;
					goto IL_1ED;
				}
			}
			while (!flag)
			{
				this.trendGenre = UnityEngine.Random.Range(0, this.genres_.genres_LEVEL.Length);
				if (this.genres_.genres_UNLOCK[this.trendGenre])
				{
					flag = true;
				}
				num++;
				if (num > 10000)
				{
					flag = true;
				}
			}
			IL_1ED:
			num = 0;
			flag = false;
			while (!flag)
			{
				this.trendNextGenre = UnityEngine.Random.Range(0, this.genres_.genres_LEVEL.Length);
				if (this.genres_.genres_UNLOCK[this.trendNextGenre])
				{
					flag = true;
				}
				num++;
				if (num > 10000)
				{
					flag = true;
				}
			}
			num = 0;
			flag = false;
			if (UnityEngine.Random.Range(0, 100) < 20 || this.trendNextAntiGenre == -1)
			{
				int num2 = 0;
				if (newGame && !this.multiplayer)
				{
					for (int i = 0; i < this.genres_.genres_RES_POINTS_LEFT.Length; i++)
					{
						if (this.genres_.genres_RES_POINTS_LEFT[i] <= 0f)
						{
							num2 = i;
							break;
						}
					}
				}
				while (!flag)
				{
					this.trendAntiGenre = UnityEngine.Random.Range(0, this.genres_.genres_LEVEL.Length);
					if (this.genres_.genres_UNLOCK[this.trendAntiGenre] && this.trendAntiGenre != this.trendGenre)
					{
						flag = true;
					}
					if (newGame && !this.multiplayer && this.trendAntiGenre == num2)
					{
						Debug.Log("Nicht das eigene Fangenre wählen!");
						flag = false;
					}
					num++;
					if (num > 10000)
					{
						flag = true;
					}
				}
			}
			else
			{
				this.trendAntiGenre = this.trendNextAntiGenre;
			}
			num = 0;
			flag = false;
			while (!flag)
			{
				this.trendNextAntiGenre = UnityEngine.Random.Range(0, this.genres_.genres_LEVEL.Length);
				if (this.genres_.genres_UNLOCK[this.trendNextAntiGenre] && this.trendNextAntiGenre != this.trendNextGenre)
				{
					flag = true;
				}
				num++;
				if (num > 10000)
				{
					flag = true;
				}
			}
			if (this.trendGenre == this.trendAntiGenre)
			{
				for (int j = 0; j < this.genres_.genres_LEVEL.Length; j++)
				{
					if (this.genres_.genres_UNLOCK[j] && this.trendGenre != j)
					{
						this.trendAntiGenre = j;
						break;
					}
				}
			}
			if (this.trendTheme == this.trendNextAntiTheme)
			{
				for (int k = 0; k < this.themes_.themes_LEVEL.Length; k++)
				{
					if (this.trendTheme != k)
					{
						this.trendNextAntiTheme = k;
						break;
					}
				}
			}
			this.ShowTrendNews();
			if (this.multiplayer && this.mpCalls_.isServer)
			{
				this.mpCalls_.SERVER_Send_Trend();
			}
		}
	}

	
	public void ShowTrendNews()
	{
		if (this.year != 1976 || this.month != 1)
		{
			this.guiMain_.CreateTopNewsTrend(this.genres_.GetName(this.trendGenre) + " / " + this.tS_.GetThemes(this.trendTheme), this.genres_.GetPic(this.trendGenre));
		}
	}

	
	public int PassedMonth()
	{
		return (this.year - 1976) * 12 + this.month;
	}

	
	public void DrawFilter(int mode, bool force)
	{
		if (!force)
		{
			this.filterTimer += Time.deltaTime;
			if (this.filterTimer < 1f)
			{
				return;
			}
			this.filterTimer = 0f;
		}
		Color32 color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, 0);
		Color32[] pixels = this.specialTextures[0].GetPixels32();
		for (int i = 0; i < pixels.Length; i++)
		{
			pixels[i] = color;
		}
		this.specialTextures[0].SetPixels32(pixels);
		switch (mode)
		{
		case 0:
			for (int j = 0; j < mapScript.mapSizeX; j++)
			{
				for (int k = 0; k < mapScript.mapSizeY; k++)
				{
					float num = this.mapScript_.mapAusstattung[j, k];
					if (num > 1f)
					{
						num = 1f;
					}
					this.specialTextures[0].SetPixel(j, k, new Color(0f, 1f, 0f, num));
				}
			}
			break;
		case 1:
			for (int l = 0; l < mapScript.mapSizeX; l++)
			{
				for (int m = 0; m < mapScript.mapSizeY; m++)
				{
					float num2 = this.mapScript_.mapMuell[l, m];
					if (num2 > 1f)
					{
						num2 = 1f;
					}
					this.specialTextures[0].SetPixel(l, m, new Color(1f, 0f, 0f, num2));
				}
			}
			break;
		case 2:
			for (int n = 0; n < mapScript.mapSizeX; n++)
			{
				for (int num3 = 0; num3 < mapScript.mapSizeY; num3++)
				{
					float num4 = this.mapScript_.mapWaerme[n, num3];
					if (num4 > 1f)
					{
						num4 = 1f;
					}
					int num5 = 0;
					if (this.mapScript_.mapRoomScript[n, num3])
					{
						num5 = this.mapScript_.mapRoomScript[n, num3].typ;
					}
					if (num5 != 15)
					{
						this.specialTextures[0].SetPixel(n, num3, new Color(0f, 1f, 0f, num4));
					}
					else
					{
						this.specialTextures[0].SetPixel(n, num3, new Color(1f, 0f, 0f, num4));
					}
				}
			}
			break;
		}
		this.specialTextures[0].Apply();
	}

	
	public void SetAllFloorTextures(int mode)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Floor");
		if (array.Length != 0)
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i])
				{
					floorScript component = array[i].GetComponent<floorScript>();
					if (component && this.IsMyBuilding(this.mapScript_.mapBuilding[Mathf.RoundToInt(array[i].transform.position.x), Mathf.RoundToInt(array[i].transform.position.z)]))
					{
						if (mode == 0)
						{
							component.SetStandardTexture();
						}
						if (mode == 1)
						{
							component.SetFilterTexture();
						}
					}
				}
			}
		}
	}

	
	public void SetBuildGrid(bool b)
	{
		if (!b)
		{
			this.floorMaterials[2].EnableKeyword("_DETAIL_MULX2");
			this.floorMaterials[2].SetTexture("_DetailAlbedoMap", null);
			return;
		}
		this.floorMaterials[2].EnableKeyword("_DETAIL_MULX2");
		this.floorMaterials[2].SetTexture("_DetailAlbedoMap", this.specialTextures[1]);
	}

	
	public void AddFanshopverlauf(long i)
	{
		this.fanshopverlauf[0] += i;
	}

	
	public void AddVerkaufsverlaufKonsolen(long i)
	{
		this.verkaufsverlaufKonsolen[0] += i;
	}

	
	public void AddVerkaufsverlauf(long i)
	{
		this.verkaufsverlauf[0] += i;
	}

	
	public void AddDownloadverlauf(long i)
	{
		this.downloadverlauf[0] += i;
	}

	
	public void AddFanverlauf(long i)
	{
		this.fansverlauf[0] = i;
	}

	
	public void AddAboverlauf(long i)
	{
		this.aboverlauf[0] = i;
	}

	
	public void AddFinanzverlauf(long i)
	{
		this.finanzVerlauf[0] = i;
	}

	
	private void UpdateStatsVerlaeufe()
	{
		for (int i = this.fanshopverlauf.Length - 1; i > 0; i--)
		{
			this.fanshopverlauf[i] = this.fanshopverlauf[i - 1];
		}
		this.fanshopverlauf[0] = 0L;
		for (int j = this.finanzVerlauf.Length - 1; j > 0; j--)
		{
			this.finanzVerlauf[j] = this.finanzVerlauf[j - 1];
		}
		this.finanzVerlauf[0] = 0L;
		for (int k = this.verkaufsverlauf.Length - 1; k > 0; k--)
		{
			this.verkaufsverlauf[k] = this.verkaufsverlauf[k - 1];
		}
		this.verkaufsverlauf[0] = 0L;
		for (int l = this.verkaufsverlaufKonsolen.Length - 1; l > 0; l--)
		{
			this.verkaufsverlaufKonsolen[l] = this.verkaufsverlaufKonsolen[l - 1];
		}
		this.verkaufsverlaufKonsolen[0] = 0L;
		for (int m = this.aboverlauf.Length - 1; m > 0; m--)
		{
			this.aboverlauf[m] = this.aboverlauf[m - 1];
		}
		this.aboverlauf[0] = 0L;
		for (int n = this.downloadverlauf.Length - 1; n > 0; n--)
		{
			this.downloadverlauf[n] = this.downloadverlauf[n - 1];
		}
		this.downloadverlauf[0] = 0L;
		for (int num = this.fansverlauf.Length - 1; num > 0; num--)
		{
			this.fansverlauf[num] = this.fansverlauf[num - 1];
		}
		this.fansverlauf[0] = 0L;
	}

	
	public long GetKreditlimit()
	{
		long num = (long)(250000 * (this.year - 1975));
		switch (this.difficulty)
		{
		case 0:
			num = (long)(300000 * (this.year - 1975));
			num += (long)(30000 * this.studioPoints);
			num += (long)(30000 * Mathf.RoundToInt(this.auftragsAnsehen));
			break;
		case 1:
			num = (long)(250000 * (this.year - 1975));
			num += (long)(25000 * this.studioPoints);
			num += (long)(25000 * Mathf.RoundToInt(this.auftragsAnsehen));
			break;
		case 2:
			num = (long)(200000 * (this.year - 1975));
			num += (long)(20000 * this.studioPoints);
			num += (long)(20000 * Mathf.RoundToInt(this.auftragsAnsehen));
			break;
		case 3:
			num = (long)(150000 * (this.year - 1975));
			num += (long)(15000 * this.studioPoints);
			num += (long)(15000 * Mathf.RoundToInt(this.auftragsAnsehen));
			break;
		case 4:
			num = (long)(100000 * (this.year - 1975));
			num += (long)(10000 * this.studioPoints);
			num += (long)(10000 * Mathf.RoundToInt(this.auftragsAnsehen));
			break;
		case 5:
			num = (long)(50000 * (this.year - 1975));
			num += (long)(5000 * this.studioPoints);
			num += (long)(5000 * Mathf.RoundToInt(this.auftragsAnsehen));
			break;
		}
		return num;
	}

	
	public long GetKredit()
	{
		return this.kredit;
	}

	
	public int GetKreditZinsen()
	{
		if (this.kredit <= 0L)
		{
			return 0;
		}
		int result = 0;
		switch (this.difficulty)
		{
		case 0:
			result = Mathf.RoundToInt((float)(this.kredit / 180L));
			break;
		case 1:
			result = Mathf.RoundToInt((float)(this.kredit / 160L));
			break;
		case 2:
			result = Mathf.RoundToInt((float)(this.kredit / 140L));
			break;
		case 3:
			result = Mathf.RoundToInt((float)(this.kredit / 120L));
			break;
		case 4:
			result = Mathf.RoundToInt((float)(this.kredit / 100L));
			break;
		case 5:
			result = Mathf.RoundToInt((float)(this.kredit / 80L));
			break;
		}
		return result;
	}

	
	public void CreateFoto(characterScript cSPhoto_, charArbeitsmarkt aSPhoto_)
	{
		if (!this.cameraPersonalPhoto.activeSelf)
		{
			this.cameraPersonalPhoto.SetActive(true);
		}
		if (this.charFoto)
		{
			UnityEngine.Object.Destroy(this.charFoto);
		}
		characterScript characterScript;
		if (cSPhoto_)
		{
			characterScript = this.CreatePlayer(cSPhoto_.male, cSPhoto_.model_body, cSPhoto_.model_eyes, cSPhoto_.model_hair, cSPhoto_.model_beard, cSPhoto_.model_skinColor, cSPhoto_.model_hairColor, cSPhoto_.model_beardColor, cSPhoto_.model_HoseColor, cSPhoto_.model_ShirtColor, cSPhoto_.model_Add1Color);
		}
		else
		{
			characterScript = this.CreatePlayer(aSPhoto_.male, aSPhoto_.model_body, aSPhoto_.model_eyes, aSPhoto_.model_hair, aSPhoto_.model_beard, aSPhoto_.model_skinColor, aSPhoto_.model_hairColor, aSPhoto_.model_beardColor, aSPhoto_.model_HoseColor, aSPhoto_.model_ShirtColor, aSPhoto_.model_Add1Color);
		}
		this.charFoto = characterScript.gameObject.transform.GetChild(0).gameObject;
		this.charFoto.name = "CHARFOTO";
		this.charFoto.transform.SetParent(null);
		this.charFoto.transform.position = new Vector3(0f, 0f, 0f);
		this.charFoto.transform.eulerAngles = new Vector3(0f, 0f, 0f);
		this.SetLayer(4, this.charFoto.transform);
		this.charFoto.GetComponent<Animator>().CrossFade("idle", 0.1f, 0, 0f, 0.4f);
		UnityEngine.Object.Destroy(characterScript.gameObject);
		base.StartCoroutine(this.RemovePhoto());
	}

	
	private IEnumerator RemovePhoto()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		if (this.cameraPersonalPhoto.activeSelf)
		{
			this.cameraPersonalPhoto.SetActive(false);
		}
		if (this.charFoto)
		{
			UnityEngine.Object.Destroy(this.charFoto);
		}
		yield break;
	}

	
	public void DestroyMainMenuObjects()
	{
		base.StartCoroutine(this.DestroyMainMenuObjectsAfterOneFrame());
	}

	
	private IEnumerator DestroyMainMenuObjectsAfterOneFrame()
	{
		yield return new WaitForEndOfFrame();
		for (int i = 0; i < this.mainMenuObjects.Length; i++)
		{
			if (this.mainMenuObjects[i])
			{
				UnityEngine.Object.Destroy(this.mainMenuObjects[i]);
			}
		}
		yield break;
	}

	
	private void UpdateExklusivPublisher()
	{
		if (this.exklusivVertrag_ID != -1)
		{
			this.exklusivVertrag_laufzeit--;
			if (this.exklusivVertrag_laufzeit < 0)
			{
				publisherScript exklusivPublisher = this.GetExklusivPublisher();
				if (exklusivPublisher)
				{
					string text = this.tS_.GetText(1053);
					text = text.Replace("<NAME>", "<color=blue>" + exklusivPublisher.GetName() + "</color>");
					this.guiMain_.OpenMenu(false);
					this.guiMain_.MessageBox(text, true);
				}
				this.RemovePublisherExklusivVertrag();
			}
		}
	}

	
	public void RemovePublisherExklusivVertrag()
	{
		this.exklusivVertrag_ID = -1;
		this.exklusivVertrag_laufzeit = 0;
		this.exkklusivVertragScript_ = null;
	}

	
	public publisherScript GetExklusivPublisher()
	{
		if (this.exklusivVertrag_ID == -1)
		{
			return null;
		}
		if (!this.exkklusivVertragScript_)
		{
			GameObject gameObject = GameObject.Find("PUB_" + this.exklusivVertrag_ID.ToString());
			if (gameObject)
			{
				this.exkklusivVertragScript_ = gameObject.GetComponent<publisherScript>();
			}
		}
		if (this.exkklusivVertragScript_)
		{
			return this.exkklusivVertragScript_;
		}
		return null;
	}

	
	public bool IsMyBuilding(int id_)
	{
		return this.buildings[id_];
	}

	
	public void UpdateGlobalEvent()
	{
		if (this.settings_RandomEventsOff)
		{
			return;
		}
		if (this.year <= 1976)
		{
			return;
		}
		if (this.globalEvent == -1)
		{
			if ((!this.multiplayer || (this.multiplayer && this.mpCalls_.isServer)) && !this.guiMain_.menuOpen)
			{
				if (!this.settings_history)
				{
					if (UnityEngine.Random.Range(0, 100) <= 1)
					{
						this.guiMain_.uiObjects[216].SetActive(true);
						this.guiMain_.uiObjects[216].GetComponent<Menu_RandomEventGlobal>().Init(-1);
						if (this.multiplayer && this.mpCalls_.isServer)
						{
							this.mpCalls_.SERVER_Send_GlobalEvent(this.globalEvent, this.globalEventWeeks);
							this.guiMain_.BUTTON_GameSpeed(0f);
							return;
						}
					}
				}
				else
				{
					this.guiMain_.uiObjects[216].SetActive(true);
					this.guiMain_.uiObjects[216].GetComponent<Menu_RandomEventGlobal>().History();
					if (this.globalEvent != -1 && this.multiplayer && this.mpCalls_.isServer)
					{
						this.mpCalls_.SERVER_Send_GlobalEvent(this.globalEvent, this.globalEventWeeks);
						this.guiMain_.BUTTON_GameSpeed(0f);
						return;
					}
				}
			}
		}
		else
		{
			this.globalEventWeeks--;
			if (this.globalEventWeeks <= 0)
			{
				this.globalEvent = -1;
				this.globalEventWeeks = 0;
			}
		}
	}

	
	public void SetGlobalEvent(int i)
	{
		this.globalEvent = i;
		this.globalEventWeeks = UnityEngine.Random.Range(16, 32);
	}

	
	private void PayBankZinsen()
	{
		if (this.globalEvent == 4)
		{
			this.Pay((long)(this.GetKreditZinsen() * 3), 20);
			return;
		}
		this.Pay((long)this.GetKreditZinsen(), 20);
	}

	
	public void NewMarktforschung()
	{
		this.marktforschung_datum = this.guiMain_.GetDate();
		if (this.unlock_.Get(59))
		{
			this.marktforschung_digtal = this.games_.GetDigitalSells();
			this.marktforschung_retail = 1f - this.marktforschung_digtal;
		}
		else
		{
			this.marktforschung_digtal = 0f;
			this.marktforschung_retail = 1f;
		}
		this.marktforschung_deluxe = this.games_.GetDeluxeCurve();
		this.marktforschung_collectors = this.games_.GetCollectorsCurve();
		this.marktforschung_arcade = this.games_.GetArcadeCurve();
		this.marktforschung_bestPlattform = -1;
		this.marktforschung_bestPlattformKonsole = -1;
		this.marktforschung_bestPlattformHandheld = -1;
		this.marktforschung_bestPlattformHandy = -1;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Platform");
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				platformScript component = array[i].GetComponent<platformScript>();
				if (component.OwnerIsNPC() && component.IsVerfuegbar())
				{
					if (component.typ == 0 && num < component.units_max)
					{
						num = component.units_max;
						this.marktforschung_bestPlattform = component.myID;
					}
					if (component.typ == 1 && num2 < component.units_max)
					{
						num2 = component.units_max;
						this.marktforschung_bestPlattformKonsole = component.myID;
					}
					if (component.typ == 2 && num3 < component.units_max)
					{
						num3 = component.units_max;
						this.marktforschung_bestPlattformHandheld = component.myID;
					}
					if (component.typ == 3 && num4 < component.units_max)
					{
						num4 = component.units_max;
						this.marktforschung_bestPlattformHandy = component.myID;
					}
				}
			}
		}
		this.marktforschung_badPlattform = -1;
		this.marktforschung_badPlattformKonsole = -1;
		this.marktforschung_badPlattformHandheld = -1;
		this.marktforschung_badPlattformHandy = -1;
		num = -1;
		num2 = -1;
		num3 = -1;
		num4 = -1;
		for (int j = 0; j < array.Length; j++)
		{
			if (array[j])
			{
				platformScript component2 = array[j].GetComponent<platformScript>();
				if (component2.OwnerIsNPC() && component2.IsVerfuegbar())
				{
					if (component2.typ == 0 && (num > component2.units_max || num == -1))
					{
						num = component2.units_max;
						this.marktforschung_badPlattform = component2.myID;
					}
					if (component2.typ == 1 && (num2 > component2.units_max || num2 == -1))
					{
						num2 = component2.units_max;
						this.marktforschung_badPlattformKonsole = component2.myID;
					}
					if (component2.typ == 2 && (num3 > component2.units_max || num3 == -1))
					{
						num3 = component2.units_max;
						this.marktforschung_badPlattformHandheld = component2.myID;
					}
					if (component2.typ == 3 && (num4 > component2.units_max || num4 == -1))
					{
						num4 = component2.units_max;
						this.marktforschung_badPlattformHandy = component2.myID;
					}
				}
			}
		}
		this.marktforschung_nextGenre = this.trendNextGenre;
		this.marktforschung_nextTopic = this.trendNextTheme;
		this.marktforschung_nextBadGenre = this.trendNextAntiGenre;
		this.marktforschung_nextBadTopic = this.trendNextAntiTheme;
	}

	
	private void UpdateCars()
	{
		for (int i = 0; i < this.carList.Count; i++)
		{
			if (this.carList[i])
			{
				this.carList[i].transform.Translate(Vector3.forward * this.GetDeltaTime() * 20f);
				if (this.carList[i].transform.position.x < -300f || this.carList[i].transform.position.x > 300f || this.carList[i].transform.position.z < -300f || this.carList[i].transform.position.z > 300f)
				{
					UnityEngine.Object.Destroy(this.carList[i]);
				}
			}
		}
		for (int j = 0; j < this.carList.Count; j++)
		{
			if (!this.carList[j])
			{
				this.carList.RemoveAt(j);
			}
		}
		this.carSpawnTimer += this.GetDeltaTime();
		if (this.carSpawnTimer < 2f)
		{
			return;
		}
		this.carSpawnTimer = 0f;
		GameObject[] array = GameObject.FindGameObjectsWithTag("CarSpawn");
		if (array.Length != 0)
		{
			int num = UnityEngine.Random.Range(0, array.Length);
			if (array[num] && UnityEngine.Random.Range(0, 100) > 50)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.carPrefabs[UnityEngine.Random.Range(0, this.carPrefabs.Length)]);
				gameObject.transform.position = new Vector3(array[num].transform.position.x, -0.54f, array[num].transform.position.z);
				gameObject.transform.eulerAngles = array[num].transform.eulerAngles;
				gameObject.transform.Translate(-Vector3.forward * UnityEngine.Random.Range(0f, 1f));
				this.carList.Add(gameObject);
			}
		}
	}

	
	public string GetSavegameTitle()
	{
		if (!this.multiplayer)
		{
			return "savegame";
		}
		return "mp";
	}

	
	public void SetRandomMultiplayerSaveID()
	{
		this.multiplayerSaveID = UnityEngine.Random.Range(100, 9999999);
	}

	
	public void SendSystemMessage(string c)
	{
		if (this.multiplayer && this.mpCalls_)
		{
			if (this.mpCalls_.isServer)
			{
				this.mpCalls_.SERVER_Send_Chat(this.myID, c);
				return;
			}
			this.mpCalls_.CLIENT_Send_Chat(c);
		}
	}

	
	public void UpdateWeatherEffects()
	{
		if (this.globalLight)
		{
			if (this.weatherEffects[0].activeSelf)
			{
				this.globalLight.color = Color.Lerp(this.globalLight.color, this.globalLightColors[1], 0.01f);
			}
			else
			{
				this.globalLight.color = Color.Lerp(this.globalLight.color, this.globalLightColors[0], 0.01f);
			}
		}
		if (this.settings_.disableWetter)
		{
			for (int i = 0; i < this.weatherEffects.Length; i++)
			{
				if (this.weatherEffects[i] && this.weatherEffects[i].activeSelf)
				{
					this.weatherEffects[i].SetActive(false);
				}
			}
			return;
		}
		if (this.weatherTimer > 0f)
		{
			this.weatherTimer -= this.GetDeltaTime();
			return;
		}
		for (int j = 0; j < this.weatherEffects.Length; j++)
		{
			if (this.weatherEffects[j] && this.weatherEffects[j].activeSelf)
			{
				this.weatherEffects[j].SetActive(false);
				this.weatherTimer = UnityEngine.Random.Range(20f, 40f);
				return;
			}
		}
		if (UnityEngine.Random.Range(0, 5) == 1)
		{
			this.weatherEffects[0].SetActive(true);
			this.weatherTimer = UnityEngine.Random.Range(20f, 40f);
			return;
		}
		if (this.office != 4 && UnityEngine.Random.Range(0, 3) == 1 && (this.month == 11 || this.month == 12 || this.month == 1 || this.month == 2))
		{
			this.weatherEffects[1].SetActive(true);
			this.weatherTimer = UnityEngine.Random.Range(20f, 40f);
			return;
		}
		this.weatherTimer = UnityEngine.Random.Range(20f, 40f);
	}

	
	private void UpdateBankWarning()
	{
		if (this.money < 0L)
		{
			this.bankWarning++;
			if (this.bankWarning >= 19)
			{
				this.bankWarning = 18;
				this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[251]);
				this.guiMain_.OpenMenu(false);
				this.sfx_.PlaySound(57, false);
			}
		}
		else
		{
			this.bankWarning = 0;
		}
		if (this.bankWarning == 6)
		{
			this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[249]);
			this.guiMain_.OpenMenu(false);
			this.sfx_.PlaySound(57, false);
		}
	}

	
	public void CloseMultiplayerView()
	{
		GameObject.Find("ROOMS").transform.position = new Vector3(0f, 0f, 0f);
		this.guiMain_.uiObjects[164].SetActive(true);
		for (int i = 0; i < this.mapScript_.ROOMS_MP.transform.childCount; i++)
		{
			Transform child = this.mapScript_.ROOMS_MP.transform.GetChild(i);
			if (child)
			{
				UnityEngine.Object.Destroy(child.gameObject);
			}
		}
		for (int j = 0; j < this.arrayCharacters.Length; j++)
		{
			if (this.arrayCharacters[j])
			{
				this.arrayCharacters[j].transform.localScale = new Vector3(1f, 1f, 1f);
			}
		}
		for (int k = 0; k < this.arrayObjects.Length; k++)
		{
			if (this.arrayObjects[k])
			{
				this.arrayObjects[k].transform.localScale = new Vector3(1f, 1f, 1f);
			}
		}
		for (int l = 0; l < this.arrayRobots.Length; l++)
		{
			if (this.arrayRobots[l])
			{
				this.arrayRobots[l].transform.localScale = new Vector3(1f, 1f, 1f);
			}
		}
		for (int m = 0; m < this.arrayMuell.Length; m++)
		{
			if (this.arrayMuell[m])
			{
				this.arrayMuell[m].transform.localScale = new Vector3(1f, 1f, 1f);
			}
		}
	}

	
	public void ShowMultiplayerView(int slot)
	{
		if (slot > this.mpCalls_.playersMP.Count)
		{
			return;
		}
		int playerID = this.mpCalls_.playersMP[slot].playerID;
		if (playerID == this.myID)
		{
			return;
		}
		if (this.guiMain_.uiObjects[252].activeSelf)
		{
			return;
		}
		this.guiMain_.OpenMenu(true);
		this.guiMain_.uiObjects[252].SetActive(true);
		this.guiMain_.uiObjects[252].GetComponent<Menu_MultiplayerView>().Init(playerID);
		GameObject.Find("ROOMS").transform.position = new Vector3(0f, 1000f, 0f);
		this.mapScript_.CreateWalls_Multiplayer(playerID);
		this.guiMain_.uiObjects[164].SetActive(false);
		for (int i = 0; i < this.arrayCharacters.Length; i++)
		{
			if (this.arrayCharacters[i])
			{
				this.arrayCharacters[i].transform.localScale = new Vector3(0f, 0f, 0f);
			}
		}
		for (int j = 0; j < this.arrayObjects.Length; j++)
		{
			if (this.arrayObjects[j])
			{
				this.arrayObjects[j].transform.localScale = new Vector3(0f, 0f, 0f);
			}
		}
		for (int k = 0; k < this.arrayRobots.Length; k++)
		{
			if (this.arrayRobots[k])
			{
				this.arrayRobots[k].transform.localScale = new Vector3(0f, 0f, 0f);
			}
		}
		for (int l = 0; l < this.arrayMuell.Length; l++)
		{
			if (this.arrayMuell[l])
			{
				this.arrayMuell[l].transform.localScale = new Vector3(0f, 0f, 0f);
			}
		}
		player_mp player_mp = this.mpCalls_.FindPlayer(playerID);
		if (player_mp != null)
		{
			for (int m = 0; m < player_mp.objects.Count; m++)
			{
				this.CreateMultiplayerObject(player_mp.objects[m].id, player_mp.objects[m].typ, player_mp.objects[m].posX, player_mp.objects[m].posY, player_mp.objects[m].rotation);
			}
		}
	}

	
	public void Multiplayer_SendMap(int x, int y)
	{
		if (this.multiplayer)
		{
			if (this.mpCalls_.isClient)
			{
				this.mpCalls_.CLIENT_Send_Map(x, y);
				return;
			}
			this.mpCalls_.SERVER_Send_Map(x, y);
		}
	}

	
	public void Multiplayer_SendObject(int id, int typ, float posX, float posY, float rot)
	{
		if (this.multiplayer)
		{
			if (this.mpCalls_.isClient)
			{
				this.mpCalls_.CLIENT_Send_Object(id, typ, posX, posY, rot);
				return;
			}
			this.mpCalls_.SERVER_Send_Object(id, typ, posX, posY, rot);
		}
	}

	
	public void Multiplayer_SendObjectDelete(int id)
	{
		if (this.multiplayer)
		{
			if (this.save_ && this.save_.loadingSavegame)
			{
				return;
			}
			if (!this.guiMain_)
			{
				return;
			}
			if (this.guiMain_.uiObjects[202].activeSelf)
			{
				return;
			}
			if (this.guiMain_.uiObjects[238].activeSelf)
			{
				return;
			}
			if (this.guiMain_.uiObjects[152].activeSelf)
			{
				return;
			}
			if (this.mpCalls_.isClient)
			{
				this.mpCalls_.CLIENT_Send_ObjectDelete(id);
				return;
			}
			this.mpCalls_.SERVER_Send_ObjectDelete(id);
		}
	}

	
	public void CreateMultiplayerObject(int id, int typ, float posX, float posY, float rot)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mapScript_.prefabsInventar[typ]);
		gameObject.transform.position = new Vector3(posX, 0f, posY);
		gameObject.transform.eulerAngles = new Vector3(0f, rot, 0f);
		objectScript component = gameObject.GetComponent<objectScript>();
		component.multiplayerObject = true;
		if (component.footprint)
		{
			UnityEngine.Object.Destroy(component.footprint);
		}
		gameObject.transform.GetChild(0).transform.parent = this.mapScript_.ROOMS_MP.transform;
		UnityEngine.Object.Destroy(gameObject);
	}

	
	public void AddColliderLayer(Transform go)
	{
		this.listColliderLayer.Add(go);
	}

	
	public void ResetAllColliderLayer()
	{
		for (int i = 0; i < this.listColliderLayer.Count; i++)
		{
			if (this.listColliderLayer[i])
			{
				this.SetLayer(0, this.listColliderLayer[i]);
			}
		}
		this.listColliderLayer.Clear();
	}

	
	public void AddStudioPoints(int i)
	{
		int studioLevel = this.GetStudioLevel(this.studioPoints);
		this.studioPoints += i;
		int studioLevel2 = this.GetStudioLevel(this.studioPoints);
		if (studioLevel < studioLevel2)
		{
			this.guiMain_.CreateTopNewsStudiobewertung(this.tS_.GetStudioBewertung(studioLevel2));
		}
		if (studioLevel2 == 10 && this.difficulty >= 5)
		{
			this.achScript_.SetAchivement(58);
		}
	}

	
	public int GetStudioLevel(int points)
	{
		int result = 0;
		if (points > 30)
		{
			result = 1;
		}
		if (points > 150)
		{
			result = 2;
		}
		if (points > 300)
		{
			result = 3;
		}
		if (points > 600)
		{
			result = 4;
		}
		if (points > 800)
		{
			result = 5;
		}
		if (points > 1000)
		{
			result = 6;
		}
		if (points > 1500)
		{
			result = 7;
		}
		if (points > 2000)
		{
			result = 8;
		}
		if (points > 3000)
		{
			result = 9;
		}
		if (points > 4000)
		{
			result = 10;
		}
		return result;
	}

	
	public bool Muttersprache(int i)
	{
		int countryID = this.GetCountryID();
		switch (i)
		{
		case 0:
			if (countryID == 0 || countryID == 2 || countryID == 13 || countryID == 7 || countryID == 40)
			{
				return true;
			}
			break;
		case 1:
			if (countryID == 5 || countryID == 14 || countryID == 16)
			{
				return true;
			}
			break;
		case 2:
			if (countryID == 6)
			{
				return true;
			}
			break;
		case 3:
			if (countryID == 10)
			{
				return true;
			}
			break;
		case 4:
			if (countryID == 9 || countryID == 22)
			{
				return true;
			}
			break;
		case 5:
			if (countryID == 11)
			{
				return true;
			}
			break;
		case 6:
			if (countryID == 12)
			{
				return true;
			}
			break;
		case 7:
			if (countryID == 8)
			{
				return true;
			}
			break;
		case 8:
			if (countryID == 3)
			{
				return true;
			}
			break;
		case 9:
			if (countryID == 4)
			{
				return true;
			}
			break;
		case 10:
			if (countryID == 1)
			{
				return true;
			}
			break;
		}
		return false;
	}

	
	public bool NotEnoughMoney(int wantToPay)
	{
		return this.money + this.GetKreditlimit() < (long)wantToPay;
	}

	
	public void Multiplayer_SendDataAfterGameStart()
	{
	}

	
	public GameObject CreateMuell(int id, int gfx)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.miscGamePrefabs[id]);
		muellScript component = gameObject.GetComponent<muellScript>();
		component.myGFXSlot = gfx;
		component.mS_ = this;
		component.main_ = base.gameObject;
		this.findMuell = true;
		return gameObject;
	}

	
	public int GetAchivementBonus(int id_)
	{
		return this.amountAchivementsBonus[id_];
	}

	
	public void UpdateAchivementBonus()
	{
		for (int i = 0; i < this.amountAchivementsBonus.Length; i++)
		{
			this.amountAchivementsBonus[i] = 0;
		}
		for (int j = 0; j < this.achivementsBonus.Length; j++)
		{
			if (!this.achivementsDisabled[j] && this.achivements[j])
			{
				this.amountAchivementsBonus[this.achivementsBonus[j]]++;
			}
		}
	}

	
	public int GetAmountContracts(int contractTyp_)
	{
		switch (contractTyp_)
		{
		case 0:
			return this.contractWorkMain_.anzProduction;
		case 1:
			return this.contractWorkMain_.anzQA;
		case 2:
			return this.contractWorkMain_.anzGFX;
		case 3:
			return this.contractWorkMain_.anzSFX;
		case 4:
			return this.contractWorkMain_.anzMotion;
		case 5:
			return this.contractWorkMain_.anzWerkstatt;
		case 6:
			return this.contractWorkMain_.anzHardware;
		case 7:
			return this.contractWorkMain_.anzDEV;
		default:
			return 0;
		}
	}

	
	public void FindMyPublisherScript()
	{
		if (!this.myPubS_)
		{
			GameObject gameObject = GameObject.Find("PUB_" + this.myID.ToString());
			if (gameObject)
			{
				this.myPubS_ = gameObject.GetComponent<publisherScript>();
			}
		}
	}

	
	public string GetCompanyName()
	{
		if (!this.myPubS_)
		{
			this.FindMyPublisherScript();
		}
		if (this.myPubS_)
		{
			return this.myPubS_.GetName();
		}
		return "";
	}

	
	public void SetCompanyName(string c)
	{
		if (!this.myPubS_)
		{
			this.FindMyPublisherScript();
		}
		if (this.myPubS_)
		{
			this.myPubS_.name_EN = c;
			if (this.multiplayer)
			{
				this.FindMyPublisherScript();
				if (this.mpCalls_.isClient)
				{
					this.mpCalls_.CLIENT_Send_Publisher(this.myPubS_);
				}
				if (this.mpCalls_.isServer)
				{
					this.mpCalls_.SERVER_Send_Publisher(this.myPubS_);
				}
			}
		}
	}

	
	public int GetCompanyLogoID()
	{
		if (!this.myPubS_)
		{
			this.FindMyPublisherScript();
		}
		if (this.myPubS_)
		{
			return this.myPubS_.logoID;
		}
		return 0;
	}

	
	public void SetCompanyLogoID(int i)
	{
		if (!this.myPubS_)
		{
			this.FindMyPublisherScript();
		}
		if (this.myPubS_)
		{
			this.myPubS_.logoID = i;
			if (this.multiplayer)
			{
				this.FindMyPublisherScript();
				if (this.mpCalls_.isClient)
				{
					this.mpCalls_.CLIENT_Send_Publisher(this.myPubS_);
				}
				if (this.mpCalls_.isServer)
				{
					this.mpCalls_.SERVER_Send_Publisher(this.myPubS_);
				}
			}
		}
	}

	
	public int GetAwards(int i)
	{
		if (!this.myPubS_)
		{
			this.FindMyPublisherScript();
		}
		if (this.myPubS_)
		{
			return this.myPubS_.awards[i];
		}
		return 0;
	}

	
	public void AddAwards(int i, publisherScript script_)
	{
		if (!script_)
		{
			return;
		}
		script_.awards[i]++;
		if (this.multiplayer)
		{
			this.FindMyPublisherScript();
			if (this.mpCalls_.isClient)
			{
				this.mpCalls_.CLIENT_Send_Publisher(script_);
			}
			if (this.mpCalls_.isServer)
			{
				this.mpCalls_.SERVER_Send_Publisher(script_);
			}
		}
	}

	
	public int GetCountryID()
	{
		if (!this.myPubS_)
		{
			this.FindMyPublisherScript();
		}
		if (this.myPubS_)
		{
			return this.myPubS_.country;
		}
		return 0;
	}

	
	public void SetCountryID(int i)
	{
		if (!this.myPubS_)
		{
			this.FindMyPublisherScript();
		}
		if (this.myPubS_)
		{
			this.myPubS_.country = i;
			if (this.multiplayer)
			{
				this.FindMyPublisherScript();
				if (this.mpCalls_.isClient)
				{
					this.mpCalls_.CLIENT_Send_Publisher(this.myPubS_);
				}
				if (this.mpCalls_.isServer)
				{
					this.mpCalls_.SERVER_Send_Publisher(this.myPubS_);
				}
			}
		}
	}

	
	public int GetFanGenreID()
	{
		if (!this.myPubS_)
		{
			this.FindMyPublisherScript();
		}
		if (this.myPubS_)
		{
			return this.myPubS_.fanGenre;
		}
		return 0;
	}

	
	public void SetFanGenreID(int i)
	{
		if (!this.myPubS_)
		{
			this.FindMyPublisherScript();
		}
		if (this.myPubS_)
		{
			this.myPubS_.fanGenre = i;
			if (this.multiplayer)
			{
				this.FindMyPublisherScript();
				if (this.mpCalls_.isClient)
				{
					this.mpCalls_.CLIENT_Send_Publisher(this.myPubS_);
				}
				if (this.mpCalls_.isServer)
				{
					this.mpCalls_.SERVER_Send_Publisher(this.myPubS_);
				}
			}
		}
	}

	
	public publisherScript CreatePlayerPublisher(int id_)
	{
		GameObject gameObject = GameObject.Find("PUB_" + id_.ToString());
		if (gameObject)
		{
			return gameObject.GetComponent<publisherScript>();
		}
		publisherScript publisherScript = this.publisher_.CreatePublisher();
		publisherScript.myID = id_;
		publisherScript.Init();
		publisherScript.isPlayer = true;
		publisherScript.isUnlocked = true;
		publisherScript.date_year = 1976;
		publisherScript.date_month = 1;
		publisherScript.stars = 1f;
		publisherScript.developer = true;
		publisherScript.publisher = true;
		publisherScript.relation = 0f;
		publisherScript.share = 1f;
		publisherScript.notForSale = true;
		return publisherScript;
	}

	
	public string buildVersion = "BUILD 2020.08.22A";

	
	public float[] gameSpeeds;

	
	public GameObject[] mainMenuObjects;

	
	public GameObject[] weatherEffects;

	
	public Light globalLight;

	
	public Color[] globalLightColors;

	
	public int exklusivVertrag_ID = -1;

	
	public int exklusivVertrag_laufzeit;

	
	private publisherScript exkklusivVertragScript_;

	
	public float record_Gameplay;

	
	public float record_Grafik;

	
	public float record_Sound;

	
	public float record_Technik;

	
	public float auftragsAnsehen;

	
	public int studioPoints;

	
	public int myID = 100000;

	
	public publisherScript myPubS_;

	
	public string playerName = "";

	
	public int bankWarning;

	
	public bool multiplayer;

	
	public bool mpLobbyOpen;

	
	public int multiplayerSaveID;

	
	public int office;

	
	public int savegameVersion;

	
	public int year = 1976;

	
	public int month = 1;

	
	public int week = 1;

	
	public int difficulty;

	
	public int globalEvent = -1;

	
	public int globalEventWeeks;

	
	public int trendGenre;

	
	public int trendAntiGenre = 1;

	
	public int trendTheme;

	
	public int trendAntiTheme = 1;

	
	public int trendWeeks = 20;

	
	public int trendNextGenre;

	
	public int trendNextAntiGenre;

	
	public int trendNextTheme;

	
	public int trendNextAntiTheme;

	
	public long money = 200000L;

	
	public long kredit;

	
	public bool[] buildings;

	
	public int anrufe;

	
	public bool lastGameCommercialFlop;

	
	public float dayTimer;

	
	public float gameSpeed = 1f;

	
	private float gameSpeedOrg;

	
	private bool pauseGame;

	
	public int anzKonkurrenten;

	
	public float speedSetting = 0.05f;

	
	public int goldeneSchallplatten;

	
	public int platinSchallplatten;

	
	public int diamantSchallplatten;

	
	public int award_GOTY;

	
	public int award_Studio;

	
	public int award_Grafik;

	
	public int award_Sound;

	
	public int award_Trendsetter;

	
	public int award_Publisher;

	
	public int pubOffersAmount;

	
	public int lastUsedEngine;

	
	public GameObject charFoto;

	
	public GameObject guiPops;

	
	public int personal_druck = 1;

	
	public int personal_pausen = 1;

	
	public int personal_motivation = 40;

	
	public int personal_crunch = 90;

	
	public bool personal_dontLeaveBuilding;

	
	public bool personal_RobotDontLeaveBuilding;

	
	public bool personal_ki;

	
	public string[] personal_group_names = new string[12];

	
	public long[] fanshopverlauf = new long[24];

	
	public long[] finanzVerlauf = new long[24];

	
	public long[] verkaufsverlauf = new long[24];

	
	public long[] verkaufsverlaufKonsolen = new long[24];

	
	public long[] aboverlauf = new long[24];

	
	public long[] downloadverlauf = new long[24];

	
	public long[] fansverlauf = new long[24];

	
	public long[] finanzenMonat = new long[100];

	
	public long[] finanzenMonatLast = new long[100];

	
	public long[] finanzenJahr = new long[100];

	
	public long[] finanzenJahrLast = new long[100];

	
	public List<long> finanzVerlaufEinnahmen = new List<long>();

	
	public List<long> finanzVerlaufAusgaben = new List<long>();

	
	public List<string> history = new List<string>();

	
	public List<int> madGamesCon_Jahr = new List<int>();

	
	public List<int> madGamesCon_BestGrafik = new List<int>();

	
	public List<int> madGamesCon_BestSound = new List<int>();

	
	public List<int> madGamesCon_BestStudio = new List<int>();

	
	public List<int> madGamesCon_BestPublisher = new List<int>();

	
	public List<int> madGamesCon_BestGame = new List<int>();

	
	public List<int> madGamesCon_BadGame = new List<int>();

	
	public bool[] devLegendsInUse;

	
	public bool[] devLegendsFemale;

	
	public bool[] devLegendsDesigner;

	
	public bool[] devLegendsProgrammierer;

	
	public bool[] devLegendsGrafiker;

	
	public bool[] devLegendsMusiker;

	
	public bool[] devLegendsForscher;

	
	public bool[] devLegendsHardware;

	
	public bool[] newsSetting;

	
	public bool[] gameTabFilter;

	
	public int[] lastGamesGenre;

	
	public int gelangweiltGenre;

	
	public int sauerBugs;

	
	public int awardBonus;

	
	public float awardBonusAmount;

	
	public bool[] achivements;

	
	public bool[] achivementsDisabled;

	
	public int[] achivementsBonus;

	
	public int[] amountAchivementsBonus;

	
	public string marktforschung_datum = "";

	
	public float marktforschung_digtal = -1f;

	
	public float marktforschung_retail = -1f;

	
	public float marktforschung_deluxe = -1f;

	
	public float marktforschung_collectors = -1f;

	
	public float marktforschung_arcade = -1f;

	
	public int marktforschung_bestPlattform = -1;

	
	public int marktforschung_badPlattform = -1;

	
	public int marktforschung_bestPlattformKonsole = -1;

	
	public int marktforschung_badPlattformKonsole = -1;

	
	public int marktforschung_bestPlattformHandheld = -1;

	
	public int marktforschung_badPlattformHandheld = -1;

	
	public int marktforschung_bestPlattformHandy = -1;

	
	public int marktforschung_badPlattformHandy = -1;

	
	public int marktforschung_nextGenre = -1;

	
	public int marktforschung_nextTopic = -1;

	
	public int marktforschung_nextBadGenre = -1;

	
	public int marktforschung_nextBadTopic = -1;

	
	public bool automatic_RemoveGameFormMarket;

	
	public bool settings_TutorialOff = true;

	
	public bool settings_RandomEventsOff = true;

	
	public bool settings_autoPauseForMultiplayer;

	
	public bool settings_RandomReviews;

	
	public bool settings_arbeitsgeschwindigkeitAnpassen;

	
	public bool settings_history;

	
	public bool settings_plattformEnd;

	
	public bool badGameThisYear;

	
	public bool sellLagerbestandAutomatic;

	
	public textScript tS_;

	
	public settingsScript settings_;

	
	public mapScript mapScript_;

	
	public unlockScript unlock_;

	
	private pickCharacterScript pickChar_;

	
	private pickObjectScript pickObject_;

	
	public genres genres_;

	
	private themes themes_;

	
	private engineFeatures eF_;

	
	public gameplayFeatures gF_;

	
	public hardware hardware_;

	
	public hardwareFeatures hardwareFeatures_;

	
	private arbeitsmarkt arbeitsmarkt_;

	
	public platforms platforms_;

	
	public copyProtect copyProtect_;

	
	public anitCheat antiCheat_;

	
	public licences licences_;

	
	public games games_;

	
	public GUI_Main guiMain_;

	
	private npcEngines npcEngines_;

	
	private publisher publisher_;

	
	private createCharScript cCS_;

	
	public contractWorkMain contractWorkMain_;

	
	public publishingOfferMain publishingOfferMain_;

	
	private savegameScript save_;

	
	public cameraMovementScript cmS_;

	
	public sfxScript sfx_;

	
	public NetworkManager manager;

	
	public mpCalls mpCalls_;

	
	public achiementScript achScript_;

	
	public reviewText reviewText_;

	
	public forschungSonstiges forschungSonstiges_;

	
	public roomDataScript rdS_;

	
	public GameObject[] arrayCharacters;

	
	public List<GameObject> arrayCharactersForDoors = new List<GameObject>();

	
	public GameObject[] arrayObjects;

	
	public GameObject[] arrayRooms;

	
	public GameObject[] arrayRobots;

	
	public GameObject[] arrayMuell;

	
	public objectScript[] arrayObjectScripts;

	
	public characterScript[] arrayCharactersScripts;

	
	public GameObject[] miscParticlePrefabs;

	
	public GameObject[] miscGamePrefabs;

	
	public Material[] specialMaterials;

	
	public Material[] floorMaterials;

	
	public Texture2D[] specialTextures;

	
	public Shader[] shaders;

	
	public float objectRotation;

	
	public GameObject pickedObject;

	
	public List<GameObject> pickedChars = new List<GameObject>();

	
	public bool snapObject;

	
	public bool snapRotation;

	
	public GameObject cameraPersonalPhoto;

	
	public VectorLine roomLine;

	
	public float weatherTimer;

	
	public int anzSprechblasen;

	
	public LayerMask layerMask_Floor;

	
	public const int taskID_taskForschung = 10;

	
	public const int taskID_taskEngine = 20;

	
	public const int taskID_taskUpdate = 30;

	
	public const int taskID_taskGame = 40;

	
	public const int taskID_taskF2PUpdate = 50;

	
	public const int taskID_taskMarketing = 60;

	
	public const int taskID_taskMarketingSpezial = 70;

	
	public const int taskID_taskMitarbeitersuche = 80;

	
	public const int taskID_taskTraining = 90;

	
	public const int taskID_taskSpielbericht = 100;

	
	public const int taskID_taskGameplayVerbessern = 110;

	
	public const int taskID_taskBugfixing = 120;

	
	public const int taskID_taskGrafikVerbessern = 130;

	
	public const int taskID_taskSoundVerbessern = 140;

	
	public const int taskID_taskAnimationVerbessern = 150;

	
	public const int taskID_taskProduction = 160;

	
	public const int taskID_taskArcadeProduction = 170;

	
	public const int taskID_taskKonsole = 180;

	
	public const int taskID_taskFankampagne = 190;

	
	public const int taskID_taskSupport = 200;

	
	public const int taskID_taskContractWork = 210;

	
	public const int taskID_taskContractWait = 220;

	
	public const int taskID_taskMarktforschung = 230;

	
	public const int taskID_taskPolishing = 240;

	
	public const int taskID_taskUnterstuetzen = 250;

	
	public const int taskID_taskWait = 260;

	
	public const int taskID_taskFanshop = 270;

	
	public bool findObjects = true;

	
	public bool findRooms = true;

	
	public bool findCharacters = true;

	
	public bool findMuell = true;

	
	public bool findRobots = true;

	
	public bool officeLoaded;

	
	private bool contendIsLoaded;

	
	private float updateKiTimer;

	
	private float updateUnkorrekterRoom;

	
	public int autoSaveInterval = -1;

	
	public float lauf = 0.2f;

	
	private float filterTimer;

	
	public float carSpawnTimer;

	
	public List<GameObject> carList = new List<GameObject>();

	
	public GameObject[] carPrefabs;

	
	public List<Transform> listColliderLayer = new List<Transform>();
}
