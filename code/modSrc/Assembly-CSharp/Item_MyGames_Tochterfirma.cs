﻿using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_MyGames_Tochterfirma : MonoBehaviour
{
	
	private void Start()
	{
		this.SetData();
	}

	
	public void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		this.uiObjects[2].GetComponent<Text>().text = string.Concat(new string[]
		{
			this.tS_.GetText(1291),
			": ",
			this.game_.GetUserReviewPercent().ToString(),
			"%  ",
			this.tS_.GetText(1292),
			": ",
			this.game_.reviewTotal.ToString(),
			"%"
		});
		this.uiObjects[1].GetComponent<Text>().text = (base.gameObject.transform.GetSiblingIndex() + 1).ToString();
		this.uiObjects[3].GetComponent<Image>().sprite = this.game_.GetDeveloperLogo();
		this.tooltip_.c = this.game_.GetTooltip();
	}

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[46].SetActive(true);
		this.guiMain_.uiObjects[46].GetComponent<Menu_Review>().Init(this.game_);
	}

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public GUI_Main guiMain_;

	
	public tooltip tooltip_;

	
	public gameScript game_;

	
	public genres genres_;
}
