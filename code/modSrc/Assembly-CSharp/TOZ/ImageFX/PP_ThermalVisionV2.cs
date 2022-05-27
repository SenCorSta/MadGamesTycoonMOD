using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003D8 RID: 984
	[ExecuteInEditMode]
	public sealed class PP_ThermalVisionV2 : PostProcessBase
	{
		// Token: 0x06002347 RID: 9031 RVA: 0x00017F3B File Offset: 0x0001613B
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/ThermalVisionV2");
			this.cam = base.GetComponent<Camera>();
			this.cam.depthTextureMode |= DepthTextureMode.DepthNormals;
		}

		// Token: 0x06002348 RID: 9032 RVA: 0x0016E5E0 File Offset: 0x0016C7E0
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

		// Token: 0x06002349 RID: 9033 RVA: 0x0016DF04 File Offset: 0x0016C104
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

		// Token: 0x0600234A RID: 9034 RVA: 0x0016E7E4 File Offset: 0x0016C9E4
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

		// Token: 0x04002D61 RID: 11617
		public Texture2D ThermalTex;

		// Token: 0x04002D62 RID: 11618
		public Texture2D NoiseTex;

		// Token: 0x04002D63 RID: 11619
		public float NoiseAmount = 1f;

		// Token: 0x04002D64 RID: 11620
		[Range(0f, 2.2f)]
		public float Gamma = 2.2f;

		// Token: 0x04002D65 RID: 11621
		private Camera cam;
	}
}
