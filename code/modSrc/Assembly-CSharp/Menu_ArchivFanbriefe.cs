using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000185 RID: 389
public class Menu_ArchivFanbriefe : MonoBehaviour
{
	// Token: 0x06000EA1 RID: 3745 RVA: 0x0009D190 File Offset: 0x0009B390
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000EA2 RID: 3746 RVA: 0x0009D198 File Offset: 0x0009B398
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
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
	}

	// Token: 0x06000EA3 RID: 3747 RVA: 0x0009D27E File Offset: 0x0009B47E
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000EA4 RID: 3748 RVA: 0x0009D2B8 File Offset: 0x0009B4B8
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

	// Token: 0x06000EA5 RID: 3749 RVA: 0x0009D31C File Offset: 0x0009B51C
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_ArchivFanbrief>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000EA6 RID: 3750 RVA: 0x0009D378 File Offset: 0x0009B578
	public void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.TAB_NoArchiv(0);
	}

	// Token: 0x06000EA7 RID: 3751 RVA: 0x0009D390 File Offset: 0x0009B590
	public void InitDropdowns()
	{
		this.FindScripts();
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(273));
		list.Add(this.tS_.GetText(217));
		list.Add(this.tS_.GetText(275));
		list.Add(this.tS_.GetText(277));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06000EA8 RID: 3752 RVA: 0x0009D464 File Offset: 0x0009B664
	private void Init(bool gekauft)
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData(gekauft);
	}

	// Token: 0x06000EA9 RID: 3753 RVA: 0x0009D4BC File Offset: 0x0009B6BC
	private void SetData(bool archiv_)
	{
		bool isOn = this.uiObjects[6].GetComponent<Toggle>().isOn;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && component.archiv_fanbriefe == archiv_ && (!isOn || (isOn && !component.typ_contractGame)) && this.CheckGameData(component) && !this.Exists(this.uiObjects[0], component.myID))
				{
					Item_ArchivFanbrief component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_ArchivFanbrief>();
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.genres_ = this.genres_;
					component2.game_ = component;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x06000EAA RID: 3754 RVA: 0x0009D5FC File Offset: 0x0009B7FC
	public bool CheckGameData(gameScript script_)
	{
		return script_ && (script_.ownerID == this.mS_.myID || script_.publisherID == this.mS_.myID) && !script_.typ_addon && !script_.typ_budget && !script_.typ_mmoaddon && !script_.typ_bundle && !script_.typ_bundleAddon && !script_.typ_goty && !script_.inDevelopment && script_.GetAmountFanbriefe() > 0;
	}

	// Token: 0x06000EAB RID: 3755 RVA: 0x0009D67C File Offset: 0x0009B87C
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
				Item_ArchivFanbrief component = gameObject.GetComponent<Item_ArchivFanbrief>();
				switch (value)
				{
				case 0:
					gameObject.name = component.game_.GetNameSimple();
					break;
				case 1:
					gameObject.name = component.game_.maingenre.ToString();
					break;
				case 2:
				{
					float num = (float)component.game_.date_month;
					num /= 13f;
					gameObject.name = component.game_.date_year.ToString() + num.ToString();
					break;
				}
				case 3:
					gameObject.name = component.game_.sellsTotal.ToString();
					break;
				case 4:
					gameObject.name = component.game_.reviewTotal.ToString();
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

	// Token: 0x06000EAC RID: 3756 RVA: 0x0009D7E8 File Offset: 0x0009B9E8
	public void BUTTON_All()
	{
		this.sfx_.PlaySound(3, true);
		int childCount = this.uiObjects[0].transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = this.uiObjects[0].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				Item_ArchivFanbrief component = gameObject.GetComponent<Item_ArchivFanbrief>();
				if (component)
				{
					component.BUTTON_Click();
				}
			}
		}
	}

	// Token: 0x06000EAD RID: 3757 RVA: 0x0009D857 File Offset: 0x0009BA57
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000EAE RID: 3758 RVA: 0x0009D872 File Offset: 0x0009BA72
	public void TAB_NoArchiv(int t)
	{
		this.TAB = t;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.SetTab(this.uiObjects[4], t);
		this.Init(false);
	}

	// Token: 0x06000EAF RID: 3759 RVA: 0x0009D8A3 File Offset: 0x0009BAA3
	public void TAB_Archiv(int t)
	{
		this.TAB = t;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.SetTab(this.uiObjects[4], t);
		this.Init(true);
	}

	// Token: 0x06000EB0 RID: 3760 RVA: 0x0009D8D4 File Offset: 0x0009BAD4
	public void TOGGLE_AuftragsspieleAusblenden()
	{
		if (this.TAB == 0)
		{
			this.Init(false);
			return;
		}
		this.Init(true);
	}

	// Token: 0x0400130E RID: 4878
	public GameObject[] uiPrefabs;

	// Token: 0x0400130F RID: 4879
	public GameObject[] uiObjects;

	// Token: 0x04001310 RID: 4880
	private mainScript mS_;

	// Token: 0x04001311 RID: 4881
	private GameObject main_;

	// Token: 0x04001312 RID: 4882
	private GUI_Main guiMain_;

	// Token: 0x04001313 RID: 4883
	private sfxScript sfx_;

	// Token: 0x04001314 RID: 4884
	private textScript tS_;

	// Token: 0x04001315 RID: 4885
	private engineFeatures eF_;

	// Token: 0x04001316 RID: 4886
	private genres genres_;

	// Token: 0x04001317 RID: 4887
	private int TAB;

	// Token: 0x04001318 RID: 4888
	private float updateTimer;
}
