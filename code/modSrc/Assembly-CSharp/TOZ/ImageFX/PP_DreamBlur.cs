using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	
	[ExecuteInEditMode]
	public sealed class PP_DreamBlur : PostProcessBase
	{
		
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/DreamBlur");
		}

		
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Desaturation", this.Desaturation);
			this.mat.SetFloat("_Strength", this.Strength);
		}

		
		[Range(0f, 1f)]
		public float Desaturation = 1f;

		
		public float Strength = 1f;
	}
}
