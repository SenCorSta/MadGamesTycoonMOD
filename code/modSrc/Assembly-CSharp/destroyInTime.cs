using System;
using UnityEngine;


public class destroyInTime : MonoBehaviour
{
	
	private void Update()
	{
		this.timer += Time.deltaTime;
		if (this.timer >= this.timeToLife)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	
	public float timeToLife = 3f;

	
	private float timer;
}
