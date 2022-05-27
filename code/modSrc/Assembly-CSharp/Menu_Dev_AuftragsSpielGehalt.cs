using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200010A RID: 266
public class Menu_Dev_AuftragsSpielGehalt : MonoBehaviour
{
	// Token: 0x06000892 RID: 2194 RVA: 0x0005DCDF File Offset: 0x0005BEDF
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000893 RID: 2195 RVA: 0x0005DCE8 File Offset: 0x0005BEE8
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

	// Token: 0x06000894 RID: 2196 RVA: 0x0005DD94 File Offset: 0x0005BF94
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

	// Token: 0x06000895 RID: 2197 RVA: 0x0005DEED File Offset: 0x0005C0ED
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x06000896 RID: 2198 RVA: 0x0005DF08 File Offset: 0x0005C108
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

	// Token: 0x04000D16 RID: 3350
	public GameObject[] uiObjects;

	// Token: 0x04000D17 RID: 3351
	private GameObject main_;

	// Token: 0x04000D18 RID: 3352
	private mainScript mS_;

	// Token: 0x04000D19 RID: 3353
	private textScript tS_;

	// Token: 0x04000D1A RID: 3354
	private GUI_Main guiMain_;

	// Token: 0x04000D1B RID: 3355
	private sfxScript sfx_;

	// Token: 0x04000D1C RID: 3356
	private gameScript gS_;

	// Token: 0x04000D1D RID: 3357
	public int myID;
}
