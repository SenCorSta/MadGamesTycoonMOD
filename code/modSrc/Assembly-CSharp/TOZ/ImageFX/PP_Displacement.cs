using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003C0 RID: 960
	[ExecuteInEditMode]
	public sealed class PP_Displacement : PostProcessBase
	{
		// Token: 0x06002330 RID: 9008 RVA: 0x0016FB86 File Offset: 0x0016DD86
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Displacement");
		}

		// Token: 0x06002331 RID: 9009 RVA: 0x0016FB98 File Offset: 0x0016DD98
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x06002332 RID: 9010 RVA: 0x0016FBAD File Offset: 0x0016DDAD
		private void ApplyVariables()
		{
			if (this.BumpTexture != null)
			{
				this.mat.SetTexture("_BumpTex", this.BumpTexture);
			}
			this.mat.SetFloat("_Amount", this.Amount);
		}

		// Token: 0x04002D34 RID: 11572
		public Texture2D BumpTexture;

		// Token: 0x04002D35 RID: 11573
		[Range(-1f, 1f)]
		public float Amount = 0.5f;
	}
}
