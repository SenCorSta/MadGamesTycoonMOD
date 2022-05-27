using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000243 RID: 579
public class Menu_Stats_MyGamesReview : MonoBehaviour
{
	// Token: 0x06001658 RID: 5720 RVA: 0x000E2F7F File Offset: 0x000E117F
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001659 RID: 5721 RVA: 0x000E2F88 File Offset: 0x000E1188
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
	}

	// Token: 0x0600165A RID: 5722 RVA: 0x000E3050 File Offset: 0x000E1250
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x0600165B RID: 5723 RVA: 0x000E3088 File Offset: 0x000E1288
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
		this.SetData();
	}

	// Token: 0x0600165C RID: 5724 RVA: 0x000E30D4 File Offset: 0x000E12D4
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_MyGames_Review>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600165D RID: 5725 RVA: 0x000E3130 File Offset: 0x000E1330
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x0600165E RID: 5726 RVA: 0x000E3138 File Offset: 0x000E1338
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
	}

	// Token: 0x0600165F RID: 5727 RVA: 0x000E31C8 File Offset: 0x000E13C8
	public void Init()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.SetData();
	}

	// Token: 0x06001660 RID: 5728 RVA: 0x000E31DC File Offset: 0x000E13DC
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && this.CheckGameData(component) && !this.Exists(this.uiObjects[0], component.myID))
				{
					Item_MyGames_Review component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_MyGames_Review>();
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.genres_ = this.genres_;
					component2.game_ = component;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x06001661 RID: 5729 RVA: 0x000E32E8 File Offset: 0x000E14E8
	public bool CheckGameData(gameScript script_)
	{
		if (script_ && (script_.ownerID == this.mS_.myID || script_.publisherID == this.mS_.myID))
		{
			if (this.uiObjects[6].GetComponent<Toggle>().isOn && script_.developerID != this.mS_.myID)
			{
				return false;
			}
			if (!script_.inDevelopment && !script_.typ_budget && !script_.typ_goty && !script_.schublade)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001662 RID: 5730 RVA: 0x000E336F File Offset: 0x000E156F
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001663 RID: 5731 RVA: 0x000E338C File Offset: 0x000E158C
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
				Item_MyGames_Review component = gameObject.GetComponent<Item_MyGames_Review>();
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

	// Token: 0x06001664 RID: 5732 RVA: 0x000E3458 File Offset: 0x000E1658
	public void TOGGLE_OnlyMyGames()
	{
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x04001A4E RID: 6734
	private mainScript mS_;

	// Token: 0x04001A4F RID: 6735
	private GameObject main_;

	// Token: 0x04001A50 RID: 6736
	private GUI_Main guiMain_;

	// Token: 0x04001A51 RID: 6737
	private sfxScript sfx_;

	// Token: 0x04001A52 RID: 6738
	private textScript tS_;

	// Token: 0x04001A53 RID: 6739
	private genres genres_;

	// Token: 0x04001A54 RID: 6740
	public GameObject[] uiPrefabs;

	// Token: 0x04001A55 RID: 6741
	public GameObject[] uiObjects;

	// Token: 0x04001A56 RID: 6742
	private float updateTimer;
}
