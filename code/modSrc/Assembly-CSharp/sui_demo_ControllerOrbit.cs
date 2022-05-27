using System;
using Suimono.Core;
using UnityEngine;


public class sui_demo_ControllerOrbit : MonoBehaviour
{
	
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

	
	public bool isActive;

	
	public bool isControllable = true;

	
	public bool isExtraZoom;

	
	public Transform cameraTarget;

	
	public bool reverseYAxis = true;

	
	public bool reverseXAxis;

	
	public Vector2 mouseSensitivity = new Vector2(4f, 4f);

	
	public float cameraFOV = 35f;

	
	public Vector3 cameraOffset = new Vector3(0f, 0f, 0f);

	
	public float cameraLean;

	
	public float rotationSensitivity = 6f;

	
	public Vector3 rotationLimits = new Vector3(0f, 0f, 0f);

	
	public float minZoomAmount = 1.25f;

	
	public float maxZoomAmount = 8f;

	
	public bool showDebug;

	
	public sui_demo_animCharacter targetAnimator;

	
	private Transform cameraObject;

	
	private Vector2 axisSensitivity = new Vector2(4f, 4f);

	
	private float followDistance = 10f;

	
	private float followHeight = 1f;

	
	private float followLat;

	
	private float camFOV = 35f;

	
	private float camRotation;

	
	private Vector3 camRot;

	
	private float camHeight = 4f;

	
	private bool isInWater;

	
	private bool isInWaterDeep;

	
	private bool isUnderWater;

	
	private Vector3 targetPosition;

	
	private float MouseRotationDistance;

	
	private float MouseVerticalDistance;

	
	private GameObject suimonoGameObject;

	
	private SuimonoModule suimonoModuleObject;

	
	private float followTgtDistance;

	
	private bool orbitView;

	
	private Quaternion targetRotation;

	
	private float MouseScrollDistance;

	
	private float setFOV = 1f;

	
	private float moveForward;

	
	private float moveSideways;

	
	private bool isWalking;

	
	private bool isRunning;

	
	private bool isSprinting;

	
	private Vector3 savePos;

	
	private float oldMouseRotation;

	
	private sui_demo_ControllerMaster MC;

	
	private sui_demo_InputController IC;

	
	private float runButtonTime;

	
	private bool toggleRun;

	
	private float gSlope;

	
	private float useSlope;
}
