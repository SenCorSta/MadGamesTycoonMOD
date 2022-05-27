using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_MultiplayerSave : MonoBehaviour
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
		if (!this.save_)
		{
			this.save_ = this.main_.GetComponent<savegameScript>();
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

	
	public void OnEnable()
	{
		this.FindScripts();
	}

	
	public void Init(int saveID_)
	{
		this.saveID = saveID_;
	}

	
	private void Update()
	{
		if (this.uiObjects[1].GetComponent<Button>().interactable)
		{
			this.timer += Time.deltaTime;
			if (this.timer > 1f)
			{
				this.BUTTON_Yes();
				return;
			}
		}
		else
		{
			this.timer = 0f;
		}
	}

	
	public void BUTTON_Yes()
	{
		this.save_.Save(this.saveID);
		base.gameObject.SetActive(false);
	}

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private savegameScript save_;

	
	private int saveID = -1;

	
	private float timer;
}
