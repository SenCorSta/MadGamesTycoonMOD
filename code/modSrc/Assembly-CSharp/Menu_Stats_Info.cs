using System;
using UnityEngine;

// Token: 0x0200023F RID: 575
public class Menu_Stats_Info : MonoBehaviour
{
	// Token: 0x06001625 RID: 5669 RVA: 0x000E1C6E File Offset: 0x000DFE6E
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001626 RID: 5670 RVA: 0x000E1C78 File Offset: 0x000DFE78
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

	// Token: 0x06001627 RID: 5671 RVA: 0x000E1D22 File Offset: 0x000DFF22
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001628 RID: 5672 RVA: 0x000E1D48 File Offset: 0x000DFF48
	public void BUTTON_Awards()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[144]);
		this.guiMain_.uiObjects[144].GetComponent<Menu_Stats_Awards>().Init(this.mS_.myPubS_);
	}

	// Token: 0x06001629 RID: 5673 RVA: 0x000E1DA4 File Offset: 0x000DFFA4
	public void BUTTON_MadGamesAwards()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[288]);
	}

	// Token: 0x0600162A RID: 5674 RVA: 0x000E1DCF File Offset: 0x000DFFCF
	public void BUTTON_Marktanalyse()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[136]);
	}

	// Token: 0x0600162B RID: 5675 RVA: 0x000E1DFA File Offset: 0x000DFFFA
	public void BUTTON_History()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[135]);
	}

	// Token: 0x0600162C RID: 5676 RVA: 0x000E1E25 File Offset: 0x000E0025
	public void BUTTON_Verkauf()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[127]);
	}

	// Token: 0x0600162D RID: 5677 RVA: 0x000E1E4D File Offset: 0x000E004D
	public void BUTTON_Abos()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[130]);
	}

	// Token: 0x0600162E RID: 5678 RVA: 0x000E1E78 File Offset: 0x000E0078
	public void BUTTON_Download()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[129]);
	}

	// Token: 0x0600162F RID: 5679 RVA: 0x000E1EA3 File Offset: 0x000E00A3
	public void BUTTON_Fans()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[128]);
	}

	// Token: 0x06001630 RID: 5680 RVA: 0x000E1ECE File Offset: 0x000E00CE
	public void BUTTON_GenreBeliebtheit()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[280]);
	}

	// Token: 0x06001631 RID: 5681 RVA: 0x000E1EF9 File Offset: 0x000E00F9
	public void BUTTON_Fanshop()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[372]);
	}

	// Token: 0x04001A28 RID: 6696
	public GameObject[] uiObjects;

	// Token: 0x04001A29 RID: 6697
	private roomScript rS_;

	// Token: 0x04001A2A RID: 6698
	private GameObject main_;

	// Token: 0x04001A2B RID: 6699
	private mainScript mS_;

	// Token: 0x04001A2C RID: 6700
	private textScript tS_;

	// Token: 0x04001A2D RID: 6701
	private GUI_Main guiMain_;

	// Token: 0x04001A2E RID: 6702
	private sfxScript sfx_;
}
