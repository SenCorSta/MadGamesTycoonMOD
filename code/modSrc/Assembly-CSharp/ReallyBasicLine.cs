using System;
using UnityEngine;
using Vectrosity;


public class ReallyBasicLine : MonoBehaviour
{
	
	private void Start()
	{
		VectorLine.SetLine(Color.white, new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2((float)(Screen.width - 1), (float)(Screen.height - 1))
		});
	}
}
