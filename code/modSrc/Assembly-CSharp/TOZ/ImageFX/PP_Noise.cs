using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003CB RID: 971
	[ExecuteInEditMode]
	public sealed class PP_Noise : PostProcessBase
	{
		// Token: 0x06002313 RID: 8979 RVA: 0x00017AAC File Offset: 0x00015CAC
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Noise");
		}

		// Token: 0x06002314 RID: 8980 RVA: 0x00017ABE File Offset: 0x00015CBE
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x06002315 RID: 8981 RVA: 0x00017AD3 File Offset: 0x00015CD3
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Scale", this.Scale);
		}

		// Token: 0x04002D41 RID: 11585
		[Range(0f, 2f)]
		public float Scale = 0.5f;
	}
}
