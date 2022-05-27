using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vectrosity
{
	// Token: 0x02000393 RID: 915
	[AddComponentMenu("Vectrosity/VisibilityControlStatic")]
	public class VisibilityControlStatic : MonoBehaviour
	{
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060021D6 RID: 8662 RVA: 0x00016A51 File Offset: 0x00014C51
		public RefInt objectNumber
		{
			get
			{
				return this.m_objectNumber;
			}
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x0015EDDC File Offset: 0x0015CFDC
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

		// Token: 0x060021D8 RID: 8664 RVA: 0x00016A59 File Offset: 0x00014C59
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

		// Token: 0x060021D9 RID: 8665 RVA: 0x00016A68 File Offset: 0x00014C68
		private void OnBecameVisible()
		{
			this.m_vectorLine.active = true;
			VectorManager.DrawArrayLine(this.m_objectNumber.i);
		}

		// Token: 0x060021DA RID: 8666 RVA: 0x00016A86 File Offset: 0x00014C86
		private void OnBecameInvisible()
		{
			this.m_vectorLine.active = false;
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x00016A94 File Offset: 0x00014C94
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

		// Token: 0x060021DC RID: 8668 RVA: 0x00016ACA File Offset: 0x00014CCA
		public void DontDestroyLine()
		{
			this.m_dontDestroyLine = true;
		}

		// Token: 0x060021DD RID: 8669 RVA: 0x00016AD3 File Offset: 0x00014CD3
		public Matrix4x4 GetMatrix()
		{
			return this.m_originalMatrix;
		}

		// Token: 0x0400295F RID: 10591
		private RefInt m_objectNumber;

		// Token: 0x04002960 RID: 10592
		private VectorLine m_vectorLine;

		// Token: 0x04002961 RID: 10593
		private bool m_destroyed;

		// Token: 0x04002962 RID: 10594
		private bool m_dontDestroyLine;

		// Token: 0x04002963 RID: 10595
		private Matrix4x4 m_originalMatrix;
	}
}
