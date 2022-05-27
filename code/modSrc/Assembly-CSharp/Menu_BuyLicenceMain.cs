using System;
using UnityEngine;

// Token: 0x0200018E RID: 398
public class Menu_BuyLicenceMain : MonoBehaviour
{
	// Token: 0x06000F19 RID: 3865 RVA: 0x0000AB3F File Offset: 0x00008D3F
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000F1A RID: 3866 RVA: 0x000ADF50 File Offset: 0x000AC150
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

	// Token: 0x06000F1B RID: 3867 RVA: 0x0000AB47 File Offset: 0x00008D47
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		if (!this.guiMain_.uiObjects[56].activeSelf)
		{
			this.guiMain_.CloseMenu();
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000F1C RID: 3868 RVA: 0x0000AB82 File Offset: 0x00008D82
	public void BUTTON_BuyLicence()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[52]);
	}

	// Token: 0x06000F1D RID: 3869 RVA: 0x0000ABAA File Offset: 0x00008DAA
	public void BUTTON_SellLicence()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[54]);
	}

	// Token: 0x04001367 RID: 4967
	public GameObject[] uiObjects;

	// Token: 0x04001368 RID: 4968
	private GameObject main_;

	// Token: 0x04001369 RID: 4969
	private mainScript mS_;

	// Token: 0x0400136A RID: 4970
	private textScript tS_;

	// Token: 0x0400136B RID: 4971
	private GUI_Main guiMain_;

	// Token: 0x0400136C RID: 4972
	private sfxScript sfx_;
}
