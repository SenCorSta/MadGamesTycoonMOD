using System;
using Suimono.Core;
using UnityEngine;

// Token: 0x02000037 RID: 55
public class sui_demo_ControllerOrbit : MonoBehaviour
{
	// Token: 0x060000CA RID: 202 RVA: 0x0001F230 File Offset: 0x0001D430
	private void Awake()
	{
		this.suimonoGameObject = GameObject.Find("SUIMONO_Module");
		if (this.suimonoGameObject != null)
		{
			this.suimonoModuleObject = this.suimonoGameObject.GetComponent<SuimonoModule>();
		}
		this.targetPosition = this.cameraTarget.position;
		this.targetRotation = this.cameraTarget.rotation;
		this.MC = base.gameObject.GetComponent<sui_demo_ControllerMaster>();
		this.IC = base.gameObject.GetComponent<sui_demo_InputController>();
	}

	// Token: 0x060000CB RID: 203 RVA: 0x0001F2B0 File Offset: 0x0001D4B0
	private void FixedUpdate()
	{
		if (this.isActive)
		{
			this.cameraObject = this.MC.cameraObject;
			if (this.isControllable)
			{
				this.moveForward = 0f;
				this.moveSideways = 0f;
				if (this.IC.inputKeyW)
				{
					this.moveForward = 1f;
				}
				if (this.IC.inputKeyS)
				{
					this.moveForward = -1f;
				}
				if (this.IC.inputKeyA)
				{
					this.moveSideways = -1f;
				}
				if (this.IC.inputKeyD)
				{
					this.moveSideways = 1f;
				}
				this.isExtraZoom = this.IC.inputMouseKey1;
				if (this.isExtraZoom)
				{
					this.setFOV = 0.5f;
				}
				else
				{
					this.setFOV = 1f;
				}
				this.isWalking = false;
				if (this.moveForward != 0f || this.moveSideways != 0f)
				{
					this.isWalking = true;
				}
				if (this.IC.inputKeySHIFTL)
				{
					this.runButtonTime += Time.deltaTime;
					if (this.runButtonTime > 0.2f)
					{
						this.isSprinting = true;
					}
				}
				else
				{
					if (this.runButtonTime > 0f)
					{
						if (this.runButtonTime < 0.2f)
						{
							this.isRunning = !this.isRunning;
							if (this.isRunning)
							{
								this.toggleRun = true;
							}
						}
						if (this.runButtonTime > 0.2f)
						{
							this.isRunning = false;
						}
					}
					if (this.isSprinting && this.toggleRun)
					{
						this.isRunning = true;
					}
					this.isSprinting = false;
					this.runButtonTime = 0f;
				}
				if (Input.mousePosition.x > 325f || Input.mousePosition.y < 265f)
				{
					this.orbitView = (this.IC.inputMouseKey0 || this.IC.inputMouseKey1);
				}
				else
				{
					this.orbitView = false;
					this.IC.inputMouseKey0 = false;
					this.IC.inputMouseKey1 = false;
				}
			}
			this.targetPosition = this.cameraTarget.position;
			this.oldMouseRotation = this.MouseRotationDistance;
			this.MouseRotationDistance = this.IC.inputMouseX;
			this.MouseVerticalDistance = this.IC.inputMouseY;
			this.MouseScrollDistance = this.IC.inputMouseWheel;
			if (this.reverseXAxis)
			{
				this.MouseRotationDistance = -this.IC.inputMouseX;
			}
			if (this.reverseYAxis)
			{
				this.MouseVerticalDistance = -this.IC.inputMouseY;
			}
			if (!this.isControllable)
			{
				this.camFOV = 63.2f;
				this.followLat = Mathf.Lerp(this.followLat, -0.85f, Time.deltaTime * 4f);
				this.followHeight = Mathf.Lerp(this.followHeight, 1.8f, Time.deltaTime * 4f);
				this.followDistance = Mathf.Lerp(this.followDistance, 5f, Time.deltaTime * 4f);
				this.axisSensitivity = new Vector2(Mathf.Lerp(this.axisSensitivity.x, this.mouseSensitivity.x, Time.deltaTime * 4f), Mathf.Lerp(this.axisSensitivity.y, this.mouseSensitivity.y, Time.deltaTime * 4f));
				this.cameraObject.GetComponent<Camera>().fieldOfView = this.camFOV;
			}
			this.camFOV = Mathf.Lerp(this.camFOV, this.cameraFOV * this.setFOV, Time.deltaTime * 4f);
			this.followLat = Mathf.Lerp(this.followLat, -0.4f, Time.deltaTime * 4f);
			this.followHeight = Mathf.Lerp(this.followHeight, 1.4f, Time.deltaTime * 2f);
			this.axisSensitivity = new Vector2(Mathf.Lerp(this.axisSensitivity.x, this.mouseSensitivity.x, Time.deltaTime * 4f), Mathf.Lerp(this.axisSensitivity.y, this.mouseSensitivity.y, Time.deltaTime * 4f));
			Cursor.lockState = CursorLockMode.None;
			if (this.suimonoModuleObject != null)
			{
				float num = this.suimonoModuleObject.SuimonoGetHeight(this.cameraTarget.position, "object depth");
				this.isInWater = false;
				if (num < 0f)
				{
					num = 0f;
				}
				if (num > 0f)
				{
					this.isInWater = true;
					this.isInWaterDeep = false;
					this.isUnderWater = false;
					if (num >= 0.9f && num < 1.8f)
					{
						this.isInWaterDeep = true;
					}
					if (num >= 1.8f)
					{
						this.isUnderWater = true;
					}
				}
			}
			if (this.isControllable)
			{
				float num2 = 2f;
				this.followDistance -= this.MouseScrollDistance * 20f;
				this.followDistance = Mathf.Clamp(this.followDistance, this.minZoomAmount, this.maxZoomAmount);
				this.followTgtDistance = Mathf.Lerp(this.followTgtDistance, this.followDistance, Time.deltaTime * num2);
				if (this.orbitView)
				{
					this.camRotation = Mathf.Lerp(this.oldMouseRotation, this.MouseRotationDistance * this.axisSensitivity.x, Time.deltaTime);
				}
				this.targetRotation.eulerAngles = new Vector3(this.targetRotation.eulerAngles.x, this.targetRotation.eulerAngles.y + this.camRotation, this.targetRotation.eulerAngles.z);
				this.cameraObject.transform.eulerAngles = new Vector3(this.targetRotation.eulerAngles.x, this.targetRotation.eulerAngles.y, this.cameraObject.transform.eulerAngles.z);
				if (this.orbitView)
				{
					this.camHeight = Mathf.Lerp(this.camHeight, this.camHeight + this.MouseVerticalDistance * this.axisSensitivity.y, Time.deltaTime);
				}
				this.camHeight = Mathf.Clamp(this.camHeight, -45f, 45f);
				this.cameraObject.transform.position = new Vector3(this.cameraTarget.transform.position.x + this.cameraOffset.x + -this.cameraObject.transform.up.x * this.followTgtDistance, Mathf.Lerp(this.camHeight, this.cameraTarget.transform.position.y + this.cameraOffset.y + -this.cameraObject.transform.up.y * this.followTgtDistance, Time.deltaTime * 0.5f), this.cameraTarget.transform.position.z + this.cameraOffset.z + -this.cameraObject.transform.up.z * this.followTgtDistance);
				this.cameraObject.transform.LookAt(new Vector3(this.targetPosition.x, this.targetPosition.y + this.followHeight, this.targetPosition.z));
			}
			if (this.isControllable)
			{
				this.cameraObject.GetComponent<Camera>().fieldOfView = this.camFOV;
			}
		}
		if (this.targetAnimator != null)
		{
			this.targetAnimator.isWalking = this.isWalking;
			this.targetAnimator.isRunning = this.isRunning;
			this.targetAnimator.isSprinting = this.isSprinting;
			this.targetAnimator.moveForward = this.moveForward;
			this.targetAnimator.moveSideways = this.moveSideways;
			this.targetAnimator.gSlope = this.gSlope;
			this.targetAnimator.useSlope = this.useSlope;
			this.targetAnimator.isInWater = this.isInWater;
			this.targetAnimator.isInWaterDeep = this.isInWaterDeep;
			this.targetAnimator.isUnderWater = this.isUnderWater;
		}
	}

