using System;
using UnityEngine;


public class CFX_Demo_RandomDirectionTranslate : MonoBehaviour
{
	
	private void Start()
	{
		this.dir = new Vector3(UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f)).normalized;
		this.dir.Scale(this.axis);
		this.dir += this.baseDir;
	}

	
	private void Update()
	{
		base.transform.Translate(this.dir * this.speed * Time.deltaTime);
		if (this.gravity)
		{
			base.transform.Translate(Physics.gravity * Time.deltaTime);
		}
	}

	
	public float speed = 30f;

	
	public Vector3 baseDir = Vector3.zero;

	
	public Vector3 axis = Vector3.forward;

	
	public bool gravity;

	
	private Vector3 dir;
}
