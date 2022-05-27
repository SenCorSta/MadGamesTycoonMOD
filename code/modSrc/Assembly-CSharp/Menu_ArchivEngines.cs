using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000184 RID: 388
public class Menu_ArchivEngines : MonoBehaviour
{
	// Token: 0x06000E91 RID: 3729 RVA: 0x0009C9A7 File Offset: 0x0009ABA7
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000E92 RID: 3730 RVA: 0x0009C9B0 File Offset: 0x0009ABB0
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

	// Token: 0x06000E93 RID: 3731 RVA: 0x0009CA96 File Offset: 0x0009AC96
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000E94 RID: 3732 RVA: 0x0009CAD0 File Offset: 0x0009ACD0
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

	// Token: 0x06000E95 RID: 3733 RVA: 0x0009CB34 File Offset: 0x0009AD34
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_ArchivEngine>().eS_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000E96 RID: 3734 RVA: 0x0009CB90 File Offset: 0x0009AD90
	public void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.TAB_NoArchiv(0);
	}

	// Token: 0x06000E97 RID: 3735 RVA: 0x0009CBA8 File Offset: 0x0009ADA8
	public void InitDropdowns()
	{
		this.FindScripts();
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(4));
		list.Add(this.tS_.GetText(245));
		list.Add(this.tS_.GetText(160));
		list.Add(this.tS_.GetText(261));
		list.Add(this.tS_.GetText(258));
		list.Add(this.tS_.GetText(260));
		list.Add(this.tS_.GetText(268));
		list.Add(this.tS_.GetText(1218));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06000E98 RID: 3736 RVA: 0x0009CCD0 File Offset: 0x0009AED0
	private void Init(bool gekauft)
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData(gekauft);
	}

	// Token: 0x06000E99 RID: 3737 RVA: 0x0009CD28 File Offset: 0x0009AF28
	private void SetData(bool archiv_)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Engine");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				engineScript component = array[i].GetComponent<engineScript>();
				if (component && component.archiv_engine == archiv_ && this.CheckData(component) && !this.Exists(this.uiObjects[0], component.myID))
				{
					Item_ArchivEngine component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_ArchivEngine>();
					component2.eS_ = component;
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x06000E9A RID: 3738 RVA: 0x0009CE34 File Offset: 0x0009B034
	public bool CheckData(engineScript script_)
	{
		return script_ && script_.myID != 0 && ((script_.isUnlocked && script_.gekauft) || (script_.ownerID == this.mS_.myID && script_.devPointsStart <= 0f) || (script_.ownerID == this.mS_.myID && script_.updating));
	}

	// Token: 0x06000E9B RID: 3739 RVA: 0x0009CEA0 File Offset: 0x0009B0A0
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
				Item_ArchivEngine component = gameObject.GetComponent<Item_ArchivEngine>();
				switch (value)
				{
				case 0:
					gameObject.name = component.eS_.GetName();
					break;
				case 1:
					gameObject.name = (component.eS_.GetTechLevel() * 1000 + component.eS_.GetFeaturesAmount()).ToString();
					break;
				case 2:
					gameObject.name = component.eS_.spezialgenre.ToString();
					break;
				case 3:
					gameObject.name = component.eS_.GetFeaturesAmount().ToString();
					break;
				case 4:
					gameObject.name = component.eS_.GetGamesAmount().ToString();
					break;
				case 5:
					if (component.eS_.ownerID == this.mS_.myID)
					{
						gameObject.name = "2";
					}
					else
					{
						gameObject.name = "1";
					}
					break;
				case 6:
					gameObject.name = component.eS_.gewinnbeteiligung.ToString();
					break;
				case 7:
					gameObject.name = component.eS_.GetVerkaufteLizenzen().ToString();
					break;
				case 8:
					gameObject.name = component.eS_.spezialplatform.ToString();
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

	// Token: 0x06000E9C RID: 3740 RVA: 0x0009D0A4 File Offset: 0x0009B2A4
	public void BUTTON_All()
	{
		this.sfx_.PlaySound(3, true);
		int childCount = this.uiObjects[0].transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = this.uiObjects[0].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				Item_ArchivEngine component = gameObject.GetComponent<Item_ArchivEngine>();
				if (component)
				{
					component.BUTTON_Click();
				}
			}
		}
	}

	// Token: 0x06000E9D RID: 3741 RVA: 0x0009D113 File Offset: 0x0009B313
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000E9E RID: 3742 RVA: 0x0009D12E File Offset: 0x0009B32E
	public void TAB_NoArchiv(int t)
	{
		this.TAB = t;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.SetTab(this.uiObjects[4], t);
		this.Init(false);
	}

	// Token: 0x06000E9F RID: 3743 RVA: 0x0009D15F File Offset: 0x0009B35F
	public void TAB_Archiv(int t)
	{
		this.TAB = t;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.SetTab(this.uiObjects[4], t);
		this.Init(true);
	}

	// Token: 0x04001303 RID: 4867
	public GameObject[] uiPrefabs;

	// Token: 0x04001304 RID: 4868
	public GameObject[] uiObjects;

	// Token: 0x04001305 RID: 4869
	private mainScript mS_;

	// Token: 0x04001306 RID: 4870
	private GameObject main_;

	// Token: 0x04001307 RID: 4871
	private GUI_Main guiMain_;

	// Token: 0x04001308 RID: 4872
	private sfxScript sfx_;

	// Token: 0x04001309 RID: 4873
	private textScript tS_;

	// Token: 0x0400130A RID: 4874
	private engineFeatures eF_;

	// Token: 0x0400130B RID: 4875
	private genres genres_;

	// Token: 0x0400130C RID: 4876
	private int TAB;

	// Token: 0x0400130D RID: 4877
	private float updateTimer;
}
