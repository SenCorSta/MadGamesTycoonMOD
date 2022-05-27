using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_PublisherExklusiv : MonoBehaviour
{
	
	private void Start()
	{
		this.SetData();
	}

	
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
		this.uiObjects[1].GetComponent<Image>().sprite = this.pS_.GetLogo();
		this.uiObjects[5].GetComponent<Text>().text = this.mS_.GetMoney((long)this.pS_.GetShareExklusiv(), true);
		this.uiObjects[6].GetComponent<Text>().text = this.mS_.GetMoney((long)this.pS_.GetMoneyExklusiv(), true);
		this.guiMain_.DrawStars(this.uiObjects[3], Mathf.RoundToInt(this.pS_.stars / 20f));
		string text = this.tS_.GetText(1048);
		text = text.Replace("<NUM>", this.pS_.exklusivLaufzeit.ToString());
		this.uiObjects[7].GetComponent<Text>().text = text;
		this.tooltip_.c = this.pS_.GetTooltip();
	}

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[200].GetComponent<Menu_PublisherExklusiv>().SelectPublisher(this.pS_);
	}

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public genres genres_;

	
	public GUI_Main guiMain_;

	
	public tooltip tooltip_;

	
	public publisherScript pS_;
}
