using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003B8 RID: 952
	[ExecuteInEditMode]
	public sealed class PP_BlurV : PostProcessBase
	{
		// Token: 0x060022CA RID: 8906 RVA: 0x000174AC File Offset: 0x000156AC
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/BlurV");
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x000174BE File Offset: 0x000156BE
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x060022CC RID: 8908 RVA: 0x000174D3 File Offset: 0x000156D3
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Strength", this.Strength);
		}

		// Token: 0x04002D18 RID: 11544
		public float Strength = 1f;
	}
}
