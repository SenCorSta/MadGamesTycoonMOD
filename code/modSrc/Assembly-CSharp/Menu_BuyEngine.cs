using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200018B RID: 395
public class Menu_BuyEngine : MonoBehaviour
{
	// Token: 0x06000EF3 RID: 3827 RVA: 0x0000A92D File Offset: 0x00008B2D
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000EF4 RID: 3828 RVA: 0x000ACECC File Offset: 0x000AB0CC
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

	// Token: 0x06000EF5 RID: 3829 RVA: 0x0000A935 File Offset: 0x00008B35
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000EF6 RID: 3830 RVA: 0x000ACFB4 File Offset: 0x000AB1B4
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
		int tab = this.TAB;
		if (tab == 0)
		{
			this.SetData(false);
			return;
		}
		if (tab != 1)
		{
			return;
		}
		this.SetData(true);
	}

	// Token: 0x06000EF7 RID: 3831 RVA: 0x000AD018 File Offset: 0x000AB218
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_BuyEngine>().eS_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000EF8 RID: 3832 RVA: 0x0000A96D File Offset: 0x00008B6D
	public void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.TAB_BuyEngine(0);
	}

	// Token: 0x06000EF9 RID: 3833 RVA: 0x000AD074 File Offset: 0x000AB274
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
		list.Add(this.tS_.GetText(1218));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06000EFA RID: 3834 RVA: 0x000AD198 File Offset: 0x000AB398
	private void Init(bool gekauft)
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData(gekauft);
	}

	// Token: 0x06000EFB RID: 3835 RVA: 0x000AD1F0 File Offset: 0x000AB3F0
	private void SetData(bool gekauft)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Engine");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				engineScript component = array[i].GetComponent<engineScript>();
				if (component && component.isUnlocked && !component.playerEngine && component.gekauft == gekauft && ((!component.playerEngine && component.multiplayerSlot == -1) || (component.multiplayerSlot != -1 && component.sellEngine)) && component.myID != 0 && !this.Exists(this.uiObjects[0], component.myID))
				{
					Item_BuyEngine component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_BuyEngine>();
					component2.eS_ = component;
					component2.eF_ = this.eF_;
					component2.genres_ = this.genres_;
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x06000EFC RID: 3836 RVA: 0x000AD354 File Offset: 0x000AB554
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
				Item_BuyEngine component = gameObject.GetComponent<Item_BuyEngine>();
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

	// Token: 0x06000EFD RID: 3837 RVA: 0x0000A982 File Offset: 0x00008B82
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		if (!this.guiMain_.uiObjects[56].activeSelf)
		{
			this.guiMain_.CloseMenu();
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000EFE RID: 3838 RVA: 0x0000A9BD File Offset: 0x00008BBD
	public void TAB_BuyEngine(int t)
	{
		this.TAB = t;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.SetTab(this.uiObjects[4], t);
		this.Init(false);
	}

	// Token: 0x06000EFF RID: 3839 RVA: 0x0000A9EE File Offset: 0x00008BEE
	public void TAB_MyEngines(int t)
	{
		this.TAB = t;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.SetTab(this.uiObjects[4], t);
		this.Init(true);
	}

	// Token: 0x04001348 RID: 4936
	public GameObject[] uiPrefabs;

	// Token: 0x04001349 RID: 4937
	public GameObject[] uiObjects;

	// Token: 0x0400134A RID: 4938
	private mainScript mS_;

	// Token: 0x0400134B RID: 4939
	private GameObject main_;

	// Token: 0x0400134C RID: 4940
	private GUI_Main guiMain_;

	// Token: 0x0400134D RID: 4941
	private sfxScript sfx_;

	// Token: 0x0400134E RID: 4942
	private textScript tS_;

	// Token: 0x0400134F RID: 4943
	private engineFeatures eF_;

	// Token: 0x04001350 RID: 4944
	private genres genres_;

	// Token: 0x04001351 RID: 4945
	private int TAB;

	// Token: 0x04001352 RID: 4946
	private float updateTimer;
}
