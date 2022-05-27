using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000156 RID: 342
public class Menu_ReleaseDate_F2P : MonoBehaviour
{
	// Token: 0x06000C90 RID: 3216 RVA: 0x00008C7B File Offset: 0x00006E7B
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000C91 RID: 3217 RVA: 0x000988FC File Offset: 0x00096AFC
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

	// Token: 0x06000C92 RID: 3218 RVA: 0x00008C83 File Offset: 0x00006E83
	public void Init(gameScript game_, taskGame t_)
	{
		this.FindScripts();
		this.gS_ = game_;
		this.task_ = t_;
		this.uiObjects[0].GetComponent<Text>().text = this.gS_.GetNameWithTag();
		this.SLIDER_Wochen();
	}

	// Token: 0x06000C93 RID: 3219 RVA: 0x00098A9C File Offset: 0x00096C9C
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

	// Token: 0x06000C94 RID: 3220 RVA: 0x00008CBC File Offset: 0x00006EBC
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000C95 RID: 3221 RVA: 0x00098B34 File Offset: 0x00096D34
	public void BUTTON_Ok()
	{
		this.sfx_.PlaySound(3, true);
		if (this.task_)
		{
			UnityEngine.Object.Destroy(this.task_.gameObject);
		}
		this.gS_.releaseDate = Mathf.RoundToInt(this.uiObjects[2].GetComponent<Slider>().value);
		if (this.gS_.handy && this.gS_.gameTyp != 2)
		{
			this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[301]);
			this.guiMain_.uiObjects[301].GetComponent<Menu_HandyPreis>().Init(this.gS_);
		}
		else
		{
			this.gS_.SetPublisher(-1);
			this.gS_.SetOnMarket();
			this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[71]);
			this.guiMain_.uiObjects[71].GetComponent<Menu_Dev_XP>().Init(this.gS_);
		}
		this.guiMain_.uiObjects[69].SetActive(false);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0400110B RID: 4363
	public GameObject[] uiObjects;

	// Token: 0x0400110C RID: 4364
	private GameObject main_;

	// Token: 0x0400110D RID: 4365
	private mainScript mS_;

	// Token: 0x0400110E RID: 4366
	private textScript tS_;

	// Token: 0x0400110F RID: 4367
	private GUI_Main guiMain_;

	// Token: 0x04001110 RID: 4368
	private sfxScript sfx_;

	// Token: 0x04001111 RID: 4369
	private genres genres_;

	// Token: 0x04001112 RID: 4370
	private themes themes_;

	// Token: 0x04001113 RID: 4371
	private licences licences_;

	// Token: 0x04001114 RID: 4372
	private engineFeatures eF_;

	// Token: 0x04001115 RID: 4373
	private cameraMovementScript cmS_;

	// Token: 0x04001116 RID: 4374
	private unlockScript unlock_;

	// Token: 0x04001117 RID: 4375
	private gameplayFeatures gF_;

	// Token: 0x04001118 RID: 4376
	private games games_;

	// Token: 0x04001119 RID: 4377
	private gameScript gS_;

	// Token: 0x0400111A RID: 4378
	private taskGame task_;
}
