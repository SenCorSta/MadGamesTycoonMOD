using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003C3 RID: 963
	[ExecuteInEditMode]
	public sealed class PP_FakeHDR : PostProcessBase
	{
		// Token: 0x0600233B RID: 9019 RVA: 0x0016FC81 File Offset: 0x0016DE81
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/FakeHDR");
		}

		// Token: 0x0600233C RID: 9020 RVA: 0x0016FC93 File Offset: 0x0016DE93
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x0600233D RID: 9021 RVA: 0x0016FCA8 File Offset: 0x0016DEA8
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Amount", this.Amount);
			this.mat.SetFloat("_Multiplier", this.Multiplier);
		}

		// Token: 0x04002D38 RID: 11576
		[Range(0f, 1f)]
		public float Amount = 0.5f;

		// Token: 0x04002D39 RID: 11577
		[Range(0f, 1f)]
		public float Multiplier = 1f;
	}
}
