using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003C9 RID: 969
	[ExecuteInEditMode]
	public sealed class PP_NightVisionV1 : PostProcessBase
	{
		// Token: 0x0600230C RID: 8972 RVA: 0x00017A73 File Offset: 0x00015C73
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/NightVisionV1");
		}

		// Token: 0x0600230D RID: 8973 RVA: 0x00017588 File Offset: 0x00015788
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			Graphics.Blit(src, dest, this.mat);
		}
	}
}
