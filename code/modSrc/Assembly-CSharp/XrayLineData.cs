﻿using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000370 RID: 880
public class XrayLineData : MonoBehaviour
{
	// Token: 0x06001FF2 RID: 8178 RVA: 0x0014E588 File Offset: 0x0014C788
	private void Awake()
	{
		XrayLineData.use = this;
		this.shapePoints = new List<List<Vector3>>();
		this.shapePoints.Add(new List<Vector3>
		{
			new Vector3(-0.5f, -0.5f, 0.5f),
			new Vector3(0.5f, -0.5f, 0.5f),
			new Vector3(-0.5f, 0.5f, 0.5f),
			new Vector3(-0.5f, -0.5f, 0.5f),
			new Vector3(0.5f, -0.5f, 0.5f),
			new Vector3(0.5f, 0.5f, 0.5f),
			new Vector3(0.5f, 0.5f, 0.5f),
			new Vector3(-0.5f, 0.5f, 0.5f),
			new Vector3(-0.5f, 0.5f, -0.5f),
			new Vector3(-0.5f, 0.5f, 0.5f),
			new Vector3(0.5f, 0.5f, 0.5f),
			new Vector3(0.5f, 0.5f, -0.5f),
			new Vector3(0.5f, 0.5f, -0.5f),
			new Vector3(-0.5f, 0.5f, -0.5f),
			new Vector3(-0.5f, -0.5f, -0.5f),
			new Vector3(-0.5f, 0.5f, -0.5f),
			new Vector3(0.5f, 0.5f, -0.5f),
			new Vector3(0.5f, -0.5f, -0.5f),
			new Vector3(0.5f, -0.5f, -0.5f),
			new Vector3(-0.5f, -0.5f, -0.5f),
			new Vector3(-0.5f, -0.5f, 0.5f),
			new Vector3(-0.5f, -0.5f, -0.5f),
			new Vector3(0.5f, -0.5f, -0.5f),
			new Vector3(0.5f, -0.5f, 0.5f)
		});
		this.shapePoints.Add(new List<Vector3>
		{
			new Vector3(-0.25f, 0.433f, 0f),
			new Vector3(0f, 0.5f, 0f),
			new Vector3(0f, 0.5f, 0f),
			new Vector3(-0.177f, 0.433f, -0.177f),
			new Vector3(-0.177f, 0.433f, -0.177f),
			new Vector3(-0.25f, 0.433f, 0f),
			new Vector3(0f, 0.5f, 0f),
			new Vector3(0f, 0.433f, -0.25f),
			new Vector3(0f, 0.433f, -0.25f),
			new Vector3(-0.177f, 0.433f, -0.177f),
			new Vector3(-0.306f, 0.25f, -0.306f),
			new Vector3(-0.177f, 0.433f, -0.177f),
			new Vector3(0f, 0.433f, -0.25f),
			new Vector3(0f, 0.25f, -0.433f),
			new Vector3(0f, 0.25f, -0.433f),
			new Vector3(-0.306f, 0.25f, -0.306f),
			new Vector3(-0.354f, 0f, -0.354f),
			new Vector3(-0.306f, 0.25f, -0.306f),
			new Vector3(0f, 0.25f, -0.433f),
			new Vector3(0f, 0f, -0.5f),
			new Vector3(0f, 0f, -0.5f),
			new Vector3(-0.354f, 0f, -0.354f),
			new Vector3(-0.306f, -0.25f, -0.306f),
			new Vector3(-0.354f, 0f, -0.354f),
			new Vector3(0f, 0f, -0.5f),
			new Vector3(0f, -0.25f, -0.433f),
			new Vector3(0f, -0.25f, -0.433f),
			new Vector3(-0.306f, -0.25f, -0.306f),
			new Vector3(-0.177f, -0.433f, -0.177f),
			new Vector3(-0.306f, -0.25f, -0.306f),
			new Vector3(0f, -0.25f, -0.433f),
			new Vector3(0f, -0.433f, -0.25f),
			new Vector3(0f, -0.433f, -0.25f),
			new Vector3(-0.177f, -0.433f, -0.177f),
			new Vector3(0f, -0.433f, -0.25f),
			new Vector3(0f, -0.5f, 0f),
			new Vector3(0f, -0.5f, 0f),
			new Vector3(-0.177f, -0.433f, -0.177f),
			new Vector3(0f, -0.433f, -0.25f),
			new Vector3(0.177f, -0.433f, -0.177f),
			new Vector3(0.177f, -0.433f, -0.177f),
			new Vector3(0f, -0.5f, 0f),
			new Vector3(0.177f, -0.433f, -0.177f),
			new Vector3(0.25f, -0.433f, 0f),
			new Vector3(0.25f, -0.433f, 0f),
			new Vector3(0f, -0.5f, 0f),
			new Vector3(0.433f, -0.25f, 0f),
			new Vector3(0.25f, -0.433f, 0f),
			new Vector3(0.433f, -0.25f, 0f),
			new Vector3(0.306f, -0.25f, 0.306f),
			new Vector3(0.354f, 0f, 0.354f),
			new Vector3(0.306f, -0.25f, 0.306f),
			new Vector3(0.354f, 0f, 0.354f),
			new Vector3(0f, 0f, 0.5f),
			new Vector3(0f, 0.25f, 0.433f),
			new Vector3(0f, 0f, 0.5f),
			new Vector3(0f, 0.25f, 0.433f),
			new Vector3(-0.306f, 0.25f, 0.306f),
			new Vector3(-0.177f, 0.433f, 0.177f),
			new Vector3(-0.306f, 0.25f, 0.306f),
			new Vector3(-0.177f, 0.433f, 0.177f),
			new Vector3(-0.25f, 0.433f, 0f),
			new Vector3(-0.177f, 0.433f, 0.177f),
			new Vector3(0f, 0.5f, 0f),
			new Vector3(0.25f, -0.433f, 0f),
			new Vector3(0.177f, -0.433f, 0.177f),
			new Vector3(0.177f, -0.433f, 0.177f),
			new Vector3(0f, -0.5f, 0f),
			new Vector3(0.306f, -0.25f, 0.306f),
			new Vector3(0.177f, -0.433f, 0.177f),
			new Vector3(0.306f, -0.25f, 0.306f),
			new Vector3(0f, -0.25f, 0.433f),
			new Vector3(0f, 0f, 0.5f),
			new Vector3(0f, -0.25f, 0.433f),
			new Vector3(0f, 0f, 0.5f),
			new Vector3(-0.354f, 0f, 0.354f),
			new Vector3(-0.306f, 0.25f, 0.306f),
			new Vector3(-0.354f, 0f, 0.354f),
			new Vector3(-0.306f, 0.25f, 0.306f),
			new Vector3(-0.433f, 0.25f, 0f),
			new Vector3(-0.25f, 0.433f, 0f),
			new Vector3(-0.433f, 0.25f, 0f),
			new Vector3(0.177f, -0.433f, 0.177f),
			new Vector3(0f, -0.433f, 0.25f),
			new Vector3(0f, -0.433f, 0.25f),
			new Vector3(0f, -0.5f, 0f),
			new Vector3(0f, -0.25f, 0.433f),
			new Vector3(0f, -0.433f, 0.25f),
			new Vector3(0f, -0.25f, 0.433f),
			new Vector3(-0.306f, -0.25f, 0.306f),
			new Vector3(-0.354f, 0f, 0.354f),
			new Vector3(-0.306f, -0.25f, 0.306f),
			new Vector3(-0.354f, 0f, 0.354f),
			new Vector3(-0.5f, 0f, 0f),
			new Vector3(-0.433f, 0.25f, 0f),
			new Vector3(-0.5f, 0f, 0f),
			new Vector3(-0.433f, 0.25f, 0f),
			new Vector3(-0.306f, 0.25f, -0.306f),
			new Vector3(0f, -0.433f, 0.25f),
			new Vector3(-0.177f, -0.433f, 0.177f),
			new Vector3(-0.177f, -0.433f, 0.177f),
			new Vector3(0f, -0.5f, 0f),
			new Vector3(-0.306f, -0.25f, 0.306f),
			new Vector3(-0.177f, -0.433f, 0.177f),
			new Vector3(-0.306f, -0.25f, 0.306f),
			new Vector3(-0.433f, -0.25f, 0f),
			new Vector3(-0.5f, 0f, 0f),
			new Vector3(-0.433f, -0.25f, 0f),
			new Vector3(-0.5f, 0f, 0f),
			new Vector3(-0.354f, 0f, -0.354f),
			new Vector3(-0.177f, -0.433f, 0.177f),
			new Vector3(-0.25f, -0.433f, 0f),
			new Vector3(-0.25f, -0.433f, 0f),
			new Vector3(0f, -0.5f, 0f),
			new Vector3(-0.433f, -0.25f, 0f),
			new Vector3(-0.25f, -0.433f, 0f),
			new Vector3(-0.433f, -0.25f, 0f),
			new Vector3(-0.306f, -0.25f, -0.306f),
			new Vector3(0f, 0.25f, 0.433f),
			new Vector3(0f, 0.433f, 0.25f),
			new Vector3(0f, 0.433f, 0.25f),
			new Vector3(-0.177f, 0.433f, 0.177f),
			new Vector3(0f, 0.25f, 0.433f),
			new Vector3(0.306f, 0.25f, 0.306f),
			new Vector3(0.306f, 0.25f, 0.306f),
			new Vector3(0.177f, 0.433f, 0.177f),
			new Vector3(0.177f, 0.433f, 0.177f),
			new Vector3(0f, 0.433f, 0.25f),
			new Vector3(0.306f, 0.25f, 0.306f),
			new Vector3(0.433f, 0.25f, 0f),
			new Vector3(0.433f, 0.25f, 0f),
			new Vector3(0.25f, 0.433f, 0f),
			new Vector3(0.25f, 0.433f, 0f),
			new Vector3(0.177f, 0.433f, 0.177f),
			new Vector3(0.433f, 0.25f, 0f),
			new Vector3(0.306f, 0.25f, -0.306f),
			new Vector3(0.306f, 0.25f, -0.306f),
			new Vector3(0.177f, 0.433f, -0.177f),
			new Vector3(0.177f, 0.433f, -0.177f),
			new Vector3(0.25f, 0.433f, 0f),
			new Vector3(0.306f, 0.25f, -0.306f),
			new Vector3(0f, 0.25f, -0.433f),
			new Vector3(0f, 0.433f, -0.25f),
			new Vector3(0.177f, 0.433f, -0.177f),
			new Vector3(0f, 0.433f, 0.25f),
			new Vector3(0f, 0.5f, 0f),
			new Vector3(0.177f, 0.433f, 0.177f),
			new Vector3(0f, 0.5f, 0f),
			new Vector3(0.25f, 0.433f, 0f),
			new Vector3(0f, 0.5f, 0f),
			new Vector3(0.354f, 0f, 0.354f),
			new Vector3(0.306f, 0.25f, 0.306f),
			new Vector3(0.354f, 0f, 0.354f),
			new Vector3(0.5f, 0f, 0f),
			new Vector3(0.5f, 0f, 0f),
			new Vector3(0.433f, 0.25f, 0f),
			new Vector3(0.5f, 0f, 0f),
			new Vector3(0.354f, 0f, -0.354f),
			new Vector3(0.354f, 0f, -0.354f),
			new Vector3(0.306f, 0.25f, -0.306f),
			new Vector3(0.354f, 0f, -0.354f),
			new Vector3(0f, 0f, -0.5f),
			new Vector3(0.433f, -0.25f, 0f),
			new Vector3(0.5f, 0f, 0f),
			new Vector3(0.433f, -0.25f, 0f),
			new Vector3(0.306f, -0.25f, -0.306f),
			new Vector3(0.306f, -0.25f, -0.306f),
			new Vector3(0.354f, 0f, -0.354f),
			new Vector3(0.306f, -0.25f, -0.306f),
			new Vector3(0f, -0.25f, -0.433f),
			new Vector3(0.177f, 0.433f, -0.177f),
			new Vector3(0f, 0.5f, 0f),
			new Vector3(0.177f, -0.433f, -0.177f),
			new Vector3(0.306f, -0.25f, -0.306f),
			new Vector3(-0.25f, -0.433f, 0f),
			new Vector3(-0.177f, -0.433f, -0.177f)
		});
	}

	// Token: 0x04002888 RID: 10376
	public static XrayLineData use;

	// Token: 0x04002889 RID: 10377
	public Texture lineTexture;

	// Token: 0x0400288A RID: 10378
	public float lineWidth = 1f;

	// Token: 0x0400288B RID: 10379
	[HideInInspector]
	public List<List<Vector3>> shapePoints;
}
