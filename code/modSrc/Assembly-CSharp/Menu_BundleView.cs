using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_BundleView : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.Find("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
	}

	
	public void Init(gameScript script_)
	{
		this.FindScripts();
		this.gS_ = script_;
		for (int i = 0; i < this.gS_.bundleID.Length; i++)
		{
			this.SetGame(i, this.gS_.GetBundleGame(i));
		}
		this.uiObjects[0].GetComponent<Text>().text = this.gS_.GetNameWithTag();
		this.uiObjects[27].GetComponent<Text>().text = this.mS_.GetMoney(this.gS_.sellsTotal, false);
		if (this.gS_.GetGesamtGewinn() >= 0L)
		{
			this.uiObjects[28].GetComponent<Text>().text = this.mS_.GetMoney(this.gS_.GetGesamtGewinn(), true);
			return;
		}
		this.uiObjects[28].GetComponent<Text>().text = "<color=red>" + this.mS_.GetMoney(this.gS_.GetGesamtGewinn(), true) + "</color>";
	}

	
	public void SetGame(int slot, gameScript script_)
	{
		if (!script_)
		{
			this.uiObjects[22 + slot].GetComponent<tooltip>().c = "";
			this.uiObjects[2 + slot].GetComponent<Text>().text = "";
			this.uiObjects[7 + slot].GetComponent<Text>().text = "";
			this.uiObjects[12 + slot].GetComponent<Image>().sprite = this.guiMain_.uiSprites[19];
			this.uiObjects[17 + slot].GetComponent<Image>().sprite = this.guiMain_.uiSprites[19];
			this.uiObjects[29 + slot].GetComponent<Text>().text = "";
		}
		else
		{
			this.uiObjects[22 + slot].GetComponent<tooltip>().c = script_.GetTooltip();
			this.uiObjects[2 + slot].GetComponent<Text>().text = "<b>" + script_.GetNameWithTag() + "</b>";
			this.uiObjects[7 + slot].GetComponent<Text>().text = script_.GetReleaseDateString();
			this.uiObjects[12 + slot].GetComponent<Image>().sprite = this.genres_.GetPic(script_.maingenre);
			this.uiObjects[17 + slot].GetComponent<Image>().sprite = this.guiMain_.uiSprites[30];
			this.uiObjects[29 + slot].GetComponent<Text>().text = Mathf.RoundToInt((float)script_.reviewTotal).ToString() + "%";
		}
		this.guiMain_.DrawStarsColor(this.uiObjects[1], Mathf.RoundToInt(this.GetQuality()), Color.white);
	}

	
	public float GetQuality()
	{
		return (float)(this.gS_.reviewTotal / 20);
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public GameObject[] uiObjects;

	
	private roomScript rS_;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private genres genres_;

	
	private games games_;

	
	public gameScript gS_;
}
