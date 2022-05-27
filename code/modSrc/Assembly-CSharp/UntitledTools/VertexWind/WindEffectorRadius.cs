using System;
using UnityEngine;

namespace UntitledTools.VertexWind
{
	// Token: 0x020003B2 RID: 946
	public class WindEffectorRadius : MonoBehaviour
	{
		// Token: 0x060022B3 RID: 8883 RVA: 0x00002098 File Offset: 0x00000298
		private void OnDrawGizmosSelected()
		{
		}

		// Token: 0x060022B4 RID: 8884 RVA: 0x00002098 File Offset: 0x00000298
		private void OnDrawGizmos()
		{
		}

		// Token: 0x04002D0F RID: 11535
		[Tooltip("The radius of the effect multiplier")]
		public float radius = 10f;

		// Token: 0x04002D10 RID: 11536
		[Tooltip("The \"amount\" override used in the wind effector")]
		public Vector3 amount = Vector3.one * 0.5f;
	}
}
