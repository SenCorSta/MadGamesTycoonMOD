using System;
using UnityEngine;


public class Player : MonoBehaviour
{
	
	private void Start()
	{
		this.rb = base.GetComponent<Rigidbody>();
	}

	
	private void FixedUpdate()
	{
		float axis = Input.GetAxis("Horizontal");
		float axis2 = Input.GetAxis("Vertical");
		Vector3 a = new Vector3(axis, 0f, axis2);
		this.rb.AddForce(a * this.speed);
	}

	
	public float speed;

	
	private Rigidbody rb;
}
