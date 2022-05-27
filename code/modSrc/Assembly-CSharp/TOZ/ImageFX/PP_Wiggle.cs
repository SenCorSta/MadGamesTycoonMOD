using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003DE RID: 990
	[ExecuteInEditMode]
	public sealed class PP_Wiggle : PostProcessBase
	{
		// Token: 0x0600235E RID: 9054 RVA: 0x00018107 File Offset: 0x00016307
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Wiggle");
		}

		// Token: 0x0600235F RID: 9055 RVA: 0x00018119 File Offset: 0x00016319
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x0001812E File Offset: 0x0001632E
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Speed", this.Speed);
			this.mat.SetFloat("_Amplitude", this.Amplitude);
		}

		// Token: 0x04002D6C RID: 11628
		public float Speed = 10f;

		// Token: 0x04002D6D RID: 11629
		public float Amplitude = 0.05f;
	}
}
