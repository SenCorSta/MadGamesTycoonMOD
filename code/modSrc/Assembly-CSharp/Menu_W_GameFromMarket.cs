using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000205 RID: 517
public class Menu_W_GameFromMarket : MonoBehaviour
{
	// Token: 0x060013CA RID: 5066 RVA: 0x000CF6ED File Offset: 0x000CD8ED
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060013CB RID: 5067 RVA: 0x000CF6F8 File Offset: 0x000CD8F8
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

	// Token: 0x060013CC RID: 5068 RVA: 0x000CF7A2 File Offset: 0x000CD9A2
	public void Init(gameScript gS_)
	{
		this.game_ = gS_;
		this.uiObjects[0].GetComponent<Text>().text = gS_.GetNameWithTag();
	}

	// Token: 0x060013CD RID: 5069 RVA: 0x000CF7C3 File Offset: 0x000CD9C3
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060013CE RID: 5070 RVA: 0x000CF7E0 File Offset: 0x000CD9E0
	public void BUTTON_Yes()
	{
		this.sfx_.PlaySound(3, true);
		if (this.game_)
		{
			this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[82]);
			this.guiMain_.uiObjects[82].GetComponent<Menu_GameFromMarket>().Init(this.game_, true);
			this.game_.RemoveFromMarket();
			if (this.mS_.multiplayer)
			{
				if (this.mS_.mpCalls_.isServer)
				{
					this.mS_.mpCalls_.SERVER_Send_GameData(this.game_);
				}
				if (this.mS_.mpCalls_.isClient)
				{
					this.mS_.mpCalls_.CLIENT_Send_GameData(this.game_);
				}
			}
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x040017E6 RID: 6118
	public GameObject[] uiObjects;

	// Token: 0x040017E7 RID: 6119
	private GameObject main_;

	// Token: 0x040017E8 RID: 6120
	private mainScript mS_;

	// Token: 0x040017E9 RID: 6121
	private textScript tS_;

	// Token: 0x040017EA RID: 6122
	private GUI_Main guiMain_;

	// Token: 0x040017EB RID: 6123
	private sfxScript sfx_;

	// Token: 0x040017EC RID: 6124
	private gameScript game_;
}
