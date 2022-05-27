using System;
using UnityEngine;


public class sui_demo_animCharacter : MonoBehaviour
{
	
	private void Start()
	{
		this.physRigidbody = base.GetComponent<Rigidbody>();
		this.physAnimation = base.GetComponent<Animation>();
		this.useClip = "anim_miho_idle_normal";
		this.defaultClip = this.useClip;
		this.SetBoneTransforms();
	}

	
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

	
	private void resetPos()
	{
		float y = base.transform.position.y;
		base.transform.position = new Vector3(this.boneRoot.transform.position.x, y, this.boneRoot.transform.position.z);
	}

	
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

	
	public bool isWalking;

	
	public bool isRunning;

	
	public bool isSprinting;

	
	public bool isInWater;

	
	public bool isInWaterDeep;

	
	public bool isUnderWater;

	
	public bool isAtSurface;

	
	public bool isFloating;

	
	public bool isFalling;

	
	public bool isInBoat;

	
	public float moveSideways;

	
	public float moveForward;

	
	public float moveVertical;

	
	public float wetAmount;

	
	public float gSlope;

	
	public float useSlope;

	
	private GameObject cameraObject;

	
	private Rigidbody physRigidbody;

	
	private Animation physAnimation;

	
	private string currClip;

	
	private string useClip;

	
	private string defaultClip;

	
	private float fadeSpeed;

	
	private float playSpeed = 1f;

	
	private float animTime;

	
	private float blinkTime;

	
	private bool doBlink;

	
	private float eyelidTime;

	
	private float randBlinkNum = 2f;

	
	private float eyeRand;

	
	private float headRand;

	
	private float headTgt;

	
	private float headTime;

	
	private float randHeadNum = 4f;

	
	private float randHeadSpd = 4f;

	
	private Transform boneRoot;

	
	private Transform boneLEye;

	
	private Transform boneREye;

	
	private Transform boneLEyelid;

	
	private Transform boneREyelid;

	
	private Transform boneHead;

	
	private Transform boneNeck;
}
