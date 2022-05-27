using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000150 RID: 336
public class Menu_GameDev_Licence : MonoBehaviour
{
	// Token: 0x06000C51 RID: 3153 RVA: 0x00008A11 File Offset: 0x00006C11
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000C52 RID: 3154 RVA: 0x00094DEC File Offset: 0x00092FEC
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

	// Token: 0x06000C53 RID: 3155 RVA: 0x00008A19 File Offset: 0x00006C19
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000C54 RID: 3156 RVA: 0x00094EB4 File Offset: 0x000930B4
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

	// Token: 0x06000C55 RID: 3157 RVA: 0x00094F00 File Offset: 0x00093100
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_DevGame_Licence>().myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000C56 RID: 3158 RVA: 0x00008A51 File Offset: 0x00006C51
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x06000C57 RID: 3159 RVA: 0x00094F58 File Offset: 0x00093158
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

	// Token: 0x06000C58 RID: 3160 RVA: 0x00095024 File Offset: 0x00093224
	private void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x06000C59 RID: 3161 RVA: 0x00095078 File Offset: 0x00093278
	private void SetData()
	{
		for (int i = 0; i < this.licences_.licence_ANGEBOT.Length; i++)
		{
			if (this.licences_.licence_GEKAUFT[i] > 0 && !this.Exists(this.uiObjects[0], i))
			{
				Item_DevGame_Licence component = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_DevGame_Licence>();
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

	// Token: 0x06000C5A RID: 3162 RVA: 0x00095164 File Offset: 0x00093364
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
				Item_DevGame_Licence component = gameObject.GetComponent<Item_DevGame_Licence>();
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

	// Token: 0x06000C5B RID: 3163 RVA: 0x00008A65 File Offset: 0x00006C65
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000C5C RID: 3164 RVA: 0x00008A80 File Offset: 0x00006C80
	public void BUTTON_RemoveLicence()
	{
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().SetLicence(-1);
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x040010B0 RID: 4272
	public GameObject[] uiPrefabs;

	// Token: 0x040010B1 RID: 4273
	public GameObject[] uiObjects;

	// Token: 0x040010B2 RID: 4274
	private mainScript mS_;

	// Token: 0x040010B3 RID: 4275
	private GameObject main_;

	// Token: 0x040010B4 RID: 4276
	private GUI_Main guiMain_;

	// Token: 0x040010B5 RID: 4277
	private sfxScript sfx_;

	// Token: 0x040010B6 RID: 4278
	private textScript tS_;

	// Token: 0x040010B7 RID: 4279
	private licences licences_;

	// Token: 0x040010B8 RID: 4280
	private float updateTimer;
}
