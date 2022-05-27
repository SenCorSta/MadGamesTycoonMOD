using System;
using UnityEngine;

namespace Suimono.Core
{
	// Token: 0x020003A4 RID: 932
	[ExecuteInEditMode]
	public class cameraCausticsHandler : MonoBehaviour
	{
		// Token: 0x060022A8 RID: 8872 RVA: 0x0016AEC1 File Offset: 0x001690C1
		private void Start()
		{
			this.moduleObject = (SuimonoModule)UnityEngine.Object.FindObjectOfType(typeof(SuimonoModule));
			if (this.moduleObject != null)
			{
				this.causticLight = this.moduleObject.suimonoModuleLibrary.causticObjectLight;
			}
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x0016AF01 File Offset: 0x00169101
		private void LateUpdate()
		{
			if (!Application.isPlaying)
			{
				this.causticLight.enabled = false;
			}
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x0016AF18 File Offset: 0x00169118
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

		// Token: 0x060022AB RID: 8875 RVA: 0x0016B000 File Offset: 0x00169200
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

		// Token: 0x04002C0D RID: 11277
		public bool isUnderwater;

		// Token: 0x04002C0E RID: 11278
		public Light causticLight;

		// Token: 0x04002C0F RID: 11279
		public suiCausToolType causticType;

		// Token: 0x04002C10 RID: 11280
		private bool enableCaustics = true;

		// Token: 0x04002C11 RID: 11281
		private SuimonoModule moduleObject;
	}
}
