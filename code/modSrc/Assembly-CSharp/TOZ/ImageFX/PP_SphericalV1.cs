using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003D5 RID: 981
	[ExecuteInEditMode]
	public sealed class PP_SphericalV1 : PostProcessBase
	{
		// Token: 0x0600233B RID: 9019 RVA: 0x00017E47 File Offset: 0x00016047
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/SphericalV1");
		}

		// Token: 0x0600233C RID: 9020 RVA: 0x00017E59 File Offset: 0x00016059
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x0600233D RID: 9021 RVA: 0x00017E6E File Offset: 0x0001606E
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Radius", this.Radius);
		}

		// Token: 0x04002D5C RID: 11612
		public float Radius = 1f;
	}
}
