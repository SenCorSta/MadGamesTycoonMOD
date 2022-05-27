using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000188 RID: 392
public class Menu_ArchivSpielkonzepte : MonoBehaviour
{
	// Token: 0x06000ECD RID: 3789 RVA: 0x0009E1A5 File Offset: 0x0009C3A5
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000ECE RID: 3790 RVA: 0x0009E1B0 File Offset: 0x0009C3B0
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

	// Token: 0x06000ECF RID: 3791 RVA: 0x0009E296 File Offset: 0x0009C496
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000ED0 RID: 3792 RVA: 0x0009E2D0 File Offset: 0x0009C4D0
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

	// Token: 0x06000ED1 RID: 3793 RVA: 0x0009E334 File Offset: 0x0009C534
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_ArchivSpielkonzept>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000ED2 RID: 3794 RVA: 0x0009E390 File Offset: 0x0009C590
	public void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.TAB_NoArchiv(0);
	}

	// Token: 0x06000ED3 RID: 3795 RVA: 0x0009E3A8 File Offset: 0x0009C5A8
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

	// Token: 0x06000ED4 RID: 3796 RVA: 0x0009E47C File Offset: 0x0009C67C
	private void Init(bool gekauft)
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData(gekauft);
	}

	// Token: 0x06000ED5 RID: 3797 RVA: 0x0009E4D4 File Offset: 0x0009C6D4
	private void SetData(bool archiv_)
	{
		bool isOn = this.uiObjects[6].GetComponent<Toggle>().isOn;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && component.archiv_spielkonzept == archiv_ && (!isOn || (isOn && !component.typ_contractGame)) && this.CheckGameData(component) && !this.Exists(this.uiObjects[0], component.myID))
				{
					Item_ArchivSpielkonzept component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_ArchivSpielkonzept>();
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

	// Token: 0x06000ED6 RID: 3798 RVA: 0x0009E614 File Offset: 0x0009C814
	public bool CheckGameData(gameScript script_)
	{
		return script_ && (script_.developerID == this.mS_.myID || script_.IsMyAuftragsspiel()) && !script_.typ_budget && !script_.typ_bundle && !script_.typ_bundleAddon && !script_.typ_goty && !script_.pubOffer;
	}

	// Token: 0x06000ED7 RID: 3799 RVA: 0x0009E670 File Offset: 0x0009C870
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
				Item_ArchivSpielkonzept component = gameObject.GetComponent<Item_ArchivSpielkonzept>();
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

	// Token: 0x06000ED8 RID: 3800 RVA: 0x0009E7DC File Offset: 0x0009C9DC
	public void BUTTON_All()
	{
		this.sfx_.PlaySound(3, true);
		int childCount = this.uiObjects[0].transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = this.uiObjects[0].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				Item_ArchivSpielkonzept component = gameObject.GetComponent<Item_ArchivSpielkonzept>();
				if (component)
				{
					component.BUTTON_Click();
				}
			}
		}
	}

	// Token: 0x06000ED9 RID: 3801 RVA: 0x0009E84B File Offset: 0x0009CA4B
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000EDA RID: 3802 RVA: 0x0009E866 File Offset: 0x0009CA66
	public void TAB_NoArchiv(int t)
	{
		this.TAB = t;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.SetTab(this.uiObjects[4], t);
		this.Init(false);
	}

	// Token: 0x06000EDB RID: 3803 RVA: 0x0009E897 File Offset: 0x0009CA97
	public void TAB_Archiv(int t)
	{
		this.TAB = t;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.SetTab(this.uiObjects[4], t);
		this.Init(true);
	}

	// Token: 0x06000EDC RID: 3804 RVA: 0x0009E8C8 File Offset: 0x0009CAC8
	public void TOGGLE_AuftragsspieleAusblenden()
	{
		if (this.TAB == 0)
		{
			this.Init(false);
			return;
		}
		this.Init(true);
	}

	// Token: 0x0400132B RID: 4907
	public GameObject[] uiPrefabs;

	// Token: 0x0400132C RID: 4908
	public GameObject[] uiObjects;

	// Token: 0x0400132D RID: 4909
	private mainScript mS_;

	// Token: 0x0400132E RID: 4910
	private GameObject main_;

	// Token: 0x0400132F RID: 4911
	private GUI_Main guiMain_;

	// Token: 0x04001330 RID: 4912
	private sfxScript sfx_;

	// Token: 0x04001331 RID: 4913
	private textScript tS_;

	// Token: 0x04001332 RID: 4914
	private engineFeatures eF_;

	// Token: 0x04001333 RID: 4915
	private genres genres_;

	// Token: 0x04001334 RID: 4916
	private int TAB;

	// Token: 0x04001335 RID: 4917
	private float updateTimer;
}
