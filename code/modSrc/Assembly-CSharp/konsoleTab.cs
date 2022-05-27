using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000106 RID: 262
public class konsoleTab : MonoBehaviour
{
	// Token: 0x06000869 RID: 2153 RVA: 0x0000644B File Offset: 0x0000464B
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600086A RID: 2154 RVA: 0x0006E134 File Offset: 0x0006C334
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
		if (!this.themes_)
		{
			this.themes_ = this.main_.GetComponent<themes>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
		if (!this.tooltip_)
		{
			this.tooltip_ = base.GetComponent<tooltip>();
		}
		if (!this.rect)
		{
			this.rect = base.GetComponent<RectTransform>();
		}
		if (!this.myImage)
		{
			this.myImage = base.GetComponent<Image>();
		}
		if (!this.myButton)
		{
			this.myButton = base.GetComponent<Button>();
		}
		if (!this.settings_)
		{
			this.settings_ = this.main_.GetComponent<settingsScript>();
		}
		if (!this.licences_)
		{
			this.licences_ = this.main_.GetComponent<licences>();
		}
	}

	// Token: 0x0600086B RID: 2155 RVA: 0x00006453 File Offset: 0x00004653
	public void Init(int platID_)
	{
		this.platformID = platID_;
		this.UpdateData();
		this.BUTTON_Minimize();
		this.BUTTON_Minimize();
		base.gameObject.transform.SetSiblingIndex(0);
	}

	// Token: 0x0600086C RID: 2156 RVA: 0x0006E2D8 File Offset: 0x0006C4D8
	private void FindMyPlatform()
	{
		if (this.platformID == -1)
		{
			return;
		}
		if (!this.pS_)
		{
			GameObject gameObject = GameObject.Find("PLATFORM_" + this.platformID.ToString());
			if (gameObject)
			{
				this.pS_ = gameObject.GetComponent<platformScript>();
			}
		}
	}

