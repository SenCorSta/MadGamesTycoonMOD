using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000187 RID: 391
public class Menu_ArchivSpielberichte : MonoBehaviour
{
	// Token: 0x06000EBC RID: 3772 RVA: 0x0009DA9A File Offset: 0x0009BC9A
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000EBD RID: 3773 RVA: 0x0009DAA4 File Offset: 0x0009BCA4
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

	// Token: 0x06000EBE RID: 3774 RVA: 0x0009DB8A File Offset: 0x0009BD8A
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000EBF RID: 3775 RVA: 0x0009DBC4 File Offset: 0x0009BDC4
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

	// Token: 0x06000EC0 RID: 3776 RVA: 0x0009DC28 File Offset: 0x0009BE28
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_ArchivSpielbericht>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000EC1 RID: 3777 RVA: 0x0009DC84 File Offset: 0x0009BE84
	public void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.TAB_NoArchiv(0);
	}

	// Token: 0x06000EC2 RID: 3778 RVA: 0x0009DC9C File Offset: 0x0009BE9C
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

	// Token: 0x06000EC3 RID: 3779 RVA: 0x0009DD70 File Offset: 0x0009BF70
	private void Init(bool gekauft)
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData(gekauft);
	}

	// Token: 0x06000EC4 RID: 3780 RVA: 0x0009DDC8 File Offset: 0x0009BFC8
	private void SetData(bool archiv_)
	{
		bool isOn = this.uiObjects[6].GetComponent<Toggle>().isOn;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && component.archiv_spielbericht == archiv_ && (!isOn || (isOn && !component.typ_contractGame)) && this.CheckGameData(component) && !this.Exists(this.uiObjects[0], component.myID))
				{
					Item_ArchivSpielbericht component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_ArchivSpielbericht>();
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

	// Token: 0x06000EC5 RID: 3781 RVA: 0x0009DF05 File Offset: 0x0009C105
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.developerID == this.mS_.myID && script_.spielbericht && !script_.typ_budget && !script_.typ_goty;
	}

	// Token: 0x06000EC6 RID: 3782 RVA: 0x0009DF40 File Offset: 0x0009C140
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
				Item_ArchivSpielbericht component = gameObject.GetComponent<Item_ArchivSpielbericht>();
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

	// Token: 0x06000EC7 RID: 3783 RVA: 0x0009E0AC File Offset: 0x0009C2AC
	public void BUTTON_All()
	{
		this.sfx_.PlaySound(3, true);
		int childCount = this.uiObjects[0].transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = this.uiObjects[0].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				Item_ArchivSpielbericht component = gameObject.GetComponent<Item_ArchivSpielbericht>();
				if (component)
				{
					component.BUTTON_Click();
				}
			}
		}
	}

	// Token: 0x06000EC8 RID: 3784 RVA: 0x0009E11B File Offset: 0x0009C31B
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000EC9 RID: 3785 RVA: 0x0009E136 File Offset: 0x0009C336
	public void TAB_NoArchiv(int t)
	{
		this.TAB = t;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.SetTab(this.uiObjects[4], t);
		this.Init(false);
	}

	// Token: 0x06000ECA RID: 3786 RVA: 0x0009E167 File Offset: 0x0009C367
	public void TAB_Archiv(int t)
	{
		this.TAB = t;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.SetTab(this.uiObjects[4], t);
		this.Init(true);
	}

	// Token: 0x06000ECB RID: 3787 RVA: 0x0009E198 File Offset: 0x0009C398
	public void TOGGLE_AuftragsspieleAusblenden()
	{
		if (this.TAB == 0)
		{
			this.Init(false);
			return;
		}
		this.Init(true);
	}

	// Token: 0x04001320 RID: 4896
	public GameObject[] uiPrefabs;

	// Token: 0x04001321 RID: 4897
	public GameObject[] uiObjects;

	// Token: 0x04001322 RID: 4898
	private mainScript mS_;

	// Token: 0x04001323 RID: 4899
	private GameObject main_;

	// Token: 0x04001324 RID: 4900
	private GUI_Main guiMain_;

	// Token: 0x04001325 RID: 4901
	private sfxScript sfx_;

	// Token: 0x04001326 RID: 4902
	private textScript tS_;

	// Token: 0x04001327 RID: 4903
	private engineFeatures eF_;

	// Token: 0x04001328 RID: 4904
	private genres genres_;

	// Token: 0x04001329 RID: 4905
	private int TAB;

	// Token: 0x0400132A RID: 4906
	private float updateTimer;
}
