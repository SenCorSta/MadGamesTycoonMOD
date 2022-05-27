using System;
using UnityEngine;

namespace Suimono.Core
{
	
	[ExecuteInEditMode]
	public class SuimonoCamera_depth : MonoBehaviour
	{
		
		private void Start()
		{
			this.useMat = new Material(Shader.Find("Suimono2/SuimonoDepth"));
		}

		
		private void LateUpdate()
		{
			this._sceneDepth = Mathf.Clamp(this._sceneDepth, 0f, 100f);
			this._shoreDepth = Mathf.Clamp(this._shoreDepth, 0f, 100f);
			this.useMat.SetFloat("_sceneDepth", this._sceneDepth);
			this.useMat.SetFloat("_shoreDepth", this._shoreDepth);
		}

		
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			Graphics.Blit(source, destination, this.useMat);
		}

		
		[HideInInspector]
		public float _sceneDepth = 20f;

		
		[HideInInspector]
		public float _shoreDepth = 45f;

		
		private Material useMat;
	}
}
