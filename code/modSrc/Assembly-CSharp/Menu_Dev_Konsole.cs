using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200015D RID: 349
public class Menu_Dev_Konsole : MonoBehaviour
{
	// Token: 0x06000CEA RID: 3306 RVA: 0x0008CDC8 File Offset: 0x0008AFC8
	private void Start()
	{
		this.FindScripts();
		this.uiObjects[51].GetComponent<Slider>().value = 128f;
	}

	// Token: 0x06000CEB RID: 3307 RVA: 0x0008CDE8 File Offset: 0x0008AFE8
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindGameObjectWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
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
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.themes_)
		{
			this.themes_ = this.main_.GetComponent<themes>();
		}
		if (!this.licences_)
		{
			this.licences_ = this.main_.GetComponent<licences>();
		}
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
		if (!this.cmS_)
		{
			this.cmS_ = GameObject.Find("CamMovement").GetComponent<cameraMovementScript>();
		}
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
		}
		if (!this.forschungSonstiges_)
		{
			this.forschungSonstiges_ = this.main_.GetComponent<forschungSonstiges>();
		}
		if (!this.gF_)
		{
			this.gF_ = this.main_.GetComponent<gameplayFeatures>();
		}
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
		if (!this.platforms_)
		{
			this.platforms_ = this.main_.GetComponent<platforms>();
		}
		if (!this.hardware_)
		{
			this.hardware_ = this.main_.GetComponent<hardware>();
		}
		if (!this.hardwareFeatures_)
		{
			this.hardwareFeatures_ = this.main_.GetComponent<hardwareFeatures>();
		}
	}

	// Token: 0x06000CEC RID: 3308 RVA: 0x0008CFFE File Offset: 0x0008B1FE
	private void OnEnable()
	{
		this.FindScripts();
		this.cmS_.disableMovement = true;
	}

	// Token: 0x06000CED RID: 3309 RVA: 0x0008D012 File Offset: 0x0008B212
	private void OnDisable()
	{
		this.FindScripts();
		this.cmS_.disableMovement = false;
	}

	// Token: 0x06000CEE RID: 3310 RVA: 0x0008D026 File Offset: 0x0008B226
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x06000CEF RID: 3311 RVA: 0x0008D044 File Offset: 0x0008B244
	public void Init(roomScript roomScript_, int platformTyp_)
	{
		if (platformTyp_ != this.platformTyp)
		{
			this.ClearData();
		}
		this.platformTyp = platformTyp_;
		this.FindScripts();
		this.rS_ = roomScript_;
		this.InitDropdowns();
		this.Init_KonsolenFeatures();
		if (this.component_cpu == -1)
		{
			this.component_cpu = this.FindBestComponents(0);
		}
		if (this.component_gfx == -1)
		{
			this.component_gfx = this.FindBestComponents(1);
		}
		if (this.component_ram == -1)
		{
			this.component_ram = this.FindBestComponents(2);
		}
		if (this.component_hdd == -1)
		{
			this.component_hdd = this.FindBestComponents(3);
		}
		if (this.component_sfx == -1)
		{
			this.component_sfx = this.FindBestComponents(4);
		}
		if (this.component_disc == -1)
		{
			this.component_disc = this.FindBestComponents(6);
		}
		if (this.component_case == -1)
		{
			this.component_case = this.FindBestComponents(8);
		}
		if (platformTyp_ == 1)
		{
			if (this.component_cooling == -1)
			{
				this.component_cooling = this.FindBestComponents(5);
			}
			this.uiObjects[23].SetActive(false);
			this.uiObjects[24].GetComponent<Button>().interactable = true;
			if (this.component_controller == -1)
			{
				this.component_controller = this.FindBestComponents(7);
			}
			this.uiObjects[43].SetActive(false);
			this.uiObjects[44].GetComponent<Button>().interactable = true;
			if (this.anzController <= 0)
			{
				this.anzController = 1;
			}
		}
		else
		{
			this.component_cooling = -1;
			this.uiObjects[23].SetActive(true);
			this.uiObjects[24].GetComponent<Button>().interactable = false;
			this.component_controller = -1;
			this.uiObjects[43].SetActive(true);
			this.uiObjects[44].GetComponent<Button>().interactable = false;
		}
		if (platformTyp_ == 2)
		{
			if (this.component_monitor == -1)
			{
				this.component_monitor = this.FindBestComponents(9);
			}
			this.uiObjects[21].SetActive(false);
			this.uiObjects[22].GetComponent<Button>().interactable = true;
			this.anzController = 0;
		}
		else
		{
			this.component_monitor = -1;
			this.uiObjects[21].SetActive(true);
			this.uiObjects[22].GetComponent<Button>().interactable = false;
		}
		if (platformTyp_ == 1)
		{
			this.uiObjects[46].GetComponent<Image>().sprite = this.guiMain_.uiSprites[42];
		}
		if (platformTyp_ == 2)
		{
			this.uiObjects[46].GetComponent<Image>().sprite = this.guiMain_.uiSprites[43];
		}
		this.SLIDER_Color();
		this.SLIDER_Saturation();
		this.SetLeitenderTechniker(null, false);
		this.UpdateGUI();
		this.OpenSide(0);
	}

	// Token: 0x06000CF0 RID: 3312 RVA: 0x0008D2D8 File Offset: 0x0008B4D8
	private void ClearData()
	{
		this.anzController = 0;
		this.gameID = -1;
		this.component_cpu = -1;
		this.component_gfx = -1;
		this.component_ram = -1;
		this.component_hdd = -1;
		this.component_sfx = -1;
		this.component_cooling = -1;
		this.component_disc = -1;
		this.component_controller = -1;
		this.component_case = -1;
		this.component_monitor = -1;
		this.leitenderTechniker = null;
		for (int i = 1; i < this.hwFeatures.Length; i++)
		{
			this.hwFeatures[i] = false;
		}
	}

	// Token: 0x06000CF1 RID: 3313 RVA: 0x0008D35C File Offset: 0x0008B55C
	public void UpdateGUI()
	{
		this.uiObjects[3].GetComponent<Image>().sprite = this.platforms_.typSprites[this.platformTyp];
		this.UpdateKomponenten();
		this.InitConsoleColors();
		this.uiObjects[38].GetComponent<Text>().text = this.GetTechLevel().ToString();
		this.uiObjects[41].GetComponent<Text>().text = this.tS_.GetText(1612) + ": <b><color=blue>" + this.mS_.GetMoney((long)this.GetPerformance(), false) + "</color></b>";
		this.uiObjects[39].GetComponent<Text>().text = this.mS_.GetMoney((long)this.GetDevCosts(), true);
		this.uiObjects[40].GetComponent<Text>().text = this.mS_.GetMoney((long)this.GetWorkPoints(), false);
		this.uiObjects[47].GetComponent<Image>().sprite = this.platforms_.complexSprites[this.GetKomplexitaet()];
		for (int i = 0; i < this.uiObjects[45].transform.childCount; i++)
		{
			if (i < this.anzController)
			{
				this.uiObjects[45].transform.GetChild(i).GetComponent<Image>().color = Color.white;
			}
			else
			{
				this.uiObjects[45].transform.GetChild(i).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.2f);
			}
		}
		this.uiObjects[49].GetComponent<Image>().sprite = this.hardware_.GetTypPic(this.component_case);
		if (this.gameID != -1)
		{
			GameObject gameObject = GameObject.Find("GAME_" + this.gameID.ToString());
			if (gameObject)
			{
				gameScript component = gameObject.GetComponent<gameScript>();
				this.uiObjects[52].GetComponent<Text>().text = component.GetNameWithTag();
			}
		}
		else
		{
			this.uiObjects[52].GetComponent<Text>().text = this.tS_.GetText(1615);
		}
		if (this.hardwareFeatures_.IsErforscht(0))
		{
			this.uiObjects[58].SetActive(false);
		}
		else
		{
			this.uiObjects[58].SetActive(true);
			this.uiObjects[53].GetComponent<Toggle>().isOn = false;
		}
		this.UpdateConsoleColor();
	}

	// Token: 0x06000CF2 RID: 3314 RVA: 0x0008D5D0 File Offset: 0x0008B7D0
	private int FindBestComponents(int compTyp)
	{
		int result = -1;
		int num = -1;
		for (int i = 0; i < this.hardware_.hardware_TYP.Length; i++)
		{
			if (this.hardware_.hardware_UNLOCK[i] && this.hardware_.hardware_RES_POINTS_LEFT[i] <= 0f && this.hardware_.hardware_TYP[i] == compTyp && ((this.platformTyp == 1 && !this.hardware_.hardware_ONLYHANDHELD[i]) || (this.platformTyp == 2 && !this.hardware_.hardware_ONLYSTATIONARY[i])) && num <= this.hardware_.hardware_RES_POINTS[i])
			{
				result = i;
				num = this.hardware_.hardware_RES_POINTS[i];
			}
		}
		return result;
	}

	// Token: 0x06000CF3 RID: 3315 RVA: 0x0008D684 File Offset: 0x0008B884
	private void UpdateKomponenten()
	{
		if (this.component_cpu != -1)
		{
			this.uiObjects[5].GetComponent<Text>().text = this.hardware_.GetName(this.component_cpu);
			this.uiObjects[6].GetComponent<Text>().text = this.hardware_.hardware_TECH[this.component_cpu].ToString();
		}
		else
		{
			this.uiObjects[5].GetComponent<Text>().text = "";
			this.uiObjects[6].GetComponent<Text>().text = "";
		}
		if (this.component_gfx != -1)
		{
			this.uiObjects[9].GetComponent<Text>().text = this.hardware_.GetName(this.component_gfx);
			this.uiObjects[10].GetComponent<Text>().text = this.hardware_.hardware_TECH[this.component_gfx].ToString();
		}
		else
		{
			this.uiObjects[9].GetComponent<Text>().text = "";
			this.uiObjects[10].GetComponent<Text>().text = "";
		}
		if (this.component_ram != -1)
		{
			this.uiObjects[26].GetComponent<Text>().text = this.hardware_.GetName(this.component_ram);
			this.uiObjects[27].GetComponent<Text>().text = this.hardware_.hardware_TECH[this.component_ram].ToString();
		}
		else
		{
			this.uiObjects[26].GetComponent<Text>().text = "";
			this.uiObjects[27].GetComponent<Text>().text = "";
		}
		if (this.component_hdd != -1)
		{
			this.uiObjects[28].GetComponent<Text>().text = this.hardware_.GetName(this.component_hdd);
			this.uiObjects[29].GetComponent<Text>().text = this.hardware_.hardware_TECH[this.component_hdd].ToString();
		}
		else
		{
			this.uiObjects[28].GetComponent<Text>().text = "";
			this.uiObjects[29].GetComponent<Text>().text = "";
		}
		if (this.component_sfx != -1)
		{
			this.uiObjects[30].GetComponent<Text>().text = this.hardware_.GetName(this.component_sfx);
			this.uiObjects[31].GetComponent<Text>().text = this.hardware_.hardware_TECH[this.component_sfx].ToString();
		}
		else
		{
			this.uiObjects[30].GetComponent<Text>().text = "";
			this.uiObjects[31].GetComponent<Text>().text = "";
		}
		if (this.component_cooling != -1)
		{
			this.uiObjects[32].GetComponent<Text>().text = this.hardware_.GetName(this.component_cooling);
			this.uiObjects[33].GetComponent<Text>().text = this.hardware_.hardware_TECH[this.component_cooling].ToString();
		}
		else
		{
			this.uiObjects[32].GetComponent<Text>().text = "";
			this.uiObjects[33].GetComponent<Text>().text = "";
		}
		if (this.component_disc != -1)
		{
			this.uiObjects[34].GetComponent<Text>().text = this.hardware_.GetName(this.component_disc);
			this.uiObjects[35].GetComponent<Text>().text = this.hardware_.hardware_TECH[this.component_disc].ToString();
		}
		else
		{
			this.uiObjects[34].GetComponent<Text>().text = "";
			this.uiObjects[35].GetComponent<Text>().text = "";
		}
		if (this.component_monitor != -1)
		{
			this.uiObjects[36].GetComponent<Text>().text = this.hardware_.GetName(this.component_monitor);
			this.uiObjects[37].GetComponent<Text>().text = this.hardware_.hardware_TECH[this.component_monitor].ToString();
		}
		else
		{
			this.uiObjects[36].GetComponent<Text>().text = "";
			this.uiObjects[37].GetComponent<Text>().text = "";
		}
		if (this.component_controller != -1)
		{
			this.uiObjects[42].GetComponent<Text>().text = this.hardware_.GetName(this.component_controller);
		}
		else
		{
			this.uiObjects[42].GetComponent<Text>().text = "";
		}
		if (this.component_case != -1)
		{
			this.uiObjects[48].GetComponent<Text>().text = this.hardware_.GetName(this.component_case);
			return;
		}
		this.uiObjects[48].GetComponent<Text>().text = "";
	}

	// Token: 0x06000CF4 RID: 3316 RVA: 0x0008DB84 File Offset: 0x0008BD84
	private void InitConsoleColors()
	{
		for (int i = 0; i < this.uiObjects[2].transform.childCount; i++)
		{
			this.uiObjects[2].transform.GetChild(i).GetComponent<Image>().color = this.platforms_.playerPlatformColors[i];
		}
	}

	// Token: 0x06000CF5 RID: 3317 RVA: 0x0008DBDC File Offset: 0x0008BDDC
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[317]);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000CF6 RID: 3318 RVA: 0x0008DC13 File Offset: 0x0008BE13
	public void BUTTON_RandomName()
	{
		this.sfx_.PlaySound(3, true);
		this.uiObjects[4].GetComponent<InputField>().text = this.tS_.GetPlatformName();
	}

	// Token: 0x06000CF7 RID: 3319 RVA: 0x0008DC40 File Offset: 0x0008BE40
	public void BUTTON_Komponente(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[319]);
		this.guiMain_.uiObjects[319].GetComponent<Menu_Dev_KonsoleComponent>().Init(i, this.platformTyp);
	}

	// Token: 0x06000CF8 RID: 3320 RVA: 0x0008DC98 File Offset: 0x0008BE98
	public void BUTTON_Game()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[320]);
	}

	// Token: 0x06000CF9 RID: 3321 RVA: 0x0008DCC4 File Offset: 0x0008BEC4
	public void BUTTON_AnzahlController(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.anzController += i;
		if (this.anzController < 1)
		{
			this.anzController = 1;
		}
		if (this.anzController > 4)
		{
			this.anzController = 4;
		}
		if (this.platformTyp == 2)
		{
			this.anzController = 0;
		}
		this.UpdateGUI();
	}

	// Token: 0x06000CFA RID: 3322 RVA: 0x0008DD24 File Offset: 0x0008BF24
	public void BUTTON_Start()
	{
		this.sfx_.PlaySound(3, true);
		if (this.mS_.NotEnoughMoney(this.GetDevCosts()))
		{
			this.guiMain_.ShowNoMoney();
			return;
		}
		if (this.uiObjects[4].GetComponent<InputField>().text.Length <= 0)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1624), false);
			this.OpenSide(0);
			return;
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("Platform");
		platformScript platformScript;
		if (array.Length != 0)
		{
			for (int i = 0; i < array.Length; i++)
			{
				platformScript = array[i].GetComponent<platformScript>();
				if (platformScript && platformScript.GetName() == this.uiObjects[4].GetComponent<InputField>().text)
				{
					this.guiMain_.MessageBox(this.tS_.GetText(1625), false);
					this.OpenSide(0);
					return;
				}
			}
		}
		this.mS_.Pay((long)this.GetDevCosts(), 22);
		platformScript = this.platforms_.CreatePlatform();
		platformScript.myID = this.mS_.GetNewID();
		platformScript.ownerID = this.mS_.myID;
		platformScript.typ = this.platformTyp;
		platformScript.myName = this.uiObjects[4].GetComponent<InputField>().text;
		platformScript.tech = this.GetTechLevel();
		platformScript.erfahrung = 5;
		platformScript.isUnlocked = false;
		platformScript.inBesitz = true;
		platformScript.complex = this.GetKomplexitaet();
		platformScript.internet = this.uiObjects[53].GetComponent<Toggle>().isOn;
		platformScript.devPointsStart = (float)this.GetWorkPoints();
		platformScript.devPoints = platformScript.devPointsStart;
		platformScript.dev_costs = this.GetGameDevCosts();
		platformScript.gameID = this.gameID;
		platformScript.anzController = this.anzController;
		platformScript.conHueShift = this.conHueShift;
		platformScript.conSaturation = this.conSaturation;
		platformScript.component_cpu = this.component_cpu;
		platformScript.component_gfx = this.component_gfx;
		platformScript.component_ram = this.component_ram;
		platformScript.component_hdd = this.component_hdd;
		platformScript.component_sfx = this.component_sfx;
		platformScript.component_cooling = this.component_cooling;
		platformScript.component_disc = this.component_disc;
		platformScript.component_controller = this.component_controller;
		platformScript.component_case = this.component_case;
		platformScript.component_monitor = this.component_monitor;
		platformScript.entwicklungsKosten = (long)this.GetDevCosts();
		platformScript.performancePoints = this.GetPerformance();
		for (int j = 0; j < this.hwFeatures.Length; j++)
		{
			platformScript.hwFeatures[j] = this.hwFeatures[j];
		}
		if (this.uiObjects[53].GetComponent<Toggle>().isOn)
		{
			platformScript.hwFeatures[0] = true;
		}
		if (this.component_controller != -1)
		{
			platformScript.needFeatures[0] = this.hardware_.hardware_NEED1[this.component_controller];
			platformScript.needFeatures[1] = this.hardware_.hardware_NEED2[this.component_controller];
			platformScript.needFeatures[2] = -1;
		}
		if (this.component_monitor != -1)
		{
			platformScript.needFeatures[0] = this.hardware_.hardware_NEED1[this.component_monitor];
			platformScript.needFeatures[1] = this.hardware_.hardware_NEED2[this.component_monitor];
			platformScript.needFeatures[2] = -1;
		}
		for (int k = 0; k < platformScript.needFeatures.Length; k++)
		{
			if (platformScript.needFeatures[k] == 0)
			{
				platformScript.needFeatures[k] = -1;
			}
		}
		platformScript.Init();
		taskKonsole taskKonsole = this.guiMain_.AddTask_Konsole();
		taskKonsole.Init(false);
		taskKonsole.konsoleID = platformScript.myID;
		taskKonsole.pS_ = platformScript;
		if (this.leitenderTechniker)
		{
			taskKonsole.leitenderTechnikerID = this.leitenderTechniker.myID;
			taskKonsole.techniker_ = this.leitenderTechniker;
		}
		GameObject gameObject = GameObject.Find("Room_" + this.rS_.myID.ToString());
		if (gameObject)
		{
			gameObject.GetComponent<roomScript>().taskID = taskKonsole.myID;
		}
		this.ClearData();
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000CFB RID: 3323 RVA: 0x0008E152 File Offset: 0x0008C352
	public void SLIDER_Color()
	{
		this.conHueShift = this.uiObjects[50].GetComponent<Slider>().value;
		this.UpdateConsoleColor();
	}

	// Token: 0x06000CFC RID: 3324 RVA: 0x0008E173 File Offset: 0x0008C373
	public void SLIDER_Saturation()
	{
		this.conSaturation = this.uiObjects[51].GetComponent<Slider>().value;
		this.UpdateConsoleColor();
	}

	// Token: 0x06000CFD RID: 3325 RVA: 0x0008E194 File Offset: 0x0008C394
	public void UpdateConsoleColor()
	{
		this.uiObjects[49].GetComponent<Image>().color = new Color(this.conHueShift / 255f, this.conSaturation / 255f, 0.5f, 1f);
	}

	// Token: 0x06000CFE RID: 3326 RVA: 0x0008E1D0 File Offset: 0x0008C3D0
	public void NextSide(int i)
	{
		this.seite += i;
		if (this.seite < 0)
		{
			this.seite = 0;
		}
		if (this.seite > 2)
		{
			this.seite = 2;
		}
		this.OpenSide(this.seite);
		this.sfx_.PlaySound(3, true);
	}

	// Token: 0x06000CFF RID: 3327 RVA: 0x0008E224 File Offset: 0x0008C424
	public void OpenSide(int i)
	{
		this.sfx_.PlaySound(3, false);
		for (int j = 0; j < this.uiSides.Length; j++)
		{
			if (this.uiSides[j].activeSelf && j != i)
			{
				this.uiSides[j].SetActive(false);
			}
		}
		this.seite = i;
		for (int k = 0; k < this.uiObjects[0].transform.childCount; k++)
		{
			this.uiObjects[0].transform.GetChild(k).GetComponent<Image>().color = Color.white;
		}
		this.uiObjects[0].transform.GetChild(i).GetComponent<Image>().color = Color.grey;
		if (!this.uiSides[i].activeSelf)
		{
			this.uiSides[i].SetActive(true);
		}
	}

	// Token: 0x06000D00 RID: 3328 RVA: 0x0008E2F7 File Offset: 0x0008C4F7
	public void SetGame(int id_)
	{
		this.gameID = id_;
		this.UpdateGUI();
	}

	// Token: 0x06000D01 RID: 3329 RVA: 0x0008E308 File Offset: 0x0008C508
	public void SetComponent(int typ_, int id_)
	{
		switch (typ_)
		{
		case 0:
			this.component_cpu = id_;
			break;
		case 1:
			this.component_gfx = id_;
			break;
		case 2:
			this.component_ram = id_;
			break;
		case 3:
			this.component_hdd = id_;
			break;
		case 4:
			this.component_sfx = id_;
			break;
		case 5:
			this.component_cooling = id_;
			break;
		case 6:
			this.component_disc = id_;
			break;
		case 7:
			this.component_controller = id_;
			break;
		case 8:
			this.component_case = id_;
			break;
		case 9:
			this.component_monitor = id_;
			break;
		}
		this.UpdateGUI();
	}

	// Token: 0x06000D02 RID: 3330 RVA: 0x0008E3A4 File Offset: 0x0008C5A4
	private int GetKomplexitaet()
	{
		float num = 0f;
		if (this.component_cpu != -1)
		{
			num += (float)this.hardware_.hardware_TECH[this.component_cpu];
		}
		if (this.component_gfx != -1)
		{
			num += (float)this.hardware_.hardware_TECH[this.component_gfx];
		}
		if (this.component_ram != -1)
		{
			num += (float)this.hardware_.hardware_TECH[this.component_ram];
		}
		if (this.component_hdd != -1)
		{
			num += (float)this.hardware_.hardware_TECH[this.component_hdd];
		}
		if (this.component_sfx != -1)
		{
			num += (float)this.hardware_.hardware_TECH[this.component_sfx];
		}
		if (this.component_cooling != -1)
		{
			num += (float)this.hardware_.hardware_TECH[this.component_cooling];
		}
		if (this.component_disc != -1)
		{
			num += (float)this.hardware_.hardware_TECH[this.component_disc];
		}
		if (this.component_monitor != -1)
		{
			num += (float)this.hardware_.hardware_TECH[this.component_monitor];
		}
		num /= 7f;
		int num2 = Mathf.RoundToInt(num);
		int num3 = 0;
		if (this.component_cpu != -1)
		{
			num3 += Mathf.Abs(num2 - this.hardware_.hardware_TECH[this.component_cpu]);
		}
		if (this.component_gfx != -1)
		{
			num3 += Mathf.Abs(num2 - this.hardware_.hardware_TECH[this.component_gfx]);
		}
		if (this.component_ram != -1)
		{
			num3 += Mathf.Abs(num2 - this.hardware_.hardware_TECH[this.component_ram]);
		}
		if (this.component_hdd != -1)
		{
			num3 += Mathf.Abs(num2 - this.hardware_.hardware_TECH[this.component_hdd]);
		}
		if (this.component_sfx != -1)
		{
			num3 += Mathf.Abs(num2 - this.hardware_.hardware_TECH[this.component_sfx]);
		}
		if (this.component_cooling != -1)
		{
			num3 += Mathf.Abs(num2 - this.hardware_.hardware_TECH[this.component_cooling]);
		}
		if (this.component_disc != -1)
		{
			num3 += Mathf.Abs(num2 - this.hardware_.hardware_TECH[this.component_disc]);
		}
		if (this.component_monitor != -1)
		{
			num3 += Mathf.Abs(num2 - this.hardware_.hardware_TECH[this.component_monitor]);
		}
		if (num3 >= 0 && num3 < 4)
		{
			return 0;
		}
		if (num3 >= 4 && num3 < 8)
		{
			return 1;
		}
		if (num3 >= 8)
		{
			return 2;
		}
		return 0;
	}

	// Token: 0x06000D03 RID: 3331 RVA: 0x0008E604 File Offset: 0x0008C804
	private int GetTechLevel()
	{
		int num = 99;
		if (this.component_cpu != -1 && this.hardware_.hardware_TECH[this.component_cpu] < num)
		{
			num = this.hardware_.hardware_TECH[this.component_cpu];
		}
		if (this.component_gfx != -1 && this.hardware_.hardware_TECH[this.component_gfx] < num)
		{
			num = this.hardware_.hardware_TECH[this.component_gfx];
		}
		if (this.component_ram != -1 && this.hardware_.hardware_TECH[this.component_ram] < num)
		{
			num = this.hardware_.hardware_TECH[this.component_ram];
		}
		if (this.component_hdd != -1 && this.hardware_.hardware_TECH[this.component_hdd] < num)
		{
			num = this.hardware_.hardware_TECH[this.component_hdd];
		}
		if (this.component_sfx != -1 && this.hardware_.hardware_TECH[this.component_sfx] < num)
		{
			num = this.hardware_.hardware_TECH[this.component_sfx];
		}
		if (this.component_cooling != -1 && this.hardware_.hardware_TECH[this.component_cooling] < num)
		{
			num = this.hardware_.hardware_TECH[this.component_cooling];
		}
		if (this.component_disc != -1 && this.hardware_.hardware_TECH[this.component_disc] < num)
		{
			num = this.hardware_.hardware_TECH[this.component_disc];
		}
		if (this.component_monitor != -1 && this.hardware_.hardware_TECH[this.component_monitor] < num)
		{
			num = this.hardware_.hardware_TECH[this.component_monitor];
		}
		return num;
	}

	// Token: 0x06000D04 RID: 3332 RVA: 0x0008E7A0 File Offset: 0x0008C9A0
	private int GetDevCosts()
	{
		int num = 0;
		if (this.component_cpu != -1)
		{
			num += this.hardware_.GetDevCosts(this.component_cpu);
		}
		if (this.component_gfx != -1)
		{
			num += this.hardware_.GetDevCosts(this.component_gfx);
		}
		if (this.component_ram != -1)
		{
			num += this.hardware_.GetDevCosts(this.component_ram);
		}
		if (this.component_hdd != -1)
		{
			num += this.hardware_.GetDevCosts(this.component_hdd);
		}
		if (this.component_sfx != -1)
		{
			num += this.hardware_.GetDevCosts(this.component_sfx);
		}
		if (this.component_cooling != -1)
		{
			num += this.hardware_.GetDevCosts(this.component_cooling);
		}
		if (this.component_disc != -1)
		{
			num += this.hardware_.GetDevCosts(this.component_disc);
		}
		if (this.component_controller != -1)
		{
			num += this.hardware_.GetDevCosts(this.component_controller);
		}
		if (this.component_case != -1)
		{
			num += this.hardware_.GetDevCosts(this.component_case);
		}
		if (this.component_monitor != -1)
		{
			num += this.hardware_.GetDevCosts(this.component_monitor);
		}
		for (int i = 1; i < this.hwFeatures.Length; i++)
		{
			if (this.hwFeatures[i])
			{
				num += this.hardwareFeatures_.GetDevCosts(i);
			}
		}
		float num2 = (float)num;
		switch (this.GetKomplexitaet())
		{
		case 1:
			num += Mathf.RoundToInt(num2 * 0.25f);
			break;
		case 2:
			num += Mathf.RoundToInt(num2 * 0.5f);
			break;
		}
		return num;
	}

	// Token: 0x06000D05 RID: 3333 RVA: 0x0008E93C File Offset: 0x0008CB3C
	private int GetWorkPoints()
	{
		int num = 3000;
		if (this.component_cpu != -1)
		{
			num += this.hardware_.GetWorkPoints(this.component_cpu);
		}
		if (this.component_gfx != -1)
		{
			num += this.hardware_.GetWorkPoints(this.component_gfx);
		}
		if (this.component_ram != -1)
		{
			num += this.hardware_.GetWorkPoints(this.component_ram);
		}
		if (this.component_hdd != -1)
		{
			num += this.hardware_.GetWorkPoints(this.component_hdd);
		}
		if (this.component_sfx != -1)
		{
			num += this.hardware_.GetWorkPoints(this.component_sfx);
		}
		if (this.component_cooling != -1)
		{
			num += this.hardware_.GetWorkPoints(this.component_cooling);
		}
		if (this.component_disc != -1)
		{
			num += this.hardware_.GetWorkPoints(this.component_disc);
		}
		if (this.component_controller != -1)
		{
			num += this.hardware_.GetWorkPoints(this.component_controller);
		}
		if (this.component_case != -1)
		{
			num += this.hardware_.GetWorkPoints(this.component_case);
		}
		if (this.component_monitor != -1)
		{
			num += this.hardware_.GetWorkPoints(this.component_monitor);
		}
		for (int i = 1; i < this.hwFeatures.Length; i++)
		{
			if (this.hwFeatures[i])
			{
				num += this.hardwareFeatures_.GetWorkPoints(i);
			}
		}
		float num2 = (float)num;
		switch (this.GetKomplexitaet())
		{
		case 1:
			num += Mathf.RoundToInt(num2 * 0.5f);
			break;
		case 2:
			num += Mathf.RoundToInt(num2 * 1f);
			break;
		}
		return num;
	}

	// Token: 0x06000D06 RID: 3334 RVA: 0x0008EADC File Offset: 0x0008CCDC
	private int GetPerformance()
	{
		int num = 0;
		if (this.component_cpu != -1)
		{
			num += this.hardware_.GetPerformance(this.component_cpu);
		}
		if (this.component_gfx != -1)
		{
			num += this.hardware_.GetPerformance(this.component_gfx);
		}
		if (this.component_ram != -1)
		{
			num += this.hardware_.GetPerformance(this.component_ram);
		}
		if (this.component_hdd != -1)
		{
			num += this.hardware_.GetPerformance(this.component_hdd);
		}
		if (this.component_sfx != -1)
		{
			num += this.hardware_.GetPerformance(this.component_sfx);
		}
		if (this.component_cooling != -1)
		{
			num += this.hardware_.GetPerformance(this.component_cooling);
		}
		if (this.component_disc != -1)
		{
			num += this.hardware_.GetPerformance(this.component_disc);
		}
		int num2 = this.component_controller;
		int num3 = this.component_case;
		if (this.component_monitor != -1)
		{
			num += this.hardware_.GetPerformance(this.component_monitor);
		}
		return num;
	}

	// Token: 0x06000D07 RID: 3335 RVA: 0x0008EBE8 File Offset: 0x0008CDE8
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[54].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(6));
		this.uiObjects[54].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[54].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[54].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06000D08 RID: 3336 RVA: 0x0008EC74 File Offset: 0x0008CE74
	private void Init_KonsolenFeatures()
	{
		this.FindScripts();
		if (this.hwFeatures.Length == 0)
		{
			this.hwFeatures = new bool[this.hardwareFeatures_.hardFeat_UNLOCK.Length];
		}
		for (int i = 0; i < this.uiObjects[55].transform.childCount; i++)
		{
			UnityEngine.Object.Destroy(this.uiObjects[55].transform.GetChild(i).gameObject);
		}
		for (int j = 1; j < this.hardwareFeatures_.hardFeat_UNLOCK.Length; j++)
		{
			if (this.hardwareFeatures_.hardFeat_UNLOCK[j] && this.hardwareFeatures_.IsErforscht(j))
			{
				string text = this.hardwareFeatures_.GetName(j);
				text = text.ToLower();
				if (this.uiObjects[56].GetComponent<InputField>().text.Length <= 0 || text.Contains(this.searchStringA.ToLower()))
				{
					Item_DevKonsole_HardwareFeature component = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[55].transform).GetComponent<Item_DevKonsole_HardwareFeature>();
					component.myID = j;
					component.hardwareFeatures_ = this.hardwareFeatures_;
					component.mS_ = this.mS_;
					component.tS_ = this.tS_;
					component.sfx_ = this.sfx_;
					component.guiMain_ = this.guiMain_;
					component.menu_ = this;
				}
			}
		}
		this.DROPDOWN_SortKonsoleneatures();
		this.guiMain_.KeinEintrag(this.uiObjects[55], this.uiObjects[57]);
	}

	// Token: 0x06000D09 RID: 3337 RVA: 0x0008EE10 File Offset: 0x0008D010
	public void DROPDOWN_SortKonsoleneatures()
	{
		int value = this.uiObjects[54].GetComponent<Dropdown>().value;
		PlayerPrefs.SetInt(this.uiObjects[54].name, value);
		int childCount = this.uiObjects[55].transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = this.uiObjects[55].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				Item_DevKonsole_HardwareFeature component = gameObject.GetComponent<Item_DevKonsole_HardwareFeature>();
				if (value != 0)
				{
					if (value == 1)
					{
						gameObject.name = this.hardwareFeatures_.GetDevCosts(component.myID).ToString();
					}
				}
				else
				{
					gameObject.name = this.hardwareFeatures_.GetName(component.myID);
				}
			}
		}
		if (value == 0)
		{
			this.mS_.SortChildrenByName(this.uiObjects[55]);
			return;
		}
		this.mS_.SortChildrenByFloat(this.uiObjects[55]);
	}

	// Token: 0x06000D0A RID: 3338 RVA: 0x0008EF00 File Offset: 0x0008D100
	public void BUTTON_Search()
	{
		this.sfx_.PlaySound(3, true);
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		for (int i = 0; i < this.uiObjects[55].transform.childCount; i++)
		{
			this.uiObjects[55].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.searchStringA = this.uiObjects[56].GetComponent<InputField>().text;
		this.Init_KonsolenFeatures();
	}

	// Token: 0x06000D0B RID: 3339 RVA: 0x0008EF84 File Offset: 0x0008D184
	public void TOGGLE_Internet()
	{
		if (this.hardwareFeatures_.IsErforscht(0))
		{
			this.Init_KonsolenFeatures();
		}
	}

	// Token: 0x06000D0C RID: 3340 RVA: 0x0008EF9C File Offset: 0x0008D19C
	public void BUTTON_AllFeatures()
	{
		this.sfx_.PlaySound(3, true);
		bool flag = false;
		for (int i = 1; i < this.hwFeatures.Length; i++)
		{
			if (this.hwFeatures[i])
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			for (int j = 1; j < this.hwFeatures.Length; j++)
			{
				if (this.hwFeatures[j])
				{
					this.hwFeatures[j] = false;
				}
			}
			this.UpdateGUI();
			return;
		}
		for (int k = 0; k < this.uiObjects[55].transform.childCount; k++)
		{
			this.uiObjects[55].transform.GetChild(k).GetComponent<Item_DevKonsole_HardwareFeature>().BUTTON_Click();
		}
	}

	// Token: 0x06000D0D RID: 3341 RVA: 0x0008F048 File Offset: 0x0008D248
	public void SetLeitenderTechniker(characterScript charS_, bool manuellSelectet)
	{
		if (charS_ && charS_.roomID != this.rS_.myID)
		{
			charS_ = null;
		}
		if (!charS_)
		{
			float num = 0f;
			GameObject[] array = GameObject.FindGameObjectsWithTag("Character");
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i])
				{
					characterScript component = array[i].GetComponent<characterScript>();
					if (component && component.roomID == this.rS_.myID)
					{
						if (component.s_technik > num)
						{
							num = component.s_technik;
							charS_ = component;
						}
						if (this.rS_.leitenderTechniker == component.myID)
						{
							charS_ = component;
							break;
						}
					}
				}
			}
		}
		if (!charS_)
		{
			this.uiObjects[59].GetComponent<Text>().text = "---";
			this.leitenderTechniker = null;
			this.rS_.leitenderTechniker = -1;
			return;
		}
		this.leitenderTechniker = charS_;
		if (this.rS_.leitenderTechniker != charS_.myID)
		{
			this.rS_.leitenderTechniker = -1;
		}
		if (manuellSelectet)
		{
			this.rS_.leitenderTechniker = charS_.myID;
		}
		this.uiObjects[59].GetComponent<Text>().text = charS_.myName;
	}

	// Token: 0x06000D0E RID: 3342 RVA: 0x0008F17A File Offset: 0x0008D37A
	public void BUTTON_AllePlattformen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[117]);
	}

	// Token: 0x06000D0F RID: 3343 RVA: 0x0008F1A2 File Offset: 0x0008D3A2
	public void BUTTON_KonsolenDetails()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[338]);
	}

	// Token: 0x06000D10 RID: 3344 RVA: 0x0008F1D0 File Offset: 0x0008D3D0
	public void BUTTON_LeitenderEntwickler()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[324]);
		this.guiMain_.uiObjects[324].GetComponent<Menu_LeitenderTechniker>().Init(this.rS_);
	}

	// Token: 0x06000D11 RID: 3345 RVA: 0x0008F228 File Offset: 0x0008D428
	private int GetGameDevCosts()
	{
		int num = 0;
		int num2 = 0;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Platform");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				platformScript component = array[i].GetComponent<platformScript>();
				if (component && component.tech == this.GetTechLevel())
				{
					num++;
					num2 += component.GetDevCosts();
				}
			}
		}
		int num3 = 0;
		for (int j = 0; j < this.hwFeatures.Length; j++)
		{
			if (this.hwFeatures[j])
			{
				num3 += 1000;
			}
		}
		float num4 = (float)(num2 / num);
		num4 += (float)num3;
		switch (this.GetKomplexitaet())
		{
		case 0:
			return Mathf.RoundToInt(num4 * 0.7f) / 100 * 100;
		case 1:
			return Mathf.RoundToInt(num4 * 1f) / 100 * 100;
		case 2:
			return Mathf.RoundToInt(num4 * 1.3f) / 100 * 100;
		default:
			return Mathf.RoundToInt(num4 * 1f) / 100 * 100;
		}
	}

	// Token: 0x04001169 RID: 4457
	public GameObject[] uiPrefabs;

	// Token: 0x0400116A RID: 4458
	public GameObject[] uiObjects;

	// Token: 0x0400116B RID: 4459
	public GameObject[] uiSides;

	// Token: 0x0400116C RID: 4460
	private int seite;

	// Token: 0x0400116D RID: 4461
	private GameObject main_;

	// Token: 0x0400116E RID: 4462
	private mainScript mS_;

	// Token: 0x0400116F RID: 4463
	private textScript tS_;

	// Token: 0x04001170 RID: 4464
	private GUI_Main guiMain_;

	// Token: 0x04001171 RID: 4465
	private sfxScript sfx_;

	// Token: 0x04001172 RID: 4466
	private genres genres_;

	// Token: 0x04001173 RID: 4467
	private themes themes_;

	// Token: 0x04001174 RID: 4468
	private licences licences_;

	// Token: 0x04001175 RID: 4469
	private engineFeatures eF_;

	// Token: 0x04001176 RID: 4470
	private cameraMovementScript cmS_;

	// Token: 0x04001177 RID: 4471
	private unlockScript unlock_;

	// Token: 0x04001178 RID: 4472
	private gameplayFeatures gF_;

	// Token: 0x04001179 RID: 4473
	private games games_;

	// Token: 0x0400117A RID: 4474
	private forschungSonstiges forschungSonstiges_;

	// Token: 0x0400117B RID: 4475
	private platforms platforms_;

	// Token: 0x0400117C RID: 4476
	private hardware hardware_;

	// Token: 0x0400117D RID: 4477
	private hardwareFeatures hardwareFeatures_;

	// Token: 0x0400117E RID: 4478
	private roomScript rS_;

	// Token: 0x0400117F RID: 4479
	public int platformTyp;

	// Token: 0x04001180 RID: 4480
	public int anzController;

	// Token: 0x04001181 RID: 4481
	public int gameID = -1;

	// Token: 0x04001182 RID: 4482
	public float conHueShift;

	// Token: 0x04001183 RID: 4483
	public float conSaturation;

	// Token: 0x04001184 RID: 4484
	public int component_cpu = -1;

	// Token: 0x04001185 RID: 4485
	public int component_gfx = -1;

	// Token: 0x04001186 RID: 4486
	public int component_ram = -1;

	// Token: 0x04001187 RID: 4487
	public int component_hdd = -1;

	// Token: 0x04001188 RID: 4488
	public int component_sfx = -1;

	// Token: 0x04001189 RID: 4489
	public int component_cooling = -1;

	// Token: 0x0400118A RID: 4490
	public int component_disc = -1;

	// Token: 0x0400118B RID: 4491
	public int component_controller = -1;

	// Token: 0x0400118C RID: 4492
	public int component_case = -1;

	// Token: 0x0400118D RID: 4493
	public int component_monitor = -1;

	// Token: 0x0400118E RID: 4494
	public bool[] hwFeatures;

	// Token: 0x0400118F RID: 4495
	public characterScript leitenderTechniker;

	// Token: 0x04001190 RID: 4496
	private string searchStringA = "";
}
