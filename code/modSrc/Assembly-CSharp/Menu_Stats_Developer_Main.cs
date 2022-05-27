using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000233 RID: 563
public class Menu_Stats_Developer_Main : MonoBehaviour
{
	// Token: 0x060015B8 RID: 5560 RVA: 0x000DD26F File Offset: 0x000DB46F
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060015B9 RID: 5561 RVA: 0x000DD278 File Offset: 0x000DB478
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

	// Token: 0x060015BA RID: 5562 RVA: 0x000DD340 File Offset: 0x000DB540
	public void Init(publisherScript script_)
	{
		this.FindScripts();
		this.pS_ = script_;
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
		this.uiObjects[1].GetComponent<Image>().sprite = this.pS_.GetLogo();
		this.guiMain_.DrawStarsColor(this.uiObjects[2], Mathf.RoundToInt(this.pS_.stars / 20f), Color.white);
		this.uiObjects[9].GetComponent<Image>().sprite = this.guiMain_.flagSprites[this.pS_.country];
		this.uiObjects[4].GetComponent<Text>().text = this.tS_.GetText(685) + ": <b>" + this.pS_.GetFirmenwertString() + "</b>";
		this.uiObjects[7].GetComponent<Image>().sprite = this.genres_.GetPic(this.pS_.fanGenre);
		this.uiObjects[7].GetComponent<tooltip>().c = this.tS_.GetText(437) + ": <b>" + this.genres_.GetName(this.pS_.fanGenre) + "</b>";
		this.uiObjects[3].GetComponent<Text>().text = this.pS_.GetDateString();
		if (this.pS_.IsTochterfirma() && this.pS_.TochterfirmaGeschlossen())
		{
			Text component = this.uiObjects[3].GetComponent<Text>();
			component.text = component.text + "\n<color=red><b>" + this.tS_.GetText(1969) + "</b></color>";
		}
		if (this.pS_.IsMyTochterfirma())
		{
			if (!this.uiObjects[8].activeSelf)
			{
				this.uiObjects[8].SetActive(true);
			}
		}
		else if (this.uiObjects[8].activeSelf)
		{
			this.uiObjects[8].SetActive(false);
		}
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

	// Token: 0x060015BB RID: 5563 RVA: 0x000DD5F9 File Offset: 0x000DB7F9
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060015BC RID: 5564 RVA: 0x000DD614 File Offset: 0x000DB814
	public void BUTTON_Awards()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[144]);
		this.guiMain_.uiObjects[144].GetComponent<Menu_Stats_Awards>().Init(this.pS_);
	}

	// Token: 0x060015BD RID: 5565 RVA: 0x000DD66C File Offset: 0x000DB86C
	public void BUTTON_Games()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[360]);
		this.guiMain_.uiObjects[360].GetComponent<Menu_Stats_Developer_Games>().Init(this.pS_);
	}

	// Token: 0x060015BE RID: 5566 RVA: 0x000DD6C4 File Offset: 0x000DB8C4
	public void BUTTON_IPs()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[361]);
		this.guiMain_.uiObjects[361].GetComponent<Menu_Stats_Developer_IPs>().Init(this.pS_);
	}

	// Token: 0x060015BF RID: 5567 RVA: 0x000DD71C File Offset: 0x000DB91C
	public void BUTTON_FirmaKaufen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[386]);
		this.guiMain_.uiObjects[386].GetComponent<Menu_W_FirmaKaufen>().Init(this.pS_);
	}

	// Token: 0x040019A6 RID: 6566
	public GameObject[] uiObjects;

	// Token: 0x040019A7 RID: 6567
	private roomScript rS_;

	// Token: 0x040019A8 RID: 6568
	private GameObject main_;

	// Token: 0x040019A9 RID: 6569
	private mainScript mS_;

	// Token: 0x040019AA RID: 6570
	private textScript tS_;

	// Token: 0x040019AB RID: 6571
	private GUI_Main guiMain_;

	// Token: 0x040019AC RID: 6572
	private sfxScript sfx_;

	// Token: 0x040019AD RID: 6573
	private genres genres_;

	// Token: 0x040019AE RID: 6574
	private publisherScript pS_;
}
