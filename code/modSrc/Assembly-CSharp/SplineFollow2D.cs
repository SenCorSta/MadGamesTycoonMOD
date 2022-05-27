﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;


public class SplineFollow2D : MonoBehaviour
{
	
	private IEnumerator Start()
	{
		List<Vector2> list = new List<Vector2>();
		int num = 1;
		GameObject gameObject = GameObject.Find("Sphere" + num++);
		while (gameObject != null)
		{
			list.Add(Camera.main.WorldToScreenPoint(gameObject.transform.position));
			gameObject = GameObject.Find("Sphere" + num++);
		}
		VectorLine line = new VectorLine("Spline", new List<Vector2>(this.segments + 1), 2f, LineType.Continuous);
		line.MakeSpline(list.ToArray(), this.segments, this.loop);
		line.Draw();
		do
		{
			for (float dist = 0f; dist < 1f; dist += Time.deltaTime * this.speed)
			{
				Vector2 point = line.GetPoint01(dist);
				this.cube.position = Camera.main.ScreenToWorldPoint(new Vector3(point.x, point.y, 10f));
				yield return null;
			}
		}
		while (this.loop);
		yield break;
	}

	
	public int segments = 250;

	
	public bool loop = true;

	
	public Transform cube;

	
	public float speed = 0.05f;
}
