using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003DC RID: 988
	[ExecuteInEditMode]
	public sealed class PP_Thicken : PostProcessBase
	{
		// Token: 0x0600239F RID: 9119 RVA: 0x001710DB File Offset: 0x0016F2DB
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Thicken");
		}

		// Token: 0x060023A0 RID: 9120 RVA: 0x0016FAA5 File Offset: 0x0016DCA5
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			Graphics.Blit(src, dest, this.mat);
		}
	}
}
