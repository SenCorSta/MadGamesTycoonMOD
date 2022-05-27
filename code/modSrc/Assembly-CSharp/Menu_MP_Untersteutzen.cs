using System;
using UnityEngine;

// Token: 0x020001CB RID: 459
public class Menu_MP_Untersteutzen : MonoBehaviour
{
	// Token: 0x0600115F RID: 4447 RVA: 0x000B801E File Offset: 0x000B621E
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001160 RID: 4448 RVA: 0x000B8028 File Offset: 0x000B6228
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

	// Token: 0x06001161 RID: 4449 RVA: 0x000B80F0 File Offset: 0x000B62F0
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06001162 RID: 4450 RVA: 0x000B801E File Offset: 0x000B621E
	public void Init()
	{
		this.FindScripts();
	}

	// Token: 0x06001163 RID: 4451 RVA: 0x000B80F8 File Offset: 0x000B62F8
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001164 RID: 4452 RVA: 0x000B8113 File Offset: 0x000B6313
	public void BUTTON_GeldSchenken()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[261]);
	}

	// Token: 0x06001165 RID: 4453 RVA: 0x000B813E File Offset: 0x000B633E
	public void BUTTON_Forschungshilfe()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[265]);
	}

	// Token: 0x06001166 RID: 4454 RVA: 0x000B816C File Offset: 0x000B636C
	public void BUTTON_EngineSchenken()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[262]);
		this.guiMain_.uiObjects[262].GetComponent<Menu_MP_EngineSchenken>().Init();
	}

	// Token: 0x06001167 RID: 4455 RVA: 0x000B81C0 File Offset: 0x000B63C0
	public void BUTTON_LizenzSchenken()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[263]);
		this.guiMain_.uiObjects[263].GetComponent<Menu_MP_LizenzSchenken>().Init();
	}

	// Token: 0x040015DB RID: 5595
	public GameObject[] uiObjects;

	// Token: 0x040015DC RID: 5596
	private roomScript rS_;

	// Token: 0x040015DD RID: 5597
	private GameObject main_;

	// Token: 0x040015DE RID: 5598
	private mainScript mS_;

	// Token: 0x040015DF RID: 5599
	private textScript tS_;

	// Token: 0x040015E0 RID: 5600
	private GUI_Main guiMain_;

	// Token: 0x040015E1 RID: 5601
	private sfxScript sfx_;

	// Token: 0x040015E2 RID: 5602
	private unlockScript unlock_;
}
