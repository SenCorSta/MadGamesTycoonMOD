using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	
	[ExecuteInEditMode]
	public sealed class PP_NightVisionV2 : PostProcessBase
	{
		
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/NightVisionV2");
		}

		
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		
		private void ApplyVariables()
		{
			if (this.NoiseTex != null)
			{
				this.mat.SetTexture("_NoiseTex", this.NoiseTex);
			}
			this.mat.SetVector("_VisionColor", this.VisionColor);
			this.mat.SetVector("_FadeColor", this.FadeColor);
			this.mat.SetFloat("_NoiseAmount", this.NoiseAmount);
			this.mat.SetFloat("_Radius", this.Radius);
			this.mat.SetFloat("_Fade", this.Fade);
			this.mat.SetFloat("_Intensity", this.Intensity);
			this.mat.SetFloat("_Gamma", this.Gamma);
		}

		
		public Texture2D NoiseTex;

		
		public Color VisionColor = Color.green;

		
		public Color FadeColor = Color.black;

		
		public float NoiseAmount = 1f;

		
		[Range(0f, 1f)]
		public float Radius = 0.5f;

		
		[Range(0f, 1f)]
		public float Fade = 0.2f;

		
		[Range(0f, 1f)]
		public float Intensity = 0.5f;

		
		[Range(0f, 2.2f)]
		public float Gamma = 2.2f;
	}
}
