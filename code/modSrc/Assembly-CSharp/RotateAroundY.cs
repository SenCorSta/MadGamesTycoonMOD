using System;
using UnityEngine;


public class RotateAroundY : MonoBehaviour
{
	
	private void Update()
	{
		base.transform.Rotate(Vector3.up * Time.deltaTime * this.rotateSpeed);
	}

	
	public float rotateSpeed = 10f;
}
