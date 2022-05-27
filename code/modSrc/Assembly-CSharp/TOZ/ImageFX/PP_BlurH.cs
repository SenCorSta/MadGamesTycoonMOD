using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003B7 RID: 951
	[ExecuteInEditMode]
	public sealed class PP_BlurH : PostProcessBase
	{
		// Token: 0x060022C6 RID: 8902 RVA: 0x0001745A File Offset: 0x0001565A
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/BlurH");
		}

		// Token: 0x060022C7 RID: 8903 RVA: 0x0001746C File Offset: 0x0001566C
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x060022C8 RID: 8904 RVA: 0x00017481 File Offset: 0x00015681
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Strength", this.Strength);
		}

		// Token: 0x04002D17 RID: 11543
		public float Strength = 1f;
	}
}
