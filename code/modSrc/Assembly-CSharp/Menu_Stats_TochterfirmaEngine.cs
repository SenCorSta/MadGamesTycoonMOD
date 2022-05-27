using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000255 RID: 597
public class Menu_Stats_TochterfirmaEngine : MonoBehaviour
{
	// Token: 0x06001722 RID: 5922 RVA: 0x00010348 File Offset: 0x0000E548
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001723 RID: 5923 RVA: 0x000F04F0 File Offset: 0x000EE6F0
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

	// Token: 0x06001724 RID: 5924 RVA: 0x00010350 File Offset: 0x0000E550
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x06001725 RID: 5925 RVA: 0x000F05D8 File Offset: 0x000EE7D8
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

	// Token: 0x06001726 RID: 5926 RVA: 0x000F062C File Offset: 0x000EE82C
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

	// Token: 0x06001727 RID: 5927 RVA: 0x000F0754 File Offset: 0x000EE954
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

	// Token: 0x06001728 RID: 5928 RVA: 0x000F07B8 File Offset: 0x000EE9B8
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Engine");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				engineScript component = array[i].GetComponent<engineScript>();
				if (component && !component.archiv_engine && ((component.playerEngine && component.devPointsStart <= 0f) || (component.playerEngine && component.updating)) && !this.Exists(this.uiObjects[0], component.myID))
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

	// Token: 0x06001729 RID: 5929 RVA: 0x000F0908 File Offset: 0x000EEB08
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
					if (component.eS_.playerEngine)
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

	// Token: 0x0600172A RID: 5930 RVA: 0x00010382 File Offset: 0x0000E582
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600172B RID: 5931 RVA: 0x000F0B00 File Offset: 0x000EED00
	public void BUTTON_RemoveEngine()
	{
		this.sfx_.PlaySound(3, true);
		this.pS_.tf_engine = -1;
		this.guiMain_.uiObjects[393].GetComponent<Menu_Stats_TochterfirmaSettings>().UpdateData();
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001B11 RID: 6929
	public GameObject[] uiPrefabs;

	// Token: 0x04001B12 RID: 6930
	public GameObject[] uiObjects;

	// Token: 0x04001B13 RID: 6931
	private mainScript mS_;

	// Token: 0x04001B14 RID: 6932
	private GameObject main_;

	// Token: 0x04001B15 RID: 6933
	private GUI_Main guiMain_;

	// Token: 0x04001B16 RID: 6934
	private sfxScript sfx_;

	// Token: 0x04001B17 RID: 6935
	private textScript tS_;

	// Token: 0x04001B18 RID: 6936
	private engineFeatures eF_;

	// Token: 0x04001B19 RID: 6937
	private genres genres_;

	// Token: 0x04001B1A RID: 6938
	private publisherScript pS_;
}
