using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003DC RID: 988
	[ExecuteInEditMode]
	public sealed class PP_Vintage : PostProcessBase
	{
		// Token: 0x06002357 RID: 9047 RVA: 0x00018082 File Offset: 0x00016282
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Vintage");
		}

		// Token: 0x06002358 RID: 9048 RVA: 0x00017588 File Offset: 0x00015788
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			Graphics.Blit(src, dest, this.mat);
		}
	}
}
