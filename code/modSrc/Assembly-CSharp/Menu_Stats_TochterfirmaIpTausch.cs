using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Stats_TochterfirmaIpTausch : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
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

	
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
			this.uiObjects[3].GetComponent<Scrollbar>().value = 5f;
		}
	}

	
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

	
	public bool CheckGameDataLeft(gameScript script_)
	{
		return script_ && !script_.pubAngebot && !script_.auftragsspiel && script_.IsMyIP(this.GetLeftPublisher()) && script_.mainIP == script_.myID;
	}

	
	public bool CheckGameDataRight(gameScript script_)
	{
		return script_ && !script_.pubAngebot && !script_.auftragsspiel && script_.IsMyIP(this.GetRightPublisher()) && script_.mainIP == script_.myID;
	}

	
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

	
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public publisherScript GetLeftPublisher()
	{
		return this.publisherList[this.uiObjects[1].GetComponent<Dropdown>().value];
	}

	
	public publisherScript GetRightPublisher()
	{
		return this.publisherList[this.uiObjects[6].GetComponent<Dropdown>().value];
	}

	
	public void DROPDOWN_LeftPublisher()
	{
		if (this.init)
		{
			this.SetDataLeft();
		}
	}

	
	public void DROPDOWN_RightPublisher()
	{
		if (this.init)
		{
			this.SetDataRight();
		}
	}

	
	private IEnumerator iInit()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		this.init = true;
		yield break;
	}

	
	private mainScript mS_;

	
	private GameObject main_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private textScript tS_;

	
	private pickCharacterScript pcS_;

	
	private roomDataScript rdS_;

	
	private genres genres_;

	
	public GameObject[] uiPrefabs;

	
	public GameObject[] uiObjects;

	
	public List<publisherScript> publisherList = new List<publisherScript>();

	
	public bool init;
}
