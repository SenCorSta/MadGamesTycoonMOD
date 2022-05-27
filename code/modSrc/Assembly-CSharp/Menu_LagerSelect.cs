using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001FB RID: 507
public class Menu_LagerSelect : MonoBehaviour
{
	// Token: 0x06001342 RID: 4930 RVA: 0x0000D340 File Offset: 0x0000B540
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001343 RID: 4931 RVA: 0x000D6A48 File Offset: 0x000D4C48
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

	// Token: 0x06001344 RID: 4932 RVA: 0x0000D348 File Offset: 0x0000B548
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06001345 RID: 4933 RVA: 0x000D6B10 File Offset: 0x000D4D10
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

	// Token: 0x06001346 RID: 4934 RVA: 0x000D6B5C File Offset: 0x000D4D5C
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

	// Token: 0x06001347 RID: 4935 RVA: 0x0000D380 File Offset: 0x0000B580
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x06001348 RID: 4936 RVA: 0x000D6BB8 File Offset: 0x000D4DB8
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

	// Token: 0x06001349 RID: 4937 RVA: 0x000D6C9C File Offset: 0x000D4E9C
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

	// Token: 0x0600134A RID: 4938 RVA: 0x000D6D14 File Offset: 0x000D4F14
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

	// Token: 0x0600134B RID: 4939 RVA: 0x0000D38E File Offset: 0x0000B58E
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.playerGame && !script_.inDevelopment && script_.publisherID == -1 && script_.GetLagerbestand() > 0;
	}

	// Token: 0x0600134C RID: 4940 RVA: 0x000D6E44 File Offset: 0x000D5044
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

	// Token: 0x0600134D RID: 4941 RVA: 0x0000D3BD File Offset: 0x0000B5BD
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600134E RID: 4942 RVA: 0x000D6FE8 File Offset: 0x000D51E8
	public void BUTTON_AllesVerkaufen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[226].SetActive(true);
		this.guiMain_.uiObjects[226].GetComponent<Menu_W_Restbestand>().Init(null);
	}

	// Token: 0x0600134F RID: 4943 RVA: 0x0000D3E3 File Offset: 0x0000B5E3
	public void TOGGLE_Automatic()
	{
		this.mS_.sellLagerbestandAutomatic = this.uiObjects[4].GetComponent<Toggle>().isOn;
	}

	// Token: 0x0400178D RID: 6029
	public GameObject[] uiPrefabs;

	// Token: 0x0400178E RID: 6030
	public GameObject[] uiObjects;

	// Token: 0x0400178F RID: 6031
	private mainScript mS_;

	// Token: 0x04001790 RID: 6032
	private GameObject main_;

	// Token: 0x04001791 RID: 6033
	private GUI_Main guiMain_;

	// Token: 0x04001792 RID: 6034
	private sfxScript sfx_;

	// Token: 0x04001793 RID: 6035
	private textScript tS_;

	// Token: 0x04001794 RID: 6036
	private genres genres_;

	// Token: 0x04001795 RID: 6037
	public roomScript rS_;

	// Token: 0x04001796 RID: 6038
	private float updateTimer;
}
