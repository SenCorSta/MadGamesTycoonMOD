using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200010F RID: 271
public class Menu_Dev_EnginePlatform : MonoBehaviour
{
	// Token: 0x060008D0 RID: 2256 RVA: 0x0005FDD9 File Offset: 0x0005DFD9
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060008D1 RID: 2257 RVA: 0x0005FDE4 File Offset: 0x0005DFE4
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
		if (!this.gF_)
		{
			this.gF_ = this.main_.GetComponent<gameplayFeatures>();
		}
		if (!this.menuDevEngine_)
		{
			this.menuDevEngine_ = this.guiMain_.uiObjects[37].GetComponent<Menu_Dev_Engine>();
		}
	}

	// Token: 0x060008D2 RID: 2258 RVA: 0x0005FED2 File Offset: 0x0005E0D2
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060008D3 RID: 2259 RVA: 0x0005FF0C File Offset: 0x0005E10C
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

	// Token: 0x060008D4 RID: 2260 RVA: 0x0005FF58 File Offset: 0x0005E158
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_DevEngine_Platform>().pS_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060008D5 RID: 2261 RVA: 0x0005FFB4 File Offset: 0x0005E1B4
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x060008D6 RID: 2262 RVA: 0x0005FFC4 File Offset: 0x0005E1C4
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(216));
		list.Add(this.tS_.GetText(217));
		list.Add(this.tS_.GetText(4));
		list.Add(this.tS_.GetText(218));
		list.Add(this.tS_.GetText(219));
		list.Add(this.tS_.GetText(220));
		list.Add(this.tS_.GetText(6));
		list.Add(this.tS_.GetText(1484));
		list.Add(this.tS_.GetText(1485));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x060008D7 RID: 2263 RVA: 0x000600F6 File Offset: 0x0005E2F6
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x060008D8 RID: 2264 RVA: 0x00060104 File Offset: 0x0005E304
	private void SetData()
	{
		bool isOn = this.uiObjects[5].GetComponent<Toggle>().isOn;
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.uiObjects[7].GetComponent<Text>().text = this.tS_.GetText(376) + ": " + this.menuDevEngine_.techLevel.ToString();
		GameObject[] array = GameObject.FindGameObjectsWithTag("Platform");
		for (int j = 0; j < array.Length; j++)
		{
			if (array[j])
			{
				platformScript component = array[j].GetComponent<platformScript>();
				if (component && (component.isUnlocked || component.ownerID == this.mS_.myID) && component.inBesitz && ((!component.vomMarktGenommen && component.tech >= this.menuDevEngine_.techLevel) || isOn) && !this.Exists(this.uiObjects[0], component.myID))
				{
					Item_DevEngine_Platform component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_DevEngine_Platform>();
					component2.myID = component.myID;
					component2.pS_ = component;
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.gF_ = this.gF_;
					component2.guiMain_ = this.guiMain_;
					component2.menuDevEngine_ = this.menuDevEngine_;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[6]);
	}

	// Token: 0x060008D9 RID: 2265 RVA: 0x000602FC File Offset: 0x0005E4FC
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
				Item_DevEngine_Platform component = gameObject.GetComponent<Item_DevEngine_Platform>();
				switch (value)
				{
				case 0:
					gameObject.name = component.pS_.GetName();
					break;
				case 1:
					gameObject.name = component.pS_.GetManufacturer().ToString();
					break;
				case 2:
				{
					float num = (float)component.pS_.date_month;
					num /= 13f;
					gameObject.name = component.pS_.date_year.ToString() + num.ToString();
					break;
				}
				case 3:
					gameObject.name = component.pS_.tech.ToString();
					break;
				case 4:
					gameObject.name = component.pS_.GetPrice().ToString();
					break;
				case 5:
					gameObject.name = component.pS_.GetMarktanteil().ToString();
					break;
				case 6:
					gameObject.name = component.pS_.GetGames().ToString();
					break;
				case 7:
					gameObject.name = component.pS_.GetDevCosts().ToString();
					break;
				case 8:
					gameObject.name = (100 - component.pS_.typ).ToString();
					break;
				case 9:
					gameObject.name = component.pS_.GetAktiveNutzer().ToString();
					break;
				}
			}
		}
		if (value == 0 || value == 1)
		{
			this.mS_.SortChildrenByName(this.uiObjects[0]);
			return;
		}
		this.mS_.SortChildrenByFloat(this.uiObjects[0]);
	}

	// Token: 0x060008DA RID: 2266 RVA: 0x00060523 File Offset: 0x0005E723
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060008DB RID: 2267 RVA: 0x0006053E File Offset: 0x0005E73E
	public void TOGGLE_HiddenAnzeigen()
	{
		this.Init();
	}

	// Token: 0x04000D4C RID: 3404
	public GameObject[] uiPrefabs;

	// Token: 0x04000D4D RID: 3405
	public GameObject[] uiObjects;

	// Token: 0x04000D4E RID: 3406
	private mainScript mS_;

	// Token: 0x04000D4F RID: 3407
	private GameObject main_;

	// Token: 0x04000D50 RID: 3408
	private GUI_Main guiMain_;

	// Token: 0x04000D51 RID: 3409
	private sfxScript sfx_;

	// Token: 0x04000D52 RID: 3410
	private textScript tS_;

	// Token: 0x04000D53 RID: 3411
	private gameplayFeatures gF_;

	// Token: 0x04000D54 RID: 3412
	private Menu_Dev_Engine menuDevEngine_;

	// Token: 0x04000D55 RID: 3413
	private float updateTimer;
}
