using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001D0 RID: 464
public class Menu_MesseSelectKonsole : MonoBehaviour
{
	// Token: 0x0600118D RID: 4493 RVA: 0x000B97E2 File Offset: 0x000B79E2
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600118E RID: 4494 RVA: 0x000B97EC File Offset: 0x000B79EC
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

	// Token: 0x0600118F RID: 4495 RVA: 0x000B98DD File Offset: 0x000B7ADD
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x06001190 RID: 4496 RVA: 0x000B990F File Offset: 0x000B7B0F
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x06001191 RID: 4497 RVA: 0x000B9920 File Offset: 0x000B7B20
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

	// Token: 0x06001192 RID: 4498 RVA: 0x000B99C0 File Offset: 0x000B7BC0
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

	// Token: 0x06001193 RID: 4499 RVA: 0x000B9B3B File Offset: 0x000B7D3B
	public bool CheckGameData(platformScript script_)
	{
		return script_ && script_.ownerID == this.mS_.myID && !script_.vomMarktGenommen;
	}

	// Token: 0x06001194 RID: 4500 RVA: 0x000B9B64 File Offset: 0x000B7D64
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

	// Token: 0x06001195 RID: 4501 RVA: 0x000B9CAF File Offset: 0x000B7EAF
	public void BUTTON_Entfernen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[186].GetComponent<Menu_MesseSelect>().SetKonsole(this.slot, null);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001196 RID: 4502 RVA: 0x000B9CEC File Offset: 0x000B7EEC
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0400160A RID: 5642
	public GameObject[] uiPrefabs;

	// Token: 0x0400160B RID: 5643
	public GameObject[] uiObjects;

	// Token: 0x0400160C RID: 5644
	private mainScript mS_;

	// Token: 0x0400160D RID: 5645
	private GameObject main_;

	// Token: 0x0400160E RID: 5646
	private GUI_Main guiMain_;

	// Token: 0x0400160F RID: 5647
	private sfxScript sfx_;

	// Token: 0x04001610 RID: 5648
	private textScript tS_;

	// Token: 0x04001611 RID: 5649
	private genres genres_;

	// Token: 0x04001612 RID: 5650
	private Menu_MesseSelect menu_MesseSelect_;

	// Token: 0x04001613 RID: 5651
	private int slot;
}
