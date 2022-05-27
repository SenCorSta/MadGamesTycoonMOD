using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003C9 RID: 969
	[ExecuteInEditMode]
	public sealed class PP_LightWave : PostProcessBase
	{
		// Token: 0x06002354 RID: 9044 RVA: 0x001703A6 File Offset: 0x0016E5A6
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/LightWave");
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x001703B8 File Offset: 0x0016E5B8
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x001703D0 File Offset: 0x0016E5D0
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Red", this.Red);
			this.mat.SetFloat("_Green", this.Green);
			this.mat.SetFloat("_Blue", this.Blue);
		}

		// Token: 0x04002D4A RID: 11594
		public float Red = 4f;

		// Token: 0x04002D4B RID: 11595
		public float Green = 4f;

		// Token: 0x04002D4C RID: 11596
		public float Blue = 4f;
	}
}
