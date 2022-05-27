using System;
using UnityEngine;

namespace Suimono.Core
{
	// Token: 0x020003A9 RID: 937
	public class fx_causticObject : MonoBehaviour
	{
		// Token: 0x06002286 RID: 8838 RVA: 0x000170BE File Offset: 0x000152BE
		private void Start()
		{
			this.moduleObject = GameObject.Find("SUIMONO_Module").GetComponent<SuimonoModule>();
			this.causticObject = GameObject.Find("_caustic_effects").GetComponent<fx_causticModule>();
			this.lightComponent = base.GetComponent<Light>();
		}

		// Token: 0x06002287 RID: 8839 RVA: 0x0016C168 File Offset: 0x0016A368
		private void LateUpdate()
		{
			if (this.causticObject.enableCaustics)
			{
				this.lightComponent.cookie = this.causticObject.useTex;
				this.lightComponent.cullingMask = this.moduleObject.causticLayer;
				this.lightComponent.color = this.causticObject.causticTint;
				this.heightMult = 1f;
				if (this.manualPlacement)
				{
					this.heightMult = 1f - this.causticObject.heightFac;
				}
				this.lightComponent.intensity = this.causticObject.causticIntensity * this.heightMult;
				this.lightComponent.cookieSize = this.causticObject.causticScale;
				if (this.causticObject.sceneLightObject != null)
				{
					if (this.causticObject.inheritLightColor)
					{
						this.lightComponent.color = this.causticObject.sceneLightObject.color * this.causticObject.causticTint;
						this.lightComponent.intensity = this.lightComponent.intensity * this.causticObject.sceneLightObject.intensity;
					}
					else
					{
						this.lightComponent.color = this.causticObject.causticTint;
					}
					if (this.causticObject.inheritLightDirection)
					{
						base.transform.eulerAngles = this.causticObject.sceneLightObject.transform.eulerAngles;
						return;
					}
					base.transform.eulerAngles = new Vector3(90f, 0f, 0f);
				}
			}
		}

		// Token: 0x04002CB7 RID: 11447
		public bool manualPlacement;

		// Token: 0x04002CB8 RID: 11448
		private SuimonoModule moduleObject;

		// Token: 0x04002CB9 RID: 11449
		private fx_causticModule causticObject;

		// Token: 0x04002CBA RID: 11450
		private Light lightComponent;

		// Token: 0x04002CBB RID: 11451
		private float heightMult = 1f;
	}
}
