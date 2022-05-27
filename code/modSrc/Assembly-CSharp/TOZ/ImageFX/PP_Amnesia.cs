using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	
	[ExecuteInEditMode]
	public sealed class PP_Amnesia : PostProcessBase
	{
		
		private void Awake()
		{
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Amnesia");
		}

		
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		
		private void ApplyVariables()
		{
			this.mat.SetFloat("_Visibility", this.Visibility);
			this.mat.SetFloat("_Speed", this.Speed);
		}

		
		[Range(0f, 1f)]
		public float Visibility = 1f;

		
		public float Speed = 3f;
	}
}
