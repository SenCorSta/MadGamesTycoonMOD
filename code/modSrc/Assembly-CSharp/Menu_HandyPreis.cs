using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000151 RID: 337
public class Menu_HandyPreis : MonoBehaviour
{
	// Token: 0x06000C5E RID: 3166 RVA: 0x00008AB4 File Offset: 0x00006CB4
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000C5F RID: 3167 RVA: 0x000952DC File Offset: 0x000934DC
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

	// Token: 0x06000C60 RID: 3168 RVA: 0x0009547C File Offset: 0x0009367C
	public void Init(gameScript game_)
	{
		this.FindScripts();
		this.gS_ = game_;
		this.uiObjects[0].GetComponent<Text>().text = this.gS_.GetNameWithTag();
		if (this.gS_.verkaufspreis[3] <= 0)
		{
			this.uiObjects[2].GetComponent<Slider>().value = 2f;
		}
		else
		{
			this.uiObjects[2].GetComponent<Slider>().value = (float)this.gS_.verkaufspreis[3];
		}
		this.SLIDER_Preis();
		if (!this.guiMain_.uiObjects[308].activeSelf)
		{
			this.uiObjects[4].SetActive(false);
			return;
		}
		this.uiObjects[4].SetActive(true);
	}

	// Token: 0x06000C61 RID: 3169 RVA: 0x00095538 File Offset: 0x00093738
	public void SLIDER_Preis()
	{
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt(this.uiObjects[2].GetComponent<Slider>().value), true);
		float value = this.uiObjects[2].GetComponent<Slider>().value;
		if (1f.Equals(value))
		{
			this.uiObjects[3].GetComponent<Text>().text = "$1.29";
			return;
		}
		if (2f.Equals(value))
		{
			this.uiObjects[3].GetComponent<Text>().text = "$2.59";
			return;
		}
		if (3f.Equals(value))
		{
			this.uiObjects[3].GetComponent<Text>().text = "$3.99";
			return;
		}
		if (4f.Equals(value))
		{
			this.uiObjects[3].GetComponent<Text>().text = "$5.19";
			return;
		}
		if (!5f.Equals(value))
		{
			return;
		}
		this.uiObjects[3].GetComponent<Text>().text = "$6.49";
	}

	// Token: 0x06000C62 RID: 3170 RVA: 0x00008ABC File Offset: 0x00006CBC
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000C63 RID: 3171 RVA: 0x00095658 File Offset: 0x00093858
	public void BUTTON_Ok()
	{
		this.sfx_.PlaySound(3, true);
		this.gS_.verkaufspreis[3] = Mathf.RoundToInt(this.uiObjects[2].GetComponent<Slider>().value);
		if (!this.guiMain_.uiObjects[308].activeSelf)
		{
			this.gS_.SetPublisher(-1);
			this.gS_.SetOnMarket();
			this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[71]);
			this.guiMain_.uiObjects[71].GetComponent<Menu_Dev_XP>().Init(this.gS_);
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x040010B9 RID: 4281
	public GameObject[] uiObjects;

	// Token: 0x040010BA RID: 4282
	private GameObject main_;

	// Token: 0x040010BB RID: 4283
	private mainScript mS_;

	// Token: 0x040010BC RID: 4284
	private textScript tS_;

	// Token: 0x040010BD RID: 4285
	private GUI_Main guiMain_;

	// Token: 0x040010BE RID: 4286
	private sfxScript sfx_;

	// Token: 0x040010BF RID: 4287
	private genres genres_;

	// Token: 0x040010C0 RID: 4288
	private themes themes_;

	// Token: 0x040010C1 RID: 4289
	private licences licences_;

	// Token: 0x040010C2 RID: 4290
	private engineFeatures eF_;

	// Token: 0x040010C3 RID: 4291
	private cameraMovementScript cmS_;

	// Token: 0x040010C4 RID: 4292
	private unlockScript unlock_;

	// Token: 0x040010C5 RID: 4293
	private gameplayFeatures gF_;

	// Token: 0x040010C6 RID: 4294
	private games games_;

	// Token: 0x040010C7 RID: 4295
	private gameScript gS_;
}
