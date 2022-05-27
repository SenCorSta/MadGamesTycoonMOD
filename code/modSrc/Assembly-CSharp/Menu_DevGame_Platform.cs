using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000120 RID: 288
public class Menu_DevGame_Platform : MonoBehaviour
{
	// Token: 0x060009EE RID: 2542 RVA: 0x000072F4 File Offset: 0x000054F4
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060009EF RID: 2543 RVA: 0x0007DE6C File Offset: 0x0007C06C
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

	// Token: 0x060009F0 RID: 2544 RVA: 0x000072FC File Offset: 0x000054FC
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060009F1 RID: 2545 RVA: 0x0007DF64 File Offset: 0x0007C164
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

	// Token: 0x060009F2 RID: 2546 RVA: 0x0007DFB8 File Offset: 0x0007C1B8
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

	// Token: 0x060009F3 RID: 2547 RVA: 0x0007E014 File Offset: 0x0007C214
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

	// Token: 0x060009F4 RID: 2548 RVA: 0x0007E080 File Offset: 0x0007C280
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

	// Token: 0x060009F5 RID: 2549 RVA: 0x00007334 File Offset: 0x00005534
	public void Init(int nr)
	{
		this.platformNR = nr;
		this.FindScripts();
		this.SetData(this.platformNR);
	}

	// Token: 0x060009F6 RID: 2550 RVA: 0x0007E1C8 File Offset: 0x0007C3C8
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
				if (component && (component.isUnlocked || component.playerConsole) && component.inBesitz && ((this.devGame_.uiObjects[146].GetComponent<Dropdown>().value != 3 && !component.vomMarktGenommen) || (this.devGame_.uiObjects[146].GetComponent<Dropdown>().value == 3 && component.vomMarktGenommen) || isOn))
				{
					if (this.devGame_.gameObject.activeSelf && ((this.devGame_.g_GamePlatform[0] != component.myID && this.devGame_.g_GamePlatform[1] != component.myID && this.devGame_.g_GamePlatform[2] != component.myID && this.devGame_.g_GamePlatform[3] != component.myID) || isOn))
					{
						bool flag = false;
						if (!component.playerConsole && this.devGame_.uiObjects[146].GetComponent<Dropdown>().value == 2)
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

	// Token: 0x060009F7 RID: 2551 RVA: 0x0007E784 File Offset: 0x0007C984
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

	// Token: 0x060009F8 RID: 2552 RVA: 0x0000734F File Offset: 0x0000554F
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060009F9 RID: 2553 RVA: 0x0007E9AC File Offset: 0x0007CBAC
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

	// Token: 0x060009FA RID: 2554 RVA: 0x0000736A File Offset: 0x0000556A
	public void TOGGLE_VomMarktGenommen()
	{
		this.Init(this.platformNR);
	}

	// Token: 0x060009FB RID: 2555 RVA: 0x0000736A File Offset: 0x0000556A
	public void TOGGLE_FitTechLevel()
	{
		this.Init(this.platformNR);
	}

	// Token: 0x04000E43 RID: 3651
	public GameObject[] uiPrefabs;

	// Token: 0x04000E44 RID: 3652
	public GameObject[] uiObjects;

	// Token: 0x04000E45 RID: 3653
	private mainScript mS_;

	// Token: 0x04000E46 RID: 3654
	private GameObject main_;

	// Token: 0x04000E47 RID: 3655
	private GUI_Main guiMain_;

	// Token: 0x04000E48 RID: 3656
	private sfxScript sfx_;

	// Token: 0x04000E49 RID: 3657
	private textScript tS_;

	// Token: 0x04000E4A RID: 3658
	private Menu_DevGame devGame_;

	// Token: 0x04000E4B RID: 3659
	private Menu_Dev_ChangePlatform changePlatform_;

	// Token: 0x04000E4C RID: 3660
	public int platformNR;

	// Token: 0x04000E4D RID: 3661
	private float updateTimer;
}
