using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_KonsolePreisSelect : MonoBehaviour
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
		if (!this.pS_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
		this.uiObjects[1].GetComponent<Text>().text = this.pS_.GetDateString();
		this.pS_.SetPic(this.uiObjects[2]);
		this.uiObjects[3].GetComponent<Text>().text = this.mS_.GetMoney((long)this.pS_.verkaufspreis, true);
		this.uiObjects[4].GetComponent<Text>().text = this.mS_.GetMoney((long)this.pS_.price, true);
		this.tooltip_.c = this.pS_.GetTooltip();
	}

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[328].SetActive(true);
		this.guiMain_.uiObjects[328].GetComponent<Menu_Konsolenpreis>().Init(this.pS_, null);
	}

	
	public platformScript pS_;

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public GUI_Main guiMain_;

	
	public tooltip tooltip_;

	
	public genres genres_;

	
	public Menu_Bundle menu_;

	
	private float updateTimer;
}
