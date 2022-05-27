using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_KonsoleDetailsSelect : MonoBehaviour
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
		if (!this.pS_.vomMarktGenommen)
		{
			this.uiObjects[1].GetComponent<Text>().text = this.pS_.GetDateString();
		}
		else
		{
			string text = this.tS_.GetText(1673);
			text = text.Replace("<DATE1>", this.pS_.GetDateString());
			text = text.Replace("<DATE2>", this.pS_.GetDateStringEnd());
			this.uiObjects[1].GetComponent<Text>().color = this.guiMain_.colors[5];
			this.uiObjects[1].GetComponent<Text>().text = text;
		}
		this.pS_.SetPic(this.uiObjects[2]);
		this.uiObjects[3].GetComponent<Image>().sprite = this.pS_.GetTypSprite();
		this.uiObjects[4].GetComponent<Text>().text = this.pS_.tech.ToString();
		this.uiObjects[5].GetComponent<Text>().text = this.mS_.GetMoney((long)this.pS_.performancePoints, false);
		this.tooltip_.c = this.pS_.GetTooltip();
	}

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[339].SetActive(true);
		this.guiMain_.uiObjects[339].GetComponent<Menu_ShowKonsoleDetails>().Init(this.pS_);
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
