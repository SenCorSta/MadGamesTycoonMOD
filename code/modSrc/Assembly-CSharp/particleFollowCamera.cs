using System;
using UnityEngine;


public class particleFollowCamera : MonoBehaviour
{
	
	private void Start()
	{
		base.transform.SetParent(null);
	}

	
	private void Update()
	{
		base.transform.position = new Vector3(this.cameraObject.transform.position.x, base.transform.position.y, this.cameraObject.transform.position.z);
	}

	
	public GameObject cameraObject;
}
