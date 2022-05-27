using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_W_GameVerwerfen : MonoBehaviour
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

	
	public void Init(gameScript script_, taskGame task_)
	{
		if (!script_)
		{
			return;
		}
		this.gS_ = script_;
		this.taskGame_ = task_;
		this.uiObjects[0].GetComponent<Text>().text = this.gS_.GetNameWithTag();
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Yes()
	{
		this.sfx_.PlaySound(3, true);
		if (this.taskGame_)
		{
			this.taskGame_.Abbrechen();
		}
		else
		{
			UnityEngine.Object.Destroy(this.gS_.gameObject);
		}
		base.gameObject.SetActive(false);
		if (this.guiMain_.uiObjects[69].activeSelf)
		{
			this.guiMain_.uiObjects[69].SetActive(false);
		}
		if (this.guiMain_.uiObjects[397].activeSelf)
		{
			this.guiMain_.uiObjects[397].SetActive(false);
		}
		this.guiMain_.CloseMenu();
	}

	
	public GameObject[] uiObjects;

	
	private platformScript pS_;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private gameScript gS_;

	
	private taskGame taskGame_;
}
