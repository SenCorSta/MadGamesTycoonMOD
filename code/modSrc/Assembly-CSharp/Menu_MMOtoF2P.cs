using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200020F RID: 527
public class Menu_MMOtoF2P : MonoBehaviour
{
	// Token: 0x0600142F RID: 5167 RVA: 0x0000DC13 File Offset: 0x0000BE13
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001430 RID: 5168 RVA: 0x000DCD50 File Offset: 0x000DAF50
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

	// Token: 0x06001431 RID: 5169 RVA: 0x0000DC1B File Offset: 0x0000BE1B
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06001432 RID: 5170 RVA: 0x000DCE18 File Offset: 0x000DB018
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

	// Token: 0x06001433 RID: 5171 RVA: 0x000DCE64 File Offset: 0x000DB064
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_MMOtoF2P>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001434 RID: 5172 RVA: 0x0000DC53 File Offset: 0x0000BE53
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x06001435 RID: 5173 RVA: 0x000DCEC0 File Offset: 0x000DB0C0
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(217));
		list.Add(this.tS_.GetText(277));
		list.Add(this.tS_.GetText(273));
		list.Add(this.tS_.GetText(275));
		list.Add(this.tS_.GetText(1236));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06001436 RID: 5174 RVA: 0x000DCFA4 File Offset: 0x000DB1A4
	public void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x06001437 RID: 5175 RVA: 0x000DCFF8 File Offset: 0x000DB1F8
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
					Item_MMOtoF2P component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_MMOtoF2P>();
					component2.game_ = component;
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.genres_ = this.genres_;
					component2.menu_ = this;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x06001438 RID: 5176 RVA: 0x000DD10C File Offset: 0x000DB30C
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.playerGame && !script_.typ_bundle && !script_.typ_bundleAddon && !script_.typ_addon && !script_.typ_addonStandalone && !script_.typ_contractGame && !script_.inDevelopment && script_.gameTyp == 1 && !script_.mmoTOf2p_created && (script_.publisherID == -1 || (!script_.isOnMarket && script_.publisherID != -1)) && !script_.pubOffer;
	}

	// Token: 0x06001439 RID: 5177 RVA: 0x000DD190 File Offset: 0x000DB390
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
				Item_MMOtoF2P component = gameObject.GetComponent<Item_MMOtoF2P>();
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
				case 5:
					gameObject.name = component.game_.abonnements.ToString();
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

	// Token: 0x0600143A RID: 5178 RVA: 0x0000DC67 File Offset: 0x0000BE67
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001852 RID: 6226
	public GameObject[] uiPrefabs;

	// Token: 0x04001853 RID: 6227
	public GameObject[] uiObjects;

	// Token: 0x04001854 RID: 6228
	private mainScript mS_;

	// Token: 0x04001855 RID: 6229
	private GameObject main_;

	// Token: 0x04001856 RID: 6230
	private GUI_Main guiMain_;

	// Token: 0x04001857 RID: 6231
	private sfxScript sfx_;

	// Token: 0x04001858 RID: 6232
	private textScript tS_;

	// Token: 0x04001859 RID: 6233
	private genres genres_;

	// Token: 0x0400185A RID: 6234
	private float updateTimer;
}
