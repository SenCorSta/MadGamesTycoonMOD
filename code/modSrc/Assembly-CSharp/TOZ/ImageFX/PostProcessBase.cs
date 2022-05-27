using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	
	[RequireComponent(typeof(Camera))]
	public abstract class PostProcessBase : MonoBehaviour
	{
		
		private void OnEnable()
		{
			if (!SystemInfo.supportsImageEffects || this.shd == null || !this.shd.isSupported)
			{
				base.enabled = false;
				return;
			}
			if (this.mat == null)
			{
				this.mat = new Material(this.shd);
				this.mat.hideFlags = HideFlags.HideAndDontSave;
			}
		}

		
		private void OnDisable()
		{
			if (this.mat != null)
			{
				UnityEngine.Object.DestroyImmediate(this.mat);
			}
		}

		
		protected Shader shd;

		
		protected Material mat;
	}
}
