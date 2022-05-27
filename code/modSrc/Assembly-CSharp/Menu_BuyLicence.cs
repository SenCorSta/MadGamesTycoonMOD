using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200018E RID: 398
public class Menu_BuyLicence : MonoBehaviour
{
	// Token: 0x06000F23 RID: 3875 RVA: 0x000A0981 File Offset: 0x0009EB81
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000F24 RID: 3876 RVA: 0x000A098C File Offset: 0x0009EB8C
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

	// Token: 0x06000F25 RID: 3877 RVA: 0x000A0A54 File Offset: 0x0009EC54
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000F26 RID: 3878 RVA: 0x000A0A8C File Offset: 0x0009EC8C
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

	// Token: 0x06000F27 RID: 3879 RVA: 0x000A0AF0 File Offset: 0x0009ECF0
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

	// Token: 0x06000F28 RID: 3880 RVA: 0x000A0B47 File Offset: 0x0009ED47
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.TAB_LicenceBuy(0);
	}

	// Token: 0x06000F29 RID: 3881 RVA: 0x000A0B5C File Offset: 0x0009ED5C
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

	// Token: 0x06000F2A RID: 3882 RVA: 0x000A0C28 File Offset: 0x0009EE28
	private void Init(bool inBesitz)
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData(inBesitz);
	}

	// Token: 0x06000F2B RID: 3883 RVA: 0x000A0C80 File Offset: 0x0009EE80
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

	// Token: 0x06000F2C RID: 3884 RVA: 0x000A0D94 File Offset: 0x0009EF94
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
					if (this.licences_.licence_GEKAUFT[component.myID] > 0)
					{
						gameObject.name = this.licences_.licence_GEKAUFT[component.myID].ToString();
					}
					else
					{
						gameObject.name = this.licences_.licence_ANGEBOT[component.myID].ToString();
					}
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

	// Token: 0x06000F2D RID: 3885 RVA: 0x000A0F4B File Offset: 0x0009F14B
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000F2E RID: 3886 RVA: 0x000A0F66 File Offset: 0x0009F166
	public void TAB_LicenceBuy(int t)
	{
		this.TAB = t;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.SetTab(this.uiObjects[4], t);
		this.Init(false);
	}

	// Token: 0x06000F2F RID: 3887 RVA: 0x000A0F97 File Offset: 0x0009F197
	public void TAB_MyLicence(int t)
	{
		this.TAB = t;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.SetTab(this.uiObjects[4], t);
		this.Init(true);
	}

	// Token: 0x04001366 RID: 4966
	public GameObject[] uiPrefabs;

	// Token: 0x04001367 RID: 4967
	public GameObject[] uiObjects;

	// Token: 0x04001368 RID: 4968
	private mainScript mS_;

	// Token: 0x04001369 RID: 4969
	private GameObject main_;

	// Token: 0x0400136A RID: 4970
	private GUI_Main guiMain_;

	// Token: 0x0400136B RID: 4971
	private sfxScript sfx_;

	// Token: 0x0400136C RID: 4972
	private textScript tS_;

	// Token: 0x0400136D RID: 4973
	private licences licences_;

	// Token: 0x0400136E RID: 4974
	private int TAB;

	// Token: 0x0400136F RID: 4975
	private float updateTimer;
}
