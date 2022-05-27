using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000348 RID: 840
public class SimpleCurve : MonoBehaviour
{
	// Token: 0x06001F5B RID: 8027 RVA: 0x0014B1D4 File Offset: 0x001493D4
	private void Start()
	{
		if (this.curvePoints.Length != 4)
		{
			Debug.Log("Curve points array must have 4 elements only");
			return;
		}
		List<Vector2> points = new List<Vector2>(this.segments + 1);
		VectorLine vectorLine = new VectorLine("Curve", points, 2f, LineType.Continuous, Joins.Weld);
		vectorLine.MakeCurve(this.curvePoints, this.segments);
		vectorLine.Draw();
	}

	// Token: 0x040027B1 RID: 10161
	public Vector2[] curvePoints;

	// Token: 0x040027B2 RID: 10162
	public int segments = 50;
}
