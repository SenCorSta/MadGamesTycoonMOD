using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003CF RID: 975
	[ExecuteInEditMode]
	public sealed class PP_Pixelated : PostProcessBase
	{
		// Token: 0x0600236A RID: 9066 RVA: 0x00170696 File Offset: 0x0016E896
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Pixelated");
		}

		// Token: 0x0600236B RID: 9067 RVA: 0x001706A8 File Offset: 0x0016E8A8
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x0600236C RID: 9068 RVA: 0x001706BD File Offset: 0x0016E8BD
		private void ApplyVariables()
		{
			this.mat.SetFloat("_PixWidth", (float)this.PixelWidth);
			this.mat.SetFloat("_PixHeight", (float)this.PixelHeight);
		}

		// Token: 0x04002D58 RID: 11608
		public int PixelWidth = 16;

		// Token: 0x04002D59 RID: 11609
		public int PixelHeight = 16;
	}
}
