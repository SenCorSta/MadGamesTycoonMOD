using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000229 RID: 553
public class Menu_Statistics_Engines : MonoBehaviour
{
	// Token: 0x06001536 RID: 5430 RVA: 0x0000E94F File Offset: 0x0000CB4F
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001537 RID: 5431 RVA: 0x000E2B04 File Offset: 0x000E0D04
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

	// Token: 0x06001538 RID: 5432 RVA: 0x0000E957 File Offset: 0x0000CB57
	private void OnEnable()
	{
		this.InitDropdowns();
		this.Init(this.uiObjects[5].GetComponent<Toggle>().isOn);
	}

	// Token: 0x06001539 RID: 5433 RVA: 0x000E2BEC File Offset: 0x000E0DEC
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

	// Token: 0x0600153A RID: 5434 RVA: 0x0000E977 File Offset: 0x0000CB77
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x0600153B RID: 5435 RVA: 0x000E2D28 File Offset: 0x000E0F28
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

	// Token: 0x0600153C RID: 5436 RVA: 0x000E2D88 File Offset: 0x000E0F88
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

	// Token: 0x0600153D RID: 5437 RVA: 0x000E2DE4 File Offset: 0x000E0FE4
	public void Init(bool nurEigeneEngines)
	{
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData(nurEigeneEngines);
	}

	// Token: 0x0600153E RID: 5438 RVA: 0x000E2E34 File Offset: 0x000E1034
	private void SetData(bool nurEigeneEngines)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Engine");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				engineScript component = array[i].GetComponent<engineScript>();
				if (component && component.myID != 0 && ((component.playerEngine && (component.Complete() || component.updating)) || (!component.playerEngine && component.isUnlocked)) && (!nurEigeneEngines || (component.playerEngine && nurEigeneEngines)) && !this.Exists(this.uiObjects[0], component.myID))
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

	// Token: 0x0600153F RID: 5439 RVA: 0x000E2F90 File Offset: 0x000E1190
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
					if (component.eS_.playerEngine)
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

	// Token: 0x06001540 RID: 5440 RVA: 0x0000E9AF File Offset: 0x0000CBAF
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.CloseMenu();
	}

	// Token: 0x06001541 RID: 5441 RVA: 0x0000E9D5 File Offset: 0x0000CBD5
	public void TOGGLE_EigeneEngines()
	{
		this.Init(this.uiObjects[5].GetComponent<Toggle>().isOn);
	}

	// Token: 0x04001940 RID: 6464
	private mainScript mS_;

	// Token: 0x04001941 RID: 6465
	private GameObject main_;

	// Token: 0x04001942 RID: 6466
	private GUI_Main guiMain_;

	// Token: 0x04001943 RID: 6467
	private sfxScript sfx_;

	// Token: 0x04001944 RID: 6468
	private textScript tS_;

	// Token: 0x04001945 RID: 6469
	private engineFeatures eF_;

	// Token: 0x04001946 RID: 6470
	private genres genres_;

	// Token: 0x04001947 RID: 6471
	public GameObject[] uiPrefabs;

	// Token: 0x04001948 RID: 6472
	public GameObject[] uiObjects;

	// Token: 0x04001949 RID: 6473
	private float updateTimer;
}
