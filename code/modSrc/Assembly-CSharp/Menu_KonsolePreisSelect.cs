using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001FB RID: 507
public class Menu_KonsolePreisSelect : MonoBehaviour
{
	// Token: 0x06001350 RID: 4944 RVA: 0x000CBFF5 File Offset: 0x000CA1F5
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001351 RID: 4945 RVA: 0x000CC000 File Offset: 0x000CA200
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

	// Token: 0x06001352 RID: 4946 RVA: 0x000CC0E6 File Offset: 0x000CA2E6
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06001353 RID: 4947 RVA: 0x000CC120 File Offset: 0x000CA320
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

	// Token: 0x06001354 RID: 4948 RVA: 0x000CC16C File Offset: 0x000CA36C
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

	// Token: 0x06001355 RID: 4949 RVA: 0x000CC1C8 File Offset: 0x000CA3C8
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x06001356 RID: 4950 RVA: 0x000CC1DC File Offset: 0x000CA3DC
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

	// Token: 0x06001357 RID: 4951 RVA: 0x000CC2E4 File Offset: 0x000CA4E4
	public void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x06001358 RID: 4952 RVA: 0x000CC338 File Offset: 0x000CA538
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

	// Token: 0x06001359 RID: 4953 RVA: 0x000CC442 File Offset: 0x000CA642
	public bool CheckPlatformData(platformScript script_)
	{
		return script_ && script_.ownerID == this.mS_.myID && !script_.vomMarktGenommen && script_.isUnlocked;
	}

	// Token: 0x0600135A RID: 4954 RVA: 0x000CC474 File Offset: 0x000CA674
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

	// Token: 0x0600135B RID: 4955 RVA: 0x000CC64F File Offset: 0x000CA84F
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x0400178C RID: 6028
	public GameObject[] uiPrefabs;

	// Token: 0x0400178D RID: 6029
	public GameObject[] uiObjects;

	// Token: 0x0400178E RID: 6030
	private mainScript mS_;

	// Token: 0x0400178F RID: 6031
	private GameObject main_;

	// Token: 0x04001790 RID: 6032
	private GUI_Main guiMain_;

	// Token: 0x04001791 RID: 6033
	private sfxScript sfx_;

	// Token: 0x04001792 RID: 6034
	private textScript tS_;

	// Token: 0x04001793 RID: 6035
	private genres genres_;

	// Token: 0x04001794 RID: 6036
	private platforms platforms_;

	// Token: 0x04001795 RID: 6037
	private float updateTimer;
}
