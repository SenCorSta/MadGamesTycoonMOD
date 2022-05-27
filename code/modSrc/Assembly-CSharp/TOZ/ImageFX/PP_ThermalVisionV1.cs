using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003D7 RID: 983
	[ExecuteInEditMode]
	public sealed class PP_ThermalVisionV1 : PostProcessBase
	{
		// Token: 0x06002343 RID: 9027 RVA: 0x00017EEB File Offset: 0x000160EB
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/ThermalVisionV1");
		}

		// Token: 0x06002344 RID: 9028 RVA: 0x00017EFD File Offset: 0x000160FD
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x06002345 RID: 9029 RVA: 0x0016E580 File Offset: 0x0016C780
		private void ApplyVariables()
		{
			this.mat.SetVector("_Color1", this.Color1);
			this.mat.SetVector("_Color2", this.Color2);
			this.mat.SetVector("_Color3", this.Color3);
		}

		// Token: 0x04002D5E RID: 11614
		public Color Color1 = Color.blue;

		// Token: 0x04002D5F RID: 11615
		public Color Color2 = Color.yellow;

		// Token: 0x04002D60 RID: 11616
		public Color Color3 = Color.red;
	}
}
