using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;


public class DrawPoints : MonoBehaviour
{
	
	private void Start()
	{
		int num = this.numberOfDots * this.numberOfRings;
		Vector2[] collection = new Vector2[num];
		Color32[] array = new Color32[num];
		float b = 1f - 0.75f / (float)num;
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = this.dotColor;
			this.dotColor *= b;
		}
		VectorLine vectorLine = new VectorLine("Dots", new List<Vector2>(collection), this.dotSize, LineType.Points);
		vectorLine.SetColors(new List<Color32>(array));
		for (int j = 0; j < this.numberOfRings; j++)
		{
			vectorLine.MakeCircle(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), (float)(Screen.height / (j + 2)), this.numberOfDots, this.numberOfDots * j);
		}
		vectorLine.Draw();
	}

	
	public float dotSize = 2f;

	
	public int numberOfDots = 100;

	
	public int numberOfRings = 8;

	
	public Color dotColor = Color.cyan;
}
