using System;
using UnityEngine;

namespace Suimono.Core
{
	
	public class fx_causticObject : MonoBehaviour
	{
		
		private void Start()
		{
			this.moduleObject = GameObject.Find("SUIMONO_Module").GetComponent<SuimonoModule>();
			this.causticObject = GameObject.Find("_caustic_effects").GetComponent<fx_causticModule>();
			this.lightComponent = base.GetComponent<Light>();
		}

		
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

		
		public bool manualPlacement;

		
		private SuimonoModule moduleObject;

		
		private fx_causticModule causticObject;

		
		private Light lightComponent;

		
		private float heightMult = 1f;
	}
}
