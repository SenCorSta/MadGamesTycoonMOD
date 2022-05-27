using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000224 RID: 548
public class Menu_Statistics_BestGames : MonoBehaviour
{
	// Token: 0x060014FA RID: 5370 RVA: 0x0000E469 File Offset: 0x0000C669
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060014FB RID: 5371 RVA: 0x000E1AD4 File Offset: 0x000DFCD4
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
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
	}

	// Token: 0x060014FC RID: 5372 RVA: 0x0000E471 File Offset: 0x0000C671
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060014FD RID: 5373 RVA: 0x000E1BBC File Offset: 0x000DFDBC
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
		this.SetData(true);
	}

	// Token: 0x060014FE RID: 5374 RVA: 0x000E1C0C File Offset: 0x000DFE0C
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			GameObject gameObject = parent_.transform.GetChild(i).gameObject;
			if (gameObject.activeSelf)
			{
				Item_BestGames component = gameObject.GetComponent<Item_BestGames>();
				if (component.game_.myID == id_)
				{
					gameObject.name = component.game_.reviewTotal.ToString();
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060014FF RID: 5375 RVA: 0x0000E4A9 File Offset: 0x0000C6A9
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06001500 RID: 5376 RVA: 0x000E1C78 File Offset: 0x000DFE78
	public void InitDropdowns()
	{
		this.FindScripts();
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(1290));
		list.Add(this.tS_.GetText(1289));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
		@int = PlayerPrefs.GetInt(this.uiObjects[6].name);
		list.Clear();
		list.Add(this.tS_.GetText(1902));
		for (int i = 0; i < this.genres_.genres_UNLOCK.Length; i++)
		{
			if (this.genres_.genres_UNLOCK[i])
			{
				list.Add(this.genres_.GetName(i));
			}
		}
		this.uiObjects[6].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[6].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[6].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06001501 RID: 5377 RVA: 0x0000E4B1 File Offset: 0x0000C6B1
	public void Init()
	{
		this.FindScripts();
		this.InitDropdowns();
		if (this.uiObjects[0].transform.childCount <= 0)
		{
			this.SetData(false);
		}
	}

	// Token: 0x06001502 RID: 5378 RVA: 0x000E1DA8 File Offset: 0x000DFFA8
	private void SetData(bool check)
	{
		int genre = this.uiObjects[6].GetComponent<Dropdown>().value - 1;
		this.games_.CreateBestGamesCharts(500, genre);
		for (int i = 0; i < this.games_.chartsList.Count; i++)
		{
			gameScript script_ = this.games_.chartsList[i].script_;
			if (script_)
			{
				if (check)
				{
					if (!this.Exists(this.uiObjects[0], script_.myID))
					{
						Item_BestGames component = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_BestGames>();
						component.mS_ = this.mS_;
						component.tS_ = this.tS_;
						component.sfx_ = this.sfx_;
						component.guiMain_ = this.guiMain_;
						component.genres_ = this.genres_;
						component.game_ = script_;
					}
				}
				else
				{
					Item_BestGames component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_BestGames>();
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.genres_ = this.genres_;
					component2.game_ = script_;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x06001503 RID: 5379 RVA: 0x0000E4DB File Offset: 0x0000C6DB
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001504 RID: 5380 RVA: 0x000E1F54 File Offset: 0x000E0154
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
				Item_BestGames component = gameObject.GetComponent<Item_BestGames>();
				if (value != 0)
				{
					if (value == 1)
					{
						gameObject.name = component.game_.GetUserReviewPercent().ToString();
					}
				}
				else
				{
					gameObject.name = component.game_.reviewTotal.ToString();
				}
			}
		}
		this.mS_.SortChildrenByFloat(this.uiObjects[0]);
	}

	// Token: 0x06001505 RID: 5381 RVA: 0x000E2020 File Offset: 0x000E0220
	public void DROPDOWN_Genre()
	{
		int value = this.uiObjects[6].GetComponent<Dropdown>().value;
		PlayerPrefs.SetInt(this.uiObjects[6].name, value);
		this.uiObjects[0].SetActive(false);
		this.uiObjects[0].SetActive(true);
		this.SetData(false);
	}

	// Token: 0x04001913 RID: 6419
	private mainScript mS_;

	// Token: 0x04001914 RID: 6420
	private GameObject main_;

	// Token: 0x04001915 RID: 6421
	private GUI_Main guiMain_;

	// Token: 0x04001916 RID: 6422
	private sfxScript sfx_;

	// Token: 0x04001917 RID: 6423
	private textScript tS_;

	// Token: 0x04001918 RID: 6424
	private genres genres_;

	// Token: 0x04001919 RID: 6425
	private games games_;

	// Token: 0x0400191A RID: 6426
	public GameObject[] uiPrefabs;

	// Token: 0x0400191B RID: 6427
	public GameObject[] uiObjects;

	// Token: 0x0400191C RID: 6428
	private float updateTimer;
}
