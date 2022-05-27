using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000204 RID: 516
public class Menu_W_GameFromMarket : MonoBehaviour
{
	// Token: 0x060013AF RID: 5039 RVA: 0x0000D720 File Offset: 0x0000B920
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060013B0 RID: 5040 RVA: 0x000D96D8 File Offset: 0x000D78D8
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

	// Token: 0x060013B1 RID: 5041 RVA: 0x0000D728 File Offset: 0x0000B928
	public void Init(gameScript gS_)
	{
		this.game_ = gS_;
		this.uiObjects[0].GetComponent<Text>().text = gS_.GetNameWithTag();
	}

	// Token: 0x060013B2 RID: 5042 RVA: 0x0000D749 File Offset: 0x0000B949
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060013B3 RID: 5043 RVA: 0x000D9784 File Offset: 0x000D7984
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

	// Token: 0x040017DD RID: 6109
	public GameObject[] uiObjects;

	// Token: 0x040017DE RID: 6110
	private GameObject main_;

	// Token: 0x040017DF RID: 6111
	private mainScript mS_;

	// Token: 0x040017E0 RID: 6112
	private textScript tS_;

	// Token: 0x040017E1 RID: 6113
	private GUI_Main guiMain_;

	// Token: 0x040017E2 RID: 6114
	private sfxScript sfx_;

	// Token: 0x040017E3 RID: 6115
	private gameScript game_;
}
