using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_BuyLicence : MonoBehaviour
{
	
	private void Start()
	{
		this.SetData();
	}

	
	private void Update()
	{
		if (this.licences_.licence_ANGEBOT[this.myID] <= 0 && this.licences_.licence_GEKAUFT[this.myID] <= 0)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
	}

	
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.licences_.GetName(this.myID);
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)this.licences_.GetPrice(this.myID), true);
		this.uiObjects[5].GetComponent<Image>().sprite = this.licences_.licenceSprites[this.licences_.licence_TYP[this.myID]];
		this.guiMain_.DrawStars(this.uiObjects[2], Mathf.RoundToInt(this.licences_.licence_QUALITY[this.myID] / 20f));
		string text = this.tS_.GetText(297);
		text = text.Replace("<NUM>", this.licences_.licence_ANGEBOT[this.myID].ToString());
		this.uiObjects[3].GetComponent<Text>().text = text;
		this.uiObjects[4].GetComponent<Text>().text = this.licences_.GetTypString(this.myID);
		this.tooltip_.c = this.licences_.GetTooltip(this.myID);
		if (this.licences_.licence_GEKAUFT[this.myID] > 0)
		{
			base.GetComponent<Button>().interactable = false;
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(307);
		}
	}

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[53]);
		this.guiMain_.uiObjects[53].GetComponent<Menu_W_BuyLicence>().Init(this.myID);
	}

	
	public int myID = -1;

	
	public licences licences_;

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public GUI_Main guiMain_;

	
	public tooltip tooltip_;
}
