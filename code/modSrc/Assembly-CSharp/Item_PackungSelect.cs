using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_PackungSelect : MonoBehaviour
{
	
	private void Start()
	{
		this.SetData();
	}

	
	private void Update()
	{
		this.DataUpdate();
	}

	
	private void DataUpdate()
	{
		this.updateTimer += Time.deltaTime;
		if (this.updateTimer < 1f)
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
		this.uiObjects[1].GetComponent<Image>().sprite = this.game_.GetScreenshot();
		this.uiObjects[7].GetComponent<Image>().sprite = this.game_.GetPlatformTypSprite();
		this.uiObjects[2].GetComponent<Text>().text = this.game_.GetLagerbestandString();
		this.uiObjects[3].GetComponent<Text>().text = this.mS_.GetMoney((long)this.game_.verkaufspreis[0], true);
		this.uiObjects[4].GetComponent<Text>().text = this.mS_.GetMoney((long)this.game_.verkaufspreis[1], true);
		this.uiObjects[5].GetComponent<Text>().text = this.mS_.GetMoney((long)this.game_.verkaufspreis[2], true);
		this.uiObjects[6].GetComponent<Text>().text = this.mS_.GetMoney((long)this.game_.verkaufspreis[3], true);
		if (!this.game_.digitalVersion)
		{
			this.uiObjects[6].GetComponent<Text>().text = "-";
		}
		if (!this.game_.retailVersion)
		{
			this.uiObjects[3].GetComponent<Text>().text = "-";
			this.uiObjects[4].GetComponent<Text>().text = "-";
			this.uiObjects[5].GetComponent<Text>().text = "-";
		}
		if (this.game_.typ_budget)
		{
			this.uiObjects[4].GetComponent<Text>().text = "-";
			this.uiObjects[5].GetComponent<Text>().text = "-";
		}
		this.tooltip_.c = this.game_.GetTooltip();
		if (this.game_.arcade)
		{
			this.uiObjects[4].GetComponent<Text>().text = "-";
			this.uiObjects[5].GetComponent<Text>().text = "-";
			this.uiObjects[6].GetComponent<Text>().text = "-";
		}
		if (this.mS_.multiplayer && !this.guiMain_.uiObjects[220].GetComponent<Menu_PackungSelect>().CheckGameData(this.game_))
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
		if (this.game_.handy)
		{
			this.guiMain_.uiObjects[301].SetActive(true);
			this.guiMain_.uiObjects[301].GetComponent<Menu_HandyPreis>().Init(this.game_);
			return;
		}
		if (this.game_.arcade)
		{
			this.guiMain_.uiObjects[307].SetActive(true);
			this.guiMain_.uiObjects[307].GetComponent<Menu_ArcadePreis>().Init(this.game_, null);
			return;
		}
		this.guiMain_.uiObjects[218].SetActive(true);
		this.guiMain_.uiObjects[218].GetComponent<Menu_Packung>().Init(this.game_, null, false, false);
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
