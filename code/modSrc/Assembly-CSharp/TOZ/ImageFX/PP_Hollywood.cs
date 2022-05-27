using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003C4 RID: 964
	[ExecuteInEditMode]
	public sealed class PP_Hollywood : PostProcessBase
	{
		// Token: 0x060022F9 RID: 8953 RVA: 0x0001790F File Offset: 0x00015B0F
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Hollywood");
		}

		// Token: 0x060022FA RID: 8954 RVA: 0x00017921 File Offset: 0x00015B21
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		// Token: 0x060022FB RID: 8955 RVA: 0x0016E090 File Offset: 0x0016C290
		private void ApplyVariables()
		{
			Matrix4x4 zero = Matrix4x4.zero;
			zero.m00 = 0.5149f;
			zero.m01 = 0.3244f;
			zero.m02 = 0.1607f;
			zero.m03 = 0f;
			zero.m10 = 0.2654f;
			zero.m11 = 0.6704f;
			zero.m12 = 0.0642f;
			zero.m13 = 0f;
			zero.m20 = 0.0248f;
			zero.m21 = 0.1248f;
			zero.m22 = 0.8504f;
			zero.m23 = 0f;
			zero.m30 = 0f;
			zero.m31 = 0f;
			zero.m32 = 0f;
			zero.m33 = 0f;
			this.mat.SetMatrix("_MtxColor", zero);
			this.mat.SetFloat("_Threshold", this.Threshold);
		}

		// Token: 0x04002D2F RID: 11567
		[Range(0f, 1f)]
		public float Threshold = 0.25f;
	}
}
