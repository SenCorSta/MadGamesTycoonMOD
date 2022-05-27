using System;
using UnityEngine;


public class newsTimer : MonoBehaviour
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
		if (!this.settings_)
		{
			this.settings_ = this.main_.GetComponent<settingsScript>();
		}
	}

	
	private void Update()
	{
		if (this.mS_.gameSpeed <= 0f)
		{
			return;
		}
		this.aliveTimer += Time.deltaTime;
		if (this.aliveTimer > this.settings_.newsTime)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	
	public GameObject main_;

	
	public mainScript mS_;

	
	public settingsScript settings_;

	
	public float aliveTimer;
}
