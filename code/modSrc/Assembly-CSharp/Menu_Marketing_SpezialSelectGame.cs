using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001AC RID: 428
public class Menu_Marketing_SpezialSelectGame : MonoBehaviour
{
	// Token: 0x0600102B RID: 4139 RVA: 0x000AAFAB File Offset: 0x000A91AB
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600102C RID: 4140 RVA: 0x000AAFB4 File Offset: 0x000A91B4
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

	// Token: 0x0600102D RID: 4141 RVA: 0x000AB07C File Offset: 0x000A927C
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x0600102E RID: 4142 RVA: 0x000AB0B4 File Offset: 0x000A92B4
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

	// Token: 0x0600102F RID: 4143 RVA: 0x000AB100 File Offset: 0x000A9300
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_MarketingSpezial_Game>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001030 RID: 4144 RVA: 0x000AB15C File Offset: 0x000A935C
	private void OnEnable()
	{
		this.InitDropdowns();
	}

	// Token: 0x06001031 RID: 4145 RVA: 0x000AB164 File Offset: 0x000A9364
	public void InitDropdowns()
	{
		this.FindScripts();
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(433));
		list.Add(this.tS_.GetText(273));
		list.Add(this.tS_.GetText(217));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06001032 RID: 4146 RVA: 0x000AB220 File Offset: 0x000A9420
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x06001033 RID: 4147 RVA: 0x000AB230 File Offset: 0x000A9430
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
					Item_MarketingSpezial_Game component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_MarketingSpezial_Game>();
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

	// Token: 0x06001034 RID: 4148 RVA: 0x000AB33C File Offset: 0x000A953C
	public bool CheckGameData(gameScript script_)
	{
		return script_ && (script_.developerID == this.mS_.myID || script_.publisherID == this.mS_.myID) && (script_.inDevelopment || script_.isOnMarket || script_.schublade) && !script_.typ_contractGame;
	}

	// Token: 0x06001035 RID: 4149 RVA: 0x000AB39A File Offset: 0x000A959A
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001036 RID: 4150 RVA: 0x000AB3B8 File Offset: 0x000A95B8
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
				Item_MarketingSpezial_Game component = gameObject.GetComponent<Item_MarketingSpezial_Game>();
				switch (value)
				{
				case 0:
					gameObject.name = component.game_.GetNameSimple();
					break;
				case 1:
					gameObject.name = component.game_.hype.ToString();
					break;
				case 2:
					gameObject.name = component.game_.maingenre.ToString();
					break;
				case 3:
				{
					float num = (float)component.game_.date_month;
					num /= 13f;
					gameObject.name = component.game_.date_year.ToString() + num.ToString();
					if (component.game_.inDevelopment || component.game_.schublade)
					{
						gameObject.name = "999999";
					}
					break;
				}
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

	// Token: 0x040014A2 RID: 5282
	private mainScript mS_;

	// Token: 0x040014A3 RID: 5283
	private GameObject main_;

	// Token: 0x040014A4 RID: 5284
	private GUI_Main guiMain_;

	// Token: 0x040014A5 RID: 5285
	private sfxScript sfx_;

	// Token: 0x040014A6 RID: 5286
	private textScript tS_;

	// Token: 0x040014A7 RID: 5287
	private genres genres_;

	// Token: 0x040014A8 RID: 5288
	public GameObject[] uiPrefabs;

	// Token: 0x040014A9 RID: 5289
	public GameObject[] uiObjects;

	// Token: 0x040014AA RID: 5290
	private float updateTimer;
}
