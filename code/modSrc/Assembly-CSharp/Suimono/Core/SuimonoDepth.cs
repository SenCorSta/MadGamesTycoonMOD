using System;
using UnityEngine;

namespace Suimono.Core
{
	// Token: 0x02000397 RID: 919
	public class SuimonoDepth : MonoBehaviour
	{
		// Token: 0x060021F4 RID: 8692 RVA: 0x00016C26 File Offset: 0x00014E26
		private void Start()
		{
			this.useMat = new Material(this.useShader);
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x00016C39 File Offset: 0x00014E39
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (this.useMat != null)
			{
				Graphics.Blit(source, destination, this.useMat);
			}
		}

		// Token: 0x0400296F RID: 10607
		public Shader useShader;

		// Token: 0x04002970 RID: 10608
		private Material useMat;
	}
}
