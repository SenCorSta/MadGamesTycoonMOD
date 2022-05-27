using System;
using UnityEngine;


public class U10PS_MouseRotation : MonoBehaviour
{
	
	private void Start()
	{
		if (this.lockMouse)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}

	
	private void Update()
	{
		this.yValue = base.transform.eulerAngles.y + this.sensitivity * Input.GetAxis("Mouse X");
		this.zValue = base.transform.eulerAngles.z + this.sensitivity * Input.GetAxis("Mouse Y");
		base.transform.eulerAngles = new Vector3(0f, Mathf.Clamp((this.yValue > 180f) ? (this.yValue - 360f) : this.yValue, -50f, 50f), (!this.lockZRot) ? Mathf.Clamp((this.zValue > 180f) ? (this.zValue - 360f) : this.zValue, -20f, 20f) : 0f);
	}

	
	public float sensitivity;

	
	public bool lockZRot;

	
	public bool lockMouse;

	
	private float yValue;

	
	private float zValue;
}
