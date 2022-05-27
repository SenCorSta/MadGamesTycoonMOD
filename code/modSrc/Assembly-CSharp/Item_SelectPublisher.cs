using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_SelectPublisher : MonoBehaviour
{
	
	private void Start()
	{
		this.SetData();
	}

	
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
		this.uiObjects[1].GetComponent<Image>().sprite = this.pS_.GetLogo();
		this.uiObjects[2].GetComponent<Image>().sprite = this.genres_.GetPic(this.pS_.fanGenre);
		this.uiObjects[5].GetComponent<Text>().text = "$" + this.pS_.GetShare().ToString();
		this.guiMain_.DrawStars(this.uiObjects[3], Mathf.RoundToInt(this.pS_.stars / 20f));
		this.guiMain_.DrawStarsColor(this.uiObjects[4], Mathf.RoundToInt(this.pS_.GetRelation() / 20f), this.guiMain_.colors[5]);
		this.tooltip_.c = this.pS_.GetTooltip();
		if (this.pS_.IsMyTochterfirma())
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
		}
	}

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[70].GetComponent<Menu_Dev_SelectPublisher>().SelectPublisher(this.pS_.myID);
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
