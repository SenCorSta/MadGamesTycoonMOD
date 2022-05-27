using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000251 RID: 593
public class Menu_Stats_Publisher_Main : MonoBehaviour
{
	// Token: 0x060016F1 RID: 5873 RVA: 0x0001015B File Offset: 0x0000E35B
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060016F2 RID: 5874 RVA: 0x000EDB40 File Offset: 0x000EBD40
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
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
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

	// Token: 0x060016F3 RID: 5875 RVA: 0x000EDC08 File Offset: 0x000EBE08
	public void Init(publisherScript script_)
	{
		this.FindScripts();
		this.pS_ = script_;
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
		this.uiObjects[1].GetComponent<Image>().sprite = this.pS_.GetLogo();
		this.guiMain_.DrawStarsColor(this.uiObjects[2], Mathf.RoundToInt(this.pS_.stars / 20f), Color.white);
		this.guiMain_.DrawStarsColor(this.uiObjects[3], Mathf.RoundToInt(this.pS_.GetRelation() / 20f), Color.white);
		this.uiObjects[4].GetComponent<Text>().text = this.tS_.GetText(436) + ": <b>$" + this.mS_.Round(this.pS_.share, 1).ToString() + "</b>";
		this.uiObjects[5].GetComponent<Text>().text = this.pS_.GetDateString();
		this.uiObjects[6].GetComponent<Image>().sprite = this.genres_.GetPic(this.pS_.fanGenre);
		this.uiObjects[6].GetComponent<tooltip>().c = this.tS_.GetText(437) + ": <b>" + this.genres_.GetName(this.pS_.fanGenre) + "</b>";
		this.uiObjects[7].GetComponent<Text>().text = this.tS_.GetText(685) + ": <b>" + this.pS_.GetFirmenwertString() + "</b>";
		if (this.pS_.tf_geschlossen)
		{
			if (!this.uiObjects[9].activeSelf)
			{
				this.uiObjects[9].SetActive(true);
			}
		}
		else if (this.uiObjects[9].activeSelf)
		{
			this.uiObjects[9].SetActive(false);
		}
		if (this.pS_.IsMyTochterfirma() || this.pS_.notForSale)
		{
			this.uiObjects[8].GetComponent<Button>().interactable = false;
		}
		else
		{
			this.uiObjects[8].GetComponent<Button>().interactable = true;
		}
		if (this.mS_.multiplayer)
		{
			this.uiObjects[8].GetComponent<Button>().interactable = false;
		}
	}

	// Token: 0x060016F4 RID: 5876 RVA: 0x00010163 File Offset: 0x0000E363
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060016F5 RID: 5877 RVA: 0x000EDE80 File Offset: 0x000EC080
	public void BUTTON_Games()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[360]);
		this.guiMain_.uiObjects[360].GetComponent<Menu_Stats_Developer_Games>().Init(this.pS_);
	}

	// Token: 0x060016F6 RID: 5878 RVA: 0x000EDED8 File Offset: 0x000EC0D8
	public void BUTTON_IPs()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[361]);
		this.guiMain_.uiObjects[361].GetComponent<Menu_Stats_Developer_IPs>().Init(this.pS_);
	}

	// Token: 0x060016F7 RID: 5879 RVA: 0x000EDF30 File Offset: 0x000EC130
	public void BUTTON_Vertrieben()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[374]);
		this.guiMain_.uiObjects[374].GetComponent<Menu_Stats_Publisher_Vertrieben>().Init(this.pS_);
	}

	// Token: 0x060016F8 RID: 5880 RVA: 0x000EDF88 File Offset: 0x000EC188
	public void BUTTON_FirmaKaufen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[386]);
		this.guiMain_.uiObjects[386].GetComponent<Menu_W_FirmaKaufen>().Init(this.pS_);
	}

	// Token: 0x04001AC7 RID: 6855
	public GameObject[] uiObjects;

	// Token: 0x04001AC8 RID: 6856
	private roomScript rS_;

	// Token: 0x04001AC9 RID: 6857
	private GameObject main_;

	// Token: 0x04001ACA RID: 6858
	private mainScript mS_;

	// Token: 0x04001ACB RID: 6859
	private textScript tS_;

	// Token: 0x04001ACC RID: 6860
	private GUI_Main guiMain_;

	// Token: 0x04001ACD RID: 6861
	private sfxScript sfx_;

	// Token: 0x04001ACE RID: 6862
	private genres genres_;

	// Token: 0x04001ACF RID: 6863
	private publisherScript pS_;
}
