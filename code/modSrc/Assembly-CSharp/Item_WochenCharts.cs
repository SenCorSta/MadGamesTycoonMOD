using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_WochenCharts : MonoBehaviour
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
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.game_.sellsPerWeek[0], false);
		this.uiObjects[3].GetComponent<Image>().sprite = this.game_.GetTypSprite();
		this.tooltip_.c = this.game_.GetTooltip();
	}

	
	private void Update()
	{
		int siblingIndex = base.gameObject.transform.GetSiblingIndex();
		this.uiObjects[1].GetComponent<Text>().text = (siblingIndex + 1).ToString();
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.game_.sellsPerWeek[0], false);
		base.gameObject.name = this.game_.sellsTotal.ToString();
		if (this.game_.lastChartPosition < siblingIndex)
		{
			this.uiObjects[4].GetComponent<Text>().text = "<color=red>▼</color>";
		}
		if (this.game_.lastChartPosition > siblingIndex)
		{
			this.uiObjects[4].GetComponent<Text>().text = "<color=green>▲</color>";
		}
		if (this.game_.lastChartPosition == siblingIndex)
		{
			this.uiObjects[4].GetComponent<Text>().text = "<color=black>●</color>";
		}
		if (this.game_.lastChartPosition == -1)
		{
			this.uiObjects[4].GetComponent<Text>().text = "<color=blue>◆</color>";
		}
		if (!this.mS_.multiplayer)
		{
			return;
		}
		if (!this.game_.isOnMarket)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
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

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public GUI_Main guiMain_;

	
	public tooltip tooltip_;

	
	public gameScript game_;

	
	public genres genres_;
}
