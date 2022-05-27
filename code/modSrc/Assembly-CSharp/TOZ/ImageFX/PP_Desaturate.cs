using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	
	[ExecuteInEditMode]
	public sealed class PP_Desaturate : PostProcessBase
	{
		
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Desaturate");
		}

		
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Amount", this.Amount);
		}

		
		[Range(0f, 1f)]
		public float Amount = 0.5f;
	}
}
