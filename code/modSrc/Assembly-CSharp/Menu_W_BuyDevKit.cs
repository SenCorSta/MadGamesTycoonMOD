using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001A0 RID: 416
public class Menu_W_BuyDevKit : MonoBehaviour
{
	// Token: 0x06000FB7 RID: 4023 RVA: 0x000A735C File Offset: 0x000A555C
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000FB8 RID: 4024 RVA: 0x000A7364 File Offset: 0x000A5564
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
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
	}

	// Token: 0x06000FB9 RID: 4025 RVA: 0x000A7410 File Offset: 0x000A5610
	public void Init(platformScript script_)
	{
		if (!script_)
		{
			return;
		}
		this.pS_ = script_;
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
		this.pS_.SetPic(this.uiObjects[1]);
	}

	// Token: 0x06000FBA RID: 4026 RVA: 0x000A745D File Offset: 0x000A565D
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000FBB RID: 4027 RVA: 0x000A7478 File Offset: 0x000A5678
	public void BUTTON_Yes()
	{
		this.pS_.inBesitz = true;
		this.sfx_.PlaySound(3, true);
		this.BUTTON_Abbrechen();
		this.mS_.Pay((long)this.pS_.GetPrice(), 3);
		if (this.mS_.multiplayer && !this.pS_.OwnerIsNPC())
		{
			if (this.mS_.mpCalls_.isServer)
			{
				this.mS_.mpCalls_.SERVER_Send_Payment(this.mS_.myID, this.pS_.ownerID, 2, this.pS_.price);
			}
			if (this.mS_.mpCalls_.isClient)
			{
				this.mS_.mpCalls_.CLIENT_Send_Payment(this.pS_.ownerID, 2, this.pS_.price);
			}
		}
		this.guiMain_.uiObjects[33].GetComponent<Menu_BuyDevKit>().TAB_DevKitsBuy(0);
	}

	// Token: 0x04001428 RID: 5160
	public GameObject[] uiObjects;

	// Token: 0x04001429 RID: 5161
	private platformScript pS_;

	// Token: 0x0400142A RID: 5162
	private GameObject main_;

	// Token: 0x0400142B RID: 5163
	private mainScript mS_;

	// Token: 0x0400142C RID: 5164
	private textScript tS_;

	// Token: 0x0400142D RID: 5165
	private GUI_Main guiMain_;

	// Token: 0x0400142E RID: 5166
	private sfxScript sfx_;
}
