using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001FA RID: 506
public class Menu_KonsolePreisSelect : MonoBehaviour
{
	// Token: 0x06001335 RID: 4917 RVA: 0x0000D2A1 File Offset: 0x0000B4A1
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001336 RID: 4918 RVA: 0x000D6474 File Offset: 0x000D4674
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
		if (!this.platforms_)
		{
			this.platforms_ = this.main_.GetComponent<platforms>();
		}
	}

	// Token: 0x06001337 RID: 4919 RVA: 0x0000D2A9 File Offset: 0x0000B4A9
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06001338 RID: 4920 RVA: 0x000D655C File Offset: 0x000D475C
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

	// Token: 0x06001339 RID: 4921 RVA: 0x000D65A8 File Offset: 0x000D47A8
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_KonsolePreisSelect>().pS_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600133A RID: 4922 RVA: 0x0000D2E1 File Offset: 0x0000B4E1
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x0600133B RID: 4923 RVA: 0x000D6604 File Offset: 0x000D4804
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(217));
		list.Add(this.tS_.GetText(4));
		list.Add(this.tS_.GetText(219));
		list.Add(this.tS_.GetText(220));
		list.Add(this.tS_.GetText(1484));
		list.Add(this.tS_.GetText(1485));
		list.Add(this.tS_.GetText(88));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x0600133C RID: 4924 RVA: 0x000D670C File Offset: 0x000D490C
	public void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x0600133D RID: 4925 RVA: 0x000D6760 File Offset: 0x000D4960
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Platform");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				platformScript component = array[i].GetComponent<platformScript>();
				if (component && this.CheckPlatformData(component) && !this.Exists(this.uiObjects[0], component.myID))
				{
					Item_KonsolePreisSelect component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_KonsolePreisSelect>();
					component2.pS_ = component;
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

	// Token: 0x0600133E RID: 4926 RVA: 0x0000D2F5 File Offset: 0x0000B4F5
	public bool CheckPlatformData(platformScript script_)
	{
		return script_ && script_.playerConsole && !script_.vomMarktGenommen && script_.isUnlocked;
	}

	// Token: 0x0600133F RID: 4927 RVA: 0x000D686C File Offset: 0x000D4A6C
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
				Item_KonsolePreisSelect component = gameObject.GetComponent<Item_KonsolePreisSelect>();
				switch (value)
				{
				case 0:
					gameObject.name = component.pS_.GetName();
					break;
				case 1:
				{
					float num = (float)component.pS_.date_month;
					num /= 13f;
					gameObject.name = component.pS_.date_year.ToString() + num.ToString();
					break;
				}
				case 2:
					gameObject.name = component.pS_.tech.ToString();
					break;
				case 3:
					gameObject.name = component.pS_.GetMarktanteil().ToString();
					break;
				case 4:
					gameObject.name = component.pS_.GetGames().ToString();
					break;
				case 5:
					gameObject.name = (100 - component.pS_.typ).ToString();
					break;
				case 6:
					gameObject.name = component.pS_.GetAktiveNutzer().ToString();
					break;
				case 7:
					gameObject.name = component.pS_.verkaufspreis.ToString();
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

	// Token: 0x06001340 RID: 4928 RVA: 0x0000D31A File Offset: 0x0000B51A
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001783 RID: 6019
	public GameObject[] uiPrefabs;

	// Token: 0x04001784 RID: 6020
	public GameObject[] uiObjects;

	// Token: 0x04001785 RID: 6021
	private mainScript mS_;

	// Token: 0x04001786 RID: 6022
	private GameObject main_;

	// Token: 0x04001787 RID: 6023
	private GUI_Main guiMain_;

	// Token: 0x04001788 RID: 6024
	private sfxScript sfx_;

	// Token: 0x04001789 RID: 6025
	private textScript tS_;

	// Token: 0x0400178A RID: 6026
	private genres genres_;

	// Token: 0x0400178B RID: 6027
	private platforms platforms_;

	// Token: 0x0400178C RID: 6028
	private float updateTimer;
}
