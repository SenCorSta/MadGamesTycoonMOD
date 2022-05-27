﻿using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000098 RID: 152
public class Item_PubOfferSelect : MonoBehaviour
{
	// Token: 0x060005DE RID: 1502 RVA: 0x0004BFF0 File Offset: 0x0004A1F0
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060005DF RID: 1503 RVA: 0x0004BFF8 File Offset: 0x0004A1F8
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

	// Token: 0x060005E0 RID: 1504 RVA: 0x0004C1D8 File Offset: 0x0004A3D8
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

	// Token: 0x060005E1 RID: 1505 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060005E2 RID: 1506 RVA: 0x0004C398 File Offset: 0x0004A598
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

	// Token: 0x060005E3 RID: 1507 RVA: 0x0004C418 File Offset: 0x0004A618
	public void BUTTON_Delete()
	{
		this.sfx_.PlaySound(3, true);
		this.game_.pubAnbgebot_Inivs = true;
		this.mS_.publishingOfferMain_.amountPublishingOffers--;
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0400091A RID: 2330
	public gameScript game_;

	// Token: 0x0400091B RID: 2331
	public GameObject[] uiObjects;

	// Token: 0x0400091C RID: 2332
	public Sprite[] iconStimmung;

	// Token: 0x0400091D RID: 2333
	public mainScript mS_;

	// Token: 0x0400091E RID: 2334
	public textScript tS_;

	// Token: 0x0400091F RID: 2335
	public sfxScript sfx_;

	// Token: 0x04000920 RID: 2336
	public GUI_Main guiMain_;

	// Token: 0x04000921 RID: 2337
	public tooltip tooltip_;

	// Token: 0x04000922 RID: 2338
	public genres genres_;

	// Token: 0x04000923 RID: 2339
	public games games_;

	// Token: 0x04000924 RID: 2340
	private float stimmungOLD;
}
