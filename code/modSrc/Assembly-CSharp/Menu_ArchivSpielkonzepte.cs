using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000187 RID: 391
public class Menu_ArchivSpielkonzepte : MonoBehaviour
{
	// Token: 0x06000EB5 RID: 3765 RVA: 0x0000A61D File Offset: 0x0000881D
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000EB6 RID: 3766 RVA: 0x000AB6AC File Offset: 0x000A98AC
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

	// Token: 0x06000EB7 RID: 3767 RVA: 0x0000A625 File Offset: 0x00008825
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000EB8 RID: 3768 RVA: 0x000AB794 File Offset: 0x000A9994
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

	// Token: 0x06000EB9 RID: 3769 RVA: 0x000AB7F8 File Offset: 0x000A99F8
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

	// Token: 0x06000EBA RID: 3770 RVA: 0x0000A65D File Offset: 0x0000885D
	public void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.TAB_NoArchiv(0);
	}

	// Token: 0x06000EBB RID: 3771 RVA: 0x000AB854 File Offset: 0x000A9A54
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

	// Token: 0x06000EBC RID: 3772 RVA: 0x000AB928 File Offset: 0x000A9B28
	private void Init(bool gekauft)
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData(gekauft);
	}

	// Token: 0x06000EBD RID: 3773 RVA: 0x000AB980 File Offset: 0x000A9B80
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

	// Token: 0x06000EBE RID: 3774 RVA: 0x000ABAC0 File Offset: 0x000A9CC0
	public bool CheckGameData(gameScript script_)
	{
		return script_ && (script_.playerGame || script_.IsMyAuftragsspiel()) && !script_.typ_budget && !script_.typ_bundle && !script_.typ_bundleAddon && !script_.typ_goty && !script_.pubOffer && !script_.auftragsspiel;
	}

	// Token: 0x06000EBF RID: 3775 RVA: 0x000ABB18 File Offset: 0x000A9D18
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

	// Token: 0x06000EC0 RID: 3776 RVA: 0x000ABC84 File Offset: 0x000A9E84
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

	// Token: 0x06000EC1 RID: 3777 RVA: 0x0000A672 File Offset: 0x00008872
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000EC2 RID: 3778 RVA: 0x0000A68D File Offset: 0x0000888D
	public void TAB_NoArchiv(int t)
	{
		this.TAB = t;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.SetTab(this.uiObjects[4], t);
		this.Init(false);
	}

	// Token: 0x06000EC3 RID: 3779 RVA: 0x0000A6BE File Offset: 0x000088BE
	public void TAB_Archiv(int t)
	{
		this.TAB = t;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.SetTab(this.uiObjects[4], t);
		this.Init(true);
	}

	// Token: 0x06000EC4 RID: 3780 RVA: 0x0000A6EF File Offset: 0x000088EF
	public void TOGGLE_AuftragsspieleAusblenden()
	{
		if (this.TAB == 0)
		{
			this.Init(false);
			return;
		}
		this.Init(true);
	}

	// Token: 0x04001322 RID: 4898
	public GameObject[] uiPrefabs;

	// Token: 0x04001323 RID: 4899
	public GameObject[] uiObjects;

	// Token: 0x04001324 RID: 4900
	private mainScript mS_;

	// Token: 0x04001325 RID: 4901
	private GameObject main_;

	// Token: 0x04001326 RID: 4902
	private GUI_Main guiMain_;

	// Token: 0x04001327 RID: 4903
	private sfxScript sfx_;

	// Token: 0x04001328 RID: 4904
	private textScript tS_;

	// Token: 0x04001329 RID: 4905
	private engineFeatures eF_;

	// Token: 0x0400132A RID: 4906
	private genres genres_;

	// Token: 0x0400132B RID: 4907
	private int TAB;

	// Token: 0x0400132C RID: 4908
	private float updateTimer;
}
