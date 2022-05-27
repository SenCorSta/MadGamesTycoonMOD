using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Options : MonoBehaviour
{
	
	private void Awake()
	{
		this.resolutions = Screen.resolutions;
		for (int i = 0; i < this.resolutions.Length; i++)
		{
			if (this.resolutions[i].width >= 1024)
			{
				if (this.resFilter.Count > 0)
				{
					if (this.resolutions[i].width != this.resFilter[this.resFilter.Count - 1].width || this.resolutions[i].height != this.resFilter[this.resFilter.Count - 1].height)
					{
						this.resFilter.Add(this.resolutions[i]);
					}
				}
				else
				{
					this.resFilter.Add(this.resolutions[i]);
				}
			}
		}
	}

	
	private void Start()
	{
		this.FindScripts();
	}

	
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
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.settings_)
		{
			this.settings_ = this.main_.GetComponent<settingsScript>();
		}
		if (!this.mpCalls_)
		{
			this.mpCalls_ = GameObject.Find("NetworkManager").GetComponent<mpCalls>();
		}
	}

	
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.Init();
		this.TAB_Settings(0);
	}

	
	private void Init()
	{
		if (this.settings_.masterVolume <= 0f)
		{
			this.uiObjects[8].GetComponent<Toggle>().isOn = false;
		}
		else
		{
			this.uiObjects[8].GetComponent<Toggle>().isOn = true;
		}
		if (this.settings_.musicVolume <= 0f)
		{
			this.uiObjects[9].GetComponent<Toggle>().isOn = false;
		}
		else
		{
			this.uiObjects[9].GetComponent<Toggle>().isOn = true;
		}
		this.uiObjects[10].GetComponent<Slider>().value = this.settings_.masterVolume;
		this.uiObjects[11].GetComponent<Slider>().value = this.settings_.musicVolume;
		this.uiObjects[13].GetComponent<Toggle>().isOn = this.settings_.vollbild;
		int num = 0;
		foreach (Resolution resolution in this.resFilter)
		{
			if (resolution.width == this.settings_.screenX && resolution.height == this.settings_.screenY)
			{
				this.uiObjects[12].GetComponent<Dropdown>().value = num;
				break;
			}
			num++;
		}
		if (this.settings_.vSync > 0)
		{
			this.uiObjects[14].GetComponent<Toggle>().isOn = true;
		}
		else
		{
			this.uiObjects[14].GetComponent<Toggle>().isOn = false;
		}
		int framerate = this.settings_.framerate;
		if (framerate <= 60)
		{
			if (framerate != 30)
			{
				if (framerate == 60)
				{
					this.uiObjects[15].GetComponent<Dropdown>().value = 1;
				}
			}
			else
			{
				this.uiObjects[15].GetComponent<Dropdown>().value = 0;
			}
		}
		else if (framerate != 90)
		{
			if (framerate == 120)
			{
				this.uiObjects[15].GetComponent<Dropdown>().value = 3;
			}
		}
		else
		{
			this.uiObjects[15].GetComponent<Dropdown>().value = 2;
		}
		this.uiObjects[1].GetComponent<Dropdown>().value = this.settings_.fullScreenMode;
		this.uiObjects[16].GetComponent<Toggle>().isOn = this.settings_.shadows;
		this.uiObjects[17].GetComponent<Toggle>().isOn = this.settings_.SSAO;
		this.uiObjects[18].GetComponent<Toggle>().isOn = this.settings_.screenSpaceReflections;
		this.uiObjects[19].GetComponent<Toggle>().isOn = this.settings_.bloom;
		this.uiObjects[20].GetComponent<Toggle>().isOn = this.settings_.ambientOcclusion;
		this.uiObjects[21].GetComponent<Toggle>().isOn = this.settings_.colorGrading;
		this.uiObjects[22].GetComponent<Dropdown>().value = this.settings_.language;
		this.uiObjects[23].GetComponent<Slider>().value = this.settings_.uiScale;
		this.uiObjects[24].GetComponent<Toggle>().isOn = this.settings_.roomConnections;
		this.uiObjects[25].GetComponent<Toggle>().isOn = this.settings_.pauseUI;
		this.uiObjects[26].GetComponent<Toggle>().isOn = this.settings_.leaderboard;
		this.uiObjects[27].GetComponent<Toggle>().isOn = this.settings_.chat;
		this.uiObjects[28].GetComponent<Toggle>().isOn = this.settings_.disableWorkIconSound;
		this.uiObjects[29].GetComponent<Toggle>().isOn = this.settings_.sprechblasen;
		this.uiObjects[31].GetComponent<Toggle>().isOn = this.settings_.scrollScreen;
		this.uiObjects[32].GetComponent<Toggle>().isOn = this.settings_.disableEngineAbrechnung;
		this.uiObjects[33].GetComponent<Toggle>().isOn = this.settings_.disableWorkIcons;
		this.uiObjects[34].GetComponent<Toggle>().isOn = this.settings_.disableArbeiterBeschwerden;
		this.uiObjects[35].GetComponent<Toggle>().isOn = this.settings_.disableWetter;
		this.uiObjects[36].GetComponent<Toggle>().isOn = this.settings_.singleplayerPause;
		this.uiObjects[37].GetComponent<Slider>().value = this.settings_.fanletterTime;
		this.uiObjects[38].GetComponent<Toggle>().isOn = this.settings_.gameTabData;
		this.uiObjects[39].GetComponent<Toggle>().isOn = this.mS_.settings_autoPauseForMultiplayer;
		this.uiObjects[40].GetComponent<Dropdown>().value = this.settings_.saveInterval;
		this.uiObjects[41].GetComponent<Slider>().value = this.settings_.newsTime;
		this.uiObjects[42].GetComponent<Slider>().value = this.settings_.helligkeit;
		this.uiObjects[43].GetComponent<Toggle>().isOn = this.settings_.dontAsk_TaskAbbrechen;
		this.uiObjects[44].GetComponent<Toggle>().isOn = this.settings_.middleMouseClose;
		this.uiObjects[45].GetComponent<Toggle>().isOn = this.settings_.camera90GradRotation;
		this.uiObjects[46].GetComponent<Toggle>().isOn = this.settings_.hideConvention;
		this.uiObjects[47].GetComponent<Toggle>().isOn = this.settings_.hideAwards;
		this.uiObjects[48].GetComponent<Toggle>().isOn = this.settings_.hideEvents;
		this.uiObjects[49].GetComponent<Toggle>().isOn = this.settings_.disableTochterfirmaAbrechnung;
		this.uiObjects[50].GetComponent<Toggle>().isOn = this.settings_.hideKuendigungen;
		this.uiObjects[51].GetComponent<Toggle>().isOn = this.settings_.tochtefirmaTAG;
	}

	
	public void InitDropdowns()
	{
		this.FindScripts();
		List<string> list = new List<string>();
		foreach (Resolution resolution in this.resFilter)
		{
			list.Add(resolution.width + "x" + resolution.height);
		}
		this.uiObjects[12].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[12].GetComponent<Dropdown>().AddOptions(list);
		list.Clear();
		list.Add(this.tS_.GetText(1395));
		list.Add(this.tS_.GetText(1396));
		list.Add(this.tS_.GetText(1397));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		list.Clear();
		list.Add(this.tS_.GetText(1449));
		list.Add(this.tS_.GetText(1450));
		list.Add(this.tS_.GetText(1451));
		list.Add(this.tS_.GetText(1452));
		this.uiObjects[40].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[40].GetComponent<Dropdown>().AddOptions(list);
	}

	
	private void Init(bool inBesitz)
	{
		this.FindScripts();
	}

	
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public void TOGGLE_MUSIC()
	{
		if (this.uiObjects[9].GetComponent<Toggle>().isOn && this.settings_.musicVolume <= 0f)
		{
			this.uiObjects[11].GetComponent<Slider>().value = 0.3f;
		}
	}

	
	public void DROPDOWN_Resolution()
	{
	}

	
	public void BUTTON_OK()
	{
		this.settings_.masterVolume = this.uiObjects[10].GetComponent<Slider>().value;
		this.settings_.musicVolume = this.uiObjects[11].GetComponent<Slider>().value;
		if (!this.uiObjects[8].GetComponent<Toggle>().isOn)
		{
			this.settings_.masterVolume = 0f;
		}
		if (!this.uiObjects[9].GetComponent<Toggle>().isOn)
		{
			this.settings_.musicVolume = 0f;
		}
		this.settings_.vollbild = this.uiObjects[13].GetComponent<Toggle>().isOn;
		this.settings_.uiScale = this.uiObjects[23].GetComponent<Slider>().value;
		if (this.settings_.screenX != this.resFilter[this.uiObjects[12].GetComponent<Dropdown>().value].width || this.settings_.screenY != this.resFilter[this.uiObjects[12].GetComponent<Dropdown>().value].height)
		{
			this.settings_.SetAutomaticGuiScale(this.resFilter[this.uiObjects[12].GetComponent<Dropdown>().value].width);
		}
		this.settings_.screenX = this.resFilter[this.uiObjects[12].GetComponent<Dropdown>().value].width;
		this.settings_.screenY = this.resFilter[this.uiObjects[12].GetComponent<Dropdown>().value].height;
		if (this.uiObjects[14].GetComponent<Toggle>().isOn)
		{
			this.settings_.vSync = 1;
		}
		else
		{
			this.settings_.vSync = 0;
		}
		switch (this.uiObjects[15].GetComponent<Dropdown>().value)
		{
		case 0:
			this.settings_.framerate = 30;
			break;
		case 1:
			this.settings_.framerate = 60;
			break;
		case 2:
			this.settings_.framerate = 90;
			break;
		case 3:
			this.settings_.framerate = 120;
			break;
		}
		this.settings_.fullScreenMode = this.uiObjects[1].GetComponent<Dropdown>().value;
		this.settings_.shadows = this.uiObjects[16].GetComponent<Toggle>().isOn;
		this.settings_.SSAO = this.uiObjects[17].GetComponent<Toggle>().isOn;
		this.settings_.screenSpaceReflections = this.uiObjects[18].GetComponent<Toggle>().isOn;
		this.settings_.bloom = this.uiObjects[19].GetComponent<Toggle>().isOn;
		this.settings_.ambientOcclusion = this.uiObjects[20].GetComponent<Toggle>().isOn;
		this.settings_.colorGrading = this.uiObjects[21].GetComponent<Toggle>().isOn;
		this.settings_.language = this.uiObjects[22].GetComponent<Dropdown>().value;
		this.settings_.roomConnections = this.uiObjects[24].GetComponent<Toggle>().isOn;
		this.settings_.pauseUI = this.uiObjects[25].GetComponent<Toggle>().isOn;
		this.settings_.leaderboard = this.uiObjects[26].GetComponent<Toggle>().isOn;
		this.settings_.chat = this.uiObjects[27].GetComponent<Toggle>().isOn;
		this.settings_.disableWorkIconSound = this.uiObjects[28].GetComponent<Toggle>().isOn;
		this.settings_.sprechblasen = this.uiObjects[29].GetComponent<Toggle>().isOn;
		this.settings_.scrollScreen = this.uiObjects[31].GetComponent<Toggle>().isOn;
		this.settings_.disableEngineAbrechnung = this.uiObjects[32].GetComponent<Toggle>().isOn;
		this.settings_.disableWorkIcons = this.uiObjects[33].GetComponent<Toggle>().isOn;
		this.settings_.disableArbeiterBeschwerden = this.uiObjects[34].GetComponent<Toggle>().isOn;
		this.settings_.disableWetter = this.uiObjects[35].GetComponent<Toggle>().isOn;
		this.settings_.singleplayerPause = this.uiObjects[36].GetComponent<Toggle>().isOn;
		this.settings_.fanletterTime = this.uiObjects[37].GetComponent<Slider>().value;
		this.settings_.gameTabData = this.uiObjects[38].GetComponent<Toggle>().isOn;
		this.mS_.settings_autoPauseForMultiplayer = this.uiObjects[39].GetComponent<Toggle>().isOn;
		this.settings_.saveInterval = this.uiObjects[40].GetComponent<Dropdown>().value;
		this.settings_.newsTime = this.uiObjects[41].GetComponent<Slider>().value;
		this.settings_.helligkeit = this.uiObjects[42].GetComponent<Slider>().value;
		this.settings_.dontAsk_TaskAbbrechen = this.uiObjects[43].GetComponent<Toggle>().isOn;
		this.settings_.middleMouseClose = this.uiObjects[44].GetComponent<Toggle>().isOn;
		this.settings_.camera90GradRotation = this.uiObjects[45].GetComponent<Toggle>().isOn;
		this.settings_.hideConvention = this.uiObjects[46].GetComponent<Toggle>().isOn;
		this.settings_.hideAwards = this.uiObjects[47].GetComponent<Toggle>().isOn;
		this.settings_.hideEvents = this.uiObjects[48].GetComponent<Toggle>().isOn;
		this.settings_.disableTochterfirmaAbrechnung = this.uiObjects[49].GetComponent<Toggle>().isOn;
		this.settings_.hideKuendigungen = this.uiObjects[50].GetComponent<Toggle>().isOn;
		this.settings_.tochtefirmaTAG = this.uiObjects[51].GetComponent<Toggle>().isOn;
		switch (this.settings_.saveInterval)
		{
		case 0:
			this.mS_.autoSaveInterval = 12;
			break;
		case 1:
			this.mS_.autoSaveInterval = 9;
			break;
		case 2:
			this.mS_.autoSaveInterval = 6;
			break;
		case 3:
			this.mS_.autoSaveInterval = 3;
			break;
		default:
			this.mS_.autoSaveInterval = 12;
			break;
		}
		this.sfx_.SetVolume();
		this.settings_.UpdateSettings();
		this.BUTTON_Close();
		GameObject gameObject = GameObject.Find("CanvasInGameMenu");
		gameObject.SetActive(false);
		gameObject.SetActive(true);
		if (this.mS_.multiplayer && this.mpCalls_.isServer)
		{
			if (this.mS_.settings_autoPauseForMultiplayer)
			{
				this.mpCalls_.SERVER_Send_Command(7);
				return;
			}
			this.mpCalls_.SERVER_Send_Command(6);
		}
	}

	
	public void TAB_Settings(int t)
	{
		this.TAB = t;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.SetTab(this.uiObjects[4], t);
		switch (t)
		{
		case 0:
			this.uiObjects[5].SetActive(true);
			this.uiObjects[6].SetActive(false);
			this.uiObjects[7].SetActive(false);
			return;
		case 1:
			this.uiObjects[5].SetActive(false);
			this.uiObjects[6].SetActive(true);
			this.uiObjects[7].SetActive(false);
			return;
		case 2:
			this.uiObjects[5].SetActive(false);
			this.uiObjects[6].SetActive(false);
			this.uiObjects[7].SetActive(true);
			return;
		default:
			return;
		}
	}

	
	public void BUTTON_DefaultGuiSize()
	{
		this.sfx_.PlaySound(3, true);
		this.uiObjects[23].GetComponent<Slider>().value = 1f;
		if (!this.mainCanvas)
		{
			this.mainCanvas = GameObject.Find("CanvasInGameMenu");
		}
		if (this.mainCanvas)
		{
			this.mainCanvas.GetComponent<CanvasScaler>().scaleFactor = 1f;
		}
	}

	
	public void SLIDER_GuiSize()
	{
		float value = this.uiObjects[23].GetComponent<Slider>().value;
		if (Mathf.Abs(this.uiObjects[30].GetComponent<RectTransform>().anchoredPosition.y * value) + (float)Screen.height * 0.5f > (float)(Screen.height - 64))
		{
			this.uiObjects[23].GetComponent<Slider>().value -= 0.1f;
			return;
		}
		if (!this.mainCanvas)
		{
			this.mainCanvas = GameObject.Find("CanvasInGameMenu");
		}
		if (this.mainCanvas)
		{
			this.mainCanvas.GetComponent<CanvasScaler>().scaleFactor = this.uiObjects[23].GetComponent<Slider>().value;
		}
	}

	
	public void SLIDER_Helligkeit()
	{
		float value = this.uiObjects[42].GetComponent<Slider>().value;
		if (this.guiMain_)
		{
			this.guiMain_.hellgikeitsObjekt.GetComponent<Image>().color = new Color(0f, 0f, 0f, 1f - value / 255f);
		}
	}

	
	public void SLIDER_FanletterTime()
	{
		float value = this.uiObjects[37].GetComponent<Slider>().value;
	}

	
	public void SLIDER_NewsTime()
	{
		float value = this.uiObjects[41].GetComponent<Slider>().value;
	}

	
	public void BUTTON_DefaultFanletterTime()
	{
		this.sfx_.PlaySound(3, true);
		this.uiObjects[37].GetComponent<Slider>().value = 5f;
	}

	
	public void BUTTON_DefaultNewsTime()
	{
		this.sfx_.PlaySound(3, true);
		this.uiObjects[41].GetComponent<Slider>().value = 5f;
	}

	
	public void BUTTON_DefaultHelligkeit()
	{
		this.sfx_.PlaySound(3, true);
		this.uiObjects[42].GetComponent<Slider>().value = 255f;
	}

	
	public GameObject[] uiObjects;

	
	private GameObject mainCanvas;

	
	private mpCalls mpCalls_;

	
	private mainScript mS_;

	
	private GameObject main_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private textScript tS_;

	
	private settingsScript settings_;

	
	private int TAB;

	
	private Resolution[] resolutions;

	
	public List<Resolution> resFilter = new List<Resolution>();
}
