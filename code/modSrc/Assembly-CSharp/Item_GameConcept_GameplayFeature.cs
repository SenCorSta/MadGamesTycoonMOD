using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_GameConcept_GameplayFeature : MonoBehaviour
{
	
	private void Start()
	{
		this.SetData();
	}

	
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.gF_.GetName(this.myID);
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)this.gF_.GetDevCosts(this.myID), true);
		this.uiObjects[2].GetComponent<Image>().sprite = this.gF_.GetTypSprite(this.myID);
		this.guiMain_.DrawStars(this.uiObjects[3], this.gF_.gameplayFeatures_LEVEL[this.myID]);
		this.tooltip_.c = this.gF_.GetTooltip(this.myID, this.gS_.maingenre);
		this.SetGoodBadIcon();
	}

	
	private void SetGoodBadIcon()
	{
		if (this.gS_.maingenre != -1)
		{
			if (this.gF_.gameplayFeatures_GOOD[this.myID, this.gS_.maingenre])
			{
				this.uiObjects[4].GetComponent<Image>().sprite = this.guiMain_.uiSprites[36];
				return;
			}
			if (this.gF_.gameplayFeatures_BAD[this.myID, this.gS_.maingenre])
			{
				this.uiObjects[4].GetComponent<Image>().sprite = this.guiMain_.uiSprites[38];
				return;
			}
		}
		this.uiObjects[4].GetComponent<Image>().sprite = this.guiMain_.uiSprites[37];
	}

	
	public int myID;

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public gameplayFeatures gF_;

	
	public GUI_Main guiMain_;

	
	public tooltip tooltip_;

	
	public gameScript gS_;
}
