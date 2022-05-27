using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200016C RID: 364
public class Menu_ShowKonsoleGames : MonoBehaviour
{
	// Token: 0x06000D98 RID: 3480 RVA: 0x00094025 File Offset: 0x00092225
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000D99 RID: 3481 RVA: 0x00094030 File Offset: 0x00092230
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

	// Token: 0x06000D9A RID: 3482 RVA: 0x000940F8 File Offset: 0x000922F8
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000D9B RID: 3483 RVA: 0x00094130 File Offset: 0x00092330
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

	// Token: 0x06000D9C RID: 3484 RVA: 0x0009417C File Offset: 0x0009237C
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

	// Token: 0x06000D9D RID: 3485 RVA: 0x000941C0 File Offset: 0x000923C0
	private void OnEnable()
	{
		this.InitDropdowns();
	}

	// Token: 0x06000D9E RID: 3486 RVA: 0x000941C8 File Offset: 0x000923C8
	public void InitDropdowns()
	{
		this.FindScripts();
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(217));
		list.Add(this.tS_.GetText(273));
		list.Add(this.tS_.GetText(275));
		list.Add(this.tS_.GetText(1290));
		list.Add(this.tS_.GetText(1289));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06000D9F RID: 3487 RVA: 0x000942B0 File Offset: 0x000924B0
	public void Init(platformScript plat_)
	{
		this.FindScripts();
		this.pS_ = plat_;
		this.uiObjects[6].GetComponent<Text>().text = this.pS_.GetName();
		this.SetData();
	}

	// Token: 0x06000DA0 RID: 3488 RVA: 0x000942E4 File Offset: 0x000924E4
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
		string text = this.tS_.GetText(297);
		text = text.Replace("<NUM>", this.uiObjects[0].transform.childCount.ToString());
		this.uiObjects[4].GetComponent<Text>().text = text;
	}

	// Token: 0x06000DA1 RID: 3489 RVA: 0x0009443C File Offset: 0x0009263C
	public bool CheckGameData(gameScript script_)
	{
		if (script_ && !script_.inDevelopment)
		{
			for (int i = 0; i < script_.gamePlatform.Length; i++)
			{
				if (script_.gamePlatform[i] == this.pS_.myID)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000DA2 RID: 3490 RVA: 0x00094484 File Offset: 0x00092684
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000DA3 RID: 3491 RVA: 0x000944A0 File Offset: 0x000926A0
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
				switch (value)
				{
				case 0:
					gameObject.name = component.game_.GetNameSimple();
					break;
				case 1:
				{
					float num = (float)component.game_.date_month;
					num /= 13f;
					gameObject.name = component.game_.date_year.ToString() + num.ToString();
					break;
				}
				case 2:
					gameObject.name = component.game_.maingenre.ToString();
					break;
				case 3:
					gameObject.name = component.game_.sellsTotal.ToString();
					break;
				case 4:
					gameObject.name = component.game_.reviewTotal.ToString();
					break;
				case 5:
					gameObject.name = component.game_.GetUserReviewPercent().ToString();
					break;
				}
			}
		}
		this.mS_.SortChildrenByFloat(this.uiObjects[0]);
	}

	// Token: 0x0400122E RID: 4654
	private mainScript mS_;

	// Token: 0x0400122F RID: 4655
	private GameObject main_;

	// Token: 0x04001230 RID: 4656
	private GUI_Main guiMain_;

	// Token: 0x04001231 RID: 4657
	private sfxScript sfx_;

	// Token: 0x04001232 RID: 4658
	private textScript tS_;

	// Token: 0x04001233 RID: 4659
	private genres genres_;

	// Token: 0x04001234 RID: 4660
	private platformScript pS_;

	// Token: 0x04001235 RID: 4661
	public GameObject[] uiPrefabs;

	// Token: 0x04001236 RID: 4662
	public GameObject[] uiObjects;

	// Token: 0x04001237 RID: 4663
	private float updateTimer;
}
