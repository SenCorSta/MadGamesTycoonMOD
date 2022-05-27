using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Stats_ShowBestIPs : MonoBehaviour
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

	
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	
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

	
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	
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

	
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.mainIP == this.gS_.myID && !script_.pubAngebot && !script_.auftragsspiel;
	}

	
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

	
	public void BUTTON_Close()
	{
		if (this.uiObjects[18].GetComponent<InputField>().text.Length > 0)
		{
			this.gS_.ipName = this.uiObjects[18].GetComponent<InputField>().text;
		}
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public GameObject[] uiPrefabs;

	
	public GameObject[] uiObjects;

	
	private mainScript mS_;

	
	private GameObject main_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private textScript tS_;

	
	private genres genres_;

	
	private games games_;

	
	public gameScript gS_;

	
	private int gameAnzahl;

	
	private int gameAnzahlForReview;

	
	private int numGOTY;

	
	private int numHit;

	
	private int numTrend;

	
	private int numGold;

	
	private int numPlatin;

	
	private int numDiamant;

	
	private int gesamtReview;

	
	private int bestReview;

	
	private int badReview;

	
	private string bestReviewName = "";

	
	private string badReviewName = "";

	
	private float gesamtSells;

	
	private float gesamtDownloads;

	
	private float gesamtAbos;

	
	private float gesamtUmsatz;

	
	private float gesamtAusgaben;
}
