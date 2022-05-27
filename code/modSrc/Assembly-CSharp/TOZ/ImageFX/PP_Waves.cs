using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003E0 RID: 992
	[ExecuteInEditMode]
	public sealed class PP_Waves : PostProcessBase
	{
		// Token: 0x060023AD RID: 9133 RVA: 0x001711E5 File Offset: 0x0016F3E5
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Waves");
		}

		// Token: 0x060023AE RID: 9134 RVA: 0x001711F7 File Offset: 0x0016F3F7
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x060023AF RID: 9135 RVA: 0x0017120C File Offset: 0x0016F40C
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Speed", this.Speed);
			this.mat.SetFloat("_Amplitude", this.Amplitude);
		}

		// Token: 0x04002D80 RID: 11648
		public float Speed = 10f;

		// Token: 0x04002D81 RID: 11649
		public float Amplitude = 0.05f;
	}
}
