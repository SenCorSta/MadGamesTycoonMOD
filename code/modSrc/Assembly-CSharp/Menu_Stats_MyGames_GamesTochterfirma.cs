using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000246 RID: 582
public class Menu_Stats_MyGames_GamesTochterfirma : MonoBehaviour
{
	// Token: 0x0600166A RID: 5738 RVA: 0x0000F8C6 File Offset: 0x0000DAC6
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600166B RID: 5739 RVA: 0x000EB820 File Offset: 0x000E9A20
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

	// Token: 0x0600166C RID: 5740 RVA: 0x0000F8CE File Offset: 0x0000DACE
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x0600166D RID: 5741 RVA: 0x000EB8E8 File Offset: 0x000E9AE8
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

	// Token: 0x0600166E RID: 5742 RVA: 0x0000F900 File Offset: 0x0000DB00
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x0600166F RID: 5743 RVA: 0x000EB92C File Offset: 0x000E9B2C
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

	// Token: 0x06001670 RID: 5744 RVA: 0x0000F908 File Offset: 0x0000DB08
	public void Init()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.SetData();
	}

	// Token: 0x06001671 RID: 5745 RVA: 0x000EB9E8 File Offset: 0x000E9BE8
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

	// Token: 0x06001672 RID: 5746 RVA: 0x000EBAF4 File Offset: 0x000E9CF4
	public bool CheckGameData(gameScript script_)
	{
		if (script_ && !script_.auftragsspiel && script_.isOnMarket)
		{
			if (!script_.devS_)
			{
				script_.FindMyDeveloper();
			}
			if (script_.devS_ && script_.devS_.tochterfirma)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001673 RID: 5747 RVA: 0x0000F91C File Offset: 0x0000DB1C
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001674 RID: 5748 RVA: 0x000EBB4C File Offset: 0x000E9D4C
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

	// Token: 0x06001675 RID: 5749 RVA: 0x0000F937 File Offset: 0x0000DB37
	public void TOGGLE_Show()
	{
		this.uiObjects[0].SetActive(false);
		this.uiObjects[0].SetActive(true);
		this.SetData();
	}

	// Token: 0x04001A69 RID: 6761
	private mainScript mS_;

	// Token: 0x04001A6A RID: 6762
	private GameObject main_;

	// Token: 0x04001A6B RID: 6763
	private GUI_Main guiMain_;

	// Token: 0x04001A6C RID: 6764
	private sfxScript sfx_;

	// Token: 0x04001A6D RID: 6765
	private textScript tS_;

	// Token: 0x04001A6E RID: 6766
	private genres genres_;

	// Token: 0x04001A6F RID: 6767
	public GameObject[] uiPrefabs;

	// Token: 0x04001A70 RID: 6768
	public GameObject[] uiObjects;
}
