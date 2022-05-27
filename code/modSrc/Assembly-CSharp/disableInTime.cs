using System;
using UnityEngine;


public class disableInTime : MonoBehaviour
{
	
	private void Update()
	{
		this.timer += Time.deltaTime;
		if (this.timer >= this.timeToLife)
		{
			base.gameObject.SetActive(false);
		}
	}

	
	private void OnEnable()
	{
		this.timer = 0f;
	}

	
	public float timeToLife = 3f;

	
	private float timer;
}
