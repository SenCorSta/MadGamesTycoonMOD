using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200023C RID: 572
public class Menu_Stats_GenreBeliebtheit : MonoBehaviour
{
	// Token: 0x060015F7 RID: 5623 RVA: 0x0000F1FD File Offset: 0x0000D3FD
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060015F8 RID: 5624 RVA: 0x000E9420 File Offset: 0x000E7620
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
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
		if (!this.themes_)
		{
			this.themes_ = this.main_.GetComponent<themes>();
		}
	}

	// Token: 0x060015F9 RID: 5625 RVA: 0x0000F205 File Offset: 0x0000D405
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060015FA RID: 5626 RVA: 0x0000F20D File Offset: 0x0000D40D
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060015FB RID: 5627 RVA: 0x000E9544 File Offset: 0x000E7744
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
		this.Init();
	}

	// Token: 0x060015FC RID: 5628 RVA: 0x000E9590 File Offset: 0x000E7790
	public void Init()
	{
		this.FindScripts();
		this.uiObjects[14].GetComponent<Text>().text = this.tS_.GetText(245) + "<color=blue><b>\n" + this.genres_.GetName(this.mS_.companySpecialGenre) + "</b></color>";
		this.uiObjects[15].GetComponent<Image>().sprite = this.genres_.GetPic(this.mS_.companySpecialGenre);
		string text = this.tS_.GetText(481);
		text = text.Replace("<TIME>", this.mS_.trendWeeks.ToString());
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
		for (int j = 0; j < this.uiObjects[0].transform.childCount; j++)
		{
			this.uiObjects[0].transform.GetChild(j).gameObject.SetActive(false);
		}
		for (int k = 0; k < this.genres_.genres_PRICE.Length; k++)
		{
			if (this.genres_.genres_UNLOCK[k])
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform);
				Item_Stats_GenreBeliebtheit component = gameObject.GetComponent<Item_Stats_GenreBeliebtheit>();
				component.guiMain_ = this.guiMain_;
				component.tS_ = this.tS_;
				float prozent = 0f;
				if (this.mS_.trendGenre != k && this.mS_.trendAntiGenre != k)
				{
					prozent = this.genres_.genres_BELIEBTHEIT[k];
				}
				if (this.mS_.trendAntiGenre == k)
				{
					prozent = 20f;
				}
				if (this.mS_.trendGenre == k)
				{
					prozent = 100f;
				}
				component.Init(this.genres_.GetName(k), prozent, this.genres_.GetPic(k));
				gameObject.name = prozent.ToString();
			}
		}
		this.mS_.SortChildrenByFloat(this.uiObjects[0]);
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[4]);
	}

	// Token: 0x060015FD RID: 5629 RVA: 0x0000F245 File Offset: 0x0000D445
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		if (this.closeMenu)
		{
			this.closeMenu = false;
			this.guiMain_.CloseMenu();
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001A08 RID: 6664
	private mainScript mS_;

	// Token: 0x04001A09 RID: 6665
	private GameObject main_;

	// Token: 0x04001A0A RID: 6666
	private GUI_Main guiMain_;

	// Token: 0x04001A0B RID: 6667
	private sfxScript sfx_;

	// Token: 0x04001A0C RID: 6668
	private textScript tS_;

	// Token: 0x04001A0D RID: 6669
	private engineFeatures eF_;

	// Token: 0x04001A0E RID: 6670
	private genres genres_;

	// Token: 0x04001A0F RID: 6671
	private games games_;

	// Token: 0x04001A10 RID: 6672
	private themes themes_;

	// Token: 0x04001A11 RID: 6673
	public GameObject[] uiPrefabs;

	// Token: 0x04001A12 RID: 6674
	public GameObject[] uiObjects;

	// Token: 0x04001A13 RID: 6675
	public bool closeMenu;

	// Token: 0x04001A14 RID: 6676
	private float updateTimer;
}
