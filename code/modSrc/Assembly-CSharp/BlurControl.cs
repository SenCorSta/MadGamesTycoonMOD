using System;
using UnityEngine;


public class BlurControl : MonoBehaviour
{
	
	private void Start()
	{
		this.value = 0f;
		base.transform.GetComponent<Renderer>().material.SetFloat("_blurSizeXY", this.value);
	}

	
	private void Update()
	{
		if (Input.GetButton("Up"))
		{
			this.value += Time.deltaTime;
			if (this.value > 20f)
			{
				this.value = 20f;
			}
			base.transform.GetComponent<Renderer>().material.SetFloat("_blurSizeXY", this.value);
			return;
		}
		if (Input.GetButton("Down"))
		{
			this.value = (this.value - Time.deltaTime) % 20f;
			if (this.value < 0f)
			{
				this.value = 0f;
			}
			base.transform.GetComponent<Renderer>().material.SetFloat("_blurSizeXY", this.value);
		}
	}

	
	private void OnGUI()
	{
		GUI.TextArea(new Rect(10f, 10f, 200f, 50f), "Press the 'Up' and 'Down' arrows \nto interact with the blur plane\nCurrent value: " + this.value);
	}

	
	private float value;
}
