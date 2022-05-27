using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200012F RID: 303
public class Menu_Dev_ChangeDesignproritaet : MonoBehaviour
{
	// Token: 0x06000AC0 RID: 2752 RVA: 0x000746AE File Offset: 0x000728AE
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000AC1 RID: 2753 RVA: 0x000746B8 File Offset: 0x000728B8
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
	}

	// Token: 0x06000AC2 RID: 2754 RVA: 0x00074874 File Offset: 0x00072A74
	public void Init(gameScript game_)
	{
		this.FindScripts();
		this.gS_ = game_;
		this.uiObjects[0].GetComponent<Text>().text = this.gS_.GetNameWithTag();
		this.uiObjects[1].GetComponent<Text>().text = Mathf.RoundToInt(this.gS_.points_gameplay).ToString();
		this.uiObjects[2].GetComponent<Text>().text = Mathf.RoundToInt(this.gS_.points_grafik).ToString();
		this.uiObjects[3].GetComponent<Text>().text = Mathf.RoundToInt(this.gS_.points_sound).ToString();
		this.uiObjects[4].GetComponent<Text>().text = Mathf.RoundToInt(this.gS_.points_technik).ToString();
		this.g_GameAP_Gameplay = this.gS_.gameAP_Gameplay;
		this.g_GameAP_Grafik = this.gS_.gameAP_Grafik;
		this.g_GameAP_Sound = this.gS_.gameAP_Sound;
		this.g_GameAP_Technik = this.gS_.gameAP_Technik;
		this.uiObjects[5].GetComponent<Slider>().value = (float)this.g_GameAP_Gameplay;
		this.uiObjects[6].GetComponent<Slider>().value = (float)this.g_GameAP_Grafik;
		this.uiObjects[7].GetComponent<Slider>().value = (float)this.g_GameAP_Sound;
		this.uiObjects[8].GetComponent<Slider>().value = (float)this.g_GameAP_Technik;
	}

	// Token: 0x06000AC3 RID: 2755 RVA: 0x000749FC File Offset: 0x00072BFC
	private int UpdateGesamtArbeitsprioritaet()
	{
		float num = this.uiObjects[5].GetComponent<Slider>().value;
		num += this.uiObjects[6].GetComponent<Slider>().value;
		num += this.uiObjects[7].GetComponent<Slider>().value;
		num += this.uiObjects[8].GetComponent<Slider>().value;
		num *= 5f;
		this.uiObjects[13].GetComponent<Text>().text = Mathf.RoundToInt(num).ToString() + "%";
		if (Mathf.RoundToInt(num) > 100)
		{
			this.uiObjects[13].GetComponent<Text>().color = Color.red;
		}
		else
		{
			this.uiObjects[13].GetComponent<Text>().color = this.guiMain_.colors[6];
		}
		float num2 = num;
		num2 *= 0.01f;
		if (num2 > 1f)
		{
			num2 = 1f;
		}
		this.uiObjects[14].GetComponent<Image>().fillAmount = num2;
		return Mathf.RoundToInt(num);
	}

	// Token: 0x06000AC4 RID: 2756 RVA: 0x00074B08 File Offset: 0x00072D08
	public void SetAP_Gameplay()
	{
		this.g_GameAP_Gameplay = Mathf.RoundToInt(this.uiObjects[5].GetComponent<Slider>().value);
		this.uiObjects[9].GetComponent<Text>().text = (this.g_GameAP_Gameplay * 5).ToString() + "%";
		this.UpdateGesamtArbeitsprioritaet();
	}

	// Token: 0x06000AC5 RID: 2757 RVA: 0x00074B68 File Offset: 0x00072D68
	public void SetAP_Grafik()
	{
		this.g_GameAP_Grafik = Mathf.RoundToInt(this.uiObjects[6].GetComponent<Slider>().value);
		this.uiObjects[10].GetComponent<Text>().text = (this.g_GameAP_Grafik * 5).ToString() + "%";
		this.UpdateGesamtArbeitsprioritaet();
	}

	// Token: 0x06000AC6 RID: 2758 RVA: 0x00074BC8 File Offset: 0x00072DC8
	public void SetAP_Sound()
	{
		this.g_GameAP_Sound = Mathf.RoundToInt(this.uiObjects[7].GetComponent<Slider>().value);
		this.uiObjects[11].GetComponent<Text>().text = (this.g_GameAP_Sound * 5).ToString() + "%";
		this.UpdateGesamtArbeitsprioritaet();
	}

	// Token: 0x06000AC7 RID: 2759 RVA: 0x00074C28 File Offset: 0x00072E28
	public void SetAP_Technik()
	{
		this.g_GameAP_Technik = Mathf.RoundToInt(this.uiObjects[8].GetComponent<Slider>().value);
		this.uiObjects[12].GetComponent<Text>().text = (this.g_GameAP_Technik * 5).ToString() + "%";
		this.UpdateGesamtArbeitsprioritaet();
	}

	// Token: 0x06000AC8 RID: 2760 RVA: 0x00074C86 File Offset: 0x00072E86
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.CloseMenu();
	}

	// Token: 0x06000AC9 RID: 2761 RVA: 0x00074CAC File Offset: 0x00072EAC
	public void BUTTON_OK()
	{
		if (!this.gS_)
		{
			return;
		}
		this.sfx_.PlaySound(3, true);
		if (this.UpdateGesamtArbeitsprioritaet() > 100)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(400), false);
			return;
		}
		this.gS_.gameAP_Gameplay = this.g_GameAP_Gameplay;
		this.gS_.gameAP_Grafik = this.g_GameAP_Grafik;
		this.gS_.gameAP_Sound = this.g_GameAP_Sound;
		this.gS_.gameAP_Technik = this.g_GameAP_Technik;
		base.gameObject.SetActive(false);
		this.guiMain_.CloseMenu();
	}

	// Token: 0x04000F00 RID: 3840
	public GameObject[] uiObjects;

	// Token: 0x04000F01 RID: 3841
	private GameObject main_;

	// Token: 0x04000F02 RID: 3842
	private mainScript mS_;

	// Token: 0x04000F03 RID: 3843
	private textScript tS_;

	// Token: 0x04000F04 RID: 3844
	private GUI_Main guiMain_;

	// Token: 0x04000F05 RID: 3845
	private sfxScript sfx_;

	// Token: 0x04000F06 RID: 3846
	private genres genres_;

	// Token: 0x04000F07 RID: 3847
	private themes themes_;

	// Token: 0x04000F08 RID: 3848
	private licences licences_;

	// Token: 0x04000F09 RID: 3849
	private engineFeatures eF_;

	// Token: 0x04000F0A RID: 3850
	private cameraMovementScript cmS_;

	// Token: 0x04000F0B RID: 3851
	private unlockScript unlock_;

	// Token: 0x04000F0C RID: 3852
	private gameplayFeatures gF_;

	// Token: 0x04000F0D RID: 3853
	private games games_;

	// Token: 0x04000F0E RID: 3854
	private platforms platforms_;

	// Token: 0x04000F0F RID: 3855
	public gameScript gS_;

	// Token: 0x04000F10 RID: 3856
	public int g_GameAP_Gameplay;

	// Token: 0x04000F11 RID: 3857
	public int g_GameAP_Grafik;

	// Token: 0x04000F12 RID: 3858
	public int g_GameAP_Sound;

	// Token: 0x04000F13 RID: 3859
	public int g_GameAP_Technik;
}
