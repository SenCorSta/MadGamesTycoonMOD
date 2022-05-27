using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vectrosity
{
	
	[AddComponentMenu("Vectrosity/VisibilityControlStatic")]
	public class VisibilityControlStatic : MonoBehaviour
	{
		
		// (get) Token: 0x060021D6 RID: 8662 RVA: 0x00016A51 File Offset: 0x00014C51
		public RefInt objectNumber
		{
			get
			{
				return this.m_objectNumber;
			}
		}

		
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

		
		private void OnBecameVisible()
		{
			this.m_vectorLine.active = true;
			VectorManager.DrawArrayLine(this.m_objectNumber.i);
		}

		
		private void OnBecameInvisible()
		{
			this.m_vectorLine.active = false;
		}

		
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

		
		public void DontDestroyLine()
		{
			this.m_dontDestroyLine = true;
		}

		
		public Matrix4x4 GetMatrix()
		{
			return this.m_originalMatrix;
		}

		
		private RefInt m_objectNumber;

		
		private VectorLine m_vectorLine;

		
		private bool m_destroyed;

		
		private bool m_dontDestroyLine;

		
		private Matrix4x4 m_originalMatrix;
	}
}
