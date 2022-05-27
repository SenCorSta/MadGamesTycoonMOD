using System;
using UnityEngine;


public class Item_GenreFit : MonoBehaviour
{
	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
