using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003D1 RID: 977
	[ExecuteInEditMode]
	public sealed class PP_SecurityCamera : PostProcessBase
	{
		// Token: 0x0600232B RID: 9003 RVA: 0x00017CED File Offset: 0x00015EED
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/SecurityCamera");
		}

		// Token: 0x0600232C RID: 9004 RVA: 0x00017CFF File Offset: 0x00015EFF
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x0600232D RID: 9005 RVA: 0x0016E4B0 File Offset: 0x0016C6B0
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Speed", this.Speed);
			this.mat.SetFloat("_Thickness", this.Thickness);
			this.mat.SetFloat("_Luminance", this.Luminance);
			this.mat.SetFloat("_Darkness", this.Darkness);
		}

		// Token: 0x04002D52 RID: 11602
		public float Speed = 2f;

		// Token: 0x04002D53 RID: 11603
		[Range(0f, 1f)]
		public float Thickness = 0.25f;

		// Token: 0x04002D54 RID: 11604
		[Range(0f, 1f)]
		public float Luminance = 0.25f;

		// Token: 0x04002D55 RID: 11605
		[Range(0f, 1f)]
		public float Darkness = 0.75f;
	}
}
