using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000156 RID: 342
public class Menu_ReleaseDate : MonoBehaviour
{
	// Token: 0x06000C9F RID: 3231 RVA: 0x00089464 File Offset: 0x00087664
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000CA0 RID: 3232 RVA: 0x0008946C File Offset: 0x0008766C
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

	// Token: 0x06000CA1 RID: 3233 RVA: 0x0008960A File Offset: 0x0008780A
	public void Init(gameScript game_, taskGame t_)
	{
		this.FindScripts();
		this.gS_ = game_;
		this.task_ = t_;
		this.uiObjects[0].GetComponent<Text>().text = this.gS_.GetNameWithTag();
		this.SLIDER_Wochen();
	}

	// Token: 0x06000CA2 RID: 3234 RVA: 0x00089644 File Offset: 0x00087844
	public void SLIDER_Wochen()
	{
		if (this.uiObjects[2].GetComponent<Slider>().value > 1f)
		{
			string text = this.tS_.GetText(1123);
			text = text.Replace("<NUM>", Mathf.RoundToInt(this.uiObjects[2].GetComponent<Slider>().value).ToString());
			this.uiObjects[1].GetComponent<Text>().text = text;
			return;
		}
		this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(1864);
	}

	// Token: 0x06000CA3 RID: 3235 RVA: 0x000896DC File Offset: 0x000878DC
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000CA4 RID: 3236 RVA: 0x000896F8 File Offset: 0x000878F8
	public void BUTTON_Ok()
	{
		this.sfx_.PlaySound(3, true);
		if (this.task_)
		{
			UnityEngine.Object.Destroy(this.task_.gameObject);
		}
		this.gS_.SetPublisher(this.mS_.myID);
		this.gS_.SetOnMarket();
		this.gS_.releaseDate = Mathf.RoundToInt(this.uiObjects[2].GetComponent<Slider>().value);
		if (!this.gS_.typ_budget && !this.gS_.typ_bundle && !this.gS_.typ_bundleAddon && !this.gS_.typ_goty)
		{
			this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[71]);
			this.guiMain_.uiObjects[71].GetComponent<Menu_Dev_XP>().Init(this.gS_);
		}
		else
		{
			this.guiMain_.CloseMenu();
		}
		this.guiMain_.uiObjects[218].SetActive(false);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001103 RID: 4355
	public GameObject[] uiObjects;

	// Token: 0x04001104 RID: 4356
	private GameObject main_;

	// Token: 0x04001105 RID: 4357
	private mainScript mS_;

	// Token: 0x04001106 RID: 4358
	private textScript tS_;

	// Token: 0x04001107 RID: 4359
	private GUI_Main guiMain_;

	// Token: 0x04001108 RID: 4360
	private sfxScript sfx_;

	// Token: 0x04001109 RID: 4361
	private genres genres_;

	// Token: 0x0400110A RID: 4362
	private themes themes_;

	// Token: 0x0400110B RID: 4363
	private licences licences_;

	// Token: 0x0400110C RID: 4364
	private engineFeatures eF_;

	// Token: 0x0400110D RID: 4365
	private cameraMovementScript cmS_;

	// Token: 0x0400110E RID: 4366
	private unlockScript unlock_;

	// Token: 0x0400110F RID: 4367
	private gameplayFeatures gF_;

	// Token: 0x04001110 RID: 4368
	private games games_;

	// Token: 0x04001111 RID: 4369
	private gameScript gS_;

	// Token: 0x04001112 RID: 4370
	private taskGame task_;
}
