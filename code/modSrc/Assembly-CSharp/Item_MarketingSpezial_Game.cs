using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_MarketingSpezial_Game : MonoBehaviour
{
	
	private void Start()
	{
		this.SetData();
	}

	
	private void Update()
	{
		if (this.game_ && !this.game_.inDevelopment && !this.game_.isOnMarket && !this.game_.schublade)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
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

	
	public void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		this.uiObjects[1].GetComponent<Text>().text = Mathf.RoundToInt(this.game_.GetHype()).ToString();
		this.uiObjects[2].GetComponent<Text>().text = this.game_.GetReleaseDateString();
		if (this.game_.specialMarketing[0] == 0)
		{
			this.uiObjects[3].GetComponent<Image>().color = this.guiMain_.colors[6];
		}
		if (this.game_.specialMarketing[1] == 0)
		{
			this.uiObjects[4].GetComponent<Image>().color = this.guiMain_.colors[6];
		}
		if (this.game_.specialMarketing[2] == 0)
		{
			this.uiObjects[5].GetComponent<Image>().color = this.guiMain_.colors[6];
		}
		if (this.game_.specialMarketing[3] == 0)
		{
			this.uiObjects[6].GetComponent<Image>().color = this.guiMain_.colors[6];
		}
		if (this.game_.specialMarketing[4] == 0)
		{
			this.uiObjects[7].GetComponent<Image>().color = this.guiMain_.colors[6];
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
		this.guiMain_.uiObjects[295].SetActive(false);
		this.guiMain_.uiObjects[294].GetComponent<Menu_MarketingSpezial>().SetGame(this.game_);
	}

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public GUI_Main guiMain_;

	
	public tooltip tooltip_;

	
	public gameScript game_;

	
	public genres genres_;

	
	private float updateTimer;
}
