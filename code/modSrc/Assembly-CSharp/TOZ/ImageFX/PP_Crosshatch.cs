using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003BE RID: 958
	[ExecuteInEditMode]
	public sealed class PP_Crosshatch : PostProcessBase
	{
		// Token: 0x06002328 RID: 9000 RVA: 0x0016FABC File Offset: 0x0016DCBC
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Crosshatch");
		}

		// Token: 0x06002329 RID: 9001 RVA: 0x0016FACE File Offset: 0x0016DCCE
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x0600232A RID: 9002 RVA: 0x0016FAE3 File Offset: 0x0016DCE3
		private void ApplyVariables()
		{
			this.mat.SetVector("_LineColor", this.LineColor);
			this.mat.SetFloat("_Strength", this.Strength);
		}

		// Token: 0x04002D31 RID: 11569
		[Range(1E-05f, 0.1f)]
		public float Strength = 0.01f;

		// Token: 0x04002D32 RID: 11570
		public Color LineColor = Color.white;
	}
}
