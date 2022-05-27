using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;


public class DrawPath : MonoBehaviour
{
	
	private void Start()
	{
		this.pathLine = new VectorLine("Path", new List<Vector3>(), this.lineTex, 12f, LineType.Continuous);
		this.pathLine.color = Color.green;
		this.pathLine.textureScale = 1f;
		this.MakeBall();
		base.StartCoroutine(this.SamplePoints(this.ball.transform));
	}

	
	private void MakeBall()
	{
		if (this.ball)
		{
			UnityEngine.Object.Destroy(this.ball);
		}
		this.ball = UnityEngine.Object.Instantiate<GameObject>(this.ballPrefab, new Vector3(-2.25f, -4.4f, -1.9f), Quaternion.Euler(300f, 70f, 310f));
		this.ball.GetComponent<Rigidbody>().useGravity = true;
		this.ball.GetComponent<Rigidbody>().AddForce(this.ball.transform.forward * this.force, ForceMode.Impulse);
	}

	
	private IEnumerator SamplePoints(Transform thisTransform)
	{
		bool running = true;
		while (running)
		{
			this.pathLine.points3.Add(thisTransform.position);
			int num = this.pathIndex + 1;
			this.pathIndex = num;
			if (num == this.maxPoints)
			{
				running = false;
			}
			yield return new WaitForSeconds(0.05f);
			if (this.continuousUpdate)
			{
				this.pathLine.Draw();
			}
		}
		yield break;
	}

	
	private void OnGUI()
	{
		if (GUI.Button(new Rect(10f, 10f, 100f, 30f), "Reset"))
		{
			this.Reset();
		}
		if (!this.continuousUpdate && GUI.Button(new Rect(10f, 45f, 100f, 30f), "Draw Path"))
		{
			this.pathLine.Draw();
		}
	}

	
	private void Reset()
	{
		base.StopAllCoroutines();
		this.MakeBall();
		this.pathLine.points3.Clear();
		this.pathLine.Draw();
		this.pathIndex = 0;
		base.StartCoroutine(this.SamplePoints(this.ball.transform));
	}

	
	public Texture lineTex;

	
	public Color lineColor = Color.green;

	
	public int maxPoints = 500;

	
	public bool continuousUpdate = true;

	
	public GameObject ballPrefab;

	
	public float force = 16f;

	
	private VectorLine pathLine;

	
	private int pathIndex;

	
	private GameObject ball;
}
