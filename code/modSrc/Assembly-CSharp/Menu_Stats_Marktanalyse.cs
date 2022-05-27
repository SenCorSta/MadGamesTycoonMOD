using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000240 RID: 576
public class Menu_Stats_Marktanalyse : MonoBehaviour
{
	// Token: 0x06001633 RID: 5683 RVA: 0x000E1F24 File Offset: 0x000E0124
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001634 RID: 5684 RVA: 0x000E1F2C File Offset: 0x000E012C
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

	// Token: 0x06001635 RID: 5685 RVA: 0x000E204E File Offset: 0x000E024E
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x06001636 RID: 5686 RVA: 0x000E2064 File Offset: 0x000E0264
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		this.FindScripts();
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(1665));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06001637 RID: 5687 RVA: 0x000E20F4 File Offset: 0x000E02F4
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06001638 RID: 5688 RVA: 0x000E212C File Offset: 0x000E032C
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

	// Token: 0x06001639 RID: 5689 RVA: 0x000E2178 File Offset: 0x000E0378
	public void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		for (int j = 0; j < this.genres_.genres_PRICE.Length; j++)
		{
			int amountGamesWithGenre_OnMarket = this.games_.GetAmountGamesWithGenre_OnMarket(j);
			if (amountGamesWithGenre_OnMarket > 0)
			{
				string text = this.genres_.GetName(j);
				this.searchStringA = this.searchStringA.ToLower();
				text = text.ToLower();
				if (this.uiObjects[6].GetComponent<InputField>().text.Length <= 0 || text.Contains(this.searchStringA))
				{
					UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_Stats_Marktanalyse>().Init(this.genres_.GetName(j), this.tS_.GetText(271) + ": " + amountGamesWithGenre_OnMarket.ToString(), this.genres_.GetPic(j), this.genres_.GetSpriteMarkt(j), amountGamesWithGenre_OnMarket, 0);
				}
			}
		}
		for (int k = 0; k < this.themes_.themes_LEVEL.Length; k++)
		{
			int num = this.themes_.themes_MARKT[k];
			if (num > 0)
			{
				string text2 = this.tS_.GetThemes(k);
				this.searchStringA = this.searchStringA.ToLower();
				text2 = text2.ToLower();
				if (this.uiObjects[6].GetComponent<InputField>().text.Length <= 0 || text2.Contains(this.searchStringA))
				{
					UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_Stats_Marktanalyse>().Init(this.tS_.GetThemes(k), this.tS_.GetText(271) + ": " + num.ToString(), this.guiMain_.uiSprites[6], this.themes_.GetSpriteMarkt(k), num, 1);
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[4]);
	}

	// Token: 0x0600163A RID: 5690 RVA: 0x000E2404 File Offset: 0x000E0604
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600163B RID: 5691 RVA: 0x000E2420 File Offset: 0x000E0620
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
				Item_Stats_Marktanalyse component = gameObject.GetComponent<Item_Stats_Marktanalyse>();
				if (value != 0)
				{
					if (value == 1)
					{
						if (component.typ == 0)
						{
							gameObject.name = (component.anzGames + 100000).ToString();
						}
						else
						{
							gameObject.name = component.anzGames.ToString();
						}
					}
				}
				else
				{
					gameObject.name = component.typ.ToString() + "_" + component.myName;
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

	// Token: 0x0600163C RID: 5692 RVA: 0x000E2534 File Offset: 0x000E0734
	public void BUTTON_Search()
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.searchStringA = this.uiObjects[6].GetComponent<InputField>().text;
		this.Init();
	}

	// Token: 0x04001A2F RID: 6703
	private mainScript mS_;

	// Token: 0x04001A30 RID: 6704
	private GameObject main_;

	// Token: 0x04001A31 RID: 6705
	private GUI_Main guiMain_;

	// Token: 0x04001A32 RID: 6706
	private sfxScript sfx_;

	// Token: 0x04001A33 RID: 6707
	private textScript tS_;

	// Token: 0x04001A34 RID: 6708
	private engineFeatures eF_;

	// Token: 0x04001A35 RID: 6709
	private genres genres_;

	// Token: 0x04001A36 RID: 6710
	private games games_;

	// Token: 0x04001A37 RID: 6711
	private themes themes_;

	// Token: 0x04001A38 RID: 6712
	public GameObject[] uiPrefabs;

	// Token: 0x04001A39 RID: 6713
	public GameObject[] uiObjects;

	// Token: 0x04001A3A RID: 6714
	private float updateTimer;

	// Token: 0x04001A3B RID: 6715
	private string searchStringA = "";
}
