using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003B7 RID: 951
	[ExecuteInEditMode]
	public sealed class PP_Amnesia : PostProcessBase
	{
		// Token: 0x0600230D RID: 8973 RVA: 0x0016F860 File Offset: 0x0016DA60
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Amnesia");
		}

		// Token: 0x0600230E RID: 8974 RVA: 0x0016F872 File Offset: 0x0016DA72
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x0600230F RID: 8975 RVA: 0x0016F887 File Offset: 0x0016DA87
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Visibility", this.Visibility);
			this.mat.SetFloat("_Speed", this.Speed);
		}

		// Token: 0x04002D29 RID: 11561
		[Range(0f, 1f)]
		public float Visibility = 1f;

		// Token: 0x04002D2A RID: 11562
		public float Speed = 3f;
	}
}
