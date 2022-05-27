using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;


public class MaskLine1 : MonoBehaviour
{
	
	private void Start()
	{
		this.rectLine = new VectorLine("Rects", new List<Vector3>(this.numberOfRects * 8), 2f);
		int num = 0;
		for (int i = 0; i < this.numberOfRects; i++)
		{
			this.rectLine.MakeRect(new Rect(UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(0.25f, 3f), UnityEngine.Random.Range(0.25f, 2f)), num);
			num += 8;
		}
		this.rectLine.color = this.lineColor;
		this.rectLine.capLength = 1f;
		this.rectLine.drawTransform = base.transform;
		this.rectLine.SetMask(this.mask);
		this.startPos = base.transform.position;
	}

	
	private void Update()
	{
		this.t = Mathf.Repeat(this.t + Time.deltaTime * this.moveSpeed, 360f);
		base.transform.position = new Vector2(this.startPos.x + Mathf.Sin(this.t) * 1.5f, this.startPos.y + Mathf.Cos(this.t) * 1.5f);
		this.rectLine.Draw();
	}

	
	public int numberOfRects = 30;

	
	public Color lineColor = Color.green;

	
	public GameObject mask;

	
	public float moveSpeed = 2f;

	
	private VectorLine rectLine;

	
	private float t;

	
	private Vector3 startPos;
}
