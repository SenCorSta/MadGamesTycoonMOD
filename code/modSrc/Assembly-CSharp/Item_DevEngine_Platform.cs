using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_DevEngine_Platform : MonoBehaviour
{
	
	private void Start()
	{
		this.SetData();
	}

	
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
		this.pS_.SetPic(this.uiObjects[1]);
		string text = this.pS_.GetDateString();
		text = string.Concat(new string[]
		{
			text,
			"\n",
			this.tS_.GetText(219),
			": ",
			this.pS_.GetMarktanteilString()
		});
		this.uiObjects[2].GetComponent<Text>().text = text;
		this.uiObjects[6].GetComponent<Text>().text = this.pS_.tech.ToString();
		this.guiMain_.DrawStars(this.uiObjects[7], this.pS_.erfahrung);
		this.uiObjects[9].GetComponent<Image>().sprite = this.pS_.GetComplexSprite();
		if (this.pS_.internet)
		{
			this.uiObjects[4].SetActive(true);
		}
		else
		{
			this.uiObjects[4].SetActive(false);
		}
		text = "";
		if (this.pS_.needFeatures[0] != -1)
		{
			text = this.gF_.GetName(this.pS_.needFeatures[0]);
		}
		if (this.pS_.needFeatures[1] != -1)
		{
			text = text + "\n" + this.gF_.GetName(this.pS_.needFeatures[1]);
		}
		if (this.pS_.needFeatures[2] != -1)
		{
			text = text + "\n" + this.gF_.GetName(this.pS_.needFeatures[2]);
		}
		this.uiObjects[5].GetComponent<Text>().text = text;
		this.tooltip_.c = this.pS_.GetTooltip();
		this.uiObjects[10].GetComponent<Image>().sprite = this.pS_.GetTypSprite();
		this.uiObjects[10].GetComponent<tooltip>().c = this.pS_.GetTypString();
		if (this.pS_.vomMarktGenommen)
		{
			this.uiObjects[3].SetActive(true);
		}
		else
		{
			this.uiObjects[8].SetActive(false);
		}
		if (this.pS_.tech < this.menuDevEngine_.techLevel)
		{
			this.uiObjects[3].SetActive(true);
			tooltip tooltip = this.tooltip_;
			tooltip.c = tooltip.c + "\n\n<color=red><b>" + this.tS_.GetText(378) + "</b></color>";
			base.gameObject.GetComponent<Button>().interactable = false;
			return;
		}
	}

	
	private void Update()
	{
		this.updateTimer += Time.deltaTime;
		if (this.updateTimer < 1f)
		{
			return;
		}
		this.updateTimer = 0f;
		this.SetData();
	}

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, false);
		this.menuDevEngine_.SetSpezialplatform(this.myID);
		this.guiMain_.uiObjects[237].GetComponent<Menu_Dev_EnginePlatform>().BUTTON_Close();
	}

	
	public int myID;

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public GUI_Main guiMain_;

	
	public tooltip tooltip_;

	
	public platformScript pS_;

	
	public gameplayFeatures gF_;

	
	public Menu_Dev_Engine menuDevEngine_;

	
	private float updateTimer;
}
