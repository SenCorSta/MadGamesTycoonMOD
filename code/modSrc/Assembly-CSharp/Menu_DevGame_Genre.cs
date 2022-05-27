using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Menu_DevGame_Genre : MonoBehaviour
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
		if (!this.mDevGame_)
		{
			this.mDevGame_ = this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>();
		}
	}

	
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	
	private void MultiplayerUpdate()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.updateTimer += Time.deltaTime;
		if (this.updateTimer < 5f)
		{
			return;
		}
		this.updateTimer = 0f;
		this.SetData(this.genreArt);
	}

	
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_DevGame_Genre>().myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	
	public void Init(int g)
	{
		this.FindScripts();
		this.genreArt = g;
		this.InitDropdowns();
		this.SetData(this.genreArt);
	}

	
	private void SetData(int g)
	{
		if (g == 0)
		{
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(343);
		}
		else
		{
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(344);
		}
		this.uiObjects[6].GetComponent<Button>().interactable = true;
		if (g == 0)
		{
			if (this.mDevGame_.g_GameMainGenre == -1)
			{
				this.uiObjects[6].GetComponent<Button>().interactable = false;
			}
		}
		else if (this.mDevGame_.g_GameSubGenre == -1)
		{
			this.uiObjects[6].GetComponent<Button>().interactable = false;
		}
		string text = this.tS_.GetText(812);
		text = text.Replace("<TEXT>", "<color=yellow>" + this.genres_.GetName(this.mS_.companySpecialGenre) + "</color>");
		this.uiObjects[5].GetComponent<Text>().text = text;
		text = this.tS_.GetText(1911);
		text = text.Replace("<NAME>", "<color=blue>" + this.genres_.GetName(this.mS_.companySpecialGenre) + "</color>");
		this.uiObjects[8].GetComponent<tooltip>().c = text;
		for (int i = 0; i < 5; i++)
		{
			if (this.mS_.lastGamesGenre[i] >= 0)
			{
				this.uiObjects[9 + i].transform.GetChild(0).GetComponent<Image>().sprite = this.genres_.GetPic(this.mS_.lastGamesGenre[i]);
				this.uiObjects[9 + i].GetComponent<tooltip>().c = this.genres_.GetName(this.mS_.lastGamesGenre[i]);
			}
			else
			{
				this.uiObjects[9 + i].transform.GetChild(0).GetComponent<Image>().sprite = this.guiMain_.uiSprites[19];
				this.uiObjects[9 + i].GetComponent<tooltip>().c = "";
			}
		}
		for (int j = 0; j < this.genres_.genres_RES_POINTS.Length; j++)
		{
			if (this.genres_.genres_UNLOCK[j] && this.genres_.IsErforscht(j) && !this.Exists(this.uiObjects[0], j))
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform);
				Item_DevGame_Genre component = gameObject.GetComponent<Item_DevGame_Genre>();
				component.myID = j;
				component.mS_ = this.mS_;
				component.tS_ = this.tS_;
				component.sfx_ = this.sfx_;
				component.guiMain_ = this.guiMain_;
				component.genres_ = this.genres_;
				component.genreArt = g;
				if (j == this.mDevGame_.g_GameSubGenre || j == this.mDevGame_.g_GameMainGenre)
				{
					gameObject.GetComponent<Button>().interactable = false;
				}
				if (this.genres_.IsGenreCombination(this.mDevGame_.g_GameMainGenre, j) && g != 0)
				{
					gameObject.GetComponent<Image>().color = Color.green;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[4]);
	}

	
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_GenreEntfernen()
	{
		if (this.genreArt == 0)
		{
			this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().SetMainGenre(-1);
		}
		else
		{
			this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().SetSubGenre(-1);
		}
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_GenreBeliebtheit()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[280].SetActive(true);
	}

	
	public void BUTTON_Marktanalyse()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[136].SetActive(true);
	}

	
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[7].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(355));
		list.Add(this.tS_.GetText(5));
		list.Add(this.tS_.GetText(1380));
		list.Add(this.tS_.GetText(1665));
		this.uiObjects[7].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[7].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[7].GetComponent<Dropdown>().value = @int;
	}

	
	public void DROPDOWN_Sort()
	{
		int value = this.uiObjects[7].GetComponent<Dropdown>().value;
		PlayerPrefs.SetInt(this.uiObjects[7].name, value);
		int childCount = this.uiObjects[0].transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = this.uiObjects[0].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				Item_DevGame_Genre component = gameObject.GetComponent<Item_DevGame_Genre>();
				switch (value)
				{
				case 0:
					gameObject.name = this.genres_.GetName(component.myID);
					break;
				case 1:
					gameObject.name = this.genres_.genres_LEVEL[component.myID].ToString();
					break;
				case 2:
					gameObject.name = this.genres_.GetFloatBeliebtheit(component.myID).ToString();
					break;
				case 3:
					gameObject.name = (-this.genres_.genres_MARKT[component.myID]).ToString();
					break;
				}
			}
		}
		if (value == 0)
		{
			this.mS_.SortChildrenByName(this.uiObjects[0]);
			return;
		}
		this.mS_.SortChildrenByFloat(this.uiObjects[0]);
	}

	
	public int genreArt;

	
	private mainScript mS_;

	
	private GameObject main_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private textScript tS_;

	
	private genres genres_;

	
	private Menu_DevGame mDevGame_;

	
	public GameObject[] uiPrefabs;

	
	public GameObject[] uiObjects;

	
	private float updateTimer;
}
