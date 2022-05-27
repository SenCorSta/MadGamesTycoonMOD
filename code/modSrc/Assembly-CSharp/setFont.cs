using System;
using UnityEngine;
using UnityEngine.UI;


public class setFont : MonoBehaviour
{
	
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.Find("Main");
		}
		if (!this.settings_)
		{
			this.settings_ = this.main_.GetComponent<settingsScript>();
		}
	}

	
	private void OnEnable()
	{
		this.FindScripts();
		if (this.settings_.language == 3 || this.settings_.language == 10)
		{
			base.GetComponent<Text>().fontStyle = FontStyle.Normal;
		}
	}

	
	private GameObject main_;

	
	private settingsScript settings_;
}
