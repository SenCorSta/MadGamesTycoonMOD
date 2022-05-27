using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003DD RID: 989
	[ExecuteInEditMode]
	public sealed class PP_Waves : PostProcessBase
	{
		// Token: 0x0600235A RID: 9050 RVA: 0x00018094 File Offset: 0x00016294
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Waves");
		}

		// Token: 0x0600235B RID: 9051 RVA: 0x000180A6 File Offset: 0x000162A6
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x0600235C RID: 9052 RVA: 0x000180BB File Offset: 0x000162BB
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Speed", this.Speed);
			this.mat.SetFloat("_Amplitude", this.Amplitude);
		}

		// Token: 0x04002D6A RID: 11626
		public float Speed = 10f;

		// Token: 0x04002D6B RID: 11627
		public float Amplitude = 0.05f;
	}
}
