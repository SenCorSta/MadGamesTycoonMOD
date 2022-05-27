using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003B9 RID: 953
	[ExecuteInEditMode]
	public sealed class PP_Bleach : PostProcessBase
	{
		// Token: 0x06002315 RID: 8981 RVA: 0x0016F925 File Offset: 0x0016DB25
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Bleach");
		}

		// Token: 0x06002316 RID: 8982 RVA: 0x0016F937 File Offset: 0x0016DB37
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x06002317 RID: 8983 RVA: 0x0016F94C File Offset: 0x0016DB4C
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Opacity", this.Opacity);
		}

		// Token: 0x04002D2C RID: 11564
		[Range(0f, 1f)]
		public float Opacity = 1f;
	}
}
