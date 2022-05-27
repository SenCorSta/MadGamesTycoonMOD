using System;
using System.Collections;
using UnityEngine;

namespace AllIn1SpriteShader
{
	// Token: 0x020003FB RID: 1019
	public class DemoCamera : MonoBehaviour
	{
		// Token: 0x060023E2 RID: 9186 RVA: 0x000186B6 File Offset: 0x000168B6
		private void Awake()
		{
			this.offset = base.transform.position - this.targetedItem.position;
			base.StartCoroutine(this.SetCamAfterStart());
		}

		// Token: 0x060023E3 RID: 9187 RVA: 0x00171924 File Offset: 0x0016FB24
		private void Update()
		{
			if (!this.canUpdate)
			{
				return;
			}
			this.target.y = (float)this.demoController.GetCurrExpositor() * this.demoController.expositorDistance;
			base.transform.position = Vector3.Lerp(base.transform.position, this.target, this.speed * Time.deltaTime);
		}

		// Token: 0x060023E4 RID: 9188 RVA: 0x000186E6 File Offset: 0x000168E6
		private IEnumerator SetCamAfterStart()
		{
			yield return null;
			base.transform.position = this.targetedItem.position + this.offset;
			this.target = base.transform.position;
			this.canUpdate = true;
			yield break;
		}

		// Token: 0x04002E3B RID: 11835
		[SerializeField]
		private Transform targetedItem;

		// Token: 0x04002E3C RID: 11836
		[SerializeField]
		private All1ShaderDemoController demoController;

		// Token: 0x04002E3D RID: 11837
		[SerializeField]
		private float speed;

		// Token: 0x04002E3E RID: 11838
		private Vector3 offset;

		// Token: 0x04002E3F RID: 11839
		private Vector3 target;

		// Token: 0x04002E40 RID: 11840
		private bool canUpdate;
	}
}
