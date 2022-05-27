using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;


public class DrawLinesMouse : MonoBehaviour
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
		if (this.line3D)
		{
			this.line = new VectorLine("DrawnLine3D", new List<Vector3>(), texture, width, LineType.Continuous, Joins.Weld);
		}
		else
		{
			this.line = new VectorLine("DrawnLine", new List<Vector2>(), texture, width, LineType.Continuous, Joins.Weld);
		}
		this.line.endPointsUpdate = 2;
		if (this.useEndCap)
		{
			this.line.endCap = "RoundCap";
		}
		this.sqrMinPixelMove = this.minPixelMove * this.minPixelMove;
	}

	
	private void Update()
	{
		Vector3 mousePos = this.GetMousePos();
		if (Input.GetMouseButtonDown(0))
		{
			if (this.line3D)
			{
				this.line.points3.Clear();
				this.line.Draw3D();
			}
			else
			{
				this.line.points2.Clear();
				this.line.Draw();
			}
			this.previousPosition = Input.mousePosition;
			if (this.line3D)
			{
				this.line.points3.Add(mousePos);
			}
			else
			{
				this.line.points2.Add(mousePos);
			}
			this.canDraw = true;
			return;
		}
		if (Input.GetMouseButton(0) && (Input.mousePosition - this.previousPosition).sqrMagnitude > (float)this.sqrMinPixelMove && this.canDraw)
		{
			this.previousPosition = Input.mousePosition;
			int count;
			if (this.line3D)
			{
				this.line.points3.Add(mousePos);
				count = this.line.points3.Count;
				this.line.Draw3D();
			}
			else
			{
				this.line.points2.Add(mousePos);
				count = this.line.points2.Count;
				this.line.Draw();
			}
			if (count >= this.maxPoints)
			{
				this.canDraw = false;
			}
		}
	}

	
	private Vector3 GetMousePos()
	{
		Vector3 mousePosition = Input.mousePosition;
		if (this.line3D)
		{
			mousePosition.z = this.distanceFromCamera;
			return Camera.main.ScreenToWorldPoint(mousePosition);
		}
		return mousePosition;
	}

	
	public Texture2D lineTex;

	
	public int maxPoints = 5000;

	
	public float lineWidth = 4f;

	
	public int minPixelMove = 5;

	
	public bool useEndCap;

	
	public Texture2D capLineTex;

	
	public Texture2D capTex;

	
	public float capLineWidth = 20f;

	
	public bool line3D;

	
	public float distanceFromCamera = 1f;

	
	private VectorLine line;

	
	private Vector3 previousPosition;

	
	private int sqrMinPixelMove;

	
	private bool canDraw;
}
