using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Menu_DevGame_Theme : MonoBehaviour
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
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
		if (!this.mDevGame_)
		{
			this.mDevGame_ = this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>();
		}
		if (!this.mDevAddon_)
		{
			this.mDevAddon_ = this.guiMain_.uiObjects[193].GetComponent<Menu_Dev_AddonDo>();
		}
		if (!this.mDevMMOAddon_)
		{
			this.mDevMMOAddon_ = this.guiMain_.uiObjects[247].GetComponent<Menu_Dev_MMOAddon>();
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
		this.SetData(this.themeArt);
	}

	
	private bool Exists(GameObject parent_, int id_)
	{
		if (!this.mS_.multiplayer)
		{
			return false;
		}
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_DevGame_Theme>().myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	
	public void Init(int g)
	{
		this.uiObjects[7].GetComponent<InputField>().text = "";
		this.FindScripts();
		this.InitDropdowns();
		this.themeArt = g;
		this.SetData(this.themeArt);
	}

	
	private void SetData(int g)
	{
		if (g == 0)
		{
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(352);
		}
		else
		{
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(353);
		}
		this.uiObjects[6].GetComponent<Button>().interactable = true;
		if (g == 0)
		{
			if (this.guiMain_.uiObjects[56].activeSelf && this.mDevGame_.g_GameMainTheme == -1)
			{
				this.uiObjects[6].GetComponent<Button>().interactable = false;
			}
		}
		else
		{
			if (this.guiMain_.uiObjects[56].activeSelf && this.mDevGame_.g_GameSubTheme == -1)
			{
				this.uiObjects[6].GetComponent<Button>().interactable = false;
			}
			if (this.guiMain_.uiObjects[193].activeSelf && this.mDevAddon_.g_GameSubTheme == -1)
			{
				this.uiObjects[6].GetComponent<Button>().interactable = false;
			}
			if (this.guiMain_.uiObjects[247].activeSelf && this.mDevMMOAddon_.g_GameSubTheme == -1)
			{
				this.uiObjects[6].GetComponent<Button>().interactable = false;
			}
		}
		string text = this.uiObjects[7].GetComponent<InputField>().text;
		int length = this.uiObjects[7].GetComponent<InputField>().text.Length;
		for (int i = 0; i < this.themes_.themes_LEVEL.Length; i++)
		{
			if (this.themes_.IsErforscht(i) && !this.Exists(this.uiObjects[0], i))
			{
				bool flag = false;
				if (length > 0 && this.tS_.GetThemes(i).ToLower().Contains(text.ToLower()))
				{
					flag = true;
				}
				if (length <= 0 || flag)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform);
					Item_DevGame_Theme component = gameObject.GetComponent<Item_DevGame_Theme>();
					component.myID = i;
					component.mS_ = this.mS_;
					component.tS_ = this.tS_;
					component.sfx_ = this.sfx_;
					component.guiMain_ = this.guiMain_;
					component.themes_ = this.themes_;
					component.themeArt = g;
					component.fitGenre = this.FitGenre(i);
					if (this.guiMain_.uiObjects[56].activeSelf && (i == this.mDevGame_.g_GameSubTheme || i == this.mDevGame_.g_GameMainTheme))
					{
						gameObject.GetComponent<Button>().interactable = false;
					}
					if (this.guiMain_.uiObjects[193].activeSelf && (i == this.mDevAddon_.g_GameSubTheme || i == this.mDevAddon_.gS_.gameMainTheme))
					{
						gameObject.GetComponent<Button>().interactable = false;
					}
					if (this.guiMain_.uiObjects[247].activeSelf && (i == this.mDevMMOAddon_.g_GameSubTheme || i == this.mDevMMOAddon_.gS_.gameMainTheme))
					{
						gameObject.GetComponent<Button>().interactable = false;
					}
					if (this.themes_.IsThemesFitWithGenre(i, this.mDevGame_.g_GameMainGenre))
					{
						gameObject.GetComponent<Image>().color = Color.green;
					}
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[4]);
	}

	
	private int FitGenre(int theme_)
	{
		int num = -1;
		if (this.guiMain_.uiObjects[56].activeSelf)
		{
			num = this.mDevGame_.g_GameMainGenre;
		}
		if (this.guiMain_.uiObjects[193].activeSelf)
		{
			num = this.mDevAddon_.gS_.maingenre;
		}
		if (this.guiMain_.uiObjects[247].activeSelf)
		{
			num = this.mDevMMOAddon_.gS_.maingenre;
		}
		if (num != -1)
		{
			int i = 0;
			while (i < this.games_.arrayGamesScripts.Length)
			{
				if (this.games_.arrayGamesScripts[i] && this.games_.arrayGamesScripts[i].spielbericht && this.games_.arrayGamesScripts[i].maingenre == num && (this.games_.arrayGamesScripts[i].playerGame || this.games_.arrayGamesScripts[i].IsMyAuftragsspiel()) && (this.games_.arrayGamesScripts[i].gameMainTheme == theme_ || this.games_.arrayGamesScripts[i].gameSubTheme == theme_))
				{
					if (this.themes_.IsThemesFitWithGenre(theme_, num))
					{
						return 1;
					}
					return -1;
				}
				else
				{
					i++;
				}
			}
		}
		return 0;
	}

	
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[5].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(355));
		list.Add(this.tS_.GetText(5));
		list.Add(this.tS_.GetText(1665));
		list.Add(this.tS_.GetText(1894));
		list.Add(this.tS_.GetText(1895));
		this.uiObjects[5].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[5].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[5].GetComponent<Dropdown>().value = @int;
	}

	
	public void DROPDOWN_Sort()
	{
		int value = this.uiObjects[5].GetComponent<Dropdown>().value;
		PlayerPrefs.SetInt(this.uiObjects[5].name, value);
		int childCount = this.uiObjects[0].transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = this.uiObjects[0].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				Item_DevGame_Theme component = gameObject.GetComponent<Item_DevGame_Theme>();
				switch (value)
				{
				case 0:
					gameObject.name = this.tS_.GetThemes(component.myID);
					break;
				case 1:
					gameObject.name = this.themes_.themes_LEVEL[component.myID].ToString();
					break;
				case 2:
					gameObject.name = (-this.themes_.themes_MARKT[component.myID]).ToString();
					break;
				case 3:
					if (component.fitGenre == 1)
					{
						gameObject.name = "2";
					}
					if (component.fitGenre == 0)
					{
						gameObject.name = "1";
					}
					if (component.fitGenre == -1)
					{
						gameObject.name = "0";
					}
					break;
				case 4:
					if (component.fitGenre == -1)
					{
						gameObject.name = "2";
					}
					if (component.fitGenre == 0)
					{
						gameObject.name = "1";
					}
					if (component.fitGenre == 1)
					{
						gameObject.name = "0";
					}
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

	
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_ThemaEntfernen()
	{
		if (this.themeArt == 0)
		{
			if (this.guiMain_.uiObjects[56].activeSelf)
			{
				this.mDevGame_.SetMainTheme(-1);
			}
		}
		else
		{
			if (this.guiMain_.uiObjects[56].activeSelf)
			{
				this.mDevGame_.SetSubTheme(-1);
			}
			if (this.guiMain_.uiObjects[193].activeSelf)
			{
				this.mDevAddon_.SetSubTheme(-1);
			}
			if (this.guiMain_.uiObjects[247].activeSelf)
			{
				this.mDevMMOAddon_.SetSubTheme(-1);
			}
		}
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Search()
	{
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData(this.themeArt);
	}

	
	public void BUTTON_Marktanalyse()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[136].SetActive(true);
	}

	
	public int themeArt;

	
	private mainScript mS_;

	
	private GameObject main_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private textScript tS_;

	
	private themes themes_;

	
	private games games_;

	
	private Menu_DevGame mDevGame_;

	
	private Menu_Dev_AddonDo mDevAddon_;

	
	private Menu_Dev_MMOAddon mDevMMOAddon_;

	
	public GameObject[] uiPrefabs;

	
	public GameObject[] uiObjects;

	
	private float updateTimer;
}
