﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000194 RID: 404
public class Menu_GameFromMarket : MonoBehaviour
{
	// Token: 0x06000F4C RID: 3916 RVA: 0x0000AE58 File Offset: 0x00009058
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000F4D RID: 3917 RVA: 0x000AF480 File Offset: 0x000AD680
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindGameObjectWithTag("Main");
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
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.themes_)
		{
			this.themes_ = this.main_.GetComponent<themes>();
		}
		if (!this.licences_)
		{
			this.licences_ = this.main_.GetComponent<licences>();
		}
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
		if (!this.cmS_)
		{
			this.cmS_ = GameObject.Find("CamMovement").GetComponent<cameraMovementScript>();
		}
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
		}
		if (!this.gF_)
		{
			this.gF_ = this.main_.GetComponent<gameplayFeatures>();
		}
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
	}

	// Token: 0x06000F4E RID: 3918 RVA: 0x000AF620 File Offset: 0x000AD820
	public void Init(gameScript game_, bool selbstVomMarktGenommen)
	{
		this.FindScripts();
		if (!selbstVomMarktGenommen && this.gS_)
		{
			this.listMenu.Add(game_);
			return;
		}
		this.sfx_.PlaySound(39, false);
		this.gS_ = game_;
		if (!this.gS_)
		{
			this.BUTTON_Close();
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.gS_.GetNameWithTag();
		this.uiObjects[1].GetComponent<Text>().text = this.gS_.GetDeveloperName() + " / " + this.gS_.GetPublisherName();
		if (this.gS_.pubOffer)
		{
			Text component = this.uiObjects[1].GetComponent<Text>();
			component.text = component.text + "\n<color=blue>" + this.tS_.GetText(1744) + "</color>";
		}
		string text;
		if (!selbstVomMarktGenommen)
		{
			text = this.tS_.GetText(490);
			text = text.Replace("<NAME>", this.gS_.GetNameWithTag());
			this.uiObjects[2].GetComponent<Text>().text = text;
		}
		else
		{
			text = this.tS_.GetText(1143);
			text = text.Replace("<NAME>", this.gS_.GetNameWithTag());
			this.uiObjects[2].GetComponent<Text>().text = text;
		}
		text = "";
		if (this.gS_.gameTyp != 2)
		{
			text = text + this.tS_.GetText(491) + "\n";
		}
		if (this.gS_.gameTyp == 2)
		{
			text = text + this.tS_.GetText(1391) + "\n";
		}
		if (this.gS_.bestChartPosition != 0)
		{
			text = text + this.tS_.GetText(1669) + "\n";
		}
		if (this.gS_.gameTyp != 2)
		{
			text = text + this.tS_.GetText(696) + "\n";
		}
		if (this.gS_.gameTyp == 2)
		{
			text = text + this.tS_.GetText(697) + "\n";
		}
		if (this.gS_.gameTyp != 2 && !this.gS_.handy)
		{
			if (this.gS_.sellsTotalStandard > 0L)
			{
				text = text + "     " + this.tS_.GetText(1103) + "\n";
			}
			if (this.gS_.sellsTotalDeluxe > 0L)
			{
				text = text + "     " + this.tS_.GetText(1104) + "\n";
			}
			if (this.gS_.sellsTotalCollectors > 0L)
			{
				text = text + "     " + this.tS_.GetText(1105) + "\n";
			}
			if (this.gS_.sellsTotalOnline > 0L)
			{
				text = text + "     " + this.tS_.GetText(1126) + "\n";
			}
		}
		text += "\n";
		if (!this.gS_.pubOffer)
		{
			text = text + this.tS_.GetText(6) + "\n";
		}
		else
		{
			text = text + this.tS_.GetText(1730) + "\n";
		}
		if (this.gS_.PUBOFFER_GetGewinnbeteiligung() > 0)
		{
			text = string.Concat(new string[]
			{
				text,
				this.tS_.GetText(1731),
				" (",
				Mathf.RoundToInt((float)this.gS_.PUBOFFER_GetGewinnbeteiligung()).ToString(),
				"%)\n"
			});
		}
		if (this.gS_.GetMarketingkosten() > 0L)
		{
			text = text + this.tS_.GetText(492) + "\n";
		}
		if (this.gS_.costs_enginegebuehren > 0L)
		{
			text = text + this.tS_.GetText(493) + "\n";
		}
		if (this.gS_.costs_production > 0L)
		{
			text = text + this.tS_.GetText(530) + "\n";
		}
		if (this.gS_.costs_updates > 0L)
		{
			text = text + this.tS_.GetText(1406) + "\n";
		}
		if (this.gS_.costs_server > 0L)
		{
			text = text + this.tS_.GetText(531) + "\n";
		}
		text += "\n";
		if (this.gS_.umsatzAbos > 0L)
		{
			text = text + this.tS_.GetText(1236) + "\n";
		}
		if (this.gS_.umsatzInApp > 0L)
		{
			text = text + this.tS_.GetText(1177) + "\n";
		}
		if (this.gS_.gameTyp != 2)
		{
			text += this.tS_.GetText(1239);
		}
		this.uiObjects[3].GetComponent<Text>().text = text;
		text = "";
		text = text + this.gS_.weeksOnMarket.ToString() + "\n";
		if (this.gS_.bestChartPosition != 0)
		{
			text = text + this.gS_.bestChartPosition.ToString() + "\n";
		}
		text = text + this.mS_.GetMoney(this.gS_.sellsTotal, false) + "\n";
		if (this.gS_.gameTyp != 2 && !this.gS_.handy)
		{
			if (this.gS_.sellsTotalStandard > 0L)
			{
				text = text + this.mS_.GetMoney(this.gS_.sellsTotalStandard, false) + "\n";
			}
			if (this.gS_.sellsTotalDeluxe > 0L)
			{
				text = text + this.mS_.GetMoney(this.gS_.sellsTotalDeluxe, false) + "\n";
			}
			if (this.gS_.sellsTotalCollectors > 0L)
			{
				text = text + this.mS_.GetMoney(this.gS_.sellsTotalCollectors, false) + "\n";
			}
			if (this.gS_.sellsTotalOnline > 0L)
			{
				text = text + this.mS_.GetMoney(this.gS_.sellsTotalOnline, false) + "\n";
			}
		}
		text += "\n";
		text += "<color=red>";
		if (!this.gS_.pubOffer)
		{
			text = text + this.mS_.GetMoney(this.gS_.GetEntwicklungskosten(), true) + "\n";
		}
		else
		{
			text = text + this.mS_.GetMoney((long)this.gS_.PUBOFFER_GetGarantiesumme(), true) + "\n";
		}
		if (this.gS_.PUBOFFER_GetGewinnbeteiligung() > 0)
		{
			text = text + this.mS_.GetMoney(this.gS_.GetUmsatzbeteiligung(), true) + "\n";
		}
		if (this.gS_.GetMarketingkosten() > 0L)
		{
			text = text + this.mS_.GetMoney(this.gS_.GetMarketingkosten(), true) + "\n";
		}
		if (this.gS_.costs_enginegebuehren > 0L)
		{
			text = text + this.mS_.GetMoney(this.gS_.costs_enginegebuehren, true) + "\n";
		}
		if (this.gS_.costs_production > 0L)
		{
			text = text + this.mS_.GetMoney(this.gS_.costs_production, true) + "\n";
		}
		if (this.gS_.costs_updates > 0L)
		{
			text = text + this.mS_.GetMoney(this.gS_.costs_updates, true) + "\n";
		}
		if (this.gS_.costs_server > 0L)
		{
			text = text + this.mS_.GetMoney(this.gS_.costs_server, true) + "\n";
		}
		text += "</color>";
		text += "\n";
		text += "<color=green>";
		if (this.gS_.umsatzAbos > 0L)
		{
			text = text + this.mS_.GetMoney(this.gS_.umsatzAbos, true) + "\n";
		}
		if (this.gS_.umsatzInApp > 0L)
		{
			text = text + this.mS_.GetMoney(this.gS_.umsatzInApp, true) + "\n";
		}
		if (this.gS_.gameTyp != 2)
		{
			text += this.mS_.GetMoney(this.gS_.GetUmsatzVerkauf(), true);
		}
		text += "</color>";
		this.uiObjects[4].GetComponent<Text>().text = text;
		if (this.gS_.GetGesamtGewinn() < 0L)
		{
			this.uiObjects[5].GetComponent<Text>().text = "<color=red>" + this.mS_.GetMoney(this.gS_.GetGesamtGewinn(), true) + "</color>";
		}
		else
		{
			this.uiObjects[5].GetComponent<Text>().text = "<color=green>" + this.mS_.GetMoney(this.gS_.GetGesamtGewinn(), true) + "</color>";
		}
		if (!this.gS_.exklusiv && !this.gS_.herstellerExklusiv)
		{
			if (this.uiObjects[6].activeSelf)
			{
				this.uiObjects[6].SetActive(false);
				return;
			}
		}
		else
		{
			if (!this.uiObjects[6].activeSelf)
			{
				this.uiObjects[6].SetActive(true);
			}
			if (!this.gS_.gamePlatformScript[0])
			{
				this.gS_.FindMyPlatforms();
			}
			if (this.gS_.gamePlatformScript[0])
			{
				if (this.gS_.exklusiv)
				{
					string text2 = this.tS_.GetText(1314);
					text2 = text2.Replace("<NUM>", this.mS_.GetMoney(this.gS_.exklusivKonsolenSells, false));
					text2 = text2.Replace("<NAME>", this.gS_.gamePlatformScript[0].GetName());
					this.gS_.gamePlatformScript[0].SetPic(this.uiObjects[7]);
					this.uiObjects[6].GetComponent<Text>().text = text2;
				}
				if (this.gS_.herstellerExklusiv)
				{
					string text3 = this.tS_.GetText(1697);
					text3 = text3.Replace("<NUM>", this.mS_.GetMoney(this.gS_.exklusivKonsolenSells, false));
					this.uiObjects[7].GetComponent<Image>().sprite = this.guiMain_.uiSprites[19];
					this.uiObjects[6].GetComponent<Text>().text = text3;
					return;
				}
			}
			else
			{
				this.uiObjects[6].GetComponent<Text>().text = "";
				this.uiObjects[7].GetComponent<Image>().sprite = this.guiMain_.uiSprites[19];
			}
		}
	}

	// Token: 0x06000F4F RID: 3919 RVA: 0x0000AE60 File Offset: 0x00009060
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x06000F50 RID: 3920 RVA: 0x000B0170 File Offset: 0x000AE370
	public void BUTTON_Close()
	{
		this.FindScripts();
		this.sfx_.PlaySound(3, true);
		this.gS_ = null;
		if (this.listMenu.Count > 0)
		{
			base.gameObject.SetActive(false);
			base.gameObject.SetActive(true);
			this.Init(this.listMenu[0], false);
			this.listMenu.RemoveAt(0);
			return;
		}
		base.gameObject.SetActive(false);
		if (!this.guiMain_.uiObjects[223].activeSelf)
		{
			this.guiMain_.CloseMenu();
		}
	}

	// Token: 0x0400139F RID: 5023
	public GameObject[] uiObjects;

	// Token: 0x040013A0 RID: 5024
	private GameObject main_;

	// Token: 0x040013A1 RID: 5025
	private mainScript mS_;

	// Token: 0x040013A2 RID: 5026
	private textScript tS_;

	// Token: 0x040013A3 RID: 5027
	private GUI_Main guiMain_;

	// Token: 0x040013A4 RID: 5028
	private sfxScript sfx_;

	// Token: 0x040013A5 RID: 5029
	private genres genres_;

	// Token: 0x040013A6 RID: 5030
	private themes themes_;

	// Token: 0x040013A7 RID: 5031
	private licences licences_;

	// Token: 0x040013A8 RID: 5032
	private engineFeatures eF_;

	// Token: 0x040013A9 RID: 5033
	private cameraMovementScript cmS_;

	// Token: 0x040013AA RID: 5034
	private unlockScript unlock_;

	// Token: 0x040013AB RID: 5035
	private gameplayFeatures gF_;

	// Token: 0x040013AC RID: 5036
	private games games_;

	// Token: 0x040013AD RID: 5037
	private gameScript gS_;

	// Token: 0x040013AE RID: 5038
	public List<gameScript> listMenu = new List<gameScript>();
}
