using System;
using UnityEngine;


public class sui_demo_animBoat : MonoBehaviour
{
	
	private void Awake()
	{
		GameObject gameObject = new GameObject();
		gameObject.name = "BoatAudioObjectA";
		gameObject.AddComponent<AudioSource>();
		gameObject.transform.position = base.transform.position;
		gameObject.transform.parent = base.transform;
		this.audioObjectA = gameObject.GetComponent<AudioSource>();
		GameObject gameObject2 = new GameObject();
		gameObject2.name = "BoatAudioObjectB";
		gameObject2.AddComponent<AudioSource>();
		gameObject2.transform.position = base.transform.position;
		gameObject2.transform.parent = base.transform;
		this.audioObjectB = gameObject2.GetComponent<AudioSource>();
	}

	
	private void LateUpdate()
	{
		if (this.rudderObject != null)
		{
			if (this.engineRotation == 0f)
			{
				this.engineRot = Mathf.Lerp(this.engineRot, 90f, Time.deltaTime * 2.5f);
			}
			else
			{
				this.engineRot = Mathf.Lerp(this.engineRot, 90f - 60f * this.engineRotation, Time.deltaTime);
			}
			this.rudderObject.transform.localEulerAngles = new Vector3(this.rudderObject.transform.localEulerAngles.x, this.engineRot, this.rudderObject.transform.localEulerAngles.z);
		}
		if (this.propObject != null)
		{
			this.propSpd = 0f;
			if (this.behaviorIsOn)
			{
				this.propSpd = 200f;
				if (this.behaviorIsRevving)
				{
					this.propSpd = 1200f;
				}
				if (this.behaviorIsRevvingHigh)
				{
					this.propSpd = 3000f;
				}
				if (this.behaviorIsRevvingBack)
				{
					this.propSpd = -800f;
				}
			}
			this.propellerSpeed = Mathf.Lerp(this.propellerSpeed, this.propSpd, Time.deltaTime);
			this.propObject.transform.localEulerAngles = new Vector3(this.propObject.transform.localEulerAngles.x, this.propObject.transform.localEulerAngles.y, this.propObject.transform.localEulerAngles.z + Time.deltaTime * this.propellerSpeed);
		}
		if (this.audioObjectA != null && this.audioObjectB != null)
		{
			float num = 1f;
			this.audioObjectA.minDistance = 10f;
			this.audioObjectA.maxDistance = 30f;
			this.audioObjectB.minDistance = 10f;
			this.audioObjectB.maxDistance = 30f;
			if (this.behaviorIsOn)
			{
				this.audioObjectA.loop = true;
				this.audioObjectB.loop = true;
				if (this.isOn)
				{
					this.useClip = this.audioEngineIdle;
					if (this.behaviorIsRevving)
					{
						num = 10f;
						if (this.currentClip == this.audioEngineRevAbove)
						{
							num = 10f;
						}
						if (this.currentClip == this.audioEngineRevHigh)
						{
							num = 10f;
						}
						this.useClip = this.audioEngineRev;
						if (this.behaviorIsRevvingHigh)
						{
							num = 10f;
							this.useClip = this.audioEngineRevHigh;
						}
					}
				}
				if (!this.isOn)
				{
					this.onTime += Time.deltaTime;
					if (this.onTime >= 1f)
					{
						this.isOn = true;
					}
					num = 10f;
					this.useClip = this.audioEngineStart;
				}
			}
			else
			{
				this.audioObjectA.loop = false;
				this.audioObjectB.loop = false;
				if (this.isOn)
				{
					this.onTime -= Time.deltaTime;
					if (this.onTime <= -0.5f)
					{
						this.isOn = false;
					}
					num = 10f;
					this.useClip = this.audioEngineStop;
				}
				else
				{
					this.onTime = 0f;
					this.isOn = false;
					if (this.audioObjectA.isPlaying)
					{
						this.audioObjectA.Stop();
					}
					if (this.audioObjectB.isPlaying)
					{
						this.audioObjectB.Stop();
					}
				}
			}
			if (this.currentClip != this.useClip)
			{
				this.audioObjectA.Stop();
				this.audioObjectA.clip = this.useClip;
				this.audioObjectA.volume = 0f;
				this.audioObjectB.Stop();
				this.audioObjectB.clip = this.currentClip;
				this.audioObjectB.volume = 1f;
				this.currentClip = this.useClip;
			}
			this.audioObjectA.volume = Mathf.Lerp(this.audioObjectA.volume, 1f, Time.deltaTime * num);
			this.audioObjectB.volume = Mathf.Lerp(this.audioObjectB.volume, 0f, Time.deltaTime * num);
			if (this.behaviorIsOn || this.isOn)
			{
				if (!this.audioObjectA.isPlaying)
				{
					this.audioObjectA.Play();
				}
				if (!this.audioObjectB.isPlaying)
				{
					this.audioObjectB.Play();
				}
			}
		}
	}

	
	public GameObject propObject;

	
	public GameObject rudderObject;

	
	public float propellerSpeed;

	
	public float engineRotation;

	
	public Transform playerPosition;

	
	public Transform playerExit;

	
	public AudioClip audioEngineStart;

	
	public AudioClip audioEngineStop;

	
	public AudioClip audioEngineIdle;

	
	public AudioClip audioEngineRev;

	
	public AudioClip audioEngineRevHigh;

	
	public AudioClip audioEngineRevAbove;

	
	public bool behaviorIsOn;

	
	public bool behaviorIsInWater;

	
	public bool behaviorIsRevving;

	
	public bool behaviorIsRevvingBack;

	
	public bool behaviorIsRevvingHigh;

	
	private AudioSource audioObjectA;

	
	private AudioSource audioObjectB;

	
	private AudioClip useClip;

	
	private AudioClip currentClip;

	
	private float engineRot = 90f;

	
	private bool isOn;

	
	private float onTime;

	
	private float propSpd;
}
