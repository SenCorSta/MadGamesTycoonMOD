using System;
using UnityEngine;

// Token: 0x0200001D RID: 29
public class U10PS_MouseRotation : MonoBehaviour
{
	// Token: 0x06000080 RID: 128 RVA: 0x00004781 File Offset: 0x00002981
	private void Start()
	{
		if (this.lockMouse)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}

	// Token: 0x06000081 RID: 129 RVA: 0x00004798 File Offset: 0x00002998
	private void Update()
	{
		this.yValue = base.transform.eulerAngles.y + this.sensitivity * Input.GetAxis("Mouse X");
		this.zValue = base.transform.eulerAngles.z + this.sensitivity * Input.GetAxis("Mouse Y");
		base.transform.eulerAngles = new Vector3(0f, Mathf.Clamp((this.yValue > 180f) ? (this.yValue - 360f) : this.yValue, -50f, 50f), (!this.lockZRot) ? Mathf.Clamp((this.zValue > 180f) ? (this.zValue - 360f) : this.zValue, -20f, 20f) : 0f);
	}

	// Token: 0x04000091 RID: 145
	public float sensitivity;

	// Token: 0x04000092 RID: 146
	public bool lockZRot;

	// Token: 0x04000093 RID: 147
	public bool lockMouse;

	// Token: 0x04000094 RID: 148
	private float yValue;

	// Token: 0x04000095 RID: 149
	private float zValue;
}
