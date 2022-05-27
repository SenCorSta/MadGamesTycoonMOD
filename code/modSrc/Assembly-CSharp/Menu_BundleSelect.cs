using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001F3 RID: 499
public class Menu_BundleSelect : MonoBehaviour
{
	// Token: 0x060012E7 RID: 4839 RVA: 0x0000CFA4 File Offset: 0x0000B1A4
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060012E8 RID: 4840 RVA: 0x000D3FC4 File Offset: 0x000D21C4
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
		if (!this.menuBundle_)
		{
			this.menuBundle_ = this.guiMain_.uiObjects[267].GetComponent<Menu_Bundle>();
		}
	}

	// Token: 0x060012E9 RID: 4841 RVA: 0x0000CFAC File Offset: 0x0000B1AC
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060012EA RID: 4842 RVA: 0x000D40B8 File Offset: 0x000D22B8
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

	// Token: 0x060012EB RID: 4843 RVA: 0x000D4104 File Offset: 0x000D2304
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_BundleSelect>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060012EC RID: 4844 RVA: 0x0000CFE4 File Offset: 0x0000B1E4
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x060012ED RID: 4845 RVA: 0x000D4160 File Offset: 0x000D2360
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(217));
		list.Add(this.tS_.GetText(277));
		list.Add(this.tS_.GetText(273));
		list.Add(this.tS_.GetText(275));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x060012EE RID: 4846 RVA: 0x000D422C File Offset: 0x000D242C
	public void Init(int slot_)
	{
		this.FindScripts();
		this.slot = slot_;
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x060012EF RID: 4847 RVA: 0x000D4288 File Offset: 0x000D2488
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
					Item_BundleSelect component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_BundleSelect>();
					component2.game_ = component;
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.genres_ = this.genres_;
					component2.menu_ = this.menuBundle_;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x060012F0 RID: 4848 RVA: 0x000D43A4 File Offset: 0x000D25A4
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.playerGame && !script_.inDevelopment && !script_.isOnMarket && script_.typ_budget && !script_.bundle_created && !script_.pubOffer && script_.gameTyp == 0 && !script_.typ_mmoaddon && !script_.typ_bundle && !script_.typ_addon && !script_.typ_addonStandalone && this.menuBundle_.games[0] != script_ && this.menuBundle_.games[1] != script_ && this.menuBundle_.games[2] != script_ && this.menuBundle_.games[3] != script_ && this.menuBundle_.games[4] != script_ && !script_.handy && !script_.arcade;
	}

	// Token: 0x060012F1 RID: 4849 RVA: 0x000D44B0 File Offset: 0x000D26B0
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
				Item_BundleSelect component = gameObject.GetComponent<Item_BundleSelect>();
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
					gameObject.name = component.game_.reviewTotal.ToString();
					break;
				case 3:
					gameObject.name = component.game_.maingenre.ToString();
					break;
				case 4:
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

	// Token: 0x060012F2 RID: 4850 RVA: 0x0000CFF2 File Offset: 0x0000B1F2
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0400173F RID: 5951
	public GameObject[] uiPrefabs;

	// Token: 0x04001740 RID: 5952
	public GameObject[] uiObjects;

	// Token: 0x04001741 RID: 5953
	private mainScript mS_;

	// Token: 0x04001742 RID: 5954
	private GameObject main_;

	// Token: 0x04001743 RID: 5955
	private GUI_Main guiMain_;

	// Token: 0x04001744 RID: 5956
	private sfxScript sfx_;

	// Token: 0x04001745 RID: 5957
	private textScript tS_;

	// Token: 0x04001746 RID: 5958
	private genres genres_;

	// Token: 0x04001747 RID: 5959
	private Menu_Bundle menuBundle_;

	// Token: 0x04001748 RID: 5960
	public int slot;

	// Token: 0x04001749 RID: 5961
	private float updateTimer;
}
