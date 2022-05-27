using System;
using UnityEngine;

namespace Vectrosity
{
	
	[AddComponentMenu("Vectrosity/VisibilityControlAlways")]
	public class VisibilityControlAlways : MonoBehaviour
	{
		
		// (get) Token: 0x060021D1 RID: 8657 RVA: 0x000169DF File Offset: 0x00014BDF
		public RefInt objectNumber
		{
			get
			{
				return this.m_objectNumber;
			}
		}

		
		public void Setup(VectorLine line)
		{
			VectorManager.VisibilitySetup(base.transform, line, out this.m_objectNumber);
			VectorManager.DrawArrayLine2(this.m_objectNumber.i);
			this.m_vectorLine = line;
		}

		
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

		
		public void DontDestroyLine()
		{
			this.m_dontDestroyLine = true;
		}

		
		private RefInt m_objectNumber;

		
		private VectorLine m_vectorLine;

		
		private bool m_destroyed;

		
		private bool m_dontDestroyLine;
	}
}
