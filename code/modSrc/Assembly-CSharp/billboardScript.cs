using System;
using UnityEngine;


public class billboardScript : MonoBehaviour
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
		base.gameObject.transform.LookAt(this.camera_.transform);
	}

	
	private GameObject camera_;
}
