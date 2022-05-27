using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003DF RID: 991
	[ExecuteInEditMode]
	public sealed class PP_Vintage : PostProcessBase
	{
		// Token: 0x060023AA RID: 9130 RVA: 0x001711D3 File Offset: 0x0016F3D3
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Vintage");
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x0016FAA5 File Offset: 0x0016DCA5
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			Graphics.Blit(src, dest, this.mat);
		}
	}
}
