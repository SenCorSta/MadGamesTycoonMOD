using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;


public class Line : MonoBehaviour
{
	
	private void Start()
	{
		new VectorLine("Line", new List<Vector2>
		{
			new Vector2(0f, (float)UnityEngine.Random.Range(0, Screen.height)),
			new Vector2((float)(Screen.width - 1), (float)UnityEngine.Random.Range(0, Screen.height))
		}, 2f).Draw();
	}
}
