using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Vectrosity;

// Token: 0x0200006B RID: 107
public class GUI_Main : MonoBehaviour
{
	// Token: 0x06000405 RID: 1029 RVA: 0x0003E824 File Offset: 0x0003CA24
	private void Awake()
	{
		this.mpCalls_ = GameObject.Find("NetworkManager").GetComponent<mpCalls>();
		this.InitVectrocity();
	}

	// Token: 0x06000406 RID: 1030 RVA: 0x0003E841 File Offset: 0x0003CA41
	private void Start()
	{
		this.FindScripts();
		this.LoadPlayerAndCompanyName();
	}

	// Token: 0x06000407 RID: 1031 RVA: 0x0003E850 File Offset: 0x0003CA50
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

	// Token: 0x06000408 RID: 1032 RVA: 0x0003E8F3 File Offset: 0x0003CAF3
	public void UpdateOnce()
	{
		this.SetMainGuiData();
		this.UpdateAuftragsansehen(0f);
		this.InitToggles();
	}

	// Token: 0x06000409 RID: 1033 RVA: 0x0003E90C File Offset: 0x0003CB0C
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

	// Token: 0x0600040A RID: 1034 RVA: 0x0003EB5A File Offset: 0x0003CD5A
	public bool IsStartMenuActive()
	{
		return this.uiObjects[151].activeSelf;
	}

	// Token: 0x0600040B RID: 1035 RVA: 0x0003EB6D File Offset: 0x0003CD6D
	public void ActivateMenu(GameObject go)
	{
		if (!go.activeSelf)
		{
			go.SetActive(true);
		}
	}

	// Token: 0x0600040C RID: 1036 RVA: 0x0003EB7E File Offset: 0x0003CD7E
	public void DeactivateMenu(GameObject go)
	{
		if (go.activeSelf)
		{
			go.SetActive(false);
		}
	}

	// Token: 0x0600040D RID: 1037 RVA: 0x0003EB8F File Offset: 0x0003CD8F
	public Vector2 GetAnchoredPosition(Vector2 v)
	{
		return v / this.settings_.uiScale;
	}

	// Token: 0x0600040E RID: 1038 RVA: 0x0003EBA2 File Offset: 0x0003CDA2
	public bool IsMouseOverGUI()
	{
		return EventSystem.current.IsPointerOverGameObject();
	}

	// Token: 0x0600040F RID: 1039 RVA: 0x0003EBAE File Offset: 0x0003CDAE
	public IEnumerator MoneyPopEnumerate(int i, Vector3 pos, bool green)
	{
		yield return new WaitForSeconds(UnityEngine.Random.Range(0f, 1f));
		this.MoneyPop(i, pos, green);
		yield break;
	}

	// Token: 0x06000410 RID: 1040 RVA: 0x0003EBD4 File Offset: 0x0003CDD4
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

	// Token: 0x06000411 RID: 1041 RVA: 0x0003ED77 File Offset: 0x0003CF77
	public void ShowTooltip(string c)
	{
		if (!this.guiTooltip.tooltipEnabled || c != this.guiTooltip.myText.text)
		{
			this.guiTooltip.SetActive(c);
		}
	}

	// Token: 0x06000412 RID: 1042 RVA: 0x0003EDAA File Offset: 0x0003CFAA
	public void DisableTooltip()
	{
		if (this.guiTooltip.tooltipEnabled)
		{
			this.guiTooltip.SetInactive();
		}
	}

	// Token: 0x06000413 RID: 1043 RVA: 0x0003EDC4 File Offset: 0x0003CFC4
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

	// Token: 0x06000414 RID: 1044 RVA: 0x0003EDEE File Offset: 0x0003CFEE
	public void DisableObjectTooltip()
	{
		if (this.objectTooltip.tooltipEnabled)
		{
			this.objectTooltip.SetInactive();
		}
	}

	// Token: 0x06000415 RID: 1045 RVA: 0x0003EE08 File Offset: 0x0003D008
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

	// Token: 0x06000416 RID: 1046 RVA: 0x0003EEE4 File Offset: 0x0003D0E4
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

	// Token: 0x06000417 RID: 1047 RVA: 0x0003EF94 File Offset: 0x0003D194
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

	// Token: 0x06000418 RID: 1048 RVA: 0x0003F0C5 File Offset: 0x0003D2C5
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

	// Token: 0x06000419 RID: 1049 RVA: 0x0003F0D4 File Offset: 0x0003D2D4
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

	// Token: 0x0600041A RID: 1050 RVA: 0x0003F0E4 File Offset: 0x0003D2E4
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

	// Token: 0x0600041B RID: 1051 RVA: 0x0003F1A0 File Offset: 0x0003D3A0
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

	// Token: 0x0600041C RID: 1052 RVA: 0x0003F2A4 File Offset: 0x0003D4A4
	public void UpdateSupportIcon()
	{
		this.supportTimer += Time.deltaTime;
		if (this.supportTimer > 1f)
		{
			this.supportTimer = 0f;
			this.uiObjects[140].GetComponent<Image>().sprite = this.iconSupport[this.mS_.GetAnrufeAmount()];
		}
	}

	// Token: 0x0600041D RID: 1053 RVA: 0x0003F304 File Offset: 0x0003D504
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

	// Token: 0x0600041E RID: 1054 RVA: 0x0003F638 File Offset: 0x0003D838
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

	// Token: 0x0600041F RID: 1055 RVA: 0x0003F8C0 File Offset: 0x0003DAC0
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

	// Token: 0x06000420 RID: 1056 RVA: 0x0003FA3C File Offset: 0x0003DC3C
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

	// Token: 0x06000421 RID: 1057 RVA: 0x000400B4 File Offset: 0x0003E2B4
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

