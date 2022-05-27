using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200018B RID: 395
public class Menu_BuyDevKit : MonoBehaviour
{
	// Token: 0x06000EFC RID: 3836 RVA: 0x0009F503 File Offset: 0x0009D703
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000EFD RID: 3837 RVA: 0x0009F50C File Offset: 0x0009D70C
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

	// Token: 0x06000EFE RID: 3838 RVA: 0x0009F5B6 File Offset: 0x0009D7B6
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000EFF RID: 3839 RVA: 0x0009F5F0 File Offset: 0x0009D7F0
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

	// Token: 0x06000F00 RID: 3840 RVA: 0x0009F654 File Offset: 0x0009D854
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

	// Token: 0x06000F01 RID: 3841 RVA: 0x0009F6B0 File Offset: 0x0009D8B0
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.TAB_DevKitsBuy(0);
	}

	// Token: 0x06000F02 RID: 3842 RVA: 0x0009F6C8 File Offset: 0x0009D8C8
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

	// Token: 0x06000F03 RID: 3843 RVA: 0x0009F7FC File Offset: 0x0009D9FC
	private void Init(bool inBesitz)
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData(inBesitz);
	}

	// Token: 0x06000F04 RID: 3844 RVA: 0x0009F854 File Offset: 0x0009DA54
	private void SetData(bool inBesitz)
	{
		bool isOn = this.uiObjects[5].GetComponent<Toggle>().isOn;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Platform");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				platformScript component = array[i].GetComponent<platformScript>();
				if (component && component.isUnlocked && component.inBesitz == inBesitz && (component.OwnerIsNPC() || component.thridPartyGames || !component.OwnerIsNPC()) && (!component.vomMarktGenommen || isOn) && !this.Exists(this.uiObjects[0], component.myID))
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

	// Token: 0x06000F05 RID: 3845 RVA: 0x0009F9A8 File Offset: 0x0009DBA8
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

	// Token: 0x06000F06 RID: 3846 RVA: 0x0009FBD0 File Offset: 0x0009DDD0
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		if (!this.guiMain_.uiObjects[56].activeSelf && !this.guiMain_.uiObjects[102].activeSelf && !this.guiMain_.uiObjects[37].activeSelf && !this.guiMain_.uiObjects[99].activeSelf)
		{
			this.guiMain_.CloseMenu();
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000F07 RID: 3847 RVA: 0x0009FC55 File Offset: 0x0009DE55
	public void TAB_DevKitsBuy(int t)
	{
		this.TAB = t;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.SetTab(this.uiObjects[4], t);
		this.Init(false);
	}

	// Token: 0x06000F08 RID: 3848 RVA: 0x0009FC86 File Offset: 0x0009DE86
	public void TAB_MyDevKits(int t)
	{
		this.TAB = t;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.SetTab(this.uiObjects[4], t);
		this.Init(true);
	}

	// Token: 0x06000F09 RID: 3849 RVA: 0x0009FCB8 File Offset: 0x0009DEB8
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

	// Token: 0x04001348 RID: 4936
	public GameObject[] uiPrefabs;

	// Token: 0x04001349 RID: 4937
	public GameObject[] uiObjects;

	// Token: 0x0400134A RID: 4938
	private mainScript mS_;

	// Token: 0x0400134B RID: 4939
	private GameObject main_;

	// Token: 0x0400134C RID: 4940
	private GUI_Main guiMain_;

	// Token: 0x0400134D RID: 4941
	private sfxScript sfx_;

	// Token: 0x0400134E RID: 4942
	private textScript tS_;

	// Token: 0x0400134F RID: 4943
	private int TAB;

	// Token: 0x04001350 RID: 4944
	private float updateTimer;
}
