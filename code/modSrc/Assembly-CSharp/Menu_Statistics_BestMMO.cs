using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000225 RID: 549
public class Menu_Statistics_BestMMO : MonoBehaviour
{
	// Token: 0x06001507 RID: 5383 RVA: 0x0000E4F6 File Offset: 0x0000C6F6
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001508 RID: 5384 RVA: 0x000E2078 File Offset: 0x000E0278
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

	// Token: 0x06001509 RID: 5385 RVA: 0x0000E4FE File Offset: 0x0000C6FE
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x0600150A RID: 5386 RVA: 0x0000E530 File Offset: 0x0000C730
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x0600150B RID: 5387 RVA: 0x000E2140 File Offset: 0x000E0340
	public void InitDropdowns()
	{
		this.FindScripts();
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(1906));
		list.Add(this.tS_.GetText(1903));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x0600150C RID: 5388 RVA: 0x0000E538 File Offset: 0x0000C738
	public void Init()
	{
		this.FindScripts();
		this.InitDropdowns();
		if (this.uiObjects[0].transform.childCount <= 0)
		{
			this.SetData();
		}
	}

	// Token: 0x0600150D RID: 5389 RVA: 0x000E21D0 File Offset: 0x000E03D0
	private void SetData()
	{
		int value = this.uiObjects[1].GetComponent<Dropdown>().value;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && component.gameTyp == 1 && ((value == 0 && !component.inDevelopment && component.isOnMarket && component.abonnements > 0) || (value == 1 && !component.inDevelopment && component.bestAbonnements > 0)))
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform);
					Item_BestMMO component2 = gameObject.GetComponent<Item_BestMMO>();
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.genres_ = this.genres_;
					component2.game_ = component;
					component2.sort = value;
					if (value == 0)
					{
						gameObject.name = component.abonnements.ToString();
					}
					if (value == 1)
					{
						gameObject.name = component.bestAbonnements.ToString();
					}
				}
			}
		}
		this.mS_.SortChildrenByFloat(this.uiObjects[0]);
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x0600150E RID: 5390 RVA: 0x0000E561 File Offset: 0x0000C761
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600150F RID: 5391 RVA: 0x000E2354 File Offset: 0x000E0554
	public void DROPDOWN_Sort()
	{
		int value = this.uiObjects[1].GetComponent<Dropdown>().value;
		PlayerPrefs.SetInt(this.uiObjects[1].name, value);
		this.uiObjects[0].SetActive(false);
		this.uiObjects[0].SetActive(true);
		this.SetData();
	}

	// Token: 0x0400191D RID: 6429
	private mainScript mS_;

	// Token: 0x0400191E RID: 6430
	private GameObject main_;

	// Token: 0x0400191F RID: 6431
	private GUI_Main guiMain_;

	// Token: 0x04001920 RID: 6432
	private sfxScript sfx_;

	// Token: 0x04001921 RID: 6433
	private textScript tS_;

	// Token: 0x04001922 RID: 6434
	private genres genres_;

	// Token: 0x04001923 RID: 6435
	public GameObject[] uiPrefabs;

	// Token: 0x04001924 RID: 6436
	public GameObject[] uiObjects;
}
