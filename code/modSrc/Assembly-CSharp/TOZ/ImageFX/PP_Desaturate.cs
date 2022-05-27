using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003BC RID: 956
	[ExecuteInEditMode]
	public sealed class PP_Desaturate : PostProcessBase
	{
		// Token: 0x060022D9 RID: 8921 RVA: 0x00017617 File Offset: 0x00015817
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Desaturate");
		}

		// Token: 0x060022DA RID: 8922 RVA: 0x00017629 File Offset: 0x00015829
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x060022DB RID: 8923 RVA: 0x0001763E File Offset: 0x0001583E
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Amount", this.Amount);
		}

		// Token: 0x04002D1D RID: 11549
		[Range(0f, 1f)]
		public float Amount = 0.5f;
	}
}
