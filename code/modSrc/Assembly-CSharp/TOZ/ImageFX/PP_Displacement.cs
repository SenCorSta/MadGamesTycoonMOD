using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003BD RID: 957
	[ExecuteInEditMode]
	public sealed class PP_Displacement : PostProcessBase
	{
		// Token: 0x060022DD RID: 8925 RVA: 0x00017669 File Offset: 0x00015869
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Displacement");
		}

		// Token: 0x060022DE RID: 8926 RVA: 0x0001767B File Offset: 0x0001587B
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x060022DF RID: 8927 RVA: 0x00017690 File Offset: 0x00015890
		private void ApplyVariables()
		{
			if (this.BumpTexture != null)
			{
				this.mat.SetTexture("_BumpTex", this.BumpTexture);
			}
			this.mat.SetFloat("_Amount", this.Amount);
		}

		// Token: 0x04002D1E RID: 11550
		public Texture2D BumpTexture;

		// Token: 0x04002D1F RID: 11551
		[Range(-1f, 1f)]
		public float Amount = 0.5f;
	}
}
