using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200021C RID: 540
public class Menu_ShowKonsoleDetailsSelect : MonoBehaviour
{
	// Token: 0x060014C2 RID: 5314 RVA: 0x000D6B41 File Offset: 0x000D4D41
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060014C3 RID: 5315 RVA: 0x000D6B4C File Offset: 0x000D4D4C
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

	// Token: 0x060014C4 RID: 5316 RVA: 0x000D6C32 File Offset: 0x000D4E32
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060014C5 RID: 5317 RVA: 0x000D6C6C File Offset: 0x000D4E6C
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

	// Token: 0x060014C6 RID: 5318 RVA: 0x000D6CB8 File Offset: 0x000D4EB8
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_KonsoleDetailsSelect>().pS_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060014C7 RID: 5319 RVA: 0x000D6D14 File Offset: 0x000D4F14
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x060014C8 RID: 5320 RVA: 0x000D6D28 File Offset: 0x000D4F28
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(217));
		list.Add(this.tS_.GetText(4));
		list.Add(this.tS_.GetText(1612));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x060014C9 RID: 5321 RVA: 0x000D6DDC File Offset: 0x000D4FDC
	public void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x060014CA RID: 5322 RVA: 0x000D6E30 File Offset: 0x000D5030
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
					Item_KonsoleDetailsSelect component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_KonsoleDetailsSelect>();
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

	// Token: 0x060014CB RID: 5323 RVA: 0x000D6F3A File Offset: 0x000D513A
	public bool CheckPlatformData(platformScript script_)
	{
		return script_ && script_.ownerID == this.mS_.myID;
	}

	// Token: 0x060014CC RID: 5324 RVA: 0x000D6F5C File Offset: 0x000D515C
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
				Item_KonsoleDetailsSelect component = gameObject.GetComponent<Item_KonsoleDetailsSelect>();
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
					if (!component.pS_.isUnlocked)
					{
						gameObject.name = "999999";
					}
					break;
				}
				case 2:
					gameObject.name = component.pS_.tech.ToString();
					break;
				case 3:
					gameObject.name = component.pS_.performancePoints.ToString();
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

	// Token: 0x060014CD RID: 5325 RVA: 0x000D70C3 File Offset: 0x000D52C3
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x040018CC RID: 6348
	public GameObject[] uiPrefabs;

	// Token: 0x040018CD RID: 6349
	public GameObject[] uiObjects;

	// Token: 0x040018CE RID: 6350
	private mainScript mS_;

	// Token: 0x040018CF RID: 6351
	private GameObject main_;

	// Token: 0x040018D0 RID: 6352
	private GUI_Main guiMain_;

	// Token: 0x040018D1 RID: 6353
	private sfxScript sfx_;

	// Token: 0x040018D2 RID: 6354
	private textScript tS_;

	// Token: 0x040018D3 RID: 6355
	private genres genres_;

	// Token: 0x040018D4 RID: 6356
	private platforms platforms_;

	// Token: 0x040018D5 RID: 6357
	private float updateTimer;
}
