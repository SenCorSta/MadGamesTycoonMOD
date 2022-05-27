using System;
using UnityEngine;

// Token: 0x0200012B RID: 299
public class Menu_Dev_Auftrag : MonoBehaviour
{
	// Token: 0x06000A9D RID: 2717 RVA: 0x000733C3 File Offset: 0x000715C3
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000A9E RID: 2718 RVA: 0x000733CC File Offset: 0x000715CC
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

	// Token: 0x06000A9F RID: 2719 RVA: 0x00073494 File Offset: 0x00071694
	public void Init(roomScript script_)
	{
		this.FindScripts();
		if (!script_)
		{
			this.BUTTON_Abbrechen();
			return;
		}
		this.rS_ = script_;
	}

	// Token: 0x06000AA0 RID: 2720 RVA: 0x000734B2 File Offset: 0x000716B2
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000AA1 RID: 2721 RVA: 0x000734D8 File Offset: 0x000716D8
	public void BUTTON_Komponente()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[96]);
		this.guiMain_.uiObjects[96].GetComponent<Menu_Dev_AuftragSelect>().Init(this.rS_);
	}

	// Token: 0x06000AA2 RID: 2722 RVA: 0x00073538 File Offset: 0x00071738
	public void BUTTON_Auftragsspiel()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[99]);
		this.guiMain_.uiObjects[99].GetComponent<Menu_Dev_Auftragsspiel>().Init(this.rS_);
	}

	// Token: 0x04000ECB RID: 3787
	public GameObject[] uiObjects;

	// Token: 0x04000ECC RID: 3788
	private roomScript rS_;

	// Token: 0x04000ECD RID: 3789
	private GameObject main_;

	// Token: 0x04000ECE RID: 3790
	private mainScript mS_;

	// Token: 0x04000ECF RID: 3791
	private textScript tS_;

	// Token: 0x04000ED0 RID: 3792
	private GUI_Main guiMain_;

	// Token: 0x04000ED1 RID: 3793
	private sfxScript sfx_;

	// Token: 0x04000ED2 RID: 3794
	private unlockScript unlock_;
}
