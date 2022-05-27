using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;


public class DrawGrid : MonoBehaviour
{
	
	private void Start()
	{
		this.gridLine = new VectorLine("Grid", new List<Vector2>(), 1f);
		this.gridLine.alignOddWidthToPixels = true;
		this.MakeGrid();
	}

	
	private void OnGUI()
	{
		GUI.Label(new Rect(10f, 10f, 30f, 20f), this.gridPixels.ToString());
		this.gridPixels = (int)GUI.HorizontalSlider(new Rect(40f, 15f, 590f, 20f), (float)this.gridPixels, 5f, 200f);
		if (GUI.changed)
		{
			this.MakeGrid();
		}
	}

	
	private void MakeGrid()
	{
		int newCount = (Screen.width / this.gridPixels + 1 + (Screen.height / this.gridPixels + 1)) * 2;
		this.gridLine.Resize(newCount);
		int num = 0;
		for (int i = 0; i < Screen.width; i += this.gridPixels)
		{
			this.gridLine.points2[num++] = new Vector2((float)i, 0f);
			this.gridLine.points2[num++] = new Vector2((float)i, (float)(Screen.height - 1));
		}
		for (int j = 0; j < Screen.height; j += this.gridPixels)
		{
			this.gridLine.points2[num++] = new Vector2(0f, (float)j);
			this.gridLine.points2[num++] = new Vector2((float)(Screen.width - 1), (float)j);
		}
		this.gridLine.Draw();
	}

	
	public int gridPixels = 50;

	
	private VectorLine gridLine;
}
