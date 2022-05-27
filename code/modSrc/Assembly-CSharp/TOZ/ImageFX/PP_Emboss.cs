using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003BF RID: 959
	[ExecuteInEditMode]
	public sealed class PP_Emboss : PostProcessBase
	{
		// Token: 0x060022E5 RID: 8933 RVA: 0x00017752 File Offset: 0x00015952
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Emboss");
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x00017588 File Offset: 0x00015788
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			Graphics.Blit(src, dest, this.mat);
		}
	}
}
