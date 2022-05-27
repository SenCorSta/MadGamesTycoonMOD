﻿using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	
	[ExecuteInEditMode]
	public sealed class PP_Negative : PostProcessBase
	{
		
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Negative");
		}

		
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			Graphics.Blit(src, dest, this.mat);
		}
	}
}
