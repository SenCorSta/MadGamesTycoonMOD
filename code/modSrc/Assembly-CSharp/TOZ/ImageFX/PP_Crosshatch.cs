using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003BB RID: 955
	[ExecuteInEditMode]
	public sealed class PP_Crosshatch : PostProcessBase
	{
		// Token: 0x060022D5 RID: 8917 RVA: 0x0001759F File Offset: 0x0001579F
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Crosshatch");
		}

		// Token: 0x060022D6 RID: 8918 RVA: 0x000175B1 File Offset: 0x000157B1
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x060022D7 RID: 8919 RVA: 0x000175C6 File Offset: 0x000157C6
		private void ApplyVariables()
		{
			this.mat.SetVector("_LineColor", this.LineColor);
			this.mat.SetFloat("_Strength", this.Strength);
		}

		// Token: 0x04002D1B RID: 11547
		[Range(1E-05f, 0.1f)]
		public float Strength = 0.01f;

		// Token: 0x04002D1C RID: 11548
		public Color LineColor = Color.white;
	}
}
