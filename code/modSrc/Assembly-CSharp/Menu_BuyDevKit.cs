using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200018A RID: 394
public class Menu_BuyDevKit : MonoBehaviour
{
	// Token: 0x06000EE4 RID: 3812 RVA: 0x0000A876 File Offset: 0x00008A76
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000EE5 RID: 3813 RVA: 0x000AC7A4 File Offset: 0x000AA9A4
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindGameObjectWithTag("Main");
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
	}

	// Token: 0x06000EE6 RID: 3814 RVA: 0x0000A87E File Offset: 0x00008A7E
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000EE7 RID: 3815 RVA: 0x000AC850 File Offset: 0x000AAA50
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
		int tab = this.TAB;
		if (tab == 0)
		{
			this.SetData(false);
			return;
		}
		if (tab != 1)
		{
			return;
		}
		this.SetData(true);
	}

	// Token: 0x06000EE8 RID: 3816 RVA: 0x000AC8B4 File Offset: 0x000AAAB4
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_Platform_BuyDevKit>().pS_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000EE9 RID: 3817 RVA: 0x0000A8B6 File Offset: 0x00008AB6
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.TAB_DevKitsBuy(0);
	}

	// Token: 0x06000EEA RID: 3818 RVA: 0x000AC910 File Offset: 0x000AAB10
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

	// Token: 0x06000EEB RID: 3819 RVA: 0x000ACA44 File Offset: 0x000AAC44
	private void Init(bool inBesitz)
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData(inBesitz);
	}

	// Token: 0x06000EEC RID: 3820 RVA: 0x000ACA9C File Offset: 0x000AAC9C
	private void SetData(bool inBesitz)
	{
		bool isOn = this.uiObjects[5].GetComponent<Toggle>().isOn;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Platform");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				platformScript component = array[i].GetComponent<platformScript>();
				if (component && component.isUnlocked && component.inBesitz == inBesitz && (component.npc || component.thridPartyGames || component.playerConsole) && (!component.vomMarktGenommen || isOn) && !this.Exists(this.uiObjects[0], component.myID))
				{
					Item_Platform_BuyDevKit component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_Platform_BuyDevKit>();
					component2.myID = component.myID;
					component2.pS_ = component;
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[6]);
	}

	// Token: 0x06000EED RID: 3821 RVA: 0x000ACBF0 File Offset: 0x000AADF0
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
				Item_Platform_BuyDevKit component = gameObject.GetComponent<Item_Platform_BuyDevKit>();
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

	// Token: 0x06000EEE RID: 3822 RVA: 0x000ACE18 File Offset: 0x000AB018
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		if (!this.guiMain_.uiObjects[56].activeSelf && !this.guiMain_.uiObjects[102].activeSelf && !this.guiMain_.uiObjects[37].activeSelf && !this.guiMain_.uiObjects[99].activeSelf)
		{
			this.guiMain_.CloseMenu();
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000EEF RID: 3823 RVA: 0x0000A8CB File Offset: 0x00008ACB
	public void TAB_DevKitsBuy(int t)
	{
		this.TAB = t;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.SetTab(this.uiObjects[4], t);
		this.Init(false);
	}

	// Token: 0x06000EF0 RID: 3824 RVA: 0x0000A8FC File Offset: 0x00008AFC
	public void TAB_MyDevKits(int t)
	{
		this.TAB = t;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.SetTab(this.uiObjects[4], t);
		this.Init(true);
	}

	// Token: 0x06000EF1 RID: 3825 RVA: 0x000ACEA0 File Offset: 0x000AB0A0
	public void TOGGLE_VomMarktGenommen()
	{
		int tab = this.TAB;
		if (tab == 0)
		{
			this.TAB_DevKitsBuy(0);
			return;
		}
		if (tab != 1)
		{
			return;
		}
		this.TAB_MyDevKits(1);
	}

	// Token: 0x0400133F RID: 4927
	public GameObject[] uiPrefabs;

	// Token: 0x04001340 RID: 4928
	public GameObject[] uiObjects;

	// Token: 0x04001341 RID: 4929
	private mainScript mS_;

	// Token: 0x04001342 RID: 4930
	private GameObject main_;

	// Token: 0x04001343 RID: 4931
	private GUI_Main guiMain_;

	// Token: 0x04001344 RID: 4932
	private sfxScript sfx_;

	// Token: 0x04001345 RID: 4933
	private textScript tS_;

	// Token: 0x04001346 RID: 4934
	private int TAB;

	// Token: 0x04001347 RID: 4935
	private float updateTimer;
}
