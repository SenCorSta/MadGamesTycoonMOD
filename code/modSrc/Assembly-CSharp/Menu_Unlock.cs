using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001BA RID: 442
public class Menu_Unlock : MonoBehaviour
{
	// Token: 0x060010B1 RID: 4273 RVA: 0x000B0B4C File Offset: 0x000AED4C
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060010B2 RID: 4274 RVA: 0x000B0B54 File Offset: 0x000AED54
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
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
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

	// Token: 0x060010B3 RID: 4275 RVA: 0x000B0C1C File Offset: 0x000AEE1C
	public void Init(string c, bool close)
	{
		this.FindScripts();
		this.uiObjects[0].GetComponent<Text>().text = c;
		this.closeMenu = close;
	}

	// Token: 0x060010B4 RID: 4276 RVA: 0x000B0C3E File Offset: 0x000AEE3E
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x060010B5 RID: 4277 RVA: 0x000B0C59 File Offset: 0x000AEE59
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.unlock_.CheckUnlock(true);
	}

	// Token: 0x060010B6 RID: 4278 RVA: 0x000B0C80 File Offset: 0x000AEE80
	public void BUTTON_Yes()
	{
		if (this.closeMenu)
		{
			this.guiMain_.CloseMenu();
		}
		this.BUTTON_Abbrechen();
	}

	// Token: 0x04001520 RID: 5408
	public GameObject[] uiObjects;

	// Token: 0x04001521 RID: 5409
	private GameObject main_;

	// Token: 0x04001522 RID: 5410
	private mainScript mS_;

	// Token: 0x04001523 RID: 5411
	private textScript tS_;

	// Token: 0x04001524 RID: 5412
	private GUI_Main guiMain_;

	// Token: 0x04001525 RID: 5413
	private sfxScript sfx_;

	// Token: 0x04001526 RID: 5414
	private unlockScript unlock_;

	// Token: 0x04001527 RID: 5415
	private bool closeMenu;
}
