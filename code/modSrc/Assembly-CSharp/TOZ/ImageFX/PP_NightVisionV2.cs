using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003CD RID: 973
	[ExecuteInEditMode]
	public sealed class PP_NightVisionV2 : PostProcessBase
	{
		// Token: 0x06002362 RID: 9058 RVA: 0x001704E4 File Offset: 0x0016E6E4
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/NightVisionV2");
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x001704F6 File Offset: 0x0016E6F6
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x0017050C File Offset: 0x0016E70C
		private void ApplyVariables()
		{
			if (this.NoiseTex != null)
			{
				this.mat.SetTexture("_NoiseTex", this.NoiseTex);
			}
			this.mat.SetVector("_VisionColor", this.VisionColor);
			this.mat.SetVector("_FadeColor", this.FadeColor);
			this.mat.SetFloat("_NoiseAmount", this.NoiseAmount);
			this.mat.SetFloat("_Radius", this.Radius);
			this.mat.SetFloat("_Fade", this.Fade);
			this.mat.SetFloat("_Intensity", this.Intensity);
			this.mat.SetFloat("_Gamma", this.Gamma);
		}

		// Token: 0x04002D4F RID: 11599
		public Texture2D NoiseTex;

		// Token: 0x04002D50 RID: 11600
		public Color VisionColor = Color.green;

		// Token: 0x04002D51 RID: 11601
		public Color FadeColor = Color.black;

		// Token: 0x04002D52 RID: 11602
		public float NoiseAmount = 1f;

		// Token: 0x04002D53 RID: 11603
		[Range(0f, 1f)]
		public float Radius = 0.5f;

		// Token: 0x04002D54 RID: 11604
		[Range(0f, 1f)]
		public float Fade = 0.2f;

		// Token: 0x04002D55 RID: 11605
		[Range(0f, 1f)]
		public float Intensity = 0.5f;

		// Token: 0x04002D56 RID: 11606
		[Range(0f, 2.2f)]
		public float Gamma = 2.2f;
	}
}
