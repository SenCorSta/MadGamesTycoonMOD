using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000169 RID: 361
public class Menu_LeitenderTechniker : MonoBehaviour
{
	// Token: 0x06000D7C RID: 3452 RVA: 0x00092210 File Offset: 0x00090410
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000D7D RID: 3453 RVA: 0x00092218 File Offset: 0x00090418
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
		if (!this.menuDevKonsole_)
		{
			this.menuDevKonsole_ = this.guiMain_.uiObjects[318].GetComponent<Menu_Dev_Konsole>();
		}
		if (!this.menuEntwicklungsbericht_)
		{
			this.menuEntwicklungsbericht_ = this.guiMain_.uiObjects[325].GetComponent<Menu_Dev_KonsoleEntwicklungsbericht>();
		}
	}

	// Token: 0x06000D7E RID: 3454 RVA: 0x00092350 File Offset: 0x00090550
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000D7F RID: 3455 RVA: 0x00092388 File Offset: 0x00090588
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

	// Token: 0x06000D80 RID: 3456 RVA: 0x000923D4 File Offset: 0x000905D4
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_LeitenderDesigner>().cS_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000D81 RID: 3457 RVA: 0x00092430 File Offset: 0x00090630
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

	// Token: 0x06000D82 RID: 3458 RVA: 0x00092594 File Offset: 0x00090794
	public void Init(roomScript roomS_)
	{
		this.FindScripts();
		this.rS_ = roomS_;
		this.InitDropdowns();
		this.SetData();
		if (this.rS_.leitenderTechniker == -1)
		{
			this.uiObjects[8].GetComponent<Toggle>().isOn = true;
			return;
		}
		this.uiObjects[8].GetComponent<Toggle>().isOn = false;
	}

	// Token: 0x06000D83 RID: 3459 RVA: 0x000925F0 File Offset: 0x000907F0
	private void SetData()
	{
		if (!this.rS_)
		{
			return;
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("Character");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				characterScript component = array[i].GetComponent<characterScript>();
				if (component && component.roomID == this.rS_.myID)
				{
					string text = component.myName;
					this.searchStringA = this.searchStringA.ToLower();
					text = text.ToLower();
					if ((this.uiObjects[7].GetComponent<InputField>().text.Length <= 0 || text.Contains(this.searchStringA)) && !this.Exists(this.uiObjects[0], component.myID))
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform);
						Item_LeitenderDesigner component2 = gameObject.GetComponent<Item_LeitenderDesigner>();
						component2.characterID = component.myID;
						component2.cS_ = component;
						component2.mS_ = this.mS_;
						component2.tS_ = this.tS_;
						component2.sfx_ = this.sfx_;
						component2.guiMain_ = this.guiMain_;
						component2.rdS_ = this.rdS_;
						if (this.menuDevKonsole_.gameObject.activeSelf && this.menuDevKonsole_.leitenderTechniker == component)
						{
							gameObject.GetComponent<Button>().interactable = false;
						}
						if (this.menuEntwicklungsbericht_.gameObject.activeSelf && this.menuEntwicklungsbericht_.GetLeitenderTechniker() == component)
						{
							gameObject.GetComponent<Button>().interactable = false;
						}
					}
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[6]);
	}

	// Token: 0x06000D84 RID: 3460 RVA: 0x000927D3 File Offset: 0x000909D3
	public void BUTTON_Close()
	{
		this.SetAuto();
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000D85 RID: 3461 RVA: 0x000927F4 File Offset: 0x000909F4
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
				Item_LeitenderDesigner component = gameObject.GetComponent<Item_LeitenderDesigner>();
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

	// Token: 0x06000D86 RID: 3462 RVA: 0x000932AC File Offset: 0x000914AC
	public void BUTTON_Search()
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		if (string.Equals(this.searchStringA.ToLower(), this.uiObjects[7].GetComponent<InputField>().text.ToLower()))
		{
			return;
		}
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.searchStringA = this.uiObjects[7].GetComponent<InputField>().text;
		this.SetData();
	}

	// Token: 0x06000D87 RID: 3463 RVA: 0x0009334C File Offset: 0x0009154C
	public void SetAuto()
	{
		if (this.rS_)
		{
			if (this.uiObjects[8].GetComponent<Toggle>().isOn)
			{
				this.rS_.leitenderTechniker = -1;
				if (this.menuDevKonsole_.gameObject.activeSelf)
				{
					this.menuDevKonsole_.SetLeitenderTechniker(null, false);
				}
				if (this.menuEntwicklungsbericht_.gameObject.activeSelf)
				{
					this.menuEntwicklungsbericht_.SetLeitenderTechniker(null, false);
					return;
				}
			}
			else
			{
				if (this.menuDevKonsole_.gameObject.activeSelf && this.menuDevKonsole_.leitenderTechniker)
				{
					this.rS_.leitenderTechniker = this.menuDevKonsole_.leitenderTechniker.myID;
				}
				if (this.menuEntwicklungsbericht_.gameObject.activeSelf && this.menuEntwicklungsbericht_.GetLeitenderTechniker())
				{
					this.rS_.leitenderTechniker = this.menuEntwicklungsbericht_.GetLeitenderTechniker().myID;
				}
			}
		}
	}

	// Token: 0x06000D88 RID: 3464 RVA: 0x00002715 File Offset: 0x00000915
	public void TOGGLE_Automatik()
	{
	}

	// Token: 0x0400120A RID: 4618
	private mainScript mS_;

	// Token: 0x0400120B RID: 4619
	private GameObject main_;

	// Token: 0x0400120C RID: 4620
	private GUI_Main guiMain_;

	// Token: 0x0400120D RID: 4621
	private sfxScript sfx_;

	// Token: 0x0400120E RID: 4622
	private textScript tS_;

	// Token: 0x0400120F RID: 4623
	private pickCharacterScript pcS_;

	// Token: 0x04001210 RID: 4624
	private roomDataScript rdS_;

	// Token: 0x04001211 RID: 4625
	private Menu_Dev_Konsole menuDevKonsole_;

	// Token: 0x04001212 RID: 4626
	private Menu_Dev_KonsoleEntwicklungsbericht menuEntwicklungsbericht_;

	// Token: 0x04001213 RID: 4627
	public GameObject[] uiPrefabs;

	// Token: 0x04001214 RID: 4628
	public GameObject[] uiObjects;

	// Token: 0x04001215 RID: 4629
	private roomScript rS_;

	// Token: 0x04001216 RID: 4630
	private float updateTimer;

	// Token: 0x04001217 RID: 4631
	private string searchStringA = "";
}
