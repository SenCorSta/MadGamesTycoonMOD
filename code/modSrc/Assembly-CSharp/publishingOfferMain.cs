using System;
using UnityEngine;
using UnityEngine.UI;


public class publishingOfferMain : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindGameObjectWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.rdS_)
		{
			this.rdS_ = this.main_.GetComponent<roomDataScript>();
		}
		if (!this.fS_)
		{
			this.fS_ = this.main_.GetComponent<forschungSonstiges>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
		}
		if (!this.forschungSonstiges_)
		{
			this.forschungSonstiges_ = this.main_.GetComponent<forschungSonstiges>();
		}
		if (!this.platforms_)
		{
			this.platforms_ = this.main_.GetComponent<platforms>();
		}
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
	}

	
	public publishingOffer CreatePublishingOffer()
	{
		publishingOffer component = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0]).GetComponent<publishingOffer>();
		component.main_ = this.main_;
		component.mS_ = this.mS_;
		component.tS_ = this.tS_;
		component.genres_ = this.genres_;
		return component;
	}

	
	public void UpdateGUI()
	{
		if (this.amountPublishingOffers > 0 && this.forschungSonstiges_.IsErforscht(33))
		{
			if (!this.uiObjects[0].activeSelf)
			{
				this.uiObjects[0].SetActive(true);
			}
			this.uiObjects[0].transform.GetChild(0).GetComponent<Text>().text = this.amountPublishingOffers.ToString();
			return;
		}
		if (this.uiObjects[0].activeSelf)
		{
			this.uiObjects[0].SetActive(false);
		}
	}

	
	public void UpdatePublishingOffer(bool forceNewPublishingOffer)
	{
	}

	
	private int GetPlatform()
	{
		int result = 0;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Platform");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				platformScript component = array[i].GetComponent<platformScript>();
				if (component && component.isUnlocked && !component.vomMarktGenommen && component.typ != 3 && component.typ != 4)
				{
					result = component.myID;
					if (UnityEngine.Random.Range(0, 100) > 70)
					{
						return component.myID;
					}
				}
			}
		}
		return result;
	}

	
	private int GetRandomDeveloperID()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Publisher");
		if (array.Length != 0)
		{
			bool flag = false;
			while (!flag)
			{
				int num = UnityEngine.Random.Range(0, array.Length);
				if (array[num])
				{
					publisherScript component = array[num].GetComponent<publisherScript>();
					if (component && component.isUnlocked && component.developer && !component.publisher && !component.onlyMobile)
					{
						return component.myID;
					}
				}
			}
		}
		return 0;
	}

	
	public GameObject[] uiPrefabs;

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private roomDataScript rdS_;

	
	private forschungSonstiges fS_;

	
	private genres genres_;

	
	private unlockScript unlock_;

	
	private forschungSonstiges forschungSonstiges_;

	
	private platforms platforms_;

	
	private games games_;

	
	public int amountPublishingOffers;
}
