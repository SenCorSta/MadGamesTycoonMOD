using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;


public class SelectLine : MonoBehaviour
{
	
	private void Start()
	{
		this.lines = new VectorLine[this.numberOfLines];
		this.wasSelected = new bool[this.numberOfLines];
		for (int i = 0; i < this.numberOfLines; i++)
		{
			this.lines[i] = new VectorLine("SelectLine", new List<Vector2>(5), this.lineThickness, LineType.Continuous, Joins.Fill);
			this.SetPoints(i);
		}
	}

	
	private void SetPoints(int i)
	{
		for (int j = 0; j < this.lines[i].points2.Count; j++)
		{
			this.lines[i].points2[j] = new Vector2((float)UnityEngine.Random.Range(0, Screen.width), (float)UnityEngine.Random.Range(0, Screen.height - 20));
		}
		this.lines[i].Draw();
	}

	
	private void Update()
	{
		for (int i = 0; i < this.numberOfLines; i++)
		{
			int num;
			if (this.lines[i].Selected(Input.mousePosition, this.extraThickness, out num))
			{
				if (!this.wasSelected[i])
				{
					this.lines[i].SetColor(Color.green);
					this.wasSelected[i] = true;
				}
				if (Input.GetMouseButtonDown(0))
				{
					this.SetPoints(i);
				}
			}
			else if (this.wasSelected[i])
			{
				this.wasSelected[i] = false;
				this.lines[i].SetColor(Color.white);
			}
		}
	}

	
	private void OnGUI()
	{
		GUI.Label(new Rect(10f, 10f, 800f, 30f), "Click a line to make a new line");
	}

	
	public float lineThickness = 10f;

	
	public int extraThickness = 2;

	
	public int numberOfLines = 2;

	
	private VectorLine[] lines;

	
	private bool[] wasSelected;
}
