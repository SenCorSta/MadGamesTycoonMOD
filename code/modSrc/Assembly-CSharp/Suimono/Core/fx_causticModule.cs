using System;
using UnityEngine;

namespace Suimono.Core
{
	// Token: 0x020003A8 RID: 936
	public class fx_causticModule : MonoBehaviour
	{
		// Token: 0x06002283 RID: 8835 RVA: 0x00017087 File Offset: 0x00015287
		private void Start()
		{
			this.moduleObject = (SuimonoModule)UnityEngine.Object.FindObjectOfType(typeof(SuimonoModule));
			this.lightObject = base.transform.Find("mainCausticObject").gameObject;
		}

		// Token: 0x06002284 RID: 8836 RVA: 0x0016C030 File Offset: 0x0016A230
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

		// Token: 0x04002CA8 RID: 11432
		public bool enableCaustics = true;

		// Token: 0x04002CA9 RID: 11433
		public Light sceneLightObject;

		// Token: 0x04002CAA RID: 11434
		public bool inheritLightColor;

		// Token: 0x04002CAB RID: 11435
		public bool inheritLightDirection;

		// Token: 0x04002CAC RID: 11436
		public Color causticTint = new Color(1f, 1f, 1f, 1f);

		// Token: 0x04002CAD RID: 11437
		public float causticIntensity = 2f;

		// Token: 0x04002CAE RID: 11438
		public float causticScale = 4f;

		// Token: 0x04002CAF RID: 11439
		public float heightFac;

		// Token: 0x04002CB0 RID: 11440
		public int causticFPS = 32;

		// Token: 0x04002CB1 RID: 11441
		public Texture2D[] causticFrames;

		// Token: 0x04002CB2 RID: 11442
		public Texture2D useTex;

		// Token: 0x04002CB3 RID: 11443
		private float causticsTime;

		// Token: 0x04002CB4 RID: 11444
		private SuimonoModule moduleObject;

		// Token: 0x04002CB5 RID: 11445
		private GameObject lightObject;

		// Token: 0x04002CB6 RID: 11446
		private int frameIndex;
	}
}
