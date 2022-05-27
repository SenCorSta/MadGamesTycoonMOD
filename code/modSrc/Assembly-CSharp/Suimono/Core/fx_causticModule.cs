using System;
using UnityEngine;

namespace Suimono.Core
{
	
	public class fx_causticModule : MonoBehaviour
	{
		
		private void Start()
		{
			this.moduleObject = (SuimonoModule)UnityEngine.Object.FindObjectOfType(typeof(SuimonoModule));
			this.lightObject = base.transform.Find("mainCausticObject").gameObject;
		}

		
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

		
		public bool enableCaustics = true;

		
		public Light sceneLightObject;

		
		public bool inheritLightColor;

		
		public bool inheritLightDirection;

		
		public Color causticTint = new Color(1f, 1f, 1f, 1f);

		
		public float causticIntensity = 2f;

		
		public float causticScale = 4f;

		
		public float heightFac;

		
		public int causticFPS = 32;

		
		public Texture2D[] causticFrames;

		
		public Texture2D useTex;

		
		private float causticsTime;

		
		private SuimonoModule moduleObject;

		
		private GameObject lightObject;

		
		private int frameIndex;
	}
}
