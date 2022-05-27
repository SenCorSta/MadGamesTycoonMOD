using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000203 RID: 515
public class Menu_RemoveKonsoleSelect : MonoBehaviour
{
	// Token: 0x060013A2 RID: 5026 RVA: 0x0000D6A6 File Offset: 0x0000B8A6
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060013A3 RID: 5027 RVA: 0x000D9138 File Offset: 0x000D7338
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

	// Token: 0x060013A4 RID: 5028 RVA: 0x0000D6AE File Offset: 0x0000B8AE
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060013A5 RID: 5029 RVA: 0x000D9220 File Offset: 0x000D7420
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

	// Token: 0x060013A6 RID: 5030 RVA: 0x000D926C File Offset: 0x000D746C
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

	// Token: 0x060013A7 RID: 5031 RVA: 0x0000D6E6 File Offset: 0x0000B8E6
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x060013A8 RID: 5032 RVA: 0x000D92C8 File Offset: 0x000D74C8
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

	// Token: 0x060013A9 RID: 5033 RVA: 0x000D93BC File Offset: 0x000D75BC
	public void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x060013AA RID: 5034 RVA: 0x000D9410 File Offset: 0x000D7610
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

	// Token: 0x060013AB RID: 5035 RVA: 0x0000D2F5 File Offset: 0x0000B4F5
	public bool CheckPlatformData(platformScript script_)
	{
		return script_ && script_.playerConsole && !script_.vomMarktGenommen && script_.isUnlocked;
	}

	// Token: 0x060013AC RID: 5036 RVA: 0x000D951C File Offset: 0x000D771C
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

	// Token: 0x060013AD RID: 5037 RVA: 0x0000D6FA File Offset: 0x0000B8FA
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x040017D3 RID: 6099
	public GameObject[] uiPrefabs;

	// Token: 0x040017D4 RID: 6100
	public GameObject[] uiObjects;

	// Token: 0x040017D5 RID: 6101
	private mainScript mS_;

	// Token: 0x040017D6 RID: 6102
	private GameObject main_;

	// Token: 0x040017D7 RID: 6103
	private GUI_Main guiMain_;

	// Token: 0x040017D8 RID: 6104
	private sfxScript sfx_;

	// Token: 0x040017D9 RID: 6105
	private textScript tS_;

	// Token: 0x040017DA RID: 6106
	private genres genres_;

	// Token: 0x040017DB RID: 6107
	private platforms platforms_;

	// Token: 0x040017DC RID: 6108
	private float updateTimer;
}
