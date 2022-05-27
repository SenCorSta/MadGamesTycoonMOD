using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	
	[ExecuteInEditMode]
	public sealed class PP_LineArt : PostProcessBase
	{
		
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/LineArt");
		}

		
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		
		private void ApplyVariables()
		{
			this.mat.SetVector("_Color", this.Color);
			this.mat.SetFloat("_Strength", this.Strength);
		}

		
		public Color Color = Color.black;

		
		public float Strength = 80f;
	}
}
