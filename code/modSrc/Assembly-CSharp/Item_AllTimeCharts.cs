﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Item_AllTimeCharts : MonoBehaviour
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
		if (this.game_.ownerID == this.mS_.myID || this.game_.publisherID == this.mS_.myID)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
		}
		if (this.mS_.multiplayer && this.game_.GameFromMitspieler())
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[8];
		}
		this.uiObjects[1].GetComponent<Text>().text = (base.gameObject.transform.GetSiblingIndex() + 1).ToString();
		if (this.guiMain_.uiObjects[375].activeSelf)
		{
			this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney(this.game_.umsatzTotal, true);
		}
		else
		{
			this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney(this.game_.sellsTotal, false);
		}
		this.uiObjects[3].GetComponent<Image>().sprite = this.game_.GetTypSprite();
		base.StartCoroutine(this.iSetTooltip());
	}

	
	private void Update()
	{
		this.uiObjects[1].GetComponent<Text>().text = (base.gameObject.transform.GetSiblingIndex() + 1).ToString();
		if (!this.mS_.multiplayer)
		{
			return;
		}
		if (this.guiMain_.uiObjects[375].activeSelf)
		{
			this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney(this.game_.umsatzTotal, true);
			base.gameObject.name = this.game_.umsatzTotal.ToString();
			return;
		}
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney(this.game_.sellsTotal, false);
		base.gameObject.name = this.game_.sellsTotal.ToString();
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

	
	private IEnumerator iSetTooltip()
	{
		yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 1f));
		if (this.game_)
		{
			this.tooltip_.c = this.game_.GetTooltip();
		}
		yield break;
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
