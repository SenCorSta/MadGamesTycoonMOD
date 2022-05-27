using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_DevKonsole_HardwareFeature : MonoBehaviour
{
	
	private void Start()
	{
		this.SetData();
	}

	
	private void Update()
	{
		if (this.menu_.hwFeatures[this.myID])
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
			return;
		}
		base.GetComponent<Image>().color = Color.white;
	}

	
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.hardwareFeatures_.GetName(this.myID);
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)this.hardwareFeatures_.GetDevCosts(this.myID), true);
		if (this.hardwareFeatures_.hardFeat_NEEDINTERNET[this.myID])
		{
			this.uiObjects[2].SetActive(true);
			if (!this.menu_.uiObjects[53].GetComponent<Toggle>().isOn)
			{
				base.GetComponent<Button>().interactable = false;
				this.menu_.hwFeatures[this.myID] = false;
			}
		}
		if (this.hardwareFeatures_.hardFeat_ONLYSTATIONARY[this.myID])
		{
			this.uiObjects[4].GetComponent<Image>().sprite = this.guiMain_.uiSprites[13];
			if (this.menu_.platformTyp == 2)
			{
				base.GetComponent<Button>().interactable = false;
				this.menu_.hwFeatures[this.myID] = false;
			}
		}
		if (this.hardwareFeatures_.hardFeat_ONLYHANDHELD[this.myID])
		{
			this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.uiSprites[13];
			if (this.menu_.platformTyp == 1)
			{
				base.GetComponent<Button>().interactable = false;
				this.menu_.hwFeatures[this.myID] = false;
			}
		}
		this.tooltip_.c = this.hardwareFeatures_.GetTooltip(this.myID);
	}

	
	public void BUTTON_Click()
	{
		if (base.GetComponent<Button>().interactable)
		{
			this.sfx_.PlaySound(3, true);
			this.menu_.hwFeatures[this.myID] = !this.menu_.hwFeatures[this.myID];
			this.menu_.UpdateGUI();
		}
	}

	
	public int myID;

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public hardwareFeatures hardwareFeatures_;

	
	public GUI_Main guiMain_;

	
	public tooltip tooltip_;

	
	public Menu_Dev_Konsole menu_;
}
