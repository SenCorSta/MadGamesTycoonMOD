using System;
using UnityEngine;

namespace Suimono.Core
{
	// Token: 0x02000396 RID: 918
	[ExecuteInEditMode]
	public class SuimonoCamera_depth : MonoBehaviour
	{
		// Token: 0x060021F0 RID: 8688 RVA: 0x00016BE2 File Offset: 0x00014DE2
		private void Start()
		{
			this.useMat = new Material(Shader.Find("Suimono2/SuimonoDepth"));
		}

		// Token: 0x060021F1 RID: 8689 RVA: 0x0015EF94 File Offset: 0x0015D194
		private void LateUpdate()
		{
			this._sceneDepth = Mathf.Clamp(this._sceneDepth, 0f, 100f);
			this._shoreDepth = Mathf.Clamp(this._shoreDepth, 0f, 100f);
			this.useMat.SetFloat("_sceneDepth", this._sceneDepth);
			this.useMat.SetFloat("_shoreDepth", this._shoreDepth);
		}

		// Token: 0x060021F2 RID: 8690 RVA: 0x00016BF9 File Offset: 0x00014DF9
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			Graphics.Blit(source, destination, this.useMat);
		}

		// Token: 0x0400296C RID: 10604
		[HideInInspector]
		public float _sceneDepth = 20f;

		// Token: 0x0400296D RID: 10605
		[HideInInspector]
		public float _shoreDepth = 45f;

		// Token: 0x0400296E RID: 10606
		private Material useMat;
	}
}
