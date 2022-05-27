using System;
using UnityEngine;
using UnityEngine.UI;


public class gameTab : MonoBehaviour
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

	
	public void Init(int gameID_)
	{
		this.gameID = gameID_;
		this.UpdateData();
		this.BUTTON_Minimize();
		this.BUTTON_Minimize();
	}

	
	private void FindMyGame()
	{
		if (this.gameID == -1)
		{
			return;
		}
		if (!this.gS_)
		{
			GameObject gameObject = GameObject.Find("GAME_" + this.gameID.ToString());
			if (gameObject)
			{
				this.gS_ = gameObject.GetComponent<gameScript>();
				return;
			}
			UnityEngine.Object.Destroy(base.gameObject);
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
		if (!this.gS_)
		{
			this.FindMyGame();
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
			this.uiObjects[0].GetComponent<Text>().text = this.gS_.GetNameWithTag();
			long gesamtGewinn = this.gS_.GetGesamtGewinn();
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
			if (this.gS_.points_bugs <= 0f)
			{
				if (this.uiObjects[42].activeSelf)
				{
					this.uiObjects[42].SetActive(false);
				}
			}
			else if (!this.uiObjects[42].activeSelf)
			{
				this.uiObjects[42].SetActive(true);
			}
			if ((!this.gS_.commercialFlop || this.gS_.weeksOnMarket < 4) && this.uiObjects[41].activeSelf)
			{
				this.uiObjects[41].SetActive(false);
			}
			if (this.gS_.commercialFlop && this.gS_.weeksOnMarket >= 4 && !this.uiObjects[41].activeSelf)
			{
				this.uiObjects[41].SetActive(true);
			}
			if ((!this.gS_.commercialHit || this.gS_.weeksOnMarket < 4) && this.uiObjects[43].activeSelf)
			{
				this.uiObjects[43].SetActive(false);
			}
			if (this.gS_.commercialHit && this.gS_.weeksOnMarket >= 4 && !this.uiObjects[43].activeSelf)
			{
				this.uiObjects[43].SetActive(true);
			}
		}
		else if (!this.gS_.schublade)
		{
			string text = "";
			text = string.Concat(new string[]
			{
				text,
				this.gS_.GetNameWithTag(),
				"\n<color=#2980B9>",
				this.mS_.GetMoney((long)this.sellsPerWeek, false),
				"</color><color=black>|</color>"
			});
			if (this.gS_.publisherID == this.mS_.myID && this.gS_.gameTyp != 2 && this.gS_.retailVersion)
			{
				text = text + "<color=magenta>" + this.gS_.GetLagerbestandString() + "</color><color=black>|</color>";
			}
			if (this.gS_.GetGesamtGewinn() >= 0L)
			{
				text = text + "<color=green>" + this.mS_.GetMoney(this.gS_.GetGesamtGewinn(), true) + "</color>";
			}
			else
			{
				text = text + "<color=red>" + this.mS_.GetMoney(this.gS_.GetGesamtGewinn(), true) + "</color>";
			}
			text = string.Concat(new object[]
			{
				text,
				"|<color=#741616>",
				Mathf.RoundToInt(this.gS_.GetHype()),
				"</color>"
			});
			this.uiObjects[0].GetComponent<Text>().text = text;
		}
		else
		{
			string text2 = this.gS_.GetNameWithTag() + "\n<color=#2980B9>" + this.tS_.GetText(1704) + "</color>";
			this.uiObjects[0].GetComponent<Text>().text = text2;
		}
		this.uiBalken[0].GetComponent<Image>().fillAmount = Mathf.Lerp(this.uiBalken[0].GetComponent<Image>().fillAmount, this.fillBalken, 0.08f);
		if (!this.gS_.schublade)
		{
			this.sellsPerWeek = Mathf.Lerp(this.sellsPerWeek, (float)this.gS_.sellsPerWeek[0], 0.08f);
			this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)this.sellsPerWeek, false);
			this.sellsTotal = Mathf.Lerp(this.sellsTotal, (float)this.gS_.sellsTotal, 0.08f);
			this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.sellsTotal, false);
			if (this.uiObjects[39].activeSelf)
			{
				this.uiObjects[39].SetActive(false);
			}
		}
		else
		{
			if (!this.uiObjects[39].activeSelf)
			{
				this.uiObjects[39].SetActive(true);
			}
			this.myImage.color = this.guiMain_.colors[17];
		}
		this.uiObjects[5].GetComponent<Text>().text = Mathf.RoundToInt(this.gS_.GetHype()).ToString();
		if (!this.gS_.typ_bundle)
		{
			this.uiObjects[10].GetComponent<Text>().text = this.mS_.Round(this.gS_.GetIpBekanntheit(), 1).ToString();
		}
		else
		{
			this.uiObjects[10].GetComponent<Text>().text = "-";
		}
		this.uiObjects[23].GetComponent<Text>().text = this.mS_.GetMoney((long)this.gS_.userPositiv, false);
		this.uiObjects[24].GetComponent<Text>().text = this.mS_.GetMoney((long)this.gS_.userNegativ, false);
		if (this.gS_.gameTyp == 1 && !this.gS_.schublade)
		{
			if (this.gS_.abonnementsWoche >= 0)
			{
				this.uiObjects[22].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt((float)this.gS_.abonnements), false) + " (+" + this.mS_.GetMoney((long)Mathf.RoundToInt((float)this.gS_.abonnementsWoche), false) + ")";
			}
			else
			{
				this.uiObjects[22].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt((float)this.gS_.abonnements), false) + " <color=red>(" + this.mS_.GetMoney((long)Mathf.RoundToInt((float)this.gS_.abonnementsWoche), false) + ")</color>";
			}
			this.uiObjects[31].GetComponent<Image>().fillAmount = this.gS_.mmoInteresse * 0.01f;
			this.uiObjects[32].GetComponent<Image>().fillAmount = this.gS_.GetMMOAbnutzung() * 0.01f;
		}
		if (this.gS_.gameTyp == 2 && !this.gS_.schublade)
		{
			if (this.gS_.abonnementsWoche >= 0)
			{
				this.uiObjects[28].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt((float)this.gS_.abonnements), false) + " (+" + this.mS_.GetMoney((long)Mathf.RoundToInt((float)this.gS_.abonnementsWoche), false) + ")";
			}
			else
			{
				this.uiObjects[28].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt((float)this.gS_.abonnements), false) + " <color=red>(" + this.mS_.GetMoney((long)Mathf.RoundToInt((float)this.gS_.abonnementsWoche), false) + ")</color>";
			}
			this.uiObjects[29].GetComponent<Image>().fillAmount = this.gS_.f2pInteresse * 0.01f;
			this.uiObjects[30].GetComponent<Image>().fillAmount = this.gS_.GetF2PAbnutzung() * 0.01f;
		}
		int chartsWeekPosition = this.games_.GetChartsWeekPosition(this.gS_.myID);
		if (chartsWeekPosition != -1)
		{
			this.uiObjects[19].GetComponent<Text>().text = chartsWeekPosition.ToString();
		}
		else
		{
			this.uiObjects[19].GetComponent<Text>().text = "-";
		}
		if (this.fullView && !this.gS_.schublade)
		{
			if (this.gS_.HasInAppPurchases())
			{
				this.uiObjects[26].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt((float)this.gS_.inAppPurchaseWeek * this.gS_.GetInAppPurchaseMoneyPerWeek()), true);
				if (!this.uiObjects[25].activeSelf)
				{
					this.uiObjects[25].SetActive(true);
				}
			}
			else if (this.uiObjects[25].activeSelf)
			{
				this.uiObjects[25].SetActive(false);
			}
		}
		if (this.gS_.arcade && !this.gS_.schublade)
		{
			this.uiObjects[34].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt((float)this.gS_.vorbestellungen), false);
			this.uiObjects[38].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt((float)this.gS_.stornierungen), false);
			if (!this.uiObjects[33].activeSelf)
			{
				this.uiObjects[33].SetActive(true);
			}
		}
		else if (this.uiObjects[33].activeSelf)
		{
			this.uiObjects[33].SetActive(false);
		}
		if (this.fullView && this.gS_.publisherID != this.mS_.myID && !this.gS_.schublade)
		{
			if (this.uiObjects[11].activeSelf)
			{
				this.uiObjects[11].SetActive(false);
			}
			if (!this.uiObjects[12].activeSelf)
			{
				this.uiObjects[12].SetActive(true);
			}
			if (this.uiObjects[13].activeSelf)
			{
				this.uiObjects[13].SetActive(false);
			}
			if (this.uiObjects[27].activeSelf)
			{
				this.uiObjects[27].SetActive(false);
			}
			if (this.gS_.gameTyp == 1)
			{
				if (!this.uiObjects[21].activeSelf)
				{
					this.uiObjects[21].SetActive(true);
				}
			}
			else if (this.uiObjects[21].activeSelf)
			{
				this.uiObjects[21].SetActive(false);
			}
		}
		if (this.fullView && this.gS_.publisherID == this.mS_.myID && !this.gS_.schublade)
		{
			if (this.gS_.gameTyp != 2 && !this.gS_.handy && !this.gS_.arcade)
			{
				if (!this.uiObjects[11].activeSelf)
				{
					this.uiObjects[11].SetActive(true);
				}
				if (this.gS_.retailVersion)
				{
					this.uiObjects[15].GetComponent<Text>().text = this.mS_.GetMoney((long)this.gS_.GetVorbestellungen(), false);
					this.uiObjects[16].GetComponent<Text>().text = this.gS_.GetLagerbestandString();
					if (this.uiObjects[17].activeSelf)
					{
						this.uiObjects[17].SetActive(false);
					}
					if (!this.uiObjects[18].activeSelf)
					{
						this.uiObjects[18].SetActive(true);
					}
				}
				else
				{
					if (this.uiObjects[18].activeSelf)
					{
						this.uiObjects[18].SetActive(false);
					}
					if (!this.uiObjects[17].activeSelf)
					{
						this.uiObjects[17].SetActive(true);
					}
				}
			}
			else if (this.uiObjects[11].activeSelf)
			{
				this.uiObjects[11].SetActive(false);
			}
			if (this.gS_.releaseDate > 0)
			{
				if (this.uiObjects[12].activeSelf)
				{
					this.uiObjects[12].SetActive(false);
				}
				if (!this.uiObjects[13].activeSelf)
				{
					this.uiObjects[13].SetActive(true);
				}
				if (this.uiObjects[21].activeSelf)
				{
					this.uiObjects[21].SetActive(false);
				}
				if (this.uiObjects[27].activeSelf)
				{
					this.uiObjects[27].SetActive(false);
				}
				if (this.gS_.releaseDate > 1)
				{
					string text3 = this.tS_.GetText(1123);
					text3 = text3.Replace("<NUM>", this.gS_.releaseDate.ToString());
					this.uiObjects[14].GetComponent<Text>().text = text3;
				}
				else
				{
					this.uiObjects[14].GetComponent<Text>().text = this.tS_.GetText(1864);
				}
			}
			else
			{
				if (!this.uiObjects[12].activeSelf)
				{
					this.uiObjects[12].SetActive(true);
				}
				if (this.uiObjects[13].activeSelf)
				{
					this.uiObjects[13].SetActive(false);
				}
				if (this.gS_.gameTyp == 1)
				{
					if (!this.uiObjects[21].activeSelf)
					{
						this.uiObjects[21].SetActive(true);
					}
				}
				else if (this.uiObjects[21].activeSelf)
				{
					this.uiObjects[21].SetActive(false);
				}
				if (this.gS_.gameTyp == 2)
				{
					if (!this.uiObjects[27].activeSelf)
					{
						this.uiObjects[27].SetActive(true);
					}
				}
				else if (this.uiObjects[27].activeSelf)
				{
					this.uiObjects[27].SetActive(false);
				}
			}
		}
		if (this.gS_.publisherID == this.mS_.myID && !this.gS_.schublade)
		{
			if (this.gS_.retailVersion)
			{
				if (this.gS_.lagerbestand[0] <= 0 && !this.gS_.arcade)
				{
					this.myImage.color = Color.Lerp(this.guiMain_.colors[14], this.guiMain_.colors[5], Mathf.PingPong(Time.time, 1f));
				}
				else
				{
					this.myImage.color = this.guiMain_.colors[14];
				}
			}
			if (!this.gS_.retailVersion || this.gS_.arcade)
			{
				this.myImage.color = this.guiMain_.colors[14];
			}
		}
		if (this.gS_.schublade)
		{
			if (this.uiObjects[11].activeSelf)
			{
				this.uiObjects[11].SetActive(false);
			}
			if (this.uiObjects[12].activeSelf)
			{
				this.uiObjects[12].SetActive(false);
			}
			if (this.uiObjects[13].activeSelf)
			{
				this.uiObjects[13].SetActive(false);
			}
			if (this.uiObjects[21].activeSelf)
			{
				this.uiObjects[21].SetActive(false);
			}
			if (this.uiObjects[27].activeSelf)
			{
				this.uiObjects[27].SetActive(false);
			}
			if (this.uiObjects[33].activeSelf)
			{
				this.uiObjects[33].SetActive(false);
			}
			if (this.uiObjects[25].activeSelf)
			{
				this.uiObjects[25].SetActive(false);
			}
		}
		if (this.fullView)
		{
			float num = 0f;
			float num2 = 5f;
			num += 22f;
			num += 55f;
			num += 25f + num2;
			if (!this.gS_.schublade)
			{
				if (this.uiObjects[33].activeSelf)
				{
					num += 32f + num2;
				}
				if (this.uiObjects[11].activeSelf)
				{
					num += 32f + num2;
				}
				if (this.uiObjects[12].activeSelf)
				{
					num += 73f + num2;
				}
				if (this.uiObjects[13].activeSelf)
				{
					num += 32f + num2;
				}
				if (this.uiObjects[21].activeSelf)
				{
					num += 32f + num2;
				}
				if (this.uiObjects[25].activeSelf)
				{
					num += 17f + num2;
				}
				if (this.uiObjects[27].activeSelf)
				{
					num += 32f + num2;
				}
			}
			num += 20f + num2;
			if (Mathf.RoundToInt(this.rect.sizeDelta.y) != Mathf.RoundToInt(num))
			{
				this.rect.sizeDelta = new Vector2(220f, (float)Mathf.RoundToInt(num));
			}
		}
		if (this.guiMain_ && this.guiMain_.guiTooltip.tooltipEnabled)
		{
			if (this.tooltipTimer <= 0f)
			{
				this.tooltipTimer = 1f;
				this.tooltip_.c = this.gS_.GetTooltip();
				return;
			}
			this.tooltipTimer -= Time.deltaTime;
		}
	}

	
	public void UpdateData()
	{
		this.FindScripts();
		if (!this.gS_)
		{
			this.FindMyGame();
			if (!this.gS_)
			{
				return;
			}
		}
		this.uiObjects[9].GetComponent<Image>().sprite = this.gS_.GetPlatformTypSprite();
		this.uiObjects[35].GetComponent<Image>().sprite = this.genres_.GetPic(this.gS_.maingenre);
		this.uiObjects[36].GetComponent<Image>().sprite = this.gS_.GetSizeSprite();
		if (!this.gS_.pubOffer)
		{
			if (this.uiObjects[40].activeSelf)
			{
				this.uiObjects[40].SetActive(false);
			}
		}
		else if (!this.uiObjects[40].activeSelf)
		{
			this.uiObjects[40].SetActive(true);
		}
		if (this.gS_.gameLicence != -1)
		{
			this.uiObjects[37].GetComponent<Image>().sprite = this.licences_.licenceSprites[this.licences_.licence_TYP[this.gS_.gameLicence]];
		}
		else
		{
			this.uiObjects[37].GetComponent<Image>().sprite = this.guiMain_.uiSprites[19];
		}
		long gesamtGewinn = this.gS_.GetGesamtGewinn();
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
		this.uiObjects[4].GetComponent<Text>().text = this.gS_.weeksOnMarket.ToString();
		this.uiObjects[6].GetComponent<Image>().sprite = this.gS_.GetTypSprite();
		float num = 0f;
		for (int i = 0; i < this.uiBalken.Length; i++)
		{
			if ((float)this.gS_.sellsPerWeek[i] > num)
			{
				num = (float)this.gS_.sellsPerWeek[i];
			}
		}
		for (int j = 1; j < this.uiBalken.Length; j++)
		{
			float num2 = (float)this.gS_.sellsPerWeek[j];
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
		float num3 = (float)this.gS_.sellsPerWeek[0];
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
			if (this.gS_.publisherID != this.mS_.myID)
			{
				this.uiObjects[8].GetComponent<Image>().sprite = this.uiSprites[0];
			}
			if (this.gS_.publisherID == this.mS_.myID)
			{
				this.uiObjects[8].GetComponent<Image>().sprite = this.uiSprites[0];
			}
			this.UpdateData();
			return;
		}
		this.uiObjects[8].GetComponent<Image>().sprite = this.uiSprites[1];
	}

	
	public void BUTTON_Click()
	{
		if (!this.gS_)
		{
			return;
		}
		if (this.gS_.schublade)
		{
			GameObject gameObject = GameObject.Find("Task_" + this.gS_.schubladeTaskID.ToString());
			if (gameObject)
			{
				this.sfx_.PlaySound(3, true);
				this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[69]);
				this.guiMain_.uiObjects[69].GetComponent<Menu_DevGame_Complete>().Init(this.gS_, gameObject.GetComponent<taskGame>());
				this.guiMain_.OpenMenu(false);
				return;
			}
		}
		else
		{
			this.sfx_.PlaySound(3, true);
			this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[91]);
			this.guiMain_.uiObjects[91].GetComponent<Menu_Game_Umsatz>().Init(this.gS_);
			this.guiMain_.OpenMenu(false);
		}
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

	
	public int gameID = -1;

	
	public gameScript gS_;

	
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
