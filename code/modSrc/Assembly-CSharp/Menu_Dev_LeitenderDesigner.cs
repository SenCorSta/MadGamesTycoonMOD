using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000138 RID: 312
public class Menu_Dev_LeitenderDesigner : MonoBehaviour
{
	// Token: 0x06000B36 RID: 2870 RVA: 0x0007930A File Offset: 0x0007750A
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000B37 RID: 2871 RVA: 0x00079314 File Offset: 0x00077514
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
		if (!this.menuDevGame_)
		{
			this.menuDevGame_ = this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>();
		}
		if (!this.menuEntwicklungsbericht_)
		{
			this.menuEntwicklungsbericht_ = this.guiMain_.uiObjects[73].GetComponent<Menu_Dev_GameEntwicklungsbericht>();
		}
		if (!this.mDevAddon_)
		{
			this.mDevAddon_ = this.guiMain_.uiObjects[193].GetComponent<Menu_Dev_AddonDo>();
		}
		if (!this.mDevMMOAddon_)
		{
			this.mDevMMOAddon_ = this.guiMain_.uiObjects[247].GetComponent<Menu_Dev_MMOAddon>();
		}
	}

	// Token: 0x06000B38 RID: 2872 RVA: 0x00079498 File Offset: 0x00077698
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000B39 RID: 2873 RVA: 0x000794D0 File Offset: 0x000776D0
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

	// Token: 0x06000B3A RID: 2874 RVA: 0x0007951C File Offset: 0x0007771C
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

	// Token: 0x06000B3B RID: 2875 RVA: 0x00079578 File Offset: 0x00077778
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

	// Token: 0x06000B3C RID: 2876 RVA: 0x000796DC File Offset: 0x000778DC
	public void Init(roomScript roomS_)
	{
		this.FindScripts();
		this.rS_ = roomS_;
		this.InitDropdowns();
		this.SetData();
		if (this.rS_.leitenderGamedesigner == -1)
		{
			this.uiObjects[8].GetComponent<Toggle>().isOn = true;
			return;
		}
		this.uiObjects[8].GetComponent<Toggle>().isOn = false;
	}

	// Token: 0x06000B3D RID: 2877 RVA: 0x00079738 File Offset: 0x00077938
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
						if (this.menuDevGame_.gameObject.activeSelf && this.menuDevGame_.g_leitenderDesigner == component)
						{
							gameObject.GetComponent<Button>().interactable = false;
						}
						if (this.mDevAddon_.gameObject.activeSelf && this.mDevAddon_.g_leitenderDesigner == component)
						{
							gameObject.GetComponent<Button>().interactable = false;
						}
						if (this.menuEntwicklungsbericht_.gameObject.activeSelf && this.menuEntwicklungsbericht_.GetLeitenderEntwickler() == component)
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

	// Token: 0x06000B3E RID: 2878 RVA: 0x0007994D File Offset: 0x00077B4D
	public void BUTTON_Close()
	{
		this.SetAuto();
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000B3F RID: 2879 RVA: 0x00079970 File Offset: 0x00077B70
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

	// Token: 0x06000B40 RID: 2880 RVA: 0x0007A428 File Offset: 0x00078628
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

	// Token: 0x06000B41 RID: 2881 RVA: 0x0007A4C8 File Offset: 0x000786C8
	public void SetAuto()
	{
		if (this.rS_)
		{
			if (this.uiObjects[8].GetComponent<Toggle>().isOn)
			{
				this.rS_.leitenderGamedesigner = -1;
				if (this.menuDevGame_.gameObject.activeSelf)
				{
					this.menuDevGame_.SetLeitenderDesigner(null, false);
				}
				if (this.mDevAddon_.gameObject.activeSelf)
				{
					this.mDevAddon_.SetLeitenderDesigner(null, false);
				}
				if (this.menuEntwicklungsbericht_.gameObject.activeSelf)
				{
					this.menuEntwicklungsbericht_.SetLeitenderDesigner(null, false);
					return;
				}
			}
			else
			{
				if (this.menuDevGame_.gameObject.activeSelf && this.menuDevGame_.g_leitenderDesigner)
				{
					this.rS_.leitenderGamedesigner = this.menuDevGame_.g_leitenderDesigner.myID;
				}
				if (this.mDevAddon_.gameObject.activeSelf && this.menuDevGame_.g_leitenderDesigner)
				{
					this.rS_.leitenderGamedesigner = this.menuDevGame_.g_leitenderDesigner.myID;
				}
				if (this.menuEntwicklungsbericht_.gameObject.activeSelf && this.menuEntwicklungsbericht_.GetLeitenderEntwickler())
				{
					this.rS_.leitenderGamedesigner = this.menuEntwicklungsbericht_.GetLeitenderEntwickler().myID;
				}
			}
		}
	}

	// Token: 0x06000B42 RID: 2882 RVA: 0x00002715 File Offset: 0x00000915
	public void TOGGLE_Automatik()
	{
	}

	// Token: 0x04000F89 RID: 3977
	private mainScript mS_;

	// Token: 0x04000F8A RID: 3978
	private GameObject main_;

	// Token: 0x04000F8B RID: 3979
	private GUI_Main guiMain_;

	// Token: 0x04000F8C RID: 3980
	private sfxScript sfx_;

	// Token: 0x04000F8D RID: 3981
	private textScript tS_;

	// Token: 0x04000F8E RID: 3982
	private pickCharacterScript pcS_;

	// Token: 0x04000F8F RID: 3983
	private roomDataScript rdS_;

	// Token: 0x04000F90 RID: 3984
	private Menu_DevGame menuDevGame_;

	// Token: 0x04000F91 RID: 3985
	private Menu_Dev_GameEntwicklungsbericht menuEntwicklungsbericht_;

	// Token: 0x04000F92 RID: 3986
	private Menu_Dev_AddonDo mDevAddon_;

	// Token: 0x04000F93 RID: 3987
	private Menu_Dev_MMOAddon mDevMMOAddon_;

	// Token: 0x04000F94 RID: 3988
	public GameObject[] uiPrefabs;

	// Token: 0x04000F95 RID: 3989
	public GameObject[] uiObjects;

	// Token: 0x04000F96 RID: 3990
	private roomScript rS_;

	// Token: 0x04000F97 RID: 3991
	private float updateTimer;

	// Token: 0x04000F98 RID: 3992
	private string searchStringA = "";
}
