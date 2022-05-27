using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200025B RID: 603
public class Menu_Stats_Tochterfirma_Main : MonoBehaviour
{
	// Token: 0x06001765 RID: 5989 RVA: 0x00010559 File Offset: 0x0000E759
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001766 RID: 5990 RVA: 0x000F3050 File Offset: 0x000F1250
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

	// Token: 0x06001767 RID: 5991 RVA: 0x00010561 File Offset: 0x0000E761
	public void Init(publisherScript script_)
	{
		this.FindScripts();
		this.pS_ = script_;
		this.UpdateData();
	}

	// Token: 0x06001768 RID: 5992 RVA: 0x000F3118 File Offset: 0x000F1318
	public void UpdateData()
	{
		if (!this.pS_)
		{
			return;
		}
		this.uiObjects[1].GetComponent<Image>().sprite = this.pS_.GetLogo();
		this.guiMain_.DrawStarsColor(this.uiObjects[2], Mathf.RoundToInt(this.pS_.stars / 20f), Color.white);
		this.uiObjects[5].GetComponent<Text>().text = this.pS_.GetDateString();
		this.uiObjects[6].GetComponent<Image>().sprite = this.genres_.GetPic(this.pS_.fanGenre);
		this.uiObjects[6].GetComponent<tooltip>().c = this.tS_.GetText(437) + ": <b>" + this.genres_.GetName(this.pS_.fanGenre) + "</b>";
		this.uiObjects[7].GetComponent<Text>().text = this.tS_.GetText(685) + ": <b>" + this.pS_.GetFirmenwertString() + "</b>";
		this.uiObjects[14].GetComponent<Text>().text = this.tS_.GetText(1934) + ": <b>" + this.mS_.GetMoney(this.pS_.GetVerwaltungskosten(), true) + "</b>";
		if (this.pS_.developer)
		{
			int num = this.pS_.newGameInWeeks;
			if (num < 2)
			{
				num = 2;
			}
			string text = this.tS_.GetText(1948);
			text = text.Replace("<NUM>", "<color=blue><b>" + num.ToString() + "</b></color>");
			this.uiObjects[21].GetComponent<Text>().text = text;
			float num2 = (float)this.pS_.newGameInWeeksORG;
			if (num2 <= (float)this.pS_.newGameInWeeks)
			{
				num2 = (float)this.pS_.newGameInWeeks;
			}
			num2 = 100f / num2;
			num2 = 100f - num2 * (float)this.pS_.newGameInWeeks;
			this.uiObjects[19].GetComponent<Image>().fillAmount = num2 * 0.01f;
			if (num <= 2)
			{
				this.uiObjects[19].GetComponent<Image>().fillAmount = 1f;
			}
			if (this.pS_.newGameInWeeks > 2)
			{
				this.uiObjects[20].GetComponent<Text>().text = this.tS_.GetText(1944) + ": " + Mathf.RoundToInt(num2).ToString() + "%";
			}
			else
			{
				this.uiObjects[20].GetComponent<Text>().text = this.tS_.GetText(1947);
			}
		}
		else
		{
			this.uiObjects[21].GetComponent<Text>().text = "";
			this.uiObjects[20].GetComponent<Text>().text = this.tS_.GetText(1949);
			this.uiObjects[19].GetComponent<Image>().fillAmount = 0f;
		}
		if (this.pS_.tf_geschlossen)
		{
			this.uiObjects[12].SetActive(true);
			this.uiObjects[18].SetActive(true);
			this.uiObjects[0].GetComponent<Text>().text = "<color=red>" + this.pS_.GetName() + "</color>";
			this.uiObjects[10].GetComponent<Text>().text = this.pS_.GetDeveloperPublisherString();
		}
		else
		{
			this.uiObjects[12].SetActive(false);
			this.uiObjects[18].SetActive(false);
			this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
			this.uiObjects[10].GetComponent<Text>().text = this.pS_.GetDeveloperPublisherString();
		}
		if (!this.pS_.publisher)
		{
			this.uiObjects[13].SetActive(true);
			this.uiObjects[4].GetComponent<Text>().text = this.tS_.GetText(436) + ": <b>$ -</b>";
		}
		else
		{
			this.uiObjects[13].SetActive(false);
			this.uiObjects[4].GetComponent<Text>().text = this.tS_.GetText(436) + ": <b>$" + this.mS_.Round(this.pS_.share, 1).ToString() + "</b>";
		}
		if (!this.pS_.tf_publisher)
		{
			this.uiObjects[8].GetComponent<Button>().interactable = false;
			this.uiObjects[16].GetComponent<Button>().interactable = true;
		}
		else
		{
			this.uiObjects[8].GetComponent<Button>().interactable = true;
			this.uiObjects[16].GetComponent<Button>().interactable = false;
		}
		if (!this.pS_.tf_developer)
		{
			this.uiObjects[9].GetComponent<Button>().interactable = false;
			this.uiObjects[17].GetComponent<Button>().interactable = true;
		}
		else
		{
			this.uiObjects[9].GetComponent<Button>().interactable = true;
			this.uiObjects[17].GetComponent<Button>().interactable = false;
		}
		if (this.pS_.stars >= 100f || this.pS_.GetStarsAmount() >= 5)
		{
			this.uiObjects[15].GetComponent<Button>().interactable = false;
		}
		else
		{
			this.uiObjects[15].GetComponent<Button>().interactable = true;
		}
		this.UpdateGewinnanteilTooltip();
	}

