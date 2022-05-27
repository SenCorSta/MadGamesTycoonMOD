using System;
using UnityEngine;


public class CFX_AutoRotate : MonoBehaviour
{
	
	private void Update()
	{
		base.transform.Rotate(this.rotation * Time.deltaTime, this.space);
	}

	
	public Vector3 rotation;

	
	public Space space = Space.Self;
}
