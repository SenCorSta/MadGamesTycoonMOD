using System;
using UnityEngine;

// Token: 0x020001CB RID: 459
public class Menu_MP_Untersteutzen : MonoBehaviour
{
	// Token: 0x0600115F RID: 4447 RVA: 0x000B8012 File Offset: 0x000B6212
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001160 RID: 4448 RVA: 0x000B801C File Offset: 0x000B621C
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

	// Token: 0x06001161 RID: 4449 RVA: 0x000B80E4 File Offset: 0x000B62E4
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06001162 RID: 4450 RVA: 0x000B8012 File Offset: 0x000B6212
	public void Init()
	{
		this.FindScripts();
	}

	// Token: 0x06001163 RID: 4451 RVA: 0x000B80EC File Offset: 0x000B62EC
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001164 RID: 4452 RVA: 0x000B8107 File Offset: 0x000B6307
	public void BUTTON_GeldSchenken()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[261]);
	}

	// Token: 0x06001165 RID: 4453 RVA: 0x000B8132 File Offset: 0x000B6332
	public void BUTTON_Forschungshilfe()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[265]);
	}

	// Token: 0x06001166 RID: 4454 RVA: 0x000B8160 File Offset: 0x000B6360
	public void BUTTON_EngineSchenken()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[262]);
		this.guiMain_.uiObjects[262].GetComponent<Menu_MP_EngineSchenken>().Init();
	}

	// Token: 0x06001167 RID: 4455 RVA: 0x000B81B4 File Offset: 0x000B63B4
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
