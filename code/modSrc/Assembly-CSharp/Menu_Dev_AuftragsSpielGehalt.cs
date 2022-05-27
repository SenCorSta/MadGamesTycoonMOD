using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000109 RID: 265
public class Menu_Dev_AuftragsSpielGehalt : MonoBehaviour
{
	// Token: 0x06000883 RID: 2179 RVA: 0x000064B3 File Offset: 0x000046B3
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000884 RID: 2180 RVA: 0x0006F754 File Offset: 0x0006D954
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

	// Token: 0x06000885 RID: 2181 RVA: 0x0006F800 File Offset: 0x0006DA00
	public void Init(gameScript game_)
	{
		this.FindScripts();
		if (game_)
		{
			this.gS_ = game_;
			string text = this.tS_.GetText(630);
			text = text.Replace("<NUM1>", this.mS_.GetMoney((long)this.gS_.auftragsspiel_gehalt, true));
			text = text.Replace("<NUM2>", this.mS_.GetMoney((long)this.gS_.auftragsspiel_bonus, true));
			text = text.Replace("<NUM3>", this.gS_.auftragsspiel_mindestbewertung.ToString() + "%");
			this.uiObjects[0].GetComponent<Text>().text = text;
			GameObject gameObject = GameObject.Find("PUB_" + this.gS_.publisherID.ToString());
			if (gameObject)
			{
				this.uiObjects[1].GetComponent<Image>().sprite = gameObject.GetComponent<publisherScript>().GetLogo();
			}
			if (this.mS_.multiplayer)
			{
				if (this.mS_.mpCalls_.isServer)
				{
					this.mS_.mpCalls_.SERVER_Send_Game(this.gS_);
				}
				if (this.mS_.mpCalls_.isClient)
				{
					this.mS_.mpCalls_.CLIENT_Send_Game(this.gS_);
				}
			}
		}
	}

	// Token: 0x06000886 RID: 2182 RVA: 0x000064BB File Offset: 0x000046BB
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x06000887 RID: 2183 RVA: 0x0006F95C File Offset: 0x0006DB5C
	public void BUTTON_Abbrechen()
	{
		if (this.gS_)
		{
			this.mS_.Earn((long)this.gS_.auftragsspiel_gehalt, 6);
		}
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.CloseMenu();
	}

	// Token: 0x04000D0E RID: 3342
	public GameObject[] uiObjects;

	// Token: 0x04000D0F RID: 3343
	private GameObject main_;

	// Token: 0x04000D10 RID: 3344
	private mainScript mS_;

	// Token: 0x04000D11 RID: 3345
	private textScript tS_;

	// Token: 0x04000D12 RID: 3346
	private GUI_Main guiMain_;

	// Token: 0x04000D13 RID: 3347
	private sfxScript sfx_;

	// Token: 0x04000D14 RID: 3348
	private gameScript gS_;

	// Token: 0x04000D15 RID: 3349
	public int myID;
}
