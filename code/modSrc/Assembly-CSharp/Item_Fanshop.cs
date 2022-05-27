using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_Fanshop : MonoBehaviour
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
		if (this.game_.merchKeinVerkauf)
		{
			this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.uiSprites[13];
			this.uiObjects[3].GetComponent<tooltip>().c = this.tS_.GetText(1853);
			return;
		}
		this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.uiSprites[14];
		this.uiObjects[3].GetComponent<tooltip>().c = this.tS_.GetText(1854);
	}

	
	public void SetData()
	{
		if (!this.game_)
		{
			return;
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
		this.guiMain_.uiObjects[367].SetActive(true);
		this.guiMain_.uiObjects[367].GetComponent<Menu_Fanshop>().Init(this.game_);
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
