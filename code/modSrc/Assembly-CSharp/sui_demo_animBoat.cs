using System;
using UnityEngine;

// Token: 0x0200003A RID: 58
public class sui_demo_animBoat : MonoBehaviour
{
	// Token: 0x060000D2 RID: 210 RVA: 0x0001FE50 File Offset: 0x0001E050
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

	// Token: 0x060000D3 RID: 211 RVA: 0x0001FEF4 File Offset: 0x0001E0F4
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

	// Token: 0x040001FA RID: 506
	public GameObject propObject;

	// Token: 0x040001FB RID: 507
	public GameObject rudderObject;

	// Token: 0x040001FC RID: 508
	public float propellerSpeed;

	// Token: 0x040001FD RID: 509
	public float engineRotation;

	// Token: 0x040001FE RID: 510
	public Transform playerPosition;

	// Token: 0x040001FF RID: 511
	public Transform playerExit;

	// Token: 0x04000200 RID: 512
	public AudioClip audioEngineStart;

	// Token: 0x04000201 RID: 513
	public AudioClip audioEngineStop;

	// Token: 0x04000202 RID: 514
	public AudioClip audioEngineIdle;

	// Token: 0x04000203 RID: 515
	public AudioClip audioEngineRev;

	// Token: 0x04000204 RID: 516
	public AudioClip audioEngineRevHigh;

	// Token: 0x04000205 RID: 517
	public AudioClip audioEngineRevAbove;

	// Token: 0x04000206 RID: 518
	public bool behaviorIsOn;

	// Token: 0x04000207 RID: 519
	public bool behaviorIsInWater;

	// Token: 0x04000208 RID: 520
	public bool behaviorIsRevving;

	// Token: 0x04000209 RID: 521
	public bool behaviorIsRevvingBack;

	// Token: 0x0400020A RID: 522
	public bool behaviorIsRevvingHigh;

	// Token: 0x0400020B RID: 523
	private AudioSource audioObjectA;

	// Token: 0x0400020C RID: 524
	private AudioSource audioObjectB;

	// Token: 0x0400020D RID: 525
	private AudioClip useClip;

	// Token: 0x0400020E RID: 526
	private AudioClip currentClip;

	// Token: 0x0400020F RID: 527
	private float engineRot = 90f;

	// Token: 0x04000210 RID: 528
	private bool isOn;

	// Token: 0x04000211 RID: 529
	private float onTime;

	// Token: 0x04000212 RID: 530
	private float propSpd;
}
