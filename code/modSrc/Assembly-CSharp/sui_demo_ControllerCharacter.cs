using System;
using Suimono.Core;
using UnityEngine;

// Token: 0x02000034 RID: 52
public class sui_demo_ControllerCharacter : MonoBehaviour
{
	// Token: 0x060000C3 RID: 195 RVA: 0x0001D54C File Offset: 0x0001B74C
	private void Awake()
	{
		this.suimonoGameObject = GameObject.Find("SUIMONO_Module");
		if (this.suimonoGameObject != null)
		{
			this.suimonoModuleObject = this.suimonoGameObject.GetComponent<SuimonoModule>();
		}
		this.targetPosition = this.cameraTarget.position;
		this.targetRotation = this.cameraTarget.rotation;
		if (this.cameraTarget != null)
		{
			this.targetAnimator = this.cameraTarget.gameObject.GetComponent<sui_demo_animCharacter>();
		}
		if (this.buoyancyTarget != null)
		{
			this.buoyancyObject = this.buoyancyTarget.GetComponent<fx_buoyancy>();
		}
		this.MC = base.gameObject.GetComponent<sui_demo_ControllerMaster>();
		this.IC = base.gameObject.GetComponent<sui_demo_InputController>();
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x0001D610 File Offset: 0x0001B810
	private void LateUpdate()
	{
		if (this.rotationLimits.x != 0f)
		{
			if (this.cameraTarget.transform.eulerAngles.x < 360f - this.rotationLimits.x && this.cameraTarget.transform.eulerAngles.x > this.rotationLimits.x + 10f)
			{
				this.cameraTarget.transform.eulerAngles = new Vector3(360f - this.rotationLimits.x, this.cameraTarget.transform.eulerAngles.y, this.cameraTarget.transform.eulerAngles.z);
			}
			else if (this.cameraTarget.transform.eulerAngles.x > this.rotationLimits.x && this.cameraTarget.transform.eulerAngles.x < 360f - this.rotationLimits.x)
			{
				this.cameraTarget.transform.eulerAngles = new Vector3(this.rotationLimits.x, this.cameraTarget.transform.eulerAngles.y, this.cameraTarget.transform.eulerAngles.z);
			}
		}
		if (this.rotationLimits.y != 0f)
		{
			if (this.cameraTarget.transform.eulerAngles.y < 360f - this.rotationLimits.y && this.cameraTarget.transform.eulerAngles.y > this.rotationLimits.y + 10f)
			{
				this.cameraTarget.transform.eulerAngles = new Vector3(this.cameraTarget.transform.eulerAngles.x, 360f - this.rotationLimits.y, this.cameraTarget.transform.eulerAngles.z);
			}
			else if (this.cameraTarget.transform.eulerAngles.y > this.rotationLimits.y && this.cameraTarget.transform.eulerAngles.y < 360f - this.rotationLimits.y)
			{
				this.cameraTarget.transform.eulerAngles = new Vector3(this.cameraTarget.transform.eulerAngles.x, this.rotationLimits.y, this.cameraTarget.transform.eulerAngles.z);
			}
		}
		if (this.rotationLimits.z != 0f)
		{
			if (this.cameraTarget.transform.eulerAngles.z < 360f - this.rotationLimits.z && this.cameraTarget.transform.eulerAngles.z > this.rotationLimits.z + 10f)
			{
				this.cameraTarget.transform.eulerAngles = new Vector3(this.cameraTarget.transform.eulerAngles.x, this.cameraTarget.transform.eulerAngles.y, 360f - this.rotationLimits.z);
				return;
			}
			if (this.cameraTarget.transform.eulerAngles.z > this.rotationLimits.z && this.cameraTarget.transform.eulerAngles.z < 360f - this.rotationLimits.z)
			{
				this.cameraTarget.transform.eulerAngles = new Vector3(this.cameraTarget.transform.eulerAngles.x, this.cameraTarget.transform.eulerAngles.y, this.rotationLimits.z);
			}
		}
	}

	// Token: 0x060000C5 RID: 197 RVA: 0x0001DA04 File Offset: 0x0001BC04
	private void FixedUpdate()
	{
		if (this.isActive)
		{
			this.cameraObject = this.MC.cameraObject;
			if (this.isControllable)
			{
				this.moveForward = 0f;
				this.moveSideways = 0f;
				this.moveVert = 0f;
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
				if (this.IC.inputKeyQ)
				{
					this.moveVert = -1f;
				}
				if (this.IC.inputKeyE)
				{
					this.moveVert = 1f;
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
				this.orbitView = this.IC.inputKeySPACE;
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
				this.waterLevel = this.suimonoModuleObject.SuimonoGetHeight(this.cameraTarget.position, "object depth");
				this.isInWater = false;
				if (this.waterLevel < 0f)
				{
					this.waterLevel = 0f;
				}
				if (this.waterLevel > 0f)
				{
					this.isInWater = true;
					this.isInWaterDeep = false;
					this.isUnderWater = false;
					this.isFloating = false;
					this.isAtSurface = false;
					if (this.waterLevel >= 0.9f && this.waterLevel < 1.8f)
					{
						this.isInWaterDeep = true;
					}
					if (this.waterLevel >= 1.8f)
					{
						this.isUnderWater = true;
					}
					if (this.waterLevel >= 1.2f && this.waterLevel < 1.8f)
					{
						this.isAtSurface = true;
					}
					if (this.isInWaterDeep && this.waterLevel > 2f)
					{
						this.isFloating = true;
					}
				}
				if (this.isUnderWater)
				{
					if (this.buoyancyObject != null)
					{
						this.buoyancyObject.engageBuoyancy = false;
					}
				}
				else if (this.buoyancyObject != null)
				{
					this.buoyancyObject.engageBuoyancy = true;
				}
			}
			if (this.isControllable)
			{
				if (!this.orbitView)
				{
					this.rotSense = this.rotationSensitivity;
					if (this.isSprinting)
					{
						this.rotSense *= 2f;
					}
					float num = 0f;
					if (this.moveForward == 1f && this.moveSideways == 0f)
					{
						float num2 = this.cameraObject.transform.eulerAngles.y;
						if (this.cameraTarget.eulerAngles.y - num2 > 180f)
						{
							num = -360f;
						}
						if (this.cameraTarget.eulerAngles.y - num2 < -180f)
						{
							num = 360f;
						}
						this.cameraTarget.eulerAngles = new Vector3(this.cameraTarget.eulerAngles.x, Mathf.Lerp(this.cameraTarget.eulerAngles.y + num, num2, Time.deltaTime * this.rotSense), this.cameraTarget.eulerAngles.z);
					}
					else if (this.moveForward == -1f && this.moveSideways == 0f)
					{
						this.rotSense *= 1f;
						float num2 = this.cameraObject.transform.eulerAngles.y - 180f;
						if (this.cameraTarget.eulerAngles.y - num2 > 180f)
						{
							num = -360f;
						}
						if (this.cameraTarget.eulerAngles.y - num2 < -180f)
						{
							num = 360f;
						}
						this.cameraTarget.eulerAngles = new Vector3(this.cameraTarget.eulerAngles.x, Mathf.Lerp(this.cameraTarget.eulerAngles.y + num, num2, Time.deltaTime * this.rotSense), this.cameraTarget.eulerAngles.z);
					}
					else if (this.moveSideways != 0f && this.moveForward == 0f)
					{
						float num2 = this.cameraObject.transform.eulerAngles.y + 90f * this.moveSideways;
						if (this.cameraTarget.eulerAngles.y - num2 > 180f)
						{
							num = -360f;
						}
						if (this.cameraTarget.eulerAngles.y - num2 < -180f)
						{
							num = 360f;
						}
						this.cameraTarget.eulerAngles = new Vector3(this.cameraTarget.eulerAngles.x, Mathf.Lerp(this.cameraTarget.eulerAngles.y + num, num2, Time.deltaTime * this.rotSense), this.cameraTarget.eulerAngles.z);
					}
					else if (this.moveSideways != 0f && this.moveForward == 1f)
					{
						this.rotSense *= 1.4f;
						float num2 = this.cameraObject.transform.eulerAngles.y + 45f * this.moveSideways;
						if (this.cameraTarget.eulerAngles.y - num2 > 180f)
						{
							num = -360f;
						}
						if (this.cameraTarget.eulerAngles.y - num2 < -180f)
						{
							num = 360f;
						}
						this.cameraTarget.eulerAngles = new Vector3(this.cameraTarget.eulerAngles.x, Mathf.Lerp(this.cameraTarget.eulerAngles.y + num, num2, Time.deltaTime * this.rotSense), this.cameraTarget.eulerAngles.z);
					}
					else if (this.moveSideways != 0f && this.moveForward == -1f)
					{
						this.rotSense *= 1.4f;
						float num2 = this.cameraObject.transform.eulerAngles.y - 180f - 45f * this.moveSideways;
						if (this.cameraTarget.eulerAngles.y - num2 > 180f)
						{
							num = 360f;
						}
						if (this.cameraTarget.eulerAngles.y - num2 < -180f)
						{
							num = -360f;
						}
						this.cameraTarget.eulerAngles = new Vector3(this.cameraTarget.eulerAngles.x, Mathf.Lerp(this.cameraTarget.eulerAngles.y - num, num2, Time.deltaTime * this.rotSense), this.cameraTarget.eulerAngles.z);
					}
					else
					{
						this.xMove = Mathf.Lerp(this.xMove, 0f, Time.deltaTime);
					}
					this.cameraTarget.eulerAngles = new Vector3(0f, this.cameraTarget.eulerAngles.y, 0f);
				}
				this.gSlope = 0f;
				this.useSlope = 0f;
				Vector3 vector = this.cameraTarget.position + new Vector3(0f, 1f, 0f);
				Vector3 vector2 = this.cameraTarget.position + new Vector3(0f, -0.25f, 0f);
				RaycastHit raycastHit;
				if (Physics.Linecast(vector, vector2, out raycastHit) && raycastHit.transform != this.cameraTarget.transform)
				{
					if (this.showDebug)
					{
						Debug.DrawLine(vector, vector2, Color.red);
					}
					RaycastHit raycastHit2;
					if (Physics.Linecast(vector + this.cameraTarget.forward * 0.25f, vector2 + this.cameraTarget.forward * 0.25f, out raycastHit2))
					{
						if (this.showDebug)
						{
							Debug.DrawLine(vector + this.cameraTarget.forward * 0.25f, vector2 + this.cameraTarget.forward * 0.25f, Color.green);
						}
						if (this.showDebug)
						{
							Debug.DrawLine(raycastHit.point, raycastHit2.point, Color.black);
						}
						float y = raycastHit2.point.y - raycastHit.point.y;
						float x = raycastHit2.point.x - raycastHit.point.x;
						this.gSlope = Mathf.Atan2(y, x) * 57.29578f;
						this.useSlope = this.gSlope;
						if (this.gSlope < 0f)
						{
							this.useSlope = 90f + Mathf.Atan2(y, x) * 57.29578f % 90f;
						}
					}
				}
				if (this.isUnderWater)
				{
					this.moveSideways = 0f;
				}
				if (this.moveForward == 0f && this.moveSideways == 0f)
				{
					this.isWalking = false;
					this.isRunning = false;
					this.isSprinting = false;
				}
				float num3 = 1.7f;
				if (this.isRunning)
				{
					num3 = 2.5f;
				}
				if (this.isSprinting)
				{
					num3 = 3.5f;
				}
				this.moveSpeed = this.walkSpeed;
				if (this.isInWaterDeep || this.isUnderWater)
				{
					this.isRunning = false;
				}
				if (this.isRunning)
				{
					this.moveSpeed = this.runSpeed;
				}
				if (this.isSprinting)
				{
					this.moveSpeed = this.sprintSpeed;
				}
				if (this.moveForward != 0f && this.moveSideways != 0f)
				{
					this.moveSpeed *= 0.5f;
				}
				float num4 = 1f;
				if (this.isInWater)
				{
					num4 = 0.8f;
				}
				if (this.isInWaterDeep)
				{
					num4 = 0.6f;
				}
				if (this.isUnderWater)
				{
					num4 = 0.5f;
				}
				this.moveSpeed *= num4;
				if (this.gSlope > 0f && this.useSlope > 0.25f && this.useSlope < 90f)
				{
					this.moveSpeed *= 1.25f - this.useSlope / 90f;
				}
				this.useSpeed = Mathf.Lerp(this.useSpeed, this.moveSpeed * this.moveForward, Time.deltaTime * num3);
				this.useSideSpeed = Mathf.Lerp(this.useSideSpeed, this.moveSpeed * this.moveSideways, Time.deltaTime * num3);
				if (this.isUnderWater || this.isInWater)
				{
					this.useVertSpeed = Mathf.Lerp(this.useVertSpeed, this.moveSpeed * this.moveVert, Time.deltaTime * num3);
				}
				else
				{
					this.useVertSpeed = 0f;
				}
				if (this.cameraTarget.GetComponent<Rigidbody>())
				{
					Vector3 a = this.cameraTarget.transform.forward * this.useSpeed * this.moveForwardTgt;
					if (this.moveForward != 0f && this.moveForwardTgt != this.moveForward)
					{
						this.moveForwardTgt = this.moveForward;
					}
					Vector3 b = this.cameraTarget.transform.forward * this.useSideSpeed * this.moveSidewaysTgt;
					if (this.moveSideways != 0f && this.moveSidewaysTgt != this.moveSideways)
					{
						this.moveSidewaysTgt = this.moveSideways;
					}
					Vector3 b2 = new Vector3(0f, this.useVertSpeed, 0f);
					this.cameraTarget.GetComponent<Rigidbody>().MovePosition(this.cameraTarget.GetComponent<Rigidbody>().position + (a + b + b2));
				}
				float num5 = 2f;
				this.followDistance -= this.MouseScrollDistance * 8f;
				this.followDistance = Mathf.Clamp(this.followDistance, this.minZoomAmount, this.maxZoomAmount);
				this.followTgtDistance = Mathf.Lerp(this.followTgtDistance, this.followDistance, Time.deltaTime * num5);
				this.camRotation = Mathf.Lerp(this.oldMouseRotation, this.MouseRotationDistance * this.axisSensitivity.x, Time.deltaTime);
				this.targetRotation.eulerAngles = new Vector3(this.targetRotation.eulerAngles.x, this.targetRotation.eulerAngles.y + this.camRotation, this.targetRotation.eulerAngles.z);
				this.cameraObject.transform.eulerAngles = new Vector3(this.targetRotation.eulerAngles.x, this.targetRotation.eulerAngles.y, this.cameraObject.transform.eulerAngles.z);
				this.camHeight = Mathf.Lerp(this.camHeight, this.camHeight + this.MouseVerticalDistance * this.axisSensitivity.y, Time.deltaTime);
				if (this.keepAboveSurface && this.suimonoModuleObject != null)
				{
					this.camHeight = Mathf.Clamp(this.camHeight, this.waterLevel + 0.25f, 12f);
				}
				else
				{
					this.camHeight = Mathf.Clamp(this.camHeight, -1f, 12f);
				}
				this.cameraObject.transform.position = this.cameraTarget.transform.position + -this.cameraObject.transform.forward * this.followTgtDistance;
				this.cameraObject.transform.position = new Vector3(this.cameraObject.transform.position.x, this.cameraObject.transform.position.y + this.camHeight, this.cameraObject.transform.position.z);
				this.cameraObject.transform.LookAt(new Vector3(this.targetPosition.x, this.targetPosition.y + this.followHeight, this.targetPosition.z));
				if (this.handleObjectOcclusion)
				{
					Vector3 position = this.cameraTarget.transform.position;
					position = new Vector3(position.x, position.y + this.followHeight, position.z);
					RaycastHit raycastHit3 = default(RaycastHit);
					if (Physics.Linecast(position, this.cameraObject.transform.position, out raycastHit3) && raycastHit3.transform.gameObject.layer != this.suimonoModuleObject.layerWaterNum && !(raycastHit3.transform == base.transform) && !(raycastHit3.transform == this.cameraTarget))
					{
						bool flag = false;
						if (raycastHit3.transform.GetComponent<Collider>() != null && raycastHit3.transform.GetComponent<Collider>().isTrigger)
						{
							flag = true;
						}
						if (!flag)
						{
							this.cameraObject.transform.position = raycastHit3.point;
						}
					}
				}
				this.cameraObject.transform.eulerAngles = new Vector3(this.cameraObject.transform.rotation.eulerAngles.x, this.cameraObject.transform.rotation.eulerAngles.y, this.cameraLean);
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
			this.targetAnimator.isFloating = this.isFloating;
			this.targetAnimator.isAtSurface = this.isAtSurface;
			this.targetAnimator.isFalling = this.isFalling;
			this.targetAnimator.isInBoat = this.isInBoat;
		}
	}

	// Token: 0x0400015B RID: 347
	public bool isActive;

	// Token: 0x0400015C RID: 348
	public bool isControllable = true;

	// Token: 0x0400015D RID: 349
	public bool isExtraZoom;

	// Token: 0x0400015E RID: 350
	public bool keepAboveSurface;

	// Token: 0x0400015F RID: 351
	public bool handleObjectOcclusion = true;

	// Token: 0x04000160 RID: 352
	public Transform cameraTarget;

	// Token: 0x04000161 RID: 353
	public Transform buoyancyTarget;

	// Token: 0x04000162 RID: 354
	public bool reverseYAxis = true;

	// Token: 0x04000163 RID: 355
	public bool reverseXAxis;

	// Token: 0x04000164 RID: 356
	public Vector2 mouseSensitivity = new Vector2(4f, 4f);

	// Token: 0x04000165 RID: 357
	public float cameraFOV = 35f;

	// Token: 0x04000166 RID: 358
	public Vector2 cameraOffset = new Vector2(0f, 0f);

	// Token: 0x04000167 RID: 359
	public float cameraLean;

	// Token: 0x04000168 RID: 360
	public float walkSpeed = 0.02f;

	// Token: 0x04000169 RID: 361
	public float runSpeed = 0.4f;

	// Token: 0x0400016A RID: 362
	public float sprintSpeed = 0.4f;

	// Token: 0x0400016B RID: 363
	public float rotationSensitivity = 6f;

	// Token: 0x0400016C RID: 364
	public Vector3 rotationLimits = new Vector3(0f, 0f, 0f);

	// Token: 0x0400016D RID: 365
	public float minZoomAmount = 1.25f;

	// Token: 0x0400016E RID: 366
	public float maxZoomAmount = 8f;

	// Token: 0x0400016F RID: 367
	public bool showDebug;

	// Token: 0x04000170 RID: 368
	public sui_demo_animCharacter targetAnimator;

	// Token: 0x04000171 RID: 369
	public bool isInBoat;

	// Token: 0x04000172 RID: 370
	private Transform cameraObject;

	// Token: 0x04000173 RID: 371
	private fx_buoyancy buoyancyObject;

	// Token: 0x04000174 RID: 372
	private float rotSense;

	// Token: 0x04000175 RID: 373
	private Vector2 axisSensitivity = new Vector2(4f, 4f);

	// Token: 0x04000176 RID: 374
	private float followDistance = 5f;

	// Token: 0x04000177 RID: 375
	private float followHeight = 1f;

	// Token: 0x04000178 RID: 376
	private float followLat;

	// Token: 0x04000179 RID: 377
	private float camFOV = 35f;

	// Token: 0x0400017A RID: 378
	private float camRotation;

	// Token: 0x0400017B RID: 379
	private Vector3 camRot;

	// Token: 0x0400017C RID: 380
	private float camHeight = 2f;

	// Token: 0x0400017D RID: 381
	private bool isInWater;

	// Token: 0x0400017E RID: 382
	private bool isInWaterDeep;

	// Token: 0x0400017F RID: 383
	private bool isUnderWater;

	// Token: 0x04000180 RID: 384
	private bool isAtSurface;

	// Token: 0x04000181 RID: 385
	private bool isFloating;

	// Token: 0x04000182 RID: 386
	private bool isFalling;

	// Token: 0x04000183 RID: 387
	private Vector3 targetPosition;

	// Token: 0x04000184 RID: 388
	private float MouseRotationDistance;

	// Token: 0x04000185 RID: 389
	private float MouseVerticalDistance;

	// Token: 0x04000186 RID: 390
	private GameObject suimonoGameObject;

	// Token: 0x04000187 RID: 391
	private SuimonoModule suimonoModuleObject;

	// Token: 0x04000188 RID: 392
	private float followTgtDistance;

	// Token: 0x04000189 RID: 393
	private bool orbitView;

	// Token: 0x0400018A RID: 394
	private Quaternion targetRotation;

	// Token: 0x0400018B RID: 395
	private float MouseScrollDistance;

	// Token: 0x0400018C RID: 396
	private float setFOV = 1f;

	// Token: 0x0400018D RID: 397
	private float useSpeed;

	// Token: 0x0400018E RID: 398
	private float useSideSpeed;

	// Token: 0x0400018F RID: 399
	private float useVertSpeed;

	// Token: 0x04000190 RID: 400
	private float moveSpeed = 0.05f;

	// Token: 0x04000191 RID: 401
	private float moveForward;

	// Token: 0x04000192 RID: 402
	private float moveSideways;

	// Token: 0x04000193 RID: 403
	private float moveForwardTgt;

	// Token: 0x04000194 RID: 404
	private float moveSidewaysTgt;

	// Token: 0x04000195 RID: 405
	private float moveVert;

	// Token: 0x04000196 RID: 406
	private bool isWalking;

	// Token: 0x04000197 RID: 407
	private bool isRunning;

	// Token: 0x04000198 RID: 408
	private bool isSprinting;

	// Token: 0x04000199 RID: 409
	private Vector3 savePos;

	// Token: 0x0400019A RID: 410
	private float oldMouseRotation;

	// Token: 0x0400019B RID: 411
	private sui_demo_ControllerMaster MC;

	// Token: 0x0400019C RID: 412
	private sui_demo_InputController IC;

	// Token: 0x0400019D RID: 413
	private float xMove;

	// Token: 0x0400019E RID: 414
	private float runButtonTime;

	// Token: 0x0400019F RID: 415
	private bool toggleRun;

	// Token: 0x040001A0 RID: 416
	private float gSlope;

	// Token: 0x040001A1 RID: 417
	private float useSlope;

	// Token: 0x040001A2 RID: 418
	private float waterLevel;
}
