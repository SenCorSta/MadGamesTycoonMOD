using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003C6 RID: 966
	[ExecuteInEditMode]
	public sealed class PP_HeightFog : PostProcessBase
	{
		// Token: 0x06002347 RID: 9031 RVA: 0x0016FDBC File Offset: 0x0016DFBC
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/HeightFog");
			this.cam = base.GetComponent<Camera>();
			this.cam.depthTextureMode |= DepthTextureMode.Depth;
		}

		// Token: 0x06002348 RID: 9032 RVA: 0x0016FDF0 File Offset: 0x0016DFF0
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
			PP_HeightFog.CustomGraphicsBlit(src, dest, this.mat, 0);
		}

		// Token: 0x06002349 RID: 9033 RVA: 0x0016FFF4 File Offset: 0x0016E1F4
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

		// Token: 0x0600234A RID: 9034 RVA: 0x001700C8 File Offset: 0x0016E2C8
		private void ApplyVariables()
		{
			if (this.NoiseTex != null)
			{
				this.mat.SetTexture("_NoiseTex", this.NoiseTex);
			}
			this.mat.SetVector("_Height", new Vector4(this.Height, 1f / this.FallOff));
			this.mat.SetFloat("_Density", this.Density * 0.01f);
			this.mat.SetColor("_FogColor", this.FogColor);
			this.mat.SetFloat("_Scale", this.Scale);
			this.mat.SetFloat("_Speed", this.Speed);
		}

		// Token: 0x04002D3D RID: 11581
		[Range(0f, 100f)]
		public float Density = 100f;

		// Token: 0x04002D3E RID: 11582
		public float Height;

		// Token: 0x04002D3F RID: 11583
		public float FallOff = 1f;

		// Token: 0x04002D40 RID: 11584
		public float Scale = 0.0025f;

		// Token: 0x04002D41 RID: 11585
		public float Speed = 0.005f;

		// Token: 0x04002D42 RID: 11586
		public Texture2D NoiseTex;

		// Token: 0x04002D43 RID: 11587
		public Color FogColor = Color.gray;

		// Token: 0x04002D44 RID: 11588
		private Camera cam;
	}
}
