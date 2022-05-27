using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	
	[ExecuteInEditMode]
	public sealed class PP_HeightFog : PostProcessBase
	{
		
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/HeightFog");
			this.cam = base.GetComponent<Camera>();
			this.cam.depthTextureMode |= DepthTextureMode.Depth;
		}

		
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

		
		[Range(0f, 100f)]
		public float Density = 100f;

		
		public float Height;

		
		public float FallOff = 1f;

		
		public float Scale = 0.0025f;

		
		public float Speed = 0.005f;

		
		public Texture2D NoiseTex;

		
		public Color FogColor = Color.gray;

		
		private Camera cam;
	}
}
