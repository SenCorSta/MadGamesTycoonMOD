﻿using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_InAppVerwalten : MonoBehaviour
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
		this.uiObjects[1].GetComponent<Image>().sprite = this.genres_.GetPic(this.game_.maingenre);
		this.uiObjects[6].GetComponent<Image>().sprite = this.game_.GetTypSprite();
		for (int i = 0; i < this.game_.inAppPurchase.Length; i++)
		{
			if (this.game_.inAppPurchase[i])
			{
				this.uiObjects[7 + i].GetComponent<Image>().color = Color.white;
			}
			else
			{
				this.uiObjects[7 + i].GetComponent<Image>().color = this.guiMain_.colors[6];
			}
		}
		this.tooltip_.c = this.game_.GetTooltip();
	}

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		if (!this.menu_.CheckGameData(this.game_))
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[278]);
		this.guiMain_.uiObjects[278].GetComponent<Menu_InAppPurchases>().Init(this.game_, false);
	}

	
	public gameScript game_;

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public GUI_Main guiMain_;

	
	public tooltip tooltip_;

	
	public genres genres_;

	
	public Menu_InAppVerwalten menu_;

	
	private float updateTimer;
}
