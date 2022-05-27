using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200021B RID: 539
public class Menu_Charts_MostPlayedF2P : MonoBehaviour
{
	// Token: 0x060014B8 RID: 5304 RVA: 0x000D6783 File Offset: 0x000D4983
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060014B9 RID: 5305 RVA: 0x000D678C File Offset: 0x000D498C
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

	// Token: 0x060014BA RID: 5306 RVA: 0x000D6854 File Offset: 0x000D4A54
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x060014BB RID: 5307 RVA: 0x000D6886 File Offset: 0x000D4A86
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060014BC RID: 5308 RVA: 0x000D6890 File Offset: 0x000D4A90
	public void InitDropdowns()
	{
		this.FindScripts();
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(1907));
		list.Add(this.tS_.GetText(1904));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x060014BD RID: 5309 RVA: 0x000D6920 File Offset: 0x000D4B20
	public void Init()
	{
		this.FindScripts();
		this.InitDropdowns();
		if (this.uiObjects[0].transform.childCount <= 0)
		{
			this.SetData();
		}
	}

	// Token: 0x060014BE RID: 5310 RVA: 0x000D694C File Offset: 0x000D4B4C
	private void SetData()
	{
		int value = this.uiObjects[1].GetComponent<Dropdown>().value;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && component.gameTyp == 2 && ((value == 0 && !component.inDevelopment && component.isOnMarket && component.abonnements > 0) || (value == 1 && !component.inDevelopment && component.bestAbonnements > 0)))
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

	// Token: 0x060014BF RID: 5311 RVA: 0x000D6AD0 File Offset: 0x000D4CD0
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060014C0 RID: 5312 RVA: 0x000D6AEC File Offset: 0x000D4CEC
	public void DROPDOWN_Sort()
	{
		int value = this.uiObjects[1].GetComponent<Dropdown>().value;
		PlayerPrefs.SetInt(this.uiObjects[1].name, value);
		this.uiObjects[0].SetActive(false);
		this.uiObjects[0].SetActive(true);
		this.SetData();
	}

	// Token: 0x040018C4 RID: 6340
	private mainScript mS_;

	// Token: 0x040018C5 RID: 6341
	private GameObject main_;

	// Token: 0x040018C6 RID: 6342
	private GUI_Main guiMain_;

	// Token: 0x040018C7 RID: 6343
	private sfxScript sfx_;

	// Token: 0x040018C8 RID: 6344
	private textScript tS_;

	// Token: 0x040018C9 RID: 6345
	private genres genres_;

	// Token: 0x040018CA RID: 6346
	public GameObject[] uiPrefabs;

	// Token: 0x040018CB RID: 6347
	public GameObject[] uiObjects;
}
