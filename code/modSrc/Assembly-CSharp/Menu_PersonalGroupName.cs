using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_PersonalGroupName : MonoBehaviour
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
		if (!this.main_)
		{
			return;
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

	
	private void OnEnable()
	{
		this.FindScripts();
		this.cmS_.disableMovement = true;
	}

	
	private void OnDisable()
	{
		this.FindScripts();
		if (!this.cmS_)
		{
			return;
		}
		this.cmS_.disableMovement = false;
	}

	
	public void Init(int group_)
	{
		this.FindScripts();
		this.group = group_;
		if (this.mS_.personal_group_names[this.group].Length > 0)
		{
			this.uiObjects[0].GetComponent<InputField>().text = this.mS_.personal_group_names[this.group];
			return;
		}
		this.uiObjects[0].GetComponent<InputField>().text = "";
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Yes()
	{
		this.mS_.personal_group_names[this.group] = this.uiObjects[0].GetComponent<InputField>().text;
		this.guiMain_.uiObjects[32].GetComponent<Menu_PersonalGroups>().InitDropdowns();
		this.guiMain_.uiObjects[32].GetComponent<Menu_PersonalGroups>().DROPDOWN_Group();
		this.BUTTON_Abbrechen();
	}

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private cameraMovementScript cmS_;

	
	private int group = -1;
}
