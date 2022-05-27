using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000241 RID: 577
public class Menu_Stats_MyGamesAuftragsspiele : MonoBehaviour
{
	// Token: 0x0600162D RID: 5677 RVA: 0x0000F5A4 File Offset: 0x0000D7A4
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600162E RID: 5678 RVA: 0x000EA690 File Offset: 0x000E8890
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

	// Token: 0x0600162F RID: 5679 RVA: 0x0000F5AC File Offset: 0x0000D7AC
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06001630 RID: 5680 RVA: 0x000EA758 File Offset: 0x000E8958
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

	// Token: 0x06001631 RID: 5681 RVA: 0x000EA7A4 File Offset: 0x000E89A4
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).GetComponent<Item_MyGames_Auftragsspiel>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001632 RID: 5682 RVA: 0x0000F5E4 File Offset: 0x0000D7E4
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06001633 RID: 5683 RVA: 0x000EA7E8 File Offset: 0x000E89E8
	public void InitDropdowns()
	{
		this.FindScripts();
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(604));
		list.Add(this.tS_.GetText(1290));
		list.Add(this.tS_.GetText(1289));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06001634 RID: 5684 RVA: 0x0000F5EC File Offset: 0x0000D7EC
	public void Init()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.SetData();
	}

	// Token: 0x06001635 RID: 5685 RVA: 0x000EA8A4 File Offset: 0x000E8AA4
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
					Item_MyGames_Auftragsspiel component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_MyGames_Auftragsspiel>();
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

	// Token: 0x06001636 RID: 5686 RVA: 0x0000F600 File Offset: 0x0000D800
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.IsMyAuftragsspiel() && !script_.inDevelopment && !script_.schublade;
	}

	// Token: 0x06001637 RID: 5687 RVA: 0x0000F625 File Offset: 0x0000D825
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001638 RID: 5688 RVA: 0x000EA9B0 File Offset: 0x000E8BB0
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
				Item_MyGames_Auftragsspiel component = gameObject.GetComponent<Item_MyGames_Auftragsspiel>();
				switch (value)
				{
				case 0:
					gameObject.name = component.game_.GetNameSimple();
					break;
				case 1:
					gameObject.name = component.game_.publisherID.ToString();
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
