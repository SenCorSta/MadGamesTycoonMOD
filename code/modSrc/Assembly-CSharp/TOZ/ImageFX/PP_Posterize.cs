using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003D0 RID: 976
	[ExecuteInEditMode]
	public sealed class PP_Posterize : PostProcessBase
	{
		// Token: 0x0600236E RID: 9070 RVA: 0x00170705 File Offset: 0x0016E905
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Posterize");
		}

		// Token: 0x0600236F RID: 9071 RVA: 0x00170717 File Offset: 0x0016E917
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x0017072C File Offset: 0x0016E92C
		private void ApplyVariables()
		{
			this.mat.SetInt("_Colors", this.Colors);
			this.mat.SetFloat("_Gamma", this.Gamma);
		}

		// Token: 0x04002D5A RID: 11610
		public int Colors = 4;

		// Token: 0x04002D5B RID: 11611
		[Range(0f, 2.2f)]
		public float Gamma = 1f;
	}
}
