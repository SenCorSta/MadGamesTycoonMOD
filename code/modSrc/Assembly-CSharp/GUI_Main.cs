using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Vectrosity;


public class GUI_Main : MonoBehaviour
{
	
	private void Awake()
	{
		this.mpCalls_ = GameObject.Find("NetworkManager").GetComponent<mpCalls>();
		this.InitVectrocity();
	}

	
	private void Start()
	{
		this.FindScripts();
		this.LoadPlayerAndCompanyName();
	}

	
	private void Update()
	{
		this.UpdateUIHotkey();
		if (this.IsStartMenuActive())
		{
			return;
		}
		this.UpdateTutorial();
		this.UpdateData();
		this.UpdateStudioBewertung();
		this.UpdateFilterToggles();
		this.UpdatePanelMultiplayer();
		this.mS_.DrawFilter(this.filterToggles, false);
		if (!this.mS_.multiplayer && !this.menuOpen && Input.GetKeyUp(KeyCode.Space))
		{
			if (this.mS_.GetGameSpeed() > 0f)
			{
				this.spacePause = true;
				this.mS_.PauseGame(true);
				return;
			}
			this.spacePause = false;
			this.mS_.PauseGame(false);
		}
	}

	
	public void UpdateOnce()
	{
		this.SetMainGuiData();
		this.UpdateAuftragsansehen(0f);
		this.InitToggles();
	}

	
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindGameObjectWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.settings_)
		{
			this.settings_ = this.main_.GetComponent<settingsScript>();
		}
		if (!this.camera_)
		{
			this.camera_ = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
		}
		if (!this.guiMainButtons_)
		{
			this.guiMainButtons_ = base.transform.Find("GUI_MainButtons").GetComponent<GUI_MainButtons>();
		}
		if (!this.guiTooltip)
		{
			this.guiTooltip = base.GetComponent<GUI_Tooltip>();
		}
		if (!this.objectTooltip)
		{
			this.objectTooltip = base.GetComponent<objectTooltip>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.themes_)
		{
			this.themes_ = this.main_.GetComponent<themes>();
		}
		if (!this.forschungSonstiges_)
		{
			this.forschungSonstiges_ = this.main_.GetComponent<forschungSonstiges>();
		}
		if (!this.mapScript_)
		{
			this.mapScript_ = this.main_.GetComponent<mapScript>();
		}
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
		}
		if (!this.contractMain_)
		{
			this.contractMain_ = this.main_.GetComponent<contractWorkMain>();
		}
		if (!this.publishingOfferMain_)
		{
			this.publishingOfferMain_ = this.main_.GetComponent<publishingOfferMain>();
		}
		if (!this.pcS_)
		{
			this.pcS_ = this.main_.GetComponent<pickCharacterScript>();
		}
		if (!this.gF_)
		{
			this.gF_ = this.main_.GetComponent<gameplayFeatures>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.postProcess_)
		{
			this.postProcess_ = this.camera_.GetComponent<PostProcessing>();
		}
	}

	
	public bool IsStartMenuActive()
	{
		return this.uiObjects[151].activeSelf;
	}

	
	public void ActivateMenu(GameObject go)
	{
		if (!go.activeSelf)
		{
			go.SetActive(true);
		}
	}

	
	public void DeactivateMenu(GameObject go)
	{
		if (go.activeSelf)
		{
			go.SetActive(false);
		}
	}

	
	public Vector2 GetAnchoredPosition(Vector2 v)
	{
		return v / this.settings_.uiScale;
	}

	
	public bool IsMouseOverGUI()
	{
		return EventSystem.current.IsPointerOverGameObject();
	}

	
	public IEnumerator MoneyPopEnumerate(int i, Vector3 pos, bool green)
	{
		yield return new WaitForSeconds(UnityEngine.Random.Range(0f, 1f));
		this.MoneyPop(i, pos, green);
		yield break;
	}

	
	public void MoneyPop(int i, Vector3 pos, bool green)
	{
		Vector2 vector = this.camera_.WorldToScreenPoint(pos);
		if (vector.x >= 0f && vector.x <= (float)Screen.width && vector.y >= 0f && vector.y <= (float)Screen.height)
		{
			GameObject gameObject = null;
			for (int j = 0; j < this.moneyPopList.Count; j++)
			{
				if (this.moneyPopList[j] && this.moneyPopList[j].transform.position.x >= 99998f)
				{
					gameObject = this.moneyPopList[j];
					break;
				}
			}
			if (!gameObject)
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(99999f, 99999f, 0f), Quaternion.identity);
				this.moneyPopList.Add(gameObject);
				gameObject.transform.SetParent(this.uiPops.transform);
			}
			moneyPop component = gameObject.GetComponent<moneyPop>();
			component.myTimer = 0f;
			component.camera_ = this.camera_;
			component.myPosition = pos;
			component.settings_ = this.settings_;
			component.Init(new Vector2(vector.x, vector.y - (float)Screen.height));
			if (component.text_)
			{
				component.text_.text = this.mS_.GetMoney((long)i, true);
				if (green)
				{
					component.text_.color = this.colors[24];
				}
			}
		}
	}

	
	public void ShowTooltip(string c)
	{
		if (!this.guiTooltip.tooltipEnabled || c != this.guiTooltip.myText.text)
		{
			this.guiTooltip.SetActive(c);
		}
	}

	
	public void DisableTooltip()
	{
		if (this.guiTooltip.tooltipEnabled)
		{
			this.guiTooltip.SetInactive();
		}
	}

	
	public void ShowObjectTooltip(objectScript script_)
	{
		if (!script_)
		{
			this.DisableObjectTooltip();
			return;
		}
		if (!this.objectTooltip.tooltipEnabled)
		{
			this.objectTooltip.SetActive(script_);
		}
	}

	
	public void DisableObjectTooltip()
	{
		if (this.objectTooltip.tooltipEnabled)
		{
			this.objectTooltip.SetInactive();
		}
	}

	
	public void OpenMenu(bool hideChars)
	{
		this.FindScripts();
		this.menuOpen = true;
		this.disableRoomGUI = true;
		if (!this.mS_.multiplayer && !this.settings_.singleplayerPause)
		{
			this.mS_.PauseGame(true);
		}
		if (hideChars)
		{
			base.StartCoroutine(this.HideChars());
		}
		this.uiObjects[1].GetComponent<GUI_MainButtons>().CloseAllDropdowns();
		this.MainButtonsInteractable(false);
		this.CloseAllRoomButtons();
		if (this.mS_.multiplayer && this.mS_.settings_autoPauseForMultiplayer)
		{
			if (this.mpCalls_.isClient)
			{
				this.mpCalls_.SetPause(true);
				this.mpCalls_.CLIENT_Send_Command(2);
				return;
			}
			if (this.mpCalls_.isServer)
			{
				this.mpCalls_.SetPause(true);
				this.mpCalls_.SERVER_Send_AutoPause();
			}
		}
	}

	
	public void CloseMenu()
	{
		this.menuOpen = false;
		this.disableRoomGUI = false;
		this.mS_.PauseGame(false);
		if (base.gameObject.activeSelf)
		{
			base.StartCoroutine(this.UnhideChars());
		}
		this.MainButtonsInteractable(true);
		if (this.mS_.multiplayer && this.mS_.settings_autoPauseForMultiplayer && this.mS_.myID != -1)
		{
			if (this.mpCalls_.isClient)
			{
				this.mpCalls_.SetPause(false);
				this.mpCalls_.CLIENT_Send_Command(3);
				return;
			}
			this.mpCalls_.SetPause(false);
			this.mpCalls_.SERVER_Send_AutoPause();
		}
	}

	
	private void MainButtonsInteractable(bool b)
	{
		for (int i = 0; i < this.guiMainButtons_.transform.childCount; i++)
		{
			Transform child = this.guiMainButtons_.transform.GetChild(i);
			if (child)
			{
				child.GetComponent<Button>().interactable = b;
				if (b)
				{
					child.GetChild(1).GetComponent<Image>().color = Color.white;
				}
				else
				{
					child.GetChild(1).GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
				}
			}
		}
		this.uiObjects[106].GetComponent<Button>().interactable = b;
		this.uiObjects[107].GetComponent<Button>().interactable = b;
		this.uiObjects[160].GetComponent<Button>().interactable = b;
		this.uiObjects[346].GetComponent<Button>().interactable = b;
		this.uiObjects[347].GetComponent<Button>().interactable = b;
		this.uiObjects[376].GetComponent<Button>().interactable = b;
		this.uiObjects[381].GetComponent<Button>().interactable = b;
	}

	
	private IEnumerator UnhideChars()
	{
		yield return new WaitForEndOfFrame();
		for (int i = 0; i < this.mS_.arrayCharacters.Length; i++)
		{
			if (this.mS_.arrayCharacters[i])
			{
				this.mS_.arrayCharacters[i].GetComponent<characterScript>().UnhideChar();
			}
		}
		yield break;
	}

	
	private IEnumerator HideChars()
	{
		yield return new WaitForEndOfFrame();
		for (int i = 0; i < this.mS_.arrayCharacters.Length; i++)
		{
			if (this.mS_.arrayCharacters[i])
			{
				this.mS_.arrayCharacters[i].GetComponent<characterScript>().HideChar();
			}
		}
		yield break;
	}

	
	public void SetMainGuiData()
	{
		this.uiObjects[6].GetComponent<Text>().text = this.mS_.GetCompanyName();
		this.uiObjects[5].GetComponent<Image>().sprite = this.flagSprites[this.mS_.GetCountryID()];
		this.uiObjects[5].GetComponent<tooltip>().c = this.tS_.GetCountry(this.mS_.GetCountryID());
		if (this.logoSprites.Length > this.mS_.GetCompanyLogoID())
		{
			this.uiObjects[11].GetComponent<Image>().sprite = this.GetCompanyLogo(this.mS_.GetCompanyLogoID());
		}
		this.UpdateTrend();
		this.UpdateStudioBewertung();
	}

	
	public void UpdateFans()
	{
		this.fansTimer += Time.deltaTime;
		int amountFans = this.genres_.GetAmountFans();
		if (this.fansTimer > 1f)
		{
			this.fansTimer = 0f;
			this.uiObjects[229].GetComponent<tooltip>().c = this.GetFansTooltip();
			if (this.fansOld > amountFans)
			{
				this.fansString = "<color=red>" + this.tS_.GetText(100) + "</color>";
			}
			if (this.fansOld < amountFans)
			{
				this.fansString = "<color=green>" + this.tS_.GetText(99) + "</color>";
			}
			this.fansOld = amountFans;
		}
		this.uiObjects[8].GetComponent<Text>().text = this.mS_.GetMoney((long)amountFans, false);
		Text component = this.uiObjects[8].GetComponent<Text>();
		component.text += this.fansString;
	}

	
	public void UpdateSupportIcon()
	{
		this.supportTimer += Time.deltaTime;
		if (this.supportTimer > 1f)
		{
			this.supportTimer = 0f;
			this.uiObjects[140].GetComponent<Image>().sprite = this.iconSupport[this.mS_.GetAnrufeAmount()];
		}
	}

	
	public void UpdateMoney()
	{
		this.moneyTimer += Time.deltaTime;
		if (this.moneyTimer > 1f)
		{
			this.moneyTimer = 0f;
			if (this.moneyOld > this.mS_.money)
			{
				this.moneyString = "<color=red>" + this.tS_.GetText(100) + "</color>";
			}
			if (this.moneyOld < this.mS_.money)
			{
				this.moneyString = "<color=green>" + this.tS_.GetText(99) + "</color>";
			}
			this.moneyOld = this.mS_.money;
		}
		if (this.mS_.money >= 0L)
		{
			this.uiObjects[7].GetComponent<Text>().text = this.mS_.GetMoney(this.mS_.money, false);
		}
		else
		{
			this.uiObjects[7].GetComponent<Text>().text = "<color=red>" + this.mS_.GetMoney(this.mS_.money, false) + "</color>";
		}
		Text component = this.uiObjects[7].GetComponent<Text>();
		component.text += this.moneyString;
		if (!this.moneyTooltip)
		{
			this.moneyTooltip = this.uiObjects[376].GetComponent<tooltip>();
			return;
		}
		this.moneyTooltip.c = "<b>" + this.tS_.GetText(704) + "</b>\n";
		tooltip tooltip;
		if (this.mS_.finanzenMonat_GetGewinn() < 0L)
		{
			tooltip = this.moneyTooltip;
			tooltip.c = string.Concat(new string[]
			{
				tooltip.c,
				this.tS_.GetText(699),
				": <color=red>",
				this.mS_.GetMoney(this.mS_.finanzenMonat_GetGewinn(), true),
				"</color>\n"
			});
		}
		else
		{
			tooltip = this.moneyTooltip;
			tooltip.c = string.Concat(new string[]
			{
				tooltip.c,
				this.tS_.GetText(699),
				": <color=green>",
				this.mS_.GetMoney(this.mS_.finanzenMonat_GetGewinn(), true),
				"</color>\n"
			});
		}
		if (this.mS_.finanzenMonatLast_GetGewinn() < 0L)
		{
			tooltip = this.moneyTooltip;
			tooltip.c = string.Concat(new string[]
			{
				tooltip.c,
				this.tS_.GetText(701),
				": <color=red>",
				this.mS_.GetMoney(this.mS_.finanzenMonatLast_GetGewinn(), true),
				"</color>\n"
			});
			return;
		}
		tooltip = this.moneyTooltip;
		tooltip.c = string.Concat(new string[]
		{
			tooltip.c,
			this.tS_.GetText(701),
			": <color=green>",
			this.mS_.GetMoney(this.mS_.finanzenMonatLast_GetGewinn(), true),
			"</color>\n"
		});
	}

	
	public void UpdateCharts()
	{
		int num = 9;
		this.uiObjects[377].GetComponent<Text>().text = "";
		int num2 = 0;
		switch (this.uiObjects[380].GetComponent<Dropdown>().value)
		{
		case 0:
			num2 = this.mS_.games_.chartsWeekList.Count;
			break;
		case 1:
			num2 = this.mS_.games_.chartsWeekList_Handy.Count;
			break;
		case 2:
			num2 = this.mS_.games_.chartsWeekList_Arcade.Count;
			break;
		case 3:
			num2 = this.mS_.games_.chartsWeekList_F2P.Count;
			break;
		}
		for (int i = 0; i < num2; i++)
		{
			gameScript gameScript = null;
			switch (this.uiObjects[380].GetComponent<Dropdown>().value)
			{
			case 0:
				gameScript = this.mS_.games_.chartsWeekList[i].script_;
				break;
			case 1:
				gameScript = this.mS_.games_.chartsWeekList_Handy[i].script_;
				break;
			case 2:
				gameScript = this.mS_.games_.chartsWeekList_Arcade[i].script_;
				break;
			case 3:
				gameScript = this.mS_.games_.chartsWeekList_F2P[i].script_;
				break;
			}
			if (gameScript)
			{
				int num3 = i;
				string str = "";
				if (gameScript.lastChartPosition < num3)
				{
					str = "<color=red>▼</color> ";
				}
				if (gameScript.lastChartPosition > num3)
				{
					str = "<color=green>▲</color> ";
				}
				if (gameScript.lastChartPosition == num3)
				{
					str = "<color=black>●</color> ";
				}
				if (gameScript.lastChartPosition == -1)
				{
					str = "<color=blue>◆</color> ";
				}
				Text component = this.uiObjects[377].GetComponent<Text>();
				component.text += str;
				if (gameScript.ownerID == this.mS_.myID || gameScript.publisherID == this.mS_.myID)
				{
					Text component2 = this.uiObjects[377].GetComponent<Text>();
					component2.text = component2.text + "<color=blue>" + gameScript.GetNameWithTag() + "</color>\n";
				}
				else
				{
					Text component3 = this.uiObjects[377].GetComponent<Text>();
					component3.text = component3.text + gameScript.GetNameWithTag() + "\n";
				}
				if (i >= num)
				{
					break;
				}
			}
		}
	}

	
	private void UpdateMainGuiCharts()
	{
		if (this.uiObjects[379].GetComponent<Toggle>().isOn && !this.uiObjects[0].activeSelf && !this.uiObjects[15].activeSelf && !this.uiObjects[19].activeSelf && !this.uiObjects[20].activeSelf)
		{
			if (!this.uiObjects[378].activeSelf)
			{
				this.uiObjects[378].SetActive(true);
				int value = this.uiObjects[380].GetComponent<Dropdown>().value;
				List<string> list = new List<string>();
				list.Add(this.tS_.GetText(1780));
				list.Add(this.tS_.GetText(1796));
				list.Add(this.tS_.GetText(1797));
				list.Add(this.tS_.GetText(1779));
				this.uiObjects[380].GetComponent<Dropdown>().ClearOptions();
				this.uiObjects[380].GetComponent<Dropdown>().AddOptions(list);
				this.uiObjects[380].GetComponent<Dropdown>().value = value;
				return;
			}
		}
		else if (this.uiObjects[378].activeSelf)
		{
			this.uiObjects[378].SetActive(false);
		}
	}

	
	public void UpdateData()
	{
		if (!this.uiObjects[145].activeSelf)
		{
			return;
		}
		this.UpdateMoney();
		this.UpdateMainGuiCharts();
		this.UpdateFans();
		this.UpdateSupportIcon();
		this.uiObjects[9].GetComponent<Text>().text = this.GetDate();
		this.uiObjects[10].GetComponent<Image>().fillAmount = this.mS_.dayTimer;
		this.uiObjects[10].GetComponent<Image>().color = this.colors[17];
		if (this.mS_.multiplayer && this.mS_.settings_autoPauseForMultiplayer && this.mpCalls_.AutoPause())
		{
			this.uiObjects[10].GetComponent<Image>().color = this.colors[18];
		}
		this.UpdateGameSpeedButtons();
		if (this.uiObjects[23].transform.childCount > 3)
		{
			if (!this.uiObjects[24].activeSelf)
			{
				this.uiObjects[24].SetActive(true);
			}
		}
		else if (this.uiObjects[24].activeSelf)
		{
			this.uiObjects[24].SetActive(false);
		}
		this.timerUpdateTopLeiste += Time.deltaTime;
		if (this.timerUpdateTopLeiste > 1f)
		{
			this.UpdateTrend();
			this.timerUpdateTopLeiste = 0f;
			float num = this.durchschnittsMotivation;
			this.durchschnittsMotivation = 0f;
			foreach (GameObject gameObject in this.mS_.arrayCharacters)
			{
				if (gameObject)
				{
					characterScript component = gameObject.GetComponent<characterScript>();
					if (component)
					{
						this.durchschnittsMotivation += component.s_motivation;
					}
				}
			}
			this.durchschnittsMotivation /= (float)this.mS_.arrayCharacters.Length;
			if (this.durchschnittsMotivation > num)
			{
				this.uiObjects[75].GetComponent<Text>().text = string.Concat(new string[]
				{
					"<color=green>",
					this.tS_.GetText(99),
					"</color> ",
					this.mS_.Round(this.durchschnittsMotivation, 1).ToString(),
					"%"
				});
			}
			else
			{
				this.uiObjects[75].GetComponent<Text>().text = string.Concat(new string[]
				{
					"<color=red>",
					this.tS_.GetText(100),
					"</color> ",
					this.mS_.Round(this.durchschnittsMotivation, 1).ToString(),
					"%"
				});
			}
		}
		this.timerUpdateContract += Time.deltaTime;
		if (this.timerUpdateContract > 1f)
		{
			this.timerUpdateContract = 0f;
			this.contractMain_.UpdateGUI();
			this.publishingOfferMain_.UpdateGUI();
			this.UpdateArbeitsmarktIcon();
		}
		this.timerUpdateGlobalEvent += Time.deltaTime;
		if (this.timerUpdateGlobalEvent > 1f)
		{
			this.timerUpdateGlobalEvent = 0f;
			if (this.mS_.globalEvent != -1)
			{
				if (!this.uiObjects[217].activeSelf)
				{
					this.uiObjects[217].SetActive(true);
				}
				this.uiObjects[217].GetComponent<Image>().sprite = this.iconGlobalEvents[this.mS_.globalEvent];
				switch (this.mS_.globalEvent)
				{
				case 0:
					this.uiObjects[217].GetComponent<tooltip>().c = this.tS_.GetText(1080);
					break;
				case 1:
					this.uiObjects[217].GetComponent<tooltip>().c = this.tS_.GetText(1081);
					break;
				case 2:
					this.uiObjects[217].GetComponent<tooltip>().c = this.tS_.GetText(1082);
					break;
				case 3:
					this.uiObjects[217].GetComponent<tooltip>().c = this.tS_.GetText(1083);
					break;
				case 4:
					this.uiObjects[217].GetComponent<tooltip>().c = this.tS_.GetText(1084);
					break;
				case 5:
					this.uiObjects[217].GetComponent<tooltip>().c = this.tS_.GetText(1085);
					break;
				case 6:
					this.uiObjects[217].GetComponent<tooltip>().c = this.tS_.GetText(1086);
					break;
				case 7:
					this.uiObjects[217].GetComponent<tooltip>().c = this.tS_.GetText(1087);
					break;
				case 8:
					this.uiObjects[217].GetComponent<tooltip>().c = this.tS_.GetText(1316);
					break;
				case 9:
					this.uiObjects[217].GetComponent<tooltip>().c = this.tS_.GetText(1088);
					break;
				case 10:
					this.uiObjects[217].GetComponent<tooltip>().c = this.tS_.GetText(1377);
					break;
				case 11:
					this.uiObjects[217].GetComponent<tooltip>().c = this.tS_.GetText(1384);
					break;
				case 12:
					this.uiObjects[217].GetComponent<tooltip>().c = this.tS_.GetText(1089);
					break;
				case 13:
					this.uiObjects[217].GetComponent<tooltip>().c = this.tS_.GetText(1889);
					break;
				}
			}
			else if (this.uiObjects[217].activeSelf)
			{
				this.uiObjects[217].SetActive(false);
			}
		}
		this.UpdateRoomNoJob();
		this.UpdateMitarbeiterNoJob();
		this.UpdateExklusivVertragIcon();
		this.UpdateBankWarningIcon();
		this.UpdateWeihnachtSommerIcon();
		this.UpdateLangweiligIcon();
		this.UpdateSauerBugsIcon();
		this.UpdateAwardBonusIcon();
	}

	
	private void UpdateBankWarningIcon()
	{
		this.updateBankWarningIcon_Timer += Time.deltaTime;
		if (this.updateBankWarningIcon_Timer < 1f)
		{
			return;
		}
		this.updateBankWarningIcon_Timer = 0f;
		if (this.mS_.bankWarning < 6)
		{
			if (this.uiObjects[250].activeSelf)
			{
				this.uiObjects[250].SetActive(false);
				return;
			}
		}
		else
		{
			if (!this.uiObjects[250].activeSelf)
			{
				this.uiObjects[250].SetActive(true);
			}
			this.uiObjects[250].transform.GetChild(0).GetComponent<Text>().text = (18 - this.mS_.bankWarning).ToString();
		}
	}

	
	private void UpdateWeihnachtSommerIcon()
	{
		if (this.mS_.month == 12 || this.mS_.month == 1)
		{
			if (!this.uiObjects[256].activeSelf)
			{
				this.uiObjects[256].SetActive(true);
			}
			if (this.uiObjects[257].activeSelf)
			{
				this.uiObjects[257].SetActive(false);
			}
			return;
		}
		if (this.mS_.month == 6 || this.mS_.month == 7)
		{
			if (this.uiObjects[256].activeSelf)
			{
				this.uiObjects[256].SetActive(false);
			}
			if (!this.uiObjects[257].activeSelf)
			{
				this.uiObjects[257].SetActive(true);
			}
			return;
		}
		if (this.uiObjects[256].activeSelf)
		{
			this.uiObjects[256].SetActive(false);
		}
		if (this.uiObjects[257].activeSelf)
		{
			this.uiObjects[257].SetActive(false);
		}
	}

	
	private void UpdateLangweiligIcon()
	{
		this.mS_.gelangweiltGenre = -1;
		if (this.mS_.difficulty <= 2)
		{
			if (this.uiObjects[258].activeSelf)
			{
				this.uiObjects[258].SetActive(false);
			}
			return;
		}
		for (int i = 0; i < this.genres_.genres_UNLOCK.Length; i++)
		{
			if (this.mS_.GetFanGenreID() != i)
			{
				int num = 0;
				switch (this.mS_.difficulty)
				{
				case 3:
					if (this.mS_.lastGamesGenre[0] == i)
					{
						num++;
					}
					if (this.mS_.lastGamesGenre[1] == i)
					{
						num++;
					}
					if (this.mS_.lastGamesGenre[2] == i)
					{
						num++;
					}
					if (this.mS_.lastGamesGenre[3] == i)
					{
						num++;
					}
					if (this.mS_.lastGamesGenre[4] == i)
					{
						num++;
					}
					if (num >= 4)
					{
						this.mS_.gelangweiltGenre = i;
					}
					break;
				case 4:
					if (this.mS_.lastGamesGenre[0] == i)
					{
						num++;
					}
					if (this.mS_.lastGamesGenre[1] == i)
					{
						num++;
					}
					if (this.mS_.lastGamesGenre[2] == i)
					{
						num++;
					}
					if (this.mS_.lastGamesGenre[3] == i)
					{
						num++;
					}
					if (this.mS_.lastGamesGenre[4] == i)
					{
						num++;
					}
					if (num >= 4)
					{
						this.mS_.gelangweiltGenre = i;
					}
					break;
				case 5:
					if (this.mS_.lastGamesGenre[0] == i)
					{
						num++;
					}
					if (this.mS_.lastGamesGenre[1] == i)
					{
						num++;
					}
					if (this.mS_.lastGamesGenre[2] == i)
					{
						num++;
					}
					if (this.mS_.lastGamesGenre[3] == i)
					{
						num++;
					}
					if (this.mS_.lastGamesGenre[4] == i)
					{
						num++;
					}
					if (num >= 3)
					{
						this.mS_.gelangweiltGenre = i;
					}
					break;
				}
			}
		}
		if (this.mS_.gelangweiltGenre != -1)
		{
			if (!this.uiObjects[258].activeSelf)
			{
				this.uiObjects[258].SetActive(true);
			}
			string text = this.tS_.GetText(1302);
			text = text.Replace("<NAME>", this.genres_.GetName(this.mS_.gelangweiltGenre));
			this.uiObjects[258].GetComponent<tooltip>().c = text;
			return;
		}
		if (this.uiObjects[258].activeSelf)
		{
			this.uiObjects[258].SetActive(false);
		}
	}

	
	private void UpdateSauerBugsIcon()
	{
		if (this.mS_.sauerBugs > 0)
		{
			if (!this.uiObjects[259].activeSelf)
			{
				this.uiObjects[259].SetActive(true);
				return;
			}
		}
		else if (this.uiObjects[259].activeSelf)
		{
			this.uiObjects[259].SetActive(false);
		}
	}

	
	private void UpdateAwardBonusIcon()
	{
		if (this.mS_.awardBonus > 0)
		{
			if (!this.uiObjects[404].activeSelf)
			{
				this.uiObjects[404].SetActive(true);
				string text = this.tS_.GetText(2011);
				text = text.Replace("<NUM>", Mathf.RoundToInt(this.mS_.awardBonusAmount * 100f).ToString());
				this.uiObjects[404].GetComponent<tooltip>().c = text;
				return;
			}
		}
		else if (this.uiObjects[404].activeSelf)
		{
			this.uiObjects[404].SetActive(false);
		}
	}

	
	private void UpdateExklusivVertragIcon()
	{
		this.updateExklusivVertragIcon_Timer += Time.deltaTime;
		if (this.updateExklusivVertragIcon_Timer < 1f)
		{
			return;
		}
		this.updateExklusivVertragIcon_Timer = 0f;
		if (this.mS_.exklusivVertrag_ID == -1)
		{
			if (this.uiObjects[212].activeSelf)
			{
				this.uiObjects[212].SetActive(false);
				return;
			}
		}
		else
		{
			publisherScript exklusivPublisher = this.mS_.GetExklusivPublisher();
			if (exklusivPublisher)
			{
				if (!this.uiObjects[212].activeSelf)
				{
					this.uiObjects[212].SetActive(true);
				}
				this.uiObjects[212].GetComponent<Image>().sprite = exklusivPublisher.GetLogo();
				this.uiObjects[212].transform.GetChild(0).GetComponent<Text>().text = this.mS_.exklusivVertrag_laufzeit.ToString();
				string text = this.tS_.GetText(1048);
				text = text.Replace("<NUM>", this.mS_.exklusivVertrag_laufzeit.ToString());
				string text2 = "<b>" + this.tS_.GetText(1021) + "\n\n";
				text2 = text2 + exklusivPublisher.GetName() + "</b>\n";
				text2 = string.Concat(new string[]
				{
					text2,
					this.tS_.GetText(436),
					": <color=blue><b>",
					this.mS_.GetMoney((long)exklusivPublisher.GetShareExklusiv(), true),
					"</b></color>\n"
				});
				text2 = string.Concat(new string[]
				{
					text2,
					this.tS_.GetText(1023),
					": <color=blue><b>",
					text,
					"</b></color>\n"
				});
				this.uiObjects[212].GetComponent<tooltip>().c = text2;
			}
		}
	}

	
	public void UpdateAuftragsansehen(float f)
	{
		this.mS_.auftragsAnsehen += f;
		if (this.mS_.auftragsAnsehen < 0f)
		{
			this.mS_.auftragsAnsehen = 0f;
		}
		if (this.mS_.auftragsAnsehen > 100f)
		{
			this.mS_.auftragsAnsehen = 100f;
			if (this.mS_.achScript_)
			{
				this.mS_.achScript_.SetAchivement(43);
			}
		}
		if (f >= 0f)
		{
			this.uiObjects[76].GetComponent<Text>().text = this.mS_.Round(this.mS_.auftragsAnsehen, 1).ToString() + "% <color=green>" + this.tS_.GetText(99) + "</color>";
			return;
		}
		this.uiObjects[76].GetComponent<Text>().text = this.mS_.Round(this.mS_.auftragsAnsehen, 1).ToString() + "% <color=red>" + this.tS_.GetText(100) + "</color>";
	}

	
	public void UpdateTrend()
	{
		this.uiObjects[77].GetComponent<Image>().sprite = this.genres_.GetPic(this.mS_.trendGenre);
		this.uiObjects[78].GetComponent<Image>().sprite = this.genres_.GetPic(this.mS_.trendAntiGenre);
		this.uiObjects[79].GetComponent<Text>().text = string.Concat(new string[]
		{
			"<color=green>",
			this.tS_.GetThemes(this.mS_.trendTheme),
			"</color>\n<color=red>",
			this.tS_.GetThemes(this.mS_.trendAntiTheme),
			"</color>"
		});
		string text = "";
		text += this.tS_.GetText(479);
		text = text.Replace("<NAME1>", this.genres_.GetName(this.mS_.trendGenre));
		text = text.Replace("<NAME2>", this.tS_.GetThemes(this.mS_.trendTheme));
		text += "\n\n";
		text += this.tS_.GetText(480);
		text = text.Replace("<NAME1>", this.genres_.GetName(this.mS_.trendAntiGenre));
		text = text.Replace("<NAME2>", this.tS_.GetThemes(this.mS_.trendAntiTheme));
		text += "\n\n";
		text += this.tS_.GetText(481);
		text = text.Replace("<TIME>", this.mS_.trendWeeks.ToString());
		this.uiObjects[80].GetComponent<tooltip>().c = text;
	}

	
	public void UpdateStudioBewertung()
	{
		this.updateStudioBewertungTimer -= Time.deltaTime;
		if (this.updateStudioBewertungTimer > 3f)
		{
			return;
		}
		this.updateStudioBewertungTimer = 3f;
		int studioLevel = this.mS_.GetStudioLevel(this.mS_.studioPoints);
		this.uiObjects[16].GetComponent<tooltip>().c = "<b>" + this.tS_.GetStudioBewertung(studioLevel) + "</b>\n" + this.tS_.GetText(1493);
		for (int i = 0; i < this.uiObjects[16].transform.childCount; i++)
		{
			this.uiObjects[16].transform.GetChild(i).GetComponent<Image>().fillAmount = 0f;
		}
		if (studioLevel >= 1)
		{
			this.uiObjects[16].transform.GetChild(0).GetComponent<Image>().fillAmount = 0.5f;
		}
		if (studioLevel >= 2)
		{
			this.uiObjects[16].transform.GetChild(0).GetComponent<Image>().fillAmount = 1f;
		}
		if (studioLevel >= 3)
		{
			this.uiObjects[16].transform.GetChild(1).GetComponent<Image>().fillAmount = 0.5f;
		}
		if (studioLevel >= 4)
		{
			this.uiObjects[16].transform.GetChild(1).GetComponent<Image>().fillAmount = 1f;
		}
		if (studioLevel >= 5)
		{
			this.uiObjects[16].transform.GetChild(2).GetComponent<Image>().fillAmount = 0.5f;
		}
		if (studioLevel >= 6)
		{
			this.uiObjects[16].transform.GetChild(2).GetComponent<Image>().fillAmount = 1f;
		}
		if (studioLevel >= 7)
		{
			this.uiObjects[16].transform.GetChild(3).GetComponent<Image>().fillAmount = 0.5f;
		}
		if (studioLevel >= 8)
		{
			this.uiObjects[16].transform.GetChild(3).GetComponent<Image>().fillAmount = 1f;
		}
		if (studioLevel >= 9)
		{
			this.uiObjects[16].transform.GetChild(4).GetComponent<Image>().fillAmount = 0.5f;
		}
		if (studioLevel >= 10)
		{
			this.uiObjects[16].transform.GetChild(4).GetComponent<Image>().fillAmount = 1f;
		}
	}

	
	public void UpdateGameSpeedButtons()
	{
		if (this.mS_.multiplayer)
		{
			if (this.mpCalls_.isClient)
			{
				this.uiObjects[203].GetComponent<Button>().interactable = false;
				this.uiObjects[204].GetComponent<Button>().interactable = false;
				this.uiObjects[205].GetComponent<Button>().interactable = false;
			}
			else
			{
				this.uiObjects[203].GetComponent<Button>().interactable = true;
				this.uiObjects[204].GetComponent<Button>().interactable = true;
				this.uiObjects[205].GetComponent<Button>().interactable = true;
			}
		}
		this.uiObjects[12].GetComponent<Image>().color = this.colors[16];
		if (this.mS_.GetGameSpeed() == 0f)
		{
			if (!this.uiObjects[12].activeSelf)
			{
				this.uiObjects[12].SetActive(true);
			}
			if (this.uiObjects[13].activeSelf)
			{
				this.uiObjects[13].SetActive(false);
			}
			if (this.uiObjects[14].activeSelf)
			{
				this.uiObjects[14].SetActive(false);
			}
			if (!this.mS_.multiplayer && this.mS_.IsForcedPause())
			{
				this.uiObjects[12].GetComponent<Image>().color = Color.grey;
			}
			return;
		}
		if (this.mS_.GetGameSpeed() == 1f)
		{
			if (this.uiObjects[12].activeSelf)
			{
				this.uiObjects[12].SetActive(false);
			}
			if (!this.uiObjects[13].activeSelf)
			{
				this.uiObjects[13].SetActive(true);
			}
			if (this.uiObjects[14].activeSelf)
			{
				this.uiObjects[14].SetActive(false);
			}
			return;
		}
		if (this.uiObjects[12].activeSelf)
		{
			this.uiObjects[12].SetActive(false);
		}
		if (this.uiObjects[13].activeSelf)
		{
			this.uiObjects[13].SetActive(false);
		}
		if (!this.uiObjects[14].activeSelf)
		{
			this.uiObjects[14].SetActive(true);
		}
	}

	
	public void AddHistory(string c)
	{
		this.mS_.history.Add("<b>" + this.GetDate() + "</b>\n" + c);
	}

	
	public void CreateTopNewsStudiobewertung(string text_)
	{
		this.FindScripts();
		this.sfx_.PlaySound(27, true);
		UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[19], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[22].transform).transform.Find("Text").GetComponent<Text>().text = text_;
		this.AddHistory(this.tS_.GetText(1478) + "\n<color=blue>" + text_ + "</color>");
	}

	
	public void CreateTopNewsUnlock(string name_, Sprite sprite_)
	{
		this.FindScripts();
		this.sfx_.PlaySound(27, true);
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[13], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[22].transform);
		gameObject.transform.Find("Text").GetComponent<Text>().text = name_;
		gameObject.transform.Find("Icon").GetComponent<Image>().sprite = sprite_;
		this.AddHistory(this.tS_.GetText(527) + "\n<color=blue>" + name_ + "</color>");
	}

	
	public void CreateTopNewsInfo(string text_)
	{
		this.FindScripts();
		this.sfx_.PlaySound(27, true);
		UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[12], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[22].transform).transform.Find("Text").GetComponent<Text>().text = text_;
		this.AddHistory(text_);
	}

	
	public void CreateTopNewsAchivement(int id_)
	{
		this.FindScripts();
		this.sfx_.PlaySound(27, true);
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[22], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[22].transform);
		string text = "<color=blue>" + this.tS_.GetAchivementName(id_) + "</color>\n";
		text += this.tS_.GetAchivementDesc(id_);
		gameObject.transform.Find("Text").GetComponent<Text>().text = text;
	}

	
	public void CreateTopNewsTrend(string name_, Sprite sprite_)
	{
		this.FindScripts();
		this.sfx_.PlaySound(27, true);
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[10], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[22].transform);
		gameObject.transform.Find("Text").GetComponent<Text>().text = name_;
		gameObject.transform.Find("Icon").GetComponent<Image>().sprite = sprite_;
		this.AddHistory(this.tS_.GetText(482) + "\n<color=blue>" + name_ + "</color>");
	}

	
	public void CreateTopNewsGoldeneSchallplatte(string name_)
	{
		if (this.mS_.newsSetting[7])
		{
			this.FindScripts();
			this.sfx_.PlaySound(27, true);
			UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[14], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[22].transform).transform.Find("Text").GetComponent<Text>().text = name_;
			this.AddHistory(this.tS_.GetText(759) + "\n<color=blue>" + name_ + "</color>");
		}
	}

	
	public void CreateTopNewsPlatinSchallplatte(string name_)
	{
		if (this.mS_.newsSetting[7])
		{
			this.FindScripts();
			this.sfx_.PlaySound(27, true);
			UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[17], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[22].transform).transform.Find("Text").GetComponent<Text>().text = name_;
			this.AddHistory(this.tS_.GetText(1311) + "\n<color=blue>" + name_ + "</color>");
		}
	}

	
	public void CreateTopNewsDiamantSchallplatte(string name_)
	{
		if (this.mS_.newsSetting[7])
		{
			this.FindScripts();
			this.sfx_.PlaySound(27, true);
			UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[18], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[22].transform).transform.Find("Text").GetComponent<Text>().text = name_;
			this.AddHistory(this.tS_.GetText(1312) + "\n<color=blue>" + name_ + "</color>");
		}
	}

	
	public void CreateTopNewsNewPublisher(string name_, Sprite sprite_)
	{
		if (this.mS_.newsSetting[1])
		{
			this.FindScripts();
			this.sfx_.PlaySound(27, true);
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[9], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[22].transform);
			gameObject.transform.Find("Text").GetComponent<Text>().text = name_;
			gameObject.transform.Find("Icon").GetComponent<Image>().sprite = sprite_;
			this.AddHistory(this.tS_.GetText(431) + "\n<color=blue>" + name_ + "</color>");
		}
	}

	
	public void CreateTopNewsCopyProtect(string name_)
	{
		if (this.mS_.newsSetting[2])
		{
			this.FindScripts();
			this.sfx_.PlaySound(27, true);
			UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[7], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[22].transform).transform.Find("Text").GetComponent<Text>().text = name_;
			this.AddHistory(this.tS_.GetText(289) + "\n<color=blue>" + name_ + "</color>");
		}
	}

	
	public void CreateTopNewsAntiCheat(string name_)
	{
		if (this.mS_.newsSetting[8])
		{
			this.FindScripts();
			this.sfx_.PlaySound(27, true);
			UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[16], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[22].transform).transform.Find("Text").GetComponent<Text>().text = name_;
			this.AddHistory(this.tS_.GetText(1216) + "\n<color=blue>" + name_ + "</color>");
		}
	}

	
	public void CreateTopNewsNpcEngine(string name_)
	{
		if (this.mS_.newsSetting[3])
		{
			this.FindScripts();
			this.sfx_.PlaySound(27, true);
			UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[6], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[22].transform).transform.Find("Text").GetComponent<Text>().text = name_;
			this.AddHistory(this.tS_.GetText(265) + "\n<color=blue>" + name_ + "</color>");
		}
	}

	
	public void CreateTopNewsForschung(string name_, Sprite sprite_)
	{
		if (this.mS_.newsSetting[4])
		{
			this.FindScripts();
			this.sfx_.PlaySound(27, true);
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[1], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[22].transform);
			gameObject.transform.Find("Text").GetComponent<Text>().text = name_;
			gameObject.transform.Find("Icon").GetComponent<Image>().sprite = sprite_;
			this.AddHistory(this.tS_.GetText(164) + "\n<color=blue>" + name_ + "</color>");
		}
	}

	
	public void CreateTopNewsPlatform(platformScript platS_)
	{
		if (this.mS_.newsSetting[5])
		{
			this.FindScripts();
			this.sfx_.PlaySound(27, true);
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[4], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[22].transform);
			gameObject.transform.Find("Text").GetComponent<Text>().text = platS_.GetName();
			platS_.SetPic(gameObject.transform.Find("Icon").gameObject);
			this.AddHistory(this.tS_.GetText(236) + "\n<color=blue>" + platS_.GetName() + "</color>");
		}
	}

	
	public void CreateTopNewsPlatformRemove(platformScript platS_)
	{
		if (this.mS_.newsSetting[6])
		{
			this.FindScripts();
			this.sfx_.PlaySound(27, true);
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[5], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[22].transform);
			gameObject.transform.Find("Text").GetComponent<Text>().text = platS_.GetName();
			platS_.SetPic(gameObject.transform.Find("Icon").gameObject);
			this.AddHistory(this.tS_.GetText(237) + "\n<color=blue>" + platS_.GetName() + "</color>");
		}
	}

	
	public void CreateTopNewsDevLegend(string text_, int beruf)
	{
		this.FindScripts();
		this.sfx_.PlaySound(27, true);
		UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[8], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[22].transform).transform.Find("Text").GetComponent<Text>().text = text_ + "\n<size=14><color=green>[" + this.tS_.GetText(beruf + 137) + "]</color></size>";
		this.AddHistory(this.tS_.GetText(426) + "\n<color=blue>" + text_ + "</color>");
	}

	
	public void CreateLeftNews(int roomID_, Sprite sprite_, string tooltip_, Sprite smallSprite)
	{
		this.FindScripts();
		this.sfx_.PlaySound(28, true);
		LeftNews component = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[2], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[23].transform).GetComponent<LeftNews>();
		component.Init(roomID_, sprite_, tooltip_, smallSprite);
		component.Init(roomID_, sprite_, tooltip_, smallSprite);
		this.AddHistory(tooltip_);
	}

	
	public string GetDate()
	{
		return this.tS_.GetText(102) + this.mS_.year.ToString() + " " + this.tS_.GetText(103) + this.mS_.month.ToString() + " " + this.tS_.GetText(104) + this.mS_.week.ToString();
	}

	
	private void UpdateMitarbeiterNoJob()
	{
		this.timerUpdateMitarbeiterNoJob += Time.deltaTime;
		if (this.timerUpdateMitarbeiterNoJob < 1f)
		{
			return;
		}
		this.timerUpdateMitarbeiterNoJob = 0f;
		for (int i = 0; i < this.mS_.arrayCharacters.Length; i++)
		{
			if (this.mS_.arrayCharacters[i])
			{
				characterScript component = this.mS_.arrayCharacters[i].GetComponent<characterScript>();
				if (component.roomID == -1 && !component.picked)
				{
					this.uiObjects[103].GetComponent<Image>().color = this.colors[5];
					this.uiObjects[103].GetComponent<Animation>().Play();
					return;
				}
			}
		}
		this.uiObjects[103].GetComponent<Image>().color = this.colors[6];
		this.uiObjects[103].GetComponent<Animation>().Stop();
	}

	
	private void UpdateRoomNoJob()
	{
		this.timerUpdateRoomNoJob += Time.deltaTime;
		if (this.timerUpdateRoomNoJob < 1f)
		{
			return;
		}
		this.timerUpdateRoomNoJob = 0f;
		for (int i = 0; i < this.mS_.arrayRooms.Length; i++)
		{
			if (this.mS_.arrayRooms[i])
			{
				roomScript component = this.mS_.arrayRooms[i].GetComponent<roomScript>();
				if (component.taskID == -1 && component.myID >= 100 && component.typ != 0 && component.typ != 9 && component.typ != 11 && component.typ != 12 && component.typ != 15)
				{
					this.uiObjects[281].GetComponent<Image>().color = this.colors[5];
					this.uiObjects[281].GetComponent<Animation>().Play();
					return;
				}
			}
		}
		this.uiObjects[281].GetComponent<Image>().color = this.colors[6];
		this.uiObjects[281].GetComponent<Animation>().Stop();
	}

	
	public void BUTTON_NewsSetting()
	{
		this.sfx_.PlaySound(3, true);
		this.OpenMenu(false);
		this.uiObjects[168].SetActive(true);
	}

	
	public void BUTTON_Options()
	{
		this.sfx_.PlaySound(3, true);
		this.OpenMenu(false);
		this.uiObjects[155].SetActive(true);
	}

	
	public void BUTTON_UpdateAllObjects()
	{
		this.sfx_.PlaySound(3, true);
		this.OpenMenu(false);
		this.uiObjects[342].SetActive(true);
	}

	
	public void BUTTON_Money()
	{
		this.sfx_.PlaySound(3, true);
		this.OpenMenu(false);
		this.uiObjects[132].SetActive(true);
	}

	
	public void BUTTON_Trend()
	{
		this.sfx_.PlaySound(3, true);
		this.OpenMenu(false);
		this.uiObjects[280].SetActive(true);
		this.uiObjects[280].GetComponent<Menu_Stats_GenreBeliebtheit>().closeMenu = true;
	}

	
	public void BUTTON_RoomNoJob()
	{
		this.sfx_.PlaySound(3, true);
		if (this.roomNoJobOld >= this.mS_.arrayRooms.Length)
		{
			this.roomNoJobOld = 0;
		}
		for (int i = this.roomNoJobOld; i < this.mS_.arrayRooms.Length; i++)
		{
			if (this.mS_.arrayRooms[i])
			{
				roomScript component = this.mS_.arrayRooms[i].GetComponent<roomScript>();
				if (component.taskID == -1 && component.myID >= 100 && component.typ != 0 && component.typ != 9 && component.typ != 11 && component.typ != 12 && component.typ != 15)
				{
					this.roomNoJobOld = i + 1;
					this.camera_.transform.parent.position = new Vector3(component.uiPos.x, this.camera_.transform.parent.position.y, component.uiPos.z);
					return;
				}
			}
		}
	}

	
	public void BUTTON_MitarbeiterNoJob()
	{
		this.sfx_.PlaySound(3, true);
		GameObject[] array = GameObject.FindGameObjectsWithTag("Character");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				characterScript component = array[i].GetComponent<characterScript>();
				if (component && component.roomID == -1)
				{
					this.pcS_.PickFromExternObject(array[i]);
				}
			}
		}
	}

	
	public void BUTTON_GameSpeed(float f)
	{
		this.FindScripts();
		this.spacePause = false;
		if (this.mS_.multiplayer)
		{
			if (this.mpCalls_.isClient)
			{
				return;
			}
			if (this.mpCalls_.isServer)
			{
				for (int i = 0; i < this.mS_.mpCalls_.players.Length; i++)
				{
					if (this.mS_.mpCalls_.players[i] && !this.mS_.mpCalls_.GetAllPlayersReady())
					{
						this.uiObjects[210].SetActive(true);
						return;
					}
				}
				this.mpCalls_.SERVER_Send_GameSpeed(Mathf.RoundToInt(f));
				this.mS_.SetGameSpeed(f);
			}
		}
		if (!this.menuOpen)
		{
			this.mS_.PauseGame(false);
			this.uiObjects[1].GetComponent<GUI_MainButtons>().CloseAllDropdowns();
			this.CloseAllRoomButtons();
			this.mS_.SetGameSpeed(f);
			if (!this.mS_.settings_TutorialOff && f == 3f)
			{
				this.SetTutorialStep(13);
			}
		}
	}

	
	public bool IsRoomMenuOpen()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("RoomButton");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] && array[i].GetComponent<roomButtonScript>().IsMenuOpen())
			{
				return true;
			}
		}
		return false;
	}

	
	public void CreatePerkIconsNewGame(Menu_NewGameCEO script_, Transform parentTransform)
	{
		for (int i = 0; i < parentTransform.childCount; i++)
		{
			UnityEngine.Object.Destroy(parentTransform.GetChild(i).gameObject);
		}
		for (int j = 0; j < script_.perks.Length; j++)
		{
			if (script_.perks[j] && this.uiPerks[j])
			{
				UnityEngine.Object.Instantiate<GameObject>(this.uiPerks[j], parentTransform);
			}
		}
	}

	
	public void CreatePerkIcons(characterScript cS_, Transform parentTransform)
	{
		for (int i = 0; i < parentTransform.childCount; i++)
		{
			UnityEngine.Object.Destroy(parentTransform.GetChild(i).gameObject);
		}
		for (int j = 0; j < cS_.perks.Length; j++)
		{
			if (cS_.perks[j] && this.uiPerks[j])
			{
				UnityEngine.Object.Instantiate<GameObject>(this.uiPerks[j], parentTransform);
			}
		}
	}

	
	public void CreatePerkIconsArbeitsmarkt(charArbeitsmarkt cS_, Transform parentTransform)
	{
		for (int i = 0; i < parentTransform.childCount; i++)
		{
			UnityEngine.Object.Destroy(parentTransform.GetChild(i).gameObject);
		}
		for (int j = 0; j < cS_.perks.Length; j++)
		{
			if (cS_.perks[j])
			{
				UnityEngine.Object.Instantiate<GameObject>(this.uiPerks[j], parentTransform);
			}
		}
	}

	
	public void CloseAllRoomButtons()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("RoomButton");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				array[i].GetComponent<roomButtonScript>().CloseAllMenus();
			}
		}
	}

	
	public void DROPDOWN_BuildRoom(int i)
	{
		switch (i)
		{
		case 1:
			if (!this.unlock_.Get(0))
			{
				return;
			}
			break;
		case 2:
			if (!this.unlock_.Get(2))
			{
				return;
			}
			break;
		case 3:
			if (!this.forschungSonstiges_.IsErforscht(28))
			{
				return;
			}
			break;
		case 4:
			if (!this.forschungSonstiges_.IsErforscht(31))
			{
				return;
			}
			break;
		case 5:
			if (!this.forschungSonstiges_.IsErforscht(32))
			{
				return;
			}
			break;
		case 6:
			if (!this.forschungSonstiges_.IsErforscht(30))
			{
				return;
			}
			break;
		case 7:
			if (!this.forschungSonstiges_.IsErforscht(29))
			{
				return;
			}
			break;
		case 8:
			if (!this.forschungSonstiges_.IsErforscht(39))
			{
				return;
			}
			break;
		case 9:
			if (!this.forschungSonstiges_.IsErforscht(33))
			{
				return;
			}
			break;
		case 10:
			if (!this.unlock_.Get(8))
			{
				return;
			}
			break;
		case 11:
			if (!this.unlock_.Get(14))
			{
				return;
			}
			break;
		case 12:
			if (!this.unlock_.Get(13))
			{
				return;
			}
			break;
		case 13:
			if (!this.forschungSonstiges_.IsErforscht(34))
			{
				return;
			}
			break;
		case 14:
			if (!this.forschungSonstiges_.IsErforscht(33))
			{
				return;
			}
			break;
		case 15:
			if (!this.unlock_.Get(9))
			{
				return;
			}
			break;
		case 17:
			if (!this.forschungSonstiges_.IsErforscht(38))
			{
				return;
			}
			break;
		}
		this.ActivateMenu(this.uiObjects[19]);
		this.uiObjects[19].GetComponent<Menu_BuildRoom>().BUTTON_SelectRoom(i);
		this.OpenMenu(true);
	}

	
	public void DROPDOWN_BuyInventar(int i)
	{
		switch (i)
		{
		case 1:
			if (!this.unlock_.Get(0))
			{
				return;
			}
			break;
		case 2:
			if (!this.unlock_.Get(2))
			{
				return;
			}
			break;
		case 3:
			if (!this.forschungSonstiges_.IsErforscht(28))
			{
				return;
			}
			break;
		case 4:
			if (!this.forschungSonstiges_.IsErforscht(31))
			{
				return;
			}
			break;
		case 5:
			if (!this.forschungSonstiges_.IsErforscht(32))
			{
				return;
			}
			break;
		case 6:
			if (!this.forschungSonstiges_.IsErforscht(30))
			{
				return;
			}
			break;
		case 7:
			if (!this.forschungSonstiges_.IsErforscht(29))
			{
				return;
			}
			break;
		case 8:
			if (!this.forschungSonstiges_.IsErforscht(39))
			{
				return;
			}
			break;
		case 9:
			if (!this.forschungSonstiges_.IsErforscht(33))
			{
				return;
			}
			break;
		case 10:
			if (!this.unlock_.Get(8))
			{
				return;
			}
			break;
		case 11:
			if (!this.unlock_.Get(14))
			{
				return;
			}
			break;
		case 12:
			if (!this.unlock_.Get(13))
			{
				return;
			}
			break;
		case 13:
			if (!this.forschungSonstiges_.IsErforscht(34))
			{
				return;
			}
			break;
		case 14:
			if (!this.forschungSonstiges_.IsErforscht(33))
			{
				return;
			}
			break;
		case 15:
			if (!this.unlock_.Get(9))
			{
				return;
			}
			break;
		case 17:
			if (!this.forschungSonstiges_.IsErforscht(38))
			{
				return;
			}
			break;
		}
		this.ActivateMenu(this.uiObjects[20]);
		this.uiObjects[20].GetComponent<Menu_BuyInventar>().BUTTON_SelectInventar(i);
		this.OpenMenu(true);
	}

	
	public void DROPDOWN_PersonalUebersicht()
	{
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[29]);
		this.OpenMenu(false);
	}

	
	public void DROPDOWN_PreisUndPackung()
	{
		if (!this.forschungSonstiges_.IsErforscht(33))
		{
			return;
		}
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[220]);
		this.uiObjects[220].GetComponent<Menu_PackungSelect>().Init(null);
		this.OpenMenu(false);
	}

	
	public void DROPDOWN_PublishingOffer()
	{
		if (!this.forschungSonstiges_.IsErforscht(33))
		{
			return;
		}
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[349]);
		this.uiObjects[349].GetComponent<Menu_PublishingOfferSelect>().Init();
		this.OpenMenu(false);
	}

	
	public void DROPDOWN_BudgetGame()
	{
		if (!this.forschungSonstiges_.IsErforscht(33))
		{
			return;
		}
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[227]);
		this.uiObjects[227].GetComponent<Menu_BudgetSelect>().Init();
		this.OpenMenu(false);
	}

	
	public void DROPDOWN_GotyGame()
	{
		if (!this.forschungSonstiges_.IsErforscht(33))
		{
			return;
		}
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[274]);
		this.uiObjects[274].GetComponent<Menu_GOTYSelect>().Init();
		this.OpenMenu(false);
	}

	
	public void DROPDOWN_Bundle()
	{
		if (!this.forschungSonstiges_.IsErforscht(33))
		{
			return;
		}
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[267]);
		this.OpenMenu(false);
	}

	
	public void DROPDOWN_BundleAddon()
	{
		if (!this.forschungSonstiges_.IsErforscht(33))
		{
			return;
		}
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[271]);
		this.OpenMenu(false);
	}

	
	public void DROPDOWN_RemoveGameFromMarket()
	{
		if (this.forschungSonstiges_.IsErforscht(33) || this.forschungSonstiges_.IsErforscht(38) || this.unlock_.Get(65))
		{
			this.sfx_.PlaySound(3, false);
			this.ActivateMenu(this.uiObjects[223]);
			this.uiObjects[223].GetComponent<Menu_RemoveGameSelect>().Init();
			this.OpenMenu(false);
		}
	}

	
	public void DROPDOWN_InAppPurchaseVerwalten()
	{
		if (!this.gF_.IsErforscht(57))
		{
			return;
		}
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[277]);
		this.OpenMenu(false);
	}

	
	public void DROPDOWN_HandyPreise()
	{
		if (!this.unlock_.Get(65))
		{
			return;
		}
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[308]);
		this.OpenMenu(false);
	}

	
	public void DROPDOWN_ArcadePreise()
	{
		if (!this.forschungSonstiges_.IsErforscht(38))
		{
			return;
		}
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[309]);
		this.OpenMenu(false);
	}

	
	public void DROPDOWN_KonsolePreis()
	{
		if (!this.forschungSonstiges_.IsErforscht(39))
		{
			return;
		}
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[330]);
		this.OpenMenu(false);
	}

	
	public void DROPDOWN_KonsoleVomMarktNehmen()
	{
		if (!this.forschungSonstiges_.IsErforscht(39))
		{
			return;
		}
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[331]);
		this.OpenMenu(false);
	}

	
	public void DROPDOWN_Personaleinstellungen()
	{
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[197]);
		this.OpenMenu(false);
	}

	
	public void DROPDOWN_Arbeitsmarkt()
	{
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[30]);
		this.OpenMenu(false);
	}

	
	public void DROPDOWN_PersonalGroups()
	{
		if (!this.uiObjects[1].transform.GetChild(0).GetComponent<Button>().IsInteractable())
		{
			return;
		}
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[32]);
		this.OpenMenu(false);
	}

	
	public void DROPDOWN_Fanshop()
	{
		if (!this.forschungSonstiges_.IsErforscht(29))
		{
			return;
		}
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[366]);
		this.OpenMenu(false);
	}

	
	public void DROPDOWN_Bank()
	{
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[138]);
		this.OpenMenu(false);
	}

	
	public void DROPDOWN_Archiv()
	{
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[289]);
		this.OpenMenu(false);
	}

	
	public void DROPDOWN_Marktforschung()
	{
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[230]);
		this.uiObjects[230].GetComponent<Menu_Marktforschung>().Init(null);
		this.OpenMenu(false);
	}

	
	public void DROPDOWN_Multiplayer()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[254]);
		this.OpenMenu(false);
	}

	
	public void DROPDOWN_BuyDevKits()
	{
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[33]);
		this.OpenMenu(false);
	}

	
	public void DROPDOWN_BuyEngine()
	{
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[42]);
		this.OpenMenu(false);
	}

	
	public void DROPDOWN_BuyCopyProtect()
	{
		if (!this.unlock_.Get(31))
		{
			return;
		}
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[49]);
		this.OpenMenu(false);
	}

	
	public void DROPDOWN_BuyAntiCheat()
	{
		if (!this.unlock_.Get(64))
		{
			return;
		}
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[234]);
		this.OpenMenu(false);
	}

	
	public void DROPDOWN_BuyLicence()
	{
		if (!this.unlock_.Get(25))
		{
			return;
		}
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[51]);
		this.OpenMenu(false);
	}

	
	public void DROPDOWN_PublisherExklusivvertrag()
	{
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[200]);
		this.OpenMenu(false);
	}

	
	public void DROPDOWN_Firmenname()
	{
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[47]);
		this.OpenMenu(false);
	}

	
	public void DROPDOWN_Achivements()
	{
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[364]);
		this.OpenMenu(false);
	}

	
	public void DROPDOWN_Statstics_Charts()
	{
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[114]);
		this.OpenMenu(false);
	}

	
	public void DROPDOWN_Statstics_Platforms()
	{
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[117]);
		this.OpenMenu(false);
	}

	
	public void DROPDOWN_Statstics_DevPubs()
	{
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[118]);
		this.OpenMenu(false);
	}

	
	public void DROPDOWN_Statstics_Engines()
	{
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[121]);
		this.OpenMenu(false);
	}

	
	public void DROPDOWN_Statstics_Tochterfirmen()
	{
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[385]);
		this.OpenMenu(false);
	}

	
	public void DROPDOWN_Statstics_MyGames()
	{
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[123]);
		this.OpenMenu(false);
	}

	
	public void DROPDOWN_Statstics_MyKonsolen()
	{
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[334]);
		this.OpenMenu(false);
	}

	
	public void DROPDOWN_Statstics_Infos()
	{
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[126]);
		this.OpenMenu(false);
	}

	
	public void DROPDOWN_Statstics_Finanzen()
	{
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[131]);
		this.OpenMenu(false);
	}

	
	public void BUTTON_CloseOpenAllGameTabs()
	{
		this.sfx_.PlaySound(3, true);
		for (int i = 0; i < this.uiObjects[81].transform.childCount; i++)
		{
			if (this.uiObjects[81].transform.GetChild(i).GetComponent<gameTab>())
			{
				this.uiObjects[81].transform.GetChild(i).GetComponent<gameTab>().fullView = this.gameTabsFullView;
				this.uiObjects[81].transform.GetChild(i).GetComponent<gameTab>().BUTTON_Minimize();
			}
			if (this.uiObjects[81].transform.GetChild(i).GetComponent<konsoleTab>())
			{
				this.uiObjects[81].transform.GetChild(i).GetComponent<konsoleTab>().fullView = this.gameTabsFullView;
				this.uiObjects[81].transform.GetChild(i).GetComponent<konsoleTab>().BUTTON_Minimize();
			}
		}
		this.gameTabsFullView = !this.gameTabsFullView;
	}

	
	public void BUTTON_GameTabFilter()
	{
		if (!this.uiObjects[358].activeSelf)
		{
			this.sfx_.PlaySound(3, true);
			this.uiObjects[358].GetComponent<Menu_GameTabFilter>().Init(this.menuOpen);
			this.ActivateMenu(this.uiObjects[358]);
			this.OpenMenu(false);
		}
	}

	
	public void BUTTON_DeleteAllLeftNews()
	{
		this.sfx_.PlaySound(3, true);
		for (int i = 1; i < this.uiObjects[23].transform.childCount; i++)
		{
			Transform child = this.uiObjects[23].transform.GetChild(i);
			if (child && child.GetComponent<LeftNews>())
			{
				UnityEngine.Object.Destroy(child.gameObject);
			}
		}
	}

	
	public taskMitarbeitersuche AddTask_Mitarbeitersuche()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[25]).GetComponent<taskMitarbeitersuche>();
	}

	
	public taskKonsole AddTask_Konsole()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[22]).GetComponent<taskKonsole>();
	}

	
	public taskArcadeProduction AddTask_ArcadeProduction()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[21]).GetComponent<taskArcadeProduction>();
	}

	
	public taskF2PUpdate AddTask_F2PUpdate()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[20]).GetComponent<taskF2PUpdate>();
	}

	
	public taskMarketingSpezial AddTask_MarketingSpezial()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[19]).GetComponent<taskMarketingSpezial>();
	}

	
	public taskPolishing AddTask_Polishing()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[18]).GetComponent<taskPolishing>();
	}

	
	public taskMarktforschung AddTask_Marktforschung()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[17]).GetComponent<taskMarktforschung>();
	}

	
	public taskProduction AddTask_Production()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[16]).GetComponent<taskProduction>();
	}

	
	public taskSpielbericht AddTask_Spielbericht()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[15]).GetComponent<taskSpielbericht>();
	}

	
	public taskAnimationVerbessern AddTask_AnimationVerbessern()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[14]).GetComponent<taskAnimationVerbessern>();
	}

	
	public taskSoundVerbessern AddTask_SoundVerbessern()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[13]).GetComponent<taskSoundVerbessern>();
	}

	
	public taskGrafikVerbessern AddTask_GrafikVerbessern()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[12]).GetComponent<taskGrafikVerbessern>();
	}

	
	public taskGameplayVerbessern AddTask_GameplayVerbessern()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[11]).GetComponent<taskGameplayVerbessern>();
	}

	
	public taskUpdate AddTask_Update()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[7]).GetComponent<taskUpdate>();
	}

	
	public taskBugfixing AddTask_Bugfixing()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[10]).GetComponent<taskBugfixing>();
	}

	
	public taskForschung AddTask_Forschung()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[0]).GetComponent<taskForschung>();
	}

	
	public taskEngine AddTask_Engine()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[1]).GetComponent<taskEngine>();
	}

	
	public taskGame AddTask_Game()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[2]).GetComponent<taskGame>();
	}

	
	public taskUnterstuetzen AddTask_Unterstuetzen()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[3]).GetComponent<taskUnterstuetzen>();
	}

	
	public taskMarketing AddTask_Marketing()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[4]).GetComponent<taskMarketing>();
	}

	
	public taskFankampagne AddTask_Fankampagne()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[8]).GetComponent<taskFankampagne>();
	}

	
	public taskSupport AddTask_Support()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[9]).GetComponent<taskSupport>();
	}

	
	public taskFanshop AddTask_Fanshop()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[26]).GetComponent<taskFanshop>();
	}

	
	public taskTraining AddTask_Training()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[5]).GetComponent<taskTraining>();
	}

	
	public taskContractWork AddTask_ContractWork()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[6]).GetComponent<taskContractWork>();
	}

	
	public taskContractWait AddTask_ContractWait()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[23]).GetComponent<taskContractWait>();
	}

	
	public taskWait AddTask_Wait()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[24]).GetComponent<taskWait>();
	}

	
	public Sprite Get3Stars(int i)
	{
		return this.uiSprites[45 + i];
	}

	
	public void DrawIpBekanntheit(GameObject element, gameScript gS_)
	{
		int num = 0;
		if (gS_)
		{
			num = Mathf.RoundToInt(gS_.GetIpBekanntheit() * 2f);
		}
		for (int i = 0; i < element.transform.childCount; i++)
		{
			element.transform.GetChild(i).GetComponent<Image>().fillAmount = 0f;
		}
		if (num >= 1)
		{
			element.transform.GetChild(0).GetComponent<Image>().fillAmount = 0.5f;
		}
		if (num >= 2)
		{
			element.transform.GetChild(0).GetComponent<Image>().fillAmount = 1f;
		}
		if (num >= 3)
		{
			element.transform.GetChild(1).GetComponent<Image>().fillAmount = 0.5f;
		}
		if (num >= 4)
		{
			element.transform.GetChild(1).GetComponent<Image>().fillAmount = 1f;
		}
		if (num >= 5)
		{
			element.transform.GetChild(2).GetComponent<Image>().fillAmount = 0.5f;
		}
		if (num >= 6)
		{
			element.transform.GetChild(2).GetComponent<Image>().fillAmount = 1f;
		}
		if (num >= 7)
		{
			element.transform.GetChild(3).GetComponent<Image>().fillAmount = 0.5f;
		}
		if (num >= 8)
		{
			element.transform.GetChild(3).GetComponent<Image>().fillAmount = 1f;
		}
		if (num >= 9)
		{
			element.transform.GetChild(4).GetComponent<Image>().fillAmount = 0.5f;
		}
		if (num >= 10)
		{
			element.transform.GetChild(4).GetComponent<Image>().fillAmount = 1f;
		}
	}

	
	public void DrawStars10_Color(GameObject element, int b, Color color_)
	{
		for (int i = 0; i < element.transform.childCount; i++)
		{
			element.transform.GetChild(i).GetComponent<Image>().fillAmount = 0f;
			element.transform.GetChild(i).GetComponent<Image>().color = color_;
		}
		if (b >= 1)
		{
			element.transform.GetChild(0).GetComponent<Image>().fillAmount = 0.5f;
		}
		if (b >= 2)
		{
			element.transform.GetChild(0).GetComponent<Image>().fillAmount = 1f;
		}
		if (b >= 3)
		{
			element.transform.GetChild(1).GetComponent<Image>().fillAmount = 0.5f;
		}
		if (b >= 4)
		{
			element.transform.GetChild(1).GetComponent<Image>().fillAmount = 1f;
		}
		if (b >= 5)
		{
			element.transform.GetChild(2).GetComponent<Image>().fillAmount = 0.5f;
		}
		if (b >= 6)
		{
			element.transform.GetChild(2).GetComponent<Image>().fillAmount = 1f;
		}
		if (b >= 7)
		{
			element.transform.GetChild(3).GetComponent<Image>().fillAmount = 0.5f;
		}
		if (b >= 8)
		{
			element.transform.GetChild(3).GetComponent<Image>().fillAmount = 1f;
		}
		if (b >= 9)
		{
			element.transform.GetChild(4).GetComponent<Image>().fillAmount = 0.5f;
		}
		if (b >= 10)
		{
			element.transform.GetChild(4).GetComponent<Image>().fillAmount = 1f;
		}
	}

	
	public void DrawStarsColor(GameObject element, int i, Color color_)
	{
		element.transform.GetChild(0).GetComponent<Image>().color = Color.black;
		element.transform.GetChild(1).GetComponent<Image>().color = Color.black;
		element.transform.GetChild(2).GetComponent<Image>().color = Color.black;
		element.transform.GetChild(3).GetComponent<Image>().color = Color.black;
		element.transform.GetChild(4).GetComponent<Image>().color = Color.black;
		if (i >= 1)
		{
			element.transform.GetChild(0).GetComponent<Image>().color = color_;
		}
		if (i >= 2)
		{
			element.transform.GetChild(1).GetComponent<Image>().color = color_;
		}
		if (i >= 3)
		{
			element.transform.GetChild(2).GetComponent<Image>().color = color_;
		}
		if (i >= 4)
		{
			element.transform.GetChild(3).GetComponent<Image>().color = color_;
		}
		if (i >= 5)
		{
			element.transform.GetChild(4).GetComponent<Image>().color = color_;
		}
	}

	
	public void DrawStars(GameObject element, int i)
	{
		element.transform.GetChild(0).GetComponent<Image>().color = Color.black;
		element.transform.GetChild(1).GetComponent<Image>().color = Color.black;
		element.transform.GetChild(2).GetComponent<Image>().color = Color.black;
		element.transform.GetChild(3).GetComponent<Image>().color = Color.black;
		element.transform.GetChild(4).GetComponent<Image>().color = Color.black;
		if (i >= 1)
		{
			element.transform.GetChild(0).GetComponent<Image>().color = this.colors[0];
		}
		if (i >= 2)
		{
			element.transform.GetChild(1).GetComponent<Image>().color = this.colors[0];
		}
		if (i >= 3)
		{
			element.transform.GetChild(2).GetComponent<Image>().color = this.colors[0];
		}
		if (i >= 4)
		{
			element.transform.GetChild(3).GetComponent<Image>().color = this.colors[0];
		}
		if (i >= 5)
		{
			element.transform.GetChild(4).GetComponent<Image>().color = this.colors[0];
		}
	}

	
	private IEnumerator IE_CreateStars(GameObject element, int i)
	{
		yield return new WaitForSeconds(UnityEngine.Random.Range(0f, 0.3f));
		UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[21], new Vector3(0f, 0f, 0f), Quaternion.identity, element.transform);
		UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[21], new Vector3(0f, 0f, 0f), Quaternion.identity, element.transform);
		UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[21], new Vector3(0f, 0f, 0f), Quaternion.identity, element.transform);
		UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[21], new Vector3(0f, 0f, 0f), Quaternion.identity, element.transform);
		UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[21], new Vector3(0f, 0f, 0f), Quaternion.identity, element.transform);
		this.DrawStars(element, i);
		yield break;
	}

	
	public void DisableTabs(GameObject p)
	{
		for (int i = 0; i < p.transform.childCount; i++)
		{
			p.transform.GetChild(i).GetComponent<Image>().color = Color.white;
		}
	}

	
	public void SetTab(GameObject p, int t)
	{
		this.DisableTabs(p);
		p.transform.GetChild(t).GetComponent<Image>().color = this.colors[1];
	}

	
	public void ShowNoMoney()
	{
		this.sfx_.PlaySound(29, false);
		this.uiObjects[34].SetActive(true);
	}

	
	public void ShowGameHasSaved()
	{
		this.sfx_.PlaySound(49, false);
		this.uiObjects[158].SetActive(true);
	}

	
	public void MessageBox(string c, bool closeMenu)
	{
		this.ActivateMenu(this.uiObjects[40]);
		this.uiObjects[40].GetComponent<Menu_Messagebox>().Init(c, closeMenu);
	}

	
	public void MessageBoxSave(int menuID, bool closeMenu)
	{
		this.ActivateMenu(this.uiObjects[297]);
		this.uiObjects[297].GetComponent<Menu_MessageBoxSave>().Init(menuID, closeMenu);
	}

	
	public void UnlockBox(string c, bool closeMenu)
	{
		this.OpenMenu(false);
		this.ActivateMenu(this.uiObjects[170]);
		this.uiObjects[170].GetComponent<Menu_Unlock>().Init(c, closeMenu);
	}

	
	public void KeinEintrag(GameObject content, GameObject go)
	{
		base.StartCoroutine(this.IE_KeinEintrag(content, go));
	}

	
	private IEnumerator IE_KeinEintrag(GameObject content, GameObject go)
	{
		yield return new WaitForEndOfFrame();
		if (content.transform.childCount <= 0)
		{
			go.SetActive(true);
		}
		else
		{
			go.SetActive(false);
		}
		yield break;
	}

	
	public void InitVectrocity()
	{
		this.initVectrocity = true;
		VectorLine.SetEndCap("Arrows", EndCap.Both, new Texture2D[]
		{
			this.unterstuetzenLine[0],
			this.unterstuetzenLine[1],
			this.unterstuetzenLine[2]
		});
		VectorLine.SetEndCap("ArrowsCharRoom", EndCap.Both, new Texture2D[]
		{
			this.unterstuetzenLine[3],
			this.unterstuetzenLine[4],
			this.unterstuetzenLine[5]
		});
		VectorLine.SetEndCap("RoomLine", EndCap.Both, new Texture2D[]
		{
			this.unterstuetzenLine[6],
			this.unterstuetzenLine[7],
			this.unterstuetzenLine[8]
		});
		VectorLine.SetEndCap("VerschiebeTask", EndCap.Both, new Texture2D[]
		{
			this.unterstuetzenLine[9],
			this.unterstuetzenLine[10],
			this.unterstuetzenLine[11]
		});
	}

	
	public void RemoveVectrocity()
	{
		this.initVectrocity = false;
		VectorLine.SetEndCap("Arrows", EndCap.None);
		VectorLine.SetEndCap("ArrowsCharRoom", EndCap.None);
		VectorLine.SetEndCap("RoomLine", EndCap.None);
		VectorLine.SetEndCap("VerschiebeTask", EndCap.None);
	}

	
	public void UpdateFilterToggles()
	{
		if (!this.filterGameObject_)
		{
			this.filterGameObject_ = GameObject.Find("FilterView");
			if (!this.filterGameObject_)
			{
				return;
			}
		}
		if (this.uiObjects[85].GetComponent<Toggle>().isOn && this.filterToggles != 0)
		{
			this.filterToggles = 0;
			this.uiObjects[86].GetComponent<Toggle>().isOn = false;
			this.uiObjects[87].GetComponent<Toggle>().isOn = false;
			this.filterGameObject_.SetActive(true);
			this.mapScript_.UpdateMapFilter(true);
			this.mS_.DrawFilter(this.filterToggles, true);
			this.mS_.SetAllFloorTextures(1);
			return;
		}
		if (this.uiObjects[86].GetComponent<Toggle>().isOn && this.filterToggles != 1)
		{
			this.filterToggles = 1;
			this.uiObjects[85].GetComponent<Toggle>().isOn = false;
			this.uiObjects[87].GetComponent<Toggle>().isOn = false;
			this.filterGameObject_.SetActive(true);
			this.mapScript_.UpdateMapMuell(true);
			this.mS_.DrawFilter(this.filterToggles, true);
			this.mS_.SetAllFloorTextures(1);
			return;
		}
		if (this.uiObjects[87].GetComponent<Toggle>().isOn && this.filterToggles != 2)
		{
			this.filterToggles = 2;
			this.uiObjects[85].GetComponent<Toggle>().isOn = false;
			this.uiObjects[86].GetComponent<Toggle>().isOn = false;
			this.filterGameObject_.SetActive(true);
			this.mapScript_.UpdateMapFilter(true);
			this.mS_.DrawFilter(this.filterToggles, true);
			this.mS_.SetAllFloorTextures(1);
			return;
		}
		if (!this.uiObjects[85].GetComponent<Toggle>().isOn && !this.uiObjects[86].GetComponent<Toggle>().isOn && !this.uiObjects[87].GetComponent<Toggle>().isOn && this.filterToggles != -1)
		{
			this.filterToggles = -1;
			this.filterGameObject_.SetActive(false);
			this.mS_.SetAllFloorTextures(0);
		}
	}

	
	public void ShowFanLetter(int i, string gameName)
	{
		if (!this.uiObjects[113].activeSelf)
		{
			string text = this.tS_.GetFanLetter(i);
			text = text.Replace("<NAME>", gameName);
			this.uiObjects[113].SetActive(true);
			this.uiObjects[113].transform.GetChild(0).GetComponent<Text>().text = text;
			this.sfx_.PlaySound(43);
			base.StartCoroutine(this.DeactivateFanLetter());
		}
	}

	
	private IEnumerator DeactivateFanLetter()
	{
		yield return new WaitForSeconds(this.settings_.fanletterTime);
		this.uiObjects[113].SetActive(false);
		yield break;
	}

	
	public void ShowInGameUI(bool show)
	{
		this.uiObjects[1].SetActive(show);
		this.uiObjects[145].SetActive(show);
		this.uiObjects[146].SetActive(show);
		this.uiObjects[147].SetActive(show);
		this.uiObjects[164].SetActive(show);
		this.uiObjects[165].SetActive(show);
		this.uiObjects[166].SetActive(show);
		this.uiObjects[167].SetActive(show);
	}

	
	private void UpdateUIHotkey()
	{
		if (this.UIHotkey)
		{
			this.UIHotkey.GetComponent<Button>().onClick.Invoke();
			this.UIHotkey = null;
			this.UIHotkeySiblingIndex = -1;
		}
	}

	
	public void SetUIHotkey(GameObject go)
	{
		if (go.transform.GetSiblingIndex() > this.UIHotkeySiblingIndex)
		{
			this.UIHotkey = go;
		}
	}

	
	public void SelectInputField()
	{
		this.selectInputField = true;
	}

	
	public void DeselectInputField()
	{
		this.selectInputField = false;
	}

	
	public void EVENT_MitarbeiterMotivation()
	{
		this.ActivateMenu(this.uiObjects[184]);
		this.uiObjects[184].GetComponent<Menu_PersonalMotivation>().Init();
		this.OpenMenu(false);
	}

	
	public void AddChat(int id_, string c)
	{
		if (c.Contains("<IMMOBILIE>"))
		{
			c = this.tS_.GetText(1268);
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[15], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[208].transform);
		if (id_ != -1)
		{
			if (id_ != this.mS_.myID)
			{
				gameObject.GetComponent<Text>().text = string.Concat(new string[]
				{
					"<color=cyan>",
					this.mpCalls_.GetPlayerName(id_),
					" [",
					this.mpCalls_.GetCompanyName(id_),
					"]:</color> ",
					c
				});
				return;
			}
			gameObject.GetComponent<Text>().text = string.Concat(new string[]
			{
				"<color=orange>",
				this.mpCalls_.GetPlayerName(id_),
				" [",
				this.mpCalls_.GetCompanyName(id_),
				"]:</color> ",
				c
			});
		}
	}

	
	public void INPUTFIELD_Chat()
	{
		string text = this.uiObjects[209].GetComponent<InputField>().text;
		if (text.Length <= 0)
		{
			return;
		}
		this.uiObjects[209].GetComponent<InputField>().text = "";
		if (this.mpCalls_.isServer)
		{
			this.AddChat(this.mS_.myID, text);
			this.mpCalls_.SERVER_Send_Chat(this.mS_.myID, text);
		}
		if (this.mpCalls_.isClient)
		{
			this.mpCalls_.CLIENT_Send_Chat(text);
		}
	}

	
	public void UpdatePanelMultiplayer()
	{
		if (!this.mS_.multiplayer)
		{
			if (this.uiObjects[206].activeSelf)
			{
				this.uiObjects[206].SetActive(false);
			}
			if (this.uiObjects[207].activeSelf)
			{
				this.uiObjects[207].SetActive(false);
			}
			return;
		}
		if (this.mS_.myID == -1)
		{
			return;
		}
		if (this.uiObjects[206].activeSelf != this.settings_.leaderboard)
		{
			this.uiObjects[206].SetActive(this.settings_.leaderboard);
		}
		if (this.uiObjects[207].activeSelf != this.settings_.chat)
		{
			this.uiObjects[207].SetActive(this.settings_.chat);
		}
		for (int i = 0; i < 4; i++)
		{
			if (this.mpCalls_.playersMP.Count - 1 >= i)
			{
				if (!this.uiObjects[206].transform.GetChild(i).gameObject.activeSelf)
				{
					this.uiObjects[206].transform.GetChild(i).gameObject.SetActive(true);
				}
			}
			else if (this.uiObjects[206].transform.GetChild(i).gameObject.activeSelf)
			{
				this.uiObjects[206].transform.GetChild(i).gameObject.SetActive(false);
			}
		}
		if (this.settings_.leaderboard)
		{
			for (int j = 0; j < this.mpCalls_.playersMP.Count; j++)
			{
				int playerID = this.mpCalls_.playersMP[j].playerID;
				Transform child = this.uiObjects[206].transform.GetChild(j).transform.GetChild(0);
				child.GetComponent<Text>().text = this.mpCalls_.GetCompanyName(playerID);
				ColorBlock colorBlock = child.GetComponent<Button>().colors;
				if (this.mS_.settings_autoPauseForMultiplayer)
				{
					if (this.mpCalls_.GetPause(playerID))
					{
						colorBlock.normalColor = this.colors[19];
					}
					else
					{
						colorBlock.normalColor = Color.white;
					}
				}
				else
				{
					colorBlock.normalColor = Color.white;
				}
				if (playerID == this.mS_.myID)
				{
					colorBlock.normalColor = this.colors[0];
				}
				child.GetComponent<Button>().colors = colorBlock;
				child.GetChild(0).GetComponent<Text>().text = this.mS_.GetMoney(this.mpCalls_.GetMoney(playerID), true);
				if (this.mpCalls_.GetMoney(playerID) >= 0L)
				{
					child.GetChild(0).GetComponent<Text>().color = this.colors[4];
				}
				else
				{
					child.GetChild(0).GetComponent<Text>().color = this.colors[8];
				}
				string text = this.tS_.GetText(763);
				text = text.Replace("<NUM>", this.mS_.GetMoney((long)this.mpCalls_.GetFans(playerID), false));
				child.GetChild(1).GetComponent<Text>().text = text;
				child.GetChild(2).GetComponent<Image>().sprite = this.GetCompanyLogo(this.mpCalls_.GetLogo(playerID));
			}
		}
	}

	
	public void OpenEngineAbrechnung(gameScript script_)
	{
		this.ActivateMenu(this.uiObjects[83]);
		this.OpenMenu(false);
		this.uiObjects[83].GetComponent<Menu_Engine_Abrechnung>().Init(script_);
	}

	
	public void OpenTochterfirmaAbrechnung(gameScript script_)
	{
		this.ActivateMenu(this.uiObjects[398]);
		this.OpenMenu(false);
		this.uiObjects[398].GetComponent<Menu_TochterfirmaAbrechnung>().Init(script_);
	}

	
	public string GetFansTooltip()
	{
		string text = string.Concat(new string[]
		{
			"<b>",
			this.tS_.GetText(97),
			"</b>\n",
			this.tS_.GetText(1160),
			"\n"
		});
		this.SortFans();
		for (int i = 0; i < this.fansSortList.Count; i++)
		{
			if (this.fansSortList[i].fans < 20000000)
			{
				text = string.Concat(new string[]
				{
					text,
					"\n",
					this.genres_.GetName(this.fansSortList[i].myID),
					": <color=blue><b>",
					this.mS_.GetMoney((long)this.fansSortList[i].fans, false),
					"</b></color>"
				});
			}
			else
			{
				text = string.Concat(new string[]
				{
					text,
					"\n",
					this.genres_.GetName(this.fansSortList[i].myID),
					": <color=green><b>",
					this.mS_.GetMoney((long)this.fansSortList[i].fans, false),
					"</b></color>"
				});
			}
		}
		return text;
	}

	
	public void SortFans()
	{
		this.fansSortList.Clear();
		for (int j = 0; j < this.genres_.genres_FANS.Length; j++)
		{
			if (this.genres_.genres_FANS[j] > 0)
			{
				this.fansSortList.Add(new FansSortList(j, this.genres_.genres_FANS[j]));
			}
		}
		this.fansSortList = (from i in this.fansSortList
		orderby i.fans descending
		select i).ToList<FansSortList>();
	}

	
	public void InitToggles()
	{
		if (PlayerPrefs.GetInt("Toggle_Walls") == 1)
		{
			this.uiObjects[241].GetComponent<Toggle>().isOn = true;
		}
		else
		{
			this.uiObjects[241].GetComponent<Toggle>().isOn = false;
		}
		if (PlayerPrefs.GetInt("Toggle_PickChars") == 1)
		{
			this.uiObjects[2].GetComponent<Toggle>().isOn = true;
		}
		else
		{
			this.uiObjects[2].GetComponent<Toggle>().isOn = false;
		}
		if (PlayerPrefs.GetInt("Toggle_RoomUI") == 1)
		{
			this.uiObjects[4].GetComponent<Toggle>().isOn = true;
		}
		else
		{
			this.uiObjects[4].GetComponent<Toggle>().isOn = false;
		}
		if (PlayerPrefs.GetInt("Toggle_Ausstattung") == 1)
		{
			this.uiObjects[85].GetComponent<Toggle>().isOn = true;
		}
		else
		{
			this.uiObjects[85].GetComponent<Toggle>().isOn = false;
		}
		if (PlayerPrefs.GetInt("Toggle_Muell") == 1)
		{
			this.uiObjects[86].GetComponent<Toggle>().isOn = true;
		}
		else
		{
			this.uiObjects[86].GetComponent<Toggle>().isOn = false;
		}
		if (PlayerPrefs.GetInt("Toggle_Waerme") == 1)
		{
			this.uiObjects[87].GetComponent<Toggle>().isOn = true;
		}
		else
		{
			this.uiObjects[87].GetComponent<Toggle>().isOn = false;
		}
		if (PlayerPrefs.GetInt("Toggle_Charts") == 1)
		{
			this.uiObjects[379].GetComponent<Toggle>().isOn = true;
		}
		else
		{
			this.uiObjects[379].GetComponent<Toggle>().isOn = false;
		}
		if (!this.mS_.settings_TutorialOff)
		{
			this.uiObjects[2].GetComponent<Toggle>().isOn = false;
			this.uiObjects[3].GetComponent<Toggle>().isOn = false;
			this.uiObjects[4].GetComponent<Toggle>().isOn = false;
		}
	}

	
	public void TOGGLE_Charts()
	{
		if (this.mS_)
		{
			this.mS_.games_.UpdateChartsWeek();
			this.UpdateCharts();
		}
		if (!this.uiObjects[379].GetComponent<Toggle>().isOn)
		{
			PlayerPrefs.SetInt("Toggle_Charts", 0);
			return;
		}
		PlayerPrefs.SetInt("Toggle_Charts", 1);
	}

	
	public void TOGGLE_GameTab_TABS()
	{
		this.uiObjects[362].GetComponent<GamesGroupContent>().timer = 10f;
	}

	
	public void TOGGLE_Walls()
	{
		this.ShowWalls(this.uiObjects[241].GetComponent<Toggle>().isOn);
		if (!this.uiObjects[241].GetComponent<Toggle>().isOn)
		{
			PlayerPrefs.SetInt("Toggle_Walls", 0);
			return;
		}
		PlayerPrefs.SetInt("Toggle_Walls", 1);
	}

	
	public void TOGGLE_PickChars()
	{
		if (!this.mS_.settings_TutorialOff)
		{
			this.uiObjects[2].GetComponent<Toggle>().isOn = false;
			return;
		}
		if (!this.uiObjects[297].activeSelf && this.uiObjects[2].GetComponent<Toggle>().isOn)
		{
			this.MessageBoxSave(1, true);
		}
		if (!this.uiObjects[2].GetComponent<Toggle>().isOn)
		{
			PlayerPrefs.SetInt("Toggle_PickChars", 0);
			return;
		}
		PlayerPrefs.SetInt("Toggle_PickChars", 1);
	}

	
	public void TOGGLE_PickObjects()
	{
		if (!this.mS_.settings_TutorialOff)
		{
			this.uiObjects[3].GetComponent<Toggle>().isOn = false;
			return;
		}
		if (!this.uiObjects[297].activeSelf && this.uiObjects[3].GetComponent<Toggle>().isOn)
		{
			this.MessageBoxSave(0, true);
		}
		if (!this.uiObjects[3].GetComponent<Toggle>().isOn)
		{
			PlayerPrefs.SetInt("Toggle_PickObjects", 0);
		}
		else
		{
			PlayerPrefs.SetInt("Toggle_PickObjects", 1);
		}
		for (int i = 0; i < this.mS_.arrayObjects.Length; i++)
		{
			if (this.mS_.arrayObjects[i])
			{
				this.mS_.arrayObjects[i].GetComponent<objectScript>().MouseLeave();
			}
		}
	}

	
	public void TOGGLE_RoomUI()
	{
		if (!this.mS_.settings_TutorialOff)
		{
			this.uiObjects[4].GetComponent<Toggle>().isOn = false;
			return;
		}
		if (!this.uiObjects[4].GetComponent<Toggle>().isOn)
		{
			PlayerPrefs.SetInt("Toggle_RoomUI", 0);
			return;
		}
		PlayerPrefs.SetInt("Toggle_RoomUI", 1);
	}

	
	public void TOGGLE_Ausstattung()
	{
		if (!this.uiObjects[85].GetComponent<Toggle>().isOn)
		{
			PlayerPrefs.SetInt("Toggle_Ausstattung", 0);
			return;
		}
		PlayerPrefs.SetInt("Toggle_Ausstattung", 1);
	}

	
	public void TOGGLE_Muell()
	{
		if (!this.uiObjects[86].GetComponent<Toggle>().isOn)
		{
			PlayerPrefs.SetInt("Toggle_Muell", 0);
			return;
		}
		PlayerPrefs.SetInt("Toggle_Muell", 1);
	}

	
	public void TOGGLE_Waerme()
	{
		if (!this.uiObjects[87].GetComponent<Toggle>().isOn)
		{
			PlayerPrefs.SetInt("Toggle_Waerme", 0);
			return;
		}
		PlayerPrefs.SetInt("Toggle_Waerme", 1);
	}

	
	public void ShowWalls(bool show)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("HideWall");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				Vector3 position = array[i].transform.position;
				int num = Mathf.RoundToInt(array[i].transform.eulerAngles.y);
				bool flag = false;
				if (num == 180 && this.mapScript_.mapBuilding[Mathf.RoundToInt(position.x) + 1, Mathf.RoundToInt(position.z)] == 0)
				{
					flag = true;
				}
				if (num == 0 && this.mapScript_.mapBuilding[Mathf.RoundToInt(position.x) - 1, Mathf.RoundToInt(position.z)] == 0)
				{
					flag = true;
				}
				if ((num == -90 || num == 270) && this.mapScript_.mapBuilding[Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.z) - 1] == 0)
				{
					flag = true;
				}
				if (num == 90 && this.mapScript_.mapBuilding[Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.z) + 1] == 0)
				{
					flag = true;
				}
				if ((this.mapScript_.mapBuilding[Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.z)] != 0 && this.mapScript_.mapBuilding[Mathf.RoundToInt(position.x + 1f), Mathf.RoundToInt(position.z)] != 0 && this.mapScript_.mapBuilding[Mathf.RoundToInt(position.x - 1f), Mathf.RoundToInt(position.z)] != 0 && this.mapScript_.mapBuilding[Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.z + 1f)] != 0 && this.mapScript_.mapBuilding[Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.z - 1f)] != 0 && this.mapScript_.mapBuilding[Mathf.RoundToInt(position.x + 1f), Mathf.RoundToInt(position.z + 1f)] != 0 && this.mapScript_.mapBuilding[Mathf.RoundToInt(position.x - 1f), Mathf.RoundToInt(position.z - 1f)] != 0 && this.mapScript_.mapBuilding[Mathf.RoundToInt(position.x + 1f), Mathf.RoundToInt(position.z - 1f)] != 0 && this.mapScript_.mapBuilding[Mathf.RoundToInt(position.x - 1f), Mathf.RoundToInt(position.z + 1f)] != 0) || flag)
				{
					if (!show)
					{
						array[i].transform.position = new Vector3(array[i].transform.position.x, 0f, array[i].transform.position.z);
					}
					else
					{
						array[i].transform.position = new Vector3(array[i].transform.position.x, -1.45f, array[i].transform.position.z);
					}
				}
			}
		}
		array = GameObject.FindGameObjectsWithTag("DoorOben");
		for (int j = 0; j < array.Length; j++)
		{
			if (array[j])
			{
				array[j].GetComponent<MeshRenderer>().enabled = !show;
			}
		}
		array = GameObject.FindGameObjectsWithTag("WallBeton");
		for (int k = 0; k < array.Length; k++)
		{
			if (array[k])
			{
				array[k].GetComponent<MeshRenderer>().enabled = show;
			}
		}
	}

	
	private void LoadPlayerAndCompanyName()
	{
		this.uiObjects[159].GetComponent<Menu_NewGame>().uiObjects[0].GetComponent<InputField>().text = PlayerPrefs.GetString("CompanyName");
		this.uiObjects[201].GetComponent<mpMain>().uiObjects[11].GetComponent<InputField>().text = PlayerPrefs.GetString("PlayerName");
	}

	
	private void UpdateTutorial()
	{
		if (this.mS_.settings_TutorialOff)
		{
			if (this.uiObjects[248].activeSelf)
			{
				this.uiObjects[248].SetActive(false);
			}
			return;
		}
		if (!this.uiObjects[248].activeSelf)
		{
			this.uiObjects[248].SetActive(true);
		}
	}

	
	public void SetTutorialStep(int i)
	{
		if (this.uiObjects[248].GetComponent<Menu_Tutorial>().step == i - 1)
		{
			this.uiObjects[248].GetComponent<Menu_Tutorial>().SetStep(i);
		}
	}

	
	public int GetTutorialStep()
	{
		return this.uiObjects[248].GetComponent<Menu_Tutorial>().step;
	}

	
	public void CameraBlend()
	{
		this.postProcess_.BlendIn();
	}

	
	public Sprite GetCompanyLogo(int i)
	{
		if (i > this.logoSprites.Length)
		{
			return this.logoSprites[0];
		}
		return this.logoSprites[i];
	}

	
	public void UpdateArbeitsmarktIcon()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Arbeitsmarkt");
		if (array.Length != 0)
		{
			if (!this.uiObjects[384].activeSelf)
			{
				this.uiObjects[384].SetActive(true);
			}
			this.uiObjects[384].transform.GetChild(0).gameObject.GetComponent<Text>().text = array.Length.ToString();
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			int num8 = 0;
			for (int i = 0; i < array.Length; i++)
			{
				charArbeitsmarkt component = array[i].GetComponent<charArbeitsmarkt>();
				if (component)
				{
					switch (component.beruf)
					{
					case 0:
						num++;
						break;
					case 1:
						num2++;
						break;
					case 2:
						num3++;
						break;
					case 3:
						num4++;
						break;
					case 4:
						num5++;
						break;
					case 5:
						num6++;
						break;
					case 6:
						num7++;
						break;
					case 7:
						num8++;
						break;
					}
				}
			}
			string text = "<b>" + this.tS_.GetText(191) + "</b>\n\n";
			if (num > 0)
			{
				text = string.Concat(new string[]
				{
					text,
					this.tS_.GetText(137),
					": <color=blue><b>",
					num.ToString(),
					"</b></color>\n"
				});
			}
			if (num2 > 0)
			{
				text = string.Concat(new string[]
				{
					text,
					this.tS_.GetText(138),
					": <color=blue><b>",
					num2.ToString(),
					"</b></color>\n"
				});
			}
			if (num3 > 0)
			{
				text = string.Concat(new string[]
				{
					text,
					this.tS_.GetText(139),
					": <color=blue><b>",
					num3.ToString(),
					"</b></color>\n"
				});
			}
			if (num4 > 0)
			{
				text = string.Concat(new string[]
				{
					text,
					this.tS_.GetText(140),
					": <color=blue><b>",
					num4.ToString(),
					"</b></color>\n"
				});
			}
			if (num5 > 0)
			{
				text = string.Concat(new string[]
				{
					text,
					this.tS_.GetText(141),
					": <color=blue><b>",
					num5.ToString(),
					"</b></color>\n"
				});
			}
			if (num6 > 0)
			{
				text = string.Concat(new string[]
				{
					text,
					this.tS_.GetText(142),
					": <color=blue><b>",
					num6.ToString(),
					"</b></color>\n"
				});
			}
			if (num7 > 0)
			{
				text = string.Concat(new string[]
				{
					text,
					this.tS_.GetText(143),
					": <color=blue><b>",
					num7.ToString(),
					"</b></color>\n"
				});
			}
			if (num8 > 0)
			{
				text = string.Concat(new string[]
				{
					text,
					this.tS_.GetText(144),
					": <color=blue><b>",
					num8.ToString(),
					"</b></color>\n"
				});
			}
			this.uiObjects[384].GetComponent<tooltip>().c = text;
			return;
		}
		if (this.uiObjects[384].activeSelf)
		{
			this.uiObjects[384].SetActive(false);
		}
	}

	
	public GameObject[] uiPrefabs;

	
	public GameObject uiPops;

	
	public GameObject[] uiObjects;

	
	public GameObject[] uiPerks;

	
	public GameObject[] uiTaskPrefabs;

	
	public Sprite[] uiSprites;

	
	public Sprite[] inventarSprites;

	
	public Sprite[] flagSprites;

	
	public Sprite[] logoSprites;

	
	public Sprite[] charIcons;

	
	public Sprite[] iconSupport;

	
	public Sprite[] iconGlobalEvents;

	
	public Sprite[] iconAchivements;

	
	public Sprite[] iconAchivementsOff;

	
	public Color[] colors;

	
	public Color[] colorsBalken;

	
	public Texture2D[] unterstuetzenLine;

	
	public Material matFill_Window;

	
	public bool menuOpen;

	
	public bool disableRoomGUI;

	
	public bool spacePause;

	
	public Camera camera_;

	
	private float durchschnittsMotivation = 100f;

	
	private float timerUpdateTopLeiste;

	
	private float timerUpdateContract;

	
	private float timerUpdateGlobalEvent;

	
	public GameObject hellgikeitsObjekt;

	
	private GameObject main_;

	
	private settingsScript settings_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private sfxScript sfx_;

	
	public GUI_Tooltip guiTooltip;

	
	private objectTooltip objectTooltip;

	
	private genres genres_;

	
	private themes themes_;

	
	private mapScript mapScript_;

	
	private unlockScript unlock_;

	
	private contractWorkMain contractMain_;

	
	private publishingOfferMain publishingOfferMain_;

	
	private pickCharacterScript pcS_;

	
	private mpCalls mpCalls_;

	
	private forschungSonstiges forschungSonstiges_;

	
	private gameplayFeatures gF_;

	
	private PostProcessing postProcess_;

	
	public GUI_MainButtons guiMainButtons_;

	
	private List<GameObject> moneyPopList = new List<GameObject>();

	
	private float fansTimer;

	
	private int fansOld;

	
	private string fansString = "";

	
	public float supportTimer;

	
	private float moneyTimer;

	
	private long moneyOld;

	
	private string moneyString = "";

	
	private tooltip moneyTooltip;

	
	private float updateBankWarningIcon_Timer;

	
	private float updateExklusivVertragIcon_Timer;

	
	private float updateStudioBewertungTimer;

	
	private float timerUpdateMitarbeiterNoJob;

	
	private float timerUpdateRoomNoJob;

	
	private int roomNoJobOld;

	
	private bool gameTabsFullView = true;

	
	public bool initVectrocity;

	
	private GameObject filterGameObject_;

	
	public int filterToggles = -1;

	
	public int UIHotkeySiblingIndex = -1;

	
	public GameObject UIHotkey;

	
	public bool selectInputField;

	
	private List<FansSortList> fansSortList = new List<FansSortList>();
}
