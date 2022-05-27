using System;
using System.Collections;
using UnityEngine;

namespace AllIn1SpriteShader
{
	// Token: 0x020003F9 RID: 1017
	public class Demo2AutoScroll : MonoBehaviour
	{
		// Token: 0x060023D9 RID: 9177 RVA: 0x001717A0 File Offset: 0x0016F9A0
		private void Start()
		{
			this.sceneDescription.SetActive(false);
			Camera.main.fieldOfView = 60f;
			this.children = base.GetComponentsInChildren<Transform>();
			for (int i = 0; i < this.children.Length; i++)
			{
				if (this.children[i].gameObject != base.gameObject)
				{
					this.children[i].gameObject.SetActive(false);
					this.children[i].localPosition = Vector3.zero;
				}
			}
			this.totalTime /= (float)this.children.Length;
			base.StartCoroutine(this.ScrollElements());
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x00018690 File Offset: 0x00016890
		private IEnumerator ScrollElements()
		{
			int i = 0;
			for (;;)
			{
				if (this.children[i].gameObject == base.gameObject)
				{
					i = (i + 1) % this.children.Length;
				}
				else
				{
					this.children[i].gameObject.SetActive(true);
					yield return new WaitForSeconds(this.totalTime);
					this.children[i].gameObject.SetActive(false);
					i = (i + 1) % this.children.Length;
				}
			}
			yield break;
		}

		// Token: 0x04002E34 RID: 11828
		private Transform[] children;

		// Token: 0x04002E35 RID: 11829
		public float totalTime;

		// Token: 0x04002E36 RID: 11830
		public GameObject sceneDescription;
	}
}
