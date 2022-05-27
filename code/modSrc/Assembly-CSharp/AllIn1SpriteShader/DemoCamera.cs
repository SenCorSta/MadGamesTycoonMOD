using System;
using System.Collections;
using UnityEngine;

namespace AllIn1SpriteShader
{
	// Token: 0x020003FE RID: 1022
	public class DemoCamera : MonoBehaviour
	{
		// Token: 0x06002435 RID: 9269 RVA: 0x001748BA File Offset: 0x00172ABA
		private void Awake()
		{
			this.offset = base.transform.position - this.targetedItem.position;
			base.StartCoroutine(this.SetCamAfterStart());
		}

		// Token: 0x06002436 RID: 9270 RVA: 0x001748EC File Offset: 0x00172AEC
		private void Update()
		{
			if (!this.canUpdate)
			{
				return;
			}
			this.target.y = (float)this.demoController.GetCurrExpositor() * this.demoController.expositorDistance;
			base.transform.position = Vector3.Lerp(base.transform.position, this.target, this.speed * Time.deltaTime);
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x00174952 File Offset: 0x00172B52
		private IEnumerator SetCamAfterStart()
		{
			yield return null;
			base.transform.position = this.targetedItem.position + this.offset;
			this.target = base.transform.position;
			this.canUpdate = true;
			yield break;
		}

		// Token: 0x04002E51 RID: 11857
		[SerializeField]
		private Transform targetedItem;

		// Token: 0x04002E52 RID: 11858
		[SerializeField]
		private All1ShaderDemoController demoController;

		// Token: 0x04002E53 RID: 11859
		[SerializeField]
		private float speed;

		// Token: 0x04002E54 RID: 11860
		private Vector3 offset;

		// Token: 0x04002E55 RID: 11861
		private Vector3 target;

		// Token: 0x04002E56 RID: 11862
		private bool canUpdate;
	}
}
