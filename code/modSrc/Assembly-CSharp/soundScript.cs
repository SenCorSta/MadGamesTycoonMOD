using System;
using UnityEngine;


public class soundScript : MonoBehaviour
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
		if (!this.sS_)
		{
			this.sS_ = this.main_.GetComponent<settingsScript>();
		}
		if (!this.soundSource)
		{
			this.soundSource = base.GetComponent<AudioSource>();
			this.orgVolume = this.soundSource.volume;
		}
		this.myRenderer = base.GetComponent<MeshRenderer>();
	}

	
	private void Update()
	{
		if (this.muteOnPausedGame && this.mS_.GetGameSpeed() <= 0f)
		{
			this.soundSource.volume = 0f;
			return;
		}
		if (this.pitchToGamespeed && this.oldGamespeed != this.mS_.GetGameSpeed())
		{
			this.oldGamespeed = this.mS_.GetGameSpeed();
			this.soundSource.pitch = this.mS_.GetGameSpeed();
		}
		this.soundSource.volume = this.orgVolume * this.sS_.masterVolume;
	}

	
	private AudioSource soundSource;

	
	private settingsScript sS_;

	
	private mainScript mS_;

	
	private GameObject main_;

	
	private MeshRenderer myRenderer;

	
	private float orgVolume = 1f;

	
	private float oldGamespeed = 1f;

	
	public bool pitchToGamespeed;

	
	public bool muteOnPausedGame;
}
