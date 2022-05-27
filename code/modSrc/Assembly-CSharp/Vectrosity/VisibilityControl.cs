using System;
using System.Collections;
using UnityEngine;

namespace Vectrosity
{
	// Token: 0x02000391 RID: 913
	[AddComponentMenu("Vectrosity/VisibilityControl")]
	public class VisibilityControl : MonoBehaviour
	{
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600220A RID: 8714 RVA: 0x0015FCC4 File Offset: 0x0015DEC4
		public RefInt objectNumber
		{
			get
			{
				return this.m_objectNumber;
			}
		}

		// Token: 0x0600220B RID: 8715 RVA: 0x0015FCCC File Offset: 0x0015DECC
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

		// Token: 0x0600220C RID: 8716 RVA: 0x0015FD1E File Offset: 0x0015DF1E
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

		// Token: 0x0600220D RID: 8717 RVA: 0x0015FD2D File Offset: 0x0015DF2D
		private IEnumerator OnBecameVisible()
		{
			yield return new WaitForEndOfFrame();
			this.m_vectorLine.active = true;
			yield break;
		}

		// Token: 0x0600220E RID: 8718 RVA: 0x0015FD3C File Offset: 0x0015DF3C
		private IEnumerator OnBecameInvisible()
		{
			yield return new WaitForEndOfFrame();
			this.m_vectorLine.active = false;
			yield break;
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x0015FD4B File Offset: 0x0015DF4B
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

		// Token: 0x06002210 RID: 8720 RVA: 0x0015FD81 File Offset: 0x0015DF81
		public void DontDestroyLine()
		{
			this.m_dontDestroyLine = true;
		}

		// Token: 0x04002964 RID: 10596
		private RefInt m_objectNumber;

		// Token: 0x04002965 RID: 10597
		private VectorLine m_vectorLine;

		// Token: 0x04002966 RID: 10598
		private bool m_destroyed;

		// Token: 0x04002967 RID: 10599
		private bool m_dontDestroyLine;
	}
}
