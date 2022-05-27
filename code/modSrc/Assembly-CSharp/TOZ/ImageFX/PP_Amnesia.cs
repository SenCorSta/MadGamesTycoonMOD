using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003B4 RID: 948
	[ExecuteInEditMode]
	public sealed class PP_Amnesia : PostProcessBase
	{
		// Token: 0x060022BA RID: 8890 RVA: 0x00017343 File Offset: 0x00015543
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Amnesia");
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x00017355 File Offset: 0x00015555
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x0001736A File Offset: 0x0001556A
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Visibility", this.Visibility);
			this.mat.SetFloat("_Speed", this.Speed);
		}

		// Token: 0x04002D13 RID: 11539
		[Range(0f, 1f)]
		public float Visibility = 1f;

		// Token: 0x04002D14 RID: 11540
		public float Speed = 3f;
	}
}
