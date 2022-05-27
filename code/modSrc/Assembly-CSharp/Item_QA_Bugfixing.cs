﻿using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_QA_Bugfixing : MonoBehaviour
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
		this.uiObjects[1].GetComponent<Image>().sprite = this.genres_.GetPic(this.game_.maingenre);
		this.uiObjects[1].GetComponent<tooltip>().c = this.genres_.GetName(this.game_.maingenre);
		this.uiObjects[2].GetComponent<Image>().sprite = this.game_.GetTypSprite();
		this.uiObjects[3].GetComponent<Image>().sprite = this.game_.GetPlatformTypSprite();
		this.uiObjects[4].GetComponent<Image>().sprite = this.game_.GetScreenshot();
		this.uiObjects[5].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt(this.game_.points_bugs), false).ToString() + " " + this.tS_.GetText(424);
		base.GetComponent<tooltip>().c = this.game_.GetTooltip();
	}

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.uiObjects[171].GetComponent<Menu_QA_BugfixingSelectGame>().StartBugfixing(this.game_);
	}

	
	public gameScript game_;

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public GUI_Main guiMain_;

	
	public tooltip tooltip_;

	
	public genres genres_;

	
	public roomScript rS_;

	
	private float updateTimer;
}
