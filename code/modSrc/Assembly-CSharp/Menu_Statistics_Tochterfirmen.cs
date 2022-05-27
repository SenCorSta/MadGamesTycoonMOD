using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200022C RID: 556
public class Menu_Statistics_Tochterfirmen : MonoBehaviour
{
	// Token: 0x0600155B RID: 5467 RVA: 0x0000EB29 File Offset: 0x0000CD29
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600155C RID: 5468 RVA: 0x000E3CF4 File Offset: 0x000E1EF4
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

	// Token: 0x0600155D RID: 5469 RVA: 0x0000EB31 File Offset: 0x0000CD31
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x0600155E RID: 5470 RVA: 0x000E3E00 File Offset: 0x000E2000
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_Stats_Tochterfirma>().pS_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600155F RID: 5471 RVA: 0x0000EB63 File Offset: 0x0000CD63
	private void OnEnable()
	{
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x06001560 RID: 5472 RVA: 0x0000EB71 File Offset: 0x0000CD71
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x06001561 RID: 5473 RVA: 0x000E3E5C File Offset: 0x000E205C
	private void SetData()
	{
		long num = 0L;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Publisher");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				publisherScript component = array[i].GetComponent<publisherScript>();
				if (component.isUnlocked && component.IsMyTochterfirma())
				{
					string text = component.GetName();
					this.searchStringA = this.searchStringA.ToLower();
					text = text.ToLower();
					if ((this.uiObjects[6].GetComponent<InputField>().text.Length <= 0 || text.Contains(this.searchStringA)) && !this.Exists(this.uiObjects[0], component.myID))
					{
						Item_Stats_Tochterfirma component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_Stats_Tochterfirma>();
						component2.pS_ = component;
						component2.playerID = -1;
						component2.mS_ = this.mS_;
						component2.tS_ = this.tS_;
						component2.sfx_ = this.sfx_;
						component2.guiMain_ = this.guiMain_;
						component2.genres_ = this.genres_;
						num += component.GetVerwaltungskosten();
					}
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[4]);
		this.uiObjects[7].GetComponent<Text>().text = this.tS_.GetText(1934) + ": <b><color=red>" + this.mS_.GetMoney(num, true) + "</color></b>";
	}

	// Token: 0x06001562 RID: 5474 RVA: 0x000E4008 File Offset: 0x000E2208
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[5].name);
		this.FindScripts();
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(355));
		list.Add(this.tS_.GetText(687));
		list.Add(this.tS_.GetText(685));
		list.Add(this.tS_.GetText(271));
		list.Add(this.tS_.GetText(1944));
		this.uiObjects[5].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[5].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[5].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06001563 RID: 5475 RVA: 0x000E40DC File Offset: 0x000E22DC
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
				Item_Stats_Tochterfirma component = gameObject.GetComponent<Item_Stats_Tochterfirma>();
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
						gameObject.name = (0 - component.pS_.newGameInWeeks).ToString();
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

	// Token: 0x06001564 RID: 5476 RVA: 0x0000EB7F File Offset: 0x0000CD7F
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		if (!this.guiMain_.uiObjects[118].activeSelf)
		{
			this.guiMain_.CloseMenu();
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001565 RID: 5477 RVA: 0x000E4240 File Offset: 0x000E2440
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

	// Token: 0x04001960 RID: 6496
	private mainScript mS_;

	// Token: 0x04001961 RID: 6497
	private GameObject main_;

	// Token: 0x04001962 RID: 6498
	private GUI_Main guiMain_;

	// Token: 0x04001963 RID: 6499
	private sfxScript sfx_;

	// Token: 0x04001964 RID: 6500
	private textScript tS_;

	// Token: 0x04001965 RID: 6501
	private themes themes_;

	// Token: 0x04001966 RID: 6502
	private Menu_DevGame mDevGame_;

	// Token: 0x04001967 RID: 6503
	private genres genres_;

	// Token: 0x04001968 RID: 6504
	private gameScript gS_;

	// Token: 0x04001969 RID: 6505
	private taskGame task_;

	// Token: 0x0400196A RID: 6506
	public GameObject[] uiPrefabs;

	// Token: 0x0400196B RID: 6507
	public GameObject[] uiObjects;

	// Token: 0x0400196C RID: 6508
	private string searchStringA = "";
}
