using System;
using UnityEngine;

namespace AllIn1SpriteShader
{
	// Token: 0x020003FE RID: 1022
	public class DemoItem : MonoBehaviour
	{
		// Token: 0x060023F0 RID: 9200 RVA: 0x00018759 File Offset: 0x00016959
		private void Update()
		{
			base.transform.LookAt(base.transform.position + DemoItem.lookAtZ);
		}

		// Token: 0x04002E4D RID: 11853
		private static Vector3 lookAtZ = new Vector3(0f, 0f, 1f);
	}
}
