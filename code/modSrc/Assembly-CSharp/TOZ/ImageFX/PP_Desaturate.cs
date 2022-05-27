using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003BF RID: 959
	[ExecuteInEditMode]
	public sealed class PP_Desaturate : PostProcessBase
	{
		// Token: 0x0600232C RID: 9004 RVA: 0x0016FB34 File Offset: 0x0016DD34
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Desaturate");
		}

		// Token: 0x0600232D RID: 9005 RVA: 0x0016FB46 File Offset: 0x0016DD46
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x0600232E RID: 9006 RVA: 0x0016FB5B File Offset: 0x0016DD5B
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Amount", this.Amount);
		}

		// Token: 0x04002D33 RID: 11571
		[Range(0f, 1f)]
		public float Amount = 0.5f;
	}
}
