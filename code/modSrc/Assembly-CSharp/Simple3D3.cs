using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000379 RID: 889
public class Simple3D3 : MonoBehaviour
{
	// Token: 0x06002051 RID: 8273 RVA: 0x0014F83C File Offset: 0x0014DA3C
	private void Start()
	{
		List<Vector3> points = new List<Vector3>
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
		};
		VectorLine line = new VectorLine(base.gameObject.name, points, 3f);
		VectorManager.useDraw3D = true;
		VectorManager.ObjectSetup(base.gameObject, line, Visibility.Dynamic, Brightness.None, false);
	}
}
