using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000125 RID: 293
public class Menu_Dev_Addon : MonoBehaviour
{
	// Token: 0x06000A2C RID: 2604 RVA: 0x0006EFFD File Offset: 0x0006D1FD
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000A2D RID: 2605 RVA: 0x0006F008 File Offset: 0x0006D208
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

	// Token: 0x06000A2E RID: 2606 RVA: 0x0006F0D0 File Offset: 0x0006D2D0
	public void Init(roomScript script_)
	{
		this.FindScripts();
		if (!script_)
		{
			return;
		}
		this.rS_ = script_;
		this.Unlock(21, this.uiObjects[1], this.uiObjects[0]);
		this.Unlock(22, this.uiObjects[3], this.uiObjects[2]);
	}

	// Token: 0x06000A2F RID: 2607 RVA: 0x0006F124 File Offset: 0x0006D324
	private void Unlock(int id_, GameObject lock_, GameObject button_)
	{
		if (this.unlock_.unlock[id_])
		{
			button_.GetComponent<Button>().interactable = true;
			if (lock_)
			{
				lock_.SetActive(false);
				return;
			}
		}
		else
		{
			button_.GetComponent<Button>().interactable = false;
			if (lock_)
			{
				lock_.SetActive(true);
			}
		}
	}

	// Token: 0x06000A30 RID: 2608 RVA: 0x0006F177 File Offset: 0x0006D377
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000A31 RID: 2609 RVA: 0x0006F1A0 File Offset: 0x0006D3A0
	public void BUTTON_Update()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[105]);
		this.guiMain_.uiObjects[105].GetComponent<Menu_Dev_UpdateSelectGame>().Init(this.rS_, 0);
	}

	// Token: 0x06000A32 RID: 2610 RVA: 0x0006F200 File Offset: 0x0006D400
	public void BUTTON_Addon()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[189]);
		this.guiMain_.uiObjects[189].GetComponent<Menu_Dev_AddonSelectGame>().Init(this.rS_);
	}

	// Token: 0x06000A33 RID: 2611 RVA: 0x0006F264 File Offset: 0x0006D464
	public void BUTTON_MMOAddon()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[246]);
		this.guiMain_.uiObjects[246].GetComponent<Menu_Dev_AddonMMOSelectGame>().Init(this.rS_);
	}

	// Token: 0x06000A34 RID: 2612 RVA: 0x0006F2C8 File Offset: 0x0006D4C8
	public void BUTTON_F2PUpdate()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[299]);
		this.guiMain_.uiObjects[299].GetComponent<Menu_Dev_F2PUpdateSelectGame>().Init(this.rS_);
	}

	// Token: 0x04000E77 RID: 3703
	public GameObject[] uiObjects;

	// Token: 0x04000E78 RID: 3704
	private roomScript rS_;

	// Token: 0x04000E79 RID: 3705
	private GameObject main_;

	// Token: 0x04000E7A RID: 3706
	private mainScript mS_;

	// Token: 0x04000E7B RID: 3707
	private textScript tS_;

	// Token: 0x04000E7C RID: 3708
	private GUI_Main guiMain_;

	// Token: 0x04000E7D RID: 3709
	private sfxScript sfx_;

	// Token: 0x04000E7E RID: 3710
	private unlockScript unlock_;
}
