using System;
using UnityEngine;

namespace Suimono.Core
{
	// Token: 0x0200039A RID: 922
	public class SuimonoDepth : MonoBehaviour
	{
		// Token: 0x06002247 RID: 8775 RVA: 0x00160368 File Offset: 0x0015E568
		private void Start()
		{
			this.useMat = new Material(this.useShader);
		}

		// Token: 0x06002248 RID: 8776 RVA: 0x0016037B File Offset: 0x0015E57B
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (this.useMat != null)
			{
				Graphics.Blit(source, destination, this.useMat);
			}
		}

		// Token: 0x04002985 RID: 10629
		public Shader useShader;

		// Token: 0x04002986 RID: 10630
		private Material useMat;
	}
}
