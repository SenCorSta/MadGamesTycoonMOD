using System;
using UnityEngine;

namespace Suimono.Core
{
	
	public class SuimonoDepth : MonoBehaviour
	{
		
		private void Start()
		{
			this.useMat = new Material(this.useShader);
		}

		
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (this.useMat != null)
			{
				Graphics.Blit(source, destination, this.useMat);
			}
		}

		
		public Shader useShader;

		
		private Material useMat;
	}
}
