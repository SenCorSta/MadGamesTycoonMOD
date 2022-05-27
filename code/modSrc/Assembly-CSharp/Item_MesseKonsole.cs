using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_MesseKonsole : MonoBehaviour
{
	
	private void Start()
	{
		this.SetData();
	}

	
	public void SetData()
	{
		if (!this.pS_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
		if (this.pS_.isUnlocked)
		{
			this.uiObjects[1].GetComponent<Text>().text = this.pS_.GetDateString();
		}
		else
		{
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(528);
		}
		this.pS_.SetPic(this.uiObjects[2]);
		this.uiObjects[3].GetComponent<Image>().sprite = this.pS_.GetTypSprite();
		this.uiObjects[4].GetComponent<Text>().text = Mathf.RoundToInt(this.pS_.GetHype()).ToString();
		Menu_MesseSelect component = this.guiMain_.uiObjects[186].GetComponent<Menu_MesseSelect>();
		if (component.konsolen[0] == this.pS_ || component.konsolen[1] == this.pS_)
		{
			base.GetComponent<Button>().interactable = false;
		}
		this.tooltip_.c = this.pS_.GetTooltip();
	}

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[186].GetComponent<Menu_MesseSelect>().SetKonsole(this.slot, this.pS_);
		this.guiMain_.uiObjects[323].SetActive(false);
	}

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public GUI_Main guiMain_;

	
	public tooltip tooltip_;

	
	public platformScript pS_;

	
	public int slot;
}
