using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vectrosity
{
	// Token: 0x02000396 RID: 918
	[AddComponentMenu("Vectrosity/VisibilityControlStatic")]
	public class VisibilityControlStatic : MonoBehaviour
	{
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06002229 RID: 8745 RVA: 0x0015FF6D File Offset: 0x0015E16D
		public RefInt objectNumber
		{
			get
			{
				return this.m_objectNumber;
			}
		}

		// Token: 0x0600222A RID: 8746 RVA: 0x0015FF78 File Offset: 0x0015E178
		public void Setup(VectorLine line, bool makeBounds)
		{
			if (makeBounds)
			{
				VectorManager.SetupBoundsMesh(base.gameObject, line);
			}
			this.m_originalMatrix = base.transform.localToWorldMatrix;
			List<Vector3> list = new List<Vector3>(line.points3);
			for (int i = 0; i < list.Count; i++)
			{
				list[i] = this.m_originalMatrix.MultiplyPoint3x4(list[i]);
			}
			line.points3 = list;
			this.m_vectorLine = line;
			VectorManager.VisibilityStaticSetup(line, out this.m_objectNumber);
			base.StartCoroutine(this.WaitCheck());
		}

		// Token: 0x0600222B RID: 8747 RVA: 0x00160002 File Offset: 0x0015E202
		private IEnumerator WaitCheck()
		{
			VectorManager.DrawArrayLine(this.m_objectNumber.i);
			yield return null;
			yield return null;
			if (!base.GetComponent<Renderer>().isVisible)
			{
				this.m_vectorLine.active = false;
			}
			yield break;
		}

		// Token: 0x0600222C RID: 8748 RVA: 0x00160011 File Offset: 0x0015E211
		private void OnBecameVisible()
		{
			this.m_vectorLine.active = true;
			VectorManager.DrawArrayLine(this.m_objectNumber.i);
		}

		// Token: 0x0600222D RID: 8749 RVA: 0x0016002F File Offset: 0x0015E22F
		private void OnBecameInvisible()
		{
			this.m_vectorLine.active = false;
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x0016003D File Offset: 0x0015E23D
		private void OnDestroy()
		{
			if (this.m_destroyed)
			{
				return;
			}
			this.m_destroyed = true;
			VectorManager.VisibilityStaticRemove(this.m_objectNumber.i);
			if (this.m_dontDestroyLine)
			{
				return;
			}
			VectorLine.Destroy(ref this.m_vectorLine);
		}

		// Token: 0x0600222F RID: 8751 RVA: 0x00160073 File Offset: 0x0015E273
		public void DontDestroyLine()
		{
			this.m_dontDestroyLine = true;
		}

		// Token: 0x06002230 RID: 8752 RVA: 0x0016007C File Offset: 0x0015E27C
		public Matrix4x4 GetMatrix()
		{
			return this.m_originalMatrix;
		}

		// Token: 0x04002975 RID: 10613
		private RefInt m_objectNumber;

		// Token: 0x04002976 RID: 10614
		private VectorLine m_vectorLine;

		// Token: 0x04002977 RID: 10615
		private bool m_destroyed;

		// Token: 0x04002978 RID: 10616
		private bool m_dontDestroyLine;

		// Token: 0x04002979 RID: 10617
		private Matrix4x4 m_originalMatrix;
	}
}
