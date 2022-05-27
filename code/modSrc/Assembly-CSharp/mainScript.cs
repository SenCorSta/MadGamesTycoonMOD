using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vectrosity;

// Token: 0x02000329 RID: 809
public class mainScript : MonoBehaviour
{
	// Token: 0x06001C8D RID: 7309 RVA: 0x000139AB File Offset: 0x00011BAB
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001C8E RID: 7310 RVA: 0x00120AA4 File Offset: 0x0011ECA4
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

	// Token: 0x06001C8F RID: 7311 RVA: 0x00120EA4 File Offset: 0x0011F0A4
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

	// Token: 0x06001C90 RID: 7312 RVA: 0x00120F48 File Offset: 0x0011F148
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

	// Token: 0x06001C91 RID: 7313 RVA: 0x0012102C File Offset: 0x0011F22C
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

	// Token: 0x06001C92 RID: 7314 RVA: 0x000139B3 File Offset: 0x00011BB3
	private IEnumerator iInitScene(bool fromSavegame)
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		this.mapScript_.InitBuilding(fromSavegame);
		GameObject.Find("CamMovement").GetComponent<cameraMovementScript>().FindCameraLimits();
		this.officeLoaded = true;
		yield break;
	}

	// Token: 0x06001C93 RID: 7315 RVA: 0x000139C9 File Offset: 0x00011BC9
	private void Cheat()
	{
		if (Input.GetKeyDown(KeyCode.C))
		{
			this.guiMain_.uiObjects[216].SetActive(true);
			this.guiMain_.uiObjects[216].GetComponent<Menu_RandomEventGlobal>().Init(13);
		}
	}

	// Token: 0x06001C94 RID: 7316 RVA: 0x001210B0 File Offset: 0x0011F2B0
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

	// Token: 0x06001C95 RID: 7317 RVA: 0x00013A08 File Offset: 0x00011C08
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

	// Token: 0x06001C96 RID: 7318 RVA: 0x001211A0 File Offset: 0x0011F3A0
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
		if (!this.multiplayer || (this.multiplayer && this.mpCalls_.isServer))
		{
			this.UpdateTrend();
		}
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
		this.contractWorkMain_.UpdateContractWork(true);
		this.platforms_.UpdatePlatformSells(true, false);
		for (int i = 0; i < this.newsSetting.Length; i++)
		{
			this.newsSetting[i] = true;
		}
	}

	// Token: 0x06001C97 RID: 7319 RVA: 0x001213A0 File Offset: 0x0011F5A0
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

	// Token: 0x06001C98 RID: 7320 RVA: 0x00121408 File Offset: 0x0011F608
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

	// Token: 0x06001C99 RID: 7321 RVA: 0x00121590 File Offset: 0x0011F790
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

	// Token: 0x06001C9A RID: 7322 RVA: 0x00121664 File Offset: 0x0011F864
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

	// Token: 0x06001C9B RID: 7323 RVA: 0x00013A40 File Offset: 0x00011C40
	private void FindRobots()
	{
		this.arrayRobots = GameObject.FindGameObjectsWithTag("Robot");
	}

	// Token: 0x06001C9C RID: 7324 RVA: 0x00013A52 File Offset: 0x00011C52
	private void FindMuell()
	{
		this.arrayMuell = GameObject.FindGameObjectsWithTag("Muell");
	}

	// Token: 0x06001C9D RID: 7325 RVA: 0x00121724 File Offset: 0x0011F924
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

	// Token: 0x06001C9E RID: 7326 RVA: 0x00013A64 File Offset: 0x00011C64
	public void FindRooms()
	{
		this.arrayRooms = GameObject.FindGameObjectsWithTag("Room");
	}

	// Token: 0x06001C9F RID: 7327 RVA: 0x0012178C File Offset: 0x0011F98C
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

	// Token: 0x06001CA0 RID: 7328 RVA: 0x00013A76 File Offset: 0x00011C76
	public bool IsForcedPause()
	{
		return this.pauseGame;
	}

	// Token: 0x06001CA1 RID: 7329 RVA: 0x001219BC File Offset: 0x0011FBBC
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

	// Token: 0x06001CA2 RID: 7330 RVA: 0x00121A50 File Offset: 0x0011FC50
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
			this.WochenUpdates();
			if (this.week >= 5)
			{
				this.MonatlicheUpdates();
			}
		}
	}

	// Token: 0x06001CA3 RID: 7331 RVA: 0x00121AD4 File Offset: 0x0011FCD4
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
		this.UpdateTrend();
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
				if (component2)
				{
					component2.CreateNewGame2(false);
				}
			}
		}
		this.unlock_.CheckUnlock(true);
	}

	// Token: 0x06001CA4 RID: 7332 RVA: 0x00121C88 File Offset: 0x0011FE88
	public void MonatlicheUpdates()
	{
		if (this.multiplayer && this.mpCalls_.isServer)
		{
			this.mpCalls_.SERVER_Send_Command(4);
		}
		this.week = 1;
		this.month++;
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
				if (component3)
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
			this.AutoSaveMultiplayer();
		}
		this.Autosave();
	}

	// Token: 0x06001CA5 RID: 7333 RVA: 0x00121E44 File Offset: 0x00120044
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

	// Token: 0x06001CA6 RID: 7334 RVA: 0x00121E8C File Offset: 0x0012008C
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
		this.autoSaveInterval--;
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

	// Token: 0x06001CA7 RID: 7335 RVA: 0x00013A7E File Offset: 0x00011C7E
	public void AutoSaveMultiplayer()
	{
		if (this.multiplayer)
		{
			this.save_.SaveMultiplayer(0);
			this.guiMain_.ShowGameHasSaved();
		}
	}

	// Token: 0x06001CA8 RID: 7336 RVA: 0x00121F7C File Offset: 0x0012017C
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

	// Token: 0x06001CA9 RID: 7337 RVA: 0x00122014 File Offset: 0x00120214
	private void MadGamesConvention()
	{
		if (this.month == 7 && this.week == 1)
		{
			this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[185]);
			this.guiMain_.uiObjects[185].GetComponent<Menu_Messe>().Init();
		}
	}

	// Token: 0x06001CAA RID: 7338 RVA: 0x0012206C File Offset: 0x0012026C
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

	// Token: 0x06001CAB RID: 7339 RVA: 0x00122134 File Offset: 0x00120334
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

	// Token: 0x06001CAC RID: 7340 RVA: 0x001221C8 File Offset: 0x001203C8
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

	// Token: 0x06001CAD RID: 7341 RVA: 0x0012220C File Offset: 0x0012040C
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

	// Token: 0x06001CAE RID: 7342 RVA: 0x00122340 File Offset: 0x00120540
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

	// Token: 0x06001CAF RID: 7343 RVA: 0x00122388 File Offset: 0x00120588
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

	// Token: 0x06001CB0 RID: 7344 RVA: 0x001223D0 File Offset: 0x001205D0
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

	// Token: 0x06001CB1 RID: 7345 RVA: 0x00122418 File Offset: 0x00120618
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

	// Token: 0x06001CB2 RID: 7346 RVA: 0x00122460 File Offset: 0x00120660
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

	// Token: 0x06001CB3 RID: 7347 RVA: 0x001224B8 File Offset: 0x001206B8
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

	// Token: 0x06001CB4 RID: 7348 RVA: 0x00122548 File Offset: 0x00120748
	public void AddMadGameConvetionVerlauf(int bestGrafik_, int bestSound_, int bestStudio_, int bestStudioPlayer_, int bestPublisher_, int bestPublisherPlayer_, int bestGame_, int badBame_)
	{
		this.madGamesCon_Jahr.Add(this.year);
		this.madGamesCon_BestGrafik.Add(bestGrafik_);
		this.madGamesCon_BestSound.Add(bestSound_);
		this.madGamesCon_BestStudio.Add(bestStudio_);
		this.madGamesCon_BestStudioPlayer.Add(bestStudioPlayer_);
		this.madGamesCon_BestPublisher.Add(bestPublisher_);
		this.madGamesCon_BestPublisherPlayer.Add(bestPublisherPlayer_);
		this.madGamesCon_BestGame.Add(bestGame_);
		this.madGamesCon_BadGame.Add(badBame_);
	}

	// Token: 0x06001CB5 RID: 7349 RVA: 0x001225CC File Offset: 0x001207CC
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

	// Token: 0x06001CB6 RID: 7350 RVA: 0x00122AC0 File Offset: 0x00120CC0
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

	// Token: 0x06001CB7 RID: 7351 RVA: 0x00122DB8 File Offset: 0x00120FB8
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

	// Token: 0x06001CB8 RID: 7352 RVA: 0x00122E84 File Offset: 0x00121084
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

	// Token: 0x06001CB9 RID: 7353 RVA: 0x00122ED0 File Offset: 0x001210D0
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

	// Token: 0x06001CBA RID: 7354 RVA: 0x00122F40 File Offset: 0x00121140
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

	// Token: 0x06001CBB RID: 7355 RVA: 0x00122F90 File Offset: 0x00121190
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
			while (i > 0)
			{
				if (i <= 0)
				{
					Debug.Log("i: " + i);
					break;
				}
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
			for (int l = 0; l < this.genres_.genres_FANS.Length; l++)
			{
				if (this.genres_.genres_FANS[l] > 20000000)
				{
					this.genres_.genres_FANS[l] = 0;
				}
			}
		}
		if (this.genres_.GetAmountFans() >= 1000000)
		{
			this.achScript_.SetAchivement(54);
		}
	}

	// Token: 0x06001CBC RID: 7356 RVA: 0x0012318C File Offset: 0x0012138C
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

	// Token: 0x06001CBD RID: 7357 RVA: 0x00013A9F File Offset: 0x00011C9F
	public void UpdatePathfindingNextFrameExtern()
	{
		base.StartCoroutine(this.UpdatePathfindingNextFrame());
	}

	// Token: 0x06001CBE RID: 7358 RVA: 0x00013AAE File Offset: 0x00011CAE
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

	// Token: 0x06001CBF RID: 7359 RVA: 0x00013ABD File Offset: 0x00011CBD
	public void DisableSelectLayer(GameObject go)
	{
		base.StartCoroutine(this.iDisableSelectLayer(go));
	}

	// Token: 0x06001CC0 RID: 7360 RVA: 0x00013ACD File Offset: 0x00011CCD
	private IEnumerator iDisableSelectLayer(GameObject go)
	{
		yield return new WaitForEndOfFrame();
		if (go && go.transform.GetChild(0))
		{
			this.SetLayer(0, go.transform.GetChild(0));
		}
		yield break;
	}

	// Token: 0x06001CC1 RID: 7361 RVA: 0x00123204 File Offset: 0x00121404
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

	// Token: 0x06001CC2 RID: 7362 RVA: 0x00123280 File Offset: 0x00121480
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

	// Token: 0x06001CC3 RID: 7363 RVA: 0x00013AE3 File Offset: 0x00011CE3
	public void AddPickedChar(GameObject go)
	{
		this.pickedChars.Add(go);
	}

	// Token: 0x06001CC4 RID: 7364 RVA: 0x00013AF1 File Offset: 0x00011CF1
	public void SetGameSpeed(float f)
	{
		this.gameSpeed = f;
	}

	// Token: 0x06001CC5 RID: 7365 RVA: 0x001232E0 File Offset: 0x001214E0
	public float GetGameSpeed()
	{
		float result = this.gameSpeed;
		if (this.multiplayer && this.settings_autoPauseForMultiplayer && this.mpCalls_.AutoPause())
		{
			result = 0f;
		}
		return result;
	}

	// Token: 0x06001CC6 RID: 7366 RVA: 0x00013AFA File Offset: 0x00011CFA
	public float GetDeltaTime()
	{
		return Time.deltaTime * this.GetGameSpeed();
	}

	// Token: 0x06001CC7 RID: 7367 RVA: 0x000FD9BC File Offset: 0x000FBBBC
	public float Round(float value, int digits)
	{
		float num = Mathf.Pow(10f, (float)digits);
		return Mathf.Round(value * num) / num;
	}

	// Token: 0x06001CC8 RID: 7368 RVA: 0x00123318 File Offset: 0x00121518
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

	// Token: 0x06001CC9 RID: 7369 RVA: 0x001233C8 File Offset: 0x001215C8
	private float GetFloatMax(string stringValue)
	{
		float num = 1f;
		float.TryParse(stringValue, out num);
		return -num;
	}

	// Token: 0x06001CCA RID: 7370 RVA: 0x001233E8 File Offset: 0x001215E8
	private float GetFloatMin(string stringValue)
	{
		float result = 1f;
		float.TryParse(stringValue, out result);
		return result;
	}

	// Token: 0x06001CCB RID: 7371 RVA: 0x00123408 File Offset: 0x00121608
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

	// Token: 0x06001CCC RID: 7372 RVA: 0x001234A4 File Offset: 0x001216A4
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

	// Token: 0x06001CCD RID: 7373 RVA: 0x00003785 File Offset: 0x00001985
	public int GetNewID()
	{
		return UnityEngine.Random.Range(1, 2000000000);
	}

	// Token: 0x06001CCE RID: 7374 RVA: 0x001234E8 File Offset: 0x001216E8
	private void UpdateSpecialMaterials()
	{
		float x = Mathf.Cos(Time.time * this.GetGameSpeed()) * 0.5f + 1f;
		float y = Mathf.Sin(Time.time * this.GetGameSpeed()) * 0.5f + 1f;
		this.specialMaterials[0].mainTextureScale = new Vector2(x, y);
		this.specialMaterials[1].mainTextureOffset = new Vector2(Time.time * (this.GetGameSpeed() * 0.3f), 0f);
		this.specialMaterials[2].mainTextureOffset = new Vector2(0f, -Time.time * (this.GetGameSpeed() * 0.26f));
		this.specialMaterials[3].mainTextureOffset = new Vector2(0f, -Time.time * (this.GetGameSpeed() * 1.5f));
	}

	// Token: 0x06001CCF RID: 7375 RVA: 0x001235C4 File Offset: 0x001217C4
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

	// Token: 0x06001CD0 RID: 7376 RVA: 0x001236E0 File Offset: 0x001218E0
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

	// Token: 0x06001CD1 RID: 7377 RVA: 0x001237D4 File Offset: 0x001219D4
	private void UpdateTrend()
	{
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
					goto IL_1E3;
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
			IL_1E3:
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
			if (UnityEngine.Random.Range(0, 100) >= 20)
			{
				if (this.trendNextAntiGenre != -1)
				{
					this.trendAntiGenre = this.trendNextAntiGenre;
					goto IL_2A0;
				}
			}
			while (!flag)
			{
				this.trendAntiGenre = UnityEngine.Random.Range(0, this.genres_.genres_LEVEL.Length);
				if (this.genres_.genres_UNLOCK[this.trendAntiGenre] && this.trendAntiGenre != this.trendGenre)
				{
					flag = true;
				}
				num++;
				if (num > 10000)
				{
					flag = true;
				}
			}
			IL_2A0:
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
				for (int i = 0; i < this.genres_.genres_LEVEL.Length; i++)
				{
					if (this.genres_.genres_UNLOCK[i] && this.trendGenre != i)
					{
						this.trendAntiGenre = i;
						break;
					}
				}
			}
			if (this.trendTheme == this.trendNextAntiTheme)
			{
				for (int j = 0; j < this.themes_.themes_LEVEL.Length; j++)
				{
					if (this.trendTheme != j)
					{
						this.trendNextAntiTheme = j;
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

	// Token: 0x06001CD2 RID: 7378 RVA: 0x00123B7C File Offset: 0x00121D7C
	public void ShowTrendNews()
	{
		if (this.year != 1976 || this.month != 1)
		{
			this.guiMain_.CreateTopNewsTrend(this.genres_.GetName(this.trendGenre) + " / " + this.tS_.GetThemes(this.trendTheme), this.genres_.GetPic(this.trendGenre));
		}
	}

	// Token: 0x06001CD3 RID: 7379 RVA: 0x00013B08 File Offset: 0x00011D08
	public int PassedMonth()
	{
		return (this.year - 1976) * 12 + this.month;
	}

	// Token: 0x06001CD4 RID: 7380 RVA: 0x00123BE8 File Offset: 0x00121DE8
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

	// Token: 0x06001CD5 RID: 7381 RVA: 0x00123E68 File Offset: 0x00122068
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

	// Token: 0x06001CD6 RID: 7382 RVA: 0x00123F0C File Offset: 0x0012210C
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

	// Token: 0x06001CD7 RID: 7383 RVA: 0x00013B20 File Offset: 0x00011D20
	public void AddFanshopverlauf(long i)
	{
		this.fanshopverlauf[0] += i;
	}

	// Token: 0x06001CD8 RID: 7384 RVA: 0x00013B33 File Offset: 0x00011D33
	public void AddVerkaufsverlaufKonsolen(long i)
	{
		this.verkaufsverlaufKonsolen[0] += i;
	}

	// Token: 0x06001CD9 RID: 7385 RVA: 0x00013B46 File Offset: 0x00011D46
	public void AddVerkaufsverlauf(long i)
	{
		this.verkaufsverlauf[0] += i;
	}

	// Token: 0x06001CDA RID: 7386 RVA: 0x00013B59 File Offset: 0x00011D59
	public void AddDownloadverlauf(long i)
	{
		this.downloadverlauf[0] += i;
	}

	// Token: 0x06001CDB RID: 7387 RVA: 0x00013B6C File Offset: 0x00011D6C
	public void AddFanverlauf(long i)
	{
		this.fansverlauf[0] = i;
	}

	// Token: 0x06001CDC RID: 7388 RVA: 0x00013B77 File Offset: 0x00011D77
	public void AddAboverlauf(long i)
	{
		this.aboverlauf[0] = i;
	}

	// Token: 0x06001CDD RID: 7389 RVA: 0x00013B82 File Offset: 0x00011D82
	public void AddFinanzverlauf(long i)
	{
		this.finanzVerlauf[0] = i;
	}

	// Token: 0x06001CDE RID: 7390 RVA: 0x00123F70 File Offset: 0x00122170
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

	// Token: 0x06001CDF RID: 7391 RVA: 0x001240E8 File Offset: 0x001222E8
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

	// Token: 0x06001CE0 RID: 7392 RVA: 0x00013B8D File Offset: 0x00011D8D
	public long GetKredit()
	{
		return this.kredit;
	}

	// Token: 0x06001CE1 RID: 7393 RVA: 0x001242A0 File Offset: 0x001224A0
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

	// Token: 0x06001CE2 RID: 7394 RVA: 0x0012435C File Offset: 0x0012255C
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

	// Token: 0x06001CE3 RID: 7395 RVA: 0x00013B95 File Offset: 0x00011D95
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

	// Token: 0x06001CE4 RID: 7396 RVA: 0x00013BA4 File Offset: 0x00011DA4
	public void DestroyMainMenuObjects()
	{
		base.StartCoroutine(this.DestroyMainMenuObjectsAfterOneFrame());
	}

	// Token: 0x06001CE5 RID: 7397 RVA: 0x00013BB3 File Offset: 0x00011DB3
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

	// Token: 0x06001CE6 RID: 7398 RVA: 0x0012450C File Offset: 0x0012270C
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

	// Token: 0x06001CE7 RID: 7399 RVA: 0x00013BC2 File Offset: 0x00011DC2
	public void RemovePublisherExklusivVertrag()
	{
		this.exklusivVertrag_ID = -1;
		this.exklusivVertrag_laufzeit = 0;
		this.exkklusivVertragScript_ = null;
	}

	// Token: 0x06001CE8 RID: 7400 RVA: 0x0012459C File Offset: 0x0012279C
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

	// Token: 0x06001CE9 RID: 7401 RVA: 0x00013BD9 File Offset: 0x00011DD9
	public bool IsMyBuilding(int id_)
	{
		return this.buildings[id_];
	}

	// Token: 0x06001CEA RID: 7402 RVA: 0x00124608 File Offset: 0x00122808
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

	// Token: 0x06001CEB RID: 7403 RVA: 0x00013BE3 File Offset: 0x00011DE3
	public void SetGlobalEvent(int i)
	{
		this.globalEvent = i;
		this.globalEventWeeks = UnityEngine.Random.Range(16, 32);
	}

	// Token: 0x06001CEC RID: 7404 RVA: 0x00013BFB File Offset: 0x00011DFB
	private void PayBankZinsen()
	{
		if (this.globalEvent == 4)
		{
			this.Pay((long)(this.GetKreditZinsen() * 3), 20);
			return;
		}
		this.Pay((long)this.GetKreditZinsen(), 20);
	}

	// Token: 0x06001CED RID: 7405 RVA: 0x00124798 File Offset: 0x00122998
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
				if (!component.playerConsole && component.multiplaySlot == -1 && component.IsVerfuegbar())
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
				if (!component2.playerConsole && component2.multiplaySlot == -1 && component2.IsVerfuegbar())
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

	// Token: 0x06001CEE RID: 7406 RVA: 0x00124AC0 File Offset: 0x00122CC0
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

	// Token: 0x06001CEF RID: 7407 RVA: 0x00013C27 File Offset: 0x00011E27
	public string GetSavegameTitle()
	{
		if (!this.multiplayer)
		{
			return "savegame";
		}
		return "mp";
	}

	// Token: 0x06001CF0 RID: 7408 RVA: 0x00013C3C File Offset: 0x00011E3C
	public void SetRandomMultiplayerSaveID()
	{
		this.multiplayerSaveID = UnityEngine.Random.Range(100, 9999999);
	}

	// Token: 0x06001CF1 RID: 7409 RVA: 0x00124D08 File Offset: 0x00122F08
	public void SendSystemMessage(string c)
	{
		if (this.multiplayer && this.mpCalls_)
		{
			if (this.mpCalls_.isServer)
			{
				this.mpCalls_.SERVER_Send_Chat(this.mpCalls_.myID, c);
				return;
			}
			this.mpCalls_.CLIENT_Send_Chat(c);
		}
	}

	// Token: 0x06001CF2 RID: 7410 RVA: 0x00124D5C File Offset: 0x00122F5C
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

	// Token: 0x06001CF3 RID: 7411 RVA: 0x00124F44 File Offset: 0x00123144
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

	// Token: 0x06001CF4 RID: 7412 RVA: 0x00124FFC File Offset: 0x001231FC
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

	// Token: 0x06001CF5 RID: 7413 RVA: 0x001251C0 File Offset: 0x001233C0
	public void ShowMultiplayerView(int slot)
	{
		if (slot > this.mpCalls_.playersMP.Count)
		{
			return;
		}
		int playerID = this.mpCalls_.playersMP[slot].playerID;
		if (playerID == this.mpCalls_.myID)
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

	// Token: 0x06001CF6 RID: 7414 RVA: 0x00013C50 File Offset: 0x00011E50
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

	// Token: 0x06001CF7 RID: 7415 RVA: 0x00013C82 File Offset: 0x00011E82
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

	// Token: 0x06001CF8 RID: 7416 RVA: 0x00125460 File Offset: 0x00123660
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

	// Token: 0x06001CF9 RID: 7417 RVA: 0x00125514 File Offset: 0x00123714
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

	// Token: 0x06001CFA RID: 7418 RVA: 0x00013CBE File Offset: 0x00011EBE
	public void AddColliderLayer(Transform go)
	{
		this.listColliderLayer.Add(go);
	}

	// Token: 0x06001CFB RID: 7419 RVA: 0x001255B8 File Offset: 0x001237B8
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

	// Token: 0x06001CFC RID: 7420 RVA: 0x0012560C File Offset: 0x0012380C
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

	// Token: 0x06001CFD RID: 7421 RVA: 0x00125678 File Offset: 0x00123878
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

	// Token: 0x06001CFE RID: 7422 RVA: 0x00013CCC File Offset: 0x00011ECC
	public int GetMyMultiplayerID()
	{
		if (!this.mpCalls_)
		{
			this.FindScripts();
		}
		return this.mpCalls_.myID;
	}

	// Token: 0x06001CFF RID: 7423 RVA: 0x001256EC File Offset: 0x001238EC
	public bool Muttersprache(int i)
	{
		switch (i)
		{
		case 0:
			if (this.country == 0 || this.country == 2 || this.country == 13 || this.country == 7 || this.country == 40)
			{
				return true;
			}
			break;
		case 1:
			if (this.country == 5 || this.country == 14 || this.country == 16)
			{
				return true;
			}
			break;
		case 2:
			if (this.country == 6)
			{
				return true;
			}
			break;
		case 3:
			if (this.country == 10)
			{
				return true;
			}
			break;
		case 4:
			if (this.country == 9 || this.country == 22)
			{
				return true;
			}
			break;
		case 5:
			if (this.country == 11)
			{
				return true;
			}
			break;
		case 6:
			if (this.country == 12)
			{
				return true;
			}
			break;
		case 7:
			if (this.country == 8)
			{
				return true;
			}
			break;
		case 8:
			if (this.country == 3)
			{
				return true;
			}
			break;
		case 9:
			if (this.country == 4)
			{
				return true;
			}
			break;
		case 10:
			if (this.country == 1)
			{
				return true;
			}
			break;
		}
		return false;
	}

	// Token: 0x06001D00 RID: 7424 RVA: 0x00013CEC File Offset: 0x00011EEC
	public bool NotEnoughMoney(int wantToPay)
	{
		return this.money + this.GetKreditlimit() < (long)wantToPay;
	}

	// Token: 0x06001D01 RID: 7425 RVA: 0x00002098 File Offset: 0x00000298
	public void Multiplayer_SendDataAfterGameStart()
	{
	}

	// Token: 0x06001D02 RID: 7426 RVA: 0x00013D02 File Offset: 0x00011F02
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

	// Token: 0x06001D03 RID: 7427 RVA: 0x00013D37 File Offset: 0x00011F37
	public int GetAchivementBonus(int id_)
	{
		return this.amountAchivementsBonus[id_];
	}

	// Token: 0x06001D04 RID: 7428 RVA: 0x001257F4 File Offset: 0x001239F4
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

	// Token: 0x06001D05 RID: 7429 RVA: 0x0012585C File Offset: 0x00123A5C
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

	// Token: 0x040023E4 RID: 9188
	public string buildVersion = "BUILD 2020.08.22A";

	// Token: 0x040023E5 RID: 9189
	public float[] gameSpeeds;

	// Token: 0x040023E6 RID: 9190
	public GameObject[] mainMenuObjects;

	// Token: 0x040023E7 RID: 9191
	public GameObject[] weatherEffects;

	// Token: 0x040023E8 RID: 9192
	public Light globalLight;

	// Token: 0x040023E9 RID: 9193
	public Color[] globalLightColors;

	// Token: 0x040023EA RID: 9194
	public bool multiplayer;

	// Token: 0x040023EB RID: 9195
	public bool mpLobbyOpen;

	// Token: 0x040023EC RID: 9196
	public int multiplayerSaveID;

	// Token: 0x040023ED RID: 9197
	public int myID = 100000;

	// Token: 0x040023EE RID: 9198
	public string companyName = "";

	// Token: 0x040023EF RID: 9199
	public string playerName = "";

	// Token: 0x040023F0 RID: 9200
	public long money = 200000L;

	// Token: 0x040023F1 RID: 9201
	public long kredit;

	// Token: 0x040023F2 RID: 9202
	public int bankWarning;

	// Token: 0x040023F3 RID: 9203
	public int savegameVersion;

	// Token: 0x040023F4 RID: 9204
	public int office;

	// Token: 0x040023F5 RID: 9205
	public bool[] buildings;

	// Token: 0x040023F6 RID: 9206
	public int anrufe;

	// Token: 0x040023F7 RID: 9207
	public float auftragsAnsehen;

	// Token: 0x040023F8 RID: 9208
	public int logo;

	// Token: 0x040023F9 RID: 9209
	public int country;

	// Token: 0x040023FA RID: 9210
	public int year = 1976;

	// Token: 0x040023FB RID: 9211
	public int month = 1;

	// Token: 0x040023FC RID: 9212
	public int week = 1;

	// Token: 0x040023FD RID: 9213
	public int globalEvent = -1;

	// Token: 0x040023FE RID: 9214
	public int globalEventWeeks;

	// Token: 0x040023FF RID: 9215
	public int trendGenre;

	// Token: 0x04002400 RID: 9216
	public int trendAntiGenre = 1;

	// Token: 0x04002401 RID: 9217
	public int trendTheme;

	// Token: 0x04002402 RID: 9218
	public int trendAntiTheme = 1;

	// Token: 0x04002403 RID: 9219
	public int trendWeeks = 20;

	// Token: 0x04002404 RID: 9220
	public int trendNextGenre;

	// Token: 0x04002405 RID: 9221
	public int trendNextAntiGenre;

	// Token: 0x04002406 RID: 9222
	public int trendNextTheme;

	// Token: 0x04002407 RID: 9223
	public int trendNextAntiTheme;

	// Token: 0x04002408 RID: 9224
	public bool lastGameCommercialFlop;

	// Token: 0x04002409 RID: 9225
	public float dayTimer;

	// Token: 0x0400240A RID: 9226
	public float gameSpeed = 1f;

	// Token: 0x0400240B RID: 9227
	private float gameSpeedOrg;

	// Token: 0x0400240C RID: 9228
	private bool pauseGame;

	// Token: 0x0400240D RID: 9229
	public int difficulty;

	// Token: 0x0400240E RID: 9230
	public int anzKonkurrenten;

	// Token: 0x0400240F RID: 9231
	public float speedSetting = 0.05f;

	// Token: 0x04002410 RID: 9232
	public int goldeneSchallplatten;

	// Token: 0x04002411 RID: 9233
	public int platinSchallplatten;

	// Token: 0x04002412 RID: 9234
	public int diamantSchallplatten;

	// Token: 0x04002413 RID: 9235
	public int studioPoints;

	// Token: 0x04002414 RID: 9236
	public int award_GOTY;

	// Token: 0x04002415 RID: 9237
	public int award_Studio;

	// Token: 0x04002416 RID: 9238
	public int award_Grafik;

	// Token: 0x04002417 RID: 9239
	public int award_Sound;

	// Token: 0x04002418 RID: 9240
	public int award_Trendsetter;

	// Token: 0x04002419 RID: 9241
	public int award_Publisher;

	// Token: 0x0400241A RID: 9242
	public int pubOffersAmount;

	// Token: 0x0400241B RID: 9243
	public GameObject charFoto;

	// Token: 0x0400241C RID: 9244
	public GameObject guiPops;

	// Token: 0x0400241D RID: 9245
	public int exklusivVertrag_ID = -1;

	// Token: 0x0400241E RID: 9246
	public int exklusivVertrag_laufzeit;

	// Token: 0x0400241F RID: 9247
	private publisherScript exkklusivVertragScript_;

	// Token: 0x04002420 RID: 9248
	public int personal_druck = 1;

	// Token: 0x04002421 RID: 9249
	public int personal_pausen = 1;

	// Token: 0x04002422 RID: 9250
	public int personal_motivation = 40;

	// Token: 0x04002423 RID: 9251
	public int personal_crunch = 90;

	// Token: 0x04002424 RID: 9252
	public bool personal_dontLeaveBuilding;

	// Token: 0x04002425 RID: 9253
	public bool personal_RobotDontLeaveBuilding;

	// Token: 0x04002426 RID: 9254
	public bool personal_ki;

	// Token: 0x04002427 RID: 9255
	public string[] personal_group_names = new string[12];

	// Token: 0x04002428 RID: 9256
	public long[] fanshopverlauf = new long[24];

	// Token: 0x04002429 RID: 9257
	public long[] finanzVerlauf = new long[24];

	// Token: 0x0400242A RID: 9258
	public long[] verkaufsverlauf = new long[24];

	// Token: 0x0400242B RID: 9259
	public long[] verkaufsverlaufKonsolen = new long[24];

	// Token: 0x0400242C RID: 9260
	public long[] aboverlauf = new long[24];

	// Token: 0x0400242D RID: 9261
	public long[] downloadverlauf = new long[24];

	// Token: 0x0400242E RID: 9262
	public long[] fansverlauf = new long[24];

	// Token: 0x0400242F RID: 9263
	public long[] finanzenMonat = new long[100];

	// Token: 0x04002430 RID: 9264
	public long[] finanzenMonatLast = new long[100];

	// Token: 0x04002431 RID: 9265
	public long[] finanzenJahr = new long[100];

	// Token: 0x04002432 RID: 9266
	public long[] finanzenJahrLast = new long[100];

	// Token: 0x04002433 RID: 9267
	public List<long> finanzVerlaufEinnahmen = new List<long>();

	// Token: 0x04002434 RID: 9268
	public List<long> finanzVerlaufAusgaben = new List<long>();

	// Token: 0x04002435 RID: 9269
	public List<string> history = new List<string>();

	// Token: 0x04002436 RID: 9270
	public List<int> madGamesCon_Jahr = new List<int>();

	// Token: 0x04002437 RID: 9271
	public List<int> madGamesCon_BestGrafik = new List<int>();

	// Token: 0x04002438 RID: 9272
	public List<int> madGamesCon_BestSound = new List<int>();

	// Token: 0x04002439 RID: 9273
	public List<int> madGamesCon_BestStudio = new List<int>();

	// Token: 0x0400243A RID: 9274
	public List<int> madGamesCon_BestStudioPlayer = new List<int>();

	// Token: 0x0400243B RID: 9275
	public List<int> madGamesCon_BestPublisher = new List<int>();

	// Token: 0x0400243C RID: 9276
	public List<int> madGamesCon_BestPublisherPlayer = new List<int>();

	// Token: 0x0400243D RID: 9277
	public List<int> madGamesCon_BestGame = new List<int>();

	// Token: 0x0400243E RID: 9278
	public List<int> madGamesCon_BadGame = new List<int>();

	// Token: 0x0400243F RID: 9279
	public bool[] devLegendsInUse;

	// Token: 0x04002440 RID: 9280
	public bool[] devLegendsFemale;

	// Token: 0x04002441 RID: 9281
	public bool[] devLegendsDesigner;

	// Token: 0x04002442 RID: 9282
	public bool[] devLegendsProgrammierer;

	// Token: 0x04002443 RID: 9283
	public bool[] devLegendsGrafiker;

	// Token: 0x04002444 RID: 9284
	public bool[] devLegendsMusiker;

	// Token: 0x04002445 RID: 9285
	public bool[] devLegendsForscher;

	// Token: 0x04002446 RID: 9286
	public bool[] devLegendsHardware;

	// Token: 0x04002447 RID: 9287
	public int companySpecialGenre;

	// Token: 0x04002448 RID: 9288
	public bool[] newsSetting;

	// Token: 0x04002449 RID: 9289
	public bool[] gameTabFilter;

	// Token: 0x0400244A RID: 9290
	public int[] lastGamesGenre;

	// Token: 0x0400244B RID: 9291
	public int gelangweiltGenre;

	// Token: 0x0400244C RID: 9292
	public int sauerBugs;

	// Token: 0x0400244D RID: 9293
	public bool[] achivements;

	// Token: 0x0400244E RID: 9294
	public bool[] achivementsDisabled;

	// Token: 0x0400244F RID: 9295
	public int[] achivementsBonus;

	// Token: 0x04002450 RID: 9296
	public int[] amountAchivementsBonus;

	// Token: 0x04002451 RID: 9297
	public int[] awards;

	// Token: 0x04002452 RID: 9298
	public float record_Gameplay;

	// Token: 0x04002453 RID: 9299
	public float record_Grafik;

	// Token: 0x04002454 RID: 9300
	public float record_Sound;

	// Token: 0x04002455 RID: 9301
	public float record_Technik;

	// Token: 0x04002456 RID: 9302
	public string marktforschung_datum = "";

	// Token: 0x04002457 RID: 9303
	public float marktforschung_digtal = -1f;

	// Token: 0x04002458 RID: 9304
	public float marktforschung_retail = -1f;

	// Token: 0x04002459 RID: 9305
	public float marktforschung_deluxe = -1f;

	// Token: 0x0400245A RID: 9306
	public float marktforschung_collectors = -1f;

	// Token: 0x0400245B RID: 9307
	public float marktforschung_arcade = -1f;

	// Token: 0x0400245C RID: 9308
	public int marktforschung_bestPlattform = -1;

	// Token: 0x0400245D RID: 9309
	public int marktforschung_badPlattform = -1;

	// Token: 0x0400245E RID: 9310
	public int marktforschung_bestPlattformKonsole = -1;

	// Token: 0x0400245F RID: 9311
	public int marktforschung_badPlattformKonsole = -1;

	// Token: 0x04002460 RID: 9312
	public int marktforschung_bestPlattformHandheld = -1;

	// Token: 0x04002461 RID: 9313
	public int marktforschung_badPlattformHandheld = -1;

	// Token: 0x04002462 RID: 9314
	public int marktforschung_bestPlattformHandy = -1;

	// Token: 0x04002463 RID: 9315
	public int marktforschung_badPlattformHandy = -1;

	// Token: 0x04002464 RID: 9316
	public int marktforschung_nextGenre = -1;

	// Token: 0x04002465 RID: 9317
	public int marktforschung_nextTopic = -1;

	// Token: 0x04002466 RID: 9318
	public int marktforschung_nextBadGenre = -1;

	// Token: 0x04002467 RID: 9319
	public int marktforschung_nextBadTopic = -1;

	// Token: 0x04002468 RID: 9320
	public bool automatic_RemoveGameFormMarket;

	// Token: 0x04002469 RID: 9321
	public bool settings_TutorialOff = true;

	// Token: 0x0400246A RID: 9322
	public bool settings_RandomEventsOff = true;

	// Token: 0x0400246B RID: 9323
	public bool settings_autoPauseForMultiplayer;

	// Token: 0x0400246C RID: 9324
	public bool settings_RandomReviews;

	// Token: 0x0400246D RID: 9325
	public bool settings_arbeitsgeschwindigkeitAnpassen;

	// Token: 0x0400246E RID: 9326
	public bool settings_history;

	// Token: 0x0400246F RID: 9327
	public bool settings_plattformEnd;

	// Token: 0x04002470 RID: 9328
	public bool badGameThisYear;

	// Token: 0x04002471 RID: 9329
	public bool sellLagerbestandAutomatic;

	// Token: 0x04002472 RID: 9330
	public textScript tS_;

	// Token: 0x04002473 RID: 9331
	public settingsScript settings_;

	// Token: 0x04002474 RID: 9332
	public mapScript mapScript_;

	// Token: 0x04002475 RID: 9333
	public unlockScript unlock_;

	// Token: 0x04002476 RID: 9334
	private pickCharacterScript pickChar_;

	// Token: 0x04002477 RID: 9335
	private pickObjectScript pickObject_;

	// Token: 0x04002478 RID: 9336
	public genres genres_;

	// Token: 0x04002479 RID: 9337
	private themes themes_;

	// Token: 0x0400247A RID: 9338
	private engineFeatures eF_;

	// Token: 0x0400247B RID: 9339
	public gameplayFeatures gF_;

	// Token: 0x0400247C RID: 9340
	public hardware hardware_;

	// Token: 0x0400247D RID: 9341
	public hardwareFeatures hardwareFeatures_;

	// Token: 0x0400247E RID: 9342
	private arbeitsmarkt arbeitsmarkt_;

	// Token: 0x0400247F RID: 9343
	public platforms platforms_;

	// Token: 0x04002480 RID: 9344
	public copyProtect copyProtect_;

	// Token: 0x04002481 RID: 9345
	public anitCheat antiCheat_;

	// Token: 0x04002482 RID: 9346
	public licences licences_;

	// Token: 0x04002483 RID: 9347
	public games games_;

	// Token: 0x04002484 RID: 9348
	public GUI_Main guiMain_;

	// Token: 0x04002485 RID: 9349
	private npcEngines npcEngines_;

	// Token: 0x04002486 RID: 9350
	private publisher publisher_;

	// Token: 0x04002487 RID: 9351
	private createCharScript cCS_;

	// Token: 0x04002488 RID: 9352
	public contractWorkMain contractWorkMain_;

	// Token: 0x04002489 RID: 9353
	public publishingOfferMain publishingOfferMain_;

	// Token: 0x0400248A RID: 9354
	private savegameScript save_;

	// Token: 0x0400248B RID: 9355
	public cameraMovementScript cmS_;

	// Token: 0x0400248C RID: 9356
	public sfxScript sfx_;

	// Token: 0x0400248D RID: 9357
	public NetworkManager manager;

	// Token: 0x0400248E RID: 9358
	public mpCalls mpCalls_;

	// Token: 0x0400248F RID: 9359
	public achiementScript achScript_;

	// Token: 0x04002490 RID: 9360
	public reviewText reviewText_;

	// Token: 0x04002491 RID: 9361
	public forschungSonstiges forschungSonstiges_;

	// Token: 0x04002492 RID: 9362
	public roomDataScript rdS_;

	// Token: 0x04002493 RID: 9363
	public GameObject[] arrayCharacters;

	// Token: 0x04002494 RID: 9364
	public List<GameObject> arrayCharactersForDoors = new List<GameObject>();

	// Token: 0x04002495 RID: 9365
	public GameObject[] arrayObjects;

	// Token: 0x04002496 RID: 9366
	public GameObject[] arrayRooms;

	// Token: 0x04002497 RID: 9367
	public GameObject[] arrayRobots;

	// Token: 0x04002498 RID: 9368
	public GameObject[] arrayMuell;

	// Token: 0x04002499 RID: 9369
	public objectScript[] arrayObjectScripts;

	// Token: 0x0400249A RID: 9370
	public characterScript[] arrayCharactersScripts;

	// Token: 0x0400249B RID: 9371
	public GameObject[] miscParticlePrefabs;

	// Token: 0x0400249C RID: 9372
	public GameObject[] miscGamePrefabs;

	// Token: 0x0400249D RID: 9373
	public Material[] specialMaterials;

	// Token: 0x0400249E RID: 9374
	public Material[] floorMaterials;

	// Token: 0x0400249F RID: 9375
	public Texture2D[] specialTextures;

	// Token: 0x040024A0 RID: 9376
	public Shader[] shaders;

	// Token: 0x040024A1 RID: 9377
	public float objectRotation;

	// Token: 0x040024A2 RID: 9378
	public GameObject pickedObject;

	// Token: 0x040024A3 RID: 9379
	public List<GameObject> pickedChars = new List<GameObject>();

	// Token: 0x040024A4 RID: 9380
	public bool snapObject;

	// Token: 0x040024A5 RID: 9381
	public bool snapRotation;

	// Token: 0x040024A6 RID: 9382
	public GameObject cameraPersonalPhoto;

	// Token: 0x040024A7 RID: 9383
	public VectorLine roomLine;

	// Token: 0x040024A8 RID: 9384
	public float weatherTimer;

	// Token: 0x040024A9 RID: 9385
	public int anzSprechblasen;

	// Token: 0x040024AA RID: 9386
	public LayerMask layerMask_Floor;

	// Token: 0x040024AB RID: 9387
	public const int taskID_taskForschung = 10;

	// Token: 0x040024AC RID: 9388
	public const int taskID_taskEngine = 20;

	// Token: 0x040024AD RID: 9389
	public const int taskID_taskUpdate = 30;

	// Token: 0x040024AE RID: 9390
	public const int taskID_taskGame = 40;

	// Token: 0x040024AF RID: 9391
	public const int taskID_taskF2PUpdate = 50;

	// Token: 0x040024B0 RID: 9392
	public const int taskID_taskMarketing = 60;

	// Token: 0x040024B1 RID: 9393
	public const int taskID_taskMarketingSpezial = 70;

	// Token: 0x040024B2 RID: 9394
	public const int taskID_taskMitarbeitersuche = 80;

	// Token: 0x040024B3 RID: 9395
	public const int taskID_taskTraining = 90;

	// Token: 0x040024B4 RID: 9396
	public const int taskID_taskSpielbericht = 100;

	// Token: 0x040024B5 RID: 9397
	public const int taskID_taskGameplayVerbessern = 110;

	// Token: 0x040024B6 RID: 9398
	public const int taskID_taskBugfixing = 120;

	// Token: 0x040024B7 RID: 9399
	public const int taskID_taskGrafikVerbessern = 130;

	// Token: 0x040024B8 RID: 9400
	public const int taskID_taskSoundVerbessern = 140;

	// Token: 0x040024B9 RID: 9401
	public const int taskID_taskAnimationVerbessern = 150;

	// Token: 0x040024BA RID: 9402
	public const int taskID_taskProduction = 160;

	// Token: 0x040024BB RID: 9403
	public const int taskID_taskArcadeProduction = 170;

	// Token: 0x040024BC RID: 9404
	public const int taskID_taskKonsole = 180;

	// Token: 0x040024BD RID: 9405
	public const int taskID_taskFankampagne = 190;

	// Token: 0x040024BE RID: 9406
	public const int taskID_taskSupport = 200;

	// Token: 0x040024BF RID: 9407
	public const int taskID_taskContractWork = 210;

	// Token: 0x040024C0 RID: 9408
	public const int taskID_taskContractWait = 220;

	// Token: 0x040024C1 RID: 9409
	public const int taskID_taskMarktforschung = 230;

	// Token: 0x040024C2 RID: 9410
	public const int taskID_taskPolishing = 240;

	// Token: 0x040024C3 RID: 9411
	public const int taskID_taskUnterstuetzen = 250;

	// Token: 0x040024C4 RID: 9412
	public const int taskID_taskWait = 260;

	// Token: 0x040024C5 RID: 9413
	public const int taskID_taskFanshop = 270;

	// Token: 0x040024C6 RID: 9414
	public bool findObjects = true;

	// Token: 0x040024C7 RID: 9415
	public bool findRooms = true;

	// Token: 0x040024C8 RID: 9416
	public bool findCharacters = true;

	// Token: 0x040024C9 RID: 9417
	public bool findMuell = true;

	// Token: 0x040024CA RID: 9418
	public bool findRobots = true;

	// Token: 0x040024CB RID: 9419
	public bool officeLoaded;

	// Token: 0x040024CC RID: 9420
	private bool contendIsLoaded;

	// Token: 0x040024CD RID: 9421
	private float updateKiTimer;

	// Token: 0x040024CE RID: 9422
	private float updateUnkorrekterRoom;

	// Token: 0x040024CF RID: 9423
	public int autoSaveInterval = -1;

	// Token: 0x040024D0 RID: 9424
	public float lauf = 0.2f;

	// Token: 0x040024D1 RID: 9425
	private float filterTimer;

	// Token: 0x040024D2 RID: 9426
	public float carSpawnTimer;

	// Token: 0x040024D3 RID: 9427
	public List<GameObject> carList = new List<GameObject>();

	// Token: 0x040024D4 RID: 9428
	public GameObject[] carPrefabs;

	// Token: 0x040024D5 RID: 9429
	public List<Transform> listColliderLayer = new List<Transform>();
}
