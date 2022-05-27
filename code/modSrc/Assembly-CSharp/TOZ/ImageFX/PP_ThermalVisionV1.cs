using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003DA RID: 986
	[ExecuteInEditMode]
	public sealed class PP_ThermalVisionV1 : PostProcessBase
	{
		// Token: 0x06002396 RID: 9110 RVA: 0x00170C83 File Offset: 0x0016EE83
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/ThermalVisionV1");
		}

		// Token: 0x06002397 RID: 9111 RVA: 0x00170C95 File Offset: 0x0016EE95
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x00170CAC File Offset: 0x0016EEAC
		private void ApplyVariables()
		{
			this.mat.SetVector("_Color1", this.Color1);
			this.mat.SetVector("_Color2", this.Color2);
			this.mat.SetVector("_Color3", this.Color3);
		}

		// Token: 0x04002D74 RID: 11636
		public Color Color1 = Color.blue;

		// Token: 0x04002D75 RID: 11637
		public Color Color2 = Color.yellow;

		// Token: 0x04002D76 RID: 11638
		public Color Color3 = Color.red;
	}
}
