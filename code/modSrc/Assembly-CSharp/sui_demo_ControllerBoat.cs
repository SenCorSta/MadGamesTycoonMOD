using System;
using Suimono.Core;
using UnityEngine;

// Token: 0x02000033 RID: 51
public class sui_demo_ControllerBoat : MonoBehaviour
{
	// Token: 0x060000BF RID: 191 RVA: 0x00005DE4 File Offset: 0x00003FE4
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
			this.targetAnimator = this.cameraTarget.gameObject.GetComponent<sui_demo_animBoat>();
		}
		this.MC = base.gameObject.GetComponent<sui_demo_ControllerMaster>();
		this.IC = base.gameObject.GetComponent<sui_demo_InputController>();
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x00005E88 File Offset: 0x00004088
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

	// Token: 0x060000C1 RID: 193 RVA: 0x0000627C File Offset: 0x0000447C
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
				this.isRunning = this.IC.inputKeySHIFTL;
				if (this.moveForward == -1f)
				{
					this.isRunning = false;
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
			this.axisSensitivity = new Vector2(Mathf.Lerp(this.axisSensitivity.x, this.mouseSensitivity.x, Time.deltaTime * 4f), this.axisSensitivity.y);
			this.axisSensitivity = new Vector2(this.axisSensitivity.x, Mathf.Lerp(this.axisSensitivity.y, this.mouseSensitivity.y, Time.deltaTime * 4f));
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
					if (this.waterLevel >= 0.9f && this.waterLevel < 1.8f)
					{
						this.isInWaterDeep = true;
					}
					if (this.waterLevel >= 1.8f)
					{
						this.isUnderWater = true;
					}
				}
			}
			float num = 5f;
			if (this.isRunning && this.moveForward > 0f)
			{
				num = 2.5f;
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
			if (this.moveForward != 0f && this.moveSideways != 0f)
			{
				this.moveSpeed *= 0.75f;
			}
			if (!this.isInWater)
			{
				this.moveSpeed *= 0f;
			}
			this.useSpeed = Mathf.Lerp(this.useSpeed, this.moveSpeed * this.moveForward, Time.deltaTime * num);
			this.useSideSpeed = Mathf.Lerp(this.useSideSpeed, this.moveSpeed * this.moveSideways, Time.deltaTime * num);
			if (this.isControllable)
			{
				if (this.moveForward != 0f)
				{
					this.xMove = Mathf.Lerp(this.xMove, this.useSpeed, Time.deltaTime);
					this.zMove = Mathf.Lerp(this.zMove, this.useSpeed, Time.deltaTime);
					this.cameraTarget.eulerAngles = new Vector3(this.cameraTarget.eulerAngles.x, this.cameraTarget.eulerAngles.y + Mathf.Lerp(0f, 20f * this.moveSideways * this.moveForward, Time.deltaTime * Mathf.Abs(this.xMove * 10f)), this.cameraTarget.eulerAngles.z);
					if (this.isInWater)
					{
						this.cameraTarget.eulerAngles = new Vector3(this.cameraTarget.eulerAngles.x, this.cameraTarget.eulerAngles.y, this.cameraTarget.eulerAngles.z + Mathf.Lerp(0f, -130f * this.moveSideways * this.moveForward, Time.deltaTime * Mathf.Abs(this.zMove * 5f)));
					}
				}
				else
				{
					this.xMove = Mathf.Lerp(this.xMove, 0f, Time.deltaTime);
				}
				if (this.cameraTarget.GetComponent<Rigidbody>())
				{
					Vector3 a = this.cameraTarget.transform.forward * this.xMove;
					Vector3 b = new Vector3(0f, 0f, 0f);
					this.cameraTarget.GetComponent<Rigidbody>().MovePosition(this.cameraTarget.GetComponent<Rigidbody>().position + (a + b));
				}
				float num2 = 2f;
				this.followDistance -= this.MouseScrollDistance * 8f;
				this.followDistance = Mathf.Clamp(this.followDistance, this.minZoomAmount, this.maxZoomAmount);
				this.followTgtDistance = Mathf.Lerp(this.followTgtDistance, this.followDistance, Time.deltaTime * num2);
				this.camRotation = Mathf.Lerp(this.oldMouseRotation, this.MouseRotationDistance * this.axisSensitivity.x, Time.deltaTime);
				this.targetRotation.eulerAngles = new Vector3(this.targetRotation.eulerAngles.x, this.targetRotation.eulerAngles.y + this.camRotation, this.targetRotation.eulerAngles.z);
				this.cameraObject.transform.eulerAngles = new Vector3(this.targetRotation.eulerAngles.x, this.cameraObject.transform.eulerAngles.y, this.cameraObject.transform.eulerAngles.z);
				this.cameraObject.transform.eulerAngles = new Vector3(this.cameraObject.transform.eulerAngles.x, this.targetRotation.eulerAngles.y, this.cameraObject.transform.eulerAngles.z);
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
					RaycastHit raycastHit = default(RaycastHit);
					if (Physics.Linecast(position, this.cameraObject.transform.position, out raycastHit) && raycastHit.transform.gameObject.layer != 4 && !(raycastHit.transform == base.transform) && !(raycastHit.transform == this.cameraTarget))
					{
						bool flag = false;
						if (raycastHit.transform.GetComponent<Collider>() != null && raycastHit.transform.GetComponent<Collider>().isTrigger)
						{
							flag = true;
						}
						if (!flag)
						{
							this.cameraObject.transform.position = raycastHit.point;
						}
					}
				}
				this.cameraObject.transform.position = new Vector3(this.cameraObject.transform.position.x + this.cameraOffset.x, this.cameraObject.transform.position.y, this.cameraObject.transform.position.z);
				this.cameraObject.transform.position = new Vector3(this.cameraObject.transform.position.x, this.cameraObject.transform.position.y + this.cameraOffset.y, this.cameraObject.transform.position.z);
				this.cameraObject.transform.eulerAngles = new Vector3(this.cameraObject.transform.rotation.eulerAngles.x, this.cameraObject.transform.rotation.eulerAngles.y, this.cameraLean);
			}
			if (this.isControllable)
			{
				this.cameraObject.GetComponent<Camera>().fieldOfView = this.camFOV;
			}
			if (this.targetAnimator != null)
			{
				if (this.moveForward > 0f)
				{
					this.targetAnimator.behaviorIsRevving = true;
					this.targetAnimator.behaviorIsRevvingHigh = this.isRunning;
					this.targetAnimator.behaviorIsRevvingBack = false;
				}
				else if (this.moveForward < 0f)
				{
					this.targetAnimator.behaviorIsRevving = false;
					this.targetAnimator.behaviorIsRevvingHigh = false;
					this.targetAnimator.behaviorIsRevvingBack = true;
				}
				else if (this.moveForward == 0f)
				{
					this.targetAnimator.behaviorIsRevving = false;
					this.targetAnimator.behaviorIsRevvingHigh = false;
					this.targetAnimator.behaviorIsRevvingBack = false;
				}
				this.targetAnimator.engineRotation = this.moveSideways;
			}
		}
		if (this.targetAnimator != null)
		{
			this.targetAnimator.behaviorIsOn = this.isActive;
		}
	}

	// Token: 0x04000127 RID: 295
	public bool isActive;

	// Token: 0x04000128 RID: 296
	public bool isControllable = true;

	// Token: 0x04000129 RID: 297
	public bool isExtraZoom;

	// Token: 0x0400012A RID: 298
	public bool keepAboveSurface = true;

	// Token: 0x0400012B RID: 299
	public bool handleObjectOcclusion = true;

	// Token: 0x0400012C RID: 300
	public Transform cameraTarget;

	// Token: 0x0400012D RID: 301
	public bool reverseYAxis = true;

	// Token: 0x0400012E RID: 302
	public bool reverseXAxis;

	// Token: 0x0400012F RID: 303
	public Vector2 mouseSensitivity = new Vector2(4f, 4f);

	// Token: 0x04000130 RID: 304
	public float cameraFOV = 35f;

	// Token: 0x04000131 RID: 305
	public Vector2 cameraOffset = new Vector2(0f, 0f);

	// Token: 0x04000132 RID: 306
	public float cameraLean;

	// Token: 0x04000133 RID: 307
	public float walkSpeed = 0.02f;

	// Token: 0x04000134 RID: 308
	public float runSpeed = 0.4f;

	// Token: 0x04000135 RID: 309
	public Vector3 rotationLimits = new Vector3(0f, 0f, -30f);

	// Token: 0x04000136 RID: 310
	public float minZoomAmount = 1.25f;

	// Token: 0x04000137 RID: 311
	public float maxZoomAmount = 8f;

	// Token: 0x04000138 RID: 312
	public sui_demo_animBoat targetAnimator;

	// Token: 0x04000139 RID: 313
	private Transform cameraObject;

	// Token: 0x0400013A RID: 314
	private Vector2 axisSensitivity = new Vector2(4f, 4f);

	// Token: 0x0400013B RID: 315
	private float followDistance = 5f;

	// Token: 0x0400013C RID: 316
	private float followHeight = 1f;

	// Token: 0x0400013D RID: 317
	private float followLat;

	// Token: 0x0400013E RID: 318
	private float camFOV = 35f;

	// Token: 0x0400013F RID: 319
	private float camRotation;

	// Token: 0x04000140 RID: 320
	private Vector3 camRot;

	// Token: 0x04000141 RID: 321
	private float camHeight;

	// Token: 0x04000142 RID: 322
	private bool isInWater;

	// Token: 0x04000143 RID: 323
	private bool isInWaterDeep;

	// Token: 0x04000144 RID: 324
	private bool isUnderWater;

	// Token: 0x04000145 RID: 325
	private Vector3 targetPosition;

	// Token: 0x04000146 RID: 326
	private float MouseRotationDistance;

	// Token: 0x04000147 RID: 327
	private float MouseVerticalDistance;

	// Token: 0x04000148 RID: 328
	private GameObject suimonoGameObject;

	// Token: 0x04000149 RID: 329
	private SuimonoModule suimonoModuleObject;

	// Token: 0x0400014A RID: 330
	private float followTgtDistance;

	// Token: 0x0400014B RID: 331
	private Quaternion targetRotation;

	// Token: 0x0400014C RID: 332
	private float MouseScrollDistance;

	// Token: 0x0400014D RID: 333
	private float setFOV = 1f;

	// Token: 0x0400014E RID: 334
	private float useSpeed;

	// Token: 0x0400014F RID: 335
	private float useSideSpeed;

	// Token: 0x04000150 RID: 336
	private float moveSpeed = 0.05f;

	// Token: 0x04000151 RID: 337
	private float moveForward;

	// Token: 0x04000152 RID: 338
	private float moveSideways;

	// Token: 0x04000153 RID: 339
	private bool isRunning;

	// Token: 0x04000154 RID: 340
	private Vector3 savePos;

	// Token: 0x04000155 RID: 341
	private float oldMouseRotation;

	// Token: 0x04000156 RID: 342
	private float xMove;

	// Token: 0x04000157 RID: 343
	private float zMove;

	// Token: 0x04000158 RID: 344
	private sui_demo_ControllerMaster MC;

	// Token: 0x04000159 RID: 345
	private sui_demo_InputController IC;

	// Token: 0x0400015A RID: 346
	private float waterLevel;
}