	// Token: 0x040001AF RID: 431
	public bool isActive;

	// Token: 0x040001B0 RID: 432
	public bool isControllable = true;

	// Token: 0x040001B1 RID: 433
	public bool isExtraZoom;

	// Token: 0x040001B2 RID: 434
	public Transform cameraTarget;

	// Token: 0x040001B3 RID: 435
	public bool reverseYAxis = true;

	// Token: 0x040001B4 RID: 436
	public bool reverseXAxis;

	// Token: 0x040001B5 RID: 437
	public Vector2 mouseSensitivity = new Vector2(4f, 4f);

	// Token: 0x040001B6 RID: 438
	public float cameraFOV = 35f;

	// Token: 0x040001B7 RID: 439
	public Vector3 cameraOffset = new Vector3(0f, 0f, 0f);

	// Token: 0x040001B8 RID: 440
	public float cameraLean;

	// Token: 0x040001B9 RID: 441
	public float rotationSensitivity = 6f;

	// Token: 0x040001BA RID: 442
	public Vector3 rotationLimits = new Vector3(0f, 0f, 0f);

	// Token: 0x040001BB RID: 443
	public float minZoomAmount = 1.25f;

	// Token: 0x040001BC RID: 444
	public float maxZoomAmount = 8f;

	// Token: 0x040001BD RID: 445
	public bool showDebug;

