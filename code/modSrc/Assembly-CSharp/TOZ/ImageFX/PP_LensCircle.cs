using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003C5 RID: 965
	[ExecuteInEditMode]
	public sealed class PP_LensCircle : PostProcessBase
	{
		// Token: 0x060022FD RID: 8957 RVA: 0x00017949 File Offset: 0x00015B49
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/LensCircle");
		}

		// Token: 0x060022FE RID: 8958 RVA: 0x0001795B File Offset: 0x00015B5B
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x060022FF RID: 8959 RVA: 0x0016E18C File Offset: 0x0016C38C
		private void ApplyVariables()
		{
			this.mat.SetFloat("_CenterX", this.CenterX);
			this.mat.SetFloat("_CenterY", this.CenterY);
			this.mat.SetFloat("_RadiusX", this.RadiusX);
			this.mat.SetFloat("_RadiusY", this.RadiusY);
		}

		// Token: 0x04002D30 RID: 11568
		[Range(0f, 1f)]
		public float CenterX = 0.5f;

		// Token: 0x04002D31 RID: 11569
		[Range(0f, 1f)]
		public float CenterY = 0.5f;

		// Token: 0x04002D32 RID: 11570
		[Range(0f, 1f)]
		public float RadiusX = 1f;

		// Token: 0x04002D33 RID: 11571
		[Range(0f, 1f)]
		public float RadiusY;
	}
}
