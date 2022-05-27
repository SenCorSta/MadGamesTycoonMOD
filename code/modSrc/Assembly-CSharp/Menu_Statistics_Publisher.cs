using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200022B RID: 555
public class Menu_Statistics_Publisher : MonoBehaviour
{
	// Token: 0x0600154F RID: 5455 RVA: 0x0000EA81 File Offset: 0x0000CC81
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001550 RID: 5456 RVA: 0x000E37BC File Offset: 0x000E19BC
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
		if (!this.themes_)
		{
			this.themes_ = this.main_.GetComponent<themes>();
		}
		if (!this.mDevGame_)
		{
			this.mDevGame_ = this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
	}

	// Token: 0x06001551 RID: 5457 RVA: 0x0000EA89 File Offset: 0x0000CC89
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x06001552 RID: 5458 RVA: 0x0000EABB File Offset: 0x0000CCBB
	private void OnEnable()
	{
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x06001553 RID: 5459 RVA: 0x0000EAC9 File Offset: 0x0000CCC9
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x06001554 RID: 5460 RVA: 0x000E38C8 File Offset: 0x000E1AC8
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Publisher");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				publisherScript component = array[i].GetComponent<publisherScript>();
				if (component.isUnlocked && component.publisher && !component.onlyMobile && (!this.uiObjects[1].GetComponent<Toggle>().isOn || !component.IsMyTochterfirma()))
				{
					string text = component.GetName();
					this.searchStringA = this.searchStringA.ToLower();
					text = text.ToLower();
					if (this.uiObjects[6].GetComponent<InputField>().text.Length <= 0 || text.Contains(this.searchStringA))
					{
						Item_Stats_Publisher component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_Stats_Publisher>();
						component2.pS_ = component;
						component2.mS_ = this.mS_;
						component2.tS_ = this.tS_;
						component2.sfx_ = this.sfx_;
						component2.guiMain_ = this.guiMain_;
						component2.genres_ = this.genres_;
					}
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[4]);
	}

	// Token: 0x06001555 RID: 5461 RVA: 0x000E3A2C File Offset: 0x000E1C2C
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[5].name);
		this.FindScripts();
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(355));
		list.Add(this.tS_.GetText(434));
		list.Add(this.tS_.GetText(435));
		list.Add(this.tS_.GetText(436));
		list.Add(this.tS_.GetText(437));
		list.Add(this.tS_.GetText(1745));
		this.uiObjects[5].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[5].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[5].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06001556 RID: 5462 RVA: 0x000E3B14 File Offset: 0x000E1D14
	public void DROPDOWN_Sort()
	{
		int value = this.uiObjects[5].GetComponent<Dropdown>().value;
		PlayerPrefs.SetInt(this.uiObjects[5].name, value);
		int childCount = this.uiObjects[0].transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = this.uiObjects[0].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				Item_Stats_Publisher component = gameObject.GetComponent<Item_Stats_Publisher>();
				switch (value)
				{
				case 0:
					gameObject.name = component.pS_.GetName();
					break;
				case 1:
					gameObject.name = component.pS_.stars.ToString();
					break;
				case 2:
					gameObject.name = component.pS_.GetRelation().ToString();
					break;
				case 3:
					gameObject.name = component.pS_.share.ToString();
					break;
				case 4:
					gameObject.name = component.pS_.fanGenre.ToString();
					break;
				case 5:
					gameObject.name = component.pS_.GetAmountVertriebeneSpiele().ToString();
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

	// Token: 0x06001557 RID: 5463 RVA: 0x0000EAD7 File Offset: 0x0000CCD7
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001558 RID: 5464 RVA: 0x0000EAF2 File Offset: 0x0000CCF2
	public void TOGGLE_Tochterfirmen()
	{
		this.uiObjects[0].SetActive(false);
		this.uiObjects[0].SetActive(true);
		this.SetData();
	}

	// Token: 0x06001559 RID: 5465 RVA: 0x000E3C80 File Offset: 0x000E1E80
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

	// Token: 0x04001953 RID: 6483
	private mainScript mS_;

	// Token: 0x04001954 RID: 6484
	private GameObject main_;

	// Token: 0x04001955 RID: 6485
	private GUI_Main guiMain_;

	// Token: 0x04001956 RID: 6486
	private sfxScript sfx_;

	// Token: 0x04001957 RID: 6487
	private textScript tS_;

	// Token: 0x04001958 RID: 6488
	private themes themes_;

	// Token: 0x04001959 RID: 6489
	private Menu_DevGame mDevGame_;

	// Token: 0x0400195A RID: 6490
	private genres genres_;

	// Token: 0x0400195B RID: 6491
	private gameScript gS_;

	// Token: 0x0400195C RID: 6492
	private taskGame task_;

	// Token: 0x0400195D RID: 6493
	public GameObject[] uiPrefabs;

	// Token: 0x0400195E RID: 6494
	public GameObject[] uiObjects;

	// Token: 0x0400195F RID: 6495
	private string searchStringA = "";
}
