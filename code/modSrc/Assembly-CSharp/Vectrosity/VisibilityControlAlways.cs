using System;
using UnityEngine;

namespace Vectrosity
{
	// Token: 0x02000395 RID: 917
	[AddComponentMenu("Vectrosity/VisibilityControlAlways")]
	public class VisibilityControlAlways : MonoBehaviour
	{
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06002224 RID: 8740 RVA: 0x0015FEFB File Offset: 0x0015E0FB
		public RefInt objectNumber
		{
			get
			{
				return this.m_objectNumber;
			}
		}

		// Token: 0x06002225 RID: 8741 RVA: 0x0015FF03 File Offset: 0x0015E103
		public void Setup(VectorLine line)
		{
			VectorManager.VisibilitySetup(base.transform, line, out this.m_objectNumber);
			VectorManager.DrawArrayLine2(this.m_objectNumber.i);
			this.m_vectorLine = line;
		}

		// Token: 0x06002226 RID: 8742 RVA: 0x0015FF2E File Offset: 0x0015E12E
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

		// Token: 0x06002227 RID: 8743 RVA: 0x0015FF64 File Offset: 0x0015E164
		public void DontDestroyLine()
		{
			this.m_dontDestroyLine = true;
		}

		// Token: 0x04002971 RID: 10609
		private RefInt m_objectNumber;

		// Token: 0x04002972 RID: 10610
		private VectorLine m_vectorLine;

		// Token: 0x04002973 RID: 10611
		private bool m_destroyed;

		// Token: 0x04002974 RID: 10612
		private bool m_dontDestroyLine;
	}
}
