using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200014D RID: 333
public class Menu_Dev_UpdateSelectGame : MonoBehaviour
{
	// Token: 0x06000C35 RID: 3125 RVA: 0x00008900 File Offset: 0x00006B00
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000C36 RID: 3126 RVA: 0x00093950 File Offset: 0x00091B50
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
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
	}

	// Token: 0x06000C37 RID: 3127 RVA: 0x00008908 File Offset: 0x00006B08
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000C38 RID: 3128 RVA: 0x00093A18 File Offset: 0x00091C18
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

	// Token: 0x06000C39 RID: 3129 RVA: 0x00093A64 File Offset: 0x00091C64
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_DevGame_GameUpdate>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000C3A RID: 3130 RVA: 0x00008940 File Offset: 0x00006B40
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x06000C3B RID: 3131 RVA: 0x00093AC0 File Offset: 0x00091CC0
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(217));
		list.Add(this.tS_.GetText(277));
		list.Add(this.tS_.GetText(273));
		list.Add(this.tS_.GetText(275));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06000C3C RID: 3132 RVA: 0x00093B8C File Offset: 0x00091D8C
	public void Init(roomScript room_, int art_)
	{
		this.FindScripts();
		this.rS_ = room_;
		this.art = art_;
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x06000C3D RID: 3133 RVA: 0x00093BF0 File Offset: 0x00091DF0
	private void SetData()
	{
		if (!this.rS_)
		{
			return;
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && component.playerGame && !component.inDevelopment && component.isOnMarket && !component.typ_bundle && !component.typ_budget && !component.typ_bundleAddon && !component.typ_goty)
				{
					bool flag = true;
					if (component.portID != -1 && this.uiObjects[7].GetComponent<Toggle>().isOn)
					{
						flag = false;
					}
					if ((component.typ_addon || component.typ_addonStandalone || component.typ_mmoaddon) && this.uiObjects[8].GetComponent<Toggle>().isOn)
					{
						flag = false;
					}
					if (flag)
					{
						string text = component.GetNameSimple();
						this.searchStringA = this.searchStringA.ToLower();
						text = text.ToLower();
						if ((this.uiObjects[6].GetComponent<InputField>().text.Length <= 0 || text.Contains(this.searchStringA)) && this.art == 0 && !this.Exists(this.uiObjects[0], component.myID))
						{
							Item_DevGame_GameUpdate component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_DevGame_GameUpdate>();
							component2.game_ = component;
							component2.mS_ = this.mS_;
							component2.tS_ = this.tS_;
							component2.sfx_ = this.sfx_;
							component2.guiMain_ = this.guiMain_;
							component2.genres_ = this.genres_;
							component2.rS_ = this.rS_;
						}
					}
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x06000C3E RID: 3134 RVA: 0x00093E08 File Offset: 0x00092008
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
				Item_DevGame_GameUpdate component = gameObject.GetComponent<Item_DevGame_GameUpdate>();
				switch (value)
				{
				case 0:
					gameObject.name = component.game_.GetNameSimple();
					break;
				case 1:
				{
					float num = (float)component.game_.date_month;
					num /= 13f;
					gameObject.name = component.game_.date_year.ToString() + num.ToString();
					break;
				}
				case 2:
					gameObject.name = component.game_.reviewTotal.ToString();
					break;
				case 3:
					gameObject.name = component.game_.maingenre.ToString();
					break;
				case 4:
					gameObject.name = component.game_.sellsTotal.ToString();
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

	// Token: 0x06000C3F RID: 3135 RVA: 0x00093F74 File Offset: 0x00092174
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[104]);
		this.guiMain_.uiObjects[104].GetComponent<Menu_Dev_Addon>().Init(this.rS_);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000C40 RID: 3136 RVA: 0x00093FD4 File Offset: 0x000921D4
	public void BUTTON_Search()
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.searchStringA = this.uiObjects[6].GetComponent<InputField>().text;
		this.Init(this.rS_, this.art);
	}

	// Token: 0x06000C41 RID: 3137 RVA: 0x0000894E File Offset: 0x00006B4E
	public void TOGGLE()
	{
		this.Init(this.rS_, this.art);
	}

	// Token: 0x04001091 RID: 4241
	public GameObject[] uiPrefabs;

	// Token: 0x04001092 RID: 4242
	public GameObject[] uiObjects;

	// Token: 0x04001093 RID: 4243
	private mainScript mS_;

	// Token: 0x04001094 RID: 4244
	private GameObject main_;

	// Token: 0x04001095 RID: 4245
	private GUI_Main guiMain_;

	// Token: 0x04001096 RID: 4246
	private sfxScript sfx_;

	// Token: 0x04001097 RID: 4247
	private textScript tS_;

	// Token: 0x04001098 RID: 4248
	private genres genres_;

	// Token: 0x04001099 RID: 4249
	public roomScript rS_;

	// Token: 0x0400109A RID: 4250
	private float updateTimer;

	// Token: 0x0400109B RID: 4251
	private int art;

	// Token: 0x0400109C RID: 4252
	private string searchStringA = "";
}
