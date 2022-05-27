using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003CF RID: 975
	[ExecuteInEditMode]
	public sealed class PP_RadialBlur : PostProcessBase
	{
		// Token: 0x06002323 RID: 8995 RVA: 0x00017C37 File Offset: 0x00015E37
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/RadialBlur");
		}

		// Token: 0x06002324 RID: 8996 RVA: 0x00017C49 File Offset: 0x00015E49
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x06002325 RID: 8997 RVA: 0x0016E3E4 File Offset: 0x0016C5E4
		private void ApplyVariables()
		{
			this.mat.SetFloat("_CenterX", this.CenterX);
			this.mat.SetFloat("_CenterY", this.CenterY);
			this.mat.SetFloat("_Strength", this.Strength);
		}

		// Token: 0x04002D4A RID: 11594
		[Range(0f, 1f)]
		public float CenterX = 0.5f;

		// Token: 0x04002D4B RID: 11595
		[Range(0f, 1f)]
		public float CenterY = 0.5f;

		// Token: 0x04002D4C RID: 11596
		public float Strength = 0.2f;
	}
}
