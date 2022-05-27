using System;
using UnityEngine;

namespace TOZ.ImageFX
{
	// Token: 0x020003DF RID: 991
	[RequireComponent(typeof(Camera))]
	public abstract class PostProcessBase : MonoBehaviour
	{
		// Token: 0x06002362 RID: 9058 RVA: 0x0016E868 File Offset: 0x0016CA68
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

		// Token: 0x06002363 RID: 9059 RVA: 0x0001817A File Offset: 0x0001637A
		private void OnDisable()
		{
			if (this.mat != null)
			{
				UnityEngine.Object.DestroyImmediate(this.mat);
			}
		}

		// Token: 0x04002D6E RID: 11630
		protected Shader shd;

		// Token: 0x04002D6F RID: 11631
		protected Material mat;
	}
}
