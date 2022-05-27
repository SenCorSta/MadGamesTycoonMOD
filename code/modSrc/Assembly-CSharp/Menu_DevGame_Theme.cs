using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000123 RID: 291
public class Menu_DevGame_Theme : MonoBehaviour
{
	// Token: 0x06000A15 RID: 2581 RVA: 0x0006E29C File Offset: 0x0006C49C
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000A16 RID: 2582 RVA: 0x0006E2A4 File Offset: 0x0006C4A4
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

	// Token: 0x06000A17 RID: 2583 RVA: 0x0006E402 File Offset: 0x0006C602
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000A18 RID: 2584 RVA: 0x0006E43C File Offset: 0x0006C63C
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

	// Token: 0x06000A19 RID: 2585 RVA: 0x0006E490 File Offset: 0x0006C690
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

	// Token: 0x06000A1A RID: 2586 RVA: 0x0006E4F6 File Offset: 0x0006C6F6
	public void Init(int g)
	{
		this.uiObjects[7].GetComponent<InputField>().text = "";
		this.FindScripts();
		this.InitDropdowns();
		this.themeArt = g;
		this.SetData(this.themeArt);
	}

	// Token: 0x06000A1B RID: 2587 RVA: 0x0006E530 File Offset: 0x0006C730
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
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[4]);
	}

	// Token: 0x06000A1C RID: 2588 RVA: 0x0006E8A0 File Offset: 0x0006CAA0
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
				if (this.games_.arrayGamesScripts[i] && this.games_.arrayGamesScripts[i].spielbericht && this.games_.arrayGamesScripts[i].maingenre == num && (this.games_.arrayGamesScripts[i].ownerID == this.mS_.myID || this.games_.arrayGamesScripts[i].developerID == this.mS_.myID) && (this.games_.arrayGamesScripts[i].gameMainTheme == theme_ || this.games_.arrayGamesScripts[i].gameSubTheme == theme_))
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

	// Token: 0x06000A1D RID: 2589 RVA: 0x0006EA08 File Offset: 0x0006CC08
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

	// Token: 0x06000A1E RID: 2590 RVA: 0x0006EAD0 File Offset: 0x0006CCD0
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

	// Token: 0x06000A1F RID: 2591 RVA: 0x0006EC84 File Offset: 0x0006CE84
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000A20 RID: 2592 RVA: 0x0006ECA0 File Offset: 0x0006CEA0
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

	// Token: 0x06000A21 RID: 2593 RVA: 0x0006ED5C File Offset: 0x0006CF5C
	public void BUTTON_Search()
	{
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData(this.themeArt);
	}

	// Token: 0x06000A22 RID: 2594 RVA: 0x0006EDB0 File Offset: 0x0006CFB0
	public void BUTTON_Marktanalyse()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[136].SetActive(true);
	}

	// Token: 0x04000E60 RID: 3680
	public int themeArt;

	// Token: 0x04000E61 RID: 3681
	private mainScript mS_;

	// Token: 0x04000E62 RID: 3682
	private GameObject main_;

	// Token: 0x04000E63 RID: 3683
	private GUI_Main guiMain_;

	// Token: 0x04000E64 RID: 3684
	private sfxScript sfx_;

	// Token: 0x04000E65 RID: 3685
	private textScript tS_;

	// Token: 0x04000E66 RID: 3686
	private themes themes_;

	// Token: 0x04000E67 RID: 3687
	private games games_;

	// Token: 0x04000E68 RID: 3688
	private Menu_DevGame mDevGame_;

	// Token: 0x04000E69 RID: 3689
	private Menu_Dev_AddonDo mDevAddon_;

	// Token: 0x04000E6A RID: 3690
	private Menu_Dev_MMOAddon mDevMMOAddon_;

	// Token: 0x04000E6B RID: 3691
	public GameObject[] uiPrefabs;

	// Token: 0x04000E6C RID: 3692
	public GameObject[] uiObjects;

	// Token: 0x04000E6D RID: 3693
	private float updateTimer;
}
