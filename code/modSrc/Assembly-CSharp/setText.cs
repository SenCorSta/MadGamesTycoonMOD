using System;
using UnityEngine;
using UnityEngine.UI;


public class setText : MonoBehaviour
{
	
	private void Start()
	{
	}

	
	private void OnEnable()
	{
		this.FindScripts();
		if (this.textArray.Length > 0 && this.textID > -1)
		{
			this.c = this.tS_.GetText(this.textID);
		}
		base.GetComponent<Text>().text = this.c;
	}

	
	private void SetText()
	{
		if (this.textArray.Length > 0 && this.textID > -1)
		{
			this.c = this.tS_.GetText(this.textID);
		}
		base.GetComponent<Text>().text = this.c;
	}

	
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.Find("Main");
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.settings_)
		{
			this.settings_ = this.main_.GetComponent<settingsScript>();
		}
	}

	
	private GameObject main_;

	
	private textScript tS_;

	
	private settingsScript settings_;

	
	public int textID = -1;

	
	public string textArray = "";

	
	public string c = "";
}
