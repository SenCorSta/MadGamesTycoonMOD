using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000202 RID: 514
public class Menu_PublishingOfferSelect : MonoBehaviour
{
	// Token: 0x060013A3 RID: 5027 RVA: 0x000CE31B File Offset: 0x000CC51B
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060013A4 RID: 5028 RVA: 0x000CE324 File Offset: 0x000CC524
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
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
	}

	// Token: 0x060013A5 RID: 5029 RVA: 0x000CE40A File Offset: 0x000CC60A
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x060013A6 RID: 5030 RVA: 0x000CE43C File Offset: 0x000CC63C
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_PubOfferSelect>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060013A7 RID: 5031 RVA: 0x000CE498 File Offset: 0x000CC698
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x060013A8 RID: 5032 RVA: 0x000CE4A8 File Offset: 0x000CC6A8
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(1732));
		list.Add(this.tS_.GetText(273));
		list.Add(this.tS_.GetText(1730));
		list.Add(this.tS_.GetText(1731));
		list.Add(this.tS_.GetText(274));
		list.Add(this.tS_.GetText(1555));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x060013A9 RID: 5033 RVA: 0x000CE5A0 File Offset: 0x000CC7A0
	public void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x060013AA RID: 5034 RVA: 0x000CE5F4 File Offset: 0x000CC7F4
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && component.pubAngebot && !component.pubAnbgebot_Inivs)
				{
					string text = component.GetNameSimple();
					this.searchStringA = this.searchStringA.ToLower();
					text = text.ToLower();
					if ((this.uiObjects[6].GetComponent<InputField>().text.Length <= 0 || text.Contains(this.searchStringA)) && !this.Exists(this.uiObjects[0], component.myID))
					{
						Item_PubOfferSelect component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_PubOfferSelect>();
						component2.game_ = component;
						component2.mS_ = this.mS_;
						component2.tS_ = this.tS_;
						component2.sfx_ = this.sfx_;
						component2.guiMain_ = this.guiMain_;
						component2.genres_ = this.genres_;
						component2.games_ = this.games_;
					}
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x060013AB RID: 5035 RVA: 0x000CE764 File Offset: 0x000CC964
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
				Item_PubOfferSelect component = gameObject.GetComponent<Item_PubOfferSelect>();
				switch (value)
				{
				case 0:
					gameObject.name = component.game_.GetNameSimple();
					break;
				case 1:
					gameObject.name = component.game_.reviewTotal.ToString();
					break;
				case 2:
					gameObject.name = component.game_.maingenre.ToString();
					break;
				case 3:
					gameObject.name = component.game_.PUBOFFER_GetGarantiesumme().ToString();
					break;
				case 4:
					gameObject.name = component.game_.PUBOFFER_GetGewinnbeteiligung().ToString();
					break;
				case 5:
					gameObject.name = component.game_.GetDeveloperName();
					break;
				case 6:
					gameObject.name = component.game_.GetIpBekanntheit().ToString();
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

	// Token: 0x060013AC RID: 5036 RVA: 0x000CE8EE File Offset: 0x000CCAEE
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060013AD RID: 5037 RVA: 0x000CE914 File Offset: 0x000CCB14
	public void BUTTON_Search()
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.searchStringA = this.uiObjects[6].GetComponent<InputField>().text;
		this.Init();
	}

	// Token: 0x040017C8 RID: 6088
	public GameObject[] uiPrefabs;

	// Token: 0x040017C9 RID: 6089
	public GameObject[] uiObjects;

	// Token: 0x040017CA RID: 6090
	private mainScript mS_;

	// Token: 0x040017CB RID: 6091
	private GameObject main_;

	// Token: 0x040017CC RID: 6092
	private GUI_Main guiMain_;

	// Token: 0x040017CD RID: 6093
	private sfxScript sfx_;

	// Token: 0x040017CE RID: 6094
	private textScript tS_;

	// Token: 0x040017CF RID: 6095
	private genres genres_;

	// Token: 0x040017D0 RID: 6096
	private games games_;

	// Token: 0x040017D1 RID: 6097
	private string searchStringA = "";
}
