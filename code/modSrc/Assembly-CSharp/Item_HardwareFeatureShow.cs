using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_HardwareFeatureShow : MonoBehaviour
{
	
	private void Start()
	{
		this.SetData();
	}

	
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.hardwareFeatures_.GetName(this.myID);
		this.tooltip_.c = this.hardwareFeatures_.GetTooltip(this.myID);
	}

	
	public int myID;

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public hardwareFeatures hardwareFeatures_;

	
	public GUI_Main guiMain_;

	
	public tooltip tooltip_;
}
