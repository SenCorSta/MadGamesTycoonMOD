using System;
using UnityEngine;

// Token: 0x0200023A RID: 570
public class Menu_Stats_Finanzen : MonoBehaviour
{
	// Token: 0x060015E5 RID: 5605 RVA: 0x0000F0C0 File Offset: 0x0000D2C0
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060015E6 RID: 5606 RVA: 0x000E90E4 File Offset: 0x000E72E4
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

	// Token: 0x060015E7 RID: 5607 RVA: 0x0000F0C8 File Offset: 0x0000D2C8
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060015E8 RID: 5608 RVA: 0x0000F0EE File Offset: 0x0000D2EE
	public void BUTTON_Monatsbilanz()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[132]);
	}

	// Token: 0x060015E9 RID: 5609 RVA: 0x0000F119 File Offset: 0x0000D319
	public void BUTTON_Jahresbilanz()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[133]);
	}

	// Token: 0x060015EA RID: 5610 RVA: 0x0000F144 File Offset: 0x0000D344
	public void BUTTON_Finanzverlauf()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[134]);
	}

	// Token: 0x060015EB RID: 5611 RVA: 0x0000F16F File Offset: 0x0000D36F
	public void BUTTON_Monatsverlauf()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[137]);
	}

	// Token: 0x040019F7 RID: 6647
	public GameObject[] uiObjects;

	// Token: 0x040019F8 RID: 6648
	private roomScript rS_;

	// Token: 0x040019F9 RID: 6649
	private GameObject main_;

	// Token: 0x040019FA RID: 6650
	private mainScript mS_;

	// Token: 0x040019FB RID: 6651
	private textScript tS_;

	// Token: 0x040019FC RID: 6652
	private GUI_Main guiMain_;

	// Token: 0x040019FD RID: 6653
	private sfxScript sfx_;
}
