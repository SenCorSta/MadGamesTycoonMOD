using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000247 RID: 583
public class Menu_Stats_MyGames_GamesTochterfirma : MonoBehaviour
{
	// Token: 0x0600168B RID: 5771 RVA: 0x000E4216 File Offset: 0x000E2416
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600168C RID: 5772 RVA: 0x000E4220 File Offset: 0x000E2420
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

	// Token: 0x0600168D RID: 5773 RVA: 0x000E42E8 File Offset: 0x000E24E8
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x0600168E RID: 5774 RVA: 0x000E431C File Offset: 0x000E251C
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).GetComponent<Item_MyGames_Tochterfirma>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600168F RID: 5775 RVA: 0x000E4360 File Offset: 0x000E2560
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06001690 RID: 5776 RVA: 0x000E4368 File Offset: 0x000E2568
	public void InitDropdowns()
	{
		this.FindScripts();
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(274));
		list.Add(this.tS_.GetText(355));
		list.Add(this.tS_.GetText(1290));
		list.Add(this.tS_.GetText(1289));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06001691 RID: 5777 RVA: 0x000E4424 File Offset: 0x000E2624
	public void Init()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.SetData();
	}

	// Token: 0x06001692 RID: 5778 RVA: 0x000E4438 File Offset: 0x000E2638
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
					Item_MyGames_Tochterfirma component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_MyGames_Tochterfirma>();
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

	// Token: 0x06001693 RID: 5779 RVA: 0x000E4544 File Offset: 0x000E2744
	public bool CheckGameData(gameScript script_)
	{
		if (script_ && !script_.auftragsspiel && script_.isOnMarket)
		{
			if (!script_.devS_)
			{
				script_.FindMyDeveloper();
			}
			if (script_.devS_ && script_.devS_.IsMyTochterfirma())
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001694 RID: 5780 RVA: 0x000E4599 File Offset: 0x000E2799
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001695 RID: 5781 RVA: 0x000E45B4 File Offset: 0x000E27B4
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
				Item_MyGames_Tochterfirma component = gameObject.GetComponent<Item_MyGames_Tochterfirma>();
				switch (value)
				{
				case 0:
					gameObject.name = component.game_.GetDeveloperName();
					break;
				case 1:
					gameObject.name = component.game_.GetNameSimple();
					break;
				case 2:
					gameObject.name = component.game_.reviewTotal.ToString();
					break;
				case 3:
					gameObject.name = component.game_.GetUserReviewPercent().ToString();
					break;
				}
			}
		}
		if (value == 0 || value == 1)
		{
			this.mS_.SortChildrenByName(this.uiObjects[0]);
			return;
		}
		this.mS_.SortChildrenByFloat(this.uiObjects[0]);
	}

	// Token: 0x06001696 RID: 5782 RVA: 0x000E46D7 File Offset: 0x000E28D7
	public void TOGGLE_Show()
	{
		this.uiObjects[0].SetActive(false);
		this.uiObjects[0].SetActive(true);
		this.SetData();
	}

	// Token: 0x04001A72 RID: 6770
	private mainScript mS_;

	// Token: 0x04001A73 RID: 6771
	private GameObject main_;

	// Token: 0x04001A74 RID: 6772
	private GUI_Main guiMain_;

	// Token: 0x04001A75 RID: 6773
	private sfxScript sfx_;

	// Token: 0x04001A76 RID: 6774
	private textScript tS_;

	// Token: 0x04001A77 RID: 6775
	private genres genres_;

	// Token: 0x04001A78 RID: 6776
	public GameObject[] uiPrefabs;

	// Token: 0x04001A79 RID: 6777
	public GameObject[] uiObjects;
}
