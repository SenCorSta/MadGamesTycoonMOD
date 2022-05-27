using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;


public class SelectionBox : MonoBehaviour
{
	
	private void Start()
	{
		this.lineColors = new List<Color32>(new Color32[4]);
		this.selectionLine = new VectorLine("Selection", new List<Vector2>(5), 3f, LineType.Continuous);
		this.selectionLine.capLength = 1.5f;
	}

	
	private void OnGUI()
	{
		GUI.Label(new Rect(10f, 10f, 300f, 25f), "Click & drag to make a selection box");
	}

	
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			base.StopCoroutine("CycleColor");
			this.selectionLine.SetColor(Color.white);
			this.originalPos = Input.mousePosition;
		}
		if (Input.GetMouseButton(0))
		{
			this.selectionLine.MakeRect(this.originalPos, Input.mousePosition);
			this.selectionLine.Draw();
		}
		if (Input.GetMouseButtonUp(0))
		{
			base.StartCoroutine("CycleColor");
		}
	}

	
	private IEnumerator CycleColor()
	{
		for (;;)
		{
			for (int i = 0; i < 4; i++)
			{
				this.lineColors[i] = Color.Lerp(Color.yellow, Color.red, Mathf.PingPong((Time.time + (float)i * 0.25f) * 3f, 1f));
			}
			this.selectionLine.SetColors(this.lineColors);
			yield return null;
		}
		yield break;
	}

	
	private VectorLine selectionLine;

	
	private Vector2 originalPos;

	
	private List<Color32> lineColors;
}
