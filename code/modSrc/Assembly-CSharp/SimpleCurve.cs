using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x0200034B RID: 843
public class SimpleCurve : MonoBehaviour
{
	// Token: 0x06001FAE RID: 8110 RVA: 0x0014A508 File Offset: 0x00148708
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

	// Token: 0x040027C7 RID: 10183
	public Vector2[] curvePoints;

	// Token: 0x040027C8 RID: 10184
	public int segments = 50;
}
