using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000339 RID: 825
public class publishingOfferMain : MonoBehaviour
{
	// Token: 0x06001D9E RID: 7582 RVA: 0x000141F1 File Offset: 0x000123F1
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001D9F RID: 7583 RVA: 0x0012C0EC File Offset: 0x0012A2EC
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

	// Token: 0x06001DA0 RID: 7584 RVA: 0x0012C248 File Offset: 0x0012A448
	public publishingOffer CreatePublishingOffer()
	{
		publishingOffer component = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0]).GetComponent<publishingOffer>();
		component.main_ = this.main_;
		component.mS_ = this.mS_;
		component.tS_ = this.tS_;
		component.genres_ = this.genres_;
		return component;
	}

	// Token: 0x06001DA1 RID: 7585 RVA: 0x0012C298 File Offset: 0x0012A498
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

	// Token: 0x06001DA2 RID: 7586 RVA: 0x00002098 File Offset: 0x00000298
	public void UpdatePublishingOffer(bool forceNewPublishingOffer)
	{
	}

	// Token: 0x06001DA3 RID: 7587 RVA: 0x0011F870 File Offset: 0x0011DA70
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

	// Token: 0x06001DA4 RID: 7588 RVA: 0x0012C320 File Offset: 0x0012A520
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

	// Token: 0x0400259D RID: 9629
	public GameObject[] uiPrefabs;

	// Token: 0x0400259E RID: 9630
	public GameObject[] uiObjects;

	// Token: 0x0400259F RID: 9631
	private GameObject main_;

	// Token: 0x040025A0 RID: 9632
	private mainScript mS_;

	// Token: 0x040025A1 RID: 9633
	private textScript tS_;

	// Token: 0x040025A2 RID: 9634
	private GUI_Main guiMain_;

	// Token: 0x040025A3 RID: 9635
	private roomDataScript rdS_;

	// Token: 0x040025A4 RID: 9636
	private forschungSonstiges fS_;

	// Token: 0x040025A5 RID: 9637
	private genres genres_;

	// Token: 0x040025A6 RID: 9638
	private unlockScript unlock_;

	// Token: 0x040025A7 RID: 9639
	private forschungSonstiges forschungSonstiges_;

	// Token: 0x040025A8 RID: 9640
	private platforms platforms_;

	// Token: 0x040025A9 RID: 9641
	private games games_;

	// Token: 0x040025AA RID: 9642
	public int amountPublishingOffers;
}
