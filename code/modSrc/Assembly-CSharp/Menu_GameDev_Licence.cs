using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000151 RID: 337
public class Menu_GameDev_Licence : MonoBehaviour
{
	// Token: 0x06000C67 RID: 3175 RVA: 0x0008598B File Offset: 0x00083B8B
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000C68 RID: 3176 RVA: 0x00085994 File Offset: 0x00083B94
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

	// Token: 0x06000C69 RID: 3177 RVA: 0x00085A5C File Offset: 0x00083C5C
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000C6A RID: 3178 RVA: 0x00085A94 File Offset: 0x00083C94
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

	// Token: 0x06000C6B RID: 3179 RVA: 0x00085AE0 File Offset: 0x00083CE0
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

	// Token: 0x06000C6C RID: 3180 RVA: 0x00085B37 File Offset: 0x00083D37
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x06000C6D RID: 3181 RVA: 0x00085B4C File Offset: 0x00083D4C
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

	// Token: 0x06000C6E RID: 3182 RVA: 0x00085C18 File Offset: 0x00083E18
	private void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x06000C6F RID: 3183 RVA: 0x00085C6C File Offset: 0x00083E6C
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

	// Token: 0x06000C70 RID: 3184 RVA: 0x00085D58 File Offset: 0x00083F58
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

	// Token: 0x06000C71 RID: 3185 RVA: 0x00085ECF File Offset: 0x000840CF
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000C72 RID: 3186 RVA: 0x00085EEA File Offset: 0x000840EA
	public void BUTTON_RemoveLicence()
	{
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().SetLicence(-1);
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x040010B8 RID: 4280
	public GameObject[] uiPrefabs;

	// Token: 0x040010B9 RID: 4281
	public GameObject[] uiObjects;

	// Token: 0x040010BA RID: 4282
	private mainScript mS_;

	// Token: 0x040010BB RID: 4283
	private GameObject main_;

	// Token: 0x040010BC RID: 4284
	private GUI_Main guiMain_;

	// Token: 0x040010BD RID: 4285
	private sfxScript sfx_;

	// Token: 0x040010BE RID: 4286
	private textScript tS_;

	// Token: 0x040010BF RID: 4287
	private licences licences_;

	// Token: 0x040010C0 RID: 4288
	private float updateTimer;
}
