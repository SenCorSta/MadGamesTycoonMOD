using System;
using UnityEngine;

namespace Vectrosity
{
	
	[AddComponentMenu("Vectrosity/VisibilityControlAlways")]
	public class VisibilityControlAlways : MonoBehaviour
	{
		
		// (get) Token: 0x06002224 RID: 8740 RVA: 0x0015FEFB File Offset: 0x0015E0FB
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
