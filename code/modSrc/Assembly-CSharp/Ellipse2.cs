using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;


public class Ellipse2 : MonoBehaviour
{
	
	private void Start()
	{
		List<Vector2> points = new List<Vector2>(this.segments * 2 * this.numberOfEllipses);
		VectorLine vectorLine = new VectorLine("Line", points, this.lineTexture, 3f);
		for (int i = 0; i < this.numberOfEllipses; i++)
		{
			Vector2 v = new Vector2((float)UnityEngine.Random.Range(0, Screen.width), (float)UnityEngine.Random.Range(0, Screen.height));
			vectorLine.MakeEllipse(v, (float)UnityEngine.Random.Range(10, Screen.width / 2), (float)UnityEngine.Random.Range(10, Screen.height / 2), this.segments, i * (this.segments * 2));
		}
		vectorLine.Draw();
	}

	
	public Texture lineTexture;

	
	public int segments = 60;

	
	public int numberOfEllipses = 10;
}
