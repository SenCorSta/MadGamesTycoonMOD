using System;
using UnityEngine;


public class randomRotation : MonoBehaviour
{
	
	private void Start()
	{
		if (this.randX)
		{
			base.transform.eulerAngles = new Vector3(UnityEngine.Random.Range(0f, 359f), base.transform.eulerAngles.y, base.transform.eulerAngles.z);
		}
		if (this.randY)
		{
			base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, UnityEngine.Random.Range(0f, 359f), base.transform.eulerAngles.z);
		}
		if (this.randZ)
		{
			base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, base.transform.eulerAngles.y, UnityEngine.Random.Range(0f, 359f));
		}
	}

	
	public bool randX;

	
	public bool randY;

	
	public bool randZ;
}
