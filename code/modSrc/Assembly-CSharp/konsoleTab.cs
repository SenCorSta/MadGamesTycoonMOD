using System;
using UnityEngine;
using UnityEngine.UI;


public class konsoleTab : MonoBehaviour
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

	
	public void Init(int platID_)
	{
		this.platformID = platID_;
		this.UpdateData();
		this.BUTTON_Minimize();
		this.BUTTON_Minimize();
		base.gameObject.transform.SetSiblingIndex(0);
	}

	
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

	
	public mainScript mS_;

	
	public GameObject main_;

	
	public GUI_Main guiMain_;

	
	public sfxScript sfx_;

	
	public textScript tS_;

	
	public themes themes_;

	
	public genres genres_;

	
	public tooltip tooltip_;

	
	public games games_;

	
	public settingsScript settings_;

	
	public licences licences_;

	
	public int platformID = -1;

	
	public platformScript pS_;

	
	public GameObject[] uiPrefabs;

	
	public GameObject[] uiObjects;

	
	public GameObject[] uiBalken;

	
	public Sprite[] uiSprites;

	
	private float fillBalken;

	
	private float sellsPerWeek;

	
	private float sellsTotal;

	
	public bool fullView = true;

	
	private RectTransform rect;

	
	private Image myImage;

	
	private Button myButton;

	
	private float tooltipTimer;
}
