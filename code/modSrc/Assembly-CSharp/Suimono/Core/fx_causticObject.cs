using System;
using UnityEngine;

namespace Suimono.Core
{
	// Token: 0x020003AC RID: 940
	public class fx_causticObject : MonoBehaviour
	{
		// Token: 0x060022D9 RID: 8921 RVA: 0x0016DA47 File Offset: 0x0016BC47
		private void Start()
		{
			this.moduleObject = GameObject.Find("SUIMONO_Module").GetComponent<SuimonoModule>();
			this.causticObject = GameObject.Find("_caustic_effects").GetComponent<fx_causticModule>();
			this.lightComponent = base.GetComponent<Light>();
		}

		// Token: 0x060022DA RID: 8922 RVA: 0x0016DA80 File Offset: 0x0016BC80
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

		// Token: 0x04002CCD RID: 11469
		public bool manualPlacement;

		// Token: 0x04002CCE RID: 11470
		private SuimonoModule moduleObject;

		// Token: 0x04002CCF RID: 11471
		private fx_causticModule causticObject;

		// Token: 0x04002CD0 RID: 11472
		private Light lightComponent;

		// Token: 0x04002CD1 RID: 11473
		private float heightMult = 1f;
	}
}
