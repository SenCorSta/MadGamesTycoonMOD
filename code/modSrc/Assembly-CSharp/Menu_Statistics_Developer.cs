using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000229 RID: 553
public class Menu_Statistics_Developer : MonoBehaviour
{
	// Token: 0x06001548 RID: 5448 RVA: 0x000D9A2F File Offset: 0x000D7C2F
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001549 RID: 5449 RVA: 0x000D9A38 File Offset: 0x000D7C38
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

	// Token: 0x0600154A RID: 5450 RVA: 0x000D9B44 File Offset: 0x000D7D44
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x0600154B RID: 5451 RVA: 0x000D9B76 File Offset: 0x000D7D76
	private void OnEnable()
	{
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x0600154C RID: 5452 RVA: 0x000D9B84 File Offset: 0x000D7D84
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x0600154D RID: 5453 RVA: 0x000D9B94 File Offset: 0x000D7D94
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Publisher");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				publisherScript component = array[i].GetComponent<publisherScript>();
				if (component.isUnlocked && component.developer && component.myID != this.mS_.myID && (!this.uiObjects[1].GetComponent<Toggle>().isOn || !component.IsMyTochterfirma()))
				{
					string text = component.GetName();
					this.searchStringA = this.searchStringA.ToLower();
					text = text.ToLower();
					if (this.uiObjects[6].GetComponent<InputField>().text.Length <= 0 || text.Contains(this.searchStringA))
					{
						Item_Stats_Developer component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_Stats_Developer>();
						component2.pS_ = component;
						component2.playerID = -1;
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

	// Token: 0x0600154E RID: 5454 RVA: 0x000D9D10 File Offset: 0x000D7F10
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[5].name);
		this.FindScripts();
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(355));
		list.Add(this.tS_.GetText(687));
		list.Add(this.tS_.GetText(685));
		list.Add(this.tS_.GetText(271));
		list.Add(this.tS_.GetText(1923));
		this.uiObjects[5].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[5].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[5].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x0600154F RID: 5455 RVA: 0x000D9DE4 File Offset: 0x000D7FE4
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
				Item_Stats_Developer component = gameObject.GetComponent<Item_Stats_Developer>();
				if (component.pS_)
				{
					switch (value)
					{
					case 0:
						gameObject.name = component.pS_.GetName();
						break;
					case 1:
						gameObject.name = component.pS_.stars.ToString();
						break;
					case 2:
						gameObject.name = component.pS_.GetFirmenwert().ToString();
						break;
					case 3:
						gameObject.name = component.pS_.GetAmountGames().ToString();
						break;
					case 4:
						if (component.pS_.IsMyTochterfirma())
						{
							gameObject.name = "1";
						}
						else
						{
							gameObject.name = "0";
						}
						break;
					}
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

	// Token: 0x06001550 RID: 5456 RVA: 0x000D9F4F File Offset: 0x000D814F
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001551 RID: 5457 RVA: 0x000D9F6A File Offset: 0x000D816A
	public void TOGGLE_Tochterfirmen()
	{
		this.uiObjects[0].SetActive(false);
		this.uiObjects[0].SetActive(true);
		this.SetData();
	}

	// Token: 0x06001552 RID: 5458 RVA: 0x000D9F90 File Offset: 0x000D8190
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

	// Token: 0x0400193A RID: 6458
	private mainScript mS_;

	// Token: 0x0400193B RID: 6459
	private GameObject main_;

	// Token: 0x0400193C RID: 6460
	private GUI_Main guiMain_;

	// Token: 0x0400193D RID: 6461
	private sfxScript sfx_;

	// Token: 0x0400193E RID: 6462
	private textScript tS_;

	// Token: 0x0400193F RID: 6463
	private themes themes_;

	// Token: 0x04001940 RID: 6464
	private Menu_DevGame mDevGame_;

	// Token: 0x04001941 RID: 6465
	private genres genres_;

	// Token: 0x04001942 RID: 6466
	private gameScript gS_;

	// Token: 0x04001943 RID: 6467
	private taskGame task_;

	// Token: 0x04001944 RID: 6468
	public GameObject[] uiPrefabs;

	// Token: 0x04001945 RID: 6469
	public GameObject[] uiObjects;

	// Token: 0x04001946 RID: 6470
	private string searchStringA = "";
}
