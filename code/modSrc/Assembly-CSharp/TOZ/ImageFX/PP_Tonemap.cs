using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	
	[ExecuteInEditMode]
	public sealed class PP_Tonemap : PostProcessBase
	{
		
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Tonemap");
		}

		
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Exposure", this.Exposure);
			this.mat.SetFloat("_Gamma", this.Gamma);
		}

		
		[Range(0f, 1f)]
		public float Exposure = 0.1f;

		
		[Range(0f, 2.2f)]
		public float Gamma = 1f;
	}
}
