using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

// Token: 0x02000343 RID: 835
public class settingsScript : MonoBehaviour
{
	// Token: 0x06001EB7 RID: 7863 RVA: 0x00140244 File Offset: 0x0013E444
	private void Awake()
	{
		this.LoadSettings();
		this.UpdateSettings();
	}

	// Token: 0x06001EB8 RID: 7864 RVA: 0x00140254 File Offset: 0x0013E454
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

	// Token: 0x06001EB9 RID: 7865 RVA: 0x001404A8 File Offset: 0x0013E6A8
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

	// Token: 0x06001EBA RID: 7866 RVA: 0x001406E4 File Offset: 0x0013E8E4
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

	// Token: 0x06001EBB RID: 7867 RVA: 0x0014091C File Offset: 0x0013EB1C
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

	// Token: 0x04002675 RID: 9845
	private ES3Writer writer;

	// Token: 0x04002676 RID: 9846
	private ES3Reader reader;

	// Token: 0x04002677 RID: 9847
	public bool splashScreen;

	// Token: 0x04002678 RID: 9848
	public int language;

	// Token: 0x04002679 RID: 9849
	public int saveInterval = 12;

	// Token: 0x0400267A RID: 9850
	public float uiScale = 1f;

	// Token: 0x0400267B RID: 9851
	public bool roomConnections = true;

	// Token: 0x0400267C RID: 9852
	public bool pauseUI = true;

	// Token: 0x0400267D RID: 9853
	public bool leaderboard = true;

	// Token: 0x0400267E RID: 9854
	public bool chat = true;

	// Token: 0x0400267F RID: 9855
	public bool sprechblasen = true;

	// Token: 0x04002680 RID: 9856
	public bool scrollScreen = true;

	// Token: 0x04002681 RID: 9857
	public bool disableEngineAbrechnung;

	// Token: 0x04002682 RID: 9858
	public bool disableWorkIcons;

	// Token: 0x04002683 RID: 9859
	public bool disableArbeiterBeschwerden;

	// Token: 0x04002684 RID: 9860
	public bool singleplayerPause;

	// Token: 0x04002685 RID: 9861
	public float fanletterTime = 5f;

	// Token: 0x04002686 RID: 9862
	public float newsTime = 5f;

	// Token: 0x04002687 RID: 9863
	public bool gameTabData;

	// Token: 0x04002688 RID: 9864
	public bool dontAsk_TaskAbbrechen;

	// Token: 0x04002689 RID: 9865
	public bool middleMouseClose;

	// Token: 0x0400268A RID: 9866
	public bool camera90GradRotation;

	// Token: 0x0400268B RID: 9867
	public bool hideConvention;

	// Token: 0x0400268C RID: 9868
	public bool hideAwards;

	// Token: 0x0400268D RID: 9869
	public bool hideEvents;

	// Token: 0x0400268E RID: 9870
	public bool disableTochterfirmaAbrechnung;

	// Token: 0x0400268F RID: 9871
	public bool hideKuendigungen;

	// Token: 0x04002690 RID: 9872
	public bool tochtefirmaTAG = true;

	// Token: 0x04002691 RID: 9873
	public float musicVolume = 1f;

	// Token: 0x04002692 RID: 9874
	public float masterVolume = 1f;

	// Token: 0x04002693 RID: 9875
	public bool disableWorkIconSound;

	// Token: 0x04002694 RID: 9876
	public int screenX = 1024;

	// Token: 0x04002695 RID: 9877
	public int screenY = 768;

	// Token: 0x04002696 RID: 9878
	public bool vollbild = true;

	// Token: 0x04002697 RID: 9879
	public int framerate = 60;

	// Token: 0x04002698 RID: 9880
	public int fullScreenMode = 1;

	// Token: 0x04002699 RID: 9881
	public int vSync = 1;

	// Token: 0x0400269A RID: 9882
	public bool shadows = true;

	// Token: 0x0400269B RID: 9883
	public bool SSAO = true;

	// Token: 0x0400269C RID: 9884
	public bool screenSpaceReflections = true;

	// Token: 0x0400269D RID: 9885
	public bool bloom = true;

	// Token: 0x0400269E RID: 9886
	public bool ambientOcclusion = true;

	// Token: 0x0400269F RID: 9887
	public bool colorGrading = true;

	// Token: 0x040026A0 RID: 9888
	public bool disableWetter = true;

	// Token: 0x040026A1 RID: 9889
	public float helligkeit = 1.81f;

	// Token: 0x040026A2 RID: 9890
	private GameObject mainCanvas;

	// Token: 0x040026A3 RID: 9891
	private GUI_Main guiMain_;

	// Token: 0x040026A4 RID: 9892
	public PostProcessProfile postProcess;

	// Token: 0x040026A5 RID: 9893
	public PostProcessLayer postLayer;
}
