using System;
using UnityEngine;


public class fanletterTime : MonoBehaviour
{
	
	private void Start()
	{
	}

	
	public void Init(float f)
	{
		this.anzeigeDauer = f;
		this.timer = 0f;
		this.pause = false;
	}

	
	private void Update()
	{
		this.timer += Time.deltaTime;
		if (!this.pause)
		{
			if (this.timer > 1f)
			{
				this.pause = true;
				this.myAnimation["showFanLetter"].speed = 0f;
				return;
			}
		}
		else if (this.timer > this.anzeigeDauer)
		{
			this.pause = false;
			this.myAnimation["showFanLetter"].speed = 1f;
		}
	}

	
	public float anzeigeDauer = 1f;

	
	public float timer;

	
	public Animation myAnimation;

	
	public bool pause;
}
