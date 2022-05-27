using System;
using UnityEngine;


public class textChat : MonoBehaviour
{
	
	private void Start()
	{
	}

	
	private void Update()
	{
		this.timer += Time.deltaTime;
		if (this.timer > 30f)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	
	private float timer;
}
