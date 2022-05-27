using System;
using UnityEngine;

namespace Vectrosity
{
	// Token: 0x02000392 RID: 914
	[AddComponentMenu("Vectrosity/VisibilityControlAlways")]
	public class VisibilityControlAlways : MonoBehaviour
	{
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060021D1 RID: 8657 RVA: 0x000169DF File Offset: 0x00014BDF
		public RefInt objectNumber
		{
			get
			{
				return this.m_objectNumber;
			}
		}

		// Token: 0x060021D2 RID: 8658 RVA: 0x000169E7 File Offset: 0x00014BE7
		public void Setup(VectorLine line)
		{
			VectorManager.VisibilitySetup(base.transform, line, out this.m_objectNumber);
			VectorManager.DrawArrayLine2(this.m_objectNumber.i);
			this.m_vectorLine = line;
		}

		// Token: 0x060021D3 RID: 8659 RVA: 0x00016A12 File Offset: 0x00014C12
		private void OnDestroy()
		{
			if (this.m_destroyed)
			{
				return;
			}
			this.m_destroyed = true;
			VectorManager.VisibilityRemove(this.m_objectNumber.i);
			if (this.m_dontDestroyLine)
			{
				return;
			}
			VectorLine.Destroy(ref this.m_vectorLine);
		}

		// Token: 0x060021D4 RID: 8660 RVA: 0x00016A48 File Offset: 0x00014C48
		public void DontDestroyLine()
		{
			this.m_dontDestroyLine = true;
		}

		// Token: 0x0400295B RID: 10587
		private RefInt m_objectNumber;

		// Token: 0x0400295C RID: 10588
		private VectorLine m_vectorLine;

		// Token: 0x0400295D RID: 10589
		private bool m_destroyed;

		// Token: 0x0400295E RID: 10590
		private bool m_dontDestroyLine;
	}
}
