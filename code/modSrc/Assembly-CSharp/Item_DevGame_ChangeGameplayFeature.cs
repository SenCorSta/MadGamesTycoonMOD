using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_DevGame_ChangeGameplayFeature : MonoBehaviour
{
	
	private void Start()
	{
		if (this.uiObjects[5].activeSelf)
		{
			this.uiObjects[5].SetActive(false);
		}
		this.SetData();
		this.FindScripts();
	}

	
	private void FindScripts()
	{
		if (!this.myButton)
		{
			this.myButton = base.GetComponent<Button>();
		}
		if (!this.menu_)
		{
			this.menu_ = this.guiMain_.uiObjects[348].GetComponent<Menu_Dev_ChangeGameplayFeatures>();
		}
	}

	
	private void Update()
	{
		if (!this.menu_)
		{
			this.menu_ = this.guiMain_.uiObjects[348].GetComponent<Menu_Dev_ChangeGameplayFeatures>();
		}
		if (this.menu_.g_GameGameplayFeatures[this.myID])
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
		}
		else
		{
			base.GetComponent<Image>().color = Color.white;
		}
		this.SetGoodBadIcon();
		this.tooltip_.c = this.gF_.GetTooltip(this.myID, this.menu_.g_GameMainGenre);
	}

	
	private void SetData()
	{
		this.FindScripts();
		this.uiObjects[0].GetComponent<Text>().text = this.gF_.GetName(this.myID);
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)this.gF_.GetDevCosts(this.myID), true);
		this.uiObjects[2].GetComponent<Image>().sprite = this.gF_.GetTypSprite(this.myID);
		this.guiMain_.DrawStars(this.uiObjects[3], this.gF_.gameplayFeatures_LEVEL[this.myID]);
		this.tooltip_.c = this.gF_.GetTooltip(this.myID, this.menu_.g_GameMainGenre);
		this.SetGoodBadIcon();
		this.myButton.interactable = true;
		this.SetPlattformLock();
		if (this.gS_.gameplayFeatures_DevDone[this.myID])
		{
			this.myButton.interactable = false;
			if (!this.uiObjects[5].activeSelf)
			{
				this.uiObjects[5].SetActive(true);
			}
		}
		if (this.gS_.gameGameplayFeatures[this.myID])
		{
			this.myButton.interactable = false;
			if (!this.uiObjects[5].activeSelf)
			{
				this.uiObjects[5].SetActive(true);
			}
		}
	}

	
	public void BUTTON_Click()
	{
		this.FindScripts();
		this.SetPlattformLock();
		if (!this.myButton.interactable)
		{
			return;
		}
		this.sfx_.PlaySound(3, false);
		if (this.menu_.SetGameplayFeature(this.myID))
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
			return;
		}
		base.GetComponent<Image>().color = Color.white;
	}

	
	public void SetGoodBadIcon()
	{
		this.FindScripts();
		if (this.menu_.g_GameMainGenre != -1)
		{
			if (this.gF_.gameplayFeatures_GOOD[this.myID, this.menu_.g_GameMainGenre])
			{
				this.uiObjects[4].GetComponent<Image>().sprite = this.guiMain_.uiSprites[36];
				this.goodBad = 2;
				return;
			}
			if (this.gF_.gameplayFeatures_BAD[this.myID, this.menu_.g_GameMainGenre])
			{
				this.uiObjects[4].GetComponent<Image>().sprite = this.guiMain_.uiSprites[38];
				this.goodBad = 0;
				return;
			}
		}
		this.uiObjects[4].GetComponent<Image>().sprite = this.guiMain_.uiSprites[37];
		this.goodBad = 1;
	}

	
	private void SetPlattformLock()
	{
		if (this.gS_.arcade && this.gF_.gameplayFeatures_LOCKPLATFORM[this.myID, 4])
		{
			this.myButton.interactable = false;
			return;
		}
		if (this.gS_.handy && this.gF_.gameplayFeatures_LOCKPLATFORM[this.myID, 3])
		{
			this.myButton.interactable = false;
			return;
		}
	}

	
	public int myID;

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public gameplayFeatures gF_;

	
	public GUI_Main guiMain_;

	
	public tooltip tooltip_;

	
	public Menu_Dev_ChangeGameplayFeatures menu_;

	
	public int goodBad = 1;

	
	private Button myButton;

	
	public gameScript gS_;
}
