using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_ArchivEngine : MonoBehaviour
{
	
	private void Start()
	{
		this.SetData();
	}

	
	public void SetData()
	{
		if (!this.eS_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.eS_.GetName();
		this.uiObjects[2].GetComponent<Text>().text = this.eS_.GetTechLevel().ToString();
		this.tooltip_.c = this.eS_.GetTooltip();
	}

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		if (this.eS_)
		{
			this.eS_.archiv_engine = !this.eS_.archiv_engine;
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public GUI_Main guiMain_;

	
	public tooltip tooltip_;

	
	public engineScript eS_;
}
