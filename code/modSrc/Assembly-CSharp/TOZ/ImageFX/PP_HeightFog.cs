using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003C3 RID: 963
	[ExecuteInEditMode]
	public sealed class PP_HeightFog : PostProcessBase
	{
		// Token: 0x060022F4 RID: 8948 RVA: 0x0001789F File Offset: 0x00015A9F
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/HeightFog");
			this.cam = base.GetComponent<Camera>();
			this.cam.depthTextureMode |= DepthTextureMode.Depth;
		}

		// Token: 0x060022F5 RID: 8949 RVA: 0x0016DD00 File Offset: 0x0016BF00
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

		// Token: 0x060022F6 RID: 8950 RVA: 0x0016DF04 File Offset: 0x0016C104
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

		// Token: 0x060022F7 RID: 8951 RVA: 0x0016DFD8 File Offset: 0x0016C1D8
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

		// Token: 0x04002D27 RID: 11559
		[Range(0f, 100f)]
		public float Density = 100f;

		// Token: 0x04002D28 RID: 11560
		public float Height;

		// Token: 0x04002D29 RID: 11561
		public float FallOff = 1f;

		// Token: 0x04002D2A RID: 11562
		public float Scale = 0.0025f;

		// Token: 0x04002D2B RID: 11563
		public float Speed = 0.005f;

		// Token: 0x04002D2C RID: 11564
		public Texture2D NoiseTex;

		// Token: 0x04002D2D RID: 11565
		public Color FogColor = Color.gray;

		// Token: 0x04002D2E RID: 11566
		private Camera cam;
	}
}
