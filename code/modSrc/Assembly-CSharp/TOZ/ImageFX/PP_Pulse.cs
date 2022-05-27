using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	
	[ExecuteInEditMode]
	public sealed class PP_Pulse : PostProcessBase
	{
		
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Pulse");
		}

		
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		
		private void ApplyVariables()
		{
			this.mat.SetFloat("_DirectionX", this.DirectionX);
			this.mat.SetFloat("_DirectionY", this.DirectionY);
			this.mat.SetFloat("_Speed", this.Speed);
			this.mat.SetFloat("_Amplitude", this.Amplitude);
		}

		
		[Range(0f, 1f)]
		public float DirectionX = 0.5f;

		
		[Range(0f, 1f)]
		public float DirectionY = 0.5f;

		
		public float Speed = 5f;

		
		[Range(-1f, 1f)]
		public float Amplitude = 0.1f;
	}
}
