using System;
using UnityEngine;

// Token: 0x0200010D RID: 269
public class Menu_Dev_EngineMain : MonoBehaviour
{
	// Token: 0x060008BA RID: 2234 RVA: 0x000067A2 File Offset: 0x000049A2
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060008BB RID: 2235 RVA: 0x000713CC File Offset: 0x0006F5CC
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

	// Token: 0x060008BC RID: 2236 RVA: 0x000067AA File Offset: 0x000049AA
	public void Init(roomScript script_)
	{
		if (!script_)
		{
			return;
		}
		this.rS_ = script_;
	}

	// Token: 0x060008BD RID: 2237 RVA: 0x000067BC File Offset: 0x000049BC
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060008BE RID: 2238 RVA: 0x00071478 File Offset: 0x0006F678
	public void BUTTON_NewEngine()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[37]);
		this.guiMain_.uiObjects[37].GetComponent<Menu_Dev_Engine>().Init(this.rS_);
	}

	// Token: 0x060008BF RID: 2239 RVA: 0x000714D8 File Offset: 0x0006F6D8
	public void BUTTON_UpdateEngine()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[41]);
		this.guiMain_.uiObjects[41].GetComponent<Menu_Dev_Engine_SelectOld>().Init(this.rS_);
	}

	// Token: 0x04000D3D RID: 3389
	public GameObject[] uiObjects;

	// Token: 0x04000D3E RID: 3390
	private roomScript rS_;

	// Token: 0x04000D3F RID: 3391
	private GameObject main_;

	// Token: 0x04000D40 RID: 3392
	private mainScript mS_;

	// Token: 0x04000D41 RID: 3393
	private textScript tS_;

	// Token: 0x04000D42 RID: 3394
	private GUI_Main guiMain_;

	// Token: 0x04000D43 RID: 3395
	private sfxScript sfx_;
}
