using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_DevAddon_GameplayFeature : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
		this.SetData();
	}

	
	private void FindScripts()
	{
		if (!this.menuDevAddon_)
		{
			this.menuDevAddon_ = this.guiMain_.uiObjects[193].GetComponent<Menu_Dev_AddonDo>();
		}
		if (!this.menuDevMMOAddon_)
		{
			this.menuDevMMOAddon_ = this.guiMain_.uiObjects[247].GetComponent<Menu_Dev_MMOAddon>();
		}
	}

	
	private void Update()
	{
		if (this.guiMain_.uiObjects[193].activeSelf)
		{
			if (!this.menuDevAddon_)
			{
				this.menuDevAddon_ = this.guiMain_.uiObjects[193].GetComponent<Menu_Dev_AddonDo>();
			}
			if (this.menuDevAddon_.g_GameGameplayFeatures[this.myID])
			{
				base.GetComponent<Image>().color = this.guiMain_.colors[4];
			}
			else
			{
				base.GetComponent<Image>().color = Color.white;
			}
		}
		if (this.guiMain_.uiObjects[247].activeSelf)
		{
			if (!this.menuDevMMOAddon_)
			{
				this.menuDevMMOAddon_ = this.guiMain_.uiObjects[247].GetComponent<Menu_Dev_MMOAddon>();
			}
			if (this.menuDevMMOAddon_.g_GameGameplayFeatures[this.myID])
			{
				base.GetComponent<Image>().color = this.guiMain_.colors[4];
			}
			else
			{
				base.GetComponent<Image>().color = Color.white;
			}
		}
		if (this.guiMain_.uiObjects[193].activeSelf)
		{
			this.tooltip_.c = this.gF_.GetTooltip(this.myID, this.menuDevAddon_.gS_.maingenre);
		}
		if (this.guiMain_.uiObjects[247].activeSelf)
		{
			this.tooltip_.c = this.gF_.GetTooltip(this.myID, this.menuDevMMOAddon_.gS_.maingenre);
		}
		this.SetGoodBadIcon();
	}

	
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.gF_.GetName(this.myID);
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)this.gF_.GetDevCosts(this.myID), true);
		this.uiObjects[2].GetComponent<Image>().sprite = this.gF_.GetTypSprite(this.myID);
		this.guiMain_.DrawStars(this.uiObjects[3], this.gF_.gameplayFeatures_LEVEL[this.myID]);
		if (this.guiMain_.uiObjects[193].activeSelf)
		{
			this.tooltip_.c = this.gF_.GetTooltip(this.myID, this.menuDevAddon_.gS_.maingenre);
		}
		if (this.guiMain_.uiObjects[247].activeSelf)
		{
			this.tooltip_.c = this.gF_.GetTooltip(this.myID, this.menuDevMMOAddon_.gS_.maingenre);
		}
		this.SetGoodBadIcon();
	}

	
	public void BUTTON_Click()
	{
		if (!base.GetComponent<Button>().interactable)
		{
			return;
		}
		this.sfx_.PlaySound(3, false);
		bool flag = false;
		if (this.guiMain_.uiObjects[193].activeSelf)
		{
			flag = this.guiMain_.uiObjects[193].GetComponent<Menu_Dev_AddonDo>().SetGameplayFeature(this.myID);
		}
		if (this.guiMain_.uiObjects[247].activeSelf)
		{
			flag = this.guiMain_.uiObjects[247].GetComponent<Menu_Dev_MMOAddon>().SetGameplayFeature(this.myID);
		}
		if (flag)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
			return;
		}
		base.GetComponent<Image>().color = Color.white;
	}

	
	private void SetGoodBadIcon()
	{
		this.FindScripts();
		if (this.guiMain_.uiObjects[193].activeSelf)
		{
			if (this.menuDevAddon_.gS_.maingenre != -1)
			{
				if (this.gF_.gameplayFeatures_GOOD[this.myID, this.menuDevAddon_.gS_.maingenre])
				{
					this.uiObjects[4].GetComponent<Image>().sprite = this.guiMain_.uiSprites[36];
					this.goodBad = 2;
					return;
				}
				if (this.gF_.gameplayFeatures_BAD[this.myID, this.menuDevAddon_.gS_.maingenre])
				{
					this.uiObjects[4].GetComponent<Image>().sprite = this.guiMain_.uiSprites[38];
					this.goodBad = 0;
					return;
				}
			}
			this.uiObjects[4].GetComponent<Image>().sprite = this.guiMain_.uiSprites[37];
			this.goodBad = 1;
			return;
		}
		if (this.guiMain_.uiObjects[247].activeSelf)
		{
			if (this.menuDevMMOAddon_.gS_.maingenre != -1)
			{
				if (this.gF_.gameplayFeatures_GOOD[this.myID, this.menuDevMMOAddon_.gS_.maingenre])
				{
					this.uiObjects[4].GetComponent<Image>().sprite = this.guiMain_.uiSprites[36];
					this.goodBad = 2;
					return;
				}
				if (this.gF_.gameplayFeatures_BAD[this.myID, this.menuDevMMOAddon_.gS_.maingenre])
				{
					this.uiObjects[4].GetComponent<Image>().sprite = this.guiMain_.uiSprites[38];
					this.goodBad = 0;
					return;
				}
			}
			this.uiObjects[4].GetComponent<Image>().sprite = this.guiMain_.uiSprites[37];
			this.goodBad = 1;
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

	
	public int goodBad = 1;

	
	private Menu_Dev_AddonDo menuDevAddon_;

	
	private Menu_Dev_MMOAddon menuDevMMOAddon_;
}
