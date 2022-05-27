using System;
using UnityEngine;

namespace Vectrosity
{
	// Token: 0x02000390 RID: 912
	public struct Vector3Pair
	{
		// Token: 0x06002209 RID: 8713 RVA: 0x0015FCB4 File Offset: 0x0015DEB4
		public Vector3Pair(Vector3 point1, Vector3 point2)
		{
			this.p1 = point1;
			this.p2 = point2;
		}

		// Token: 0x04002962 RID: 10594
		public Vector3 p1;

		// Token: 0x04002963 RID: 10595
		public Vector3 p2;
	}
}
