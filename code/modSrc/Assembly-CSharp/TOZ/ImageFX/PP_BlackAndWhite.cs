using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003B8 RID: 952
	[ExecuteInEditMode]
	public sealed class PP_BlackAndWhite : PostProcessBase
	{
		// Token: 0x06002311 RID: 8977 RVA: 0x0016F8D3 File Offset: 0x0016DAD3
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/BlackAndWhite");
		}

		// Token: 0x06002312 RID: 8978 RVA: 0x0016F8E5 File Offset: 0x0016DAE5
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x06002313 RID: 8979 RVA: 0x0016F8FA File Offset: 0x0016DAFA
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Threshold", this.Threshold);
		}

		// Token: 0x04002D2B RID: 11563
		[Range(0f, 1f)]
		public float Threshold = 0.5f;
	}
}
