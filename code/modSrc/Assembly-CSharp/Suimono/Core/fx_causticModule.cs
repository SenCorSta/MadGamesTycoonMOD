using System;
using UnityEngine;

namespace Suimono.Core
{
	// Token: 0x020003AB RID: 939
	public class fx_causticModule : MonoBehaviour
	{
		// Token: 0x060022D6 RID: 8918 RVA: 0x0016D8D8 File Offset: 0x0016BAD8
		private void Start()
		{
			this.moduleObject = (SuimonoModule)UnityEngine.Object.FindObjectOfType(typeof(SuimonoModule));
			this.lightObject = base.transform.Find("mainCausticObject").gameObject;
		}

		// Token: 0x060022D7 RID: 8919 RVA: 0x0016D910 File Offset: 0x0016BB10
		private void LateUpdate()
		{
			if (base.enabled)
			{
				this.useTex = this.causticFrames[this.frameIndex];
				this.causticsTime += Time.deltaTime;
				if (this.causticsTime > 1f / ((float)this.causticFPS * 1f))
				{
					this.causticsTime = 0f;
					this.frameIndex++;
				}
				if (this.frameIndex == this.causticFrames.Length)
				{
					this.frameIndex = 0;
				}
				if (this.moduleObject != null)
				{
					if (this.moduleObject.setLight != null)
					{
						this.sceneLightObject = this.moduleObject.setLight;
					}
					if (this.lightObject != null)
					{
						this.lightObject.SetActive(this.moduleObject.enableCaustics);
					}
				}
			}
		}

		// Token: 0x04002CBE RID: 11454
		public bool enableCaustics = true;

		// Token: 0x04002CBF RID: 11455
		public Light sceneLightObject;

		// Token: 0x04002CC0 RID: 11456
		public bool inheritLightColor;

		// Token: 0x04002CC1 RID: 11457
		public bool inheritLightDirection;

		// Token: 0x04002CC2 RID: 11458
		public Color causticTint = new Color(1f, 1f, 1f, 1f);

		// Token: 0x04002CC3 RID: 11459
		public float causticIntensity = 2f;

		// Token: 0x04002CC4 RID: 11460
		public float causticScale = 4f;

		// Token: 0x04002CC5 RID: 11461
		public float heightFac;

		// Token: 0x04002CC6 RID: 11462
		public int causticFPS = 32;

		// Token: 0x04002CC7 RID: 11463
		public Texture2D[] causticFrames;

		// Token: 0x04002CC8 RID: 11464
		public Texture2D useTex;

		// Token: 0x04002CC9 RID: 11465
		private float causticsTime;

		// Token: 0x04002CCA RID: 11466
		private SuimonoModule moduleObject;

		// Token: 0x04002CCB RID: 11467
		private GameObject lightObject;

		// Token: 0x04002CCC RID: 11468
		private int frameIndex;
	}
}
