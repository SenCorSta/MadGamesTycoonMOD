using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	
	[ExecuteInEditMode]
	public sealed class PP_Pixelated : PostProcessBase
	{
		
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Pixelated");
		}

		
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		
		private void ApplyVariables()
		{
			this.mat.SetFloat("_PixWidth", (float)this.PixelWidth);
			this.mat.SetFloat("_PixHeight", (float)this.PixelHeight);
		}

		
		public int PixelWidth = 16;

		
		public int PixelHeight = 16;
	}
}
