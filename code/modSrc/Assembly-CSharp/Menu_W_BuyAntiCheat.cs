using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200019E RID: 414
public class Menu_W_BuyAntiCheat : MonoBehaviour
{
	// Token: 0x06000FAB RID: 4011 RVA: 0x000A70C0 File Offset: 0x000A52C0
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000FAC RID: 4012 RVA: 0x000A70C8 File Offset: 0x000A52C8
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

	// Token: 0x06000FAD RID: 4013 RVA: 0x000A7172 File Offset: 0x000A5372
	public void Init(antiCheatScript script_)
	{
		if (!script_)
		{
			return;
		}
		this.acS_ = script_;
		this.uiObjects[0].GetComponent<Text>().text = this.acS_.GetTooltip();
	}

	// Token: 0x06000FAE RID: 4014 RVA: 0x000A71A1 File Offset: 0x000A53A1
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000FAF RID: 4015 RVA: 0x000A71BC File Offset: 0x000A53BC
	public void BUTTON_Yes()
	{
		this.acS_.inBesitz = true;
		this.mS_.Pay((long)this.acS_.GetPrice(), 6);
		this.guiMain_.uiObjects[234].GetComponent<Menu_BuyAntiCheat>().TAB_AntiCheatBuy(0);
		this.BUTTON_Abbrechen();
	}

	// Token: 0x0400141A RID: 5146
	public GameObject[] uiObjects;

	// Token: 0x0400141B RID: 5147
	private antiCheatScript acS_;

	// Token: 0x0400141C RID: 5148
	private GameObject main_;

	// Token: 0x0400141D RID: 5149
	private mainScript mS_;

	// Token: 0x0400141E RID: 5150
	private textScript tS_;

	// Token: 0x0400141F RID: 5151
	private GUI_Main guiMain_;

	// Token: 0x04001420 RID: 5152
	private sfxScript sfx_;
}
