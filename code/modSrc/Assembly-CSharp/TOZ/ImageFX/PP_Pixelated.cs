using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003CC RID: 972
	[ExecuteInEditMode]
	public sealed class PP_Pixelated : PostProcessBase
	{
		// Token: 0x06002317 RID: 8983 RVA: 0x00017AFE File Offset: 0x00015CFE
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Pixelated");
		}

		// Token: 0x06002318 RID: 8984 RVA: 0x00017B10 File Offset: 0x00015D10
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x06002319 RID: 8985 RVA: 0x00017B25 File Offset: 0x00015D25
		private void ApplyVariables()
		{
			this.mat.SetFloat("_PixWidth", (float)this.PixelWidth);
			this.mat.SetFloat("_PixHeight", (float)this.PixelHeight);
		}

		// Token: 0x04002D42 RID: 11586
		public int PixelWidth = 16;

		// Token: 0x04002D43 RID: 11587
		public int PixelHeight = 16;
	}
}
