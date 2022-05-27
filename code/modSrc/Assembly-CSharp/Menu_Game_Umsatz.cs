using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000195 RID: 405
public class Menu_Game_Umsatz : MonoBehaviour
{
	// Token: 0x06000F52 RID: 3922 RVA: 0x0000AE8E File Offset: 0x0000908E
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000F53 RID: 3923 RVA: 0x000B020C File Offset: 0x000AE40C
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

	// Token: 0x06000F54 RID: 3924 RVA: 0x000B03AC File Offset: 0x000AE5AC
	public void Init(gameScript game_)
	{
		this.FindScripts();
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
		string text = "";
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

	// Token: 0x06000F55 RID: 3925 RVA: 0x0000AE96 File Offset: 0x00009096
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x06000F56 RID: 3926 RVA: 0x000B0E58 File Offset: 0x000AF058
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		if (!this.guiMain_.uiObjects[125].activeSelf && !this.guiMain_.uiObjects[231].activeSelf && !this.guiMain_.uiObjects[302].activeSelf && !this.guiMain_.uiObjects[306].activeSelf && !this.guiMain_.uiObjects[351].activeSelf && !this.guiMain_.uiObjects[357].activeSelf)
		{
			this.guiMain_.CloseMenu();
		}
	}

	// Token: 0x040013AF RID: 5039
	public GameObject[] uiObjects;

	// Token: 0x040013B0 RID: 5040
	private GameObject main_;

	// Token: 0x040013B1 RID: 5041
	private mainScript mS_;

	// Token: 0x040013B2 RID: 5042
	private textScript tS_;

	// Token: 0x040013B3 RID: 5043
	private GUI_Main guiMain_;

	// Token: 0x040013B4 RID: 5044
	private sfxScript sfx_;

	// Token: 0x040013B5 RID: 5045
	private genres genres_;

	// Token: 0x040013B6 RID: 5046
	private themes themes_;

	// Token: 0x040013B7 RID: 5047
	private licences licences_;

	// Token: 0x040013B8 RID: 5048
	private engineFeatures eF_;

	// Token: 0x040013B9 RID: 5049
	private cameraMovementScript cmS_;

	// Token: 0x040013BA RID: 5050
	private unlockScript unlock_;

	// Token: 0x040013BB RID: 5051
	private gameplayFeatures gF_;

	// Token: 0x040013BC RID: 5052
	private games games_;

	// Token: 0x040013BD RID: 5053
	private gameScript gS_;
}
