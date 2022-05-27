using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000258 RID: 600
public class Menu_Stats_TochterfirmaIpTausch : MonoBehaviour
{
	// Token: 0x0600175F RID: 5983 RVA: 0x000EAB1F File Offset: 0x000E8D1F
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001760 RID: 5984 RVA: 0x000EAB28 File Offset: 0x000E8D28
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
		if (!this.rdS_)
		{
			this.rdS_ = this.main_.GetComponent<roomDataScript>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.pcS_)
		{
			this.pcS_ = this.main_.GetComponent<pickCharacterScript>();
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
	}

	// Token: 0x06001761 RID: 5985 RVA: 0x000EAC2C File Offset: 0x000E8E2C
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
			this.uiObjects[3].GetComponent<Scrollbar>().value = 5f;
		}
	}

	// Token: 0x06001762 RID: 5986 RVA: 0x000EAC80 File Offset: 0x000E8E80
	public void InitDropdowns(publisherScript selected_)
	{
		this.FindScripts();
		this.publisherList.Clear();
		GameObject[] array = GameObject.FindGameObjectsWithTag("Publisher");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				publisherScript component = array[i].GetComponent<publisherScript>();
				if (component && component.isUnlocked && component.IsMyTochterfirma())
				{
					this.publisherList.Add(component);
				}
			}
		}
		this.publisherList.Sort((publisherScript a, publisherScript b) => a.GetName().CompareTo(b.GetName()));
		this.publisherList.Insert(0, this.mS_.myPubS_);
		int value = 0;
		if (selected_)
		{
			for (int j = 0; j < this.publisherList.Count; j++)
			{
				if (this.publisherList[j] && selected_ == this.publisherList[j])
				{
					value = j;
					break;
				}
			}
		}
		List<string> list = new List<string>();
		for (int k = 0; k < this.publisherList.Count; k++)
		{
			list.Add(this.publisherList[k].GetName());
		}
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = value;
		List<string> list2 = new List<string>();
		for (int l = 0; l < this.publisherList.Count; l++)
		{
			list2.Add(this.publisherList[l].GetName());
		}
		this.uiObjects[6].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[6].GetComponent<Dropdown>().AddOptions(list2);
		this.uiObjects[6].GetComponent<Dropdown>().value = 0;
	}

	// Token: 0x06001763 RID: 5987 RVA: 0x000EAE6E File Offset: 0x000E906E
	public void Init(publisherScript script_)
	{
		this.init = false;
		this.FindScripts();
		this.InitDropdowns(script_);
		this.SetDataLeft();
		this.SetDataRight();
		this.IsSamePublisher();
		base.StartCoroutine(this.iInit());
	}

	// Token: 0x06001764 RID: 5988 RVA: 0x000EAEA4 File Offset: 0x000E90A4
	public void SetDataLeft()
	{
		Debug.Log("KJLKJLIKHJLIK" + UnityEngine.Random.Range(0, 100000));
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int j = 0; j < array.Length; j++)
		{
			if (array[j])
			{
				gameScript component = array[j].GetComponent<gameScript>();
				if (component && this.CheckGameDataLeft(component))
				{
					Item_TochterfirmaIpTausch component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_TochterfirmaIpTausch>();
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.genres_ = this.genres_;
					component2.game_ = component;
				}
			}
		}
		this.mS_.SortChildrenByName(this.uiObjects[0]);
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[7]);
		this.IsSamePublisher();
	}

	// Token: 0x06001765 RID: 5989 RVA: 0x000EB004 File Offset: 0x000E9204
	public void SetDataRight()
	{
		for (int i = 0; i < this.uiObjects[4].transform.childCount; i++)
		{
			this.uiObjects[4].transform.GetChild(i).gameObject.SetActive(false);
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int j = 0; j < array.Length; j++)
		{
			if (array[j])
			{
				gameScript component = array[j].GetComponent<gameScript>();
				if (component && this.CheckGameDataRight(component))
				{
					Item_TochterfirmaIpTausch component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[4].transform).GetComponent<Item_TochterfirmaIpTausch>();
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.genres_ = this.genres_;
					component2.game_ = component;
				}
			}
		}
		this.mS_.SortChildrenByName(this.uiObjects[4]);
		this.guiMain_.KeinEintrag(this.uiObjects[4], this.uiObjects[8]);
		this.IsSamePublisher();
	}

	// Token: 0x06001766 RID: 5990 RVA: 0x000EB143 File Offset: 0x000E9343
	public bool CheckGameDataLeft(gameScript script_)
	{
		return script_ && !script_.pubAngebot && !script_.auftragsspiel && script_.IsMyIP(this.GetLeftPublisher()) && script_.mainIP == script_.myID;
	}

	// Token: 0x06001767 RID: 5991 RVA: 0x000EB17C File Offset: 0x000E937C
	public bool CheckGameDataRight(gameScript script_)
	{
		return script_ && !script_.pubAngebot && !script_.auftragsspiel && script_.IsMyIP(this.GetRightPublisher()) && script_.mainIP == script_.myID;
	}

	// Token: 0x06001768 RID: 5992 RVA: 0x000EB1B8 File Offset: 0x000E93B8
	private void IsSamePublisher()
	{
		if (this.GetLeftPublisher() == this.GetRightPublisher())
		{
			for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
			{
				this.uiObjects[0].transform.GetChild(i).gameObject.GetComponent<Button>().interactable = false;
			}
			for (int j = 0; j < this.uiObjects[4].transform.childCount; j++)
			{
				this.uiObjects[4].transform.GetChild(j).gameObject.GetComponent<Button>().interactable = false;
			}
			return;
		}
		for (int k = 0; k < this.uiObjects[0].transform.childCount; k++)
		{
			this.uiObjects[0].transform.GetChild(k).gameObject.GetComponent<Button>().interactable = true;
		}
		for (int l = 0; l < this.uiObjects[4].transform.childCount; l++)
		{
			this.uiObjects[4].transform.GetChild(l).gameObject.GetComponent<Button>().interactable = true;
		}
	}

	// Token: 0x06001769 RID: 5993 RVA: 0x000EB2DC File Offset: 0x000E94DC
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600176A RID: 5994 RVA: 0x000EB2F7 File Offset: 0x000E94F7
	public publisherScript GetLeftPublisher()
	{
		return this.publisherList[this.uiObjects[1].GetComponent<Dropdown>().value];
	}

	// Token: 0x0600176B RID: 5995 RVA: 0x000EB316 File Offset: 0x000E9516
	public publisherScript GetRightPublisher()
	{
		return this.publisherList[this.uiObjects[6].GetComponent<Dropdown>().value];
	}

	// Token: 0x0600176C RID: 5996 RVA: 0x000EB335 File Offset: 0x000E9535
	public void DROPDOWN_LeftPublisher()
	{
		if (this.init)
		{
			this.SetDataLeft();
		}
	}

	// Token: 0x0600176D RID: 5997 RVA: 0x000EB345 File Offset: 0x000E9545
	public void DROPDOWN_RightPublisher()
	{
		if (this.init)
		{
			this.SetDataRight();
		}
	}

	// Token: 0x0600176E RID: 5998 RVA: 0x000EB355 File Offset: 0x000E9555
	private IEnumerator iInit()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		this.init = true;
		yield break;
	}

	// Token: 0x04001B2E RID: 6958
	private mainScript mS_;

	// Token: 0x04001B2F RID: 6959
	private GameObject main_;

	// Token: 0x04001B30 RID: 6960
	private GUI_Main guiMain_;

	// Token: 0x04001B31 RID: 6961
	private sfxScript sfx_;

	// Token: 0x04001B32 RID: 6962
	private textScript tS_;

	// Token: 0x04001B33 RID: 6963
	private pickCharacterScript pcS_;

	// Token: 0x04001B34 RID: 6964
	private roomDataScript rdS_;

	// Token: 0x04001B35 RID: 6965
	private genres genres_;

	// Token: 0x04001B36 RID: 6966
	public GameObject[] uiPrefabs;

	// Token: 0x04001B37 RID: 6967
	public GameObject[] uiObjects;

	// Token: 0x04001B38 RID: 6968
	public List<publisherScript> publisherList = new List<publisherScript>();

	// Token: 0x04001B39 RID: 6969
	public bool init;
}
