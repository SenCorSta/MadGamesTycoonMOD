using System;
using UnityEngine;

namespace UntitledTools.VertexWind
{
	// Token: 0x020003B5 RID: 949
	public class WindEffectorRadius : MonoBehaviour
	{
		// Token: 0x06002306 RID: 8966 RVA: 0x00002715 File Offset: 0x00000915
		private void OnDrawGizmosSelected()
		{
		}

		// Token: 0x06002307 RID: 8967 RVA: 0x00002715 File Offset: 0x00000915
		private void OnDrawGizmos()
		{
		}

		// Token: 0x04002D25 RID: 11557
		[Tooltip("The radius of the effect multiplier")]
		public float radius = 10f;

		// Token: 0x04002D26 RID: 11558
		[Tooltip("The \"amount\" override used in the wind effector")]
		public Vector3 amount = Vector3.one * 0.5f;
	}
}
