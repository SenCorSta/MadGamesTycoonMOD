using System;
using UnityEngine;

namespace Vectrosity
{
	// Token: 0x0200037F RID: 895
	public class CapInfo
	{
		// Token: 0x0600206C RID: 8300 RVA: 0x00150338 File Offset: 0x0014E538
		public CapInfo(EndCap capType, Texture texture, float ratio1, float ratio2, float offset1, float offset2, float scale1, float scale2, float[] uvHeights)
		{
			this.capType = capType;
			this.texture = texture;
			this.ratio1 = ratio1;
			this.ratio2 = ratio2;
			this.offset1 = offset1;
			this.offset2 = offset2;
			this.scale1 = scale1;
			this.scale2 = scale2;
			this.uvHeights = uvHeights;
		}

		// Token: 0x040028BB RID: 10427
		public EndCap capType;

		// Token: 0x040028BC RID: 10428
		public Texture texture;

		// Token: 0x040028BD RID: 10429
		public float ratio1;

		// Token: 0x040028BE RID: 10430
		public float ratio2;

		// Token: 0x040028BF RID: 10431
		public float offset1;

		// Token: 0x040028C0 RID: 10432
		public float offset2;

		// Token: 0x040028C1 RID: 10433
		public float scale1;

		// Token: 0x040028C2 RID: 10434
		public float scale2;

		// Token: 0x040028C3 RID: 10435
		public float[] uvHeights;
	}
}
