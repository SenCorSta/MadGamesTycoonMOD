using System;
using UnityEngine;


public class animationSpeed : MonoBehaviour
{
	
	private void Start()
	{
		this.myAnimation = base.GetComponent<Animation>();
		if (!this.main_)
		{
			this.main_ = GameObject.FindGameObjectWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		this.SetAnimationSpeed();
	}

	
	private void Update()
	{
		if (this.oldGamespeed != this.mS_.GetGameSpeed())
		{
			this.oldGamespeed = this.mS_.GetGameSpeed();
			this.SetAnimationSpeed();
		}
	}

	
	private void SetAnimationSpeed()
	{
		this.myAnimation[this.animationString].speed = this.mS_.GetGameSpeed();
	}

	
	public string animationString;

	
	private Animation myAnimation;

	
	private mainScript mS_;

	
	private GameObject main_;

	
	private float oldGamespeed;
}
