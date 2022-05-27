using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_MyKonsolen_Games : MonoBehaviour
{
	
	private void Start()
	{
		this.SetData();
	}

	
	private void Update()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.uiObjects[1].GetComponent<Text>().text = (base.gameObject.transform.GetSiblingIndex() + 1).ToString();
		string text = this.tS_.GetText(297);
		text = text.Replace("<NUM>", this.mS_.GetMoney((long)this.pS_.games, false));
		this.uiObjects[2].GetComponent<Text>().text = text;
		base.gameObject.name = this.pS_.games.ToString();
	}

	
	public void SetData()
	{
		if (!this.pS_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
		this.uiObjects[1].GetComponent<Text>().text = (base.gameObject.transform.GetSiblingIndex() + 1).ToString();
		this.uiObjects[3].GetComponent<Image>().sprite = this.pS_.GetTypSprite();
		string text = this.tS_.GetText(297);
		text = text.Replace("<NUM>", this.mS_.GetMoney((long)this.pS_.games, false));
		this.uiObjects[2].GetComponent<Text>().text = text;
		this.tooltip_.c = this.pS_.GetTooltip();
	}

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[340].SetActive(true);
		this.guiMain_.uiObjects[340].GetComponent<Menu_ShowKonsoleGames>().Init(this.pS_);
	}

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public GUI_Main guiMain_;

	
	public tooltip tooltip_;

	
	public platformScript pS_;
}
