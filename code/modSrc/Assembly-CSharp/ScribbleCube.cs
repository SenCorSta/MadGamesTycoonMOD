using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;


public class ScribbleCube : MonoBehaviour
{
	
	private void Start()
	{
		this.line = new VectorLine("Line", new List<Vector3>(this.numberOfPoints), this.lineTexture, (float)this.lineWidth, LineType.Continuous);
		this.line.material = this.lineMaterial;
		this.line.drawTransform = base.transform;
		this.LineSetup(false);
	}

	
	private void LineSetup(bool resize)
	{
		if (resize)
		{
			this.lineColors = null;
			this.line.Resize(this.numberOfPoints);
		}
		for (int i = 0; i < this.line.points3.Count; i++)
		{
			this.line.points3[i] = new Vector3(UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-5f, 5f));
		}
		this.SetLineColors();
	}

	
	private void SetLineColors()
	{
		if (this.lineColors == null)
		{
			this.lineColors = new List<Color32>(new Color32[this.numberOfPoints - 1]);
		}
		for (int i = 0; i < this.lineColors.Count; i++)
		{
			this.lineColors[i] = Color.Lerp(this.color1, this.color2, (float)i / (float)this.lineColors.Count);
		}
		this.line.SetColors(this.lineColors);
	}

	
	private void LateUpdate()
	{
		this.line.Draw();
	}

	
	private void OnGUI()
	{
		GUI.Label(new Rect(20f, 10f, 250f, 30f), "Zoom with scrollwheel or arrow keys");
		if (GUI.Button(new Rect(20f, 50f, 100f, 30f), "Change colors"))
		{
			int num = UnityEngine.Random.Range(0, 3);
			int num2;
			do
			{
				num2 = UnityEngine.Random.Range(0, 3);
			}
			while (num2 == num);
			this.color1 = this.RandomColor(this.color1, num);
			this.color2 = this.RandomColor(this.color2, num2);
			this.SetLineColors();
		}
		GUI.Label(new Rect(20f, 100f, 150f, 30f), "Number of points: " + this.numberOfPoints);
		this.numberOfPoints = (int)GUI.HorizontalSlider(new Rect(20f, 130f, 120f, 30f), (float)this.numberOfPoints, 50f, 1000f);
		if (GUI.Button(new Rect(160f, 120f, 40f, 30f), "Set"))
		{
			this.LineSetup(true);
		}
	}

	
	private Color RandomColor(Color color, int component)
	{
		for (int i = 0; i < 3; i++)
		{
			if (i == component)
			{
				color[i] = UnityEngine.Random.value * 0.25f;
			}
			else
			{
				color[i] = UnityEngine.Random.value * 0.5f + 0.5f;
			}
		}
		return color;
	}

	
	public Texture lineTexture;

	
	public Material lineMaterial;

	
	public int lineWidth = 14;

	
	private Color color1 = Color.green;

	
	private Color color2 = Color.blue;

	
	private VectorLine line;

	
	private List<Color32> lineColors;

	
	private int numberOfPoints = 350;
}
