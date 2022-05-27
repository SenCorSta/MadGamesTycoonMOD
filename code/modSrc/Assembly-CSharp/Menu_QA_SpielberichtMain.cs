using System;
using UnityEngine;

// Token: 0x0200020E RID: 526
public class Menu_QA_SpielberichtMain : MonoBehaviour
{
	// Token: 0x06001438 RID: 5176 RVA: 0x000D2C78 File Offset: 0x000D0E78
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001439 RID: 5177 RVA: 0x000D2C80 File Offset: 0x000D0E80
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

	// Token: 0x0600143A RID: 5178 RVA: 0x000D2D2A File Offset: 0x000D0F2A
	public void Init(roomScript script_)
	{
		if (!script_)
		{
			return;
		}
		this.rS_ = script_;
	}

	// Token: 0x0600143B RID: 5179 RVA: 0x000D2D3C File Offset: 0x000D0F3C
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600143C RID: 5180 RVA: 0x000D2D64 File Offset: 0x000D0F64
	public void BUTTON_NewSpielbericht()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[181]);
		this.guiMain_.uiObjects[181].GetComponent<Menu_QA_NewSpielberichtSelectGame>().Init(this.rS_);
	}

	// Token: 0x0600143D RID: 5181 RVA: 0x000D2DBC File Offset: 0x000D0FBC
	public void BUTTON_ShowSpielbericht()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[182]);
		this.guiMain_.uiObjects[182].GetComponent<Menu_QA_ShowSpielberichtSelectGame>().Init();
	}

	// Token: 0x0400184B RID: 6219
	public GameObject[] uiObjects;

	// Token: 0x0400184C RID: 6220
	private roomScript rS_;

	// Token: 0x0400184D RID: 6221
	private GameObject main_;

	// Token: 0x0400184E RID: 6222
	private mainScript mS_;

	// Token: 0x0400184F RID: 6223
	private textScript tS_;

	// Token: 0x04001850 RID: 6224
	private GUI_Main guiMain_;

	// Token: 0x04001851 RID: 6225
	private sfxScript sfx_;
}
