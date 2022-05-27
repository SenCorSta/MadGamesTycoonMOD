using System;
using UnityEngine;

namespace AllIn1SpriteShader
{
	// Token: 0x02000401 RID: 1025
	public class DemoItem : MonoBehaviour
	{
		// Token: 0x06002443 RID: 9283 RVA: 0x00174C26 File Offset: 0x00172E26
		private void Update()
		{
			base.transform.LookAt(base.transform.position + DemoItem.lookAtZ);
		}

		// Token: 0x04002E63 RID: 11875
		private static Vector3 lookAtZ = new Vector3(0f, 0f, 1f);
	}
}