	// Token: 0x06001769 RID: 5993 RVA: 0x000F36C4 File Offset: 0x000F18C4
	private void UpdateGewinnanteilTooltip()
	{
		string text = this.tS_.GetText(1991);
		text = text.Replace("<NUM>", "<color=blue><b>" + this.mS_.GetMoney((long)Mathf.RoundToInt(this.pS_.share), true) + "</b></color>");
		this.uiObjects[22].GetComponent<tooltip>().c = text;
	}

	// Token: 0x0600176A RID: 5994 RVA: 0x00010576 File Offset: 0x0000E776
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[385].GetComponent<Menu_Statistics_Tochterfirmen>().BUTTON_Search();
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600176B RID: 5995 RVA: 0x000F3730 File Offset: 0x000F1930
	public void BUTTON_Rename()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[391]);
		this.guiMain_.uiObjects[391].GetComponent<Menu_TochterfirmaRename>().Init(this.pS_);
	}

	// Token: 0x0600176C RID: 5996 RVA: 0x000F3788 File Offset: 0x000F1988
	public void BUTTON_Games()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[360]);
		this.guiMain_.uiObjects[360].GetComponent<Menu_Stats_Developer_Games>().Init(this.pS_);
	}

	// Token: 0x0600176D RID: 5997 RVA: 0x000F37E0 File Offset: 0x000F19E0
	public void BUTTON_IPs()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[361]);
		this.guiMain_.uiObjects[361].GetComponent<Menu_Stats_Developer_IPs>().Init(this.pS_);
	}

	// Token: 0x0600176E RID: 5998 RVA: 0x000F3838 File Offset: 0x000F1A38
	public void BUTTON_Settings()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[393]);
		this.guiMain_.uiObjects[393].GetComponent<Menu_Stats_TochterfirmaSettings>().Init(this.pS_);
	}

	// Token: 0x0600176F RID: 5999 RVA: 0x000F3890 File Offset: 0x000F1A90
	public void BUTTON_Vertrieben()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[374]);
		this.guiMain_.uiObjects[374].GetComponent<Menu_Stats_Publisher_Vertrieben>().Init(this.pS_);
	}

	// Token: 0x06001770 RID: 6000 RVA: 0x000F38E8 File Offset: 0x000F1AE8
	public void BUTTON_Umsatz()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[396]);
		this.guiMain_.uiObjects[396].GetComponent<Menu_Stats_TochterfirmaUmsatz>().Init(this.pS_);
	}

	// Token: 0x06001771 RID: 6001 RVA: 0x000F3940 File Offset: 0x000F1B40
	public void BUTTON_FirmaVerkaufen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[395]);
		this.guiMain_.uiObjects[395].GetComponent<Menu_W_FirmaVerkaufen>().Init(this.pS_);
	}

	// Token: 0x06001772 RID: 6002 RVA: 0x000105AC File Offset: 0x0000E7AC
	public void BUTTON_FirmaSchiessen()
	{
		this.sfx_.PlaySound(3, true);
		if (this.pS_)
		{
			this.pS_.tf_geschlossen = !this.pS_.tf_geschlossen;
		}
		this.UpdateData();
	}

	// Token: 0x06001773 RID: 6003 RVA: 0x000F3998 File Offset: 0x000F1B98
	public void BUTTON_FirmaAufwerten()
	{
		if (this.pS_)
		{
			this.sfx_.PlaySound(3, true);
			this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[388]);
			this.guiMain_.uiObjects[388].GetComponent<Menu_W_FirmaAufwerten>().Init(this.pS_);
		}
	}

	// Token: 0x06001774 RID: 6004 RVA: 0x000F39FC File Offset: 0x000F1BFC
	public void BUTTON_FirmaAufwertenPublisher()
	{
		if (this.pS_)
		{
			this.sfx_.PlaySound(3, true);
			this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[389]);
			this.guiMain_.uiObjects[389].GetComponent<Menu_W_FirmaAufwertenPublisher>().Init(this.pS_);
		}
	}

	// Token: 0x06001775 RID: 6005 RVA: 0x000F3A60 File Offset: 0x000F1C60
	public void BUTTON_FirmaAufwertenDeveloper()
	{
		if (this.pS_)
		{
			this.sfx_.PlaySound(3, true);
			this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[390]);
			this.guiMain_.uiObjects[390].GetComponent<Menu_W_FirmaAufwertenDeveloper>().Init(this.pS_);
		}
	}

	// Token: 0x06001776 RID: 6006 RVA: 0x000105E7 File Offset: 0x0000E7E7
	private IEnumerator iMinusGewinnbeteiligung(int i)
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_MinusGewinnbeteiligung(i);
		}
		yield break;
	}

	// Token: 0x06001777 RID: 6007 RVA: 0x000F3AC4 File Offset: 0x000F1CC4
	public void BUTTON_MinusGewinnbeteiligung(int i)
	{
		if (this.pS_)
		{
			this.sfx_.PlaySound(3, true);
			this.pS_.share -= (float)i;
			if (this.pS_.share < 3f)
			{
				this.pS_.share = 3f;
			}
			base.StartCoroutine(this.iMinusGewinnbeteiligung(i));
			this.UpdateData();
		}
	}

	// Token: 0x06001778 RID: 6008 RVA: 0x000105FD File Offset: 0x0000E7FD
	private IEnumerator iPlusGewinnbeteiligung(int i)
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_PlusGewinnbeteiligung(i);
		}
		yield break;
	}

	// Token: 0x06001779 RID: 6009 RVA: 0x000F3B38 File Offset: 0x000F1D38
	public void BUTTON_PlusGewinnbeteiligung(int i)
	{
		if (this.pS_)
		{
			this.sfx_.PlaySound(3, true);
			this.pS_.share += (float)i;
			if (this.pS_.share > 9f)
			{
				this.pS_.share = 9f;
			}
			base.StartCoroutine(this.iPlusGewinnbeteiligung(i));
			this.UpdateData();
		}
	}

	// Token: 0x04001B4C RID: 6988
	public GameObject[] uiObjects;

	// Token: 0x04001B4D RID: 6989
	private roomScript rS_;

	// Token: 0x04001B4E RID: 6990
	private GameObject main_;

	// Token: 0x04001B4F RID: 6991
	private mainScript mS_;

	// Token: 0x04001B50 RID: 6992
	private textScript tS_;

	// Token: 0x04001B51 RID: 6993
	private GUI_Main guiMain_;

	// Token: 0x04001B52 RID: 6994
	private sfxScript sfx_;

	// Token: 0x04001B53 RID: 6995
	private genres genres_;

	// Token: 0x04001B54 RID: 6996
	private publisherScript pS_;
}
