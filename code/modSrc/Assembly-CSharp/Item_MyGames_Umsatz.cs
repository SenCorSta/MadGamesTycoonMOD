using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_MyGames_Umsatz : MonoBehaviour
{
	
	private void Start()
	{
		this.SetData();
	}

	
	private void Update()
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
		if (this.game_.pubOffer)
		{
			this.uiObjects[0].GetComponent<Text>().color = this.guiMain_.colors[23];
		}
		this.uiObjects[1].GetComponent<Text>().text = (base.gameObject.transform.GetSiblingIndex() + 1).ToString();
		this.uiObjects[3].GetComponent<Image>().sprite = this.game_.GetTypSprite();
		if (this.tooltip_.c.Length <= 0)
		{
			this.tooltip_.c = this.game_.GetTooltip();
		}
		long num = 0L;
		switch (this.menu_.uiObjects[4].GetComponent<Dropdown>().value)
		{
		case 0:
			num = this.game_.GetGesamtGewinn();
			break;
		case 1:
			num = this.game_.umsatzTotal;
			break;
		case 2:
			num = this.game_.umsatzAbos;
			break;
		case 3:
			num = this.game_.umsatzInApp;
			break;
		case 4:
			num = this.game_.GetEntwicklungskosten();
			break;
		}
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney(num, true);
		if (num < 0L)
		{
			this.uiObjects[2].GetComponent<Text>().color = this.guiMain_.colors[5];
			return;
		}
		this.uiObjects[2].GetComponent<Text>().color = this.guiMain_.colors[15];
	}

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[91]);
		this.guiMain_.uiObjects[91].GetComponent<Menu_Game_Umsatz>().Init(this.game_);
	}

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public GUI_Main guiMain_;

	
	public tooltip tooltip_;

	
	public gameScript game_;

	
	public genres genres_;

	
	public Menu_Stats_MyGames_Umsatz menu_;
}
