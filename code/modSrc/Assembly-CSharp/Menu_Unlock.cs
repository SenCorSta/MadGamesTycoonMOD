using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001B9 RID: 441
public class Menu_Unlock : MonoBehaviour
{
	// Token: 0x06001097 RID: 4247 RVA: 0x0000BBE9 File Offset: 0x00009DE9
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001098 RID: 4248 RVA: 0x000BC89C File Offset: 0x000BAA9C
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

	// Token: 0x06001099 RID: 4249 RVA: 0x0000BBF1 File Offset: 0x00009DF1
	public void Init(string c, bool close)
	{
		this.FindScripts();
		this.uiObjects[0].GetComponent<Text>().text = c;
		this.closeMenu = close;
	}

	// Token: 0x0600109A RID: 4250 RVA: 0x0000BC13 File Offset: 0x00009E13
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x0600109B RID: 4251 RVA: 0x0000BC2E File Offset: 0x00009E2E
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.unlock_.CheckUnlock(true);
	}

	// Token: 0x0600109C RID: 4252 RVA: 0x0000BC55 File Offset: 0x00009E55
	public void BUTTON_Yes()
	{
		if (this.closeMenu)
		{
			this.guiMain_.CloseMenu();
		}
		this.BUTTON_Abbrechen();
	}

	// Token: 0x04001515 RID: 5397
	public GameObject[] uiObjects;

	// Token: 0x04001516 RID: 5398
	private GameObject main_;

	// Token: 0x04001517 RID: 5399
	private mainScript mS_;

	// Token: 0x04001518 RID: 5400
	private textScript tS_;

	// Token: 0x04001519 RID: 5401
	private GUI_Main guiMain_;

	// Token: 0x0400151A RID: 5402
	private sfxScript sfx_;

	// Token: 0x0400151B RID: 5403
	private unlockScript unlock_;

	// Token: 0x0400151C RID: 5404
	private bool closeMenu;
}
