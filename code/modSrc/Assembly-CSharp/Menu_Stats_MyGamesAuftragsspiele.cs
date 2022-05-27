using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000242 RID: 578
public class Menu_Stats_MyGamesAuftragsspiele : MonoBehaviour
{
	// Token: 0x0600164B RID: 5707 RVA: 0x000E2AC7 File Offset: 0x000E0CC7
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600164C RID: 5708 RVA: 0x000E2AD0 File Offset: 0x000E0CD0
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

	// Token: 0x0600164D RID: 5709 RVA: 0x000E2B98 File Offset: 0x000E0D98
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x0600164E RID: 5710 RVA: 0x000E2BD0 File Offset: 0x000E0DD0
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

	// Token: 0x0600164F RID: 5711 RVA: 0x000E2C1C File Offset: 0x000E0E1C
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

	// Token: 0x06001650 RID: 5712 RVA: 0x000E2C60 File Offset: 0x000E0E60
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06001651 RID: 5713 RVA: 0x000E2C68 File Offset: 0x000E0E68
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

	// Token: 0x06001652 RID: 5714 RVA: 0x000E2D24 File Offset: 0x000E0F24
	public void Init()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.SetData();
	}

	// Token: 0x06001653 RID: 5715 RVA: 0x000E2D38 File Offset: 0x000E0F38
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

	// Token: 0x06001654 RID: 5716 RVA: 0x000E2E42 File Offset: 0x000E1042
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.IsMyAuftragsspiel() && !script_.inDevelopment && !script_.schublade;
	}

	// Token: 0x06001655 RID: 5717 RVA: 0x000E2E67 File Offset: 0x000E1067
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001656 RID: 5718 RVA: 0x000E2E84 File Offset: 0x000E1084
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

	// Token: 0x04001A45 RID: 6725
	private mainScript mS_;

	// Token: 0x04001A46 RID: 6726
	private GameObject main_;

	// Token: 0x04001A47 RID: 6727
	private GUI_Main guiMain_;

	// Token: 0x04001A48 RID: 6728
	private sfxScript sfx_;

	// Token: 0x04001A49 RID: 6729
	private textScript tS_;

	// Token: 0x04001A4A RID: 6730
	private genres genres_;

	// Token: 0x04001A4B RID: 6731
	public GameObject[] uiPrefabs;

	// Token: 0x04001A4C RID: 6732
	public GameObject[] uiObjects;

	// Token: 0x04001A4D RID: 6733
	private float updateTimer;
}
