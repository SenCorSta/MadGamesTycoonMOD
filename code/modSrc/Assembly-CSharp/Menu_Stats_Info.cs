using System;
using UnityEngine;

// Token: 0x0200023E RID: 574
public class Menu_Stats_Info : MonoBehaviour
{
	// Token: 0x06001607 RID: 5639 RVA: 0x0000F2A5 File Offset: 0x0000D4A5
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001608 RID: 5640 RVA: 0x000E9BA0 File Offset: 0x000E7DA0
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

	// Token: 0x06001609 RID: 5641 RVA: 0x0000F2AD File Offset: 0x0000D4AD
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600160A RID: 5642 RVA: 0x0000F2D3 File Offset: 0x0000D4D3
	public void BUTTON_Awards()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[144]);
	}

	// Token: 0x0600160B RID: 5643 RVA: 0x0000F2FE File Offset: 0x0000D4FE
	public void BUTTON_MadGamesAwards()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[288]);
	}

	// Token: 0x0600160C RID: 5644 RVA: 0x0000F329 File Offset: 0x0000D529
	public void BUTTON_Marktanalyse()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[136]);
	}

	// Token: 0x0600160D RID: 5645 RVA: 0x0000F354 File Offset: 0x0000D554
	public void BUTTON_History()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[135]);
	}

	// Token: 0x0600160E RID: 5646 RVA: 0x0000F37F File Offset: 0x0000D57F
	public void BUTTON_Verkauf()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[127]);
	}

	// Token: 0x0600160F RID: 5647 RVA: 0x0000F3A7 File Offset: 0x0000D5A7
	public void BUTTON_Abos()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[130]);
	}

	// Token: 0x06001610 RID: 5648 RVA: 0x0000F3D2 File Offset: 0x0000D5D2
	public void BUTTON_Download()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[129]);
	}

	// Token: 0x06001611 RID: 5649 RVA: 0x0000F3FD File Offset: 0x0000D5FD
	public void BUTTON_Fans()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[128]);
	}

	// Token: 0x06001612 RID: 5650 RVA: 0x0000F428 File Offset: 0x0000D628
	public void BUTTON_GenreBeliebtheit()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[280]);
	}

	// Token: 0x06001613 RID: 5651 RVA: 0x0000F453 File Offset: 0x0000D653
	public void BUTTON_Fanshop()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[372]);
	}

	// Token: 0x04001A1F RID: 6687
	public GameObject[] uiObjects;

	// Token: 0x04001A20 RID: 6688
	private roomScript rS_;

	// Token: 0x04001A21 RID: 6689
	private GameObject main_;

	// Token: 0x04001A22 RID: 6690
	private mainScript mS_;

	// Token: 0x04001A23 RID: 6691
	private textScript tS_;

	// Token: 0x04001A24 RID: 6692
	private GUI_Main guiMain_;

	// Token: 0x04001A25 RID: 6693
	private sfxScript sfx_;
}
