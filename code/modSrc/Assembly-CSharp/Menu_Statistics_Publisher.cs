using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200022C RID: 556
public class Menu_Statistics_Publisher : MonoBehaviour
{
	// Token: 0x0600156D RID: 5485 RVA: 0x000DAE00 File Offset: 0x000D9000
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600156E RID: 5486 RVA: 0x000DAE08 File Offset: 0x000D9008
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

	// Token: 0x0600156F RID: 5487 RVA: 0x000DAF14 File Offset: 0x000D9114
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x06001570 RID: 5488 RVA: 0x000DAF46 File Offset: 0x000D9146
	private void OnEnable()
	{
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x06001571 RID: 5489 RVA: 0x000DAF54 File Offset: 0x000D9154
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x06001572 RID: 5490 RVA: 0x000DAF64 File Offset: 0x000D9164
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Publisher");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				publisherScript component = array[i].GetComponent<publisherScript>();
				if (component.isUnlocked && component.publisher && !component.onlyMobile && component.myID != this.mS_.myID && (!this.uiObjects[1].GetComponent<Toggle>().isOn || !component.IsMyTochterfirma()))
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

	// Token: 0x06001573 RID: 5491 RVA: 0x000DB0E0 File Offset: 0x000D92E0
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

	// Token: 0x06001574 RID: 5492 RVA: 0x000DB1C8 File Offset: 0x000D93C8
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

	// Token: 0x06001575 RID: 5493 RVA: 0x000DB333 File Offset: 0x000D9533
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001576 RID: 5494 RVA: 0x000DB34E File Offset: 0x000D954E
	public void TOGGLE_Tochterfirmen()
	{
		this.uiObjects[0].SetActive(false);
		this.uiObjects[0].SetActive(true);
		this.SetData();
	}

	// Token: 0x06001577 RID: 5495 RVA: 0x000DB374 File Offset: 0x000D9574
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

	// Token: 0x0400195A RID: 6490
	private mainScript mS_;

	// Token: 0x0400195B RID: 6491
	private GameObject main_;

	// Token: 0x0400195C RID: 6492
	private GUI_Main guiMain_;

	// Token: 0x0400195D RID: 6493
	private sfxScript sfx_;

	// Token: 0x0400195E RID: 6494
	private textScript tS_;

	// Token: 0x0400195F RID: 6495
	private themes themes_;

	// Token: 0x04001960 RID: 6496
	private Menu_DevGame mDevGame_;

	// Token: 0x04001961 RID: 6497
	private genres genres_;

	// Token: 0x04001962 RID: 6498
	private gameScript gS_;

	// Token: 0x04001963 RID: 6499
	private taskGame task_;

	// Token: 0x04001964 RID: 6500
	public GameObject[] uiPrefabs;

	// Token: 0x04001965 RID: 6501
	public GameObject[] uiObjects;

	// Token: 0x04001966 RID: 6502
	private string searchStringA = "";
}
