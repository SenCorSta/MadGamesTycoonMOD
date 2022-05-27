using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200019D RID: 413
public class Menu_W_BuyAntiCheat : MonoBehaviour
{
	// Token: 0x06000F93 RID: 3987 RVA: 0x0000B0CC File Offset: 0x000092CC
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000F94 RID: 3988 RVA: 0x000B3B64 File Offset: 0x000B1D64
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

	// Token: 0x06000F95 RID: 3989 RVA: 0x0000B0D4 File Offset: 0x000092D4
	public void Init(antiCheatScript script_)
	{
		if (!script_)
		{
			return;
		}
		this.acS_ = script_;
		this.uiObjects[0].GetComponent<Text>().text = this.acS_.GetTooltip();
	}

	// Token: 0x06000F96 RID: 3990 RVA: 0x0000B103 File Offset: 0x00009303
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000F97 RID: 3991 RVA: 0x000B3C10 File Offset: 0x000B1E10
	public void BUTTON_Yes()
	{
		this.acS_.inBesitz = true;
		this.mS_.Pay((long)this.acS_.GetPrice(), 6);
		this.guiMain_.uiObjects[234].GetComponent<Menu_BuyAntiCheat>().TAB_AntiCheatBuy(0);
		this.BUTTON_Abbrechen();
	}

	// Token: 0x04001411 RID: 5137
	public GameObject[] uiObjects;

	// Token: 0x04001412 RID: 5138
	private antiCheatScript acS_;

	// Token: 0x04001413 RID: 5139
	private GameObject main_;

	// Token: 0x04001414 RID: 5140
	private mainScript mS_;

	// Token: 0x04001415 RID: 5141
	private textScript tS_;

	// Token: 0x04001416 RID: 5142
	private GUI_Main guiMain_;

	// Token: 0x04001417 RID: 5143
	private sfxScript sfx_;
}
