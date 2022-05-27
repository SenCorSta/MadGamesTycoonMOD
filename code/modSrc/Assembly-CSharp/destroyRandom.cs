using System;
using UnityEngine;


public class destroyRandom : MonoBehaviour
{
	
	private void Start()
	{
		if (UnityEngine.Random.Range(0, 100) > 50)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
