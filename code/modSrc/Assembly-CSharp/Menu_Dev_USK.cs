using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200014B RID: 331
public class Menu_Dev_USK : MonoBehaviour
{
	// Token: 0x06000C19 RID: 3097 RVA: 0x0000881E File Offset: 0x00006A1E
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000C1A RID: 3098 RVA: 0x00092918 File Offset: 0x00090B18
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

	// Token: 0x06000C1B RID: 3099 RVA: 0x00008826 File Offset: 0x00006A26
	private void OnEnable()
	{
		this.FindScripts();
		if (!this.mS_.settings_TutorialOff)
		{
			this.guiMain_.SetTutorialStep(17);
		}
	}

	// Token: 0x06000C1C RID: 3100 RVA: 0x00092AB8 File Offset: 0x00090CB8
	public void Init(gameScript game_)
	{
		this.FindScripts();
		this.gS_ = game_;
		string text = this.tS_.GetText(991);
		text = text.Replace("<NAME>", this.gS_.GetNameWithTag());
		this.uiObjects[0].GetComponent<Text>().text = text;
		this.uiObjects[1].GetComponent<Image>().sprite = this.games_.gamePEGI[this.gS_.usk];
	}

	// Token: 0x06000C1D RID: 3101 RVA: 0x00092B38 File Offset: 0x00090D38
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

	// Token: 0x0400106C RID: 4204
	public GameObject[] uiObjects;

	// Token: 0x0400106D RID: 4205
	private GameObject main_;

	// Token: 0x0400106E RID: 4206
	private mainScript mS_;

	// Token: 0x0400106F RID: 4207
	private textScript tS_;

	// Token: 0x04001070 RID: 4208
	private GUI_Main guiMain_;

	// Token: 0x04001071 RID: 4209
	private sfxScript sfx_;

	// Token: 0x04001072 RID: 4210
	private genres genres_;

	// Token: 0x04001073 RID: 4211
	private themes themes_;

	// Token: 0x04001074 RID: 4212
	private licences licences_;

	// Token: 0x04001075 RID: 4213
	private engineFeatures eF_;

	// Token: 0x04001076 RID: 4214
	private cameraMovementScript cmS_;

	// Token: 0x04001077 RID: 4215
	private unlockScript unlock_;

	// Token: 0x04001078 RID: 4216
	private gameplayFeatures gF_;

	// Token: 0x04001079 RID: 4217
	private games games_;

	// Token: 0x0400107A RID: 4218
	private gameScript gS_;
}
