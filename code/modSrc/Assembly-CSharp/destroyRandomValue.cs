using System;
using UnityEngine;


public class destroyRandomValue : MonoBehaviour
{
	
	private void Start()
	{
		if (UnityEngine.Random.Range(0, 100) < this.rand)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	
	public int rand = 95;
}
