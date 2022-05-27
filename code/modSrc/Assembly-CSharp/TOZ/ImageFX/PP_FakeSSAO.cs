using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003C1 RID: 961
	[ExecuteInEditMode]
	public sealed class PP_FakeSSAO : PostProcessBase
	{
		// Token: 0x060022EC RID: 8940 RVA: 0x000177D7 File Offset: 0x000159D7
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/FakeSSAO");
		}

		// Token: 0x060022ED RID: 8941 RVA: 0x000177E9 File Offset: 0x000159E9
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x060022EE RID: 8942 RVA: 0x000177FE File Offset: 0x000159FE
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Amount", this.Amount);
		}

		// Token: 0x04002D24 RID: 11556
		[Range(0f, 1f)]
		public float Amount = 4f;
	}
}
