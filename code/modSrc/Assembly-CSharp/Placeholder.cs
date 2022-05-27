using System;
using UnityEngine;


public class Placeholder : MonoBehaviour
{
	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
