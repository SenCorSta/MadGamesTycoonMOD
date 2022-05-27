using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003DA RID: 986
	[ExecuteInEditMode]
	public sealed class PP_Tonemap : PostProcessBase
	{
		// Token: 0x0600234F RID: 9039 RVA: 0x00017F9C File Offset: 0x0001619C
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Tonemap");
		}

		// Token: 0x06002350 RID: 9040 RVA: 0x00017FAE File Offset: 0x000161AE
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x06002351 RID: 9041 RVA: 0x00017FC3 File Offset: 0x000161C3
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Exposure", this.Exposure);
			this.mat.SetFloat("_Gamma", this.Gamma);
		}

		// Token: 0x04002D66 RID: 11622
		[Range(0f, 1f)]
		public float Exposure = 0.1f;

		// Token: 0x04002D67 RID: 11623
		[Range(0f, 2.2f)]
		public float Gamma = 1f;
	}
}
