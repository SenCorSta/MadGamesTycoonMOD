using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003E1 RID: 993
	[ExecuteInEditMode]
	public sealed class PP_Wiggle : PostProcessBase
	{
		// Token: 0x060023B1 RID: 9137 RVA: 0x00171258 File Offset: 0x0016F458
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Wiggle");
		}

		// Token: 0x060023B2 RID: 9138 RVA: 0x0017126A File Offset: 0x0016F46A
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x0017127F File Offset: 0x0016F47F
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Speed", this.Speed);
			this.mat.SetFloat("_Amplitude", this.Amplitude);
		}

		// Token: 0x04002D82 RID: 11650
		public float Speed = 10f;

		// Token: 0x04002D83 RID: 11651
		public float Amplitude = 0.05f;
	}
}
