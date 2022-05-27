using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003B6 RID: 950
	[ExecuteInEditMode]
	public sealed class PP_4Bit : PostProcessBase
	{
		// Token: 0x06002309 RID: 8969 RVA: 0x0016F7F1 File Offset: 0x0016D9F1
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/4Bit");
		}

		// Token: 0x0600230A RID: 8970 RVA: 0x0016F803 File Offset: 0x0016DA03
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x0600230B RID: 8971 RVA: 0x0016F818 File Offset: 0x0016DA18
		private void ApplyVariables()
		{
			this.mat.SetInt("_BitDepth", this.BitDepth);
			this.mat.SetFloat("_Contrast", this.Contrast);
		}

		// Token: 0x04002D27 RID: 11559
		public int BitDepth = 2;

		// Token: 0x04002D28 RID: 11560
		[Range(0f, 1f)]
		public float Contrast = 1f;
	}
}
