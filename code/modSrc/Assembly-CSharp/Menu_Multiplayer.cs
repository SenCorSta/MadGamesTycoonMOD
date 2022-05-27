using System;
using UnityEngine;

// Token: 0x020001D0 RID: 464
public class Menu_Multiplayer : MonoBehaviour
{
	// Token: 0x0600117E RID: 4478 RVA: 0x0000C403 File Offset: 0x0000A603
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600117F RID: 4479 RVA: 0x000C5158 File Offset: 0x000C3358
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindGameObjectWithTag("Main");
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

	// Token: 0x06001180 RID: 4480 RVA: 0x0000C40B File Offset: 0x0000A60B
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06001181 RID: 4481 RVA: 0x0000C403 File Offset: 0x0000A603
	public void Init()
	{
		this.FindScripts();
	}

	// Token: 0x06001182 RID: 4482 RVA: 0x0000C413 File Offset: 0x0000A613
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001183 RID: 4483 RVA: 0x0000C439 File Offset: 0x0000A639
	public void BUTTON_Unterstuetzen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[260]);
	}

	// Token: 0x06001184 RID: 4484 RVA: 0x0000C464 File Offset: 0x0000A664
	public void BUTTON_Sabotieren()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.MessageBox(this.tS_.GetText(408), false);
	}

	// Token: 0x06001185 RID: 4485 RVA: 0x0000C48F File Offset: 0x0000A68F
	public void BUTTON_Awards()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[276]);
	}

	// Token: 0x0400160B RID: 5643
	public GameObject[] uiObjects;

	// Token: 0x0400160C RID: 5644
	private roomScript rS_;

	// Token: 0x0400160D RID: 5645
	private GameObject main_;

	// Token: 0x0400160E RID: 5646
	private mainScript mS_;

	// Token: 0x0400160F RID: 5647
	private textScript tS_;

	// Token: 0x04001610 RID: 5648
	private GUI_Main guiMain_;

	// Token: 0x04001611 RID: 5649
	private sfxScript sfx_;

	// Token: 0x04001612 RID: 5650
	private unlockScript unlock_;
}
