using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;


public class Ellipse1 : MonoBehaviour
{
	
	private void Start()
	{
		List<Vector2> points = new List<Vector2>(this.segments + 1);
		VectorLine vectorLine = new VectorLine("Line", points, this.lineTexture, 3f, LineType.Continuous);
		vectorLine.MakeEllipse(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), this.xRadius, this.yRadius, this.segments, this.pointRotation);
		vectorLine.Draw();
	}

	
	public Texture lineTexture;

	
	public float xRadius = 120f;

	
	public float yRadius = 120f;

	
	public int segments = 60;

	
	public float pointRotation;
}
