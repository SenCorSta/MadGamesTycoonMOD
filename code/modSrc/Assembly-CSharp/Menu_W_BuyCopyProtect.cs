using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200019E RID: 414
public class Menu_W_BuyCopyProtect : MonoBehaviour
{
	// Token: 0x06000F99 RID: 3993 RVA: 0x0000B11E File Offset: 0x0000931E
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000F9A RID: 3994 RVA: 0x000B3C64 File Offset: 0x000B1E64
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

	// Token: 0x06000F9B RID: 3995 RVA: 0x0000B126 File Offset: 0x00009326
	public void Init(copyProtectScript script_)
	{
		if (!script_)
		{
			return;
		}
		this.cpS_ = script_;
		this.uiObjects[0].GetComponent<Text>().text = this.cpS_.GetTooltip();
	}

	// Token: 0x06000F9C RID: 3996 RVA: 0x0000B155 File Offset: 0x00009355
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000F9D RID: 3997 RVA: 0x000B3D10 File Offset: 0x000B1F10
	public void BUTTON_Yes()
	{
		this.cpS_.inBesitz = true;
		this.mS_.Pay((long)this.cpS_.GetPrice(), 6);
		this.guiMain_.uiObjects[49].GetComponent<Menu_BuyCopyProtect>().TAB_CopyProtectBuy(0);
		this.BUTTON_Abbrechen();
	}

	// Token: 0x04001418 RID: 5144
	public GameObject[] uiObjects;

	// Token: 0x04001419 RID: 5145
	private copyProtectScript cpS_;

	// Token: 0x0400141A RID: 5146
	private GameObject main_;

	// Token: 0x0400141B RID: 5147
	private mainScript mS_;

	// Token: 0x0400141C RID: 5148
	private textScript tS_;

	// Token: 0x0400141D RID: 5149
	private GUI_Main guiMain_;

	// Token: 0x0400141E RID: 5150
	private sfxScript sfx_;
}
