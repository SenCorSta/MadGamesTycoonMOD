using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_MyGames_MyIPs : MonoBehaviour
{
	
	private void Start()
	{
		this.SetData();
	}

	
	public void Update()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetIpName();
	}

	
	public void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		if (this.game_.playerGame)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
		}
		if (this.mS_.multiplayer && !this.game_.playerGame && this.game_.multiplayerSlot != -1 && this.game_.multiplayerSlot != this.mS_.GetMyMultiplayerID())
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[8];
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetIpName();
		this.guiMain_.DrawIpBekanntheit(this.uiObjects[1], this.game_);
		this.tooltip_.c = this.game_.GetTooltipIP();
	}

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		if (this.guiMain_.uiObjects[315].activeSelf)
		{
			this.guiMain_.uiObjects[316].SetActive(true);
			this.guiMain_.uiObjects[316].GetComponent<Menu_Stats_ShowMyIPs>().Init(this.game_);
			return;
		}
		if (this.guiMain_.uiObjects[355].activeSelf || this.guiMain_.uiObjects[361].activeSelf)
		{
			this.guiMain_.uiObjects[356].SetActive(true);
			this.guiMain_.uiObjects[356].GetComponent<Menu_Stats_ShowBestIPs>().Init(this.game_);
			return;
		}
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
