using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000121 RID: 289
public class Menu_DevGame_Platform : MonoBehaviour
{
	// Token: 0x060009FD RID: 2557 RVA: 0x0006D1DA File Offset: 0x0006B3DA
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060009FE RID: 2558 RVA: 0x0006D1E4 File Offset: 0x0006B3E4
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
		if (!this.devGame_)
		{
			this.devGame_ = this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>();
		}
		if (!this.changePlatform_)
		{
			this.changePlatform_ = this.guiMain_.uiObjects[102].GetComponent<Menu_Dev_ChangePlatform>();
		}
	}

	// Token: 0x060009FF RID: 2559 RVA: 0x0006D2DA File Offset: 0x0006B4DA
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000A00 RID: 2560 RVA: 0x0006D314 File Offset: 0x0006B514
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
		this.SetData(this.platformNR);
	}

	// Token: 0x06000A01 RID: 2561 RVA: 0x0006D368 File Offset: 0x0006B568
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_DevGame_Platform>().pS_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000A02 RID: 2562 RVA: 0x0006D3C4 File Offset: 0x0006B5C4
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.uiObjects[9].SetActive(false);
		if (this.guiMain_.uiObjects[56].activeSelf && this.devGame_.uiObjects[146].GetComponent<Dropdown>().value == 1)
		{
			this.uiObjects[9].SetActive(true);
		}
	}

	// Token: 0x06000A03 RID: 2563 RVA: 0x0006D430 File Offset: 0x0006B630
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(216));
		list.Add(this.tS_.GetText(217));
		list.Add(this.tS_.GetText(4));
		list.Add(this.tS_.GetText(218));
		list.Add(this.tS_.GetText(219));
		list.Add(this.tS_.GetText(220));
		list.Add(this.tS_.GetText(6));
		list.Add(this.tS_.GetText(1484));
		list.Add(this.tS_.GetText(1485));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = 5;
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06000A04 RID: 2564 RVA: 0x0006D575 File Offset: 0x0006B775
	public void Init(int nr)
	{
		this.platformNR = nr;
		this.FindScripts();
		this.SetData(this.platformNR);
	}

	// Token: 0x06000A05 RID: 2565 RVA: 0x0006D590 File Offset: 0x0006B790
	private void SetData(int nr)
	{
		bool isOn = this.uiObjects[5].GetComponent<Toggle>().isOn;
		this.uiObjects[4].GetComponent<Text>().text = this.tS_.GetText(360 + nr);
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		int num = 0;
		if (this.devGame_.gameObject.activeSelf)
		{
			num = this.devGame_.GetEngineTechLevel();
		}
		if (this.changePlatform_.gameObject.activeSelf)
		{
			num = this.changePlatform_.GetEngineTechLevel();
		}
		this.uiObjects[7].GetComponent<Text>().text = this.tS_.GetText(376) + ": " + num.ToString();
		GameObject[] array = GameObject.FindGameObjectsWithTag("Platform");
		for (int j = 0; j < array.Length; j++)
		{
			if (array[j])
			{
				platformScript component = array[j].GetComponent<platformScript>();
				if (component && (component.isUnlocked || component.ownerID == this.mS_.myID) && component.inBesitz && ((this.devGame_.uiObjects[146].GetComponent<Dropdown>().value != 3 && !component.vomMarktGenommen) || (this.devGame_.uiObjects[146].GetComponent<Dropdown>().value == 3 && component.vomMarktGenommen) || isOn))
				{
					if (this.devGame_.gameObject.activeSelf && ((this.devGame_.g_GamePlatform[0] != component.myID && this.devGame_.g_GamePlatform[1] != component.myID && this.devGame_.g_GamePlatform[2] != component.myID && this.devGame_.g_GamePlatform[3] != component.myID) || isOn))
					{
						bool flag = false;
						if (component.ownerID != this.mS_.myID && this.devGame_.uiObjects[146].GetComponent<Dropdown>().value == 2)
						{
							flag = true;
						}
						if (component.typ == 3 && this.devGame_.uiObjects[146].GetComponent<Dropdown>().value != 5)
						{
							flag = true;
						}
						if (component.typ != 3 && this.devGame_.uiObjects[146].GetComponent<Dropdown>().value == 5)
						{
							flag = true;
						}
						if (component.typ == 4 && this.devGame_.uiObjects[146].GetComponent<Dropdown>().value != 4)
						{
							flag = true;
						}
						if (component.typ != 4 && this.devGame_.uiObjects[146].GetComponent<Dropdown>().value == 4)
						{
							flag = true;
						}
						if (this.uiObjects[8].GetComponent<Toggle>().isOn && num > component.tech)
						{
							flag = true;
						}
						if (!flag && !this.Exists(this.uiObjects[0], component.myID))
						{
							Item_DevGame_Platform component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_DevGame_Platform>();
							component2.myID = component.myID;
							component2.pS_ = component;
							component2.mS_ = this.mS_;
							component2.tS_ = this.tS_;
							component2.sfx_ = this.sfx_;
							component2.guiMain_ = this.guiMain_;
							component2.devGame_ = this.devGame_;
						}
					}
					if (this.changePlatform_.gameObject.activeSelf && ((this.changePlatform_.g_GamePlatform[0] != component.myID && this.changePlatform_.g_GamePlatform[1] != component.myID && this.changePlatform_.g_GamePlatform[2] != component.myID && this.changePlatform_.g_GamePlatform[3] != component.myID) || isOn) && (component.tech >= this.changePlatform_.GetEngineTechLevel() || isOn))
					{
						bool flag2 = false;
						if (component.typ == 3 && !this.changePlatform_.gS_.handy)
						{
							flag2 = true;
						}
						if (component.typ != 3 && this.changePlatform_.gS_.handy)
						{
							flag2 = true;
						}
						if (component.typ == 4 && !this.changePlatform_.gS_.arcade)
						{
							flag2 = true;
						}
						if (component.typ != 4 && this.changePlatform_.gS_.arcade)
						{
							flag2 = true;
						}
						if (this.uiObjects[8].GetComponent<Toggle>().isOn && num > component.tech)
						{
							flag2 = true;
						}
						if (!flag2 && !this.Exists(this.uiObjects[0], component.myID))
						{
							Item_DevGame_Platform component3 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_DevGame_Platform>();
							component3.myID = component.myID;
							component3.pS_ = component;
							component3.mS_ = this.mS_;
							component3.tS_ = this.tS_;
							component3.sfx_ = this.sfx_;
							component3.guiMain_ = this.guiMain_;
							component3.changePlatform_ = this.changePlatform_;
						}
					}
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[6]);
	}

	// Token: 0x06000A06 RID: 2566 RVA: 0x0006DB60 File Offset: 0x0006BD60
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
				Item_DevGame_Platform component = gameObject.GetComponent<Item_DevGame_Platform>();
				switch (value)
				{
				case 0:
					gameObject.name = component.pS_.GetName();
					break;
				case 1:
					gameObject.name = component.pS_.GetManufacturer().ToString();
					break;
				case 2:
				{
					float num = (float)component.pS_.date_month;
					num /= 13f;
					gameObject.name = component.pS_.date_year.ToString() + num.ToString();
					break;
				}
				case 3:
					gameObject.name = component.pS_.tech.ToString();
					break;
				case 4:
					gameObject.name = component.pS_.GetPrice().ToString();
					break;
				case 5:
					gameObject.name = component.pS_.GetMarktanteil().ToString();
					break;
				case 6:
					gameObject.name = component.pS_.GetGames().ToString();
					break;
				case 7:
					gameObject.name = component.pS_.GetDevCosts().ToString();
					break;
				case 8:
					gameObject.name = (100 - component.pS_.typ).ToString();
					break;
				case 9:
					gameObject.name = component.pS_.GetAktiveNutzer().ToString();
					break;
				}
			}
		}
		if (value == 0 || value == 1)
		{
			this.mS_.SortChildrenByName(this.uiObjects[0]);
			return;
		}
		this.mS_.SortChildrenByFloat(this.uiObjects[0]);
	}

	// Token: 0x06000A07 RID: 2567 RVA: 0x0006DD87 File Offset: 0x0006BF87
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000A08 RID: 2568 RVA: 0x0006DDA4 File Offset: 0x0006BFA4
	public void BUTTON_PlatformEntfernen()
	{
		if (this.devGame_.gameObject.activeSelf)
		{
			this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().SetPlatform(this.platformNR, -1);
		}
		if (this.changePlatform_.gameObject.activeSelf)
		{
			this.guiMain_.uiObjects[102].GetComponent<Menu_Dev_ChangePlatform>().SetPlatform(this.platformNR, -1, false);
		}
		this.BUTTON_Close();
	}

	// Token: 0x06000A09 RID: 2569 RVA: 0x0006DE1A File Offset: 0x0006C01A
	public void TOGGLE_VomMarktGenommen()
	{
		this.Init(this.platformNR);
	}

	// Token: 0x06000A0A RID: 2570 RVA: 0x0006DE1A File Offset: 0x0006C01A
	public void TOGGLE_FitTechLevel()
	{
		this.Init(this.platformNR);
	}

	// Token: 0x04000E4B RID: 3659
	public GameObject[] uiPrefabs;

	// Token: 0x04000E4C RID: 3660
	public GameObject[] uiObjects;

	// Token: 0x04000E4D RID: 3661
	private mainScript mS_;

	// Token: 0x04000E4E RID: 3662
	private GameObject main_;

	// Token: 0x04000E4F RID: 3663
	private GUI_Main guiMain_;

	// Token: 0x04000E50 RID: 3664
	private sfxScript sfx_;

	// Token: 0x04000E51 RID: 3665
	private textScript tS_;

	// Token: 0x04000E52 RID: 3666
	private Menu_DevGame devGame_;

	// Token: 0x04000E53 RID: 3667
	private Menu_Dev_ChangePlatform changePlatform_;

	// Token: 0x04000E54 RID: 3668
	public int platformNR;

	// Token: 0x04000E55 RID: 3669
	private float updateTimer;
}
