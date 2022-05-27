using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003D6 RID: 982
	[ExecuteInEditMode]
	public sealed class PP_SphericalV2 : PostProcessBase
	{
		// Token: 0x0600233F RID: 9023 RVA: 0x00017E99 File Offset: 0x00016099
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/SphericalV2");
		}

		// Token: 0x06002340 RID: 9024 RVA: 0x00017EAB File Offset: 0x000160AB
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x06002341 RID: 9025 RVA: 0x00017EC0 File Offset: 0x000160C0
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Radius", this.Radius);
		}

		// Token: 0x04002D5D RID: 11613
		public float Radius = 1f;
	}
}
