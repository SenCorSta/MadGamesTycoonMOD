using System;
using UnityEngine;

// Token: 0x0200024E RID: 590
public class Menu_Stats_MyKonsolen_Main : MonoBehaviour
{
	// Token: 0x060016D0 RID: 5840 RVA: 0x0000FF43 File Offset: 0x0000E143
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060016D1 RID: 5841 RVA: 0x000ED370 File Offset: 0x000EB570
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

	// Token: 0x060016D2 RID: 5842 RVA: 0x0000FF4B File Offset: 0x0000E14B
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060016D3 RID: 5843 RVA: 0x0000FF71 File Offset: 0x0000E171
	public void BUTTON_Umsatz()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[336]);
	}

	// Token: 0x060016D4 RID: 5844 RVA: 0x0000FF9C File Offset: 0x0000E19C
	public void BUTTON_Sells()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[335]);
	}

	// Token: 0x060016D5 RID: 5845 RVA: 0x0000FFC7 File Offset: 0x0000E1C7
	public void BUTTON_SellsVerlauf()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[337]);
	}

	// Token: 0x060016D6 RID: 5846 RVA: 0x0000FFF2 File Offset: 0x0000E1F2
	public void BUTTON_TechnicalData()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[338]);
	}

	// Token: 0x060016D7 RID: 5847 RVA: 0x0001001D File Offset: 0x0000E21D
	public void BUTTON_Games()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[341]);
	}

	// Token: 0x060016D8 RID: 5848 RVA: 0x00010048 File Offset: 0x0000E248
	public void BUTTON_AllTimeCharts()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[343]);
	}

	// Token: 0x04001AAE RID: 6830
	public GameObject[] uiObjects;

	// Token: 0x04001AAF RID: 6831
	private roomScript rS_;

	// Token: 0x04001AB0 RID: 6832
	private GameObject main_;

	// Token: 0x04001AB1 RID: 6833
	private mainScript mS_;

	// Token: 0x04001AB2 RID: 6834
	private textScript tS_;

	// Token: 0x04001AB3 RID: 6835
	private GUI_Main guiMain_;

	// Token: 0x04001AB4 RID: 6836
	private sfxScript sfx_;
}
