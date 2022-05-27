using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	
	[ExecuteInEditMode]
	public sealed class PP_Crosshatch : PostProcessBase
	{
		
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Crosshatch");
		}

		
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		
		private void ApplyVariables()
		{
			this.mat.SetVector("_LineColor", this.LineColor);
			this.mat.SetFloat("_Strength", this.Strength);
		}

		
		[Range(1E-05f, 0.1f)]
		public float Strength = 0.01f;

		
		public Color LineColor = Color.white;
	}
}
