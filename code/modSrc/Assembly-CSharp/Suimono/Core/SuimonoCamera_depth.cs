using System;
using UnityEngine;

namespace Suimono.Core
{
	// Token: 0x02000399 RID: 921
	[ExecuteInEditMode]
	public class SuimonoCamera_depth : MonoBehaviour
	{
		// Token: 0x06002243 RID: 8771 RVA: 0x001602B5 File Offset: 0x0015E4B5
		private void Start()
		{
			this.useMat = new Material(Shader.Find("Suimono2/SuimonoDepth"));
		}

		// Token: 0x06002244 RID: 8772 RVA: 0x001602CC File Offset: 0x0015E4CC
		private void LateUpdate()
		{
			this._sceneDepth = Mathf.Clamp(this._sceneDepth, 0f, 100f);
			this._shoreDepth = Mathf.Clamp(this._shoreDepth, 0f, 100f);
			this.useMat.SetFloat("_sceneDepth", this._sceneDepth);
			this.useMat.SetFloat("_shoreDepth", this._shoreDepth);
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x0016033B File Offset: 0x0015E53B
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			Graphics.Blit(source, destination, this.useMat);
		}

		// Token: 0x04002982 RID: 10626
		[HideInInspector]
		public float _sceneDepth = 20f;

		// Token: 0x04002983 RID: 10627
		[HideInInspector]
		public float _shoreDepth = 45f;

		// Token: 0x04002984 RID: 10628
		private Material useMat;
	}
}
