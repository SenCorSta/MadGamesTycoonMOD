using System;
using UnityEngine;

// Token: 0x0200020D RID: 525
public class Menu_QA_SpielberichtMain : MonoBehaviour
{
	// Token: 0x0600141B RID: 5147 RVA: 0x0000DB2B File Offset: 0x0000BD2B
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600141C RID: 5148 RVA: 0x000DC784 File Offset: 0x000DA984
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

	// Token: 0x0600141D RID: 5149 RVA: 0x0000DB33 File Offset: 0x0000BD33
	public void Init(roomScript script_)
	{
		if (!script_)
		{
			return;
		}
		this.rS_ = script_;
	}

	// Token: 0x0600141E RID: 5150 RVA: 0x0000DB45 File Offset: 0x0000BD45
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600141F RID: 5151 RVA: 0x000DC830 File Offset: 0x000DAA30
	public void BUTTON_NewSpielbericht()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[181]);
		this.guiMain_.uiObjects[181].GetComponent<Menu_QA_NewSpielberichtSelectGame>().Init(this.rS_);
	}

	// Token: 0x06001420 RID: 5152 RVA: 0x000DC888 File Offset: 0x000DAA88
	public void BUTTON_ShowSpielbericht()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[182]);
		this.guiMain_.uiObjects[182].GetComponent<Menu_QA_ShowSpielberichtSelectGame>().Init();
	}

	// Token: 0x04001842 RID: 6210
	public GameObject[] uiObjects;

	// Token: 0x04001843 RID: 6211
	private roomScript rS_;

	// Token: 0x04001844 RID: 6212
	private GameObject main_;

	// Token: 0x04001845 RID: 6213
	private mainScript mS_;

	// Token: 0x04001846 RID: 6214
	private textScript tS_;

	// Token: 0x04001847 RID: 6215
	private GUI_Main guiMain_;

	// Token: 0x04001848 RID: 6216
	private sfxScript sfx_;
}
