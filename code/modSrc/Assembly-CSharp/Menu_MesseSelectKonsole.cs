using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001CF RID: 463
public class Menu_MesseSelectKonsole : MonoBehaviour
{
	// Token: 0x06001173 RID: 4467 RVA: 0x0000C346 File Offset: 0x0000A546
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001174 RID: 4468 RVA: 0x000C4CFC File Offset: 0x000C2EFC
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
		if (!this.menu_MesseSelect_)
		{
			this.menu_MesseSelect_ = this.guiMain_.uiObjects[186].GetComponent<Menu_MesseSelect>();
		}
	}

	// Token: 0x06001175 RID: 4469 RVA: 0x0000C34E File Offset: 0x0000A54E
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x06001176 RID: 4470 RVA: 0x0000C380 File Offset: 0x0000A580
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x06001177 RID: 4471 RVA: 0x000C4DF0 File Offset: 0x000C2FF0
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(217));
		list.Add(this.tS_.GetText(433));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06001178 RID: 4472 RVA: 0x000C4E90 File Offset: 0x000C3090
	public void Init(int slot_)
	{
		this.FindScripts();
		this.slot = slot_;
		if (this.menu_MesseSelect_.konsolen[this.slot] == null)
		{
			this.uiObjects[6].GetComponent<Button>().interactable = false;
		}
		else
		{
			this.uiObjects[6].GetComponent<Button>().interactable = true;
		}
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("Platform");
		for (int j = 0; j < array.Length; j++)
		{
			if (array[j])
			{
				platformScript component = array[j].GetComponent<platformScript>();
				if (component && this.CheckGameData(component))
				{
					Item_MesseKonsole component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_MesseKonsole>();
					component2.pS_ = component;
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.slot = this.slot;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x06001179 RID: 4473 RVA: 0x0000C38E File Offset: 0x0000A58E
	public bool CheckGameData(platformScript script_)
	{
		return script_ && script_.playerConsole && !script_.vomMarktGenommen;
	}

	// Token: 0x0600117A RID: 4474 RVA: 0x000C500C File Offset: 0x000C320C
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
				Item_MesseKonsole component = gameObject.GetComponent<Item_MesseKonsole>();
				switch (value)
				{
				case 0:
					gameObject.name = component.pS_.GetName();
					break;
				case 1:
				{
					float num = (float)component.pS_.date_month;
					num /= 13f;
					gameObject.name = component.pS_.date_year.ToString() + num.ToString();
					if (!component.pS_.isUnlocked)
					{
						gameObject.name = "999999";
					}
					break;
				}
				case 2:
					gameObject.name = component.pS_.GetHype().ToString();
					break;
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

	// Token: 0x0600117B RID: 4475 RVA: 0x0000C3AB File Offset: 0x0000A5AB
	public void BUTTON_Entfernen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[186].GetComponent<Menu_MesseSelect>().SetKonsole(this.slot, null);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600117C RID: 4476 RVA: 0x0000C3E8 File Offset: 0x0000A5E8
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001601 RID: 5633
	public GameObject[] uiPrefabs;

	// Token: 0x04001602 RID: 5634
	public GameObject[] uiObjects;

	// Token: 0x04001603 RID: 5635
	private mainScript mS_;

	// Token: 0x04001604 RID: 5636
	private GameObject main_;

	// Token: 0x04001605 RID: 5637
	private GUI_Main guiMain_;

	// Token: 0x04001606 RID: 5638
	private sfxScript sfx_;

	// Token: 0x04001607 RID: 5639
	private textScript tS_;

	// Token: 0x04001608 RID: 5640
	private genres genres_;

	// Token: 0x04001609 RID: 5641
	private Menu_MesseSelect menu_MesseSelect_;

	// Token: 0x0400160A RID: 5642
	private int slot;
}
