using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003C2 RID: 962
	[ExecuteInEditMode]
	public sealed class PP_Emboss : PostProcessBase
	{
		// Token: 0x06002338 RID: 9016 RVA: 0x0016FC6F File Offset: 0x0016DE6F
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Emboss");
		}

		// Token: 0x06002339 RID: 9017 RVA: 0x0016FAA5 File Offset: 0x0016DCA5
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			Graphics.Blit(src, dest, this.mat);
		}
	}
}
