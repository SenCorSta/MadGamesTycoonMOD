using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001E4 RID: 484
public class Menu_PersonalGroups : MonoBehaviour
{
	// Token: 0x06001239 RID: 4665 RVA: 0x0000CA89 File Offset: 0x0000AC89
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600123A RID: 4666 RVA: 0x000CCA04 File Offset: 0x000CAC04
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
		if (!this.rdS_)
		{
			this.rdS_ = this.main_.GetComponent<roomDataScript>();
		}
		if (!this.pcS_)
		{
			this.pcS_ = this.main_.GetComponent<pickCharacterScript>();
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
	}

	// Token: 0x0600123B RID: 4667 RVA: 0x000CCAEC File Offset: 0x000CACEC
	private void Update()
	{
		this.uiObjects[9].GetComponent<Text>().text = this.GetAmountInGroup().ToString();
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
			this.uiObjects[3].GetComponent<Scrollbar>().value = 5f;
		}
	}

	// Token: 0x0600123C RID: 4668 RVA: 0x0000CA91 File Offset: 0x0000AC91
	private void OnEnable()
	{
		this.Init(false);
		this.Init(true);
	}

	// Token: 0x0600123D RID: 4669 RVA: 0x000CCB64 File Offset: 0x000CAD64
	public void InitDropdowns()
	{
		this.FindScripts();
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(1018));
		list.Add(this.tS_.GetText(119));
		list.Add(this.tS_.GetText(120));
		list.Add(this.tS_.GetText(121));
		list.Add(this.tS_.GetText(122));
		list.Add(this.tS_.GetText(123));
		list.Add(this.tS_.GetText(124));
		list.Add(this.tS_.GetText(125));
		list.Add(this.tS_.GetText(126));
		list.Add(this.tS_.GetText(127));
		list.Add(this.tS_.GetText(190));
		list.Add(this.tS_.GetText(1764));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
		List<string> list2 = new List<string>();
		for (int i = 1; i <= this.mS_.personal_group_names.Length; i++)
		{
			if (this.mS_.personal_group_names[i - 1].Length <= 0)
			{
				list2.Add(this.tS_.GetText(202) + " " + i.ToString());
			}
			else
			{
				list2.Add("(" + i.ToString() + ") " + this.mS_.personal_group_names[i - 1]);
			}
		}
		this.uiObjects[6].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[6].GetComponent<Dropdown>().AddOptions(list2);
	}

	// Token: 0x0600123E RID: 4670 RVA: 0x0000CAA1 File Offset: 0x0000ACA1
	public void Init(bool updateGroup)
	{
		this.FindScripts();
		if (!updateGroup)
		{
			this.InitDropdowns();
		}
		this.SetData(updateGroup);
	}

	// Token: 0x0600123F RID: 4671 RVA: 0x000CCD7C File Offset: 0x000CAF7C
	private void SetData(bool updateGroup)
	{
		for (int i = 0; i < this.mS_.arrayCharactersScripts.Length; i++)
		{
			if (this.mS_.arrayCharactersScripts[i])
			{
				characterScript characterScript = this.mS_.arrayCharactersScripts[i];
				if (characterScript)
				{
					if (!updateGroup)
					{
						if (characterScript.group == -1)
						{
							Item_PersonalGroup component = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_PersonalGroup>();
							component.characterID = characterScript.myID;
							component.cS_ = characterScript;
							component.mS_ = this.mS_;
							component.tS_ = this.tS_;
							component.sfx_ = this.sfx_;
							component.guiMain_ = this.guiMain_;
							component.rdS_ = this.rdS_;
						}
					}
					else if (characterScript.group == this.uiObjects[6].GetComponent<Dropdown>().value + 1)
					{
						Item_PersonalGroup component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[4].transform).GetComponent<Item_PersonalGroup>();
						component2.characterID = characterScript.myID;
						component2.cS_ = characterScript;
						component2.mS_ = this.mS_;
						component2.tS_ = this.tS_;
						component2.sfx_ = this.sfx_;
						component2.guiMain_ = this.guiMain_;
						component2.rdS_ = this.rdS_;
					}
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[7]);
		this.guiMain_.KeinEintrag(this.uiObjects[4], this.uiObjects[8]);
	}

	// Token: 0x06001240 RID: 4672 RVA: 0x0000CAB9 File Offset: 0x0000ACB9
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001241 RID: 4673 RVA: 0x000CCF58 File Offset: 0x000CB158
	public void DROPDOWN_Group()
	{
		int childCount = this.uiObjects[4].transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = this.uiObjects[4].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				gameObject.SetActive(false);
			}
		}
		this.Init(true);
	}

	// Token: 0x06001242 RID: 4674 RVA: 0x000CCFB4 File Offset: 0x000CB1B4
	public void DROPDOWN_Sort()
	{
		int value = this.uiObjects[1].GetComponent<Dropdown>().value;
		PlayerPrefs.SetInt(this.uiObjects[1].name, value);
		this.Sort(0);
		this.Sort(4);
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[7]);
		this.guiMain_.KeinEintrag(this.uiObjects[4], this.uiObjects[8]);
	}

	// Token: 0x06001243 RID: 4675 RVA: 0x000CD02C File Offset: 0x000CB22C
	private void Sort(int element)
	{
		int value = this.uiObjects[1].GetComponent<Dropdown>().value;
		int childCount = this.uiObjects[element].transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = this.uiObjects[element].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				Item_PersonalGroup component = gameObject.GetComponent<Item_PersonalGroup>();
				switch (value)
				{
				case 0:
					gameObject.name = component.cS_.myName;
					switch (component.cS_.GetBestSkill())
					{
					case 0:
						component.SetData(this.tS_.GetText(119), component.cS_.s_gamedesign);
						break;
					case 1:
						component.SetData(this.tS_.GetText(120), component.cS_.s_programmieren);
						break;
					case 2:
						component.SetData(this.tS_.GetText(121), component.cS_.s_grafik);
						break;
					case 3:
						component.SetData(this.tS_.GetText(122), component.cS_.s_sound);
						break;
					case 4:
						component.SetData(this.tS_.GetText(123), component.cS_.s_pr);
						break;
					case 5:
						component.SetData(this.tS_.GetText(124), component.cS_.s_gametests);
						break;
					case 6:
						component.SetData(this.tS_.GetText(125), component.cS_.s_technik);
						break;
					case 7:
						component.SetData(this.tS_.GetText(126), component.cS_.s_forschen);
						break;
					}
					break;
				case 1:
					gameObject.name = component.cS_.beruf.ToString();
					switch (component.cS_.GetBestSkill())
					{
					case 0:
						component.SetData(this.tS_.GetText(119), component.cS_.s_gamedesign);
						break;
					case 1:
						component.SetData(this.tS_.GetText(120), component.cS_.s_programmieren);
						break;
					case 2:
						component.SetData(this.tS_.GetText(121), component.cS_.s_grafik);
						break;
					case 3:
						component.SetData(this.tS_.GetText(122), component.cS_.s_sound);
						break;
					case 4:
						component.SetData(this.tS_.GetText(123), component.cS_.s_pr);
						break;
					case 5:
						component.SetData(this.tS_.GetText(124), component.cS_.s_gametests);
						break;
					case 6:
						component.SetData(this.tS_.GetText(125), component.cS_.s_technik);
						break;
					case 7:
						component.SetData(this.tS_.GetText(126), component.cS_.s_forschen);
						break;
					}
					break;
				case 2:
					gameObject.name = component.cS_.s_gamedesign.ToString();
					component.SetData(this.tS_.GetText(119), component.cS_.s_gamedesign);
					break;
				case 3:
					gameObject.name = component.cS_.s_programmieren.ToString();
					component.SetData(this.tS_.GetText(120), component.cS_.s_programmieren);
					break;
				case 4:
					gameObject.name = component.cS_.s_grafik.ToString();
					component.SetData(this.tS_.GetText(121), component.cS_.s_grafik);
					break;
				case 5:
					gameObject.name = component.cS_.s_sound.ToString();
					component.SetData(this.tS_.GetText(122), component.cS_.s_sound);
					break;
				case 6:
					gameObject.name = component.cS_.s_pr.ToString();
					component.SetData(this.tS_.GetText(123), component.cS_.s_pr);
					break;
				case 7:
					gameObject.name = component.cS_.s_gametests.ToString();
					component.SetData(this.tS_.GetText(124), component.cS_.s_gametests);
					break;
				case 8:
					gameObject.name = component.cS_.s_technik.ToString();
					component.SetData(this.tS_.GetText(125), component.cS_.s_technik);
					break;
				case 9:
					gameObject.name = component.cS_.s_forschen.ToString();
					component.SetData(this.tS_.GetText(126), component.cS_.s_forschen);
					break;
				case 10:
					gameObject.name = component.cS_.GetGehalt().ToString();
					switch (component.cS_.GetBestSkill())
					{
					case 0:
						component.SetData(this.tS_.GetText(119), component.cS_.s_gamedesign);
						break;
					case 1:
						component.SetData(this.tS_.GetText(120), component.cS_.s_programmieren);
						break;
					case 2:
						component.SetData(this.tS_.GetText(121), component.cS_.s_grafik);
						break;
					case 3:
						component.SetData(this.tS_.GetText(122), component.cS_.s_sound);
						break;
					case 4:
						component.SetData(this.tS_.GetText(123), component.cS_.s_pr);
						break;
					case 5:
						component.SetData(this.tS_.GetText(124), component.cS_.s_gametests);
						break;
					case 6:
						component.SetData(this.tS_.GetText(125), component.cS_.s_technik);
						break;
					case 7:
						component.SetData(this.tS_.GetText(126), component.cS_.s_forschen);
						break;
					}
					break;
				case 11:
					gameObject.name = component.cS_.s_motivation.ToString();
					switch (component.cS_.GetBestSkill())
					{
					case 0:
						component.SetData(this.tS_.GetText(119), component.cS_.s_gamedesign);
						break;
					case 1:
						component.SetData(this.tS_.GetText(120), component.cS_.s_programmieren);
						break;
					case 2:
						component.SetData(this.tS_.GetText(121), component.cS_.s_grafik);
						break;
					case 3:
						component.SetData(this.tS_.GetText(122), component.cS_.s_sound);
						break;
					case 4:
						component.SetData(this.tS_.GetText(123), component.cS_.s_pr);
						break;
					case 5:
						component.SetData(this.tS_.GetText(124), component.cS_.s_gametests);
						break;
					case 6:
						component.SetData(this.tS_.GetText(125), component.cS_.s_technik);
						break;
					case 7:
						component.SetData(this.tS_.GetText(126), component.cS_.s_forschen);
						break;
					}
					break;
				case 12:
					gameObject.name = component.cS_.GetBestSkillValue().ToString();
					switch (component.cS_.GetBestSkill())
					{
					case 0:
						component.SetData(this.tS_.GetText(119), component.cS_.s_gamedesign);
						break;
					case 1:
						component.SetData(this.tS_.GetText(120), component.cS_.s_programmieren);
						break;
					case 2:
						component.SetData(this.tS_.GetText(121), component.cS_.s_grafik);
						break;
					case 3:
						component.SetData(this.tS_.GetText(122), component.cS_.s_sound);
						break;
					case 4:
						component.SetData(this.tS_.GetText(123), component.cS_.s_pr);
						break;
					case 5:
						component.SetData(this.tS_.GetText(124), component.cS_.s_gametests);
						break;
					case 6:
						component.SetData(this.tS_.GetText(125), component.cS_.s_technik);
						break;
					case 7:
						component.SetData(this.tS_.GetText(126), component.cS_.s_forschen);
						break;
					}
					break;
				}
			}
		}
		if (value == 0)
		{
			this.mS_.SortChildrenByName(this.uiObjects[element]);
			return;
		}
		this.mS_.SortChildrenByFloat(this.uiObjects[element]);
	}

	// Token: 0x06001244 RID: 4676 RVA: 0x000CDA38 File Offset: 0x000CBC38
	public void BUTTON_Select()
	{
		if (this.uiObjects[4].transform.childCount <= 0)
		{
			return;
		}
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		for (int i = 0; i < this.uiObjects[4].transform.childCount; i++)
		{
			GameObject gameObject = this.uiObjects[4].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				Item_PersonalGroup component = gameObject.GetComponent<Item_PersonalGroup>();
				if (component)
				{
					this.pcS_.PickFromExternObject(component.cS_.gameObject);
				}
			}
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001245 RID: 4677 RVA: 0x000CDAE4 File Offset: 0x000CBCE4
	public void BUTTON_Rename()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[282].SetActive(true);
		this.guiMain_.uiObjects[282].GetComponent<Menu_PersonalGroupName>().Init(this.uiObjects[6].GetComponent<Dropdown>().value);
	}

	// Token: 0x06001246 RID: 4678 RVA: 0x0000CADF File Offset: 0x0000ACDF
	public int GetAmountInGroup()
	{
		return this.uiObjects[4].transform.childCount;
	}

	// Token: 0x040016BC RID: 5820
	private mainScript mS_;

	// Token: 0x040016BD RID: 5821
	private GameObject main_;

	// Token: 0x040016BE RID: 5822
	private GUI_Main guiMain_;

	// Token: 0x040016BF RID: 5823
	private sfxScript sfx_;

	// Token: 0x040016C0 RID: 5824
	private textScript tS_;

	// Token: 0x040016C1 RID: 5825
	private pickCharacterScript pcS_;

	// Token: 0x040016C2 RID: 5826
	private roomDataScript rdS_;

	// Token: 0x040016C3 RID: 5827
	public GameObject[] uiPrefabs;

	// Token: 0x040016C4 RID: 5828
	public GameObject[] uiObjects;
}
