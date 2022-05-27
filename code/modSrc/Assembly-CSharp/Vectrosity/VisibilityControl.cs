using System;
using System.Collections;
using UnityEngine;

namespace Vectrosity
{
	
	[AddComponentMenu("Vectrosity/VisibilityControl")]
	public class VisibilityControl : MonoBehaviour
	{
		
		// (get) Token: 0x0600220A RID: 8714 RVA: 0x0015FCC4 File Offset: 0x0015DEC4
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
			VectorManager.VisibilitySetup(base.transform, line, out this.m_objectNumber);
			this.m_vectorLine = line;
			VectorManager.DrawArrayLine2(this.m_objectNumber.i);
			base.StartCoroutine(this.VisibilityTest());
		}

		
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

		
		private IEnumerator OnBecameVisible()
		{
			yield return new WaitForEndOfFrame();
			this.m_vectorLine.active = true;
			yield break;
		}

		
		private IEnumerator OnBecameInvisible()
		{
			yield return new WaitForEndOfFrame();
			this.m_vectorLine.active = false;
			yield break;
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
