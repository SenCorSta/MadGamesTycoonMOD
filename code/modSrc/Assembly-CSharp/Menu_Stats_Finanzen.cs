using System;
using UnityEngine;

// Token: 0x0200023B RID: 571
public class Menu_Stats_Finanzen : MonoBehaviour
{
	// Token: 0x06001603 RID: 5635 RVA: 0x000E0FD0 File Offset: 0x000DF1D0
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001604 RID: 5636 RVA: 0x000E0FD8 File Offset: 0x000DF1D8
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

	// Token: 0x06001605 RID: 5637 RVA: 0x000E1082 File Offset: 0x000DF282
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001606 RID: 5638 RVA: 0x000E10A8 File Offset: 0x000DF2A8
	public void BUTTON_Monatsbilanz()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[132]);
	}

	// Token: 0x06001607 RID: 5639 RVA: 0x000E10D3 File Offset: 0x000DF2D3
	public void BUTTON_Jahresbilanz()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[133]);
	}

	// Token: 0x06001608 RID: 5640 RVA: 0x000E10FE File Offset: 0x000DF2FE
	public void BUTTON_Finanzverlauf()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[134]);
	}

	// Token: 0x06001609 RID: 5641 RVA: 0x000E1129 File Offset: 0x000DF329
	public void BUTTON_Monatsverlauf()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[137]);
	}

	// Token: 0x04001A00 RID: 6656
	public GameObject[] uiObjects;

	// Token: 0x04001A01 RID: 6657
	private roomScript rS_;

	// Token: 0x04001A02 RID: 6658
	private GameObject main_;

	// Token: 0x04001A03 RID: 6659
	private mainScript mS_;

	// Token: 0x04001A04 RID: 6660
	private textScript tS_;

	// Token: 0x04001A05 RID: 6661
	private GUI_Main guiMain_;

	// Token: 0x04001A06 RID: 6662
	private sfxScript sfx_;
}
