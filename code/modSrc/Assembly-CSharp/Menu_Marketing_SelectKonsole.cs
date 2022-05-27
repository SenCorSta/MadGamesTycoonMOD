using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001AA RID: 426
public class Menu_Marketing_SelectKonsole : MonoBehaviour
{
	// Token: 0x06001006 RID: 4102 RVA: 0x0000B55E File Offset: 0x0000975E
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001007 RID: 4103 RVA: 0x000B6FC0 File Offset: 0x000B51C0
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

	// Token: 0x06001008 RID: 4104 RVA: 0x0000B566 File Offset: 0x00009766
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06001009 RID: 4105 RVA: 0x000B7088 File Offset: 0x000B5288
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

	// Token: 0x0600100A RID: 4106 RVA: 0x000B70D4 File Offset: 0x000B52D4
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_Marketing_Konsole>().pS_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600100B RID: 4107 RVA: 0x0000B59E File Offset: 0x0000979E
	private void OnEnable()
	{
		this.InitDropdowns();
	}

	// Token: 0x0600100C RID: 4108 RVA: 0x000B7130 File Offset: 0x000B5330
	public void InitDropdowns()
	{
		this.FindScripts();
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(433));
		list.Add(this.tS_.GetText(217));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x0600100D RID: 4109 RVA: 0x0000B5A6 File Offset: 0x000097A6
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x0600100E RID: 4110 RVA: 0x000B71D8 File Offset: 0x000B53D8
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Platform");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				platformScript component = array[i].GetComponent<platformScript>();
				if (component && component.playerConsole && !component.vomMarktGenommen && !this.Exists(this.uiObjects[0], component.myID))
				{
					Item_Marketing_Konsole component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_Marketing_Konsole>();
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.pS_ = component;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x0600100F RID: 4111 RVA: 0x0000B5B4 File Offset: 0x000097B4
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001010 RID: 4112 RVA: 0x000B72E0 File Offset: 0x000B54E0
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
				Item_Marketing_Konsole component = gameObject.GetComponent<Item_Marketing_Konsole>();
				switch (value)
				{
				case 0:
					gameObject.name = component.pS_.GetName();
					break;
				case 1:
					gameObject.name = component.pS_.GetHype().ToString();
					break;
				case 2:
				{
					float num = (float)component.pS_.date_month;
					num /= 13f;
					gameObject.name = component.pS_.date_year.ToString() + num.ToString();
					if (!component.pS_.isUnlocked)
					{
						gameObject.name = "999999";
					}
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

	// Token: 0x04001490 RID: 5264
	private mainScript mS_;

	// Token: 0x04001491 RID: 5265
	private GameObject main_;

	// Token: 0x04001492 RID: 5266
	private GUI_Main guiMain_;

	// Token: 0x04001493 RID: 5267
	private sfxScript sfx_;

	// Token: 0x04001494 RID: 5268
	private textScript tS_;

	// Token: 0x04001495 RID: 5269
	private genres genres_;

	// Token: 0x04001496 RID: 5270
	public GameObject[] uiPrefabs;

	// Token: 0x04001497 RID: 5271
	public GameObject[] uiObjects;

	// Token: 0x04001498 RID: 5272
	private float updateTimer;
}
