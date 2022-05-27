using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200014E RID: 334
public class Menu_Dev_UpdateSelectGame : MonoBehaviour
{
	// Token: 0x06000C4A RID: 3146 RVA: 0x0008438A File Offset: 0x0008258A
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000C4B RID: 3147 RVA: 0x00084394 File Offset: 0x00082594
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

	// Token: 0x06000C4C RID: 3148 RVA: 0x0008445C File Offset: 0x0008265C
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000C4D RID: 3149 RVA: 0x00084494 File Offset: 0x00082694
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

	// Token: 0x06000C4E RID: 3150 RVA: 0x000844E0 File Offset: 0x000826E0
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

	// Token: 0x06000C4F RID: 3151 RVA: 0x0008453C File Offset: 0x0008273C
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x06000C50 RID: 3152 RVA: 0x0008454C File Offset: 0x0008274C
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

	// Token: 0x06000C51 RID: 3153 RVA: 0x00084618 File Offset: 0x00082818
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

	// Token: 0x06000C52 RID: 3154 RVA: 0x0008467C File Offset: 0x0008287C
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
				if (component && this.CheckGameData(component))
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

	// Token: 0x06000C53 RID: 3155 RVA: 0x00084854 File Offset: 0x00082A54
	public bool CheckGameData(gameScript script_)
	{
		return script_ && (script_.ownerID == this.mS_.myID || script_.publisherID == this.mS_.myID) && !script_.inDevelopment && script_.isOnMarket && !script_.typ_bundle && !script_.typ_budget && !script_.typ_bundleAddon && !script_.typ_goty;
	}

	// Token: 0x06000C54 RID: 3156 RVA: 0x000848C4 File Offset: 0x00082AC4
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

	// Token: 0x06000C55 RID: 3157 RVA: 0x00084A30 File Offset: 0x00082C30
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[104]);
		this.guiMain_.uiObjects[104].GetComponent<Menu_Dev_Addon>().Init(this.rS_);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000C56 RID: 3158 RVA: 0x00084A90 File Offset: 0x00082C90
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

	// Token: 0x06000C57 RID: 3159 RVA: 0x00084B10 File Offset: 0x00082D10
	public void TOGGLE()
	{
		this.Init(this.rS_, this.art);
	}

	// Token: 0x04001099 RID: 4249
	public GameObject[] uiPrefabs;

	// Token: 0x0400109A RID: 4250
	public GameObject[] uiObjects;

	// Token: 0x0400109B RID: 4251
	private mainScript mS_;

	// Token: 0x0400109C RID: 4252
	private GameObject main_;

	// Token: 0x0400109D RID: 4253
	private GUI_Main guiMain_;

	// Token: 0x0400109E RID: 4254
	private sfxScript sfx_;

	// Token: 0x0400109F RID: 4255
	private textScript tS_;

	// Token: 0x040010A0 RID: 4256
	private genres genres_;

	// Token: 0x040010A1 RID: 4257
	public roomScript rS_;

	// Token: 0x040010A2 RID: 4258
	private float updateTimer;

	// Token: 0x040010A3 RID: 4259
	private int art;

	// Token: 0x040010A4 RID: 4260
	private string searchStringA = "";
}
