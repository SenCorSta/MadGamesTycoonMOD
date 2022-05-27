using System;
using UnityEngine;

// Token: 0x02000328 RID: 808
public class cameraMovementScript : MonoBehaviour
{
	// Token: 0x06001CBA RID: 7354 RVA: 0x0011CE20 File Offset: 0x0011B020
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001CBB RID: 7355 RVA: 0x0011CE28 File Offset: 0x0011B028
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

	// Token: 0x06001CBC RID: 7356 RVA: 0x0011CE79 File Offset: 0x0011B079
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

	// Token: 0x06001CBD RID: 7357 RVA: 0x0011CEB5 File Offset: 0x0011B0B5
	private void Update()
	{
		this.CameraInput();
	}

	// Token: 0x06001CBE RID: 7358 RVA: 0x0011CEC0 File Offset: 0x0011B0C0
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

	// Token: 0x040023D2 RID: 9170
	private GameObject cameraLimitA;

	// Token: 0x040023D3 RID: 9171
	private GameObject cameraLimitB;

	// Token: 0x040023D4 RID: 9172
	public bool disableMovement;

	// Token: 0x040023D5 RID: 9173
	public float movementSpeed = 1f;

	// Token: 0x040023D6 RID: 9174
	public float movementSpeedWithMouse = 0.01f;

	// Token: 0x040023D7 RID: 9175
	public float rotationSpeed = 1f;

	// Token: 0x040023D8 RID: 9176
	public float rotationSpeedWithMouse = 0.3f;

	// Token: 0x040023D9 RID: 9177
	private Vector3 lastMousePosition;

	// Token: 0x040023DA RID: 9178
	public float rotation90Grad;

	// Token: 0x040023DB RID: 9179
	public GUI_Main guiMain_;

	// Token: 0x040023DC RID: 9180
	public settingsScript settings_;
}
