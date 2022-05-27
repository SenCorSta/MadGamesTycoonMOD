using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000254 RID: 596
public class Menu_Stats_ShowMyIPs : MonoBehaviour
{
	// Token: 0x06001714 RID: 5908 RVA: 0x000102BA File Offset: 0x0000E4BA
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001715 RID: 5909 RVA: 0x000EF528 File Offset: 0x000ED728
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
	}

	// Token: 0x06001716 RID: 5910 RVA: 0x000102C2 File Offset: 0x0000E4C2
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x06001717 RID: 5911 RVA: 0x000EE644 File Offset: 0x000EC844
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

	// Token: 0x06001718 RID: 5912 RVA: 0x000102F4 File Offset: 0x0000E4F4
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x06001719 RID: 5913 RVA: 0x000EF5F0 File Offset: 0x000ED7F0
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

	// Token: 0x0600171A RID: 5914 RVA: 0x000EF6E8 File Offset: 0x000ED8E8
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

	// Token: 0x0600171B RID: 5915 RVA: 0x000EF7F0 File Offset: 0x000ED9F0
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

	// Token: 0x0600171C RID: 5916 RVA: 0x000EF89C File Offset: 0x000EDA9C
	private void ShowDaten()
	{
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
			this.tS_.GetText(1842),
			"\n",
			this.tS_.GetText(1238),
			"\n",
			this.tS_.GetText(1575),
			"\n\n<size=18>",
			this.tS_.GetText(724),
			"</size>"
		});
		float num = (float)this.gS_.merchGesamtGewinn / 1000000f;
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
			"\n\n"
		});
		Text component;
		if (num < 0f)
		{
			component = this.uiObjects[17].GetComponent<Text>();
			component.text = string.Concat(new object[]
			{
				component.text,
				"<color=red>",
				this.mS_.Round(num, 2),
				" ",
				this.tS_.GetText(1483),
				"</color>\n"
			});
		}
		else
		{
			component = this.uiObjects[17].GetComponent<Text>();
			component.text = string.Concat(new object[]
			{
				component.text,
				"<color=green>",
				this.mS_.Round(num, 2),
				" ",
				this.tS_.GetText(1483),
				"</color>\n"
			});
		}
		component = this.uiObjects[17].GetComponent<Text>();
		component.text = string.Concat(new object[]
		{
			component.text,
			"<color=green>",
			this.mS_.Round(this.gesamtUmsatz, 2),
			" ",
			this.tS_.GetText(1483),
			"</color>\n<color=red>",
			this.mS_.Round(this.gesamtAusgaben, 2),
			" ",
			this.tS_.GetText(1483),
			"</color>\n\n"
		});
		if (this.gesamtUmsatz - this.gesamtAusgaben + num >= 0f)
		{
			component = this.uiObjects[17].GetComponent<Text>();
			component.text = string.Concat(new object[]
			{
				component.text,
				"<size=18><color=green>",
				this.mS_.Round(this.gesamtUmsatz - this.gesamtAusgaben + num, 2),
				" ",
				this.tS_.GetText(1483),
				"</color></size>"
			});
			return;
		}
		component = this.uiObjects[17].GetComponent<Text>();
		component.text = string.Concat(new object[]
		{
			component.text,
			"<size=18><color=red>",
			this.mS_.Round(this.gesamtUmsatz - this.gesamtAusgaben + num, 2),
			" ",
			this.tS_.GetText(1483),
			"</color></size>"
		});
	}

	// Token: 0x0600171D RID: 5917 RVA: 0x000EFF54 File Offset: 0x000EE154
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

	// Token: 0x0600171E RID: 5918 RVA: 0x00010302 File Offset: 0x0000E502
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.playerGame && script_.mainIP == this.gS_.myID;
	}

	// Token: 0x0600171F RID: 5919 RVA: 0x000F0258 File Offset: 0x000EE458
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

	// Token: 0x06001720 RID: 5920 RVA: 0x000F0414 File Offset: 0x000EE614
	public void BUTTON_Close()
	{
		if (this.gS_)
		{
			if (this.uiObjects[18].GetComponent<InputField>().text.Length > 0)
			{
				this.gS_.ipName = this.uiObjects[18].GetComponent<InputField>().text;
			}
			if (this.mS_.multiplayer)
			{
				if (this.mS_.mpCalls_.isServer)
				{
					this.mS_.mpCalls_.SERVER_Send_GameData(this.gS_);
				}
				if (this.mS_.mpCalls_.isClient && this.gS_.playerGame)
				{
					this.mS_.mpCalls_.CLIENT_Send_GameData(this.gS_);
				}
			}
		}
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001AF6 RID: 6902
	public GameObject[] uiPrefabs;

	// Token: 0x04001AF7 RID: 6903
	public GameObject[] uiObjects;

	// Token: 0x04001AF8 RID: 6904
	private mainScript mS_;

	// Token: 0x04001AF9 RID: 6905
	private GameObject main_;

	// Token: 0x04001AFA RID: 6906
	private GUI_Main guiMain_;

	// Token: 0x04001AFB RID: 6907
	private sfxScript sfx_;

	// Token: 0x04001AFC RID: 6908
	private textScript tS_;

	// Token: 0x04001AFD RID: 6909
	private genres genres_;

	// Token: 0x04001AFE RID: 6910
	public gameScript gS_;

	// Token: 0x04001AFF RID: 6911
	private int gameAnzahl;

	// Token: 0x04001B00 RID: 6912
	private int gameAnzahlForReview;

	// Token: 0x04001B01 RID: 6913
	private int numGOTY;

	// Token: 0x04001B02 RID: 6914
	private int numHit;

	// Token: 0x04001B03 RID: 6915
	private int numTrend;

	// Token: 0x04001B04 RID: 6916
	private int numGold;

	// Token: 0x04001B05 RID: 6917
	private int numPlatin;

	// Token: 0x04001B06 RID: 6918
	private int numDiamant;

	// Token: 0x04001B07 RID: 6919
	private int gesamtReview;

	// Token: 0x04001B08 RID: 6920
	private int bestReview;

	// Token: 0x04001B09 RID: 6921
	private int badReview;

	// Token: 0x04001B0A RID: 6922
	private string bestReviewName = "";

	// Token: 0x04001B0B RID: 6923
	private string badReviewName = "";

	// Token: 0x04001B0C RID: 6924
	private float gesamtSells;

	// Token: 0x04001B0D RID: 6925
	private float gesamtDownloads;

	// Token: 0x04001B0E RID: 6926
	private float gesamtAbos;

	// Token: 0x04001B0F RID: 6927
	private float gesamtUmsatz;

	// Token: 0x04001B10 RID: 6928
	private float gesamtAusgaben;
}
