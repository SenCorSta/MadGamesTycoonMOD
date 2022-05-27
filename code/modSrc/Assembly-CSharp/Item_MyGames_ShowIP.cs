﻿using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_MyGames_ShowIP : MonoBehaviour
{
	
	private void Start()
	{
		this.SetData();
	}

	
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	
	private void MultiplayerUpdate()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.updateTimer += Time.deltaTime;
		if (this.updateTimer < 5f)
		{
			return;
		}
		this.updateTimer = 0f;
		this.SetData();
	}

	
	private void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		this.uiObjects[3].GetComponent<Text>().text = this.game_.GetReleaseDateString();
		if (this.game_.gameTyp != 2)
		{
			this.uiObjects[4].GetComponent<Text>().text = this.tS_.GetText(275) + ": " + this.mS_.GetMoney(this.game_.sellsTotal, false);
		}
		else
		{
			this.uiObjects[4].GetComponent<Text>().text = this.tS_.GetText(697) + ": " + this.mS_.GetMoney(this.game_.sellsTotal, false);
		}
		this.uiObjects[5].GetComponent<Text>().text = this.tS_.GetText(277) + ": " + Mathf.RoundToInt((float)this.game_.reviewTotal).ToString() + "%";
		this.uiObjects[2].GetComponent<Image>().sprite = this.game_.GetScreenshot();
		this.uiObjects[1].GetComponent<Image>().sprite = this.genres_.GetPic(this.game_.maingenre);
		this.uiObjects[7].GetComponent<Image>().sprite = this.game_.GetPlatformTypSprite();
		this.uiObjects[6].GetComponent<Image>().sprite = this.game_.GetTypSprite();
		this.tooltip_.c = this.game_.GetTooltip();
	}

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public void BUTTON_Click()
	{
		if (this.game_.reviewTotal > 0)
		{
			this.sfx_.PlaySound(3, true);
			this.guiMain_.uiObjects[46].SetActive(true);
			this.guiMain_.uiObjects[46].GetComponent<Menu_Review>().Init(this.game_);
		}
	}

	
	public gameScript game_;

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public GUI_Main guiMain_;

	
	public tooltip tooltip_;

	
	public genres genres_;

	
	private float updateTimer;
}
