using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_InAppPurchases : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
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

	
	public void Init(gameScript game_, bool closeMenu_)
	{
		this.closeMenu = closeMenu_;
		this.FindScripts();
		this.gS_ = game_;
		this.SetData();
	}

	
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

	
	public void BUTTON_InAppPurchase(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.g_InAppPurchase[i] = !this.g_InAppPurchase[i];
		this.SetMaxVerdienstInApp();
	}

	
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

	
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		if (this.closeMenu)
		{
			this.guiMain_.CloseMenu();
		}
		base.gameObject.SetActive(false);
	}

	
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

	
	public GameObject[] uiObjects;

	
	private mainScript mS_;

	
	private GameObject main_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private textScript tS_;

	
	private genres genres_;

	
	private games games_;

	
	private gameScript gS_;

	
	public bool[] g_InAppPurchase;

	
	private bool closeMenu;
}
