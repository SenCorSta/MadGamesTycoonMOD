using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_W_Dev_KonsoleVerwerfen : MonoBehaviour
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

	
	public void Init(platformScript script_, taskKonsole task_)
	{
		if (!script_)
		{
			return;
		}
		if (!task_)
		{
			return;
		}
		this.pS_ = script_;
		this.taskKonsole_ = task_;
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Yes()
	{
		this.sfx_.PlaySound(3, true);
		GameObject gameObject = GameObject.Find("PLATFORM_" + this.taskKonsole_.konsoleID.ToString());
		if (gameObject)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
		UnityEngine.Object.Destroy(this.taskKonsole_.gameObject);
		base.gameObject.SetActive(false);
		this.guiMain_.uiObjects[326].SetActive(false);
		this.guiMain_.CloseMenu();
	}

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private platformScript pS_;

	
	private taskKonsole taskKonsole_;
}
