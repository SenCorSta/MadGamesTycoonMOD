using System;
using System.Collections;
using UnityEngine;

namespace Vectrosity
{
	// Token: 0x0200038E RID: 910
	[AddComponentMenu("Vectrosity/VisibilityControl")]
	public class VisibilityControl : MonoBehaviour
	{
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060021B7 RID: 8631 RVA: 0x00016926 File Offset: 0x00014B26
		public RefInt objectNumber
		{
			get
			{
				return this.m_objectNumber;
			}
		}

		// Token: 0x060021B8 RID: 8632 RVA: 0x0015EC60 File Offset: 0x0015CE60
		public void Setup(VectorLine line, bool makeBounds)
		{
			if (makeBounds)
			{
				VectorManager.SetupBoundsMesh(base.gameObject, line);
			}
			VectorManager.VisibilitySetup(base.transform, line, out this.m_objectNumber);
			this.m_vectorLine = line;
			VectorManager.DrawArrayLine2(this.m_objectNumber.i);
			base.StartCoroutine(this.VisibilityTest());
		}

		// Token: 0x060021B9 RID: 8633 RVA: 0x0001692E File Offset: 0x00014B2E
		private IEnumerator VisibilityTest()
		{
			yield return null;
			yield return null;
			if (!base.GetComponent<Renderer>().isVisible)
			{
				this.m_vectorLine.active = false;
			}
			yield break;
		}

		// Token: 0x060021BA RID: 8634 RVA: 0x0001693D File Offset: 0x00014B3D
		private IEnumerator OnBecameVisible()
		{
			yield return new WaitForEndOfFrame();
			this.m_vectorLine.active = true;
			yield break;
		}

		// Token: 0x060021BB RID: 8635 RVA: 0x0001694C File Offset: 0x00014B4C
		private IEnumerator OnBecameInvisible()
		{
			yield return new WaitForEndOfFrame();
			this.m_vectorLine.active = false;
			yield break;
		}

		// Token: 0x060021BC RID: 8636 RVA: 0x0001695B File Offset: 0x00014B5B
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

		// Token: 0x060021BD RID: 8637 RVA: 0x00016991 File Offset: 0x00014B91
		public void DontDestroyLine()
		{
			this.m_dontDestroyLine = true;
		}

		// Token: 0x0400294E RID: 10574
		private RefInt m_objectNumber;

		// Token: 0x0400294F RID: 10575
		private VectorLine m_vectorLine;

		// Token: 0x04002950 RID: 10576
		private bool m_destroyed;

		// Token: 0x04002951 RID: 10577
		private bool m_dontDestroyLine;
	}
}
