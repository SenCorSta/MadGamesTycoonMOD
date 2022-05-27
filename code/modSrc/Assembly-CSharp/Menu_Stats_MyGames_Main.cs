using System;
using UnityEngine;

// Token: 0x02000249 RID: 585
public class Menu_Stats_MyGames_Main : MonoBehaviour
{
	// Token: 0x060016A4 RID: 5796 RVA: 0x000E4B5A File Offset: 0x000E2D5A
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060016A5 RID: 5797 RVA: 0x000E4B64 File Offset: 0x000E2D64
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

	// Token: 0x060016A6 RID: 5798 RVA: 0x000E4C0E File Offset: 0x000E2E0E
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060016A7 RID: 5799 RVA: 0x000E4C34 File Offset: 0x000E2E34
	public void BUTTON_Reviews()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[124]);
	}

	// Token: 0x060016A8 RID: 5800 RVA: 0x000E4C5C File Offset: 0x000E2E5C
	public void BUTTON_Sells()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[231]);
	}

	// Token: 0x060016A9 RID: 5801 RVA: 0x000E4C87 File Offset: 0x000E2E87
	public void BUTTON_SellsHandy()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[302]);
	}

	// Token: 0x060016AA RID: 5802 RVA: 0x000E4CB2 File Offset: 0x000E2EB2
	public void BUTTON_SellsArcade()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[306]);
	}

	// Token: 0x060016AB RID: 5803 RVA: 0x000E4CDD File Offset: 0x000E2EDD
	public void BUTTON_F2PDownloads()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[357]);
	}

	// Token: 0x060016AC RID: 5804 RVA: 0x000E4D08 File Offset: 0x000E2F08
	public void BUTTON_Umsatz()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[125]);
	}

	// Token: 0x060016AD RID: 5805 RVA: 0x000E4D30 File Offset: 0x000E2F30
	public void BUTTON_Konzepte()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[109]);
	}

	// Token: 0x060016AE RID: 5806 RVA: 0x000E4D58 File Offset: 0x000E2F58
	public void BUTTON_MyIPs()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[315]);
	}

	// Token: 0x060016AF RID: 5807 RVA: 0x000E4D83 File Offset: 0x000E2F83
	public void BUTTON_Fanbriefe()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[111]);
	}

	// Token: 0x060016B0 RID: 5808 RVA: 0x000E4DAC File Offset: 0x000E2FAC
	public void BUTTON_Spielberichte()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[182]);
		this.guiMain_.uiObjects[182].GetComponent<Menu_QA_ShowSpielberichtSelectGame>().Init();
	}

	// Token: 0x060016B1 RID: 5809 RVA: 0x000E4DFD File Offset: 0x000E2FFD
	public void BUTTON_Bundles()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[270]);
	}

	// Token: 0x060016B2 RID: 5810 RVA: 0x000E4E28 File Offset: 0x000E3028
	public void BUTTON_Auftragsspiele()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[287]);
	}

	// Token: 0x060016B3 RID: 5811 RVA: 0x000E4E53 File Offset: 0x000E3053
	public void BUTTON_VertriebeneSpiele()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[351]);
	}

	// Token: 0x060016B4 RID: 5812 RVA: 0x000E4E7E File Offset: 0x000E307E
	public void BUTTON_Tochterfirmen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[394]);
	}

	// Token: 0x04001A83 RID: 6787
	public GameObject[] uiObjects;

	// Token: 0x04001A84 RID: 6788
	private roomScript rS_;

	// Token: 0x04001A85 RID: 6789
	private GameObject main_;

	// Token: 0x04001A86 RID: 6790
	private mainScript mS_;

	// Token: 0x04001A87 RID: 6791
	private textScript tS_;

	// Token: 0x04001A88 RID: 6792
	private GUI_Main guiMain_;

	// Token: 0x04001A89 RID: 6793
	private sfxScript sfx_;
}
