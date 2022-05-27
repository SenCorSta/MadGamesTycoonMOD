using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;


public class MakeSpline : MonoBehaviour
{
	
	private void Start()
	{
		List<Vector3> list = new List<Vector3>();
		int num = 1;
		GameObject gameObject = GameObject.Find("Sphere" + num++);
		while (gameObject != null)
		{
			list.Add(gameObject.transform.position);
			gameObject = GameObject.Find("Sphere" + num++);
		}
		if (this.usePoints)
		{
			VectorLine vectorLine = new VectorLine("Spline", new List<Vector3>(this.segments + 1), 2f, LineType.Points);
			vectorLine.MakeSpline(list.ToArray(), this.segments, this.loop);
			vectorLine.Draw();
			return;
		}
		VectorLine vectorLine2 = new VectorLine("Spline", new List<Vector3>(this.segments + 1), 2f, LineType.Continuous);
		vectorLine2.MakeSpline(list.ToArray(), this.segments, this.loop);
		vectorLine2.Draw3D();
	}

	
	public int segments = 250;

	
	public bool loop = true;

	
	public bool usePoints;
}
