using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003CD RID: 973
	[ExecuteInEditMode]
	public sealed class PP_Posterize : PostProcessBase
	{
		// Token: 0x0600231B RID: 8987 RVA: 0x00017B6D File Offset: 0x00015D6D
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Posterize");
		}

		// Token: 0x0600231C RID: 8988 RVA: 0x00017B7F File Offset: 0x00015D7F
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x0600231D RID: 8989 RVA: 0x00017B94 File Offset: 0x00015D94
		private void ApplyVariables()
		{
			this.mat.SetInt("_Colors", this.Colors);
			this.mat.SetFloat("_Gamma", this.Gamma);
		}

		// Token: 0x04002D44 RID: 11588
		public int Colors = 4;

		// Token: 0x04002D45 RID: 11589
		[Range(0f, 2.2f)]
		public float Gamma = 1f;
	}
}
