using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000201 RID: 513
public class Menu_PublishingOfferSelect : MonoBehaviour
{
	// Token: 0x06001388 RID: 5000 RVA: 0x0000D596 File Offset: 0x0000B796
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001389 RID: 5001 RVA: 0x000D84C4 File Offset: 0x000D66C4
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

	// Token: 0x0600138A RID: 5002 RVA: 0x0000D59E File Offset: 0x0000B79E
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x0600138B RID: 5003 RVA: 0x000D85AC File Offset: 0x000D67AC
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

	// Token: 0x0600138C RID: 5004 RVA: 0x0000D5D0 File Offset: 0x0000B7D0
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x0600138D RID: 5005 RVA: 0x000D8608 File Offset: 0x000D6808
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

	// Token: 0x0600138E RID: 5006 RVA: 0x000D8700 File Offset: 0x000D6900
	public void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x0600138F RID: 5007 RVA: 0x000D8754 File Offset: 0x000D6954
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

	// Token: 0x06001390 RID: 5008 RVA: 0x000D88C4 File Offset: 0x000D6AC4
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

	// Token: 0x06001391 RID: 5009 RVA: 0x0000D5DE File Offset: 0x0000B7DE
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001392 RID: 5010 RVA: 0x000D8A50 File Offset: 0x000D6C50
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

	// Token: 0x040017BF RID: 6079
	public GameObject[] uiPrefabs;

	// Token: 0x040017C0 RID: 6080
	public GameObject[] uiObjects;

	// Token: 0x040017C1 RID: 6081
	private mainScript mS_;

	// Token: 0x040017C2 RID: 6082
	private GameObject main_;

	// Token: 0x040017C3 RID: 6083
	private GUI_Main guiMain_;

	// Token: 0x040017C4 RID: 6084
	private sfxScript sfx_;

	// Token: 0x040017C5 RID: 6085
	private textScript tS_;

	// Token: 0x040017C6 RID: 6086
	private genres genres_;

	// Token: 0x040017C7 RID: 6087
	private games games_;

	// Token: 0x040017C8 RID: 6088
	private string searchStringA = "";
}
