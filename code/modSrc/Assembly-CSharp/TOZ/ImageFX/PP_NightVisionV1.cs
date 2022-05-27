using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003CC RID: 972
	[ExecuteInEditMode]
	public sealed class PP_NightVisionV1 : PostProcessBase
	{
		// Token: 0x0600235F RID: 9055 RVA: 0x001704D2 File Offset: 0x0016E6D2
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/NightVisionV1");
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x0016FAA5 File Offset: 0x0016DCA5
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			Graphics.Blit(src, dest, this.mat);
		}
	}
}
