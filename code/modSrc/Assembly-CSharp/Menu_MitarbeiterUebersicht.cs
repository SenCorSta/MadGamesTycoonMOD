using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001DF RID: 479
public class Menu_MitarbeiterUebersicht : MonoBehaviour
{
	// Token: 0x06001215 RID: 4629 RVA: 0x000BECFC File Offset: 0x000BCEFC
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001216 RID: 4630 RVA: 0x000BED04 File Offset: 0x000BCF04
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
		if (!this.myInputField)
		{
			this.myInputField = this.uiObjects[44].GetComponent<InputField>();
		}
	}

	// Token: 0x06001217 RID: 4631 RVA: 0x000BEE0C File Offset: 0x000BD00C
	private void Update()
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		int num7 = 0;
		int num8 = 0;
		int num9 = 0;
		int num10 = 0;
		int num11 = 0;
		int num12 = 0;
		int num13 = 0;
		for (int i = 0; i < this.mS_.arrayCharacters.Length; i++)
		{
			if (this.mS_.arrayCharacters[i])
			{
				characterScript component = this.mS_.arrayCharacters[i].GetComponent<characterScript>();
				if (component)
				{
					num++;
					num2 += component.GetGehalt();
					if (component.roomS_)
					{
						switch (component.roomS_.typ)
						{
						case 1:
							num4++;
							break;
						case 2:
							num5++;
							break;
						case 3:
							num6++;
							break;
						case 4:
							num7++;
							break;
						case 5:
							num8++;
							break;
						case 6:
							num9++;
							break;
						case 7:
							num10++;
							break;
						case 8:
							num11++;
							break;
						case 10:
							num12++;
							break;
						case 13:
							num13++;
							break;
						}
					}
					else
					{
						num3++;
					}
				}
			}
		}
		this.uiObjects[16].GetComponent<Text>().text = num3.ToString();
		this.uiObjects[6].GetComponent<Text>().text = num4.ToString();
		this.uiObjects[7].GetComponent<Text>().text = num5.ToString();
		this.uiObjects[8].GetComponent<Text>().text = num6.ToString();
		this.uiObjects[9].GetComponent<Text>().text = num7.ToString();
		this.uiObjects[10].GetComponent<Text>().text = num8.ToString();
		this.uiObjects[11].GetComponent<Text>().text = num9.ToString();
		this.uiObjects[12].GetComponent<Text>().text = num10.ToString();
		this.uiObjects[13].GetComponent<Text>().text = num11.ToString();
		this.uiObjects[14].GetComponent<Text>().text = num12.ToString();
		this.uiObjects[15].GetComponent<Text>().text = num13.ToString();
		string text = this.tS_.GetText(184);
		text = text.Replace("<NUM>", num.ToString());
		this.uiObjects[4].GetComponent<Text>().text = text;
		text = this.tS_.GetText(200);
		text = text.Replace("<NUM>", this.mS_.GetMoney((long)num2, true));
		this.uiObjects[17].GetComponent<Text>().text = text;
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.uiObjects[19].GetComponent<Text>().text = "(" + this.GetAmountSelected().ToString() + ")";
		this.MultiplayerUpdate();
	}

	// Token: 0x06001218 RID: 4632 RVA: 0x000BF158 File Offset: 0x000BD358
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
		this.SetData(true);
	}

	// Token: 0x06001219 RID: 4633 RVA: 0x000BF1A8 File Offset: 0x000BD3A8
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_Personal_InRoom>().cS_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600121A RID: 4634 RVA: 0x000BF204 File Offset: 0x000BD404
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x0600121B RID: 4635 RVA: 0x000BF20C File Offset: 0x000BD40C
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

	// Token: 0x0600121C RID: 4636 RVA: 0x000BF36D File Offset: 0x000BD56D
	public void Init()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.SetData(false);
	}

	// Token: 0x0600121D RID: 4637 RVA: 0x000BF384 File Offset: 0x000BD584
	private void SetData(bool check)
	{
		int num = 0;
		for (int i = 0; i < this.mS_.arrayCharactersScripts.Length; i++)
		{
			if (this.mS_.arrayCharactersScripts[i])
			{
				characterScript characterScript = this.mS_.arrayCharactersScripts[i];
				if (characterScript)
				{
					bool flag = false;
					if (this.myInputField.text.Length > 0)
					{
						string myName = characterScript.myName;
						this.searchStringA = this.searchStringA.ToLower();
						if (myName.ToLower().Contains(this.searchStringA))
						{
							flag = true;
						}
					}
					else
					{
						flag = true;
					}
					if (flag)
					{
						num++;
						if (check)
						{
							if (!this.Exists(this.uiObjects[0], characterScript.myID))
							{
								Item_Personal_InRoom component = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], Vector3.zero, Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_Personal_InRoom>();
								component.characterID = characterScript.myID;
								component.cS_ = characterScript;
								component.mS_ = this.mS_;
								component.tS_ = this.tS_;
								component.sfx_ = this.sfx_;
								component.guiMain_ = this.guiMain_;
								component.rdS_ = this.rdS_;
							}
						}
						else
						{
							Item_Personal_InRoom component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], Vector3.zero, Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_Personal_InRoom>();
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
		}
		this.DROPDOWN_Sort();
		this.uiObjects[5].GetComponent<Toggle>().isOn = false;
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[18]);
	}

	// Token: 0x0600121E RID: 4638 RVA: 0x000BF574 File Offset: 0x000BD774
	public void BUTTON_Search()
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		if (string.Equals(this.searchStringA.ToLower(), this.uiObjects[44].GetComponent<InputField>().text.ToLower()))
		{
			return;
		}
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.searchStringA = this.uiObjects[44].GetComponent<InputField>().text;
		this.SetData(false);
	}

	// Token: 0x0600121F RID: 4639 RVA: 0x000BF615 File Offset: 0x000BD815
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001220 RID: 4640 RVA: 0x000BF63C File Offset: 0x000BD83C
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
			this.mS_.SortChildrenByName(this.uiObjects[0]);
			return;
		}
		this.mS_.SortChildrenByFloat(this.uiObjects[0]);
	}

	// Token: 0x06001221 RID: 4641 RVA: 0x000C005C File Offset: 0x000BE25C
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

	// Token: 0x06001222 RID: 4642 RVA: 0x000C00E0 File Offset: 0x000BE2E0
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

	// Token: 0x06001223 RID: 4643 RVA: 0x000C01AC File Offset: 0x000BE3AC
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

	// Token: 0x06001224 RID: 4644 RVA: 0x000C025C File Offset: 0x000BE45C
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

	// Token: 0x06001225 RID: 4645 RVA: 0x000C02D0 File Offset: 0x000BE4D0
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
		for (int i = 0; i < this.mS_.arrayCharacters.Length; i++)
		{
			if (this.mS_.arrayCharacters[i])
			{
				characterScript component = this.mS_.arrayCharacters[i].GetComponent<characterScript>();
				if (component)
				{
					num17 += 1f;
					num += component.s_gamedesign;
					num2 += component.s_programmieren;
					num3 += component.s_grafik;
					num4 += component.s_sound;
					num5 += component.s_pr;
					num6 += component.s_gametests;
					num7 += component.s_technik;
					num8 += component.s_forschen;
					switch (component.beruf)
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
		}
		this.SetBalken(this.uiObjects[20], num / num17);
		this.SetBalken(this.uiObjects[21], num2 / num17);
		this.SetBalken(this.uiObjects[22], num3 / num17);
		this.SetBalken(this.uiObjects[23], num4 / num17);
		this.SetBalken(this.uiObjects[24], num5 / num17);
		this.SetBalken(this.uiObjects[25], num6 / num17);
		this.SetBalken(this.uiObjects[26], num7 / num17);
		this.SetBalken(this.uiObjects[27], num8 / num17);
		if (num9 > 0)
		{
			this.uiObjects[28].GetComponent<Text>().text = num9.ToString();
		}
		else
		{
			this.uiObjects[28].GetComponent<Text>().text = "";
		}
		if (num10 > 0)
		{
			this.uiObjects[29].GetComponent<Text>().text = num10.ToString();
		}
		else
		{
			this.uiObjects[29].GetComponent<Text>().text = "";
		}
		if (num11 > 0)
		{
			this.uiObjects[30].GetComponent<Text>().text = num11.ToString();
		}
		else
		{
			this.uiObjects[30].GetComponent<Text>().text = "";
		}
		if (num12 > 0)
		{
			this.uiObjects[31].GetComponent<Text>().text = num12.ToString();
		}
		else
		{
			this.uiObjects[31].GetComponent<Text>().text = "";
		}
		if (num13 > 0)
		{
			this.uiObjects[32].GetComponent<Text>().text = num13.ToString();
		}
		else
		{
			this.uiObjects[32].GetComponent<Text>().text = "";
		}
		if (num14 > 0)
		{
			this.uiObjects[33].GetComponent<Text>().text = num14.ToString();
		}
		else
		{
			this.uiObjects[33].GetComponent<Text>().text = "";
		}
		if (num15 > 0)
		{
			this.uiObjects[34].GetComponent<Text>().text = num15.ToString();
		}
		else
		{
			this.uiObjects[34].GetComponent<Text>().text = "";
		}
		if (num16 > 0)
		{
			this.uiObjects[35].GetComponent<Text>().text = num16.ToString();
		}
		else
		{
			this.uiObjects[35].GetComponent<Text>().text = "";
		}
		this.uiObjects[36].GetComponent<Text>().text = this.mS_.Round(num, 1).ToString();
		this.uiObjects[37].GetComponent<Text>().text = this.mS_.Round(num2, 1).ToString();
		this.uiObjects[38].GetComponent<Text>().text = this.mS_.Round(num3, 1).ToString();
		this.uiObjects[39].GetComponent<Text>().text = this.mS_.Round(num4, 1).ToString();
		this.uiObjects[40].GetComponent<Text>().text = this.mS_.Round(num5, 1).ToString();
		this.uiObjects[41].GetComponent<Text>().text = this.mS_.Round(num6, 1).ToString();
		this.uiObjects[42].GetComponent<Text>().text = this.mS_.Round(num7, 1).ToString();
		this.uiObjects[43].GetComponent<Text>().text = this.mS_.Round(num8, 1).ToString();
	}

	// Token: 0x06001226 RID: 4646 RVA: 0x000C0810 File Offset: 0x000BEA10
	private void SetBalken(GameObject go, float val)
	{
		go.transform.Find("Value").GetComponent<Text>().text = this.mS_.Round(val, 1).ToString();
		go.transform.Find("Fill").GetComponent<Image>().fillAmount = val * 0.01f;
		go.transform.Find("Fill").GetComponent<Image>().color = this.GetValColor(val);
	}

	// Token: 0x06001227 RID: 4647 RVA: 0x000C0890 File Offset: 0x000BEA90
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

	// Token: 0x0400168C RID: 5772
	private mainScript mS_;

	// Token: 0x0400168D RID: 5773
	private GameObject main_;

	// Token: 0x0400168E RID: 5774
	private GUI_Main guiMain_;

	// Token: 0x0400168F RID: 5775
	private sfxScript sfx_;

	// Token: 0x04001690 RID: 5776
	private textScript tS_;

	// Token: 0x04001691 RID: 5777
	private pickCharacterScript pcS_;

	// Token: 0x04001692 RID: 5778
	private roomDataScript rdS_;

	// Token: 0x04001693 RID: 5779
	public GameObject[] uiPrefabs;

	// Token: 0x04001694 RID: 5780
	public GameObject[] uiObjects;

	// Token: 0x04001695 RID: 5781
	private InputField myInputField;

	// Token: 0x04001696 RID: 5782
	private float updateTimer;

	// Token: 0x04001697 RID: 5783
	private string searchStringA = "";
}
