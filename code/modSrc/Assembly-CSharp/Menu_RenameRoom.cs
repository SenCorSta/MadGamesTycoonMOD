using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001B8 RID: 440
public class Menu_RenameRoom : MonoBehaviour
{
	// Token: 0x060010A3 RID: 4259 RVA: 0x000B0365 File Offset: 0x000AE565
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060010A4 RID: 4260 RVA: 0x000B0370 File Offset: 0x000AE570
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

	// Token: 0x060010A5 RID: 4261 RVA: 0x000B043C File Offset: 0x000AE63C
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

	// Token: 0x060010A6 RID: 4262 RVA: 0x000B047C File Offset: 0x000AE67C
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
		this.cmS_.disableMovement = false;
	}

	// Token: 0x060010A7 RID: 4263 RVA: 0x000B04AE File Offset: 0x000AE6AE
	public void BUTTON_Yes()
	{
		if (this.rS_)
		{
			this.rS_.myName = this.uiObjects[0].GetComponent<InputField>().text;
		}
		this.BUTTON_Abbrechen();
	}

	// Token: 0x0400150E RID: 5390
	public GameObject[] uiObjects;

	// Token: 0x0400150F RID: 5391
	private roomScript rS_;

	// Token: 0x04001510 RID: 5392
	private GameObject main_;

	// Token: 0x04001511 RID: 5393
	private mainScript mS_;

	// Token: 0x04001512 RID: 5394
	private textScript tS_;

	// Token: 0x04001513 RID: 5395
	private GUI_Main guiMain_;

	// Token: 0x04001514 RID: 5396
	private sfxScript sfx_;

	// Token: 0x04001515 RID: 5397
	private cameraMovementScript cmS_;
}
