using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000256 RID: 598
public class Menu_Stats_TochterfirmaIP : MonoBehaviour
{
	// Token: 0x0600172D RID: 5933 RVA: 0x0001039D File Offset: 0x0000E59D
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600172E RID: 5934 RVA: 0x000F0B50 File Offset: 0x000EED50
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

	// Token: 0x0600172F RID: 5935 RVA: 0x000103A5 File Offset: 0x0000E5A5
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x06001730 RID: 5936 RVA: 0x000F0C18 File Offset: 0x000EEE18
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).GetComponent<Item_TochterfirmaIP>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001731 RID: 5937 RVA: 0x000F0C5C File Offset: 0x000EEE5C
	public void InitDropdowns()
	{
		this.FindScripts();
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(217));
		list.Add(this.tS_.GetText(1555));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06001732 RID: 5938 RVA: 0x000103D7 File Offset: 0x0000E5D7
	public void Init(publisherScript script_, int slot_)
	{
		this.slot = slot_;
		this.pS_ = script_;
		this.FindScripts();
		this.InitDropdowns();
		this.SetData();
	}

	// Token: 0x06001733 RID: 5939 RVA: 0x000F0D04 File Offset: 0x000EEF04
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
					Item_TochterfirmaIP component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_TochterfirmaIP>();
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.genres_ = this.genres_;
					component2.game_ = component;
					component2.pS_ = this.pS_;
					component2.slot = this.slot;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
		string text = this.tS_.GetText(1783) + ": " + this.uiObjects[0].transform.childCount.ToString();
		this.uiObjects[4].GetComponent<Text>().text = text;
	}

	// Token: 0x06001734 RID: 5940 RVA: 0x000F0E74 File Offset: 0x000EF074
	public bool CheckGameData(gameScript script_)
	{
		return script_ && !script_.pubAngebot && !script_.auftragsspiel && script_.IsMyIP(this.pS_) && script_.mainIP == script_.myID && script_.myID != this.pS_.tf_ipFocus[0] && script_.myID != this.pS_.tf_ipFocus[1] && script_.myID != this.pS_.tf_ipFocus[2];
	}

	// Token: 0x06001735 RID: 5941 RVA: 0x000103F9 File Offset: 0x0000E5F9
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001736 RID: 5942 RVA: 0x000F0EF8 File Offset: 0x000EF0F8
	public void BUTTON_RemoveIP()
	{
		this.sfx_.PlaySound(3, true);
		this.pS_.tf_ipFocus[this.slot] = -1;
		this.guiMain_.uiObjects[393].GetComponent<Menu_Stats_TochterfirmaSettings>().UpdateData();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001737 RID: 5943 RVA: 0x000F0F4C File Offset: 0x000EF14C
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
				Item_TochterfirmaIP component = gameObject.GetComponent<Item_TochterfirmaIP>();
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
					if (component.game_.inDevelopment)
					{
						gameObject.name = "999999";
					}
					break;
				}
				case 2:
					gameObject.name = component.game_.ipPunkte.ToString();
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

	// Token: 0x04001B1B RID: 6939
	private mainScript mS_;

	// Token: 0x04001B1C RID: 6940
	private GameObject main_;

	// Token: 0x04001B1D RID: 6941
	private GUI_Main guiMain_;

	// Token: 0x04001B1E RID: 6942
	private sfxScript sfx_;

	// Token: 0x04001B1F RID: 6943
	private textScript tS_;

	// Token: 0x04001B20 RID: 6944
	private genres genres_;

	// Token: 0x04001B21 RID: 6945
	public GameObject[] uiPrefabs;

	// Token: 0x04001B22 RID: 6946
	public GameObject[] uiObjects;

	// Token: 0x04001B23 RID: 6947
	private publisherScript pS_;

	// Token: 0x04001B24 RID: 6948
	public int slot;
}
