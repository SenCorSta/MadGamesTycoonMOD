using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003CA RID: 970
	[ExecuteInEditMode]
	public sealed class PP_NightVisionV2 : PostProcessBase
	{
		// Token: 0x0600230F RID: 8975 RVA: 0x00017A85 File Offset: 0x00015C85
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/NightVisionV2");
		}

		// Token: 0x06002310 RID: 8976 RVA: 0x00017A97 File Offset: 0x00015C97
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x06002311 RID: 8977 RVA: 0x0016E244 File Offset: 0x0016C444
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

		// Token: 0x04002D39 RID: 11577
		public Texture2D NoiseTex;

		// Token: 0x04002D3A RID: 11578
		public Color VisionColor = Color.green;

		// Token: 0x04002D3B RID: 11579
		public Color FadeColor = Color.black;

		// Token: 0x04002D3C RID: 11580
		public float NoiseAmount = 1f;

		// Token: 0x04002D3D RID: 11581
		[Range(0f, 1f)]
		public float Radius = 0.5f;

		// Token: 0x04002D3E RID: 11582
		[Range(0f, 1f)]
		public float Fade = 0.2f;

		// Token: 0x04002D3F RID: 11583
		[Range(0f, 1f)]
		public float Intensity = 0.5f;

		// Token: 0x04002D40 RID: 11584
		[Range(0f, 2.2f)]
		public float Gamma = 2.2f;
	}
}
