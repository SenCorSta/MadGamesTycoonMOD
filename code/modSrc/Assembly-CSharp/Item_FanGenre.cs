using System;
using UnityEngine;


public class Item_FanGenre : MonoBehaviour
{
	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
