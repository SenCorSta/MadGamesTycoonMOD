using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000112 RID: 274
public class Menu_Abo_Preis : MonoBehaviour
{
	// Token: 0x060008F3 RID: 2291 RVA: 0x00060F49 File Offset: 0x0005F149
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060008F4 RID: 2292 RVA: 0x00060F54 File Offset: 0x0005F154
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
	}

	// Token: 0x060008F5 RID: 2293 RVA: 0x000610F4 File Offset: 0x0005F2F4
	public void Init(gameScript game_)
	{
		this.FindScripts();
		this.gS_ = game_;
		this.uiObjects[0].GetComponent<Text>().text = this.gS_.GetNameWithTag();
		if (game_.aboPreis <= 0)
		{
			this.uiObjects[2].GetComponent<Slider>().value = 5f;
		}
		else
		{
			this.uiObjects[2].GetComponent<Slider>().value = (float)this.gS_.aboPreis;
		}
		this.SLIDER_AboPreis();
	}

	// Token: 0x060008F6 RID: 2294 RVA: 0x00061171 File Offset: 0x0005F371
	public void SLIDER_AboPreis()
	{
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt(this.uiObjects[2].GetComponent<Slider>().value), true);
	}

	// Token: 0x060008F7 RID: 2295 RVA: 0x000611A9 File Offset: 0x0005F3A9
	public void BUTTON_Close()
	{
		this.BUTTON_Ok();
	}

	// Token: 0x060008F8 RID: 2296 RVA: 0x000611B4 File Offset: 0x0005F3B4
	public void BUTTON_Ok()
	{
		this.sfx_.PlaySound(3, true);
		this.gS_.aboPreis = Mathf.RoundToInt(this.uiObjects[2].GetComponent<Slider>().value);
		if (!this.guiMain_.uiObjects[245].activeSelf)
		{
			this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[71]);
			this.guiMain_.uiObjects[71].GetComponent<Menu_Dev_XP>().Init(this.gS_);
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000D6B RID: 3435
	public GameObject[] uiObjects;

	// Token: 0x04000D6C RID: 3436
	private GameObject main_;

	// Token: 0x04000D6D RID: 3437
	private mainScript mS_;

	// Token: 0x04000D6E RID: 3438
	private textScript tS_;

	// Token: 0x04000D6F RID: 3439
	private GUI_Main guiMain_;

	// Token: 0x04000D70 RID: 3440
	private sfxScript sfx_;

	// Token: 0x04000D71 RID: 3441
	private genres genres_;

	// Token: 0x04000D72 RID: 3442
	private themes themes_;

	// Token: 0x04000D73 RID: 3443
	private licences licences_;

	// Token: 0x04000D74 RID: 3444
	private engineFeatures eF_;

	// Token: 0x04000D75 RID: 3445
	private cameraMovementScript cmS_;

	// Token: 0x04000D76 RID: 3446
	private unlockScript unlock_;

	// Token: 0x04000D77 RID: 3447
	private gameplayFeatures gF_;

	// Token: 0x04000D78 RID: 3448
	private games games_;

	// Token: 0x04000D79 RID: 3449
	private gameScript gS_;
}
