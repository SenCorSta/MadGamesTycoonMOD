using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001FA RID: 506
public class Menu_InAppVerwalten : MonoBehaviour
{
	// Token: 0x06001343 RID: 4931 RVA: 0x000CB9D4 File Offset: 0x000C9BD4
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001344 RID: 4932 RVA: 0x000CB9DC File Offset: 0x000C9BDC
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

	// Token: 0x06001345 RID: 4933 RVA: 0x000CBAA4 File Offset: 0x000C9CA4
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06001346 RID: 4934 RVA: 0x000CBADC File Offset: 0x000C9CDC
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

	// Token: 0x06001347 RID: 4935 RVA: 0x000CBB28 File Offset: 0x000C9D28
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

	// Token: 0x06001348 RID: 4936 RVA: 0x000CBB84 File Offset: 0x000C9D84
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x06001349 RID: 4937 RVA: 0x000CBB98 File Offset: 0x000C9D98
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

	// Token: 0x0600134A RID: 4938 RVA: 0x000CBC64 File Offset: 0x000C9E64
	public void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x0600134B RID: 4939 RVA: 0x000CBCB8 File Offset: 0x000C9EB8
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

	// Token: 0x0600134C RID: 4940 RVA: 0x000CBDCC File Offset: 0x000C9FCC
	public bool CheckGameData(gameScript script_)
	{
		return script_ && (script_.ownerID == this.mS_.myID || script_.publisherID == this.mS_.myID) && !script_.typ_bundle && !script_.typ_addon && !script_.typ_addonStandalone && !script_.typ_contractGame && !script_.typ_mmoaddon && !script_.inDevelopment && script_.isOnMarket && ((script_.gameGameplayFeatures[57] && script_.gameGameplayFeatures[23]) || script_.gameTyp == 2);
	}

	// Token: 0x0600134D RID: 4941 RVA: 0x000CBE64 File Offset: 0x000CA064
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

	// Token: 0x0600134E RID: 4942 RVA: 0x000CBFCF File Offset: 0x000CA1CF
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001783 RID: 6019
	public GameObject[] uiPrefabs;

	// Token: 0x04001784 RID: 6020
	public GameObject[] uiObjects;

	// Token: 0x04001785 RID: 6021
	private mainScript mS_;

	// Token: 0x04001786 RID: 6022
	private GameObject main_;

	// Token: 0x04001787 RID: 6023
	private GUI_Main guiMain_;

	// Token: 0x04001788 RID: 6024
	private sfxScript sfx_;

	// Token: 0x04001789 RID: 6025
	private textScript tS_;

	// Token: 0x0400178A RID: 6026
	private genres genres_;

	// Token: 0x0400178B RID: 6027
	private float updateTimer;
}
