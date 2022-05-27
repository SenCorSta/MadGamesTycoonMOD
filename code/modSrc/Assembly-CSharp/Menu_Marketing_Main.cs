using System;
using UnityEngine;

// Token: 0x020001A9 RID: 425
public class Menu_Marketing_Main : MonoBehaviour
{
	// Token: 0x0600100B RID: 4107 RVA: 0x000AA3DA File Offset: 0x000A85DA
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600100C RID: 4108 RVA: 0x000AA3E4 File Offset: 0x000A85E4
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

	// Token: 0x0600100D RID: 4109 RVA: 0x000AA48E File Offset: 0x000A868E
	public void Init(roomScript script_)
	{
		if (!script_)
		{
			return;
		}
		this.rS_ = script_;
	}

	// Token: 0x0600100E RID: 4110 RVA: 0x000AA4A0 File Offset: 0x000A86A0
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600100F RID: 4111 RVA: 0x000AA4C8 File Offset: 0x000A86C8
	public void BUTTON_Game()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[89]);
		this.guiMain_.uiObjects[89].GetComponent<Menu_Marketing_GameKampagne>().Init(this.rS_);
	}

	// Token: 0x06001010 RID: 4112 RVA: 0x000AA51C File Offset: 0x000A871C
	public void BUTTON_Konsole()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[321]);
		this.guiMain_.uiObjects[321].GetComponent<Menu_Marketing_KonsoleKampagne>().Init(this.rS_);
	}

	// Token: 0x04001489 RID: 5257
	public GameObject[] uiObjects;

	// Token: 0x0400148A RID: 5258
	private roomScript rS_;

	// Token: 0x0400148B RID: 5259
	private GameObject main_;

	// Token: 0x0400148C RID: 5260
	private mainScript mS_;

	// Token: 0x0400148D RID: 5261
	private textScript tS_;

	// Token: 0x0400148E RID: 5262
	private GUI_Main guiMain_;

	// Token: 0x0400148F RID: 5263
	private sfxScript sfx_;
}
