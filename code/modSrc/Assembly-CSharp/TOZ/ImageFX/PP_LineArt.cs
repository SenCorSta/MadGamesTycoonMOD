using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003C7 RID: 967
	[ExecuteInEditMode]
	public sealed class PP_LineArt : PostProcessBase
	{
		// Token: 0x06002305 RID: 8965 RVA: 0x000179E9 File Offset: 0x00015BE9
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/LineArt");
		}

		// Token: 0x06002306 RID: 8966 RVA: 0x000179FB File Offset: 0x00015BFB
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x06002307 RID: 8967 RVA: 0x00017A10 File Offset: 0x00015C10
		private void ApplyVariables()
		{
			this.mat.SetVector("_Color", this.Color);
			this.mat.SetFloat("_Strength", this.Strength);
		}

		// Token: 0x04002D37 RID: 11575
		public Color Color = Color.black;

		// Token: 0x04002D38 RID: 11576
		public float Strength = 80f;
	}
}
