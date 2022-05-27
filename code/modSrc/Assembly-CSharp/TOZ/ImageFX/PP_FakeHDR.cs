using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	
	[ExecuteInEditMode]
	public sealed class PP_FakeHDR : PostProcessBase
	{
		
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/FakeHDR");
		}

		
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Amount", this.Amount);
			this.mat.SetFloat("_Multiplier", this.Multiplier);
		}

		
		[Range(0f, 1f)]
		public float Amount = 0.5f;

		
		[Range(0f, 1f)]
		public float Multiplier = 1f;
	}
}
