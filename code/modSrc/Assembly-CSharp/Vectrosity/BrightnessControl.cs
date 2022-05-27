using System;
using UnityEngine;

namespace Vectrosity
{
	
	[AddComponentMenu("Vectrosity/BrightnessControl")]
	public class BrightnessControl : MonoBehaviour
	{
		
		// (get) Token: 0x06002012 RID: 8210 RVA: 0x00015401 File Offset: 0x00013601
		public RefInt objectNumber
		{
			get
			{
				return this.m_objectNumber;
			}
		}

		
		public void Setup(VectorLine line, bool m_useLine)
		{
			this.m_objectNumber = new RefInt(0);
			VectorManager.CheckDistanceSetup(base.transform, line, line.color, this.m_objectNumber);
			VectorManager.SetDistanceColor(this.m_objectNumber.i);
			if (m_useLine)
			{
				this.m_useLine = true;
				this.m_vectorLine = line;
			}
		}

		
		public void SetUseLine(bool useLine)
		{
			this.m_useLine = useLine;
		}

		
		private void OnBecameVisible()
		{
			VectorManager.SetOldDistance(this.m_objectNumber.i, -1);
			VectorManager.SetDistanceColor(this.m_objectNumber.i);
			if (!this.m_useLine)
			{
				return;
			}
			this.m_vectorLine.active = true;
		}

		
		public void OnBecameInvisible()
		{
			if (!this.m_useLine)
			{
				return;
			}
			this.m_vectorLine.active = false;
		}

		
		private void OnDestroy()
		{
			if (this.m_destroyed)
			{
				return;
			}
			this.m_destroyed = true;
			VectorManager.DistanceRemove(this.m_objectNumber.i);
			if (this.m_useLine)
			{
				VectorLine.Destroy(ref this.m_vectorLine);
			}
		}

		
		private RefInt m_objectNumber;

		
		private VectorLine m_vectorLine;

		
		private bool m_useLine;

		
		private bool m_destroyed;
	}
}
