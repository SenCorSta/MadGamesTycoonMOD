using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200016B RID: 363
public class Menu_ShowKonsoleGames : MonoBehaviour
{
	// Token: 0x06000D80 RID: 3456 RVA: 0x000095AD File Offset: 0x000077AD
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000D81 RID: 3457 RVA: 0x000A26E0 File Offset: 0x000A08E0
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

	// Token: 0x06000D82 RID: 3458 RVA: 0x000095B5 File Offset: 0x000077B5
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000D83 RID: 3459 RVA: 0x000A27A8 File Offset: 0x000A09A8
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

	// Token: 0x06000D84 RID: 3460 RVA: 0x000A27F4 File Offset: 0x000A09F4
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

	// Token: 0x06000D85 RID: 3461 RVA: 0x000095ED File Offset: 0x000077ED
	private void OnEnable()
	{
		this.InitDropdowns();
	}

	// Token: 0x06000D86 RID: 3462 RVA: 0x000A2838 File Offset: 0x000A0A38
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

	// Token: 0x06000D87 RID: 3463 RVA: 0x000095F5 File Offset: 0x000077F5
	public void Init(platformScript plat_)
	{
		this.FindScripts();
		this.pS_ = plat_;
		this.uiObjects[6].GetComponent<Text>().text = this.pS_.GetName();
		this.SetData();
	}

	// Token: 0x06000D88 RID: 3464 RVA: 0x000A2920 File Offset: 0x000A0B20
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

	// Token: 0x06000D89 RID: 3465 RVA: 0x000A2A78 File Offset: 0x000A0C78
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

	// Token: 0x06000D8A RID: 3466 RVA: 0x00009627 File Offset: 0x00007827
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000D8B RID: 3467 RVA: 0x000A2AC0 File Offset: 0x000A0CC0
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

	// Token: 0x04001226 RID: 4646
	private mainScript mS_;

	// Token: 0x04001227 RID: 4647
	private GameObject main_;

	// Token: 0x04001228 RID: 4648
	private GUI_Main guiMain_;

	// Token: 0x04001229 RID: 4649
	private sfxScript sfx_;

	// Token: 0x0400122A RID: 4650
	private textScript tS_;

	// Token: 0x0400122B RID: 4651
	private genres genres_;

	// Token: 0x0400122C RID: 4652
	private platformScript pS_;

	// Token: 0x0400122D RID: 4653
	public GameObject[] uiPrefabs;

	// Token: 0x0400122E RID: 4654
	public GameObject[] uiObjects;

	// Token: 0x0400122F RID: 4655
	private float updateTimer;
}
