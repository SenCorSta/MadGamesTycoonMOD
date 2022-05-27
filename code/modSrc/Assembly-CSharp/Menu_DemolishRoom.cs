using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_DemolishRoom : MonoBehaviour
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
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.cmS_)
		{
			this.cmS_ = GameObject.Find("CamMovement").GetComponent<cameraMovementScript>();
		}
	}

	
	public void Init(roomScript script_)
	{
		this.FindScripts();
		this.cmS_.disableMovement = true;
		this.money = 0;
		if (script_)
		{
			this.rS_ = script_;
			if (this.rS_.taskID == -1)
			{
				for (int i = 0; i < this.rS_.listInventar.Count; i++)
				{
					if (this.rS_.listInventar[i])
					{
						this.money += this.rS_.listInventar[i].GetComponent<objectScript>().GetVerkaufspreis();
					}
				}
				string text = this.tS_.GetText(153);
				text = text.Replace("<NUM>", this.mS_.GetMoney((long)this.money, true));
				this.uiObjects[0].GetComponent<Text>().text = text;
				return;
			}
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(638);
		}
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
		this.cmS_.disableMovement = false;
	}

	
	public void BUTTON_Yes()
	{
		if (this.rS_ && this.rS_.taskID == -1)
		{
			this.mS_.Earn((long)this.money, 0);
			this.rS_.Demolish();
			this.sfx_.PlaySound(25, true);
			this.guiMain_.ShowWalls(this.guiMain_.uiObjects[241].GetComponent<Toggle>().isOn);
		}
		this.BUTTON_Abbrechen();
	}

	
	public GameObject[] uiObjects;

	
	private roomScript rS_;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private cameraMovementScript cmS_;

	
	public int money;
}
