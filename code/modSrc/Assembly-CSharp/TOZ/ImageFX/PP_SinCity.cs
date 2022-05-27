using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	
	[ExecuteInEditMode]
	public sealed class PP_SinCity : PostProcessBase
	{
		
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/SinCity");
		}

		
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		
		private void ApplyVariables()
		{
			this.mat.SetColor("_SelectedColor", this.SelectedColor);
			this.mat.SetColor("_ReplacedColor", this.ReplacementColor);
			this.mat.SetFloat("_Brightness", this.Brightness);
			this.mat.SetFloat("_Tolerance", this.Tolerance);
		}

		
		public Color SelectedColor = Color.red;

		
		public Color ReplacementColor = Color.white;

		
		[Range(0f, 1f)]
		public float Brightness = 1f;

		
		[Range(0f, 1f)]
		public float Tolerance = 0.5f;
	}
}
