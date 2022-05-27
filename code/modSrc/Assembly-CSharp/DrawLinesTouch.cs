using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;


public class DrawLinesTouch : MonoBehaviour
{
	
	private void Start()
	{
		Texture2D texture;
		float width;
		if (this.useEndCap)
		{
			VectorLine.SetEndCap("RoundCap", EndCap.Mirror, new Texture2D[]
			{
				this.capLineTex,
				this.capTex
			});
			texture = this.capLineTex;
			width = this.capLineWidth;
		}
		else
		{
			texture = this.lineTex;
			width = this.lineWidth;
		}
		this.line = new VectorLine("DrawnLine", new List<Vector2>(), texture, width, LineType.Continuous, Joins.Weld);
		this.line.endPointsUpdate = 2;
		if (this.useEndCap)
		{
			this.line.endCap = "RoundCap";
		}
		this.sqrMinPixelMove = this.minPixelMove * this.minPixelMove;
	}

	
	private void Update()
	{
		if (Input.touchCount > 0)
		{
			this.touch = Input.GetTouch(0);
			if (this.touch.phase == TouchPhase.Began)
			{
				this.line.points2.Clear();
				this.line.Draw();
				this.previousPosition = this.touch.position;
				this.line.points2.Add(this.touch.position);
				this.canDraw = true;
				return;
			}
			if (this.touch.phase == TouchPhase.Moved && (this.touch.position - this.previousPosition).sqrMagnitude > (float)this.sqrMinPixelMove && this.canDraw)
			{
				this.previousPosition = this.touch.position;
				this.line.points2.Add(this.touch.position);
				if (this.line.points2.Count >= this.maxPoints)
				{
					this.canDraw = false;
				}
				this.line.Draw();
			}
		}
	}

	
	public Texture2D lineTex;

	
	public int maxPoints = 5000;

	
	public float lineWidth = 4f;

	
	public int minPixelMove = 5;

	
	public bool useEndCap;

	
	public Texture2D capLineTex;

	
	public Texture2D capTex;

	
	public float capLineWidth = 20f;

	
	private VectorLine line;

	
	private Vector2 previousPosition;

	
	private int sqrMinPixelMove;

	
	private bool canDraw;

	
	private Touch touch;
}
