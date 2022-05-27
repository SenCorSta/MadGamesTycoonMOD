using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	
	[ExecuteInEditMode]
	public sealed class PP_4Bit : PostProcessBase
	{
		
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/4Bit");
		}

		
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		
		private void ApplyVariables()
		{
			this.mat.SetInt("_BitDepth", this.BitDepth);
			this.mat.SetFloat("_Contrast", this.Contrast);
		}

		
		public int BitDepth = 2;

		
		[Range(0f, 1f)]
		public float Contrast = 1f;
	}
}
