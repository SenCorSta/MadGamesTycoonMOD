using System;
using UnityEngine;

// Token: 0x02000325 RID: 805
public class cameraMovementScript : MonoBehaviour
{
	// Token: 0x06001C70 RID: 7280 RVA: 0x000137DE File Offset: 0x000119DE
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001C71 RID: 7281 RVA: 0x0011EFEC File Offset: 0x0011D1EC
	private void FindScripts()
	{
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.settings_)
		{
			this.settings_ = GameObject.FindGameObjectWithTag("Main").GetComponent<settingsScript>();
		}
	}

	// Token: 0x06001C72 RID: 7282 RVA: 0x000137E6 File Offset: 0x000119E6
	public void FindCameraLimits()
	{
		this.cameraLimitA = GameObject.Find("CameraLimitA");
		if (!this.cameraLimitA)
		{
			return;
		}
		this.cameraLimitB = GameObject.Find("CameraLimitB");
		this.cameraLimitB;
	}

	// Token: 0x06001C73 RID: 7283 RVA: 0x00013822 File Offset: 0x00011A22
	private void Update()
	{
		this.CameraInput();
	}

	// Token: 0x06001C74 RID: 7284 RVA: 0x0011F040 File Offset: 0x0011D240
	private void CameraInput()
	{
		if (this.disableMovement || this.guiMain_.selectInputField)
		{
			return;
		}
		float d = this.movementSpeed * base.gameObject.transform.GetChild(0).transform.position.y / 9f;
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || (Input.mousePosition.x <= 1f && this.settings_.scrollScreen))
		{
			base.transform.Translate(Vector3.left * d * Time.smoothDeltaTime);
		}
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || (Input.mousePosition.x >= (float)(Screen.width - 1) && this.settings_.scrollScreen))
		{
			base.transform.Translate(-Vector3.left * d * Time.smoothDeltaTime);
		}
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || (Input.mousePosition.y >= (float)(Screen.height - 1) && this.settings_.scrollScreen))
		{
			base.transform.Translate(Vector3.forward * d * Time.smoothDeltaTime);
		}
		if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || (Input.mousePosition.y <= 1f && this.settings_.scrollScreen))
		{
			base.transform.Translate(-Vector3.forward * d * Time.smoothDeltaTime);
		}
		if (Input.GetMouseButton(1))
		{
			base.transform.Translate(-Vector3.forward * (Input.mousePosition.y - this.lastMousePosition.y) * this.movementSpeedWithMouse);
			base.transform.Translate(Vector3.left * (Input.mousePosition.x - this.lastMousePosition.x) * this.movementSpeedWithMouse);
		}
		if (this.cameraLimitA && this.cameraLimitB)
		{
			if (base.gameObject.transform.position.x < this.cameraLimitA.transform.position.x)
			{
				base.gameObject.transform.position = new Vector3(this.cameraLimitA.transform.position.x, base.gameObject.transform.position.y, base.gameObject.transform.position.z);
			}
			if (base.gameObject.transform.position.z < this.cameraLimitA.transform.position.z)
			{
				base.gameObject.transform.position = new Vector3(base.gameObject.transform.position.x, base.gameObject.transform.position.y, this.cameraLimitA.transform.position.z);
			}
			if (base.gameObject.transform.position.x > this.cameraLimitB.transform.position.x)
			{
				base.gameObject.transform.position = new Vector3(this.cameraLimitB.transform.position.x, base.gameObject.transform.position.y, base.gameObject.transform.position.z);
			}
			if (base.gameObject.transform.position.z > this.cameraLimitB.transform.position.z)
			{
				base.gameObject.transform.position = new Vector3(base.gameObject.transform.position.x, base.gameObject.transform.position.y, this.cameraLimitB.transform.position.z);
			}
		}
		if (!this.settings_.camera90GradRotation)
		{
			if (Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.E))
			{
				base.transform.Rotate(0f, -this.rotationSpeed * Time.smoothDeltaTime, 0f, Space.Self);
			}
			if (Input.GetKey(KeyCode.Y) || Input.GetKey(KeyCode.Q))
			{
				base.transform.Rotate(0f, this.rotationSpeed * Time.smoothDeltaTime, 0f, Space.Self);
			}
		}
		else
		{
			if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.E))
			{
				float num = base.transform.eulerAngles.y - 90f;
				num /= 90f;
				num = (float)(Mathf.RoundToInt(num) * 90);
				this.rotation90Grad = num;
			}
			if (Input.GetKeyDown(KeyCode.Y) || Input.GetKeyDown(KeyCode.Q))
			{
				float num2 = base.transform.eulerAngles.y + 90f;
				num2 /= 90f;
				num2 = (float)(Mathf.RoundToInt(num2) * 90);
				this.rotation90Grad = num2;
			}
			base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, Mathf.LerpAngle(base.transform.eulerAngles.y, this.rotation90Grad, 0.2f), base.transform.eulerAngles.z);
		}
		if (Input.GetMouseButton(2))
		{
			base.transform.Rotate(0f, (Input.mousePosition.x - this.lastMousePosition.x) * this.rotationSpeedWithMouse, 0f, Space.Self);
			this.rotation90Grad = base.transform.eulerAngles.y;
		}
		this.lastMousePosition = Input.mousePosition;
	}

	// Token: 0x040023B8 RID: 9144
	private GameObject cameraLimitA;

	// Token: 0x040023B9 RID: 9145
	private GameObject cameraLimitB;

	// Token: 0x040023BA RID: 9146
	public bool disableMovement;

	// Token: 0x040023BB RID: 9147
	public float movementSpeed = 1f;

	// Token: 0x040023BC RID: 9148
	public float movementSpeedWithMouse = 0.01f;

	// Token: 0x040023BD RID: 9149
	public float rotationSpeed = 1f;

	// Token: 0x040023BE RID: 9150
	public float rotationSpeedWithMouse = 0.3f;

	// Token: 0x040023BF RID: 9151
	private Vector3 lastMousePosition;

	// Token: 0x040023C0 RID: 9152
	public float rotation90Grad;

	// Token: 0x040023C1 RID: 9153
	public GUI_Main guiMain_;

	// Token: 0x040023C2 RID: 9154
	public settingsScript settings_;
}