	// Token: 0x040001BE RID: 446
	public sui_demo_animCharacter targetAnimator;

	// Token: 0x040001BF RID: 447
	private Transform cameraObject;

	// Token: 0x040001C0 RID: 448
	private Vector2 axisSensitivity = new Vector2(4f, 4f);

	// Token: 0x040001C1 RID: 449
	private float followDistance = 10f;

	// Token: 0x040001C2 RID: 450
	private float followHeight = 1f;

	// Token: 0x040001C3 RID: 451
	private float followLat;

	// Token: 0x040001C4 RID: 452
	private float camFOV = 35f;

	// Token: 0x040001C5 RID: 453
	private float camRotation;

	// Token: 0x040001C6 RID: 454
	private Vector3 camRot;

	// Token: 0x040001C7 RID: 455
	private float camHeight = 4f;

	// Token: 0x040001C8 RID: 456
	private bool isInWater;

	// Token: 0x040001C9 RID: 457
	private bool isInWaterDeep;

	// Token: 0x040001CA RID: 458
	private bool isUnderWater;

	// Token: 0x040001CB RID: 459
	private Vector3 targetPosition;

	// Token: 0x040001CC RID: 460
	private float MouseRotationDistance;

	// Token: 0x040001CD RID: 461
	private float MouseVerticalDistance;

	// Token: 0x040001CE RID: 462
	private GameObject suimonoGameObject;

	// Token: 0x040001CF RID: 463
	private SuimonoModule suimonoModuleObject;

	// Token: 0x040001D0 RID: 464
	private float followTgtDistance;

	// Token: 0x040001D1 RID: 465
	private bool orbitView;

	// Token: 0x040001D2 RID: 466
	private Quaternion targetRotation;

	// Token: 0x040001D3 RID: 467
	private float MouseScrollDistance;

	// Token: 0x040001D4 RID: 468
	private float setFOV = 1f;

	// Token: 0x040001D5 RID: 469
	private float moveForward;

	// Token: 0x040001D6 RID: 470
	private float moveSideways;

	// Token: 0x040001D7 RID: 471
	private bool isWalking;

	// Token: 0x040001D8 RID: 472
	private bool isRunning;

	// Token: 0x040001D9 RID: 473
	private bool isSprinting;

	// Token: 0x040001DA RID: 474
	private Vector3 savePos;

	// Token: 0x040001DB RID: 475
	private float oldMouseRotation;

	// Token: 0x040001DC RID: 476
	private sui_demo_ControllerMaster MC;

	// Token: 0x040001DD RID: 477
	private sui_demo_InputController IC;

	// Token: 0x040001DE RID: 478
	private float runButtonTime;

	// Token: 0x040001DF RID: 479
	private bool toggleRun;

	// Token: 0x040001E0 RID: 480
	private float gSlope;

	// Token: 0x040001E1 RID: 481
	private float useSlope;
}
