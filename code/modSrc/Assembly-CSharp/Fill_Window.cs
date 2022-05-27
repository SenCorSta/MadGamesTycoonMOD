using System;
using UnityEngine;
using UnityEngine.UI;


public class Fill_Window : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
		this.Init();
	}

	
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
	}

	
	private void Init()
	{
		base.GetComponent<Image>().material = this.guiMain_.matFill_Window;
		UnityEngine.Object.Destroy(this);
	}

	
	private mainScript mS_;

	
	private GameObject main_;

	
	private GUI_Main guiMain_;
}
