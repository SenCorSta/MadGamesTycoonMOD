using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Dev_Beschreibung : MonoBehaviour
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
		if (!this.mDevGame_)
		{
			this.mDevGame_ = this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>();
		}
		if (!this.mDevAddon_)
		{
			this.mDevAddon_ = this.guiMain_.uiObjects[193].GetComponent<Menu_Dev_AddonDo>();
		}
		if (!this.mDevMMOAddon_)
		{
			this.mDevMMOAddon_ = this.guiMain_.uiObjects[247].GetComponent<Menu_Dev_MMOAddon>();
		}
		if (!this.mDevEntwicklungsbericht_)
		{
			this.mDevEntwicklungsbericht_ = this.guiMain_.uiObjects[73].GetComponent<Menu_Dev_GameEntwicklungsbericht>();
		}
	}

	
	private void OnEnable()
	{
		this.Init();
	}

	
	private void Init()
	{
		this.FindScripts();
		if (this.mDevGame_.gameObject.activeSelf)
		{
			this.uiObjects[0].GetComponent<InputField>().text = this.mDevGame_.g_Beschreibung;
		}
		if (this.mDevAddon_.gameObject.activeSelf)
		{
			this.uiObjects[0].GetComponent<InputField>().text = this.mDevAddon_.g_Beschreibung;
		}
		if (this.mDevMMOAddon_.gameObject.activeSelf)
		{
			this.uiObjects[0].GetComponent<InputField>().text = this.mDevMMOAddon_.g_Beschreibung;
		}
		if (this.mDevEntwicklungsbericht_.gameObject.activeSelf)
		{
			this.uiObjects[0].GetComponent<InputField>().text = this.mDevEntwicklungsbericht_.GetBeschreibung();
		}
	}

	
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_OK()
	{
		this.sfx_.PlaySound(3, true);
		if (this.mDevGame_.gameObject.activeSelf)
		{
			this.mDevGame_.SetBeschreibung(this.uiObjects[0].GetComponent<InputField>().text);
		}
		if (this.mDevAddon_.gameObject.activeSelf)
		{
			this.mDevAddon_.SetBeschreibung(this.uiObjects[0].GetComponent<InputField>().text);
		}
		if (this.mDevMMOAddon_.gameObject.activeSelf)
		{
			this.mDevMMOAddon_.SetBeschreibung(this.uiObjects[0].GetComponent<InputField>().text);
		}
		if (this.mDevEntwicklungsbericht_.gameObject.activeSelf)
		{
			this.mDevEntwicklungsbericht_.SetBeschreibung(this.uiObjects[0].GetComponent<InputField>().text);
		}
		base.gameObject.SetActive(false);
	}

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private Menu_DevGame mDevGame_;

	
	private Menu_Dev_AddonDo mDevAddon_;

	
	private Menu_Dev_MMOAddon mDevMMOAddon_;

	
	private Menu_Dev_GameEntwicklungsbericht mDevEntwicklungsbericht_;
}
