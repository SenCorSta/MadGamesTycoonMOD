using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000124 RID: 292
public class Menu_Dev_Addon : MonoBehaviour
{
	// Token: 0x06000A1D RID: 2589 RVA: 0x0000751B File Offset: 0x0000571B
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000A1E RID: 2590 RVA: 0x0007FA48 File Offset: 0x0007DC48
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

	// Token: 0x06000A1F RID: 2591 RVA: 0x0007FB10 File Offset: 0x0007DD10
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

	// Token: 0x06000A20 RID: 2592 RVA: 0x0007FB64 File Offset: 0x0007DD64
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

	// Token: 0x06000A21 RID: 2593 RVA: 0x00007523 File Offset: 0x00005723
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000A22 RID: 2594 RVA: 0x0007FBB8 File Offset: 0x0007DDB8
	public void BUTTON_Update()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[105]);
		this.guiMain_.uiObjects[105].GetComponent<Menu_Dev_UpdateSelectGame>().Init(this.rS_, 0);
	}

	// Token: 0x06000A23 RID: 2595 RVA: 0x0007FC18 File Offset: 0x0007DE18
	public void BUTTON_Addon()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[189]);
		this.guiMain_.uiObjects[189].GetComponent<Menu_Dev_AddonSelectGame>().Init(this.rS_);
	}

	// Token: 0x06000A24 RID: 2596 RVA: 0x0007FC7C File Offset: 0x0007DE7C
	public void BUTTON_MMOAddon()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[246]);
		this.guiMain_.uiObjects[246].GetComponent<Menu_Dev_AddonMMOSelectGame>().Init(this.rS_);
	}

	// Token: 0x06000A25 RID: 2597 RVA: 0x0007FCE0 File Offset: 0x0007DEE0
	public void BUTTON_F2PUpdate()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[299]);
		this.guiMain_.uiObjects[299].GetComponent<Menu_Dev_F2PUpdateSelectGame>().Init(this.rS_);
	}

	// Token: 0x04000E6F RID: 3695
	public GameObject[] uiObjects;

	// Token: 0x04000E70 RID: 3696
	private roomScript rS_;

	// Token: 0x04000E71 RID: 3697
	private GameObject main_;

	// Token: 0x04000E72 RID: 3698
	private mainScript mS_;

	// Token: 0x04000E73 RID: 3699
	private textScript tS_;

	// Token: 0x04000E74 RID: 3700
	private GUI_Main guiMain_;

	// Token: 0x04000E75 RID: 3701
	private sfxScript sfx_;

	// Token: 0x04000E76 RID: 3702
	private unlockScript unlock_;
}
