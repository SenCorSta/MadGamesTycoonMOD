using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003C2 RID: 962
	[ExecuteInEditMode]
	public sealed class PP_Frost : PostProcessBase
	{
		// Token: 0x060022F0 RID: 8944 RVA: 0x00017829 File Offset: 0x00015A29
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Frost");
		}

		// Token: 0x060022F1 RID: 8945 RVA: 0x0001783B File Offset: 0x00015A3B
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x060022F2 RID: 8946 RVA: 0x00017850 File Offset: 0x00015A50
		private void ApplyVariables()
		{
			if (this.NoiseTex != null)
			{
				this.mat.SetTexture("_NoiseTex", this.NoiseTex);
			}
			this.mat.SetFloat("_Amount", this.Amount);
		}

		// Token: 0x04002D25 RID: 11557
		public Texture2D NoiseTex;

		// Token: 0x04002D26 RID: 11558
		public float Amount = 1f;
	}
}