	// Token: 0x06000422 RID: 1058 RVA: 0x0004017C File Offset: 0x0003E37C
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

	// Token: 0x06000423 RID: 1059 RVA: 0x000402A4 File Offset: 0x0003E4A4
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

	// Token: 0x06000424 RID: 1060 RVA: 0x00040558 File Offset: 0x0003E758
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

	// Token: 0x06000425 RID: 1061 RVA: 0x000405C0 File Offset: 0x0003E7C0
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

	// Token: 0x06000426 RID: 1062 RVA: 0x0004067C File Offset: 0x0003E87C
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

	// Token: 0x06000427 RID: 1063 RVA: 0x00040870 File Offset: 0x0003EA70
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

	// Token: 0x06000428 RID: 1064 RVA: 0x000409A0 File Offset: 0x0003EBA0
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

	// Token: 0x06000429 RID: 1065 RVA: 0x00040B84 File Offset: 0x0003ED84
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

	// Token: 0x0600042A RID: 1066 RVA: 0x00040DDC File Offset: 0x0003EFDC
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

	// Token: 0x0600042B RID: 1067 RVA: 0x00041024 File Offset: 0x0003F224
	public void AddHistory(string c)
	{
		this.mS_.history.Add("<b>" + this.GetDate() + "</b>\n" + c);
	}

