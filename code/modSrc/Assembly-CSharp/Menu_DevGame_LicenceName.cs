using System;
using UnityEngine;

// Token: 0x02000120 RID: 288
public class Menu_DevGame_LicenceName : MonoBehaviour
{
	// Token: 0x060009F8 RID: 2552 RVA: 0x0006D0DD File Offset: 0x0006B2DD
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060009F9 RID: 2553 RVA: 0x0006D0E8 File Offset: 0x0006B2E8
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
	}

	// Token: 0x060009FA RID: 2554 RVA: 0x0006D192 File Offset: 0x0006B392
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060009FB RID: 2555 RVA: 0x0006D1AD File Offset: 0x0006B3AD
	public void BUTTON_Yes()
	{
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().SetLicenceName();
		this.sfx_.PlaySound(3, true);
		this.BUTTON_Abbrechen();
	}

	// Token: 0x04000E45 RID: 3653
	public GameObject[] uiObjects;

	// Token: 0x04000E46 RID: 3654
	private GameObject main_;

	// Token: 0x04000E47 RID: 3655
	private mainScript mS_;

	// Token: 0x04000E48 RID: 3656
	private textScript tS_;

	// Token: 0x04000E49 RID: 3657
	private GUI_Main guiMain_;

	// Token: 0x04000E4A RID: 3658
	private sfxScript sfx_;
}
