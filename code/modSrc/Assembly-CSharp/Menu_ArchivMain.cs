using System;
using UnityEngine;

// Token: 0x02000185 RID: 389
public class Menu_ArchivMain : MonoBehaviour
{
	// Token: 0x06000E9A RID: 3738 RVA: 0x0000A41B File Offset: 0x0000861B
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000E9B RID: 3739 RVA: 0x000AAFF4 File Offset: 0x000A91F4
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

	// Token: 0x06000E9C RID: 3740 RVA: 0x0000A423 File Offset: 0x00008623
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06000E9D RID: 3741 RVA: 0x0000A41B File Offset: 0x0000861B
	public void Init()
	{
		this.FindScripts();
	}

	// Token: 0x06000E9E RID: 3742 RVA: 0x0000A42B File Offset: 0x0000862B
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000E9F RID: 3743 RVA: 0x0000A451 File Offset: 0x00008651
	public void BUTTON_Spielkonzepte()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[290]);
	}

	// Token: 0x06000EA0 RID: 3744 RVA: 0x0000A47C File Offset: 0x0000867C
	public void BUTTON_Spielberichte()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[291]);
	}

	// Token: 0x06000EA1 RID: 3745 RVA: 0x0000A4A7 File Offset: 0x000086A7
	public void BUTTON_Engines()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[293]);
	}

	// Token: 0x06000EA2 RID: 3746 RVA: 0x0000A4D2 File Offset: 0x000086D2
	public void BUTTON_Fanbriefe()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[292]);
	}

	// Token: 0x04001310 RID: 4880
	public GameObject[] uiObjects;

	// Token: 0x04001311 RID: 4881
	private GameObject main_;

	// Token: 0x04001312 RID: 4882
	private mainScript mS_;

	// Token: 0x04001313 RID: 4883
	private textScript tS_;

	// Token: 0x04001314 RID: 4884
	private GUI_Main guiMain_;

	// Token: 0x04001315 RID: 4885
	private sfxScript sfx_;

	// Token: 0x04001316 RID: 4886
	private unlockScript unlock_;
}
