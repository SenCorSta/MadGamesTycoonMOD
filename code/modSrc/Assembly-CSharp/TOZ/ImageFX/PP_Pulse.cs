using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003D1 RID: 977
	[ExecuteInEditMode]
	public sealed class PP_Pulse : PostProcessBase
	{
		// Token: 0x06002372 RID: 9074 RVA: 0x00170774 File Offset: 0x0016E974
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Pulse");
		}

		// Token: 0x06002373 RID: 9075 RVA: 0x00170786 File Offset: 0x0016E986
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x0017079C File Offset: 0x0016E99C
		private void ApplyVariables()
		{
			this.mat.SetFloat("_DirectionX", this.DirectionX);
			this.mat.SetFloat("_DirectionY", this.DirectionY);
			this.mat.SetFloat("_Speed", this.Speed);
			this.mat.SetFloat("_Amplitude", this.Amplitude);
		}

		// Token: 0x04002D5C RID: 11612
		[Range(0f, 1f)]
		public float DirectionX = 0.5f;

		// Token: 0x04002D5D RID: 11613
		[Range(0f, 1f)]
		public float DirectionY = 0.5f;

		// Token: 0x04002D5E RID: 11614
		public float Speed = 5f;

		// Token: 0x04002D5F RID: 11615
		[Range(-1f, 1f)]
		public float Amplitude = 0.1f;
	}
}
