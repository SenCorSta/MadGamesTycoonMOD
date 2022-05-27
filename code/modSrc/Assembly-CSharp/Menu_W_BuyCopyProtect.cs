using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200019F RID: 415
public class Menu_W_BuyCopyProtect : MonoBehaviour
{
	// Token: 0x06000FB1 RID: 4017 RVA: 0x000A7203 File Offset: 0x000A5403
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000FB2 RID: 4018 RVA: 0x000A720C File Offset: 0x000A540C
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

	// Token: 0x06000FB3 RID: 4019 RVA: 0x000A72B6 File Offset: 0x000A54B6
	public void Init(copyProtectScript script_)
	{
		if (!script_)
		{
			return;
		}
		this.cpS_ = script_;
		this.uiObjects[0].GetComponent<Text>().text = this.cpS_.GetTooltip();
	}

	// Token: 0x06000FB4 RID: 4020 RVA: 0x000A72E5 File Offset: 0x000A54E5
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000FB5 RID: 4021 RVA: 0x000A7300 File Offset: 0x000A5500
	public void BUTTON_Yes()
	{
		this.cpS_.inBesitz = true;
		this.mS_.Pay((long)this.cpS_.GetPrice(), 6);
		this.guiMain_.uiObjects[49].GetComponent<Menu_BuyCopyProtect>().TAB_CopyProtectBuy(0);
		this.BUTTON_Abbrechen();
	}

	// Token: 0x04001421 RID: 5153
	public GameObject[] uiObjects;

	// Token: 0x04001422 RID: 5154
	private copyProtectScript cpS_;

	// Token: 0x04001423 RID: 5155
	private GameObject main_;

	// Token: 0x04001424 RID: 5156
	private mainScript mS_;

	// Token: 0x04001425 RID: 5157
	private textScript tS_;

	// Token: 0x04001426 RID: 5158
	private GUI_Main guiMain_;

	// Token: 0x04001427 RID: 5159
	private sfxScript sfx_;
}
