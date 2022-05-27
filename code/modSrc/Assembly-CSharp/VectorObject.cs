﻿using System;
using UnityEngine;
using Vectrosity;


public class VectorObject : MonoBehaviour
{
	
	private void Start()
	{
		VectorLine vectorLine = new VectorLine("Shape", XrayLineData.use.shapePoints[(int)this.shape], XrayLineData.use.lineTexture, XrayLineData.use.lineWidth);
		vectorLine.color = Color.green;
		VectorManager.ObjectSetup(base.gameObject, vectorLine, Visibility.Always, Brightness.None);
	}

	
	public VectorObject.Shape shape;

	
	public enum Shape
	{
		
		Cube,
		
		Sphere
	}
}
