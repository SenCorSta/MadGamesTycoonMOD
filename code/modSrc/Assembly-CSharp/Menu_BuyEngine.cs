using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200018C RID: 396
public class Menu_BuyEngine : MonoBehaviour
{
	// Token: 0x06000F0B RID: 3851 RVA: 0x0009FCE3 File Offset: 0x0009DEE3
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000F0C RID: 3852 RVA: 0x0009FCEC File Offset: 0x0009DEEC
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

	// Token: 0x06000F0D RID: 3853 RVA: 0x0009FDD2 File Offset: 0x0009DFD2
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000F0E RID: 3854 RVA: 0x0009FE0C File Offset: 0x0009E00C
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

	// Token: 0x06000F0F RID: 3855 RVA: 0x0009FE70 File Offset: 0x0009E070
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

	// Token: 0x06000F10 RID: 3856 RVA: 0x0009FECC File Offset: 0x0009E0CC
	public void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.TAB_BuyEngine(0);
	}

	// Token: 0x06000F11 RID: 3857 RVA: 0x0009FEE4 File Offset: 0x0009E0E4
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

	// Token: 0x06000F12 RID: 3858 RVA: 0x000A0008 File Offset: 0x0009E208
	private void Init(bool gekauft)
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData(gekauft);
	}

	// Token: 0x06000F13 RID: 3859 RVA: 0x000A0060 File Offset: 0x0009E260
	private void SetData(bool gekauft)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Engine");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				engineScript component = array[i].GetComponent<engineScript>();
				if (component && component.isUnlocked && component.ownerID != this.mS_.myID && component.gekauft == gekauft && (component.OwnerIsNPC() || (component.EngineFromMitspieler() && component.sellEngine)) && component.myID != 0 && !this.Exists(this.uiObjects[0], component.myID))
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

	// Token: 0x06000F14 RID: 3860 RVA: 0x000A01C4 File Offset: 0x0009E3C4
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

	// Token: 0x06000F15 RID: 3861 RVA: 0x000A0397 File Offset: 0x0009E597
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		if (!this.guiMain_.uiObjects[56].activeSelf)
		{
			this.guiMain_.CloseMenu();
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000F16 RID: 3862 RVA: 0x000A03D2 File Offset: 0x0009E5D2
	public void TAB_BuyEngine(int t)
	{
		this.TAB = t;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.SetTab(this.uiObjects[4], t);
		this.Init(false);
	}

	// Token: 0x06000F17 RID: 3863 RVA: 0x000A0403 File Offset: 0x0009E603
	public void TAB_MyEngines(int t)
	{
		this.TAB = t;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.SetTab(this.uiObjects[4], t);
		this.Init(true);
	}

	// Token: 0x04001351 RID: 4945
	public GameObject[] uiPrefabs;

	// Token: 0x04001352 RID: 4946
	public GameObject[] uiObjects;

	// Token: 0x04001353 RID: 4947
	private mainScript mS_;

	// Token: 0x04001354 RID: 4948
	private GameObject main_;

	// Token: 0x04001355 RID: 4949
	private GUI_Main guiMain_;

	// Token: 0x04001356 RID: 4950
	private sfxScript sfx_;

	// Token: 0x04001357 RID: 4951
	private textScript tS_;

	// Token: 0x04001358 RID: 4952
	private engineFeatures eF_;

	// Token: 0x04001359 RID: 4953
	private genres genres_;

	// Token: 0x0400135A RID: 4954
	private int TAB;

	// Token: 0x0400135B RID: 4955
	private float updateTimer;
}
