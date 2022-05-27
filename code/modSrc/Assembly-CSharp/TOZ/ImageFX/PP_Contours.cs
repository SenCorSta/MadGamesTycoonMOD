using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003BD RID: 957
	[ExecuteInEditMode]
	public sealed class PP_Contours : PostProcessBase
	{
		// Token: 0x06002325 RID: 8997 RVA: 0x0016FA93 File Offset: 0x0016DC93
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Contours");
		}

		// Token: 0x06002326 RID: 8998 RVA: 0x0016FAA5 File Offset: 0x0016DCA5
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			Graphics.Blit(src, dest, this.mat);
		}
	}
}
