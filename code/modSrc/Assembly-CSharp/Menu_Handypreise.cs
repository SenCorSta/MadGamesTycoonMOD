﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001F8 RID: 504
public class Menu_Handypreise : MonoBehaviour
{
	// Token: 0x0600132B RID: 4907 RVA: 0x000CADED File Offset: 0x000C8FED
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600132C RID: 4908 RVA: 0x000CADF8 File Offset: 0x000C8FF8
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
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
	}

	// Token: 0x0600132D RID: 4909 RVA: 0x000CAEC0 File Offset: 0x000C90C0
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x0600132E RID: 4910 RVA: 0x000CAEF8 File Offset: 0x000C90F8
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

	// Token: 0x0600132F RID: 4911 RVA: 0x000CAF44 File Offset: 0x000C9144
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_Handypreis>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001330 RID: 4912 RVA: 0x000CAFA0 File Offset: 0x000C91A0
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x06001331 RID: 4913 RVA: 0x000CAFB4 File Offset: 0x000C91B4
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(88));
		list.Add(this.tS_.GetText(217));
		list.Add(this.tS_.GetText(277));
		list.Add(this.tS_.GetText(273));
		list.Add(this.tS_.GetText(275));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06001332 RID: 4914 RVA: 0x000CB094 File Offset: 0x000C9294
	public void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x06001333 RID: 4915 RVA: 0x000CB0E8 File Offset: 0x000C92E8
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && this.CheckGameData(component) && !this.Exists(this.uiObjects[0], component.myID))
				{
					Item_Handypreis component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_Handypreis>();
					component2.game_ = component;
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.genres_ = this.genres_;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x06001334 RID: 4916 RVA: 0x000CB1F4 File Offset: 0x000C93F4
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.publisherID == this.mS_.myID && !script_.inDevelopment && script_.isOnMarket && script_.gameTyp != 2 && script_.handy;
	}

	// Token: 0x06001335 RID: 4917 RVA: 0x000CB240 File Offset: 0x000C9440
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
				Item_Handypreis component = gameObject.GetComponent<Item_Handypreis>();
				switch (value)
				{
				case 0:
					gameObject.name = component.game_.GetNameSimple();
					break;
				case 1:
					gameObject.name = component.game_.verkaufspreis[3].ToString();
					break;
				case 2:
				{
					float num = (float)component.game_.date_month;
					num /= 13f;
					gameObject.name = component.game_.date_year.ToString() + num.ToString();
					break;
				}
				case 3:
					gameObject.name = component.game_.reviewTotal.ToString();
					break;
				case 4:
					gameObject.name = component.game_.maingenre.ToString();
					break;
				case 5:
					gameObject.name = component.game_.sellsTotal.ToString();
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

	// Token: 0x06001336 RID: 4918 RVA: 0x000CB3D1 File Offset: 0x000C95D1
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x0400176F RID: 5999
	public GameObject[] uiPrefabs;

	// Token: 0x04001770 RID: 6000
	public GameObject[] uiObjects;

	// Token: 0x04001771 RID: 6001
	private mainScript mS_;

	// Token: 0x04001772 RID: 6002
	private GameObject main_;

	// Token: 0x04001773 RID: 6003
	private GUI_Main guiMain_;

	// Token: 0x04001774 RID: 6004
	private sfxScript sfx_;

	// Token: 0x04001775 RID: 6005
	private textScript tS_;

	// Token: 0x04001776 RID: 6006
	private genres genres_;

	// Token: 0x04001777 RID: 6007
	private float updateTimer;
}
