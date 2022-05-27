using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200022A RID: 554
public class Menu_Statistics_Engines : MonoBehaviour
{
	// Token: 0x06001554 RID: 5460 RVA: 0x000DA017 File Offset: 0x000D8217
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001555 RID: 5461 RVA: 0x000DA020 File Offset: 0x000D8220
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
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
	}

	// Token: 0x06001556 RID: 5462 RVA: 0x000DA106 File Offset: 0x000D8306
	private void OnEnable()
	{
		this.InitDropdowns();
		this.Init(this.uiObjects[5].GetComponent<Toggle>().isOn);
	}

	// Token: 0x06001557 RID: 5463 RVA: 0x000DA128 File Offset: 0x000D8328
	public void InitDropdowns()
	{
		this.FindScripts();
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(4));
		list.Add(this.tS_.GetText(245));
		list.Add(this.tS_.GetText(160));
		list.Add(this.tS_.GetText(261));
		list.Add(this.tS_.GetText(88));
		list.Add(this.tS_.GetText(260));
		list.Add(this.tS_.GetText(268));
		list.Add(this.tS_.GetText(258));
		list.Add(this.tS_.GetText(1218));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06001558 RID: 5464 RVA: 0x000DA261 File Offset: 0x000D8461
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06001559 RID: 5465 RVA: 0x000DA29C File Offset: 0x000D849C
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
		this.SetData(this.uiObjects[5].GetComponent<Toggle>().isOn);
	}

	// Token: 0x0600155A RID: 5466 RVA: 0x000DA2FC File Offset: 0x000D84FC
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_Stats_Engine>().eS_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600155B RID: 5467 RVA: 0x000DA358 File Offset: 0x000D8558
	public void Init(bool nurEigeneEngines)
	{
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData(nurEigeneEngines);
	}

	// Token: 0x0600155C RID: 5468 RVA: 0x000DA3A8 File Offset: 0x000D85A8
	private void SetData(bool nurEigeneEngines)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Engine");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				engineScript component = array[i].GetComponent<engineScript>();
				if (component && component.myID != 0 && ((component.ownerID == this.mS_.myID && (component.Complete() || component.updating)) || (component.ownerID != this.mS_.myID && component.isUnlocked)) && (!nurEigeneEngines || (component.ownerID == this.mS_.myID && nurEigeneEngines)) && !this.Exists(this.uiObjects[0], component.myID))
				{
					Item_Stats_Engine component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_Stats_Engine>();
					component2.eS_ = component;
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.eF_ = this.eF_;
					component2.genres_ = this.genres_;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[4]);
	}

	// Token: 0x0600155D RID: 5469 RVA: 0x000DA524 File Offset: 0x000D8724
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
				Item_Stats_Engine component = gameObject.GetComponent<Item_Stats_Engine>();
				switch (value)
				{
				case 0:
					gameObject.name = component.eS_.GetName();
					break;
				case 1:
					gameObject.name = component.eS_.GetTechLevel().ToString();
					break;
				case 2:
					gameObject.name = component.eS_.spezialgenre.ToString();
					break;
				case 3:
					gameObject.name = component.eS_.GetFeaturesAmount().ToString();
					break;
				case 4:
					gameObject.name = component.eS_.GetGamesAmount().ToString();
					break;
				case 5:
					gameObject.name = component.eS_.preis.ToString();
					break;
				case 6:
					gameObject.name = component.eS_.gewinnbeteiligung.ToString();
					break;
				case 7:
					gameObject.name = component.eS_.GetVerkaufteLizenzen().ToString();
					break;
				case 8:
					if (component.eS_.ownerID == this.mS_.myID)
					{
						gameObject.name = "1";
					}
					else
					{
						gameObject.name = "0";
					}
					break;
				case 9:
					gameObject.name = component.eS_.spezialplatform.ToString();
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

	// Token: 0x0600155E RID: 5470 RVA: 0x000DA734 File Offset: 0x000D8934
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.CloseMenu();
	}

	// Token: 0x0600155F RID: 5471 RVA: 0x000DA75A File Offset: 0x000D895A
	public void TOGGLE_EigeneEngines()
	{
		this.Init(this.uiObjects[5].GetComponent<Toggle>().isOn);
	}

	// Token: 0x04001947 RID: 6471
	private mainScript mS_;

	// Token: 0x04001948 RID: 6472
	private GameObject main_;

	// Token: 0x04001949 RID: 6473
	private GUI_Main guiMain_;

	// Token: 0x0400194A RID: 6474
	private sfxScript sfx_;

	// Token: 0x0400194B RID: 6475
	private textScript tS_;

	// Token: 0x0400194C RID: 6476
	private engineFeatures eF_;

	// Token: 0x0400194D RID: 6477
	private genres genres_;

	// Token: 0x0400194E RID: 6478
	public GameObject[] uiPrefabs;

	// Token: 0x0400194F RID: 6479
	public GameObject[] uiObjects;

	// Token: 0x04001950 RID: 6480
	private float updateTimer;
}
