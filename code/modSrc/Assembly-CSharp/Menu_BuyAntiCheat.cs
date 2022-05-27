using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000189 RID: 393
public class Menu_BuyAntiCheat : MonoBehaviour
{
	// Token: 0x06000EDE RID: 3806 RVA: 0x0009E8E1 File Offset: 0x0009CAE1
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000EDF RID: 3807 RVA: 0x0009E8EC File Offset: 0x0009CAEC
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
	}

	// Token: 0x06000EE0 RID: 3808 RVA: 0x0009E996 File Offset: 0x0009CB96
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000EE1 RID: 3809 RVA: 0x0009E9D0 File Offset: 0x0009CBD0
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

	// Token: 0x06000EE2 RID: 3810 RVA: 0x0009EA34 File Offset: 0x0009CC34
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_BuyAntiCheat>().acS_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000EE3 RID: 3811 RVA: 0x0009EA90 File Offset: 0x0009CC90
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.TAB_AntiCheatBuy(0);
	}

	// Token: 0x06000EE4 RID: 3812 RVA: 0x0009EAA8 File Offset: 0x0009CCA8
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(218));
		list.Add(this.tS_.GetText(286));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06000EE5 RID: 3813 RVA: 0x0009EB48 File Offset: 0x0009CD48
	private void Init(bool inBesitz)
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData(inBesitz);
	}

	// Token: 0x06000EE6 RID: 3814 RVA: 0x0009EBA0 File Offset: 0x0009CDA0
	private void SetData(bool inBesitz)
	{
		bool isOn = this.uiObjects[5].GetComponent<Toggle>().isOn;
		GameObject[] array = GameObject.FindGameObjectsWithTag("AntiCheat");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				antiCheatScript component = array[i].GetComponent<antiCheatScript>();
				if (component && component.isUnlocked && component.inBesitz == inBesitz && (component.effekt > 0f || !isOn) && !this.Exists(this.uiObjects[0], component.myID))
				{
					Item_BuyAntiCheat component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_BuyAntiCheat>();
					component2.acS_ = component;
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[6]);
	}

	// Token: 0x06000EE7 RID: 3815 RVA: 0x0009ECD0 File Offset: 0x0009CED0
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
				Item_BuyAntiCheat component = gameObject.GetComponent<Item_BuyAntiCheat>();
				switch (value)
				{
				case 0:
					gameObject.name = component.acS_.GetName();
					break;
				case 1:
					gameObject.name = component.acS_.GetPrice().ToString();
					break;
				case 2:
					gameObject.name = component.acS_.effekt.ToString();
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

	// Token: 0x06000EE8 RID: 3816 RVA: 0x0009EDD8 File Offset: 0x0009CFD8
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		if (!this.guiMain_.uiObjects[56].activeSelf && !this.guiMain_.uiObjects[193].activeSelf && !this.guiMain_.uiObjects[247].activeSelf && !this.guiMain_.uiObjects[365].activeSelf)
		{
			this.guiMain_.CloseMenu();
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000EE9 RID: 3817 RVA: 0x0009EE66 File Offset: 0x0009D066
	public void TAB_AntiCheatBuy(int t)
	{
		this.TAB = t;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.SetTab(this.uiObjects[4], t);
		this.Init(false);
	}

	// Token: 0x06000EEA RID: 3818 RVA: 0x0009EE97 File Offset: 0x0009D097
	public void TAB_MyAntiCheat(int t)
	{
		this.TAB = t;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.SetTab(this.uiObjects[4], t);
		this.Init(true);
	}

	// Token: 0x06000EEB RID: 3819 RVA: 0x0009EEC8 File Offset: 0x0009D0C8
	public void TOGGLE_Veraltet()
	{
		int tab = this.TAB;
		if (tab == 0)
		{
			this.TAB_AntiCheatBuy(0);
			return;
		}
		if (tab != 1)
		{
			return;
		}
		this.TAB_MyAntiCheat(1);
	}

	// Token: 0x04001336 RID: 4918
	public GameObject[] uiPrefabs;

	// Token: 0x04001337 RID: 4919
	public GameObject[] uiObjects;

	// Token: 0x04001338 RID: 4920
	private mainScript mS_;

	// Token: 0x04001339 RID: 4921
	private GameObject main_;

	// Token: 0x0400133A RID: 4922
	private GUI_Main guiMain_;

	// Token: 0x0400133B RID: 4923
	private sfxScript sfx_;

	// Token: 0x0400133C RID: 4924
	private textScript tS_;

	// Token: 0x0400133D RID: 4925
	private int TAB;

	// Token: 0x0400133E RID: 4926
	private float updateTimer;
}
