using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000257 RID: 599
public class Menu_Stats_TochterfirmaIP : MonoBehaviour
{
	// Token: 0x06001753 RID: 5971 RVA: 0x000EA561 File Offset: 0x000E8761
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001754 RID: 5972 RVA: 0x000EA56C File Offset: 0x000E876C
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

	// Token: 0x06001755 RID: 5973 RVA: 0x000EA634 File Offset: 0x000E8834
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x06001756 RID: 5974 RVA: 0x000EA668 File Offset: 0x000E8868
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

	// Token: 0x06001757 RID: 5975 RVA: 0x000EA6AC File Offset: 0x000E88AC
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

	// Token: 0x06001758 RID: 5976 RVA: 0x000EA752 File Offset: 0x000E8952
	public void Init(publisherScript script_, int slot_)
	{
		this.slot = slot_;
		this.pS_ = script_;
		this.FindScripts();
		this.InitDropdowns();
		this.SetData();
	}

	// Token: 0x06001759 RID: 5977 RVA: 0x000EA774 File Offset: 0x000E8974
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

	// Token: 0x0600175A RID: 5978 RVA: 0x000EA8E4 File Offset: 0x000E8AE4
	public bool CheckGameData(gameScript script_)
	{
		return script_ && !script_.pubAngebot && !script_.auftragsspiel && script_.IsMyIP(this.pS_) && script_.mainIP == script_.myID && script_.myID != this.pS_.tf_ipFocus[0] && script_.myID != this.pS_.tf_ipFocus[1] && script_.myID != this.pS_.tf_ipFocus[2];
	}

	// Token: 0x0600175B RID: 5979 RVA: 0x000EA967 File Offset: 0x000E8B67
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600175C RID: 5980 RVA: 0x000EA984 File Offset: 0x000E8B84
	public void BUTTON_RemoveIP()
	{
		this.sfx_.PlaySound(3, true);
		this.pS_.tf_ipFocus[this.slot] = -1;
		this.guiMain_.uiObjects[393].GetComponent<Menu_Stats_TochterfirmaSettings>().UpdateData();
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600175D RID: 5981 RVA: 0x000EA9D8 File Offset: 0x000E8BD8
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

	// Token: 0x04001B24 RID: 6948
	private mainScript mS_;

	// Token: 0x04001B25 RID: 6949
	private GameObject main_;

	// Token: 0x04001B26 RID: 6950
	private GUI_Main guiMain_;

	// Token: 0x04001B27 RID: 6951
	private sfxScript sfx_;

	// Token: 0x04001B28 RID: 6952
	private textScript tS_;

	// Token: 0x04001B29 RID: 6953
	private genres genres_;

	// Token: 0x04001B2A RID: 6954
	public GameObject[] uiPrefabs;

	// Token: 0x04001B2B RID: 6955
	public GameObject[] uiObjects;

	// Token: 0x04001B2C RID: 6956
	private publisherScript pS_;

	// Token: 0x04001B2D RID: 6957
	public int slot;
}
