using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001FC RID: 508
public class Menu_LagerSelect : MonoBehaviour
{
	// Token: 0x0600135D RID: 4957 RVA: 0x000CC649 File Offset: 0x000CA849
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600135E RID: 4958 RVA: 0x000CC654 File Offset: 0x000CA854
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

	// Token: 0x0600135F RID: 4959 RVA: 0x000CC71C File Offset: 0x000CA91C
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06001360 RID: 4960 RVA: 0x000CC754 File Offset: 0x000CA954
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

	// Token: 0x06001361 RID: 4961 RVA: 0x000CC7A0 File Offset: 0x000CA9A0
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_Restbestand>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001362 RID: 4962 RVA: 0x000CC7FC File Offset: 0x000CA9FC
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x06001363 RID: 4963 RVA: 0x000CC80C File Offset: 0x000CAA0C
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(1124));
		list.Add(this.tS_.GetText(1103));
		list.Add(this.tS_.GetText(1104));
		list.Add(this.tS_.GetText(1105));
		list.Add(this.tS_.GetText(217));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06001364 RID: 4964 RVA: 0x000CC8F0 File Offset: 0x000CAAF0
	public void Init(roomScript room_)
	{
		this.FindScripts();
		this.rS_ = room_;
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.uiObjects[4].GetComponent<Toggle>().isOn = this.mS_.sellLagerbestandAutomatic;
		this.SetData();
	}

	// Token: 0x06001365 RID: 4965 RVA: 0x000CC968 File Offset: 0x000CAB68
	private void SetData()
	{
		if (!this.rS_)
		{
			return;
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && this.CheckGameData(component) && !this.Exists(this.uiObjects[0], component.myID))
				{
					Item_Restbestand component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_Restbestand>();
					component2.game_ = component;
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.genres_ = this.genres_;
					component2.rS_ = this.rS_;
					component2.menu_ = this;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x06001366 RID: 4966 RVA: 0x000CCA96 File Offset: 0x000CAC96
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.publisherID == this.mS_.myID && !script_.inDevelopment && script_.GetLagerbestand() > 0;
	}

	// Token: 0x06001367 RID: 4967 RVA: 0x000CCAC8 File Offset: 0x000CACC8
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
				Item_Restbestand component = gameObject.GetComponent<Item_Restbestand>();
				switch (value)
				{
				case 0:
					gameObject.name = component.game_.GetNameSimple();
					break;
				case 1:
					gameObject.name = component.game_.GetLagerbestand().ToString();
					break;
				case 2:
					gameObject.name = component.game_.lagerbestand[0].ToString();
					break;
				case 3:
					gameObject.name = component.game_.lagerbestand[1].ToString();
					break;
				case 4:
					gameObject.name = component.game_.lagerbestand[2].ToString();
					break;
				case 5:
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

	// Token: 0x06001368 RID: 4968 RVA: 0x000CCC69 File Offset: 0x000CAE69
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001369 RID: 4969 RVA: 0x000CCC90 File Offset: 0x000CAE90
	public void BUTTON_AllesVerkaufen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[226].SetActive(true);
		this.guiMain_.uiObjects[226].GetComponent<Menu_W_Restbestand>().Init(null);
	}

	// Token: 0x0600136A RID: 4970 RVA: 0x000CCCDD File Offset: 0x000CAEDD
	public void TOGGLE_Automatic()
	{
		this.mS_.sellLagerbestandAutomatic = this.uiObjects[4].GetComponent<Toggle>().isOn;
	}

	// Token: 0x04001796 RID: 6038
	public GameObject[] uiPrefabs;

	// Token: 0x04001797 RID: 6039
	public GameObject[] uiObjects;

	// Token: 0x04001798 RID: 6040
	private mainScript mS_;

	// Token: 0x04001799 RID: 6041
	private GameObject main_;

	// Token: 0x0400179A RID: 6042
	private GUI_Main guiMain_;

	// Token: 0x0400179B RID: 6043
	private sfxScript sfx_;

	// Token: 0x0400179C RID: 6044
	private textScript tS_;

	// Token: 0x0400179D RID: 6045
	private genres genres_;

	// Token: 0x0400179E RID: 6046
	public roomScript rS_;

	// Token: 0x0400179F RID: 6047
	private float updateTimer;
}
