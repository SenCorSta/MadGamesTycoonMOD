using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003D9 RID: 985
	[ExecuteInEditMode]
	public sealed class PP_SphericalV2 : PostProcessBase
	{
		// Token: 0x06002392 RID: 9106 RVA: 0x00170C31 File Offset: 0x0016EE31
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/SphericalV2");
		}

		// Token: 0x06002393 RID: 9107 RVA: 0x00170C43 File Offset: 0x0016EE43
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x06002394 RID: 9108 RVA: 0x00170C58 File Offset: 0x0016EE58
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Radius", this.Radius);
		}

		// Token: 0x04002D73 RID: 11635
		public float Radius = 1f;
	}
}
