using System;
using UnityEngine;

namespace Vectrosity
{
	// Token: 0x0200037C RID: 892
	public class CapInfo
	{
		// Token: 0x06002019 RID: 8217 RVA: 0x0015076C File Offset: 0x0014E96C
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

		// Token: 0x040028A5 RID: 10405
		public EndCap capType;

		// Token: 0x040028A6 RID: 10406
		public Texture texture;

		// Token: 0x040028A7 RID: 10407
		public float ratio1;

		// Token: 0x040028A8 RID: 10408
		public float ratio2;

		// Token: 0x040028A9 RID: 10409
		public float offset1;

		// Token: 0x040028AA RID: 10410
		public float offset2;

		// Token: 0x040028AB RID: 10411
		public float scale1;

		// Token: 0x040028AC RID: 10412
		public float scale2;

		// Token: 0x040028AD RID: 10413
		public float[] uvHeights;
	}
}
