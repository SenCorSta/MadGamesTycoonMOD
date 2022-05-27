using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	
	[ExecuteInEditMode]
	public sealed class PP_Frost : PostProcessBase
	{
		
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Frost");
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
			this.mat.SetFloat("_Amount", this.Amount);
		}

		
		public Texture2D NoiseTex;

		
		public float Amount = 1f;
	}
}
