using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;


public class AnimatePartialLine : MonoBehaviour
{
	
	private void Start()
	{
		this.startIndex = (float)(-(float)this.visibleLineSegments);
		this.endIndex = 0f;
		List<Vector2> points = new List<Vector2>(this.segments + 1);
		this.line = new VectorLine("Spline", points, this.lineTexture, 30f, LineType.Continuous, Joins.Weld);
		int num = Screen.width / 5;
		int num2 = Screen.height / 3;
		this.line.MakeSpline(new Vector2[]
		{
			new Vector2((float)num, (float)num2),
			new Vector2((float)(num * 2), (float)(num2 * 2)),
			new Vector2((float)(num * 3), (float)(num2 * 2)),
			new Vector2((float)(num * 4), (float)num2)
		});
	}

	
	private void Update()
	{
		this.startIndex += Time.deltaTime * this.speed;
		this.endIndex += Time.deltaTime * this.speed;
		if (this.startIndex >= (float)(this.segments + 1))
		{
			this.startIndex = (float)(-(float)this.visibleLineSegments);
			this.endIndex = 0f;
		}
		else if (this.startIndex < (float)(-(float)this.visibleLineSegments))
		{
			this.startIndex = (float)this.segments;
			this.endIndex = (float)(this.segments + this.visibleLineSegments);
		}
		this.line.drawStart = (int)this.startIndex;
		this.line.drawEnd = (int)this.endIndex;
		this.line.Draw();
	}

	
	public Texture lineTexture;

	
	public int segments = 60;

	
	public int visibleLineSegments = 20;

	
	public float speed = 60f;

	
	private float startIndex;

	
	private float endIndex;

	
	private VectorLine line;
}
