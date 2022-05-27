using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200021A RID: 538
public class Menu_Charts_MostPlayedF2P : MonoBehaviour
{
	// Token: 0x0600149A RID: 5274 RVA: 0x0000E02E File Offset: 0x0000C22E
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600149B RID: 5275 RVA: 0x000DFD1C File Offset: 0x000DDF1C
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

	// Token: 0x0600149C RID: 5276 RVA: 0x0000E036 File Offset: 0x0000C236
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x0600149D RID: 5277 RVA: 0x0000E068 File Offset: 0x0000C268
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x0600149E RID: 5278 RVA: 0x000DFDE4 File Offset: 0x000DDFE4
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

	// Token: 0x0600149F RID: 5279 RVA: 0x0000E070 File Offset: 0x0000C270
	public void Init()
	{
		this.FindScripts();
		this.InitDropdowns();
		if (this.uiObjects[0].transform.childCount <= 0)
		{
			this.SetData();
		}
	}

	// Token: 0x060014A0 RID: 5280 RVA: 0x000DFE74 File Offset: 0x000DE074
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

	// Token: 0x060014A1 RID: 5281 RVA: 0x0000E099 File Offset: 0x0000C299
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060014A2 RID: 5282 RVA: 0x000DFFF8 File Offset: 0x000DE1F8
	public void DROPDOWN_Sort()
	{
		int value = this.uiObjects[1].GetComponent<Dropdown>().value;
		PlayerPrefs.SetInt(this.uiObjects[1].name, value);
		this.uiObjects[0].SetActive(false);
		this.uiObjects[0].SetActive(true);
		this.SetData();
	}

	// Token: 0x040018BD RID: 6333
	private mainScript mS_;

	// Token: 0x040018BE RID: 6334
	private GameObject main_;

	// Token: 0x040018BF RID: 6335
	private GUI_Main guiMain_;

	// Token: 0x040018C0 RID: 6336
	private sfxScript sfx_;

	// Token: 0x040018C1 RID: 6337
	private textScript tS_;

	// Token: 0x040018C2 RID: 6338
	private genres genres_;

	// Token: 0x040018C3 RID: 6339
	public GameObject[] uiPrefabs;

	// Token: 0x040018C4 RID: 6340
	public GameObject[] uiObjects;
}
