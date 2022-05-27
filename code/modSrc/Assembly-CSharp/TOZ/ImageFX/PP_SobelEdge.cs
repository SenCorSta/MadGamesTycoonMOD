using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003D4 RID: 980
	[ExecuteInEditMode]
	public sealed class PP_SobelEdge : PostProcessBase
	{
		// Token: 0x06002337 RID: 9015 RVA: 0x00017DF5 File Offset: 0x00015FF5
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/SobelEdge");
		}

		// Token: 0x06002338 RID: 9016 RVA: 0x00017E07 File Offset: 0x00016007
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x06002339 RID: 9017 RVA: 0x00017E1C File Offset: 0x0001601C
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Threshold", this.Threshold);
		}

		// Token: 0x04002D5B RID: 11611
		public float Threshold = 1f;
	}
}
