using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	
	[ExecuteInEditMode]
	public sealed class PP_LightWave : PostProcessBase
	{
		
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/LightWave");
		}

		
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Red", this.Red);
			this.mat.SetFloat("_Green", this.Green);
			this.mat.SetFloat("_Blue", this.Blue);
		}

		
		public float Red = 4f;

		
		public float Green = 4f;

		
		public float Blue = 4f;
	}
}
