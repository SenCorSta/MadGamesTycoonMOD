using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;


public class CreateHills : MonoBehaviour
{
	
	private void Start()
	{
		this.storedPosition = this.ball.transform.position;
		this.splinePoints = new Vector2[this.numberOfHills * 2 + 1];
		this.hills = new VectorLine("Hills", new List<Vector2>(this.numberOfPoints), this.hillTexture, 12f, LineType.Continuous, Joins.Weld);
		this.hills.useViewportCoords = true;
		this.hills.collider = true;
		this.hills.physicsMaterial = this.hillPhysicsMaterial;
		UnityEngine.Random.InitState(95);
		this.CreateHillLine();
	}

	
	private void OnGUI()
	{
		if (GUI.Button(new Rect(10f, 10f, 150f, 40f), "Make new hills"))
		{
			this.CreateHillLine();
			this.ball.transform.position = this.storedPosition;
			this.ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			this.ball.GetComponent<Rigidbody2D>().WakeUp();
		}
	}

	
	private void CreateHillLine()
	{
		this.splinePoints[0] = new Vector2(-0.02f, UnityEngine.Random.Range(0.1f, 0.6f));
		float num = 0f;
		float num2 = 1f / (float)(this.numberOfHills * 2);
		int i;
		for (i = 1; i < this.splinePoints.Length; i += 2)
		{
			num += num2;
			this.splinePoints[i] = new Vector2(num, UnityEngine.Random.Range(0.3f, 0.7f));
			num += num2;
			this.splinePoints[i + 1] = new Vector2(num, UnityEngine.Random.Range(0.1f, 0.6f));
		}
		this.splinePoints[i - 1] = new Vector2(1.02f, UnityEngine.Random.Range(0.1f, 0.6f));
		this.hills.MakeSpline(this.splinePoints);
		this.hills.Draw();
	}

	
	public Texture hillTexture;

	
	public PhysicsMaterial2D hillPhysicsMaterial;

	
	public int numberOfPoints = 100;

	
	public int numberOfHills = 4;

	
	public GameObject ball;

	
	private Vector3 storedPosition;

	
	private VectorLine hills;

	
	private Vector2[] splinePoints;
}
