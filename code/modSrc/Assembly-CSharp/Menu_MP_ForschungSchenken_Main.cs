﻿using System;
using UnityEngine;

// Token: 0x020001C6 RID: 454
public class Menu_MP_ForschungSchenken_Main : MonoBehaviour
{
	// Token: 0x06001124 RID: 4388 RVA: 0x000B6CC4 File Offset: 0x000B4EC4
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001125 RID: 4389 RVA: 0x000B6CCC File Offset: 0x000B4ECC
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

	// Token: 0x06001126 RID: 4390 RVA: 0x000B6D94 File Offset: 0x000B4F94
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06001127 RID: 4391 RVA: 0x000B6CC4 File Offset: 0x000B4EC4
	public void Init()
	{
		this.FindScripts();
	}

	// Token: 0x06001128 RID: 4392 RVA: 0x000B6D9C File Offset: 0x000B4F9C
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001129 RID: 4393 RVA: 0x000B6DB8 File Offset: 0x000B4FB8
	public void BUTTON_Genres()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[266]);
		this.guiMain_.uiObjects[266].GetComponent<Menu_MP_ForschungSchenken>().Init(0);
	}

	// Token: 0x0600112A RID: 4394 RVA: 0x000B6E0C File Offset: 0x000B500C
	public void BUTTON_Themen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[266]);
		this.guiMain_.uiObjects[266].GetComponent<Menu_MP_ForschungSchenken>().Init(1);
	}

	// Token: 0x0600112B RID: 4395 RVA: 0x000B6E60 File Offset: 0x000B5060
	public void BUTTON_EngineFeatures()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[266]);
		this.guiMain_.uiObjects[266].GetComponent<Menu_MP_ForschungSchenken>().Init(2);
	}

	// Token: 0x0600112C RID: 4396 RVA: 0x000B6EB4 File Offset: 0x000B50B4
	public void BUTTON_GameplayFeatures()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[266]);
		this.guiMain_.uiObjects[266].GetComponent<Menu_MP_ForschungSchenken>().Init(3);
	}

	// Token: 0x0600112D RID: 4397 RVA: 0x000B6F08 File Offset: 0x000B5108
	public void BUTTON_Hardware()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[266]);
		this.guiMain_.uiObjects[266].GetComponent<Menu_MP_ForschungSchenken>().Init(4);
	}

	// Token: 0x0600112E RID: 4398 RVA: 0x000B6F5C File Offset: 0x000B515C
	public void BUTTON_Sonstiges()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[266]);
		this.guiMain_.uiObjects[266].GetComponent<Menu_MP_ForschungSchenken>().Init(5);
	}

	// Token: 0x0600112F RID: 4399 RVA: 0x000B6FB0 File Offset: 0x000B51B0
	public void BUTTON_HardwareFeatures()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[266]);
		this.guiMain_.uiObjects[266].GetComponent<Menu_MP_ForschungSchenken>().Init(6);
	}

	// Token: 0x040015B2 RID: 5554
	public GameObject[] uiObjects;

	// Token: 0x040015B3 RID: 5555
	private roomScript rS_;

	// Token: 0x040015B4 RID: 5556
	private GameObject main_;

	// Token: 0x040015B5 RID: 5557
	private mainScript mS_;

	// Token: 0x040015B6 RID: 5558
	private textScript tS_;

	// Token: 0x040015B7 RID: 5559
	private GUI_Main guiMain_;

	// Token: 0x040015B8 RID: 5560
	private sfxScript sfx_;

	// Token: 0x040015B9 RID: 5561
	private unlockScript unlock_;
}
