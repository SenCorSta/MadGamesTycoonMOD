using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001EF RID: 495
public class Menu_AddonBundleSelect : MonoBehaviour
{
	// Token: 0x060012C5 RID: 4805 RVA: 0x000C74E7 File Offset: 0x000C56E7
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060012C6 RID: 4806 RVA: 0x000C74F0 File Offset: 0x000C56F0
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

	// Token: 0x060012C7 RID: 4807 RVA: 0x000C75E1 File Offset: 0x000C57E1
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060012C8 RID: 4808 RVA: 0x000C761C File Offset: 0x000C581C
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

	// Token: 0x060012C9 RID: 4809 RVA: 0x000C7668 File Offset: 0x000C5868
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

	// Token: 0x060012CA RID: 4810 RVA: 0x000C76C4 File Offset: 0x000C58C4
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x060012CB RID: 4811 RVA: 0x000C76D4 File Offset: 0x000C58D4
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

	// Token: 0x060012CC RID: 4812 RVA: 0x000C77A0 File Offset: 0x000C59A0
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

	// Token: 0x060012CD RID: 4813 RVA: 0x000C77FC File Offset: 0x000C59FC
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

	// Token: 0x060012CE RID: 4814 RVA: 0x000C7918 File Offset: 0x000C5B18
	public bool CheckGameData(gameScript script_)
	{
		if (script_)
		{
			if (this.slot == 0)
			{
				if (script_.ownerID == this.mS_.myID && !script_.inDevelopment && !script_.isOnMarket && script_.gameTyp == 0 && !script_.typ_budget && script_.amountAddons > 0 && !script_.pubOffer && !script_.schublade && (script_.typ_standard || script_.typ_nachfolger || script_.typ_spinoff) && !script_.handy && !script_.arcade && this.HasUnusedAddons(script_.myID))
				{
					return true;
				}
			}
			else if (script_.ownerID == this.mS_.myID && script_.developerID == this.mS_.myID && !script_.inDevelopment && !script_.isOnMarket && script_.gameTyp == 0 && !script_.bundle_created && (script_.typ_addon || script_.typ_addonStandalone) && script_.originalIP == this.menuAddonBundle_.games[0].myID && this.menuAddonBundle_.games[0] != script_ && this.menuAddonBundle_.games[1] != script_ && this.menuAddonBundle_.games[2] != script_ && this.menuAddonBundle_.games[3] != script_ && this.menuAddonBundle_.games[4] != script_ && !script_.handy && !script_.arcade)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060012CF RID: 4815 RVA: 0x000C7AE4 File Offset: 0x000C5CE4
	private bool HasUnusedAddons(int gameID)
	{
		for (int i = 0; i < this.mS_.games_.arrayGamesScripts.Length; i++)
		{
			gameScript gameScript = this.mS_.games_.arrayGamesScripts[i];
			if (gameScript.ownerID == this.mS_.myID && gameScript.developerID == this.mS_.myID && !gameScript.inDevelopment && !gameScript.isOnMarket && !gameScript.bundle_created && (gameScript.typ_addon || gameScript.typ_addonStandalone) && gameScript.originalIP == gameID)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060012D0 RID: 4816 RVA: 0x000C7B80 File Offset: 0x000C5D80
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

	// Token: 0x060012D1 RID: 4817 RVA: 0x000C7CEB File Offset: 0x000C5EEB
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001718 RID: 5912
	public GameObject[] uiPrefabs;

	// Token: 0x04001719 RID: 5913
	public GameObject[] uiObjects;

	// Token: 0x0400171A RID: 5914
	private mainScript mS_;

	// Token: 0x0400171B RID: 5915
	private GameObject main_;

	// Token: 0x0400171C RID: 5916
	private GUI_Main guiMain_;

	// Token: 0x0400171D RID: 5917
	private sfxScript sfx_;

	// Token: 0x0400171E RID: 5918
	private textScript tS_;

	// Token: 0x0400171F RID: 5919
	private genres genres_;

	// Token: 0x04001720 RID: 5920
	private Menu_AddonBundle menuAddonBundle_;

	// Token: 0x04001721 RID: 5921
	public int slot;

	// Token: 0x04001722 RID: 5922
	private float updateTimer;
}
