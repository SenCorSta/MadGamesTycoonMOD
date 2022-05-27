using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200021B RID: 539
public class Menu_ShowKonsoleDetailsSelect : MonoBehaviour
{
	// Token: 0x060014A4 RID: 5284 RVA: 0x0000E0B4 File Offset: 0x0000C2B4
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060014A5 RID: 5285 RVA: 0x000E0050 File Offset: 0x000DE250
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

	// Token: 0x060014A6 RID: 5286 RVA: 0x0000E0BC File Offset: 0x0000C2BC
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060014A7 RID: 5287 RVA: 0x000E0138 File Offset: 0x000DE338
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

	// Token: 0x060014A8 RID: 5288 RVA: 0x000E0184 File Offset: 0x000DE384
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

	// Token: 0x060014A9 RID: 5289 RVA: 0x0000E0F4 File Offset: 0x0000C2F4
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x060014AA RID: 5290 RVA: 0x000E01E0 File Offset: 0x000DE3E0
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

	// Token: 0x060014AB RID: 5291 RVA: 0x000E0294 File Offset: 0x000DE494
	public void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x060014AC RID: 5292 RVA: 0x000E02E8 File Offset: 0x000DE4E8
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

	// Token: 0x060014AD RID: 5293 RVA: 0x0000E108 File Offset: 0x0000C308
	public bool CheckPlatformData(platformScript script_)
	{
		return script_ && script_.playerConsole;
	}

	// Token: 0x060014AE RID: 5294 RVA: 0x000E03F4 File Offset: 0x000DE5F4
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

	// Token: 0x060014AF RID: 5295 RVA: 0x0000E11D File Offset: 0x0000C31D
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x040018C5 RID: 6341
	public GameObject[] uiPrefabs;

	// Token: 0x040018C6 RID: 6342
	public GameObject[] uiObjects;

	// Token: 0x040018C7 RID: 6343
	private mainScript mS_;

	// Token: 0x040018C8 RID: 6344
	private GameObject main_;

	// Token: 0x040018C9 RID: 6345
	private GUI_Main guiMain_;

	// Token: 0x040018CA RID: 6346
	private sfxScript sfx_;

	// Token: 0x040018CB RID: 6347
	private textScript tS_;

	// Token: 0x040018CC RID: 6348
	private genres genres_;

	// Token: 0x040018CD RID: 6349
	private platforms platforms_;

	// Token: 0x040018CE RID: 6350
	private float updateTimer;
}
