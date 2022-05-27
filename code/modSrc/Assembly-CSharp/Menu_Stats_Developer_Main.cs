using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000232 RID: 562
public class Menu_Stats_Developer_Main : MonoBehaviour
{
	// Token: 0x0600159B RID: 5531 RVA: 0x0000EE16 File Offset: 0x0000D016
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600159C RID: 5532 RVA: 0x000E57E4 File Offset: 0x000E39E4
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

	// Token: 0x0600159D RID: 5533 RVA: 0x000E5890 File Offset: 0x000E3A90
	public void Init(publisherScript script_)
	{
		this.FindScripts();
		this.pS_ = script_;
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
		this.uiObjects[1].GetComponent<Image>().sprite = this.pS_.GetLogo();
		this.guiMain_.DrawStarsColor(this.uiObjects[2], Mathf.RoundToInt(this.pS_.stars / 20f), Color.white);
		this.uiObjects[3].GetComponent<Text>().text = this.pS_.GetDateString();
		this.uiObjects[4].GetComponent<Text>().text = this.tS_.GetText(685) + ": <b>" + this.pS_.GetFirmenwertString() + "</b>";
		if (this.pS_.tf_geschlossen)
		{
			if (!this.uiObjects[6].activeSelf)
			{
				this.uiObjects[6].SetActive(true);
			}
		}
		else if (this.uiObjects[6].activeSelf)
		{
			this.uiObjects[6].SetActive(false);
		}
		if (this.pS_.IsMyTochterfirma() || this.pS_.notForSale)
		{
			this.uiObjects[5].GetComponent<Button>().interactable = false;
		}
		else
		{
			this.uiObjects[5].GetComponent<Button>().interactable = true;
		}
		if (this.mS_.multiplayer)
		{
			this.uiObjects[5].GetComponent<Button>().interactable = false;
		}
	}

	// Token: 0x0600159E RID: 5534 RVA: 0x0000EE1E File Offset: 0x0000D01E
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600159F RID: 5535 RVA: 0x000E5A18 File Offset: 0x000E3C18
	public void BUTTON_Games()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[360]);
		this.guiMain_.uiObjects[360].GetComponent<Menu_Stats_Developer_Games>().Init(this.pS_);
	}

	// Token: 0x060015A0 RID: 5536 RVA: 0x000E5A70 File Offset: 0x000E3C70
	public void BUTTON_IPs()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[361]);
		this.guiMain_.uiObjects[361].GetComponent<Menu_Stats_Developer_IPs>().Init(this.pS_);
	}

	// Token: 0x060015A1 RID: 5537 RVA: 0x000E5AC8 File Offset: 0x000E3CC8
	public void BUTTON_FirmaKaufen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[386]);
		this.guiMain_.uiObjects[386].GetComponent<Menu_W_FirmaKaufen>().Init(this.pS_);
	}

	// Token: 0x0400199E RID: 6558
	public GameObject[] uiObjects;

	// Token: 0x0400199F RID: 6559
	private roomScript rS_;

	// Token: 0x040019A0 RID: 6560
	private GameObject main_;

	// Token: 0x040019A1 RID: 6561
	private mainScript mS_;

	// Token: 0x040019A2 RID: 6562
	private textScript tS_;

	// Token: 0x040019A3 RID: 6563
	private GUI_Main guiMain_;

	// Token: 0x040019A4 RID: 6564
	private sfxScript sfx_;

	// Token: 0x040019A5 RID: 6565
	private publisherScript pS_;
}
