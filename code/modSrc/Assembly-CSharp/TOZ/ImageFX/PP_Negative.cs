using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003C8 RID: 968
	[ExecuteInEditMode]
	public sealed class PP_Negative : PostProcessBase
	{
		// Token: 0x06002309 RID: 8969 RVA: 0x00017A61 File Offset: 0x00015C61
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Negative");
		}

		// Token: 0x0600230A RID: 8970 RVA: 0x00017588 File Offset: 0x00015788
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			Graphics.Blit(src, dest, this.mat);
		}
	}
}
