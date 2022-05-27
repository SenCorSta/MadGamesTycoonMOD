using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200011E RID: 286
public class Menu_DevGame_Genre : MonoBehaviour
{
	// Token: 0x060009DB RID: 2523 RVA: 0x000071DC File Offset: 0x000053DC
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060009DC RID: 2524 RVA: 0x0007D644 File Offset: 0x0007B844
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

	// Token: 0x060009DD RID: 2525 RVA: 0x000071E4 File Offset: 0x000053E4
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060009DE RID: 2526 RVA: 0x0007D734 File Offset: 0x0007B934
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

	// Token: 0x060009DF RID: 2527 RVA: 0x0007D788 File Offset: 0x0007B988
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

	// Token: 0x060009E0 RID: 2528 RVA: 0x0000721C File Offset: 0x0000541C
	public void Init(int g)
	{
		this.FindScripts();
		this.genreArt = g;
		this.InitDropdowns();
		this.SetData(this.genreArt);
	}

	// Token: 0x060009E1 RID: 2529 RVA: 0x0007D7E0 File Offset: 0x0007B9E0
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

	// Token: 0x060009E2 RID: 2530 RVA: 0x0000723D File Offset: 0x0000543D
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060009E3 RID: 2531 RVA: 0x0007DB58 File Offset: 0x0007BD58
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

	// Token: 0x060009E4 RID: 2532 RVA: 0x00007258 File Offset: 0x00005458
	public void BUTTON_GenreBeliebtheit()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[280].SetActive(true);
	}

	// Token: 0x060009E5 RID: 2533 RVA: 0x0000727E File Offset: 0x0000547E
	public void BUTTON_Marktanalyse()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[136].SetActive(true);
	}

	// Token: 0x060009E6 RID: 2534 RVA: 0x0007DBBC File Offset: 0x0007BDBC
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

	// Token: 0x060009E7 RID: 2535 RVA: 0x0007DC70 File Offset: 0x0007BE70
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

	// Token: 0x04000E32 RID: 3634
	public int genreArt;

	// Token: 0x04000E33 RID: 3635
	private mainScript mS_;

	// Token: 0x04000E34 RID: 3636
	private GameObject main_;

	// Token: 0x04000E35 RID: 3637
	private GUI_Main guiMain_;

	// Token: 0x04000E36 RID: 3638
	private sfxScript sfx_;

	// Token: 0x04000E37 RID: 3639
	private textScript tS_;

	// Token: 0x04000E38 RID: 3640
	private genres genres_;

	// Token: 0x04000E39 RID: 3641
	private Menu_DevGame mDevGame_;

	// Token: 0x04000E3A RID: 3642
	public GameObject[] uiPrefabs;

	// Token: 0x04000E3B RID: 3643
	public GameObject[] uiObjects;

	// Token: 0x04000E3C RID: 3644
	private float updateTimer;
}
