using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003DB RID: 987
	[ExecuteInEditMode]
	public sealed class PP_ThermalVisionV2 : PostProcessBase
	{
		// Token: 0x0600239A RID: 9114 RVA: 0x00170D33 File Offset: 0x0016EF33
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/ThermalVisionV2");
			this.cam = base.GetComponent<Camera>();
			this.cam.depthTextureMode |= DepthTextureMode.DepthNormals;
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x00170D64 File Offset: 0x0016EF64
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			if (!base.enabled)
			{
				Graphics.Blit(src, dest);
				return;
			}
			float nearClipPlane = this.cam.nearClipPlane;
			float farClipPlane = this.cam.farClipPlane;
			float num = this.cam.fieldOfView * 0.5f;
			float aspect = this.cam.aspect;
			Matrix4x4 identity = Matrix4x4.identity;
			Vector3 b = base.transform.right * nearClipPlane * Mathf.Tan(num * 0.017453292f) * aspect;
			Vector3 b2 = base.transform.up * nearClipPlane * Mathf.Tan(num * 0.017453292f);
			Vector3 vector = base.transform.forward * nearClipPlane - b + b2;
			float d = vector.magnitude * farClipPlane / nearClipPlane;
			vector.Normalize();
			vector *= d;
			identity.SetRow(0, vector);
			vector = base.transform.forward * nearClipPlane + b + b2;
			vector.Normalize();
			vector *= d;
			identity.SetRow(1, vector);
			vector = base.transform.forward * nearClipPlane + b - b2;
			vector.Normalize();
			vector *= d;
			identity.SetRow(2, vector);
			vector = base.transform.forward * nearClipPlane - b - b2;
			vector.Normalize();
			vector *= d;
			identity.SetRow(3, vector);
			this.mat.SetMatrix("_WS_FrustumCorners", identity);
			this.mat.SetVector("_WS_CameraPosition", base.transform.position);
			this.ApplyVariables();
			PP_ThermalVisionV2.CustomGraphicsBlit(src, dest, this.mat, 0);
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x00170F68 File Offset: 0x0016F168
		private static void CustomGraphicsBlit(RenderTexture source, RenderTexture dest, Material mat, int pass)
		{
			RenderTexture.active = dest;
			mat.SetTexture("_MainTex", source);
			GL.PushMatrix();
			GL.LoadOrtho();
			mat.SetPass(pass);
			GL.Begin(7);
			GL.MultiTexCoord2(0, 0f, 0f);
			GL.Vertex3(0f, 0f, 3f);
			GL.MultiTexCoord2(0, 1f, 0f);
			GL.Vertex3(1f, 0f, 2f);
			GL.MultiTexCoord2(0, 1f, 1f);
			GL.Vertex3(1f, 1f, 1f);
			GL.MultiTexCoord2(0, 0f, 1f);
			GL.Vertex3(0f, 1f, 0f);
			GL.End();
			GL.PopMatrix();
		}

		// Token: 0x0600239D RID: 9117 RVA: 0x0017103C File Offset: 0x0016F23C
		private void ApplyVariables()
		{
			if (this.NoiseTex != null)
			{
				this.mat.SetTexture("_NoiseTex", this.NoiseTex);
			}
			if (this.ThermalTex != null)
			{
				this.mat.SetTexture("_ThermalTex", this.ThermalTex);
			}
			this.mat.SetFloat("_NoiseAmount", this.NoiseAmount);
			this.mat.SetFloat("_Gamma", this.Gamma);
		}

		// Token: 0x04002D77 RID: 11639
		public Texture2D ThermalTex;

		// Token: 0x04002D78 RID: 11640
		public Texture2D NoiseTex;

		// Token: 0x04002D79 RID: 11641
		public float NoiseAmount = 1f;

		// Token: 0x04002D7A RID: 11642
		[Range(0f, 2.2f)]
		public float Gamma = 2.2f;

		// Token: 0x04002D7B RID: 11643
		private Camera cam;
	}
}
