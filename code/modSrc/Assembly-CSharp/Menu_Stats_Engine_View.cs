using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000235 RID: 565
public class Menu_Stats_Engine_View : MonoBehaviour
{
	// Token: 0x060015C9 RID: 5577 RVA: 0x000DDB66 File Offset: 0x000DBD66
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060015CA RID: 5578 RVA: 0x000DDB70 File Offset: 0x000DBD70
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

	// Token: 0x060015CB RID: 5579 RVA: 0x000DDC56 File Offset: 0x000DBE56
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x060015CC RID: 5580 RVA: 0x000DDC60 File Offset: 0x000DBE60
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

	// Token: 0x060015CD RID: 5581 RVA: 0x000DDCAC File Offset: 0x000DBEAC
	public void Init(engineScript s)
	{
		this.FindScripts();
		this.eS_ = s;
		this.SetData();
		if (this.eS_.ownerID == this.mS_.myID)
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

	// Token: 0x060015CE RID: 5582 RVA: 0x000DDDD8 File Offset: 0x000DBFD8
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

	// Token: 0x060015CF RID: 5583 RVA: 0x000DE033 File Offset: 0x000DC233
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060015D0 RID: 5584 RVA: 0x000DE050 File Offset: 0x000DC250
	public void BUTTON_Kaufen()
	{
		this.sfx_.PlaySound(3, true);
		this.mS_.Pay((long)this.eS_.preis, 5);
		this.eS_.gekauft = true;
		this.guiMain_.uiObjects[42].GetComponent<Menu_BuyEngine>().OnEnable();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060015D1 RID: 5585 RVA: 0x000DE0B4 File Offset: 0x000DC2B4
	public void BUTTON_ShowFeatures()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[44]);
		this.guiMain_.uiObjects[44].GetComponent<Menu_Engine_ShowFeatures>().Init(this.eS_);
	}

	// Token: 0x060015D2 RID: 5586 RVA: 0x000DE108 File Offset: 0x000DC308
	public void BUTTON_ShowGames()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[45]);
		this.guiMain_.uiObjects[45].GetComponent<Menu_Engine_ShowGames>().Init(this.eS_);
	}

	// Token: 0x060015D3 RID: 5587 RVA: 0x000DE159 File Offset: 0x000DC359
	public void SLIDER_Preis()
	{
		this.uiObjects[6].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt(this.uiObjects[9].GetComponent<Slider>().value * 1000f), true);
	}

	// Token: 0x060015D4 RID: 5588 RVA: 0x000DE198 File Offset: 0x000DC398
	public void SLIDER_Gewinnbeteiligung()
	{
		this.uiObjects[7].GetComponent<Text>().text = Mathf.RoundToInt(this.uiObjects[10].GetComponent<Slider>().value).ToString() + "%";
	}

	// Token: 0x060015D5 RID: 5589 RVA: 0x000DE1E1 File Offset: 0x000DC3E1
	public void TOGGLE_EngineVerkaufen()
	{
		this.sfx_.PlaySound(12, true);
	}

	// Token: 0x060015D6 RID: 5590 RVA: 0x000DE1F1 File Offset: 0x000DC3F1
	public void BUTTON_Preis(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.uiObjects[9].GetComponent<Slider>().value += (float)i;
	}

	// Token: 0x060015D7 RID: 5591 RVA: 0x000DE21C File Offset: 0x000DC41C
	public void BUTTON_Gewinnbeteiligung(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.uiObjects[10].GetComponent<Slider>().value += (float)i;
	}

	// Token: 0x060015D8 RID: 5592 RVA: 0x000DE248 File Offset: 0x000DC448
	public void BUTTON_OK()
	{
		this.sfx_.PlaySound(3, true);
		if (this.eS_.ownerID == this.mS_.myID && this.uiObjects[11].GetComponent<Toggle>().isOn && Mathf.RoundToInt(this.uiObjects[9].GetComponent<Slider>().value) <= 0 && Mathf.RoundToInt(this.uiObjects[10].GetComponent<Slider>().value) <= 0)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1174), false);
			return;
		}
		if (this.eS_.ownerID == this.mS_.myID)
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

	// Token: 0x040019B9 RID: 6585
	public GameObject[] uiObjects;

	// Token: 0x040019BA RID: 6586
	private roomScript rS_;

	// Token: 0x040019BB RID: 6587
	private GameObject main_;

	// Token: 0x040019BC RID: 6588
	private mainScript mS_;

	// Token: 0x040019BD RID: 6589
	private textScript tS_;

	// Token: 0x040019BE RID: 6590
	private GUI_Main guiMain_;

	// Token: 0x040019BF RID: 6591
	private sfxScript sfx_;

	// Token: 0x040019C0 RID: 6592
	private genres genres_;

	// Token: 0x040019C1 RID: 6593
	private engineFeatures eF_;

	// Token: 0x040019C2 RID: 6594
	private engineScript eS_;

	// Token: 0x040019C3 RID: 6595
	private float updateTimer;
}
