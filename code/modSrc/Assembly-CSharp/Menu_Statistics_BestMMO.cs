using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000226 RID: 550
public class Menu_Statistics_BestMMO : MonoBehaviour
{
	// Token: 0x06001525 RID: 5413 RVA: 0x000D91C2 File Offset: 0x000D73C2
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001526 RID: 5414 RVA: 0x000D91CC File Offset: 0x000D73CC
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

	// Token: 0x06001527 RID: 5415 RVA: 0x000D9294 File Offset: 0x000D7494
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x06001528 RID: 5416 RVA: 0x000D92C6 File Offset: 0x000D74C6
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06001529 RID: 5417 RVA: 0x000D92D0 File Offset: 0x000D74D0
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

	// Token: 0x0600152A RID: 5418 RVA: 0x000D9360 File Offset: 0x000D7560
	public void Init()
	{
		this.FindScripts();
		this.InitDropdowns();
		if (this.uiObjects[0].transform.childCount <= 0)
		{
			this.SetData();
		}
	}

	// Token: 0x0600152B RID: 5419 RVA: 0x000D938C File Offset: 0x000D758C
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

	// Token: 0x0600152C RID: 5420 RVA: 0x000D9510 File Offset: 0x000D7710
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600152D RID: 5421 RVA: 0x000D952C File Offset: 0x000D772C
	public void DROPDOWN_Sort()
	{
		int value = this.uiObjects[1].GetComponent<Dropdown>().value;
		PlayerPrefs.SetInt(this.uiObjects[1].name, value);
		this.uiObjects[0].SetActive(false);
		this.uiObjects[0].SetActive(true);
		this.SetData();
	}

	// Token: 0x04001924 RID: 6436
	private mainScript mS_;

	// Token: 0x04001925 RID: 6437
	private GameObject main_;

	// Token: 0x04001926 RID: 6438
	private GUI_Main guiMain_;

	// Token: 0x04001927 RID: 6439
	private sfxScript sfx_;

	// Token: 0x04001928 RID: 6440
	private textScript tS_;

	// Token: 0x04001929 RID: 6441
	private genres genres_;

	// Token: 0x0400192A RID: 6442
	public GameObject[] uiPrefabs;

	// Token: 0x0400192B RID: 6443
	public GameObject[] uiObjects;
}
