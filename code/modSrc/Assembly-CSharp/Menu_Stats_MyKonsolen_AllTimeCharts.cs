using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200024D RID: 589
public class Menu_Stats_MyKonsolen_AllTimeCharts : MonoBehaviour
{
	// Token: 0x060016DE RID: 5854 RVA: 0x000E5F2B File Offset: 0x000E412B
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060016DF RID: 5855 RVA: 0x000E5F34 File Offset: 0x000E4134
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

	// Token: 0x060016E0 RID: 5856 RVA: 0x000E5FFC File Offset: 0x000E41FC
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x060016E1 RID: 5857 RVA: 0x000E6030 File Offset: 0x000E4230
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).GetComponent<Item_MyKonsolen_AllTimeCharts>().pS_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060016E2 RID: 5858 RVA: 0x000E6074 File Offset: 0x000E4274
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060016E3 RID: 5859 RVA: 0x000E607C File Offset: 0x000E427C
	public void Init()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.SetData();
	}

	// Token: 0x060016E4 RID: 5860 RVA: 0x000E6090 File Offset: 0x000E4290
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[4].name);
		this.FindScripts();
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(1675));
		list.Add(this.tS_.GetText(1676));
		list.Add(this.tS_.GetText(1677));
		this.uiObjects[4].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[4].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[4].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x060016E5 RID: 5861 RVA: 0x000E6138 File Offset: 0x000E4338
	private void SetData()
	{
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).transform.gameObject.SetActive(false);
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("Platform");
		for (int j = 0; j < array.Length; j++)
		{
			if (array[j])
			{
				platformScript component = array[j].GetComponent<platformScript>();
				if (component && this.CheckKonsoleData(component))
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform);
					Item_MyKonsolen_AllTimeCharts component2 = gameObject.GetComponent<Item_MyKonsolen_AllTimeCharts>();
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.pS_ = component;
					gameObject.name = component.units.ToString();
				}
			}
		}
		this.mS_.SortChildrenByFloat(this.uiObjects[0]);
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x060016E6 RID: 5862 RVA: 0x000E627C File Offset: 0x000E447C
	public bool CheckKonsoleData(platformScript script_)
	{
		return script_ && script_.isUnlocked && script_.units > 0 && (script_.typ == 1 || script_.typ == 2) && (this.uiObjects[4].GetComponent<Dropdown>().value == 0 || (this.uiObjects[4].GetComponent<Dropdown>().value == 1 && script_.typ == 1) || (this.uiObjects[4].GetComponent<Dropdown>().value == 2 && script_.typ == 2));
	}

	// Token: 0x060016E7 RID: 5863 RVA: 0x000E6307 File Offset: 0x000E4507
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060016E8 RID: 5864 RVA: 0x000E6324 File Offset: 0x000E4524
	public void DROPDOWN_Sort()
	{
		int value = this.uiObjects[4].GetComponent<Dropdown>().value;
		PlayerPrefs.SetInt(this.uiObjects[4].name, value);
		this.SetData();
	}

	// Token: 0x04001AA6 RID: 6822
	private mainScript mS_;

	// Token: 0x04001AA7 RID: 6823
	private GameObject main_;

	// Token: 0x04001AA8 RID: 6824
	private GUI_Main guiMain_;

	// Token: 0x04001AA9 RID: 6825
	private sfxScript sfx_;

	// Token: 0x04001AAA RID: 6826
	private textScript tS_;

	// Token: 0x04001AAB RID: 6827
	private genres genres_;

	// Token: 0x04001AAC RID: 6828
	public GameObject[] uiPrefabs;

	// Token: 0x04001AAD RID: 6829
	public GameObject[] uiObjects;
}
