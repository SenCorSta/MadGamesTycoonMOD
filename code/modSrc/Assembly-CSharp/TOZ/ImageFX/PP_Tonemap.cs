using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003DD RID: 989
	[ExecuteInEditMode]
	public sealed class PP_Tonemap : PostProcessBase
	{
		// Token: 0x060023A2 RID: 9122 RVA: 0x001710ED File Offset: 0x0016F2ED
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Tonemap");
		}

		// Token: 0x060023A3 RID: 9123 RVA: 0x001710FF File Offset: 0x0016F2FF
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x00171114 File Offset: 0x0016F314
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Exposure", this.Exposure);
			this.mat.SetFloat("_Gamma", this.Gamma);
		}

		// Token: 0x04002D7C RID: 11644
		[Range(0f, 1f)]
		public float Exposure = 0.1f;

		// Token: 0x04002D7D RID: 11645
		[Range(0f, 2.2f)]
		public float Gamma = 1f;
	}
}
