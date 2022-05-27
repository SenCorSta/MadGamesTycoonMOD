using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_Training_Kurs : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
		this.SetData();
	}

	
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindGameObjectWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.menuTraining_)
		{
			this.menuTraining_ = this.guiMain_.uiObjects[92].GetComponent<Menu_Training_Select>();
		}
	}

	
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(538 + this.myID);
		this.uiObjects[1].GetComponent<Image>().sprite = this.menuTraining_.trainingSprites[this.myID];
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.menuTraining_.trainingCosts[this.myID], true);
		string text = this.tS_.GetText(562);
		text = text.Replace("<NUM>", this.mS_.Round(this.menuTraining_.trainingMaxLearn[this.myID], 2).ToString());
		this.uiObjects[3].GetComponent<Text>().text = text;
		this.uiObjects[4].GetComponent<Text>().text = this.tS_.GetText(563 + this.menuTraining_.trainingEffekt[this.myID]);
	}

	
	public void BUTTON_Click()
	{
		if (!this.menuTraining_.rS_)
		{
			return;
		}
		this.mS_.Pay((long)this.menuTraining_.trainingCosts[this.myID], 13);
		taskTraining taskTraining = this.guiMain_.AddTask_Training();
		taskTraining.Init(false);
		taskTraining.slot = this.myID;
		taskTraining.points = this.menuTraining_.workPoints[this.myID];
		taskTraining.pointsLeft = this.menuTraining_.workPoints[this.myID];
		taskTraining.automatic = this.menuTraining_.uiObjects[5].GetComponent<Toggle>().isOn;
		GameObject gameObject = GameObject.Find("Room_" + this.menuTraining_.rS_.myID.ToString());
		if (gameObject)
		{
			gameObject.GetComponent<roomScript>().taskID = taskTraining.myID;
		}
		this.sfx_.PlaySound(3, true);
		this.menuTraining_.BUTTON_Close();
	}

	
	public int myID;

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private sfxScript sfx_;

	
	private GUI_Main guiMain_;

	
	private Menu_Training_Select menuTraining_;
}
