using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003BB RID: 955
	[ExecuteInEditMode]
	public sealed class PP_BlurV : PostProcessBase
	{
		// Token: 0x0600231D RID: 8989 RVA: 0x0016F9C9 File Offset: 0x0016DBC9
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/BlurV");
		}

		// Token: 0x0600231E RID: 8990 RVA: 0x0016F9DB File Offset: 0x0016DBDB
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x0600231F RID: 8991 RVA: 0x0016F9F0 File Offset: 0x0016DBF0
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Strength", this.Strength);
		}

		// Token: 0x04002D2E RID: 11566
		public float Strength = 1f;
	}
}
