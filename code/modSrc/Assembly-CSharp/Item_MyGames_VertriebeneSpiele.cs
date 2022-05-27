using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_MyGames_VertriebeneSpiele : MonoBehaviour
{
	
	private void Start()
	{
		this.SetData();
	}

	
	private void Update()
	{
		this.uiObjects[1].GetComponent<Text>().text = (base.gameObject.transform.GetSiblingIndex() + 1).ToString();
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney(this.game_.GetGesamtGewinn(), true);
		base.gameObject.name = this.game_.reviewTotal.ToString();
	}

	
	public void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		long gesamtGewinn = this.game_.GetGesamtGewinn();
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney(gesamtGewinn, true);
		if (gesamtGewinn < 0L)
		{
			this.uiObjects[2].GetComponent<Text>().color = this.guiMain_.colors[5];
		}
		else
		{
			this.uiObjects[2].GetComponent<Text>().color = this.guiMain_.colors[15];
		}
		if (!this.game_.devS_)
		{
			this.game_.FindMyDeveloper();
		}
		if (this.game_.devS_)
		{
			this.uiObjects[3].GetComponent<Image>().sprite = this.game_.devS_.GetLogo();
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
}
