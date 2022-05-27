using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000184 RID: 388
public class Menu_ArchivFanbriefe : MonoBehaviour
{
	// Token: 0x06000E89 RID: 3721 RVA: 0x0000A330 File Offset: 0x00008530
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000E8A RID: 3722 RVA: 0x000AA9A0 File Offset: 0x000A8BA0
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

	// Token: 0x06000E8B RID: 3723 RVA: 0x0000A338 File Offset: 0x00008538
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000E8C RID: 3724 RVA: 0x000AAA88 File Offset: 0x000A8C88
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

	// Token: 0x06000E8D RID: 3725 RVA: 0x000AAAEC File Offset: 0x000A8CEC
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

	// Token: 0x06000E8E RID: 3726 RVA: 0x0000A370 File Offset: 0x00008570
	public void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.TAB_NoArchiv(0);
	}

	// Token: 0x06000E8F RID: 3727 RVA: 0x000AAB48 File Offset: 0x000A8D48
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

	// Token: 0x06000E90 RID: 3728 RVA: 0x000AAC1C File Offset: 0x000A8E1C
	private void Init(bool gekauft)
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData(gekauft);
	}

	// Token: 0x06000E91 RID: 3729 RVA: 0x000AAC74 File Offset: 0x000A8E74
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

	// Token: 0x06000E92 RID: 3730 RVA: 0x000AADB4 File Offset: 0x000A8FB4
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.playerGame && !script_.typ_addon && !script_.typ_budget && !script_.typ_mmoaddon && !script_.typ_bundle && !script_.typ_bundleAddon && !script_.typ_goty && !script_.inDevelopment && script_.GetAmountFanbriefe() > 0;
	}

	// Token: 0x06000E93 RID: 3731 RVA: 0x000AAE18 File Offset: 0x000A9018
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

	// Token: 0x06000E94 RID: 3732 RVA: 0x000AAF84 File Offset: 0x000A9184
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

	// Token: 0x06000E95 RID: 3733 RVA: 0x0000A385 File Offset: 0x00008585
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000E96 RID: 3734 RVA: 0x0000A3A0 File Offset: 0x000085A0
	public void TAB_NoArchiv(int t)
	{
		this.TAB = t;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.SetTab(this.uiObjects[4], t);
		this.Init(false);
	}

	// Token: 0x06000E97 RID: 3735 RVA: 0x0000A3D1 File Offset: 0x000085D1
	public void TAB_Archiv(int t)
	{
		this.TAB = t;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.SetTab(this.uiObjects[4], t);
		this.Init(true);
	}

	// Token: 0x06000E98 RID: 3736 RVA: 0x0000A402 File Offset: 0x00008602
	public void TOGGLE_AuftragsspieleAusblenden()
	{
		if (this.TAB == 0)
		{
			this.Init(false);
			return;
		}
		this.Init(true);
	}

	// Token: 0x04001305 RID: 4869
	public GameObject[] uiPrefabs;

	// Token: 0x04001306 RID: 4870
	public GameObject[] uiObjects;

	// Token: 0x04001307 RID: 4871
	private mainScript mS_;

	// Token: 0x04001308 RID: 4872
	private GameObject main_;

	// Token: 0x04001309 RID: 4873
	private GUI_Main guiMain_;

	// Token: 0x0400130A RID: 4874
	private sfxScript sfx_;

	// Token: 0x0400130B RID: 4875
	private textScript tS_;

	// Token: 0x0400130C RID: 4876
	private engineFeatures eF_;

	// Token: 0x0400130D RID: 4877
	private genres genres_;

	// Token: 0x0400130E RID: 4878
	private int TAB;

	// Token: 0x0400130F RID: 4879
	private float updateTimer;
}
