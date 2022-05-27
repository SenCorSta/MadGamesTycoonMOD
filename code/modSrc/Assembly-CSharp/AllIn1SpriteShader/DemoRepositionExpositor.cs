using System;
using UnityEngine;

namespace AllIn1SpriteShader
{
	// Token: 0x02000403 RID: 1027
	public class DemoRepositionExpositor : MonoBehaviour
	{
		// Token: 0x0600244A RID: 9290 RVA: 0x00174D4C File Offset: 0x00172F4C
		[ContextMenu("RepositionExpositor")]
		private void RepositionExpositor()
		{
			int num = 0;
			Vector3 zero = Vector3.zero;
			foreach (object obj in base.transform)
			{
				Transform transform = (Transform)obj;
				zero.x = (float)num * this.paddingX;
				transform.localPosition = zero;
				num++;
			}
		}

		// Token: 0x04002E65 RID: 11877
		[SerializeField]
		private float paddingX = 10f;
	}
}
