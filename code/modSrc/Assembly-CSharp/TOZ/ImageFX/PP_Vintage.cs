using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	
	[ExecuteInEditMode]
	public sealed class PP_Vintage : PostProcessBase
	{
		
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Vintage");
		}

		
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			Graphics.Blit(src, dest, this.mat);
		}
	}
}
