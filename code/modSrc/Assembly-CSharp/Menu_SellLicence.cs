using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200019A RID: 410
public class Menu_SellLicence : MonoBehaviour
{
	// Token: 0x06000F8D RID: 3981 RVA: 0x000A593B File Offset: 0x000A3B3B
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000F8E RID: 3982 RVA: 0x000A5944 File Offset: 0x000A3B44
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

	// Token: 0x06000F8F RID: 3983 RVA: 0x000A5A0C File Offset: 0x000A3C0C
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000F90 RID: 3984 RVA: 0x000A5A44 File Offset: 0x000A3C44
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

	// Token: 0x06000F91 RID: 3985 RVA: 0x000A5A90 File Offset: 0x000A3C90
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_SellLicence>().myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000F92 RID: 3986 RVA: 0x000A5AE7 File Offset: 0x000A3CE7
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x06000F93 RID: 3987 RVA: 0x000A5AFC File Offset: 0x000A3CFC
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(88));
		list.Add(this.tS_.GetText(302));
		list.Add(this.tS_.GetText(304));
		list.Add(this.tS_.GetText(305));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06000F94 RID: 3988 RVA: 0x000A5BC8 File Offset: 0x000A3DC8
	public void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x06000F95 RID: 3989 RVA: 0x000A5C1C File Offset: 0x000A3E1C
	private void SetData()
	{
		for (int i = 0; i < this.licences_.licence_GEKAUFT.Length; i++)
		{
			if (this.licences_.licence_GEKAUFT[i] > 0 && !this.Exists(this.uiObjects[0], i))
			{
				Item_SellLicence component = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_SellLicence>();
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

	// Token: 0x06000F96 RID: 3990 RVA: 0x000A5D08 File Offset: 0x000A3F08
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
				Item_SellLicence component = gameObject.GetComponent<Item_SellLicence>();
				switch (value)
				{
				case 0:
					gameObject.name = this.licences_.GetName(component.myID);
					break;
				case 1:
					gameObject.name = this.licences_.GetSellPrice(component.myID).ToString();
					break;
				case 2:
					gameObject.name = this.licences_.licence_QUALITY[component.myID].ToString();
					break;
				case 3:
					gameObject.name = this.licences_.licence_TYP[component.myID].ToString();
					break;
				case 4:
					gameObject.name = this.licences_.licence_GEKAUFT[component.myID].ToString();
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

	// Token: 0x06000F97 RID: 3991 RVA: 0x000A5E7F File Offset: 0x000A407F
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x040013EA RID: 5098
	public GameObject[] uiPrefabs;

	// Token: 0x040013EB RID: 5099
	public GameObject[] uiObjects;

	// Token: 0x040013EC RID: 5100
	private mainScript mS_;

	// Token: 0x040013ED RID: 5101
	private GameObject main_;

	// Token: 0x040013EE RID: 5102
	private GUI_Main guiMain_;

	// Token: 0x040013EF RID: 5103
	private sfxScript sfx_;

	// Token: 0x040013F0 RID: 5104
	private textScript tS_;

	// Token: 0x040013F1 RID: 5105
	private licences licences_;

	// Token: 0x040013F2 RID: 5106
	private float updateTimer;
}
