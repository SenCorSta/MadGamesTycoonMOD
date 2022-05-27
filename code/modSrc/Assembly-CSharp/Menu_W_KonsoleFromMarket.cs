using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000205 RID: 517
public class Menu_W_KonsoleFromMarket : MonoBehaviour
{
	// Token: 0x060013B5 RID: 5045 RVA: 0x0000D764 File Offset: 0x0000B964
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060013B6 RID: 5046 RVA: 0x000D985C File Offset: 0x000D7A5C
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

	// Token: 0x060013B7 RID: 5047 RVA: 0x0000D76C File Offset: 0x0000B96C
	public void Init(platformScript plat_)
	{
		this.pS_ = plat_;
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
	}

	// Token: 0x060013B8 RID: 5048 RVA: 0x0000D792 File Offset: 0x0000B992
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060013B9 RID: 5049 RVA: 0x000D9908 File Offset: 0x000D7B08
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

	// Token: 0x040017E4 RID: 6116
	public GameObject[] uiObjects;

	// Token: 0x040017E5 RID: 6117
	private GameObject main_;

	// Token: 0x040017E6 RID: 6118
	private mainScript mS_;

	// Token: 0x040017E7 RID: 6119
	private textScript tS_;

	// Token: 0x040017E8 RID: 6120
	private GUI_Main guiMain_;

	// Token: 0x040017E9 RID: 6121
	private sfxScript sfx_;

	// Token: 0x040017EA RID: 6122
	private platformScript pS_;
}
