using System;
using UnityEngine;
using UnityEngine.UI;


public class AmountContracts : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
		this.Init();
	}

	
	private void OnEnable()
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
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
	}

	
	private void Init()
	{
		base.gameObject.GetComponent<Text>().text = "[" + this.mS_.GetAmountContracts(this.contractTyp).ToString() + "] ";
	}

	
	public int contractTyp;

	
	private mainScript mS_;

	
	private GameObject main_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private textScript tS_;
}
