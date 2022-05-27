using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001F8 RID: 504
public class Menu_InAppPurchases : MonoBehaviour
{
	// Token: 0x0600131D RID: 4893 RVA: 0x0000D1AC File Offset: 0x0000B3AC
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600131E RID: 4894 RVA: 0x000D59B4 File Offset: 0x000D3BB4
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

	// Token: 0x0600131F RID: 4895 RVA: 0x0000D1B4 File Offset: 0x0000B3B4
	public void Init(gameScript game_, bool closeMenu_)
	{
		this.closeMenu = closeMenu_;
		this.FindScripts();
		this.gS_ = game_;
		this.SetData();
	}

	// Token: 0x06001320 RID: 4896 RVA: 0x000D5A9C File Offset: 0x000D3C9C
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

	// Token: 0x06001321 RID: 4897 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
	public void BUTTON_InAppPurchase(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.g_InAppPurchase[i] = !this.g_InAppPurchase[i];
		this.SetMaxVerdienstInApp();
	}

	// Token: 0x06001322 RID: 4898 RVA: 0x000D5CB0 File Offset: 0x000D3EB0
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

	// Token: 0x06001323 RID: 4899 RVA: 0x0000D1F9 File Offset: 0x0000B3F9
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		if (this.closeMenu)
		{
			this.guiMain_.CloseMenu();
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001324 RID: 4900 RVA: 0x000D5CF4 File Offset: 0x000D3EF4
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

	// Token: 0x06001325 RID: 4901 RVA: 0x000D5D78 File Offset: 0x000D3F78
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

	// Token: 0x06001326 RID: 4902 RVA: 0x000D5E78 File Offset: 0x000D4078
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

	// Token: 0x0400176F RID: 5999
	public GameObject[] uiObjects;

	// Token: 0x04001770 RID: 6000
	private mainScript mS_;

	// Token: 0x04001771 RID: 6001
	private GameObject main_;

	// Token: 0x04001772 RID: 6002
	private GUI_Main guiMain_;

	// Token: 0x04001773 RID: 6003
	private sfxScript sfx_;

	// Token: 0x04001774 RID: 6004
	private textScript tS_;

	// Token: 0x04001775 RID: 6005
	private genres genres_;

	// Token: 0x04001776 RID: 6006
	private games games_;

	// Token: 0x04001777 RID: 6007
	private gameScript gS_;

	// Token: 0x04001778 RID: 6008
	public bool[] g_InAppPurchase;

	// Token: 0x04001779 RID: 6009
	private bool closeMenu;
}
