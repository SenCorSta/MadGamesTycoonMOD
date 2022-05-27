using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003C5 RID: 965
	[ExecuteInEditMode]
	public sealed class PP_Frost : PostProcessBase
	{
		// Token: 0x06002343 RID: 9027 RVA: 0x0016FD46 File Offset: 0x0016DF46
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Frost");
		}

		// Token: 0x06002344 RID: 9028 RVA: 0x0016FD58 File Offset: 0x0016DF58
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x06002345 RID: 9029 RVA: 0x0016FD6D File Offset: 0x0016DF6D
		private void ApplyVariables()
		{
			if (this.NoiseTex != null)
			{
				this.mat.SetTexture("_NoiseTex", this.NoiseTex);
			}
			this.mat.SetFloat("_Amount", this.Amount);
		}

		// Token: 0x04002D3B RID: 11579
		public Texture2D NoiseTex;

		// Token: 0x04002D3C RID: 11580
		public float Amount = 1f;
	}
}
