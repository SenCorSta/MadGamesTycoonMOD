using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003B6 RID: 950
	[ExecuteInEditMode]
	public sealed class PP_Bleach : PostProcessBase
	{
		// Token: 0x060022C2 RID: 8898 RVA: 0x00017408 File Offset: 0x00015608
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Bleach");
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x0001741A File Offset: 0x0001561A
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x0001742F File Offset: 0x0001562F
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Opacity", this.Opacity);
		}

		// Token: 0x04002D16 RID: 11542
		[Range(0f, 1f)]
		public float Opacity = 1f;
	}
}
