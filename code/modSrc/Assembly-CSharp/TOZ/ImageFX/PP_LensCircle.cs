using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	
	[ExecuteInEditMode]
	public sealed class PP_LensCircle : PostProcessBase
	{
		
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/LensCircle");
		}

		
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		
		private void ApplyVariables()
		{
			this.mat.SetFloat("_CenterX", this.CenterX);
			this.mat.SetFloat("_CenterY", this.CenterY);
			this.mat.SetFloat("_RadiusX", this.RadiusX);
			this.mat.SetFloat("_RadiusY", this.RadiusY);
		}

		
		[Range(0f, 1f)]
		public float CenterX = 0.5f;

		
		[Range(0f, 1f)]
		public float CenterY = 0.5f;

		
		[Range(0f, 1f)]
		public float RadiusX = 1f;

		
		[Range(0f, 1f)]
		public float RadiusY;
	}
}
