using System;
using UnityEngine;

// Token: 0x020001C0 RID: 448
public class Menu_BankWarnung : MonoBehaviour
{
	// Token: 0x060010E7 RID: 4327 RVA: 0x000B4128 File Offset: 0x000B2328
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060010E8 RID: 4328 RVA: 0x000B4130 File Offset: 0x000B2330
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

	// Token: 0x060010E9 RID: 4329 RVA: 0x000B41DA File Offset: 0x000B23DA
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x060010EA RID: 4330 RVA: 0x000B41F5 File Offset: 0x000B23F5
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060010EB RID: 4331 RVA: 0x000B421B File Offset: 0x000B241B
	public void BUTTON_Yes()
	{
		this.BUTTON_Abbrechen();
	}

	// Token: 0x0400156F RID: 5487
	public GameObject[] uiObjects;

	// Token: 0x04001570 RID: 5488
	private GameObject main_;

	// Token: 0x04001571 RID: 5489
	private mainScript mS_;

	// Token: 0x04001572 RID: 5490
	private textScript tS_;

	// Token: 0x04001573 RID: 5491
	private GUI_Main guiMain_;

	// Token: 0x04001574 RID: 5492
	private sfxScript sfx_;
}
