using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003B5 RID: 949
	[ExecuteInEditMode]
	public sealed class PP_BlackAndWhite : PostProcessBase
	{
		// Token: 0x060022BE RID: 8894 RVA: 0x000173B6 File Offset: 0x000155B6
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/BlackAndWhite");
		}

		// Token: 0x060022BF RID: 8895 RVA: 0x000173C8 File Offset: 0x000155C8
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x060022C0 RID: 8896 RVA: 0x000173DD File Offset: 0x000155DD
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Threshold", this.Threshold);
		}

		// Token: 0x04002D15 RID: 11541
		[Range(0f, 1f)]
		public float Threshold = 0.5f;
	}
}
