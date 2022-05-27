using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200024C RID: 588
public class Menu_Stats_MyKonsolen_AllTimeCharts : MonoBehaviour
{
	// Token: 0x060016B9 RID: 5817 RVA: 0x0000FE44 File Offset: 0x0000E044
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060016BA RID: 5818 RVA: 0x000ECCF0 File Offset: 0x000EAEF0
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

	// Token: 0x060016BB RID: 5819 RVA: 0x0000FE4C File Offset: 0x0000E04C
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x060016BC RID: 5820 RVA: 0x000ECDB8 File Offset: 0x000EAFB8
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

	// Token: 0x060016BD RID: 5821 RVA: 0x0000FE7E File Offset: 0x0000E07E
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060016BE RID: 5822 RVA: 0x0000FE86 File Offset: 0x0000E086
	public void Init()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.SetData();
	}

	// Token: 0x060016BF RID: 5823 RVA: 0x000ECDFC File Offset: 0x000EAFFC
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

	// Token: 0x060016C0 RID: 5824 RVA: 0x000ECEA4 File Offset: 0x000EB0A4
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

	// Token: 0x060016C1 RID: 5825 RVA: 0x000ECFE8 File Offset: 0x000EB1E8
	public bool CheckKonsoleData(platformScript script_)
	{
		return script_ && script_.isUnlocked && script_.units > 0 && (script_.typ == 1 || script_.typ == 2) && (this.uiObjects[4].GetComponent<Dropdown>().value == 0 || (this.uiObjects[4].GetComponent<Dropdown>().value == 1 && script_.typ == 1) || (this.uiObjects[4].GetComponent<Dropdown>().value == 2 && script_.typ == 2));
	}

	// Token: 0x060016C2 RID: 5826 RVA: 0x0000FE9A File Offset: 0x0000E09A
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060016C3 RID: 5827 RVA: 0x000ED074 File Offset: 0x000EB274
	public void DROPDOWN_Sort()
	{
		int value = this.uiObjects[4].GetComponent<Dropdown>().value;
		PlayerPrefs.SetInt(this.uiObjects[4].name, value);
		this.SetData();
	}

	// Token: 0x04001A9D RID: 6813
	private mainScript mS_;

	// Token: 0x04001A9E RID: 6814
	private GameObject main_;

	// Token: 0x04001A9F RID: 6815
	private GUI_Main guiMain_;

	// Token: 0x04001AA0 RID: 6816
	private sfxScript sfx_;

	// Token: 0x04001AA1 RID: 6817
	private textScript tS_;

	// Token: 0x04001AA2 RID: 6818
	private genres genres_;

	// Token: 0x04001AA3 RID: 6819
	public GameObject[] uiPrefabs;

	// Token: 0x04001AA4 RID: 6820
	public GameObject[] uiObjects;
}