	// Token: 0x0600086D RID: 2157 RVA: 0x0006E32C File Offset: 0x0006C52C
	private void Update()
	{
		if (this.guiMain_.menuOpen)
		{
			if (this.myButton.interactable)
			{
				this.myButton.interactable = false;
			}
		}
		else if (!this.myButton.interactable)
		{
			this.myButton.interactable = true;
		}
		if (!this.pS_)
		{
			return;
		}
		if (!this.fullView)
		{
			if (this.settings_.gameTabData)
			{
				if (Mathf.RoundToInt(this.rect.sizeDelta.y) != 21)
				{
					this.rect.sizeDelta = new Vector2(220f, 21f);
				}
			}
			else if (Mathf.RoundToInt(this.rect.sizeDelta.y) != 34)
			{
				this.rect.sizeDelta = new Vector2(220f, 34f);
			}
		}
		if (this.fullView || this.settings_.gameTabData)
		{
			this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
			long gesamtGewinn = this.pS_.GetGesamtGewinn();
			if (gesamtGewinn < 0L)
			{
				this.uiObjects[3].GetComponent<Text>().color = this.guiMain_.colors[5];
				this.uiObjects[3].GetComponent<Text>().text = this.mS_.GetMoney(gesamtGewinn, true);
			}
			else
			{
				this.uiObjects[3].GetComponent<Text>().color = this.guiMain_.colors[6];
				this.uiObjects[3].GetComponent<Text>().text = this.mS_.GetMoney(gesamtGewinn, true);
			}
		}
		else
		{
			string text = this.pS_.GetName() + "\n<color=#2980B9>" + this.mS_.GetMoney((long)Mathf.RoundToInt(this.sellsPerWeek), false) + "</color><color=black>|</color>";
			long gesamtGewinn2 = this.pS_.GetGesamtGewinn();
			if (gesamtGewinn2 >= 0L)
			{
				text = text + "<color=green>" + this.mS_.GetMoney(gesamtGewinn2, true) + "</color>";
			}
			else
			{
				text = text + "<color=red>" + this.mS_.GetMoney(gesamtGewinn2, true) + "</color>";
			}
			this.uiObjects[0].GetComponent<Text>().text = text;
		}
		this.uiBalken[0].GetComponent<Image>().fillAmount = Mathf.Lerp(this.uiBalken[0].GetComponent<Image>().fillAmount, this.fillBalken, 0.08f);
		this.sellsPerWeek = Mathf.Lerp(this.sellsPerWeek, (float)this.pS_.sellsPerWeek[0], 0.08f);
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt(this.sellsPerWeek), false);
		this.sellsTotal = Mathf.Lerp(this.sellsTotal, (float)this.pS_.units, 0.08f);
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt(this.sellsTotal), false);
		this.uiObjects[5].GetComponent<Text>().text = Mathf.RoundToInt(this.pS_.GetHype()).ToString();
		this.uiObjects[10].GetComponent<Text>().text = this.pS_.tech.ToString();
		this.uiObjects[11].GetComponent<Text>().text = this.pS_.GetGames().ToString();
		this.uiObjects[12].GetComponent<Text>().text = this.mS_.Round(this.pS_.marktanteil, 1).ToString() + "%";
		if (this.pS_.IsOutdatet())
		{
			if (!this.uiObjects[13].activeSelf)
			{
				this.uiObjects[13].SetActive(true);
			}
		}
		else if (this.uiObjects[13].activeSelf)
		{
			this.uiObjects[13].SetActive(false);
		}
		if (this.fullView)
		{
			float f = 210f;
			if (Mathf.RoundToInt(this.rect.sizeDelta.y) != Mathf.RoundToInt(f))
			{
				this.rect.sizeDelta = new Vector2(220f, (float)Mathf.RoundToInt(f));
			}
		}
		if (this.guiMain_ && this.guiMain_.guiTooltip.tooltipEnabled)
		{
			if (this.tooltipTimer <= 0f)
			{
				this.tooltipTimer = 1f;
				this.tooltip_.c = this.pS_.GetTooltip();
				return;
			}
			this.tooltipTimer -= Time.deltaTime;
		}
	}

	// Token: 0x0600086E RID: 2158 RVA: 0x0006E7E4 File Offset: 0x0006C9E4
	public void UpdateData()
	{
		this.FindScripts();
		if (!this.pS_)
		{
			this.FindMyPlatform();
			if (!this.pS_)
			{
				return;
			}
		}
		this.pS_.SetPic(this.uiObjects[9]);
		this.uiObjects[6].GetComponent<Image>().sprite = this.pS_.GetTypSprite();
		this.uiObjects[4].GetComponent<Text>().text = this.pS_.weeksOnMarket.ToString();
		float num = 0f;
		for (int i = 0; i < this.uiBalken.Length; i++)
		{
			if ((float)this.pS_.sellsPerWeek[i] > num)
			{
				num = (float)this.pS_.sellsPerWeek[i];
			}
		}
		for (int j = 1; j < this.uiBalken.Length; j++)
		{
			float num2 = (float)this.pS_.sellsPerWeek[j];
			if (num2 > 0f)
			{
				this.uiBalken[j].GetComponent<Image>().fillAmount = num2 / num;
			}
			else
			{
				this.uiBalken[j].GetComponent<Image>().fillAmount = 0f;
			}
		}
		this.sellsPerWeek = 0f;
		this.uiBalken[0].GetComponent<Image>().fillAmount = 0f;
		float num3 = (float)this.pS_.sellsPerWeek[0];
		if (num3 > 0f)
		{
			this.fillBalken = num3 / num;
			return;
		}
		this.fillBalken = 0f;
	}

	// Token: 0x0600086F RID: 2159 RVA: 0x0006E950 File Offset: 0x0006CB50
	public void BUTTON_Minimize()
	{
		this.sfx_.PlaySound(3, true);
		this.fullView = !this.fullView;
		this.uiObjects[7].SetActive(this.fullView);
		if (this.fullView)
		{
			this.uiObjects[8].GetComponent<Image>().sprite = this.uiSprites[0];
			this.UpdateData();
			return;
		}
		this.uiObjects[8].GetComponent<Image>().sprite = this.uiSprites[1];
	}

	// Token: 0x06000870 RID: 2160 RVA: 0x0006E9D0 File Offset: 0x0006CBD0
	public void BUTTON_Click()
	{
		if (!this.pS_)
		{
			return;
		}
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[333]);
		this.guiMain_.uiObjects[333].GetComponent<Menu_Umsatz_Konsole>().Init(this.pS_);
		this.guiMain_.OpenMenu(false);
	}

	// Token: 0x04000CDC RID: 3292
	public mainScript mS_;

	// Token: 0x04000CDD RID: 3293
	public GameObject main_;

	// Token: 0x04000CDE RID: 3294
	public GUI_Main guiMain_;

	// Token: 0x04000CDF RID: 3295
	public sfxScript sfx_;

	// Token: 0x04000CE0 RID: 3296
	public textScript tS_;

	// Token: 0x04000CE1 RID: 3297
	public themes themes_;

	// Token: 0x04000CE2 RID: 3298
	public genres genres_;

	// Token: 0x04000CE3 RID: 3299
	public tooltip tooltip_;

	// Token: 0x04000CE4 RID: 3300
	public games games_;

	// Token: 0x04000CE5 RID: 3301
	public settingsScript settings_;

	// Token: 0x04000CE6 RID: 3302
	public licences licences_;

	// Token: 0x04000CE7 RID: 3303
	public int platformID = -1;

	// Token: 0x04000CE8 RID: 3304
	public platformScript pS_;

	// Token: 0x04000CE9 RID: 3305
	public GameObject[] uiPrefabs;

	// Token: 0x04000CEA RID: 3306
	public GameObject[] uiObjects;

	// Token: 0x04000CEB RID: 3307
	public GameObject[] uiBalken;

	// Token: 0x04000CEC RID: 3308
	public Sprite[] uiSprites;

	// Token: 0x04000CED RID: 3309
	private float fillBalken;

	// Token: 0x04000CEE RID: 3310
	private float sellsPerWeek;

	// Token: 0x04000CEF RID: 3311
	private float sellsTotal;

	// Token: 0x04000CF0 RID: 3312
	public bool fullView = true;

	// Token: 0x04000CF1 RID: 3313
	private RectTransform rect;

	// Token: 0x04000CF2 RID: 3314
	private Image myImage;

	// Token: 0x04000CF3 RID: 3315
	private Button myButton;

	// Token: 0x04000CF4 RID: 3316
	private float tooltipTimer;
}
