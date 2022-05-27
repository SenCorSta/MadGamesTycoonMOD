using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003D8 RID: 984
	[ExecuteInEditMode]
	public sealed class PP_SphericalV1 : PostProcessBase
	{
		// Token: 0x0600238E RID: 9102 RVA: 0x00170BDF File Offset: 0x0016EDDF
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/SphericalV1");
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x00170BF1 File Offset: 0x0016EDF1
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x06002390 RID: 9104 RVA: 0x00170C06 File Offset: 0x0016EE06
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Radius", this.Radius);
		}

		// Token: 0x04002D72 RID: 11634
		public float Radius = 1f;
	}
}
