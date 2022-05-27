using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000188 RID: 392
public class Menu_BuyAntiCheat : MonoBehaviour
{
	// Token: 0x06000EC6 RID: 3782 RVA: 0x0000A708 File Offset: 0x00008908
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000EC7 RID: 3783 RVA: 0x000ABCF4 File Offset: 0x000A9EF4
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

	// Token: 0x06000EC8 RID: 3784 RVA: 0x0000A710 File Offset: 0x00008910
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000EC9 RID: 3785 RVA: 0x000ABDA0 File Offset: 0x000A9FA0
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

	// Token: 0x06000ECA RID: 3786 RVA: 0x000ABE04 File Offset: 0x000AA004
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

	// Token: 0x06000ECB RID: 3787 RVA: 0x0000A748 File Offset: 0x00008948
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.TAB_AntiCheatBuy(0);
	}

	// Token: 0x06000ECC RID: 3788 RVA: 0x000ABE60 File Offset: 0x000AA060
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

	// Token: 0x06000ECD RID: 3789 RVA: 0x000ABF00 File Offset: 0x000AA100
	private void Init(bool inBesitz)
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData(inBesitz);
	}

	// Token: 0x06000ECE RID: 3790 RVA: 0x000ABF58 File Offset: 0x000AA158
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

	// Token: 0x06000ECF RID: 3791 RVA: 0x000AC088 File Offset: 0x000AA288
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

	// Token: 0x06000ED0 RID: 3792 RVA: 0x000AC190 File Offset: 0x000AA390
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		if (!this.guiMain_.uiObjects[56].activeSelf && !this.guiMain_.uiObjects[193].activeSelf && !this.guiMain_.uiObjects[247].activeSelf && !this.guiMain_.uiObjects[365].activeSelf)
		{
			this.guiMain_.CloseMenu();
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000ED1 RID: 3793 RVA: 0x0000A75D File Offset: 0x0000895D
	public void TAB_AntiCheatBuy(int t)
	{
		this.TAB = t;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.SetTab(this.uiObjects[4], t);
		this.Init(false);
	}

	// Token: 0x06000ED2 RID: 3794 RVA: 0x0000A78E File Offset: 0x0000898E
	public void TAB_MyAntiCheat(int t)
	{
		this.TAB = t;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.SetTab(this.uiObjects[4], t);
		this.Init(true);
	}

	// Token: 0x06000ED3 RID: 3795 RVA: 0x000AC220 File Offset: 0x000AA420
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

	// Token: 0x0400132D RID: 4909
	public GameObject[] uiPrefabs;

	// Token: 0x0400132E RID: 4910
	public GameObject[] uiObjects;

	// Token: 0x0400132F RID: 4911
	private mainScript mS_;

	// Token: 0x04001330 RID: 4912
	private GameObject main_;

	// Token: 0x04001331 RID: 4913
	private GUI_Main guiMain_;

	// Token: 0x04001332 RID: 4914
	private sfxScript sfx_;

	// Token: 0x04001333 RID: 4915
	private textScript tS_;

	// Token: 0x04001334 RID: 4916
	private int TAB;

	// Token: 0x04001335 RID: 4917
	private float updateTimer;
}
