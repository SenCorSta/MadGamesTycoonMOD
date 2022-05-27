using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_DevEngine_Feature : MonoBehaviour
{
	
	private void Start()
	{
		this.SetData();
	}

	
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.eF_.GetName(this.myID);
		this.uiObjects[1].GetComponent<Image>().sprite = this.eF_.engineFeatures_PICTYP[this.eF_.engineFeatures_TYP[this.myID]];
		this.uiObjects[3].GetComponent<Text>().text = this.eF_.engineFeatures_TECH[this.myID].ToString();
		this.guiMain_.DrawStars(this.uiObjects[4], this.eF_.engineFeatures_LEVEL[this.myID]);
		this.tooltip_.c = this.eF_.GetTooltip(this.myID);
		this.SetButtonColor();
		if (!this.guiMain_.uiObjects[37].GetComponent<Menu_Dev_Engine>().featuresLock[this.myID])
		{
			this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.eF_.GetDevCostsForEngine(this.myID), true);
			return;
		}
		this.uiObjects[2].GetComponent<Text>().text = "$0";
	}

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public void BUTTON_Click()
	{
		Menu_Dev_Engine component = this.guiMain_.uiObjects[37].GetComponent<Menu_Dev_Engine>();
		if (!component.featuresLock[this.myID])
		{
			this.activ = !this.activ;
			this.sfx_.PlaySound(3, true);
			component.SetFeature(this.myID, this.activ);
			this.SetButtonColor();
		}
	}

	
	private void SetButtonColor()
	{
		if (this.guiMain_.uiObjects[37].GetComponent<Menu_Dev_Engine>().featuresLock[this.myID])
		{
			base.GetComponent<Button>().interactable = false;
			return;
		}
		if (this.activ)
		{
			this.uiObjects[5].GetComponent<Image>().color = this.guiMain_.colors[4];
			return;
		}
		this.uiObjects[5].GetComponent<Image>().color = Color.white;
	}

	
	public int myID;

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public engineFeatures eF_;

	
	public GUI_Main guiMain_;

	
	public tooltip tooltip_;

	
	public bool activ;
}
