﻿using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_QA_Spielbericht : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.Find("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.themes_)
		{
			this.themes_ = this.main_.GetComponent<themes>();
		}
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
	}

	
	private void Update()
	{
		if (this.uiObjects[32].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[33].GetComponent<Scrollbar>().value = 1f;
		}
	}

	
	public void Init(gameScript game_)
	{
		this.FindScripts();
		this.gS_ = game_;
		this.uiObjects[0].GetComponent<Text>().text = game_.GetNameWithTag();
		this.uiObjects[1].GetComponent<Text>().text = string.Concat(new string[]
		{
			this.tS_.GetText(217),
			": <color=blue>",
			game_.GetReleaseDateString(),
			"</color>\n",
			this.tS_.GetText(1293),
			": <color=blue>",
			game_.GetEntwicklungsbeginnDateString(),
			"</color>"
		});
		this.uiObjects[2].GetComponent<Image>().sprite = game_.GetTypSprite();
		this.uiObjects[3].GetComponent<Image>().sprite = game_.GetPlatformTypSprite();
		this.uiObjects[4].GetComponent<Text>().text = Mathf.RoundToInt(this.genres_.genres_GAMEPLAY[game_.maingenre]).ToString() + "%";
		this.uiObjects[5].GetComponent<Text>().text = Mathf.RoundToInt(this.genres_.genres_GRAPHIC[game_.maingenre]).ToString() + "%";
		this.uiObjects[6].GetComponent<Text>().text = Mathf.RoundToInt(this.genres_.genres_SOUND[game_.maingenre]).ToString() + "%";
		this.uiObjects[7].GetComponent<Text>().text = Mathf.RoundToInt(this.genres_.genres_CONTROL[game_.maingenre]).ToString() + "%";
		this.uiObjects[2].GetComponent<tooltip>().c = this.gS_.GetTypString();
		this.uiObjects[3].GetComponent<tooltip>().c = this.gS_.GetPlatformTypString();
		string text = this.tS_.GetText(931);
		text = text.Replace("<NAME>", this.genres_.GetName(game_.maingenre));
		this.uiObjects[8].GetComponent<Text>().text = text;
		this.uiObjects[9].GetComponent<Text>().text = this.tS_.GetThemes(game_.gameMainTheme);
		this.uiObjects[12].SetActive(this.themes_.IsThemesFitWithGenre(game_.gameMainTheme, game_.maingenre));
		if (game_.gameSubTheme != -1)
		{
			this.uiObjects[10].GetComponent<Text>().text = this.tS_.GetThemes(game_.gameSubTheme);
			this.uiObjects[13].SetActive(this.themes_.IsThemesFitWithGenre(game_.gameSubTheme, game_.maingenre));
		}
		else
		{
			this.uiObjects[10].GetComponent<Text>().text = this.tS_.GetText(932);
			this.uiObjects[13].SetActive(false);
		}
		this.uiObjects[11].GetComponent<Text>().text = this.tS_.GetText(336) + ": " + game_.GetZielgruppeString();
		this.uiObjects[14].SetActive(this.genres_.IsTargetGroup(game_.maingenre, game_.gameZielgruppe));
		if (game_.subgenre == -1)
		{
			this.uiObjects[34].GetComponent<Text>().text = this.tS_.GetText(1462) + " <color=yellow>[" + game_.GetGenreString() + "]</color>";
		}
		else
		{
			this.uiObjects[34].GetComponent<Text>().text = string.Concat(new string[]
			{
				this.tS_.GetText(1462),
				" <color=yellow>[",
				game_.GetGenreString(),
				" / ",
				game_.GetSubGenreString(),
				"]</color>"
			});
		}
		if (game_.subgenre == -1)
		{
			this.uiObjects[35].GetComponent<Text>().text = this.tS_.GetText(1463) + " <color=yellow>[" + game_.GetGenreString() + "]</color>";
		}
		else
		{
			this.uiObjects[35].GetComponent<Text>().text = string.Concat(new string[]
			{
				this.tS_.GetText(1463),
				" <color=yellow>[",
				game_.GetGenreString(),
				" / ",
				game_.GetSubGenreString(),
				"]</color>"
			});
		}
		this.UpdateDesignSettings();
		text = this.tS_.GetText(933);
		text = text.Replace("<NAME>", this.genres_.GetName(game_.maingenre));
		this.uiObjects[31].GetComponent<Text>().text = text;
		for (int i = 0; i < this.genres_.genres_LEVEL.Length; i++)
		{
			if (this.genres_.genres_UNLOCK[i] && this.genres_.IsGenreCombination(game_.maingenre, i))
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[30].transform);
				gameObject.GetComponent<Image>().sprite = this.genres_.GetPic(i);
				gameObject.GetComponent<tooltip>().c = this.genres_.GetName(i);
			}
		}
	}

	
	public void UpdateDesignSettings()
	{
		for (int i = 0; i < this.uiDesignschwerpunkte.Length; i++)
		{
			this.uiDesignschwerpunkte[i].transform.GetChild(1).GetComponent<Slider>().value = (float)this.gS_.Designschwerpunkt[i];
			this.uiDesignschwerpunkte[i].transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = this.gS_.Designschwerpunkt[i].ToString();
			if (this.gS_.maingenre != -1)
			{
				if (this.genres_.GetFocus(i, this.gS_.maingenre, this.gS_.subgenre) == this.gS_.Designschwerpunkt[i])
				{
					this.uiDesignschwerpunkte[i].transform.GetChild(3).transform.GetChild(0).gameObject.SetActive(true);
				}
				else
				{
					this.uiDesignschwerpunkte[i].transform.GetChild(3).transform.GetChild(0).gameObject.SetActive(false);
				}
			}
		}
		for (int j = 0; j < this.uiDesignausrichtung.Length; j++)
		{
			this.uiDesignausrichtung[j].transform.GetChild(1).GetComponent<Slider>().value = (float)this.gS_.Designausrichtung[j];
			this.uiDesignausrichtung[j].transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = this.gS_.Designausrichtung[j].ToString();
			if (this.gS_.maingenre != -1)
			{
				if (this.genres_.GetAlign(j, this.gS_.maingenre, this.gS_.subgenre) == this.gS_.Designausrichtung[j])
				{
					this.uiDesignausrichtung[j].transform.GetChild(3).transform.GetChild(0).gameObject.SetActive(true);
				}
				else
				{
					this.uiDesignausrichtung[j].transform.GetChild(3).transform.GetChild(0).gameObject.SetActive(false);
				}
			}
		}
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Spielbeschreibung()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[199]);
		this.guiMain_.uiObjects[199].GetComponent<Menu_Dev_ShowBeschreibung>().Init(this.gS_);
	}

	
	public void BUTTON_Review()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[46].SetActive(true);
		this.guiMain_.uiObjects[46].GetComponent<Menu_Review>().Init(this.gS_);
	}

	
	public void BUTTON_Fanbriefe()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[112].SetActive(true);
		this.guiMain_.uiObjects[112].GetComponent<Menu_Dev_Fanbriefe>().Init(this.gS_);
	}

	
	public GameObject[] uiPrefabs;

	
	public GameObject[] uiObjects;

	
	public GameObject[] uiDesignschwerpunkte;

	
	public GameObject[] uiDesignausrichtung;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private genres genres_;

	
	private engineFeatures eF_;

	
	private gameScript gS_;

	
	private themes themes_;
}
