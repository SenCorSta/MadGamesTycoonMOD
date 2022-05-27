using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000204 RID: 516
public class Menu_RemoveKonsoleSelect : MonoBehaviour
{
	// Token: 0x060013BD RID: 5053 RVA: 0x000CF0CF File Offset: 0x000CD2CF
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060013BE RID: 5054 RVA: 0x000CF0D8 File Offset: 0x000CD2D8
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

	// Token: 0x060013BF RID: 5055 RVA: 0x000CF1BE File Offset: 0x000CD3BE
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060013C0 RID: 5056 RVA: 0x000CF1F8 File Offset: 0x000CD3F8
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

	// Token: 0x060013C1 RID: 5057 RVA: 0x000CF244 File Offset: 0x000CD444
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_KonsoleRemove>().pS_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060013C2 RID: 5058 RVA: 0x000CF2A0 File Offset: 0x000CD4A0
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x060013C3 RID: 5059 RVA: 0x000CF2B4 File Offset: 0x000CD4B4
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
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x060013C4 RID: 5060 RVA: 0x000CF3A8 File Offset: 0x000CD5A8
	public void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x060013C5 RID: 5061 RVA: 0x000CF3FC File Offset: 0x000CD5FC
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
					Item_KonsoleRemove component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_KonsoleRemove>();
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

	// Token: 0x060013C6 RID: 5062 RVA: 0x000CF506 File Offset: 0x000CD706
	public bool CheckPlatformData(platformScript script_)
	{
		return script_ && script_.ownerID == this.mS_.myID && !script_.vomMarktGenommen && script_.isUnlocked;
	}

	// Token: 0x060013C7 RID: 5063 RVA: 0x000CF538 File Offset: 0x000CD738
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
				Item_KonsoleRemove component = gameObject.GetComponent<Item_KonsoleRemove>();
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

	// Token: 0x060013C8 RID: 5064 RVA: 0x000CF6F3 File Offset: 0x000CD8F3
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x040017DC RID: 6108
	public GameObject[] uiPrefabs;

	// Token: 0x040017DD RID: 6109
	public GameObject[] uiObjects;

	// Token: 0x040017DE RID: 6110
	private mainScript mS_;

	// Token: 0x040017DF RID: 6111
	private GameObject main_;

	// Token: 0x040017E0 RID: 6112
	private GUI_Main guiMain_;

	// Token: 0x040017E1 RID: 6113
	private sfxScript sfx_;

	// Token: 0x040017E2 RID: 6114
	private textScript tS_;

	// Token: 0x040017E3 RID: 6115
	private genres genres_;

	// Token: 0x040017E4 RID: 6116
	private platforms platforms_;

	// Token: 0x040017E5 RID: 6117
	private float updateTimer;
}
