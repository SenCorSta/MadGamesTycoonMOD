using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003D2 RID: 978
	[ExecuteInEditMode]
	public sealed class PP_SimpleBloom : PostProcessBase
	{
		// Token: 0x0600232F RID: 9007 RVA: 0x00017D48 File Offset: 0x00015F48
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/SimpleBloom");
		}

		// Token: 0x06002330 RID: 9008 RVA: 0x00017D5A File Offset: 0x00015F5A
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x06002331 RID: 9009 RVA: 0x00017D6F File Offset: 0x00015F6F
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Gamma", this.Gamma);
		}

		// Token: 0x04002D56 RID: 11606
		[Range(0f, 2.2f)]
		public float Gamma = 2.2f;
	}
}
