using System;
using UnityEngine;


public class daylightScript : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
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
	}

	
	private void Update()
	{
		if (this.mS_)
		{
			float y = this.mS_.dayTimer * 90f + (float)(this.mS_.week - 1) * 90f;
			base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, y, base.transform.eulerAngles.z);
		}
	}

	
	private GameObject main_;

	
	private mainScript mS_;
}
