using System;
using UnityEngine;

// Token: 0x0200018F RID: 399
public class Menu_BuyLicenceMain : MonoBehaviour
{
	// Token: 0x06000F31 RID: 3889 RVA: 0x000A0FC8 File Offset: 0x0009F1C8
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000F32 RID: 3890 RVA: 0x000A0FD0 File Offset: 0x0009F1D0
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.Find("Main");
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
	}

	// Token: 0x06000F33 RID: 3891 RVA: 0x000A107A File Offset: 0x0009F27A
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		if (!this.guiMain_.uiObjects[56].activeSelf)
		{
			this.guiMain_.CloseMenu();
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000F34 RID: 3892 RVA: 0x000A10B5 File Offset: 0x0009F2B5
	public void BUTTON_BuyLicence()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[52]);
	}

	// Token: 0x06000F35 RID: 3893 RVA: 0x000A10DD File Offset: 0x0009F2DD
	public void BUTTON_SellLicence()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[54]);
	}

	// Token: 0x04001370 RID: 4976
	public GameObject[] uiObjects;

	// Token: 0x04001371 RID: 4977
	private GameObject main_;

	// Token: 0x04001372 RID: 4978
	private mainScript mS_;

	// Token: 0x04001373 RID: 4979
	private textScript tS_;

	// Token: 0x04001374 RID: 4980
	private GUI_Main guiMain_;

	// Token: 0x04001375 RID: 4981
	private sfxScript sfx_;
}
