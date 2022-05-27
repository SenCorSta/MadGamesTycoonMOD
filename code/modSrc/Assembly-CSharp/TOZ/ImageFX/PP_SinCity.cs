using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003D6 RID: 982
	[ExecuteInEditMode]
	public sealed class PP_SinCity : PostProcessBase
	{
		// Token: 0x06002386 RID: 9094 RVA: 0x00170ACB File Offset: 0x0016ECCB
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/SinCity");
		}

		// Token: 0x06002387 RID: 9095 RVA: 0x00170ADD File Offset: 0x0016ECDD
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x06002388 RID: 9096 RVA: 0x00170AF4 File Offset: 0x0016ECF4
		private void ApplyVariables()
		{
			this.mat.SetColor("_SelectedColor", this.SelectedColor);
			this.mat.SetColor("_ReplacedColor", this.ReplacementColor);
			this.mat.SetFloat("_Brightness", this.Brightness);
			this.mat.SetFloat("_Tolerance", this.Tolerance);
		}

		// Token: 0x04002D6D RID: 11629
		public Color SelectedColor = Color.red;

		// Token: 0x04002D6E RID: 11630
		public Color ReplacementColor = Color.white;

		// Token: 0x04002D6F RID: 11631
		[Range(0f, 1f)]
		public float Brightness = 1f;

		// Token: 0x04002D70 RID: 11632
		[Range(0f, 1f)]
		public float Tolerance = 0.5f;
	}
}
