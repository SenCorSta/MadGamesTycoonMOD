using System;
using UnityEngine;

// Token: 0x020001D1 RID: 465
public class Menu_Multiplayer : MonoBehaviour
{
	// Token: 0x06001198 RID: 4504 RVA: 0x000B9D07 File Offset: 0x000B7F07
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001199 RID: 4505 RVA: 0x000B9D10 File Offset: 0x000B7F10
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

	// Token: 0x0600119A RID: 4506 RVA: 0x000B9DD8 File Offset: 0x000B7FD8
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x0600119B RID: 4507 RVA: 0x000B9D07 File Offset: 0x000B7F07
	public void Init()
	{
		this.FindScripts();
	}

	// Token: 0x0600119C RID: 4508 RVA: 0x000B9DE0 File Offset: 0x000B7FE0
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600119D RID: 4509 RVA: 0x000B9E06 File Offset: 0x000B8006
	public void BUTTON_Unterstuetzen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[260]);
	}

	// Token: 0x0600119E RID: 4510 RVA: 0x000B9E31 File Offset: 0x000B8031
	public void BUTTON_Sabotieren()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.MessageBox(this.tS_.GetText(408), false);
	}

	// Token: 0x0600119F RID: 4511 RVA: 0x000B9E5C File Offset: 0x000B805C
	public void BUTTON_Awards()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[276]);
	}

	// Token: 0x04001614 RID: 5652
	public GameObject[] uiObjects;

	// Token: 0x04001615 RID: 5653
	private roomScript rS_;

	// Token: 0x04001616 RID: 5654
	private GameObject main_;

	// Token: 0x04001617 RID: 5655
	private mainScript mS_;

	// Token: 0x04001618 RID: 5656
	private textScript tS_;

	// Token: 0x04001619 RID: 5657
	private GUI_Main guiMain_;

	// Token: 0x0400161A RID: 5658
	private sfxScript sfx_;

	// Token: 0x0400161B RID: 5659
	private unlockScript unlock_;
}
