using System;
using UnityEngine;

// Token: 0x0200024F RID: 591
public class Menu_Stats_MyKonsolen_Main : MonoBehaviour
{
	// Token: 0x060016F5 RID: 5877 RVA: 0x000E66BA File Offset: 0x000E48BA
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060016F6 RID: 5878 RVA: 0x000E66C4 File Offset: 0x000E48C4
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

	// Token: 0x060016F7 RID: 5879 RVA: 0x000E676E File Offset: 0x000E496E
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060016F8 RID: 5880 RVA: 0x000E6794 File Offset: 0x000E4994
	public void BUTTON_Umsatz()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[336]);
	}

	// Token: 0x060016F9 RID: 5881 RVA: 0x000E67BF File Offset: 0x000E49BF
	public void BUTTON_Sells()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[335]);
	}

	// Token: 0x060016FA RID: 5882 RVA: 0x000E67EA File Offset: 0x000E49EA
	public void BUTTON_SellsVerlauf()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[337]);
	}

	// Token: 0x060016FB RID: 5883 RVA: 0x000E6815 File Offset: 0x000E4A15
	public void BUTTON_TechnicalData()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[338]);
	}

	// Token: 0x060016FC RID: 5884 RVA: 0x000E6840 File Offset: 0x000E4A40
	public void BUTTON_Games()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[341]);
	}

	// Token: 0x060016FD RID: 5885 RVA: 0x000E686B File Offset: 0x000E4A6B
	public void BUTTON_AllTimeCharts()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[343]);
	}

	// Token: 0x04001AB7 RID: 6839
	public GameObject[] uiObjects;

	// Token: 0x04001AB8 RID: 6840
	private roomScript rS_;

	// Token: 0x04001AB9 RID: 6841
	private GameObject main_;

	// Token: 0x04001ABA RID: 6842
	private mainScript mS_;

	// Token: 0x04001ABB RID: 6843
	private textScript tS_;

	// Token: 0x04001ABC RID: 6844
	private GUI_Main guiMain_;

	// Token: 0x04001ABD RID: 6845
	private sfxScript sfx_;
}
