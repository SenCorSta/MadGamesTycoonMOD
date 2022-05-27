using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000122 RID: 290
public class Menu_DevGame_Theme : MonoBehaviour
{
	// Token: 0x06000A06 RID: 2566 RVA: 0x000073D8 File Offset: 0x000055D8
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000A07 RID: 2567 RVA: 0x0007EE38 File Offset: 0x0007D038
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

	// Token: 0x06000A08 RID: 2568 RVA: 0x000073E0 File Offset: 0x000055E0
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000A09 RID: 2569 RVA: 0x0007EF98 File Offset: 0x0007D198
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

	// Token: 0x06000A0A RID: 2570 RVA: 0x0007EFEC File Offset: 0x0007D1EC
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

	// Token: 0x06000A0B RID: 2571 RVA: 0x00007418 File Offset: 0x00005618
	public void Init(int g)
	{
		this.uiObjects[7].GetComponent<InputField>().text = "";
		this.FindScripts();
		this.InitDropdowns();
		this.themeArt = g;
		this.SetData(this.themeArt);
	}

	// Token: 0x06000A0C RID: 2572 RVA: 0x0007F054 File Offset: 0x0007D254
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

	// Token: 0x06000A0D RID: 2573 RVA: 0x0007F3F4 File Offset: 0x0007D5F4
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

	// Token: 0x06000A0E RID: 2574 RVA: 0x0007F544 File Offset: 0x0007D744
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

	// Token: 0x06000A0F RID: 2575 RVA: 0x0007F60C File Offset: 0x0007D80C
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

	// Token: 0x06000A10 RID: 2576 RVA: 0x00007450 File Offset: 0x00005650
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000A11 RID: 2577 RVA: 0x0007F7C0 File Offset: 0x0007D9C0
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

	// Token: 0x06000A12 RID: 2578 RVA: 0x0007F87C File Offset: 0x0007DA7C
	public void BUTTON_Search()
	{
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData(this.themeArt);
	}

	// Token: 0x06000A13 RID: 2579 RVA: 0x0000746B File Offset: 0x0000566B
	public void BUTTON_Marktanalyse()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[136].SetActive(true);
	}

	// Token: 0x04000E58 RID: 3672
	public int themeArt;

	// Token: 0x04000E59 RID: 3673
	private mainScript mS_;

	// Token: 0x04000E5A RID: 3674
	private GameObject main_;

	// Token: 0x04000E5B RID: 3675
	private GUI_Main guiMain_;

	// Token: 0x04000E5C RID: 3676
	private sfxScript sfx_;

	// Token: 0x04000E5D RID: 3677
	private textScript tS_;

	// Token: 0x04000E5E RID: 3678
	private themes themes_;

	// Token: 0x04000E5F RID: 3679
	private games games_;

	// Token: 0x04000E60 RID: 3680
	private Menu_DevGame mDevGame_;

	// Token: 0x04000E61 RID: 3681
	private Menu_Dev_AddonDo mDevAddon_;

	// Token: 0x04000E62 RID: 3682
	private Menu_Dev_MMOAddon mDevMMOAddon_;

	// Token: 0x04000E63 RID: 3683
	public GameObject[] uiPrefabs;

	// Token: 0x04000E64 RID: 3684
	public GameObject[] uiObjects;

	// Token: 0x04000E65 RID: 3685
	private float updateTimer;
}
