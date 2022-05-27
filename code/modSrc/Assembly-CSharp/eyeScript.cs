using System;
using UnityEngine;


public class eyeScript : MonoBehaviour
{
	
	private void Start()
	{
		this.myCamera = GameObject.Find("Camera");
		this.myAnimation = base.GetComponent<Animation>();
	}

	
	private void Update()
	{
		this.timer += Time.deltaTime;
		if (this.timer < 5f)
		{
			return;
		}
		this.timer = 0f;
		if (this.myCamera.transform.localPosition.z < -9.5f)
		{
			this.myAnimation.Stop();
			Debug.Log("STOP");
			return;
		}
		this.myAnimation.Play();
		Debug.Log("PLAY");
	}

	
	public float timer;

	
	public GameObject myCamera;

	
	public Animation myAnimation;
}
