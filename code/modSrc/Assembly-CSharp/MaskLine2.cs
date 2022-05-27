using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;


public class MaskLine2 : MonoBehaviour
{
	
	private void Start()
	{
		this.spikeLine = new VectorLine("SpikeLine", new List<Vector3>(this.numberOfPoints), 2f, LineType.Continuous);
		float num = this.lineHeight / 2f;
		for (int i = 0; i < this.numberOfPoints; i++)
		{
			this.spikeLine.points3[i] = new Vector2(UnityEngine.Random.Range(-this.lineWidth / 2f, this.lineWidth / 2f), num);
			num -= this.lineHeight / (float)this.numberOfPoints;
		}
		this.spikeLine.color = this.lineColor;
		this.spikeLine.drawTransform = base.transform;
		this.spikeLine.SetMask(this.mask);
		this.startPos = base.transform.position;
	}

	
	private void Update()
	{
		this.t = Mathf.Repeat(this.t + Time.deltaTime, 360f);
		base.transform.position = new Vector2(this.startPos.x, this.startPos.y + Mathf.Cos(this.t) * 4f);
		this.spikeLine.Draw();
	}

	
	public int numberOfPoints = 100;

	
	public Color lineColor = Color.yellow;

	
	public GameObject mask;

	
	public float lineWidth = 9f;

	
	public float lineHeight = 17f;

	
	private VectorLine spikeLine;

	
	private float t;

	
	private Vector3 startPos;
}
