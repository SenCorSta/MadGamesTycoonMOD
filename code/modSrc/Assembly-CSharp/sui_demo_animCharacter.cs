using System;
using UnityEngine;

// Token: 0x0200003B RID: 59
public class sui_demo_animCharacter : MonoBehaviour
{
	// Token: 0x060000D5 RID: 213 RVA: 0x00009E95 File Offset: 0x00008095
	private void Start()
	{
		this.physRigidbody = base.GetComponent<Rigidbody>();
		this.physAnimation = base.GetComponent<Animation>();
		this.useClip = "anim_miho_idle_normal";
		this.defaultClip = this.useClip;
		this.SetBoneTransforms();
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x00009ECC File Offset: 0x000080CC
	private void LateUpdate()
	{
		if (!this.isInWater)
		{
			this.wetAmount -= Time.deltaTime * 0.05f;
			this.wetAmount = Mathf.Clamp(this.wetAmount, 0f, 1f);
		}
		else
		{
			this.wetAmount = 1f;
		}
		this.useClip = this.defaultClip;
		this.playSpeed = 1f;
		if (!this.isInBoat)
		{
			this.useClip = "anim_miho_idle_normal";
			this.fadeSpeed = 1.2f;
			this.playSpeed = 1f;
			if (this.isWalking)
			{
				this.useClip = "anim_miho_walk_normal";
				this.fadeSpeed = 0.5f;
				this.playSpeed = 1.1f;
				if (this.moveForward != 0f && this.moveSideways != 0f)
				{
					this.fadeSpeed = 0.5f;
					this.playSpeed = 1.1f;
				}
			}
			if (this.isRunning)
			{
				this.useClip = "anim_miho_run_normal";
				this.fadeSpeed = 0.8f;
				this.playSpeed = 0.9f;
				if (this.moveForward != 0f && this.moveSideways != 0f)
				{
					this.fadeSpeed = 0.8f;
					this.playSpeed = 0.9f;
				}
			}
			if (this.isSprinting)
			{
				this.useClip = "anim_miho_sprint_normal";
				this.fadeSpeed = 1.3f;
				this.playSpeed = 1.1f;
				if (this.moveForward != 0f && this.moveSideways != 0f)
				{
					this.fadeSpeed = 0.3f;
					this.playSpeed = 1.1f;
				}
			}
			if (this.isInWater)
			{
				this.wetAmount = 1f;
			}
			if (this.isInWaterDeep)
			{
				this.wetAmount = 1f;
				if (this.isWalking)
				{
					this.useClip = "anim_miho_walk_water";
					this.fadeSpeed = 0.8f;
					this.playSpeed = 0.8f;
				}
			}
			if (this.isUnderWater)
			{
				this.wetAmount = 1f;
				this.useClip = "anim_miho_swim_idle";
				this.fadeSpeed = 1.2f;
				this.playSpeed = 1f;
				if (this.isWalking || this.isRunning)
				{
					this.useClip = "anim_miho_swim_forward";
					this.fadeSpeed = 1.8f;
					this.playSpeed = 1f;
					if (this.isRunning)
					{
						this.playSpeed = 1.4f;
					}
				}
				if (this.physRigidbody != null)
				{
					this.physRigidbody.useGravity = false;
				}
				if (this.physRigidbody != null)
				{
					this.physRigidbody.Sleep();
				}
			}
			if (this.isAtSurface)
			{
				this.useClip = "anim_miho_swim_surface_idle";
				this.fadeSpeed = 0.8f;
				this.playSpeed = 1f;
				if (this.physRigidbody != null)
				{
					this.physRigidbody.useGravity = true;
				}
			}
		}
		else if (this.isInBoat)
		{
			this.useClip = "anim_miho_boat_sit_idle";
			this.fadeSpeed = 0.4f;
			this.playSpeed = 1f;
		}
		this.animTime += Time.deltaTime;
		if (this.physAnimation[this.useClip] != null && this.physAnimation[this.currClip] != null)
		{
			this.physAnimation[this.useClip].time = this.physAnimation[this.currClip].time;
		}
		this.currClip = this.useClip;
		this.animTime = 0f;
		if (this.physAnimation[this.currClip] != null)
		{
			this.physAnimation.CrossFade(this.currClip, this.fadeSpeed);
			this.physAnimation[this.currClip].speed = this.playSpeed;
			if (this.gSlope > 0f && this.useSlope > 15f && this.useSlope < 90f && (this.isWalking || this.isRunning || this.isSprinting))
			{
				this.physAnimation.Blend("anim_miho_walk_water", this.useSlope / 90f * 2f, 0.1f);
			}
			if (this.isFalling)
			{
				this.physAnimation.Blend("anim_miho_fall_normal", 1f, 0.1f);
			}
		}
		else
		{
			Debug.Log("animation " + this.currClip + " cannot be found!");
		}
		if (!this.doBlink)
		{
			this.blinkTime += Time.smoothDeltaTime;
			if (this.blinkTime > this.randBlinkNum)
			{
				this.blinkTime = 0f;
				this.randBlinkNum = UnityEngine.Random.Range(2f, 4f);
				this.doBlink = true;
			}
		}
		this.headTime += Time.smoothDeltaTime;
		if (this.headTime > this.randHeadNum)
		{
			this.headTime = 0f;
			if (UnityEngine.Random.Range(0f, 5f) > 0.3f)
			{
				this.headTgt = 0f;
			}
			else
			{
				this.headTgt = UnityEngine.Random.Range(-80f, 80f);
			}
			this.randHeadNum = UnityEngine.Random.Range(2f, 7f);
			this.randHeadSpd = UnityEngine.Random.Range(1f, 5f);
		}
		if (this.isRunning || this.isSprinting)
		{
			this.headTgt = 0f;
			this.randHeadSpd = 5f;
		}
		this.headRand = Mathf.SmoothStep(this.headRand, this.headTgt, Time.deltaTime * this.randHeadSpd);
		this.eyeRand = Mathf.SmoothStep(this.eyeRand, this.headTgt * 0.75f, Time.deltaTime * (this.randHeadSpd * 2f));
		if (this.eyeRand >= 35f)
		{
			this.eyeRand = 35f;
		}
		if (this.eyeRand <= -35f)
		{
			this.eyeRand = -35f;
		}
		if (this.doBlink)
		{
			float num = 0.5f;
			this.eyelidTime += Time.deltaTime;
			if (this.eyelidTime <= num)
			{
				this.boneLEyelid.transform.localEulerAngles = new Vector3(this.boneLEyelid.transform.localEulerAngles.x, this.boneLEyelid.transform.localEulerAngles.y, Mathf.SmoothStep(265f, 295f, this.eyelidTime * 5f));
				this.boneREyelid.transform.localEulerAngles = new Vector3(this.boneREyelid.transform.localEulerAngles.x, this.boneREyelid.transform.localEulerAngles.y, Mathf.SmoothStep(265f, 295f, this.eyelidTime * 5f));
			}
			if (this.eyelidTime > num)
			{
				this.eyelidTime = 0f;
				this.doBlink = false;
			}
		}
		else
		{
			this.boneLEyelid.transform.localEulerAngles = new Vector3(this.boneLEyelid.transform.localEulerAngles.x, this.boneLEyelid.transform.localEulerAngles.y, 295f);
			this.boneREyelid.transform.localEulerAngles = new Vector3(this.boneREyelid.transform.localEulerAngles.x, this.boneREyelid.transform.localEulerAngles.y, 295f);
		}
		this.boneHead.transform.localEulerAngles = new Vector3(this.headRand, this.boneHead.transform.localEulerAngles.y, this.boneHead.transform.localEulerAngles.z);
		this.boneNeck.transform.localEulerAngles = new Vector3(this.headRand * 0.5f, this.boneNeck.transform.localEulerAngles.y, this.boneNeck.transform.localEulerAngles.z);
		this.boneLEye.transform.localEulerAngles = new Vector3(this.eyeRand, this.boneLEye.transform.localEulerAngles.y, this.boneLEye.transform.localEulerAngles.z);
		this.boneREye.transform.localEulerAngles = new Vector3(this.eyeRand, this.boneREye.transform.localEulerAngles.y, this.boneREye.transform.localEulerAngles.z);
	}

	// Token: 0x060000D7 RID: 215 RVA: 0x0000A780 File Offset: 0x00008980
	private void resetPos()
	{
		float y = base.transform.position.y;
		base.transform.position = new Vector3(this.boneRoot.transform.position.x, y, this.boneRoot.transform.position.z);
	}

	// Token: 0x060000D8 RID: 216 RVA: 0x0000A7DC File Offset: 0x000089DC
	private void SetBoneTransforms()
	{
		this.boneRoot = base.transform.Find("Bip01");
		this.boneNeck = base.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Spine2/Bip01 Spine3/Bip01 Neck");
		this.boneHead = base.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Spine2/Bip01 Spine3/Bip01 Neck/Bip01 Head");
		this.boneLEye = base.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Spine2/Bip01 Spine3/Bip01 Neck/Bip01 Head/Bip01 EyeLeft");
		this.boneREye = base.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Spine2/Bip01 Spine3/Bip01 Neck/Bip01 Head/Bip01 EyeRight");
		this.boneLEyelid = base.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Spine2/Bip01 Spine3/Bip01 Neck/Bip01 Head/Bip01 EyeLidLeft");
		this.boneREyelid = base.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Spine2/Bip01 Spine3/Bip01 Neck/Bip01 Head/Bip01 EyeLidRight");
		this.boneHead.transform.localEulerAngles = new Vector3(this.headRand, this.boneHead.transform.localEulerAngles.y, this.boneHead.transform.localEulerAngles.z);
	}

	// Token: 0x04000213 RID: 531
	public bool isWalking;

	// Token: 0x04000214 RID: 532
	public bool isRunning;

	// Token: 0x04000215 RID: 533
	public bool isSprinting;

	// Token: 0x04000216 RID: 534
	public bool isInWater;

	// Token: 0x04000217 RID: 535
	public bool isInWaterDeep;

	// Token: 0x04000218 RID: 536
	public bool isUnderWater;

	// Token: 0x04000219 RID: 537
	public bool isAtSurface;

	// Token: 0x0400021A RID: 538
	public bool isFloating;

	// Token: 0x0400021B RID: 539
	public bool isFalling;

	// Token: 0x0400021C RID: 540
	public bool isInBoat;

	// Token: 0x0400021D RID: 541
	public float moveSideways;

	// Token: 0x0400021E RID: 542
	public float moveForward;

	// Token: 0x0400021F RID: 543
	public float moveVertical;

	// Token: 0x04000220 RID: 544
	public float wetAmount;

	// Token: 0x04000221 RID: 545
	public float gSlope;

	// Token: 0x04000222 RID: 546
	public float useSlope;

	// Token: 0x04000223 RID: 547
	private GameObject cameraObject;

	// Token: 0x04000224 RID: 548
	private Rigidbody physRigidbody;

	// Token: 0x04000225 RID: 549
	private Animation physAnimation;

	// Token: 0x04000226 RID: 550
	private string currClip;

	// Token: 0x04000227 RID: 551
	private string useClip;

	// Token: 0x04000228 RID: 552
	private string defaultClip;

	// Token: 0x04000229 RID: 553
	private float fadeSpeed;

	// Token: 0x0400022A RID: 554
	private float playSpeed = 1f;

	// Token: 0x0400022B RID: 555
	private float animTime;

	// Token: 0x0400022C RID: 556
	private float blinkTime;

	// Token: 0x0400022D RID: 557
	private bool doBlink;

	// Token: 0x0400022E RID: 558
	private float eyelidTime;

	// Token: 0x0400022F RID: 559
	private float randBlinkNum = 2f;

	// Token: 0x04000230 RID: 560
	private float eyeRand;

	// Token: 0x04000231 RID: 561
	private float headRand;

	// Token: 0x04000232 RID: 562
	private float headTgt;

	// Token: 0x04000233 RID: 563
	private float headTime;

	// Token: 0x04000234 RID: 564
	private float randHeadNum = 4f;

	// Token: 0x04000235 RID: 565
	private float randHeadSpd = 4f;

	// Token: 0x04000236 RID: 566
	private Transform boneRoot;

	// Token: 0x04000237 RID: 567
	private Transform boneLEye;

	// Token: 0x04000238 RID: 568
	private Transform boneREye;

	// Token: 0x04000239 RID: 569
	private Transform boneLEyelid;

	// Token: 0x0400023A RID: 570
	private Transform boneREyelid;

	// Token: 0x0400023B RID: 571
	private Transform boneHead;

	// Token: 0x0400023C RID: 572
	private Transform boneNeck;
}
