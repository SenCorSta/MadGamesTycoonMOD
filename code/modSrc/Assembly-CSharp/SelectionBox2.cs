using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;


public class SelectionBox2 : MonoBehaviour
{
	
	private void Start()
	{
		this.selectionLine = new VectorLine("Selection", new List<Vector2>(5), this.lineTexture, 4f, LineType.Continuous);
		this.selectionLine.textureScale = this.textureScale;
		this.selectionLine.alignOddWidthToPixels = true;
	}

	
	private void OnGUI()
	{
		GUI.Label(new Rect(10f, 10f, 300f, 25f), "Click & drag to make a selection box");
	}

	
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			this.originalPos = Input.mousePosition;
		}
		if (Input.GetMouseButton(0))
		{
			this.selectionLine.MakeRect(this.originalPos, Input.mousePosition);
			this.selectionLine.Draw();
		}
		this.selectionLine.textureOffset = -Time.time * 2f % 1f;
	}

	
	public Texture lineTexture;

	
	public float textureScale = 4f;

	
	private VectorLine selectionLine;

	
	private Vector2 originalPos;
}
