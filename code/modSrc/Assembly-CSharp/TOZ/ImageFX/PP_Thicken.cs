using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003D9 RID: 985
	[ExecuteInEditMode]
	public sealed class PP_Thicken : PostProcessBase
	{
		// Token: 0x0600234C RID: 9036 RVA: 0x00017F8A File Offset: 0x0001618A
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Thicken");
		}

		// Token: 0x0600234D RID: 9037 RVA: 0x00017588 File Offset: 0x00015788
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			Graphics.Blit(src, dest, this.mat);
		}
	}
}
