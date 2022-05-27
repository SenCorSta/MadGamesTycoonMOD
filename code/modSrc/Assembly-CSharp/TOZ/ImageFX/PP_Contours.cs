using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003BA RID: 954
	[ExecuteInEditMode]
	public sealed class PP_Contours : PostProcessBase
	{
		// Token: 0x060022D2 RID: 8914 RVA: 0x00017576 File Offset: 0x00015776
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Contours");
		}

		// Token: 0x060022D3 RID: 8915 RVA: 0x00017588 File Offset: 0x00015788
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			Graphics.Blit(src, dest, this.mat);
		}
	}
}
