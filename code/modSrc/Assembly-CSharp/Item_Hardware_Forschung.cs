using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_Hardware_Forschung : MonoBehaviour
{
	
	private void Start()
	{
		this.SetData();
	}

	
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.hardware_.GetName(this.myID);
		this.uiObjects[1].GetComponent<Image>().sprite = this.hardware_.GetTypPic(this.myID);
		this.uiObjects[7].GetComponent<Text>().text = this.hardware_.hardware_TECH[this.myID].ToString();
		if ((float)this.hardware_.hardware_RES_POINTS[this.myID] == this.hardware_.hardware_RES_POINTS_LEFT[this.myID])
		{
			this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.hardware_.GetPrice(this.myID), true);
		}
		else
		{
			this.uiObjects[2].GetComponent<Text>().text = this.tS_.GetText(157);
		}
		if (!this.hardware_.IsTechComponent(this.myID))
		{
			this.uiObjects[6].SetActive(false);
			this.uiObjects[12].SetActive(true);
		}
		if (this.hardware_.hardware_ONLYSTATIONARY[this.myID])
		{
			this.uiObjects[11].GetComponent<Image>().sprite = this.guiMain_.uiSprites[13];
		}
		if (this.hardware_.hardware_ONLYHANDHELD[this.myID])
		{
			this.uiObjects[10].GetComponent<Image>().sprite = this.guiMain_.uiSprites[13];
		}
		string text = this.tS_.GetText(156);
		text = text.Replace("<NUM>", this.mS_.Round(this.hardware_.hardware_RES_POINTS_LEFT[this.myID], 2).ToString());
		this.uiObjects[3].GetComponent<Text>().text = text;
		float prozent = this.hardware_.GetProzent(this.myID);
		this.uiObjects[4].GetComponent<Image>().fillAmount = prozent * 0.01f;
		this.uiObjects[5].GetComponent<Text>().text = this.mS_.Round(prozent, 1).ToString() + "%";
		this.tooltip_.c = this.hardware_.GetTooltip(this.myID);
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
		Menu_Forschung component = this.guiMain_.uiObjects[21].GetComponent<Menu_Forschung>();
		if (!this.hardware_.Pay(this.myID))
		{
			this.guiMain_.ShowNoMoney();
			return;
		}
		taskForschung taskForschung = this.guiMain_.AddTask_Forschung();
		taskForschung.Init(false);
		taskForschung.typ = 4;
		taskForschung.slot = this.myID;
		taskForschung.automatic = component.uiObjects[4].GetComponent<Toggle>().isOn;
		GameObject gameObject = GameObject.Find("Room_" + component.roomID.ToString());
		if (gameObject)
		{
			gameObject.GetComponent<roomScript>().taskID = taskForschung.myID;
		}
		this.sfx_.PlaySound(3, true);
		component.BUTTON_Close();
	}

	
	public int myID;

	
	public GameObject[] uiObjects;

	
	public Color[] colors;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public hardware hardware_;

	
	public GUI_Main guiMain_;

	
	public tooltip tooltip_;

	
	public roomScript rS_;

	
	private float updateTimer;
}
