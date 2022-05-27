using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000256 RID: 598
public class Menu_Stats_TochterfirmaEngine : MonoBehaviour
{
	// Token: 0x06001748 RID: 5960 RVA: 0x000E9E8A File Offset: 0x000E808A
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001749 RID: 5961 RVA: 0x000E9E94 File Offset: 0x000E8094
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
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
	}

	// Token: 0x0600174A RID: 5962 RVA: 0x000E9F7A File Offset: 0x000E817A
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x0600174B RID: 5963 RVA: 0x000E9FAC File Offset: 0x000E81AC
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			GameObject gameObject = parent_.transform.GetChild(i).gameObject;
			if (gameObject.activeSelf && gameObject.GetComponent<Item_TochterfirmaEngine>().eS_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600174C RID: 5964 RVA: 0x000EA000 File Offset: 0x000E8200
	public void InitDropdowns()
	{
		this.FindScripts();
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(4));
		list.Add(this.tS_.GetText(245));
		list.Add(this.tS_.GetText(160));
		list.Add(this.tS_.GetText(261));
		list.Add(this.tS_.GetText(258));
		list.Add(this.tS_.GetText(260));
		list.Add(this.tS_.GetText(268));
		list.Add(this.tS_.GetText(1218));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x0600174D RID: 5965 RVA: 0x000EA128 File Offset: 0x000E8328
	public void Init(publisherScript script_)
	{
		this.pS_ = script_;
		this.FindScripts();
		this.InitDropdowns();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x0600174E RID: 5966 RVA: 0x000EA18C File Offset: 0x000E838C
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Engine");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				engineScript component = array[i].GetComponent<engineScript>();
				if (component && !component.archiv_engine && ((component.ownerID == this.mS_.myID && component.devPointsStart <= 0f) || (component.ownerID == this.mS_.myID && component.updating)) && !this.Exists(this.uiObjects[0], component.myID))
				{
					Item_TochterfirmaEngine component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_TochterfirmaEngine>();
					component2.eS_ = component;
					component2.eF_ = this.eF_;
					component2.genres_ = this.genres_;
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.pS_ = this.pS_;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x0600174F RID: 5967 RVA: 0x000EA2F4 File Offset: 0x000E84F4
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
				Item_TochterfirmaEngine component = gameObject.GetComponent<Item_TochterfirmaEngine>();
				switch (value)
				{
				case 0:
					gameObject.name = component.eS_.GetName();
					break;
				case 1:
					gameObject.name = (component.eS_.GetTechLevel() * 1000 + component.eS_.GetFeaturesAmount()).ToString();
					break;
				case 2:
					gameObject.name = component.eS_.spezialgenre.ToString();
					break;
				case 3:
					gameObject.name = component.eS_.GetFeaturesAmount().ToString();
					break;
				case 4:
					gameObject.name = component.eS_.GetGamesAmount().ToString();
					break;
				case 5:
					if (component.eS_.ownerID == this.mS_.myID)
					{
						gameObject.name = "2";
					}
					else
					{
						gameObject.name = "1";
					}
					break;
				case 6:
					gameObject.name = component.eS_.gewinnbeteiligung.ToString();
					break;
				case 7:
					gameObject.name = component.eS_.GetVerkaufteLizenzen().ToString();
					break;
				case 8:
					gameObject.name = component.eS_.spezialplatform.ToString();
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

	// Token: 0x06001750 RID: 5968 RVA: 0x000EA4F7 File Offset: 0x000E86F7
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001751 RID: 5969 RVA: 0x000EA514 File Offset: 0x000E8714
	public void BUTTON_RemoveEngine()
	{
		this.sfx_.PlaySound(3, true);
		this.pS_.tf_engine = -1;
		this.guiMain_.uiObjects[393].GetComponent<Menu_Stats_TochterfirmaSettings>().UpdateData();
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001B1A RID: 6938
	public GameObject[] uiPrefabs;

	// Token: 0x04001B1B RID: 6939
	public GameObject[] uiObjects;

	// Token: 0x04001B1C RID: 6940
	private mainScript mS_;

	// Token: 0x04001B1D RID: 6941
	private GameObject main_;

	// Token: 0x04001B1E RID: 6942
	private GUI_Main guiMain_;

	// Token: 0x04001B1F RID: 6943
	private sfxScript sfx_;

	// Token: 0x04001B20 RID: 6944
	private textScript tS_;

	// Token: 0x04001B21 RID: 6945
	private engineFeatures eF_;

	// Token: 0x04001B22 RID: 6946
	private genres genres_;

	// Token: 0x04001B23 RID: 6947
	private publisherScript pS_;
}
