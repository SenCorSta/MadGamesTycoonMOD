using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200014C RID: 332
public class Menu_Dev_USK : MonoBehaviour
{
	// Token: 0x06000C2E RID: 3118 RVA: 0x00083272 File Offset: 0x00081472
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000C2F RID: 3119 RVA: 0x0008327C File Offset: 0x0008147C
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

	// Token: 0x06000C30 RID: 3120 RVA: 0x0008341A File Offset: 0x0008161A
	private void OnEnable()
	{
		this.FindScripts();
		if (!this.mS_.settings_TutorialOff)
		{
			this.guiMain_.SetTutorialStep(17);
		}
	}

	// Token: 0x06000C31 RID: 3121 RVA: 0x0008343C File Offset: 0x0008163C
	public void Init(gameScript game_)
	{
		this.FindScripts();
		this.gS_ = game_;
		string text = this.tS_.GetText(991);
		text = text.Replace("<NAME>", this.gS_.GetNameWithTag());
		this.uiObjects[0].GetComponent<Text>().text = text;
		this.uiObjects[1].GetComponent<Image>().sprite = this.games_.gamePEGI[this.gS_.usk];
	}

	// Token: 0x06000C32 RID: 3122 RVA: 0x000834BC File Offset: 0x000816BC
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		if (!this.mS_.settings_TutorialOff)
		{
			this.guiMain_.SetTutorialStep(18);
		}
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[46]);
		if (this.gS_.typ_contractGame)
		{
			this.guiMain_.uiObjects[46].GetComponent<Menu_Review>().InitContractGame(this.gS_);
			return;
		}
		this.guiMain_.uiObjects[46].GetComponent<Menu_Review>().Init(this.gS_);
	}

	// Token: 0x04001074 RID: 4212
	public GameObject[] uiObjects;

	// Token: 0x04001075 RID: 4213
	private GameObject main_;

	// Token: 0x04001076 RID: 4214
	private mainScript mS_;

	// Token: 0x04001077 RID: 4215
	private textScript tS_;

	// Token: 0x04001078 RID: 4216
	private GUI_Main guiMain_;

	// Token: 0x04001079 RID: 4217
	private sfxScript sfx_;

	// Token: 0x0400107A RID: 4218
	private genres genres_;

	// Token: 0x0400107B RID: 4219
	private themes themes_;

	// Token: 0x0400107C RID: 4220
	private licences licences_;

	// Token: 0x0400107D RID: 4221
	private engineFeatures eF_;

	// Token: 0x0400107E RID: 4222
	private cameraMovementScript cmS_;

	// Token: 0x0400107F RID: 4223
	private unlockScript unlock_;

	// Token: 0x04001080 RID: 4224
	private gameplayFeatures gF_;

	// Token: 0x04001081 RID: 4225
	private games games_;

	// Token: 0x04001082 RID: 4226
	private gameScript gS_;
}
