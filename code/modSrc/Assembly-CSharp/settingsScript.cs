using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;


public class settingsScript : MonoBehaviour
{
	
	private void Awake()
	{
		this.LoadSettings();
		this.UpdateSettings();
	}

	
	public void LoadSettings()
	{
		string filePath = "settings.txt";
		this.reader = ES3Reader.Create(filePath);
		if (this.reader == null)
		{
			return;
		}
		int[] array = new int[100];
		float[] array2 = new float[100];
		bool[] array3 = new bool[100];
		array = this.reader.Read<int[]>("settings_int");
		array2 = this.reader.Read<float[]>("settings_float");
		array3 = this.reader.Read<bool[]>("settings_bool");
		this.language = array[0];
		this.screenX = array[1];
		this.screenY = array[2];
		this.framerate = array[3];
		this.vSync = array[4];
		this.fullScreenMode = array[5];
		this.saveInterval = array[6];
		this.uiScale = array2[0];
		this.musicVolume = array2[1];
		this.masterVolume = array2[2];
		this.fanletterTime = array2[3];
		if (this.fanletterTime <= 0f)
		{
			this.fanletterTime = 5f;
		}
		this.newsTime = array2[4];
		if (this.newsTime <= 0f)
		{
			this.newsTime = 5f;
		}
		this.helligkeit = array2[5];
		if (this.helligkeit <= 50f)
		{
			this.helligkeit = 255f;
		}
		this.vollbild = array3[0];
		this.shadows = array3[1];
		this.SSAO = array3[2];
		this.screenSpaceReflections = array3[3];
		this.bloom = array3[4];
		this.ambientOcclusion = array3[5];
		this.colorGrading = array3[6];
		this.roomConnections = array3[7];
		this.pauseUI = array3[8];
		this.leaderboard = array3[9];
		this.chat = array3[10];
		this.disableWorkIconSound = array3[11];
		this.sprechblasen = array3[12];
		this.scrollScreen = array3[13];
		this.disableEngineAbrechnung = array3[14];
		this.disableWorkIcons = array3[15];
		this.disableArbeiterBeschwerden = array3[16];
		this.disableWetter = array3[17];
		this.singleplayerPause = false;
		this.gameTabData = array3[19];
		this.dontAsk_TaskAbbrechen = array3[20];
		this.middleMouseClose = array3[21];
		this.camera90GradRotation = array3[22];
		this.hideConvention = array3[23];
		this.hideAwards = array3[24];
		this.hideEvents = array3[25];
		this.disableTochterfirmaAbrechnung = array3[26];
		this.hideKuendigungen = array3[27];
		this.tochtefirmaTAG = array3[28];
		this.reader.Dispose();
	}

	
	public void SaveSettings()
	{
		string filePath = "settings.txt";
		ES3Settings settings = new ES3Settings();
		ES3.DeleteFile(filePath);
		this.writer = ES3Writer.Create(filePath, settings);
		int[] array = new int[100];
		float[] array2 = new float[100];
		bool[] array3 = new bool[100];
		array[0] = this.language;
		array[1] = this.screenX;
		array[2] = this.screenY;
		array[3] = this.framerate;
		array[4] = this.vSync;
		array[5] = this.fullScreenMode;
		array[6] = this.saveInterval;
		array2[0] = this.uiScale;
		array2[1] = this.musicVolume;
		array2[2] = this.masterVolume;
		array2[3] = this.fanletterTime;
		array2[4] = this.newsTime;
		array2[5] = this.helligkeit;
		array3[0] = this.vollbild;
		array3[1] = this.shadows;
		array3[2] = this.SSAO;
		array3[3] = this.screenSpaceReflections;
		array3[4] = this.bloom;
		array3[5] = this.ambientOcclusion;
		array3[6] = this.colorGrading;
		array3[7] = this.roomConnections;
		array3[8] = this.pauseUI;
		array3[9] = this.leaderboard;
		array3[10] = this.chat;
		array3[11] = this.disableWorkIconSound;
		array3[12] = this.sprechblasen;
		array3[13] = this.scrollScreen;
		array3[14] = this.disableEngineAbrechnung;
		array3[15] = this.disableWorkIcons;
		array3[16] = this.disableArbeiterBeschwerden;
		array3[17] = this.disableWetter;
		array3[18] = this.singleplayerPause;
		array3[19] = this.gameTabData;
		array3[20] = this.dontAsk_TaskAbbrechen;
		array3[21] = this.middleMouseClose;
		array3[22] = this.camera90GradRotation;
		array3[23] = this.hideConvention;
		array3[24] = this.hideAwards;
		array3[25] = this.hideEvents;
		array3[26] = this.disableTochterfirmaAbrechnung;
		array3[27] = this.hideKuendigungen;
		array3[28] = this.tochtefirmaTAG;
		this.writer.Write<int[]>("settings_int", array);
		this.writer.Write<float[]>("settings_float", array2);
		this.writer.Write<bool[]>("settings_bool", array3);
		this.writer.Save();
		this.writer.Dispose();
	}

	
	public void UpdateSettings()
	{
		QualitySettings.vSyncCount = this.vSync;
		Application.targetFrameRate = this.framerate;
		if (this.screenX > 0)
		{
			if (this.vollbild)
			{
				switch (this.fullScreenMode)
				{
				case 0:
					Screen.SetResolution(this.screenX, this.screenY, FullScreenMode.FullScreenWindow);
					break;
				case 1:
					Screen.SetResolution(this.screenX, this.screenY, FullScreenMode.ExclusiveFullScreen);
					break;
				case 2:
					Screen.SetResolution(this.screenX, this.screenY, FullScreenMode.MaximizedWindow);
					break;
				default:
					Screen.SetResolution(this.screenX, this.screenY, true);
					break;
				}
			}
			else
			{
				Screen.SetResolution(this.screenX, this.screenY, false);
			}
		}
		else
		{
			Screen.SetResolution(Screen.width, Screen.height, true);
			this.SetAutomaticGuiScale(Screen.width);
		}
		if (!this.mainCanvas)
		{
			this.mainCanvas = GameObject.Find("CanvasInGameMenu");
		}
		if (this.mainCanvas)
		{
			this.mainCanvas.GetComponent<CanvasScaler>().scaleFactor = this.uiScale;
		}
		if (this.mainCanvas)
		{
			if (this.helligkeit < 50f)
			{
				this.helligkeit = 255f;
			}
			if (this.helligkeit > 255f)
			{
				this.helligkeit = 255f;
			}
			this.mainCanvas.GetComponent<GUI_Main>().hellgikeitsObjekt.GetComponent<Image>().color = new Color(0f, 0f, 0f, 1f - this.helligkeit / 255f);
		}
		if (this.shadows)
		{
			QualitySettings.shadows = ShadowQuality.All;
		}
		else
		{
			QualitySettings.shadows = ShadowQuality.Disable;
		}
		if (!this.splashScreen)
		{
			ScreenSpaceReflections screenSpaceReflections;
			if (this.postProcess.TryGetSettings<ScreenSpaceReflections>(out screenSpaceReflections))
			{
				screenSpaceReflections.enabled.value = this.screenSpaceReflections;
			}
			Bloom bloom;
			if (this.postProcess.TryGetSettings<Bloom>(out bloom))
			{
				bloom.enabled.value = this.bloom;
			}
			AmbientOcclusion ambientOcclusion;
			if (this.postProcess.TryGetSettings<AmbientOcclusion>(out ambientOcclusion))
			{
				ambientOcclusion.enabled.value = this.ambientOcclusion;
			}
			ColorGrading colorGrading;
			if (this.postProcess.TryGetSettings<ColorGrading>(out colorGrading))
			{
				colorGrading.enabled.value = this.colorGrading;
			}
		}
		this.SaveSettings();
	}

	
	public void SetAutomaticGuiScale(int screen_width)
	{
		if (screen_width <= 1366)
		{
			if (screen_width <= 1176)
			{
				if (screen_width == 1024)
				{
					this.uiScale = 0.68f;
					goto IL_188;
				}
				if (screen_width == 1152)
				{
					this.uiScale = 0.75f;
					goto IL_188;
				}
				if (screen_width == 1176)
				{
					this.uiScale = 0.75f;
					goto IL_188;
				}
			}
			else
			{
				if (screen_width == 1280)
				{
					this.uiScale = 0.83f;
					goto IL_188;
				}
				if (screen_width == 1360)
				{
					this.uiScale = 0.83f;
					goto IL_188;
				}
				if (screen_width == 1366)
				{
					this.uiScale = 0.83f;
					goto IL_188;
				}
			}
		}
		else if (screen_width <= 1720)
		{
			if (screen_width == 1600)
			{
				this.uiScale = 1f;
				goto IL_188;
			}
			if (screen_width == 1680)
			{
				this.uiScale = 1f;
				goto IL_188;
			}
			if (screen_width == 1720)
			{
				this.uiScale = 1f;
				goto IL_188;
			}
		}
		else if (screen_width <= 2048)
		{
			if (screen_width == 1920)
			{
				this.uiScale = 1f;
				goto IL_188;
			}
			if (screen_width == 2048)
			{
				this.uiScale = 1f;
				goto IL_188;
			}
		}
		else
		{
			if (screen_width == 2560)
			{
				this.uiScale = 1f;
				goto IL_188;
			}
			if (screen_width == 3440)
			{
				this.uiScale = 1.24f;
				goto IL_188;
			}
		}
		this.uiScale = 1f;
		IL_188:
		Debug.Log(this.uiScale);
	}

	
	private ES3Writer writer;

	
	private ES3Reader reader;

	
	public bool splashScreen;

	
	public int language;

	
	public int saveInterval = 12;

	
	public float uiScale = 1f;

	
	public bool roomConnections = true;

	
	public bool pauseUI = true;

	
	public bool leaderboard = true;

	
	public bool chat = true;

	
	public bool sprechblasen = true;

	
	public bool scrollScreen = true;

	
	public bool disableEngineAbrechnung;

	
	public bool disableWorkIcons;

	
	public bool disableArbeiterBeschwerden;

	
	public bool singleplayerPause;

	
	public float fanletterTime = 5f;

	
	public float newsTime = 5f;

	
	public bool gameTabData;

	
	public bool dontAsk_TaskAbbrechen;

	
	public bool middleMouseClose;

	
	public bool camera90GradRotation;

	
	public bool hideConvention;

	
	public bool hideAwards;

	
	public bool hideEvents;

	
	public bool disableTochterfirmaAbrechnung;

	
	public bool hideKuendigungen;

	
	public bool tochtefirmaTAG = true;

	
	public float musicVolume = 1f;

	
	public float masterVolume = 1f;

	
	public bool disableWorkIconSound;

	
	public int screenX = 1024;

	
	public int screenY = 768;

	
	public bool vollbild = true;

	
	public int framerate = 60;

	
	public int fullScreenMode = 1;

	
	public int vSync = 1;

	
	public bool shadows = true;

	
	public bool SSAO = true;

	
	public bool screenSpaceReflections = true;

	
	public bool bloom = true;

	
	public bool ambientOcclusion = true;

	
	public bool colorGrading = true;

	
	public bool disableWetter = true;

	
	public float helligkeit = 1.81f;

	
	private GameObject mainCanvas;

	
	private GUI_Main guiMain_;

	
	public PostProcessProfile postProcess;

	
	public PostProcessLayer postLayer;
}
