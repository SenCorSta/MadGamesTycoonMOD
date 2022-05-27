using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;


public class DrawCurve : MonoBehaviour
{
	
	private void Start()
	{
		DrawCurve.use = this;
		this.oldWidth = Screen.width;
		this.oldSegments = this.segments;
		List<Vector2> list = new List<Vector2>();
		list.Add(new Vector2((float)Screen.width * 0.25f, (float)Screen.height * 0.25f));
		list.Add(new Vector2((float)Screen.width * 0.125f, (float)Screen.height * 0.5f));
		list.Add(new Vector2((float)Screen.width - (float)Screen.width * 0.25f, (float)Screen.height - (float)Screen.height * 0.25f));
		list.Add(new Vector2((float)Screen.width - (float)Screen.width * 0.125f, (float)Screen.height * 0.5f));
		this.controlLine = new VectorLine("Control Line", list, 2f);
		this.controlLine.color = new Color(0f, 0.75f, 0.1f, 0.6f);
		this.controlLine.Draw();
		this.line = new VectorLine("Curve", new List<Vector2>(this.segments + 1), this.lineTexture, 5f, LineType.Continuous, Joins.Weld);
		this.line.MakeCurve(list[0], list[1], list[2], list[3], this.segments);
		this.line.Draw();
		this.AddControlObjects();
		this.AddControlObjects();
	}

	
	private void SetLine()
	{
		if (this.useDottedLine)
		{
			this.line.texture = this.dottedLineTexture;
			this.line.color = this.dottedLineColor;
			this.line.lineWidth = 8f;
			this.line.textureScale = 1f;
			return;
		}
		this.line.texture = this.lineTexture;
		this.line.color = this.lineColor;
		this.line.lineWidth = 5f;
		this.line.textureScale = 0f;
	}

	
	private void AddControlObjects()
	{
		this.anchorObject = UnityEngine.Object.Instantiate<GameObject>(this.anchorPoint, this.controlLine.points2[this.pointIndex], Quaternion.identity);
		this.anchorObject.transform.SetParent(this.canvas, true);
		CurvePointControl component = this.anchorObject.GetComponent<CurvePointControl>();
		int num = this.pointIndex;
		this.pointIndex = num + 1;
		component.objectNumber = num;
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.controlPoint, this.controlLine.points2[this.pointIndex], Quaternion.identity);
		gameObject.transform.SetParent(this.anchorObject.transform, true);
		CurvePointControl component2 = gameObject.GetComponent<CurvePointControl>();
		num = this.pointIndex;
		this.pointIndex = num + 1;
		component2.objectNumber = num;
	}

	
	public void UpdateLine(int objectNumber, Vector2 pos)
	{
		Vector2 b = this.controlLine.points2[objectNumber];
		this.controlLine.points2[objectNumber] = pos;
		int num = objectNumber / 4;
		int num2 = num * 4;
		this.line.MakeCurve(this.controlLine.points2[num2], this.controlLine.points2[num2 + 1], this.controlLine.points2[num2 + 2], this.controlLine.points2[num2 + 3], this.segments, num * (this.segments + 1));
		if (objectNumber % 2 == 0)
		{
			List<Vector2> points = this.controlLine.points2;
			int index = objectNumber + 1;
			points[index] += pos - b;
			if (objectNumber > 0 && objectNumber < this.controlLine.points2.Count - 2)
			{
				this.controlLine.points2[objectNumber + 2] = pos;
				points = this.controlLine.points2;
				index = objectNumber + 3;
				points[index] += pos - b;
				this.line.MakeCurve(this.controlLine.points2[num2 + 4], this.controlLine.points2[num2 + 5], this.controlLine.points2[num2 + 6], this.controlLine.points2[num2 + 7], this.segments, (num + 1) * (this.segments + 1));
			}
		}
		this.line.Draw();
		this.controlLine.Draw();
	}

	
	private void OnGUI()
	{
		if (GUI.Button(new Rect(20f, 20f, 100f, 30f), "Add Point"))
		{
			this.AddPoint();
		}
		GUI.Label(new Rect(20f, 59f, 200f, 30f), "Curve resolution: " + this.segments);
		this.segments = (int)GUI.HorizontalSlider(new Rect(20f, 80f, 150f, 30f), (float)this.segments, 3f, 60f);
		if (this.oldSegments != this.segments)
		{
			this.oldSegments = this.segments;
			this.ChangeSegments();
		}
		this.useDottedLine = GUI.Toggle(new Rect(20f, 105f, 80f, 20f), this.useDottedLine, " Dotted line");
		if (this.oldDottedLineSetting != this.useDottedLine)
		{
			this.oldDottedLineSetting = this.useDottedLine;
			this.SetLine();
			this.line.Draw();
		}
		GUILayout.BeginArea(new Rect(20f, 150f, 150f, 800f));
		if (GUILayout.Button(this.listPoints ? "Hide points" : "List points", new GUILayoutOption[]
		{
			GUILayout.Width(100f)
		}))
		{
			this.listPoints = !this.listPoints;
		}
		if (this.listPoints)
		{
			int num = 0;
			for (int i = 0; i < this.controlLine.points2.Count; i += 2)
			{
				GUILayout.Label(string.Concat(new object[]
				{
					"Anchor ",
					num,
					": (",
					(int)this.controlLine.points2[i].x,
					", ",
					(int)this.controlLine.points2[i].y,
					")"
				}), Array.Empty<GUILayoutOption>());
				GUILayout.Label(string.Concat(new object[]
				{
					"Control ",
					num++,
					": (",
					(int)this.controlLine.points2[i + 1].x,
					", ",
					(int)this.controlLine.points2[i + 1].y,
					")"
				}), Array.Empty<GUILayoutOption>());
			}
		}
		GUILayout.EndArea();
	}

	
	private void AddPoint()
	{
		if (this.line.points2.Count + this.controlLine.points2.Count + this.segments + 4 > 16383)
		{
			return;
		}
		this.controlLine.points2.Add(this.controlLine.points2[this.pointIndex - 2]);
		this.controlLine.points2.Add(this.controlLine.points2[this.pointIndex - 1]);
		Vector2 b = (this.controlLine.points2[this.pointIndex - 2] - this.controlLine.points2[this.pointIndex - 4]) * 0.25f;
		this.controlLine.points2.Add(this.controlLine.points2[this.pointIndex - 2] + b);
		this.controlLine.points2.Add(this.controlLine.points2[this.pointIndex - 1] + b);
		if (this.controlLine.points2[this.pointIndex + 2].x > (float)Screen.width || this.controlLine.points2[this.pointIndex + 2].y > (float)Screen.height || this.controlLine.points2[this.pointIndex + 2].x < 0f || this.controlLine.points2[this.pointIndex + 2].y < 0f)
		{
			this.controlLine.points2[this.pointIndex + 2] = this.controlLine.points2[this.pointIndex - 2] - b;
			this.controlLine.points2[this.pointIndex + 3] = this.controlLine.points2[this.pointIndex - 1] - b;
		}
		Vector2 vector = this.controlLine.points2[this.pointIndex - 1] + (this.controlLine.points2[this.pointIndex] - this.controlLine.points2[this.pointIndex - 1]) * 2f;
		this.pointIndex++;
		this.controlLine.points2[this.pointIndex] = vector;
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.controlPoint, vector, Quaternion.identity);
		gameObject.transform.SetParent(this.anchorObject.transform, true);
		CurvePointControl component = gameObject.GetComponent<CurvePointControl>();
		int num = this.pointIndex;
		this.pointIndex = num + 1;
		component.objectNumber = num;
		this.AddControlObjects();
		this.controlLine.Draw();
		VectorLine vectorLine = this.line;
		int num2 = this.segments + 1;
		num = this.numberOfCurves + 1;
		this.numberOfCurves = num;
		vectorLine.Resize(num2 * num);
		this.line.MakeCurve(this.controlLine.points2[this.pointIndex - 4], this.controlLine.points2[this.pointIndex - 3], this.controlLine.points2[this.pointIndex - 2], this.controlLine.points2[this.pointIndex - 1], this.segments, (this.segments + 1) * (this.numberOfCurves - 1));
		this.line.Draw();
	}

	
	private void ChangeSegments()
	{
		if (this.segments * 4 * this.numberOfCurves > 65534)
		{
			return;
		}
		this.line.Resize((this.segments + 1) * this.numberOfCurves);
		for (int i = 0; i < this.numberOfCurves; i++)
		{
			this.line.MakeCurve(this.controlLine.points2[i * 4], this.controlLine.points2[i * 4 + 1], this.controlLine.points2[i * 4 + 2], this.controlLine.points2[i * 4 + 3], this.segments, (this.segments + 1) * i);
		}
		this.line.Draw();
	}

	
	private void Update()
	{
		if (Screen.width != this.oldWidth)
		{
			this.oldWidth = Screen.width;
			this.ChangeResolution();
		}
	}

	
	private void ChangeResolution()
	{
		foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("GameController"))
		{
			gameObject.transform.position = this.controlLine.points2[gameObject.GetComponent<CurvePointControl>().objectNumber];
		}
	}

	
	public Texture lineTexture;

	
	public Color lineColor = Color.white;

	
	public Texture dottedLineTexture;

	
	public Color dottedLineColor = Color.yellow;

	
	public int segments = 60;

	
	public GameObject anchorPoint;

	
	public GameObject controlPoint;

	
	public Transform canvas;

	
	private int numberOfCurves = 1;

	
	private VectorLine line;

	
	private VectorLine controlLine;

	
	private int pointIndex;

	
	private GameObject anchorObject;

	
	private int oldWidth;

	
	private bool useDottedLine;

	
	private bool oldDottedLineSetting;

	
	private int oldSegments;

	
	private bool listPoints;

	
	public static DrawCurve use;
}
