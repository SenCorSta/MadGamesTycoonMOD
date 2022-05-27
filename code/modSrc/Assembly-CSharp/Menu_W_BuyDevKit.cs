using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_W_BuyDevKit : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
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
	}

	
	public void Init(platformScript script_)
	{
		if (!script_)
		{
			return;
		}
		this.pS_ = script_;
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
		this.pS_.SetPic(this.uiObjects[1]);
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Yes()
	{
		this.pS_.inBesitz = true;
		this.sfx_.PlaySound(3, true);
		this.BUTTON_Abbrechen();
		this.mS_.Pay((long)this.pS_.GetPrice(), 3);
		if (this.mS_.multiplayer && !this.pS_.OwnerIsNPC())
		{
			if (this.mS_.mpCalls_.isServer)
			{
				this.mS_.mpCalls_.SERVER_Send_Payment(this.mS_.myID, this.pS_.ownerID, 2, this.pS_.price);
			}
			if (this.mS_.mpCalls_.isClient)
			{
				this.mS_.mpCalls_.CLIENT_Send_Payment(this.pS_.ownerID, 2, this.pS_.price);
			}
		}
		this.guiMain_.uiObjects[33].GetComponent<Menu_BuyDevKit>().TAB_DevKitsBuy(0);
	}

	
	public GameObject[] uiObjects;

	
	private platformScript pS_;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;
}
