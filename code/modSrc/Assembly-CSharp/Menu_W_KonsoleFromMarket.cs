using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000206 RID: 518
public class Menu_W_KonsoleFromMarket : MonoBehaviour
{
	// Token: 0x060013D0 RID: 5072 RVA: 0x000CF8B6 File Offset: 0x000CDAB6
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060013D1 RID: 5073 RVA: 0x000CF8C0 File Offset: 0x000CDAC0
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

	// Token: 0x060013D2 RID: 5074 RVA: 0x000CF96A File Offset: 0x000CDB6A
	public void Init(platformScript plat_)
	{
		this.pS_ = plat_;
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
	}

	// Token: 0x060013D3 RID: 5075 RVA: 0x000CF990 File Offset: 0x000CDB90
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060013D4 RID: 5076 RVA: 0x000CF9AC File Offset: 0x000CDBAC
	public void BUTTON_Yes()
	{
		this.sfx_.PlaySound(3, true);
		if (this.pS_)
		{
			this.pS_.RemoveFromMarket();
			if (this.mS_.multiplayer)
			{
				if (this.mS_.mpCalls_.isServer)
				{
					this.mS_.mpCalls_.SERVER_Send_Platform(this.pS_);
				}
				if (this.mS_.mpCalls_.isClient)
				{
					this.mS_.mpCalls_.CLIENT_Send_Platform(this.pS_);
				}
			}
		}
		this.guiMain_.uiObjects[331].SetActive(false);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x040017ED RID: 6125
	public GameObject[] uiObjects;

	// Token: 0x040017EE RID: 6126
	private GameObject main_;

	// Token: 0x040017EF RID: 6127
	private mainScript mS_;

	// Token: 0x040017F0 RID: 6128
	private textScript tS_;

	// Token: 0x040017F1 RID: 6129
	private GUI_Main guiMain_;

	// Token: 0x040017F2 RID: 6130
	private sfxScript sfx_;

	// Token: 0x040017F3 RID: 6131
	private platformScript pS_;
}
