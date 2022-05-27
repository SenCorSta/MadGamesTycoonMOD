﻿using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_MyKonsolen_AllTimeCharts : MonoBehaviour
{
	
	private void Start()
	{
		this.SetData();
	}

	
	private void Update()
	{
		this.uiObjects[1].GetComponent<Text>().text = (base.gameObject.transform.GetSiblingIndex() + 1).ToString();
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.pS_.units, false);
		base.gameObject.name = this.pS_.units.ToString();
	}

	
	public void SetData()
	{
		if (!this.pS_)
		{
			return;
		}
		if (this.pS_.playerConsole)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
		}
		else
		{
			base.GetComponent<Button>().interactable = false;
		}
		if (this.mS_.multiplayer && !this.pS_.playerConsole && this.pS_.multiplaySlot != -1)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[8];
		}
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
		this.uiObjects[1].GetComponent<Text>().text = (base.gameObject.transform.GetSiblingIndex() + 1).ToString();
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.pS_.units, false);
		this.uiObjects[3].GetComponent<Image>().sprite = this.pS_.GetTypSprite();
		this.tooltip_.c = this.pS_.GetTooltip();
	}

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[333]);
		this.guiMain_.uiObjects[333].GetComponent<Menu_Umsatz_Konsole>().Init(this.pS_);
	}

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public GUI_Main guiMain_;

	
	public tooltip tooltip_;

	
	public platformScript pS_;
}
