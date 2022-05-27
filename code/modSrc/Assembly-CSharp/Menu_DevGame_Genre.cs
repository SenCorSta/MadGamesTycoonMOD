using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200011F RID: 287
public class Menu_DevGame_Genre : MonoBehaviour
{
	// Token: 0x060009EA RID: 2538 RVA: 0x0006C8C7 File Offset: 0x0006AAC7
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060009EB RID: 2539 RVA: 0x0006C8D0 File Offset: 0x0006AAD0
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

	// Token: 0x060009EC RID: 2540 RVA: 0x0006C9BE File Offset: 0x0006ABBE
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060009ED RID: 2541 RVA: 0x0006C9F8 File Offset: 0x0006ABF8
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

	// Token: 0x060009EE RID: 2542 RVA: 0x0006CA4C File Offset: 0x0006AC4C
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

	// Token: 0x060009EF RID: 2543 RVA: 0x0006CAA3 File Offset: 0x0006ACA3
	public void Init(int g)
	{
		this.FindScripts();
		this.genreArt = g;
		this.InitDropdowns();
		this.SetData(this.genreArt);
	}

	// Token: 0x060009F0 RID: 2544 RVA: 0x0006CAC4 File Offset: 0x0006ACC4
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
		text = text.Replace("<TEXT>", "<color=yellow>" + this.genres_.GetName(this.mS_.GetFanGenreID()) + "</color>");
		this.uiObjects[5].GetComponent<Text>().text = text;
		text = this.tS_.GetText(1911);
		text = text.Replace("<NAME>", "<color=blue>" + this.genres_.GetName(this.mS_.GetFanGenreID()) + "</color>");
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
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[4]);
	}

	// Token: 0x060009F1 RID: 2545 RVA: 0x0006CE10 File Offset: 0x0006B010
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060009F2 RID: 2546 RVA: 0x0006CE2C File Offset: 0x0006B02C
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

	// Token: 0x060009F3 RID: 2547 RVA: 0x0006CE8E File Offset: 0x0006B08E
	public void BUTTON_GenreBeliebtheit()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[280].SetActive(true);
	}

	// Token: 0x060009F4 RID: 2548 RVA: 0x0006CEB4 File Offset: 0x0006B0B4
	public void BUTTON_Marktanalyse()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[136].SetActive(true);
	}

	// Token: 0x060009F5 RID: 2549 RVA: 0x0006CEDC File Offset: 0x0006B0DC
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

	// Token: 0x060009F6 RID: 2550 RVA: 0x0006CF90 File Offset: 0x0006B190
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

	// Token: 0x04000E3A RID: 3642
	public int genreArt;

	// Token: 0x04000E3B RID: 3643
	private mainScript mS_;

	// Token: 0x04000E3C RID: 3644
	private GameObject main_;

	// Token: 0x04000E3D RID: 3645
	private GUI_Main guiMain_;

	// Token: 0x04000E3E RID: 3646
	private sfxScript sfx_;

	// Token: 0x04000E3F RID: 3647
	private textScript tS_;

	// Token: 0x04000E40 RID: 3648
	private genres genres_;

	// Token: 0x04000E41 RID: 3649
	private Menu_DevGame mDevGame_;

	// Token: 0x04000E42 RID: 3650
	public GameObject[] uiPrefabs;

	// Token: 0x04000E43 RID: 3651
	public GameObject[] uiObjects;

	// Token: 0x04000E44 RID: 3652
	private float updateTimer;
}
