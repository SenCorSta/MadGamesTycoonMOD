﻿using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_BestMMO : MonoBehaviour
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
		if (this.game_.playerGame)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
		}
		if (this.mS_.multiplayer && !this.game_.playerGame && this.game_.multiplayerSlot != -1)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[8];
		}
		this.uiObjects[1].GetComponent<Text>().text = (base.gameObject.transform.GetSiblingIndex() + 1).ToString();
		if (this.sort == 0)
		{
			this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.game_.abonnements, false);
		}
		if (this.sort == 1)
		{
			this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.game_.bestAbonnements, false);
		}
		this.uiObjects[3].GetComponent<Image>().sprite = this.genres_.GetPic(this.game_.maingenre);
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

	
	public int sort;
}
