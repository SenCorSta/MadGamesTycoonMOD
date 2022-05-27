using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001F9 RID: 505
public class Menu_InAppVerwalten : MonoBehaviour
{
	// Token: 0x06001328 RID: 4904 RVA: 0x0000D227 File Offset: 0x0000B427
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001329 RID: 4905 RVA: 0x000D5EEC File Offset: 0x000D40EC
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

	// Token: 0x0600132A RID: 4906 RVA: 0x0000D22F File Offset: 0x0000B42F
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x0600132B RID: 4907 RVA: 0x000D5FB4 File Offset: 0x000D41B4
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

	// Token: 0x0600132C RID: 4908 RVA: 0x000D6000 File Offset: 0x000D4200
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_InAppVerwalten>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600132D RID: 4909 RVA: 0x0000D267 File Offset: 0x0000B467
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x0600132E RID: 4910 RVA: 0x000D605C File Offset: 0x000D425C
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(217));
		list.Add(this.tS_.GetText(277));
		list.Add(this.tS_.GetText(273));
		list.Add(this.tS_.GetText(275));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x0600132F RID: 4911 RVA: 0x000D6128 File Offset: 0x000D4328
	public void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x06001330 RID: 4912 RVA: 0x000D617C File Offset: 0x000D437C
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && this.CheckGameData(component) && !this.Exists(this.uiObjects[0], component.myID))
				{
					Item_InAppVerwalten component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_InAppVerwalten>();
					component2.game_ = component;
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.genres_ = this.genres_;
					component2.menu_ = this;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x06001331 RID: 4913 RVA: 0x000D6290 File Offset: 0x000D4490
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.playerGame && !script_.typ_bundle && !script_.typ_addon && !script_.typ_addonStandalone && !script_.typ_contractGame && !script_.typ_mmoaddon && !script_.inDevelopment && script_.isOnMarket && ((script_.gameGameplayFeatures[57] && script_.gameGameplayFeatures[23]) || script_.gameTyp == 2);
	}

	// Token: 0x06001332 RID: 4914 RVA: 0x000D6308 File Offset: 0x000D4508
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
				Item_InAppVerwalten component = gameObject.GetComponent<Item_InAppVerwalten>();
				switch (value)
				{
				case 0:
					gameObject.name = component.game_.GetNameSimple();
					break;
				case 1:
				{
					float num = (float)component.game_.date_month;
					num /= 13f;
					gameObject.name = component.game_.date_year.ToString() + num.ToString();
					break;
				}
				case 2:
					gameObject.name = component.game_.reviewTotal.ToString();
					break;
				case 3:
					gameObject.name = component.game_.maingenre.ToString();
					break;
				case 4:
					gameObject.name = component.game_.sellsTotal.ToString();
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

	// Token: 0x06001333 RID: 4915 RVA: 0x0000D27B File Offset: 0x0000B47B
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x0400177A RID: 6010
	public GameObject[] uiPrefabs;

	// Token: 0x0400177B RID: 6011
	public GameObject[] uiObjects;

	// Token: 0x0400177C RID: 6012
	private mainScript mS_;

	// Token: 0x0400177D RID: 6013
	private GameObject main_;

	// Token: 0x0400177E RID: 6014
	private GUI_Main guiMain_;

	// Token: 0x0400177F RID: 6015
	private sfxScript sfx_;

	// Token: 0x04001780 RID: 6016
	private textScript tS_;

	// Token: 0x04001781 RID: 6017
	private genres genres_;

	// Token: 0x04001782 RID: 6018
	private float updateTimer;
}
