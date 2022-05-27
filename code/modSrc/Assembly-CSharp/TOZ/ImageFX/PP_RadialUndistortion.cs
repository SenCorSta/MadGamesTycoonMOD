using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003D0 RID: 976
	[ExecuteInEditMode]
	public sealed class PP_RadialUndistortion : PostProcessBase
	{
		// Token: 0x06002327 RID: 8999 RVA: 0x00017C87 File Offset: 0x00015E87
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/RadialUndistortion");
		}

		// Token: 0x06002328 RID: 9000 RVA: 0x00017C99 File Offset: 0x00015E99
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x06002329 RID: 9001 RVA: 0x0016E434 File Offset: 0x0016C634
		private void ApplyVariables()
		{
			this.mat.SetFloat("_F", this.F);
			this.mat.SetFloat("_OX", this.CenterX);
			this.mat.SetFloat("_OY", this.CenterY);
			this.mat.SetFloat("_K1", this.K1);
			this.mat.SetFloat("_K2", this.K2);
		}

		// Token: 0x04002D4D RID: 11597
		public float CenterX = 320f;

		// Token: 0x04002D4E RID: 11598
		public float CenterY = 240f;

		// Token: 0x04002D4F RID: 11599
		public float F = 0.9f;

		// Token: 0x04002D50 RID: 11600
		public float K1 = -0.27f;

		// Token: 0x04002D51 RID: 11601
		public float K2 = 0.08f;
	}
}
