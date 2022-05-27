using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001B7 RID: 439
public class Menu_RenameRoom : MonoBehaviour
{
	// Token: 0x06001089 RID: 4233 RVA: 0x0000BAF8 File Offset: 0x00009CF8
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600108A RID: 4234 RVA: 0x000BC1A8 File Offset: 0x000BA3A8
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
		if (!this.cmS_)
		{
			this.cmS_ = GameObject.Find("CamMovement").GetComponent<cameraMovementScript>();
		}
	}

	// Token: 0x0600108B RID: 4235 RVA: 0x0000BB00 File Offset: 0x00009D00
	public void Init(roomScript script_)
	{
		this.FindScripts();
		this.cmS_.disableMovement = true;
		if (script_)
		{
			this.rS_ = script_;
			this.uiObjects[0].GetComponent<InputField>().text = this.rS_.myName;
		}
	}

	// Token: 0x0600108C RID: 4236 RVA: 0x0000BB40 File Offset: 0x00009D40
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
		this.cmS_.disableMovement = false;
	}

	// Token: 0x0600108D RID: 4237 RVA: 0x0000BB72 File Offset: 0x00009D72
	public void BUTTON_Yes()
	{
		if (this.rS_)
		{
			this.rS_.myName = this.uiObjects[0].GetComponent<InputField>().text;
		}
		this.BUTTON_Abbrechen();
	}

	// Token: 0x04001503 RID: 5379
	public GameObject[] uiObjects;

	// Token: 0x04001504 RID: 5380
	private roomScript rS_;

	// Token: 0x04001505 RID: 5381
	private GameObject main_;

	// Token: 0x04001506 RID: 5382
	private mainScript mS_;

	// Token: 0x04001507 RID: 5383
	private textScript tS_;

	// Token: 0x04001508 RID: 5384
	private GUI_Main guiMain_;

	// Token: 0x04001509 RID: 5385
	private sfxScript sfx_;

	// Token: 0x0400150A RID: 5386
	private cameraMovementScript cmS_;
}
