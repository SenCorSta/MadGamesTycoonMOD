using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003C0 RID: 960
	[ExecuteInEditMode]
	public sealed class PP_FakeHDR : PostProcessBase
	{
		// Token: 0x060022E8 RID: 8936 RVA: 0x00017764 File Offset: 0x00015964
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/FakeHDR");
		}

		// Token: 0x060022E9 RID: 8937 RVA: 0x00017776 File Offset: 0x00015976
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x060022EA RID: 8938 RVA: 0x0001778B File Offset: 0x0001598B
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Amount", this.Amount);
			this.mat.SetFloat("_Multiplier", this.Multiplier);
		}

		// Token: 0x04002D22 RID: 11554
		[Range(0f, 1f)]
		public float Amount = 0.5f;

		// Token: 0x04002D23 RID: 11555
		[Range(0f, 1f)]
		public float Multiplier = 1f;
	}
}
