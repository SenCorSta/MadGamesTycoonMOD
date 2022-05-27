using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_Restbestand : MonoBehaviour
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
		if (this.updateTimer < 0.3f)
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
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.game_.GetLagerbestand(), false);
		this.uiObjects[3].GetComponent<Text>().text = this.mS_.GetMoney((long)this.game_.lagerbestand[0], false);
		this.uiObjects[4].GetComponent<Text>().text = this.mS_.GetMoney((long)this.game_.lagerbestand[1], false);
		this.uiObjects[5].GetComponent<Text>().text = this.mS_.GetMoney((long)this.game_.lagerbestand[2], false);
		this.tooltip_.c = this.game_.GetTooltip();
		if (!this.menu_.CheckGameData(this.game_))
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
		if (!this.menu_.CheckGameData(this.game_))
		{
			return;
		}
		this.guiMain_.uiObjects[226].SetActive(true);
		this.guiMain_.uiObjects[226].GetComponent<Menu_W_Restbestand>().Init(this.game_);
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

	
	public Menu_LagerSelect menu_;

	
	private float updateTimer;
}
