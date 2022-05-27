using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001F4 RID: 500
public class Menu_BundleSelect : MonoBehaviour
{
	// Token: 0x06001302 RID: 4866 RVA: 0x000C97D3 File Offset: 0x000C79D3
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001303 RID: 4867 RVA: 0x000C97DC File Offset: 0x000C79DC
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
		if (!this.menuBundle_)
		{
			this.menuBundle_ = this.guiMain_.uiObjects[267].GetComponent<Menu_Bundle>();
		}
	}

	// Token: 0x06001304 RID: 4868 RVA: 0x000C98CD File Offset: 0x000C7ACD
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06001305 RID: 4869 RVA: 0x000C9908 File Offset: 0x000C7B08
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

	// Token: 0x06001306 RID: 4870 RVA: 0x000C9954 File Offset: 0x000C7B54
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_BundleSelect>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001307 RID: 4871 RVA: 0x000C99B0 File Offset: 0x000C7BB0
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x06001308 RID: 4872 RVA: 0x000C99C0 File Offset: 0x000C7BC0
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

	// Token: 0x06001309 RID: 4873 RVA: 0x000C9A8C File Offset: 0x000C7C8C
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

	// Token: 0x0600130A RID: 4874 RVA: 0x000C9AE8 File Offset: 0x000C7CE8
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
					Item_BundleSelect component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_BundleSelect>();
					component2.game_ = component;
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.genres_ = this.genres_;
					component2.menu_ = this.menuBundle_;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x0600130B RID: 4875 RVA: 0x000C9C04 File Offset: 0x000C7E04
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.ownerID == this.mS_.myID && !script_.inDevelopment && !script_.isOnMarket && script_.typ_budget && !script_.bundle_created && !script_.pubOffer && script_.gameTyp == 0 && !script_.typ_mmoaddon && !script_.typ_bundle && !script_.typ_addon && !script_.typ_addonStandalone && this.menuBundle_.games[0] != script_ && this.menuBundle_.games[1] != script_ && this.menuBundle_.games[2] != script_ && this.menuBundle_.games[3] != script_ && this.menuBundle_.games[4] != script_ && !script_.handy && !script_.arcade;
	}

	// Token: 0x0600130C RID: 4876 RVA: 0x000C9D1C File Offset: 0x000C7F1C
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
				Item_BundleSelect component = gameObject.GetComponent<Item_BundleSelect>();
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

	// Token: 0x0600130D RID: 4877 RVA: 0x000C9E87 File Offset: 0x000C8087
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001748 RID: 5960
	public GameObject[] uiPrefabs;

	// Token: 0x04001749 RID: 5961
	public GameObject[] uiObjects;

	// Token: 0x0400174A RID: 5962
	private mainScript mS_;

	// Token: 0x0400174B RID: 5963
	private GameObject main_;

	// Token: 0x0400174C RID: 5964
	private GUI_Main guiMain_;

	// Token: 0x0400174D RID: 5965
	private sfxScript sfx_;

	// Token: 0x0400174E RID: 5966
	private textScript tS_;

	// Token: 0x0400174F RID: 5967
	private genres genres_;

	// Token: 0x04001750 RID: 5968
	private Menu_Bundle menuBundle_;

	// Token: 0x04001751 RID: 5969
	public int slot;

	// Token: 0x04001752 RID: 5970
	private float updateTimer;
}
