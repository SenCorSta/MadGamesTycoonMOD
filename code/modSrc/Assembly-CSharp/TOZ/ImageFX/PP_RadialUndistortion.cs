using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003D3 RID: 979
	[ExecuteInEditMode]
	public sealed class PP_RadialUndistortion : PostProcessBase
	{
		// Token: 0x0600237A RID: 9082 RVA: 0x001708D4 File Offset: 0x0016EAD4
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/RadialUndistortion");
		}

		// Token: 0x0600237B RID: 9083 RVA: 0x001708E6 File Offset: 0x0016EAE6
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x0600237C RID: 9084 RVA: 0x001708FC File Offset: 0x0016EAFC
		private void ApplyVariables()
		{
			this.mat.SetFloat("_F", this.F);
			this.mat.SetFloat("_OX", this.CenterX);
			this.mat.SetFloat("_OY", this.CenterY);
			this.mat.SetFloat("_K1", this.K1);
			this.mat.SetFloat("_K2", this.K2);
		}

		// Token: 0x04002D63 RID: 11619
		public float CenterX = 320f;

		// Token: 0x04002D64 RID: 11620
		public float CenterY = 240f;

		// Token: 0x04002D65 RID: 11621
		public float F = 0.9f;

		// Token: 0x04002D66 RID: 11622
		public float K1 = -0.27f;

		// Token: 0x04002D67 RID: 11623
		public float K2 = 0.08f;
	}
}
