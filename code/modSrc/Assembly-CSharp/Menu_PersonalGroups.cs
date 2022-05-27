using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Menu_PersonalGroups : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
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

	
	private void Update()
	{
		this.uiObjects[9].GetComponent<Text>().text = this.GetAmountInGroup().ToString();
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
			this.uiObjects[3].GetComponent<Scrollbar>().value = 5f;
		}
	}

	
	private void OnEnable()
	{
		this.Init(false);
		this.Init(true);
	}

	
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

	
	public void Init(bool updateGroup)
	{
		this.FindScripts();
		if (!updateGroup)
		{
			this.InitDropdowns();
		}
		this.SetData(updateGroup);
	}

	
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

	
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	
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

	
	public void DROPDOWN_Sort()
	{
		int value = this.uiObjects[1].GetComponent<Dropdown>().value;
		PlayerPrefs.SetInt(this.uiObjects[1].name, value);
		this.Sort(0);
		this.Sort(4);
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[7]);
		this.guiMain_.KeinEintrag(this.uiObjects[4], this.uiObjects[8]);
	}

	
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

	
	public void BUTTON_Rename()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[282].SetActive(true);
		this.guiMain_.uiObjects[282].GetComponent<Menu_PersonalGroupName>().Init(this.uiObjects[6].GetComponent<Dropdown>().value);
	}

	
	public int GetAmountInGroup()
	{
		return this.uiObjects[4].transform.childCount;
	}

	
	private mainScript mS_;

	
	private GameObject main_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private textScript tS_;

	
	private pickCharacterScript pcS_;

	
	private roomDataScript rdS_;

	
	public GameObject[] uiPrefabs;

	
	public GameObject[] uiObjects;
}
