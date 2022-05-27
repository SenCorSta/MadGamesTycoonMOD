using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001EB RID: 491
public class MenuPublishingOfferVerhandlung : MonoBehaviour
{
	// Token: 0x0600129D RID: 4765 RVA: 0x000C6120 File Offset: 0x000C4320
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600129E RID: 4766 RVA: 0x000C6128 File Offset: 0x000C4328
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
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
	}

	// Token: 0x0600129F RID: 4767 RVA: 0x000C6210 File Offset: 0x000C4410
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
		if (!this.game_.pubAngebot_AngebotWoche)
		{
			this.uiObjects[5].GetComponent<Button>().interactable = true;
		}
		else
		{
			this.uiObjects[5].GetComponent<Button>().interactable = false;
		}
		if (!this.game_.pubAngebot || this.game_.isOnMarket)
		{
			this.BUTTON_Abbrechen();
		}
	}

	// Token: 0x060012A0 RID: 4768 RVA: 0x000C628C File Offset: 0x000C448C
	public void Init(gameScript pO_)
	{
		if (!pO_)
		{
			this.BUTTON_Abbrechen();
		}
		this.game_ = pO_;
		this.FindScripts();
		this.uiObjects[3].GetComponent<Slider>().value = 0f;
		string text = this.tS_.GetText(1735);
		text = text.Replace("<NAME1>", this.game_.GetDeveloperName());
		text = text.Replace("<NAME2>", this.game_.GetNameWithTag());
		this.uiObjects[0].GetComponent<Text>().text = text;
		this.garantiesummeAngebot = this.game_.PUBOFFER_GetGarantiesumme();
		this.gewinnbeteiligungAngebot = (float)this.game_.PUBOFFER_GetGewinnbeteiligung();
		this.SetData();
	}

	// Token: 0x060012A1 RID: 4769 RVA: 0x000C6348 File Offset: 0x000C4548
	private void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)this.garantiesummeAngebot, true);
		this.uiObjects[2].GetComponent<Text>().text = Mathf.RoundToInt(this.gewinnbeteiligungAngebot).ToString() + "%";
		this.uiObjects[4].GetComponent<Image>().fillAmount = this.game_.pubAngebot_Stimmung * 0.01f;
		if (this.game_.pubAngebot_Stimmung < 33f)
		{
			this.uiObjects[4].GetComponent<Image>().color = this.guiMain_.colorsBalken[0];
		}
		if (this.game_.pubAngebot_Stimmung > 33f && this.game_.pubAngebot_Stimmung < 66f)
		{
			this.uiObjects[4].GetComponent<Image>().color = this.guiMain_.colorsBalken[1];
		}
		if (this.game_.pubAngebot_Stimmung > 66f)
		{
			this.uiObjects[4].GetComponent<Image>().color = this.guiMain_.colorsBalken[2];
		}
	}

	// Token: 0x060012A2 RID: 4770 RVA: 0x000C648C File Offset: 0x000C468C
	public void SLIDER_Angebot()
	{
		float num = this.game_.pubAngebot_VerhandlungProzent - this.uiObjects[3].GetComponent<Slider>().value;
		num *= 0.01f;
		this.garantiesummeAngebot = Mathf.RoundToInt((float)this.game_.pubAngebot_Garantiesumme * num);
		this.gewinnbeteiligungAngebot = (float)Mathf.RoundToInt(this.game_.pubAngebot_Gewinnbeteiligung * num);
		this.SetData();
	}

	// Token: 0x060012A3 RID: 4771 RVA: 0x000C64F8 File Offset: 0x000C46F8
	private IEnumerator iMinus(int i)
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_Minus(i);
		}
		yield break;
	}

	// Token: 0x060012A4 RID: 4772 RVA: 0x000C6510 File Offset: 0x000C4710
	public void BUTTON_Minus(int i)
	{
		this.sfx_.PlaySound(3, true);
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			this.uiObjects[3].GetComponent<Slider>().value -= 10f;
		}
		else
		{
			this.uiObjects[3].GetComponent<Slider>().value -= 1f;
		}
		base.StartCoroutine(this.iMinus(i));
	}

	// Token: 0x060012A5 RID: 4773 RVA: 0x000C658E File Offset: 0x000C478E
	private IEnumerator iPlus(int i)
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_Plus(i);
		}
		yield break;
	}

	// Token: 0x060012A6 RID: 4774 RVA: 0x000C65A4 File Offset: 0x000C47A4
	public void BUTTON_Plus(int i)
	{
		this.sfx_.PlaySound(3, true);
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			this.uiObjects[3].GetComponent<Slider>().value += 10f;
		}
		else
		{
			this.uiObjects[3].GetComponent<Slider>().value += 1f;
		}
		base.StartCoroutine(this.iPlus(i));
	}

	// Token: 0x060012A7 RID: 4775 RVA: 0x000C6624 File Offset: 0x000C4824
	public void BUTTON_Angebot()
	{
		if (this.uiObjects[3].GetComponent<Slider>().value <= 0f)
		{
			return;
		}
		this.sfx_.PlaySound(3, true);
		this.game_.pubAngebot_VerhandlungProzent -= this.uiObjects[3].GetComponent<Slider>().value;
		this.game_.pubAngebot_Stimmung -= this.uiObjects[3].GetComponent<Slider>().value * UnityEngine.Random.Range(this.game_.pubAngebot_Verhandlung, this.game_.pubAngebot_Verhandlung + 2f);
		this.uiObjects[3].GetComponent<Slider>().value = 0f;
		if (this.game_.pubAngebot_Stimmung < 0f)
		{
			this.game_.pubAngebot_Stimmung = 0f;
		}
		if (this.game_.pubAngebot_Stimmung > 100f)
		{
			this.game_.pubAngebot_Stimmung = 100f;
		}
		if (this.game_.pubAngebot_VerhandlungProzent < 0f)
		{
			this.game_.pubAngebot_VerhandlungProzent = 0f;
		}
		if (this.game_.pubAngebot_VerhandlungProzent > 100f)
		{
			this.game_.pubAngebot_VerhandlungProzent = 100f;
		}
		this.game_.pubAngebot_AngebotWoche = true;
		this.SLIDER_Angebot();
		if (this.game_.pubAngebot_Stimmung <= 0f)
		{
			this.BUTTON_Abbrechen();
			this.sfx_.PlaySound(53, true);
			this.guiMain_.MessageBox(this.tS_.GetText(1738), false);
			this.game_.pubAnbgebot_Inivs = true;
			this.mS_.publishingOfferMain_.amountPublishingOffers--;
			return;
		}
		this.sfx_.PlaySound(54, true);
	}

	// Token: 0x060012A8 RID: 4776 RVA: 0x000C67E8 File Offset: 0x000C49E8
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060012A9 RID: 4777 RVA: 0x000C6803 File Offset: 0x000C4A03
	public void BUTTON_OK()
	{
		this.sfx_.PlaySound(3, true);
		this.CreateGame();
	}

	// Token: 0x060012AA RID: 4778 RVA: 0x000C6818 File Offset: 0x000C4A18
	private void CreateGame()
	{
		this.mS_.Pay((long)this.game_.PUBOFFER_GetGarantiesumme(), 25);
		this.mS_.publishingOfferMain_.amountPublishingOffers--;
		this.game_.pubAngebot = false;
		this.game_.pubOffer = true;
		this.game_.costs_marketing = 0L;
		this.game_.costs_mitarbeiter = 0L;
		this.game_.costs_server = 0L;
		this.game_.hype = (float)UnityEngine.Random.Range(0, 15);
		this.game_.date_start_month = UnityEngine.Random.Range(1, 10);
		this.game_.date_start_year = this.mS_.year - UnityEngine.Random.Range(2, 4);
		if (this.game_.date_start_year < 1976)
		{
			this.game_.date_start_year = 1976;
			this.game_.date_start_month = 1;
		}
		if (!this.mS_.settings_RandomEventsOff)
		{
			if (this.game_.reviewTotal >= 70 && UnityEngine.Random.Range(0, 100) < this.mS_.difficulty)
			{
				this.game_.commercialFlop = true;
			}
			if (!this.game_.commercialFlop && !this.game_.handy && !this.game_.typ_addon && !this.game_.typ_budget && !this.game_.typ_bundleAddon && !this.game_.typ_contractGame && !this.game_.typ_goty && !this.game_.typ_mmoaddon && UnityEngine.Random.Range(0, 100) == 1)
			{
				this.game_.commercialHit = true;
			}
		}
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[218]);
		this.guiMain_.uiObjects[218].GetComponent<Menu_Packung>().Init(this.game_, null, true, true);
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
		this.guiMain_.uiObjects[349].SetActive(false);
		base.gameObject.SetActive(false);
	}

	// Token: 0x040016FB RID: 5883
	public GameObject[] uiObjects;

	// Token: 0x040016FC RID: 5884
	private GameObject main_;

	// Token: 0x040016FD RID: 5885
	private mainScript mS_;

	// Token: 0x040016FE RID: 5886
	private textScript tS_;

	// Token: 0x040016FF RID: 5887
	private GUI_Main guiMain_;

	// Token: 0x04001700 RID: 5888
	private sfxScript sfx_;

	// Token: 0x04001701 RID: 5889
	private genres genres_;

	// Token: 0x04001702 RID: 5890
	private games games_;

	// Token: 0x04001703 RID: 5891
	private gameScript game_;

	// Token: 0x04001704 RID: 5892
	private int garantiesummeAngebot;

	// Token: 0x04001705 RID: 5893
	private float gewinnbeteiligungAngebot;
}
