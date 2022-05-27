using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001CF RID: 463
public class Menu_MesseSelectGame : MonoBehaviour
{
	// Token: 0x06001182 RID: 4482 RVA: 0x000B91F8 File Offset: 0x000B73F8
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001183 RID: 4483 RVA: 0x000B9200 File Offset: 0x000B7400
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

	// Token: 0x06001184 RID: 4484 RVA: 0x000B92F1 File Offset: 0x000B74F1
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x06001185 RID: 4485 RVA: 0x000B9323 File Offset: 0x000B7523
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x06001186 RID: 4486 RVA: 0x000B9334 File Offset: 0x000B7534
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(217));
		list.Add(this.tS_.GetText(273));
		list.Add(this.tS_.GetText(325));
		list.Add(this.tS_.GetText(433));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06001187 RID: 4487 RVA: 0x000B9400 File Offset: 0x000B7600
	public void Init(int slot_)
	{
		this.FindScripts();
		this.slot = slot_;
		if (this.menu_MesseSelect_.games[this.slot] == null)
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
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int j = 0; j < array.Length; j++)
		{
			if (array[j])
			{
				gameScript component = array[j].GetComponent<gameScript>();
				if (component && this.CheckGameData(component))
				{
					Item_MesseGame component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_MesseGame>();
					component2.game_ = component;
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.genres_ = this.genres_;
					component2.slot = this.slot;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x06001188 RID: 4488 RVA: 0x000B958C File Offset: 0x000B778C
	public bool CheckGameData(gameScript script_)
	{
		return script_ && (script_.ownerID == this.mS_.myID || script_.developerID == this.mS_.myID || script_.publisherID == this.mS_.myID) && (script_.inDevelopment || script_.isOnMarket || script_.schublade) && !script_.typ_contractGame;
	}

	// Token: 0x06001189 RID: 4489 RVA: 0x000B9600 File Offset: 0x000B7800
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
				Item_MesseGame component = gameObject.GetComponent<Item_MesseGame>();
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
					if (component.game_.inDevelopment || component.game_.schublade)
					{
						gameObject.name = "999999";
					}
					break;
				}
				case 2:
					gameObject.name = component.game_.maingenre.ToString();
					break;
				case 3:
					gameObject.name = component.game_.gameTyp.ToString();
					break;
				case 4:
					gameObject.name = component.game_.GetHype().ToString();
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

	// Token: 0x0600118A RID: 4490 RVA: 0x000B9796 File Offset: 0x000B7996
	public void BUTTON_Entfernen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[186].GetComponent<Menu_MesseSelect>().SetGame(this.slot, null);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600118B RID: 4491 RVA: 0x000B97D3 File Offset: 0x000B79D3
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001600 RID: 5632
	public GameObject[] uiPrefabs;

	// Token: 0x04001601 RID: 5633
	public GameObject[] uiObjects;

	// Token: 0x04001602 RID: 5634
	private mainScript mS_;

	// Token: 0x04001603 RID: 5635
	private GameObject main_;

	// Token: 0x04001604 RID: 5636
	private GUI_Main guiMain_;

	// Token: 0x04001605 RID: 5637
	private sfxScript sfx_;

	// Token: 0x04001606 RID: 5638
	private textScript tS_;

	// Token: 0x04001607 RID: 5639
	private genres genres_;

	// Token: 0x04001608 RID: 5640
	private Menu_MesseSelect menu_MesseSelect_;

	// Token: 0x04001609 RID: 5641
	private int slot;
}
