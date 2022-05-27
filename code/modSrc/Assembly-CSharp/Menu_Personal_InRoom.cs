using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001E8 RID: 488
public class Menu_Personal_InRoom : MonoBehaviour
{
	// Token: 0x06001269 RID: 4713 RVA: 0x0000CC09 File Offset: 0x0000AE09
	private void Start()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x0600126A RID: 4714 RVA: 0x000CED48 File Offset: 0x000CCF48
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

	// Token: 0x0600126B RID: 4715 RVA: 0x000CEE30 File Offset: 0x000CD030
	private void Update()
	{
		if (!this.rS_)
		{
			return;
		}
		string text = this.tS_.GetText(184);
		text = text.Replace("<NUM>", this.uiObjects[0].transform.childCount.ToString() + " / " + this.rS_.GetArbeitsplaetze().ToString());
		this.uiObjects[4].GetComponent<Text>().text = text;
		this.uiObjects[9].GetComponent<Text>().text = this.rS_.AnzahlArbeitsplaetzeBisUberfuellt().ToString();
		this.uiObjects[8].GetComponent<Text>().text = "(" + this.GetAmountSelected().ToString() + ")";
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x0600126C RID: 4716 RVA: 0x000CEF38 File Offset: 0x000CD138
	public void InitDropdowns()
	{
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
	}

	// Token: 0x0600126D RID: 4717 RVA: 0x000CF09C File Offset: 0x000CD29C
	public void Init(int roomID_)
	{
		this.FindScripts();
		this.roomID = roomID_;
		int num = 0;
		GameObject gameObject = GameObject.Find("Room_" + roomID_.ToString());
		if (gameObject)
		{
			this.rS_ = gameObject.GetComponent<roomScript>();
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("Character");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				characterScript component = array[i].GetComponent<characterScript>();
				if (component && component.roomID == this.roomID)
				{
					num++;
					Item_Personal_InRoom component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_Personal_InRoom>();
					component2.characterID = component.myID;
					component2.cS_ = component;
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.rdS_ = this.rdS_;
				}
			}
		}
		this.DROPDOWN_Sort();
		if (this.rS_)
		{
			string text = this.tS_.GetText(1301);
			text = text.Replace("<NAME>", this.rdS_.GetName(this.rS_.typ));
			switch (this.rS_.typ)
			{
			case 1:
				text = text.Replace("<TEXT>", string.Concat(new string[]
				{
					this.tS_.GetText(137),
					", ",
					this.tS_.GetText(138),
					", ",
					this.tS_.GetText(139),
					", ",
					this.tS_.GetText(140)
				}));
				break;
			case 2:
				text = text.Replace("<TEXT>", this.tS_.GetText(144));
				break;
			case 3:
				text = text.Replace("<TEXT>", this.tS_.GetText(142));
				break;
			case 4:
				text = text.Replace("<TEXT>", this.tS_.GetText(139));
				break;
			case 5:
				text = text.Replace("<TEXT>", this.tS_.GetText(140));
				break;
			case 6:
				text = text.Replace("<TEXT>", this.tS_.GetText(141));
				break;
			case 7:
				text = text.Replace("<TEXT>", this.tS_.GetText(141));
				break;
			case 8:
				text = text.Replace("<TEXT>", this.tS_.GetText(143));
				break;
			case 10:
				text = text.Replace("<TEXT>", this.tS_.GetText(138));
				break;
			case 13:
				text = this.tS_.GetText(31);
				break;
			case 17:
				text = text.Replace("<TEXT>", this.tS_.GetText(143));
				break;
			}
			this.uiObjects[7].GetComponent<Text>().text = text;
		}
		this.uiObjects[5].GetComponent<Toggle>().isOn = false;
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[6]);
	}

	// Token: 0x0600126E RID: 4718 RVA: 0x0000CC17 File Offset: 0x0000AE17
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600126F RID: 4719 RVA: 0x000CF470 File Offset: 0x000CD670
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
				Item_Personal_InRoom component = gameObject.GetComponent<Item_Personal_InRoom>();
				switch (value)
				{
				case 0:
					gameObject.name = component.cS_.myName;
					if (component.cS_.GetBestSkill() == 0)
					{
						component.SetData(this.tS_.GetText(119), component.cS_.s_gamedesign);
					}
					if (component.cS_.GetBestSkill() == 1)
					{
						component.SetData(this.tS_.GetText(120), component.cS_.s_programmieren);
					}
					if (component.cS_.GetBestSkill() == 2)
					{
						component.SetData(this.tS_.GetText(121), component.cS_.s_grafik);
					}
					if (component.cS_.GetBestSkill() == 3)
					{
						component.SetData(this.tS_.GetText(122), component.cS_.s_sound);
					}
					if (component.cS_.GetBestSkill() == 4)
					{
						component.SetData(this.tS_.GetText(123), component.cS_.s_pr);
					}
					if (component.cS_.GetBestSkill() == 5)
					{
						component.SetData(this.tS_.GetText(124), component.cS_.s_gametests);
					}
					if (component.cS_.GetBestSkill() == 6)
					{
						component.SetData(this.tS_.GetText(125), component.cS_.s_technik);
					}
					if (component.cS_.GetBestSkill() == 7)
					{
						component.SetData(this.tS_.GetText(126), component.cS_.s_forschen);
					}
					break;
				case 1:
					gameObject.name = component.cS_.beruf.ToString();
					if (component.cS_.GetBestSkill() == 0)
					{
						component.SetData(this.tS_.GetText(119), component.cS_.s_gamedesign);
					}
					if (component.cS_.GetBestSkill() == 1)
					{
						component.SetData(this.tS_.GetText(120), component.cS_.s_programmieren);
					}
					if (component.cS_.GetBestSkill() == 2)
					{
						component.SetData(this.tS_.GetText(121), component.cS_.s_grafik);
					}
					if (component.cS_.GetBestSkill() == 3)
					{
						component.SetData(this.tS_.GetText(122), component.cS_.s_sound);
					}
					if (component.cS_.GetBestSkill() == 4)
					{
						component.SetData(this.tS_.GetText(123), component.cS_.s_pr);
					}
					if (component.cS_.GetBestSkill() == 5)
					{
						component.SetData(this.tS_.GetText(124), component.cS_.s_gametests);
					}
					if (component.cS_.GetBestSkill() == 6)
					{
						component.SetData(this.tS_.GetText(125), component.cS_.s_technik);
					}
					if (component.cS_.GetBestSkill() == 7)
					{
						component.SetData(this.tS_.GetText(126), component.cS_.s_forschen);
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
					if (component.cS_.GetBestSkill() == 0)
					{
						component.SetData(this.tS_.GetText(119), component.cS_.s_gamedesign);
					}
					if (component.cS_.GetBestSkill() == 1)
					{
						component.SetData(this.tS_.GetText(120), component.cS_.s_programmieren);
					}
					if (component.cS_.GetBestSkill() == 2)
					{
						component.SetData(this.tS_.GetText(121), component.cS_.s_grafik);
					}
					if (component.cS_.GetBestSkill() == 3)
					{
						component.SetData(this.tS_.GetText(122), component.cS_.s_sound);
					}
					if (component.cS_.GetBestSkill() == 4)
					{
						component.SetData(this.tS_.GetText(123), component.cS_.s_pr);
					}
					if (component.cS_.GetBestSkill() == 5)
					{
						component.SetData(this.tS_.GetText(124), component.cS_.s_gametests);
					}
					if (component.cS_.GetBestSkill() == 6)
					{
						component.SetData(this.tS_.GetText(125), component.cS_.s_technik);
					}
					if (component.cS_.GetBestSkill() == 7)
					{
						component.SetData(this.tS_.GetText(126), component.cS_.s_forschen);
					}
					break;
				case 11:
					gameObject.name = component.cS_.s_motivation.ToString();
					if (component.cS_.GetBestSkill() == 0)
					{
						component.SetData(this.tS_.GetText(119), component.cS_.s_gamedesign);
					}
					if (component.cS_.GetBestSkill() == 1)
					{
						component.SetData(this.tS_.GetText(120), component.cS_.s_programmieren);
					}
					if (component.cS_.GetBestSkill() == 2)
					{
						component.SetData(this.tS_.GetText(121), component.cS_.s_grafik);
					}
					if (component.cS_.GetBestSkill() == 3)
					{
						component.SetData(this.tS_.GetText(122), component.cS_.s_sound);
					}
					if (component.cS_.GetBestSkill() == 4)
					{
						component.SetData(this.tS_.GetText(123), component.cS_.s_pr);
					}
					if (component.cS_.GetBestSkill() == 5)
					{
						component.SetData(this.tS_.GetText(124), component.cS_.s_gametests);
					}
					if (component.cS_.GetBestSkill() == 6)
					{
						component.SetData(this.tS_.GetText(125), component.cS_.s_technik);
					}
					if (component.cS_.GetBestSkill() == 7)
					{
						component.SetData(this.tS_.GetText(126), component.cS_.s_forschen);
					}
					break;
				case 12:
					gameObject.name = component.cS_.GetBestSkillValue().ToString();
					if (component.cS_.GetBestSkill() == 0)
					{
						component.SetData(this.tS_.GetText(119), component.cS_.s_gamedesign);
					}
					if (component.cS_.GetBestSkill() == 1)
					{
						component.SetData(this.tS_.GetText(120), component.cS_.s_programmieren);
					}
					if (component.cS_.GetBestSkill() == 2)
					{
						component.SetData(this.tS_.GetText(121), component.cS_.s_grafik);
					}
					if (component.cS_.GetBestSkill() == 3)
					{
						component.SetData(this.tS_.GetText(122), component.cS_.s_sound);
					}
					if (component.cS_.GetBestSkill() == 4)
					{
						component.SetData(this.tS_.GetText(123), component.cS_.s_pr);
					}
					if (component.cS_.GetBestSkill() == 5)
					{
						component.SetData(this.tS_.GetText(124), component.cS_.s_gametests);
					}
					if (component.cS_.GetBestSkill() == 6)
					{
						component.SetData(this.tS_.GetText(125), component.cS_.s_technik);
					}
					if (component.cS_.GetBestSkill() == 7)
					{
						component.SetData(this.tS_.GetText(126), component.cS_.s_forschen);
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

	// Token: 0x06001270 RID: 4720 RVA: 0x000CFF28 File Offset: 0x000CE128
	public void TOGGLE_All()
	{
		this.sfx_.PlaySound(12, true);
		bool isOn = this.uiObjects[5].GetComponent<Toggle>().isOn;
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			GameObject gameObject = this.uiObjects[0].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				gameObject.GetComponent<Item_Personal_InRoom>().uiObjects[8].GetComponent<Toggle>().isOn = isOn;
			}
		}
	}

	// Token: 0x06001271 RID: 4721 RVA: 0x000CFFAC File Offset: 0x000CE1AC
	public void BUTTON_Entlassen()
	{
		bool flag = false;
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			GameObject gameObject = this.uiObjects[0].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				Item_Personal_InRoom component = gameObject.GetComponent<Item_Personal_InRoom>();
				if (component && component.cS_.myID != 1 && component.uiObjects[8].GetComponent<Toggle>().isOn)
				{
					this.guiMain_.uiObjects[27].GetComponent<Menu_PersonalEntlassen>().AddCharacter(component.cS_);
					flag = true;
				}
			}
		}
		if (flag)
		{
			this.sfx_.PlaySound(3, true);
			this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[27]);
		}
	}

	// Token: 0x06001272 RID: 4722 RVA: 0x000D0078 File Offset: 0x000CE278
	public void BUTTON_Select()
	{
		bool flag = false;
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			GameObject gameObject = this.uiObjects[0].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				Item_Personal_InRoom component = gameObject.GetComponent<Item_Personal_InRoom>();
				if (component && component.uiObjects[8].GetComponent<Toggle>().isOn)
				{
					this.pcS_.PickFromExternObject(component.cS_.gameObject);
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

	// Token: 0x06001273 RID: 4723 RVA: 0x000D0128 File Offset: 0x000CE328
	public int GetAmountSelected()
	{
		this.DrawBalkenDurchschnitt();
		int num = 0;
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			GameObject gameObject = this.uiObjects[0].transform.GetChild(i).gameObject;
			if (gameObject && gameObject.GetComponent<Item_Personal_InRoom>().uiObjects[8].GetComponent<Toggle>().isOn)
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06001274 RID: 4724 RVA: 0x000D019C File Offset: 0x000CE39C
	private void DrawBalkenDurchschnitt()
	{
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = 0f;
		float num6 = 0f;
		float num7 = 0f;
		float num8 = 0f;
		int num9 = 0;
		int num10 = 0;
		int num11 = 0;
		int num12 = 0;
		int num13 = 0;
		int num14 = 0;
		int num15 = 0;
		int num16 = 0;
		float num17 = 0f;
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			GameObject gameObject = this.uiObjects[0].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				num17 += 1f;
				characterScript cS_ = gameObject.GetComponent<Item_Personal_InRoom>().cS_;
				num += cS_.s_gamedesign;
				num2 += cS_.s_programmieren;
				num3 += cS_.s_grafik;
				num4 += cS_.s_sound;
				num5 += cS_.s_pr;
				num6 += cS_.s_gametests;
				num7 += cS_.s_technik;
				num8 += cS_.s_forschen;
				switch (cS_.beruf)
				{
				case 0:
					num9++;
					break;
				case 1:
					num10++;
					break;
				case 2:
					num11++;
					break;
				case 3:
					num12++;
					break;
				case 4:
					num13++;
					break;
				case 5:
					num14++;
					break;
				case 6:
					num15++;
					break;
				case 7:
					num16++;
					break;
				}
			}
		}
		this.SetBalken(this.uiObjects[10], num / num17);
		this.SetBalken(this.uiObjects[11], num2 / num17);
		this.SetBalken(this.uiObjects[12], num3 / num17);
		this.SetBalken(this.uiObjects[13], num4 / num17);
		this.SetBalken(this.uiObjects[14], num5 / num17);
		this.SetBalken(this.uiObjects[15], num6 / num17);
		this.SetBalken(this.uiObjects[16], num7 / num17);
		this.SetBalken(this.uiObjects[17], num8 / num17);
		if (num9 > 0)
		{
			this.uiObjects[18].GetComponent<Text>().text = num9.ToString();
		}
		else
		{
			this.uiObjects[18].GetComponent<Text>().text = "";
		}
		if (num10 > 0)
		{
			this.uiObjects[19].GetComponent<Text>().text = num10.ToString();
		}
		else
		{
			this.uiObjects[19].GetComponent<Text>().text = "";
		}
		if (num11 > 0)
		{
			this.uiObjects[20].GetComponent<Text>().text = num11.ToString();
		}
		else
		{
			this.uiObjects[20].GetComponent<Text>().text = "";
		}
		if (num12 > 0)
		{
			this.uiObjects[21].GetComponent<Text>().text = num12.ToString();
		}
		else
		{
			this.uiObjects[21].GetComponent<Text>().text = "";
		}
		if (num13 > 0)
		{
			this.uiObjects[22].GetComponent<Text>().text = num13.ToString();
		}
		else
		{
			this.uiObjects[22].GetComponent<Text>().text = "";
		}
		if (num14 > 0)
		{
			this.uiObjects[23].GetComponent<Text>().text = num14.ToString();
		}
		else
		{
			this.uiObjects[23].GetComponent<Text>().text = "";
		}
		if (num15 > 0)
		{
			this.uiObjects[24].GetComponent<Text>().text = num15.ToString();
		}
		else
		{
			this.uiObjects[24].GetComponent<Text>().text = "";
		}
		if (num16 > 0)
		{
			this.uiObjects[25].GetComponent<Text>().text = num16.ToString();
		}
		else
		{
			this.uiObjects[25].GetComponent<Text>().text = "";
		}
		this.uiObjects[26].GetComponent<Text>().text = this.mS_.Round(num, 1).ToString();
		this.uiObjects[27].GetComponent<Text>().text = this.mS_.Round(num2, 1).ToString();
		this.uiObjects[28].GetComponent<Text>().text = this.mS_.Round(num3, 1).ToString();
		this.uiObjects[29].GetComponent<Text>().text = this.mS_.Round(num4, 1).ToString();
		this.uiObjects[30].GetComponent<Text>().text = this.mS_.Round(num5, 1).ToString();
		this.uiObjects[31].GetComponent<Text>().text = this.mS_.Round(num6, 1).ToString();
		this.uiObjects[32].GetComponent<Text>().text = this.mS_.Round(num7, 1).ToString();
		this.uiObjects[33].GetComponent<Text>().text = this.mS_.Round(num8, 1).ToString();
	}

	// Token: 0x06001275 RID: 4725 RVA: 0x000D06DC File Offset: 0x000CE8DC
	private void SetBalken(GameObject go, float val)
	{
		go.transform.Find("Value").GetComponent<Text>().text = this.mS_.Round(val, 1).ToString();
		go.transform.Find("Fill").GetComponent<Image>().fillAmount = val * 0.01f;
		go.transform.Find("Fill").GetComponent<Image>().color = this.GetValColor(val);
	}

	// Token: 0x06001276 RID: 4726 RVA: 0x000D075C File Offset: 0x000CE95C
	private Color GetValColor(float val)
	{
		if (val < 30f)
		{
			return this.guiMain_.colorsBalken[0];
		}
		if (val >= 30f && val < 70f)
		{
			return this.guiMain_.colorsBalken[1];
		}
		if (val >= 70f)
		{
			return this.guiMain_.colorsBalken[2];
		}
		return this.guiMain_.colorsBalken[0];
	}

	// Token: 0x040016E0 RID: 5856
	private mainScript mS_;

	// Token: 0x040016E1 RID: 5857
	private GameObject main_;

	// Token: 0x040016E2 RID: 5858
	private GUI_Main guiMain_;

	// Token: 0x040016E3 RID: 5859
	private sfxScript sfx_;

	// Token: 0x040016E4 RID: 5860
	private textScript tS_;

	// Token: 0x040016E5 RID: 5861
	private pickCharacterScript pcS_;

	// Token: 0x040016E6 RID: 5862
	private roomDataScript rdS_;

	// Token: 0x040016E7 RID: 5863
	public int roomID = -1;

	// Token: 0x040016E8 RID: 5864
	private roomScript rS_;

	// Token: 0x040016E9 RID: 5865
	public GameObject[] uiPrefabs;

	// Token: 0x040016EA RID: 5866
	public GameObject[] uiObjects;
}
