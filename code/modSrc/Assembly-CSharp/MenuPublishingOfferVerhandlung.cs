using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001EA RID: 490
public class MenuPublishingOfferVerhandlung : MonoBehaviour
{
	// Token: 0x06001282 RID: 4738 RVA: 0x0000CC88 File Offset: 0x0000AE88
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001283 RID: 4739 RVA: 0x000D0C24 File Offset: 0x000CEE24
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

	// Token: 0x06001284 RID: 4740 RVA: 0x000D0D0C File Offset: 0x000CEF0C
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

	// Token: 0x06001285 RID: 4741 RVA: 0x000D0D88 File Offset: 0x000CEF88
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

	// Token: 0x06001286 RID: 4742 RVA: 0x000D0E44 File Offset: 0x000CF044
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

	// Token: 0x06001287 RID: 4743 RVA: 0x000D0F88 File Offset: 0x000CF188
	public void SLIDER_Angebot()
	{
		float num = this.game_.pubAngebot_VerhandlungProzent - this.uiObjects[3].GetComponent<Slider>().value;
		num *= 0.01f;
		this.garantiesummeAngebot = Mathf.RoundToInt((float)this.game_.pubAngebot_Garantiesumme * num);
		this.gewinnbeteiligungAngebot = (float)Mathf.RoundToInt(this.game_.pubAngebot_Gewinnbeteiligung * num);
		this.SetData();
	}

	// Token: 0x06001288 RID: 4744 RVA: 0x0000CC90 File Offset: 0x0000AE90
	private IEnumerator iMinus(int i)
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_Minus(i);
		}
		yield break;
	}

	// Token: 0x06001289 RID: 4745 RVA: 0x000D0FF4 File Offset: 0x000CF1F4
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

	// Token: 0x0600128A RID: 4746 RVA: 0x0000CCA6 File Offset: 0x0000AEA6
	private IEnumerator iPlus(int i)
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_Plus(i);
		}
		yield break;
	}

	// Token: 0x0600128B RID: 4747 RVA: 0x000D1074 File Offset: 0x000CF274
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

	// Token: 0x0600128C RID: 4748 RVA: 0x000D10F4 File Offset: 0x000CF2F4
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

	// Token: 0x0600128D RID: 4749 RVA: 0x0000CCBC File Offset: 0x0000AEBC
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600128E RID: 4750 RVA: 0x0000CCD7 File Offset: 0x0000AED7
	public void BUTTON_OK()
	{
		this.sfx_.PlaySound(3, true);
		this.CreateGame();
	}

	// Token: 0x0600128F RID: 4751 RVA: 0x000D12B8 File Offset: 0x000CF4B8
	private void CreateGame()
	{
		this.mS_.Pay((long)this.game_.PUBOFFER_GetGarantiesumme(), 25);
		this.mS_.publishingOfferMain_.amountPublishingOffers--;
		this.game_.playerGame = true;
		if (this.mS_.multiplayer)
		{
			this.game_.multiplayerSlot = this.mS_.mpCalls_.myID;
		}
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

	// Token: 0x040016F2 RID: 5874
	public GameObject[] uiObjects;

	// Token: 0x040016F3 RID: 5875
	private GameObject main_;

	// Token: 0x040016F4 RID: 5876
	private mainScript mS_;

	// Token: 0x040016F5 RID: 5877
	private textScript tS_;

	// Token: 0x040016F6 RID: 5878
	private GUI_Main guiMain_;

	// Token: 0x040016F7 RID: 5879
	private sfxScript sfx_;

	// Token: 0x040016F8 RID: 5880
	private genres genres_;

	// Token: 0x040016F9 RID: 5881
	private games games_;

	// Token: 0x040016FA RID: 5882
	private gameScript game_;

	// Token: 0x040016FB RID: 5883
	private int garantiesummeAngebot;

	// Token: 0x040016FC RID: 5884
	private float gewinnbeteiligungAngebot;
}
