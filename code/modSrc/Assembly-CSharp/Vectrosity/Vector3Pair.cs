using System;
using UnityEngine;

namespace Vectrosity
{
	// Token: 0x0200038D RID: 909
	public struct Vector3Pair
	{
		// Token: 0x060021B6 RID: 8630 RVA: 0x00016916 File Offset: 0x00014B16
		public Vector3Pair(Vector3 point1, Vector3 point2)
		{
			this.p1 = point1;
			this.p2 = point2;
		}

		// Token: 0x0400294C RID: 10572
		public Vector3 p1;

		// Token: 0x0400294D RID: 10573
		public Vector3 p2;
	}
}
