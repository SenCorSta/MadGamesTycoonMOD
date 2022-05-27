using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003BE RID: 958
	[ExecuteInEditMode]
	public sealed class PP_DreamBlur : PostProcessBase
	{
		// Token: 0x060022E1 RID: 8929 RVA: 0x000176DF File Offset: 0x000158DF
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/DreamBlur");
		}

		// Token: 0x060022E2 RID: 8930 RVA: 0x000176F1 File Offset: 0x000158F1
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x060022E3 RID: 8931 RVA: 0x00017706 File Offset: 0x00015906
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Desaturation", this.Desaturation);
			this.mat.SetFloat("_Strength", this.Strength);
		}

		// Token: 0x04002D20 RID: 11552
		[Range(0f, 1f)]
		public float Desaturation = 1f;

		// Token: 0x04002D21 RID: 11553
		public float Strength = 1f;
	}
}
