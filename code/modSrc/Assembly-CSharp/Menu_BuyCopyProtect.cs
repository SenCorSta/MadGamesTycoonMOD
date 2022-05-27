using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200018A RID: 394
public class Menu_BuyCopyProtect : MonoBehaviour
{
	// Token: 0x06000EED RID: 3821 RVA: 0x0009EEFF File Offset: 0x0009D0FF
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000EEE RID: 3822 RVA: 0x0009EF08 File Offset: 0x0009D108
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

	// Token: 0x06000EEF RID: 3823 RVA: 0x0009EFB2 File Offset: 0x0009D1B2
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000EF0 RID: 3824 RVA: 0x0009EFEC File Offset: 0x0009D1EC
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

	// Token: 0x06000EF1 RID: 3825 RVA: 0x0009F050 File Offset: 0x0009D250
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_BuyCopyProtect>().cpS_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000EF2 RID: 3826 RVA: 0x0009F0AC File Offset: 0x0009D2AC
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.TAB_CopyProtectBuy(0);
	}

	// Token: 0x06000EF3 RID: 3827 RVA: 0x0009F0C4 File Offset: 0x0009D2C4
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

	// Token: 0x06000EF4 RID: 3828 RVA: 0x0009F164 File Offset: 0x0009D364
	private void Init(bool inBesitz)
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData(inBesitz);
	}

	// Token: 0x06000EF5 RID: 3829 RVA: 0x0009F1BC File Offset: 0x0009D3BC
	private void SetData(bool inBesitz)
	{
		bool isOn = this.uiObjects[5].GetComponent<Toggle>().isOn;
		GameObject[] array = GameObject.FindGameObjectsWithTag("CopyProtect");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				copyProtectScript component = array[i].GetComponent<copyProtectScript>();
				if (component && component.isUnlocked && component.inBesitz == inBesitz && (component.effekt > 0f || !isOn) && !this.Exists(this.uiObjects[0], component.myID))
				{
					Item_BuyCopyProtect component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_BuyCopyProtect>();
					component2.cpS_ = component;
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

	// Token: 0x06000EF6 RID: 3830 RVA: 0x0009F2EC File Offset: 0x0009D4EC
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
				Item_BuyCopyProtect component = gameObject.GetComponent<Item_BuyCopyProtect>();
				switch (value)
				{
				case 0:
					gameObject.name = component.cpS_.GetName();
					break;
				case 1:
					gameObject.name = component.cpS_.GetPrice().ToString();
					break;
				case 2:
					gameObject.name = component.cpS_.effekt.ToString();
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

	// Token: 0x06000EF7 RID: 3831 RVA: 0x0009F3F4 File Offset: 0x0009D5F4
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		if (!this.guiMain_.uiObjects[56].activeSelf && !this.guiMain_.uiObjects[193].activeSelf && !this.guiMain_.uiObjects[247].activeSelf && !this.guiMain_.uiObjects[365].activeSelf)
		{
			this.guiMain_.CloseMenu();
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000EF8 RID: 3832 RVA: 0x0009F482 File Offset: 0x0009D682
	public void TAB_CopyProtectBuy(int t)
	{
		this.TAB = t;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.SetTab(this.uiObjects[4], t);
		this.Init(false);
	}

	// Token: 0x06000EF9 RID: 3833 RVA: 0x0009F4B3 File Offset: 0x0009D6B3
	public void TAB_MyCopyProtect(int t)
	{
		this.TAB = t;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.SetTab(this.uiObjects[4], t);
		this.Init(true);
	}

	// Token: 0x06000EFA RID: 3834 RVA: 0x0009F4E4 File Offset: 0x0009D6E4
	public void TOGGLE_Veraltet()
	{
		int tab = this.TAB;
		if (tab == 0)
		{
			this.TAB_CopyProtectBuy(0);
			return;
		}
		if (tab != 1)
		{
			return;
		}
		this.TAB_MyCopyProtect(1);
	}

	// Token: 0x0400133F RID: 4927
	public GameObject[] uiPrefabs;

	// Token: 0x04001340 RID: 4928
	public GameObject[] uiObjects;

	// Token: 0x04001341 RID: 4929
	private mainScript mS_;

	// Token: 0x04001342 RID: 4930
	private GameObject main_;

	// Token: 0x04001343 RID: 4931
	private GUI_Main guiMain_;

	// Token: 0x04001344 RID: 4932
	private sfxScript sfx_;

	// Token: 0x04001345 RID: 4933
	private textScript tS_;

	// Token: 0x04001346 RID: 4934
	private int TAB;

	// Token: 0x04001347 RID: 4935
	private float updateTimer;
}
