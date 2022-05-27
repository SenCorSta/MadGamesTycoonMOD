using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003B3 RID: 947
	[ExecuteInEditMode]
	public sealed class PP_4Bit : PostProcessBase
	{
		// Token: 0x060022B6 RID: 8886 RVA: 0x000172D4 File Offset: 0x000154D4
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/4Bit");
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x000172E6 File Offset: 0x000154E6
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x000172FB File Offset: 0x000154FB
		private void ApplyVariables()
		{
			this.mat.SetInt("_BitDepth", this.BitDepth);
			this.mat.SetFloat("_Contrast", this.Contrast);
		}

		// Token: 0x04002D11 RID: 11537
		public int BitDepth = 2;

		// Token: 0x04002D12 RID: 11538
		[Range(0f, 1f)]
		public float Contrast = 1f;
	}
}
