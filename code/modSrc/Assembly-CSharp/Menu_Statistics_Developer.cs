using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000228 RID: 552
public class Menu_Statistics_Developer : MonoBehaviour
{
	// Token: 0x0600152A RID: 5418 RVA: 0x0000E8A7 File Offset: 0x0000CAA7
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600152B RID: 5419 RVA: 0x000E2504 File Offset: 0x000E0704
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

	// Token: 0x0600152C RID: 5420 RVA: 0x0000E8AF File Offset: 0x0000CAAF
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x0600152D RID: 5421 RVA: 0x0000E8E1 File Offset: 0x0000CAE1
	private void OnEnable()
	{
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x0600152E RID: 5422 RVA: 0x0000E8EF File Offset: 0x0000CAEF
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x0600152F RID: 5423 RVA: 0x000E2610 File Offset: 0x000E0810
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Publisher");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				publisherScript component = array[i].GetComponent<publisherScript>();
				if (component.isUnlocked && component.developer && (!this.uiObjects[1].GetComponent<Toggle>().isOn || !component.IsMyTochterfirma()))
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
		if (this.mS_.multiplayer)
		{
			for (int j = 0; j < this.mS_.mpCalls_.playersMP.Count; j++)
			{
				int playerID = this.mS_.mpCalls_.playersMP[j].playerID;
				Item_Stats_Developer component3 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_Stats_Developer>();
				component3.pS_ = null;
				component3.playerID = playerID;
				component3.mS_ = this.mS_;
				component3.tS_ = this.tS_;
				component3.sfx_ = this.sfx_;
				component3.guiMain_ = this.guiMain_;
				component3.genres_ = this.genres_;
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[4]);
	}

	// Token: 0x06001530 RID: 5424 RVA: 0x000E2850 File Offset: 0x000E0A50
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

	// Token: 0x06001531 RID: 5425 RVA: 0x000E2924 File Offset: 0x000E0B24
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

	// Token: 0x06001532 RID: 5426 RVA: 0x0000E8FD File Offset: 0x0000CAFD
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001533 RID: 5427 RVA: 0x0000E918 File Offset: 0x0000CB18
	public void TOGGLE_Tochterfirmen()
	{
		this.uiObjects[0].SetActive(false);
		this.uiObjects[0].SetActive(true);
		this.SetData();
	}

	// Token: 0x06001534 RID: 5428 RVA: 0x000E2A90 File Offset: 0x000E0C90
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

	// Token: 0x04001933 RID: 6451
	private mainScript mS_;

	// Token: 0x04001934 RID: 6452
	private GameObject main_;

	// Token: 0x04001935 RID: 6453
	private GUI_Main guiMain_;

	// Token: 0x04001936 RID: 6454
	private sfxScript sfx_;

	// Token: 0x04001937 RID: 6455
	private textScript tS_;

	// Token: 0x04001938 RID: 6456
	private themes themes_;

	// Token: 0x04001939 RID: 6457
	private Menu_DevGame mDevGame_;

	// Token: 0x0400193A RID: 6458
	private genres genres_;

	// Token: 0x0400193B RID: 6459
	private gameScript gS_;

	// Token: 0x0400193C RID: 6460
	private taskGame task_;

	// Token: 0x0400193D RID: 6461
	public GameObject[] uiPrefabs;

	// Token: 0x0400193E RID: 6462
	public GameObject[] uiObjects;

	// Token: 0x0400193F RID: 6463
	private string searchStringA = "";
}
