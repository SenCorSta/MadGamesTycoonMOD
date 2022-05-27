using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003CA RID: 970
	[ExecuteInEditMode]
	public sealed class PP_LineArt : PostProcessBase
	{
		// Token: 0x06002358 RID: 9048 RVA: 0x00170448 File Offset: 0x0016E648
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/LineArt");
		}

		// Token: 0x06002359 RID: 9049 RVA: 0x0017045A File Offset: 0x0016E65A
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x0600235A RID: 9050 RVA: 0x0017046F File Offset: 0x0016E66F
		private void ApplyVariables()
		{
			this.mat.SetVector("_Color", this.Color);
			this.mat.SetFloat("_Strength", this.Strength);
		}

		// Token: 0x04002D4D RID: 11597
		public Color Color = Color.black;

		// Token: 0x04002D4E RID: 11598
		public float Strength = 80f;
	}
}
