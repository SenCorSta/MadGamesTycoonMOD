using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000111 RID: 273
public class Menu_Abo_Preis : MonoBehaviour
{
	// Token: 0x060008E4 RID: 2276 RVA: 0x00006968 File Offset: 0x00004B68
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060008E5 RID: 2277 RVA: 0x00072504 File Offset: 0x00070704
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

	// Token: 0x060008E6 RID: 2278 RVA: 0x000726A4 File Offset: 0x000708A4
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

	// Token: 0x060008E7 RID: 2279 RVA: 0x00006970 File Offset: 0x00004B70
	public void SLIDER_AboPreis()
	{
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt(this.uiObjects[2].GetComponent<Slider>().value), true);
	}

	// Token: 0x060008E8 RID: 2280 RVA: 0x000069A8 File Offset: 0x00004BA8
	public void BUTTON_Close()
	{
		this.BUTTON_Ok();
	}

	// Token: 0x060008E9 RID: 2281 RVA: 0x00072724 File Offset: 0x00070924
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

	// Token: 0x04000D63 RID: 3427
	public GameObject[] uiObjects;

	// Token: 0x04000D64 RID: 3428
	private GameObject main_;

	// Token: 0x04000D65 RID: 3429
	private mainScript mS_;

	// Token: 0x04000D66 RID: 3430
	private textScript tS_;

	// Token: 0x04000D67 RID: 3431
	private GUI_Main guiMain_;

	// Token: 0x04000D68 RID: 3432
	private sfxScript sfx_;

	// Token: 0x04000D69 RID: 3433
	private genres genres_;

	// Token: 0x04000D6A RID: 3434
	private themes themes_;

	// Token: 0x04000D6B RID: 3435
	private licences licences_;

	// Token: 0x04000D6C RID: 3436
	private engineFeatures eF_;

	// Token: 0x04000D6D RID: 3437
	private cameraMovementScript cmS_;

	// Token: 0x04000D6E RID: 3438
	private unlockScript unlock_;

	// Token: 0x04000D6F RID: 3439
	private gameplayFeatures gF_;

	// Token: 0x04000D70 RID: 3440
	private games games_;

	// Token: 0x04000D71 RID: 3441
	private gameScript gS_;
}
