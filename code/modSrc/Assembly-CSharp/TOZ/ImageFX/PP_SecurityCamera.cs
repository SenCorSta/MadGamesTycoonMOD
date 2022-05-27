using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	
	[ExecuteInEditMode]
	public sealed class PP_SecurityCamera : PostProcessBase
	{
		
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/SecurityCamera");
		}

		
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Speed", this.Speed);
			this.mat.SetFloat("_Thickness", this.Thickness);
			this.mat.SetFloat("_Luminance", this.Luminance);
			this.mat.SetFloat("_Darkness", this.Darkness);
		}

		
		public float Speed = 2f;

		
		[Range(0f, 1f)]
		public float Thickness = 0.25f;

		
		[Range(0f, 1f)]
		public float Luminance = 0.25f;

		
		[Range(0f, 1f)]
		public float Darkness = 0.75f;
	}
}
