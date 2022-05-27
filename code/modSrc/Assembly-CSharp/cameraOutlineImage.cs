using System;
using UnityEngine;
using UnityEngine.UI;


public class cameraOutlineImage : MonoBehaviour
{
	
	private void Start()
	{
		this.myImage = base.GetComponent<RawImage>();
	}

	
	private void Update()
	{
		if (this.blink)
		{
			float a = 0.1f + Mathf.PingPong(Time.realtimeSinceStartup * 2f, 1f) * 0.5f;
			this.myImage.color = new Color(this.myImage.color.r, this.myImage.color.g, this.myImage.color.b, a);
		}
	}

	
	private RawImage myImage;

	
	public bool blink = true;
}
