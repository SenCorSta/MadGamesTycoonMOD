using System;
using UnityEngine;


public class maschineAnimSound : MonoBehaviour
{
	
	private void Start()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindGameObjectWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
	}

	
	private void Update()
	{
		if (!this.myAnimation.isPlaying)
		{
			this.mySound.Stop();
			this.mySound.Play();
			return;
		}
		if (this.oldGamespeed != this.mS_.GetGameSpeed())
		{
			this.oldGamespeed = this.mS_.GetGameSpeed();
			this.mySound.pitch = this.mS_.GetGameSpeed();
		}
	}

	
	public AudioSource mySound;

	
	public Animation myAnimation;

	
	private mainScript mS_;

	
	private GameObject main_;

	
	private float oldGamespeed;
}
