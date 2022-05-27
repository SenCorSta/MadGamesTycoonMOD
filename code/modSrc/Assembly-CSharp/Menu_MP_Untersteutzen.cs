using System;
using UnityEngine;

// Token: 0x020001CA RID: 458
public class Menu_MP_Untersteutzen : MonoBehaviour
{
	// Token: 0x06001145 RID: 4421 RVA: 0x0000C179 File Offset: 0x0000A379
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001146 RID: 4422 RVA: 0x000C3738 File Offset: 0x000C1938
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

	// Token: 0x06001147 RID: 4423 RVA: 0x0000C181 File Offset: 0x0000A381
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06001148 RID: 4424 RVA: 0x0000C179 File Offset: 0x0000A379
	public void Init()
	{
		this.FindScripts();
	}

	// Token: 0x06001149 RID: 4425 RVA: 0x0000C189 File Offset: 0x0000A389
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600114A RID: 4426 RVA: 0x0000C1A4 File Offset: 0x0000A3A4
	public void BUTTON_GeldSchenken()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[261]);
	}

	// Token: 0x0600114B RID: 4427 RVA: 0x0000C1CF File Offset: 0x0000A3CF
	public void BUTTON_Forschungshilfe()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[265]);
	}

	// Token: 0x0600114C RID: 4428 RVA: 0x000C3800 File Offset: 0x000C1A00
	public void BUTTON_EngineSchenken()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[262]);
		this.guiMain_.uiObjects[262].GetComponent<Menu_MP_EngineSchenken>().Init();
	}

	// Token: 0x0600114D RID: 4429 RVA: 0x000C3854 File Offset: 0x000C1A54
	public void BUTTON_LizenzSchenken()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[263]);
		this.guiMain_.uiObjects[263].GetComponent<Menu_MP_LizenzSchenken>().Init();
	}

	// Token: 0x040015D2 RID: 5586
	public GameObject[] uiObjects;

	// Token: 0x040015D3 RID: 5587
	private roomScript rS_;

	// Token: 0x040015D4 RID: 5588
	private GameObject main_;

	// Token: 0x040015D5 RID: 5589
	private mainScript mS_;

	// Token: 0x040015D6 RID: 5590
	private textScript tS_;

	// Token: 0x040015D7 RID: 5591
	private GUI_Main guiMain_;

	// Token: 0x040015D8 RID: 5592
	private sfxScript sfx_;

	// Token: 0x040015D9 RID: 5593
	private unlockScript unlock_;
}
