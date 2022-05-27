using System;
using UnityEngine;

namespace AllIn1SpriteShader
{
	// Token: 0x02000400 RID: 1024
	public class DemoRepositionExpositor : MonoBehaviour
	{
		// Token: 0x060023F7 RID: 9207 RVA: 0x00171C9C File Offset: 0x0016FE9C
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

		// Token: 0x04002E4F RID: 11855
		[SerializeField]
		private float paddingX = 10f;
	}
}
