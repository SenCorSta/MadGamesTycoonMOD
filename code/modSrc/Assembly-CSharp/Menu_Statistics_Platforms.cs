using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200022B RID: 555
public class Menu_Statistics_Platforms : MonoBehaviour
{
	// Token: 0x06001561 RID: 5473 RVA: 0x000DA774 File Offset: 0x000D8974
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001562 RID: 5474 RVA: 0x000DA77C File Offset: 0x000D897C
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindGameObjectWithTag("Main");
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
	}

	// Token: 0x06001563 RID: 5475 RVA: 0x000DA826 File Offset: 0x000D8A26
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06001564 RID: 5476 RVA: 0x000DA860 File Offset: 0x000D8A60
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

	// Token: 0x06001565 RID: 5477 RVA: 0x000DA8AC File Offset: 0x000D8AAC
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).GetComponent<Item_Stats_Platform>().pS_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001566 RID: 5478 RVA: 0x000DA8F0 File Offset: 0x000D8AF0
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x06001567 RID: 5479 RVA: 0x000DA904 File Offset: 0x000D8B04
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
		list.Add(this.tS_.GetText(215));
		list.Add(this.tS_.GetText(1484));
		list.Add(this.tS_.GetText(1485));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06001568 RID: 5480 RVA: 0x000DAA3C File Offset: 0x000D8C3C
	private void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x06001569 RID: 5481 RVA: 0x000DAA90 File Offset: 0x000D8C90
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Platform");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				platformScript component = array[i].GetComponent<platformScript>();
				if (component && component.isUnlocked && !this.Exists(this.uiObjects[0], component.myID))
				{
					Item_Stats_Platform component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_Stats_Platform>();
					component2.myID = component.myID;
					component2.pS_ = component;
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[6]);
	}

	// Token: 0x0600156A RID: 5482 RVA: 0x000DAB9C File Offset: 0x000D8D9C
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
				Item_Stats_Platform component = gameObject.GetComponent<Item_Stats_Platform>();
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
					if (component.pS_.typ == 4)
					{
						gameObject.name = "0";
					}
					break;
				case 6:
					gameObject.name = component.pS_.GetGames().ToString();
					break;
				case 7:
					if (component.pS_.inBesitz)
					{
						gameObject.name = "1";
					}
					else
					{
						gameObject.name = "0";
					}
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

	// Token: 0x0600156B RID: 5483 RVA: 0x000DADEE File Offset: 0x000D8FEE
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		if (!this.guiMain_.uiObjects[318].activeSelf)
		{
			this.guiMain_.CloseMenu();
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001951 RID: 6481
	public GameObject[] uiPrefabs;

	// Token: 0x04001952 RID: 6482
	public GameObject[] uiObjects;

	// Token: 0x04001953 RID: 6483
	private mainScript mS_;

	// Token: 0x04001954 RID: 6484
	private GameObject main_;

	// Token: 0x04001955 RID: 6485
	private GUI_Main guiMain_;

	// Token: 0x04001956 RID: 6486
	private sfxScript sfx_;

	// Token: 0x04001957 RID: 6487
	private textScript tS_;

	// Token: 0x04001958 RID: 6488
	private int TAB;

	// Token: 0x04001959 RID: 6489
	private float updateTimer;
}
