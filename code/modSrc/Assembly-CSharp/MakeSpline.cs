using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000366 RID: 870
public class MakeSpline : MonoBehaviour
{
	// Token: 0x06001FD1 RID: 8145 RVA: 0x0014DDCC File Offset: 0x0014BFCC
	private void Start()
	{
		List<Vector3> list = new List<Vector3>();
		int num = 1;
		GameObject gameObject = GameObject.Find("Sphere" + num++);
		while (gameObject != null)
		{
			list.Add(gameObject.transform.position);
			gameObject = GameObject.Find("Sphere" + num++);
		}
		if (this.usePoints)
		{
			VectorLine vectorLine = new VectorLine("Spline", new List<Vector3>(this.segments + 1), 2f, LineType.Points);
			vectorLine.MakeSpline(list.ToArray(), this.segments, this.loop);
			vectorLine.Draw();
			return;
		}
		VectorLine vectorLine2 = new VectorLine("Spline", new List<Vector3>(this.segments + 1), 2f, LineType.Continuous);
		vectorLine2.MakeSpline(list.ToArray(), this.segments, this.loop);
		vectorLine2.Draw3D();
	}

	// Token: 0x04002862 RID: 10338
	public int segments = 250;

	// Token: 0x04002863 RID: 10339
	public bool loop = true;

	// Token: 0x04002864 RID: 10340
	public bool usePoints;
}
