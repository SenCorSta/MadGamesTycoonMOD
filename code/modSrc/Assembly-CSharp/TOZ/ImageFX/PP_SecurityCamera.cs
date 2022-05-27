using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003D4 RID: 980
	[ExecuteInEditMode]
	public sealed class PP_SecurityCamera : PostProcessBase
	{
		// Token: 0x0600237E RID: 9086 RVA: 0x001709B6 File Offset: 0x0016EBB6
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/SecurityCamera");
		}

		// Token: 0x0600237F RID: 9087 RVA: 0x001709C8 File Offset: 0x0016EBC8
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x06002380 RID: 9088 RVA: 0x001709E0 File Offset: 0x0016EBE0
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Speed", this.Speed);
			this.mat.SetFloat("_Thickness", this.Thickness);
			this.mat.SetFloat("_Luminance", this.Luminance);
			this.mat.SetFloat("_Darkness", this.Darkness);
		}

		// Token: 0x04002D68 RID: 11624
		public float Speed = 2f;

		// Token: 0x04002D69 RID: 11625
		[Range(0f, 1f)]
		public float Thickness = 0.25f;

		// Token: 0x04002D6A RID: 11626
		[Range(0f, 1f)]
		public float Luminance = 0.25f;

		// Token: 0x04002D6B RID: 11627
		[Range(0f, 1f)]
		public float Darkness = 0.75f;
	}
}
