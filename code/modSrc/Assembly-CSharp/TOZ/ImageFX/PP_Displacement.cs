using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	
	[ExecuteInEditMode]
	public sealed class PP_Displacement : PostProcessBase
	{
		
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Displacement");
		}

		
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		
		private void ApplyVariables()
		{
			if (this.BumpTexture != null)
			{
				this.mat.SetTexture("_BumpTex", this.BumpTexture);
			}
			this.mat.SetFloat("_Amount", this.Amount);
		}

		
		public Texture2D BumpTexture;

		
		[Range(-1f, 1f)]
		public float Amount = 0.5f;
	}
}
