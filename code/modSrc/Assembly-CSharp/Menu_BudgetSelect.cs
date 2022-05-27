using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001F2 RID: 498
public class Menu_BudgetSelect : MonoBehaviour
{
	// Token: 0x060012E8 RID: 4840 RVA: 0x000C87F0 File Offset: 0x000C69F0
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060012E9 RID: 4841 RVA: 0x000C87F8 File Offset: 0x000C69F8
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

	// Token: 0x060012EA RID: 4842 RVA: 0x000C88C0 File Offset: 0x000C6AC0
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060012EB RID: 4843 RVA: 0x000C88F8 File Offset: 0x000C6AF8
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

	// Token: 0x060012EC RID: 4844 RVA: 0x000C8944 File Offset: 0x000C6B44
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_BudgetSelect>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060012ED RID: 4845 RVA: 0x000C89A0 File Offset: 0x000C6BA0
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x060012EE RID: 4846 RVA: 0x000C89B0 File Offset: 0x000C6BB0
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(1152));
		list.Add(this.tS_.GetText(217));
		list.Add(this.tS_.GetText(277));
		list.Add(this.tS_.GetText(273));
		list.Add(this.tS_.GetText(275));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x060012EF RID: 4847 RVA: 0x000C8A94 File Offset: 0x000C6C94
	public void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x060012F0 RID: 4848 RVA: 0x000C8AE8 File Offset: 0x000C6CE8
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
					Item_BudgetSelect component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_BudgetSelect>();
					component2.game_ = component;
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.genres_ = this.genres_;
					component2.menu_ = this;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x060012F1 RID: 4849 RVA: 0x000C8BFC File Offset: 0x000C6DFC
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.ownerID == this.mS_.myID && !script_.inDevelopment && !script_.isOnMarket && !script_.budget_created && script_.gameTyp == 0 && !script_.schublade && !script_.typ_goty && (script_.typ_standard || script_.typ_nachfolger || script_.typ_remaster || script_.typ_spinoff) && !script_.handy && !script_.arcade && !script_.typ_bundle && !script_.typ_bundleAddon && !script_.pubOffer;
	}

	// Token: 0x060012F2 RID: 4850 RVA: 0x000C8CA4 File Offset: 0x000C6EA4
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
				Item_BudgetSelect component = gameObject.GetComponent<Item_BudgetSelect>();
				switch (value)
				{
				case 0:
					gameObject.name = component.game_.GetNameSimple();
					break;
				case 1:
					gameObject.name = component.game_.freigabeBudget.ToString();
					break;
				case 2:
				{
					float num = (float)component.game_.date_month;
					num /= 13f;
					gameObject.name = component.game_.date_year.ToString() + num.ToString();
					break;
				}
				case 3:
					gameObject.name = component.game_.reviewTotal.ToString();
					break;
				case 4:
					gameObject.name = component.game_.maingenre.ToString();
					break;
				case 5:
					gameObject.name = component.game_.sellsTotal.ToString();
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

	// Token: 0x060012F3 RID: 4851 RVA: 0x000C8E2F File Offset: 0x000C702F
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001735 RID: 5941
	public GameObject[] uiPrefabs;

	// Token: 0x04001736 RID: 5942
	public GameObject[] uiObjects;

	// Token: 0x04001737 RID: 5943
	private mainScript mS_;

	// Token: 0x04001738 RID: 5944
	private GameObject main_;

	// Token: 0x04001739 RID: 5945
	private GUI_Main guiMain_;

	// Token: 0x0400173A RID: 5946
	private sfxScript sfx_;

	// Token: 0x0400173B RID: 5947
	private textScript tS_;

	// Token: 0x0400173C RID: 5948
	private genres genres_;

	// Token: 0x0400173D RID: 5949
	private float updateTimer;
}
