using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000369 RID: 873
public class MakeSpline : MonoBehaviour
{
	// Token: 0x06002024 RID: 8228 RVA: 0x0014D68C File Offset: 0x0014B88C
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

	// Token: 0x04002878 RID: 10360
	public int segments = 250;

	// Token: 0x04002879 RID: 10361
	public bool loop = true;

	// Token: 0x0400287A RID: 10362
	public bool usePoints;
}
