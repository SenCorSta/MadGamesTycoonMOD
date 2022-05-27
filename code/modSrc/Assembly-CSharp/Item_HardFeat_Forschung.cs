using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_HardFeat_Forschung : MonoBehaviour
{
	
	private void Start()
	{
		this.SetData();
	}

	
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.hardwareFeatures_.GetName(this.myID);
		this.uiObjects[1].GetComponent<Image>().sprite = this.hardwareFeatures_.GetSprite(this.myID);
		if ((float)this.hardwareFeatures_.hardFeat_RES_POINTS[this.myID] == this.hardwareFeatures_.hardFeat_RES_POINTS_LEFT[this.myID])
		{
			this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.hardwareFeatures_.GetPrice(this.myID), true);
		}
		else
		{
			this.uiObjects[2].GetComponent<Text>().text = this.tS_.GetText(157);
		}
		if (this.hardwareFeatures_.hardFeat_NEEDINTERNET[this.myID])
		{
			this.uiObjects[6].SetActive(true);
		}
		if (this.hardwareFeatures_.hardFeat_ONLYSTATIONARY[this.myID])
		{
			this.uiObjects[11].GetComponent<Image>().sprite = this.guiMain_.uiSprites[13];
		}
		if (this.hardwareFeatures_.hardFeat_ONLYHANDHELD[this.myID])
		{
			this.uiObjects[10].GetComponent<Image>().sprite = this.guiMain_.uiSprites[13];
		}
		string text = this.tS_.GetText(156);
		text = text.Replace("<NUM>", this.mS_.Round(this.hardwareFeatures_.hardFeat_RES_POINTS_LEFT[this.myID], 2).ToString());
		this.uiObjects[3].GetComponent<Text>().text = text;
		float prozent = this.hardwareFeatures_.GetProzent(this.myID);
		this.uiObjects[4].GetComponent<Image>().fillAmount = prozent * 0.01f;
		this.uiObjects[5].GetComponent<Text>().text = this.mS_.Round(prozent, 1).ToString() + "%";
		this.tooltip_.c = this.hardwareFeatures_.GetTooltip(this.myID);
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
		if (!this.hardwareFeatures_.Pay(this.myID))
		{
			this.guiMain_.ShowNoMoney();
			return;
		}
		taskForschung taskForschung = this.guiMain_.AddTask_Forschung();
		taskForschung.Init(false);
		taskForschung.typ = 6;
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

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public hardwareFeatures hardwareFeatures_;

	
	public GUI_Main guiMain_;

	
	public tooltip tooltip_;

	
	public roomScript rS_;

	
	private float updateTimer;
}
