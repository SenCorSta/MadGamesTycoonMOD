using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200018D RID: 397
public class Menu_BuyLicence : MonoBehaviour
{
	// Token: 0x06000F0B RID: 3851 RVA: 0x0000AA6D File Offset: 0x00008C6D
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000F0C RID: 3852 RVA: 0x000ADA1C File Offset: 0x000ABC1C
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
		if (!this.licences_)
		{
			this.licences_ = this.main_.GetComponent<licences>();
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

	// Token: 0x06000F0D RID: 3853 RVA: 0x0000AA75 File Offset: 0x00008C75
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000F0E RID: 3854 RVA: 0x000ADAE4 File Offset: 0x000ABCE4
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

	// Token: 0x06000F0F RID: 3855 RVA: 0x000ADB48 File Offset: 0x000ABD48
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_BuyLicence>().myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000F10 RID: 3856 RVA: 0x0000AAAD File Offset: 0x00008CAD
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.TAB_LicenceBuy(0);
	}

	// Token: 0x06000F11 RID: 3857 RVA: 0x000ADBA0 File Offset: 0x000ABDA0
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(218));
		list.Add(this.tS_.GetText(302));
		list.Add(this.tS_.GetText(304));
		list.Add(this.tS_.GetText(305));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06000F12 RID: 3858 RVA: 0x000ADC6C File Offset: 0x000ABE6C
	private void Init(bool inBesitz)
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData(inBesitz);
	}

	// Token: 0x06000F13 RID: 3859 RVA: 0x000ADCC4 File Offset: 0x000ABEC4
	private void SetData(bool inBesitz)
	{
		for (int i = 0; i < this.licences_.licence_ANGEBOT.Length; i++)
		{
			if (((!inBesitz && this.licences_.licence_ANGEBOT[i] > 0 && this.licences_.licence_GEKAUFT[i] == 0) || (inBesitz && this.licences_.licence_GEKAUFT[i] > 0)) && !this.Exists(this.uiObjects[0], i))
			{
				Item_BuyLicence component = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_BuyLicence>();
				component.myID = i;
				component.licences_ = this.licences_;
				component.mS_ = this.mS_;
				component.tS_ = this.tS_;
				component.sfx_ = this.sfx_;
				component.guiMain_ = this.guiMain_;
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x06000F14 RID: 3860 RVA: 0x000ADDD8 File Offset: 0x000ABFD8
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
				Item_BuyLicence component = gameObject.GetComponent<Item_BuyLicence>();
				switch (value)
				{
				case 0:
					gameObject.name = this.licences_.GetName(component.myID);
					break;
				case 1:
					gameObject.name = this.licences_.GetPrice(component.myID).ToString();
					break;
				case 2:
					gameObject.name = this.licences_.licence_QUALITY[component.myID].ToString();
					break;
				case 3:
					gameObject.name = this.licences_.licence_TYP[component.myID].ToString();
					break;
				case 4:
					gameObject.name = this.licences_.licence_ANGEBOT[component.myID].ToString();
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

	// Token: 0x06000F15 RID: 3861 RVA: 0x0000AAC2 File Offset: 0x00008CC2
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000F16 RID: 3862 RVA: 0x0000AADD File Offset: 0x00008CDD
	public void TAB_LicenceBuy(int t)
	{
		this.TAB = t;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.SetTab(this.uiObjects[4], t);
		this.Init(false);
	}

	// Token: 0x06000F17 RID: 3863 RVA: 0x0000AB0E File Offset: 0x00008D0E
	public void TAB_MyLicence(int t)
	{
		this.TAB = t;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.SetTab(this.uiObjects[4], t);
		this.Init(true);
	}

	// Token: 0x0400135D RID: 4957
	public GameObject[] uiPrefabs;

	// Token: 0x0400135E RID: 4958
	public GameObject[] uiObjects;

	// Token: 0x0400135F RID: 4959
	private mainScript mS_;

	// Token: 0x04001360 RID: 4960
	private GameObject main_;

	// Token: 0x04001361 RID: 4961
	private GUI_Main guiMain_;

	// Token: 0x04001362 RID: 4962
	private sfxScript sfx_;

	// Token: 0x04001363 RID: 4963
	private textScript tS_;

	// Token: 0x04001364 RID: 4964
	private licences licences_;

	// Token: 0x04001365 RID: 4965
	private int TAB;

	// Token: 0x04001366 RID: 4966
	private float updateTimer;
}
