using System;
using System.Collections;
using UnityEngine;

namespace AllIn1SpriteShader
{
	// Token: 0x020003FC RID: 1020
	public class Demo2AutoScroll : MonoBehaviour
	{
		// Token: 0x0600242C RID: 9260 RVA: 0x00174714 File Offset: 0x00172914
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

		// Token: 0x0600242D RID: 9261 RVA: 0x001747BD File Offset: 0x001729BD
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

		// Token: 0x04002E4A RID: 11850
		private Transform[] children;

		// Token: 0x04002E4B RID: 11851
		public float totalTime;

		// Token: 0x04002E4C RID: 11852
		public GameObject sceneDescription;
	}
}
