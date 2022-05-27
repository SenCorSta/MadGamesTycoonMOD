using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003CE RID: 974
	[ExecuteInEditMode]
	public sealed class PP_Noise : PostProcessBase
	{
		// Token: 0x06002366 RID: 9062 RVA: 0x00170644 File Offset: 0x0016E844
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Noise");
		}

		// Token: 0x06002367 RID: 9063 RVA: 0x00170656 File Offset: 0x0016E856
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x06002368 RID: 9064 RVA: 0x0017066B File Offset: 0x0016E86B
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Scale", this.Scale);
		}

		// Token: 0x04002D57 RID: 11607
		[Range(0f, 2f)]
		public float Scale = 0.5f;
	}
}
