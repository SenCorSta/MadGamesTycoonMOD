using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003B9 RID: 953
	[ExecuteInEditMode]
	public sealed class PP_Charcoal : PostProcessBase
	{
		// Token: 0x060022CE RID: 8910 RVA: 0x000174FE File Offset: 0x000156FE
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Charcoal");
		}

		// Token: 0x060022CF RID: 8911 RVA: 0x00017510 File Offset: 0x00015710
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x060022D0 RID: 8912 RVA: 0x00017525 File Offset: 0x00015725
		private void ApplyVariables()
		{
			this.mat.SetVector("_LineColor", this.LineColor);
			this.mat.SetFloat("_Strength", this.Strength);
		}

		// Token: 0x04002D19 RID: 11545
		[Range(0f, 1f)]
		public float Strength = 1f;

		// Token: 0x04002D1A RID: 11546
		public Color LineColor = Color.black;
	}
}
