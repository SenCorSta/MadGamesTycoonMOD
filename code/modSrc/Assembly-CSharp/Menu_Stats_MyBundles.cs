using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000241 RID: 577
public class Menu_Stats_MyBundles : MonoBehaviour
{
	// Token: 0x0600163E RID: 5694 RVA: 0x000E25BB File Offset: 0x000E07BB
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600163F RID: 5695 RVA: 0x000E25C4 File Offset: 0x000E07C4
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

	// Token: 0x06001640 RID: 5696 RVA: 0x000E268C File Offset: 0x000E088C
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06001641 RID: 5697 RVA: 0x000E26C4 File Offset: 0x000E08C4
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

	// Token: 0x06001642 RID: 5698 RVA: 0x000E2710 File Offset: 0x000E0910
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).GetComponent<Item_MyBundles>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001643 RID: 5699 RVA: 0x000E2754 File Offset: 0x000E0954
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06001644 RID: 5700 RVA: 0x000E275C File Offset: 0x000E095C
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(277));
		list.Add(this.tS_.GetText(217));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06001645 RID: 5701 RVA: 0x000E27FC File Offset: 0x000E09FC
	public void Init()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.SetData();
	}

	// Token: 0x06001646 RID: 5702 RVA: 0x000E2810 File Offset: 0x000E0A10
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
					Item_MyBundles component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_MyBundles>();
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.genres_ = this.genres_;
					component2.game_ = component;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x06001647 RID: 5703 RVA: 0x000E291A File Offset: 0x000E0B1A
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.developerID == this.mS_.myID && !script_.inDevelopment && (script_.typ_bundle || script_.typ_bundleAddon);
	}

	// Token: 0x06001648 RID: 5704 RVA: 0x000E2952 File Offset: 0x000E0B52
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001649 RID: 5705 RVA: 0x000E2970 File Offset: 0x000E0B70
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
				Item_MyBundles component = gameObject.GetComponent<Item_MyBundles>();
				switch (value)
				{
				case 0:
					gameObject.name = component.game_.GetNameSimple();
					break;
				case 1:
					gameObject.name = component.game_.reviewTotal.ToString();
					break;
				case 2:
				{
					float num = (float)component.game_.date_month;
					num /= 13f;
					gameObject.name = component.game_.date_year.ToString() + num.ToString();
					break;
				}
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

	// Token: 0x04001A3C RID: 6716
	private mainScript mS_;

	// Token: 0x04001A3D RID: 6717
	private GameObject main_;

	// Token: 0x04001A3E RID: 6718
	private GUI_Main guiMain_;

	// Token: 0x04001A3F RID: 6719
	private sfxScript sfx_;

	// Token: 0x04001A40 RID: 6720
	private textScript tS_;

	// Token: 0x04001A41 RID: 6721
	private genres genres_;

	// Token: 0x04001A42 RID: 6722
	public GameObject[] uiPrefabs;

	// Token: 0x04001A43 RID: 6723
	public GameObject[] uiObjects;

	// Token: 0x04001A44 RID: 6724
	private float updateTimer;
}
