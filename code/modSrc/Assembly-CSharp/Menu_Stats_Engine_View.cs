using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000234 RID: 564
public class Menu_Stats_Engine_View : MonoBehaviour
{
	// Token: 0x060015AB RID: 5547 RVA: 0x0000EE7A File Offset: 0x0000D07A
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060015AC RID: 5548 RVA: 0x000E5EA8 File Offset: 0x000E40A8
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
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
	}

	// Token: 0x060015AD RID: 5549 RVA: 0x0000EE82 File Offset: 0x0000D082
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x060015AE RID: 5550 RVA: 0x000E5F90 File Offset: 0x000E4190
	private void MultiplayerUpdate()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.updateTimer += Time.deltaTime;
		if (this.updateTimer < 5f)
		{
			return;
		}
		this.updateTimer = 0f;
		this.SetData();
	}

	// Token: 0x060015AF RID: 5551 RVA: 0x000E5FDC File Offset: 0x000E41DC
	public void Init(engineScript s)
	{
		this.FindScripts();
		this.eS_ = s;
		this.SetData();
		if (this.eS_.playerEngine)
		{
			this.uiObjects[8].SetActive(true);
			this.uiObjects[16].SetActive(false);
			this.uiObjects[9].GetComponent<Slider>().value = (float)(this.eS_.preis / 1000);
			this.uiObjects[10].GetComponent<Slider>().value = (float)this.eS_.gewinnbeteiligung;
			this.uiObjects[11].GetComponent<Toggle>().isOn = this.eS_.sellEngine;
			this.uiObjects[17].GetComponent<Text>().text = this.mS_.GetMoney((long)this.eS_.umsatz, true);
			this.uiObjects[18].GetComponent<Text>().text = this.eS_.GetVerkaufteLizenzen().ToString();
			return;
		}
		this.uiObjects[8].SetActive(false);
		this.uiObjects[16].SetActive(true);
	}

	// Token: 0x060015B0 RID: 5552 RVA: 0x000E60FC File Offset: 0x000E42FC
	private void SetData()
	{
		if (!this.eS_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.eS_.GetName();
		this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(4) + " " + this.eS_.GetTechLevel();
		this.uiObjects[2].GetComponent<Text>().text = this.genres_.GetName(this.eS_.spezialgenre);
		this.uiObjects[4].GetComponent<Text>().text = this.eS_.GetGamesAmount().ToString() + " " + this.tS_.GetText(271);
		this.uiObjects[5].GetComponent<Text>().text = this.eS_.GetFeaturesAmount().ToString() + " " + this.tS_.GetText(272);
		this.uiObjects[6].GetComponent<Text>().text = this.mS_.GetMoney((long)this.eS_.preis, true);
		this.uiObjects[7].GetComponent<Text>().text = this.eS_.gewinnbeteiligung.ToString() + "%";
		this.uiObjects[12].GetComponent<Image>().sprite = this.genres_.GetPic(this.eS_.spezialgenre);
		if (this.eS_.date_year > 0)
		{
			this.uiObjects[19].GetComponent<Text>().text = this.eS_.GetReleaseDateString();
		}
		else
		{
			this.uiObjects[19].GetComponent<Text>().text = "";
		}
		this.guiMain_.DrawStars(this.uiObjects[3], this.genres_.genres_LEVEL[this.eS_.spezialgenre]);
		platformScript spezialPlatformScript = this.eS_.GetSpezialPlatformScript();
		if (spezialPlatformScript)
		{
			this.uiObjects[15].GetComponent<Text>().text = spezialPlatformScript.GetName();
			spezialPlatformScript.SetPic(this.uiObjects[13]);
			this.guiMain_.DrawStars(this.uiObjects[14], spezialPlatformScript.erfahrung);
		}
	}

	// Token: 0x060015B1 RID: 5553 RVA: 0x0000EE8A File Offset: 0x0000D08A
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060015B2 RID: 5554 RVA: 0x000E6358 File Offset: 0x000E4558
	public void BUTTON_Kaufen()
	{
		this.sfx_.PlaySound(3, true);
		this.mS_.Pay((long)this.eS_.preis, 5);
		this.eS_.gekauft = true;
		this.guiMain_.uiObjects[42].GetComponent<Menu_BuyEngine>().OnEnable();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060015B3 RID: 5555 RVA: 0x000E63BC File Offset: 0x000E45BC
	public void BUTTON_ShowFeatures()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[44]);
		this.guiMain_.uiObjects[44].GetComponent<Menu_Engine_ShowFeatures>().Init(this.eS_);
	}

	// Token: 0x060015B4 RID: 5556 RVA: 0x000E6410 File Offset: 0x000E4610
	public void BUTTON_ShowGames()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[45]);
		this.guiMain_.uiObjects[45].GetComponent<Menu_Engine_ShowGames>().Init(this.eS_);
	}

	// Token: 0x060015B5 RID: 5557 RVA: 0x0000EEA5 File Offset: 0x0000D0A5
	public void SLIDER_Preis()
	{
		this.uiObjects[6].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt(this.uiObjects[9].GetComponent<Slider>().value * 1000f), true);
	}

	// Token: 0x060015B6 RID: 5558 RVA: 0x000E6464 File Offset: 0x000E4664
	public void SLIDER_Gewinnbeteiligung()
	{
		this.uiObjects[7].GetComponent<Text>().text = Mathf.RoundToInt(this.uiObjects[10].GetComponent<Slider>().value).ToString() + "%";
	}

	// Token: 0x060015B7 RID: 5559 RVA: 0x0000EEE4 File Offset: 0x0000D0E4
	public void TOGGLE_EngineVerkaufen()
	{
		this.sfx_.PlaySound(12, true);
	}

	// Token: 0x060015B8 RID: 5560 RVA: 0x0000EEF4 File Offset: 0x0000D0F4
	public void BUTTON_Preis(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.uiObjects[9].GetComponent<Slider>().value += (float)i;
	}

	// Token: 0x060015B9 RID: 5561 RVA: 0x0000EF1F File Offset: 0x0000D11F
	public void BUTTON_Gewinnbeteiligung(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.uiObjects[10].GetComponent<Slider>().value += (float)i;
	}

	// Token: 0x060015BA RID: 5562 RVA: 0x000E64B0 File Offset: 0x000E46B0
	public void BUTTON_OK()
	{
		this.sfx_.PlaySound(3, true);
		if (this.eS_.playerEngine && this.uiObjects[11].GetComponent<Toggle>().isOn && Mathf.RoundToInt(this.uiObjects[9].GetComponent<Slider>().value) <= 0 && Mathf.RoundToInt(this.uiObjects[10].GetComponent<Slider>().value) <= 0)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1174), false);
			return;
		}
		if (this.eS_.playerEngine)
		{
			this.eS_.preis = Mathf.RoundToInt(this.uiObjects[9].GetComponent<Slider>().value * 1000f);
			this.eS_.gewinnbeteiligung = Mathf.RoundToInt(this.uiObjects[10].GetComponent<Slider>().value);
			this.eS_.sellEngine = this.uiObjects[11].GetComponent<Toggle>().isOn;
			if (this.mS_.multiplayer)
			{
				if (this.mS_.mpCalls_.isServer)
				{
					this.mS_.mpCalls_.SERVER_Send_Engine(this.eS_);
				}
				if (this.mS_.mpCalls_.isClient)
				{
					this.mS_.mpCalls_.CLIENT_Send_Engine(this.eS_);
				}
			}
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x040019B0 RID: 6576
	public GameObject[] uiObjects;

	// Token: 0x040019B1 RID: 6577
	private roomScript rS_;

	// Token: 0x040019B2 RID: 6578
	private GameObject main_;

	// Token: 0x040019B3 RID: 6579
	private mainScript mS_;

	// Token: 0x040019B4 RID: 6580
	private textScript tS_;

	// Token: 0x040019B5 RID: 6581
	private GUI_Main guiMain_;

	// Token: 0x040019B6 RID: 6582
	private sfxScript sfx_;

	// Token: 0x040019B7 RID: 6583
	private genres genres_;

	// Token: 0x040019B8 RID: 6584
	private engineFeatures eF_;

	// Token: 0x040019B9 RID: 6585
	private engineScript eS_;

	// Token: 0x040019BA RID: 6586
	private float updateTimer;
}
