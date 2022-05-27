using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	
	[ExecuteInEditMode]
	public sealed class PP_RadialUndistortion : PostProcessBase
	{
		
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/RadialUndistortion");
		}

		
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		
		private void ApplyVariables()
		{
			this.mat.SetFloat("_F", this.F);
			this.mat.SetFloat("_OX", this.CenterX);
			this.mat.SetFloat("_OY", this.CenterY);
			this.mat.SetFloat("_K1", this.K1);
			this.mat.SetFloat("_K2", this.K2);
		}

		
		public float CenterX = 320f;

		
		public float CenterY = 240f;

		
		public float F = 0.9f;

		
		public float K1 = -0.27f;

		
		public float K2 = 0.08f;
	}
}