	// Token: 0x0600042C RID: 1068 RVA: 0x0004104C File Offset: 0x0003F24C
	public void CreateTopNewsStudiobewertung(string text_)
	{
		this.FindScripts();
		this.sfx_.PlaySound(27, true);
		UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[19], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[22].transform).transform.Find("Text").GetComponent<Text>().text = text_;
		this.AddHistory(this.tS_.GetText(1478) + "\n<color=blue>" + text_ + "</color>");
	}

	// Token: 0x0600042D RID: 1069 RVA: 0x000410E4 File Offset: 0x0003F2E4
	public void CreateTopNewsUnlock(string name_, Sprite sprite_)
	{
		this.FindScripts();
		this.sfx_.PlaySound(27, true);
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[13], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[22].transform);
		gameObject.transform.Find("Text").GetComponent<Text>().text = name_;
		gameObject.transform.Find("Icon").GetComponent<Image>().sprite = sprite_;
		this.AddHistory(this.tS_.GetText(527) + "\n<color=blue>" + name_ + "</color>");
	}

	// Token: 0x0600042E RID: 1070 RVA: 0x00041198 File Offset: 0x0003F398
	public void CreateTopNewsInfo(string text_)
	{
		this.FindScripts();
		this.sfx_.PlaySound(27, true);
		UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[12], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[22].transform).transform.Find("Text").GetComponent<Text>().text = text_;
		this.AddHistory(text_);
	}

	// Token: 0x0600042F RID: 1071 RVA: 0x00041210 File Offset: 0x0003F410
	public void CreateTopNewsAchivement(int id_)
	{
		this.FindScripts();
		this.sfx_.PlaySound(27, true);
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[22], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[22].transform);
		string text = "<color=blue>" + this.tS_.GetAchivementName(id_) + "</color>\n";
		text += this.tS_.GetAchivementDesc(id_);
		gameObject.transform.Find("Text").GetComponent<Text>().text = text;
	}

	// Token: 0x06000430 RID: 1072 RVA: 0x000412B0 File Offset: 0x0003F4B0
	public void CreateTopNewsTrend(string name_, Sprite sprite_)
	{
		this.FindScripts();
		this.sfx_.PlaySound(27, true);
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[10], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[22].transform);
		gameObject.transform.Find("Text").GetComponent<Text>().text = name_;
		gameObject.transform.Find("Icon").GetComponent<Image>().sprite = sprite_;
		this.AddHistory(this.tS_.GetText(482) + "\n<color=blue>" + name_ + "</color>");
	}

	// Token: 0x06000431 RID: 1073 RVA: 0x00041364 File Offset: 0x0003F564
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

	// Token: 0x06000432 RID: 1074 RVA: 0x0004140C File Offset: 0x0003F60C
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

	// Token: 0x06000433 RID: 1075 RVA: 0x000414B4 File Offset: 0x0003F6B4
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

	// Token: 0x06000434 RID: 1076 RVA: 0x0004155C File Offset: 0x0003F75C
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

	// Token: 0x06000435 RID: 1077 RVA: 0x00041620 File Offset: 0x0003F820
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

	// Token: 0x06000436 RID: 1078 RVA: 0x000416C8 File Offset: 0x0003F8C8
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

	// Token: 0x06000437 RID: 1079 RVA: 0x00041770 File Offset: 0x0003F970
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

	// Token: 0x06000438 RID: 1080 RVA: 0x00041818 File Offset: 0x0003FA18
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

	// Token: 0x06000439 RID: 1081 RVA: 0x000418DC File Offset: 0x0003FADC
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

	// Token: 0x0600043A RID: 1082 RVA: 0x000419AC File Offset: 0x0003FBAC
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

	// Token: 0x0600043B RID: 1083 RVA: 0x00041A7C File Offset: 0x0003FC7C
	public void CreateTopNewsDevLegend(string text_, int beruf)
	{
		this.FindScripts();
		this.sfx_.PlaySound(27, true);
		UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[8], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[22].transform).transform.Find("Text").GetComponent<Text>().text = text_ + "\n<size=14><color=green>[" + this.tS_.GetText(beruf + 137) + "]</color></size>";
		this.AddHistory(this.tS_.GetText(426) + "\n<color=blue>" + text_ + "</color>");
	}

	// Token: 0x0600043C RID: 1084 RVA: 0x00041B34 File Offset: 0x0003FD34
	public void CreateLeftNews(int roomID_, Sprite sprite_, string tooltip_, Sprite smallSprite)
	{
		this.FindScripts();
		this.sfx_.PlaySound(28, true);
		LeftNews component = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[2], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[23].transform).GetComponent<LeftNews>();
		component.Init(roomID_, sprite_, tooltip_, smallSprite);
		component.Init(roomID_, sprite_, tooltip_, smallSprite);
		this.AddHistory(tooltip_);
	}

	// Token: 0x0600043D RID: 1085 RVA: 0x00041BAC File Offset: 0x0003FDAC
	public string GetDate()
	{
		return this.tS_.GetText(102) + this.mS_.year.ToString() + " " + this.tS_.GetText(103) + this.mS_.month.ToString() + " " + this.tS_.GetText(104) + this.mS_.week.ToString();
	}

	// Token: 0x0600043E RID: 1086 RVA: 0x00041C2C File Offset: 0x0003FE2C
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

	// Token: 0x0600043F RID: 1087 RVA: 0x00041D1C File Offset: 0x0003FF1C
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

	// Token: 0x06000440 RID: 1088 RVA: 0x00041E51 File Offset: 0x00040051
	public void BUTTON_NewsSetting()
	{
		this.sfx_.PlaySound(3, true);
		this.OpenMenu(false);
		this.uiObjects[168].SetActive(true);
	}

	// Token: 0x06000441 RID: 1089 RVA: 0x00041E79 File Offset: 0x00040079
	public void BUTTON_Options()
	{
		this.sfx_.PlaySound(3, true);
		this.OpenMenu(false);
		this.uiObjects[155].SetActive(true);
	}

	// Token: 0x06000442 RID: 1090 RVA: 0x00041EA1 File Offset: 0x000400A1
	public void BUTTON_UpdateAllObjects()
	{
		this.sfx_.PlaySound(3, true);
		this.OpenMenu(false);
		this.uiObjects[342].SetActive(true);
	}

	// Token: 0x06000443 RID: 1091 RVA: 0x00041EC9 File Offset: 0x000400C9
	public void BUTTON_Money()
	{
		this.sfx_.PlaySound(3, true);
		this.OpenMenu(false);
		this.uiObjects[132].SetActive(true);
	}

	// Token: 0x06000444 RID: 1092 RVA: 0x00041EF1 File Offset: 0x000400F1
	public void BUTTON_Trend()
	{
		this.sfx_.PlaySound(3, true);
		this.OpenMenu(false);
		this.uiObjects[280].SetActive(true);
		this.uiObjects[280].GetComponent<Menu_Stats_GenreBeliebtheit>().closeMenu = true;
	}

	// Token: 0x06000445 RID: 1093 RVA: 0x00041F30 File Offset: 0x00040130
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

	// Token: 0x06000446 RID: 1094 RVA: 0x00042050 File Offset: 0x00040250
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

	// Token: 0x06000447 RID: 1095 RVA: 0x000420B8 File Offset: 0x000402B8
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

	// Token: 0x06000448 RID: 1096 RVA: 0x000421CC File Offset: 0x000403CC
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

	// Token: 0x06000449 RID: 1097 RVA: 0x00042210 File Offset: 0x00040410
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

	// Token: 0x0600044A RID: 1098 RVA: 0x0004227C File Offset: 0x0004047C
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

	// Token: 0x0600044B RID: 1099 RVA: 0x000422E8 File Offset: 0x000404E8
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

	// Token: 0x0600044C RID: 1100 RVA: 0x00042344 File Offset: 0x00040544
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

	// Token: 0x0600044D RID: 1101 RVA: 0x00042384 File Offset: 0x00040584
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

	// Token: 0x0600044E RID: 1102 RVA: 0x00042524 File Offset: 0x00040724
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

	// Token: 0x0600044F RID: 1103 RVA: 0x000426C1 File Offset: 0x000408C1
	public void DROPDOWN_PersonalUebersicht()
	{
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[29]);
		this.OpenMenu(false);
	}

	// Token: 0x06000450 RID: 1104 RVA: 0x000426E8 File Offset: 0x000408E8
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

	// Token: 0x06000451 RID: 1105 RVA: 0x00042744 File Offset: 0x00040944
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

	// Token: 0x06000452 RID: 1106 RVA: 0x000427A0 File Offset: 0x000409A0
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

	// Token: 0x06000453 RID: 1107 RVA: 0x000427FC File Offset: 0x000409FC
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

	// Token: 0x06000454 RID: 1108 RVA: 0x00042855 File Offset: 0x00040A55
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

	// Token: 0x06000455 RID: 1109 RVA: 0x0004288D File Offset: 0x00040A8D
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

	// Token: 0x06000456 RID: 1110 RVA: 0x000428C8 File Offset: 0x00040AC8
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

	// Token: 0x06000457 RID: 1111 RVA: 0x0004293E File Offset: 0x00040B3E
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

	// Token: 0x06000458 RID: 1112 RVA: 0x00042976 File Offset: 0x00040B76
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

	// Token: 0x06000459 RID: 1113 RVA: 0x000429AE File Offset: 0x00040BAE
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

	// Token: 0x0600045A RID: 1114 RVA: 0x000429E6 File Offset: 0x00040BE6
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

	// Token: 0x0600045B RID: 1115 RVA: 0x00042A1E File Offset: 0x00040C1E
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

	// Token: 0x0600045C RID: 1116 RVA: 0x00042A56 File Offset: 0x00040C56
	public void DROPDOWN_Personaleinstellungen()
	{
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[197]);
		this.OpenMenu(false);
	}

	// Token: 0x0600045D RID: 1117 RVA: 0x00042A7E File Offset: 0x00040C7E
	public void DROPDOWN_Arbeitsmarkt()
	{
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[30]);
		this.OpenMenu(false);
	}

	// Token: 0x0600045E RID: 1118 RVA: 0x00042AA4 File Offset: 0x00040CA4
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

	// Token: 0x0600045F RID: 1119 RVA: 0x00042AF4 File Offset: 0x00040CF4
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

	// Token: 0x06000460 RID: 1120 RVA: 0x00042B2C File Offset: 0x00040D2C
	public void DROPDOWN_Bank()
	{
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[138]);
		this.OpenMenu(false);
	}

	// Token: 0x06000461 RID: 1121 RVA: 0x00042B54 File Offset: 0x00040D54
	public void DROPDOWN_Archiv()
	{
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[289]);
		this.OpenMenu(false);
	}

	// Token: 0x06000462 RID: 1122 RVA: 0x00042B7C File Offset: 0x00040D7C
	public void DROPDOWN_Marktforschung()
	{
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[230]);
		this.uiObjects[230].GetComponent<Menu_Marktforschung>().Init(null);
		this.OpenMenu(false);
	}

	// Token: 0x06000463 RID: 1123 RVA: 0x00042BBB File Offset: 0x00040DBB
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

	// Token: 0x06000464 RID: 1124 RVA: 0x00042BF1 File Offset: 0x00040DF1
	public void DROPDOWN_BuyDevKits()
	{
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[33]);
		this.OpenMenu(false);
	}

	// Token: 0x06000465 RID: 1125 RVA: 0x00042C16 File Offset: 0x00040E16
	public void DROPDOWN_BuyEngine()
	{
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[42]);
		this.OpenMenu(false);
	}

	// Token: 0x06000466 RID: 1126 RVA: 0x00042C3B File Offset: 0x00040E3B
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

	// Token: 0x06000467 RID: 1127 RVA: 0x00042C70 File Offset: 0x00040E70
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

	// Token: 0x06000468 RID: 1128 RVA: 0x00042CA8 File Offset: 0x00040EA8
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

	// Token: 0x06000469 RID: 1129 RVA: 0x00042CDD File Offset: 0x00040EDD
	public void DROPDOWN_PublisherExklusivvertrag()
	{
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[200]);
		this.OpenMenu(false);
	}

	// Token: 0x0600046A RID: 1130 RVA: 0x00042D05 File Offset: 0x00040F05
	public void DROPDOWN_Firmenname()
	{
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[47]);
		this.OpenMenu(false);
	}

	// Token: 0x0600046B RID: 1131 RVA: 0x00042D2A File Offset: 0x00040F2A
	public void DROPDOWN_Achivements()
	{
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[364]);
		this.OpenMenu(false);
	}

	// Token: 0x0600046C RID: 1132 RVA: 0x00042D52 File Offset: 0x00040F52
	public void DROPDOWN_Statstics_Charts()
	{
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[114]);
		this.OpenMenu(false);
	}

	// Token: 0x0600046D RID: 1133 RVA: 0x00042D77 File Offset: 0x00040F77
	public void DROPDOWN_Statstics_Platforms()
	{
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[117]);
		this.OpenMenu(false);
	}

	// Token: 0x0600046E RID: 1134 RVA: 0x00042D9C File Offset: 0x00040F9C
	public void DROPDOWN_Statstics_DevPubs()
	{
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[118]);
		this.OpenMenu(false);
	}

	// Token: 0x0600046F RID: 1135 RVA: 0x00042DC1 File Offset: 0x00040FC1
	public void DROPDOWN_Statstics_Engines()
	{
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[121]);
		this.OpenMenu(false);
	}

	// Token: 0x06000470 RID: 1136 RVA: 0x00042DE6 File Offset: 0x00040FE6
	public void DROPDOWN_Statstics_Tochterfirmen()
	{
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[385]);
		this.OpenMenu(false);
	}

	// Token: 0x06000471 RID: 1137 RVA: 0x00042E0E File Offset: 0x0004100E
	public void DROPDOWN_Statstics_MyGames()
	{
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[123]);
		this.OpenMenu(false);
	}

	// Token: 0x06000472 RID: 1138 RVA: 0x00042E33 File Offset: 0x00041033
	public void DROPDOWN_Statstics_MyKonsolen()
	{
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[334]);
		this.OpenMenu(false);
	}

	// Token: 0x06000473 RID: 1139 RVA: 0x00042E5B File Offset: 0x0004105B
	public void DROPDOWN_Statstics_Infos()
	{
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[126]);
		this.OpenMenu(false);
	}

	// Token: 0x06000474 RID: 1140 RVA: 0x00042E80 File Offset: 0x00041080
	public void DROPDOWN_Statstics_Finanzen()
	{
		this.sfx_.PlaySound(3, false);
		this.ActivateMenu(this.uiObjects[131]);
		this.OpenMenu(false);
	}

	// Token: 0x06000475 RID: 1141 RVA: 0x00042EA8 File Offset: 0x000410A8
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

	// Token: 0x06000476 RID: 1142 RVA: 0x00042FBC File Offset: 0x000411BC
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

	// Token: 0x06000477 RID: 1143 RVA: 0x00043020 File Offset: 0x00041220
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

	// Token: 0x06000478 RID: 1144 RVA: 0x0004308D File Offset: 0x0004128D
	public taskMitarbeitersuche AddTask_Mitarbeitersuche()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[25]).GetComponent<taskMitarbeitersuche>();
	}

	// Token: 0x06000479 RID: 1145 RVA: 0x000430A2 File Offset: 0x000412A2
	public taskKonsole AddTask_Konsole()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[22]).GetComponent<taskKonsole>();
	}

	// Token: 0x0600047A RID: 1146 RVA: 0x000430B7 File Offset: 0x000412B7
	public taskArcadeProduction AddTask_ArcadeProduction()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[21]).GetComponent<taskArcadeProduction>();
	}

	// Token: 0x0600047B RID: 1147 RVA: 0x000430CC File Offset: 0x000412CC
	public taskF2PUpdate AddTask_F2PUpdate()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[20]).GetComponent<taskF2PUpdate>();
	}

	// Token: 0x0600047C RID: 1148 RVA: 0x000430E1 File Offset: 0x000412E1
	public taskMarketingSpezial AddTask_MarketingSpezial()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[19]).GetComponent<taskMarketingSpezial>();
	}

	// Token: 0x0600047D RID: 1149 RVA: 0x000430F6 File Offset: 0x000412F6
	public taskPolishing AddTask_Polishing()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[18]).GetComponent<taskPolishing>();
	}

	// Token: 0x0600047E RID: 1150 RVA: 0x0004310B File Offset: 0x0004130B
	public taskMarktforschung AddTask_Marktforschung()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[17]).GetComponent<taskMarktforschung>();
	}

	// Token: 0x0600047F RID: 1151 RVA: 0x00043120 File Offset: 0x00041320
	public taskProduction AddTask_Production()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[16]).GetComponent<taskProduction>();
	}

	// Token: 0x06000480 RID: 1152 RVA: 0x00043135 File Offset: 0x00041335
	public taskSpielbericht AddTask_Spielbericht()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[15]).GetComponent<taskSpielbericht>();
	}

	// Token: 0x06000481 RID: 1153 RVA: 0x0004314A File Offset: 0x0004134A
	public taskAnimationVerbessern AddTask_AnimationVerbessern()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[14]).GetComponent<taskAnimationVerbessern>();
	}

	// Token: 0x06000482 RID: 1154 RVA: 0x0004315F File Offset: 0x0004135F
	public taskSoundVerbessern AddTask_SoundVerbessern()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[13]).GetComponent<taskSoundVerbessern>();
	}

	// Token: 0x06000483 RID: 1155 RVA: 0x00043174 File Offset: 0x00041374
	public taskGrafikVerbessern AddTask_GrafikVerbessern()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[12]).GetComponent<taskGrafikVerbessern>();
	}

	// Token: 0x06000484 RID: 1156 RVA: 0x00043189 File Offset: 0x00041389
	public taskGameplayVerbessern AddTask_GameplayVerbessern()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[11]).GetComponent<taskGameplayVerbessern>();
	}

	// Token: 0x06000485 RID: 1157 RVA: 0x0004319E File Offset: 0x0004139E
	public taskUpdate AddTask_Update()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[7]).GetComponent<taskUpdate>();
	}

	// Token: 0x06000486 RID: 1158 RVA: 0x000431B2 File Offset: 0x000413B2
	public taskBugfixing AddTask_Bugfixing()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[10]).GetComponent<taskBugfixing>();
	}

	// Token: 0x06000487 RID: 1159 RVA: 0x000431C7 File Offset: 0x000413C7
	public taskForschung AddTask_Forschung()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[0]).GetComponent<taskForschung>();
	}

	// Token: 0x06000488 RID: 1160 RVA: 0x000431DB File Offset: 0x000413DB
	public taskEngine AddTask_Engine()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[1]).GetComponent<taskEngine>();
	}

	// Token: 0x06000489 RID: 1161 RVA: 0x000431EF File Offset: 0x000413EF
	public taskGame AddTask_Game()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[2]).GetComponent<taskGame>();
	}

	// Token: 0x0600048A RID: 1162 RVA: 0x00043203 File Offset: 0x00041403
	public taskUnterstuetzen AddTask_Unterstuetzen()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[3]).GetComponent<taskUnterstuetzen>();
	}

	// Token: 0x0600048B RID: 1163 RVA: 0x00043217 File Offset: 0x00041417
	public taskMarketing AddTask_Marketing()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[4]).GetComponent<taskMarketing>();
	}

	// Token: 0x0600048C RID: 1164 RVA: 0x0004322B File Offset: 0x0004142B
	public taskFankampagne AddTask_Fankampagne()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[8]).GetComponent<taskFankampagne>();
	}

	// Token: 0x0600048D RID: 1165 RVA: 0x0004323F File Offset: 0x0004143F
	public taskSupport AddTask_Support()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[9]).GetComponent<taskSupport>();
	}

	// Token: 0x0600048E RID: 1166 RVA: 0x00043254 File Offset: 0x00041454
	public taskFanshop AddTask_Fanshop()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[26]).GetComponent<taskFanshop>();
	}

	// Token: 0x0600048F RID: 1167 RVA: 0x00043269 File Offset: 0x00041469
	public taskTraining AddTask_Training()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[5]).GetComponent<taskTraining>();
	}

	// Token: 0x06000490 RID: 1168 RVA: 0x0004327D File Offset: 0x0004147D
	public taskContractWork AddTask_ContractWork()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[6]).GetComponent<taskContractWork>();
	}

	// Token: 0x06000491 RID: 1169 RVA: 0x00043291 File Offset: 0x00041491
	public taskContractWait AddTask_ContractWait()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[23]).GetComponent<taskContractWait>();
	}

	// Token: 0x06000492 RID: 1170 RVA: 0x000432A6 File Offset: 0x000414A6
	public taskWait AddTask_Wait()
	{
		return UnityEngine.Object.Instantiate<GameObject>(this.uiTaskPrefabs[24]).GetComponent<taskWait>();
	}

	// Token: 0x06000493 RID: 1171 RVA: 0x000432BB File Offset: 0x000414BB
	public Sprite Get3Stars(int i)
	{
		return this.uiSprites[45 + i];
	}

	// Token: 0x06000494 RID: 1172 RVA: 0x000432C8 File Offset: 0x000414C8
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

	// Token: 0x06000495 RID: 1173 RVA: 0x0004345C File Offset: 0x0004165C
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

	// Token: 0x06000496 RID: 1174 RVA: 0x000435EC File Offset: 0x000417EC
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

	// Token: 0x06000497 RID: 1175 RVA: 0x00043708 File Offset: 0x00041908
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

	// Token: 0x06000498 RID: 1176 RVA: 0x0004385A File Offset: 0x00041A5A
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

	// Token: 0x06000499 RID: 1177 RVA: 0x00043878 File Offset: 0x00041A78
	public void DisableTabs(GameObject p)
	{
		for (int i = 0; i < p.transform.childCount; i++)
		{
			p.transform.GetChild(i).GetComponent<Image>().color = Color.white;
		}
	}

	// Token: 0x0600049A RID: 1178 RVA: 0x000438B6 File Offset: 0x00041AB6
	public void SetTab(GameObject p, int t)
	{
		this.DisableTabs(p);
		p.transform.GetChild(t).GetComponent<Image>().color = this.colors[1];
	}

	// Token: 0x0600049B RID: 1179 RVA: 0x000438E1 File Offset: 0x00041AE1
	public void ShowNoMoney()
	{
		this.sfx_.PlaySound(29, false);
		this.uiObjects[34].SetActive(true);
	}

	// Token: 0x0600049C RID: 1180 RVA: 0x00043900 File Offset: 0x00041B00
	public void ShowGameHasSaved()
	{
		this.sfx_.PlaySound(49, false);
		this.uiObjects[158].SetActive(true);
	}

	// Token: 0x0600049D RID: 1181 RVA: 0x00043922 File Offset: 0x00041B22
	public void MessageBox(string c, bool closeMenu)
	{
		this.ActivateMenu(this.uiObjects[40]);
		this.uiObjects[40].GetComponent<Menu_Messagebox>().Init(c, closeMenu);
	}

	// Token: 0x0600049E RID: 1182 RVA: 0x00043948 File Offset: 0x00041B48
	public void MessageBoxSave(int menuID, bool closeMenu)
	{
		this.ActivateMenu(this.uiObjects[297]);
		this.uiObjects[297].GetComponent<Menu_MessageBoxSave>().Init(menuID, closeMenu);
	}

	// Token: 0x0600049F RID: 1183 RVA: 0x00043974 File Offset: 0x00041B74
	public void UnlockBox(string c, bool closeMenu)
	{
		this.OpenMenu(false);
		this.ActivateMenu(this.uiObjects[170]);
		this.uiObjects[170].GetComponent<Menu_Unlock>().Init(c, closeMenu);
	}

	// Token: 0x060004A0 RID: 1184 RVA: 0x000439A7 File Offset: 0x00041BA7
	public void KeinEintrag(GameObject content, GameObject go)
	{
		base.StartCoroutine(this.IE_KeinEintrag(content, go));
	}

	// Token: 0x060004A1 RID: 1185 RVA: 0x000439B8 File Offset: 0x00041BB8
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

	// Token: 0x060004A2 RID: 1186 RVA: 0x000439D0 File Offset: 0x00041BD0
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

	// Token: 0x060004A3 RID: 1187 RVA: 0x00043AAF File Offset: 0x00041CAF
	public void RemoveVectrocity()
	{
		this.initVectrocity = false;
		VectorLine.SetEndCap("Arrows", EndCap.None);
		VectorLine.SetEndCap("ArrowsCharRoom", EndCap.None);
		VectorLine.SetEndCap("RoomLine", EndCap.None);
		VectorLine.SetEndCap("VerschiebeTask", EndCap.None);
	}

	// Token: 0x060004A4 RID: 1188 RVA: 0x00043AE4 File Offset: 0x00041CE4
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

	// Token: 0x060004A5 RID: 1189 RVA: 0x00043D10 File Offset: 0x00041F10
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

	// Token: 0x060004A6 RID: 1190 RVA: 0x00043D8F File Offset: 0x00041F8F
	private IEnumerator DeactivateFanLetter()
	{
		yield return new WaitForSeconds(this.settings_.fanletterTime);
		this.uiObjects[113].SetActive(false);
		yield break;
	}

	// Token: 0x060004A7 RID: 1191 RVA: 0x00043DA0 File Offset: 0x00041FA0
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

	// Token: 0x060004A8 RID: 1192 RVA: 0x00043E39 File Offset: 0x00042039
	private void UpdateUIHotkey()
	{
		if (this.UIHotkey)
		{
			this.UIHotkey.GetComponent<Button>().onClick.Invoke();
			this.UIHotkey = null;
			this.UIHotkeySiblingIndex = -1;
		}
	}

	// Token: 0x060004A9 RID: 1193 RVA: 0x00043E6B File Offset: 0x0004206B
	public void SetUIHotkey(GameObject go)
	{
		if (go.transform.GetSiblingIndex() > this.UIHotkeySiblingIndex)
		{
			this.UIHotkey = go;
		}
	}

	// Token: 0x060004AA RID: 1194 RVA: 0x00043E87 File Offset: 0x00042087
	public void SelectInputField()
	{
		this.selectInputField = true;
	}

	// Token: 0x060004AB RID: 1195 RVA: 0x00043E90 File Offset: 0x00042090
	public void DeselectInputField()
	{
		this.selectInputField = false;
	}

	// Token: 0x060004AC RID: 1196 RVA: 0x00043E99 File Offset: 0x00042099
	public void EVENT_MitarbeiterMotivation()
	{
		this.ActivateMenu(this.uiObjects[184]);
		this.uiObjects[184].GetComponent<Menu_PersonalMotivation>().Init();
		this.OpenMenu(false);
	}

	// Token: 0x060004AD RID: 1197 RVA: 0x00043ECC File Offset: 0x000420CC
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

	// Token: 0x060004AE RID: 1198 RVA: 0x00043FE8 File Offset: 0x000421E8
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

	// Token: 0x060004AF RID: 1199 RVA: 0x00044080 File Offset: 0x00042280
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

	// Token: 0x060004B0 RID: 1200 RVA: 0x00044407 File Offset: 0x00042607
	public void OpenEngineAbrechnung(gameScript script_)
	{
		this.ActivateMenu(this.uiObjects[83]);
		this.OpenMenu(false);
		this.uiObjects[83].GetComponent<Menu_Engine_Abrechnung>().Init(script_);
	}

	// Token: 0x060004B1 RID: 1201 RVA: 0x00044433 File Offset: 0x00042633
	public void OpenTochterfirmaAbrechnung(gameScript script_)
	{
		this.ActivateMenu(this.uiObjects[398]);
		this.OpenMenu(false);
		this.uiObjects[398].GetComponent<Menu_TochterfirmaAbrechnung>().Init(script_);
	}

	// Token: 0x060004B2 RID: 1202 RVA: 0x00044468 File Offset: 0x00042668
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

	// Token: 0x060004B3 RID: 1203 RVA: 0x000445CC File Offset: 0x000427CC
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

	// Token: 0x060004B4 RID: 1204 RVA: 0x00044660 File Offset: 0x00042860
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

	// Token: 0x060004B5 RID: 1205 RVA: 0x0004483C File Offset: 0x00042A3C
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

	// Token: 0x060004B6 RID: 1206 RVA: 0x0004489B File Offset: 0x00042A9B
	public void TOGGLE_GameTab_TABS()
	{
		this.uiObjects[362].GetComponent<GamesGroupContent>().timer = 10f;
	}

	// Token: 0x060004B7 RID: 1207 RVA: 0x000448B8 File Offset: 0x00042AB8
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

	// Token: 0x060004B8 RID: 1208 RVA: 0x00044910 File Offset: 0x00042B10
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

	// Token: 0x060004B9 RID: 1209 RVA: 0x00044998 File Offset: 0x00042B98
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

	// Token: 0x060004BA RID: 1210 RVA: 0x00044A64 File Offset: 0x00042C64
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

	// Token: 0x060004BB RID: 1211 RVA: 0x00044ABD File Offset: 0x00042CBD
	public void TOGGLE_Ausstattung()
	{
		if (!this.uiObjects[85].GetComponent<Toggle>().isOn)
		{
			PlayerPrefs.SetInt("Toggle_Ausstattung", 0);
			return;
		}
		PlayerPrefs.SetInt("Toggle_Ausstattung", 1);
	}

	// Token: 0x060004BC RID: 1212 RVA: 0x00044AEB File Offset: 0x00042CEB
	public void TOGGLE_Muell()
	{
		if (!this.uiObjects[86].GetComponent<Toggle>().isOn)
		{
			PlayerPrefs.SetInt("Toggle_Muell", 0);
			return;
		}
		PlayerPrefs.SetInt("Toggle_Muell", 1);
	}

	// Token: 0x060004BD RID: 1213 RVA: 0x00044B19 File Offset: 0x00042D19
	public void TOGGLE_Waerme()
	{
		if (!this.uiObjects[87].GetComponent<Toggle>().isOn)
		{
			PlayerPrefs.SetInt("Toggle_Waerme", 0);
			return;
		}
		PlayerPrefs.SetInt("Toggle_Waerme", 1);
	}

	// Token: 0x060004BE RID: 1214 RVA: 0x00044B48 File Offset: 0x00042D48
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

	// Token: 0x060004BF RID: 1215 RVA: 0x00044F30 File Offset: 0x00043130
	private void LoadPlayerAndCompanyName()
	{
		this.uiObjects[159].GetComponent<Menu_NewGame>().uiObjects[0].GetComponent<InputField>().text = PlayerPrefs.GetString("CompanyName");
		this.uiObjects[201].GetComponent<mpMain>().uiObjects[11].GetComponent<InputField>().text = PlayerPrefs.GetString("PlayerName");
	}

	// Token: 0x060004C0 RID: 1216 RVA: 0x00044F98 File Offset: 0x00043198
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

	// Token: 0x060004C1 RID: 1217 RVA: 0x00044FFD File Offset: 0x000431FD
	public void SetTutorialStep(int i)
	{
		if (this.uiObjects[248].GetComponent<Menu_Tutorial>().step == i - 1)
		{
			this.uiObjects[248].GetComponent<Menu_Tutorial>().SetStep(i);
		}
	}

	// Token: 0x060004C2 RID: 1218 RVA: 0x00045031 File Offset: 0x00043231
	public int GetTutorialStep()
	{
		return this.uiObjects[248].GetComponent<Menu_Tutorial>().step;
	}

	// Token: 0x060004C3 RID: 1219 RVA: 0x00045049 File Offset: 0x00043249
	public void CameraBlend()
	{
		this.postProcess_.BlendIn();
	}

	// Token: 0x060004C4 RID: 1220 RVA: 0x00045056 File Offset: 0x00043256
	public Sprite GetCompanyLogo(int i)
	{
		if (i > this.logoSprites.Length)
		{
			return this.logoSprites[0];
		}
		return this.logoSprites[i];
	}

	// Token: 0x060004C5 RID: 1221 RVA: 0x00045074 File Offset: 0x00043274
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

	// Token: 0x0400076E RID: 1902
	public GameObject[] uiPrefabs;

	// Token: 0x0400076F RID: 1903
	public GameObject uiPops;

	// Token: 0x04000770 RID: 1904
	public GameObject[] uiObjects;

	// Token: 0x04000771 RID: 1905
	public GameObject[] uiPerks;

	// Token: 0x04000772 RID: 1906
	public GameObject[] uiTaskPrefabs;

	// Token: 0x04000773 RID: 1907
	public Sprite[] uiSprites;

	// Token: 0x04000774 RID: 1908
	public Sprite[] inventarSprites;

	// Token: 0x04000775 RID: 1909
	public Sprite[] flagSprites;

	// Token: 0x04000776 RID: 1910
	public Sprite[] logoSprites;

	// Token: 0x04000777 RID: 1911
	public Sprite[] charIcons;

	// Token: 0x04000778 RID: 1912
	public Sprite[] iconSupport;

	// Token: 0x04000779 RID: 1913
	public Sprite[] iconGlobalEvents;

	// Token: 0x0400077A RID: 1914
	public Sprite[] iconAchivements;

	// Token: 0x0400077B RID: 1915
	public Sprite[] iconAchivementsOff;

	// Token: 0x0400077C RID: 1916
	public Color[] colors;

	// Token: 0x0400077D RID: 1917
	public Color[] colorsBalken;

	// Token: 0x0400077E RID: 1918
	public Texture2D[] unterstuetzenLine;

	// Token: 0x0400077F RID: 1919
	public Material matFill_Window;

	// Token: 0x04000780 RID: 1920
	public bool menuOpen;

	// Token: 0x04000781 RID: 1921
	public bool disableRoomGUI;

	// Token: 0x04000782 RID: 1922
	public bool spacePause;

	// Token: 0x04000783 RID: 1923
	public Camera camera_;

	// Token: 0x04000784 RID: 1924
	private float durchschnittsMotivation = 100f;

	// Token: 0x04000785 RID: 1925
	private float timerUpdateTopLeiste;

	// Token: 0x04000786 RID: 1926
	private float timerUpdateContract;

	// Token: 0x04000787 RID: 1927
	private float timerUpdateGlobalEvent;

	// Token: 0x04000788 RID: 1928
	public GameObject hellgikeitsObjekt;

	// Token: 0x04000789 RID: 1929
	private GameObject main_;

	// Token: 0x0400078A RID: 1930
	private settingsScript settings_;

	// Token: 0x0400078B RID: 1931
	private mainScript mS_;

	// Token: 0x0400078C RID: 1932
	private textScript tS_;

	// Token: 0x0400078D RID: 1933
	private sfxScript sfx_;

	// Token: 0x0400078E RID: 1934
	public GUI_Tooltip guiTooltip;

	// Token: 0x0400078F RID: 1935
	private objectTooltip objectTooltip;

	// Token: 0x04000790 RID: 1936
	private genres genres_;

	// Token: 0x04000791 RID: 1937
	private themes themes_;

	// Token: 0x04000792 RID: 1938
	private mapScript mapScript_;

	// Token: 0x04000793 RID: 1939
	private unlockScript unlock_;

	// Token: 0x04000794 RID: 1940
	private contractWorkMain contractMain_;

	// Token: 0x04000795 RID: 1941
	private publishingOfferMain publishingOfferMain_;

	// Token: 0x04000796 RID: 1942
	private pickCharacterScript pcS_;

	// Token: 0x04000797 RID: 1943
	private mpCalls mpCalls_;

	// Token: 0x04000798 RID: 1944
	private forschungSonstiges forschungSonstiges_;

	// Token: 0x04000799 RID: 1945
	private gameplayFeatures gF_;

	// Token: 0x0400079A RID: 1946
	private PostProcessing postProcess_;

	// Token: 0x0400079B RID: 1947
	public GUI_MainButtons guiMainButtons_;

	// Token: 0x0400079C RID: 1948
	private List<GameObject> moneyPopList = new List<GameObject>();

	// Token: 0x0400079D RID: 1949
	private float fansTimer;

	// Token: 0x0400079E RID: 1950
	private int fansOld;

	// Token: 0x0400079F RID: 1951
	private string fansString = "";

	// Token: 0x040007A0 RID: 1952
	public float supportTimer;

	// Token: 0x040007A1 RID: 1953
	private float moneyTimer;

	// Token: 0x040007A2 RID: 1954
	private long moneyOld;

	// Token: 0x040007A3 RID: 1955
	private string moneyString = "";

	// Token: 0x040007A4 RID: 1956
	private tooltip moneyTooltip;

	// Token: 0x040007A5 RID: 1957
	private float updateBankWarningIcon_Timer;

	// Token: 0x040007A6 RID: 1958
	private float updateExklusivVertragIcon_Timer;

	// Token: 0x040007A7 RID: 1959
	private float updateStudioBewertungTimer;

	// Token: 0x040007A8 RID: 1960
	private float timerUpdateMitarbeiterNoJob;

	// Token: 0x040007A9 RID: 1961
	private float timerUpdateRoomNoJob;

	// Token: 0x040007AA RID: 1962
	private int roomNoJobOld;

	// Token: 0x040007AB RID: 1963
	private bool gameTabsFullView = true;

	// Token: 0x040007AC RID: 1964
	public bool initVectrocity;

	// Token: 0x040007AD RID: 1965
	private GameObject filterGameObject_;

	// Token: 0x040007AE RID: 1966
	public int filterToggles = -1;

	// Token: 0x040007AF RID: 1967
	public int UIHotkeySiblingIndex = -1;

	// Token: 0x040007B0 RID: 1968
	public GameObject UIHotkey;

	// Token: 0x040007B1 RID: 1969
	public bool selectInputField;

	// Token: 0x040007B2 RID: 1970
	private List<FansSortList> fansSortList = new List<FansSortList>();
}
