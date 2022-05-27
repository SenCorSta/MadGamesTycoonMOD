using System;
using UnityEngine;

// Token: 0x020001A8 RID: 424
public class Menu_Marketing_Main : MonoBehaviour
{
	// Token: 0x06000FF3 RID: 4083 RVA: 0x0000B4AD File Offset: 0x000096AD
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000FF4 RID: 4084 RVA: 0x000B69D0 File Offset: 0x000B4BD0
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

	// Token: 0x06000FF5 RID: 4085 RVA: 0x0000B4B5 File Offset: 0x000096B5
	public void Init(roomScript script_)
	{
		if (!script_)
		{
			return;
		}
		this.rS_ = script_;
	}

	// Token: 0x06000FF6 RID: 4086 RVA: 0x0000B4C7 File Offset: 0x000096C7
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000FF7 RID: 4087 RVA: 0x000B6A7C File Offset: 0x000B4C7C
	public void BUTTON_Game()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[89]);
		this.guiMain_.uiObjects[89].GetComponent<Menu_Marketing_GameKampagne>().Init(this.rS_);
	}

	// Token: 0x06000FF8 RID: 4088 RVA: 0x000B6AD0 File Offset: 0x000B4CD0
	public void BUTTON_Konsole()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[321]);
		this.guiMain_.uiObjects[321].GetComponent<Menu_Marketing_KonsoleKampagne>().Init(this.rS_);
	}

	// Token: 0x04001480 RID: 5248
	public GameObject[] uiObjects;

	// Token: 0x04001481 RID: 5249
	private roomScript rS_;

	// Token: 0x04001482 RID: 5250
	private GameObject main_;

	// Token: 0x04001483 RID: 5251
	private mainScript mS_;

	// Token: 0x04001484 RID: 5252
	private textScript tS_;

	// Token: 0x04001485 RID: 5253
	private GUI_Main guiMain_;

	// Token: 0x04001486 RID: 5254
	private sfxScript sfx_;
}
