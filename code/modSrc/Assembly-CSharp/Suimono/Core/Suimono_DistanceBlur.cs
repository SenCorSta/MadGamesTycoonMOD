using System;
using UnityEngine;

namespace Suimono.Core
{
	// Token: 0x020003A0 RID: 928
	public class Suimono_DistanceBlur : MonoBehaviour
	{
		// Token: 0x06002298 RID: 8856 RVA: 0x00169484 File Offset: 0x00167684
		private void Start()
		{
			this.CreateMaterial();
		}

		// Token: 0x06002299 RID: 8857 RVA: 0x0016948C File Offset: 0x0016768C
		private void CreateMaterial()
		{
			if (this.material == null)
			{
				this.material = new Material(this.blurShader);
				this.material.hideFlags = HideFlags.DontSave;
			}
		}

		// Token: 0x0600229A RID: 8858 RVA: 0x001694BA File Offset: 0x001676BA
		public void OnDisable()
		{
			if (this.material)
			{
				UnityEngine.Object.DestroyImmediate(this.material);
			}
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x001694D4 File Offset: 0x001676D4
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (this.material == null)
			{
				this.CreateMaterial();
			}
			this.iterations = Mathf.FloorToInt(Mathf.Lerp(0f, 2f, this.blurAmt));
			this.downsample = Mathf.FloorToInt(Mathf.Lerp(0f, 2f, this.blurAmt));
			this.blurSpread = Mathf.Lerp(0f, 2f, this.blurAmt);
			float num = 1f / (1f * (float)(1 << this.downsample));
			this.material.SetVector("_Parameter", new Vector4(this.blurSpread * num, -this.blurSpread * num, 0f, 0f));
			source.filterMode = FilterMode.Bilinear;
			int width = source.width >> this.downsample;
			int height = source.height >> this.downsample;
			RenderTexture renderTexture = RenderTexture.GetTemporary(width, height, 0, source.format);
			renderTexture.filterMode = FilterMode.Bilinear;
			Graphics.Blit(source, renderTexture, this.material, 0);
			int num2 = 0;
			for (int i = 0; i < this.iterations; i++)
			{
				float num3 = (float)i * 1f;
				this.material.SetVector("_Parameter", new Vector4(this.blurAmt * num + num3, -this.blurAmt * num - num3, 0f, 0f));
				RenderTexture temporary = RenderTexture.GetTemporary(width, height, 0, source.format);
				temporary.filterMode = FilterMode.Bilinear;
				Graphics.Blit(renderTexture, temporary, this.material, 1 + num2);
				RenderTexture.ReleaseTemporary(renderTexture);
				renderTexture = temporary;
				temporary = RenderTexture.GetTemporary(width, height, 0, source.format);
				temporary.filterMode = FilterMode.Bilinear;
				Graphics.Blit(renderTexture, temporary, this.material, 2 + num2);
				RenderTexture.ReleaseTemporary(renderTexture);
				renderTexture = temporary;
			}
			Graphics.Blit(renderTexture, destination);
			RenderTexture.ReleaseTemporary(renderTexture);
		}

		// Token: 0x04002B90 RID: 11152
		public float blurAmt;

		// Token: 0x04002B91 RID: 11153
		public int iterations = 3;

		// Token: 0x04002B92 RID: 11154
		public float blurSpread = 0.6f;

		// Token: 0x04002B93 RID: 11155
		public Shader blurShader;

		// Token: 0x04002B94 RID: 11156
		public Material material;

		// Token: 0x04002B95 RID: 11157
		private float offc;

		// Token: 0x04002B96 RID: 11158
		private float off;

		// Token: 0x04002B97 RID: 11159
		private int rtW;

		// Token: 0x04002B98 RID: 11160
		private int rtH;

		// Token: 0x04002B99 RID: 11161
		private int i;

		// Token: 0x04002B9A RID: 11162
		private RenderTexture buffer;

		// Token: 0x04002B9B RID: 11163
		private RenderTexture buffer2;

		// Token: 0x04002B9C RID: 11164
		[Range(0f, 2f)]
		public int downsample = 1;

		// Token: 0x04002B9D RID: 11165
		[Range(0f, 10f)]
		public float blurSize = 3f;
	}
}
