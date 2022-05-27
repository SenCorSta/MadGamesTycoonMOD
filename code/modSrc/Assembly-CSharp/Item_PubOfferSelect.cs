using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_PubOfferSelect : MonoBehaviour
{
	
	private void Start()
	{
		this.SetData();
	}

	
	private void Update()
	{
		if (!this.game_)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		if (this.game_.isOnMarket || !this.game_.pubAngebot)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		if (this.game_.pubAngebot_Stimmung <= 0f || this.game_.pubAnbgebot_Inivs)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		this.uiObjects[5].GetComponent<Text>().text = this.tS_.GetText(1730) + ": <color=red>" + this.mS_.GetMoney((long)this.game_.PUBOFFER_GetGarantiesumme(), true) + "</color>";
		this.uiObjects[6].GetComponent<Text>().text = this.tS_.GetText(1731) + ": <color=red>" + Mathf.RoundToInt((float)this.game_.PUBOFFER_GetGewinnbeteiligung()).ToString() + "%</color>";
		if (this.stimmungOLD != this.game_.pubAngebot_Stimmung)
		{
			this.stimmungOLD = this.game_.pubAngebot_Stimmung;
			if (this.game_.pubAngebot_Stimmung < 33f)
			{
				this.uiObjects[9].GetComponent<Image>().sprite = this.iconStimmung[2];
			}
			if (this.game_.pubAngebot_Stimmung > 33f && this.game_.pubAngebot_Stimmung < 66f)
			{
				this.uiObjects[9].GetComponent<Image>().sprite = this.iconStimmung[1];
			}
			if (this.game_.pubAngebot_Stimmung > 66f)
			{
				this.uiObjects[9].GetComponent<Image>().sprite = this.iconStimmung[0];
			}
			this.tooltip_.c = this.game_.GetTooltip();
		}
	}

	
	private void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		this.uiObjects[2].GetComponent<Image>().sprite = this.genres_.GetPic(this.game_.maingenre);
		this.uiObjects[3].GetComponent<Image>().sprite = this.games_.gameSizeSprites[this.game_.gameSize];
		this.uiObjects[4].GetComponent<Image>().sprite = this.game_.GetScreenshot();
		this.uiObjects[10].GetComponent<Text>().text = this.game_.PUBOFFER_GetRetailDigitalString();
		this.uiObjects[15].GetComponent<Text>().text = this.mS_.Round(this.game_.GetIpBekanntheit(), 1).ToString();
		this.uiObjects[7].GetComponent<Image>().sprite = this.game_.GetDeveloperLogo();
		this.game_.FindMyPlatforms();
		for (int i = 0; i < this.game_.gamePlatform.Length; i++)
		{
			platformScript platformScript = this.game_.gamePlatformScript[i];
			if (platformScript)
			{
				platformScript.SetPic(this.uiObjects[11 + i]);
			}
			else
			{
				this.uiObjects[11 + i].GetComponent<Image>().sprite = this.guiMain_.uiSprites[19];
			}
		}
		this.guiMain_.DrawStars(this.uiObjects[8], Mathf.RoundToInt((float)(this.game_.reviewTotal / 20)));
		this.tooltip_.c = this.game_.GetTooltip();
	}

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public void BUTTON_Click()
	{
		if (!this.game_)
		{
			return;
		}
		if (this.game_.isOnMarket || !this.game_.pubAngebot)
		{
			return;
		}
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[350]);
		this.guiMain_.uiObjects[350].GetComponent<MenuPublishingOfferVerhandlung>().Init(this.game_);
	}

	
	public void BUTTON_Delete()
	{
		this.sfx_.PlaySound(3, true);
		this.game_.pubAnbgebot_Inivs = true;
		this.mS_.publishingOfferMain_.amountPublishingOffers--;
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public gameScript game_;

	
	public GameObject[] uiObjects;

	
	public Sprite[] iconStimmung;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public GUI_Main guiMain_;

	
	public tooltip tooltip_;

	
	public genres genres_;

	
	public games games_;

	
	private float stimmungOLD;
}
