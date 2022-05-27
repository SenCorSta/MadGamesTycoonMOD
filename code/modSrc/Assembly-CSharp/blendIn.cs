using System;
using UnityEngine;
using UnityEngine.UI;


public class blendIn : MonoBehaviour
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
	}

	
	private void OnEnable()
	{
		this.uiObjects[0].GetComponent<Image>().fillAmount = 1f;
	}

	
	private void Update()
	{
		if (this.uiObjects[0].GetComponent<Image>().fillAmount > 0f)
		{
			this.uiObjects[0].GetComponent<Image>().fillAmount -= Time.deltaTime * 2f;
			if (this.uiObjects[0].GetComponent<Image>().fillAmount <= 0f)
			{
				this.uiObjects[0].GetComponent<Image>().fillAmount = 1f;
				base.gameObject.SetActive(false);
			}
		}
	}

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;
}
