using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003CE RID: 974
	[ExecuteInEditMode]
	public sealed class PP_Pulse : PostProcessBase
	{
		// Token: 0x0600231F RID: 8991 RVA: 0x00017BDC File Offset: 0x00015DDC
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Pulse");
		}

		// Token: 0x06002320 RID: 8992 RVA: 0x00017BEE File Offset: 0x00015DEE
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x06002321 RID: 8993 RVA: 0x0016E37C File Offset: 0x0016C57C
		private void ApplyVariables()
		{
			this.mat.SetFloat("_DirectionX", this.DirectionX);
			this.mat.SetFloat("_DirectionY", this.DirectionY);
			this.mat.SetFloat("_Speed", this.Speed);
			this.mat.SetFloat("_Amplitude", this.Amplitude);
		}

		// Token: 0x04002D46 RID: 11590
		[Range(0f, 1f)]
		public float DirectionX = 0.5f;

		// Token: 0x04002D47 RID: 11591
		[Range(0f, 1f)]
		public float DirectionY = 0.5f;

		// Token: 0x04002D48 RID: 11592
		public float Speed = 5f;

		// Token: 0x04002D49 RID: 11593
		[Range(-1f, 1f)]
		public float Amplitude = 0.1f;
	}
}
