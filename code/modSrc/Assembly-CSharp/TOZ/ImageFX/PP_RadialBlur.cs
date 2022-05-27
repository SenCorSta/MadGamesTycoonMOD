using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003D2 RID: 978
	[ExecuteInEditMode]
	public sealed class PP_RadialBlur : PostProcessBase
	{
		// Token: 0x06002376 RID: 9078 RVA: 0x00170835 File Offset: 0x0016EA35
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/RadialBlur");
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x00170847 File Offset: 0x0016EA47
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x0017085C File Offset: 0x0016EA5C
		private void ApplyVariables()
		{
			this.mat.SetFloat("_CenterX", this.CenterX);
			this.mat.SetFloat("_CenterY", this.CenterY);
			this.mat.SetFloat("_Strength", this.Strength);
		}

		// Token: 0x04002D60 RID: 11616
		[Range(0f, 1f)]
		public float CenterX = 0.5f;

		// Token: 0x04002D61 RID: 11617
		[Range(0f, 1f)]
		public float CenterY = 0.5f;

		// Token: 0x04002D62 RID: 11618
		public float Strength = 0.2f;
	}
}
