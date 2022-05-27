using System;
using UnityEngine;

// Token: 0x020001BF RID: 447
public class Menu_BankWarnung : MonoBehaviour
{
	// Token: 0x060010CD RID: 4301 RVA: 0x0000BD5F File Offset: 0x00009F5F
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060010CE RID: 4302 RVA: 0x000BFCA0 File Offset: 0x000BDEA0
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

	// Token: 0x060010CF RID: 4303 RVA: 0x0000BD67 File Offset: 0x00009F67
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x060010D0 RID: 4304 RVA: 0x0000BD82 File Offset: 0x00009F82
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060010D1 RID: 4305 RVA: 0x0000BDA8 File Offset: 0x00009FA8
	public void BUTTON_Yes()
	{
		this.BUTTON_Abbrechen();
	}

	// Token: 0x04001566 RID: 5478
	public GameObject[] uiObjects;

	// Token: 0x04001567 RID: 5479
	private GameObject main_;

	// Token: 0x04001568 RID: 5480
	private mainScript mS_;

	// Token: 0x04001569 RID: 5481
	private textScript tS_;

	// Token: 0x0400156A RID: 5482
	private GUI_Main guiMain_;

	// Token: 0x0400156B RID: 5483
	private sfxScript sfx_;
}
