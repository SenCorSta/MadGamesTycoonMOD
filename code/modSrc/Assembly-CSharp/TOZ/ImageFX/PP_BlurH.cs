using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003BA RID: 954
	[ExecuteInEditMode]
	public sealed class PP_BlurH : PostProcessBase
	{
		// Token: 0x06002319 RID: 8985 RVA: 0x0016F977 File Offset: 0x0016DB77
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/BlurH");
		}

		// Token: 0x0600231A RID: 8986 RVA: 0x0016F989 File Offset: 0x0016DB89
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x0600231B RID: 8987 RVA: 0x0016F99E File Offset: 0x0016DB9E
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Strength", this.Strength);
		}

		// Token: 0x04002D2D RID: 11565
		public float Strength = 1f;
	}
}
