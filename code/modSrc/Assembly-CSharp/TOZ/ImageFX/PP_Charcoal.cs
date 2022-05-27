using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003BC RID: 956
	[ExecuteInEditMode]
	public sealed class PP_Charcoal : PostProcessBase
	{
		// Token: 0x06002321 RID: 8993 RVA: 0x0016FA1B File Offset: 0x0016DC1B
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Charcoal");
		}

		// Token: 0x06002322 RID: 8994 RVA: 0x0016FA2D File Offset: 0x0016DC2D
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x06002323 RID: 8995 RVA: 0x0016FA42 File Offset: 0x0016DC42
		private void ApplyVariables()
		{
			this.mat.SetVector("_LineColor", this.LineColor);
			this.mat.SetFloat("_Strength", this.Strength);
		}

		// Token: 0x04002D2F RID: 11567
		[Range(0f, 1f)]
		public float Strength = 1f;

		// Token: 0x04002D30 RID: 11568
		public Color LineColor = Color.black;
	}
}
