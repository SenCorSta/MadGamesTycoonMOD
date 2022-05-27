using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000242 RID: 578
public class Menu_Stats_MyGamesReview : MonoBehaviour
{
	// Token: 0x0600163A RID: 5690 RVA: 0x0000F640 File Offset: 0x0000D840
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600163B RID: 5691 RVA: 0x000EAAD8 File Offset: 0x000E8CD8
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

	// Token: 0x0600163C RID: 5692 RVA: 0x0000F648 File Offset: 0x0000D848
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x0600163D RID: 5693 RVA: 0x000EABA0 File Offset: 0x000E8DA0
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

	// Token: 0x0600163E RID: 5694 RVA: 0x000A27F4 File Offset: 0x000A09F4
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).GetComponent<Item_MyGames_Review>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600163F RID: 5695 RVA: 0x0000F680 File Offset: 0x0000D880
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06001640 RID: 5696 RVA: 0x000EABEC File Offset: 0x000E8DEC
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

	// Token: 0x06001641 RID: 5697 RVA: 0x0000F688 File Offset: 0x0000D888
	public void Init()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.SetData();
	}

	// Token: 0x06001642 RID: 5698 RVA: 0x000EAC7C File Offset: 0x000E8E7C
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

	// Token: 0x06001643 RID: 5699 RVA: 0x0000F69C File Offset: 0x0000D89C
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.playerGame && !script_.inDevelopment && !script_.typ_budget && !script_.typ_goty && !script_.schublade;
	}

	// Token: 0x06001644 RID: 5700 RVA: 0x0000F6D1 File Offset: 0x0000D8D1
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001645 RID: 5701 RVA: 0x000EAD88 File Offset: 0x000E8F88
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

	// Token: 0x04001A45 RID: 6725
	private mainScript mS_;

	// Token: 0x04001A46 RID: 6726
	private GameObject main_;

	// Token: 0x04001A47 RID: 6727
	private GUI_Main guiMain_;

	// Token: 0x04001A48 RID: 6728
	private sfxScript sfx_;

	// Token: 0x04001A49 RID: 6729
	private textScript tS_;

	// Token: 0x04001A4A RID: 6730
	private genres genres_;

	// Token: 0x04001A4B RID: 6731
	public GameObject[] uiPrefabs;

	// Token: 0x04001A4C RID: 6732
	public GameObject[] uiObjects;

	// Token: 0x04001A4D RID: 6733
	private float updateTimer;
}
