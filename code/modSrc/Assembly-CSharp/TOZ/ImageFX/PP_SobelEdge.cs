using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003D7 RID: 983
	[ExecuteInEditMode]
	public sealed class PP_SobelEdge : PostProcessBase
	{
		// Token: 0x0600238A RID: 9098 RVA: 0x00170B8D File Offset: 0x0016ED8D
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/SobelEdge");
		}

		// Token: 0x0600238B RID: 9099 RVA: 0x00170B9F File Offset: 0x0016ED9F
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x0600238C RID: 9100 RVA: 0x00170BB4 File Offset: 0x0016EDB4
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Threshold", this.Threshold);
		}

		// Token: 0x04002D71 RID: 11633
		public float Threshold = 1f;
	}
}
