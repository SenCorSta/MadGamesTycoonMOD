using System;
using UnityEngine;


public class muellScript : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
		this.mS_.findMuell = true;
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

	
	private void OnDestroy()
	{
		if (this.mS_)
		{
			this.mS_.findMuell = true;
		}
	}

	
	public int myGFXSlot = -1;

	
	public mainScript mS_;

	
	public GameObject main_;
}
