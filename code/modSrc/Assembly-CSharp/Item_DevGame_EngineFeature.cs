using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_DevGame_EngineFeature : MonoBehaviour
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
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.eF_.GetDevCosts(this.myID), true);
		this.guiMain_.DrawStars(this.uiObjects[4], this.eF_.engineFeatures_LEVEL[this.myID]);
		this.tooltip_.c = this.eF_.GetTooltip(this.myID);
		if (this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().g_GameEngineFeature[this.eF_.engineFeatures_TYP[this.myID]] == this.myID)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[7];
		}
	}

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().SetEngineFeature(this.eF_.engineFeatures_TYP[this.myID], this.myID);
		this.guiMain_.uiObjects[67].GetComponent<Menu_DevGame_EngineFeature>().BUTTON_Close();
	}

	
	public int myID;

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public engineFeatures eF_;

	
	public GUI_Main guiMain_;

	
	public tooltip tooltip_;
}
