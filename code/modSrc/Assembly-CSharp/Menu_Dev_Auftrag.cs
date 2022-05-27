using System;
using UnityEngine;

// Token: 0x0200012A RID: 298
public class Menu_Dev_Auftrag : MonoBehaviour
{
	// Token: 0x06000A8C RID: 2700 RVA: 0x000078CA File Offset: 0x00005ACA
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000A8D RID: 2701 RVA: 0x00083A2C File Offset: 0x00081C2C
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

	// Token: 0x06000A8E RID: 2702 RVA: 0x000078D2 File Offset: 0x00005AD2
	public void Init(roomScript script_)
	{
		this.FindScripts();
		if (!script_)
		{
			this.BUTTON_Abbrechen();
			return;
		}
		this.rS_ = script_;
	}

	// Token: 0x06000A8F RID: 2703 RVA: 0x000078F0 File Offset: 0x00005AF0
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000A90 RID: 2704 RVA: 0x00083AF4 File Offset: 0x00081CF4
	public void BUTTON_Komponente()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[96]);
		this.guiMain_.uiObjects[96].GetComponent<Menu_Dev_AuftragSelect>().Init(this.rS_);
	}

	// Token: 0x06000A91 RID: 2705 RVA: 0x00083B54 File Offset: 0x00081D54
	public void BUTTON_Auftragsspiel()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[99]);
		this.guiMain_.uiObjects[99].GetComponent<Menu_Dev_Auftragsspiel>().Init(this.rS_);
	}

	// Token: 0x04000EC3 RID: 3779
	public GameObject[] uiObjects;

	// Token: 0x04000EC4 RID: 3780
	private roomScript rS_;

	// Token: 0x04000EC5 RID: 3781
	private GameObject main_;

	// Token: 0x04000EC6 RID: 3782
	private mainScript mS_;

	// Token: 0x04000EC7 RID: 3783
	private textScript tS_;

	// Token: 0x04000EC8 RID: 3784
	private GUI_Main guiMain_;

	// Token: 0x04000EC9 RID: 3785
	private sfxScript sfx_;

	// Token: 0x04000ECA RID: 3786
	private unlockScript unlock_;
}
