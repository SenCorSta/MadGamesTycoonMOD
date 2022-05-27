using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vectrosity;

// Token: 0x0200032C RID: 812
public class mainScript : MonoBehaviour
{
	// Token: 0x06001CD7 RID: 7383 RVA: 0x0011EAC3 File Offset: 0x0011CCC3
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001CD8 RID: 7384 RVA: 0x0011EACC File Offset: 0x0011CCCC
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

	// Token: 0x06001CD9 RID: 7385 RVA: 0x0011EECC File Offset: 0x0011D0CC
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

	// Token: 0x06001CDA RID: 7386 RVA: 0x0011EF70 File Offset: 0x0011D170
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

	// Token: 0x06001CDB RID: 7387 RVA: 0x0011F054 File Offset: 0x0011D254
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

	// Token: 0x06001CDC RID: 7388 RVA: 0x0011F098 File Offset: 0x0011D298
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

	// Token: 0x06001CDD RID: 7389 RVA: 0x0011F0D0 File Offset: 0x0011D2D0
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

	// Token: 0x06001CDE RID: 7390 RVA: 0x0011F153 File Offset: 0x0011D353
	private IEnumerator iInitScene(bool fromSavegame)
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		this.mapScript_.InitBuilding(fromSavegame);
		GameObject.Find("CamMovement").GetComponent<cameraMovementScript>().FindCameraLimits();
		this.officeLoaded = true;
		yield break;
	}

	// Token: 0x06001CDF RID: 7391 RVA: 0x0011F169 File Offset: 0x0011D369
	private void Cheat()
	{
		if (Input.GetKeyDown(KeyCode.C))
		{
			this.guiMain_.uiObjects[216].SetActive(true);
			this.guiMain_.uiObjects[216].GetComponent<Menu_RandomEventGlobal>().Init(13);
		}
	}

	// Token: 0x06001CE0 RID: 7392 RVA: 0x0011F1A8 File Offset: 0x0011D3A8
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

	// Token: 0x06001CE1 RID: 7393 RVA: 0x0011F296 File Offset: 0x0011D496
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

	// Token: 0x06001CE2 RID: 7394 RVA: 0x0011F2D0 File Offset: 0x0011D4D0
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

	// Token: 0x06001CE3 RID: 7395 RVA: 0x0011F4D4 File Offset: 0x0011D6D4
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

	// Token: 0x06001CE4 RID: 7396 RVA: 0x0011F53C File Offset: 0x0011D73C
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

	// Token: 0x06001CE5 RID: 7397 RVA: 0x0011F6C4 File Offset: 0x0011D8C4
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

	// Token: 0x06001CE6 RID: 7398 RVA: 0x0011F798 File Offset: 0x0011D998
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

	// Token: 0x06001CE7 RID: 7399 RVA: 0x0011F857 File Offset: 0x0011DA57
	private void FindRobots()
	{
		this.arrayRobots = GameObject.FindGameObjectsWithTag("Robot");
	}

	// Token: 0x06001CE8 RID: 7400 RVA: 0x0011F869 File Offset: 0x0011DA69
	private void FindMuell()
	{
		this.arrayMuell = GameObject.FindGameObjectsWithTag("Muell");
	}

	// Token: 0x06001CE9 RID: 7401 RVA: 0x0011F87C File Offset: 0x0011DA7C
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

	// Token: 0x06001CEA RID: 7402 RVA: 0x0011F8E3 File Offset: 0x0011DAE3
	public void FindRooms()
	{
		this.arrayRooms = GameObject.FindGameObjectsWithTag("Room");
	}

	// Token: 0x06001CEB RID: 7403 RVA: 0x0011F8F8 File Offset: 0x0011DAF8
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

	// Token: 0x06001CEC RID: 7404 RVA: 0x0011FB25 File Offset: 0x0011DD25
	public bool IsForcedPause()
	{
		return this.pauseGame;
	}

	// Token: 0x06001CED RID: 7405 RVA: 0x0011FB30 File Offset: 0x0011DD30
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

	// Token: 0x06001CEE RID: 7406 RVA: 0x0011FBC4 File Offset: 0x0011DDC4
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

	// Token: 0x06001CEF RID: 7407 RVA: 0x0011FC50 File Offset: 0x0011DE50
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

	// Token: 0x06001CF0 RID: 7408 RVA: 0x0011FE3C File Offset: 0x0011E03C
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

	// Token: 0x06001CF1 RID: 7409 RVA: 0x00120004 File Offset: 0x0011E204
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

	// Token: 0x06001CF2 RID: 7410 RVA: 0x0012004C File Offset: 0x0011E24C
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

	// Token: 0x06001CF3 RID: 7411 RVA: 0x0012012D File Offset: 0x0011E32D
	public void AutoSaveMultiplayer()
	{
		if (this.multiplayer)
		{
			this.save_.SaveMultiplayer(0);
			this.guiMain_.ShowGameHasSaved();
		}
	}

	// Token: 0x06001CF4 RID: 7412 RVA: 0x00120150 File Offset: 0x0011E350
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

	// Token: 0x06001CF5 RID: 7413 RVA: 0x001201E8 File Offset: 0x0011E3E8
	private void MadGamesConvention()
	{
		if (this.month == 7 && this.week == 1)
		{
			this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[185]);
			this.guiMain_.uiObjects[185].GetComponent<Menu_Messe>().Init();
		}
	}

	// Token: 0x06001CF6 RID: 7414 RVA: 0x00120240 File Offset: 0x0011E440
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

	// Token: 0x06001CF7 RID: 7415 RVA: 0x00120308 File Offset: 0x0011E508
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

	// Token: 0x06001CF8 RID: 7416 RVA: 0x0012039C File Offset: 0x0011E59C
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

	// Token: 0x06001CF9 RID: 7417 RVA: 0x001203E0 File Offset: 0x0011E5E0
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

	// Token: 0x06001CFA RID: 7418 RVA: 0x00120514 File Offset: 0x0011E714
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

	// Token: 0x06001CFB RID: 7419 RVA: 0x0012055C File Offset: 0x0011E75C
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

	// Token: 0x06001CFC RID: 7420 RVA: 0x001205A4 File Offset: 0x0011E7A4
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

	// Token: 0x06001CFD RID: 7421 RVA: 0x001205EC File Offset: 0x0011E7EC
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

	// Token: 0x06001CFE RID: 7422 RVA: 0x00120634 File Offset: 0x0011E834
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

	// Token: 0x06001CFF RID: 7423 RVA: 0x0012068C File Offset: 0x0011E88C
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

	// Token: 0x06001D00 RID: 7424 RVA: 0x0012071C File Offset: 0x0011E91C
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

	// Token: 0x06001D01 RID: 7425 RVA: 0x00120788 File Offset: 0x0011E988
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

	// Token: 0x06001D02 RID: 7426 RVA: 0x00120C7C File Offset: 0x0011EE7C
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

	// Token: 0x06001D03 RID: 7427 RVA: 0x00120F74 File Offset: 0x0011F174
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

	// Token: 0x06001D04 RID: 7428 RVA: 0x00121040 File Offset: 0x0011F240
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

	// Token: 0x06001D05 RID: 7429 RVA: 0x0012108C File Offset: 0x0011F28C
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

	// Token: 0x06001D06 RID: 7430 RVA: 0x001210FC File Offset: 0x0011F2FC
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

	// Token: 0x06001D07 RID: 7431 RVA: 0x0012114C File Offset: 0x0011F34C
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

	// Token: 0x06001D08 RID: 7432 RVA: 0x00121330 File Offset: 0x0011F530
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

	// Token: 0x06001D09 RID: 7433 RVA: 0x001213A5 File Offset: 0x0011F5A5
	public void UpdatePathfindingNextFrameExtern()
	{
		base.StartCoroutine(this.UpdatePathfindingNextFrame());
	}

	// Token: 0x06001D0A RID: 7434 RVA: 0x001213B4 File Offset: 0x0011F5B4
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

	// Token: 0x06001D0B RID: 7435 RVA: 0x001213C3 File Offset: 0x0011F5C3
	public void DisableSelectLayer(GameObject go)
	{
		base.StartCoroutine(this.iDisableSelectLayer(go));
	}

	// Token: 0x06001D0C RID: 7436 RVA: 0x001213D3 File Offset: 0x0011F5D3
	private IEnumerator iDisableSelectLayer(GameObject go)
	{
		yield return new WaitForEndOfFrame();
		if (go && go.transform.GetChild(0))
		{
			this.SetLayer(0, go.transform.GetChild(0));
		}
		yield break;
	}

	// Token: 0x06001D0D RID: 7437 RVA: 0x001213EC File Offset: 0x0011F5EC
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

	// Token: 0x06001D0E RID: 7438 RVA: 0x00121468 File Offset: 0x0011F668
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

	// Token: 0x06001D0F RID: 7439 RVA: 0x001214C7 File Offset: 0x0011F6C7
	public void AddPickedChar(GameObject go)
	{
		this.pickedChars.Add(go);
	}

	// Token: 0x06001D10 RID: 7440 RVA: 0x001214D5 File Offset: 0x0011F6D5
	public void SetGameSpeed(float f)
	{
		this.gameSpeed = f;
	}

	// Token: 0x06001D11 RID: 7441 RVA: 0x001214E0 File Offset: 0x0011F6E0
	public float GetGameSpeed()
	{
		float result = this.gameSpeed;
		if (this.multiplayer && this.settings_autoPauseForMultiplayer && this.mpCalls_.AutoPause())
		{
			result = 0f;
		}
		return result;
	}

	// Token: 0x06001D12 RID: 7442 RVA: 0x00121518 File Offset: 0x0011F718
	public float GetDeltaTime()
	{
		return Time.deltaTime * this.GetGameSpeed();
	}

	// Token: 0x06001D13 RID: 7443 RVA: 0x00121528 File Offset: 0x0011F728
	public float Round(float value, int digits)
	{
		float num = Mathf.Pow(10f, (float)digits);
		return Mathf.Round(value * num) / num;
	}

	// Token: 0x06001D14 RID: 7444 RVA: 0x0012154C File Offset: 0x0011F74C
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

	// Token: 0x06001D15 RID: 7445 RVA: 0x001215FC File Offset: 0x0011F7FC
	private float GetFloatMax(string stringValue)
	{
		float num = 1f;
		float.TryParse(stringValue, out num);
		return -num;
	}

	// Token: 0x06001D16 RID: 7446 RVA: 0x0012161C File Offset: 0x0011F81C
	private float GetFloatMin(string stringValue)
	{
		float result = 1f;
		float.TryParse(stringValue, out result);
		return result;
	}

	// Token: 0x06001D17 RID: 7447 RVA: 0x0012163C File Offset: 0x0011F83C
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

	// Token: 0x06001D18 RID: 7448 RVA: 0x001216D8 File Offset: 0x0011F8D8
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

	// Token: 0x06001D19 RID: 7449 RVA: 0x0002A86C File Offset: 0x00028A6C
	public int GetNewID()
	{
		return UnityEngine.Random.Range(1, 2000000000);
	}

	// Token: 0x06001D1A RID: 7450 RVA: 0x0012171C File Offset: 0x0011F91C
	private void UpdateSpecialMaterials()
	{
		float x = Mathf.Cos(Time.time * this.GetGameSpeed()) * 0.5f + 1f;
		float y = Mathf.Sin(Time.time * this.GetGameSpeed()) * 0.5f + 1f;
		this.specialMaterials[0].mainTextureScale = new Vector2(x, y);
		this.specialMaterials[1].mainTextureOffset = new Vector2(Time.time * (this.GetGameSpeed() * 0.3f), 0f);
		this.specialMaterials[2].mainTextureOffset = new Vector2(0f, -Time.time * (this.GetGameSpeed() * 0.26f));
		this.specialMaterials[3].mainTextureOffset = new Vector2(0f, -Time.time * (this.GetGameSpeed() * 1.5f));
	}

	// Token: 0x06001D1B RID: 7451 RVA: 0x001217F8 File Offset: 0x0011F9F8
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

	// Token: 0x06001D1C RID: 7452 RVA: 0x00121914 File Offset: 0x0011FB14
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

	// Token: 0x06001D1D RID: 7453 RVA: 0x00121A08 File Offset: 0x0011FC08
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

	// Token: 0x06001D1E RID: 7454 RVA: 0x00121E30 File Offset: 0x00120030
	public void ShowTrendNews()
	{
		if (this.year != 1976 || this.month != 1)
		{
			this.guiMain_.CreateTopNewsTrend(this.genres_.GetName(this.trendGenre) + " / " + this.tS_.GetThemes(this.trendTheme), this.genres_.GetPic(this.trendGenre));
		}
	}

	// Token: 0x06001D1F RID: 7455 RVA: 0x00121E9B File Offset: 0x0012009B
	public int PassedMonth()
	{
		return (this.year - 1976) * 12 + this.month;
	}

	// Token: 0x06001D20 RID: 7456 RVA: 0x00121EB4 File Offset: 0x001200B4
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

	// Token: 0x06001D21 RID: 7457 RVA: 0x00122134 File Offset: 0x00120334
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

	// Token: 0x06001D22 RID: 7458 RVA: 0x001221D8 File Offset: 0x001203D8
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

	// Token: 0x06001D23 RID: 7459 RVA: 0x0012223A File Offset: 0x0012043A
	public void AddFanshopverlauf(long i)
	{
		this.fanshopverlauf[0] += i;
	}

	// Token: 0x06001D24 RID: 7460 RVA: 0x0012224D File Offset: 0x0012044D
	public void AddVerkaufsverlaufKonsolen(long i)
	{
		this.verkaufsverlaufKonsolen[0] += i;
	}

	// Token: 0x06001D25 RID: 7461 RVA: 0x00122260 File Offset: 0x00120460
	public void AddVerkaufsverlauf(long i)
	{
		this.verkaufsverlauf[0] += i;
	}

	// Token: 0x06001D26 RID: 7462 RVA: 0x00122273 File Offset: 0x00120473
	public void AddDownloadverlauf(long i)
	{
		this.downloadverlauf[0] += i;
	}

	// Token: 0x06001D27 RID: 7463 RVA: 0x00122286 File Offset: 0x00120486
	public void AddFanverlauf(long i)
	{
		this.fansverlauf[0] = i;
	}

	// Token: 0x06001D28 RID: 7464 RVA: 0x00122291 File Offset: 0x00120491
	public void AddAboverlauf(long i)
	{
		this.aboverlauf[0] = i;
	}

	// Token: 0x06001D29 RID: 7465 RVA: 0x0012229C File Offset: 0x0012049C
	public void AddFinanzverlauf(long i)
	{
		this.finanzVerlauf[0] = i;
	}

	// Token: 0x06001D2A RID: 7466 RVA: 0x001222A8 File Offset: 0x001204A8
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

	// Token: 0x06001D2B RID: 7467 RVA: 0x00122420 File Offset: 0x00120620
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

	// Token: 0x06001D2C RID: 7468 RVA: 0x001225D5 File Offset: 0x001207D5
	public long GetKredit()
	{
		return this.kredit;
	}

	// Token: 0x06001D2D RID: 7469 RVA: 0x001225E0 File Offset: 0x001207E0
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

	// Token: 0x06001D2E RID: 7470 RVA: 0x0012269C File Offset: 0x0012089C
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

	// Token: 0x06001D2F RID: 7471 RVA: 0x0012284C File Offset: 0x00120A4C
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

	// Token: 0x06001D30 RID: 7472 RVA: 0x0012285B File Offset: 0x00120A5B
	public void DestroyMainMenuObjects()
	{
		base.StartCoroutine(this.DestroyMainMenuObjectsAfterOneFrame());
	}

	// Token: 0x06001D31 RID: 7473 RVA: 0x0012286A File Offset: 0x00120A6A
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

	// Token: 0x06001D32 RID: 7474 RVA: 0x0012287C File Offset: 0x00120A7C
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

	// Token: 0x06001D33 RID: 7475 RVA: 0x00122909 File Offset: 0x00120B09
	public void RemovePublisherExklusivVertrag()
	{
		this.exklusivVertrag_ID = -1;
		this.exklusivVertrag_laufzeit = 0;
		this.exkklusivVertragScript_ = null;
	}

	// Token: 0x06001D34 RID: 7476 RVA: 0x00122920 File Offset: 0x00120B20
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

	// Token: 0x06001D35 RID: 7477 RVA: 0x00122989 File Offset: 0x00120B89
	public bool IsMyBuilding(int id_)
	{
		return this.buildings[id_];
	}

	// Token: 0x06001D36 RID: 7478 RVA: 0x00122994 File Offset: 0x00120B94
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

	// Token: 0x06001D37 RID: 7479 RVA: 0x00122B23 File Offset: 0x00120D23
	public void SetGlobalEvent(int i)
	{
		this.globalEvent = i;
		this.globalEventWeeks = UnityEngine.Random.Range(16, 32);
	}

	// Token: 0x06001D38 RID: 7480 RVA: 0x00122B3B File Offset: 0x00120D3B
	private void PayBankZinsen()
	{
		if (this.globalEvent == 4)
		{
			this.Pay((long)(this.GetKreditZinsen() * 3), 20);
			return;
		}
		this.Pay((long)this.GetKreditZinsen(), 20);
	}

	// Token: 0x06001D39 RID: 7481 RVA: 0x00122B68 File Offset: 0x00120D68
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

	// Token: 0x06001D3A RID: 7482 RVA: 0x00122E78 File Offset: 0x00121078
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

	// Token: 0x06001D3B RID: 7483 RVA: 0x001230BF File Offset: 0x001212BF
	public string GetSavegameTitle()
	{
		if (!this.multiplayer)
		{
			return "savegame";
		}
		return "mp";
	}

	// Token: 0x06001D3C RID: 7484 RVA: 0x001230D4 File Offset: 0x001212D4
	public void SetRandomMultiplayerSaveID()
	{
		this.multiplayerSaveID = UnityEngine.Random.Range(100, 9999999);
	}

	// Token: 0x06001D3D RID: 7485 RVA: 0x001230E8 File Offset: 0x001212E8
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

	// Token: 0x06001D3E RID: 7486 RVA: 0x00123138 File Offset: 0x00121338
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

	// Token: 0x06001D3F RID: 7487 RVA: 0x00123320 File Offset: 0x00121520
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

	// Token: 0x06001D40 RID: 7488 RVA: 0x001233D8 File Offset: 0x001215D8
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

	// Token: 0x06001D41 RID: 7489 RVA: 0x0012359C File Offset: 0x0012179C
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

	// Token: 0x06001D42 RID: 7490 RVA: 0x00123837 File Offset: 0x00121A37
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

	// Token: 0x06001D43 RID: 7491 RVA: 0x00123869 File Offset: 0x00121A69
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

	// Token: 0x06001D44 RID: 7492 RVA: 0x001238A8 File Offset: 0x00121AA8
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

	// Token: 0x06001D45 RID: 7493 RVA: 0x0012395C File Offset: 0x00121B5C
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

	// Token: 0x06001D46 RID: 7494 RVA: 0x00123A00 File Offset: 0x00121C00
	public void AddColliderLayer(Transform go)
	{
		this.listColliderLayer.Add(go);
	}

	// Token: 0x06001D47 RID: 7495 RVA: 0x00123A10 File Offset: 0x00121C10
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

	// Token: 0x06001D48 RID: 7496 RVA: 0x00123A64 File Offset: 0x00121C64
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

	// Token: 0x06001D49 RID: 7497 RVA: 0x00123AD0 File Offset: 0x00121CD0
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

	// Token: 0x06001D4A RID: 7498 RVA: 0x00123B44 File Offset: 0x00121D44
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

	// Token: 0x06001D4B RID: 7499 RVA: 0x00123BF3 File Offset: 0x00121DF3
	public bool NotEnoughMoney(int wantToPay)
	{
		return this.money + this.GetKreditlimit() < (long)wantToPay;
	}

	// Token: 0x06001D4C RID: 7500 RVA: 0x00002715 File Offset: 0x00000915
	public void Multiplayer_SendDataAfterGameStart()
	{
	}

	// Token: 0x06001D4D RID: 7501 RVA: 0x00123C09 File Offset: 0x00121E09
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

	// Token: 0x06001D4E RID: 7502 RVA: 0x00123C3E File Offset: 0x00121E3E
	public int GetAchivementBonus(int id_)
	{
		return this.amountAchivementsBonus[id_];
	}

	// Token: 0x06001D4F RID: 7503 RVA: 0x00123C48 File Offset: 0x00121E48
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

	// Token: 0x06001D50 RID: 7504 RVA: 0x00123CB0 File Offset: 0x00121EB0
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

	// Token: 0x06001D51 RID: 7505 RVA: 0x00123D48 File Offset: 0x00121F48
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

	// Token: 0x06001D52 RID: 7506 RVA: 0x00123D91 File Offset: 0x00121F91
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

	// Token: 0x06001D53 RID: 7507 RVA: 0x00123DC4 File Offset: 0x00121FC4
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

	// Token: 0x06001D54 RID: 7508 RVA: 0x00123E47 File Offset: 0x00122047
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

	// Token: 0x06001D55 RID: 7509 RVA: 0x00123E78 File Offset: 0x00122078
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

	// Token: 0x06001D56 RID: 7510 RVA: 0x00123EFB File Offset: 0x001220FB
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

	// Token: 0x06001D57 RID: 7511 RVA: 0x00123F2C File Offset: 0x0012212C
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

	// Token: 0x06001D58 RID: 7512 RVA: 0x00123F93 File Offset: 0x00122193
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

	// Token: 0x06001D59 RID: 7513 RVA: 0x00123FC4 File Offset: 0x001221C4
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

	// Token: 0x06001D5A RID: 7514 RVA: 0x00124047 File Offset: 0x00122247
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

	// Token: 0x06001D5B RID: 7515 RVA: 0x00124078 File Offset: 0x00122278
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

	// Token: 0x06001D5C RID: 7516 RVA: 0x001240FC File Offset: 0x001222FC
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

	// Token: 0x040023FE RID: 9214
	public string buildVersion = "BUILD 2020.08.22A";

	// Token: 0x040023FF RID: 9215
	public float[] gameSpeeds;

	// Token: 0x04002400 RID: 9216
	public GameObject[] mainMenuObjects;

	// Token: 0x04002401 RID: 9217
	public GameObject[] weatherEffects;

	// Token: 0x04002402 RID: 9218
	public Light globalLight;

	// Token: 0x04002403 RID: 9219
	public Color[] globalLightColors;

	// Token: 0x04002404 RID: 9220
	public int exklusivVertrag_ID = -1;

	// Token: 0x04002405 RID: 9221
	public int exklusivVertrag_laufzeit;

	// Token: 0x04002406 RID: 9222
	private publisherScript exkklusivVertragScript_;

	// Token: 0x04002407 RID: 9223
	public float record_Gameplay;

	// Token: 0x04002408 RID: 9224
	public float record_Grafik;

	// Token: 0x04002409 RID: 9225
	public float record_Sound;

	// Token: 0x0400240A RID: 9226
	public float record_Technik;

	// Token: 0x0400240B RID: 9227
	public float auftragsAnsehen;

	// Token: 0x0400240C RID: 9228
	public int studioPoints;

	// Token: 0x0400240D RID: 9229
	public int myID = 100000;

	// Token: 0x0400240E RID: 9230
	public publisherScript myPubS_;

	// Token: 0x0400240F RID: 9231
	public string playerName = "";

	// Token: 0x04002410 RID: 9232
	public int bankWarning;

	// Token: 0x04002411 RID: 9233
	public bool multiplayer;

	// Token: 0x04002412 RID: 9234
	public bool mpLobbyOpen;

	// Token: 0x04002413 RID: 9235
	public int multiplayerSaveID;

	// Token: 0x04002414 RID: 9236
	public int office;

	// Token: 0x04002415 RID: 9237
	public int savegameVersion;

	// Token: 0x04002416 RID: 9238
	public int year = 1976;

	// Token: 0x04002417 RID: 9239
	public int month = 1;

	// Token: 0x04002418 RID: 9240
	public int week = 1;

	// Token: 0x04002419 RID: 9241
	public int difficulty;

	// Token: 0x0400241A RID: 9242
	public int globalEvent = -1;

	// Token: 0x0400241B RID: 9243
	public int globalEventWeeks;

	// Token: 0x0400241C RID: 9244
	public int trendGenre;

	// Token: 0x0400241D RID: 9245
	public int trendAntiGenre = 1;

	// Token: 0x0400241E RID: 9246
	public int trendTheme;

	// Token: 0x0400241F RID: 9247
	public int trendAntiTheme = 1;

	// Token: 0x04002420 RID: 9248
	public int trendWeeks = 20;

	// Token: 0x04002421 RID: 9249
	public int trendNextGenre;

	// Token: 0x04002422 RID: 9250
	public int trendNextAntiGenre;

	// Token: 0x04002423 RID: 9251
	public int trendNextTheme;

	// Token: 0x04002424 RID: 9252
	public int trendNextAntiTheme;

	// Token: 0x04002425 RID: 9253
	public long money = 200000L;

	// Token: 0x04002426 RID: 9254
	public long kredit;

	// Token: 0x04002427 RID: 9255
	public bool[] buildings;

	// Token: 0x04002428 RID: 9256
	public int anrufe;

	// Token: 0x04002429 RID: 9257
	public bool lastGameCommercialFlop;

	// Token: 0x0400242A RID: 9258
	public float dayTimer;

	// Token: 0x0400242B RID: 9259
	public float gameSpeed = 1f;

	// Token: 0x0400242C RID: 9260
	private float gameSpeedOrg;

	// Token: 0x0400242D RID: 9261
	private bool pauseGame;

	// Token: 0x0400242E RID: 9262
	public int anzKonkurrenten;

	// Token: 0x0400242F RID: 9263
	public float speedSetting = 0.05f;

	// Token: 0x04002430 RID: 9264
	public int goldeneSchallplatten;

	// Token: 0x04002431 RID: 9265
	public int platinSchallplatten;

	// Token: 0x04002432 RID: 9266
	public int diamantSchallplatten;

	// Token: 0x04002433 RID: 9267
	public int award_GOTY;

	// Token: 0x04002434 RID: 9268
	public int award_Studio;

	// Token: 0x04002435 RID: 9269
	public int award_Grafik;

	// Token: 0x04002436 RID: 9270
	public int award_Sound;

	// Token: 0x04002437 RID: 9271
	public int award_Trendsetter;

	// Token: 0x04002438 RID: 9272
	public int award_Publisher;

	// Token: 0x04002439 RID: 9273
	public int pubOffersAmount;

	// Token: 0x0400243A RID: 9274
	public int lastUsedEngine;

	// Token: 0x0400243B RID: 9275
	public GameObject charFoto;

	// Token: 0x0400243C RID: 9276
	public GameObject guiPops;

	// Token: 0x0400243D RID: 9277
	public int personal_druck = 1;

	// Token: 0x0400243E RID: 9278
	public int personal_pausen = 1;

	// Token: 0x0400243F RID: 9279
	public int personal_motivation = 40;

	// Token: 0x04002440 RID: 9280
	public int personal_crunch = 90;

	// Token: 0x04002441 RID: 9281
	public bool personal_dontLeaveBuilding;

	// Token: 0x04002442 RID: 9282
	public bool personal_RobotDontLeaveBuilding;

	// Token: 0x04002443 RID: 9283
	public bool personal_ki;

	// Token: 0x04002444 RID: 9284
	public string[] personal_group_names = new string[12];

	// Token: 0x04002445 RID: 9285
	public long[] fanshopverlauf = new long[24];

	// Token: 0x04002446 RID: 9286
	public long[] finanzVerlauf = new long[24];

	// Token: 0x04002447 RID: 9287
	public long[] verkaufsverlauf = new long[24];

	// Token: 0x04002448 RID: 9288
	public long[] verkaufsverlaufKonsolen = new long[24];

	// Token: 0x04002449 RID: 9289
	public long[] aboverlauf = new long[24];

	// Token: 0x0400244A RID: 9290
	public long[] downloadverlauf = new long[24];

	// Token: 0x0400244B RID: 9291
	public long[] fansverlauf = new long[24];

	// Token: 0x0400244C RID: 9292
	public long[] finanzenMonat = new long[100];

	// Token: 0x0400244D RID: 9293
	public long[] finanzenMonatLast = new long[100];

	// Token: 0x0400244E RID: 9294
	public long[] finanzenJahr = new long[100];

	// Token: 0x0400244F RID: 9295
	public long[] finanzenJahrLast = new long[100];

	// Token: 0x04002450 RID: 9296
	public List<long> finanzVerlaufEinnahmen = new List<long>();

	// Token: 0x04002451 RID: 9297
	public List<long> finanzVerlaufAusgaben = new List<long>();

	// Token: 0x04002452 RID: 9298
	public List<string> history = new List<string>();

	// Token: 0x04002453 RID: 9299
	public List<int> madGamesCon_Jahr = new List<int>();

	// Token: 0x04002454 RID: 9300
	public List<int> madGamesCon_BestGrafik = new List<int>();

	// Token: 0x04002455 RID: 9301
	public List<int> madGamesCon_BestSound = new List<int>();

	// Token: 0x04002456 RID: 9302
	public List<int> madGamesCon_BestStudio = new List<int>();

	// Token: 0x04002457 RID: 9303
	public List<int> madGamesCon_BestPublisher = new List<int>();

	// Token: 0x04002458 RID: 9304
	public List<int> madGamesCon_BestGame = new List<int>();

	// Token: 0x04002459 RID: 9305
	public List<int> madGamesCon_BadGame = new List<int>();

	// Token: 0x0400245A RID: 9306
	public bool[] devLegendsInUse;

	// Token: 0x0400245B RID: 9307
	public bool[] devLegendsFemale;

	// Token: 0x0400245C RID: 9308
	public bool[] devLegendsDesigner;

	// Token: 0x0400245D RID: 9309
	public bool[] devLegendsProgrammierer;

	// Token: 0x0400245E RID: 9310
	public bool[] devLegendsGrafiker;

	// Token: 0x0400245F RID: 9311
	public bool[] devLegendsMusiker;

	// Token: 0x04002460 RID: 9312
	public bool[] devLegendsForscher;

	// Token: 0x04002461 RID: 9313
	public bool[] devLegendsHardware;

	// Token: 0x04002462 RID: 9314
	public bool[] newsSetting;

	// Token: 0x04002463 RID: 9315
	public bool[] gameTabFilter;

	// Token: 0x04002464 RID: 9316
	public int[] lastGamesGenre;

	// Token: 0x04002465 RID: 9317
	public int gelangweiltGenre;

	// Token: 0x04002466 RID: 9318
	public int sauerBugs;

	// Token: 0x04002467 RID: 9319
	public int awardBonus;

	// Token: 0x04002468 RID: 9320
	public float awardBonusAmount;

	// Token: 0x04002469 RID: 9321
	public bool[] achivements;

	// Token: 0x0400246A RID: 9322
	public bool[] achivementsDisabled;

	// Token: 0x0400246B RID: 9323
	public int[] achivementsBonus;

	// Token: 0x0400246C RID: 9324
	public int[] amountAchivementsBonus;

	// Token: 0x0400246D RID: 9325
	public string marktforschung_datum = "";

	// Token: 0x0400246E RID: 9326
	public float marktforschung_digtal = -1f;

	// Token: 0x0400246F RID: 9327
	public float marktforschung_retail = -1f;

	// Token: 0x04002470 RID: 9328
	public float marktforschung_deluxe = -1f;

	// Token: 0x04002471 RID: 9329
	public float marktforschung_collectors = -1f;

	// Token: 0x04002472 RID: 9330
	public float marktforschung_arcade = -1f;

	// Token: 0x04002473 RID: 9331
	public int marktforschung_bestPlattform = -1;

	// Token: 0x04002474 RID: 9332
	public int marktforschung_badPlattform = -1;

	// Token: 0x04002475 RID: 9333
	public int marktforschung_bestPlattformKonsole = -1;

	// Token: 0x04002476 RID: 9334
	public int marktforschung_badPlattformKonsole = -1;

	// Token: 0x04002477 RID: 9335
	public int marktforschung_bestPlattformHandheld = -1;

	// Token: 0x04002478 RID: 9336
	public int marktforschung_badPlattformHandheld = -1;

	// Token: 0x04002479 RID: 9337
	public int marktforschung_bestPlattformHandy = -1;

	// Token: 0x0400247A RID: 9338
	public int marktforschung_badPlattformHandy = -1;

	// Token: 0x0400247B RID: 9339
	public int marktforschung_nextGenre = -1;

	// Token: 0x0400247C RID: 9340
	public int marktforschung_nextTopic = -1;

	// Token: 0x0400247D RID: 9341
	public int marktforschung_nextBadGenre = -1;

	// Token: 0x0400247E RID: 9342
	public int marktforschung_nextBadTopic = -1;

	// Token: 0x0400247F RID: 9343
	public bool automatic_RemoveGameFormMarket;

	// Token: 0x04002480 RID: 9344
	public bool settings_TutorialOff = true;

	// Token: 0x04002481 RID: 9345
	public bool settings_RandomEventsOff = true;

	// Token: 0x04002482 RID: 9346
	public bool settings_autoPauseForMultiplayer;

	// Token: 0x04002483 RID: 9347
	public bool settings_RandomReviews;

	// Token: 0x04002484 RID: 9348
	public bool settings_arbeitsgeschwindigkeitAnpassen;

	// Token: 0x04002485 RID: 9349
	public bool settings_history;

	// Token: 0x04002486 RID: 9350
	public bool settings_plattformEnd;

	// Token: 0x04002487 RID: 9351
	public bool badGameThisYear;

	// Token: 0x04002488 RID: 9352
	public bool sellLagerbestandAutomatic;

	// Token: 0x04002489 RID: 9353
	public textScript tS_;

	// Token: 0x0400248A RID: 9354
	public settingsScript settings_;

	// Token: 0x0400248B RID: 9355
	public mapScript mapScript_;

	// Token: 0x0400248C RID: 9356
	public unlockScript unlock_;

	// Token: 0x0400248D RID: 9357
	private pickCharacterScript pickChar_;

	// Token: 0x0400248E RID: 9358
	private pickObjectScript pickObject_;

	// Token: 0x0400248F RID: 9359
	public genres genres_;

	// Token: 0x04002490 RID: 9360
	private themes themes_;

	// Token: 0x04002491 RID: 9361
	private engineFeatures eF_;

	// Token: 0x04002492 RID: 9362
	public gameplayFeatures gF_;

	// Token: 0x04002493 RID: 9363
	public hardware hardware_;

	// Token: 0x04002494 RID: 9364
	public hardwareFeatures hardwareFeatures_;

	// Token: 0x04002495 RID: 9365
	private arbeitsmarkt arbeitsmarkt_;

	// Token: 0x04002496 RID: 9366
	public platforms platforms_;

	// Token: 0x04002497 RID: 9367
	public copyProtect copyProtect_;

	// Token: 0x04002498 RID: 9368
	public anitCheat antiCheat_;

	// Token: 0x04002499 RID: 9369
	public licences licences_;

	// Token: 0x0400249A RID: 9370
	public games games_;

	// Token: 0x0400249B RID: 9371
	public GUI_Main guiMain_;

	// Token: 0x0400249C RID: 9372
	private npcEngines npcEngines_;

	// Token: 0x0400249D RID: 9373
	private publisher publisher_;

	// Token: 0x0400249E RID: 9374
	private createCharScript cCS_;

	// Token: 0x0400249F RID: 9375
	public contractWorkMain contractWorkMain_;

	// Token: 0x040024A0 RID: 9376
	public publishingOfferMain publishingOfferMain_;

	// Token: 0x040024A1 RID: 9377
	private savegameScript save_;

	// Token: 0x040024A2 RID: 9378
	public cameraMovementScript cmS_;

	// Token: 0x040024A3 RID: 9379
	public sfxScript sfx_;

	// Token: 0x040024A4 RID: 9380
	public NetworkManager manager;

	// Token: 0x040024A5 RID: 9381
	public mpCalls mpCalls_;

	// Token: 0x040024A6 RID: 9382
	public achiementScript achScript_;

	// Token: 0x040024A7 RID: 9383
	public reviewText reviewText_;

	// Token: 0x040024A8 RID: 9384
	public forschungSonstiges forschungSonstiges_;

	// Token: 0x040024A9 RID: 9385
	public roomDataScript rdS_;

	// Token: 0x040024AA RID: 9386
	public GameObject[] arrayCharacters;

	// Token: 0x040024AB RID: 9387
	public List<GameObject> arrayCharactersForDoors = new List<GameObject>();

	// Token: 0x040024AC RID: 9388
	public GameObject[] arrayObjects;

	// Token: 0x040024AD RID: 9389
	public GameObject[] arrayRooms;

	// Token: 0x040024AE RID: 9390
	public GameObject[] arrayRobots;

	// Token: 0x040024AF RID: 9391
	public GameObject[] arrayMuell;

	// Token: 0x040024B0 RID: 9392
	public objectScript[] arrayObjectScripts;

	// Token: 0x040024B1 RID: 9393
	public characterScript[] arrayCharactersScripts;

	// Token: 0x040024B2 RID: 9394
	public GameObject[] miscParticlePrefabs;

	// Token: 0x040024B3 RID: 9395
	public GameObject[] miscGamePrefabs;

	// Token: 0x040024B4 RID: 9396
	public Material[] specialMaterials;

	// Token: 0x040024B5 RID: 9397
	public Material[] floorMaterials;

	// Token: 0x040024B6 RID: 9398
	public Texture2D[] specialTextures;

	// Token: 0x040024B7 RID: 9399
	public Shader[] shaders;

	// Token: 0x040024B8 RID: 9400
	public float objectRotation;

	// Token: 0x040024B9 RID: 9401
	public GameObject pickedObject;

	// Token: 0x040024BA RID: 9402
	public List<GameObject> pickedChars = new List<GameObject>();

	// Token: 0x040024BB RID: 9403
	public bool snapObject;

	// Token: 0x040024BC RID: 9404
	public bool snapRotation;

	// Token: 0x040024BD RID: 9405
	public GameObject cameraPersonalPhoto;

	// Token: 0x040024BE RID: 9406
	public VectorLine roomLine;

	// Token: 0x040024BF RID: 9407
	public float weatherTimer;

	// Token: 0x040024C0 RID: 9408
	public int anzSprechblasen;

	// Token: 0x040024C1 RID: 9409
	public LayerMask layerMask_Floor;

	// Token: 0x040024C2 RID: 9410
	public const int taskID_taskForschung = 10;

	// Token: 0x040024C3 RID: 9411
	public const int taskID_taskEngine = 20;

	// Token: 0x040024C4 RID: 9412
	public const int taskID_taskUpdate = 30;

	// Token: 0x040024C5 RID: 9413
	public const int taskID_taskGame = 40;

	// Token: 0x040024C6 RID: 9414
	public const int taskID_taskF2PUpdate = 50;

	// Token: 0x040024C7 RID: 9415
	public const int taskID_taskMarketing = 60;

	// Token: 0x040024C8 RID: 9416
	public const int taskID_taskMarketingSpezial = 70;

	// Token: 0x040024C9 RID: 9417
	public const int taskID_taskMitarbeitersuche = 80;

	// Token: 0x040024CA RID: 9418
	public const int taskID_taskTraining = 90;

	// Token: 0x040024CB RID: 9419
	public const int taskID_taskSpielbericht = 100;

	// Token: 0x040024CC RID: 9420
	public const int taskID_taskGameplayVerbessern = 110;

	// Token: 0x040024CD RID: 9421
	public const int taskID_taskBugfixing = 120;

	// Token: 0x040024CE RID: 9422
	public const int taskID_taskGrafikVerbessern = 130;

	// Token: 0x040024CF RID: 9423
	public const int taskID_taskSoundVerbessern = 140;

	// Token: 0x040024D0 RID: 9424
	public const int taskID_taskAnimationVerbessern = 150;

	// Token: 0x040024D1 RID: 9425
	public const int taskID_taskProduction = 160;

	// Token: 0x040024D2 RID: 9426
	public const int taskID_taskArcadeProduction = 170;

	// Token: 0x040024D3 RID: 9427
	public const int taskID_taskKonsole = 180;

	// Token: 0x040024D4 RID: 9428
	public const int taskID_taskFankampagne = 190;

	// Token: 0x040024D5 RID: 9429
	public const int taskID_taskSupport = 200;

	// Token: 0x040024D6 RID: 9430
	public const int taskID_taskContractWork = 210;

	// Token: 0x040024D7 RID: 9431
	public const int taskID_taskContractWait = 220;

	// Token: 0x040024D8 RID: 9432
	public const int taskID_taskMarktforschung = 230;

	// Token: 0x040024D9 RID: 9433
	public const int taskID_taskPolishing = 240;

	// Token: 0x040024DA RID: 9434
	public const int taskID_taskUnterstuetzen = 250;

	// Token: 0x040024DB RID: 9435
	public const int taskID_taskWait = 260;

	// Token: 0x040024DC RID: 9436
	public const int taskID_taskFanshop = 270;

	// Token: 0x040024DD RID: 9437
	public bool findObjects = true;

	// Token: 0x040024DE RID: 9438
	public bool findRooms = true;

	// Token: 0x040024DF RID: 9439
	public bool findCharacters = true;

	// Token: 0x040024E0 RID: 9440
	public bool findMuell = true;

	// Token: 0x040024E1 RID: 9441
	public bool findRobots = true;

	// Token: 0x040024E2 RID: 9442
	public bool officeLoaded;

	// Token: 0x040024E3 RID: 9443
	private bool contendIsLoaded;

	// Token: 0x040024E4 RID: 9444
	private float updateKiTimer;

	// Token: 0x040024E5 RID: 9445
	private float updateUnkorrekterRoom;

	// Token: 0x040024E6 RID: 9446
	public int autoSaveInterval = -1;

	// Token: 0x040024E7 RID: 9447
	public float lauf = 0.2f;

	// Token: 0x040024E8 RID: 9448
	private float filterTimer;

	// Token: 0x040024E9 RID: 9449
	public float carSpawnTimer;

	// Token: 0x040024EA RID: 9450
	public List<GameObject> carList = new List<GameObject>();

	// Token: 0x040024EB RID: 9451
	public GameObject[] carPrefabs;

	// Token: 0x040024EC RID: 9452
	public List<Transform> listColliderLayer = new List<Transform>();
}
