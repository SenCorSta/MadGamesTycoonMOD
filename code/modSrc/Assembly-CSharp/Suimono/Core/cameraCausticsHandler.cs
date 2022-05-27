using System;
using UnityEngine;

namespace Suimono.Core
{
	// Token: 0x020003A1 RID: 929
	[ExecuteInEditMode]
	public class cameraCausticsHandler : MonoBehaviour
	{
		// Token: 0x06002255 RID: 8789 RVA: 0x00016F85 File Offset: 0x00015185
		private void Start()
		{
			this.moduleObject = (SuimonoModule)UnityEngine.Object.FindObjectOfType(typeof(SuimonoModule));
			if (this.moduleObject != null)
			{
				this.causticLight = this.moduleObject.suimonoModuleLibrary.causticObjectLight;
			}
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x00016FC5 File Offset: 0x000151C5
		private void LateUpdate()
		{
			if (!Application.isPlaying)
			{
				this.causticLight.enabled = false;
			}
		}

		// Token: 0x06002257 RID: 8791 RVA: 0x0016971C File Offset: 0x0016791C
		private void OnPreCull()
		{
			if (this.causticLight != null)
			{
				if (this.moduleObject != null)
				{
					this.enableCaustics = this.moduleObject.enableCaustics;
					if (this.moduleObject.setLight != null && (!this.moduleObject.setLight.enabled || !this.moduleObject.setLight.gameObject.activeSelf))
					{
						this.enableCaustics = false;
					}
				}
				if (this.causticType == suiCausToolType.aboveWater)
				{
					this.causticLight.enabled = false;
				}
				else if (this.causticType == suiCausToolType.belowWater)
				{
					this.causticLight.enabled = this.enableCaustics;
				}
				else
				{
					this.causticLight.enabled = false;
				}
				if (this.isUnderwater)
				{
					this.causticLight.enabled = false;
				}
				if (!Application.isPlaying)
				{
					this.causticLight.enabled = false;
				}
			}
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x00169804 File Offset: 0x00167A04
		private void OnPostRender()
		{
			if (this.causticLight != null)
			{
				if (this.isUnderwater)
				{
					this.causticLight.enabled = true;
				}
				else
				{
					this.causticLight.enabled = false;
				}
				if (!Application.isPlaying)
				{
					this.causticLight.enabled = false;
				}
			}
		}

		// Token: 0x04002BF7 RID: 11255
		public bool isUnderwater;

		// Token: 0x04002BF8 RID: 11256
		public Light causticLight;

		// Token: 0x04002BF9 RID: 11257
		public suiCausToolType causticType;

		// Token: 0x04002BFA RID: 11258
		private bool enableCaustics = true;

		// Token: 0x04002BFB RID: 11259
		private SuimonoModule moduleObject;
	}
}
