using System;
using UnityEngine;


public class golfMainObject : MonoBehaviour
{
	
	private void Start()
	{
	}

	
	public void RandomRotation()
	{
		if (this.mainObject)
		{
			this.mainObject.transform.localEulerAngles = new Vector3(0f, UnityEngine.Random.Range(-13f, 6f), 0f);
			this.audio_.Play();
		}
	}

	
	public GameObject mainObject;

	
	public AudioSource audio_;
}
