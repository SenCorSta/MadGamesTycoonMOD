using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000253 RID: 595
public class Menu_Stats_ShowBestIPs : MonoBehaviour
{
	// Token: 0x06001706 RID: 5894 RVA: 0x00010224 File Offset: 0x0000E424
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001707 RID: 5895 RVA: 0x000EE55C File Offset: 0x000EC75C
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

	// Token: 0x06001708 RID: 5896 RVA: 0x0001022C File Offset: 0x0000E42C
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x06001709 RID: 5897 RVA: 0x000EE644 File Offset: 0x000EC844
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_MyGames_ShowIP>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600170A RID: 5898 RVA: 0x0001025E File Offset: 0x0000E45E
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x0600170B RID: 5899 RVA: 0x000EE6A0 File Offset: 0x000EC8A0
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(217));
		list.Add(this.tS_.GetText(277));
		list.Add(this.tS_.GetText(273));
		list.Add(this.tS_.GetText(275));
		list.Add(this.tS_.GetText(1484));
		list.Add(this.tS_.GetText(325));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x0600170C RID: 5900 RVA: 0x000EE798 File Offset: 0x000EC998
	public void Init(gameScript game_)
	{
		this.FindScripts();
		this.gS_ = game_;
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		string text = this.tS_.GetText(1557);
		text = text.Replace("<NAME>", this.gS_.GetIpName());
		this.uiObjects[4].GetComponent<Text>().text = text;
		this.guiMain_.DrawIpBekanntheit(this.uiObjects[6], this.gS_);
		this.uiObjects[18].GetComponent<InputField>().text = "";
		if (this.gS_.ipName != null && this.gS_.ipName.Length > 0)
		{
			this.uiObjects[18].GetComponent<InputField>().text = this.gS_.ipName;
		}
		this.SetData();
	}

	// Token: 0x0600170D RID: 5901 RVA: 0x000EE8A0 File Offset: 0x000ECAA0
	private void ResetDaten()
	{
		this.gameAnzahl = 0;
		this.gameAnzahlForReview = 0;
		this.numGOTY = 0;
		this.numHit = 0;
		this.numTrend = 0;
		this.numGold = 0;
		this.numPlatin = 0;
		this.numDiamant = 0;
		this.gesamtReview = 0;
		this.bestReview = -1;
		this.badReview = 9999;
		this.bestReviewName = "";
		this.badReviewName = "";
		this.gesamtSells = 0f;
		this.gesamtDownloads = 0f;
		this.gesamtAbos = 0f;
		this.gesamtUmsatz = 0f;
		this.gesamtAusgaben = 0f;
	}

	// Token: 0x0600170E RID: 5902 RVA: 0x000EE94C File Offset: 0x000ECB4C
	private void ShowDaten()
	{
		this.uiObjects[19].GetComponent<Text>().text = this.gS_.GetDeveloperName();
		this.uiObjects[20].GetComponent<Image>().sprite = this.gS_.GetDeveloperLogo();
		string text = this.tS_.GetText(297);
		text = text.Replace("<NUM>", this.gameAnzahl.ToString());
		this.uiObjects[7].GetComponent<Text>().text = text;
		this.uiObjects[8].GetComponent<Text>().text = this.numGOTY.ToString() + "x";
		this.uiObjects[9].GetComponent<Text>().text = this.numHit.ToString() + "x";
		this.uiObjects[10].GetComponent<Text>().text = this.numTrend.ToString() + "x";
		this.uiObjects[11].GetComponent<Text>().text = this.numGold.ToString() + "x";
		this.uiObjects[12].GetComponent<Text>().text = this.numPlatin.ToString() + "x";
		this.uiObjects[13].GetComponent<Text>().text = this.numDiamant.ToString() + "x";
		this.uiObjects[14].GetComponent<Text>().text = string.Concat(new string[]
		{
			this.tS_.GetText(1571),
			": <color=green>",
			this.bestReviewName,
			"</color>\n",
			this.tS_.GetText(1572),
			": <color=red>",
			this.badReviewName,
			"</color>\n",
			this.tS_.GetText(1573)
		});
		if (this.gameAnzahlForReview > 0)
		{
			this.uiObjects[15].GetComponent<Text>().text = string.Concat(new string[]
			{
				"<color=green>",
				this.bestReview.ToString(),
				"%</color>\n<color=red>",
				this.badReview.ToString(),
				"%</color>\n",
				(this.gesamtReview / this.gameAnzahlForReview).ToString(),
				"%"
			});
		}
		else
		{
			this.uiObjects[15].GetComponent<Text>().text = "<color=green>--%</color>\n<color=red>--%</color>\n--%";
		}
		this.uiObjects[16].GetComponent<Text>().text = string.Concat(new string[]
		{
			this.tS_.GetText(696),
			"\n",
			this.tS_.GetText(697),
			"\n",
			this.tS_.GetText(702),
			"\n\n",
			this.tS_.GetText(1238),
			"\n",
			this.tS_.GetText(1575),
			"\n\n<size=18>",
			this.tS_.GetText(724),
			"</size>"
		});
		if (this.gesamtUmsatz - this.gesamtAusgaben >= 0f)
		{
			this.uiObjects[17].GetComponent<Text>().text = string.Concat(new object[]
			{
				this.mS_.Round(this.gesamtSells, 2),
				" ",
				this.tS_.GetText(1483),
				"\n",
				this.mS_.Round(this.gesamtDownloads, 2),
				" ",
				this.tS_.GetText(1483),
				"\n",
				this.mS_.Round(this.gesamtAbos, 2),
				" ",
				this.tS_.GetText(1483),
				"\n\n<color=green>",
				this.mS_.Round(this.gesamtUmsatz, 2),
				" ",
				this.tS_.GetText(1483),
				"</color>\n<color=red>",
				this.mS_.Round(this.gesamtAusgaben, 2),
				" ",
				this.tS_.GetText(1483),
				"</color>\n\n<size=18><color=green>",
				this.mS_.Round(this.gesamtUmsatz - this.gesamtAusgaben, 2),
				" ",
				this.tS_.GetText(1483),
				"</color></size>"
			});
			return;
		}
		this.uiObjects[17].GetComponent<Text>().text = string.Concat(new object[]
		{
			this.mS_.Round(this.gesamtSells, 2),
			" ",
			this.tS_.GetText(1483),
			"\n",
			this.mS_.Round(this.gesamtDownloads, 2),
			" ",
			this.tS_.GetText(1483),
			"\n",
			this.mS_.Round(this.gesamtAbos, 2),
			" ",
			this.tS_.GetText(1483),
			"\n\n<color=green>",
			this.mS_.Round(this.gesamtUmsatz, 2),
			" ",
			this.tS_.GetText(1483),
			"</color>\n<color=red>",
			this.mS_.Round(this.gesamtAusgaben, 2),
			" ",
			this.tS_.GetText(1483),
			"</color>\n\n<size=18><color=red>",
			this.mS_.Round(this.gesamtUmsatz - this.gesamtAusgaben, 2),
			" ",
			this.tS_.GetText(1483),
			"</color></size>"
		});
	}

	// Token: 0x0600170F RID: 5903 RVA: 0x000EF008 File Offset: 0x000ED208
	private void SetData()
	{
		if (!this.gS_)
		{
			return;
		}
		this.ResetDaten();
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && this.CheckGameData(component) && !this.Exists(this.uiObjects[0], component.myID))
				{
					Item_MyGames_ShowIP component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_MyGames_ShowIP>();
					component2.game_ = component;
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.genres_ = this.genres_;
					this.gameAnzahl++;
					if (!component.inDevelopment)
					{
						if (component.HasGold())
						{
							this.numGold++;
						}
						if (component.HasPlatin())
						{
							this.numPlatin++;
						}
						if (component.HasDiamant())
						{
							this.numDiamant++;
						}
						float num;
						if (component.gameTyp == 2)
						{
							num = (float)component.sellsTotal;
							num /= 1000000f;
							this.gesamtDownloads += num;
						}
						else
						{
							num = (float)component.sellsTotal;
							num /= 1000000f;
							this.gesamtSells += num;
						}
						if (component.gameTyp == 1)
						{
							num = (float)component.abonnements;
							num /= 1000000f;
							this.gesamtAbos += num;
						}
						num = (float)component.umsatzTotal;
						num /= 1000000f;
						this.gesamtUmsatz += num;
						num = (float)component.GetGesamtAusgaben();
						num /= 1000000f;
						this.gesamtAusgaben += num;
						if (!component.typ_budget && !component.typ_bundle && !component.typ_bundleAddon)
						{
							this.gameAnzahlForReview++;
							this.gesamtReview += component.reviewTotal;
							if (component.goty)
							{
								this.numGOTY++;
							}
							if (component.trendsetter)
							{
								this.numTrend++;
							}
							if (component.IsHit())
							{
								this.numHit++;
							}
							if (this.bestReview < component.reviewTotal)
							{
								this.bestReview = component.reviewTotal;
								this.bestReviewName = component.GetNameWithTag();
							}
							if (this.badReview > component.reviewTotal)
							{
								this.badReview = component.reviewTotal;
								this.badReviewName = component.GetNameWithTag();
							}
						}
					}
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
		this.ShowDaten();
	}

	// Token: 0x06001710 RID: 5904 RVA: 0x0001026C File Offset: 0x0000E46C
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.mainIP == this.gS_.myID && !script_.pubAngebot && !script_.auftragsspiel;
	}

	// Token: 0x06001711 RID: 5905 RVA: 0x000EF30C File Offset: 0x000ED50C
	public void DROPDOWN_Sort()
	{
		int value = this.uiObjects[1].GetComponent<Dropdown>().value;
		PlayerPrefs.SetInt(this.uiObjects[1].name, value);
		int childCount = this.uiObjects[0].transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = this.uiObjects[0].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				Item_MyGames_ShowIP component = gameObject.GetComponent<Item_MyGames_ShowIP>();
				switch (value)
				{
				case 0:
					gameObject.name = component.game_.GetNameSimple();
					break;
				case 1:
				{
					float num = (float)component.game_.date_month;
					num /= 13f;
					gameObject.name = component.game_.date_year.ToString() + num.ToString();
					if (component.game_.inDevelopment)
					{
						gameObject.name = "999999";
					}
					break;
				}
				case 2:
					gameObject.name = component.game_.reviewTotal.ToString();
					break;
				case 3:
					gameObject.name = component.game_.maingenre.ToString();
					break;
				case 4:
					gameObject.name = component.game_.sellsTotal.ToString();
					break;
				case 5:
					gameObject.name = component.game_.GetPlatformTypString();
					break;
				case 6:
					gameObject.name = component.game_.GetTypString();
					break;
				}
			}
		}
		if (value == 0 || value == 5 || value == 6)
		{
			this.mS_.SortChildrenByName(this.uiObjects[0]);
			return;
		}
		this.mS_.SortChildrenByFloat(this.uiObjects[0]);
	}

	// Token: 0x06001712 RID: 5906 RVA: 0x000EF4C8 File Offset: 0x000ED6C8
	public void BUTTON_Close()
	{
		if (this.uiObjects[18].GetComponent<InputField>().text.Length > 0)
		{
			this.gS_.ipName = this.uiObjects[18].GetComponent<InputField>().text;
		}
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001ADA RID: 6874
	public GameObject[] uiPrefabs;

	// Token: 0x04001ADB RID: 6875
	public GameObject[] uiObjects;

	// Token: 0x04001ADC RID: 6876
	private mainScript mS_;

	// Token: 0x04001ADD RID: 6877
	private GameObject main_;

	// Token: 0x04001ADE RID: 6878
	private GUI_Main guiMain_;

	// Token: 0x04001ADF RID: 6879
	private sfxScript sfx_;

	// Token: 0x04001AE0 RID: 6880
	private textScript tS_;

	// Token: 0x04001AE1 RID: 6881
	private genres genres_;

	// Token: 0x04001AE2 RID: 6882
	private games games_;

	// Token: 0x04001AE3 RID: 6883
	public gameScript gS_;

	// Token: 0x04001AE4 RID: 6884
	private int gameAnzahl;

	// Token: 0x04001AE5 RID: 6885
	private int gameAnzahlForReview;

	// Token: 0x04001AE6 RID: 6886
	private int numGOTY;

	// Token: 0x04001AE7 RID: 6887
	private int numHit;

	// Token: 0x04001AE8 RID: 6888
	private int numTrend;

	// Token: 0x04001AE9 RID: 6889
	private int numGold;

	// Token: 0x04001AEA RID: 6890
	private int numPlatin;

	// Token: 0x04001AEB RID: 6891
	private int numDiamant;

	// Token: 0x04001AEC RID: 6892
	private int gesamtReview;

	// Token: 0x04001AED RID: 6893
	private int bestReview;

	// Token: 0x04001AEE RID: 6894
	private int badReview;

	// Token: 0x04001AEF RID: 6895
	private string bestReviewName = "";

	// Token: 0x04001AF0 RID: 6896
	private string badReviewName = "";

	// Token: 0x04001AF1 RID: 6897
	private float gesamtSells;

	// Token: 0x04001AF2 RID: 6898
	private float gesamtDownloads;

	// Token: 0x04001AF3 RID: 6899
	private float gesamtAbos;

	// Token: 0x04001AF4 RID: 6900
	private float gesamtUmsatz;

	// Token: 0x04001AF5 RID: 6901
	private float gesamtAusgaben;
}
