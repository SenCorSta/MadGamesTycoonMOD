using System;
using UnityEngine;

// Token: 0x020001DB RID: 475
public class Menu_W_UpdateObjects : MonoBehaviour
{
	// Token: 0x060011DF RID: 4575 RVA: 0x000BC61B File Offset: 0x000BA81B
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060011E0 RID: 4576 RVA: 0x000BC624 File Offset: 0x000BA824
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

	// Token: 0x060011E1 RID: 4577 RVA: 0x000BC6CE File Offset: 0x000BA8CE
	public void Init(roomScript script_)
	{
		this.FindScripts();
		this.rS_ = script_;
	}

	// Token: 0x060011E2 RID: 4578 RVA: 0x000BC6DD File Offset: 0x000BA8DD
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060011E3 RID: 4579 RVA: 0x000BC703 File Offset: 0x000BA903
	public void BUTTON_Yes()
	{
		if (this.rS_)
		{
			this.rS_.UpdateInventar(true);
		}
		this.BUTTON_Abbrechen();
	}

	// Token: 0x0400165C RID: 5724
	public GameObject[] uiObjects;

	// Token: 0x0400165D RID: 5725
	private roomScript rS_;

	// Token: 0x0400165E RID: 5726
	private GameObject main_;

	// Token: 0x0400165F RID: 5727
	private mainScript mS_;

	// Token: 0x04001660 RID: 5728
	private textScript tS_;

	// Token: 0x04001661 RID: 5729
	private GUI_Main guiMain_;

	// Token: 0x04001662 RID: 5730
	private sfxScript sfx_;
}
