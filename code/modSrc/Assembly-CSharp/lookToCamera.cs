using System;
using UnityEngine;


public class lookToCamera : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
	private void FindScripts()
	{
		this.camera_ = GameObject.Find("Camera");
	}

	
	private void Update()
	{
		base.gameObject.transform.rotation = this.camera_.transform.rotation;
		base.gameObject.transform.position = new Vector3(base.transform.position.x, this.camera_.transform.position.z + 5f, base.transform.position.z);
	}

	
	private GameObject camera_;
}
