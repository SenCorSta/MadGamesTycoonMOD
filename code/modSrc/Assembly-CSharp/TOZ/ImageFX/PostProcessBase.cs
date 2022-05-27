using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003E2 RID: 994
	[RequireComponent(typeof(Camera))]
	public abstract class PostProcessBase : MonoBehaviour
	{
		// Token: 0x060023B5 RID: 9141 RVA: 0x001712CC File Offset: 0x0016F4CC
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

		// Token: 0x060023B6 RID: 9142 RVA: 0x0017132F File Offset: 0x0016F52F
		private void OnDisable()
		{
			if (this.mat != null)
			{
				UnityEngine.Object.DestroyImmediate(this.mat);
			}
		}

		// Token: 0x04002D84 RID: 11652
		protected Shader shd;

		// Token: 0x04002D85 RID: 11653
		protected Material mat;
	}
}
