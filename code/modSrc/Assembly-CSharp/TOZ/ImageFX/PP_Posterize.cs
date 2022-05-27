using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	
	[ExecuteInEditMode]
	public sealed class PP_Posterize : PostProcessBase
	{
		
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Posterize");
		}

		
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		
		private void ApplyVariables()
		{
			this.mat.SetInt("_Colors", this.Colors);
			this.mat.SetFloat("_Gamma", this.Gamma);
		}

		
		public int Colors = 4;

		
		[Range(0f, 2.2f)]
		public float Gamma = 1f;
	}
}
