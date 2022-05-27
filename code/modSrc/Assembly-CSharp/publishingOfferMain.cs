using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200033C RID: 828
public class publishingOfferMain : MonoBehaviour
{
	// Token: 0x06001DF5 RID: 7669 RVA: 0x0012ADEA File Offset: 0x00128FEA
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001DF6 RID: 7670 RVA: 0x0012ADF4 File Offset: 0x00128FF4
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

	// Token: 0x06001DF7 RID: 7671 RVA: 0x0012AF50 File Offset: 0x00129150
	public publishingOffer CreatePublishingOffer()
	{
		publishingOffer component = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0]).GetComponent<publishingOffer>();
		component.main_ = this.main_;
		component.mS_ = this.mS_;
		component.tS_ = this.tS_;
		component.genres_ = this.genres_;
		return component;
	}

	// Token: 0x06001DF8 RID: 7672 RVA: 0x0012AFA0 File Offset: 0x001291A0
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

	// Token: 0x06001DF9 RID: 7673 RVA: 0x00002715 File Offset: 0x00000915
	public void UpdatePublishingOffer(bool forceNewPublishingOffer)
	{
	}

	// Token: 0x06001DFA RID: 7674 RVA: 0x0012B028 File Offset: 0x00129228
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

	// Token: 0x06001DFB RID: 7675 RVA: 0x0012B0A8 File Offset: 0x001292A8
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

	// Token: 0x040025B4 RID: 9652
	public GameObject[] uiPrefabs;

	// Token: 0x040025B5 RID: 9653
	public GameObject[] uiObjects;

	// Token: 0x040025B6 RID: 9654
	private GameObject main_;

	// Token: 0x040025B7 RID: 9655
	private mainScript mS_;

	// Token: 0x040025B8 RID: 9656
	private textScript tS_;

	// Token: 0x040025B9 RID: 9657
	private GUI_Main guiMain_;

	// Token: 0x040025BA RID: 9658
	private roomDataScript rdS_;

	// Token: 0x040025BB RID: 9659
	private forschungSonstiges fS_;

	// Token: 0x040025BC RID: 9660
	private genres genres_;

	// Token: 0x040025BD RID: 9661
	private unlockScript unlock_;

	// Token: 0x040025BE RID: 9662
	private forschungSonstiges forschungSonstiges_;

	// Token: 0x040025BF RID: 9663
	private platforms platforms_;

	// Token: 0x040025C0 RID: 9664
	private games games_;

	// Token: 0x040025C1 RID: 9665
	public int amountPublishingOffers;
}
