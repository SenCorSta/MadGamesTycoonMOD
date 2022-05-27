using System;
using UnityEngine;

// Token: 0x0200011F RID: 287
public class Menu_DevGame_LicenceName : MonoBehaviour
{
	// Token: 0x060009E9 RID: 2537 RVA: 0x000072A4 File Offset: 0x000054A4
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060009EA RID: 2538 RVA: 0x0007DDC0 File Offset: 0x0007BFC0
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

	// Token: 0x060009EB RID: 2539 RVA: 0x000072AC File Offset: 0x000054AC
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060009EC RID: 2540 RVA: 0x000072C7 File Offset: 0x000054C7
	public void BUTTON_Yes()
	{
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().SetLicenceName();
		this.sfx_.PlaySound(3, true);
		this.BUTTON_Abbrechen();
	}

	// Token: 0x04000E3D RID: 3645
	public GameObject[] uiObjects;

	// Token: 0x04000E3E RID: 3646
	private GameObject main_;

	// Token: 0x04000E3F RID: 3647
	private mainScript mS_;

	// Token: 0x04000E40 RID: 3648
	private textScript tS_;

	// Token: 0x04000E41 RID: 3649
	private GUI_Main guiMain_;

	// Token: 0x04000E42 RID: 3650
	private sfxScript sfx_;
}
