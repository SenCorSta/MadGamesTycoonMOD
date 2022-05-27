using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003D5 RID: 981
	[ExecuteInEditMode]
	public sealed class PP_SimpleBloom : PostProcessBase
	{
		// Token: 0x06002382 RID: 9090 RVA: 0x00170A79 File Offset: 0x0016EC79
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/SimpleBloom");
		}

		// Token: 0x06002383 RID: 9091 RVA: 0x00170A8B File Offset: 0x0016EC8B
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x06002384 RID: 9092 RVA: 0x00170AA0 File Offset: 0x0016ECA0
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Gamma", this.Gamma);
		}

		// Token: 0x04002D6C RID: 11628
		[Range(0f, 2.2f)]
		public float Gamma = 2.2f;
	}
}
