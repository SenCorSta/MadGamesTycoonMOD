﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Dev_ShowConcept : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
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
		if (!this.licences_)
		{
			this.licences_ = this.main_.GetComponent<licences>();
		}
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
		if (!this.cmS_)
		{
			this.cmS_ = GameObject.Find("CamMovement").GetComponent<cameraMovementScript>();
		}
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
		}
		if (!this.gF_)
		{
			this.gF_ = this.main_.GetComponent<gameplayFeatures>();
		}
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
	}

	
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
		if (this.uiObjects[35].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[36].GetComponent<Scrollbar>().value = 1f;
		}
	}

	
	public void Init(gameScript gameScript_)
	{
		this.FindScripts();
		this.gS_ = gameScript_;
		this.uiObjects[0].GetComponent<Text>().text = this.gS_.GetNameWithTag();
		this.uiObjects[7].GetComponent<tooltip>().c = this.gS_.GetTypString();
		this.uiObjects[39].GetComponent<tooltip>().c = this.gS_.GetPlatformTypString();
		this.uiObjects[1].GetComponent<Text>().text = Mathf.RoundToInt(this.gS_.points_gameplay).ToString();
		this.uiObjects[2].GetComponent<Text>().text = Mathf.RoundToInt(this.gS_.points_grafik).ToString();
		this.uiObjects[3].GetComponent<Text>().text = Mathf.RoundToInt(this.gS_.points_sound).ToString();
		this.uiObjects[4].GetComponent<Text>().text = Mathf.RoundToInt(this.gS_.points_technik).ToString();
		this.uiObjects[40].GetComponent<Image>().sprite = this.games_.gamePEGI[this.gS_.usk];
		this.uiObjects[7].GetComponent<Image>().sprite = this.gS_.GetTypSprite();
		this.uiObjects[39].GetComponent<Image>().sprite = this.gS_.GetPlatformTypSprite();
		this.uiObjects[24].GetComponent<Image>().sprite = this.gS_.GetSizeSprite();
		this.uiObjects[41].GetComponent<Component_Aufwertungen>().Init(this.gS_);
		string text = "<b>" + this.tS_.GetText(158) + ":</b> " + this.genres_.GetName(this.gS_.maingenre);
		if (this.gS_.subgenre != -1)
		{
			text = text + " / " + this.genres_.GetName(this.gS_.subgenre);
		}
		text = string.Concat(new string[]
		{
			text,
			"\n<b>",
			this.tS_.GetText(159),
			":</b> ",
			this.tS_.GetThemes(this.gS_.gameMainTheme)
		});
		if (this.gS_.gameSubTheme != -1)
		{
			text = text + " / " + this.tS_.GetThemes(this.gS_.gameSubTheme);
		}
		this.uiObjects[8].GetComponent<Text>().text = text;
		Text component = this.uiObjects[8].GetComponent<Text>();
		component.text = string.Concat(new string[]
		{
			component.text,
			"\n<b>",
			this.tS_.GetText(336),
			":</b> ",
			this.tS_.GetText(337 + this.gS_.gameZielgruppe)
		});
		if (this.gS_.gameLicence != -1)
		{
			component = this.uiObjects[8].GetComponent<Text>();
			component.text = string.Concat(new string[]
			{
				component.text,
				"\n<b>",
				this.tS_.GetText(356),
				":</b> ",
				this.licences_.GetName(this.gS_.gameLicence)
			});
		}
		for (int i = 0; i < this.gS_.gamePlatform.Length; i++)
		{
			if (this.gS_.gamePlatform[i] != -1)
			{
				GameObject gameObject = GameObject.Find("PLATFORM_" + this.gS_.gamePlatform[i].ToString());
				if (gameObject)
				{
					platformScript component2 = gameObject.GetComponent<platformScript>();
					this.uiObjects[9 + i].SetActive(true);
					component2.SetPic(this.uiObjects[9 + i]);
					this.uiObjects[9 + i].GetComponent<tooltip>().c = component2.GetTooltip();
				}
			}
			else
			{
				this.uiObjects[9 + i].SetActive(false);
			}
		}
		for (int j = 0; j < this.gS_.gameLanguage.Length; j++)
		{
			if (this.gS_.gameLanguage[j])
			{
				this.uiObjects[13 + j].GetComponent<Image>().color = Color.white;
			}
			else
			{
				this.uiObjects[13 + j].GetComponent<Image>().color = this.guiMain_.colors[6];
			}
		}
		this.UpdateDesignSettings();
		this.uiObjects[30].GetComponent<Slider>().value = (float)this.gS_.gameAP_Gameplay;
		this.uiObjects[31].GetComponent<Slider>().value = (float)this.gS_.gameAP_Grafik;
		this.uiObjects[32].GetComponent<Slider>().value = (float)this.gS_.gameAP_Sound;
		this.uiObjects[33].GetComponent<Slider>().value = (float)this.gS_.gameAP_Technik;
		this.uiObjects[42].GetComponent<Text>().text = (this.gS_.gameAP_Gameplay * 5).ToString() + "%";
		this.uiObjects[43].GetComponent<Text>().text = (this.gS_.gameAP_Grafik * 5).ToString() + "%";
		this.uiObjects[44].GetComponent<Text>().text = (this.gS_.gameAP_Sound * 5).ToString() + "%";
		this.uiObjects[45].GetComponent<Text>().text = (this.gS_.gameAP_Technik * 5).ToString() + "%";
		for (int k = 0; k < this.uiObjects[34].transform.childCount; k++)
		{
			UnityEngine.Object.Destroy(this.uiObjects[34].transform.GetChild(k).gameObject);
		}
		for (int l = 0; l < this.gF_.gameplayFeatures_LEVEL.Length; l++)
		{
			if (this.gS_.gameplayFeatures_DevDone[l])
			{
				Item_GameConcept_GameplayFeature component3 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[34].transform).GetComponent<Item_GameConcept_GameplayFeature>();
				component3.myID = l;
				component3.gF_ = this.gF_;
				component3.mS_ = this.mS_;
				component3.tS_ = this.tS_;
				component3.sfx_ = this.sfx_;
				component3.guiMain_ = this.guiMain_;
				component3.gS_ = this.gS_;
			}
		}
		if (!this.guiMain_.uiObjects[56].activeSelf || (this.guiMain_.uiObjects[56].activeSelf && this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().typ_remaster))
		{
			if (this.uiObjects[37].activeSelf)
			{
				this.uiObjects[37].SetActive(false);
			}
			if (this.uiObjects[46].activeSelf)
			{
				this.uiObjects[46].SetActive(false);
			}
		}
		else
		{
			if (!this.uiObjects[37].activeSelf)
			{
				this.uiObjects[37].SetActive(true);
			}
			if (!this.uiObjects[46].activeSelf)
			{
				this.uiObjects[46].SetActive(true);
			}
		}
		base.StartCoroutine(this.ResizeGameplayFeatures());
	}

	
	private IEnumerator ResizeGameplayFeatures()
	{
		this.uiObjects[34].SetActive(false);
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		this.uiObjects[34].SetActive(true);
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		this.uiObjects[38].SetActive(false);
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		this.uiObjects[38].SetActive(true);
		yield break;
	}

	
	public void BUTTON_Close()
	{
		for (int i = 0; i < this.uiObjects[34].transform.childCount; i++)
		{
			UnityEngine.Object.Destroy(this.uiObjects[34].transform.GetChild(i).gameObject);
		}
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_CopyDesigneinstellungen()
	{
		if (!this.gS_)
		{
			return;
		}
		this.sfx_.PlaySound(3, true);
		Menu_DevGame component = this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>();
		component.CopyDesignSettings(this.gS_);
		component.uiObjects[97].GetComponent<Slider>().value = (float)this.gS_.gameAP_Gameplay;
		component.SetAP_Gameplay();
		component.uiObjects[98].GetComponent<Slider>().value = (float)this.gS_.gameAP_Grafik;
		component.SetAP_Grafik();
		component.uiObjects[99].GetComponent<Slider>().value = (float)this.gS_.gameAP_Sound;
		component.SetAP_Sound();
		component.uiObjects[100].GetComponent<Slider>().value = (float)this.gS_.gameAP_Technik;
		component.SetAP_Technik();
		this.guiMain_.uiObjects[109].SetActive(false);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_CopyConcept()
	{
		if (!this.gS_)
		{
			return;
		}
		this.sfx_.PlaySound(3, true);
		Menu_DevGame component = this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>();
		if (!component.typ_contractGame)
		{
			component.SetGameTyp(this.gS_.gameTyp);
			component.SetGameSize(this.gS_.gameSize);
			if (!component.typ_nachfolger)
			{
				component.SetMainGenre(this.gS_.maingenre);
			}
		}
		component.SetZielgruppe(this.gS_.gameZielgruppe);
		component.SetSubGenre(this.gS_.subgenre);
		component.SetMainTheme(this.gS_.gameMainTheme);
		component.SetSubTheme(this.gS_.gameSubTheme);
		component.CopyDesignSettings(this.gS_);
		component.uiObjects[97].GetComponent<Slider>().value = (float)this.gS_.gameAP_Gameplay;
		component.SetAP_Gameplay();
		component.uiObjects[98].GetComponent<Slider>().value = (float)this.gS_.gameAP_Grafik;
		component.SetAP_Grafik();
		component.uiObjects[99].GetComponent<Slider>().value = (float)this.gS_.gameAP_Sound;
		component.SetAP_Sound();
		component.uiObjects[100].GetComponent<Slider>().value = (float)this.gS_.gameAP_Technik;
		component.SetAP_Technik();
		for (int i = 0; i < this.gS_.gameLanguage.Length; i++)
		{
			component.g_GameLanguage[i] = !this.gS_.gameLanguage[i];
			component.SetLanguage(i);
		}
		for (int j = 0; j < this.gS_.gameGameplayFeatures.Length; j++)
		{
			component.g_GameGameplayFeatures[j] = !this.gS_.gameGameplayFeatures[j];
			component.SetGameplayFeature(j);
		}
		this.guiMain_.uiObjects[109].SetActive(false);
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

	
	public void UpdateDesignSettings()
	{
		for (int i = 0; i < this.uiDesignschwerpunkte.Length; i++)
		{
			this.uiDesignschwerpunkte[i].transform.GetChild(1).GetComponent<Slider>().value = (float)this.gS_.Designschwerpunkt[i];
			this.uiDesignschwerpunkte[i].transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = this.gS_.Designschwerpunkt[i].ToString();
			if (this.gS_.maingenre != -1 && this.genres_.GetFocusKnown(i, this.gS_.maingenre, this.gS_.subgenre) && this.genres_.GetFocus(i, this.gS_.maingenre, this.gS_.subgenre) == this.gS_.Designschwerpunkt[i])
			{
				this.uiDesignschwerpunkte[i].transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = "<color=green>" + this.gS_.Designschwerpunkt[i].ToString() + "</color>";
			}
		}
		for (int j = 0; j < this.uiDesignausrichtung.Length; j++)
		{
			this.uiDesignausrichtung[j].transform.GetChild(1).GetComponent<Slider>().value = (float)this.gS_.Designausrichtung[j];
			this.uiDesignausrichtung[j].transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = this.gS_.Designausrichtung[j].ToString();
			if (this.gS_.maingenre != -1 && this.genres_.GetAlignKnown(j, this.gS_.maingenre, this.gS_.subgenre) && this.genres_.GetAlign(j, this.gS_.maingenre, this.gS_.subgenre) == this.gS_.Designausrichtung[j])
			{
				this.uiDesignausrichtung[j].transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = "<color=green>" + this.gS_.Designausrichtung[j].ToString() + "</color>";
			}
		}
	}

	
	public GameObject[] uiPrefabs;

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private genres genres_;

	
	private themes themes_;

	
	private licences licences_;

	
	private engineFeatures eF_;

	
	private cameraMovementScript cmS_;

	
	private unlockScript unlock_;

	
	private gameplayFeatures gF_;

	
	private games games_;

	
	private gameScript gS_;

	
	public GameObject[] uiDesignschwerpunkte;

	
	public GameObject[] uiDesignausrichtung;
}
