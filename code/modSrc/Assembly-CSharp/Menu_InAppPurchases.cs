using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001F9 RID: 505
public class Menu_InAppPurchases : MonoBehaviour
{
	// Token: 0x06001338 RID: 4920 RVA: 0x000CB3F7 File Offset: 0x000C95F7
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001339 RID: 4921 RVA: 0x000CB400 File Offset: 0x000C9600
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
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

	// Token: 0x0600133A RID: 4922 RVA: 0x000CB4E6 File Offset: 0x000C96E6
	public void Init(gameScript game_, bool closeMenu_)
	{
		this.closeMenu = closeMenu_;
		this.FindScripts();
		this.gS_ = game_;
		this.SetData();
	}

	// Token: 0x0600133B RID: 4923 RVA: 0x000CB504 File Offset: 0x000C9704
	private void SetData()
	{
		for (int i = 0; i < this.gS_.inAppPurchase.Length; i++)
		{
			this.g_InAppPurchase[i] = this.gS_.inAppPurchase[i];
		}
		this.SetMaxVerdienstInApp();
		this.uiObjects[0].GetComponent<Text>().text = this.gS_.GetNameWithTag();
		this.uiObjects[1].GetComponent<Text>().text = "$" + this.mS_.Round(this.games_.inAppPurchasePrice[0], 1).ToString() + "0";
		this.uiObjects[2].GetComponent<Text>().text = "$" + this.mS_.Round(this.games_.inAppPurchasePrice[1], 1).ToString() + "0";
		this.uiObjects[3].GetComponent<Text>().text = "$" + this.mS_.Round(this.games_.inAppPurchasePrice[2], 1).ToString() + "0";
		this.uiObjects[4].GetComponent<Text>().text = "$" + this.mS_.Round(this.games_.inAppPurchasePrice[3], 1).ToString() + "0";
		this.uiObjects[5].GetComponent<Text>().text = "$" + this.mS_.Round(this.games_.inAppPurchasePrice[4], 1).ToString() + "0";
		this.uiObjects[6].GetComponent<Text>().text = "$" + this.mS_.Round(this.games_.inAppPurchasePrice[5], 1).ToString() + "0";
		this.uiObjects[8].GetComponent<Text>().text = this.mS_.GetMoney(this.gS_.umsatzInApp, true);
	}

	// Token: 0x0600133C RID: 4924 RVA: 0x000CB717 File Offset: 0x000C9917
	public void BUTTON_InAppPurchase(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.g_InAppPurchase[i] = !this.g_InAppPurchase[i];
		this.SetMaxVerdienstInApp();
	}

	// Token: 0x0600133D RID: 4925 RVA: 0x000CB740 File Offset: 0x000C9940
	public void BUTTON_AlleInAppPurchase()
	{
		this.sfx_.PlaySound(3, true);
		bool flag = this.g_InAppPurchase[0];
		for (int i = 0; i < 6; i++)
		{
			this.g_InAppPurchase[i] = !flag;
		}
		this.SetMaxVerdienstInApp();
	}

	// Token: 0x0600133E RID: 4926 RVA: 0x000CB782 File Offset: 0x000C9982
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		if (this.closeMenu)
		{
			this.guiMain_.CloseMenu();
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600133F RID: 4927 RVA: 0x000CB7B0 File Offset: 0x000C99B0
	public void BUTTON_Yes()
	{
		for (int i = 0; i < this.gS_.inAppPurchase.Length; i++)
		{
			this.gS_.inAppPurchase[i] = this.g_InAppPurchase[i];
		}
		this.guiMain_.uiObjects[277].GetComponent<Menu_InAppVerwalten>().Init();
		this.sfx_.PlaySound(3, true);
		if (this.closeMenu)
		{
			this.guiMain_.CloseMenu();
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001340 RID: 4928 RVA: 0x000CB834 File Offset: 0x000C9A34
	private float SetMaxVerdienstInApp()
	{
		float num = 0f;
		float num2 = 0f;
		for (int i = 0; i < 6; i++)
		{
			if (this.g_InAppPurchase[i])
			{
				num += this.games_.inAppPurchasePrice[i];
				num2 += this.games_.inAppPurchaseHate[i];
				this.uiObjects[10 + i].GetComponent<Image>().color = this.guiMain_.colors[20];
			}
			else
			{
				this.uiObjects[10 + i].GetComponent<Image>().color = Color.white;
			}
		}
		this.uiObjects[7].GetComponent<Text>().text = "$" + this.mS_.Round(num, 2).ToString();
		this.uiObjects[9].GetComponent<Image>().fillAmount = num2 * 0.01f;
		this.uiObjects[9].GetComponent<Image>().color = this.GetValColor(100f - num2);
		return num;
	}

	// Token: 0x06001341 RID: 4929 RVA: 0x000CB934 File Offset: 0x000C9B34
	private Color GetValColor(float val)
	{
		if (val < 30f)
		{
			return this.guiMain_.colorsBalken[0];
		}
		if (val >= 30f && val < 70f)
		{
			return this.guiMain_.colorsBalken[1];
		}
		if (val >= 70f)
		{
			return this.guiMain_.colorsBalken[2];
		}
		return this.guiMain_.colorsBalken[0];
	}

	// Token: 0x04001778 RID: 6008
	public GameObject[] uiObjects;

	// Token: 0x04001779 RID: 6009
	private mainScript mS_;

	// Token: 0x0400177A RID: 6010
	private GameObject main_;

	// Token: 0x0400177B RID: 6011
	private GUI_Main guiMain_;

	// Token: 0x0400177C RID: 6012
	private sfxScript sfx_;

	// Token: 0x0400177D RID: 6013
	private textScript tS_;

	// Token: 0x0400177E RID: 6014
	private genres genres_;

	// Token: 0x0400177F RID: 6015
	private games games_;

	// Token: 0x04001780 RID: 6016
	private gameScript gS_;

	// Token: 0x04001781 RID: 6017
	public bool[] g_InAppPurchase;

	// Token: 0x04001782 RID: 6018
	private bool closeMenu;
}
