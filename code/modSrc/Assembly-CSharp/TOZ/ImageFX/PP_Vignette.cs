using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003DE RID: 990
	[ExecuteInEditMode]
	public sealed class PP_Vignette : PostProcessBase
	{
		// Token: 0x060023A6 RID: 9126 RVA: 0x00171160 File Offset: 0x0016F360
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Vignette");
		}

		// Token: 0x060023A7 RID: 9127 RVA: 0x00171172 File Offset: 0x0016F372
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x060023A8 RID: 9128 RVA: 0x00171187 File Offset: 0x0016F387
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Radius", this.Radius);
			this.mat.SetFloat("_Darkness", this.Darkness);
		}

		// Token: 0x04002D7E RID: 11646
		public float Radius = 10f;

		// Token: 0x04002D7F RID: 11647
		public float Darkness = 0.5f;
	}
}
