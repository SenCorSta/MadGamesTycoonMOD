using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200019F RID: 415
public class Menu_W_BuyDevKit : MonoBehaviour
{
	// Token: 0x06000F9F RID: 3999 RVA: 0x0000B170 File Offset: 0x00009370
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000FA0 RID: 4000 RVA: 0x000B3D60 File Offset: 0x000B1F60
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

	// Token: 0x06000FA1 RID: 4001 RVA: 0x000B3E0C File Offset: 0x000B200C
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

	// Token: 0x06000FA2 RID: 4002 RVA: 0x0000B178 File Offset: 0x00009378
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000FA3 RID: 4003 RVA: 0x000B3E5C File Offset: 0x000B205C
	public void BUTTON_Yes()
	{
		this.pS_.inBesitz = true;
		this.sfx_.PlaySound(3, true);
		this.BUTTON_Abbrechen();
		this.mS_.Pay((long)this.pS_.GetPrice(), 3);
		if (this.pS_.multiplaySlot != -1)
		{
			if (this.mS_.mpCalls_.isServer)
			{
				this.mS_.mpCalls_.SERVER_Send_Payment(this.mS_.mpCalls_.myID, this.pS_.multiplaySlot, 2, this.pS_.price);
			}
			if (this.mS_.mpCalls_.isClient)
			{
				this.mS_.mpCalls_.CLIENT_Send_Payment(this.pS_.multiplaySlot, 2, this.pS_.price);
			}
		}
		this.guiMain_.uiObjects[33].GetComponent<Menu_BuyDevKit>().TAB_DevKitsBuy(0);
	}

	// Token: 0x0400141F RID: 5151
	public GameObject[] uiObjects;

	// Token: 0x04001420 RID: 5152
	private platformScript pS_;

	// Token: 0x04001421 RID: 5153
	private GameObject main_;

	// Token: 0x04001422 RID: 5154
	private mainScript mS_;

	// Token: 0x04001423 RID: 5155
	private textScript tS_;

	// Token: 0x04001424 RID: 5156
	private GUI_Main guiMain_;

	// Token: 0x04001425 RID: 5157
	private sfxScript sfx_;
}
