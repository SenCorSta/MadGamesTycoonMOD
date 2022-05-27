using System;
using UnityEngine;

// Token: 0x02000227 RID: 551
public class Menu_Statistics_DevPubsMain : MonoBehaviour
{
	// Token: 0x06001523 RID: 5411 RVA: 0x0000E7FE File Offset: 0x0000C9FE
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001524 RID: 5412 RVA: 0x000E2458 File Offset: 0x000E0658
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

	// Token: 0x06001525 RID: 5413 RVA: 0x0000E806 File Offset: 0x0000CA06
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001526 RID: 5414 RVA: 0x0000E82C File Offset: 0x0000CA2C
	public void BUTTON_Publisher()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[119]);
	}

	// Token: 0x06001527 RID: 5415 RVA: 0x0000E854 File Offset: 0x0000CA54
	public void BUTTON_Entwickler()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[120]);
	}

	// Token: 0x06001528 RID: 5416 RVA: 0x0000E87C File Offset: 0x0000CA7C
	public void BUTTON_Tochterfirmen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[385]);
	}

	// Token: 0x0400192C RID: 6444
	public GameObject[] uiObjects;

	// Token: 0x0400192D RID: 6445
	private roomScript rS_;

	// Token: 0x0400192E RID: 6446
	private GameObject main_;

	// Token: 0x0400192F RID: 6447
	private mainScript mS_;

	// Token: 0x04001930 RID: 6448
	private textScript tS_;

	// Token: 0x04001931 RID: 6449
	private GUI_Main guiMain_;

	// Token: 0x04001932 RID: 6450
	private sfxScript sfx_;
}
