using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003C8 RID: 968
	[ExecuteInEditMode]
	public sealed class PP_LensCircle : PostProcessBase
	{
		// Token: 0x06002350 RID: 9040 RVA: 0x001702F1 File Offset: 0x0016E4F1
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/LensCircle");
		}

		// Token: 0x06002351 RID: 9041 RVA: 0x00170303 File Offset: 0x0016E503
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x06002352 RID: 9042 RVA: 0x00170318 File Offset: 0x0016E518
		private void ApplyVariables()
		{
			this.mat.SetFloat("_CenterX", this.CenterX);
			this.mat.SetFloat("_CenterY", this.CenterY);
			this.mat.SetFloat("_RadiusX", this.RadiusX);
			this.mat.SetFloat("_RadiusY", this.RadiusY);
		}

		// Token: 0x04002D46 RID: 11590
		[Range(0f, 1f)]
		public float CenterX = 0.5f;

		// Token: 0x04002D47 RID: 11591
		[Range(0f, 1f)]
		public float CenterY = 0.5f;

		// Token: 0x04002D48 RID: 11592
		[Range(0f, 1f)]
		public float RadiusX = 1f;

		// Token: 0x04002D49 RID: 11593
		[Range(0f, 1f)]
		public float RadiusY;
	}
}
