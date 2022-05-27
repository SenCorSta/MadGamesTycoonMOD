using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200025B RID: 603
public class Menu_Stats_TochterfirmaPlatform : MonoBehaviour
{
	// Token: 0x06001779 RID: 6009 RVA: 0x000EB3F8 File Offset: 0x000E95F8
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600177A RID: 6010 RVA: 0x000EB400 File Offset: 0x000E9600
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

	// Token: 0x0600177B RID: 6011 RVA: 0x000EB4AA File Offset: 0x000E96AA
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x0600177C RID: 6012 RVA: 0x000EB4DC File Offset: 0x000E96DC
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_TochterfirmaPlatform>().pS_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600177D RID: 6013 RVA: 0x000EB538 File Offset: 0x000E9738
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

	// Token: 0x0600177E RID: 6014 RVA: 0x000EB66C File Offset: 0x000E986C
	public void Init(publisherScript script_, int slot_)
	{
		this.slot = slot_;
		this.pubS_ = script_;
		this.FindScripts();
		this.InitDropdowns();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x0600177F RID: 6015 RVA: 0x000EB6D4 File Offset: 0x000E98D4
	private void SetData()
	{
		bool isOn = this.uiObjects[5].GetComponent<Toggle>().isOn;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Platform");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				platformScript component = array[i].GetComponent<platformScript>();
				if (component && component.isUnlocked && component.inBesitz && (component.OwnerIsNPC() || (component.thridPartyGames && component.ownerID == this.mS_.myID)) && (!component.vomMarktGenommen || isOn) && this.pubS_.tf_platformFocus[0] != component.myID && this.pubS_.tf_platformFocus[1] != component.myID && this.pubS_.tf_platformFocus[2] != component.myID && this.pubS_.tf_platformFocus[3] != component.myID)
				{
					string text = component.GetName();
					this.searchStringA = this.searchStringA.ToLower();
					text = text.ToLower();
					if ((this.uiObjects[7].GetComponent<InputField>().text.Length <= 0 || text.Contains(this.searchStringA)) && !this.Exists(this.uiObjects[0], component.myID))
					{
						Item_TochterfirmaPlatform component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_TochterfirmaPlatform>();
						component2.myID = component.myID;
						component2.pS_ = component;
						component2.mS_ = this.mS_;
						component2.tS_ = this.tS_;
						component2.sfx_ = this.sfx_;
						component2.guiMain_ = this.guiMain_;
						component2.pubS_ = this.pubS_;
						component2.slot = this.slot;
					}
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[6]);
	}

	// Token: 0x06001780 RID: 6016 RVA: 0x000EB8FC File Offset: 0x000E9AFC
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
				Item_TochterfirmaPlatform component = gameObject.GetComponent<Item_TochterfirmaPlatform>();
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

	// Token: 0x06001781 RID: 6017 RVA: 0x000EBB23 File Offset: 0x000E9D23
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001782 RID: 6018 RVA: 0x000EBB3E File Offset: 0x000E9D3E
	public void TOGGLE_VomMarktGenommen()
	{
		this.Init(this.pubS_, this.slot);
	}

	// Token: 0x06001783 RID: 6019 RVA: 0x000EBB54 File Offset: 0x000E9D54
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
		this.searchStringA = this.uiObjects[7].GetComponent<InputField>().text;
		this.Init(this.pubS_, this.slot);
	}

	// Token: 0x06001784 RID: 6020 RVA: 0x000EBBD4 File Offset: 0x000E9DD4
	public void BUTTON_RemovePlatform()
	{
		this.sfx_.PlaySound(3, true);
		this.pubS_.tf_platformFocus[this.slot] = -1;
		this.guiMain_.uiObjects[393].GetComponent<Menu_Stats_TochterfirmaSettings>().UpdateData();
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001B3F RID: 6975
	public GameObject[] uiPrefabs;

	// Token: 0x04001B40 RID: 6976
	public GameObject[] uiObjects;

	// Token: 0x04001B41 RID: 6977
	private mainScript mS_;

	// Token: 0x04001B42 RID: 6978
	private GameObject main_;

	// Token: 0x04001B43 RID: 6979
	private GUI_Main guiMain_;

	// Token: 0x04001B44 RID: 6980
	private sfxScript sfx_;

	// Token: 0x04001B45 RID: 6981
	private textScript tS_;

	// Token: 0x04001B46 RID: 6982
	private publisherScript pubS_;

	// Token: 0x04001B47 RID: 6983
	private int slot;

	// Token: 0x04001B48 RID: 6984
	private string searchStringA = "";
}
