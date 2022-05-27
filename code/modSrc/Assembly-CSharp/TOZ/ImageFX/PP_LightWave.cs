using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003C6 RID: 966
	[ExecuteInEditMode]
	public sealed class PP_LightWave : PostProcessBase
	{
		// Token: 0x06002301 RID: 8961 RVA: 0x00017999 File Offset: 0x00015B99
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/LightWave");
		}

		// Token: 0x06002302 RID: 8962 RVA: 0x000179AB File Offset: 0x00015BAB
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x06002303 RID: 8963 RVA: 0x0016E1F4 File Offset: 0x0016C3F4
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Red", this.Red);
			this.mat.SetFloat("_Green", this.Green);
			this.mat.SetFloat("_Blue", this.Blue);
		}

		// Token: 0x04002D34 RID: 11572
		public float Red = 4f;

		// Token: 0x04002D35 RID: 11573
		public float Green = 4f;

		// Token: 0x04002D36 RID: 11574
		public float Blue = 4f;
	}
}
