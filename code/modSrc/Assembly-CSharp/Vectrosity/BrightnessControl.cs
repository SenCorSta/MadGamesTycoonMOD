using System;
using UnityEngine;

namespace Vectrosity
{
	// Token: 0x0200037B RID: 891
	[AddComponentMenu("Vectrosity/BrightnessControl")]
	public class BrightnessControl : MonoBehaviour
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06002012 RID: 8210 RVA: 0x00015401 File Offset: 0x00013601
		public RefInt objectNumber
		{
			get
			{
				return this.m_objectNumber;
			}
		}

		// Token: 0x06002013 RID: 8211 RVA: 0x00150714 File Offset: 0x0014E914
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

		// Token: 0x06002014 RID: 8212 RVA: 0x00015409 File Offset: 0x00013609
		public void SetUseLine(bool useLine)
		{
			this.m_useLine = useLine;
		}

		// Token: 0x06002015 RID: 8213 RVA: 0x00015412 File Offset: 0x00013612
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

		// Token: 0x06002016 RID: 8214 RVA: 0x0001544A File Offset: 0x0001364A
		public void OnBecameInvisible()
		{
			if (!this.m_useLine)
			{
				return;
			}
			this.m_vectorLine.active = false;
		}

		// Token: 0x06002017 RID: 8215 RVA: 0x00015461 File Offset: 0x00013661
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

		// Token: 0x040028A1 RID: 10401
		private RefInt m_objectNumber;

		// Token: 0x040028A2 RID: 10402
		private VectorLine m_vectorLine;

		// Token: 0x040028A3 RID: 10403
		private bool m_useLine;

		// Token: 0x040028A4 RID: 10404
		private bool m_destroyed;
	}
}
