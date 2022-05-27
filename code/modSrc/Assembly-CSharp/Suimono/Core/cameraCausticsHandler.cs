using System;
using UnityEngine;

namespace Suimono.Core
{
	
	[ExecuteInEditMode]
	public class cameraCausticsHandler : MonoBehaviour
	{
		
		private void Start()
		{
			this.moduleObject = (SuimonoModule)UnityEngine.Object.FindObjectOfType(typeof(SuimonoModule));
			if (this.moduleObject != null)
			{
				this.causticLight = this.moduleObject.suimonoModuleLibrary.causticObjectLight;
			}
		}

		
		private void LateUpdate()
		{
			if (!Application.isPlaying)
			{
				this.causticLight.enabled = false;
			}
		}

		
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

		
		public bool isUnderwater;

		
		public Light causticLight;

		
		public suiCausToolType causticType;

		
		private bool enableCaustics = true;

		
		private SuimonoModule moduleObject;
	}
}
