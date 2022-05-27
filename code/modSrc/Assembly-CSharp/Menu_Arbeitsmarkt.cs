using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001DD RID: 477
public class Menu_Arbeitsmarkt : MonoBehaviour
{
	// Token: 0x060011EA RID: 4586 RVA: 0x0000C842 File Offset: 0x0000AA42
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060011EB RID: 4587 RVA: 0x000C8740 File Offset: 0x000C6940
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

	// Token: 0x060011EC RID: 4588 RVA: 0x000C8828 File Offset: 0x000C6A28
	private void Update()
	{
		string text = this.tS_.GetText(198);
		text = text.Replace("<NUM>", this.uiObjects[0].transform.childCount.ToString());
		this.uiObjects[4].GetComponent<Text>().text = text;
		this.uiObjects[7].GetComponent<Text>().text = "(" + this.GetAmountSelected().ToString() + ")";
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060011ED RID: 4589 RVA: 0x000C88E4 File Offset: 0x000C6AE4
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
		this.SetData();
	}

	// Token: 0x060011EE RID: 4590 RVA: 0x000C8930 File Offset: 0x000C6B30
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_Arbeitsmarkt>().charAM_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060011EF RID: 4591 RVA: 0x0000C84A File Offset: 0x0000AA4A
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060011F0 RID: 4592 RVA: 0x000C898C File Offset: 0x000C6B8C
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
		list.Add(this.tS_.GetText(1764));
		list.Add(this.tS_.GetText(1765));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x060011F1 RID: 4593 RVA: 0x0000C852 File Offset: 0x0000AA52
	public void Init()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.SetData();
	}

	// Token: 0x060011F2 RID: 4594 RVA: 0x000C8AF4 File Offset: 0x000C6CF4
	private void SetData()
	{
		int num = 0;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Arbeitsmarkt");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				charArbeitsmarkt component = array[i].GetComponent<charArbeitsmarkt>();
				if (component)
				{
					num++;
					if (!this.Exists(this.uiObjects[0], component.myID))
					{
						Item_Arbeitsmarkt component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_Arbeitsmarkt>();
						component2.characterID = component.myID;
						component2.charAM_ = component;
						component2.mS_ = this.mS_;
						component2.tS_ = this.tS_;
						component2.sfx_ = this.sfx_;
						component2.guiMain_ = this.guiMain_;
					}
				}
			}
		}
		this.DROPDOWN_Sort();
		this.uiObjects[5].GetComponent<Toggle>().isOn = false;
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[6]);
	}

	// Token: 0x060011F3 RID: 4595 RVA: 0x0000C866 File Offset: 0x0000AA66
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060011F4 RID: 4596 RVA: 0x000C8C0C File Offset: 0x000C6E0C
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
				Item_Arbeitsmarkt component = gameObject.GetComponent<Item_Arbeitsmarkt>();
				switch (value)
				{
				case 0:
					gameObject.name = component.charAM_.myName;
					if (component.charAM_.GetBestSkill() == 0)
					{
						component.SetData(this.tS_.GetText(119), component.charAM_.s_gamedesign);
					}
					if (component.charAM_.GetBestSkill() == 1)
					{
						component.SetData(this.tS_.GetText(120), component.charAM_.s_programmieren);
					}
					if (component.charAM_.GetBestSkill() == 2)
					{
						component.SetData(this.tS_.GetText(121), component.charAM_.s_grafik);
					}
					if (component.charAM_.GetBestSkill() == 3)
					{
						component.SetData(this.tS_.GetText(122), component.charAM_.s_sound);
					}
					if (component.charAM_.GetBestSkill() == 4)
					{
						component.SetData(this.tS_.GetText(123), component.charAM_.s_pr);
					}
					if (component.charAM_.GetBestSkill() == 5)
					{
						component.SetData(this.tS_.GetText(124), component.charAM_.s_gametests);
					}
					if (component.charAM_.GetBestSkill() == 6)
					{
						component.SetData(this.tS_.GetText(125), component.charAM_.s_technik);
					}
					if (component.charAM_.GetBestSkill() == 7)
					{
						component.SetData(this.tS_.GetText(126), component.charAM_.s_forschen);
					}
					break;
				case 1:
					gameObject.name = component.charAM_.beruf.ToString();
					if (component.charAM_.GetBestSkill() == 0)
					{
						component.SetData(this.tS_.GetText(119), component.charAM_.s_gamedesign);
					}
					if (component.charAM_.GetBestSkill() == 1)
					{
						component.SetData(this.tS_.GetText(120), component.charAM_.s_programmieren);
					}
					if (component.charAM_.GetBestSkill() == 2)
					{
						component.SetData(this.tS_.GetText(121), component.charAM_.s_grafik);
					}
					if (component.charAM_.GetBestSkill() == 3)
					{
						component.SetData(this.tS_.GetText(122), component.charAM_.s_sound);
					}
					if (component.charAM_.GetBestSkill() == 4)
					{
						component.SetData(this.tS_.GetText(123), component.charAM_.s_pr);
					}
					if (component.charAM_.GetBestSkill() == 5)
					{
						component.SetData(this.tS_.GetText(124), component.charAM_.s_gametests);
					}
					if (component.charAM_.GetBestSkill() == 6)
					{
						component.SetData(this.tS_.GetText(125), component.charAM_.s_technik);
					}
					if (component.charAM_.GetBestSkill() == 7)
					{
						component.SetData(this.tS_.GetText(126), component.charAM_.s_forschen);
					}
					break;
				case 2:
					gameObject.name = component.charAM_.s_gamedesign.ToString();
					component.SetData(this.tS_.GetText(119), component.charAM_.s_gamedesign);
					break;
				case 3:
					gameObject.name = component.charAM_.s_programmieren.ToString();
					component.SetData(this.tS_.GetText(120), component.charAM_.s_programmieren);
					break;
				case 4:
					gameObject.name = component.charAM_.s_grafik.ToString();
					component.SetData(this.tS_.GetText(121), component.charAM_.s_grafik);
					break;
				case 5:
					gameObject.name = component.charAM_.s_sound.ToString();
					component.SetData(this.tS_.GetText(122), component.charAM_.s_sound);
					break;
				case 6:
					gameObject.name = component.charAM_.s_pr.ToString();
					component.SetData(this.tS_.GetText(123), component.charAM_.s_pr);
					break;
				case 7:
					gameObject.name = component.charAM_.s_gametests.ToString();
					component.SetData(this.tS_.GetText(124), component.charAM_.s_gametests);
					break;
				case 8:
					gameObject.name = component.charAM_.s_technik.ToString();
					component.SetData(this.tS_.GetText(125), component.charAM_.s_technik);
					break;
				case 9:
					gameObject.name = component.charAM_.s_forschen.ToString();
					component.SetData(this.tS_.GetText(126), component.charAM_.s_forschen);
					break;
				case 10:
					gameObject.name = component.charAM_.GetGehalt().ToString();
					if (component.charAM_.GetBestSkill() == 0)
					{
						component.SetData(this.tS_.GetText(119), component.charAM_.s_gamedesign);
					}
					if (component.charAM_.GetBestSkill() == 1)
					{
						component.SetData(this.tS_.GetText(120), component.charAM_.s_programmieren);
					}
					if (component.charAM_.GetBestSkill() == 2)
					{
						component.SetData(this.tS_.GetText(121), component.charAM_.s_grafik);
					}
					if (component.charAM_.GetBestSkill() == 3)
					{
						component.SetData(this.tS_.GetText(122), component.charAM_.s_sound);
					}
					if (component.charAM_.GetBestSkill() == 4)
					{
						component.SetData(this.tS_.GetText(123), component.charAM_.s_pr);
					}
					if (component.charAM_.GetBestSkill() == 5)
					{
						component.SetData(this.tS_.GetText(124), component.charAM_.s_gametests);
					}
					if (component.charAM_.GetBestSkill() == 6)
					{
						component.SetData(this.tS_.GetText(125), component.charAM_.s_technik);
					}
					if (component.charAM_.GetBestSkill() == 7)
					{
						component.SetData(this.tS_.GetText(126), component.charAM_.s_forschen);
					}
					break;
				case 11:
					gameObject.name = component.charAM_.GetBestSkillValue().ToString();
					if (component.charAM_.GetBestSkill() == 0)
					{
						component.SetData(this.tS_.GetText(119), component.charAM_.s_gamedesign);
					}
					if (component.charAM_.GetBestSkill() == 1)
					{
						component.SetData(this.tS_.GetText(120), component.charAM_.s_programmieren);
					}
					if (component.charAM_.GetBestSkill() == 2)
					{
						component.SetData(this.tS_.GetText(121), component.charAM_.s_grafik);
					}
					if (component.charAM_.GetBestSkill() == 3)
					{
						component.SetData(this.tS_.GetText(122), component.charAM_.s_sound);
					}
					if (component.charAM_.GetBestSkill() == 4)
					{
						component.SetData(this.tS_.GetText(123), component.charAM_.s_pr);
					}
					if (component.charAM_.GetBestSkill() == 5)
					{
						component.SetData(this.tS_.GetText(124), component.charAM_.s_gametests);
					}
					if (component.charAM_.GetBestSkill() == 6)
					{
						component.SetData(this.tS_.GetText(125), component.charAM_.s_technik);
					}
					if (component.charAM_.GetBestSkill() == 7)
					{
						component.SetData(this.tS_.GetText(126), component.charAM_.s_forschen);
					}
					break;
				case 12:
					gameObject.name = component.charAM_.wochenAmArbeitsmarkt.ToString();
					if (component.charAM_.GetBestSkill() == 0)
					{
						component.SetData(this.tS_.GetText(119), component.charAM_.s_gamedesign);
					}
					if (component.charAM_.GetBestSkill() == 1)
					{
						component.SetData(this.tS_.GetText(120), component.charAM_.s_programmieren);
					}
					if (component.charAM_.GetBestSkill() == 2)
					{
						component.SetData(this.tS_.GetText(121), component.charAM_.s_grafik);
					}
					if (component.charAM_.GetBestSkill() == 3)
					{
						component.SetData(this.tS_.GetText(122), component.charAM_.s_sound);
					}
					if (component.charAM_.GetBestSkill() == 4)
					{
						component.SetData(this.tS_.GetText(123), component.charAM_.s_pr);
					}
					if (component.charAM_.GetBestSkill() == 5)
					{
						component.SetData(this.tS_.GetText(124), component.charAM_.s_gametests);
					}
					if (component.charAM_.GetBestSkill() == 6)
					{
						component.SetData(this.tS_.GetText(125), component.charAM_.s_technik);
					}
					if (component.charAM_.GetBestSkill() == 7)
					{
						component.SetData(this.tS_.GetText(126), component.charAM_.s_forschen);
					}
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

	// Token: 0x060011F5 RID: 4597 RVA: 0x000C96C4 File Offset: 0x000C78C4
	public void TOGGLE_All()
	{
		this.sfx_.PlaySound(12, true);
		bool isOn = this.uiObjects[5].GetComponent<Toggle>().isOn;
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			GameObject gameObject = this.uiObjects[0].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				gameObject.GetComponent<Item_Arbeitsmarkt>().uiObjects[8].GetComponent<Toggle>().isOn = isOn;
			}
		}
	}

	// Token: 0x060011F6 RID: 4598 RVA: 0x000C9748 File Offset: 0x000C7948
	public void BUTTON_Einstellen()
	{
		bool flag = false;
		createCharScript component = this.main_.GetComponent<createCharScript>();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			GameObject gameObject = this.uiObjects[0].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				Item_Arbeitsmarkt component2 = gameObject.GetComponent<Item_Arbeitsmarkt>();
				if (component2 && component2.uiObjects[8].GetComponent<Toggle>().isOn)
				{
					characterScript characterScript = component.CreateCharacter(component2.charAM_.myID, component2.charAM_.male, component2.charAM_.model_body);
					characterScript.model_body = component2.charAM_.model_body;
					characterScript.model_eyes = component2.charAM_.model_eyes;
					characterScript.model_hair = component2.charAM_.model_hair;
					characterScript.model_beard = component2.charAM_.model_beard;
					characterScript.model_skinColor = component2.charAM_.model_skinColor;
					characterScript.model_hairColor = component2.charAM_.model_hairColor;
					characterScript.model_beardColor = component2.charAM_.model_beardColor;
					characterScript.model_HoseColor = component2.charAM_.model_HoseColor;
					characterScript.model_ShirtColor = component2.charAM_.model_ShirtColor;
					characterScript.model_Add1Color = component2.charAM_.model_Add1Color;
					characterScript.gameObject.transform.GetChild(0).GetComponent<characterGFXScript>().Init(true);
					this.mS_.CopyArbeitsmarktCharacterData(component2.charAM_, characterScript);
					this.pcS_.PickFromExternObject(characterScript.gameObject);
					component2.charAM_.RemoveFromArbeitsmarkt(true);
					flag = true;
				}
			}
		}
		if (flag)
		{
			this.sfx_.PlaySound(3, true);
			this.guiMain_.CloseMenu();
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x060011F7 RID: 4599 RVA: 0x000C993C File Offset: 0x000C7B3C
	public int GetAmountSelected()
	{
		this.AnzahlBewerber();
		int num = 0;
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			GameObject gameObject = this.uiObjects[0].transform.GetChild(i).gameObject;
			if (gameObject && gameObject.GetComponent<Item_Arbeitsmarkt>().uiObjects[8].GetComponent<Toggle>().isOn)
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x060011F8 RID: 4600 RVA: 0x000C99B0 File Offset: 0x000C7BB0
	private void AnzahlBewerber()
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		int num7 = 0;
		int num8 = 0;
		float num9 = 0f;
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			GameObject gameObject = this.uiObjects[0].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				num9 += 1f;
				switch (gameObject.GetComponent<Item_Arbeitsmarkt>().charAM_.beruf)
				{
				case 0:
					num++;
					break;
				case 1:
					num2++;
					break;
				case 2:
					num3++;
					break;
				case 3:
					num4++;
					break;
				case 4:
					num5++;
					break;
				case 5:
					num6++;
					break;
				case 6:
					num7++;
					break;
				case 7:
					num8++;
					break;
				}
			}
		}
		if (num > 0)
		{
			this.uiObjects[8].GetComponent<Text>().text = num.ToString();
		}
		else
		{
			this.uiObjects[8].GetComponent<Text>().text = "";
		}
		if (num2 > 0)
		{
			this.uiObjects[9].GetComponent<Text>().text = num2.ToString();
		}
		else
		{
			this.uiObjects[9].GetComponent<Text>().text = "";
		}
		if (num3 > 0)
		{
			this.uiObjects[10].GetComponent<Text>().text = num3.ToString();
		}
		else
		{
			this.uiObjects[10].GetComponent<Text>().text = "";
		}
		if (num4 > 0)
		{
			this.uiObjects[11].GetComponent<Text>().text = num4.ToString();
		}
		else
		{
			this.uiObjects[11].GetComponent<Text>().text = "";
		}
		if (num5 > 0)
		{
			this.uiObjects[12].GetComponent<Text>().text = num5.ToString();
		}
		else
		{
			this.uiObjects[12].GetComponent<Text>().text = "";
		}
		if (num6 > 0)
		{
			this.uiObjects[13].GetComponent<Text>().text = num6.ToString();
		}
		else
		{
			this.uiObjects[13].GetComponent<Text>().text = "";
		}
		if (num7 > 0)
		{
			this.uiObjects[14].GetComponent<Text>().text = num7.ToString();
		}
		else
		{
			this.uiObjects[14].GetComponent<Text>().text = "";
		}
		if (num8 > 0)
		{
			this.uiObjects[15].GetComponent<Text>().text = num8.ToString();
			return;
		}
		this.uiObjects[15].GetComponent<Text>().text = "";
	}

	// Token: 0x04001679 RID: 5753
	private mainScript mS_;

	// Token: 0x0400167A RID: 5754
	private GameObject main_;

	// Token: 0x0400167B RID: 5755
	private GUI_Main guiMain_;

	// Token: 0x0400167C RID: 5756
	private sfxScript sfx_;

	// Token: 0x0400167D RID: 5757
	private textScript tS_;

	// Token: 0x0400167E RID: 5758
	private pickCharacterScript pcS_;

	// Token: 0x0400167F RID: 5759
	private roomDataScript rdS_;

	// Token: 0x04001680 RID: 5760
	public GameObject[] uiPrefabs;

	// Token: 0x04001681 RID: 5761
	public GameObject[] uiObjects;

	// Token: 0x04001682 RID: 5762
	private float updateTimer;
}
