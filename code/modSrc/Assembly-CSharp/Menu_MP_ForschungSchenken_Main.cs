using System;
using UnityEngine;

// Token: 0x020001C5 RID: 453
public class Menu_MP_ForschungSchenken_Main : MonoBehaviour
{
	// Token: 0x0600110A RID: 4362 RVA: 0x0000BFD8 File Offset: 0x0000A1D8
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600110B RID: 4363 RVA: 0x000C257C File Offset: 0x000C077C
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

	// Token: 0x0600110C RID: 4364 RVA: 0x0000BFE0 File Offset: 0x0000A1E0
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x0600110D RID: 4365 RVA: 0x0000BFD8 File Offset: 0x0000A1D8
	public void Init()
	{
		this.FindScripts();
	}

	// Token: 0x0600110E RID: 4366 RVA: 0x0000BFE8 File Offset: 0x0000A1E8
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600110F RID: 4367 RVA: 0x000C2644 File Offset: 0x000C0844
	public void BUTTON_Genres()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[266]);
		this.guiMain_.uiObjects[266].GetComponent<Menu_MP_ForschungSchenken>().Init(0);
	}

	// Token: 0x06001110 RID: 4368 RVA: 0x000C2698 File Offset: 0x000C0898
	public void BUTTON_Themen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[266]);
		this.guiMain_.uiObjects[266].GetComponent<Menu_MP_ForschungSchenken>().Init(1);
	}

	// Token: 0x06001111 RID: 4369 RVA: 0x000C26EC File Offset: 0x000C08EC
	public void BUTTON_EngineFeatures()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[266]);
		this.guiMain_.uiObjects[266].GetComponent<Menu_MP_ForschungSchenken>().Init(2);
	}

	// Token: 0x06001112 RID: 4370 RVA: 0x000C2740 File Offset: 0x000C0940
	public void BUTTON_GameplayFeatures()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[266]);
		this.guiMain_.uiObjects[266].GetComponent<Menu_MP_ForschungSchenken>().Init(3);
	}

	// Token: 0x06001113 RID: 4371 RVA: 0x000C2794 File Offset: 0x000C0994
	public void BUTTON_Hardware()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[266]);
		this.guiMain_.uiObjects[266].GetComponent<Menu_MP_ForschungSchenken>().Init(4);
	}

	// Token: 0x06001114 RID: 4372 RVA: 0x000C27E8 File Offset: 0x000C09E8
	public void BUTTON_Sonstiges()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[266]);
		this.guiMain_.uiObjects[266].GetComponent<Menu_MP_ForschungSchenken>().Init(5);
	}

	// Token: 0x06001115 RID: 4373 RVA: 0x000C283C File Offset: 0x000C0A3C
	public void BUTTON_HardwareFeatures()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[266]);
		this.guiMain_.uiObjects[266].GetComponent<Menu_MP_ForschungSchenken>().Init(6);
	}

	// Token: 0x040015A9 RID: 5545
	public GameObject[] uiObjects;

	// Token: 0x040015AA RID: 5546
	private roomScript rS_;

	// Token: 0x040015AB RID: 5547
	private GameObject main_;

	// Token: 0x040015AC RID: 5548
	private mainScript mS_;

	// Token: 0x040015AD RID: 5549
	private textScript tS_;

	// Token: 0x040015AE RID: 5550
	private GUI_Main guiMain_;

	// Token: 0x040015AF RID: 5551
	private sfxScript sfx_;

	// Token: 0x040015B0 RID: 5552
	private unlockScript unlock_;
}
