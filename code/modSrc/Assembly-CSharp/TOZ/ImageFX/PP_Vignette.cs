using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003DB RID: 987
	[ExecuteInEditMode]
	public sealed class PP_Vignette : PostProcessBase
	{
		// Token: 0x06002353 RID: 9043 RVA: 0x0001800F File Offset: 0x0001620F
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Vignette");
		}

		// Token: 0x06002354 RID: 9044 RVA: 0x00018021 File Offset: 0x00016221
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x00018036 File Offset: 0x00016236
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Radius", this.Radius);
			this.mat.SetFloat("_Darkness", this.Darkness);
		}

		// Token: 0x04002D68 RID: 11624
		public float Radius = 10f;

		// Token: 0x04002D69 RID: 11625
		public float Darkness = 0.5f;
	}
}
