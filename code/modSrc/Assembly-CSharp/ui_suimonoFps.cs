using System;
using UnityEngine;
using UnityEngine.UI;


public class ui_suimonoFps : MonoBehaviour
{
	
	private void Start()
	{
		base.InvokeRepeating("SetType", 0.1f, 0.5f);
	}

	
	private void LateUpdate()
	{
		if (this.showFPS)
		{
			this.timeleft -= Time.deltaTime;
			this.accum += Time.timeScale / Time.deltaTime;
			this.frames += 1f;
			if (this.timeleft <= 0f)
			{
				this.timeleft = this.updateInterval;
				this.accum = 0f;
				this.frames = 0f;
				return;
			}
		}
		else
		{
			this.textObj_fps.text = "";
		}
	}

	
	private void SetType()
	{
		if (this.textObj_fps != null && this.accum > 0f && this.frames > 0f)
		{
			this.textObj_fps.text = "FPS: " + (this.accum / this.frames).ToString("f0");
		}
	}

	
	public Text textObj_fps;

	
	public bool showFPS = true;

	
	private float updateInterval = 0.5f;

	
	private float accum;

	
	private float frames;

	
	private float timeleft;
}
