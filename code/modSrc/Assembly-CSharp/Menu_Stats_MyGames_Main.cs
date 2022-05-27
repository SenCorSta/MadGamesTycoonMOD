using System;
using UnityEngine;

// Token: 0x02000248 RID: 584
public class Menu_Stats_MyGames_Main : MonoBehaviour
{
	// Token: 0x06001682 RID: 5762 RVA: 0x0000FA02 File Offset: 0x0000DC02
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001683 RID: 5763 RVA: 0x000EBEFC File Offset: 0x000EA0FC
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

	// Token: 0x06001684 RID: 5764 RVA: 0x0000FA0A File Offset: 0x0000DC0A
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001685 RID: 5765 RVA: 0x0000FA30 File Offset: 0x0000DC30
	public void BUTTON_Reviews()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[124]);
	}

	// Token: 0x06001686 RID: 5766 RVA: 0x0000FA58 File Offset: 0x0000DC58
	public void BUTTON_Sells()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[231]);
	}

	// Token: 0x06001687 RID: 5767 RVA: 0x0000FA83 File Offset: 0x0000DC83
	public void BUTTON_SellsHandy()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[302]);
	}

	// Token: 0x06001688 RID: 5768 RVA: 0x0000FAAE File Offset: 0x0000DCAE
	public void BUTTON_SellsArcade()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[306]);
	}

	// Token: 0x06001689 RID: 5769 RVA: 0x0000FAD9 File Offset: 0x0000DCD9
	public void BUTTON_F2PDownloads()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[357]);
	}

	// Token: 0x0600168A RID: 5770 RVA: 0x0000FB04 File Offset: 0x0000DD04
	public void BUTTON_Umsatz()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[125]);
	}

	// Token: 0x0600168B RID: 5771 RVA: 0x0000FB2C File Offset: 0x0000DD2C
	public void BUTTON_Konzepte()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[109]);
	}

	// Token: 0x0600168C RID: 5772 RVA: 0x0000FB54 File Offset: 0x0000DD54
	public void BUTTON_MyIPs()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[315]);
	}

	// Token: 0x0600168D RID: 5773 RVA: 0x0000FB7F File Offset: 0x0000DD7F
	public void BUTTON_Fanbriefe()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[111]);
	}

	// Token: 0x0600168E RID: 5774 RVA: 0x000EBFA8 File Offset: 0x000EA1A8
	public void BUTTON_Spielberichte()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[182]);
		this.guiMain_.uiObjects[182].GetComponent<Menu_QA_ShowSpielberichtSelectGame>().Init();
	}

	// Token: 0x0600168F RID: 5775 RVA: 0x0000FBA7 File Offset: 0x0000DDA7
	public void BUTTON_Bundles()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[270]);
	}

	// Token: 0x06001690 RID: 5776 RVA: 0x0000FBD2 File Offset: 0x0000DDD2
	public void BUTTON_Auftragsspiele()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[287]);
	}

	// Token: 0x06001691 RID: 5777 RVA: 0x0000FBFD File Offset: 0x0000DDFD
	public void BUTTON_VertriebeneSpiele()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[351]);
	}

	// Token: 0x06001692 RID: 5778 RVA: 0x0000FC28 File Offset: 0x0000DE28
	public void BUTTON_Tochterfirmen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[394]);
	}

	// Token: 0x04001A7A RID: 6778
	public GameObject[] uiObjects;

	// Token: 0x04001A7B RID: 6779
	private roomScript rS_;

	// Token: 0x04001A7C RID: 6780
	private GameObject main_;

	// Token: 0x04001A7D RID: 6781
	private mainScript mS_;

	// Token: 0x04001A7E RID: 6782
	private textScript tS_;

	// Token: 0x04001A7F RID: 6783
	private GUI_Main guiMain_;

	// Token: 0x04001A80 RID: 6784
	private sfxScript sfx_;
}
