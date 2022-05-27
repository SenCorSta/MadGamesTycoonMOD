using System;
using UnityEngine;


public class CameraZoom : MonoBehaviour
{
	
	private void Update()
	{
		base.transform.Translate(Vector3.forward * this.zoomSpeed * Input.GetAxis("Mouse ScrollWheel"));
		base.transform.Translate(Vector3.forward * this.keyZoomSpeed * Time.deltaTime * Input.GetAxis("Vertical"));
	}

	
	public float zoomSpeed = 10f;

	
	public float keyZoomSpeed = 20f;
}
