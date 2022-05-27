using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003D3 RID: 979
	[ExecuteInEditMode]
	public sealed class PP_SinCity : PostProcessBase
	{
		// Token: 0x06002333 RID: 9011 RVA: 0x00017D9A File Offset: 0x00015F9A
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/SinCity");
		}

		// Token: 0x06002334 RID: 9012 RVA: 0x00017DAC File Offset: 0x00015FAC
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x06002335 RID: 9013 RVA: 0x0016E518 File Offset: 0x0016C718
		private void ApplyVariables()
		{
			this.mat.SetColor("_SelectedColor", this.SelectedColor);
			this.mat.SetColor("_ReplacedColor", this.ReplacementColor);
			this.mat.SetFloat("_Brightness", this.Brightness);
			this.mat.SetFloat("_Tolerance", this.Tolerance);
		}

		// Token: 0x04002D57 RID: 11607
		public Color SelectedColor = Color.red;

		// Token: 0x04002D58 RID: 11608
		public Color ReplacementColor = Color.white;

		// Token: 0x04002D59 RID: 11609
		[Range(0f, 1f)]
		public float Brightness = 1f;

		// Token: 0x04002D5A RID: 11610
		[Range(0f, 1f)]
		public float Tolerance = 0.5f;
	}
}
