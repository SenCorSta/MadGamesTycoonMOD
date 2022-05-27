using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003CB RID: 971
	[ExecuteInEditMode]
	public sealed class PP_Negative : PostProcessBase
	{
		// Token: 0x0600235C RID: 9052 RVA: 0x001704C0 File Offset: 0x0016E6C0
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Negative");
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x0016FAA5 File Offset: 0x0016DCA5
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			Graphics.Blit(src, dest, this.mat);
		}
	}
}
