using System;
using UnityEngine;


public class RotateViewpoint : MonoBehaviour
{
	
	private void Update()
	{
		base.transform.RotateAround(Vector3.zero, Vector3.right, this.rotateSpeed * Time.deltaTime);
	}

	
	public float rotateSpeed = 5f;
}
