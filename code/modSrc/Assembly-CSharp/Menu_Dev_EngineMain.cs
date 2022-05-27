using System;
using UnityEngine;

// Token: 0x0200010E RID: 270
public class Menu_Dev_EngineMain : MonoBehaviour
{
	// Token: 0x060008C9 RID: 2249 RVA: 0x0005FC2E File Offset: 0x0005DE2E
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060008CA RID: 2250 RVA: 0x0005FC38 File Offset: 0x0005DE38
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

	// Token: 0x060008CB RID: 2251 RVA: 0x0005FCE2 File Offset: 0x0005DEE2
	public void Init(roomScript script_)
	{
		if (!script_)
		{
			return;
		}
		this.rS_ = script_;
	}

	// Token: 0x060008CC RID: 2252 RVA: 0x0005FCF4 File Offset: 0x0005DEF4
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060008CD RID: 2253 RVA: 0x0005FD1C File Offset: 0x0005DF1C
	public void BUTTON_NewEngine()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[37]);
		this.guiMain_.uiObjects[37].GetComponent<Menu_Dev_Engine>().Init(this.rS_);
	}

	// Token: 0x060008CE RID: 2254 RVA: 0x0005FD7C File Offset: 0x0005DF7C
	public void BUTTON_UpdateEngine()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[41]);
		this.guiMain_.uiObjects[41].GetComponent<Menu_Dev_Engine_SelectOld>().Init(this.rS_);
	}

	// Token: 0x04000D45 RID: 3397
	public GameObject[] uiObjects;

	// Token: 0x04000D46 RID: 3398
	private roomScript rS_;

	// Token: 0x04000D47 RID: 3399
	private GameObject main_;

	// Token: 0x04000D48 RID: 3400
	private mainScript mS_;

	// Token: 0x04000D49 RID: 3401
	private textScript tS_;

	// Token: 0x04000D4A RID: 3402
	private GUI_Main guiMain_;

	// Token: 0x04000D4B RID: 3403
	private sfxScript sfx_;
}
