using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200012E RID: 302
public class Menu_Dev_ChangeDesignproritaet : MonoBehaviour
{
	// Token: 0x06000AAF RID: 2735 RVA: 0x00007A7A File Offset: 0x00005C7A
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000AB0 RID: 2736 RVA: 0x00084B64 File Offset: 0x00082D64
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

	// Token: 0x06000AB1 RID: 2737 RVA: 0x00084D20 File Offset: 0x00082F20
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

	// Token: 0x06000AB2 RID: 2738 RVA: 0x00084EA8 File Offset: 0x000830A8
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

	// Token: 0x06000AB3 RID: 2739 RVA: 0x00084FB4 File Offset: 0x000831B4
	public void SetAP_Gameplay()
	{
		this.g_GameAP_Gameplay = Mathf.RoundToInt(this.uiObjects[5].GetComponent<Slider>().value);
		this.uiObjects[9].GetComponent<Text>().text = (this.g_GameAP_Gameplay * 5).ToString() + "%";
		this.UpdateGesamtArbeitsprioritaet();
	}

	// Token: 0x06000AB4 RID: 2740 RVA: 0x00085014 File Offset: 0x00083214
	public void SetAP_Grafik()
	{
		this.g_GameAP_Grafik = Mathf.RoundToInt(this.uiObjects[6].GetComponent<Slider>().value);
		this.uiObjects[10].GetComponent<Text>().text = (this.g_GameAP_Grafik * 5).ToString() + "%";
		this.UpdateGesamtArbeitsprioritaet();
	}

	// Token: 0x06000AB5 RID: 2741 RVA: 0x00085074 File Offset: 0x00083274
	public void SetAP_Sound()
	{
		this.g_GameAP_Sound = Mathf.RoundToInt(this.uiObjects[7].GetComponent<Slider>().value);
		this.uiObjects[11].GetComponent<Text>().text = (this.g_GameAP_Sound * 5).ToString() + "%";
		this.UpdateGesamtArbeitsprioritaet();
	}

	// Token: 0x06000AB6 RID: 2742 RVA: 0x000850D4 File Offset: 0x000832D4
	public void SetAP_Technik()
	{
		this.g_GameAP_Technik = Mathf.RoundToInt(this.uiObjects[8].GetComponent<Slider>().value);
		this.uiObjects[12].GetComponent<Text>().text = (this.g_GameAP_Technik * 5).ToString() + "%";
		this.UpdateGesamtArbeitsprioritaet();
	}

	// Token: 0x06000AB7 RID: 2743 RVA: 0x00007A82 File Offset: 0x00005C82
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.CloseMenu();
	}

	// Token: 0x06000AB8 RID: 2744 RVA: 0x00085134 File Offset: 0x00083334
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

	// Token: 0x04000EF8 RID: 3832
	public GameObject[] uiObjects;

	// Token: 0x04000EF9 RID: 3833
	private GameObject main_;

	// Token: 0x04000EFA RID: 3834
	private mainScript mS_;

	// Token: 0x04000EFB RID: 3835
	private textScript tS_;

	// Token: 0x04000EFC RID: 3836
	private GUI_Main guiMain_;

	// Token: 0x04000EFD RID: 3837
	private sfxScript sfx_;

	// Token: 0x04000EFE RID: 3838
	private genres genres_;

	// Token: 0x04000EFF RID: 3839
	private themes themes_;

	// Token: 0x04000F00 RID: 3840
	private licences licences_;

	// Token: 0x04000F01 RID: 3841
	private engineFeatures eF_;

	// Token: 0x04000F02 RID: 3842
	private cameraMovementScript cmS_;

	// Token: 0x04000F03 RID: 3843
	private unlockScript unlock_;

	// Token: 0x04000F04 RID: 3844
	private gameplayFeatures gF_;

	// Token: 0x04000F05 RID: 3845
	private games games_;

	// Token: 0x04000F06 RID: 3846
	private platforms platforms_;

	// Token: 0x04000F07 RID: 3847
	public gameScript gS_;

	// Token: 0x04000F08 RID: 3848
	public int g_GameAP_Gameplay;

	// Token: 0x04000F09 RID: 3849
	public int g_GameAP_Grafik;

	// Token: 0x04000F0A RID: 3850
	public int g_GameAP_Sound;

	// Token: 0x04000F0B RID: 3851
	public int g_GameAP_Technik;
}
