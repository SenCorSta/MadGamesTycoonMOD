using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	
	[ExecuteInEditMode]
	public sealed class PP_ThermalVisionV1 : PostProcessBase
	{
		
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/ThermalVisionV1");
		}

		
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		
		private void ApplyVariables()
		{
			this.mat.SetVector("_Color1", this.Color1);
			this.mat.SetVector("_Color2", this.Color2);
			this.mat.SetVector("_Color3", this.Color3);
		}

		
		public Color Color1 = Color.blue;

		
		public Color Color2 = Color.yellow;

		
		public Color Color3 = Color.red;
	}
}
