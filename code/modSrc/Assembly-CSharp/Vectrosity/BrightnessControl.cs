using System;
using UnityEngine;

namespace Vectrosity
{
	// Token: 0x0200037E RID: 894
	[AddComponentMenu("Vectrosity/BrightnessControl")]
	public class BrightnessControl : MonoBehaviour
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06002065 RID: 8293 RVA: 0x0015024A File Offset: 0x0014E44A
		public RefInt objectNumber
		{
			get
			{
				return this.m_objectNumber;
			}
		}

		// Token: 0x06002066 RID: 8294 RVA: 0x00150254 File Offset: 0x0014E454
		public void Setup(VectorLine line, bool m_useLine)
		{
			this.m_objectNumber = new RefInt(0);
			VectorManager.CheckDistanceSetup(base.transform, line, line.color, this.m_objectNumber);
			VectorManager.SetDistanceColor(this.m_objectNumber.i);
			if (m_useLine)
			{
				this.m_useLine = true;
				this.m_vectorLine = line;
			}
		}

		// Token: 0x06002067 RID: 8295 RVA: 0x001502AB File Offset: 0x0014E4AB
		public void SetUseLine(bool useLine)
		{
			this.m_useLine = useLine;
		}

		// Token: 0x06002068 RID: 8296 RVA: 0x001502B4 File Offset: 0x0014E4B4
		private void OnBecameVisible()
		{
			VectorManager.SetOldDistance(this.m_objectNumber.i, -1);
			VectorManager.SetDistanceColor(this.m_objectNumber.i);
			if (!this.m_useLine)
			{
				return;
			}
			this.m_vectorLine.active = true;
		}

		// Token: 0x06002069 RID: 8297 RVA: 0x001502EC File Offset: 0x0014E4EC
		public void OnBecameInvisible()
		{
			if (!this.m_useLine)
			{
				return;
			}
			this.m_vectorLine.active = false;
		}

		// Token: 0x0600206A RID: 8298 RVA: 0x00150303 File Offset: 0x0014E503
		private void OnDestroy()
		{
			if (this.m_destroyed)
			{
				return;
			}
			this.m_destroyed = true;
			VectorManager.DistanceRemove(this.m_objectNumber.i);
			if (this.m_useLine)
			{
				VectorLine.Destroy(ref this.m_vectorLine);
			}
		}

		// Token: 0x040028B7 RID: 10423
		private RefInt m_objectNumber;

		// Token: 0x040028B8 RID: 10424
		private VectorLine m_vectorLine;

		// Token: 0x040028B9 RID: 10425
		private bool m_useLine;

		// Token: 0x040028BA RID: 10426
		private bool m_destroyed;
	}
}
