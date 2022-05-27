using System;
using UnityEngine;

// Token: 0x020001DA RID: 474
public class Menu_W_UpdateObjects : MonoBehaviour
{
	// Token: 0x060011C5 RID: 4549 RVA: 0x0000C6D3 File Offset: 0x0000A8D3
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060011C6 RID: 4550 RVA: 0x000C776C File Offset: 0x000C596C
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

	// Token: 0x060011C7 RID: 4551 RVA: 0x0000C6DB File Offset: 0x0000A8DB
	public void Init(roomScript script_)
	{
		this.FindScripts();
		this.rS_ = script_;
	}

	// Token: 0x060011C8 RID: 4552 RVA: 0x0000C6EA File Offset: 0x0000A8EA
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060011C9 RID: 4553 RVA: 0x0000C710 File Offset: 0x0000A910
	public void BUTTON_Yes()
	{
		if (this.rS_)
		{
			this.rS_.UpdateInventar(true);
		}
		this.BUTTON_Abbrechen();
	}

	// Token: 0x04001653 RID: 5715
	public GameObject[] uiObjects;

	// Token: 0x04001654 RID: 5716
	private roomScript rS_;

	// Token: 0x04001655 RID: 5717
	private GameObject main_;

	// Token: 0x04001656 RID: 5718
	private mainScript mS_;

	// Token: 0x04001657 RID: 5719
	private textScript tS_;

	// Token: 0x04001658 RID: 5720
	private GUI_Main guiMain_;

	// Token: 0x04001659 RID: 5721
	private sfxScript sfx_;
}
