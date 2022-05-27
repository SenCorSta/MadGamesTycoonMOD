using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003C1 RID: 961
	[ExecuteInEditMode]
	public sealed class PP_DreamBlur : PostProcessBase
	{
		// Token: 0x06002334 RID: 9012 RVA: 0x0016FBFC File Offset: 0x0016DDFC
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/DreamBlur");
		}

		// Token: 0x06002335 RID: 9013 RVA: 0x0016FC0E File Offset: 0x0016DE0E
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x06002336 RID: 9014 RVA: 0x0016FC23 File Offset: 0x0016DE23
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Desaturation", this.Desaturation);
			this.mat.SetFloat("_Strength", this.Strength);
		}

		// Token: 0x04002D36 RID: 11574
		[Range(0f, 1f)]
		public float Desaturation = 1f;

		// Token: 0x04002D37 RID: 11575
		public float Strength = 1f;
	}
}
