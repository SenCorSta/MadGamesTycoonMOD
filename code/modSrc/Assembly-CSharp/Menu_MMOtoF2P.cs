using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000210 RID: 528
public class Menu_MMOtoF2P : MonoBehaviour
{
	// Token: 0x0600144C RID: 5196 RVA: 0x000D3329 File Offset: 0x000D1529
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600144D RID: 5197 RVA: 0x000D3334 File Offset: 0x000D1534
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

	// Token: 0x0600144E RID: 5198 RVA: 0x000D33FC File Offset: 0x000D15FC
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x0600144F RID: 5199 RVA: 0x000D3434 File Offset: 0x000D1634
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

	// Token: 0x06001450 RID: 5200 RVA: 0x000D3480 File Offset: 0x000D1680
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

	// Token: 0x06001451 RID: 5201 RVA: 0x000D34DC File Offset: 0x000D16DC
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x06001452 RID: 5202 RVA: 0x000D34F0 File Offset: 0x000D16F0
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

	// Token: 0x06001453 RID: 5203 RVA: 0x000D35D4 File Offset: 0x000D17D4
	public void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x06001454 RID: 5204 RVA: 0x000D3628 File Offset: 0x000D1828
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

	// Token: 0x06001455 RID: 5205 RVA: 0x000D373C File Offset: 0x000D193C
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.ownerID == this.mS_.myID && !script_.typ_bundle && !script_.typ_bundleAddon && !script_.typ_addon && !script_.typ_addonStandalone && !script_.typ_contractGame && !script_.inDevelopment && script_.gameTyp == 1 && !script_.mmoTOf2p_created && (script_.publisherID == this.mS_.myID || (!script_.isOnMarket && script_.publisherID != this.mS_.myID)) && !script_.pubOffer;
	}

	// Token: 0x06001456 RID: 5206 RVA: 0x000D37E4 File Offset: 0x000D19E4
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

	// Token: 0x06001457 RID: 5207 RVA: 0x000D396C File Offset: 0x000D1B6C
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x0400185B RID: 6235
	public GameObject[] uiPrefabs;

	// Token: 0x0400185C RID: 6236
	public GameObject[] uiObjects;

	// Token: 0x0400185D RID: 6237
	private mainScript mS_;

	// Token: 0x0400185E RID: 6238
	private GameObject main_;

	// Token: 0x0400185F RID: 6239
	private GUI_Main guiMain_;

	// Token: 0x04001860 RID: 6240
	private sfxScript sfx_;

	// Token: 0x04001861 RID: 6241
	private textScript tS_;

	// Token: 0x04001862 RID: 6242
	private genres genres_;

	// Token: 0x04001863 RID: 6243
	private float updateTimer;
}
