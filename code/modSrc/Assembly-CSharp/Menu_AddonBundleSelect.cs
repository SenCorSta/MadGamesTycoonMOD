using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001EE RID: 494
public class Menu_AddonBundleSelect : MonoBehaviour
{
	// Token: 0x060012AA RID: 4778 RVA: 0x0000CD75 File Offset: 0x0000AF75
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060012AB RID: 4779 RVA: 0x000D1F68 File Offset: 0x000D0168
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
		if (!this.menuAddonBundle_)
		{
			this.menuAddonBundle_ = this.guiMain_.uiObjects[271].GetComponent<Menu_AddonBundle>();
		}
	}

	// Token: 0x060012AC RID: 4780 RVA: 0x0000CD7D File Offset: 0x0000AF7D
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060012AD RID: 4781 RVA: 0x000D205C File Offset: 0x000D025C
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

	// Token: 0x060012AE RID: 4782 RVA: 0x000D20A8 File Offset: 0x000D02A8
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_AddonBundleSelect>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060012AF RID: 4783 RVA: 0x0000CDB5 File Offset: 0x0000AFB5
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x060012B0 RID: 4784 RVA: 0x000D2104 File Offset: 0x000D0304
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

	// Token: 0x060012B1 RID: 4785 RVA: 0x000D21D0 File Offset: 0x000D03D0
	public void Init(int slot_)
	{
		this.FindScripts();
		this.slot = slot_;
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x060012B2 RID: 4786 RVA: 0x000D222C File Offset: 0x000D042C
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && this.CheckGameData(component) && !this.Exists(this.uiObjects[0], component.myID))
				{
					Item_AddonBundleSelect component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_AddonBundleSelect>();
					component2.game_ = component;
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.genres_ = this.genres_;
					component2.menu_ = this.menuAddonBundle_;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x060012B3 RID: 4787 RVA: 0x000D2348 File Offset: 0x000D0548
	public bool CheckGameData(gameScript script_)
	{
		if (script_)
		{
			if (this.slot == 0)
			{
				if (script_.playerGame && !script_.inDevelopment && !script_.isOnMarket && script_.gameTyp == 0 && !script_.typ_budget && script_.amountAddons > 0 && !script_.pubOffer && (script_.typ_standard || script_.typ_nachfolger || script_.typ_spinoff) && !script_.handy && !script_.arcade && this.HasUnusedAddons(script_.myID))
				{
					return true;
				}
			}
			else if (script_.playerGame && !script_.inDevelopment && !script_.isOnMarket && script_.gameTyp == 0 && !script_.bundle_created && (script_.typ_addon || script_.typ_addonStandalone) && script_.originalIP == this.menuAddonBundle_.games[0].myID && this.menuAddonBundle_.games[0] != script_ && this.menuAddonBundle_.games[1] != script_ && this.menuAddonBundle_.games[2] != script_ && this.menuAddonBundle_.games[3] != script_ && this.menuAddonBundle_.games[4] != script_ && !script_.handy && !script_.arcade)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060012B4 RID: 4788 RVA: 0x000D24E0 File Offset: 0x000D06E0
	private bool HasUnusedAddons(int gameID)
	{
		for (int i = 0; i < this.mS_.games_.arrayGamesScripts.Length; i++)
		{
			gameScript gameScript = this.mS_.games_.arrayGamesScripts[i];
			if (gameScript.playerGame && !gameScript.inDevelopment && !gameScript.isOnMarket && !gameScript.bundle_created && (gameScript.typ_addon || gameScript.typ_addonStandalone) && gameScript.originalIP == gameID)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060012B5 RID: 4789 RVA: 0x000D255C File Offset: 0x000D075C
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
				Item_AddonBundleSelect component = gameObject.GetComponent<Item_AddonBundleSelect>();
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

	// Token: 0x060012B6 RID: 4790 RVA: 0x0000CDC3 File Offset: 0x0000AFC3
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0400170F RID: 5903
	public GameObject[] uiPrefabs;

	// Token: 0x04001710 RID: 5904
	public GameObject[] uiObjects;

	// Token: 0x04001711 RID: 5905
	private mainScript mS_;

	// Token: 0x04001712 RID: 5906
	private GameObject main_;

	// Token: 0x04001713 RID: 5907
	private GUI_Main guiMain_;

	// Token: 0x04001714 RID: 5908
	private sfxScript sfx_;

	// Token: 0x04001715 RID: 5909
	private textScript tS_;

	// Token: 0x04001716 RID: 5910
	private genres genres_;

	// Token: 0x04001717 RID: 5911
	private Menu_AddonBundle menuAddonBundle_;

	// Token: 0x04001718 RID: 5912
	public int slot;

	// Token: 0x04001719 RID: 5913
	private float updateTimer;
}
