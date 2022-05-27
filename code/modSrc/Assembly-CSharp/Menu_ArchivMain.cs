using System;
using UnityEngine;

// Token: 0x02000186 RID: 390
public class Menu_ArchivMain : MonoBehaviour
{
	// Token: 0x06000EB2 RID: 3762 RVA: 0x0009D8E1 File Offset: 0x0009BAE1
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000EB3 RID: 3763 RVA: 0x0009D8EC File Offset: 0x0009BAEC
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
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
	}

	// Token: 0x06000EB4 RID: 3764 RVA: 0x0009D9B4 File Offset: 0x0009BBB4
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06000EB5 RID: 3765 RVA: 0x0009D8E1 File Offset: 0x0009BAE1
	public void Init()
	{
		this.FindScripts();
	}

	// Token: 0x06000EB6 RID: 3766 RVA: 0x0009D9BC File Offset: 0x0009BBBC
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000EB7 RID: 3767 RVA: 0x0009D9E2 File Offset: 0x0009BBE2
	public void BUTTON_Spielkonzepte()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[290]);
	}

	// Token: 0x06000EB8 RID: 3768 RVA: 0x0009DA0D File Offset: 0x0009BC0D
	public void BUTTON_Spielberichte()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[291]);
	}

	// Token: 0x06000EB9 RID: 3769 RVA: 0x0009DA38 File Offset: 0x0009BC38
	public void BUTTON_Engines()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[293]);
	}

	// Token: 0x06000EBA RID: 3770 RVA: 0x0009DA63 File Offset: 0x0009BC63
	public void BUTTON_Fanbriefe()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[292]);
	}

	// Token: 0x04001319 RID: 4889
	public GameObject[] uiObjects;

	// Token: 0x0400131A RID: 4890
	private GameObject main_;

	// Token: 0x0400131B RID: 4891
	private mainScript mS_;

	// Token: 0x0400131C RID: 4892
	private textScript tS_;

	// Token: 0x0400131D RID: 4893
	private GUI_Main guiMain_;

	// Token: 0x0400131E RID: 4894
	private sfxScript sfx_;

	// Token: 0x0400131F RID: 4895
	private unlockScript unlock_;
}
