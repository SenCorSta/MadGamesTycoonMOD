using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003C4 RID: 964
	[ExecuteInEditMode]
	public sealed class PP_FakeSSAO : PostProcessBase
	{
		// Token: 0x0600233F RID: 9023 RVA: 0x0016FCF4 File Offset: 0x0016DEF4
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/FakeSSAO");
		}

		// Token: 0x06002340 RID: 9024 RVA: 0x0016FD06 File Offset: 0x0016DF06
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x06002341 RID: 9025 RVA: 0x0016FD1B File Offset: 0x0016DF1B
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Amount", this.Amount);
		}

		// Token: 0x04002D3A RID: 11578
		[Range(0f, 1f)]
		public float Amount = 4f;
	}
}
